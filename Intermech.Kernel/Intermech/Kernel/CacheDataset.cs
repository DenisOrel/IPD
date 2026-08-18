// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.CacheDataset
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using Intermech.Kernel.Services;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Threading;


namespace Intermech.Kernel;

public class CacheDataset : KernelRoot, ICacheDataset, IObjectsInfoCache
{
  private static int _productTypeID = -1;
  private static int _artTypeID = -1;
  private static int _docTypeID = -1;
  private static int _spcTypeID = -1;
  internal bool CacheLoaded;
  internal DataSet _DBSet;
  private DataTable _ObjectAttrsTable;
  private DataTable _RelationAttrsTable;
  private DataSet _PossibleValuesTables;
  internal ConcurrentDictionary<long, QuickObjectInfo> _ObjectsInfoCacheWrapper;
  internal ConcurrentDictionary<Guid, QuickObjectInfo> _ObjectsInfoGuidCacheWrapper;
  private ConcurrentDictionary<int, int> _AttrGroupNames;
  private DateTime _ModifyDate;
  private ReaderWriterLock md_rwl = new ReaderWriterLock();
  internal Hashtable ObjecTypeGUIDs = Hashtable.Synchronized(new Hashtable());
  private ConcurrentDictionary<int, string> _AccessLevels = new ConcurrentDictionary<int, string>();
  internal ConcurrentDictionary<int, int> ObjecTypeParents = new ConcurrentDictionary<int, int>();
  internal ConcurrentDictionary<int, int[]> ObjecTypeDecodingAttributes = new ConcurrentDictionary<int, int[]>();
  internal Hashtable CaptionAttributes = Hashtable.Synchronized(new Hashtable());
  internal Hashtable PrimaryKeys = new Hashtable();
  internal Hashtable tablesModifyTime = Hashtable.Synchronized(new Hashtable());
  internal Hashtable _FilePrototypes = Hashtable.Synchronized(new Hashtable());
  internal Hashtable AttributesInViewsHash = Hashtable.Synchronized(new Hashtable());
  private ConcurrentDictionary<CacheDataset.Attribute4Formulas, int[]> _FormulaAttributesHash4Objects = new ConcurrentDictionary<CacheDataset.Attribute4Formulas, int[]>();
  private ConcurrentDictionary<CacheDataset.Attribute4Formulas, int[]> _FormulaAttributesHash4Relations = new ConcurrentDictionary<CacheDataset.Attribute4Formulas, int[]>();
  private int[] EmptyAttrArray = new int[0];
  internal string[] _MainObjectsView = new string[1]
  {
    "IMS_OBJECTS_VIEW"
  };
  public static bool PatchMode = false;
  internal string[] TablesNameList = new string[20]
  {
    "IMS_ATTR_GROUPS",
    "IMS_ATTRIBUTES",
    "IMS_ATTR_IN_GROUPS",
    "IMS_DBVERSION",
    "IMS_LANGUAGES",
    "IMS_LC_STEPS",
    "IMS_LEVELS",
    "IMS_OBJECT_TYPES",
    "IMS_OBJTYPES_TREE",
    "IMS_RELATION_TYPES",
    "IMS_SUBJECT_AREAS",
    "IMS_TYPES_APPLICABILITY",
    "IMS_ATTR4OBJ_TYPES",
    "IMS_ATTR4RELATION_TYPES",
    "IMS_LC_LINKS",
    "IMS_FORMULA_ATTRS",
    "IMS_METADATA",
    "IMS_POSSIBLE_VALUES",
    "IMS_LC_SCHEMAS",
    "IMS_MD_EXTENSIONS"
  };
  private ReaderWriterLockSlim cacheLock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
  private List<int> _SyncParentObjectTypes;
  private List<int> _SyncCheckInParentObjectTypes;
  private IPossibleValuesCache pvCache;
  private List<Tuple<long, Guid, string>> _Users;
  private IDbManager _dbManager;

  internal event TableChangedHandler TableValueChanged;

  public CacheDataset(IDbManager db)
  {
    if (AdminUtilsService.ServerRunMode == ServerRunModes.Console)
      Console.Write(LocalizationHolder.rm.GetString("Kernel_659"));
    this._DBSet = new DataSet();
    this._DBSet.RemotingFormat = SerializationFormat.Binary;
    this._ObjectAttrsTable = db.ExecuteDataTable(sc_12972.ssp_appserver_12973());
    this._RelationAttrsTable = db.ExecuteDataTable("SELECT * FROM IMS_RELATION_ATTRS WHERE F_PRJLINK_ID = -1");
    this._PossibleValuesTables = new DataSet();
    this.InitPossibleValuesTable(db, "F_STRING_VALUE");
    this.InitPossibleValuesTable(db, "F_INTEGER_VALUE");
    this.InitPossibleValuesTable(db, "F_DOUBLE_VALUE");
    this.InitPossibleValuesTable(db, "F_DATE_VALUE");
    this._ObjectsInfoCacheWrapper = new ConcurrentDictionary<long, QuickObjectInfo>();
    this._ObjectsInfoGuidCacheWrapper = new ConcurrentDictionary<Guid, QuickObjectInfo>();
    this._AttrGroupNames = new ConcurrentDictionary<int, int>();
    DataSetProcessor.FillPrimaryKeys(this.PrimaryKeys);
    this.LoadTables(db);
    this.CheckDBVersion(db);
    DataTable table = this.GetTable("IMS_METADATA");
    bool flag = false;
    foreach (string tablesName in this.TablesNameList)
    {
      if (tablesName != "IMS_METADATA" && table.Rows.Find((object) tablesName) == null)
      {
        db.ExecuteNonQuery($"INSERT INTO IMS_METADATA (F_TABLE_NAME, F_MODIFY_DATE) VALUES ({SqlHelper.QString(tablesName)}, {db.DataProvider.Now})");
        flag = true;
      }
    }
    if (flag)
      this.ReloadTables((IUserSession) null, db, new string[1]
      {
        "IMS_METADATA"
      });
    if (AdminUtilsService.ServerRunMode != ServerRunModes.Console)
      return;
    Console.WriteLine("OK");
  }

  internal void FillSyncParentObjectTypes(IDbManager db)
  {
    List<int> intList1 = new List<int>(50);
    List<int> intList2 = new List<int>(50);
    DataTable dataTable = db.ExecuteDataTable("SELECT F_INOBJECT_TYPE, F_OPTIONS FROM IMS_TYPES_APPLICABILITY");
    for (int index1 = 0; index1 < dataTable.Rows.Count; ++index1)
    {
      ApplicabilityOptions int32 = (ApplicabilityOptions) Convert.ToInt32(dataTable.Rows[index1]["F_OPTIONS"]);
      if ((int32 & ApplicabilityOptions.ChangeLCStep) == ApplicabilityOptions.ChangeLCStep)
      {
        List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(Convert.ToInt32(dataTable.Rows[index1]["F_INOBJECT_TYPE"]));
        for (int index2 = 0; index2 < childrenIdRecursive.Count; ++index2)
        {
          if (intList1.IndexOf(childrenIdRecursive[index2]) < 0)
            intList1.Add(childrenIdRecursive[index2]);
        }
      }
      if ((int32 & ApplicabilityOptions.SyncCheckin) == ApplicabilityOptions.SyncCheckin)
      {
        List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(Convert.ToInt32(dataTable.Rows[index1]["F_INOBJECT_TYPE"]));
        for (int index3 = 0; index3 < childrenIdRecursive.Count; ++index3)
        {
          if (intList2.IndexOf(childrenIdRecursive[index3]) < 0)
            intList2.Add(childrenIdRecursive[index3]);
        }
      }
    }
    this._SyncParentObjectTypes = intList1;
    this._SyncCheckInParentObjectTypes = intList2;
  }

  public bool IsSyncParentObjectType(int objTypeID)
  {
    return this._SyncParentObjectTypes == null || this._SyncParentObjectTypes.IndexOf(objTypeID) > -1;
  }

  public bool IsSyncCheckInParentObjectType(int objTypeID)
  {
    return this._SyncCheckInParentObjectTypes == null || this._SyncCheckInParentObjectTypes.IndexOf(objTypeID) > -1;
  }

