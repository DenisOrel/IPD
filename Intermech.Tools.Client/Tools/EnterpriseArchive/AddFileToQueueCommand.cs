// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.EnterpriseArchive.AddFileToQueueCommand
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
using Intermech.Tools.EnterpriseArchive.SpecialFiles;
using Intermech.Tools.EnterpriseArchive.UI;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.FileTrees;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

#nullable disable
namespace Intermech.Tools.EnterpriseArchive;

internal sealed class AddFileToQueueCommand : EnterpriseArchiveCommand
{
  private static readonly BooleanSwitch traceSwitch = new BooleanSwitch("EnterpriseArchive.AddFileToQueue", string.Empty, "0");
  private IFileVault fileVault;
  private IFileImportService fileImporter;
  private IIntegratorRegistry integrators;
  private QueueFile queueFile;
  private PathCollection stopTable;
  private List<AddFileToQueueCommand.RootFile> innerRootFiles;
  private LinkedList<IIntegrator> capableIntegrators;

  public AddFileToQueueCommand()
    : base(LocalizationHolder.rm.GetString("SR_228"), true)
  {
  }

  protected override void PrepareCommand()
  {
    base.PrepareCommand();
    this.fileVault = ClientContext.FileVault;
    this.fileImporter = ClientContext.FileImporter;
    this.integrators = ClientContext.Integrators;
  }

  protected override void ResetCommand()
  {
    base.ResetCommand();
    this.fileVault = (IFileVault) null;
    this.fileImporter = (IFileImportService) null;
    this.integrators = (IIntegratorRegistry) null;
    this.queueFile = (QueueFile) null;
    this.stopTable = (PathCollection) null;
    this.innerRootFiles = (List<AddFileToQueueCommand.RootFile>) null;
  }

  protected override void DoCommand()
  {
    base.DoCommand();
    this.ReadQueueFile();
    this.SelectRootFiles();
    this.CollectCapableIntegrators();
    LinkedList<ReadOnlyFileTreeNode> fileTrees = this.ScanFileTrees();
    if (fileTrees.Count <= 0)
      return;
    this.UpdateQueueFile(fileTrees);
  }

  private void ReadQueueFile()
  {
    this.CheckAborted();
    this.queueFile = QueueFileServices.ReadQueue();
    this.stopTable = new PathCollection((IEnumerable<string>) this.queueFile.AsList().ConvertAll<string>((Converter<string, string>) (knownFile => Path.Combine((string) (ValueCell<string>) ArchiveParameters.Common.Location, knownFile))));
    if (!AddFileToQueueCommand.traceSwitch.Enabled)
      return;
    Trace.WriteLine($"AddFileToQueueCommand: The import queue is loaded from its file. Some stats: import states {this.queueFile.ImportStages.Count}, graph nodes: {this.queueFile.DocumentNodesCount}");
    TraceUtils.TraceFileList("AddFileToQueueCommand: the list of all files in the import queue", (ICollection<string>) this.stopTable);
  }

  private void UpdateQueueFile(LinkedList<ReadOnlyFileTreeNode> fileTrees)
  {
    this.CheckAborted();
    this.DisplayMessage(LocalizationHolder.rm.GetString("SR_229"));
    PathCollection files = this.queueFile.Append(fileTrees);
    if (files.Count > 0)
      QueueFileServices.ReplaceQueue(this.queueFile);
    if (!AddFileToQueueCommand.traceSwitch.Enabled)
      return;
    TraceUtils.TraceFileList("AddFileToQueueCommand: the list of files added to the import queue", (ICollection<string>) files);
  }

