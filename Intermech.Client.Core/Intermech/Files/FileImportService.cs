
// Type: Intermech.Files.FileImportService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Collections;
using Intermech.ControlFlow;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.IO;
using Intermech.Localization;
using Intermech.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;


namespace Intermech.Files;

/// <summary>
/// Реализует службу, занимающуюся импортом файлов в базу IPS. Все методы, свойства и события службы являются thread-safe.
/// </summary>
internal sealed class FileImportService : IFileImportService
{
  private readonly IOpenFilesService openFiles;
  private readonly IFileVault fileVault;
  private readonly IOutputView outputView;
  private readonly IUINotificationService uiNotificationService;
  private PathComparer filePathComparer;

  /// <summary>Создает объект.</summary>
  public FileImportService(
    IOpenFilesService openFilesService,
    IFileVault fileVault,
    IOutputView outputView,
    IUINotificationService uiNotificationService)
  {
    if (openFilesService == null)
      throw new ArgumentNullException(nameof (openFilesService));
    if (fileVault == null)
      throw new ArgumentNullException(nameof (fileVault));
    if (outputView == null)
      throw new ArgumentNullException(nameof (outputView));
    if (uiNotificationService == null)
      throw new ArgumentNullException(nameof (uiNotificationService));
    this.openFiles = openFilesService;
    this.fileVault = fileVault;
    this.outputView = outputView;
    this.uiNotificationService = uiNotificationService;
    this.filePathComparer = new PathComparer();
  }

  /// <summary>
  /// Выполняет импорт указанного файла в базу IPS. В результате импорта в базе создается новых объект.
  /// </summary>
  /// <param name="fullPath">Абсолютный путь к импортируемому файлу</param>
  /// <returns>Идентификатор версии объекта, созданного в результате импорта файла</returns>
  /// <exception cref="T:System.ArgumentException">Параметр <paramref name="fullPath" /> должен быть непустой строкой. Параметр <paramref name="fullPath" /> должен содержать путь в абсолютной форме.</exception>
  /// <exception cref="T:System.Exception">Ошибка в процессе импорта файла</exception>
  public long ImportFile(string fullPath)
  {
    this.CheckFullPathArg(fullPath);
    return ProgressSinks.DialogService.Invoke<FileImportResult>($"Импорт файла '{Path.GetFileName(fullPath)}'", ProgressSinkDialogFlags.Default, (Func<IPercentageProgressSink, FileImportResult>) (progressSink => this.ImportFile(fullPath, new FileImportOptions()
    {
      NotifyOnDeferredFilesErrors = true,
      ProgressSink = progressSink
    }))).UnwrapObjectId();
  }

  /// <summary>
  /// Выполняет импорт указанного файла в базу IPS. В результате импорта в базе создается новых объект.
  /// </summary>
  /// <param name="fullPath">Абсолютный путь к импортируемому файлу</param>
  /// <param name="importOptions">Опции импорта файла</param>
  /// <returns>Результат импорта файла</returns>
  /// <exception cref="T:System.ArgumentException">Параметр <paramref name="fullPath" /> должен быть непустой строкой. Параметр <paramref name="fullPath" /> должен содержать путь в абсолютной форме.</exception>
  /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="importOptions" /> не должен быть равен null</exception>
  public FileImportResult ImportFile(string fullPath, FileImportOptions importOptions)
  {
    this.CheckFullPathArg(fullPath);
    this.CheckImportOptionsArg(importOptions);
    return this.ImportFileCore(fullPath, importOptions, false);
  }