  private void CheckDBVersion(IDbManager db)
  {
    DataRow[] dataRowArray = this.GetTable("IMS_DBVERSION").Select("F_MODULE_NAME = 'KERNEL'");
    if (dataRowArray.Length == 0)
      return;
    int int32 = Convert.ToInt32(dataRowArray[0]["F_VERSION_ID"]);
    if (int32 > 710)
    {
      string str = string.Format(LocalizationHolder.rm.GetString(sc_12972.ssp_appserver_12974()), (object) int32, (object) 710);
      if (ServerServices.GetService(typeof (IEventLogHelper)) is EventLogHelper service)
        service.AddToTrace(str, Consts.traceAlways, string.Empty);
      throw new KernelException(str);
    }
    if (int32 == 710)
      return;
    object obj1 = db.ExecuteScalar($"SELECT F_VALUE FROM IMS_CONFIGS WHERE F_MODULE_NAME = '{"KERNEL"}' AND F_SECTION_ID = 'DB_PATCH' AND F_PARAM_NAME = 'PATCH_COMP' AND F_USER_ID = 0");
    if (obj1 != null && obj1 != DBNull.Value)
    {
      int num = 0;
      object obj2 = db.ExecuteScalar($"SELECT F_VALUE FROM IMS_CONFIGS WHERE F_MODULE_NAME = '{"KERNEL"}' AND F_SECTION_ID = 'DB_PATCH' AND F_PARAM_NAME = 'PATCH_VER' AND F_USER_ID = 0");
      try
      {
        num = Convert.ToInt32(obj2);
      }
      catch
      {
      }
      string str = string.Format(sc_12972.ssp_appserver_12975(), obj1, (object) num);
      if (ServerServices.GetService(typeof (IEventLogHelper)) is EventLogHelper service)
        service.AddToTrace(str, Consts.traceAlways, string.Empty);
      throw new KernelException(str);
    }
    db.ExecuteNonQuery($"INSERT INTO IMS_CONFIGS (F_MODULE_NAME, F_SECTION_ID,  F_PARAM_NAME, F_USER_ID, F_VALUE) VALUES ('KERNEL', 'DB_PATCH', 'PATCH_COMP', 0, '{EnvironmentConsts.MachineName}')");
    db.ExecuteNonQuery($"INSERT INTO IMS_CONFIGS (F_MODULE_NAME, F_SECTION_ID,  F_PARAM_NAME, F_USER_ID, F_VALUE) VALUES ('KERNEL', 'DB_PATCH', 'PATCH_VER', 0, {710})");
    CacheDataset.PatchMode = true;
  }

  private void InitPossibleValuesTable(IDbManager db, string dataFieldName)
  {
    DataTable table = db.ExecuteDataTable(string.Format(sc_12972.ssp_appserver_12976(), (object) dataFieldName));
    table.TableName = dataFieldName;
    this._PossibleValuesTables.Tables.Add(table);
  }

  internal DataTable GetPossibleValuesTable(string dataFieldName)
  {
    return this._PossibleValuesTables.Tables[dataFieldName];
  }

  public string[] GetUpdateTables(int attributeID, int objectTypeID, int relationTypeID)
  {
    if (attributeID < -1)
      attributeID = -1;
    object obj = this.AttributesInViewsHash[(object) new Attribute4ID(attributeID, objectTypeID, relationTypeID)];
    if (obj == null)
    {
      if (objectTypeID > -1)
      {
        DataRow dataRow1 = this.GetTable("IMS_ATTRIBUTES").Rows.Find((object) attributeID);
        DataRow dataRow2 = this.GetTable("IMS_OBJECT_TYPES").Rows.Find((object) objectTypeID);
        if (dataRow1 != null && dataRow2 != null && Convert.ToInt32(dataRow1["F_INVIEW"]) != 0 && (Convert.ToInt32(dataRow2["F_OPTIONS"]) & 16 /*0x10*/) == 0)
          return this._MainObjectsView;
      }
      return (string[]) null;
    }
    if (objectTypeID > -1)
    {
      DataRow dataRow = this.GetTable("IMS_OBJECT_TYPES").Rows.Find((object) objectTypeID);
      if (dataRow != null && (Convert.ToInt32(dataRow["F_OPTIONS"]) & 16 /*0x10*/) == 16 /*0x10*/)
      {
        string[] tables = (obj as Attribute4Props).Tables;
        if (tables != null)
        {
          for (int index = 0; index < tables.Length; ++index)
          {
            if (tables[index] == "IMV_O" + objectTypeID.ToString())
              return new string[1]{ tables[index] };
          }
        }
        return (string[]) null;
      }
    }
    return (obj as Attribute4Props).Tables;
  }

  public AttributeOptions GetAttributeOptions(
    int attributeID,
    int objectTypeID,
    int relationTypeID)
  {
    object obj = this.AttributesInViewsHash[(object) new Attribute4ID(attributeID, objectTypeID, relationTypeID)];
    return obj == null ? AttributeOptions.None : (obj as Attribute4Props).Options;
  }

  public OptimizationModes GetOptimizationMode(Attribute4ID attrStruct)
  {
    if (attrStruct.AttributeID < -1)
      attrStruct.AttributeID = -1;
    object obj = this.AttributesInViewsHash[(object) attrStruct];
    return obj == null ? OptimizationModes.NotFound : (obj as Attribute4Props).Mode;
  }

  public OptimizationModes GetOptimizationMode(int attributeID)
  {
    return this.GetOptimizationMode(new Attribute4ID(attributeID, -1, -1));
  }

  public OptimizationModes GetOptimizationMode(
    int attributeID,
    int objectTypeID,
    int relationTypeID)
  {
    return this.GetOptimizationMode(new Attribute4ID(attributeID, objectTypeID, relationTypeID));
  }

  public void SetAttrProperties(
    Attribute4ID attrID,
    OptimizationModes newValue,
    AttributeOptions options)
  {
    if (this.AttributesInViewsHash[(object) attrID] is Attribute4Props attribute4Props)
    {
      if (newValue != OptimizationModes.NotFound)
        attribute4Props.Mode = newValue;
      if (attribute4Props.Options == options)
        return;
      attribute4Props.Options = options;
    }
    else
      this.AttributesInViewsHash[(object) attrID] = (object) new Attribute4Props(newValue, options);
  }

  public DateTime ModifyDate
  {
    get
    {
      this.md_rwl.AcquireReaderLock(TimeSpan.FromHours(1.0));
      try
      {
        return this._ModifyDate;
      }
      finally
      {
        this.md_rwl.ReleaseReaderLock();
      }
    }
  }

  private void RefreshModifyDate(string tableName, IDbManager db)
  {
    this.md_rwl.AcquireWriterLock(TimeSpan.FromHours(1.0));
    try
    {
      this._ModifyDate = DateTime.UtcNow;
      object obj = db.ExecuteScalar("SELECT F_MODIFY_DATE FROM IMS_METADATA WHERE F_TABLE_NAME = :tblName", db.Parameter("tblName", (object) tableName.ToUpper()));
      if (obj != null && obj != DBNull.Value)
        this.tablesModifyTime[(object) tableName] = (object) Convert.ToDateTime(obj);
      else
        this.tablesModifyTime[(object) tableName] = (object) this._ModifyDate;
    }
    finally
    {
      this.md_rwl.ReleaseWriterLock();
    }
  }

  public DataTable GetObjectAttsEmptyRow(int attributeID, long objectID, int inListID)
  {
    DataTable objectAttsEmptyRow = this._ObjectAttrsTable.Clone();
    DataRow row = objectAttsEmptyRow.NewRow();
    row["F_ATTRIBUTE_ID"] = (object) attributeID;
    row["F_OBJECT_ID"] = (object) objectID;
    row["F_INLIST_ID"] = (object) inListID;
    objectAttsEmptyRow.Rows.Add(row);
    return objectAttsEmptyRow;
  }

  public void AddObjectInfo(QuickObjectInfo info)
  {
    this._ObjectsInfoCacheWrapper.TryAdd(info.ObjectID, info);
  }

  public void UpdateObjectInfo(QuickObjectInfo info)
  {
    QuickObjectInfo quickObjectInfo;
    this._ObjectsInfoCacheWrapper.TryRemove(info.ObjectID, out quickObjectInfo);
    this._ObjectsInfoCacheWrapper.TryAdd(info.ObjectID, info);
    this._ObjectsInfoGuidCacheWrapper.TryRemove(info.VersionGuid, out quickObjectInfo);
    this._ObjectsInfoGuidCacheWrapper.TryAdd(info.VersionGuid, info);
  }

  public void DeleteObjectInfo(long objectID, Guid versionGuid)
  {
    QuickObjectInfo quickObjectInfo;
    this._ObjectsInfoCacheWrapper.TryRemove(objectID, out quickObjectInfo);
    if (!(versionGuid != Guid.Empty))
      return;
    this._ObjectsInfoGuidCacheWrapper.TryRemove(versionGuid, out quickObjectInfo);
  }

  public QuickObjectInfo GetObjectInfo(IDbManager db, long objectID)
  {
    if (objectID == 0L || objectID == -1L)
      return new QuickObjectInfo(0L, string.Empty, -1, Guid.Empty, -1L);
    QuickObjectInfo objectInfo;
    if (!this._ObjectsInfoCacheWrapper.TryGetValue(objectID, out objectInfo))
    {
      DataTable dataTable1 = db.ExecuteDataTable("SELECT F_OBJECT_TYPE, F_ID FROM IMS_OBJECTS WHERE F_OBJECT_ID = :id", db.Parameter("id", (object) objectID));
      if (dataTable1.Rows.Count == 0)
        return new QuickObjectInfo(objectID, string.Empty, -1, Guid.Empty, -1L);
      string str = objectID >= 0L ? "CAPTION" : "F_WORK_CAPTION";
      DataTable dataTable2 = db.ExecuteDataTable($"SELECT {str}, F_GUID FROM IMS_GUID WHERE F_OBJECT_ID IN (:id, :id_min)", db.Parameter("id", (object) objectID), db.Parameter("id_min", (object) -objectID));
      if (dataTable2.Rows.Count == 0)
        return new QuickObjectInfo(objectID, string.Empty, -1, Guid.Empty, -1L);
      objectInfo = new QuickObjectInfo(objectID, dataTable2.Rows[0][0].ToString(), Convert.ToInt32(dataTable1.Rows[0][0]), new Guid(dataTable2.Rows[0][1].ToString()), Convert.ToInt64(dataTable1.Rows[0][1]));
      this._ObjectsInfoCacheWrapper[objectID] = objectInfo;
    }
    return objectInfo;
  }

