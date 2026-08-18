// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.CacheObjectBase`1
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces.Localization;
using System;
using System.Data;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Базовый класс для прокси-классов (типов объектов) клиенской сессии.
/// </summary>
/// <typeparam name="TID">Тип идентификатора метаданного</typeparam>
internal abstract class CacheObjectBase<TID> : MarshalByRefObject, IDBGuid, IDBLocalizable
{
  /// <summary>Идентификатор элемента метаданных.</summary>
  protected readonly TID _id;
  /// <summary>Интерфейс на клиентскую сессию</summary>
  protected readonly ClientSession _clientSession;
  /// <summary>
  /// Таблица с параметрами, которые могут хранится в данных объектах.
  /// </summary>
  protected HybridTable paramsTable = new HybridTable();
  /// <summary>Категория</summary>
  protected int _CategoryType;
  /// <summary>Идентификатор объекта данной категории</summary>
  protected long _CategoryID;
  /// <summary>Имя таблицы, в которой содержаться эти объекты</summary>
  protected string _DBTableName = string.Empty;

  /// <summary>Создает объект.</summary>
  /// <param name="session">Клиентская сессия</param>
  /// <param name="id">Идентификатор элемента метаданных</param>
  protected CacheObjectBase(ClientSession session, TID id)
  {
    this._clientSession = session != null ? session : throw new ArgumentNullException(nameof (session));
    this._id = id;
  }

  /// <summary>Инициализация класса</summary>
  /// <param name="aCategoryType">Категория прав доступа</param>
  /// <param name="aCategoryID">Идентификатор объекта данной категории</param>
  /// <param name="tableName">Имя таблицы, в которой содержаться эти объекты</param>
  /// <param name="throwMessage">Сообщение об ошибке идентификатора объекта</param>
  protected virtual void InitOptions(
    int aCategoryType,
    long aCategoryID,
    string tableName,
    string throwMessage)
  {
    this._CategoryType = aCategoryType;
    this._CategoryID = aCategoryID;
    this._DBTableName = tableName;
    this.paramsTable.Create(this.GetSourceDataRowFromCache());
    if (this.paramsTable.RowsCount != 0)
      return;
    this.ReloadClientCache();
    if (this.paramsTable.RowsCount == 0)
      throw new KernelException($"{throwMessage}: {this._id}");
  }

  /// <summary>
  /// Возвращает строку таблицы из клиентского кэша, описывающая текущий элемент метаданных.
  /// </summary>
  /// <returns>Строка таблицы из клиентского кэша, описывающая текущий элемент метаданных. Может быть null</returns>
  protected virtual DataRow GetSourceDataRowFromCache()
  {
    DataTable table = this._clientSession.ClientCache.GetTable(this._DBTableName);
    return table == null ? (DataRow) null : this.GetSourceDataRowFromCache(table);
  }

  /// <summary>
  /// Возвращает строку таблицы из клиентского кэша, описывающая текущий элемент метаданных.
  /// </summary>
  /// <param name="dataTable">Таблица клиентского кэша, содержащая метаданные этого вида</param>
  /// <returns>Строка таблицы из клиентского кэша, описывающая текущий элемент метаданных. Может быть null</returns>
  protected virtual DataRow GetSourceDataRowFromCache(DataTable dataTable)
  {
    return dataTable.Rows.Find((object) this._id);
  }

  /// <summary>Перечитывает клиентский кэш и paramsTable</summary>
  protected virtual void ReloadClientCache()
  {
    this._clientSession.ClientCache.ReloadCacheCategory(this._CategoryType, this._clientSession.Session);
    this.paramsTable.Create(this.GetSourceDataRowFromCache());
  }

  public abstract object GetServerObject();

  /// <summary>
  /// Если true, то это наш системный GUID, =&gt; удалять такой объект нельзя.
  /// </summary>
  public virtual bool IsSystemGUID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return SystemGUIDs.IsSystemGUID(this.GUID);
    }
  }

  /// <summary>GUID</summary>
  public virtual Guid GUID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return new Guid(this.paramsTable[0]["F_GUID"].ToString());
    }
    [DebuggerStepThrough] set => this._clientSession.Guard.ValidateCall();
  }

  public string Languages
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return (this.GetServerObject() as IDBLocalizable).Languages;
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      (this.GetServerObject() as IDBLocalizable).Languages = value;
    }
  }
}
