// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.EnterpriseArchive.ImportFilesCommand
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Collections;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.IO;
using Intermech.Localization;
using Intermech.Mvp;
using Intermech.Mvp.Components.Dialogs;
using Intermech.Settings;
using Intermech.Text;
using Intermech.Tools.EnterpriseArchive.SpecialFiles;
using Intermech.Tools.EnterpriseArchive.UI;
using Intermech.Tools.Integrators.FileTrees;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

#nullable disable
namespace Intermech.Tools.EnterpriseArchive;

internal sealed class ImportFilesCommand : EnterpriseArchiveCommand
{
  private static readonly BooleanSwitch traceSwitch = new BooleanSwitch("EnterpriseArchive.ImportFiles", string.Empty, "0");
  private IFileVault fileVault;
  private IFileImportService fileImporter;
  private long userId;
  private string userName;
  private QueueFile queueFile;
  private LinkedList<FileBucket> selectedFiles;
  private bool selectedFilesLocked;
  private VersionsRulePackage editorRule;
  private IReplaceFilePolicy replacePolicy;
  private LinkedList<string> copiedFiles;
  private LinkedList<FileError> copyErrors;
  private PathCollection copiedDependencies;

  public ImportFilesCommand()
    : base(LocalizationHolder.rm.GetString("SR_253"), true)
  {
    this.AutoCloseOnSuccess = true;
  }

  protected override void PrepareCommand()
  {
    base.PrepareCommand();
    this.fileVault = ClientContext.FileVault;
    this.fileImporter = ClientContext.FileImporter;
    ICurrentUserAndRole service = ServiceUtils.GetService<ICurrentUserAndRole>((object) ServicesManager.ServiceContainer, true);
    this.userId = service.UserID;
    this.userName = service.UserName;
  }

  protected override void ResetCommand()
  {
    base.ResetCommand();
    this.fileVault = (IFileVault) null;
    this.fileImporter = (IFileImportService) null;
    this.userId = 0L;
    this.userName = (string) null;
    this.queueFile = (QueueFile) null;
    this.selectedFiles = (LinkedList<FileBucket>) null;
    this.selectedFilesLocked = false;
    this.editorRule = (VersionsRulePackage) null;
    this.replacePolicy = (IReplaceFilePolicy) null;
    this.copiedFiles = (LinkedList<string>) null;
    this.copyErrors = (LinkedList<FileError>) null;
    this.copiedDependencies = (PathCollection) null;
  }

  protected override void DoCommand()
  {
    base.DoCommand();
    this.ReadQueueFile();
    try
    {
      this.SelectFiles();
      this.copiedFiles = new LinkedList<string>();
      this.copyErrors = new LinkedList<FileError>();
      if (!this.selectedFilesLocked)
        this.LockSelectedFiles();
      this.CopyBucketsToVault();
      if (this.copyErrors.Count > 0)
        this.ReportCopyErrors();
    }
    finally
    {
      if (this.selectedFilesLocked)
        this.UnlockNotCopiedSelectedFiles();
    }
    this.ImportFilesFromVault((ICollection<string>) this.copiedFiles);
  }

  private void ReadQueueFile()
  {
    this.CheckAborted();
    this.queueFile = QueueFileServices.ReadQueue();
    if (!ImportFilesCommand.traceSwitch.Enabled)
      return;
    Trace.WriteLine($"ImportFilesCommand: The import queue is loaded from its file. Some stats: import states {this.queueFile.ImportStages.Count}, graph nodes: {this.queueFile.DocumentNodesCount}");
  }

  private ICollection<string> FindLockedVaultFiles()
  {
    PathCollection lockedFiles = LockFileServices.GetLockedFiles(this.userId);
    if (lockedFiles.Count > 0)
    {
      List<string> fileNames1 = new List<string>(lockedFiles.Count);
      List<string> fileNames2 = new List<string>(lockedFiles.Count);
      foreach (string path2 in (OrderedList<string>) lockedFiles)
        (File.Exists(Path.Combine(this.fileVault.WorkArea.AreaPath, path2)) ? fileNames1 : fileNames2).Add(path2);
      List<FileOrigin> fileOrigins = this.fileVault.WorkArea.GetFileOrigins((IList<string>) fileNames1, true);
      LinkedList<FileOrigin> asLinkedList = CollectionUtils.ExtractAsLinkedList<FileOrigin>((IList<FileOrigin>) fileOrigins, (Predicate<FileOrigin>) (origin => origin.OriginType != 0));
      if (fileOrigins.Count > 0)
      {
        LockFileServices.Unlock((ICollection<string>) fileNames2, this.userId);
        LockFileServices.Unlock((ICollection<string>) CollectionUtils.ConvertAsList<FileOrigin, string>((ICollection<FileOrigin>) asLinkedList, (Converter<FileOrigin, string>) (origin => origin.FileName)), this.userId);
        return (ICollection<string>) fileOrigins.ConvertAll<string>((Converter<FileOrigin, string>) (origin => origin.FileName));
      }
      LockFileServices.UnlockAll(this.userId);
    }
    return (ICollection<string>) new string[0];
  }