  public QuickObjectInfo GetObjectInfo(IDbManager db, Guid objectGUID)
  {
    if (objectGUID == Guid.Empty)
      return new QuickObjectInfo(0L, string.Empty, -1, Guid.Empty, -1L);
    QuickObjectInfo objectInfo;
    if (!this._ObjectsInfoGuidCacheWrapper.TryGetValue(objectGUID, out objectInfo))
    {
      string empty = string.Empty;
      DataTable dataTable1 = db.ExecuteDataTable("SELECT F_OBJECT_ID, CAPTION, F_WORK_CAPTION FROM IMS_GUID WHERE F_GUID = :guidPar", db.Parameter("guidPar", (object) objectGUID));
      if (dataTable1.Rows.Count <= 0)
        return new QuickObjectInfo(0L, string.Empty, -1, objectGUID, -1L);
      if (dataTable1.Rows[0][1] != DBNull.Value)
        empty = dataTable1.Rows[0][1].ToString();
      if (empty == string.Empty)
        empty = dataTable1.Rows[0][2].ToString();
      long int64 = Convert.ToInt64(dataTable1.Rows[0][0]);
      DataTable dataTable2 = db.ExecuteDataTable("SELECT F_OBJECT_TYPE, F_ID FROM IMS_OBJECTS WHERE F_OBJECT_ID = :id", db.Parameter("id", (object) int64));
      objectInfo = new QuickObjectInfo(int64, empty, Convert.ToInt32(dataTable2.Rows[0][0]), objectGUID, Convert.ToInt64(dataTable2.Rows[0][1]));
      this._ObjectsInfoGuidCacheWrapper[objectGUID] = objectInfo;
    }
    return objectInfo;
  }

  public DataTable GetRelationAttsEmptyRow(int attributeID, long relationID, int inListID)
  {
    DataTable relationAttsEmptyRow = this._RelationAttrsTable.Clone();
    DataRow row = relationAttsEmptyRow.NewRow();
    row["F_ATTRIBUTE_ID"] = (object) attributeID;
    row["F_PRJLINK_ID"] = (object) relationID;
    row["F_INLIST_ID"] = (object) inListID;
    relationAttsEmptyRow.Rows.Add(row);
    return relationAttsEmptyRow;
  }

  public void LoadTables(IDbManager db)
  {
    if (this.CacheLoaded)
      return;
    this._ObjectsInfoCacheWrapper.Clear();
    this._ObjectsInfoGuidCacheWrapper.Clear();
    this.ReloadTables((IUserSession) null, db, this.TablesNameList);
    this.CacheLoaded = true;
    if (!(ServerServices.GetService(typeof (IEventLogHelper)) is EventLogHelper service))
      return;
    service.OnCacheReload(db);
  }

  public void ChangeTableValue(
    string filterStr,
    string tableName,
    string fieldName,
    object newValue,
    IUserSession uSession)
  {
    List<object> objectList = (List<object>) null;
    this.cacheLock.EnterWriteLock();
    try
    {
      DataRow[] dataRowArray = this._DBSet.Tables[tableName].Select(filterStr);
      UserSession userSession = uSession as UserSession;
      if (dataRowArray.Length != 0)
      {
        objectList = new List<object>(dataRowArray.Length);
        foreach (DataRow dataRow in dataRowArray)
        {
          objectList.Add(dataRow[fieldName]);
          dataRow[fieldName] = newValue;
        }
        userSession?.AddModifiedCacheTable(tableName);
      }
      this.RefreshModifyDate(tableName, userSession.DataManager);
    }
    finally
    {
      this.cacheLock.ExitWriteLock();
    }
    CacheDataset.UpdateMetaDataHelper(uSession, tableName, MetaDataHelperServiceUpdateTask.MetaDataIncGeneration);
    this.OnTableChanged(objectList != null ? (TableChangedEventArgs) new TableValueChangedArgs(uSession, filterStr, tableName, fieldName, objectList.ToArray(), newValue) : (TableChangedEventArgs) new TableValueChangedArgs(uSession, filterStr, tableName, fieldName));
  }

  private void OnTableChanged(TableChangedEventArgs args)
  {
    if (this.TableValueChanged == null)
      return;
    this.TableValueChanged((object) this, args);
  }

  public void EnterReadLocker() => this.cacheLock.EnterReadLock();

  public void ExitReadLocker() => this.cacheLock.ExitReadLock();

  private void AddRow(DataTable toTable, DataRow row, IUserSession uSession)
  {
    this.cacheLock.EnterWriteLock();
    try
    {
      DataRow row1 = toTable.NewRow();
      for (int index = 0; index < toTable.Columns.Count; ++index)
        row1[toTable.Columns[index].ColumnName] = row[toTable.Columns[index].ColumnName];
      toTable.Rows.Add(row1);
    }
    finally
    {
      this.cacheLock.ExitWriteLock();
    }
    this.RefreshModifyDate(toTable.TableName, (uSession as UserSession).DataManager);
    if (uSession != null)
    {
      (uSession as UserSession).AddModifiedCacheTable(toTable.TableName);
      if (toTable.TableName == "IMS_FORMULA_ATTRS")
        this.RebuildFormulasCache(toTable);
      CacheDataset.UpdateMetaDataHelper(uSession, toTable.TableName, MetaDataHelperServiceUpdateTask.MetaDataIncGeneration);
    }
    this.OnTableChanged((TableChangedEventArgs) new TableValueAddedArgs(uSession, toTable.TableName, row));
  }

  internal static void UpdateMetaDataHelper(
    IUserSession session,
    string tableName,
    MetaDataHelperServiceUpdateTask advTask)
  {
    if (session == null || MetaDataHelper.Locked)
      return;
    if (string.IsNullOrEmpty(tableName))
    {
      MetaDataHelperUpdateService.AddTask(MetaDataHelperServiceUpdateTask.Full);
    }
    else
    {
      MetaDataHelperServiceUpdateTask serviceUpdateTask = MetaDataHelperServiceUpdateTask.None;
      try
      {
        switch (tableName)
        {
          case "IMS_ATTR4OBJ_TYPES":
          case "IMS_ATTR4RELATION_TYPES":
          case "IMS_ATTRIBUTES":
          case "IMS_POSSIBLE_VALUES":
            serviceUpdateTask = MetaDataHelperServiceUpdateTask.AttrTypes;
            break;
          case "IMS_LC_SCHEMAS":
          case "IMS_LC_STEPS":
          case "IMS_LEVELS":
            serviceUpdateTask = MetaDataHelperServiceUpdateTask.LCSteps;
            break;
          case "IMS_OBJECT_TYPES":
            serviceUpdateTask = MetaDataHelperServiceUpdateTask.Full;
            break;
          case "IMS_OBJTYPES_TREE":
            serviceUpdateTask = MetaDataHelperServiceUpdateTask.ObjectTypesHierarchy;
            break;
          case "IMS_RELATION_TYPES":
          case "IMS_TYPES_APPLICABILITY":
            serviceUpdateTask = MetaDataHelperServiceUpdateTask.RelationTypes;
            break;
          default:
            serviceUpdateTask = MetaDataHelperServiceUpdateTask.Full;
            break;
        }
      }
      finally
      {
        MetaDataHelperUpdateService.AddTask(serviceUpdateTask | advTask);
      }
    }
  }

  public void AddRow(string toTableName, DataRow row, IUserSession uSession)
  {
    this.AddRow(this.GetTable(toTableName), row, uSession);
  }

  private void AddAllAttributesGroup(DataTable tbl)
  {
    this.cacheLock.EnterWriteLock();
    try
    {
      DataRow row = tbl.NewRow();
      row["F_GROUP_ID"] = (object) -1;
      row["F_GROUP_NAME"] = (object) Consts.AllAttributesGroupName;
      row["F_NOTE"] = (object) LocalizationHolder.rm.GetString("Kernel_660");
      row["F_AREA_ID"] = (object) DBNull.Value;
      row["F_LANGUAGE_ID"] = (object) DBNull.Value;
      row["F_GUID"] = (object) "cad00341-306c-11d8-b4e9-00304f19f545";
      if (tbl.Columns.IndexOf("F_PARENT_ID") >= 0)
        row["F_PARENT_ID"] = (object) 0;
      tbl.Rows.Add(row);
    }
    finally
    {
      this.cacheLock.ExitWriteLock();
    }
  }

