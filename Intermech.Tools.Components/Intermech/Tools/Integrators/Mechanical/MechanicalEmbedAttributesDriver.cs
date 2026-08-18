// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.MechanicalEmbedAttributesDriver
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

public abstract class MechanicalEmbedAttributesDriver : DocumentEmbedAttributesDriver
{
  private List<ISidecarObjectsEmbedAttributesExtension> sidecarObjectsExtensions;

  /// <summary>Создает объект.</summary>
  /// <param name="integrator">Ссылка на объект интегратора</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на объект интегратора не может быть null</exception>
  protected MechanicalEmbedAttributesDriver(IIntegrator integrator)
    : base(integrator)
  {
    this.sidecarObjectsExtensions = new List<ISidecarObjectsEmbedAttributesExtension>();
  }

  /// <summary>
  /// Возвращает коллекцию расширений для обновления ассоциированных объектов IPS.
  /// Ассоциированные объекты - это вспомогательные объекты, связанные с исходными объектами
  /// косвенной связью (например, через содержимое файла исходного объекта).
  /// </summary>
  public ICollection<ISidecarObjectsEmbedAttributesExtension> SidecarObjectsExtensions
  {
    get => (ICollection<ISidecarObjectsEmbedAttributesExtension>) this.sidecarObjectsExtensions;
  }

  protected override void InitializeDriver(long documentId, int documentTypeId)
  {
    base.InitializeDriver(documentId, documentTypeId);
    this.InitializeSidecarObjectsExtensions(documentId, documentTypeId);
  }

  private void InitializeSidecarObjectsExtensions(long documentId, int documentTypeId)
  {
    if (this.sidecarObjectsExtensions.Count == 0)
      return;
    foreach (ISidecarObjectsEmbedAttributesExtension objectsExtension in this.sidecarObjectsExtensions)
      objectsExtension.Initialize(documentId, documentTypeId);
  }

  protected override void ClearDriver()
  {
    this.CleanupSidecarObjectsExtensions();
    base.ClearDriver();
  }

  private void CleanupSidecarObjectsExtensions()
  {
    if (this.sidecarObjectsExtensions.Count == 0)
      return;
    foreach (ISidecarObjectsEmbedAttributesExtension objectsExtension in this.sidecarObjectsExtensions)
      objectsExtension.Cleanup();
  }

  protected override bool DoEmbedAttributes(
    long documentId,
    int documentType,
    string documentFilePath,
    ValueBag documentAttributes)
  {
    int num = base.DoEmbedAttributes(documentId, documentType, documentFilePath, documentAttributes) ? 1 : 0;
    this.RaiseAfterEmbedAttributesEvent(documentId, documentType, documentFilePath, documentAttributes);
    return num != 0;
  }

  private void RaiseAfterEmbedAttributesEvent(
    long documentId,
    int documentType,
    string documentFilePath,
    ValueBag documentAttributes)
  {
    if (this.sidecarObjectsExtensions.Count == 0)
      return;
    foreach (ISidecarObjectsEmbedAttributesExtension objectsExtension in this.sidecarObjectsExtensions)
      objectsExtension.AfterEmbedAttributes(documentId, documentType, documentFilePath, documentAttributes);
  }

  protected override void DoSaveModifiedDocument(IOpenDocument document)
  {
    base.DoSaveModifiedDocument(document);
    this.RaiseAfterSaveModifiedDocument(document);
  }

  private void RaiseAfterSaveModifiedDocument(IOpenDocument document)
  {
    if (this.sidecarObjectsExtensions.Count == 0)
      return;
    foreach (ISidecarObjectsEmbedAttributesExtension objectsExtension in this.sidecarObjectsExtensions)
      objectsExtension.AfterSaveModifiedDocument(document);
  }

  protected override void DoFlushChanges()
  {
    base.DoFlushChanges();
    this.RaiseAfterFlushChanges();
  }

  private void RaiseAfterFlushChanges()
  {
    if (this.sidecarObjectsExtensions.Count == 0)
      return;
    foreach (ISidecarObjectsEmbedAttributesExtension objectsExtension in this.sidecarObjectsExtensions)
      objectsExtension.AfterFlushChanges();
  }
}
