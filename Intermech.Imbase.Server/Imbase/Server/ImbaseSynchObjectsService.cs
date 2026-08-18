// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.ImbaseSynchObjectsService
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

#nullable disable
namespace Intermech.Imbase.Server;

internal class ImbaseSynchObjectsService : LongLifeObject, IImbaseSynchObjectsService
{
  private Dictionary<Guid, ProcessedItemsInfo> _tasks = new Dictionary<Guid, ProcessedItemsInfo>();

  public void SynchronizeObjects(
    Guid sessionGuid,
    Guid taskGuid,
    Dictionary<int, List<long>> objDict,
    bool createVersion,
    int bindingAttrID = 0)
  {
    if (objDict == null || objDict.Count <= 0)
      return;
    ImbaseSynchObjectsService.Helper helper = new ImbaseSynchObjectsService.Helper(sessionGuid)
    {
      CreateVersion = createVersion
    };
    helper.LoadTask(this._tasks, taskGuid);
    try
    {
      helper.Task.Count = objDict.SelectMany<KeyValuePair<int, List<long>>, long>((System.Func<KeyValuePair<int, List<long>>, IEnumerable<long>>) (x => x.Value.Select<long, long>((System.Func<long, long>) (y => y)))).Count<long>();
      helper.BindAttrID = bindingAttrID;
      helper.NotSynchTypeIDs = objDict.Keys.Where<int>((System.Func<int, bool>) (x => !ImbaseHelper.CanObjectTypeContainAttribute(x, Intermech.Imbase.Consts.ImbaseObjectRefAttID))).ToList<int>();
      ISynchronizationObjService service = ApplicationServices.Container.GetService<ISynchronizationObjService>();
      foreach (KeyValuePair<int, List<long>> keyValuePair in objDict)
      {
        try
        {
          if (helper.NotSynchTypeIDs.Contains(keyValuePair.Key))
            throw new ApplicationException(LocalizationHolder.rm.GetString("Imbase_ObjType_ImbaseObjRef_CantContainsAttr"));
          DataTable dataObjects = this.GetDataObjects(helper.Session, keyValuePair.Key, keyValuePair.Value, helper.BindAttrID);
          this.SynchronizeObjectsForType(helper, keyValuePair.Key, service, dataObjects);
        }
        catch (ApplicationException ex)
        {
          helper.Task.CountByType += keyValuePair.Value.Count;
          helper.Task.AddNotSynchType(keyValuePair.Key, ex.Message);
        }
        if (!helper.Task.TaskRunning)
          break;
      }
    }
    finally
    {
      helper.Task.TaskRunning = false;
      helper.Task.FinishedTime = DateTime.Now;
    }
  }

  public void SynchronizeObjects(
    Guid sessionGuid,
    Guid taskGuid,
    int typeID,
    bool createVersion,
    int bindingAttrID = 0)
  {
    if (typeID == -1)
      return;
    ImbaseSynchObjectsService.Helper helper = new ImbaseSynchObjectsService.Helper(sessionGuid)
    {
      CreateVersion = createVersion
    };
    helper.LoadTask(this._tasks, taskGuid);
    try
    {
      helper.BindAttrID = bindingAttrID;
      List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(typeID);
      helper.NotSynchTypeIDs = childrenIdRecursive.Where<int>((System.Func<int, bool>) (x => !ImbaseHelper.CanObjectTypeContainAttribute(x, Intermech.Imbase.Consts.ImbaseObjectRefAttID))).ToList<int>();
      Dictionary<int, DataTable> dictionary = new Dictionary<int, DataTable>();
      ISynchronizationObjService service = ApplicationServices.Container.GetService<ISynchronizationObjService>();
      foreach (int num in childrenIdRecursive)
      {
        try
        {
          if (helper.NotSynchTypeIDs.Contains(num))
            throw new ApplicationException(LocalizationHolder.rm.GetString("Imbase_ObjType_ImbaseObjRef_CantContainsAttr"));
          DataTable dataObjects = this.GetDataObjects(helper.Session, num, connectedAttrID: helper.BindAttrID);
          dictionary.Add(num, dataObjects);
        }
        catch (ApplicationException ex)
        {
          helper.Task.AddNotSynchType(num, ex.Message);
        }
      }
      helper.Task.Count = dictionary.Values.SelectMany<DataTable, DataRow>((System.Func<DataTable, IEnumerable<DataRow>>) (x => (IEnumerable<DataRow>) x.AsEnumerable().Select<DataRow, DataRow>((System.Func<DataRow, DataRow>) (y => y)))).Count<DataRow>();
      foreach (KeyValuePair<int, DataTable> keyValuePair in dictionary)
      {
        try
        {
          this.SynchronizeObjectsForType(helper, keyValuePair.Key, service, keyValuePair.Value);
        }
        catch (ApplicationException ex)
        {
          helper.Task.CountByType += keyValuePair.Value.Rows.Count;
          helper.Task.AddNotSynchType(keyValuePair.Key, ex.Message);
        }
        if (!helper.Task.TaskRunning)
          break;
      }
    }
    finally
    {
      helper.Task.TaskRunning = false;
      helper.Task.FinishedTime = DateTime.Now;
    }
  }