  private void AddSystemAttribute(
    DataTable tbl,
    ObligatoryObjectAttributes attribute,
    string note,
    string guidStr,
    UniqueValueModes unique)
  {
    DataRow row = tbl.NewRow();
    row["F_ATTRIBUTE_ID"] = (object) Convert.ToInt32((object) attribute);
    row["F_NAME"] = (object) ObligatoryObjectAttributesHelper.GetCaption(attribute);
    row["F_SHORT_NAME"] = (object) DBNull.Value;
    row["F_ALIAS"] = (object) DBNull.Value;
    row["F_NOTE"] = (object) note;
    row["F_ATTRIBUTE_TYPE"] = (object) Convert.ToInt32((object) FieldTypes.ftSystem);
    row["F_DEFAULT_VALUE"] = (object) DBNull.Value;
    row["F_MULTIPLE_VALUED"] = (object) Convert.ToInt32((object) MultiValueModes.SingleValue);
    row["F_COMPUTED"] = (object) Convert.ToInt32((object) ComputeValueModes.StoredValue);
    row["F_SIZE_TYPE"] = (object) 0;
    row["F_FORMULA"] = (object) DBNull.Value;
    row["F_GUID"] = (object) new Guid(guidStr);
    row["F_AREA_ID"] = (object) DBNull.Value;
    row["F_UNIQUE"] = (object) Convert.ToInt32((object) unique);
    row["F_LANGUAGE_ID"] = (object) DBNull.Value;
    row["F_LEVEL_ID"] = (object) 0;
    row["F_CONTENT"] = (object) 0;
    row["F_MASTER_ID"] = (object) 0;
    row["F_SOURCE_ID"] = (object) 0;
    row["F_OPTIONS"] = (object) Convert.ToInt32((object) AttributeOptions.None);
    row["F_INVIEW"] = attribute != ObligatoryObjectAttributes.F_MODIFY_DATE ? (object) 2 : (object) 0;
    tbl.Rows.Add(row);
  }

  public int GetObjectTypeParentID(int objectTypeID)
  {
    return this.ObjecTypeParents.ContainsKey(objectTypeID) ? this.ObjecTypeParents[objectTypeID] : -1;
  }

  public virtual bool IsInhertitedFrom(int childTypeID, int parTypeID)
  {
    if (childTypeID == parTypeID)
      return true;
    for (int key = this.ObjecTypeParents.ContainsKey(childTypeID) ? this.ObjecTypeParents[childTypeID] : -1; key != -1; key = this.ObjecTypeParents.ContainsKey(key) ? this.ObjecTypeParents[key] : -1)
    {
      if (key == parTypeID)
        return true;
    }
    return false;
  }

  public bool IsProduct(int objType)
  {
    return CacheDataset._productTypeID > 0 && this.IsInhertitedFrom(objType, CacheDataset._productTypeID);
  }

  public bool IsArticle(int objType) => this.IsInhertitedFrom(objType, CacheDataset._artTypeID);

  public bool IsDocument(int objType) => this.IsInhertitedFrom(objType, CacheDataset._docTypeID);

  public bool IsSpecification(int objType)
  {
    return this.IsInhertitedFrom(objType, CacheDataset._spcTypeID);
  }

  private void FillObjectTypesID(DataTable objTypes)
  {
    if (CacheDataset._artTypeID != -1)
      return;
    DataRow[] dataRowArray1 = objTypes.Select($"F_GUID = {SqlHelper.QString("cad00268-306c-11d8-b4e9-00304f19f545")}");
    if (dataRowArray1 != null && dataRowArray1.Length != 0)
      CacheDataset._artTypeID = Convert.ToInt32(dataRowArray1[0]["F_OBJECT_TYPE"]);
    DataRow[] dataRowArray2 = objTypes.Select($"F_GUID = {SqlHelper.QString("cad00070-306c-11d8-b4e9-00304f19f545")}");
    if (dataRowArray2 != null && dataRowArray2.Length != 0)
      CacheDataset._docTypeID = Convert.ToInt32(dataRowArray2[0]["F_OBJECT_TYPE"]);
    DataRow[] dataRowArray3 = objTypes.Select($"F_GUID = {SqlHelper.QString("cad00133-306c-11d8-b4e9-00304f19f545")}");
    if (dataRowArray3 != null && dataRowArray3.Length != 0)
      CacheDataset._spcTypeID = Convert.ToInt32(dataRowArray3[0]["F_OBJECT_TYPE"]);
    DataRow[] dataRowArray4 = objTypes.Select($"F_GUID = {SqlHelper.QString("cadd9a56-306c-11d8-b4e9-00304f19f545")}");
    if (dataRowArray4 == null || dataRowArray4.Length == 0)
      return;
    CacheDataset._productTypeID = Convert.ToInt32(dataRowArray4[0]["F_OBJECT_TYPE"]);
  }

  public int ProductTypeID => CacheDataset._productTypeID;

  public int ArticleTypeID => CacheDataset._artTypeID;

  public int DocumentTypeID => CacheDataset._docTypeID;

  private void FillObjectTypeParents(DataTable table, IDbManager db)
  {
    this.ObjecTypeParents.Clear();
    int count = table.Rows.Count;
    for (int index = 0; index < count; ++index)
    {
      DataRow row = table.Rows[index];
      this.ObjecTypeParents.TryAdd(Convert.ToInt32(row["F_OBJECT_TYPE"]), Convert.ToInt32(row["F_PARENT_ID"]));
    }
  }

  public Guid GetObjectTypeGuid(int objectTypeID, bool throwIfNotFound)
  {
    object objectTypeGuid = this.ObjecTypeGUIDs[(object) objectTypeID];
    if (objectTypeGuid == null)
    {
      this.EnterReadLocker();
      try
      {
        DataRow[] dataRowArray = this.GetTable("IMS_OBJECT_TYPES").Select("F_OBJECT_TYPE = " + objectTypeID.ToString());
        if (dataRowArray.Length == 0)
        {
          if (throwIfNotFound)
            throw new KernelException(string.Format(LocalizationHolder.rm.GetString(sc_12972.ssp_appserver_12977()), (object) objectTypeID));
          objectTypeGuid = (object) Guid.Empty;
        }
        else
        {
          objectTypeGuid = (object) new Guid(dataRowArray[0]["F_GUID"].ToString());
          this.ObjecTypeGUIDs[(object) objectTypeID] = objectTypeGuid;
        }
      }
      finally
      {
        this.ExitReadLocker();
      }
    }
    return (Guid) objectTypeGuid;
  }

  private void FillObjectTypeGuids(DataTable table)
  {
    lock (this.ObjecTypeGUIDs)
    {
      this.ObjecTypeGUIDs.Clear();
      foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
        this.ObjecTypeGUIDs.Add((object) Convert.ToInt32(row["F_OBJECT_TYPE"]), (object) new Guid(row["F_GUID"].ToString()));
    }
  }

  internal void FillCaptionAttributes(DataTable table)
  {
    lock (this.CaptionAttributes)
    {
      this.CaptionAttributes.Clear();
      int columnIndex1 = table.Columns.IndexOf("F_CAPTION_ATTRIBUTE");
      int columnIndex2 = table.Columns.IndexOf("F_OBJECT_TYPE");
      foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
      {
        int int32_1 = Convert.ToInt32(row[columnIndex1]);
        if (int32_1 > 0)
        {
          int int32_2 = Convert.ToInt32(row[columnIndex2]);
          object captionAttribute = this.CaptionAttributes[(object) int32_1];
          if (captionAttribute == null)
            this.CaptionAttributes[(object) int32_1] = (object) new ArrayList()
            {
              (object) int32_2
            };
          else
            (captionAttribute as ArrayList).Add((object) int32_2);
        }
      }
      foreach (ArrayList arrayList in (IEnumerable) this.CaptionAttributes.Values)
        arrayList.Sort();
    }
  }

  private void FillAttributeIDHash(DataTable tbl)
  {
    int columnIndex1 = tbl.Columns.IndexOf("F_INVIEW");
    int columnIndex2 = tbl.Columns.IndexOf("F_ATTRIBUTE_ID");
    int columnIndex3 = tbl.Columns.IndexOf("F_OPTIONS");
    foreach (DataRow row in (InternalDataCollectionBase) tbl.Rows)
    {
      OptimizationModes int32 = (OptimizationModes) Convert.ToInt32(row[columnIndex1]);
      this.AttributesInViewsHash[(object) new Attribute4ID(Convert.ToInt32(row[columnIndex2]))] = (object) new Attribute4Props(int32, (AttributeOptions) Convert.ToInt32(row[columnIndex3]));
    }
  }

