
// Type: Intermech.Files.ViewAreaService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;


namespace Intermech.Files;

/// <summary>
/// Реализует область просмотра в файловом хранилище пользователя. Все методы класса являются thread-safe.
/// </summary>
internal sealed class ViewAreaService : AreaBase, IViewArea, IFileArea
{
  private readonly List<SubArea> subAreas;
  private FileDifferenceCalculator diffCalculator;
  private string indexFilesDirectoryPath;

  public ViewAreaService(FileVaultService vault, string areaDirectory, string displayName)
    : base(vault, areaDirectory, displayName)
  {
    this.subAreas = new List<SubArea>(32 /*0x20*/);
    this.diffCalculator = new FileDifferenceCalculator();
  }

  /// <summary>Выполняет инициализацию файловой области.</summary>
  internal override void Initialize()
  {
    base.Initialize();
    this.indexFilesDirectoryPath = Path.Combine(this.vault.SystemArea.AreaPath, this.areaDirectory);
    if (!Directory.Exists(this.indexFilesDirectoryPath))
      Directory.CreateDirectory(this.indexFilesDirectoryPath);
    this.PrecacheExistingAreas();
  }

  private void PrecacheExistingAreas()
  {
    foreach (string directory in Directory.GetDirectories(this.areaPath, "*", SearchOption.TopDirectoryOnly))
    {
      string relativePath = PathUtils.GetRelativePath(directory, this.areaPath, RelativePathOptions.ThrowIfNotPossible);
      string str = this.MakeIndexFileName(relativePath);
      if (File.Exists(str))
      {
        this.subAreas.Add(new SubArea(this, relativePath, str));
      }
      else
      {
        FileUtils.DeleteFilesSilently(directory, true);
        FileUtils.DeleteDirectorySilently(directory, true);
      }
    }
  }

  /// <summary>
  /// Публикует список объектов в области просмотра файлового хранилища.
  /// </summary>
  /// <param name="objectList">Список версий публикуемых объектов</param>
  /// <returns>Описатель головного объекта после публикации</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на список версий объектов не может быть null</exception>
  [MethodImpl(MethodImplOptions.Synchronized)]
  public PublishedObject Publish(IList<DBObjectState> objectList)
  {
    ReusableSubArea reusableSubArea = objectList != null ? this.FindOrCreateSubArea(this.EmitPublishActions(this.CalculateDiff(objectList))) : throw new ArgumentNullException();
    if (reusableSubArea.MissingItems.Count > 0)
      this.PublishFiles(reusableSubArea.SubArea, reusableSubArea.MissingItems);
    return this.MakePublishedObject(objectList[0], reusableSubArea.SubArea);
  }

  private List<DBObjectFilesDifferences> CalculateDiff(IList<DBObjectState> objectList)
  {
    List<DBObjectStateWithFiles> fileStates = this.vault.DBFilesInfo.GetFileStates(objectList);
    List<DBObjectFilesDifferences> diff = new List<DBObjectFilesDifferences>(fileStates.Count);
    foreach (DBObjectStateWithFiles objectStateWithFiles in fileStates)
    {
      if (objectStateWithFiles.Owner.IsEditableState && this.vault.WorkArea.IsObjectPublished(objectStateWithFiles.Owner.ObjectId))
      {
        List<FileState> remoteStates = new List<FileState>((IEnumerable<FileState>) objectStateWithFiles.Files);
        List<FileState> localStates = new List<FileState>(objectStateWithFiles.Files.Count);
        foreach (FileState file in objectStateWithFiles.Files)
        {
          string path = Path.Combine(this.vault.WorkArea.AreaPath, file.FileName);
          if (File.Exists(path))
          {
            FileState fileState = FileState.FromFile(path, file.FileName);
            localStates.Add(fileState);
          }
        }
        DBObjectFilesDifferences filesDifferences = new DBObjectFilesDifferences(objectStateWithFiles.Owner);
        filesDifferences.DifferencePairs.AddRange((IEnumerable<FileDifferencePair>) this.diffCalculator.Calculate(localStates, remoteStates));
        diff.Add(filesDifferences);
      }
      else
      {
        DBObjectFilesDifferences filesDifferences = new DBObjectFilesDifferences(objectStateWithFiles.Owner);
        foreach (FileState file in objectStateWithFiles.Files)
          filesDifferences.DifferencePairs.Add(this.diffCalculator.Calculate((FileState) null, file));
        diff.Add(filesDifferences);
      }
    }
    return diff;
  }