  public DataTable GetInfoAboutObjectsProcessed(Guid taskGuid, out int count, out int current)
  {
    DataTable objectsProcessed = (DataTable) null;
    count = current = 0;
    if (this._tasks.ContainsKey(taskGuid))
    {
      ProcessedItemsInfo task = this._tasks[taskGuid];
      count = task.Count;
      current = task.Current;
      objectsProcessed = task.ProcessedInfoCopy();
    }
    return objectsProcessed;
  }

  public void StopTask(Guid taskGuid)
  {
    if (!this._tasks.ContainsKey(taskGuid))
      return;
    this._tasks[taskGuid].TaskRunning = false;
  }

  private void SynchronizeObjectsForType(
    ImbaseSynchObjectsService.Helper helper,
    int objTypeID,
    ISynchronizationObjService synchronizationObjService,
    DataTable dt)
  {
    string objectTypeName = MetaDataHelper.GetObjectTypeName(objTypeID);
    bool flag = ImbaseHelper.CanObjectTypeContainAttribute(objTypeID, MetaDataHelper.GetAttributeTypeID("cad0020f-306c-11d8-b4e9-00304f19f545"));
    Dictionary<string, List<long>> notBindObjs = new Dictionary<string, List<long>>();
    string message;
    foreach (DataRow row in (InternalDataCollectionBase) dt.Rows)
    {
      long int64 = Convert.ToInt64(row[SynchStrHelper.COLUMN_NAME_OBJECT_ID]);
      object obj1 = row[SynchStrHelper.COLUMN_NAME_IMBASE_OBJECT_REF];
      long imbaseObjId = 0;
      if (obj1 != null && obj1 != DBNull.Value)
        imbaseObjId = Convert.ToInt64(obj1);
      object obj2 = row[SynchStrHelper.COLUMN_NAME_RECORD_ID];
      long recId = obj2 == null || obj2 == DBNull.Value ? -1L : Convert.ToInt64(obj2);
      QuickObjectInfo objectInfo = helper.Session.GetObjectInfo(int64);
      try
      {
        SynchObjectsStatus status = synchronizationObjService.Synchronize(helper.Session, int64, imbaseObjId, recId, helper.CreateVersion, out message);
        if (status == SynchObjectsStatus.DontLinkedWithIMBASE)
        {
          if (helper.BindAttrID == 0)
            throw new ApplicationException(message);
          string str = LocalizationHolder.rm.GetString("Imbase_Obj_NotBindWithImbase");
          if (!flag)
            throw new ApplicationException($"{str}. {string.Format(LocalizationHolder.rm.GetString("Imbase_CantAddAttr_CodeImbase"), (object) objectTypeName, (object) objTypeID.ToString())}");
          string key = Convert.ToString(row[Convert.ToString(helper.BindAttrID)]).Trim();
          if (string.IsNullOrEmpty(key))
            throw new ApplicationException($"{str}. {LocalizationHolder.rm.GetString("Imbase_Synch_AttrForBind_Empty")}.");
          this.CheckPossibilityOfBindObjectWithImbase(helper.Session, int64, objTypeID);
          if (!notBindObjs.ContainsKey(key))
            notBindObjs.Add(key, new List<long>(1));
          notBindObjs[key].Add(int64);
        }
        else
        {
          helper.Task.AddProcessedObject(int64, objectInfo.Caption, this.GetStatusStr(status), message);
          ++helper.Task.Current;
        }
      }
      catch (ApplicationException ex)
      {
        helper.Task.AddProcessedObject(int64, objectInfo.Caption, SynchStrHelper.NotSynchronized, ex.Message);
        ++helper.Task.Current;
      }
      if (!helper.Task.TaskRunning)
        break;
    }
    if (notBindObjs == null || notBindObjs.Count <= 0 || !helper.Task.TaskRunning)
      return;
    dt = this.Search(helper, notBindObjs);
    foreach (DataRow row in (InternalDataCollectionBase) dt.Rows)
    {
      if (!helper.Task.TaskRunning)
        break;
      long int64 = Convert.ToInt64(row[SynchStrHelper.COLUMN_NAME_OBJECT_ID]);
      object obj3 = row[SynchStrHelper.COLUMN_NAME_IMBASE_OBJECT_REF];
      long imbaseObjId = 0;
      if (obj3 != null && obj3 != DBNull.Value)
        imbaseObjId = Convert.ToInt64(obj3);
      object obj4 = row[SynchStrHelper.COLUMN_NAME_RECORD_ID];
      long recId = obj4 == null || obj4 == DBNull.Value ? -1L : Convert.ToInt64(obj4);
      QuickObjectInfo objectInfo = helper.Session.GetObjectInfo(int64);
      try
      {
        SynchObjectsStatus status = synchronizationObjService.Synchronize(helper.Session, int64, imbaseObjId, recId, helper.CreateVersion, out message);
        if (status == SynchObjectsStatus.DontLinkedWithIMBASE)
          throw new ApplicationException($"{message}. {LocalizationHolder.rm.GetString("Imbase_Synch_IndexValue_Empty")}");
        helper.Task.AddProcessedObject(int64, objectInfo.Caption, this.GetStatusStr(status), message);
        ++helper.Task.Current;
      }
      catch (ApplicationException ex)
      {
        helper.Task.AddProcessedObject(int64, objectInfo.Caption, SynchStrHelper.NotSynchronized, ex.Message);
        ++helper.Task.Current;
      }
    }
  }