  internal void FillAttributeID4RelationHash(DataTable tbl, IDbManager db)
  {
    this.cacheLock.EnterReadLock();
    try
    {
      int columnIndex1 = tbl.Columns.IndexOf("F_INVIEW");
      int columnIndex2 = tbl.Columns.IndexOf("F_ATTRIBUTE_ID");
      int columnIndex3 = tbl.Columns.IndexOf("F_OPTIONS");
      Hashtable hashtable = new Hashtable();
      foreach (DataRow row in (InternalDataCollectionBase) tbl.Rows)
      {
        string[] tables = (string[]) null;
        OptimizationModes int32 = (OptimizationModes) Convert.ToInt32(row[columnIndex1]);
        switch (int32)
        {
          case OptimizationModes.Read:
          case OptimizationModes.Seek:
            tables = new string[1]
            {
              "IMV_R" + row["F_RELATION_TYPE"].ToString()
            };
            hashtable[(object) Convert.ToInt32(row["F_RELATION_TYPE"])] = (object) true;
            break;
        }
        this.AttributesInViewsHash[(object) new Attribute4ID(Convert.ToInt32(row[columnIndex2]), -1, Convert.ToInt32(row["F_RELATION_TYPE"]))] = (object) new Attribute4Props(int32, tables, (AttributeOptions) Convert.ToInt32(row[columnIndex3]));
      }
      DataTable dataTable = db.ExecuteDataTable("SELECT * FROM IMS_RELATION_TYPES");
      dataTable.Columns.IndexOf("F_RELATION_TYPE");
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        int int32 = Convert.ToInt32(row["F_RELATION_TYPE"]);
        if (hashtable[(object) int32] != null)
          this.AttributesInViewsHash[(object) new Attribute4ID(-1, -1, int32)] = (object) new Attribute4Props(OptimizationModes.Seek, new string[1]
          {
            "IMV_R" + int32.ToString()
          }, AttributeOptions.None);
        else
          this.AttributesInViewsHash[(object) new Attribute4ID(-1, -1, int32)] = (object) new Attribute4Props(OptimizationModes.Seek, (string[]) null, AttributeOptions.None);
      }
    }
    finally
    {
      this.cacheLock.ExitReadLock();
    }
  }

  private void FillDecodingAttributesCache(int objectTypeID, int attrID)
  {
    int[] numArray1;
    if (this.ObjecTypeDecodingAttributes.TryGetValue(objectTypeID, out numArray1))
    {
      int[] numArray2 = new int[numArray1.Length + 1];
      numArray1.CopyTo((Array) numArray2, 0);
      numArray2[numArray1.Length] = attrID;
      this.ObjecTypeDecodingAttributes[objectTypeID] = numArray2;
    }
    else
      this.ObjecTypeDecodingAttributes.TryAdd(objectTypeID, new int[1]
      {
        attrID
      });
  }

  public int[] GetDecodingAttributes(int objectTypeID)
  {
    int[] numArray;
    return this.ObjecTypeDecodingAttributes.TryGetValue(objectTypeID, out numArray) ? numArray : (int[]) null;
  }

  internal void FillAttributeID4ObjectHash(DataTable tbl, IDbManager db)
  {
    this.cacheLock.EnterReadLock();
    try
    {
      Hashtable hashtable1 = Hashtable.Synchronized(new Hashtable());
      int columnIndex1 = tbl.Columns.IndexOf("F_INVIEW");
      int columnIndex2 = tbl.Columns.IndexOf("F_ATTRIBUTE_ID");
      int columnIndex3 = tbl.Columns.IndexOf("F_OBJECT_TYPE");
      int columnIndex4 = tbl.Columns.IndexOf("F_OPTIONS");
      DataTable dataTable1 = db.ExecuteDataTable("SELECT * FROM IMS_ATTRIBUTES");
      DataColumn[] dataColumnArray = new DataColumn[1]
      {
        dataTable1.Columns["F_ATTRIBUTE_ID"]
      };
      dataTable1.PrimaryKey = dataColumnArray;
      DataTable dataTable2 = db.ExecuteDataTable("SELECT F_OBJECT_TYPE, F_OPTIONS FROM IMS_OBJECT_TYPES");
      Hashtable hashtable2 = new Hashtable(dataTable2.Rows.Count);
      for (int index = 0; index < dataTable2.Rows.Count; ++index)
        hashtable2.Add((object) Convert.ToInt32(dataTable2.Rows[index][0]), (object) Convert.ToInt32(dataTable2.Rows[index][1]));
      List<string> stringList = new List<string>();
      Hashtable hashtable3 = new Hashtable();
      this.ObjecTypeDecodingAttributes.Clear();
      foreach (DataRow row in (InternalDataCollectionBase) tbl.Rows)
      {
        AttributeOptions int32_1 = (AttributeOptions) Convert.ToInt32(row[columnIndex4]);
        int int32_2 = Convert.ToInt32(row[columnIndex3]);
        int int32_3 = Convert.ToInt32(row[columnIndex2]);
        if ((int32_1 & AttributeOptions.GetDescriptionEvent) == AttributeOptions.GetDescriptionEvent)
          this.FillDecodingAttributesCache(int32_2, int32_3);
        stringList.Clear();
        OptimizationModes int32_4 = (OptimizationModes) Convert.ToInt32(row[columnIndex1]);
        switch (int32_4)
        {
          case OptimizationModes.Read:
          case OptimizationModes.Seek:
            stringList.Add("IMV_O" + int32_2.ToString());
            hashtable3[(object) int32_2] = (object) true;
            break;
        }
        for (int objectTypeParentId = this.GetObjectTypeParentID(int32_2); objectTypeParentId > -1; objectTypeParentId = this.GetObjectTypeParentID(objectTypeParentId))
        {
          DataRow dataRow = tbl.Rows.Find(new object[2]
          {
            (object) int32_3,
            (object) objectTypeParentId
          });
          if (dataRow != null && (Convert.ToInt32(dataRow[columnIndex1]) == 1 || Convert.ToInt32(dataRow[columnIndex1]) == 2) && ((ObjectTypeOptions) hashtable2[(object) objectTypeParentId] & ObjectTypeOptions.LocalObjectType) == ObjectTypeOptions.None)
            stringList.Add("IMV_O" + objectTypeParentId.ToString());
        }
        DataRow dataRow1 = dataTable1.Rows.Find((object) int32_3);
        if (Convert.ToInt32(dataRow1["F_INVIEW"]) == 1 || Convert.ToInt32(dataRow1["F_INVIEW"]) == 2)
          stringList.Add("IMS_OBJECTS_VIEW");
        string[] array = stringList.Count != 0 ? stringList.ToArray() : (string[]) null;
        hashtable1[(object) new Attribute4ID(int32_3, int32_2, -1)] = (object) new Attribute4Props(int32_4, array, int32_1);
      }
      for (int index = 0; index < dataTable2.Rows.Count; ++index)
      {
        int int32 = Convert.ToInt32(dataTable2.Rows[index][0]);
        stringList.Clear();
        if (hashtable3[(object) int32] != null)
          stringList.Add("IMV_O" + int32.ToString());
        else if (((ObjectTypeOptions) hashtable2[(object) int32] & ObjectTypeOptions.LocalObjectType) == ObjectTypeOptions.LocalObjectType)
          stringList.Add("IMV_O" + int32.ToString());
        for (int objectTypeParentId = this.GetObjectTypeParentID(int32); objectTypeParentId > -1; objectTypeParentId = this.GetObjectTypeParentID(objectTypeParentId))
        {
          if (hashtable3[(object) objectTypeParentId] != null && ((ObjectTypeOptions) hashtable2[(object) objectTypeParentId] & ObjectTypeOptions.LocalObjectType) == ObjectTypeOptions.None)
            stringList.Add("IMV_O" + objectTypeParentId.ToString());
        }
        stringList.Add("IMS_OBJECTS_VIEW");
        string[] array = stringList.ToArray();
        hashtable1[(object) new Attribute4ID(-1, int32, -1)] = (object) new Attribute4Props(OptimizationModes.Seek, array, AttributeOptions.None);
      }
      this.AttributesInViewsHash = hashtable1;
      DataTable table = this._DBSet.Tables["IMS_ATTR4RELATION_TYPES"];
      if (table == null)
        return;
      this.FillAttributeID4RelationHash(table, db);
    }
    finally
    {
      this.cacheLock.ExitReadLock();
    }
  }

  private string GetLoadTableSQL(string tableName)
  {
    string loadTableSql;
    switch (tableName)
    {
      case "IMS_ATTR4RELATION_TYPES":
        loadTableSql = sc_12972.ssp_appserver_12978();
        break;
      case "IMS_ATTR4OBJ_TYPES":
        loadTableSql = sc_12972.ssp_appserver_12979();
        break;
      default:
        loadTableSql = "SELECT * FROM " + tableName;
        break;
    }
    return loadTableSql;
  }

  public void ReloadTables(IUserSession uSession, IDbManager db, params string[] tablesList)
  {
    foreach (string tables in tablesList)
    {
      DataTable dataTable = db.ExecuteDataTable(this.GetLoadTableSQL(tables));
      dataTable.TableName = tables;
      object primaryKey = this.PrimaryKeys[(object) tables];
      if (primaryKey != null)
      {
        string[] strArray = (string[]) primaryKey;
        DataColumn[] dataColumnArray = new DataColumn[strArray.Length];
        for (int index = 0; index < strArray.Length; ++index)
          dataColumnArray[index] = dataTable.Columns[strArray[index]];
        dataTable.PrimaryKey = dataColumnArray;
      }
      this.cacheLock.EnterWriteLock();
      try
      {
        switch (tables)
        {
          case "IMS_FORMULA_ATTRS":
            this.RebuildFormulasCache(dataTable);
            break;
          case "IMS_ATTR_GROUPS":
            this.AddAllAttributesGroup(dataTable);
            break;
          case "IMS_OBJECT_TYPES":
            this.FillObjectTypeGuids(dataTable);
            this.FillCaptionAttributes(dataTable);
            this.FillObjectTypesID(dataTable);
            break;
          case "IMS_ATTR4RELATION_TYPES":
            this.FillAttributeID4RelationHash(dataTable, db);
            break;
          case "IMS_ATTR4OBJ_TYPES":
            this.FillAttributeID4ObjectHash(dataTable, db);
            break;
          case "IMS_OBJTYPES_TREE":
            this.FillObjectTypeParents(dataTable, db);
            break;
          case "IMS_ATTR_IN_GROUPS":
            this.FillAttrGroupNames(dataTable);
            break;
          case "IMS_POSSIBLE_VALUES":
            this.FillAccessLevels(dataTable);
            break;
          case "IMS_LANGUAGES":
            IEnumerator enumerator = dataTable.Rows.GetEnumerator();
            try
            {
              while (enumerator.MoveNext())
              {
                DataRow current = (DataRow) enumerator.Current;
                if (current["F_DEFAULT"].ToString() != string.Empty && current["F_DEFAULT"].ToString() != "0")
                  DBLanguageCollection.DefaultLanguage = current["F_LANGUAGE_ID"].ToString();
              }
              break;
            }
            finally
            {
              if (enumerator is IDisposable disposable)
                disposable.Dispose();
            }
          case "IMS_ATTRIBUTES":
            this.FillAttributeIDHash(dataTable);
            this.AddSystemAttribute(dataTable, ObligatoryObjectAttributes.F_VERSION_RESULT, LocalizationHolder.rm.GetString("Kernel_704"), "cad001f0-306c-11d8-b4e9-00304f19f545", UniqueValueModes.NotUnique);
            this.AddSystemAttribute(dataTable, ObligatoryObjectAttributes.F_ELEMENT_STATUSES, LocalizationHolder.rm.GetString("Kernel_705"), "cad005f1-306c-11d8-b4e9-00304f19f545", UniqueValueModes.NotUnique);
            this.AddSystemAttribute(dataTable, ObligatoryObjectAttributes.F_ACTUAL_DATE, LocalizationHolder.rm.GetString("Kernel_706"), "cad0080f-306c-11d8-b4e9-00304f19f545", UniqueValueModes.NotUnique);
            this.AddSystemAttribute(dataTable, ObligatoryObjectAttributes.F_PARENT_OBJECT_ID, "Виртуальный атрибут, который содержит идентификатор версии объекта, на основе которой была создана данная версия объекта.", "cadd9717-306c-11d8-b4e9-00304f19f545", UniqueValueModes.NotUnique);
            this.AddSystemAttribute(dataTable, ObligatoryObjectAttributes.F_VERSIONS_COUNT, "Виртуальный атрибут, который отображает текущее количество версий объекта в базе данных.", "cadd98e9-306c-11d8-b4e9-00304f19f545", UniqueValueModes.NotUnique);
            this.AddSystemAttribute(dataTable, ObligatoryObjectAttributes.F_REFERENCE_COUNT, "Виртуальный атрибут, который содержит текущее количество ссылок на данную версию объекта из ссылочных атрибутов объектов (включает рабочие копии объектов).", "cadd98ed-306c-11d8-b4e9-00304f19f545", UniqueValueModes.NotUnique);
            this.AddSystemAttribute(dataTable, ObligatoryObjectAttributes.F_RELATIONS_COUNT, "Виртуальный атрибут, который содержит текущее количество версий объектов, в которые данный объект входит связями любого типа (включает рабочие копии объектов).", "cadd98ee-306c-11d8-b4e9-00304f19f545", UniqueValueModes.NotUnique);
            this.AddSystemAttribute(dataTable, ObligatoryObjectAttributes.F_LCSTEP_DATE, "Виртуальный атрибут, который отображает дату и время перевода версии объекта на текущий шаг ЖЦ.", "cadd9972-306c-11d8-b4e9-00304f19f545", UniqueValueModes.NotUnique);
            break;
        }
        if (this._DBSet.Tables[tables] != null)
          this._DBSet.Tables.Remove(tables);
        this._DBSet.Tables.Add(dataTable);
      }
      finally
      {
        this.cacheLock.ExitWriteLock();
      }
      this.RefreshModifyDate(tables, db);
    }
    CacheDataset.UpdateMetaDataHelper(uSession, tablesList.Length == 1 ? tablesList[0] : string.Empty, MetaDataHelperServiceUpdateTask.MetaDataGeneration);
  }

  private void FillAccessLevels(DataTable Table1)
  {
    DataRow[] dataRowArray1 = this.GetTable("IMS_ATTRIBUTES").Select("F_GUID = " + SqlHelper.QString("cad00816-306c-11d8-b4e9-00304f19f545"));
    if (dataRowArray1.Length == 0)
      return;
    DataRow[] dataRowArray2 = Table1.Select($"F_ATTRIBUTE_ID = {dataRowArray1[0]["F_ATTRIBUTE_ID"]} AND F_OBJECT_TYPE = -1 AND F_RELATION_TYPE = -1");
    this._AccessLevels.Clear();
    for (int index = 0; index < dataRowArray2.Length; ++index)
      this._AccessLevels.TryAdd(Convert.ToInt32(dataRowArray2[index]["F_INTEGER_VALUE"]), dataRowArray2[index]["F_DESCRIPTION"].ToString());
  }

  public string GetAccessCaption(int accessLevel)
  {
    string str;
    return this._AccessLevels.TryGetValue(accessLevel, out str) ? str : "Неизвестый уровень доступа N" + accessLevel.ToString();
  }

  public bool AccessLevelExists(int accessLevel) => this._AccessLevels.ContainsKey(accessLevel);

  internal void FillAttrGroupNames(DataTable Table1)
  {
    this._AttrGroupNames.Clear();
    int columnIndex1 = Table1.Columns.IndexOf("F_ATTRIBUTE_ID");
    int columnIndex2 = Table1.Columns.IndexOf("F_GROUP_ID");
    for (int index = 0; index < Table1.Rows.Count; ++index)
      this._AttrGroupNames.GetOrAdd(Convert.ToInt32(Table1.Rows[index][columnIndex1]), Convert.ToInt32(Table1.Rows[index][columnIndex2]));
  }

  public DataTable GetTable(string tableName)
  {
    this.cacheLock.EnterReadLock();
    try
    {
      return this._DBSet.Tables[tableName] ?? throw new KernelException($"Table {tableName} not found in metadata cache.");
    }
    finally
    {
      this.cacheLock.ExitReadLock();
    }
  }

  public int DeleteRecords(string tableName, string condition, IUserSession uSession)
  {
    int num = 0;
    bool flag = false;
    this.cacheLock.EnterWriteLock();
    DataTable table;
    DataTable dataTable;
    try
    {
      table = this._DBSet.Tables[tableName];
      dataTable = table.Clone();
      DataRow[] fromRows = table.Select(condition);
      if (fromRows.Length != 0)
        DataSetProcessor.AssignRows(dataTable, (IEnumerable<DataRow>) fromRows);
      foreach (DataRow row in fromRows)
      {
        table.Rows.Remove(row);
        flag = true;
        ++num;
      }
      this.RefreshModifyDate(tableName, (uSession as UserSession).DataManager);
    }
    finally
    {
      this.cacheLock.ExitWriteLock();
    }
    if (uSession != null & flag)
    {
      (uSession as UserSession).AddModifiedCacheTable(tableName);
      if (tableName == "IMS_FORMULA_ATTRS")
        this.RebuildFormulasCache(table);
      CacheDataset.UpdateMetaDataHelper(uSession, tableName, MetaDataHelperServiceUpdateTask.MetaDataIncGeneration);
      this.OnTableChanged((TableChangedEventArgs) new TableValueDeletedArgs(uSession, tableName, dataTable));
    }
    return num;
  }

  public bool ReloadOldTables(IDbManager db)
  {
    bool flag = false;
    DataTable table1 = db.ExecuteDataTable(sc_12972.ssp_appserver_12980());
    table1.TableName = "IMS_METADATA";
    table1.PrimaryKey = new DataColumn[1]
    {
      table1.Columns[0]
    };
    DataTable table2 = this.GetTable("IMS_METADATA");
    for (int index = 0; index < this.TablesNameList.Length; ++index)
    {
      DataRow dataRow1 = table1.Rows.Find((object) this.TablesNameList[index]);
      if (dataRow1 != null)
      {
        DataRow dataRow2 = table2.Rows.Find(dataRow1[0]);
        if (dataRow2 != null)
        {
          if (Convert.ToDateTime(dataRow1[1]) != Convert.ToDateTime(dataRow2["F_MODIFY_DATE"]))
          {
            flag = true;
            this.ReloadTables((IUserSession) null, db, new string[1]
            {
              dataRow1[0].ToString()
            });
          }
        }
        else
        {
          flag = true;
          this.ReloadTables((IUserSession) null, db, new string[1]
          {
            dataRow1[0].ToString()
          });
        }
      }
    }
    if (flag)
    {
      this.cacheLock.EnterWriteLock();
      try
      {
        this._DBSet.Tables.Remove("IMS_METADATA");
        this._DBSet.Tables.Add(table1);
      }
      finally
      {
        this.cacheLock.ExitWriteLock();
      }
    }
    return flag;
  }

  public List<string> GetObjectAttrsTables()
  {
    List<string> objectAttrsTables = new List<string>();
    objectAttrsTables.Add("IMS_OBJECT_ATTRS");
    DataTable table = this.GetTable("IMS_OBJECT_TYPES");
    this.EnterReadLocker();
    try
    {
      int columnIndex1 = table.Columns.IndexOf("F_OPTIONS");
      int columnIndex2 = table.Columns.IndexOf("F_OBJECT_TYPE");
      for (int index = 0; index < table.Rows.Count; ++index)
      {
        if ((Convert.ToInt32(table.Rows[index][columnIndex1]) & 16 /*0x10*/) == 16 /*0x10*/)
          objectAttrsTables.Add("IMV_A" + Convert.ToString(table.Rows[index][columnIndex2]));
      }
    }
    finally
    {
      this.ExitReadLocker();
    }
    return objectAttrsTables;
  }

  public string GetAttributesTableName(int objectTypeID)
  {
    DataRow dataRow = this.GetTable("IMS_OBJECT_TYPES").Rows.Find((object) objectTypeID);
    if (dataRow == null)
      throw new KernelException("Неизвестный идентификатор типа объектов: " + objectTypeID.ToString());
    return (Convert.ToInt32(dataRow["F_OPTIONS"]) & 16 /*0x10*/) == 16 /*0x10*/ ? "IMV_A" + objectTypeID.ToString() : "IMS_OBJECT_ATTRS";
  }

  private void AddFilePrototype(DataRow row, int atID, int otID, bool isPersonalPrototype)
  {
    object obj = !isPersonalPrototype ? this._FilePrototypes[(object) new FilePrototypeID(atID, otID)] : this._FilePrototypes[(object) new FilePrototypeID(atID, otID, Convert.ToInt64(row[2]))];
    long int64 = Convert.ToInt64(row[0]);
    long[] numArray1;
    if (obj == null)
    {
      numArray1 = new long[1]{ int64 };
    }
    else
    {
      numArray1 = (long[]) obj;
      bool flag = false;
      for (int index = 0; index < numArray1.Length; ++index)
      {
        if (numArray1[index] == int64)
        {
          flag = true;
          break;
        }
      }
      if (!flag)
      {
        long[] numArray2 = new long[numArray1.Length + 1];
        numArray1.CopyTo((Array) numArray2, 0);
        numArray2[numArray1.Length] = int64;
        numArray1 = numArray2;
      }
    }
    if (isPersonalPrototype)
      this._FilePrototypes[(object) new FilePrototypeID(atID, otID, Convert.ToInt64(row[2]))] = (object) numArray1;
    else
      this._FilePrototypes[(object) new FilePrototypeID(atID, otID)] = (object) numArray1;
  }

  public void LoadFilePrototypes(IUserSession session, int objectTypeID)
  {
    try
    {
      bool showPersonalObjects = session.ShowPersonalObjects;
      session.ShowPersonalObjects = true;
      lock (this._FilePrototypes)
      {
        try
        {
          if (objectTypeID == -1)
            this._FilePrototypes.Clear();
          DataTable dataTable = session.GetObjectCollection(session.IdentHelper.GetObjectTypeID("cad00342-306c-11d8-b4e9-00304f19f545")).Select(new DBRecordSetParams((ConditionStructure[]) null, new object[3]
          {
            (object) -2,
            (object) -7,
            (object) -8
          }));
          int attributeId1 = session.IdentHelper.GetAttributeID("cad00149-306c-11d8-b4e9-00304f19f545");
          int attributeId2 = session.IdentHelper.GetAttributeID("cad001d0-306c-11d8-b4e9-00304f19f545");
          int objectTypeId = session.IdentHelper.GetObjectTypeID("cad00347-306c-11d8-b4e9-00304f19f545");
          DataTable table1 = this.GetTable("IMS_OBJECT_TYPES");
          DataTable table2 = this.GetTable("IMS_ATTRIBUTES");
          this.EnterReadLocker();
          try
          {
            foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
            {
              IDBObject dbObject = session.GetObject(Convert.ToInt64(row[0]));
              IDBAttribute attributeById = dbObject.GetAttributeByID(attributeId1);
              if (attributeById != null)
              {
                for (int index1 = 0; index1 < attributeById.ValuesCount; ++index1)
                {
                  attributeById.Index = index1;
                  if (attributeById.AsString != string.Empty)
                  {
                    DataRow[] dataRowArray1 = table1.Select("F_GUID = " + SqlHelper.QString(attributeById.AsString));
                    if (dataRowArray1.Length != 0)
                    {
                      int int32 = Convert.ToInt32(dataRowArray1[0]["F_OBJECT_TYPE"]);
                      if (objectTypeID == -1 || objectTypeID != int32)
                      {
                        int atID = session.IdentHelper.FileAttributeID;
                        IDBAttribute byId = dbObject.Attributes.FindByID(attributeId2);
                        if (byId != null)
                        {
                          for (int index2 = 0; index2 < byId.ValuesCount; ++index2)
                          {
                            byId.Index = index2;
                            if (byId.AsString != string.Empty)
                            {
                              DataRow[] dataRowArray2 = table2.Select("F_GUID = " + SqlHelper.QString(byId.AsString));
                              if (dataRowArray2.Length != 0)
                              {
                                atID = Convert.ToInt32(dataRowArray2[0]["F_ATTRIBUTE_ID"]);
                                this.AddFilePrototype(row, atID, int32, Convert.ToInt32(row[1]) == objectTypeId);
                              }
                            }
                            else
                              this.AddFilePrototype(row, atID, int32, Convert.ToInt32(row[1]) == objectTypeId);
                          }
                        }
                        else
                          this.AddFilePrototype(row, atID, int32, Convert.ToInt32(row[1]) == objectTypeId);
                      }
                    }
                  }
                }
              }
            }
          }
          finally
          {
            this.ExitReadLocker();
          }
        }
        finally
        {
          session.ShowPersonalObjects = showPersonalObjects;
        }
      }
    }
    catch (Exception ex)
    {
      (session.EventLog as EventLog)._EventLogHelper.AddEvent(0L, 0L, 14, 0L, LocalizationHolder.rm.GetString("Kernel_707"), ex.Message, ActionType.Load, EventlogRecordType.Error, session.UserID, EnvironmentConsts.MachineName, session);
    }
  }

  public long[] GetFilePrototype(int attributeID, int objectTypeID, long userID)
  {
    long[] filePrototype1 = (long[]) null;
    object filePrototype2 = this._FilePrototypes[(object) new FilePrototypeID(attributeID, objectTypeID, userID)];
    if (filePrototype2 != null)
      filePrototype1 = (long[]) filePrototype2;
    else if (userID > 0L)
    {
      object filePrototype3 = this._FilePrototypes[(object) new FilePrototypeID(attributeID, objectTypeID, 0L)];
      if (filePrototype3 != null)
      {
        filePrototype1 = (long[]) filePrototype3;
      }
      else
      {
        int objectTypeParentId = this.GetObjectTypeParentID(objectTypeID);
        if (objectTypeParentId >= 0)
          filePrototype1 = this.GetFilePrototype(attributeID, objectTypeParentId, userID);
      }
    }
    return filePrototype1;
  }

  public void DeleteFilePrototype(long prototypeID)
  {
    lock (this._FilePrototypes)
    {
      IDictionaryEnumerator enumerator1 = this._FilePrototypes.GetEnumerator();
      ArrayList arrayList1 = new ArrayList();
      ListDictionary listDictionary = (ListDictionary) null;
      while (enumerator1.MoveNext())
      {
        long[] numArray = (long[]) enumerator1.Value;
        for (int index1 = 0; index1 < numArray.Length; ++index1)
        {
          if (numArray[index1] == prototypeID)
          {
            if (numArray.Length > 1)
            {
              ArrayList arrayList2 = new ArrayList();
              for (int index2 = 0; index2 < numArray.Length; ++index2)
              {
                if (index2 != index1)
                  arrayList2.Add((object) numArray[index2]);
              }
              if (listDictionary == null)
                listDictionary = new ListDictionary();
              listDictionary[enumerator1.Key] = (object) (long[]) arrayList2.ToArray(typeof (long));
              break;
            }
            arrayList1.Add(enumerator1.Key);
            break;
          }
        }
      }
      foreach (object key in arrayList1)
        this._FilePrototypes.Remove(key);
      if (listDictionary == null)
        return;
      IDictionaryEnumerator enumerator2 = listDictionary.GetEnumerator();
      while (enumerator2.MoveNext())
        this._FilePrototypes[enumerator2.Key] = enumerator2.Value;
    }
  }

  public int[] GetFormulasID(int attributeID, int typeID, int mode, bool isObject)
  {
    CacheDataset.Attribute4Formulas key = new CacheDataset.Attribute4Formulas(attributeID, typeID, mode);
    int[] numArray1;
    int[] numArray2;
    return isObject ? (this._FormulaAttributesHash4Objects.TryGetValue(key, out numArray1) || this._FormulaAttributesHash4Objects.TryGetValue(new CacheDataset.Attribute4Formulas(attributeID, -1, mode), out numArray1) ? numArray1 : this.EmptyAttrArray) : (this._FormulaAttributesHash4Relations.TryGetValue(key, out numArray2) || this._FormulaAttributesHash4Relations.TryGetValue(new CacheDataset.Attribute4Formulas(attributeID, -1, mode), out numArray2) ? numArray2 : this.EmptyAttrArray);
  }

  private void SearchFormulas(
    DataTable tbl,
    int mode,
    string mainTypeName,
    string subTypeName,
    ConcurrentDictionary<CacheDataset.Attribute4Formulas, int[]> h4)
  {
    int columnIndex1 = tbl.Columns.IndexOf(mainTypeName);
    int columnIndex2 = tbl.Columns.IndexOf("F_ATTRIBUTE_ID");
    int columnIndex3 = tbl.Columns.IndexOf("F_FORMULA_ID");
    tbl.Columns.IndexOf("F_MODE_ID");
    DataRow[] dataRowArray = tbl.Select($"{subTypeName} = -1 AND F_MODE_ID = {mode}", $"F_ATTRIBUTE_ID, {mainTypeName}");
    int typeID = -2;
    int attributeID = -10000;
    List<int> intList1 = new List<int>();
    List<int> intList2 = new List<int>();
    for (int index1 = 0; index1 < dataRowArray.Length; ++index1)
    {
      DataRow dataRow = dataRowArray[index1];
      int int32_1 = Convert.ToInt32(dataRow[columnIndex1]);
      int int32_2 = Convert.ToInt32(dataRow[columnIndex3]);
      int int32_3 = Convert.ToInt32(dataRow[columnIndex2]);
      if (int32_1 != typeID || int32_3 != attributeID)
      {
        if (attributeID > -10000)
        {
          for (int index2 = 0; index2 < intList1.Count; ++index2)
          {
            if (!intList2.Contains(intList1[index2]))
              intList2.Add(intList1[index2]);
          }
          h4.TryAdd(new CacheDataset.Attribute4Formulas(attributeID, typeID, mode), intList2.ToArray());
          intList2.Clear();
        }
        if (attributeID != int32_3)
        {
          intList1.Clear();
          attributeID = int32_3;
        }
        typeID = int32_1;
      }
      if (int32_1 == -1)
        intList1.Add(int32_2);
      else
        intList2.Add(int32_2);
    }
    if (attributeID <= -10000)
      return;
    for (int index = 0; index < intList1.Count; ++index)
    {
      if (!intList2.Contains(intList1[index]))
        intList2.Add(intList1[index]);
    }
    h4.TryAdd(new CacheDataset.Attribute4Formulas(attributeID, typeID, mode), intList2.ToArray());
  }

  public void RebuildFormulasCache(DataTable tbl)
  {
    ConcurrentDictionary<CacheDataset.Attribute4Formulas, int[]> h4_1 = new ConcurrentDictionary<CacheDataset.Attribute4Formulas, int[]>();
    ConcurrentDictionary<CacheDataset.Attribute4Formulas, int[]> h4_2 = new ConcurrentDictionary<CacheDataset.Attribute4Formulas, int[]>();
    this.SearchFormulas(tbl, Consts.Attribute4Formula, "F_OBJECT_TYPE", "F_RELATION_TYPE", h4_1);
    this.SearchFormulas(tbl, Consts.Attribute4ValidationRule, "F_OBJECT_TYPE", "F_RELATION_TYPE", h4_1);
    this.SearchFormulas(tbl, Consts.Attribute4Formula, "F_RELATION_TYPE", "F_OBJECT_TYPE", h4_2);
    this.SearchFormulas(tbl, Consts.Attribute4ValidationRule, "F_RELATION_TYPE", "F_OBJECT_TYPE", h4_2);
    this._FormulaAttributesHash4Objects = h4_1;
    this._FormulaAttributesHash4Relations = h4_2;
  }

  public string GetAttributeGroupName(int attrID)
  {
    int attrGroupID;
    return this._AttrGroupNames.TryGetValue(attrID, out attrGroupID) ? MetaDataHelper.GetAttributeGroup(attrGroupID).Name : string.Empty;
  }

  public void InitPossibleValuesCache(IUserSession session)
  {
    if (this.pvCache != null)
      return;
    this.pvCache = (IPossibleValuesCache) new PossibleValuesCache(this.GetTable("IMS_POSSIBLE_VALUES"), session);
  }

  public string GetDescription(int attrID, object val)
  {
    return this.pvCache == null ? string.Empty : this.pvCache.GetDescription(attrID, val);
  }

  public void ReloadPossibleValuesCache(IUserSession session)
  {
    this.InitPossibleValuesCache(session);
    this.pvCache.ReloadCache((session as UserSession).DataManager.ExecuteDataTable("SELECT * FROM IMS_POSSIBLE_VALUES WHERE F_DESCRIPTION IS NOT NULL"), session);
  }

  public void ClearUsersCache() => this._Users = (List<Tuple<long, Guid, string>>) null;

  private List<Tuple<long, Guid, string>> LoadUsersCache()
  {
    IUserSession sessionTemporaryClone = (ServerServices.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents).GetSystemSessionTemporaryClone(nameof (LoadUsersCache));
    List<Tuple<long, Guid, string>> tupleList;
    try
    {
      DataTable dataTable = sessionTemporaryClone.GetObjectCollection(sessionTemporaryClone.IdentHelper.UsersTypeID).Select(new DBRecordSetParams((ConditionStructure[]) null, new object[3]
      {
        (object) -2,
        (object) -12,
        (object) -50
      })
      {
        RecordCount = -1
      });
      tupleList = new List<Tuple<long, Guid, string>>(dataTable.Rows.Count);
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        tupleList.Add(new Tuple<long, Guid, string>(Convert.ToInt64(row[0]), new Guid(row[1].ToString()), row[2].ToString()));
    }
    finally
    {
      sessionTemporaryClone.Logout(nameof (LoadUsersCache));
    }
    return tupleList;
  }

  public Tuple<long, Guid, string>[] GetUsersCache()
  {
    List<Tuple<long, Guid, string>> tupleList = this._Users;
    if (tupleList == null)
    {
      tupleList = this.LoadUsersCache();
      this._Users = tupleList;
    }
    return tupleList.ToArray();
  }

  public void AddUserToCache(IDBObject userObject)
  {
    this._Users?.Add(new Tuple<long, Guid, string>(userObject.ObjectID, userObject.ObjectGUID, userObject.Caption));
  }

  private IDbManager DataManager
  {
    get
    {
      if (this._dbManager == null)
        this._dbManager = (ServerServices.GetService(typeof (IDbManagerService)) as IDbManagerService).CreateDbManager();
      return this._dbManager;
    }
  }

  public string GetObjectCaption(long objectID)
  {
    return this.GetObjectInfo(this.DataManager, objectID).Caption;
  }

  public string GetObjectCaption(Guid objectGuid)
  {
    return this.GetObjectInfo(this.DataManager, objectGuid).Caption;
  }

  public QuickObjectInfo GetObjectInfo(long objectID)
  {
    return this.GetObjectInfo(this.DataManager, objectID);
  }

  public QuickObjectInfo GetObjectInfo(Guid objectGuid)
  {
    return this.GetObjectInfo(this.DataManager, objectGuid);
  }

  public string GetObjectCaptionByID(long ID) => throw new NotImplementedException();

  public QuickObjectInfo GetObjectInfoByID(long ID) => throw new NotImplementedException();

  public void Reset() => throw new NotImplementedException();

  private class Attribute4Formulas
  {
    public int AttributeID;
    public int TypeID;
    public int Mode;

    public Attribute4Formulas(int attributeID, int typeID, int mode)
    {
      this.AttributeID = attributeID;
      this.TypeID = typeID;
      this.Mode = mode;
    }

    public override int GetHashCode() => this.AttributeID << 15 ^ this.TypeID ^ this.Mode;

    public override bool Equals(object obj)
    {
      if (!(obj is CacheDataset.Attribute4Formulas))
        return false;
      CacheDataset.Attribute4Formulas attribute4Formulas = (CacheDataset.Attribute4Formulas) obj;
      return attribute4Formulas.GetHashCode() == this.GetHashCode() && attribute4Formulas.AttributeID == this.AttributeID && attribute4Formulas.TypeID == this.TypeID && attribute4Formulas.Mode == this.Mode;
    }
  }
}
