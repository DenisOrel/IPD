// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.Cadmech2DService
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Runtime;
using Intermech.Tools.Data;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.Mechanical;
using Intermech.UI;
using System;
using System.Diagnostics;
using System.IO;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal sealed class Cadmech2DService : IntegratorService
{
  private readonly CaptureChangesManager ccManager;
  private readonly ChangesDriver ccDriver;
  private readonly AssemblyDwgDataExtractor extractor;
  private readonly ToolServiceReportBuilder uiReporter;
  private AcadIntegratorSettingsService settingsService;

  public Cadmech2DService(IIntegrator owner)
    : base(owner)
  {
    this.uiReporter = new ToolServiceReportBuilder();
    this.ccDriver = new ChangesDriver(owner);
    this.ccDriver.ProcessingSchemas = DwgDriverProcessingSchemas.MechanicalDocuments;
    this.ccDriver.MechanicalDocuments.UpdateArticles = true;
    this.ccManager = new CaptureChangesManager();
    this.ccManager.Driver = (ICaptureChangesDriver) this.ccDriver;
    this.ccManager.KeepCheckedOut = true;
    this.extractor = new AssemblyDwgDataExtractor();
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

  protected override void DoInitialize()
  {
    base.DoInitialize();
    if (this.SettingsService == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "SettingsService");
  }

  public StructData CreateComposition(
    string dwgPath,
    string fieldLayout,
    string structFile,
    string passportData)
  {
    if (string.IsNullOrEmpty(dwgPath))
      throw new ArgumentException(Intermech.Localization.Localization.rm.GetString("Cadmech.Integrator_80"), nameof (dwgPath));
    if (!Path.IsPathRooted(dwgPath))
      throw new ArgumentException(Intermech.Localization.Localization.rm.GetString("Cadmech.Integrator_81"), nameof (dwgPath));
    if (string.IsNullOrEmpty(fieldLayout))
      throw new ArgumentException(Intermech.Localization.Localization.rm.GetString("Cadmech.Integrator_82"), "fieldLayoutString");
    if (string.IsNullOrEmpty(structFile))
      throw new ArgumentException(Intermech.Localization.Localization.rm.GetString("Cadmech.Integrator_83"), "structFileString");
    if (passportData == null)
      throw new ArgumentException(Intermech.Localization.Localization.rm.GetString("Cadmech.Integrator_84"), nameof (passportData));
    this.RequireReadyState();
    lock (this.Integrator.SyncRoot)
    {
      this.LicenseService.Check();
      Intermech.Files.DBObjectState dwgWorkObject = this.LookupForEditableDwgObject(dwgPath);
      using (UIReport.CreateScope())
      {
        try
        {
          if (UIReport.Enabled)
            this.uiReporter.ReportStart(string.Format(Intermech.Localization.Localization.rm.GetString("Cadmech.Integrator_111"), (object) dwgWorkObject.Caption));
          this.ccDriver.MechanicalDocuments.ArticleEmitter = (IDwgArticleEmitter) new AssemblyDwgArticleEmitter((MechanicalDriver) this.ccDriver.MechanicalDocuments, (IServiceProvider) this.Integrator, new DwgInputData()
          {
            FieldLayoutContent = new FileContent("ATR.TXT", fieldLayout),
            StructFileContent = new FileContent(Path.GetFileName(dwgPath), structFile),
            PassportData = passportData
          });
          CaptureChangesResult captureChangesResult = ProgressSinks.DialogService.Invoke<CaptureChangesResult>($"Создание спецификации для {Path.GetFileName(dwgPath)}", ProgressSinkDialogFlags.Default, (Func<IPercentageProgressSink, CaptureChangesResult>) (progressSink => this.ccManager.CaptureChanges(new CaptureChangesActionParameters()
          {
            ObjectId = dwgWorkObject.ObjectId,
            ProgressSink = progressSink
          })));
          StructData outputData = new StructData();
          outputData.DwgPath = dwgPath;
          this.extractor.Perform(captureChangesResult.Database, outputData);
          if (UIReport.Enabled)
            this.uiReporter.ReportSuccess();
          return outputData;
        }
        catch (Exception ex)
        {
          if (UIReport.Enabled)
            this.uiReporter.ReportFail(ex);
          throw;
        }
        finally
        {
          this.ccDriver.MechanicalDocuments.ArticleEmitter = (IDwgArticleEmitter) null;
        }
      }
    }
  }

  public string PackCompositionToFile(StructData structData, string fieldLayout)
  {
    if (structData == null)
      throw new ArgumentNullException(nameof (structData), Intermech.Localization.Localization.rm.GetString("Cadmech.Integrator_86"));
    if (string.IsNullOrEmpty(fieldLayout))
      throw new ArgumentException(Intermech.Localization.Localization.rm.GetString("Cadmech.Integrator_87"), "fieldLayoutString");
    this.RequireReadyState();
    lock (this.Integrator.SyncRoot)
    {
      this.LookupForEditableDwgObject(structData.DwgPath);
      BaseSpecJob job = new BaseSpecJob();
      job.ProcessingMode = StructFileProcessingModes.Cadmech;
      job.SuffixMode = true;
      this.ApplyUpdateResult(new MappedUpdater().UpdateSpecDummy(structData.StructFile, structData.Spec));
      return new StructFileParser().ToFile(new StructFileCodec((IServiceProvider) this.Integrator).Encode(job, structData.StructFile), new FileContent("ATRSPC.TXT", fieldLayout));
    }
  }

  private void ApplyUpdateResult(UpdateResult updateResult)
  {
    if (updateResult.NewRecords.Count <= 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBTransactions customService = (IDBTransactions) sessionKeeper.Session.GetCustomService(typeof (IDBTransactions));
      customService.StartTransaction();
      try
      {
        for (int index1 = 0; index1 < updateResult.NewRecords.Count; ++index1)
        {
          SpecRecord newRecord = updateResult.NewRecords[index1];
          for (int index2 = 0; index2 < newRecord.Relations.Count; ++index2)
          {
            SpecRelation relation = newRecord.Relations[index2];
            sessionKeeper.Session.GetRelation(relation.RelationGuid, relation.ProjectId).SetAttributesValues(new AttributeValues[2]
            {
              new AttributeValues(IDCache.Default.OccurenceKey.Id, (object) newRecord.Part.PartGuid)
              {
                IsNew = true,
                ThrowSetException = true
              },
              new AttributeValues(IDCache.Default.BasedOnCADModel.Id, (object) true)
              {
                IsNew = true,
                ThrowSetException = true
              }
            });
          }
        }
        customService.Commit();
      }
      catch
      {
        customService.Rollback();
        throw;
      }
    }
  }

  private Intermech.Files.DBObjectState LookupForEditableDwgObject(string dwgPath)
  {
    IFileVault service = ServiceUtils.GetService<IFileVault>((object) ServicesManager.ServiceContainer, true);
    FileOrigin fileOrigin = service.FindArea(dwgPath) == service.WorkArea ? service.WorkArea.GetFileOrigin(dwgPath, false) : (FileOrigin) null;
    Intermech.Files.DBObjectState dbObjectState = fileOrigin != null && fileOrigin.OriginType == FileOriginType.WorkFile ? fileOrigin.WorkObject : throw new FaultException($"Файл сборочного чертежа '{dwgPath}' не зарегистрирован в IPS.");
    return dbObjectState.IsEditableState ? dbObjectState : throw new FaultException($"Объект сборочного чертежа '{dbObjectState.Caption}' недоступен для модификации.");
  }
}