  private List<ViewAreaPublishItem> EmitPublishActions(List<DBObjectFilesDifferences> totalDiff)
  {
    List<ViewAreaPublishItem> viewAreaPublishItemList = new List<ViewAreaPublishItem>(totalDiff.Count);
    foreach (DBObjectFilesDifferences filesDifferences in totalDiff)
    {
      LinkedList<IViewAreaPublishAction> actions = new LinkedList<IViewAreaPublishAction>();
      foreach (FileDifferencePair differencePair in filesDifferences.DifferencePairs)
      {
        switch (differencePair.DifferenceType)
        {
          case FileDifferenceType.MissingFile:
          case FileDifferenceType.OutdatedFile:
            actions.AddLast((IViewAreaPublishAction) new ViewAreaDownloadAction(differencePair.RemoteState));
            continue;
          case FileDifferenceType.UnchangedFile:
          case FileDifferenceType.UpdatedFile:
            actions.AddLast((IViewAreaPublishAction) new ViewAreaCopyLocalAction(differencePair.RemoteState, differencePair.LocalState, Path.Combine(this.vault.WorkArea.AreaPath, differencePair.LocalState.FileName)));
            continue;
          default:
            continue;
        }
      }
      if (actions.Count > 0)
        viewAreaPublishItemList.Add(new ViewAreaPublishItem(filesDifferences.ObjectState, (ICollection<IViewAreaPublishAction>) actions));
    }
    return viewAreaPublishItemList;
  }

  private ReusableSubArea FindOrCreateSubArea(List<ViewAreaPublishItem> publishItems)
  {
    List<ReusableSubArea> reusableSubAreaList = new List<ReusableSubArea>(this.subAreas.Count + 4);
    foreach (SubArea subArea in this.subAreas)
    {
      List<ViewAreaPublishItem> missingActions = this.FindMissingActions(publishItems, subArea);
      if (missingActions != null)
      {
        ReusableSubArea orCreateSubArea = new ReusableSubArea(subArea, missingActions);
        if (missingActions.Count == 0)
          return orCreateSubArea;
        reusableSubAreaList.Add(orCreateSubArea);
      }
    }
    if (reusableSubAreaList.Count > 0)
    {
      ReusableSubArea orCreateSubArea = reusableSubAreaList[0];
      for (int index = 1; index < reusableSubAreaList.Count; ++index)
      {
        if (reusableSubAreaList[index].MissingItems.Count < orCreateSubArea.MissingItems.Count)
          orCreateSubArea = reusableSubAreaList[index];
      }
      return orCreateSubArea;
    }
    string randomFileName = Path.GetRandomFileName();
    string indexFilePath = this.MakeIndexFileName(randomFileName);
    SubArea subArea1 = new SubArea(this, randomFileName, indexFilePath);
    this.subAreas.Add(subArea1);
    return new ReusableSubArea(subArea1, publishItems);
  }

  private List<ViewAreaPublishItem> FindMissingActions(
    List<ViewAreaPublishItem> publishItems,
    SubArea subArea)
  {
    List<ViewAreaPublishItem> missingActions = new List<ViewAreaPublishItem>(publishItems.Count);
    foreach (ViewAreaPublishItem publishItem in publishItems)
    {
      List<IViewAreaPublishAction> actions = new List<IViewAreaPublishAction>(publishItem.Actions.Count);
      foreach (IViewAreaPublishAction action in (IEnumerable<IViewAreaPublishAction>) publishItem.Actions)
      {
        FileState publishedState = subArea.FindPublishedState(action.PublishedFileState.FileName);
        if (publishedState != null)
        {
          if (publishedState.CompareTo(action.PublishedFileState) != 0)
            return (List<ViewAreaPublishItem>) null;
          string path = Path.Combine(subArea.SubareaPath, publishedState.FileName);
          if (!File.Exists(path) || (publishItem.DBObject.ModifyMode == ObjectModifyModes.CantModify || publishItem.DBObject.ModifyMode == ObjectModifyModes.CreateVersion) && this.vault.AlteredFilesService.IsFileAltered(path))
            return (List<ViewAreaPublishItem>) null;
        }
        else
          actions.Add(action);
      }
      if (actions.Count > 0)
        missingActions.Add(new ViewAreaPublishItem(publishItem.DBObject, (ICollection<IViewAreaPublishAction>) actions));
    }
    return missingActions;
  }

