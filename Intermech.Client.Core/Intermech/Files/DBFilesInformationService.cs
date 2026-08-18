
// Type: Intermech.Files.DBFilesInformationService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Files;

/// <summary>
/// Реализация сервиса для получения информации о состояниях файлов объектов IPS в базе данных. Реализация является thread safe.
/// </summary>
internal class DBFilesInformationService : IDBFilesInformationService
{
  /// <summary>Определяет мастер-файл для указанного объекта.</summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <param name="throwIfNotFound">Следует ли сбрасывать исключение при отсутствии мастер-файла у объекта</param>
  /// <returns>Имя файла в относительной форме (так, как оно записано в базе IPS)</returns>
  /// <exception cref="T:System.ArgumentException">objectId - Не задан идентификатор версии объекта IPS</exception>
  /// <exception cref="T:Intermech.FaultException">У объекта отсутствует мастер-файл или нет атрибута "Файл"</exception>
  public string GetMasterFileName(long objectId, bool throwIfNotFound)
  {
    this.CheckObjectIdArg(objectId);
    string firstFileName = this.GetFirstFileName(objectId);
    return firstFileName != null || !throwIfNotFound ? firstFileName : throw new FaultException(LocalizationHolder.rm.GetString("Client.Core_1290"));
  }

  /// <summary>
  /// Возвращает имя первого файла в атрибуте 'Файл' указанного объекта. Значения атрибута, равные DBNULL,
  /// не учитываются методом.
  /// </summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <returns>Имя первого файла объекта или null, если у объекта нет файлов</returns>
  private string GetFirstFileName(long objectId)
  {
    List<string> fileNames;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      fileNames = this.GetFileNames(this.TryGetFileAttribute(sessionKeeper.Session, objectId));
    if (fileNames.Count == 0)
      return (string) null;
    foreach (string firstFileName in fileNames)
    {
      if (!string.IsNullOrEmpty(firstFileName))
        return firstFileName;
    }
    return (string) null;
  }

  /// <summary>
  /// Возвращает список имен файлов в атрибуте 'Файл' указанного объекта. Значения атрибута, равные DBNULL,
  /// игнорируются и в результате выполнения метода не присутствуют.
  /// </summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <returns>Список имен файлов объекта</returns>
  /// <exception cref="T:System.ArgumentException">objectId - Не задан идентификатор версии объекта IPS</exception>
  public List<string> GetFileNames(long objectId)
  {
    this.CheckObjectIdArg(objectId);
    List<string> fileNames;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      fileNames = this.GetFileNames(this.TryGetFileAttribute(sessionKeeper.Session, objectId));
    if (fileNames.Count == 0)
      return fileNames;
    fileNames.RemoveAll(new Predicate<string>(string.IsNullOrEmpty));
    return fileNames;
  }

  /// <summary>
  /// Возвращает идентификаторы блобов файлов в атрибуте 'Файл' указанного объекта. Значения атрибута, равные DBNULL,
  /// игнорируются и в результате выполнения метода не присутствуют.
  /// </summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <returns>Список пар из имен и идентификаторов блобов файлов</returns>
  /// <exception cref="T:System.ArgumentException">objectId - Не задан идентификатор версии объекта IPS</exception>
  public List<Tuple<string, long>> GetFileBlobIds(long objectId)
  {
    this.CheckObjectIdArg(objectId);
    DataTable filesTableFast = this.TryGetFilesTableFast(objectId);
    if (filesTableFast == null)
      return new List<Tuple<string, long>>(0);
    List<Tuple<string, long>> fileBlobIds = new List<Tuple<string, long>>(filesTableFast.Rows.Count);
    foreach (DataRow row in (InternalDataCollectionBase) filesTableFast.Rows)
    {
      string str = Convert.ToString(row[1]);
      if (!string.IsNullOrEmpty(str))
      {
        long int64 = Convert.ToInt64(row[0]);
        fileBlobIds.Add(Tuple.Create<string, long>(str, int64));
      }
    }
    return fileBlobIds;
  }

