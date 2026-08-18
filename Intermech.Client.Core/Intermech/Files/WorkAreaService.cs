
// Type: Intermech.Files.WorkAreaService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Collections;
using Intermech.ControlFlow;
using Intermech.Data.KeyValueStores;
using Intermech.Interfaces;
using Intermech.IO;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.CompilerServices;


namespace Intermech.Files;

/// <summary>
/// Реализует рабочую область в файловом хранилище пользователя. Все методы класса являются thread-safe.
/// </summary>
internal sealed class WorkAreaService : 
  AreaBase,
  IWorkArea,
  IFileArea,
  IFileAreaPublishedObjects,
  IFileAreaLocalState
{
  private readonly IOpenFilesService openFiles;
  private FileTracker fileTracker;
  private readonly Lazy<DateTime> userLoginTime;
  private IWorkAreaIndex index;

  public WorkAreaService(FileVaultService vault, string areaDirectory, string displayName)
    : base(vault, areaDirectory, displayName)
  {
    this.openFiles = vault.OpenFilesService;
    this.fileTracker = (FileTracker) new WorkAreaFileTracker(1024 /*0x0400*/);
    this.userLoginTime = new Lazy<DateTime>(new Func<DateTime>(WorkAreaService.GetUserLoginTime));
  }

  /// <summary>Выполняет инициализацию файловой области.</summary>
  internal override void Initialize()
  {
    base.Initialize();
    this.ConvertXmlToDatIndex();
    this.OpenIndex();
    this.CleanupOnDbRestore();
    this.ReportViewOnlyAlteredFiles();
  }

  private string GetIndexFilePath(string fileType)
  {
    return Path.Combine(this.vault.SystemArea.AreaPath, $"{this.areaDirectory}-index.{fileType}");
  }

  private void ConvertXmlToDatIndex()
  {
    string indexFilePath1 = this.GetIndexFilePath("xml");
    if (!File.Exists(indexFilePath1))
      return;
    string indexFilePath2 = this.GetIndexFilePath("dat");
    if (File.Exists(indexFilePath2))
      File.Delete(indexFilePath2);
    WorkAreaXmlIndex workAreaXmlIndex = new WorkAreaXmlIndex(indexFilePath1);
    WorkAreaSQLiteIndexFile sqliteIndexFile = new WorkAreaSQLiteIndexFile(indexFilePath2, 1024 /*0x0400*/, true);
    try
    {
      new WorkAreaSQLiteIndex(sqliteIndexFile).BatchAppend((ICollection<DBObjectState>) workAreaXmlIndex.Query());
    }
    finally
    {
      sqliteIndexFile.ReleaseDatabase();
    }
    File.SetAttributes(indexFilePath1, FileAttributes.Normal);
    File.Delete(indexFilePath1);
  }

  private void OpenIndex()
  {
    this.index = (IWorkAreaIndex) new WorkAreaKVSIndex((BackupReplica<long, WorkAreaIndexDBObjectRecord>) new WorkAreaKVSIndexSQLiteReplica(new WorkAreaSQLiteIndexFile(this.GetIndexFilePath("dat"), 1024 /*0x0400*/, false), this.vault.EventLogService.DefaultLog));
  }

  private void CleanupOnDbRestore()
  {
    UserLoginEventsHandler loginEventsHandler = new UserLoginEventsHandler();
    loginEventsHandler.MarkerFilePath = this.GetIndexFilePath("lck");
    loginEventsHandler.FirstLogin += new EventHandler(this.RemoveForeignObjects);
    loginEventsHandler.LoginAfterDbRestore += new EventHandler(this.RemoveForeignObjects);
    loginEventsHandler.CheckLogin();
  }

  private void RemoveForeignObjects(object sender, EventArgs e)
  {
    this.index.BatchRemove((ICollection<DBObjectState>) this.vault.DBObjectsInfo.ExtractDeadObjects(this.GetPublishedObjects()));
  }

  private void ReportViewOnlyAlteredFiles()
  {
    List<DBObjectState> list = this.index.Query();
    this.vault.DBObjectsInfo.RemoveDeadObjects(list);
    if (list.Count == 0)
      return;
    List<string> pathList = new List<string>(list.Count * 4);
    foreach (DBObjectState dbObjectState in list)
    {
      if (dbObjectState.ModifyMode == ObjectModifyModes.CantModify || dbObjectState.ModifyMode == ObjectModifyModes.CreateVersion)
      {
        foreach (string fileName in this.vault.DBFilesInfo.GetFileNames(dbObjectState.ObjectId))
          pathList.Add(Path.Combine(this.areaPath, fileName));
      }
    }
    if (pathList.Count == 0)
      return;
    this.vault.AlteredFilesService.ReportAlteredFiles((ICollection<string>) pathList);
  }

  /// <summary>
  /// Записывает на диск часть внутреннего состояния файловой области, которая сохраняется между сеансами работы клиента IPS.
  /// Метод используется для сохранения состояния файловой области перед завершением работы клиента IPS.
  /// </summary>
  public void Flush() => this.index.Flush();

  /// <summary>Возвращает трекер состояний файлов в рабочей области.</summary>
  public FileTracker FileTracker => this.fileTracker;

  /// <summary>
  /// Возвращает время начала текущего сеанса работы пользователя.
  /// </summary>
  internal DateTime UserLoginTime => this.userLoginTime.Value;

  private static DateTime GetUserLoginTime()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return sessionKeeper.Session.GetUserLoginEvents().CurrentLoginDateTime;
  }

  /// <summary>
  /// Публикует/обновляет список объектов в рабочей области файлового хранилища.
  /// </summary>
  /// <param name="objectList">Список версий публикуемых объектов</param>
  /// <param name="replaceFilePolicy">Политика перезаписи существующих в рабочей области файлов</param>
  /// <returns>Статистика по файловым операциям в рабочей области</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на список версий объектов и политику перезаписи файлов не может быть null</exception>
  [MethodImpl(MethodImplOptions.Synchronized)]
  public WorkAreaUpdateStats Publish(
    IList<DBObjectState> objectList,
    IReplaceFilePolicy replaceFilePolicy)
  {
    if (objectList == null)
      throw new ArgumentNullException();
    if (replaceFilePolicy == null)
      throw new ArgumentNullException();
    foreach (DBObjectState dbObjectState in (IEnumerable<DBObjectState>) objectList)
    {
      if (dbObjectState == null)
        throw new InvalidOperationException(LocalizationHolder.rm.GetString("Client.Core_1279"));
    }
    return this.PublishInternal(objectList, replaceFilePolicy);
  }

  private WorkAreaUpdateStats PublishInternal(
    IList<DBObjectState> objectList,
    IReplaceFilePolicy replaceFilePolicy)
  {
    List<DBObjectState> collection = new List<DBObjectState>(objectList.Count);
    foreach (DBObjectState newObjectState in (IEnumerable<DBObjectState>) objectList)
    {
      DBObjectState publishedObjectById = this.FindPublishedObjectById(newObjectState.Id);
      if (publishedObjectById != null && this.IsVersionTransition(newObjectState, publishedObjectById))
        collection.Add(publishedObjectById);
    }
    if (collection.Count > 0)
    {
      List<DBObjectState> dbObjectStateList = new List<DBObjectState>((IEnumerable<DBObjectState>) collection);
      this.vault.DBObjectsInfo.RemoveDeadObjects(dbObjectStateList);
      DBObjectFilesDifferenceCalculator differenceCalculator = this.CreateObjectFilesDifferenceCalculator(dbObjectStateList.Count);
      differenceCalculator.AddRange((ICollection<DBObjectState>) dbObjectStateList);
      differenceCalculator.Calculate();
      this.Save(this.vault.DBObjectsInfo.FindUnsavedObjects(differenceCalculator.Results, true));
    }
    DBObjectFilesDifferenceCalculator differenceCalculator1 = this.CreateObjectFilesDifferenceCalculator(objectList.Count);
    differenceCalculator1.AddRange((ICollection<DBObjectState>) objectList);
    differenceCalculator1.Calculate();
    List<WorkAreaPublishItem> workAreaPublishItemList = new List<WorkAreaPublishItem>(objectList.Count);
    foreach (DBObjectFilesDifferences result in differenceCalculator1.Results)
    {
      DBObjectState publishedObjectById = this.FindPublishedObjectById(result.ObjectState.Id);
      this.ApplyReplaceFilePolicy(result, publishedObjectById, replaceFilePolicy);
      workAreaPublishItemList.Add(new WorkAreaPublishItem(result.ObjectState, publishedObjectById, result.DifferencePairs));
    }
    List<string> stringList = new List<string>(workAreaPublishItemList.Count * 16 /*0x10*/);
    foreach (WorkAreaPublishItem workAreaPublishItem in workAreaPublishItemList)
    {
      List<FileDifferencePair> all = workAreaPublishItem.FilePairs.FindAll((Predicate<FileDifferencePair>) (filePair => filePair.DifferenceType == FileDifferenceType.OutdatedFile));
      stringList.AddRange((IEnumerable<string>) all.ConvertAll<string>((Converter<FileDifferencePair, string>) (filePair => Path.Combine(this.areaPath, filePair.LocalState.FileName))));
    }
    List<string> asList = CollectionUtils.ExtractAsList<string>((IList<string>) stringList, new Predicate<string>(((IOpenFiles) this.openFiles).IsReloadable));
    object reloadState = stringList.Count > 0 ? this.openFiles.Unload((IEnumerable<string>) stringList) : (object) null;
    try
    {
      WorkAreaUpdateStats stats = new WorkAreaUpdateStats();
      stats.ReloadedFiles = stringList.Count + asList.Count;
      List<DBObjectState> appendList = new List<DBObjectState>(workAreaPublishItemList.Count);
      List<DBObjectState> updateList = new List<DBObjectState>(workAreaPublishItemList.Count);
      foreach (WorkAreaPublishItem workAreaPublishItem in workAreaPublishItemList)
      {
        ICollection<IFileAttributeAction> actions = this.EmitUpdateActions(workAreaPublishItem, workAreaPublishItem.FilePairs, stats);
        if (actions.Count > 0)
          FileOperations.BatchReadFiles(workAreaPublishItem.DBObject.ObjectId, actions);
        if (workAreaPublishItem.PublishedObject != null)
          updateList.Add(workAreaPublishItem.DBObject);
        else
          appendList.Add(workAreaPublishItem.DBObject);
      }
      this.index.BatchUpdate((ICollection<DBObjectState>) updateList, (ICollection<DBObjectState>) appendList);
      return stats;
    }
    finally
    {
      if (reloadState != null)
        this.openFiles.Reload(reloadState);
      if (asList.Count > 0)
      {
        foreach (string filePath in asList)
          this.openFiles.Reload(filePath);
      }
    }
  }

  private void ReplaceViewOnlyAlteredLocalFiles(DBObjectFilesDifferences dbObjectDiffItem)
  {
    for (int index = 0; index < dbObjectDiffItem.DifferencePairs.Count; ++index)
    {
      FileDifferencePair differencePair = dbObjectDiffItem.DifferencePairs[index];
      if (differencePair.LocalState != null)
      {
        string str = Path.Combine(this.areaPath, differencePair.LocalState.FileName);
        if ((differencePair.DifferenceType == FileDifferenceType.UpdatedFile || differencePair.DifferenceType == FileDifferenceType.UnchangedFile && this.vault.AlteredFilesService.IsFileAltered(str)) && !this.openFiles.IsOpen(str))
        {
          FileDifferencePair fileDifferencePair = new FileDifferencePair(FileDifferenceType.OutdatedFile, differencePair.LocalState, differencePair.RemoteState);
          dbObjectDiffItem.DifferencePairs[index] = fileDifferencePair;
        }
      }
    }
  }

  private void ReplaceAnyAlteredLocalFiles(DBObjectFilesDifferences dbObjectDiffItem)
  {
    for (int index = 0; index < dbObjectDiffItem.DifferencePairs.Count; ++index)
    {
      FileDifferencePair differencePair = dbObjectDiffItem.DifferencePairs[index];
      if (differencePair.DifferenceType != FileDifferenceType.OutdatedFile && differencePair.LocalState != null && differencePair.RemoteState != null)
        dbObjectDiffItem.DifferencePairs[index] = new FileDifferencePair(FileDifferenceType.OutdatedFile, differencePair.LocalState, differencePair.RemoteState);
    }
  }

  private void ApplyReplaceFilePolicy(
    DBObjectFilesDifferences dbObjectDiffItem,
    DBObjectState alreadyPublishedObject,
    IReplaceFilePolicy replaceFilePolicy)
  {
    if (alreadyPublishedObject != null)
    {
      if (this.IsVersionTransition(dbObjectDiffItem.ObjectState, alreadyPublishedObject))
      {
        this.ReplaceAnyAlteredLocalFiles(dbObjectDiffItem);
        return;
      }
      bool flag = this.IsViewOnlyCopy(alreadyPublishedObject);
      if (flag && dbObjectDiffItem.ObjectState.IsEditableState)
      {
        this.ReplaceAnyAlteredLocalFiles(dbObjectDiffItem);
        return;
      }
      if (flag)
      {
        this.ReplaceViewOnlyAlteredLocalFiles(dbObjectDiffItem);
        return;
      }
    }
    List<FileDifferencePair> askUserPairs = new List<FileDifferencePair>(dbObjectDiffItem.DifferencePairs.Count);
    int index = 0;
    while (index < dbObjectDiffItem.DifferencePairs.Count)
    {
      FileDifferencePair differencePair = dbObjectDiffItem.DifferencePairs[index];
      if (differencePair.DifferenceType == FileDifferenceType.UpdatedFile)
      {
        askUserPairs.Add(differencePair);
        dbObjectDiffItem.DifferencePairs.RemoveAt(index);
      }
      else
        ++index;
    }
    if (askUserPairs.Count <= 0)
      return;
    List<FileDifferencePair> collection = replaceFilePolicy.Apply((IWorkArea) this, dbObjectDiffItem.ObjectState, alreadyPublishedObject, askUserPairs);
    dbObjectDiffItem.DifferencePairs.AddRange((IEnumerable<FileDifferencePair>) collection);
  }

  private bool IsViewOnlyCopy(DBObjectState objectState)
  {
    return objectState.ModifyMode == ObjectModifyModes.CantModify || objectState.ModifyMode == ObjectModifyModes.CreateVersion;
  }

  private bool IsVersionTransition(DBObjectState newObjectState, DBObjectState oldObjectState)
  {
    return Math.Abs(newObjectState.ObjectId) != Math.Abs(oldObjectState.ObjectId);
  }

  private ICollection<IFileAttributeAction> EmitUpdateActions(
    WorkAreaPublishItem item,
    List<FileDifferencePair> diffs,
    WorkAreaUpdateStats stats)
  {
    bool flag = !item.DBObject.IsEditableState || item.PublishedObject != null && !item.PublishedObject.IsEditableState;
    LinkedList<IFileAttributeAction> linkedList = new LinkedList<IFileAttributeAction>();
    Dictionary<object, object> dbObjectContext = new Dictionary<object, object>();
    foreach (FileDifferencePair diff in diffs)
    {
      string fileName = diff.RemoteState.FileName;
      string str = Path.Combine(this.areaPath, fileName);
      switch (diff.DifferenceType)
      {
        case FileDifferenceType.MissingFile:
          linkedList.AddLast((IFileAttributeAction) new DownloadFileAction(diff.RemoteState, str));
          if (flag)
          {
            bool attribute = this.vault.ReadOnlyLocalFiles.CalculateAttribute(item.DBObject, (IDictionary<object, object>) dbObjectContext, fileName, str);
            linkedList.AddLast((IFileAttributeAction) new MakeReadOnlyFileAction(str, attribute, this.vault.OpenFilesService));
          }
          linkedList.AddLast(this.CreateUpdateTrackerAction(item.DBObject.ObjectId, diff.RemoteState));
          ++stats.DownloadedFiles;
          continue;
        case FileDifferenceType.OutdatedFile:
          linkedList.AddLast((IFileAttributeAction) new DeleteLocalFileAction(diff.LocalState, str));
          linkedList.AddLast((IFileAttributeAction) new FileAttributeActionAdapter<IAction>((IAction) new DeleteAlteredLocalFileAction(this.vault.AlteredFilesService, str)));
          linkedList.AddLast((IFileAttributeAction) new DownloadFileAction(diff.RemoteState, str));
          if (flag)
          {
            bool attribute = this.vault.ReadOnlyLocalFiles.CalculateAttribute(item.DBObject, (IDictionary<object, object>) dbObjectContext, fileName, str);
            linkedList.AddLast((IFileAttributeAction) new MakeReadOnlyFileAction(str, attribute, this.vault.OpenFilesService));
          }
          linkedList.AddLast(this.CreateUpdateTrackerAction(item.DBObject.ObjectId, diff.RemoteState));
          ++stats.RefreshedFiles;
          continue;
        case FileDifferenceType.UnchangedFile:
        case FileDifferenceType.UpdatedFile:
          if (flag)
          {
            bool attribute = this.vault.ReadOnlyLocalFiles.CalculateAttribute(item.DBObject, (IDictionary<object, object>) dbObjectContext, fileName, str);
            if (FileUtils.GetReadOnlyAttribute(str) != attribute)
              linkedList.AddLast((IFileAttributeAction) new MakeReadOnlyFileAction(str, attribute, this.vault.OpenFilesService));
          }
          if (!this.FileTracker.TryGetLastWriteTime(item.DBObject.ObjectId, diff.RemoteState.FileName).HasValue)
          {
            linkedList.AddLast(this.CreateUpdateTrackerAction(item.DBObject.ObjectId, diff.RemoteState));
            continue;
          }
          continue;
        default:
          continue;
      }
    }
    return (ICollection<IFileAttributeAction>) linkedList;
  }

  private IFileAttributeAction CreateUpdateTrackerAction(long objectId, FileState fileState)
  {
    return (IFileAttributeAction) new FileAttributeActionAdapter<IAction>((IAction) new TrackDownloadedFileAction(this.fileTracker, objectId, fileState));
  }

  /// <summary>
  /// Позволяет найти опубликованные объекты, требующие обновления.
  /// </summary>
  /// <param name="list">Список проверяемых объектов и состояния файлов этих объектов</param>
  /// <param name="applyFileFilter">Флаг, включающий отсеивание файлов, не требующих обновления</param>
  /// <returns>Список объектов, требующих обновления</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на список объектов не может быть null</exception>
  [Obsolete("Use the IDBObjectsInformationService instead of this", true)]
  public List<DBObjectFilesDifferences> FindOutdatedObjects(
    List<DBObjectFilesDifferences> list,
    bool applyFileFilter)
  {
    return this.vault.DBObjectsInfo.FindOutdatedObjects(list, applyFileFilter);
  }

  /// <summary>
  /// Включает в рабочую область объект, который был импортирован в IPS.
  /// </summary>
  /// <param name="objectId">Идентификатор версиb импортированного объекта</param>
  /// <exception cref="T:System.ArgumentException">Идентификатор версии объекта не задан</exception>
  [MethodImpl(MethodImplOptions.Synchronized)]
  public void Attach(long objectId)
  {
    if (objectId == 0L)
      throw new ArgumentException();
    this.AttachInternal((IList<DBObjectState>) new DBObjectState[1]
    {
      this.vault.DBObjectsInfo.GetObjectState(objectId, true)
    });
  }

  /// <summary>
  /// Включает в рабочую область объекты, которые были импортированы в IPS.
  /// </summary>
  /// <param name="objectList">Список идентификаторов версий импортированных объектов</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на список версий объектов не может быть null</exception>
  [MethodImpl(MethodImplOptions.Synchronized)]
  public void Attach(IList<long> objectList)
  {
    List<DBObjectState> objectList1 = objectList != null ? new List<DBObjectState>(objectList.Count) : throw new ArgumentNullException();
    foreach (long objectId in (IEnumerable<long>) objectList)
      objectList1.Add(this.vault.DBObjectsInfo.GetObjectState(objectId, true));
    if (objectList1.Count <= 0)
      return;
    this.AttachInternal((IList<DBObjectState>) objectList1);
  }

  private void AttachInternal(IList<DBObjectState> objectList)
  {
    foreach (DBObjectState dbObjectState in (IEnumerable<DBObjectState>) objectList)
    {
      DBObjectState publishedObjectById = this.FindPublishedObjectById(dbObjectState.Id);
      if (publishedObjectById != null)
        throw new InvalidOperationException(string.Format(LocalizationHolder.rm.GetString("Client.Core_1280"), (object) dbObjectState.Caption, (object) publishedObjectById.ObjectId));
    }
    this.index.BatchAppend((ICollection<DBObjectState>) objectList);
    foreach (DBObjectStateWithFiles fileState in this.vault.DBFilesInfo.GetFileStates(objectList))
    {
      bool readOnly = !fileState.Owner.IsEditableState;
      foreach (FileState file in fileState.Files)
        FileUtils.SetReadOnlyAttribute(Path.Combine(this.areaPath, file.FileName), readOnly);
    }
  }

  /// <summary>
  /// Отменяет публикацию объекта в рабочей области и удаляет его файлы с диска.
  /// </summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <exception cref="T:System.ArgumentException">Идентификатор версии объекта не задан</exception>
  [MethodImpl(MethodImplOptions.Synchronized)]
  public void Unpublish(long objectId)
  {
    DBObjectState dbObjectState = objectId != 0L ? this.FindPublishedObjectByVersionId(objectId) : throw new ArgumentException();
    if (dbObjectState == null)
      return;
    this.UnpublishInternal((IList<DBObjectState>) new DBObjectState[1]
    {
      dbObjectState
    });
  }

  /// <summary>
  /// Отменяет для указанных объектов публикацию в рабочей области файлового хранилища и удаляет их файлы с диска.
  /// </summary>
  /// <param name="objectList">Список идентификаторов версий объектов</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на список идентификаторов не может быть null</exception>
  [MethodImpl(MethodImplOptions.Synchronized)]
  public void Unpublish(IList<long> objectList)
  {
    List<DBObjectState> publishedObjects = objectList != null ? new List<DBObjectState>(objectList.Count) : throw new ArgumentNullException();
    foreach (long objectId in (IEnumerable<long>) objectList)
    {
      DBObjectState objectByVersionId = this.FindPublishedObjectByVersionId(objectId);
      if (objectByVersionId != null)
        publishedObjects.Add(objectByVersionId);
    }
    if (publishedObjects.Count <= 0)
      return;
    this.UnpublishInternal((IList<DBObjectState>) publishedObjects);
  }

  private void UnpublishInternal(IList<DBObjectState> publishedObjects)
  {
    List<DBObjectStateWithFiles> fileStates = this.vault.DBFilesInfo.GetFileStates(publishedObjects);
    List<DBObjectState> list = new List<DBObjectState>(fileStates.Count);
    try
    {
      foreach (DBObjectStateWithFiles objectStateWithFiles in fileStates)
      {
        List<string> stringList = new List<string>(objectStateWithFiles.Files.Count);
        List<string> fileList = new List<string>(objectStateWithFiles.Files.Count);
        foreach (FileState file in objectStateWithFiles.Files)
        {
          string fileName = file.FileName;
          string path = Path.Combine(this.areaPath, fileName);
          if (File.Exists(path))
          {
            stringList.Add(fileName);
            fileList.Add(path);
          }
        }
        if (fileList.Count > 0)
        {
          this.openFiles.Unload((IEnumerable<string>) fileList);
          for (int index = 0; index < fileList.Count; ++index)
          {
            File.SetAttributes(fileList[index], FileAttributes.Normal);
            File.Delete(fileList[index]);
            this.FileTracker.RemoveFileState(stringList[index]);
          }
        }
        list.Add(objectStateWithFiles.Owner);
      }
    }
    finally
    {
      this.index.BatchRemove((ICollection<DBObjectState>) list);
    }
  }

  /// <summary>
  /// Позволяет найти опубликованные объекты, имеющие несохраненные изменения.
  /// </summary>
  /// <param name="list">Список проверяемых объектов и состояния файлов этих объектов</param>
  /// <param name="applyFileFilter">Флаг, включающий отсеивание файлов, не содержащих изменений</param>
  /// <returns>Список объектов с несохраненными изменениями</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на список объектов не может быть null</exception>
  [Obsolete("Use the IDBObjectsInformationService instead of this", true)]
  public List<DBObjectFilesDifferences> FindUnsavedObjects(
    List<DBObjectFilesDifferences> list,
    bool applyFileFilter)
  {
    return this.vault.DBObjectsInfo.FindUnsavedObjects(list, applyFileFilter);
  }

  /// <summary>
  /// Выполняет быстрое сохранение в базу IPS указанного объекта. Если в указанный объект не мог быть
  /// изменен или отсутствует в базе IPS, то метод ничего не делает, исключение при этом не сбрасывается.
  /// </summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <returns>true, если быстрое сохранение в базу IPS действительно выполнялось, иначе - false</returns>
  /// <exception cref="T:System.ArgumentException">Идентификатор версии объекта не задан</exception>
  [MethodImpl(MethodImplOptions.Synchronized)]
  public bool Save(long objectId)
  {
    DBObjectState dbObjectState = objectId != 0L ? this.FindPublishedObjectByVersionId(objectId) : throw new ArgumentException();
    if (dbObjectState != null)
    {
      List<DBObjectState> dbObjectStateList = new List<DBObjectState>();
      dbObjectStateList.Add(dbObjectState);
      this.vault.DBObjectsInfo.RemoveDeadObjects(dbObjectStateList);
      if (dbObjectStateList.Count > 0 && AreaBase.HasAccessRights(dbObjectStateList[0].ObjectId, ActionType.Edit))
      {
        DBObjectFilesDifferenceCalculator differenceCalculator = this.CreateObjectFilesDifferenceCalculator(dbObjectStateList.Count);
        differenceCalculator.AddRange((ICollection<DBObjectState>) dbObjectStateList);
        differenceCalculator.Calculate();
        return this.Save(this.vault.DBObjectsInfo.FindUnsavedObjects(differenceCalculator.Results, true)) > 0;
      }
    }
    return false;
  }

  /// <summary>
  /// Выполняет быстрое сохранение в базу IPS указанных объектов. Список объектов должен быть получен с помощью метода FindUnsavedObjects.
  /// </summary>
  /// <param name="objectList">Список сохраняемых объектов</param>
  /// <returns>Возвращает количество объектов, для которых было выполнено сохранение файлов в базу IPS</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на список объектов не может быть null</exception>
  [MethodImpl(MethodImplOptions.Synchronized)]
  public int Save(List<DBObjectFilesDifferences> objectList)
  {
    if (objectList == null)
      throw new ArgumentNullException(nameof (objectList));
    int num = 0;
    foreach (DBObjectFilesDifferences filesDifferences in objectList)
    {
      if (AreaBase.HasAccessRights(filesDifferences.ObjectState.ObjectId, ActionType.Edit))
      {
        List<IFileAttributeAction> actions = this.EmitUploadActions(filesDifferences.DifferencePairs);
        if (actions.Count > 0)
        {
          FileOperations.BatchUpdateFiles(filesDifferences.ObjectState.ObjectId, (IList<IFileAttributeAction>) actions);
          ++num;
          foreach (IFileAttributeAction fileAttributeAction in actions)
          {
            if (fileAttributeAction is IObjectFilesUploadResult uploadResult)
              new TrackUploadedFileAction(this.fileTracker, filesDifferences.ObjectState.ObjectId, uploadResult).Perform();
          }
        }
      }
    }
    return num;
  }

  private List<IFileAttributeAction> EmitUploadActions(List<FileDifferencePair> diffs)
  {
    List<IFileAttributeAction> fileAttributeActionList = new List<IFileAttributeAction>(diffs.Count);
    foreach (FileDifferencePair diff in diffs)
    {
      if (diff.DifferenceType == FileDifferenceType.UpdatedFile)
        fileAttributeActionList.Add((IFileAttributeAction) new UploadFileAction(diff.LocalState, Path.Combine(this.areaPath, diff.LocalState.FileName)));
    }
    return fileAttributeActionList;
  }

  /// <summary>
  /// Позволяет определить происхождение файла в рабочей области.
  /// </summary>
  /// <param name="fileName">Путь и имя файла</param>
  /// <param name="isRelativeName">Признак, что путь к файлу задан в относительной форме</param>
  /// <returns>Найденные сведения о происхождении файла</returns>
  /// <exception cref="T:System.ArgumentException">Не задан путь к файлу</exception>
  /// <exception cref="T:System.InvalidOperationException">Путь к файлу указан не в абсолютной форме</exception>
  [MethodImpl(MethodImplOptions.Synchronized)]
  public FileOrigin GetFileOrigin(string fileName, bool isRelativeName)
  {
    if (string.IsNullOrEmpty(fileName))
      throw new ArgumentException();
    string fileName1 = isRelativeName ? fileName : this.ConvertFullNameToRelative(fileName);
    DataTable fileNameTable;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      fileNameTable = ServiceUtils.GetService<IFileNamesService>((object) sessionKeeper.Session, true).GetFileNameTable(fileName1, sessionKeeper.Session.SessionGUID);
    if (fileNameTable.Rows.Count <= 0)
      return new FileOrigin(fileName, FileOriginType.NewFile, -1L, (DBObjectState) null);
    long int64 = Convert.ToInt64(fileNameTable.Rows[0][1]);
    DBObjectState workObject = this.index.Find(int64);
    if (workObject != null)
    {
      bool flag = false;
      for (int index = 0; index < fileNameTable.Rows.Count; ++index)
      {
        if (workObject.ObjectId == Convert.ToInt64(fileNameTable.Rows[index][0]))
        {
          flag = true;
          break;
        }
      }
      if (!flag)
        workObject = (DBObjectState) null;
    }
    return workObject == null ? new FileOrigin(fileName, FileOriginType.DetachedFile, int64, (DBObjectState) null) : new FileOrigin(fileName, FileOriginType.WorkFile, int64, workObject);
  }

  /// <summary>
  /// Позволяет определить происхождение указанных файлов в рабочей области.
  /// </summary>
  /// <param name="fileNames">Коллекция путей и имен файлов</param>
  /// <param name="isRelativeNames">Признак, что пути к файлам заданы в относительной форме</param>
  /// <returns>Найденные сведения о происхождении файлов</returns>
  /// <exception cref="T:System.ArgumentException">Не задан путь к файлу</exception>
  /// <exception cref="T:System.InvalidOperationException">Путь к файлу указан не в абсолютной форме</exception>
  [MethodImpl(MethodImplOptions.Synchronized)]
  public List<FileOrigin> GetFileOrigins(IList<string> fileNames, bool isRelativeNames)
  {
    string[] fileName = fileNames != null ? new string[fileNames.Count] : throw new ArgumentNullException();
    PathDictionary<int> pathDictionary = new PathDictionary<int>(fileNames.Count);
    for (int index = 0; index < fileNames.Count; ++index)
    {
      fileName[index] = isRelativeNames ? fileNames[index] : this.ConvertFullNameToRelative(fileNames[index]);
      pathDictionary.Add(fileName[index], index);
    }
    DataTable fileNameTable;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      fileNameTable = ServiceUtils.GetService<IFileNamesService>((object) sessionKeeper.Session, true).GetFileNameTable(fileName, sessionKeeper.Session.SessionGUID);
    FileOrigin[] collection = new FileOrigin[fileNames.Count];
    DataRowCollection rows = fileNameTable.Rows;
    int count = rows.Count;
    int num;
    for (int index1 = 0; index1 < count; index1 += num)
    {
      DataRow dataRow = rows[index1];
      long int64_1 = Convert.ToInt64(dataRow[1]);
      string str = Convert.ToString(dataRow[2]);
      num = 1;
      while (index1 + num < count && PathUtils.IsSamePath(str, Convert.ToString(rows[index1 + num][2])))
        ++num;
      DBObjectState workObject = this.index.Find(int64_1);
      if (workObject != null)
      {
        bool flag = false;
        for (int index2 = index1; index2 < index1 + num; ++index2)
        {
          if (workObject.ObjectId == Convert.ToInt64(rows[index2][0]))
          {
            flag = true;
            break;
          }
        }
        if (!flag)
          workObject = (DBObjectState) null;
      }
      int index3;
      if (pathDictionary.TryGetValue(str, out index3))
      {
        FileOrigin fileOrigin = workObject != null ? new FileOrigin(fileNames[index3], FileOriginType.WorkFile, int64_1, workObject) : new FileOrigin(fileNames[index3], FileOriginType.DetachedFile, int64_1, (DBObjectState) null);
        collection[index3] = fileOrigin;
        pathDictionary.Remove(str);
      }
      else
      {
        long int64_2 = Convert.ToInt64(dataRow[0]);
        throw new InvalidOperationException($"Внутренняя ошибка IPS: для объекта с ид. версии = {int64_2} имена файлов, полученные из сервиса {"IFileNamesService"}, не совпадают с именами из файлового атрибута.").WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(int64_2));
      }
    }
    foreach (KeyValuePair<string, int> keyValuePair in (Dictionary<string, int>) pathDictionary)
      collection[keyValuePair.Value] = new FileOrigin(fileNames[keyValuePair.Value], FileOriginType.NewFile, -1L, (DBObjectState) null);
    return new List<FileOrigin>((IEnumerable<FileOrigin>) collection);
  }

  private string ConvertFullNameToRelative(string fullName)
  {
    return (Path.IsPathRooted(fullName) ? PathUtils.GetRelativePath(fullName, this.areaPath, RelativePathOptions.None) : throw new InvalidOperationException()) ?? throw new InvalidOperationException(string.Format(LocalizationHolder.rm.GetString("Client.Core_1281"), (object) fullName)).WithRecoveryActions((ErrorRecoveryAction) new OpenFileRecoveryAction(fullName));
  }

  /// <summary>
  /// Возвращает список объектов, опубликованных в рабочей области. Для построения списка объектов
  /// используется индекс рабочей области.
  /// </summary>
  /// <returns>Список опубликованных версий объектов</returns>
  [MethodImpl(MethodImplOptions.Synchronized)]
  public List<DBObjectState> GetPublishedObjects() => this.index.Query();

  /// <summary>
  /// Возвращает список объектов, опубликованных в рабочей области и не использовавшихся с указанной даты.
  /// </summary>
  /// <param name="noUseSinceDate">Дата в UTC</param>
  /// <returns>Список опубликованных версий объектов</returns>
  [MethodImpl(MethodImplOptions.Synchronized)]
  public List<DBObjectState> GetPublishedObjects(DateTime noUseSinceDate)
  {
    return this.index.QueryNotUsed(noUseSinceDate);
  }

  /// <summary>Проверяет публикацию объекта в рабочей области.</summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <returns>true, если указанная версия объекта опубликована в рабочей области; false - если не опубликована</returns>
  /// <exception cref="T:System.ArgumentException">Не задан идентификатор версии объекта</exception>
  [MethodImpl(MethodImplOptions.Synchronized)]
  public bool IsObjectPublished(long objectId) => this.index.Contains(objectId);

  /// <summary>
  /// Позволяет найти опубликованный объект по идентификатору версии объекта.
  /// </summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <returns>Состояние опубликованной версии объекта или null, если объект не был опубликован</returns>
  /// <exception cref="T:System.ArgumentException">Не задан идентификатор версии объекта</exception>
  [MethodImpl(MethodImplOptions.Synchronized)]
  public DBObjectState FindPublishedObjectByVersionId(long objectId)
  {
    return this.index.FindByVersionId(objectId);
  }

  /// <summary>
  /// Позволяет найти опубликованный объект по идентификатору объекта.
  /// </summary>
  /// <param name="id">Идентификатор объекта</param>
  /// <returns>Состояние опубликованной версии объекта или null, если объект не был опубликован</returns>
  /// <exception cref="T:System.ArgumentException">Не задан идентификатор объекта</exception>
  [MethodImpl(MethodImplOptions.Synchronized)]
  public DBObjectState FindPublishedObjectById(long id) => this.index.Find(id);

  /// <summary>
  /// Создает объект для определения изменений в локальных файлах объектов IPS.
  /// </summary>
  /// <param name="objectCapacity">Начальная емкость коллекции объектов IPS</param>
  /// <returns>Специализированный объект для пакетного определения изменений в локальных файлах объектов IPS</returns>
  /// <exception cref="T:System.ArgumentOutOfRangeException">objectCapacity</exception>
  public DBObjectFilesDifferenceCalculator CreateObjectFilesDifferenceCalculator(int objectCapacity = 16 /*0x10*/)
  {
    if (objectCapacity < 0)
      throw new ArgumentOutOfRangeException(nameof (objectCapacity));
    return new DBObjectFilesDifferenceCalculator((IFileArea) this, (IDBObjectFilesDifferenceRules) new WorkAreaFilesDifferenceRules(this.FileTracker), this.vault.DBFilesInfo, objectCapacity);
  }
}