  private string GetStatusStr(SynchObjectsStatus status)
  {
    switch (status)
    {
      case SynchObjectsStatus.NotNeedToModified:
        return SynchStrHelper.NotNeedToSync;
      case SynchObjectsStatus.Synchronized:
        return SynchStrHelper.Synchronized;
      case SynchObjectsStatus.NotSynchronized:
        return SynchStrHelper.NotSynchronized;
      default:
        throw new ArgumentOutOfRangeException();
    }
  }

  private DataTable GetDataObjects(
    IUserSession session,
    int objTypeID,
    List<long> objIDs = null,
    int connectedAttrID = 0)
  {
    DataTable dataObjects = (DataTable) null;
    IDBObjectCollection objectCollection = session.GetObjectCollection(objTypeID);
    if (objectCollection != null)
    {
      objectCollection.ObjectTypeID = objTypeID;
      List<ConditionStructure> conditionStructureList = new List<ConditionStructure>()
      {
        objIDs != null ? new ConditionStructure(-2, RelationalOperators.In, (object) objIDs.ToArray(), LogicalOperators.NONE, 0, false) : new ConditionStructure(-7, RelationalOperators.Equal, (object) objTypeID, LogicalOperators.NONE, 0, false)
      };
      List<ColumnDescriptor> columnDescriptorList = new List<ColumnDescriptor>()
      {
        new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
        new ColumnDescriptor((object) Intermech.Imbase.Consts.ImbaseObjectRefAttID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.ASC, 0),
        new ColumnDescriptor((object) Intermech.Imbase.Consts.ImbaseInternalOldKeyAttID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0),
        new ColumnDescriptor((object) -6, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0)
      };
      if (connectedAttrID != 0)
        columnDescriptorList.Add(new ColumnDescriptor((object) connectedAttrID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0));
      DBRecordSetParams paramSet = new DBRecordSetParams(conditionStructureList.ToArray(), columnDescriptorList.ToArray());
      dataObjects = objectCollection.Select(paramSet);
    }
    if (dataObjects == null || dataObjects.Rows.Count == 0)
      throw new ApplicationException(LocalizationHolder.rm.GetString("Imbase_Objs_Data_Empty"));
    return dataObjects;
  }

