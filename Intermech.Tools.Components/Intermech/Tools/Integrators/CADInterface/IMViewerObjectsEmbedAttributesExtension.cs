// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.IMViewerObjectsEmbedAttributesExtension
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces.Data.SidecarObjects;
using Intermech.Tools.Integrators.Mechanical;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

internal sealed class IMViewerObjectsEmbedAttributesExtension : 
  SidecarObjectsEmbedAttributesExtension
{
  private readonly ICADSettingsService integratorSettingsService;
  private CADSettings cadSettings;

  public IMViewerObjectsEmbedAttributesExtension(
    CIEmbedAttributesDriver driver,
    IMViewerObjectsIDCache imvIDCache,
    ICADSettingsService integratorSettingsService)
    : base((MechanicalEmbedAttributesDriver) driver, (SidecarObjectsIDCache) imvIDCache)
  {
    this.integratorSettingsService = integratorSettingsService != null ? integratorSettingsService : throw new ArgumentNullException(nameof (integratorSettingsService));
  }

  private CADSettings CADSettings
  {
    [DebuggerStepThrough] get
    {
      if (this.cadSettings == null)
        this.cadSettings = this.integratorSettingsService.GetCADSettings();
      return this.cadSettings;
    }
  }

  public override void Cleanup()
  {
    base.Cleanup();
    this.ResetCachedValues();
  }

  private void ResetCachedValues()
  {
    if (this.cadSettings == null)
      return;
    this.cadSettings = (CADSettings) null;
  }

  protected override bool IsSourceDocument(long documentId, int documentTypeId)
  {
    DocumentGroup byDocumentType = this.CADSettings.FileDocumentGroups.FindByDocumentType(documentTypeId, false);
    return byDocumentType != null && (!(byDocumentType.Name != "Assembly") || !(byDocumentType.Name != "Part")) && base.IsSourceDocument(documentId, documentTypeId);
  }
}
