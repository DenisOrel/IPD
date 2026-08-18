// Decompiled with JetBrains decompiler
// Type: Intermech.Files.IFileVault
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Files;

/// <summary>
/// Файловый сервис IPS. Он предназначен для публикации файлов объектов в файловом хранилище пользователя.
/// Все методы этого интерфейса являются thread-safe.
/// </summary>
public interface IFileVault : IFileAreas, IEnumerable<IFileArea>, IEnumerable
{
  /// <summary>Возвращает состояние объекта в базе IPS.</summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <param name="throwIfNotFound">Признак, нужно ли сбрасывать исключение при отсутствии объекта</param>
  /// <returns>Состояние объекта в базе или null, если указанного объекта нет в базе IPS</returns>
  /// <exception cref="T:Intermech.KernelException">Указанный объект отсутствует в базе IPS</exception>
  [Obsolete("Use the service IDBObjectsInformationService (IFileVault.DBObjectsInfo) instead of this", true)]
  DBObjectState GetObjectState(long objectId, bool throwIfNotFound);

  /// <summary>
  /// Возвращает список имен файлов в атрибуте 'Файл' указанного объекта. Значения атрибута, равные DBNULL,
  /// игнорируются и в результате выполнения метода не присутствуют.
  /// </summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <returns>Список имен файлов объекта</returns>
  /// <exception cref="T:System.ArgumentException">Не задан идентификатор версии объекта</exception>
  [Obsolete("Use the service IDBFilesInformationService (IFileVault.DBFilesInfo) instead of this", true)]
  List<string> GetObjectFileNames(long objectId);

  /// <summary>
  /// Возвращает состояния файлов в атрибуте 'Файл' указанного объекта. Значения атрибута, равные DBNULL,
  /// игнорируются и в результате выполнения метода не присутствуют.
  /// </summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <returns>Список состояний файлов</returns>
  /// <exception cref="T:System.ArgumentException">Не задан идентификатор версии объекта</exception>
  [Obsolete("Use the service IDBFilesInformationService (IFileVault.DBFilesInfo) instead of this", true)]
  List<FileState> GetObjectFileStates(long objectId);

  /// <summary>Определяет мастер-файл для указанного объекта.</summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <param name="throwIfNotFound">Следует ли сбрасывать исключение при отсутствии мастер-файла у объекта</param>
  /// <returns>Имя файла в относительной форме (так, как оно записано в базе IPS)</returns>
  /// <exception cref="T:System.ArgumentException">Не задан идентификатор версии объекта</exception>
  /// <exception cref="T:Intermech.FaultException">У объекта отсутствует мастер-файл или нет атрибута "Файл"</exception>
  [Obsolete("Use the service IDBFilesInformationService (IFileVault.DBFilesInfo) instead of this", true)]
  string GetObjectMasterFile(long objectId, bool throwIfNotFound);

  /// <summary>
  /// Создает список, содержащий один указанный объект. Этот метод используется в случаях, когда требуется опубликовать
  /// объект без учета его связей с другими объектами.
  /// </summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <returns>Построенный список состояний объектов</returns>
  /// <exception cref="T:Intermech.KernelException">Указанный объект отсутствует в базе IPS</exception>
  [Obsolete("Use the service IDBObjectsInformationService (IFileVault.DBObjectsInfo) instead of this", true)]
  List<DBObjectState> CreateStateListForSingleObject(long objectId);

  /// <summary>
  /// Создает список, содержащий указанный объект и все связанные с ним объекты по всем типам связей, для которых настроено
  /// извлечение файлов.
  /// </summary>
  /// <param name="rootObjectId">Идентификатор версии корневого объекта</param>
  /// <param name="versionsRule">Правило подбора версий объектов</param>
  /// <returns>Построенный список состояний объектов</returns>
  /// <exception cref="T:Intermech.KernelException">Указанный объект отсутствует в базе IPS</exception>
  [Obsolete("Use the service IDBObjectsInformationService (IFileVault.DBObjectsInfo) instead of this", true)]
  List<DBObjectState> CreateStateListForDocumentTree(
    long rootObjectId,
    VersionsRulePackage versionsRule);

  /// <summary>
  /// Удаляет из списка все объекты, которые не были ранее опубликованы в указанной файловой области.
  /// </summary>
  /// <param name="list">Список объектов</param>
  /// <param name="area">Файловая область</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылки на список объектов и файловую область не должны быть null</exception>
  [Obsolete("Use the service IDBObjectsInformationService (IFileVault.DBObjectsInfo) instead of this", true)]
  void RemoveUnpublishedObjects(List<DBObjectState> list, IFileAreaPublishedObjects area);