  private void CheckPossibilityOfBindObjectWithImbase(
    IUserSession session,
    long objID,
    int objTypeID)
  {
    IDBObject objectActualCopy = session.GetObjectActualCopy(objID, false);
    if (objectActualCopy.ObjectModifyMode == ObjectModifyModes.InBase || objectActualCopy.ObjectModifyMode == ObjectModifyModes.Checkout && objectActualCopy.CheckoutBy == session.UserID)
    {
      IDBAttribute attributeByGuid1 = objectActualCopy.GetAttributeByGuid(Intermech.Imbase.Consts.ImbaseObjectRefAttGUID);
      if (attributeByGuid1 != null && attributeByGuid1.ReadOnly)
        throw new ApplicationException(string.Format(LocalizationHolder.rm.GetString("Imbase_CantBindWithImbase_ReadOnlyAttr"), (object) MetaDataHelper.GetAttributeTypeName(attributeByGuid1.AttributeID)));
      IDBAttribute attributeByGuid2 = objectActualCopy.GetAttributeByGuid(new Guid("cad0020f-306c-11d8-b4e9-00304f19f545"));
      if (attributeByGuid2 != null && attributeByGuid2.ReadOnly)
        throw new ApplicationException(string.Format(LocalizationHolder.rm.GetString("Imbase_CantBindWithImbase_ReadOnlyAttr"), (object) MetaDataHelper.GetAttributeTypeName(attributeByGuid2.AttributeID)));
    }
    else
    {
      if (objectActualCopy.ObjectModifyMode != ObjectModifyModes.CantModify)
        return;
      if (ImbaseSynchObjectsService.IsContentAttr(objTypeID, Intermech.Imbase.Consts.ImbaseObjectRefAttID))
        throw new ApplicationException(LocalizationHolder.rm.GetString("Imbase_ObjectModifyModes_CantModify"));
      int attributeTypeId = MetaDataHelper.GetAttributeTypeID(new Guid("cad0020f-306c-11d8-b4e9-00304f19f545"));
      if (ImbaseSynchObjectsService.IsContentAttr(objTypeID, attributeTypeId))
        throw new ApplicationException(LocalizationHolder.rm.GetString("Imbase_ObjectModifyModes_CantModify"));
    }
  }

  private DataTable Search(
    ImbaseSynchObjectsService.Helper helper,
    Dictionary<string, List<long>> notBindObjs)
  {
    DataTable result = new DataTable();
    result.Columns.AddRange(new DataColumn[3]
    {
      new DataColumn(SynchStrHelper.COLUMN_NAME_OBJECT_ID),
      new DataColumn(SynchStrHelper.COLUMN_NAME_IMBASE_OBJECT_REF),
      new DataColumn(SynchStrHelper.COLUMN_NAME_RECORD_ID)
    });
    if (helper.Catalogs != null)
    {
      UserSession session = (UserSession) helper.Session;
      if (session == null)
        return result;
      Dictionary<string, string> dictionary = notBindObjs.ToDictionary<KeyValuePair<string, List<long>>, string, string>((System.Func<KeyValuePair<string, List<long>>, string>) (x => x.Key), (System.Func<KeyValuePair<string, List<long>>, string>) (y => session.StringNormalizer.GetIndexedString(y.Key)));
      foreach (long catalogID in helper.Catalogs.Select<KeyValuePair<long, int>, long>((System.Func<KeyValuePair<long, int>, long>) (x => x.Key)).ToList<long>())
      {
        List<string> list = dictionary.Values.Distinct<string>().ToList<string>();
        DataTable dtTemp = this.Search(session, catalogID, helper.BindAttrID, list);
        if (dtTemp != null)
        {
          this.ParseResult(result, dtTemp, notBindObjs, dictionary);
          if (dictionary.Count == 0)
            break;
        }
      }
      if (dictionary.Count > 0)
      {
        foreach (KeyValuePair<string, string> keyValuePair in dictionary)
          notBindObjs[keyValuePair.Key].ForEach((Action<long>) (x => result.Rows.Add((object) x, null, null)));
      }
    }
    return result;
  }

