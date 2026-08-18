// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.ClientCacheService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Кэш клиента.</summary>
internal sealed class ClientCacheService : IClientCache
{
  /// <summary>DataSet с таблицами БД.</summary>
  private DataSet _cacheDataSet;
  /// <summary>Файл с кэшем</summary>
  private CacheFile _cacheFile;
  /// <summary>Список кэшированных таблиц</summary>
  private string[] _tablesNameList;
  /// <summary>Список для фильтрации по типам атрибутов</summary>
  private int[] _visibleAttr;
  /// <summary>Список для фильтрации по типам связей</summary>
  private int[] _visibleRel;
  /// <summary>Список для фильтрации по типам объектов</summary>
  private int[] _visibleObj;
  /// <summary>Список для фильтрации</summary>
  private int[] _visibleAttrGroups;
  /// <summary>Список для фильтрации</summary>
  private int[] _visibleLCLevel;
  /// <summary>Флаг изменения в кэше</summary>
  private bool _cacheReloaded;
  /// <summary>Соответствие категорий таблицам</summary>
  private Hashtable _categories = new Hashtable();
  /// <summary>Блокирует обновление клиентского кэша</summary>
  private bool _lockReload;
  /// <summary>Фоновая задача зачитки кэша из файла</summary>
  private Task _loadCacheFromDiskTask;

  public ClientCacheService()
  {
    this._categories[(object) "IMS_ATTRIBUTES"] = (object) 3;
    this._categories[(object) "IMS_ATTR_GROUPS"] = (object) 12;
    this._categories[(object) "IMS_OBJECT_TYPES"] = (object) 4;
    this._categories[(object) "IMS_RELATION_TYPES"] = (object) 6;
    this._categories[(object) "IMS_LEVELS"] = (object) 8;
    this._loadCacheFromDiskTask = Task.Run(new Action(this.LoadCacheFromDisk));
  }

  /// <summary>DataSet с таблицами БД</summary>
  public DataSet CacheDataSet => this._cacheDataSet;

  /// <summary>Очистить списки видимых объектов</summary>
  /// <param name="Categories">Категории обновляемых объектов, если -1, то очистятся все</param>
  public void ClearVisibleList(params int[] Categories)
  {
    foreach (int category in Categories)
    {
      switch (category)
      {
        case -1:
          this._visibleAttr = (int[]) null;
          this._visibleRel = (int[]) null;
          this._visibleObj = (int[]) null;
          this._visibleAttrGroups = (int[]) null;
          this._visibleLCLevel = (int[]) null;
          return;
        case 3:
          this._visibleAttr = (int[]) null;
          break;
        case 4:
          this._visibleObj = (int[]) null;
          break;
        case 6:
          this._visibleRel = (int[]) null;
          break;
        case 8:
          this._visibleLCLevel = (int[]) null;
          break;
        case 12:
          this._visibleAttrGroups = (int[]) null;
          break;
      }
    }
  }