  private FileImportResult ImportFileCore(
    string fullPath,
    FileImportOptions importOptions,
    bool isDeferredFileMode)
  {
    FileInfo fileInfo = new FileInfo(fullPath);
    if (!fileInfo.Exists)
    {
      this.openFiles.Save(fullPath);
      fileInfo.Refresh();
      if (!fileInfo.Exists)
        return (FileImportResult) new FileImportResult.IgnoredFile(fullPath, LocalizationHolder.rm.GetString("Client.Core_1309"));
    }
    CanImportInfo canImportInfo = this.CanImportFile(fullPath);
    switch (canImportInfo.Status)
    {
      case CanImportStatus.NewFile:
      case CanImportStatus.ExternalFile:
        return this.ProcessNewFile(fullPath, fileInfo, importOptions, isDeferredFileMode);
      case CanImportStatus.AlreadyImportedFile:
        FileImportResult fileImportResult = this.ProcessAlreadyImportedFile(fullPath, canImportInfo);
        if (fileImportResult is FileImportResult.AlreadyImportedFile)
        {
          FileImportResult.AlreadyImportedFile alreadyImportedFile = (FileImportResult.AlreadyImportedFile) fileImportResult;
          if (new AttachNewFileToExistingObjectConfirmation(fullPath, alreadyImportedFile.ObjectId).ConfirmAction())
            this.fileVault.WorkArea.Attach(alreadyImportedFile.ObjectId);
        }
        return fileImportResult;
      case CanImportStatus.AlreadyImportedAndPublishedFile:
        return this.ProcessAlreadyImportedFile(fullPath, canImportInfo);
      default:
        throw new NotSupportedEnumException((Enum) canImportInfo.Status);
    }
  }

  private FileImportResult ProcessNewFile(
    string fullPath,
    FileInfo fileInfo,
    FileImportOptions importOptions,
    bool isDeferredFileMode)
  {
    using (new DynamicScope())
    {
      this.MakeUICommandContext();
      ImportFileHandler importHandler;
      ImportFileCapabilities importCapabilities;
      try
      {
        Tuple<ImportFileHandler, ImportFileCapabilities> importHandlerInfo = this.GetImportHandlerInfo(fileInfo);
        importHandler = importHandlerInfo.Item1;
        importCapabilities = importHandlerInfo.Item2;
      }
      catch (Exception ex)
      {
        return (FileImportResult) new FileImportResult.Error(fullPath, ex);
      }
      IPercentageProgressSink percentageProgressSink = importOptions.ProgressSink ?? ProgressSinks.NullPercentageSink;
      percentageProgressSink.SetState($"Импорт файла {Path.GetFileName(fullPath)}");
      double fileProgressRange = this.GetMainFileProgressRange(importCapabilities, importOptions);
      FileImportOptions importOptions1 = importOptions.Clone();
      importOptions1.ImportDeferredFiles = false;
      importOptions1.ProgressSink = percentageProgressSink.CreateNestedSink(fileProgressRange);
      FileImportResult importResult = this.InvokeImportHandler(importHandler, fullPath, importOptions1);
      if (importResult is FileImportResult.Success)
      {
        FileImportResult.Success mainFileResult = (FileImportResult.Success) importResult;
        if (mainFileResult.DeferredFiles.Count != 0 && importOptions.ImportDeferredFiles)
        {
          this.ImportDeferredFiles(fullPath, mainFileResult, importOptions, percentageProgressSink.CreateNestedSink(100.0 - fileProgressRange));
          mainFileResult.DeferredFiles.Clear();
        }
      }
      else if (isDeferredFileMode && importOptions.NotifyOnDeferredFilesErrors)
        this.ShowErrorNotification(importResult);
      percentageProgressSink.SetState(string.Empty);
      percentageProgressSink.SetProgress(100.0);
      return importResult;
    }
  }

  private double GetMainFileProgressRange(
    ImportFileCapabilities importCapabilities,
    FileImportOptions importOptions)
  {
    return (importCapabilities & ImportFileCapabilities.DeferredImport) != ImportFileCapabilities.None && importOptions.ImportDeferredFiles ? 30.0 : 99.0;
  }

  private void MakeUICommandContext()
  {
    if (UIVars.UICommand.Value != null)
      return;
    UIVars.UICommand.Declare(new UICommandInfo(LocalizationHolder.rm.GetString("Client.Core_1308")));
  }

