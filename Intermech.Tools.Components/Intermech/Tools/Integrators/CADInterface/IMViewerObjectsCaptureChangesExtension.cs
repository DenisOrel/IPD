// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.IMViewerObjectsCaptureChangesExtension
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.CADInterface.Proxies;
using Intermech.ControlFlow;
using Intermech.Data;
using Intermech.Data.SectionEntities;
using Intermech.Interfaces.Data.SidecarObjects;
using Intermech.Services.IMViewer;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators.Mechanical;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

internal sealed class IMViewerObjectsCaptureChangesExtension : SidecarObjectsCaptureChangesExtension
{
  private ICADSettingsService integratorSettingsService;
  private CADSettings cadSettings;
  private IIMViewerObjectCreatorService imviewerService;

  public IMViewerObjectsCaptureChangesExtension(
    MechanicalDriver driver,
    IMViewerObjectsIDCache imvIDCache,
    ICADSettingsService integratorSettingsService,
    IIMViewerObjectCreatorService imviewerService)
    : base(driver, (SidecarObjectsIDCache) imvIDCache)
  {
    if (integratorSettingsService == null)
      throw new ArgumentNullException(nameof (integratorSettingsService));
    if (imviewerService == null)
      throw new ArgumentNullException(nameof (imviewerService));
    this.integratorSettingsService = integratorSettingsService;
    this.imviewerService = imviewerService;
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

  public override bool IsSourceDocument(SectionEntity documentEntity)
  {
    if (documentEntity == null)
      throw new ArgumentNullException(nameof (documentEntity));
    DocumentGroup byDocumentType = this.CADSettings.FileDocumentGroups.FindByDocumentType(ObjectSection.GetObjectType(documentEntity), false);
    return byDocumentType != null && (!(byDocumentType.Name != "Assembly") || !(byDocumentType.Name != "Part")) && base.IsSourceDocument(documentEntity);
  }

  protected override bool CanCreateNewSidecarObject(SectionEntity cadDocumentEntity)
  {
    return this.CADSettings.EnableIMViewerFiles && base.CanCreateNewSidecarObject(cadDocumentEntity);
  }

  /// <summary>
  /// Возвращает путь к выделенной папке для генерации ассоциированных файлов.
  /// </summary>
  /// <returns>Абсолютный путь к папке</returns>
  protected internal override string GetSidecarFilesBaseDirectory()
  {
    return this.imviewerService.ConverterBaseDirectory;
  }

  protected internal override SidecarObjectUpdateMode GetSidecarObjectUpdateMode(
    SectionEntity cadDocumentEntity)
  {
    return !this.CADSettings.EnableIMViewerFiles ? SidecarObjectUpdateMode.SetOutdated : SidecarObjectUpdateMode.KeepActual;
  }

  protected internal override IAction TryCreateBlankSidecarObjectAction(
    SectionEntity documentEntity,
    SectionEntity sidecarEntity)
  {
    return (IAction) new CreateIMViewerBlankObjectAction(documentEntity, sidecarEntity, this.imviewerService);
  }

  protected internal override SidecarFileResult TryCreateOrUpdateSidecarFile(
    SectionEntity cadDocumentEntity,
    string cadDocumentBaseDirectory)
  {
    CADDocumentProxy document = cadDocumentEntity.Sections.Get<CIDocumentData>().Document;
    try
    {
      return (SidecarFileResult) new SidecarFileResult.Success(this.imviewerService.CreateViewerFile(document.FullName, cadDocumentBaseDirectory, document.CADSystem, false));
    }
    catch (Exception ex)
    {
      return (SidecarFileResult) new SidecarFileResult.Error(ex.Message);
    }
  }

  protected internal override string TryCreateSidecarObjectCaption(
    long documentId,
    int documentType,
    ValueBag documentAttributeBag,
    IEnumerable<StringKey> identityAttributeNames)
  {
    return this.imviewerService.CreateViewerObjectCaption(documentId, documentType, documentAttributeBag, identityAttributeNames);
  }

  protected internal override string CreateErrorWhenSidecarFileUpdateFailed(
    SectionEntity cadDocumentEntity)
  {
    return $"Не удалось создать/обновить IMV-файл по исходному файлу '{FilesSection.GetMasterFile(cadDocumentEntity)}'.";
  }
}