  public DataTable Search(UserSession session, long catalogID, int attrID, List<string> hashText)
  {
    string tableName = ImbaseIndexingService.GenerateTableName(catalogID, attrID);
    IQueryBuilder queryBuilder = session.GetQueryBuilder();
    string format = " SELECT idx.* FROM {0} idx, {1} tmp WHERE idx.F_HASHTEXT = tmp.F_VALUE {3} AND tmp.F_KEY = {2} ";
    INConditionValue cValue = queryBuilder.StartINCondition((object) -50, (Array) hashText.ToArray(), true);
    try
    {
      return session.DataManager.ExecuteDataTable(string.Format(format, (object) tableName, (object) cValue.TmpTableName, (object) cValue.SelectKey, (object) session.DataManager.DataProvider.GetCollateSQL()));
    }
    finally
    {
      queryBuilder.StopINCondition(cValue);
    }
  }

  private void ParseResult(
    DataTable dtResult,
    DataTable dtTemp,
    Dictionary<string, List<long>> notBindObjs,
    Dictionary<string, string> hashTextDict)
  {
    Dictionary<string, Tuple<long, long>> dictionary1 = new Dictionary<string, Tuple<long, long>>(dtTemp.Rows.Count);
    Dictionary<string, Tuple<long, long>> dictionary2 = new Dictionary<string, Tuple<long, long>>(dtTemp.Rows.Count);
    foreach (DataRow row in (InternalDataCollectionBase) dtTemp.Rows)
    {
      string key1 = Convert.ToString(row[IndexesField.F_TEXT]);
      if (!dictionary1.ContainsKey(key1))
      {
        long int64_1 = Convert.ToInt64(row[IndexesField.F_LINK_ID]);
        long int64_2 = Convert.ToInt64(row[IndexesField.F_TABKEY]);
        dictionary1.Add(key1, Tuple.Create<long, long>(int64_1, int64_2));
        string key2 = Convert.ToString(row[IndexesField.F_HASHTEXT]);
        if (!dictionary2.ContainsKey(key2))
          dictionary2.Add(key2, Tuple.Create<long, long>(int64_1, int64_2));
      }
    }
    foreach (string key in hashTextDict.Keys.ToList<string>())
    {
      Tuple<long, long> t;
      if (dictionary1.TryGetValue(key, out t) || dictionary2.TryGetValue(hashTextDict[key], out t))
      {
        notBindObjs[key].ForEach((Action<long>) (x => dtResult.Rows.Add((object) x, (object) t.Item1, (object) t.Item2)));
        hashTextDict.Remove(key);
        notBindObjs.Remove(key);
      }
    }
  }

