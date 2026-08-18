
// Type: Intermech.Files.DBObjectsInformationService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using System;
using System.Collections.Generic;


namespace Intermech.Files;

/// <summary>
/// Реализация сервиса для получения информации о состояниях объектов IPS в базе данных. Реализация является thread safe.
/// </summary>
internal class DBObjectsInformationService : IDBObjectsInformationService
{
  private IFileAttributeEditorService fileAttributeEditorService;

  public DBObjectsInformationService(
    IFileAttributeEditorService fileAttributeEditorService)
  {
    this.fileAttributeEditorService = fileAttributeEditorService;
  }

  /// <summary>Возвращает состояние объекта в базе IPS.</summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <param name="throwIfNotFound">Признак, нужно ли сбрасывать исключение при отсутствии объекта</param>
  /// <returns>Состояние объекта в базе или null, если указанного объекта нет в базе IPS</returns>
  /// <exception cref="T:Intermech.KernelException">Указанный объект отсутствует в базе IPS</exception>
  public DBObjectState GetObjectState(long objectId, bool throwIfNotFound)
  {
    if (objectId == 0L)
      throw new ArgumentException();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectId, throwIfNotFound);
      return dbObject != null ? new DBObjectState(dbObject.ID, objectId, dbObject.ObjectModifyMode, dbObject.Caption) : (DBObjectState) null;
    }
  }

  /// <summary>Возвращает состояние объекта в базе IPS.</summary>
  /// <param name="dbObject">Идентификатор версии объекта</param>
  /// <returns>Состояние объекта IPS</returns>
  /// <exception cref="T:ArgumentNullException">dbObject</exception>
  public DBObjectState GetObjectState(IDBObject dbObject)
  {
    if (dbObject == null)
      throw new ArgumentNullException(nameof (dbObject));
    return new DBObjectState(dbObject.ID, dbObject.ObjectID, dbObject.ObjectModifyMode, dbObject.Caption);
  }

  /// <summary>
  /// Создает список, содержащий один указанный объект. Этот метод используется в случаях, когда требуется опубликовать
  /// объект без учета его связей с другими объектами.
  /// </summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <returns>Построенный список состояний объектов</returns>
  /// <exception cref="T:Intermech.KernelException">Указанный объект отсутствует в базе IPS</exception>
  public List<DBObjectState> CreateStateListForSingleObject(long objectId)
  {
    return new SkipObjectTreeBuilder(objectId, (IDBObjectsInformationService) this).BuildList();
  }

  /// <summary>
  /// Создает список, содержащий указанный объект и все связанные с ним объекты по всем типам связей, для которых настроено
  /// извлечение файлов.
  /// </summary>
  /// <param name="rootObjectId">Идентификатор версии корневого объекта</param>
  /// <param name="versionsRule">Правило подбора версий объектов</param>
  /// <returns>Построенный список состояний объектов</returns>
  /// <exception cref="T:Intermech.KernelException">Указанный объект отсутствует в базе IPS</exception>
  public List<DBObjectState> CreateStateListForObjectTree(
    long rootObjectId,
    VersionsRulePackage versionsRule)
  {
    return new RecursiveTreeBuilder(rootObjectId, versionsRule, (IDBObjectsInformationService) this, this.fileAttributeEditorService).BuildList();
  }

  /// <summary>
  /// Удаляет из списка все объекты, которые не были ранее опубликованы в указанной файловой области.
  /// </summary>
  /// <param name="list">Список объектов</param>
  /// <param name="area">Файловая область</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылки на список объектов и файловую область не должны быть null</exception>
  public void RemoveUnpublishedObjects(List<DBObjectState> list, IFileAreaPublishedObjects area)
  {
    if (list == null)
      throw new ArgumentNullException(nameof (list));
    if (area == null)
      throw new ArgumentNullException(nameof (area));
    list.RemoveAll((Predicate<DBObjectState>) (objectState => !area.IsObjectPublished(objectState.ObjectId)));
  }

  /// <summary>
  /// Извлекает из списка все объекты, которые не были ранее опубликованы в указанной файловой области.
  /// </summary>
  /// <param name="list">Список объектов</param>
  /// <param name="area">Файловая область</param>
  /// <returns>Список с извлеченными неопубликованными объектами</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылки на список объектов и файловую область не должны быть null</exception>
  public List<DBObjectState> ExtractUnpublishedObjects(
    List<DBObjectState> list,
    IFileAreaPublishedObjects area)
  {
    if (list == null)
      throw new ArgumentNullException(nameof (list));
    if (area == null)
      throw new ArgumentNullException(nameof (area));
    List<DBObjectState> unpublishedObjects = new List<DBObjectState>(list.Count);
    for (int index = list.Count - 1; index >= 0; --index)
    {
      if (!area.IsObjectPublished(list[index].ObjectId))
      {
        unpublishedObjects.Add(list[index]);
        list.RemoveAt(index);
      }
    }
    return unpublishedObjects;
  }

  /// <summary>
  /// Удаляет из списка все мертвые объекты (отсутствующие в базе IPS). Этот метод используется в случаях, когда список
  /// построен на основе локальных данных о публикации объектов в файловой области.
  /// </summary>
  /// <param name="list">Список объектов</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на список объектов не должна быть null</exception>
  public void RemoveDeadObjects(List<DBObjectState> list)
  {
    long[] objectIds = list != null ? new long[list.Count] : throw new ArgumentNullException(nameof (list));
    for (int index = 0; index < list.Count; ++index)
      objectIds[index] = list[index].ObjectId;
    List<long> aliveObjectsTable = DBHelper.GetLiveObjectsOnly((ICollection<long>) objectIds);
    aliveObjectsTable.Sort();
    list.RemoveAll((Predicate<DBObjectState>) (objectState => aliveObjectsTable.BinarySearch(objectState.ObjectId) < 0));
  }

  /// <summary>
  /// Извлекает из списка все мертвые объекты (отсутствующие в базе IPS). Этот метод используется в случаях, когда список
  /// построен на основе локальных данных о публикации объектов в файловой области.
  /// </summary>
  /// <param name="list">Список объектов</param>
  /// <returns>Список с извлеченными мертвыми объектами</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на список объектов не должна быть null</exception>
  public List<DBObjectState> ExtractDeadObjects(List<DBObjectState> list)
  {
    long[] objectIds = list != null ? new long[list.Count] : throw new ArgumentNullException(nameof (list));
    for (int index = 0; index < list.Count; ++index)
      objectIds[index] = list[index].ObjectId;
    List<long> liveObjectsOnly = DBHelper.GetLiveObjectsOnly((ICollection<long>) objectIds);
    liveObjectsOnly.Sort();
    List<DBObjectState> deadObjects = new List<DBObjectState>(list.Count);
    for (int index = list.Count - 1; index >= 0; --index)
    {
      if (liveObjectsOnly.BinarySearch(list[index].ObjectId) < 0)
      {
        deadObjects.Add(list[index]);
        list.RemoveAt(index);
      }
    }
    return deadObjects;
  }

  /// <summary>
  /// Позволяет найти объекты IPS, чьи локальные файлы устарели/отсутствуют и, соответственно, требуют замены файлами из базы данных.
  /// </summary>
  /// <param name="list">Список проверяемых объектов IPS и состояния файлов этих объектов</param>
  /// <param name="applyFileFilter">Флаг, включающий отсеивание файлов, не требующих обновления</param>
  /// <returns>Список объектов IPS, требующих обновления</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на список объектов IPS не может быть null</exception>
  public List<DBObjectFilesDifferences> FindOutdatedObjects(
    List<DBObjectFilesDifferences> list,
    bool applyFileFilter)
  {
    List<DBObjectFilesDifferences> outdatedObjects = list != null ? new List<DBObjectFilesDifferences>(list.Count) : throw new ArgumentNullException(nameof (list));
    if (list.Count > 0)
    {
      foreach (DBObjectFilesDifferences filesDifferences1 in list)
      {
        Predicate<FileDifferencePair> filter = filesDifferences1.ObjectState.IsEditableState ? new Predicate<FileDifferencePair>(this.OutdatedFilePredicate) : new Predicate<FileDifferencePair>(this.BrokenFilePredicate);
        if (filesDifferences1.DifferencePairs.Exists(filter))
        {
          DBObjectFilesDifferences filesDifferences2 = filesDifferences1.Clone();
          if (applyFileFilter)
            filesDifferences2.DifferencePairs.RemoveAll((Predicate<FileDifferencePair>) (diffPair => !filter(diffPair)));
          outdatedObjects.Add(filesDifferences2);
        }
      }
    }
    return outdatedObjects;
  }

  private bool OutdatedFilePredicate(FileDifferencePair diffPair)
  {
    return diffPair.DifferenceType == FileDifferenceType.MissingFile || diffPair.DifferenceType == FileDifferenceType.OutdatedFile;
  }

  private bool BrokenFilePredicate(FileDifferencePair diffPair)
  {
    return diffPair.DifferenceType != FileDifferenceType.UnchangedFile && diffPair.DifferenceType != FileDifferenceType.NewFile;
  }

  /// <summary>
  /// Позволяет найти объекты IPS, чьи локальные файлы имеют несохраненные изменения.
  /// </summary>
  /// <param name="list">Список проверяемых объектов IPS и состояния файлов этих объектов</param>
  /// <param name="applyFileFilter">Флаг, включающий отсеивание файлов, не содержащих изменений</param>
  /// <returns>Список объектов IPS с несохраненными изменениями</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на список объектов IPS не может быть null</exception>
  public List<DBObjectFilesDifferences> FindUnsavedObjects(
    List<DBObjectFilesDifferences> list,
    bool applyFileFilter)
  {
    List<DBObjectFilesDifferences> unsavedObjects = list != null ? new List<DBObjectFilesDifferences>(list.Count) : throw new ArgumentNullException(nameof (list));
    if (list.Count > 0)
    {
      foreach (DBObjectFilesDifferences filesDifferences1 in list)
      {
        if (filesDifferences1.ObjectState.IsEditableState && filesDifferences1.DifferencePairs.Exists(new Predicate<FileDifferencePair>(this.UnsavedFilePredicate)))
        {
          DBObjectFilesDifferences filesDifferences2 = filesDifferences1.Clone();
          if (applyFileFilter)
            filesDifferences2.DifferencePairs.RemoveAll((Predicate<FileDifferencePair>) (diffPair => !this.UnsavedFilePredicate(diffPair)));
          unsavedObjects.Add(filesDifferences2);
        }
      }
    }
    return unsavedObjects;
  }

  private bool UnsavedFilePredicate(FileDifferencePair diffPair)
  {
    return diffPair.DifferenceType == FileDifferenceType.NewFile || diffPair.DifferenceType == FileDifferenceType.UpdatedFile;
  }
}
