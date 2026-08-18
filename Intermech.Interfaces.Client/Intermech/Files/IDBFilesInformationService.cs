// Decompiled with JetBrains decompiler
// Type: Intermech.Files.IDBFilesInformationService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Files;

/// <summary>
/// Интерфейс сервиса для получения информации о состояниях файлов объектов IPS в базе данных.
/// </summary>
public interface IDBFilesInformationService
{
  /// <summary>Определяет мастер-файл для указанного объекта.</summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <param name="throwIfNotFound">Следует ли сбрасывать исключение при отсутствии мастер-файла у объекта</param>
  /// <returns>Имя файла в относительной форме (так, как оно записано в базе IPS)</returns>
  /// <exception cref="T:System.ArgumentException">objectId - Не задан идентификатор версии объекта IPS</exception>
  /// <exception cref="T:Intermech.FaultException">У объекта отсутствует мастер-файл или нет атрибута "Файл"</exception>
  string GetMasterFileName(long objectId, bool throwIfNotFound);

  /// <summary>
  /// Возвращает список имен файлов в атрибуте 'Файл' указанного объекта. Значения атрибута, равные DBNULL,
  /// игнорируются и в результате выполнения метода не присутствуют.
  /// </summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <returns>Список имен файлов объекта</returns>
  /// <exception cref="T:System.ArgumentException">objectId - Не задан идентификатор версии объекта IPS</exception>
  List<string> GetFileNames(long objectId);

  /// <summary>
  /// Возвращает идентификаторы блобов файлов в атрибуте 'Файл' указанного объекта. Значения атрибута, равные DBNULL,
  /// игнорируются и в результате выполнения метода не присутствуют.
  /// </summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <returns>Список пар из имен и идентификаторов блобов файлов</returns>
  /// <exception cref="T:System.ArgumentException">objectId - Не задан идентификатор версии объекта IPS</exception>
  List<Tuple<string, long>> GetFileBlobIds(long objectId);

  /// <summary>
  /// Возвращает описания типов файлов в атрибуте 'Файл' указанного объекта. Значения атрибута, равные DBNULL,
  /// игнорируются и в результате выполнения метода не присутствуют.
  /// </summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <returns>Список типов файлов</returns>
  /// <exception cref="T:System.ArgumentException">objectId - Не задан идентификатор версии объекта IPS</exception>
  List<Tuple<string, FileTypes>> GetFileTypes(long objectId);

  /// <summary>
  /// Возвращает состояния файлов в атрибуте 'Файл' указанного объекта. Значения атрибута, равные DBNULL,
  /// игнорируются и в результате выполнения метода не присутствуют.
  /// </summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <returns>Список состояний файлов</returns>
  /// <exception cref="T:System.ArgumentException">objectId - Не задан идентификатор версии объекта IPS</exception>
  List<FileState> GetFileStates(long objectId);

  /// <summary>
  /// Возвращает состояния файлов в атрибуте 'Файл' для указанных объектов. Значения атрибута, равные DBNULL,
  /// игнорируются и в результате выполнения метода не присутствуют.
  /// </summary>
  /// <param name="objects">Список описателей версий объектов</param>
  /// <returns>Список описателей версий объектов и состояний их файлов</returns>
  /// <exception cref="T:ArgumentNullException">objects</exception>
  List<DBObjectStateWithFiles> GetFileStates(IList<DBObjectState> objects);
}
