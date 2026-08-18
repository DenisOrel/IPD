// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.EnterpriseArchive.FileStatesPresenter
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.IO;
using Intermech.Localization;
using Intermech.Mvp;
using Intermech.Mvp.Components.Dialogs;
using Intermech.Settings;
using Intermech.Tools.EnterpriseArchive.SpecialFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

#nullable disable
namespace Intermech.Tools.EnterpriseArchive;

internal sealed class FileStatesPresenter : ExtendedPresenter<IFileStatesView>
{
  private readonly FileStatesPresenter.DirectoryMap directoryMap;
  private readonly LinkedList<FileStatesPresenter.FileStateEntry> allFiles;
  private string viewDirectoryPath;
  private List<FileStatesPresenter.FileStateEntry> viewFileList;
  private volatile bool fileStatesComplete;
  private string rootDirectory;

  public FileStatesPresenter()
  {
    this.directoryMap = new FileStatesPresenter.DirectoryMap();
    this.allFiles = new LinkedList<FileStatesPresenter.FileStateEntry>();
  }

  public string RootDirectory
  {
    get => this.rootDirectory;
    set
    {
      this.CheckAllowPropertyChange();
      this.rootDirectory = value;
    }
  }

  protected override void OnAttachView()
  {
    base.OnAttachView();
    this.View.EnableSaveButton(false);
    this.View.EnableSaveAllButton(false);
    this.View.Save += new EventHandler(this.OnSave);
    this.View.SaveAll += new EventHandler(this.OnSaveAll);
    this.View.FileTree.SelectionChanged += new EventHandler(this.OnTreeSelectionChanged);
  }

  protected override void OnDetachView()
  {
    base.OnDetachView();
    this.View.FileTree.SelectionChanged -= new EventHandler(this.OnTreeSelectionChanged);
    this.View.Save -= new EventHandler(this.OnSave);
    this.View.SaveAll -= new EventHandler(this.OnSaveAll);
    this.directoryMap.Clear();
    this.allFiles.Clear();
    this.viewDirectoryPath = (string) null;
    this.viewFileList = (List<FileStatesPresenter.FileStateEntry>) null;
  }

  protected override void OnStartBackgroundTask()
  {
    base.OnStartBackgroundTask();
    ThreadPool.QueueUserWorkItem(new WaitCallback(this.GetFileStatesInBackground));
  }

  private void OnTreeSelectionChanged(object sender, EventArgs e)
  {
    this.viewDirectoryPath = this.View.FileTree.GetSelectedNode();
    if (this.viewDirectoryPath != null)
      this.directoryMap.TryGetValue(this.viewDirectoryPath, out this.viewFileList);
    this.DisplaySelectedFileList();
  }

  private void DisplaySelectedFileList()
  {
    this.View.FileList.ClearItems();
    if (this.viewFileList != null && this.viewFileList.Count > 0)
    {
      foreach (FileStatesPresenter.FileStateEntry viewFile in this.viewFileList)
        this.View.FileList.AppendItem(viewFile.FileName, viewFile.FileName, viewFile.Extenstion, viewFile.Length, viewFile.LastWriteTime, this.GetImportStateName(viewFile.State));
      this.View.FileList.AutoSizeColumns();
      this.View.FileList.ReapplySort();
    }
    this.View.SelectedDir = this.viewDirectoryPath;
    this.ToggleSaveButtons();
  }

  private void OnSave(object sender, EventArgs e)
  {
    this.SaveNotImported((ICollection<FileStatesPresenter.FileStateEntry>) this.viewFileList);
  }

  private void OnSaveAll(object sender, EventArgs e)
  {
    this.SaveNotImported((ICollection<FileStatesPresenter.FileStateEntry>) this.allFiles);
  }

  private void SaveNotImported(
    ICollection<FileStatesPresenter.FileStateEntry> files)
  {
    LinkedList<FileStatesPresenter.FileStateEntry> linkedList = new LinkedList<FileStatesPresenter.FileStateEntry>();
    foreach (FileStatesPresenter.FileStateEntry file in (IEnumerable<FileStatesPresenter.FileStateEntry>) files)
    {
      if (file.State == FileStatesPresenter.ImportState.NotImported)
        linkedList.AddLast(file);
    }
    if (linkedList.Count == 0)
    {
      MvpContext.ViewService.ShowModal((IPresenter) new SimpleMessagePresenter(LocalizationHolder.rm.GetString("SR_255"), LocalizationHolder.rm.GetString("SR_256"), MessageIcon.Information));
    }
    else
    {
      SaveFilePresenter saveFilePresenter = new SaveFilePresenter();
      saveFilePresenter.Title = LocalizationHolder.rm.GetString("SR_257");
      saveFilePresenter.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
      saveFilePresenter.FileName = "filelist.txt";
      saveFilePresenter.ExtensionFilter = LocalizationHolder.rm.GetString("SR_258");
      saveFilePresenter.DefaultExtension = "txt";
      MvpContext.ViewService.ShowModal((IPresenter) saveFilePresenter);
      if (string.IsNullOrEmpty(saveFilePresenter.SelectedPath))
        return;
      using (TextWriter textWriter = (TextWriter) new StreamWriter(saveFilePresenter.SelectedPath, false, Encoding.Default))
      {
        foreach (FileStatesPresenter.FileStateEntry fileStateEntry in linkedList)
          textWriter.WriteLine(fileStateEntry.FilePath);
      }
    }
  }

