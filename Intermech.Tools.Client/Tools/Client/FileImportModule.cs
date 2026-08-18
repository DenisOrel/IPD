// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.FileImportModule
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.ApplicationModel;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.Simple;
using Intermech.UI;
using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace Intermech.Tools.Client;

internal sealed class FileImportModule : InitializerModule
{
  private IOpenFilesService openFilesService;
  private IFileVaultSettingsService fileVaultSettingsService;
  private IFileImportService fileImportService;

  public FileImportModule(
    IOpenFilesService openFilesService,
    IFileVaultSettingsService fileVaultSettingsService,
    IFileImportService fileImportService)
  {
    if (openFilesService == null)
      throw new ArgumentNullException(nameof (openFilesService));
    if (fileVaultSettingsService == null)
      throw new ArgumentNullException(nameof (fileVaultSettingsService));
    if (fileImportService == null)
      throw new ArgumentNullException(nameof (fileImportService));
    this.openFilesService = openFilesService;
    this.fileVaultSettingsService = fileVaultSettingsService;
    this.fileImportService = fileImportService;
  }

  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.fileImportService.FileProbe += new EventHandler<FileProbeEventArgs>(this.IntegratorFileImportProbe);
    this.fileImportService.FallbackProbe += new EventHandler<FileProbeEventArgs>(this.SimpleFileImportProbe);
  }

  protected override void DoShutdown()
  {
    base.DoShutdown();
    this.fileImportService.FileProbe -= new EventHandler<FileProbeEventArgs>(this.IntegratorFileImportProbe);
    this.fileImportService.FallbackProbe -= new EventHandler<FileProbeEventArgs>(this.SimpleFileImportProbe);
  }

  private void IntegratorFileImportProbe(object sender, FileProbeEventArgs e)
  {
    List<IntegratorObject> integrators;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      integrators = ServiceUtils.GetService<IIntegratorServer>((object) sessionKeeper.Session, true).GetIntegrators();
    foreach (IntegratorObject iobj in integrators)
    {
      IIntegrator integrator = ClientContext.Integrators.GetIntegrator(iobj, false);
      if (integrator != null)
      {
        IFileImportSupport service = ServiceUtils.GetService<IFileImportSupport>((object) integrator, false);
        if (service != null)
        {
          e.FileContent.Seek(0L, SeekOrigin.Begin);
          if (service.CanImportFile(e.FileInfo, e.FileContent))
          {
            e.ImportHandler = new ImportFileHandler(service.ImportFile);
            e.ImportCapabilities = service.GetImportFileCapabilities();
            break;
          }
        }
      }
    }
  }

  private void SimpleFileImportProbe(object sender, FileProbeEventArgs e)
  {
    e.ImportHandler = new ImportFileHandler(this.SimpleFileImportHandler);
  }

  private FileImportResult SimpleFileImportHandler(string fullPath, FileImportOptions importOptions)
  {
    ToolServiceReportBuilder serviceReportBuilder = new ToolServiceReportBuilder();
    if (UIReport.Enabled)
      serviceReportBuilder.ReportFileImportStart(fullPath);
    try
    {
      long objectId = this.SimpleFileImportCore(fullPath, importOptions);
      if (UIReport.Enabled)
        serviceReportBuilder.ReportSuccess();
      return (FileImportResult) new FileImportResult.Success(fullPath, objectId);
    }
    catch (Exception ex)
    {
      if (UIReport.Enabled)
        serviceReportBuilder.ReportFail(ex);
      throw;
    }
  }

  private long SimpleFileImportCore(string fullPath, FileImportOptions importOptions)
  {
    TransferFileToWorkspaceAction toWorkspaceAction = new TransferFileToWorkspaceAction(this.openFilesService, this.fileVaultSettingsService);
    toWorkspaceAction.ImportMode = TransferFileToWorkspaceMode.SourceFileOnly;
    toWorkspaceAction.SourcePath = fullPath;
    toWorkspaceAction.Perform();
    fullPath = toWorkspaceAction.TargetPath;
    return new CaptureChangesManager()
    {
      Driver = ((ICaptureChangesDriver) new AnyFileCaptureChangesDriver())
    }.ImportFile(new ImportFileActionParameters()
    {
      FullPath = fullPath,
      ProgressSink = importOptions.ProgressSink
    }).ObjectId;
  }
}
