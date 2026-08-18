// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.ImbaseRestructuringTablesService
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;

#nullable disable
namespace Intermech.Imbase.Server;

internal class ImbaseRestructuringTablesService : LongLifeObject, IImbaseRestructuringTablesService
{
  private static IUserSession _systemSession;
  private IAsyncResult _asyncResult;
  private object _lockObject = new object();
  private int _countItems;
  private int _currItemNumbetr;
  private int _completed;
  private bool _terminated;
  private bool _stopped;
  private bool _paused;
  private List<RestructuringTablesAttrSettings> _settings;
  private List<ImbaseRestructuringTablesService.TableRefInfo> _tableRefInfoList;
  private Dictionary<long, List<int>> _addedAttrs = new Dictionary<long, List<int>>();

  private bool IsProcessStoped
  {
    get
    {
      while (this._paused)
      {
        if (this._stopped)
          return true;
        Thread.Sleep(1000);
      }
      return this._stopped;
    }
  }

  private long SourceObjID { get; set; }

  private long UserID { get; set; }

  public List<RestructuringTablesExteption> ExceptionInfo { get; private set; }

  public int State
  {
    get
    {
      if (this._terminated)
        return -2;
      if (this._asyncResult == null)
        return 0;
      return !this._paused ? this._completed + 1 : -1;
    }
  }

  public int Value
  {
    get
    {
      return this._countItems != 0 ? (this._completed = this._terminated ? 100 : (int) ((double) this._currItemNumbetr / (double) this._countItems * 100.0)) : 100;
    }
  }

  public void Pause()
  {
    lock (this._lockObject)
      this._paused = true;
  }

  public void Start(long userID, long sourceObjID, List<RestructuringTablesAttrSettings> settings)
  {
    lock (this._lockObject)
    {
      if (this._asyncResult == null)
      {
        this._terminated = this._stopped = this._paused = false;
        this._completed = 0;
        this._countItems = this._currItemNumbetr = 0;
        this.UserID = userID;
        this.SourceObjID = sourceObjID;
        this._settings = settings;
        if (this._tableRefInfoList != null)
          this._tableRefInfoList.Clear();
        this._addedAttrs.Clear();
        this._asyncResult = new ImbaseRestructuringTablesService.RestructuringTablesHandler(this.ScanProcess).BeginInvoke(new AsyncCallback(this.OnTaskTerminated), (object) null);
      }
      else
        this._paused = false;
    }
  }

  public void Stop()
  {
    if (this._asyncResult == null)
      return;
    lock (this._lockObject)
      this._stopped = true;
    this._asyncResult.AsyncWaitHandle.WaitOne();
    this._asyncResult = (IAsyncResult) null;
  }

