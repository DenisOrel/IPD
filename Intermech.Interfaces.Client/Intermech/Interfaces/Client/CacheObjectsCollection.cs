// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.CacheObjectsCollection
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Базовый класс для прокси-классов (коллекций типов объектов) клиенской сессии.
/// </summary>
internal class CacheObjectsCollection : MarshalByRefObject
{
  /// <summary>Имя таблицы, в которой содержаться эти объекты</summary>
  protected string _DBTableName = string.Empty;
  /// <summary>Имя идентификационного поля объекта данной категории</summary>
  protected string _DBKeyField = string.Empty;
  /// <summary>Если true, то фильтровать по предметным областям</summary>
  protected bool _Filtering;
  /// <summary>Интерфейс клиентской сессии</summary>
  protected ClientSession _clientSession;
  /// <summary>Идентификатор родителя</summary>
  protected object _ParentID;

  public CacheObjectsCollection(ClientSession clientSession, bool filterRecs)
  {
    this._clientSession = clientSession;
    this._Filtering = filterRecs;
  }

  /// <summary>Инициализация класса</summary>
  /// <param name="tableName">Имя таблицы, в которой содержаться эти объекты</param>
  /// <param name="keyField">Имя идентификационного поля объекта данной категории</param>
  protected virtual void InitOptions(string tableName, string keyField)
  {
    this._DBTableName = tableName;
    this._DBKeyField = keyField;
  }

  /// <summary>Идентификатор родителя</summary>
  public virtual object ParentID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._ParentID;
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      this._ParentID = value;
    }
  }

  /// <summary>
  /// Возвращает SQL-условие, отсеивающее только объекты, входящие в состав parentID
  /// </summary>
  protected virtual string GetParentSQL() => string.Empty;

  /// <summary>
  /// Возвращает таблицу с объектами входящими в состав parentID и отсортированными по orderBy
  /// </summary>
  public virtual DataTable Select(string orderBy, params object[] addInfo)
  {
    this._clientSession.Guard.ValidateCall();
    DataTable table = this._clientSession.ClientCache.GetTable(this._DBTableName);
    DataTable dataTable = table.Clone();
    this.FillCaptions(dataTable);
    string parentSql = this.GetParentSQL();
    if (string.IsNullOrEmpty(parentSql) && string.IsNullOrEmpty(orderBy))
    {
      DataSetProcessor.AddTable(dataTable, table, true);
    }
    else
    {
      DataRow[] fromRows = table.Select(parentSql, orderBy);
      DataSetProcessor.AssignRows(dataTable, (IEnumerable<DataRow>) fromRows, true, true);
    }
    if (this._Filtering)
      dataTable = this._clientSession.ClientCache.GetFilteredTable(this._DBTableName, this._DBKeyField, dataTable);
    return dataTable;
  }

  protected virtual int GetCount()
  {
    return this._clientSession.ClientCache.GetTable(this._DBTableName).Select(this.GetParentSQL()).Length;
  }

  /// <summary>
  /// Порожденные классы заполняют заголовки таблицы, возвращаемой селектом
  /// </summary>
  protected virtual void FillCaptions(DataTable datatable)
  {
    foreach (DataColumn column in (InternalDataCollectionBase) datatable.Columns)
      column.Caption = this.GetCaption(column.ColumnName);
  }

  private string GetCaption(string id)
  {
    return (DataSetProcessor.ColumnCaptions[(object) id] ?? (object) id).ToString();
  }

  protected void ReloadCache(int category)
  {
    if (category != 0)
      this._clientSession.ClientCache.ClearVisibleList(3);
    this._clientSession.ClientCache.ReloadCache(this._clientSession.Session);
  }

  /// <summary>
  /// Допускает ли данный объект условные проверки прав доступа
  /// </summary>
  public bool EnabledConditionAccess => false;
}