  /// <summary>Загружает с сервера списки для фильтрации</summary>
  /// <param name="Session">Интерфейс на клиентскую сессию</param>
  /// <param name="Categories">Категория обновляемых объектов, если -1, то обновятся все</param>
  private void GetVisibleValues(params int[] Categories)
  {
    if (Categories.Length == 0)
      return;
    ArrayList arrayList1 = new ArrayList();
    if (Categories[0] == -1)
      arrayList1.AddRange(this._categories.Values);
    else
      arrayList1.AddRange((ICollection) Categories);
    arrayList1.Sort();
    ArrayList arrayList2 = new ArrayList();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (int num in arrayList1)
      {
        if (arrayList2.BinarySearch((object) num) < 0)
        {
          switch (num)
          {
            case 3:
              this._visibleAttr = sessionKeeper.Session.GetAttributeTypeCollection(-1, true).GetVisibleList();
              break;
            case 4:
              this._visibleObj = sessionKeeper.Session.GetObjectTypeCollection(-2, true).GetVisibleList();
              break;
            case 6:
              this._visibleRel = sessionKeeper.Session.GetRelationTypeCollection(true).GetVisibleList();
              break;
            case 8:
              this._visibleLCLevel = sessionKeeper.Session.GetLifecycleLevelCollection(true).GetVisibleList();
              break;
            case 12:
              this._visibleAttrGroups = sessionKeeper.Session.GetAttributesGroupCollection(true).GetVisibleList();
              break;
          }
          arrayList2.Add((object) num);
        }
      }
    }
  }

  private int[] FormingCategories(string[] tableNames)
  {
    ArrayList arrayList = new ArrayList();
    foreach (string tableName in tableNames)
    {
      if (this._categories.ContainsKey((object) tableName))
        arrayList.Add(this._categories[(object) tableName]);
    }
    return (int[]) arrayList.ToArray(typeof (int));
  }

  /// <summary>Выполняет очистку кэша.</summary>
  public void ClearCache()
  {
    lock (this)
    {
      this._tablesNameList = (string[]) null;
      this._visibleAttr = (int[]) null;
      this._visibleAttrGroups = (int[]) null;
      this._visibleLCLevel = (int[]) null;
      this._visibleObj = (int[]) null;
      this._visibleRel = (int[]) null;
      this._cacheReloaded = false;
      EventHandler cleared = this.Cleared;
      if (cleared == null)
        return;
      cleared((object) this, EventArgs.Empty);
    }
  }

  /// <summary>
  /// Первоначальная загрузка данных в кэш с диска отдельном потоке
  /// </summary>
  private void LoadCacheFromDisk()
  {
    this._cacheFile = new CacheFile();
    this._cacheDataSet = this._cacheFile.LoadData();
  }

  /// <summary>
  /// Первоначальная загрузка данных в кэш (вторая фаза, изредка требующая сессию)
  /// </summary>
  public void LoadCache(IUserSession Session)
  {
    this._loadCacheFromDiskTask.Wait();
    this._tablesNameList = Session.ServerCache.GetTableNames();
    if (this.CheckCache())
      this.ReloadCacheInternal(Session);
    else
      this.RecreateCacheInternal(Session);
    MetaDataHelper.SyncMetadata(this._cacheDataSet, true);
    EventHandler<ClientCacheReloadedEventArgs> reloaded = this.Reloaded;
    if (reloaded == null)
      return;
    reloaded((object) this, new ClientCacheReloadedEventArgs(Session));
  }

  /// <summary>Сохранение кэша в файл</summary>
  public void SaveCache()
  {
    if (!this.CheckCache() || !this._cacheReloaded)
      return;
    this._cacheFile.SaveData(this._cacheDataSet);
  }

  /// <summary>Синхронизация кэша с серверным кэшем.</summary>
  public void ReloadCache(IUserSession Session)
  {
    if (this._lockReload)
      return;
    lock (this)
    {
      if (!this.ReloadCacheInternal(Session))
        return;
      MetaDataHelper.SyncMetadata(this._cacheDataSet, true);
      EventHandler<ClientCacheReloadedEventArgs> reloaded = this.Reloaded;
      if (reloaded == null)
        return;
      reloaded((object) this, new ClientCacheReloadedEventArgs(Session));
    }
  }

  private bool ReloadCacheInternal(IUserSession Session)
  {
    Hashtable serverTable = this.SetLastUpdate(Session.ServerCache.GetTablesModifyTime());
    Hashtable hashtable = this.SetLastUpdate(this._cacheDataSet.Tables["IMS_METADATA"]);
    ArrayList arrayList = new ArrayList();
    foreach (string tablesName in this._tablesNameList)
    {
      if (tablesName != "IMS_METADATA" && (DateTime) hashtable[(object) tablesName] != (DateTime) serverTable[(object) tablesName])
        arrayList.Add((object) tablesName);
    }
    if (arrayList.Count <= 0)
      return false;
    this.LoadTablesFromServer(Session, (string[]) arrayList.ToArray(typeof (string)));
    this.SetCacheTableMetadata(serverTable);
    return true;
  }

  private void RecreateCacheInternal(IUserSession Session)
  {
    Hashtable serverTable = this.SetLastUpdate(Session.ServerCache.GetTablesModifyTime());
    this.LoadTablesFromServer(Session, this._tablesNameList);
    this.SetCacheTableMetadata(serverTable);
  }

  public int[] GetVisibleList(int category)
  {
    switch (category)
    {
      case 3:
        if (this._visibleAttr == null)
          this.GetVisibleValues(3);
        return this._visibleAttr;
      case 4:
        if (this._visibleObj == null)
          this.GetVisibleValues(4);
        return this._visibleObj;
      case 6:
        if (this._visibleRel == null)
          this.GetVisibleValues(6);
        return this._visibleRel;
      case 8:
        if (this._visibleLCLevel == null)
          this.GetVisibleValues(8);
        return this._visibleLCLevel;
      case 12:
        if (this._visibleAttrGroups == null)
          this.GetVisibleValues(12);
        return this._visibleAttrGroups;
      default:
        return (int[]) null;
    }
  }

  /// <summary>Загружает таблицы с серверного кэша</summary>
  private void LoadTablesFromServer(IUserSession Session, params string[] tableNames)
  {
    foreach (DataTable table in Session.ServerCache.GetTables(tableNames))
    {
      if (this._cacheDataSet.Tables[table.TableName] != null)
        this._cacheDataSet.Tables.Remove(table.TableName);
      this._cacheDataSet.Tables.Add(table);
      switch (table.TableName)
      {
        case "IMS_ATTRIBUTES":
          this._visibleAttr = Session.GetAttributeTypeCollection(-1, true).GetVisibleList();
          break;
        case "IMS_ATTR_GROUPS":
          this._visibleAttrGroups = Session.GetAttributesGroupCollection(true).GetVisibleList();
          break;
        case "IMS_OBJECT_TYPES":
          this._visibleObj = Session.GetObjectTypeCollection(-2, true).GetVisibleList();
          break;
        case "IMS_RELATION_TYPES":
          this._visibleRel = Session.GetRelationTypeCollection(true).GetVisibleList();
          break;
        case "IMS_LEVELS":
          this._visibleLCLevel = Session.GetLifecycleLevelCollection(true).GetVisibleList();
          break;
        case "IMS_TYPES_APPLICABILITY":
          CDBRelationsApplicabilityCache.Reset();
          break;
      }
    }
    if (this._cacheReloaded || tableNames.Length == 0)
      return;
    this._cacheReloaded = true;
  }

  /// <summary>Установка значений в таблице кэша IMS_METADATA</summary>
  /// <param name="serverTable"></param>
  private void SetCacheTableMetadata(Hashtable serverTable)
  {
    this._cacheDataSet.Tables["IMS_METADATA"].Rows.Clear();
    IDictionaryEnumerator enumerator = serverTable.GetEnumerator();
    while (enumerator.MoveNext())
    {
      DataRow fromRow = this._cacheDataSet.Tables["IMS_METADATA"].NewRow();
      fromRow["F_TABLE_NAME"] = enumerator.Key;
      fromRow["F_MODIFY_DATE"] = enumerator.Value;
      DataSetProcessor.AddRow(this._cacheDataSet.Tables["IMS_METADATA"], fromRow, false);
    }
    this._cacheDataSet.Tables["IMS_METADATA"].AcceptChanges();
  }

  /// <summary>
  /// Заполняет коллекцию датами последнего обновления таблиц
  /// </summary>
  private Hashtable SetLastUpdate(DataTable table)
  {
    Hashtable hashtable = new Hashtable();
    foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
      hashtable.Add(row["F_TABLE_NAME"], row["F_MODIFY_DATE"]);
    return hashtable;
  }

  /// <summary>Проверяет кэш</summary>
  /// <returns></returns>
  private bool CheckCache()
  {
    return this._cacheDataSet != null && this._cacheDataSet.Tables.Count == this._tablesNameList.Length;
  }

  /// <summary>Возвращает таблицу кэша</summary>
  /// <param name="tableName">Имя таблицы</param>
  /// <returns></returns>
  public DataTable GetTable(string tableName) => this._cacheDataSet.Tables[tableName];

  /// <summary>Возвращает отфильтрованную таблицу кэша</summary>
  /// <param name="tableName">Имя таблицы</param>
  /// <param name="fieldName">Имя идентификационного поля объекта данной категории</param>
  public DataTable GetFilteredTable(string tableName, string fieldName)
  {
    return this.GetFilteredTable(tableName, fieldName, (DataTable) null);
  }

  /// <summary>Возвращает отфильтрованную таблицу кэша</summary>
  /// <param name="tableName">Имя таблицы</param>
  /// <param name="fieldName">Имя идентификационного поля объекта данной категории</param>
  /// <param name="tbl">Таблица фильтруемых данных. Если null, то берется из кэша по tableName</param>
  /// <returns></returns>
  public DataTable GetFilteredTable(string tableName, string fieldName, DataTable tbl)
  {
    int[] keys;
    switch (fieldName)
    {
      case "F_ATTRIBUTE_ID":
        if (this._visibleAttr == null)
          this.GetVisibleValues(3);
        keys = this._visibleAttr;
        break;
      case "F_OBJECT_TYPE":
        if (this._visibleObj == null)
          this.GetVisibleValues(4);
        keys = this._visibleObj;
        break;
      case "F_RELATION_TYPE":
        if (this._visibleRel == null)
          this.GetVisibleValues(6);
        keys = this._visibleRel;
        break;
      case "F_GROUP_ID":
        if (this._visibleAttrGroups == null)
          this.GetVisibleValues(12);
        keys = this._visibleAttrGroups;
        break;
      case "F_LEVEL_ID":
        if (this._visibleLCLevel == null)
          this.GetVisibleValues(8);
        keys = this._visibleLCLevel;
        break;
      default:
        return (DataTable) null;
    }
    if (tbl == null)
      tbl = this.GetTable(tableName);
    return this.FilteringTable(tbl, fieldName, keys);
  }

  /// <summary>Возвращает отфильтрованную таблицу кэша</summary>
  /// <param name="tableName">Имя таблицы</param>
  /// <param name="columnName">Имя идентификационного поля объекта данной категории</param>
  /// <param name="keys">Список для фильтрации</param>
  /// <returns></returns>
  private DataTable FilteringTable(DataTable tbl, string columnName, int[] keys)
  {
    List<int> intList = new List<int>((IEnumerable<int>) keys);
    intList.Sort();
    keys = intList.ToArray();
    DataTable toTable = tbl.Clone();
    int columnIndex = toTable.Columns.IndexOf(columnName);
    if (keys.Length != 0)
    {
      int key1 = keys[0];
      int key2 = keys[keys.Length - 1];
      List<DataRow> dataRowList = new List<DataRow>(tbl.Rows.Count);
      for (int index = 0; index < tbl.Rows.Count; ++index)
      {
        DataRow row = tbl.Rows[index];
        int int32 = Convert.ToInt32(row[columnIndex]);
        if (int32 >= key1 && int32 <= key2 && Array.BinarySearch<int>(keys, int32) >= 0)
          dataRowList.Add(row);
      }
      if (dataRowList.Count > 0)
      {
        if (toTable.MinimumCapacity - toTable.Rows.Count < dataRowList.Count)
          toTable.MinimumCapacity = toTable.Rows.Count + dataRowList.Count;
        toTable.BeginLoadData();
        try
        {
          foreach (DataRow fromRow in dataRowList)
            DataSetProcessor.AddRow(toTable, fromRow, false);
        }
        finally
        {
          toTable.EndLoadData();
        }
      }
    }
    return toTable;
  }

  public void ReloadCacheCategory(int CategoryID, IUserSession Session)
  {
    this.ReloadCache(Session);
  }

  public bool LockReload
  {
    set => this._lockReload = value;
  }

  /// <summary>Событие очистки клиентского кэша.</summary>
  public event EventHandler Cleared;

  /// <summary>
  /// Событие перезагрузки клиентского кэша.
  /// Новые данные были загружены в кэш с сервера приложений.
  /// </summary>
  public event EventHandler<ClientCacheReloadedEventArgs> Reloaded;
}