  private void GetFileStatesInBackground(object state0)
  {
    this.SendToViewThread((Action) (() => this.View.ShowToast(LocalizationHolder.rm.GetString("SR_259"))));
    this.SafeCollectFiles();
    if (!this.IsAttachedToView)
      return;
    this.SafeGetFileStates();
    if (!this.IsAttachedToView)
      return;
    this.SendToViewThread(new Action(this.DisplayGetFileStatesComplete));
  }

  private void DisplayGetFileStatesComplete()
  {
    this.fileStatesComplete = true;
    this.View.HideToast();
    this.ToggleSaveButtons();
  }

  private void ToggleSaveButtons()
  {
    this.View.EnableSaveAllButton(this.fileStatesComplete && this.allFiles.Count > 0);
    this.View.EnableSaveButton(this.fileStatesComplete && this.viewFileList != null && this.viewFileList.Count > 0);
  }

  private void SafeCollectFiles()
  {
    try
    {
      this.CollectFiles();
    }
    catch
    {
    }
  }

  private void CollectFiles()
  {
    LinkedList<FileStatesPresenter.FileStateEntry> bucket = new LinkedList<FileStatesPresenter.FileStateEntry>();
    int num = 16 /*0x10*/;
    try
    {
      foreach (string safeEnumerateFile in ArchiveFiles.SafeEnumerateFiles(this.rootDirectory))
      {
        try
        {
          FileInfo fileInfo = new FileInfo(safeEnumerateFile);
          FileStatesPresenter.FileStateEntry fileStateEntry = new FileStatesPresenter.FileStateEntry(safeEnumerateFile, fileInfo.Length, fileInfo.LastWriteTime);
          bucket.AddLast(fileStateEntry);
        }
        catch (IOException ex)
        {
        }
        if (bucket.Count == num)
        {
          this.SendToViewThread((Action) (() => this.AppendFiles((ICollection<FileStatesPresenter.FileStateEntry>) bucket)));
          bucket.Clear();
          if (num < 1024 /*0x0400*/)
            num += 64 /*0x40*/;
        }
        if (!this.IsAttachedToView)
          break;
      }
    }
    finally
    {
      if (bucket.Count > 0)
        this.SendToViewThread((Action) (() => this.AppendFiles((ICollection<FileStatesPresenter.FileStateEntry>) bucket)));
    }
  }

  private void AppendFiles(
    ICollection<FileStatesPresenter.FileStateEntry> fileBucket)
  {
    bool flag = false;
    foreach (FileStatesPresenter.FileStateEntry fileStateEntry in (IEnumerable<FileStatesPresenter.FileStateEntry>) fileBucket)
    {
      List<FileStatesPresenter.FileStateEntry> fileStateEntryList;
      if (!this.directoryMap.TryGetValue(fileStateEntry.DirectoryPath, out fileStateEntryList))
      {
        fileStateEntryList = new List<FileStatesPresenter.FileStateEntry>();
        this.directoryMap.Add(fileStateEntry.DirectoryPath, fileStateEntryList);
        this.DisplayDirectoryPath(fileStateEntry.DirectoryPath);
        if (string.IsNullOrEmpty(this.viewDirectoryPath))
          this.View.FileTree.SelectNode(fileStateEntry.DirectoryPath);
      }
      fileStateEntryList.Add(fileStateEntry);
      this.allFiles.AddLast(fileStateEntry);
      if (this.viewFileList == fileStateEntryList && !this.View.FileList.ContainsItem(fileStateEntry.FileName))
      {
        this.View.FileList.AppendItem(fileStateEntry.FileName, fileStateEntry.FileName, fileStateEntry.Extenstion, fileStateEntry.Length, fileStateEntry.LastWriteTime, this.GetImportStateName(fileStateEntry.State));
        flag = ((flag ? 1 : 0) | 1) != 0;
      }
    }
    if (!flag)
      return;
    this.View.FileList.AutoSizeColumns();
    this.View.FileList.ReapplySort();
  }

  private void SafeGetFileStates()
  {
    try
    {
      ICollection<FileStatesPresenter.ImportState> fileStates = this.CalculateFileStates();
      this.SendToViewThread((Action) (() => this.DisplayFileStates(fileStates)));
    }
    catch
    {
    }
  }

