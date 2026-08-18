// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IClientCache
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Data;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Интерфейс на клиентский кэш.</summary>
public interface IClientCache
{
  /// <summary>DataSet с таблицами БД</summary>
  DataSet CacheDataSet { get; }

  /// <summary>Выполняет очистку кэша.</summary>
  void ClearCache();

  /// <summary>Первоначальная загрузка данных в кэш</summary>
  void LoadCache(IUserSession Session);

  /// <summary>Синхронизация кэша с серверным кэшем.</summary>
  void ReloadCache(IUserSession Session);

  /// <summary>
  /// Обновление таблиц клиентского кэша в зависимости от передаваемой категории объектов.
  /// </summary>
  /// <param name="CategoryID">Категории объектов.</param>
  /// <param name="Session">Пользовательская сессия</param>
  void ReloadCacheCategory(int CategoryID, IUserSession Session);

  /// <summary>Сохранение кэша в файл</summary>
  void SaveCache();

  /// <summary>Возвращает массив видимых идентификаторов</summary>
  /// <param name="category">Категория объектов</param>
  /// <returns></returns>
  int[] GetVisibleList(int category);

  /// <summary>Возвращает таблицу кэша</summary>
  DataTable GetTable(string tableName);

  /// <summary>Возвращает отфильтрованную таблицу кэша</summary>
  /// <param name="tableName">Имя таблицы</param>
  /// <param name="fieldName">Имя идентификационного поля объекта данной категории</param>
  DataTable GetFilteredTable(string tableName, string fieldName);

  /// <summary>Возвращает отфильтрованную таблицу кэша</summary>
  /// <param name="tableName">Имя таблицы</param>
  /// <param name="fieldName">Имя идентификационного поля объекта данной категории</param>
  /// <param name="tbl">Таблица фильтруемых данных. Если null, то берется из кэша по tableName</param>
  /// <returns></returns>
  DataTable GetFilteredTable(string tableName, string fieldName, DataTable tbl);

  /// <summary>Очистить списки видимых объектов</summary>
  /// <param name="Categories">Категории обновляемых объектов, если -1, то очистятся все</param>
  void ClearVisibleList(params int[] Categories);

  /// <summary>Блокирует обновление клиентского кэша</summary>
  bool LockReload { set; }

  /// <summary>Событие очистки клиентского кэша.</summary>
  event EventHandler Cleared;

  /// <summary>
  /// Событие перезагрузки клиентского кэша.
  /// Новые данные были загружены в кэш с сервера приложений.
  /// </summary>
  event EventHandler<ClientCacheReloadedEventArgs> Reloaded;
}