  private void SelectFiles()
  {
    this.CheckAborted();
    this.DisplayMessage(LocalizationHolder.rm.GetString("SR_264"));
    ICollection<string> lockedVaultFiles = this.FindLockedVaultFiles();
    if (lockedVaultFiles.Count > 0)
    {
      if (ImportFilesCommand.traceSwitch.Enabled)
        TraceUtils.TraceFileList("ImportFilesCommand: the list of found locked vault files", lockedVaultFiles);
      if (!this.AskUserToResumeImport())
      {
        if (ImportFilesCommand.traceSwitch.Enabled)
          Trace.Write("ImportFilesCommand: A user doesn't like to resume import. An unlock will be performed.");
        this.SafeRemoveFilesFromVault(lockedVaultFiles);
        this.UnlockNotCopiedFiles(lockedVaultFiles);
        lockedVaultFiles.Clear();
      }
    }
    if (lockedVaultFiles.Count > 0)
    {
      this.SelectLockedVaultFiles(lockedVaultFiles);
    }
    else
    {
      ImportSourcePresenter importSourcePresenter = new ImportSourcePresenter();
      this.ShowChildModalView((IPresenter) importSourcePresenter);
      switch (importSourcePresenter.SelectedSource)
      {
        case ImportSource.ImportQueue:
          this.SelectFilesFromQueue();
          break;
        case ImportSource.ListFile:
          this.SelectFilesFromListFile();
          break;
        case ImportSource.Disk:
          this.SelectFilesFromDisk();
          break;
        default:
          throw new NotSupportedEnumException((Enum) importSourcePresenter.SelectedSource);
      }
    }
    if (!ImportFilesCommand.traceSwitch.Enabled)
      return;
    TraceUtils.TraceBucketList("ImportFilesCommand: the list of file buckets to process", (ICollection<FileBucket>) this.selectedFiles);
  }

  private bool AskUserToResumeImport()
  {
    YesNoMessagePresenter messagePresenter = new YesNoMessagePresenter(LocalizationHolder.rm.GetString("SR_265"), this.commandName, MessageIcon.Question);
    this.ShowChildModalView((IPresenter) messagePresenter);
    return messagePresenter.IsSuccessful;
  }

  private void SelectLockedVaultFiles(ICollection<string> lockedVaultFiles)
  {
    this.selectedFiles = this.queueFile.GroupFilesByQueue(lockedVaultFiles);
    this.selectedFilesLocked = true;
  }

  private void SelectFilesFromQueue()
  {
    Tuple<IImportStage, LinkedList<FileBucket>> incompleteStage = this.FindIncompleteStage(this.queueFile);
    if (incompleteStage == null)
      throw new CancelCommandException(LocalizationHolder.rm.GetString("SR_266"));
    LockFileServices.FilterAndLock(incompleteStage.Item2, (int) (ValueCell<int>) ArchiveParameters.Common.ImportBatchSize, this.userId, this.userName, false);
    if (incompleteStage.Item2.Count == 0)
      throw new CancelCommandException(LocalizationHolder.rm.GetString("SR_267"));
    this.selectedFiles = incompleteStage.Item2;
    this.selectedFilesLocked = true;
  }

