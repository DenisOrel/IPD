// Decompiled with JetBrains decompiler
// Type: Intermech.CompositionTracking.Server.CompositionTrackingSessionTask
// Assembly: Intermech.CompositionTracking.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 560FA293-6728-4C34-9171-0CC07BE87BF4
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.CompositionTracking.Server.dll

using Intermech.CompositionTracking.Server.Methods;
using Intermech.CompositionTracking.Server.Params;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.CompositionTracking;
using Intermech.Interfaces.Contexts;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Threading;

#nullable disable
namespace Intermech.CompositionTracking.Server;

internal class CompositionTrackingSessionTask : MarshalByRefObject, IDisposable
{
  private readonly object _locker = new object();
  private readonly CompositionTrackingSession _trackingSession;
  private readonly CompositionTrackingBaseMethod _method;
  private CompositionTrackingParams _trackingParams;
  private int _isActive;
  private List<long> _commandLog = new List<long>();
  private HashSet<long> _commandObjects = new HashSet<long>();
  private Dictionary<int, List<ObjInfoItem>> _expandedObjects = new Dictionary<int, List<ObjInfoItem>>();
  private List<IDBObject> _delayedObjects = new List<IDBObject>();
  private const string DefaultFiltrationRule = "cad001e2-306c-11d8-b4e9-00304f19f545";

  private void RegisterCommandLog(long objectId)
  {
    if (objectId == 0L)
      return;
    this.RegisterCommandLog(new List<long>() { objectId });
  }

  private void RegisterCommandLog(List<long> objIdList)
  {
    if (objIdList == null || objIdList.Count == 0)
      return;
    lock (this._commandLog)
    {
      this._commandLog.AddRange((IEnumerable<long>) objIdList);
      GenericListHelper.MakeUnique<long>(this._commandLog);
    }
  }

  private void DoExecuteObjects(List<IDBObject> dbObjectList)
  {
    if (CompositionTrackingServerHolder.TrackingService == null || dbObjectList == null || dbObjectList.Count == 0)
      return;
    this.CleanUpTrackingObjectList(dbObjectList);
    if (dbObjectList.Count == 0)
      return;
    this._commandObjects.AddRange<long>(dbObjectList.Where<IDBObject>((System.Func<IDBObject, bool>) (x => x != null)).Select<IDBObject, long>((System.Func<IDBObject, long>) (x => x.ObjectID)));
    Dictionary<long, IDBObject> dbObjCache = new Dictionary<long, IDBObject>(dbObjectList.Count);
    Dictionary<int, List<ObjInfoItem>> dbTypedList = new Dictionary<int, List<ObjInfoItem>>(dbObjectList.Count);
    foreach (IDBObject dbObject in dbObjectList)
    {
      if (dbObject != null)
      {
        List<ObjInfoItem> objInfoItemList;
        if (!dbTypedList.TryGetValue(dbObject.ObjectType, out objInfoItemList))
        {
          objInfoItemList = new List<ObjInfoItem>();
          dbTypedList.Add(dbObject.ObjectType, objInfoItemList);
        }
        objInfoItemList.Add(new ObjInfoItem(dbObject));
        if (!dbObjCache.ContainsKey(dbObject.ObjectID))
          dbObjCache.Add(dbObject.ObjectID, dbObject);
      }
    }
    this.ProceedTrackingObject_Recursive(dbObjCache, dbTypedList);
    this.ProceedTrackingObject_Simple(dbObjCache, dbTypedList);
  }

  private void CleanUpTrackingObjectList(List<IDBObject> dbObjList)
  {
    List<IDBObject> collection = new List<IDBObject>(dbObjList.Count);
    foreach (IDBObject dbObj in dbObjList)
    {
      if (dbObj != null && !this._commandObjects.Contains(dbObj.ObjectID))
        collection.Add(dbObj);
    }
    dbObjList.Clear();
    dbObjList.AddRange((IEnumerable<IDBObject>) collection);
  }

