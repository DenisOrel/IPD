// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.ConstructionalExtension
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.ControlFlow;
using Intermech.Data.SectionEntities;
using Intermech.Interfaces;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal sealed class ConstructionalExtension : 
  DocumentCaptureChangesDriver,
  IDwgDriver,
  ICaptureChangesDriver
{
  private readonly IIntegrator integrator;
  private IApplicationFileTypes fileTypeSvc;
  private AcadIntegratorSettings integratorSettings;
  private StandardSchedulerStages schedulerStages;

  public ConstructionalExtension(IIntegrator integrator)
  {
    this.integrator = integrator != null ? integrator : throw new ArgumentNullException();
  }

  public IIntegrator Integrator => this.integrator;

  protected override void ValidateDriverProperties()
  {
    base.ValidateDriverProperties();
    this.integratorSettings = ServiceUtils.GetService<AcadIntegratorSettingsService>((object) this.Integrator, true).GetSettings();
    this.integratorSettings.ConstructionalSettings.CheckEnabled();
  }

  protected override void InitializeDriver()
  {
    base.InitializeDriver();
    this.fileTypeSvc = ServiceUtils.GetService<IApplicationFileTypes>((object) this.Integrator, true);
  }

  protected override void InitializeDriverContextServices()
  {
    base.InitializeDriverContextServices();
    this.schedulerStages = new StandardSchedulerStages(this.DriverContext.Scheduler);
  }

  protected override void ClearDriver()
  {
    base.ClearDriver();
    this.fileTypeSvc = (IApplicationFileTypes) null;
    this.integratorSettings = (AcadIntegratorSettings) null;
    this.schedulerStages = (StandardSchedulerStages) null;
  }

  public AcadIntegratorSettings IntegratorSettings => this.integratorSettings;

  public IDrawingTypesInfo DrawingTypes
  {
    get => (IDrawingTypesInfo) this.integratorSettings.ConstructionalSettings;
  }

  public StandardSchedulerStages SchedulerStages => this.schedulerStages;

  public override DocumentFileData OpenDocumentFile(SectionEntity documentItem, string fullPath)
  {
    if (documentItem == null)
      throw new ArgumentNullException(nameof (documentItem));
    return fullPath != null ? new DocumentFileData(fullPath) : throw new ArgumentNullException(nameof (fullPath));
  }

  protected override IAction CreateTypedDocumentHandler(
    SectionEntity docItem,
    object documentKind,
    int documentType)
  {
    Guid groupId = this.DocumentKindToGroupId(documentKind);
    if (groupId == ConstructionalSettings.DrawingsGroup)
      return this.CreateDrawingHandler(docItem);
    throw new NotImplementedException($"Не реализована поддержка группы документов типа '{groupId}'.");
  }

  private IAction CreateDrawingHandler(SectionEntity docItem)
  {
    DrawingHandler drawingHandler = docItem != null ? new DrawingHandler(this, this.DriverContext, docItem) : throw new ArgumentNullException(nameof (docItem));
    drawingHandler.ScheduleAdapter = DocumentScheduleAdapter.FromStandardScheduler(this.SchedulerStages);
    return (IAction) drawingHandler;
  }

  protected override List<LocalId<int>> DetectNewDocumentType(SectionEntity docItem)
  {
    List<LocalId<int>> typesByGroupType = this.DrawingTypes.GetDrawingTypesByGroupType(ConstructionalSettings.DrawingsGroup);
    return this.FilterDocumentTypesByExtension(docItem, typesByGroupType);
  }

  public override bool IsDocumentTypeSupported(int documentType)
  {
    return this.DrawingTypes.FindSettings(documentType) != null;
  }

  protected override object DoMapDocumentTypeToKind(int documentType)
  {
    return this.GroupIdToDocumentKind(ConstructionalSettings.DrawingsGroup);
  }

  private Guid DocumentKindToGroupId(object documentKind)
  {
    return documentKind != null ? (Guid) documentKind : throw new ArgumentNullException(nameof (documentKind));
  }

  private object GroupIdToDocumentKind(Guid groupId) => (object) groupId;

  protected override SelectedObjectType SelectNewDocumentTypeSilent(
    SectionEntity docItem,
    List<object> possibleDocumentKinds,
    List<LocalId<int>> possibleDocumentTypes)
  {
    return new SelectedObjectType(this.GetBestDwgType(possibleDocumentTypes).Id, true);
  }

  private LocalId<int> GetBestDwgType(List<LocalId<int>> dwgTypes)
  {
    return ((this.FindDwgType(dwgTypes, true, XRefMode.Documents) ?? this.FindDwgType(dwgTypes, true, XRefMode.AncillaryFiles)) ?? this.FindDwgType(dwgTypes, true, XRefMode.Ignore)) ?? dwgTypes[0];
  }

  private LocalId<int> FindDwgType(List<LocalId<int>> dwgTypes, bool scanStamp, XRefMode xrefMode)
  {
    foreach (LocalId<int> dwgType in dwgTypes)
    {
      DrawingTypeSettings settings = this.DrawingTypes.GetSettings(dwgType.Id);
      if ((!scanStamp || !string.IsNullOrEmpty(settings.StmName)) && (xrefMode == XRefMode.Ignore || settings.XRefMode == xrefMode))
        return dwgType;
    }
    return (LocalId<int>) null;
  }
}