  private Tuple<IImportStage, LinkedList<FileBucket>> FindIncompleteStage(QueueFile queueFile)
  {
    foreach (IImportStage importStage in (IEnumerable<IImportStage>) queueFile.ImportStages)
    {
      List<string> fileNames = new List<string>(importStage.Buckets.Count * 2);
      foreach (ICollection<string> bucket in (IEnumerable<ICollection<string>>) importStage.Buckets)
        fileNames.AddRange((IEnumerable<string>) bucket);
      List<FileOrigin> fileOrigins = this.fileVault.WorkArea.GetFileOrigins((IList<string>) fileNames, true);
      LinkedList<FileBucket> linkedList = new LinkedList<FileBucket>();
      foreach (ICollection<string> bucket in (IEnumerable<ICollection<string>>) importStage.Buckets)
      {
        FileBucket fileBucket = new FileBucket(bucket.Count);
        foreach (string str in (IEnumerable<string>) bucket)
        {
          string fileName = str;
          FileOrigin fileOrigin = fileOrigins.Find((Predicate<FileOrigin>) (origin => PathUtils.IsSamePath(origin.FileName, fileName)));
          if (fileOrigin != null && fileOrigin.OriginType == FileOriginType.NewFile)
            fileBucket.Add(fileName);
        }
        if (fileBucket.Count > 0)
          linkedList.AddLast(fileBucket);
      }
      if (linkedList.Count > 0)
        return new Tuple<IImportStage, LinkedList<FileBucket>>(importStage, linkedList);
    }
    return (Tuple<IImportStage, LinkedList<FileBucket>>) null;
  }

  private void SelectFilesFromListFile()
  {
    OpenFilePresenter openFilePresenter = new OpenFilePresenter();
    openFilePresenter.Title = LocalizationHolder.rm.GetString("SR_268");
    openFilePresenter.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
    openFilePresenter.FileName = "filelist.txt";
    openFilePresenter.ExtensionFilter = LocalizationHolder.rm.GetString("SR_269");
    openFilePresenter.DefaultExtension = "txt";
    openFilePresenter.AllowMultiSelect = false;
    this.ShowChildModalView((IPresenter) openFilePresenter);
    if (openFilePresenter.SelectedFiles.Count == 0)
      throw new AbortException();
    this.SelectFilesFromListFile(openFilePresenter.SelectedFiles[0]);
  }

  private void SelectFilesFromListFile(string listFilePath)
  {
    this.SelectFilesFromDisk((ICollection<string>) new List<string>((IEnumerable<string>) File.ReadAllLines(listFilePath, Encoding.Default)));
  }

  private void SelectFilesFromDisk()
  {
    OpenFilePresenter openFilePresenter = new OpenFilePresenter();
    openFilePresenter.Title = LocalizationHolder.rm.GetString("SR_270");
    openFilePresenter.InitialDirectory = (string) (ValueCell<string>) ArchiveParameters.Common.Location;
    openFilePresenter.ExtensionFilter = LocalizationHolder.rm.GetString("SR_240");
    openFilePresenter.AllowMultiSelect = true;
    this.ShowChildModalView((IPresenter) openFilePresenter);
    if (openFilePresenter.SelectedFiles.Count == 0)
      throw new AbortException();
    this.SelectFilesFromDisk((ICollection<string>) openFilePresenter.SelectedFiles);
  }

  private void SelectFilesFromDisk(ICollection<string> pathList)
  {
    List<string> list = new List<string>((IEnumerable<string>) pathList);
    CollectionUtils.Transform<string>((IList<string>) list, (Converter<string, string>) (path => TextServices.Trim(path)));
    char[] badChars = Path.GetInvalidPathChars();
    list.RemoveAll((Predicate<string>) (path => string.IsNullOrEmpty(path) || path.IndexOfAny(badChars) >= 0));
    CollectionUtils.RemoveDuplicates<string>(list);
    list.RemoveAll((Predicate<string>) (path => !PathUtils.IsPlacedIn(path, (string) (ValueCell<string>) ArchiveParameters.Common.Location)));
    List<string> stringList = list.ConvertAll<string>((Converter<string, string>) (path => PathUtils.GetRelativePath(path, (string) (ValueCell<string>) ArchiveParameters.Common.Location, RelativePathOptions.ThrowIfNotPossible)));
    if (stringList.Count > 0)
    {
      List<FileOrigin> fileOrigins = this.fileVault.WorkArea.GetFileOrigins((IList<string>) stringList, true);
      fileOrigins.RemoveAll((Predicate<FileOrigin>) (origin => origin.OriginType != 0));
      stringList = fileOrigins.ConvertAll<string>((Converter<FileOrigin, string>) (origin => origin.FileName));
    }
    if (stringList.Count > 0)
      LockFileServices.FilterAndLock(stringList, this.userId, this.userName);
    if (stringList.Count < pathList.Count)
    {
      List<string> goodFiles = new List<string>((IEnumerable<string>) stringList);
      if (goodFiles.Count > 0)
        CollectionUtils.Transform<string>((IList<string>) goodFiles, (Converter<string, string>) (path => Path.Combine((string) (ValueCell<string>) ArchiveParameters.Common.Location, path)));
      PathCollection files = new PathCollection((IEnumerable<string>) CollectionUtils.FindAllAsList<string>(pathList, (Predicate<string>) (path => !CollectionUtils.Exists<string>((IEnumerable<string>) goodFiles, (Predicate<string>) (goodPath => PathUtils.IsSamePath(goodPath, path))))));
      if (files.Count > 0)
      {
        FileListExplanationViewModel viewModel = new FileListExplanationViewModel();
        viewModel.Caption = this.commandName;
        viewModel.Explanation = stringList.Count == 0 ? LocalizationHolder.rm.GetString("SR_271") : LocalizationHolder.rm.GetString("SR_272");
        viewModel.FileListName = LocalizationHolder.rm.GetString("SR_273");
        foreach (string str in (OrderedList<string>) files)
          viewModel.FileList.Add(str);
        this.ShowChildModalView((IPresenter) new FileListExplainationPresenter(viewModel));
        if (ImportFilesCommand.traceSwitch.Enabled)
          TraceUtils.TraceFileList("ImportFilesCommand: the list of unsupported files", (ICollection<string>) files);
      }
    }
    this.selectedFiles = new LinkedList<FileBucket>();
    foreach (string str in stringList)
    {
      FileBucket fileBucket = new FileBucket();
      fileBucket.Add(str);
      this.selectedFiles.AddLast(fileBucket);
    }
    this.selectedFilesLocked = true;
  }