  public CompositionTrackingSessionTask(
    CompositionTrackingSession trackingSession,
    CompositionTrackingBaseMethod method)
  {
    this._trackingSession = trackingSession ?? throw new ArgumentNullException(nameof (trackingSession));
    this._method = method ?? throw new ArgumentNullException(nameof (method));
  }

  public void Dispose()
  {
    this._commandLog.Clear();
    this._commandLog = (List<long>) null;
    this._commandObjects.Clear();
    this._commandObjects = (HashSet<long>) null;
    this._delayedObjects.Clear();
    this._delayedObjects = (List<IDBObject>) null;
    this._expandedObjects.Clear();
    this._expandedObjects = (Dictionary<int, List<ObjInfoItem>>) null;
  }

  public void Execute()
  {
    if (this._trackingParams == null)
      return;
    IDBObject targetObject = this.Method.GetTargetObject(this.Params);
    lock (this._locker)
    {
      if (this._isActive != 0)
      {
        lock (this._delayedObjects)
        {
          this._delayedObjects.Add(targetObject);
          return;
        }
      }
    }
    lock (this._locker)
      ++this._isActive;
    try
    {
      List<IDBObject> dbObjectList = new List<IDBObject>()
      {
        targetObject
      };
      this.DoExecuteObjects(dbObjectList);
      do
      {
        Thread.Sleep(10);
        lock (this._delayedObjects)
        {
          dbObjectList.Clear();
          dbObjectList.AddRange((IEnumerable<IDBObject>) this._delayedObjects);
          this._delayedObjects.Clear();
        }
        this.DoExecuteObjects(dbObjectList);
      }
      while (dbObjectList.Count != 0);
    }
    finally
    {
      lock (this._locker)
        --this._isActive;
    }
  }

  internal CompositionTrackingParams Params
  {
    get => this._trackingParams;
    set => this._trackingParams = value;
  }

  internal CompositionTrackingBaseMethod Method => this._method;

  internal List<long> GetCommandLog()
  {
    lock (this._commandLog)
      return this._commandLog;
  }

  private static List<long> GetRootObjIDs(
    long objId,
    Dictionary<long, List<long>> part2ProjCache,
    Dictionary<long, List<long>> obj2RootCache)
  {
    List<long> rootObjIds;
    if (obj2RootCache.TryGetValue(objId, out rootObjIds))
      return rootObjIds;
    List<long> list = new List<long>();
    List<long> longList;
    if (!part2ProjCache.TryGetValue(objId, out longList))
    {
      list.Add(objId);
      return list;
    }
    foreach (long objId1 in longList)
      list.AddRange((IEnumerable<long>) CompositionTrackingSessionTask.GetRootObjIDs(objId1, part2ProjCache, obj2RootCache));
    GenericListHelper.MakeUnique<long>(list);
    obj2RootCache.Add(objId, list);
    return list;
  }