  private void CollectCapableIntegrators()
  {
    this.CheckAborted();
    this.DisplayMessage(LocalizationHolder.rm.GetString("SR_230"));
    List<IntegratorObject> integrators;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      integrators = ServiceUtils.GetService<IIntegratorServer>((object) sessionKeeper.Session, true).GetIntegrators();
    this.capableIntegrators = new LinkedList<IIntegrator>();
    foreach (IIntegrator integrator1 in this.integrators.GetIntegrators())
    {
      IIntegrator integrator = integrator1;
      int index = integrators.FindIndex((Predicate<IntegratorObject>) (iobj => iobj.Id == integrator.Id));
      if (index >= 0)
      {
        integrators.RemoveAt(index);
        if (ServiceUtils.IsServiceAvailable((object) integrator, typeof (IApplicationFileTypes)) && ServiceUtils.IsServiceAvailable((object) integrator, typeof (IFileTreeScanSupport)))
          this.capableIntegrators.AddLast(integrator);
      }
    }
    if (integrators.Count > 0)
    {
      YesNoMessagePresenter dlg = new YesNoMessagePresenter(LocalizationHolder.rm.GetString("SR_231"), this.commandName, MessageIcon.Warning);
      this.SendToViewThread((Action) (() => MvpContext.ViewService.ShowModal((IPresenter) dlg)));
      if (!dlg.IsSuccessful)
        throw new AbortException();
    }
    if (!AddFileToQueueCommand.traceSwitch.Enabled)
      return;
    Trace.WriteLine(string.Format("AddFileToQueueCommand: the list of applications with integrator support"));
    Trace.Indent();
    foreach (IIntegrator capableIntegrator in this.capableIntegrators)
      Trace.WriteLine(capableIntegrator.DisplayName);
    Trace.Unindent();
  }

  private void SelectRootFiles()
  {
    this.CheckAborted();
    this.DisplayMessage(LocalizationHolder.rm.GetString("SR_232"));
    List<string> stringList = this.SelectFilesFromDisk();
    if (AddFileToQueueCommand.traceSwitch.Enabled)
      TraceUtils.TraceFileList("AddFileToQueueCommand: the list of the original root files", (ICollection<string>) stringList);
    LinkedList<FileError> linkedList = new LinkedList<FileError>();
    this.FilterOutImportedFiles(stringList, linkedList);
    this.FilterOutQueuedFiles(stringList, linkedList);
    if (linkedList.Count > 0)
    {
      FileErrorsExplanationViewModel viewModel = new FileErrorsExplanationViewModel();
      viewModel.Caption = this.commandName;
      viewModel.Explanation = stringList.Count == 0 ? LocalizationHolder.rm.GetString("SR_233") : LocalizationHolder.rm.GetString("SR_234");
      viewModel.FileListName = LocalizationHolder.rm.GetString("SR_235");
      viewModel.FileList.AddRange<FileError>((IEnumerable<FileError>) linkedList);
      this.ShowChildModalView((IPresenter) new FileErrorsExplainationPresenter(viewModel));
      if (AddFileToQueueCommand.traceSwitch.Enabled)
        TraceUtils.TraceFileErrors("AddFileToQueueCommand: the list of select errors", (ICollection<FileError>) linkedList);
    }
    this.innerRootFiles = this.ToRootFiles((ICollection<string>) stringList);
    if (!AddFileToQueueCommand.traceSwitch.Enabled)
      return;
    TraceUtils.TraceFileList("AddFileToQueueCommand: the list of the filtered root file", (ICollection<string>) this.innerRootFiles.ConvertAll<string>((Converter<AddFileToQueueCommand.RootFile, string>) (rootFile => rootFile.Path)));
  }

  private List<AddFileToQueueCommand.RootFile> ToRootFiles(ICollection<string> diskFiles)
  {
    return CollectionUtils.ConvertAsList<string, AddFileToQueueCommand.RootFile>(diskFiles, (Converter<string, AddFileToQueueCommand.RootFile>) (path => new AddFileToQueueCommand.RootFile(path)));
  }

  private void FilterOutImportedFiles(List<string> diskFiles, LinkedList<FileError> errors)
  {
    List<string> fileNames = new List<string>(diskFiles.Count);
    foreach (string diskFile in diskFiles)
    {
      if (PathUtils.IsPlacedIn(diskFile, (string) (ValueCell<string>) ArchiveParameters.Common.Location))
      {
        string relativePath = PathUtils.GetRelativePath(diskFile, (string) (ValueCell<string>) ArchiveParameters.Common.Location, RelativePathOptions.ThrowIfNotPossible);
        fileNames.Add(relativePath);
      }
      else
        errors?.AddLast(new FileError(diskFile, LocalizationHolder.rm.GetString("SR_236")));
    }
    diskFiles.Clear();
    if (fileNames.Count <= 0)
      return;
    List<FileOrigin> fileOrigins = this.fileVault.WorkArea.GetFileOrigins((IList<string>) fileNames, true);
    LinkedList<FileOrigin> asLinkedList = CollectionUtils.ExtractAsLinkedList<FileOrigin>((IList<FileOrigin>) fileOrigins, (Predicate<FileOrigin>) (origin => origin.OriginType != 0));
    if (errors != null)
    {
      foreach (FileOrigin fileOrigin in asLinkedList)
        errors.AddLast(new FileError(Path.Combine((string) (ValueCell<string>) ArchiveParameters.Common.Location, fileOrigin.FileName), LocalizationHolder.rm.GetString("SR_237")));
    }
    diskFiles.AddRange((IEnumerable<string>) fileOrigins.ConvertAll<string>((Converter<FileOrigin, string>) (origin => Path.Combine((string) (ValueCell<string>) ArchiveParameters.Common.Location, origin.FileName))));
  }