  private void LockSelectedFiles()
  {
    this.CheckAborted();
    this.DisplayMessage(LocalizationHolder.rm.GetString("SR_274"));
    LinkedList<FileBucket> linkedList = LockFileServices.FilterAndLock(this.selectedFiles, int.MaxValue, this.userId, this.userName, true);
    this.selectedFilesLocked = true;
    foreach (List<string> stringList in linkedList)
    {
      foreach (string fileName in stringList)
        this.copyErrors.AddLast(new FileError(fileName, LocalizationHolder.rm.GetString("SR_275")));
    }
    if (!ImportFilesCommand.traceSwitch.Enabled)
      return;
    TraceUtils.TraceBucketList("ImportFilesCommand: the list of file buckets after locking", (ICollection<FileBucket>) this.selectedFiles);
    TraceUtils.TraceBucketList("ImportFilesCommand: The list of filtered out buckets", (ICollection<FileBucket>) linkedList);
  }

  private void UnlockNotCopiedSelectedFiles()
  {
    LinkedList<string> linkedList = new LinkedList<string>();
    foreach (FileBucket selectedFile in this.selectedFiles)
      linkedList.AddRange<string>((IEnumerable<string>) selectedFile.FindAll((Predicate<string>) (bucketFile =>
      {
        string localBucketFile = Path.Combine(this.fileVault.WorkArea.AreaPath, bucketFile);
        return !CollectionUtils.Exists<string>((IEnumerable<string>) this.copiedFiles, (Predicate<string>) (copiedFile => PathUtils.IsSamePath(copiedFile, localBucketFile)));
      })));
    if (linkedList.Count <= 0)
      return;
    this.UnlockNotCopiedFiles((ICollection<string>) linkedList);
  }

  private void UnlockNotCopiedFiles(ICollection<string> notCopiedFiles)
  {
    LockFileServices.Unlock(notCopiedFiles, this.userId);
    if (!ImportFilesCommand.traceSwitch.Enabled)
      return;
    TraceUtils.TraceFileList("ImportFilesCommand: the list of unlocked files due to copy errors", notCopiedFiles);
  }

  private void CopyBucketsToVault()
  {
    this.CheckAborted();
    this.DisplayMessage(LocalizationHolder.rm.GetString("SR_276"));
    foreach (FileBucket selectedFile in this.selectedFiles)
    {
      this.CheckAborted();
      try
      {
        this.CopyBucketToVault(selectedFile);
      }
      catch (CopyToVaultException ex)
      {
        if (ImportFilesCommand.traceSwitch.Enabled)
          ImportFilesCommand.TraceCopyException((Exception) ex, selectedFile);
        this.SafeRemoveFilesFromVault((ICollection<string>) selectedFile);
        foreach (string str in (List<string>) selectedFile)
        {
          string error = PathUtils.IsSamePath(str, ex.FileName) ? $"{ex.Message} {ex.InnerException.Message}" : string.Format(LocalizationHolder.rm.GetString("SR_277"), (object) ex.FileName);
          this.copyErrors.AddLast(new FileError(str, error));
        }
      }
      catch (Exception ex)
      {
        if (ImportFilesCommand.traceSwitch.Enabled)
          ImportFilesCommand.TraceCopyException(ex, selectedFile);
        this.SafeRemoveFilesFromVault((ICollection<string>) selectedFile);
        foreach (string fileName in (List<string>) selectedFile)
          this.copyErrors.AddLast(new FileError(fileName, ex.Message));
      }
    }
  }

