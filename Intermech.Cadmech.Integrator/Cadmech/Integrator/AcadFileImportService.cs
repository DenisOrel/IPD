// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.AcadFileImportService
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.Runtime;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators;
using System;
using System.Diagnostics;
using System.IO;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal sealed class AcadFileImportService : FileImportService
{
  private readonly ChangesDriver ccDriver;
  private AcadIntegratorSettingsService settingsService;
  private ActiveCADSystemService activeCADSystemService;

  public AcadFileImportService(IIntegrator owner)
    : base(owner)
  {
    this.ccDriver = new ChangesDriver(owner);
  }

  public AcadIntegratorSettingsService SettingsService
  {
    [DebuggerStepThrough] get
    {
      lock (this.Integrator.SyncRoot)
        return this.settingsService;
    }
    [DebuggerStepThrough] set
    {
      lock (this.Integrator.SyncRoot)
      {
        this.RequireNotInitialized();
        this.settingsService = value;
      }
    }
  }

  public ActiveCADSystemService ActiveCADSystemService
  {
    [DebuggerStepThrough] get
    {
      lock (this.Integrator.SyncRoot)
        return this.activeCADSystemService;
    }
    [DebuggerStepThrough] set
    {
      lock (this.Integrator.SyncRoot)
      {
        this.RequireNotInitialized();
        this.activeCADSystemService = value;
      }
    }
  }

  public new TransferFileToWorkspaceMode AllowTransferFileToWorkspace
  {
    get => base.AllowTransferFileToWorkspace;
    set => base.AllowTransferFileToWorkspace = value;
  }

  protected override void DoInitialize()
  {
    base.DoInitialize();
    if (this.SettingsService == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "SettingsService");
    if (this.ActiveCADSystemService == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "ActiveCADSystemService");
    this.AllowTransferFileToWorkspace = TransferFileToWorkspaceMode.FilesByMask;
  }

  protected override bool DoCheckCanImportFile(FileInfo fileInfo, Stream fileContent)
  {
    return base.DoCheckCanImportFile(fileInfo, fileContent) && this.ActiveCADSystemService.GetActiveCADSystem().Id == this.Integrator.Id;
  }

  protected override ICaptureChangesDriver GetCaptureChangesDriver()
  {
    return (ICaptureChangesDriver) this.ccDriver;
  }

  protected override void SetCaptureChangesParameters(bool extendedImport)
  {
    base.SetCaptureChangesParameters(extendedImport);
    if (AcadImportVars.MechanicalOnly.Value)
    {
      this.ccDriver.ProcessingSchemas = DwgDriverProcessingSchemas.MechanicalDocuments;
      this.ccDriver.MechanicalDocuments.RootDocumentGroup = AcadImportVars.RootDocumentTypes.Value;
    }
    else if (AcadImportVars.ConstructionalOnly.Value)
      this.ccDriver.ProcessingSchemas = DwgDriverProcessingSchemas.ConstructionalDocuments;
    this.ccDriver.ApplyTypicalSettings();
  }

  protected override void ResetCaptureChangesParameters()
  {
    base.ResetCaptureChangesParameters();
    this.ccDriver.ProcessingSchemas = (DwgDriverProcessingSchemas) 0;
    this.ccDriver.MechanicalDocuments.RootDocumentGroup = Guid.Empty;
    this.ccDriver.MechanicalDocuments.ArticleEmitter = (IDwgArticleEmitter) null;
  }
}