  private void FilterOutQueuedFiles(List<string> diskFiles, LinkedList<FileError> errors)
  {
    if (diskFiles.Count <= 0)
      return;
    LinkedList<string> asLinkedList = CollectionUtils.ExtractAsLinkedList<string>((IList<string>) diskFiles, new Predicate<string>(((OrderedList<string>) this.stopTable).Contains));
    if (errors == null)
      return;
    foreach (string fileName in asLinkedList)
      errors.AddLast(new FileError(fileName, LocalizationHolder.rm.GetString("SR_238")));
  }

  private List<string> SelectFilesFromDisk()
  {
    OpenFilePresenter dlg = new OpenFilePresenter();
    dlg.Title = LocalizationHolder.rm.GetString("SR_239");
    dlg.InitialDirectory = (string) (ValueCell<string>) ArchiveParameters.Common.Location;
    dlg.ExtensionFilter = LocalizationHolder.rm.GetString("SR_240");
    dlg.AllowMultiSelect = true;
    this.SendToViewThread((Action) (() => MvpContext.ViewService.ShowModal((IPresenter) dlg)));
    if (dlg.SelectedFiles.Count == 0)
      throw new AbortException();
    return new List<string>((IEnumerable<string>) dlg.SelectedFiles);
  }

  private LinkedList<ReadOnlyFileTreeNode> ScanFileTrees()
  {
    this.CheckAborted();
    this.DisplayMessage(LocalizationHolder.rm.GetString("SR_241"));
    LinkedList<ReadOnlyFileTreeNode> allFileTrees = new LinkedList<ReadOnlyFileTreeNode>();
    LinkedList<FileError> linkedList = new LinkedList<FileError>();
    bool allowDelay = true;
    string format = LocalizationHolder.rm.GetString("SR_242");
    bool flag;
    do
    {
      for (int index = 0; index < this.innerRootFiles.Count; ++index)
      {
        AddFileToQueueCommand.RootFile innerRootFile = this.innerRootFiles[index];
        this.CheckAborted();
        this.DisplayMessage(string.Format(format, (object) Path.GetFileName(innerRootFile.Path), (object) (index + 1), (object) this.innerRootFiles.Count));
        if (!this.stopTable.Contains(innerRootFile.Path))
          this.ScanFile(innerRootFile, allowDelay, allFileTrees, linkedList);
      }
      flag = CollectionUtils.Exists<AddFileToQueueCommand.RootFile>((IEnumerable<AddFileToQueueCommand.RootFile>) this.innerRootFiles, (Predicate<AddFileToQueueCommand.RootFile>) (rootFile => rootFile.State != AddFileToQueueCommand.RootFileState.AddedToQueue && !this.stopTable.Contains(rootFile.Path)));
      if (flag)
      {
        allowDelay = false;
        format = LocalizationHolder.rm.GetString("SR_243");
      }
    }
    while (flag);
    this.DisplayMessage(LocalizationHolder.rm.GetString("SR_244"));
    if (linkedList.Count > 0)
    {
      FileErrorsExplanationViewModel viewModel = new FileErrorsExplanationViewModel();
      viewModel.Caption = this.commandName;
      viewModel.Explanation = LocalizationHolder.rm.GetString("SR_245");
      viewModel.FileListName = LocalizationHolder.rm.GetString("SR_235");
      viewModel.FileList.AddRange<FileError>((IEnumerable<FileError>) linkedList);
      this.ShowChildModalView((IPresenter) new FileErrorsExplainationPresenter(viewModel));
      if (AddFileToQueueCommand.traceSwitch.Enabled)
        TraceUtils.TraceFileErrors("AddFileToQueueCommand: the list of scan errors", (ICollection<FileError>) linkedList);
    }
    return allFileTrees;
  }