  private static void TraceCopyException(Exception x, FileBucket bucket)
  {
    Trace.WriteLine($"ImportFilesCommand: (XX) Exception in CopyBucketsToVault. Exception type: {x.GetType()}, exception message: {x.Message}");
    TraceUtils.TraceFileList("ImportFilesCommand: (XX) The list of files in bucket caused to exception", (ICollection<string>) bucket);
  }

  private void CopyBucketToVault(FileBucket bucket)
  {
    foreach (string relativePath in (List<string>) bucket)
      this.ProtectFileIfNeed(relativePath);
    this.CopyDependenciesToVault(bucket);
    List<string> items = new List<string>(bucket.Count);
    foreach (string relativeName in (List<string>) bucket)
      items.Add(this.CopyFileToVault(relativeName));
    this.copiedFiles.AddRange<string>((IEnumerable<string>) items);
  }

  private void CopyDependenciesToVault(FileBucket bucket)
  {
    foreach (string str in (List<string>) bucket)
    {
      ReadOnlyFileTreeNode document = this.queueFile.FindDocument(str);
      if (document != null && document.Dependencies.Count > 0)
      {
        if (this.editorRule == null && this.replacePolicy == null)
        {
          this.editorRule = VersionsRuleSources.GetEditorRule();
          this.replacePolicy = (IReplaceFilePolicy) new PreserveAnyChanges();
        }
        if (this.copiedDependencies == null)
          this.copiedDependencies = new PathCollection(1024 /*0x0400*/);
        foreach (string dependency in (IEnumerable<string>) document.Dependencies)
        {
          string depFileName = dependency;
          if (!bucket.Exists((Predicate<string>) (bucketFile => PathUtils.IsSamePath(bucketFile, depFileName))))
          {
            if (!this.copiedDependencies.Contains(depFileName))
            {
              try
              {
                FileOrigin fileOrigin = this.fileVault.WorkArea.GetFileOrigin(depFileName, true);
                switch (fileOrigin.OriginType)
                {
                  case FileOriginType.WorkFile:
                    this.fileVault.WorkArea.Publish((IList<DBObjectState>) this.fileVault.DBObjectsInfo.CreateStateListForObjectTree(fileOrigin.WorkObject.ObjectId, this.editorRule), this.replacePolicy);
                    break;
                  case FileOriginType.DetachedFile:
                    long objectId;
                    using (SessionKeeper sessionKeeper = new SessionKeeper())
                      objectId = sessionKeeper.Session.GetObjectByVersionsRule(fileOrigin.Id, this.editorRule.OwnerId, true).ObjectID;
                    this.fileVault.WorkArea.Publish((IList<DBObjectState>) this.fileVault.DBObjectsInfo.CreateStateListForObjectTree(objectId, this.editorRule), this.replacePolicy);
                    break;
                }
                if (ImportFilesCommand.traceSwitch.Enabled)
                  Trace.WriteLine($"ImportFilesCommand: The dependency file '{depFileName}' for '{str}' is copied to workspace from IPS database.");
              }
              catch (Exception ex)
              {
                if (ImportFilesCommand.traceSwitch.Enabled)
                  Trace.WriteLine($"ImportFilesCommand: (XX) Exception in CopyDependenciesToVault. File: '{str}', exception type: {ex.GetType()}, exception message: {ex.Message}");
                throw new CopyToVaultException(str, LocalizationHolder.rm.GetString("SR_278"), ex);
              }
              finally
              {
                this.copiedDependencies.Add(depFileName);
              }
            }
          }
        }
      }
    }
  }

