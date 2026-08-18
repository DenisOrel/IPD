// Decompiled with JetBrains decompiler
// Type: Intermech.Redline.IClientRedliningService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Redline;

/// <summary>
/// Сервис, позволяющий работать с файлами "Red Line" для объектов IPS
/// </summary>
public interface IClientRedliningService
{
  /// <summary>
  /// Выполнить синхронизацию файлов "Red Line" с базой данных IPS
  /// </summary>
  void Sync();

  /// <summary>
  /// Синхронизировать указанных файлов "Red Line" с базой данных IPS
  /// </summary>
  /// <param name="items">Список идентификаторов версий объектов и их файлов,
  /// для которых требуется выполнить синхронизацию</param>
  void Sync(List<Tuple<long, string>> items);

  /// <summary>Полностью очистить список синхронизации</summary>
  void Clear();

  /// <summary>
  /// Добавить в службу объект, файлы "Red Line" которого требуется синхронизировать с базой IPS
  /// </summary>
  /// <param name="objectID">Идентификатор версии объекта</param>
  /// <param name="path">Папка, в которую выгружены файлы данного объекта</param>
  void AddObject(long objectID, string path);

  /// <summary>
  /// Добавить в службу объект, файлы "Red Line" которого требуется синхронизировать с базой IPS
  /// </summary>
  /// <param name="objectID">Идентификатор версии объекта</param>
  /// <param name="paths">Папки, в которые выгружены файлы указанного объекта</param>
  void AddObject(long objectID, IList<string> paths);

  /// <summary>
  /// Добавить в службу объекты, файлы "Red Line" которых требуется синхронизировать с базой IPS
  /// </summary>
  /// <param name="objectIDs">Идентификаторы версий объектов</param>
  /// <param name="paths">Папки, в которые выгружены файлы данных объектов</param>
  void AddObjects(IList<long> objectIDs, IList<string> paths);

  /// <summary>
  /// Удалить указанную версию объекта из списка синхронизации
  /// </summary>
  /// <param name="objectID">Идентификатор версии объекта</param>
  /// <param name="path">Папка, в которую выгружен указанный объект</param>
  void Remove(long objectID, string path);

  /// <summary>
  /// Удалить указанные версии объектов из списка синхронизации
  /// </summary>
  /// <param name="objectIDs">Идентификаторы версий объектов</param>
  /// <param name="paths">Папки, в которые выгружены указанные объекты</param>
  void Remove(IList<long> objectIDs, IList<string> paths);

  /// <summary>
  /// Удалить указанную версию объекта из списка синхронизации (из всех найденных папок)
  /// </summary>
  /// <param name="objectID">Идентификатор версии объекта</param>
  void Remove(long objectID);

  /// <summary>
  /// Удалить указанные версии объектов из списка синхронизации (из всех найденных папок)
  /// </summary>
  /// <param name="objectIDs">Идентификаторы версий объектов</param>
  void Remove(IList<long> objectIDs);

  /// <summary>
  /// Удалить из списка синхронизации все версии объектов, выгруженные в указанную папку
  /// </summary>
  /// <param name="path">Папка, в которую выгружена одна или несколько версий объектов</param>
  void Remove(string path);

  /// <summary>
  /// Удалить из списка синхронизации все версии объектов, выгруженные в указанные папки
  /// </summary>
  /// <param name="paths">Папки, в которые выгружена одна или несколько версий объектов</param>
  void Remove(IList<string> paths);
}
