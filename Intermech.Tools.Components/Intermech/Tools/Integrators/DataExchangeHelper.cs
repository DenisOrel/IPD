// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.DataExchangeHelper
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data.EntityDb;
using Intermech.Data.SectionEntities;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Tools.DataExchange;
using Intermech.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

#nullable disable
namespace Intermech.Tools.Integrators;

internal class DataExchangeHelper
{
  private IFileImportService fileImportService;
  private IFileVault fileVaultService;
  private IOpenFilesService openFilesService;

  /// <summary>Создает объект.</summary>
  public DataExchangeHelper()
  {
    this.fileImportService = ServiceUtils.GetService<IFileImportService>((object) ApplicationServices.Container, true);
    this.fileVaultService = ServiceUtils.GetService<IFileVault>((object) ApplicationServices.Container, true);
    this.openFilesService = ServiceUtils.GetService<IOpenFilesService>((object) ApplicationServices.Container, true);
  }

  /// <summary>
  /// Проверяет, если ли у объекта несохраненные изменения, требующие подключения интегратора для сохранения изменений в базу.
  /// </summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <returns>true - у объекта есть несохраненные изменения</returns>
  public bool ShouldCaptureChanges(long objectId)
  {
    Intermech.Files.DBObjectState objectState = this.fileVaultService.DBObjectsInfo.GetObjectState(objectId, true);
    using (UIReport.CreateScope())
    {
      UIReportBuilder uiReportBuilder = new UIReportBuilder();
      uiReportBuilder.ReportStart($"Предварительный анализ изменений в файлах объекта '{objectState}'");
      try
      {
        int num = this.ShouldCaptureChanges(objectState) ? 1 : 0;
        if (num == 0)
          UIReport.ReportEvent("Файлы объекта не содержат несохраненных изменений. Участие интегратора в сохранении изменений не требуется.");
        uiReportBuilder.ReportSuccess();
        return num != 0;
      }
      catch (Exception ex)
      {
        uiReportBuilder.ReportFail(ex);
        throw;
      }
    }
  }

  /// <summary>
  /// Проверяет, что объект в базе IPS соответствует своему файлу. Другими словами, этот метод проверяет, что последнее сохранение изменений в объекте
  /// выполнялось интегратором.
  /// </summary>
  /// <param name="objectState">Описатель версии объекта</param>
  /// <returns>true - объект соответствует своему файлу, false - объект не соответствует файлу, так как последнее сохранение было быстрым (в базу были записаны только файлы объекта)</returns>
  private bool? IsObjectMatchesToFile(Intermech.Files.DBObjectState objectState)
  {
    if (objectState.ModifyMode == ObjectModifyModes.Checkout && objectState.ObjectId < 0L)
    {
      List<FileDifferencePair> all = new FileDifferenceCalculator().Calculate(this.fileVaultService.DBFilesInfo.GetFileStates(objectState.ObjectId), this.fileVaultService.DBFilesInfo.GetFileStates(Math.Abs(objectState.ObjectId))).FindAll((Predicate<FileDifferencePair>) (item => item.DifferenceType == FileDifferenceType.UpdatedFile || item.DifferenceType == FileDifferenceType.NewFile));
      if (all.Count == 0)
        return new bool?(true);
      if (UIReport.Enabled)
      {
        UIReport.ReportEvent("Файлы рабочей копии объекта отличаются от файлов архивной копии. Для правильного сохранения изменений требуется участие интегратора.");
        UIReport.ReportEvent("Список измененных файлов:", TraceLevel.Verbose);
        UIReport.Indent();
        foreach (FileDifferencePair fileDifferencePair in all)
          UIReport.ReportEvent(fileDifferencePair.LocalState.FileName);
        UIReport.Unindent();
      }
      return new bool?();
    }
    return objectState.ModifyMode == ObjectModifyModes.InBase ? new bool?() : new bool?(true);
  }