  private string CopyFileToVault(string relativeName)
  {
    try
    {
      string str = Path.Combine((string) (ValueCell<string>) ArchiveParameters.Common.Location, relativeName);
      if (!File.Exists(str))
        throw new FileNotFoundException(LocalizationHolder.rm.GetString("SR_279"), str);
      string vault = Path.Combine(this.fileVault.WorkArea.AreaPath, relativeName);
      bool flag = File.Exists(vault);
      if (flag && File.GetLastWriteTime(vault) >= File.GetLastWriteTime(str))
      {
        if (ImportFilesCommand.traceSwitch.Enabled)
          Trace.WriteLine($"ImportFilesCommand: The file '{str}' is not copied. It'sTextCell up to date.");
        return vault;
      }
      string directoryName = Path.GetDirectoryName(vault);
      if (!flag && !Directory.Exists(directoryName))
        Directory.CreateDirectory(directoryName);
      if (flag)
        File.SetAttributes(vault, FileAttributes.Normal);
      File.Copy(str, vault, true);
      File.SetAttributes(vault, FileAttributes.Normal);
      File.SetLastWriteTime(vault, File.GetLastWriteTime(str));
      if (ImportFilesCommand.traceSwitch.Enabled)
        Trace.WriteLine($"ImportFilesCommand: The file '{str}' is copied to workspace.");
      return vault;
    }
    catch (Exception ex)
    {
      if (ImportFilesCommand.traceSwitch.Enabled)
        Trace.WriteLine($"ImportFilesCommand: (XX) Exception in CopyFileToVault. File: '{relativeName}', exception type: {ex.GetType()}, exception message: {ex.Message}");
      throw new CopyToVaultException(relativeName, LocalizationHolder.rm.GetString("SR_280"), ex);
    }
  }

  private void ProtectFileIfNeed(string relativePath)
  {
    string path = Path.Combine((string) (ValueCell<string>) ArchiveParameters.Common.Location, relativePath);
    try
    {
      FileAttributes attributes = File.GetAttributes(path);
      if ((attributes & FileAttributes.ReadOnly) != (FileAttributes) 0)
        return;
      File.SetAttributes(path, attributes | FileAttributes.ReadOnly);
      if (!ImportFilesCommand.traceSwitch.Enabled)
        return;
      Trace.WriteLine($"ImportFilesCommand: The file '{path}' protected with read-only attribute");
    }
    catch (Exception ex)
    {
      if (ImportFilesCommand.traceSwitch.Enabled)
        Trace.WriteLine($"ImportFilesCommand: (XX) Exception in ProtectFileIfNeed. File: '{relativePath}', exception type: {ex.GetType()}, exception message: {ex.Message}");
      throw new CopyToVaultException(relativePath, LocalizationHolder.rm.GetString("SR_281"), ex);
    }
  }

  private void SafeRemoveFilesFromVault(ICollection<string> files)
  {
    foreach (string file in (IEnumerable<string>) files)
    {
      try
      {
        string path = Path.Combine(this.fileVault.WorkArea.AreaPath, file);
        if (File.Exists(path))
        {
          File.SetAttributes(path, FileAttributes.Normal);
          File.Delete(path);
        }
        if (ImportFilesCommand.traceSwitch.Enabled)
          Trace.WriteLine($"ImportFilesCommand: The file '{path}' is deleted from workspace.");
      }
      catch (IOException ex)
      {
      }
      catch (UnauthorizedAccessException ex)
      {
      }
    }
  }

  private void ReportCopyErrors()
  {
    FileErrorsExplanationViewModel viewModel = new FileErrorsExplanationViewModel();
    viewModel.Caption = this.commandName;
    viewModel.Explanation = LocalizationHolder.rm.GetString("SR_282");
    viewModel.FileListName = LocalizationHolder.rm.GetString("SR_235");
    viewModel.FileList.AddRange<FileError>((IEnumerable<FileError>) this.copyErrors);
    this.ShowChildModalView((IPresenter) new FileErrorsExplainationPresenter(viewModel));
    if (!ImportFilesCommand.traceSwitch.Enabled)
      return;
    TraceUtils.TraceFileErrors("ImportFilesCommand: the list of error on file copy", (ICollection<FileError>) this.copyErrors);
  }

  private void ImportFilesFromVault(ICollection<string> localFiles)
  {
    if (ImportFilesCommand.traceSwitch.Enabled)
      TraceUtils.TraceFileList("ImportFilesCommand: the list of workspace files to import in IPS", localFiles);
    this.PostToViewThread((Action) (() => this.fileImporter.BatchImport(localFiles, (Action<long>) null)));
  }
}