  private void PublishFiles(SubArea subArea, List<ViewAreaPublishItem> missingItems)
  {
    LinkedList<FileState> linkedList = new LinkedList<FileState>();
    try
    {
      foreach (ViewAreaPublishItem missingItem in missingItems)
        this.PublishFiles(subArea, missingItem, linkedList);
    }
    finally
    {
      if (linkedList.Count > 0)
        subArea.PublishFiles((ICollection<FileState>) linkedList);
    }
  }

  private void PublishFiles(
    SubArea subArea,
    ViewAreaPublishItem item,
    LinkedList<FileState> publishedStates)
  {
    List<IFileAttributeAction> actions = new List<IFileAttributeAction>(item.Actions.Count * 2);
    Dictionary<object, object> dbObjectContext = new Dictionary<object, object>();
    foreach (IViewAreaPublishAction action in (IEnumerable<IViewAreaPublishAction>) item.Actions)
    {
      actions.Add(action.EmitFileAction(subArea));
      string fileName = action.DBFileState.FileName;
      string str = Path.Combine(subArea.SubareaPath, fileName);
      bool attribute = this.vault.ReadOnlyLocalFiles.CalculateAttribute(item.DBObject, (IDictionary<object, object>) dbObjectContext, fileName, str);
      actions.Add((IFileAttributeAction) new MakeReadOnlyFileAction(str, attribute, this.vault.OpenFilesService));
    }
    try
    {
      FileOperations.BatchReadFiles(item.DBObject.ObjectId, (ICollection<IFileAttributeAction>) actions);
      foreach (IViewAreaPublishAction action in (IEnumerable<IViewAreaPublishAction>) item.Actions)
        publishedStates.AddLast(action.PublishedFileState);
    }
    catch
    {
      List<string> stringList = new List<string>(item.Actions.Count);
      foreach (IViewAreaPublishAction action in (IEnumerable<IViewAreaPublishAction>) item.Actions)
        stringList.Add(Path.Combine(subArea.SubareaPath, action.PublishedFileState.FileName));
      foreach (string path in stringList)
      {
        if (File.Exists(path))
        {
          File.SetAttributes(path, FileAttributes.Normal);
          File.Delete(path);
        }
      }
      throw;
    }
  }

  private PublishedObject MakePublishedObject(DBObjectState dbObject, SubArea subArea)
  {
    List<Tuple<string, long>> fileBlobIds = this.vault.DBFilesInfo.GetFileBlobIds(dbObject.ObjectId);
    List<PublishedFile> objectFiles = new List<PublishedFile>(fileBlobIds.Count);
    for (int index = 0; index < fileBlobIds.Count; ++index)
    {
      string str1 = fileBlobIds[index].Item1;
      long blobId = fileBlobIds[index].Item2;
      string str2 = Path.Combine(subArea.SubareaPath, str1);
      FileState fileState = FileState.FromFile(str2, str1);
      objectFiles.Add(new PublishedFile(str2, fileState, blobId));
    }
    return new PublishedObject(dbObject, objectFiles.Count > 0 ? objectFiles[0] : (PublishedFile) null, objectFiles);
  }

  /// <summary>Очищает область просмотра.</summary>
  [MethodImpl(MethodImplOptions.Synchronized)]
  public void Cleanup()
  {
    foreach (SubArea subArea in this.subAreas.FindAll((Predicate<SubArea>) (subArea => this.Cleanup(subArea))))
    {
      subArea.Dispose();
      this.subAreas.Remove(subArea);
      FileUtils.DeleteFileSilently(subArea.IndexFilePath);
      FileUtils.DeleteDirectorySilently(subArea.SubareaPath, true);
    }
  }

  private bool Cleanup(SubArea subArea)
  {
    Tuple<PathCollection, bool> tuple = FileUtils.DeleteFilesSilently(subArea.SubareaPath, true);
    if (tuple.Item2)
      subArea.UnpublishAll();
    else
      subArea.UnpublishFiles((ICollection<string>) tuple.Item1);
    return tuple.Item2;
  }

  private string MakeIndexFileName(string subareaDirectoryName)
  {
    return Path.Combine(this.indexFilesDirectoryPath, $"{subareaDirectoryName}-index.dat");
  }
}