  private void AddColumnsToTable(long tableID, string tableCaption, bool needUpdateTable)
  {
    try
    {
      DataSet tables = TableLoadHelper.GetTables(ImbaseRestructuringTablesService._systemSession, tableID, false);
      DataTable dataTable = tables != null ? tables.Tables["IMS_ATTR_TYPES"] : throw new RestructuringTablesExteption(LocalizationHolder.rm.GetString("Imbase_RestructuringTables_LoadTablesData_Error"), tableID, tableCaption);
      DataTable table = tables.Tables["IMS_DATA"];
      string empty = string.Empty;
      bool flag = false;
      List<int> intList = new List<int>(this._settings.Count);
      foreach (RestructuringTablesAttrSettings setting in this._settings)
      {
        try
        {
          string columnName = setting.AttributeGuid.ToString();
          if (dataTable.Select($"{"F_ATTRIBUTE_GUID"}='{columnName}'").Length != 0)
            throw new RestructuringTablesExteption(string.Format(LocalizationHolder.rm.GetString("Imbase_RestructuringTables_AttrAdded_Error"), (object) setting.AttributeName, (object) setting.AttributeID), tableID, tableCaption);
          IDBAttributeType attributeType = ImbaseRestructuringTablesService._systemSession.GetAttributeType(setting.AttributeGuid);
          if (TableLoadHelper.CreateDataColumn(table, attributeType) == null)
            throw new RestructuringTablesExteption(string.Format(LocalizationHolder.rm.GetString("Imbase_RestructuringTables_ColumnAdded_Error"), (object) setting.AttributeName, (object) setting.AttributeID), tableID, tableCaption);
          if (setting.DefaultValue != null && setting.DefaultValue != DBNull.Value)
          {
            foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
              row[columnName] = setting.DefaultValue;
          }
          DataRow row1 = dataTable.NewRow();
          row1["F_ATTRIBUTE_GUID"] = (object) setting.AttributeGuid;
          row1["F_REQUIRED"] = (object) setting.Required;
          row1["F_COMPUTED"] = (object) (setting.Required != 0 || string.IsNullOrEmpty(setting.Formula) ? 0 : 2);
          row1["F_FORMULA"] = (object) setting.Formula;
          row1["F_UNIQUE"] = (object) setting.Unique;
          row1["F_DEFAULT_VALUE"] = setting.DefaultValue;
          row1["F_OPTIONS"] = (object) setting.Options;
          row1["F_UNITS"] = (object) setting.Units;
          dataTable.Rows.Add(row1);
          dataTable.AcceptChanges();
          table.AcceptChanges();
          intList.Add(setting.AttributeID);
          flag = true;
        }
        catch (RestructuringTablesExteption ex)
        {
          this.ExceptionInfo.Add(ex);
        }
      }
      if (!flag)
        return;
      TableLoadHelper.StoreData(ImbaseRestructuringTablesService._systemSession, tableID, tables, ImbaseRestructuringTablesService._systemSession.GetCustomService(typeof (ITablesIndexer)) as ITablesIndexer);
      if (!needUpdateTable)
        return;
      this._addedAttrs.Add(tableID, intList);
    }
    catch (Exception ex)
    {
      throw new RestructuringTablesExteption(LocalizationHolder.rm.GetString("Imbase_RestructuringTables_LoadTablesData_Error"), tableID, tableCaption);
    }
  }