  private void ProceedTrackingObject_Recursive(
    Dictionary<long, IDBObject> dbObjCache,
    Dictionary<int, List<ObjInfoItem>> dbTypedList)
  {
    IUserSession session = this.Params.Session;
    if (session == null || dbTypedList == null || dbTypedList.Count == 0)
      return;
    ICompositionLoadService service1 = ServiceUtils.GetService<ICompositionLoadService>((object) session, false);
    if (service1 == null)
      return;
    Dictionary<int, List<ObjInfoItem>> dictionary1 = new Dictionary<int, List<ObjInfoItem>>();
    List<ObjInfoItem> list = new List<ObjInfoItem>();
    foreach (KeyValuePair<int, List<ObjInfoItem>> dbTyped in dbTypedList)
    {
      CompositionTypeSettingDataList trackSettList;
      if (CompositionTrackingServerHolder.TrackingService.Settings.GetConfigValues(dbTyped.Key, this._method.Command, out trackSettList) && trackSettList != null && trackSettList.Count != 0)
      {
        foreach (CompositionTrackSettingData key in trackSettList.Keys)
        {
          if ((key.ObjectTypeContext.ObjectTypeId == -1 || MetaDataHelper.GetObjectType(key.ObjectTypeContext.ObjectTypeId) != null) && CompositionTrackingServerHolder.TrackingService.IsRegisteredTrackConfig(key.ObjectTypeContext, true))
          {
            bool flag1 = (CompositionTrackingObjMode.ctomAll & trackSettList[key].ObjMode) == CompositionTrackingObjMode.ctomAll;
            bool flag2 = (CompositionTrackingObjMode.ctomContext & trackSettList[key].ObjMode) == CompositionTrackingObjMode.ctomContext;
            if (flag1 || flag2)
            {
              if (!flag1)
                list.AddRange((IEnumerable<ObjInfoItem>) dbTyped.Value);
              List<ObjInfoItem> objInfoItemList;
              if (!dictionary1.TryGetValue(key.ObjectTypeContext.RelationTypeId, out objInfoItemList))
              {
                objInfoItemList = new List<ObjInfoItem>();
                dictionary1.Add(key.ObjectTypeContext.RelationTypeId, objInfoItemList);
              }
              objInfoItemList.AddRange((IEnumerable<ObjInfoItem>) dbTyped.Value);
            }
          }
        }
      }
    }
    if (dictionary1.Count == 0)
      return;
    Guid guid = new Guid("cad00202-306c-11d8-b4e9-00304f19f545");
    int attributeTypeId = MetaDataHelper.GetAttributeTypeID(guid);
    bool flag = true;
    foreach (int key in dictionary1.Keys)
    {
      IMSRelationType relationType = MetaDataHelper.GetRelationType(key);
      if (relationType != null && !relationType.AnyAttributes && MetaDataHelper.GetAttribute4RelationType(key, attributeTypeId) == null)
      {
        flag = false;
        break;
      }
    }
    List<ColumnDescriptor> columns = new List<ColumnDescriptor>();
    columns.Add(new ColumnDescriptor((object) -20, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
    columns.Add(new ColumnDescriptor((object) -23, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
    columns.Add(new ColumnDescriptor((object) -21, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
    columns.Add(new ColumnDescriptor((object) -2, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
    columns.Add(new ColumnDescriptor((object) -7, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
    if (flag)
      columns.Add(new ColumnDescriptor((object) guid, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.ASC, 0));
    List<ConditionStructure> conditionStructureList = new List<ConditionStructure>();
    Dictionary<long, List<EditingContextsObjectContainer>> dictionary2 = new Dictionary<long, List<EditingContextsObjectContainer>>();
    List<EditingContextsObjectContainer> contextsObjectContainerList;
    if (list.Count > 0)
    {
      GenericListHelper.MakeUnique<ObjInfoItem>(list);
      IDBEditingContextsService service2 = ServiceUtils.GetService<IDBEditingContextsService>((object) session, true);
      List<long> versionIDs = new List<long>(1);
      foreach (ObjInfoItem objInfoItem in list)
      {
        contextsObjectContainerList = new List<EditingContextsObjectContainer>();
        dictionary2.Add(objInfoItem.ObjectID, contextsObjectContainerList);
        versionIDs.Clear();
        versionIDs.Add(objInfoItem.ObjectID);
        foreach (long objectsContext in service2.FindObjectsContexts((object) session, versionIDs, true))
        {
          EditingContextsObjectContainer editingContextsObject = service2.GetEditingContextsObject((object) session, objectsContext, false, true);
          if (editingContextsObject != null)
            contextsObjectContainerList.Add(editingContextsObject);
        }
      }
    }
    List<IDBObject> dbObjectList = new List<IDBObject>();
    foreach (KeyValuePair<int, List<ObjInfoItem>> keyValuePair1 in dictionary1)
    {
      int key1 = keyValuePair1.Key;
      List<ObjInfoItem> resultData = keyValuePair1.Value;
      GenericListHelper.MakeUnique<ObjInfoItem>(resultData);
      List<ObjInfoItem> objInfoItemList;
      if (!this._expandedObjects.TryGetValue(key1, out objInfoItemList))
      {
        objInfoItemList = new List<ObjInfoItem>();
        this._expandedObjects.Add(key1, objInfoItemList);
      }
      GenericListHelper.GetDifference<ObjInfoItem>((IList<ObjInfoItem>) resultData, (IList<ObjInfoItem>) objInfoItemList, GenericListHelper.SearchMode.smNotExistInB, out resultData);
      if (resultData != null && resultData.Count != 0)
      {
        DataTable dataTable = service1.LoadComplexCompositions((object) session, (IEnumerable<ObjInfoItem>) resultData, (IEnumerable<int>) new List<int>((IEnumerable<int>) new int[1]
        {
          key1
        }), (IEnumerable<int>) null, (IEnumerable<ColumnDescriptor>) columns, true, false, (VersionsRule) null, (IEnumerable<ConditionStructure>) conditionStructureList.ToArray(), "cad001e2-306c-11d8-b4e9-00304f19f545", (Dictionary<long, HybridDictionary>) null, -1);
        if (dataTable != null && dataTable.Rows.Count != 0)
        {
          Dictionary<long, List<long>> part2ProjCache = new Dictionary<long, List<long>>();
          List<long> longList1;
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            if (row != null)
            {
              long int64_1 = Convert.ToInt64(row["F_PROJ_ID"]);
              long int64_2 = Convert.ToInt64(row["F_OBJECT_ID"]);
              if (!part2ProjCache.TryGetValue(int64_2, out longList1))
              {
                longList1 = new List<long>();
                part2ProjCache.Add(int64_2, longList1);
              }
              longList1.Add(int64_1);
            }
          }
          if (part2ProjCache.Count != 0)
          {
            Dictionary<long, List<long>> obj2RootCache = new Dictionary<long, List<long>>();
            List<long> longList2 = new List<long>((IEnumerable<long>) dbObjCache.Keys);
            foreach (KeyValuePair<long, List<long>> keyValuePair2 in part2ProjCache)
            {
              long key2 = keyValuePair2.Key;
              if (key2 != 0L && keyValuePair2.Value != null && keyValuePair2.Value.Count != 0 && !this._commandObjects.Contains(key2))
              {
                longList1 = longList2.Count == 1 ? longList2 : CompositionTrackingSessionTask.GetRootObjIDs(key2, part2ProjCache, obj2RootCache);
                if (longList1 != null && longList1.Count != 0)
                {
                  long key3 = 0;
                  foreach (long num in longList1)
                  {
                    if (dictionary2.TryGetValue(num, out contextsObjectContainerList))
                    {
                      if (contextsObjectContainerList != null && contextsObjectContainerList.Count != 0)
                      {
                        foreach (EditingContextsObjectContainer contextsObjectContainer in contextsObjectContainerList)
                        {
                          if (contextsObjectContainer.ExistsVersion(key2))
                          {
                            key3 = num;
                            break;
                          }
                        }
                      }
                    }
                    else
                    {
                      objInfoItemList.Add(new ObjInfoItem(num));
                      key3 = num;
                    }
                  }
                  IDBObject sourceDbObject;
                  if (dbObjCache.TryGetValue(key3, out sourceDbObject) && sourceDbObject != null)
                  {
                    IDBObject targetDbObject = session.GetObject(keyValuePair2.Key, false);
                    if (targetDbObject != null && this.Method.Execute(this.Params, sourceDbObject, ref targetDbObject))
                    {
                      dbObjectList.Add(targetDbObject);
                      this.RegisterCommandLog(targetDbObject.ObjectID);
                    }
                  }
                }
              }
            }
            GenericListHelper.MakeUnique<ObjInfoItem>(objInfoItemList);
          }
        }
      }
    }
    if (dbObjectList.Count == 0)
      return;
    this.DoExecuteObjects(dbObjectList);
  }

  private void ProceedTrackingObject_Simple(
    Dictionary<long, IDBObject> dbObjCache,
    Dictionary<int, List<ObjInfoItem>> dbTypedList)
  {
    IUserSession session = this.Params.Session;
    if (session == null || dbTypedList == null || dbTypedList.Count == 0)
      return;
    ICompositionLoadService service = ServiceUtils.GetService<ICompositionLoadService>((object) session, false);
    if (service == null)
      return;
    Dictionary<int, List<int>> dictionary1 = new Dictionary<int, List<int>>();
    Dictionary<int, List<ObjInfoItem>> dictionary2 = new Dictionary<int, List<ObjInfoItem>>();
    System.Action<int, int, int, List<int>> getInheritedTypesOnly = (System.Action<int, int, int, List<int>>) null;
    getInheritedTypesOnly = (System.Action<int, int, int, List<int>>) ((relTypeId, projTypeId, partTypeId, inheritedObjTypeIds) =>
    {
      foreach (int childObjTypeID in MetaDataHelper.GetObjectTypeChildrenID(partTypeId))
      {
        if (childObjTypeID != partTypeId)
        {
          IMSApplicability applicability = MetaDataHelper.GetApplicability(projTypeId, childObjTypeID, relTypeId);
          if (applicability != null && (applicability.ChildObjectTypeID != childObjTypeID || applicability.Public == InheritModes.Inherited))
          {
            inheritedObjTypeIds.Add(childObjTypeID);
            getInheritedTypesOnly(relTypeId, projTypeId, childObjTypeID, inheritedObjTypeIds);
          }
        }
      }
    });
    foreach (KeyValuePair<int, List<ObjInfoItem>> dbTyped in dbTypedList)
    {
      CompositionTypeSettingDataList trackSettList;
      if (CompositionTrackingServerHolder.TrackingService.Settings.GetConfigValues(dbTyped.Key, this._method.Command, out trackSettList) && trackSettList != null && trackSettList.Count != 0)
      {
        foreach (CompositionTrackSettingData key in trackSettList.Keys)
        {
          if ((key.ObjectTypeContext.ObjectTypeId == -1 || MetaDataHelper.GetObjectType(key.ObjectTypeContext.ObjectTypeId) != null) && CompositionTrackingServerHolder.TrackingService.IsRegisteredTrackConfig(key.ObjectTypeContext, true) && (CompositionTrackingObjMode.ctomProceed & trackSettList[key].ObjMode) == CompositionTrackingObjMode.ctomProceed)
          {
            List<int> intList;
            List<ObjInfoItem> objInfoItemList;
            if (!dictionary1.TryGetValue(key.ObjectTypeContext.RelationTypeId, out intList))
            {
              intList = new List<int>();
              dictionary1.Add(key.ObjectTypeContext.RelationTypeId, intList);
              objInfoItemList = new List<ObjInfoItem>();
              dictionary2.Add(key.ObjectTypeContext.RelationTypeId, objInfoItemList);
            }
            else
              objInfoItemList = dictionary2[key.ObjectTypeContext.RelationTypeId];
            intList.Add(key.ObjectTypeContext.ObjectTypeId);
            List<int> collection = new List<int>();
            getInheritedTypesOnly(key.ObjectTypeContext.RelationTypeId, dbTyped.Key, key.ObjectTypeContext.ObjectTypeId, collection);
            intList.AddRange((IEnumerable<int>) collection);
            objInfoItemList.AddRange((IEnumerable<ObjInfoItem>) dbTyped.Value);
          }
        }
      }
    }
    if (dictionary1.Count == 0)
      return;
    Guid guid = new Guid("cad00202-306c-11d8-b4e9-00304f19f545");
    int attributeTypeId = MetaDataHelper.GetAttributeTypeID(guid);
    bool flag = true;
    foreach (int key in dictionary1.Keys)
    {
      IMSRelationType relationType = MetaDataHelper.GetRelationType(key);
      if (relationType != null && !relationType.AnyAttributes && MetaDataHelper.GetAttribute4RelationType(key, attributeTypeId) == null)
      {
        flag = false;
        break;
      }
    }
    List<ColumnDescriptor> columns = new List<ColumnDescriptor>();
    columns.Add(new ColumnDescriptor((object) -20, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
    columns.Add(new ColumnDescriptor((object) -23, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
    columns.Add(new ColumnDescriptor((object) -21, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
    columns.Add(new ColumnDescriptor((object) -2, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
    columns.Add(new ColumnDescriptor((object) -7, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
    if (flag)
      columns.Add(new ColumnDescriptor((object) guid, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.ASC, 0));
    List<ConditionStructure> collection1 = new List<ConditionStructure>();
    ConditionStructure conditionStructure = new ConditionStructure(-7, RelationalOperators.In, (object) null, (object) null, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Auto, ColumnContents.Text);
    DataTable toTable = (DataTable) null;
    List<ConditionStructure> conditionStructureList = new List<ConditionStructure>();
    foreach (KeyValuePair<int, List<int>> keyValuePair in dictionary1)
    {
      int key = keyValuePair.Key;
      List<int> list = keyValuePair.Value;
      GenericListHelper.MakeUnique<int>(list);
      List<ObjInfoItem> resultData = dictionary2[key];
      GenericListHelper.MakeUnique<ObjInfoItem>(resultData);
      List<ObjInfoItem> bList;
      if (this._expandedObjects.TryGetValue(key, out bList))
        GenericListHelper.GetDifference<ObjInfoItem>((IList<ObjInfoItem>) resultData, (IList<ObjInfoItem>) bList, GenericListHelper.SearchMode.smNotExistInB, out resultData);
      conditionStructureList.Clear();
      conditionStructureList.AddRange((IEnumerable<ConditionStructure>) collection1);
      conditionStructure.Value = (object) list.ToArray();
      conditionStructureList.Add(conditionStructure);
      DataTable fromTable = service.LoadComplexCompositions((object) session, (IEnumerable<ObjInfoItem>) resultData, (IEnumerable<int>) new List<int>((IEnumerable<int>) new int[1]
      {
        key
      }), (IEnumerable<int>) list.ToArray(), (IEnumerable<ColumnDescriptor>) columns, true, false, (VersionsRule) null, (IEnumerable<ConditionStructure>) conditionStructureList.ToArray(), "cad001e2-306c-11d8-b4e9-00304f19f545", (Dictionary<long, HybridDictionary>) null, 1);
      if (fromTable != null)
      {
        if (toTable == null)
          toTable = fromTable;
        else
          DataSetProcessor.AddTable(toTable, fromTable, false);
      }
    }
    if (toTable == null || toTable.Rows.Count == 0)
      return;
    Dictionary<long, long> dictionary3 = new Dictionary<long, long>();
    foreach (DataRow row in (InternalDataCollectionBase) toTable.Rows)
    {
      long int64_1 = Convert.ToInt64(row["F_OBJECT_ID"]);
      long int64_2 = Convert.ToInt64(row["F_PROJ_ID"]);
      if (!this._commandObjects.Contains(int64_1) && !dictionary3.ContainsKey(int64_1))
        dictionary3.Add(int64_1, int64_2);
    }
    if (dictionary3.Count == 0)
      return;
    List<IDBObject> dbObjectList = new List<IDBObject>();
    foreach (KeyValuePair<long, long> keyValuePair in dictionary3)
    {
      IDBObject sourceDbObject;
      if (keyValuePair.Key != 0L && dbObjCache.TryGetValue(keyValuePair.Value, out sourceDbObject) && sourceDbObject != null)
      {
        IDBObject targetDbObject = session.GetObject(keyValuePair.Key, false);
        if (targetDbObject != null && this.Method.Execute(this._trackingParams, sourceDbObject, ref targetDbObject))
        {
          dbObjectList.Add(targetDbObject);
          this.RegisterCommandLog(targetDbObject.ObjectID);
        }
      }
    }
    if (dbObjectList.Count == 0)
      return;
    this.DoExecuteObjects(dbObjectList);
  }
}