  /// <summary>
  /// Возвращает описания типов файлов в атрибуте 'Файл' указанного объекта. Значения атрибута, равные DBNULL,
  /// игнорируются и в результате выполнения метода не присутствуют.
  /// </summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <returns>Список типов файлов</returns>
  /// <exception cref="T:System.ArgumentException">objectId - Не задан идентификатор версии объекта IPS</exception>
  public List<Tuple<string, FileTypes>> GetFileTypes(long objectId)
  {
    this.CheckObjectIdArg(objectId);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttribute fileAttribute = this.TryGetFileAttribute(sessionKeeper.Session, objectId);
      List<string> fileNames = this.GetFileNames(fileAttribute);
      if (fileNames.Count == 0)
        return new List<Tuple<string, FileTypes>>(0);
      List<Tuple<string, FileTypes>> fileTypes = new List<Tuple<string, FileTypes>>(fileNames.Count);
      IDBFileAttribute dbFileAttribute = (IDBFileAttribute) fileAttribute;
      for (int index = 0; index < fileNames.Count; ++index)
      {
        string str = fileNames[index];
        if (!string.IsNullOrEmpty(str))
        {
          fileAttribute.Index = index;
          FileTypes fileType = dbFileAttribute.FileType;
          fileTypes.Add(Tuple.Create<string, FileTypes>(str, fileType));
        }
      }
      return fileTypes;
    }
  }

  /// <summary>
  /// Возвращает состояния файлов в атрибуте 'Файл' указанного объекта. Значения атрибута, равные DBNULL,
  /// игнорируются и в результате выполнения метода не присутствуют.
  /// </summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <returns>Список состояний файлов</returns>
  /// <exception cref="T:System.ArgumentException">objectId - Не задан идентификатор версии объекта IPS</exception>
  public List<FileState> GetFileStates(long objectId)
  {
    this.CheckObjectIdArg(objectId);
    DataTable filesTableFast = this.TryGetFilesTableFast(objectId);
    if (filesTableFast == null)
      return new List<FileState>(0);
    List<FileState> fileStates = new List<FileState>(filesTableFast.Rows.Count);
    foreach (DataRow row in (InternalDataCollectionBase) filesTableFast.Rows)
    {
      string fileName = Convert.ToString(row[1]);
      if (!string.IsNullOrEmpty(fileName))
      {
        long int64 = Convert.ToInt64(row[2]);
        DateTime dateTime = Convert.ToDateTime(row[3]);
        fileStates.Add(new FileState(fileName, dateTime, int64));
      }
    }
    return fileStates;
  }

  /// <summary>
  /// Возвращает состояния файлов в атрибуте 'Файл' для указанных объектов. Значения атрибута, равные DBNULL,
  /// игнорируются и в результате выполнения метода не присутствуют.
  /// </summary>
  /// <param name="objects">Список описателей версий объектов</param>
  /// <returns>Список описателей версий объектов и состояний их файлов</returns>
  /// <exception cref="T:ArgumentNullException">objects</exception>
  public List<DBObjectStateWithFiles> GetFileStates(IList<DBObjectState> objects)
  {
    if (objects == null)
      throw new ArgumentNullException(nameof (objects));
    if (objects.Count == 0)
      return new List<DBObjectStateWithFiles>(0);
    long[] objectIds = new long[objects.Count];
    Dictionary<long, int> dictionary = new Dictionary<long, int>(objects.Count);
    DBObjectStateWithFiles[] collection = new DBObjectStateWithFiles[objects.Count];
    for (int index = 0; index < objects.Count; ++index)
    {
      objectIds[index] = objects[index].ObjectId;
      dictionary.Add(objectIds[index], index);
      collection[index] = new DBObjectStateWithFiles(objects[index], new List<FileState>());
    }
    DataTable filesTableFast = this.TryGetFilesTableFast(objectIds);
    if (filesTableFast == null)
      return new List<DBObjectStateWithFiles>(0);
    foreach (DataRow row in (InternalDataCollectionBase) filesTableFast.Rows)
    {
      string fileName = Convert.ToString(row[1]);
      if (!string.IsNullOrEmpty(fileName))
      {
        long int64_1 = Convert.ToInt64(row[2]);
        DateTime dateTime = Convert.ToDateTime(row[3]);
        long int64_2 = Convert.ToInt64(row[6]);
        int index = dictionary[int64_2];
        collection[index].Files.Add(new FileState(fileName, dateTime, int64_1));
      }
    }
    return new List<DBObjectStateWithFiles>((IEnumerable<DBObjectStateWithFiles>) collection);
  }

  private void CheckObjectIdArg(long objectId)
  {
    if (objectId == 0L)
      throw new ArgumentException("Не задан идентификатор версии объекта IPS.", nameof (objectId));
  }

  private IDBAttribute TryGetFileAttribute(IUserSession session, long objectId)
  {
    return session.GetObjectAttribute(objectId, (object) session.IdentHelper.FileAttributeID, false, false);
  }

  private List<string> GetFileNames(IDBAttribute dbFileAttribute)
  {
    return dbFileAttribute == null ? new List<string>(0) : new List<string>((IEnumerable<string>) dbFileAttribute.Descriptions);
  }

  private DataTable TryGetFilesTableFast(long objectId)
  {
    return this.TryGetFilesTableFast(new long[1]{ objectId });
  }

  private DataTable TryGetFilesTableFast(long[] objectIds)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      TimePatrol.CheckClientTime(TimePatrol.GeneralLimit);
      return ServiceUtils.GetService<IFileNamesService>((object) sessionKeeper.Session, true).GetFilesTable(objectIds, sessionKeeper.Session.SessionGUID);
    }
  }
}
