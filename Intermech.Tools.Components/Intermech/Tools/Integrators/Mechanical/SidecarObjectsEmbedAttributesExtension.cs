// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.SidecarObjectsEmbedAttributesExtension
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data;
using Intermech.Interfaces;
using Intermech.Interfaces.Data.SidecarObjects;
using System;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

public class SidecarObjectsEmbedAttributesExtension : ISidecarObjectsEmbedAttributesExtension
{
  private readonly MechanicalEmbedAttributesDriver driver;
  private readonly SidecarObjectsIDCache sidecarIDCache;
  private readonly SidecarObjectsOperations sidecarOperations;
  private bool isActive;
  private long currentDocumentId;
  private int currentDocumentTypeId;
  private bool isSourceDocumentChanged;

  public SidecarObjectsEmbedAttributesExtension(
    MechanicalEmbedAttributesDriver driver,
    SidecarObjectsIDCache sidecarIDCache)
  {
    if (driver == null)
      throw new ArgumentNullException(nameof (driver));
    if (sidecarIDCache == null)
      throw new ArgumentNullException(nameof (sidecarIDCache));
    this.driver = driver;
    this.sidecarIDCache = sidecarIDCache;
    this.sidecarOperations = new SidecarObjectsOperations(sidecarIDCache);
  }

  public SidecarObjectsIDCache SidecarIDCache => this.sidecarIDCache;

  public virtual void Initialize(long documentId, int documentTypeId)
  {
    this.isActive = this.IsSourceDocument(documentId, documentTypeId);
    this.currentDocumentId = documentId;
    this.currentDocumentTypeId = documentTypeId;
    this.isSourceDocumentChanged = false;
  }

  public virtual void Cleanup()
  {
    this.isActive = false;
    this.currentDocumentId = 0L;
    this.currentDocumentTypeId = -1;
    this.isSourceDocumentChanged = false;
  }

  protected virtual bool IsSourceDocument(long documentId, int documentTypeId) => true;

  public virtual void AfterEmbedAttributes(
    long documentId,
    int documentType,
    string documentFilePath,
    ValueBag documentAttributes)
  {
  }

  public virtual void AfterSaveModifiedDocument(IOpenDocument document)
  {
    if (!this.isActive || this.isSourceDocumentChanged)
      return;
    this.isSourceDocumentChanged = true;
  }

  public virtual void AfterFlushChanges()
  {
    if (!this.isActive || !this.isSourceDocumentChanged)
      return;
    this.MakeSidecarFileOutdated();
  }

  protected virtual void MakeSidecarFileOutdated()
  {
    long num = this.sidecarOperations.Find(this.currentDocumentId);
    if (Consts.IsUndefinedObjectId(num))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttribute attributeById = sessionKeeper.Session.GetObject(num, true).GetAttributeByID(this.sidecarIDCache.ContentStatus.Id);
      if (attributeById.AsInteger != 1L)
        return;
      attributeById.AsInteger = 2L;
    }
  }
}