  /// <summary>
  /// Извлекает из списка все объекты, которые не были ранее опубликованы в указанной файловой области.
  /// </summary>
  /// <param name="list">Список объектов</param>
  /// <param name="area">Файловая область</param>
  /// <returns>Список с извлеченными неопубликованными объектами</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылки на список объектов и файловую область не должны быть null</exception>
  [Obsolete("Use the service IDBObjectsInformationService (IFileVault.DBObjectsInfo) instead of this", true)]
  List<DBObjectState> ExtractUnpublishedObjects(
    List<DBObjectState> list,
    IFileAreaPublishedObjects area);

  /// <summary>
  /// Удаляет из списка все мертвые объекты (отсутствующие в базе IPS). Этот метод используется в случаях, когда список
  /// построен на основе локальных данных о публикации объектов в файловой области.
  /// </summary>
  /// <param name="list">Список объектов</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на список объектов не должна быть null</exception>
  [Obsolete("Use the service IDBObjectsInformationService (IFileVault.DBObjectsInfo) instead of this", true)]
  void RemoveDeadObjects(List<DBObjectState> list);

  /// <summary>
  /// Извлекает из списка все мертвые объекты (отсутствующие в базе IPS). Этот метод используется в случаях, когда список
  /// построен на основе локальных данных о публикации объектов в файловой области.
  /// </summary>
  /// <param name="list">Список объектов</param>
  /// <returns>Список с извлеченными мертвыми объектами</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на список объектов не должна быть null</exception>
  [Obsolete("Use the service IDBObjectsInformationService (IFileVault.DBObjectsInfo) instead of this", true)]
  List<DBObjectState> ExtractDeadObjects(List<DBObjectState> list);

  /// <summary>
  /// Публикует заданный объект и все объекты, связанные с ним, в указанной области файлового хранилища.
  /// </summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <param name="fileName">Имя файла в относительной форме (так, как оно записано в базе IPS)</param>
  /// <param name="versionsRule">Правило подбора версий</param>
  /// <param name="fileArea">Область файлового хранилища, в которой следует опубликовать объект</param>
  /// <returns>Абсолютный путь к файлу объекта после публикации</returns>
  /// <exception cref="T:System.ArgumentException">Не задан идентификатор объекта или имя файла</exception>
  /// <exception cref="T:System.ArgumentNullException">Не задано правило подбора версий или область файлового хранилища</exception>
  string PublishTree(
    long objectId,
    string fileName,
    VersionsRulePackage versionsRule,
    IFileArea fileArea);

  /// <summary>
  /// Публикует заданный объект и все объекты, связанные с ним, в указанной области файлового хранилища.
  /// </summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <param name="throwIfNoMasterFile">Следует ли сбрасывать исключение при отсутствии мастер-файла у объекта</param>
  /// <param name="versionsRule">Правило подбора версий</param>
  /// <param name="fileArea">Область файлового хранилища, в которой следует опубликовать объект</param>
  /// <returns>Абсолютный путь к мастер-файлу объекта после публикации. Может быть null, если у объекта нет мастер-файла или атрибута "Файл"</returns>
  /// <exception cref="T:System.ArgumentException">Не задан идентификатор объекта или имя файла</exception>
  /// <exception cref="T:System.ArgumentNullException">Не задано правило подбора версий или область файлового хранилища</exception>
  /// <exception cref="T:Intermech.FaultException">У объекта отсутствует мастер-файл или нет атрибута "Файл"</exception>
  string PublishTree(
    long objectId,
    bool throwIfNoMasterFile,
    VersionsRulePackage versionsRule,
    IFileArea fileArea);

  /// <summary>
  /// Возвращает сервис для получения информации о состоянии объектов IPS в базе данных.
  /// </summary>
  IDBObjectsInformationService DBObjectsInfo { get; }

  /// <summary>
  /// Возвращает сервис для получения информации о состоянии файлов объектов IPS в базе данных.
  /// </summary>
  IDBFilesInformationService DBFilesInfo { get; }

  /// <summary>
  /// Возвращает менеджер операций с атрибутом read-only для локальных файлов объектов IPS.
  /// </summary>
  IReadOnlyLocalFilesManager ReadOnlyLocalFiles { get; }
}