  private ICollection<FileStatesPresenter.ImportState> CalculateFileStates()
  {
    List<string> fileNames = new List<string>(this.allFiles.Count);
    foreach (FileStatesPresenter.FileStateEntry allFile in this.allFiles)
      fileNames.Add(PathUtils.GetRelativePath(allFile.FilePath, (string) (ValueCell<string>) ArchiveParameters.Common.Location, RelativePathOptions.ThrowIfNotPossible));
    PathCollection lockedFiles = LockFileServices.GetLockedFiles();
    return (ICollection<FileStatesPresenter.ImportState>) ServiceUtils.GetService<IFileVault>((object) ServicesManager.ServiceContainer, true).WorkArea.GetFileOrigins((IList<string>) fileNames, true).ConvertAll<FileStatesPresenter.ImportState>((Converter<FileOrigin, FileStatesPresenter.ImportState>) (origin =>
    {
      if (origin.OriginType == FileOriginType.WorkFile || origin.OriginType == FileOriginType.DetachedFile)
        return FileStatesPresenter.ImportState.Imported;
      return lockedFiles.Contains(origin.FileName) ? FileStatesPresenter.ImportState.InProgress : FileStatesPresenter.ImportState.NotImported;
    }));
  }

  private void DisplayFileStates(
    ICollection<FileStatesPresenter.ImportState> fileStates)
  {
    bool flag = false;
    IEnumerator<FileStatesPresenter.FileStateEntry> enumerator1 = (IEnumerator<FileStatesPresenter.FileStateEntry>) this.allFiles.GetEnumerator();
    IEnumerator<FileStatesPresenter.ImportState> enumerator2 = fileStates.GetEnumerator();
    while (enumerator1.MoveNext())
    {
      enumerator2.MoveNext();
      FileStatesPresenter.FileStateEntry current1 = enumerator1.Current;
      FileStatesPresenter.ImportState current2 = enumerator2.Current;
      current1.State = current2;
      if (!string.IsNullOrEmpty(this.viewDirectoryPath) && PathUtils.IsSamePath(current1.DirectoryPath, this.viewDirectoryPath))
      {
        this.View.FileList.UpdateItem(current1.FileName, this.GetImportStateName(current1.State));
        flag = ((flag ? 1 : 0) | 1) != 0;
      }
    }
    if (!flag)
      return;
    this.View.FileList.AutoSizeColumns();
    this.View.FileList.ReapplySort();
  }

  private string GetImportStateName(FileStatesPresenter.ImportState fileState)
  {
    switch (fileState)
    {
      case FileStatesPresenter.ImportState.NotImported:
        return LocalizationHolder.rm.GetString("SR_262");
      case FileStatesPresenter.ImportState.InProgress:
        return LocalizationHolder.rm.GetString("SR_261");
      case FileStatesPresenter.ImportState.Imported:
        return LocalizationHolder.rm.GetString("SR_260");
      default:
        return LocalizationHolder.rm.GetString("SR_263");
    }
  }

  private void DisplayDirectoryPath(string directoryPath)
  {
    List<string> stringList = PathUtils.SplitPath(directoryPath);
    int num = stringList.Count - 1;
    string str = stringList[0] + (object) Path.DirectorySeparatorChar;
    if (!this.View.FileTree.ContainsNode(str))
      this.View.FileTree.AddRootNode(str, stringList[0]);
    for (int index = 1; index <= num; ++index)
    {
      string key = Path.Combine(str, stringList[index]);
      if (!this.View.FileTree.ContainsNode(key))
      {
        this.View.FileTree.AddChildNode(str, key, stringList[index]);
        if (index == 1)
          this.View.FileTree.ExpandNode(str, true);
      }
      str = key;
    }
  }

  private sealed class DirectoryMap : PathDictionary<List<FileStatesPresenter.FileStateEntry>>
  {
  }

  private sealed class FileStateEntry
  {
    public readonly string FilePath;
    public readonly string DirectoryPath;
    public readonly string FileName;
    public readonly string Extenstion;
    public readonly long Length;
    public readonly DateTime LastWriteTime;
    public FileStatesPresenter.ImportState State;

    public FileStateEntry(string fullPath, long length, DateTime lastWriteTime)
    {
      this.FilePath = fullPath;
      this.DirectoryPath = Path.GetDirectoryName(fullPath);
      this.FileName = Path.GetFileName(fullPath);
      this.Extenstion = Path.GetExtension(this.FileName);
      if (this.Extenstion.Length > 0)
        this.Extenstion = this.Extenstion.Remove(0, 1);
      this.Length = length;
      this.LastWriteTime = lastWriteTime;
      this.State = FileStatesPresenter.ImportState.Unknown;
    }
  }

  private enum ImportState
  {
    Unknown,
    NotImported,
    InProgress,
    Imported,
  }
}
