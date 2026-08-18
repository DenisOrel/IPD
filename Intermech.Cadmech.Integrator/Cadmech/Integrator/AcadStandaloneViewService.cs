// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.AcadStandaloneViewService
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.AutoCAD.Proxies;
using Intermech.Cadmech.Integrator.DwgTasks;
using Intermech.Data;
using Intermech.Runtime;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.StandaloneView;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal sealed class AcadStandaloneViewService(IIntegrator owner) : StandaloneViewServiceBase(owner)
{
  private AcadIntegratorSettingsService integratorSettingsService;
  private CadApiService apiService;

  public AcadIntegratorSettingsService IntegratorSettingsService
  {
    [DebuggerStepThrough] get
    {
      lock (this.Integrator.SyncRoot)
        return this.integratorSettingsService;
    }
    set
    {
      lock (this.Integrator.SyncRoot)
      {
        this.RequireNotInitialized();
        this.integratorSettingsService = value;
      }
    }
  }

  public CadApiService ApiService
  {
    [DebuggerStepThrough] get
    {
      lock (this.Integrator.SyncRoot)
        return this.apiService;
    }
    set
    {
      lock (this.Integrator.SyncRoot)
      {
        this.RequireNotInitialized();
        this.apiService = value;
      }
    }
  }

  protected override void DoInitialize()
  {
    base.DoInitialize();
    if (this.IntegratorSettingsService == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "IntegratorSettingsService");
    if (this.ApiService == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "ApiService");
  }

  private string GetStmFilePath(int drawingTypeId)
  {
    return StmFile.Locate(this.GetDrawingTypeSettings(drawingTypeId));
  }

  private DrawingTypeSettings GetDrawingTypeSettings(int drawingTypeId)
  {
    AcadIntegratorSettings settings1 = this.IntegratorSettingsService.GetSettings();
    if (settings1.MechanicalSettings.IsEnabled)
    {
      DrawingTypeSettings settings2 = settings1.MechanicalSettings.FindSettings(drawingTypeId);
      if (settings2 != null)
        return settings2;
    }
    if (settings1.ConstructionalSettings.IsEnabled)
    {
      DrawingTypeSettings settings3 = settings1.ConstructionalSettings.FindSettings(drawingTypeId);
      if (settings3 != null)
        return settings3;
    }
    throw new InvalidOperationException($"Тип объектов '{drawingTypeId}' не является типом чертежа AutoCAD.");
  }

  protected override void DoInjectViewData(StandaloneViewDataInjectionOperation operation)
  {
    operation.CustomData = (object) new AcadStandaloneViewService.OpenDrawingData();
    base.DoInjectViewData(operation);
    if (!this.ApiService.IsApplicationRunning)
      return;
    this.ReloadDrawingIfOpen(operation.Parameters.FilePath);
  }

  private void ReloadDrawingIfOpen(string drawingFilePath)
  {
    using (AcadApiSession acadApiSession = new AcadApiSession((IApplicationApiService) this.ApiService))
    {
      ICadProxy application = acadApiSession.Application;
      ICadDocumentProxy openDocument = application.FindOpenDocument(drawingFilePath);
      if (openDocument == null)
        return;
      openDocument.Close(false);
      application.OpenDocument(drawingFilePath);
    }
  }

  protected override void DoWriteViewDataIntoTempFile(
    StandaloneViewDataInjectionOperation operation,
    string tempFilePath)
  {
    string stmFilePath = this.GetStmFilePath(operation.ObjectTypeId);
    using (DwgStampUpdaterTask stampUpdaterTask = new DwgStampUpdaterTask())
    {
      stampUpdaterTask.StmFilePath = stmFilePath;
      stampUpdaterTask.OpenDrawing(tempFilePath);
      ((AcadStandaloneViewService.OpenDrawingData) operation.CustomData).StampUpdater = stampUpdaterTask;
      this.DoWriteViewDataIntoOpenFile(operation);
    }
  }

  protected sealed override void DoWriteAttributesIntoOpenFile(
    StandaloneViewDataInjectionOperation operation,
    List<ValueRecord> attributeValues)
  {
    ((AcadStandaloneViewService.OpenDrawingData) operation.CustomData).StampUpdater.UpdateStamp(DwgPredicates.ParametersForStampIsValid(), new Predicate<ValueBag>(DwgPredicates.StampIsValid), (ICollection<ValueRecord>) attributeValues);
  }

  private sealed class OpenDrawingData
  {
    public DwgStampUpdaterTask StampUpdater { get; set; }
  }
}