  private static bool IsContentAttr(int objTypeID, int attrID)
  {
    IMSAttribute4ObjectType attribute4ObjectType = MetaDataHelper.GetAttribute4ObjectType(objTypeID, attrID);
    bool flag;
    if (attribute4ObjectType == null)
    {
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attrID);
      flag = (attributeType.Options & AttributeOptions.ModifyInBase) != AttributeOptions.ModifyInBase || attributeType.IsContent;
    }
    else
      flag = ImbaseSynchObjectsService.IsContentAttr(attribute4ObjectType);
    return flag;
  }

  private static bool IsContentAttr(IMSAttribute4ObjectType imsAttr)
  {
    return (imsAttr.Options & AttributeOptions.ModifyInBase) != AttributeOptions.ModifyInBase || imsAttr.IsContent;
  }

  private class Helper
  {
    private int _bindAttrID;

    internal IUserSession Session { get; }

    internal ProcessedItemsInfo Task { get; private set; }

    internal int BindAttrID
    {
      get => this._bindAttrID;
      set
      {
        this._bindAttrID = value;
        if (value != 0)
        {
          string commandText = $"SELECT {IndexesField.F_CATALOG_ID} FROM IMS_IMBASE_INDEXES WHERE {IndexesField.F_ATTRIBUTE_ID}=:a_ID";
          IDbManager dataManager = ((UserSession) this.Session).DataManager;
          DataTable source = dataManager.ExecuteDataTable(commandText, dataManager.Parameter(":a_ID", (object) value));
          if (source == null || source.Rows.Count <= 0)
            return;
          this.Catalogs = this.GetCatalogsWithCreatedType(source.AsEnumerable().Select<DataRow, long>((System.Func<DataRow, long>) (x => Convert.ToInt64(x[IndexesField.F_CATALOG_ID]))).ToList<long>());
        }
        else
          this.Catalogs = (Dictionary<long, int>) null;
      }
    }

    internal Dictionary<long, int> Catalogs { get; private set; }

    internal List<int> NotSynchTypeIDs { get; set; }

    internal bool CreateVersion { get; set; }

    internal Helper(Guid sessionGuid)
    {
      this.Session = ImbaseServer.GetSession(sessionGuid);
      if (this.Session == null)
        throw new Exception(LocalizationHolder.rm.GetString("Imbase.Server.ImbaseindexingService.NullSession"));
    }

    internal void LoadTask(Dictionary<Guid, ProcessedItemsInfo> tasks, Guid taskGuid)
    {
      if (tasks.ContainsKey(taskGuid))
      {
        this.Task = tasks[taskGuid];
        this.Task.TaskRunning = true;
      }
      else
      {
        this.Task = new ProcessedItemsInfo();
        foreach (Guid key in tasks.Where<KeyValuePair<Guid, ProcessedItemsInfo>>((System.Func<KeyValuePair<Guid, ProcessedItemsInfo>, bool>) (x => !x.Value.TaskRunning && (DateTime.Now - x.Value.FinishedTime).TotalMinutes > 10.0)).ToDictionary<KeyValuePair<Guid, ProcessedItemsInfo>, Guid, ProcessedItemsInfo>((System.Func<KeyValuePair<Guid, ProcessedItemsInfo>, Guid>) (x => x.Key), (System.Func<KeyValuePair<Guid, ProcessedItemsInfo>, ProcessedItemsInfo>) (y => y.Value)).Keys)
          tasks.Remove(key);
        tasks.Add(taskGuid, this.Task);
      }
    }

    private Dictionary<long, int> GetCatalogsWithCreatedType(List<long> catalogIDs)
    {
      Dictionary<long, int> catalogsWithCreatedType = (Dictionary<long, int>) null;
      IDBObjectCollection objectCollection = this.Session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseCatalogTypeID);
      if (objectCollection != null)
      {
        objectCollection.ObjectTypeID = Intermech.Imbase.Consts.ImbaseCatalogTypeID;
        DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[2]
        {
          new ConditionStructure(-2, RelationalOperators.In, (object) catalogIDs.ToArray(), LogicalOperators.AND, 0, false),
          new ConditionStructure(Intermech.Imbase.Consts.CreatedObjectAttID, RelationalOperators.NotEmpty, (object) null, LogicalOperators.NONE, 0, false)
        }, new ColumnDescriptor[2]
        {
          new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
          new ColumnDescriptor((object) Intermech.Imbase.Consts.CreatedObjectAttID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0)
        });
        DataTable source = objectCollection.Select(paramSet);
        if (source != null && source.Rows.Count > 0)
          catalogsWithCreatedType = source.AsEnumerable().ToDictionary<DataRow, long, int>((System.Func<DataRow, long>) (x => Convert.ToInt64(x[0])), (System.Func<DataRow, int>) (y => MetaDataHelper.GetObjectTypeID(new Guid(Convert.ToString(y[1])))));
      }
      return catalogsWithCreatedType;
    }
  }
}