  private Tuple<ImportFileHandler, ImportFileCapabilities> GetImportHandlerInfo(FileInfo fileInfo)
  {
    using (FileStream fileContent = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
    {
      FileProbeEventArgs e = new FileProbeEventArgs(fileInfo, (Stream) fileContent);
      if (this.fileProbe != null)
      {
        this.fileProbe((object) this, e);
        if (e.ImportHandler != null)
          return Tuple.Create<ImportFileHandler, ImportFileCapabilities>(e.ImportHandler, e.ImportCapabilities);
      }
      if (this.fallbackProbe != null)
      {
        fileContent.Seek(0L, SeekOrigin.Begin);
        this.fallbackProbe((object) this, e);
        if (e.ImportHandler != null)
          return Tuple.Create<ImportFileHandler, ImportFileCapabilities>(e.ImportHandler, e.ImportCapabilities);
      }
    }
    throw new FaultException(string.Format(LocalizationHolder.rm.GetString("Client.Core_1311"), (object) fileInfo.FullName));
  }

  private FileImportResult InvokeImportHandler(
    ImportFileHandler importHandler,
    string fullPath,
    FileImportOptions importOptions)
  {
    FileImportResult fileImportResult;
    try
    {
      fileImportResult = importHandler(fullPath, importOptions);
    }
    catch (Exception ex)
    {
      fileImportResult = (FileImportResult) new FileImportResult.Error(fullPath, ex);
    }
    if (fileImportResult is FileImportResult.Error && ((FileImportResult.Error) fileImportResult).Exception is AbortException)
      fileImportResult = this.CreateAbortedImportResult(fullPath);
    return fileImportResult;
  }

  private FileImportResult CreateAbortedImportResult(string fullPath)
  {
    return (FileImportResult) new FileImportResult.IgnoredFile(fullPath, "Пользователь прервал импорт файла.");
  }

  private CanImportInfo CanImportFile(string fullPath)
  {
    return this.fileVault.FindArea(fullPath) != this.fileVault.WorkArea ? new CanImportInfo(CanImportStatus.ExternalFile) : this.CanImportFile(this.fileVault.WorkArea.GetFileOrigin(fullPath, false));
  }

  private CanImportInfo CanImportFile(FileOrigin fileOrigin)
  {
    switch (fileOrigin.OriginType)
    {
      case FileOriginType.NewFile:
        return new CanImportInfo(CanImportStatus.NewFile);
      case FileOriginType.WorkFile:
        return new CanImportInfo(CanImportStatus.AlreadyImportedAndPublishedFile, fileOrigin);
      case FileOriginType.DetachedFile:
        return new CanImportInfo(CanImportStatus.AlreadyImportedFile, fileOrigin);
      default:
        throw new NotSupportedEnumException((Enum) fileOrigin.OriginType);
    }
  }

  private IList<CanImportInfo> CanImportFiles(ICollection<string> files)
  {
    CanImportInfo[] canImportInfoArray = new CanImportInfo[files.Count];
    List<string> fileNames = new List<string>(files.Count);
    List<int> intList = new List<int>(files.Count);
    int index1 = 0;
    foreach (string file in (IEnumerable<string>) files)
    {
      if (this.fileVault.FindArea(file) != this.fileVault.WorkArea)
      {
        canImportInfoArray[index1] = new CanImportInfo(CanImportStatus.ExternalFile);
      }
      else
      {
        fileNames.Add(file);
        intList.Add(index1);
      }
      ++index1;
    }
    if (fileNames.Count != 0)
    {
      List<FileOrigin> fileOrigins = this.fileVault.WorkArea.GetFileOrigins((IList<string>) fileNames, false);
      int index2 = 0;
      foreach (FileOrigin fileOrigin in fileOrigins)
      {
        int index3 = intList[index2];
        canImportInfoArray[index3] = this.CanImportFile(fileOrigin);
        ++index2;
      }
    }
    return (IList<CanImportInfo>) canImportInfoArray;
  }

  private void ImportDeferredFiles(
    string mainFilePath,
    FileImportResult.Success mainFileResult,
    FileImportOptions importOptions,
    IPercentageProgressSink progressSink)
  {
    FileImportOptions importOptions1 = importOptions.Clone();
    importOptions1.ImportDeferredFiles = false;
    importOptions1.ProgressSink = (IPercentageProgressSink) null;
    List<string> collection = new List<string>(1 + mainFileResult.DeferredFiles.Count * 2);
    collection.Add(mainFilePath);
    foreach (string deferredFile in mainFileResult.DeferredFiles)
      CollectionUtils.AddNew<string>((ICollection<string>) collection, deferredFile, (IEqualityComparer<string>) this.filePathComparer);
    int index = 1;
    IDynamicProgressUpdater dynamicProgressUpdater = ProgressSinks.CreateDynamicProgressUpdater(progressSink, collection.Count - index);
    while (index < collection.Count)
    {
      string str = collection[index];
      progressSink.SetState($"Импорт ссылочных зависимостей ({dynamicProgressUpdater.CompletedTasks + 1} из {dynamicProgressUpdater.TotalTasks}): файл {Path.GetFileName(str)}");
      FileImportResult fileImportResult = progressSink.IsCancelled ? this.CreateAbortedImportResult(str) : this.ImportFileCore(str, importOptions1, true);
      switch (fileImportResult)
      {
        case FileImportResult.Success _:
          FileImportResult.Success success = (FileImportResult.Success) fileImportResult;
          if (success.DeferredFiles.Count != 0)
          {
            int num = 0;
            foreach (string deferredFile in success.DeferredFiles)
            {
              if (CollectionUtils.AddNew<string>((ICollection<string>) collection, deferredFile, (IEqualityComparer<string>) this.filePathComparer))
                ++num;
            }
            dynamicProgressUpdater.AddTotalTasks(num);
            break;
          }
          break;
        case FileImportResult.Error _:
          mainFileResult.RelatedErrors.Add(fileImportResult);
          break;
        case FileImportResult.IgnoredFile _:
          mainFileResult.RelatedErrors.Add(fileImportResult);
          break;
      }
      ++index;
      dynamicProgressUpdater.AddCompletedTasks(1);
    }
  }

  private FileImportResult ProcessAlreadyImportedFile(string fullPath, CanImportInfo canImportInfo)
  {
    long importedObjectId = this.GetAlreadyImportedObjectId(canImportInfo.ObjectFileOrigin);
    return DBHelper.IsObjectAlive(importedObjectId) ? (FileImportResult) new FileImportResult.AlreadyImportedFile(fullPath, importedObjectId) : (FileImportResult) new FileImportResult.IgnoredFile(fullPath, $"Невозможно импортировать файл, так как в базе данных уже есть другой объект с таким же именем файла (ид. версии объекта = {importedObjectId}). Этот объект может быть не виден в окнах IPS, так как он был удален и находится в корзине. Переименуйте импортируемый файл и повторите операцию.");
  }

  private long GetAlreadyImportedObjectId(FileOrigin objectFileOrigin)
  {
    if (objectFileOrigin.WorkObject != null)
      return objectFileOrigin.WorkObject.ObjectId;
    VersionsRulePackage editorRule = VersionsRuleSources.GetEditorRule();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return sessionKeeper.Session.GetObjectByVersionsRule(objectFileOrigin.Id, editorRule.OwnerId, true).ObjectID;
  }

  /// <summary>Выполняет пакетный импорт файлов в базу IPS.</summary>
  /// <param name="importTitle">Заголовок окна для выбора импортируемых файлов</param>
  /// <param name="initialDirectory">Начальный каталог для окна выбора импортируемых файлов</param>
  /// <param name="postProcess">Метод для пост-обработки каждого импортированного объекта. Может быть null</param>
  /// <exception cref="T:System.ArgumentException">Не задан заголовок или начальный каталог для окна выбора файлов</exception>
  /// <exception cref="T:System.IO.DirectoryNotFoundException">Начальный каталог для окна выбора файлов не найден на диске</exception>
  /// <exception cref="T:Intermech.FaultException">Файл не может быть импортирован</exception>
  public void BatchImport(string importTitle, string initialDirectory, Action<long> postProcess)
  {
    if (string.IsNullOrEmpty(importTitle))
      throw new ArgumentException();
    if (string.IsNullOrEmpty(initialDirectory))
      throw new ArgumentException();
    if (!Directory.Exists(initialDirectory))
      throw new DirectoryNotFoundException(string.Format(LocalizationHolder.rm.GetString("Client.Core_1305"), (object) initialDirectory));
    OpenFileDialog openFileDialog = new OpenFileDialog();
    openFileDialog.RestoreDirectory = true;
    openFileDialog.Title = importTitle;
    openFileDialog.CheckFileExists = true;
    openFileDialog.Multiselect = true;
    openFileDialog.InitialDirectory = initialDirectory;
    List<string> stringList = new List<string>();
    if ((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IDocumentTypeSettingsService)) is IDocumentTypeSettingsService)
    {
      foreach (int documentType in MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid("cad00070-306c-11d8-b4e9-00304f19f545")))
      {
        DocumentTypeSettings settings = DocumentTypeSettingsCache.GetSettings(documentType);
        if (settings.DocumentFileExt != string.Empty)
        {
          string str = string.Format("*{0}|*{0}", (object) settings.DocumentFileExt.ToLower());
          if (!stringList.Contains(str))
            stringList.Add(str);
        }
      }
    }
    stringList.Sort();
    stringList.Add(LocalizationHolder.rm.GetString("Client.Core_1306"));
    openFileDialog.Filter = string.Join("|", stringList.ToArray());
    openFileDialog.FilterIndex = stringList.Count;
    if (openFileDialog.ShowDialog() != DialogResult.OK || openFileDialog.FileNames.Length == 0)
      return;
    BatchFileImportOptions batchImportOptions = new BatchFileImportOptions();
    batchImportOptions.NotifyOnMasterFileErrors = true;
    batchImportOptions.NotifyOnDeferredFilesErrors = true;
    if (postProcess != null)
      batchImportOptions.AfterImportAction = (Action<FileImportResult.Success>) (importResult => postProcess(importResult.ObjectId));
    this.ReportBatchImportResults(this.ImportFiles((ICollection<string>) openFileDialog.FileNames, batchImportOptions));
  }

  /// <summary>Выполняет пакетный импорт файлов в базу IPS.</summary>
  /// <param name="files">Список абсолютных путей к импортируемым файлам</param>
  /// <param name="postProcess">Метод для пост-обработки каждого импортированного объекта. Может быть null</param>
  /// <exception cref="T:System.ArgumentException">Ошибка в списке путей к импортируемым файлам</exception>
  /// <exception cref="T:Intermech.FaultException">Файл не может быть импортирован</exception>
  public void BatchImport(ICollection<string> files, Action<long> postProcess)
  {
    this.CheckFilesArg(files);
    if (files.Count == 0)
      return;
    BatchFileImportOptions batchImportOptions = new BatchFileImportOptions();
    batchImportOptions.NotifyOnMasterFileErrors = true;
    batchImportOptions.NotifyOnDeferredFilesErrors = true;
    if (postProcess != null)
      batchImportOptions.AfterImportAction = (Action<FileImportResult.Success>) (importResult => postProcess(importResult.ObjectId));
    this.ReportBatchImportResults(this.ImportFiles(files, batchImportOptions));
  }

  /// <summary>Выполняет пакетный импорт файлов в базу IPS.</summary>
  /// <param name="files">Список абсолютных путей к импортируемым файлам</param>
  /// <param name="batchImportOptions">Опции импорта файлов</param>
  /// <returns>Коллекция с результатами импорта файлов</returns>
  /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="files" /> не должен быть равен null. Параметр <paramref name="batchImportOptions" /> не должен быть равен null.</exception>
  /// <exception cref="T:System.ArgumentException">Параметр <paramref name="files" /> содержит недопустимые значения</exception>
  /// <exception cref="T:System.Exception">Ошибка в процессе импорта файла</exception>
  public List<FileImportResult> ImportFiles(
    ICollection<string> files,
    BatchFileImportOptions batchImportOptions)
  {
    this.CheckFilesArg(files);
    this.CheckBatchImportOptionsArgs(batchImportOptions);
    if (files.Count == 0)
      return new List<FileImportResult>(0);
    using (new DynamicScope())
    {
      this.MakeUICommandContext();
      return this.ImportFilesCore(files, batchImportOptions);
    }
  }

  private List<FileImportResult> ImportFilesCore(
    ICollection<string> fileNames,
    BatchFileImportOptions batchImportOptions)
  {
    IList<CanImportInfo> canImportData = this.CanImportFiles(fileNames);
    return this.InvokeWithProgress<List<FileImportResult>>(batchImportOptions, (Func<IMasterSlaveProgressSink, List<FileImportResult>>) (progressSink =>
    {
      List<FileImportResult> fileImportResultList = new List<FileImportResult>(fileNames.Count);
      IProgressUpdater progressUpdater = ProgressSinks.CreateProgressUpdater(progressSink.MasterSink, fileNames.Count);
      int index = 0;
      foreach (string fileName in (IEnumerable<string>) fileNames)
      {
        progressSink.MasterSink.SetState($"Импорт файла ({progressUpdater.CompletedTasks + 1} из {progressUpdater.TotalTasks}): {Path.GetFileName(fileName)}");
        FileImportOptions fileImportOptions = this.CreateSingleFileImportOptions(batchImportOptions, progressSink.CreateSlaveSink());
        FileImportResult importResult = progressSink.MasterSink.IsCancelled ? this.CreateAbortedImportResult(fileName) : this.ImportFile(fileName, fileImportOptions);
        if (importResult is FileImportResult.AlreadyImportedFile && (canImportData[index].Status == CanImportStatus.NewFile || canImportData[index].Status == CanImportStatus.ExternalFile))
        {
          FileImportResult.AlreadyImportedFile alreadyImportedFile = (FileImportResult.AlreadyImportedFile) importResult;
          importResult = (FileImportResult) new FileImportResult.Success(fileName, alreadyImportedFile.ObjectId);
        }
        if (importResult is FileImportResult.Success)
        {
          FileImportResult.Success success = (FileImportResult.Success) importResult;
          if (batchImportOptions.AfterImportAction != null)
            batchImportOptions.AfterImportAction(success);
        }
        else if (batchImportOptions.NotifyOnMasterFileErrors)
          this.ShowErrorNotification(importResult);
        fileImportResultList.Add(importResult);
        progressUpdater.AddCompletedTasks(1);
        ++index;
      }
      return fileImportResultList;
    }));
  }

  private void ReportBatchImportResults(List<FileImportResult> batchImportResults)
  {
    this.outputView.WriteString(LocalizationHolder.rm.GetString("Client.Core_1614"), "Результаты пакетного импорта файлов:");
    foreach (FileImportResult batchImportResult in batchImportResults)
    {
      if (batchImportResult is FileImportResult.Success)
        this.ReportSuccessfulImport((FileImportResult.Success) batchImportResult);
      else
        this.ReportUnsuccessfulImport(batchImportResult);
    }
  }

  private void ReportSuccessfulImport(FileImportResult.Success importResult)
  {
    if (importResult.RelatedErrors.Count == 0)
    {
      string text = $"Файл '{importResult.FilePath}' успешно импортирован в IPS (ид. версии объекта = {importResult.ObjectId}).";
      this.outputView.WriteString(LocalizationHolder.rm.GetString("Client.Core_1614"), text);
    }
    else
    {
      string text = $"Файл '{importResult.FilePath}' частично импортирован в IPS (ид. версии объекта = {importResult.ObjectId}), так как некоторые ссылочные зависимости файла не были импортированы. Для всех таких зависимостей в базе данных IPS были созданы черновики.";
      this.outputView.WriteString(LocalizationHolder.rm.GetString("Client.Core_1614"), text);
    }
  }

  private void ReportUnsuccessfulImport(FileImportResult importResult)
  {
    if (importResult is FileImportResult.AlreadyImportedFile)
    {
      FileImportResult.AlreadyImportedFile alreadyImportedFile = (FileImportResult.AlreadyImportedFile) importResult;
      if (this.fileVault.WorkArea.IsObjectPublished(alreadyImportedFile.ObjectId))
      {
        string text = $"Файл '{alreadyImportedFile.FilePath}' уже был импортирован в IPS ранее (ид. версии объекта = {alreadyImportedFile.ObjectId}).";
        this.outputView.WriteString(LocalizationHolder.rm.GetString("Client.Core_1614"), text);
        return;
      }
    }
    Exception exception = importResult.AsErrorException();
    this.outputView.WriteString(LocalizationHolder.rm.GetString("Client.Core_1614"), exception.Message);
  }

  private void ShowErrorNotification(FileImportResult importResult)
  {
    switch (importResult)
    {
      case FileImportResult.Error _:
      case FileImportResult.IgnoredFile _:
        Exception exception = importResult.AsErrorException().WithRecoveryActions((ErrorRecoveryAction) new OpenFileRecoveryAction(importResult.FilePath));
        UINotificationBuilder notificationBuilder = new UINotificationBuilder();
        notificationBuilder.FillFromException(exception);
        notificationBuilder.Caption = $"Ошибка импорта '{Path.GetFileName(importResult.FilePath)}'";
        this.uiNotificationService.ShowNotification(notificationBuilder.Build());
        break;
    }
  }

  private TResult InvokeWithProgress<TResult>(
    BatchFileImportOptions batchImportOptions,
    Func<IMasterSlaveProgressSink, TResult> action)
  {
    return batchImportOptions.CustomProgressSink == null ? ProgressSinks.DialogService.Invoke<TResult>("Импорт файлов", ProgressSinkDialogFlags.Default, action) : action(batchImportOptions.CustomProgressSink);
  }

  private FileImportOptions CreateSingleFileImportOptions(
    BatchFileImportOptions batchImportOptions,
    IPercentageProgressSink fileProgressSink)
  {
    return new FileImportOptions()
    {
      NotifyOnDeferredFilesErrors = batchImportOptions.NotifyOnDeferredFilesErrors,
      ProgressSink = fileProgressSink
    };
  }

  private void CheckFullPathArg(string fullPath)
  {
    if (string.IsNullOrEmpty(fullPath))
      throw new ArgumentException("Не задан путь к импортируемому файлу.", nameof (fullPath));
    if (!Path.IsPathRooted(fullPath))
      throw new ArgumentException($"Путь к импортируемому файлу '{fullPath}' задан не в абсолютной форме.", nameof (fullPath));
  }

  private void CheckImportOptionsArg(FileImportOptions importOptions)
  {
    if (importOptions == null)
      throw new ArgumentNullException(nameof (importOptions));
  }

  private void CheckFilesArg(ICollection<string> files)
  {
    if (files == null)
      throw new ArgumentNullException(nameof (files));
    if (files.Count == 0)
      return;
    foreach (string file in (IEnumerable<string>) files)
      this.CheckFullPathArg(file);
  }

  private void CheckBatchImportOptionsArgs(BatchFileImportOptions batchImportOptions)
  {
    if (batchImportOptions == null)
      throw new ArgumentNullException(nameof (batchImportOptions));
  }

  /// <summary>
  /// Событие для подключения специализированных методов импорта файлов. Оно вызывается для каждого импортируемого файла.
  /// </summary>
  public event EventHandler<FileProbeEventArgs> FileProbe;

  /// <summary>
  /// Событие для подключения методов импорта файлов, используемых при отсутствии специализированных методов импорта.
  /// </summary>
  public event EventHandler<FileProbeEventArgs> FallbackProbe;
}
