// Decompiled with JetBrains decompiler
// Type: Intermech.Files.IWorkArea
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Files;

/// <summary>
/// Рабочая область в файловом хранилище пользователя. Все методы интерфейса являются thread-safe.
/// </summary>
public interface IWorkArea : IFileArea, IFileAreaPublishedObjects
{
  /// <summary>
  /// Публикует/обновляет список объектов в рабочей области файлового хранилища.
  /// </summary>
  /// <param name="objectList">Список версий публикуемых объектов</param>
  /// <param name="replaceFilePolicy">Политика перезаписи существующих в рабочей области файлов</param>
  /// <returns>Статистика по файловым операциям в рабочей области</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на список версий объектов и политику перезаписи файлов не может быть null</exception>
  WorkAreaUpdateStats Publish(IList<DBObjectState> objectList, IReplaceFilePolicy replaceFilePolicy);

  /// <summary>
  /// Позволяет найти опубликованные объекты, требующие обновления.
  /// </summary>
  /// <param name="list">Список проверяемых объектов и состояния файлов этих объектов</param>
  /// <param name="applyFileFilter">Флаг, включающий отсеивание файлов, не требующих обновления</param>
  /// <returns>Список объектов, требующих обновления</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на список объектов не может быть null</exception>
  [Obsolete("Use the service IDBObjectsInformationService (IFileVault.DBObjectsInfo) instead of this", true)]
  List<DBObjectFilesDifferences> FindOutdatedObjects(
    List<DBObjectFilesDifferences> list,
    bool applyFileFilter);

  /// <summary>
  /// Включает в рабочую область объект, который был импортирован в IPS.
  /// </summary>
  /// <param name="objectId">Идентификатор версиb импортированного объекта</param>
  /// <exception cref="T:System.ArgumentException">Идентификатор версии объекта не задан</exception>
  void Attach(long objectId);

  /// <summary>
  /// Включает в рабочую область объекты, которые были импортированы в IPS.
  /// </summary>
  /// <param name="objectList">Список идентификаторов версий импортированных объектов</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на список версий объектов не может быть null</exception>
  void Attach(IList<long> objectList);

  /// <summary>
  /// Отменяет публикацию объекта в рабочей области и удаляет его файлы с диска.
  /// </summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <exception cref="T:System.ArgumentException">Идентификатор версии объекта не задан</exception>
  void Unpublish(long objectId);

  /// <summary>
  /// Отменяет для указанных объектов публикацию в рабочей области файлового хранилища и удаляет их файлы с диска.
  /// </summary>
  /// <param name="objectList">Список идентификаторов версий объектов</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на список идентификаторов не может быть null</exception>
  void Unpublish(IList<long> objectList);

  /// <summary>
  /// Позволяет найти опубликованные объекты, имеющие несохраненные изменения.
  /// </summary>
  /// <param name="list">Список проверяемых объектов и состояния файлов этих объектов</param>
  /// <param name="applyFileFilter">Флаг, включающий отсеивание файлов, не содержащих изменений</param>
  /// <returns>Список объектов с несохраненными изменениями</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на список объектов не может быть null</exception>
  [Obsolete("Use the service IDBObjectsInformationService (IFileVault.DBObjectsInfo) instead of this", true)]
  List<DBObjectFilesDifferences> FindUnsavedObjects(
    List<DBObjectFilesDifferences> list,
    bool applyFileFilter);

  /// <summary>
  /// Выполняет быстрое сохранение в базу IPS указанного объекта. Если указанный объект не мог быть
  /// изменен или отсутствует в базе IPS, то метод ничего не делает, исключение при этом не сбрасывается.
  /// </summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <returns>true, если быстрое сохранение в базу IPS действительно выполнялось, иначе - false</returns>
  /// <exception cref="T:System.ArgumentException">Идентификатор версии объекта не задан</exception>
  bool Save(long objectId);

  /// <summary>
  /// Выполняет быстрое сохранение в базу IPS указанных объектов. Список объектов должен быть получен с помощью метода FindUnsavedObjects.
  /// </summary>
  /// <param name="objectList">Список сохраняемых объектов</param>
  /// <returns>Возвращает количество объектов, для которых было выполнено сохранение файлов в базу IPS</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на список объектов не может быть null</exception>
  int Save(List<DBObjectFilesDifferences> objectList);

  /// <summary>
  /// Возвращает список объектов, опубликованных в рабочей области и не использовавшихся с указанной даты.
  /// </summary>
  /// <param name="noUseSinceDate">Дата в UTC</param>
  /// <returns>Список опубликованных версий объектов</returns>
  List<DBObjectState> GetPublishedObjects(DateTime noUseSinceDate);

  /// <summary>
  /// Позволяет определить происхождение файла в рабочей области.
  /// </summary>
  /// <param name="fileName">Путь и имя файла</param>
  /// <param name="isRelativeName">Признак, что путь к файлу задан в относительной форме</param>
  /// <returns>Найденные сведения о происхождении файла</returns>
  /// <exception cref="T:System.ArgumentException">Не задан путь к файлу</exception>
  /// <exception cref="T:System.InvalidOperationException">Путь к файлу указан не в абсолютной форме</exception>
  FileOrigin GetFileOrigin(string fileName, bool isRelativeName);

  /// <summary>
  /// Позволяет определить происхождение указанных файлов в рабочей области.
  /// </summary>
  /// <param name="fileNames">Коллекция путей и имен файлов</param>
  /// <param name="isRelativeNames">Признак, что пути к файлам заданы в относительной форме</param>
  /// <returns>Найденные сведения о происхождении файлов</returns>
  /// <exception cref="T:System.ArgumentException">Не задан путь к файлу</exception>
  /// <exception cref="T:System.InvalidOperationException">Путь к файлу указан не в абсолютной форме</exception>
  List<FileOrigin> GetFileOrigins(IList<string> fileNames, bool isRelativeNames);

  /// <summary>
  /// Создает объект для определения изменений в локальных файлах объектов IPS.
  /// </summary>
  /// <param name="objectCapacity">Начальная емкость коллекции объектов IPS</param>
  /// <returns>Специализированный объект для пакетного определения изменений в локальных файлах объектов IPS</returns>
  /// <exception cref="T:System.ArgumentOutOfRangeException">objectCapacity</exception>
  DBObjectFilesDifferenceCalculator CreateObjectFilesDifferenceCalculator(int objectCapacity = 16 /*0x10*/);

  /// <summary>Возвращает трекер состояний файлов в рабочей области.</summary>
  FileTracker FileTracker { get; }
}