  private void ScanFile(
    AddFileToQueueCommand.RootFile rootFile,
    bool allowDelay,
    LinkedList<ReadOnlyFileTreeNode> allFileTrees,
    LinkedList<FileError> errors)
  {
    rootFile.Integrator = this.FindIntegratorByFile(rootFile.Path);
    rootFile.State = AddFileToQueueCommand.RootFileState.TypeScannedOnly;
    if (AddFileToQueueCommand.traceSwitch.Enabled)
      Trace.WriteLine($"AddFileToQueueCommand: root file '{rootFile.Path}' will be scanned with {(rootFile.Integrator != null ? (object) rootFile.Integrator.DisplayName : (object) "<none>")}");
    if (rootFile.Integrator != null)
    {
      FileTree fileTree = ServiceUtils.GetService<IFileTreeScanSupport>((object) rootFile.Integrator, true).ScanFile(rootFile.Path, (string) (ValueCell<string>) ArchiveParameters.Common.Location, (ICollection<string>) this.stopTable);
      rootFile.State = AddFileToQueueCommand.RootFileState.AddedToQueue;
      if (AddFileToQueueCommand.traceSwitch.Enabled)
        TraceUtils.TraceFileTree($"AddFileToQueueCommand: the structure of '{rootFile.Path}'", fileTree);
      this.MergeFileTree(fileTree, allFileTrees, errors);
    }
    else if (allowDelay)
    {
      if (AddFileToQueueCommand.traceSwitch.Enabled)
        Trace.WriteLine($"AddFileToQueueCommand: root file '{rootFile.Path}' delayed till next pass.");
      this.InjectSameNamedFiles(rootFile.Path);
    }
    else
    {
      FileTree fileTree = new FileTree();
      fileTree.Nodes.AddLast(new FileTreeNode(rootFile.Path, new List<string>(), new List<string>()));
      rootFile.State = AddFileToQueueCommand.RootFileState.AddedToQueue;
      if (AddFileToQueueCommand.traceSwitch.Enabled)
        TraceUtils.TraceFileTree($"AddFileToQueueCommand: the structure of '{rootFile.Path}'", fileTree);
      this.MergeFileTree(fileTree, allFileTrees, errors);
    }
  }

  private void MergeFileTree(
    FileTree rootFileTree,
    LinkedList<ReadOnlyFileTreeNode> allFileTrees,
    LinkedList<FileError> errors)
  {
    foreach (FileTreeNode node in rootFileTree.Nodes)
    {
      this.AddToStopTable(node);
      ReadOnlyFileTreeNode relativeForm = this.ToRelativeForm(node, errors);
      if (relativeForm != null)
        allFileTrees.AddLast(relativeForm);
    }
    foreach (string badFile in rootFileTree.BadFiles)
    {
      FileError fileError = this.CheckPath(badFile) ?? new FileError(badFile, LocalizationHolder.rm.GetString("SR_246"));
      errors.AddLast(fileError);
      this.stopTable.Add(badFile);
    }
    foreach (FileTreeNode node in rootFileTree.Nodes)
    {
      this.InjectSameNamedFiles(node.Path);
      foreach (string satellite in (IEnumerable<string>) node.Satellites)
        this.InjectSameNamedFiles(satellite);
    }
  }

