// Decompiled with JetBrains decompiler
// Type: Intermech.Files.IDBObjectsInformationService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Files;

/// <summary>
/// Интерфейс сервиса для получения информации о состояниях объектов IPS в базе данных.
/// </summary>
public interface IDBObjectsInformationService
{
  /// <summary>Возвращает состояние объекта в базе IPS.</summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <param name="throwIfNotFound">Признак, нужно ли сбрасывать исключение при отсутствии объекта</param>
  /// <returns>Состояние объекта в базе или null, если указанного объекта нет в базе IPS</returns>
  /// <exception cref="T:Intermech.KernelException">Указанный объект отсутствует в базе IPS</exception>
  DBObjectState GetObjectState(long objectId, bool throwIfNotFound);

  /// <summary>Возвращает состояние объекта в базе IPS.</summary>
  /// <param name="dbObject">Идентификатор версии объекта</param>
  /// <returns>Состояние объекта IPS</returns>
  /// <exception cref="T:ArgumentNullException">dbObject</exception>
  DBObjectState GetObjectState(IDBObject dbObject);

  /// <summary>
  /// Создает список, содержащий один указанный объект. Этот метод используется в случаях, когда требуется опубликовать
  /// объект без учета его связей с другими объектами.
  /// </summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <returns>Построенный список состояний объектов</returns>
  /// <exception cref="T:Intermech.KernelException">Указанный объект отсутствует в базе IPS</exception>
  List<DBObjectState> CreateStateListForSingleObject(long objectId);

  /// <summary>
  /// Создает список, содержащий указанный объект и все связанные с ним объекты по всем типам связей, для которых настроено
  /// извлечение файлов.
  /// </summary>
  /// <param name="rootObjectId">Идентификатор версии корневого объекта</param>
  /// <param name="versionsRule">Правило подбора версий объектов</param>
  /// <returns>Построенный список состояний объектов</returns>
  /// <exception cref="T:Intermech.KernelException">Указанный объект отсутствует в базе IPS</exception>
  List<DBObjectState> CreateStateListForObjectTree(
    long rootObjectId,
    VersionsRulePackage versionsRule);

  /// <summary>
  /// Удаляет из списка все объекты, которые не были ранее опубликованы в указанной файловой области.
  /// </summary>
  /// <param name="list">Список объектов</param>
  /// <param name="area">Файловая область</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылки на список объектов и файловую область не должны быть null</exception>
  void RemoveUnpublishedObjects(List<DBObjectState> list, IFileAreaPublishedObjects area);

  /// <summary>
  /// Извлекает из списка все объекты, которые не были ранее опубликованы в указанной файловой области.
  /// </summary>
  /// <param name="list">Список объектов</param>
  /// <param name="area">Файловая область</param>
  /// <returns>Список с извлеченными неопубликованными объектами</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылки на список объектов и файловую область не должны быть null</exception>
  List<DBObjectState> ExtractUnpublishedObjects(
    List<DBObjectState> list,
    IFileAreaPublishedObjects area);

  /// <summary>
  /// Удаляет из списка все мертвые объекты (отсутствующие в базе IPS). Этот метод используется в случаях, когда список
  /// построен на основе локальных данных о публикации объектов в файловой области.
  /// </summary>
  /// <param name="list">Список объектов</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на список объектов не должна быть null</exception>
  void RemoveDeadObjects(List<DBObjectState> list);

  /// <summary>
  /// Извлекает из списка все мертвые объекты (отсутствующие в базе IPS). Этот метод используется в случаях, когда список
  /// построен на основе локальных данных о публикации объектов в файловой области.
  /// </summary>
  /// <param name="list">Список объектов</param>
  /// <returns>Список с извлеченными мертвыми объектами</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на список объектов не должна быть null</exception>
  List<DBObjectState> ExtractDeadObjects(List<DBObjectState> list);

  /// <summary>
  /// Позволяет найти объекты IPS, чьи локальные файлы устарели/отсутствуют и, соответственно, требуют замены файлами из базы данных.
  /// </summary>
  /// <param name="list">Список проверяемых объектов IPS и состояния файлов этих объектов</param>
  /// <param name="applyFileFilter">Флаг, включающий отсеивание файлов, не требующих обновления</param>
  /// <returns>Список объектов IPS, требующих обновления</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на список объектов IPS не может быть null</exception>
  List<DBObjectFilesDifferences> FindOutdatedObjects(
    List<DBObjectFilesDifferences> list,
    bool applyFileFilter);

  /// <summary>
  /// Позволяет найти объекты IPS, чьи локальные файлы имеют несохраненные изменения.
  /// </summary>
  /// <param name="list">Список проверяемых объектов IPS и состояния файлов этих объектов</param>
  /// <param name="applyFileFilter">Флаг, включающий отсеивание файлов, не содержащих изменений</param>
  /// <returns>Список объектов IPS с несохраненными изменениями</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на список объектов IPS не может быть null</exception>
  List<DBObjectFilesDifferences> FindUnsavedObjects(
    List<DBObjectFilesDifferences> list,
    bool applyFileFilter);
}