  /// <summary>
  /// Проверяет, является ли указанная версия объекта первой версией.
  /// </summary>
  /// <param name="objectState">Описатель версии объекта</param>
  /// <returns>true - это первая версия объекта, false - это последующая версия объекта</returns>
  private bool IsFirstObjectVersion(Intermech.Files.DBObjectState objectState)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return sessionKeeper.Session.GetObject(objectState.ObjectId, true).VersionID == 0;
  }

  /// <summary>
  /// Проверяет, если ли у объекта такие несохраненные изменения, которые требуют использования интегратора для сохранения изменений в базу.
  /// </summary>
  /// <param name="objectState">Описатель версии объекта</param>
  /// <returns>true - у объекта есть несохраненные изменения</returns>
  private bool ShouldCaptureChanges(Intermech.Files.DBObjectState objectState)
  {
    if (!objectState.IsEditableState)
      return false;
    if (!(this.IsObjectMatchesToFile(objectState) ?? false))
      return true;
    if (this.fileVaultService.WorkArea.IsObjectPublished(objectState.ObjectId))
    {
      string masterFileName = this.fileVaultService.DBFilesInfo.GetMasterFileName(objectState.ObjectId, false);
      if (!string.IsNullOrEmpty(masterFileName))
      {
        string str = Path.Combine(this.fileVaultService.WorkArea.AreaPath, masterFileName);
        if (File.Exists(str) && this.openFilesService.IsDirty(str))
        {
          if (UIReport.Enabled)
            UIReport.ReportEvent($"Мастер-файл объекта '{str}' открыт в приложении и имеет несохраненные изменения. Для правильного сохранения изменений требуется участие интегратора.");
          return true;
        }
      }
      DBObjectFilesDifferenceCalculator differenceCalculator = this.fileVaultService.WorkArea.CreateObjectFilesDifferenceCalculator();
      differenceCalculator.Add(objectState);
      differenceCalculator.Calculate();
      List<FileDifferencePair> all = differenceCalculator.Results[0].DifferencePairs.FindAll((Predicate<FileDifferencePair>) (item => item.DifferenceType == FileDifferenceType.UpdatedFile || item.DifferenceType == FileDifferenceType.NewFile));
      if (all.Count != 0)
      {
        if (UIReport.Enabled)
        {
          UIReport.ReportEvent("На диске файлы объекта отличаются от файлов рабочей копии. Для правильного сохранения изменений требуется участие интегратора.");
          UIReport.ReportEvent("Список измененных файлов:", TraceLevel.Verbose);
          UIReport.Indent();
          foreach (FileDifferencePair fileDifferencePair in all)
            UIReport.ReportEvent(fileDifferencePair.LocalState.FileName);
          UIReport.Unindent();
        }
        return true;
      }
    }
    return this.IsFirstObjectVersion(objectState);
  }

  public List<string> GetDeferredImportFiles(CaptureChangesResult captureChangesResult)
  {
    EntitySet entitySet = captureChangesResult.Database.Query((IQueryCondition) new BinaryCondition(SectionVirtualProperties.SectionTypeRef, BinaryOperator.Equal, (object) typeof (DraftDocumentSection)));
    List<string> deferredImportFiles = new List<string>(entitySet.Count);
    foreach (SectionEntity sectionEntity in (HashSet<IEntity>) entitySet)
    {
      DraftDocumentSection draftDocumentSection = sectionEntity.Sections.Get<DraftDocumentSection>();
      deferredImportFiles.Add(draftDocumentSection.ExternalFilePath);
    }
    return deferredImportFiles;
  }

  public void ImportDeferredDraftDocuments(
    CaptureChangesResult mainResult,
    IPercentageProgressSink progressSink)
  {
    List<string> deferredImportFiles = this.GetDeferredImportFiles(mainResult);
    if (deferredImportFiles.Count != 0)
      this.fileImportService.ImportFiles((ICollection<string>) deferredImportFiles, new BatchFileImportOptions()
      {
        NotifyOnMasterFileErrors = true,
        NotifyOnDeferredFilesErrors = true,
        CustomProgressSink = progressSink.ToMasterSlaveSink()
      });
    progressSink.SetState(string.Empty);
    progressSink.SetProgress(100.0);
  }

  public double GetMainFileProgressRange(IIntegrator integrator)
  {
    IFileImportSupport service = ServiceUtils.GetService<IFileImportSupport>((object) integrator, false);
    return service != null && (service.GetImportFileCapabilities() & ImportFileCapabilities.DeferredImport) != ImportFileCapabilities.None ? 70.0 : 99.0;
  }
}