  private IIntegrator FindIntegratorByFile(string rootFile)
  {
    if (this.capableIntegrators.Count > 0)
    {
      FileInfo fileInfo = new FileInfo(rootFile);
      using (Stream fileContent = (Stream) new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
      {
        foreach (IIntegrator capableIntegrator in this.capableIntegrators)
        {
          fileContent.Position = 0L;
          if (ServiceUtils.GetService<IApplicationFileTypes>((object) capableIntegrator, true).IsApplicationFile(fileInfo, fileContent))
            return capableIntegrator;
        }
      }
    }
    return (IIntegrator) null;
  }

  private void AddToStopTable(FileTreeNode node)
  {
    this.stopTable.Add(node.Path);
    foreach (string satellite in (IEnumerable<string>) node.Satellites)
      this.stopTable.Add(satellite);
    foreach (string dependency in (IEnumerable<string>) node.Dependencies)
      this.stopTable.Add(dependency);
  }

  private ReadOnlyFileTreeNode ToRelativeForm(FileTreeNode node, LinkedList<FileError> errors)
  {
    Tuple<string, FileError> relativeForm1 = this.ToRelativeForm(node.Path);
    if (relativeForm1.Item2 != null)
    {
      errors.AddLast(relativeForm1.Item2);
      if (node.Satellites.Count > 0)
      {
        string error = string.Format(LocalizationHolder.rm.GetString("SR_247"), (object) Path.GetFileName(node.Path));
        foreach (string satellite in (IEnumerable<string>) node.Satellites)
          errors.AddLast(new FileError(satellite, error));
      }
      if (AddFileToQueueCommand.traceSwitch.Enabled)
        Trace.WriteLine($"AddFileToQueue: the node with master file '{node.Path}' is ignored due to file errors");
      return (ReadOnlyFileTreeNode) null;
    }
    string path = relativeForm1.Item1;
    List<string> satellites = new List<string>(node.Satellites.Count);
    foreach (string satellite in (IEnumerable<string>) node.Satellites)
    {
      Tuple<string, FileError> relativeForm2 = this.ToRelativeForm(satellite);
      if (relativeForm2.Item2 == null)
      {
        if (!string.IsNullOrEmpty(relativeForm2.Item1))
          satellites.Add(relativeForm2.Item1);
      }
      else
        errors.AddLast(relativeForm2.Item2);
    }
    List<string> dependencies = new List<string>(node.Dependencies.Count);
    foreach (string dependency in (IEnumerable<string>) node.Dependencies)
    {
      Tuple<string, FileError> relativeForm3 = this.ToRelativeForm(dependency);
      if (relativeForm3.Item2 == null && !string.IsNullOrEmpty(relativeForm3.Item1))
        dependencies.Add(relativeForm3.Item1);
    }
    return new ReadOnlyFileTreeNode(path, satellites, dependencies);
  }

  private Tuple<string, FileError> ToRelativeForm(string fullPath)
  {
    FileError fileError = this.CheckPath(fullPath);
    if (fileError != null)
      return new Tuple<string, FileError>((string) null, fileError);
    string relativePath = PathUtils.GetRelativePath(fullPath, (string) (ValueCell<string>) ArchiveParameters.Common.Location, RelativePathOptions.None);
    return string.IsNullOrEmpty(relativePath) ? new Tuple<string, FileError>((string) null, new FileError(fullPath, LocalizationHolder.rm.GetString("SR_236"))) : new Tuple<string, FileError>(relativePath, (FileError) null);
  }

  private FileError CheckPath(string path)
  {
    if (path.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
      return new FileError(path, LocalizationHolder.rm.GetString("SR_248"));
    return !Path.IsPathRooted(path) ? new FileError(path, LocalizationHolder.rm.GetString("SR_249")) : (FileError) null;
  }

  private void InjectSameNamedFiles(string fullPath)
  {
    string searchPattern = Path.GetFileNameWithoutExtension(fullPath) + "*";
    List<string> diskFiles = new List<string>((IEnumerable<string>) Directory.GetFiles(Path.GetDirectoryName(fullPath), searchPattern));
    this.FilterOutQueuedFiles(diskFiles, (LinkedList<FileError>) null);
    this.FilterOutImportedFiles(diskFiles, (LinkedList<FileError>) null);
    diskFiles.RemoveAll((Predicate<string>) (foundFile => this.innerRootFiles.Exists((Predicate<AddFileToQueueCommand.RootFile>) (innerFile => PathUtils.IsSamePath(innerFile.Path, foundFile)))));
    this.innerRootFiles.AddRange((IEnumerable<AddFileToQueueCommand.RootFile>) this.ToRootFiles((ICollection<string>) diskFiles));
  }

  private sealed class RootFile
  {
    public readonly string Path;
    public AddFileToQueueCommand.RootFileState State;
    public IIntegrator Integrator;

    public RootFile(string path) => this.Path = path;
  }

  private enum RootFileState
  {
    Unscanned,
    TypeScannedOnly,
    AddedToQueue,
  }
}