  private void ExcludeUniqueAttributes(List<long> tableIDs)
  {
    IDBObjectCollection objectCollection = ImbaseRestructuringTablesService._systemSession.GetCustomService(typeof (IImbaseIndexingService)) is IImbaseIndexingService customService ? ImbaseRestructuringTablesService._systemSession.GetObjectCollection(Intermech.Imbase.Consts.ImbaseTableRefTypeID) : throw new Exception(LocalizationHolder.rm.GetString("Imbase_Indexing_NullService"));
    if (objectCollection == null)
      throw new RestructuringTablesExteption(LocalizationHolder.rm.GetString("Imbase_ObjectCollection_TableRef_Error"));
    ColumnDescriptor columnDescriptor1 = new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0);
    ColumnDescriptor columnDescriptor2 = new ColumnDescriptor((object) Intermech.Imbase.Consts.ImbaseTableRefAttID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0);
    ColumnDescriptor columnDescriptor3 = new ColumnDescriptor((object) Intermech.Imbase.Consts.ClassifFolderKeyAttId, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.ASC, 0);
    int maximumInOperands = (ImbaseRestructuringTablesService._systemSession as UserSession).DataManager.DataProvider.MaximumINOperands;
    List<string> keys = new List<string>();
    string str = "empty";
    int index = 0;
    while (index < tableIDs.Count)
    {
      int count = tableIDs.Count - index > maximumInOperands ? maximumInOperands : tableIDs.Count - index;
      long[] numArray = new long[count];
      tableIDs.CopyTo(index, numArray, 0, count);
      index += count;
      DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(Intermech.Imbase.Consts.ImbaseTableRefAttID, RelationalOperators.In, (object) numArray, (object) null, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Object, ColumnContents.ID)
      }, new ColumnDescriptor[3]
      {
        columnDescriptor1,
        columnDescriptor2,
        columnDescriptor3
      });
      DataTable dataTable = objectCollection.Select(paramSet);
      if (dataTable.Rows.Count != 0)
      {
        this._tableRefInfoList = this._tableRefInfoList ?? new List<ImbaseRestructuringTablesService.TableRefInfo>(dataTable.Rows.Count);
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          ImbaseRestructuringTablesService.TableRefInfo tableRefInfo = new ImbaseRestructuringTablesService.TableRefInfo()
          {
            TableRefID = Convert.ToInt64(row[0]),
            TableID = Convert.ToInt64(row[1]),
            ClassifKey = Convert.ToString(row[2]),
            CatalogСlassifKey = str
          };
          this._tableRefInfoList.Add(tableRefInfo);
          if (!tableRefInfo.ClassifKey.StartsWith(str))
          {
            tableRefInfo.CatalogСlassifKey = str = tableRefInfo.ClassifKey.Substring(0, 2);
            if (!keys.Contains(str))
              keys.Add(str);
          }
        }
      }
    }
    if (keys.Count <= 0)
      return;
    Dictionary<string, long> catalogIDs = this.GetCatalogIDsByClassifFolderKeys(keys);
    if (catalogIDs == null)
      return;
    this._tableRefInfoList.ForEach((Action<ImbaseRestructuringTablesService.TableRefInfo>) (tri => tri.CatalogID = catalogIDs.ContainsKey(tri.CatalogСlassifKey) ? catalogIDs[tri.CatalogСlassifKey] : 0L));
    Guid sessionGuid = ImbaseRestructuringTablesService._systemSession.SessionGUID;
    List<long> list = catalogIDs.Values.ToList<long>();
    string[] colsNames = new string[3]
    {
      IndexesField.F_CATALOG_ID,
      IndexesField.F_ATTRIBUTE_ID,
      IndexesField.F_FLAG
    };
    DataTable indexes = customService.GetIndexes(sessionGuid, list, colsNames);
    if (indexes == null)
      return;
    Dictionary<long, List<int>> dictionary = new Dictionary<long, List<int>>();
    List<int> uniqueIndexes = new List<int>();
    int num = 17;
    foreach (DataRow row in (InternalDataCollectionBase) indexes.Rows)
    {
      long int64 = Convert.ToInt64(row[IndexesField.F_CATALOG_ID]);
      int int32 = Convert.ToInt32(row[IndexesField.F_ATTRIBUTE_ID]);
      if (Convert.ToInt32(row[IndexesField.F_FLAG]) != num)
      {
        if (!dictionary.ContainsKey(int64))
          dictionary.Add(int64, new List<int>(1));
        dictionary[int64].Add(int32);
      }
      else if (!uniqueIndexes.Contains(int32))
        uniqueIndexes.Add(int32);
    }
    foreach (RestructuringTablesAttrSettings tablesAttrSettings in this._settings.Where<RestructuringTablesAttrSettings>((System.Func<RestructuringTablesAttrSettings, bool>) (x => uniqueIndexes.Contains(x.AttributeID))).ToList<RestructuringTablesAttrSettings>())
    {
      this.ExceptionInfo.Add(new RestructuringTablesExteption(string.Format(LocalizationHolder.rm.GetString("Imbase_UniqueAttribute_CanNotAdd"), (object) tablesAttrSettings.AttributeName, (object) tablesAttrSettings.AttributeID)));
      this._settings.Remove(tablesAttrSettings);
    }
    foreach (KeyValuePair<long, List<int>> keyValuePair in dictionary)
    {
      KeyValuePair<long, List<int>> pair = keyValuePair;
      this._tableRefInfoList.Where<ImbaseRestructuringTablesService.TableRefInfo>((System.Func<ImbaseRestructuringTablesService.TableRefInfo, bool>) (x => x.CatalogID == pair.Key)).ToList<ImbaseRestructuringTablesService.TableRefInfo>().ForEach((Action<ImbaseRestructuringTablesService.TableRefInfo>) (tri => tri.Indexes = pair.Value));
    }
  }

  private void ProcessingTable(long tableID)
  {
    bool flag = false;
    IDBObject dbObject = (IDBObject) null;
    try
    {
      dbObject = ImbaseRestructuringTablesService._systemSession.GetObjectActualCopy(tableID, false);
      if (dbObject == null)
        throw new RestructuringTablesExteption(LocalizationHolder.rm.GetString("Imbase_GetObject_Error"), tableID);
      if (dbObject.ObjectModifyMode == ObjectModifyModes.CantModify)
        throw new RestructuringTablesExteption(LocalizationHolder.rm.GetString("Imbase_CantModifyObject_Error"), tableID, dbObject.Caption);
      if (dbObject.CheckoutBy != 0L && dbObject.CheckoutBy != this.UserID)
        throw new RestructuringTablesExteption(LocalizationHolder.rm.GetString("Imbase_ObjectCheckOutAnotherUser_Error"), tableID, dbObject.Caption);
      bool needUpdateTable = false;
      if (dbObject.ObjectModifyMode != ObjectModifyModes.InBase)
      {
        flag = dbObject.CheckoutBy == 0L;
        if (flag)
          dbObject = dbObject.CheckOut(false);
      }
      else
        needUpdateTable = true;
      this.AddColumnsToTable(dbObject.ObjectID, dbObject.Caption, needUpdateTable);
      if (!flag)
        return;
      dbObject.CheckIn();
    }
    catch (RestructuringTablesExteption ex)
    {
      this.ExceptionInfo.Add(ex);
      if (!flag)
        return;
      dbObject.CancelChanges();
    }
  }

  private Dictionary<string, long> GetCatalogIDsByClassifFolderKeys(List<string> keys)
  {
    IDBObjectCollection objectCollection = ImbaseRestructuringTablesService._systemSession.GetObjectCollection(Intermech.Imbase.Consts.ImbaseCatalogTypeID);
    if (objectCollection == null)
      throw new IndexingException(LocalizationHolder.rm.GetString("Imbase_ObjectCollection_Catalog_Error"));
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(Intermech.Imbase.Consts.ClassifFolderKeyAttId, RelationalOperators.In, (object) keys.ToArray(), LogicalOperators.NONE, 0, true)
    }, new ColumnDescriptor[2]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) Intermech.Imbase.Consts.ClassifFolderKeyAttId, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0)
    });
    DataTable source = objectCollection.Select(paramSet);
    return source == null || source.Rows.Count <= 0 ? (Dictionary<string, long>) null : source.AsEnumerable().Select<DataRow, DataRow>((System.Func<DataRow, DataRow>) (x => x)).ToDictionary<DataRow, string, long>((System.Func<DataRow, string>) (k => Convert.ToString(k[1])), (System.Func<DataRow, long>) (v => Convert.ToInt64(v[0])));
  }

  private void GetSystemSession(string sessionName)
  {
    lock (this._lockObject)
    {
      if (ImbaseRestructuringTablesService._systemSession != null)
        return;
      ImbaseRestructuringTablesService._systemSession = (ServerServices.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents).GetSystemSessionPermanentClone(sessionName);
    }
  }

  private List<long> GetTableRefIDs(long sourceObjID)
  {
    List<long> tableRefIds = (List<long>) null;
    IDBObjectCollection objectCollection = ImbaseRestructuringTablesService._systemSession.GetObjectCollection(Intermech.Imbase.Consts.ImbaseTableRefTypeID);
    if (objectCollection == null)
      throw new RestructuringTablesExteption(LocalizationHolder.rm.GetString("Imbase_ObjectCollection_TableRef_Error"));
    string classifKeyByObjId = ImbaseHelper.GetClassifKeyByObjID(ImbaseRestructuringTablesService._systemSession, sourceObjID);
    if (!string.IsNullOrEmpty(classifKeyByObjId))
    {
      List<ColumnDescriptor> columnDescriptorList = new List<ColumnDescriptor>()
      {
        new ColumnDescriptor((object) Intermech.Imbase.Consts.ImbaseTableRefAttID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.ASC, 0)
      };
      DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[2]
      {
        new ConditionStructure(Intermech.Imbase.Consts.ClassifFolderKeyAttId, RelationalOperators.StartString, (object) classifKeyByObjId, LogicalOperators.AND, 0, false),
        new ConditionStructure(Intermech.Imbase.Consts.ImbaseTableRefAttID, RelationalOperators.NotEmpty, (object) null, LogicalOperators.NONE, 0, false)
      }, columnDescriptorList.ToArray());
      DataTable source = objectCollection.Select(paramSet);
      tableRefIds = source.Rows.Count > 0 ? source.AsEnumerable().Select<DataRow, long>((System.Func<DataRow, long>) (x => Convert.ToInt64(x[0]))).Distinct<long>().ToList<long>() : (List<long>) null;
    }
    return tableRefIds;
  }

  private void OnTaskTerminated(IAsyncResult res)
  {
    this._asyncResult = (IAsyncResult) null;
    this._terminated = true;
  }

  private void RenameFormulaFields()
  {
    if (!this._settings.Any<RestructuringTablesAttrSettings>((System.Func<RestructuringTablesAttrSettings, bool>) (x => !string.IsNullOrEmpty(x.Formula))))
      return;
    IDBAttributeTypeCollection attributeTypeCollection = ImbaseRestructuringTablesService._systemSession.GetAttributeTypeCollection(-1);
    if (attributeTypeCollection == null)
      return;
    DataTable dataTable = attributeTypeCollection.Select("");
    List<AttributeTypeProperties> attributeTypePropertiesList = new List<AttributeTypeProperties>(dataTable.Rows.Count);
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      FieldTypes int32 = (FieldTypes) Convert.ToInt32(row["F_ATTRIBUTE_TYPE"]);
      switch (int32)
      {
        case FieldTypes.ftShortBlob:
        case FieldTypes.ftFile:
        case FieldTypes.ftBlob:
          continue;
        default:
          string str = Convert.ToString(row["F_GUID"]);
          if (GuidHelper.IsGuid(str))
          {
            AttributeTypeProperties attributeTypeProperties = new AttributeTypeProperties(Convert.ToString(row["F_NAME"]), int32)
            {
              AttributeGuid = new Guid(str)
            };
            attributeTypePropertiesList.Add(attributeTypeProperties);
            continue;
          }
          continue;
      }
    }
    string empty = string.Empty;
    foreach (RestructuringTablesAttrSettings setting in this._settings)
    {
      string str = setting.Formula;
      if (!string.IsNullOrEmpty(str))
      {
        foreach (AttributeTypeProperties attributeTypeProperties in attributeTypePropertiesList)
          str = str.Replace($"[{attributeTypeProperties.Name}]", $"[{attributeTypeProperties.AttributeGuid.ToString()}]");
        setting.Formula = str;
      }
    }
  }

  private void ScanProcess()
  {
    try
    {
      this.ExceptionInfo = new List<RestructuringTablesExteption>();
      if (this.SourceObjID == 0L)
        throw new RestructuringTablesExteption(LocalizationHolder.rm.GetString("Imbase_RestructuringTables_EmptySourceID"));
      if (this._settings == null || this._settings.Count == 0)
        throw new RestructuringTablesExteption(LocalizationHolder.rm.GetString("Imbase_RestructuringTables_EmptyAttrList"));
      this.GetSystemSession("ImbaseRestructTable.ScanProcess");
      QuickObjectInfo quickObjectInfo = ImbaseRestructuringTablesService._systemSession != null ? ImbaseRestructuringTablesService._systemSession.GetObjectInfo(this.SourceObjID) : throw new RestructuringTablesExteption(LocalizationHolder.rm.GetString("Imbase_NullSession"));
      if (quickObjectInfo.Empty)
        throw new RestructuringTablesExteption(LocalizationHolder.rm.GetString("Imbase_RestructuringTables_SourceObjectInfo_LoadError"), this.SourceObjID);
      List<long> tableIDs;
      if (quickObjectInfo.ObjectTypeID == Intermech.Imbase.Consts.ImbaseTableRefTypeID)
      {
        long tableReference = TableLoadHelper.GetTableReference(ImbaseRestructuringTablesService._systemSession, this.SourceObjID);
        string format = LocalizationHolder.rm.GetString("Imbase_RestructuringTables_EmptyTableRef");
        tableIDs = tableReference != 0L ? new List<long>()
        {
          tableReference
        } : throw new RestructuringTablesExteption(string.Format(format, (object) quickObjectInfo.Caption, (object) this.SourceObjID));
      }
      else
        tableIDs = this.GetTableRefIDs(this.SourceObjID);
      if (tableIDs == null)
        return;
      this.ExcludeUniqueAttributes(tableIDs);
      if (this._settings.Count <= 0)
        return;
      this.RenameFormulaFields();
      this._countItems = tableIDs.Count;
      foreach (long tableID in tableIDs)
      {
        if (!this.IsProcessStoped)
        {
          this.ProcessingTable(tableID);
          ++this._currItemNumbetr;
        }
        else
          break;
      }
      this.UpdateIndexes();
    }
    catch (RestructuringTablesExteption ex)
    {
      this.ExceptionInfo.Add(ex);
    }
    finally
    {
      ImbaseRestructuringTablesService._systemSession.Logout("ImbaseRestructTable.ScanProcess");
      ImbaseRestructuringTablesService._systemSession = (IUserSession) null;
    }
  }

  private void UpdateIndexes()
  {
    if (!(ImbaseRestructuringTablesService._systemSession.GetCustomService(typeof (IImbaseIndexingService)) is IImbaseIndexingService customService) || this._addedAttrs.Count <= 0)
      return;
    foreach (KeyValuePair<long, List<int>> addedAttr in this._addedAttrs)
    {
      long tableID = addedAttr.Key;
      List<int> attrIDs = addedAttr.Value;
      DataSet tables = TableLoadHelper.GetTables(ImbaseRestructuringTablesService._systemSession, tableID, false);
      if (tables != null && tables.Tables.Contains("IMS_ATTR_TYPES") && tables.Tables.Contains("IMS_DATA"))
      {
        DataTable table1 = tables.Tables["IMS_ATTR_TYPES"];
        DataTable table2 = tables.Tables["IMS_DATA"];
        List<ImbaseRestructuringTablesService.TableRefInfo> list = this._tableRefInfoList.Where<ImbaseRestructuringTablesService.TableRefInfo>((System.Func<ImbaseRestructuringTablesService.TableRefInfo, bool>) (x => x.TableID == tableID)).ToList<ImbaseRestructuringTablesService.TableRefInfo>();
        Dictionary<long, List<long>> dictionary = new Dictionary<long, List<long>>(list.Count);
        foreach (ImbaseRestructuringTablesService.TableRefInfo tableRefInfo in list)
        {
          if (!dictionary.ContainsKey(tableRefInfo.CatalogID))
            dictionary.Add(tableRefInfo.CatalogID, new List<long>()
            {
              tableRefInfo.TableRefID
            });
          else
            dictionary[tableRefInfo.CatalogID].Add(tableRefInfo.TableRefID);
        }
        foreach (KeyValuePair<long, List<long>> keyValuePair in dictionary)
          customService.UpdateAfterRestructured(ImbaseRestructuringTablesService._systemSession.SessionGUID, keyValuePair.Key, keyValuePair.Value, tableID, table1, table2, attrIDs);
      }
    }
  }

  private delegate void RestructuringTablesHandler();

  private class TableRefInfo
  {
    public long CatalogID { get; set; }

    public string CatalogСlassifKey { get; set; }

    public List<int> Indexes { get; set; }

    public long TableRefID { get; set; }

    public string ClassifKey { get; set; }

    public long TableID { get; set; }
  }
}
