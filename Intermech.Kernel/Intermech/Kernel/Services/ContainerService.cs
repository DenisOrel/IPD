// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.ContainerService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace Intermech.Kernel.Services;

[Serializable]
public class ContainerService : LongLifeObject, IContainerService
{
  private ConcurrentDictionary<Guid, long> _containerObjectType;
  private ConcurrentDictionary<Guid, long> _containerLCStep;
  private ConcurrentDictionary<Guid, long> _containerLCLevel;
  private ConcurrentDictionary<string, long> _containerLCStepObjectType;
  private bool _CacheLoaded;
  public const long CountainerNotFound = -1;

  public KeyValuePair<Guid, long>[] GetObjectTypeContainers()
  {
    return this._containerObjectType.ToArray();
  }

  private long FindObjectInDB(
    IUserSession session,
    Guid objectGuid,
    ConcurrentDictionary<Guid, long> dict,
    ContainerService.operationType type)
  {
    long objectInDb = -1;
    ColumnDescriptor[] columns = new ColumnDescriptor[1]
    {
      new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0)
    };
    object[] objArray = new object[0];
    SortOrders[] sortOrdersArray = new SortOrders[0];
    string g = string.Empty;
    switch (type)
    {
      case ContainerService.operationType.ObjectType:
        g = "cad001a0-306c-11d8-b4e9-00304f19f545";
        break;
      case ContainerService.operationType.LCStep:
        g = "cad0014c-306c-11d8-b4e9-00304f19f545";
        break;
      case ContainerService.operationType.LCLevel:
        g = "cad0015b-306c-11d8-b4e9-00304f19f545";
        break;
    }
    ConditionStructure conditionStructure = new ConditionStructure(new Guid(g), RelationalOperators.Equal, (object) objectGuid, LogicalOperators.AND, 0);
    DataTable dataTable = session.GetObjectCollection(new Guid("cad0013b-306c-11d8-b4e9-00304f19f545")).Select(new DBRecordSetParams(new ConditionStructure[1]
    {
      conditionStructure
    }, columns));
    if (dataTable.Rows.Count > 0)
      objectInDb = Convert.ToInt64(dataTable.Rows[0][0]);
    dict.TryRemove(objectGuid, out long _);
    dict.TryAdd(objectGuid, objectInDb);
    return objectInDb;
  }

  public void InitCache(IUserSession session)
  {
    if (this._CacheLoaded)
      return;
    this.LoadCache(session);
  }

  private void LoadCache(IUserSession session)
  {
    ConcurrentDictionary<Guid, long> concurrentDictionary1 = new ConcurrentDictionary<Guid, long>();
    ConcurrentDictionary<Guid, long> concurrentDictionary2 = new ConcurrentDictionary<Guid, long>();
    ConcurrentDictionary<Guid, long> concurrentDictionary3 = new ConcurrentDictionary<Guid, long>();
    ConcurrentDictionary<string, long> concurrentDictionary4 = new ConcurrentDictionary<string, long>();
    foreach (DataRow row in (InternalDataCollectionBase) session.GetObjectCollection(new Guid("cad0013b-306c-11d8-b4e9-00304f19f545")).Select(new DBRecordSetParams((ConditionStructure[]) null, new object[5]
    {
      (object) -2,
      (object) new Guid("cad001a0-306c-11d8-b4e9-00304f19f545"),
      (object) new Guid("cad0014c-306c-11d8-b4e9-00304f19f545"),
      (object) new Guid("cad0015b-306c-11d8-b4e9-00304f19f545"),
      (object) new Guid("cad00922-306c-11d8-b4e9-00304f19f545")
    })).Rows)
    {
      long int64 = Convert.ToInt64(row[0]);
      if (row[1].ToString().Trim() != string.Empty)
        concurrentDictionary1.TryAdd(new Guid(row[1].ToString()), int64);
      else if (row[2].ToString().Trim() != string.Empty)
        concurrentDictionary2.TryAdd(new Guid(row[2].ToString()), int64);
      else if (row[3].ToString().Trim() != string.Empty)
        concurrentDictionary3.TryAdd(new Guid(row[3].ToString()), int64);
      else if (row[4].ToString().Trim() != string.Empty)
        concurrentDictionary4.TryAdd(row[4].ToString(), int64);
    }
    this._containerObjectType = concurrentDictionary1;
    this._containerLCStep = concurrentDictionary2;
    this._containerLCLevel = concurrentDictionary3;
    this._containerLCStepObjectType = concurrentDictionary4;
    this._CacheLoaded = true;
  }

  private IDBObject CreateContainer(
    IUserSession session,
    Guid objectGuid,
    ContainerService.operationType type)
  {
    IDBObject dbObject = session.GetObjectCollection(new Guid("cad0013b-306c-11d8-b4e9-00304f19f545")).Create();
    string g = string.Empty;
    string str = string.Empty;
    string empty = string.Empty;
    switch (type)
    {
      case ContainerService.operationType.ObjectType:
        g = "cad001a0-306c-11d8-b4e9-00304f19f545";
        str = $"{LocalizationHolder.rm.GetString("Kernel_637")}{session.GetObjectType(objectGuid).ObjectTypeName}\"";
        break;
      case ContainerService.operationType.LCStep:
        g = "cad0014c-306c-11d8-b4e9-00304f19f545";
        str = $"{LocalizationHolder.rm.GetString("Kernel_638")}{session.GetLifecycleStep(objectGuid).LCName}\"";
        break;
      case ContainerService.operationType.LCLevel:
        g = "cad0015b-306c-11d8-b4e9-00304f19f545";
        str = $"{LocalizationHolder.rm.GetString("Kernel_639")}{session.GetLifecycleLevel(objectGuid).LevelName}\"";
        break;
    }
    if (dbObject.Attributes.AddAttribute(session.GetAttributeType(new Guid(g)).AttributeID, true, new object[1]
    {
      (object) objectGuid
    }) != null)
    {
      dbObject.Caption = LocalizationHolder.rm.GetString("Kernel_640") + str;
      dbObject.OwnerID = session.IdentHelper.SystemID;
      dbObject.CommitCreation(true);
      this.AddToHashtable(dbObject.ObjectID, objectGuid, type);
      return session.GetObject(dbObject.ObjectID);
    }
    dbObject.Delete(0L);
    return (IDBObject) null;
  }

  private void AddToHashtable(long objId, Guid objectGuid, ContainerService.operationType type)
  {
    long num;
    switch (type)
    {
      case ContainerService.operationType.ObjectType:
        this._containerObjectType.TryRemove(objectGuid, out num);
        this._containerObjectType.TryAdd(objectGuid, objId);
        break;
      case ContainerService.operationType.LCStep:
        this._containerLCStep.TryRemove(objectGuid, out num);
        this._containerLCStep.TryAdd(objectGuid, objId);
        break;
      case ContainerService.operationType.LCLevel:
        this._containerLCLevel.TryRemove(objectGuid, out num);
        this._containerLCLevel.TryAdd(objectGuid, objId);
        break;
    }
  }

  private IUserSession GetUserSession(object usrSession)
  {
    switch (usrSession)
    {
      case IUserSession _:
        return usrSession as IUserSession;
      case Guid sessionGUID:
        return UserSession.GetSessionByID(sessionGUID);
      case string _:
        return UserSession.GetSessionByID(new Guid((string) usrSession));
      default:
        return (IUserSession) null;
    }
  }

  private long FindObjId(
    IUserSession usrSession,
    ConcurrentDictionary<Guid, long> dict,
    Guid objectGuid,
    ContainerService.operationType type)
  {
    long objId = -1;
    if (!this._CacheLoaded)
      this.LoadCache(usrSession);
    if (!dict.TryGetValue(objectGuid, out objId))
      objId = this.FindObjectInDB(usrSession, objectGuid, dict, type);
    return objId;
  }

  private IDBObject GetContainer(
    object session,
    Guid objectGuid,
    bool create,
    ContainerService.operationType type)
  {
    IUserSession userSession = this.GetUserSession(session);
    long objectID = -1;
    switch (type)
    {
      case ContainerService.operationType.ObjectType:
        objectID = this.FindObjId(userSession, this._containerObjectType, objectGuid, type);
        break;
      case ContainerService.operationType.LCStep:
        objectID = this.FindObjId(userSession, this._containerLCStep, objectGuid, type);
        break;
      case ContainerService.operationType.LCLevel:
        objectID = this.FindObjId(userSession, this._containerLCLevel, objectGuid, type);
        break;
    }
    if (objectID != -1L)
    {
      IDBObject container = userSession.GetObject(objectID, false);
      if (container != null)
        return container;
    }
    return !create ? (IDBObject) null : this.CreateContainer(userSession, objectGuid, type);
  }

  private long FindObjectInDB(IUserSession session, Guid stepGuid, Guid objectGuid)
  {
    long objectInDb = -1;
    ColumnDescriptor[] columns = new ColumnDescriptor[1]
    {
      new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0)
    };
    object[] objArray = new object[0];
    SortOrders[] sortOrdersArray = new SortOrders[0];
    string str = stepGuid.ToString() + (object) objectGuid;
    ConditionStructure conditionStructure = new ConditionStructure(new Guid("cad00922-306c-11d8-b4e9-00304f19f545"), RelationalOperators.Equal, (object) str, LogicalOperators.AND, 0);
    DataTable dataTable = session.GetObjectCollection(new Guid("cad0013b-306c-11d8-b4e9-00304f19f545")).Select(new DBRecordSetParams(new ConditionStructure[1]
    {
      conditionStructure
    }, columns));
    if (dataTable.Rows.Count > 0)
      objectInDb = Convert.ToInt64(dataTable.Rows[0][0]);
    this._containerLCStepObjectType.TryRemove(str, out long _);
    this._containerLCStepObjectType.TryAdd(str, objectInDb);
    return objectInDb;
  }

  private IDBObject CreateContainerLC4ObjectType(
    IUserSession session,
    Guid stepGuid,
    Guid objectGuid)
  {
    IDBObject containerLc4ObjectType = session.GetObjectCollection(new Guid("cad0013b-306c-11d8-b4e9-00304f19f545")).Create();
    string key = stepGuid.ToString() + objectGuid.ToString();
    if (containerLc4ObjectType.Attributes.AddAttribute(session.GetAttributeType(new Guid("cad00922-306c-11d8-b4e9-00304f19f545")).AttributeID, true, new object[1]
    {
      (object) key
    }) != null)
    {
      containerLc4ObjectType.Caption = $"Контейнер для шага '{session.GetLifecycleStep(stepGuid).LCName}' у типа объектов '{session.GetObjectType(objectGuid).ObjectTypeName}'";
      containerLc4ObjectType.OwnerID = session.IdentHelper.SystemID;
      containerLc4ObjectType.CommitCreation(true);
      this._containerLCStepObjectType.TryRemove(key, out long _);
      this._containerLCStepObjectType.TryAdd(key, containerLc4ObjectType.ObjectID);
      return containerLc4ObjectType;
    }
    containerLc4ObjectType.Delete(0L);
    return (IDBObject) null;
  }

  private long FindObjId(IUserSession usrSession, Guid stepGuid, Guid objectGuid)
  {
    long objId = -1;
    if (!this._CacheLoaded)
      this.LoadCache(usrSession);
    if (!this._containerLCStepObjectType.TryGetValue(stepGuid.ToString() + objectGuid.ToString(), out objId))
      objId = this.FindObjectInDB(usrSession, stepGuid, objectGuid);
    return objId;
  }

  public IDBObject GetContainerForLCStepObjectType(
    object session,
    Guid LCStepGuid,
    Guid ObjectTypeGuid,
    bool createIfNotExist)
  {
    IUserSession userSession = this.GetUserSession(session);
    long objId = this.FindObjId(userSession, LCStepGuid, ObjectTypeGuid);
    if (objId != -1L)
    {
      IDBObject lcStepObjectType = userSession.GetObject(objId);
      if (lcStepObjectType != null)
        return lcStepObjectType;
    }
    return !createIfNotExist ? this.GetContainer((object) userSession, LCStepGuid, false, ContainerService.operationType.LCStep) : this.CreateContainerLC4ObjectType(userSession, LCStepGuid, ObjectTypeGuid);
  }

  public IDBObject GetContainerForLCStepObjectType(
    object session,
    Guid LCStepGuid,
    Guid ObjectTypeGuid)
  {
    return this.GetContainerForLCStepObjectType(session, LCStepGuid, ObjectTypeGuid, false);
  }

  public IDBObject GetContainerForLCStepObjectType(
    object session,
    int LCStepID,
    int ObjectTypeID,
    bool createIfNotExist)
  {
    IUserSession userSession = this.GetUserSession(session);
    DataRow dataRow1 = (userSession as UserSession).DBCache.GetTable("IMS_LC_STEPS").Rows.Find((object) LCStepID);
    DataRow dataRow2 = (userSession as UserSession).DBCache.GetTable("IMS_OBJECT_TYPES").Rows.Find((object) ObjectTypeID);
    if (dataRow1 == null || dataRow2 == null)
      return (IDBObject) null;
    IDBLifecycleStep lifecycleStep = userSession.GetLifecycleStep(LCStepID);
    IDBObjectType objectType = userSession.GetObjectType(ObjectTypeID);
    return lifecycleStep == null || objectType == null ? (IDBObject) null : this.GetContainerForLCStepObjectType(session, ((IDBGuid) lifecycleStep).GUID, ((IDBGuid) objectType).GUID, createIfNotExist);
  }

  public IDBObject GetContainerForLCStepObjectType(object session, int LCStepID, int ObjectTypeID)
  {
    return this.GetContainerForLCStepObjectType(session, LCStepID, ObjectTypeID, false);
  }

  public IDBObject GetContainerForObjectType(
    object session,
    Guid objectTypeGuid,
    bool createIfNotExist)
  {
    return this.GetContainer(session, objectTypeGuid, createIfNotExist, ContainerService.operationType.ObjectType);
  }

  public IDBObject GetContainerForObjectType(object session, Guid objectTypeGuid)
  {
    return this.GetContainerForObjectType(session, objectTypeGuid, false);
  }

  public IDBObject GetContainerForObjectType(
    object session,
    int objectTypeID,
    bool createIfNotExist)
  {
    IDBObjectType objectType = this.GetUserSession(session).GetObjectType(objectTypeID, true);
    return this.GetContainerForObjectType(session, (objectType as IDBGuid).GUID, createIfNotExist);
  }

  public IDBObject GetContainerForObjectType(object session, int objectTypeID)
  {
    return this.GetContainerForObjectType(session, objectTypeID, false);
  }

  public IDBObject GetContainerForLCStep(object session, Guid LCStepGuid, bool createIfNotExist)
  {
    return this.GetContainer(session, LCStepGuid, createIfNotExist, ContainerService.operationType.LCStep);
  }

  public IDBObject GetContainerForLCStep(object session, Guid LCStepGuid)
  {
    return this.GetContainerForLCStep(session, LCStepGuid, false);
  }

  public IDBObject GetContainerForLCStep(object session, int LCStepID, bool createIfNotExist)
  {
    Guid lcStepGuid = MetaDataHelper.GetLCStepGuid(LCStepID);
    return !(lcStepGuid != Guid.Empty) ? (IDBObject) null : this.GetContainerForLCStep(session, lcStepGuid, createIfNotExist);
  }

  public IDBObject GetContainerForLCStep(object session, int LCStepID)
  {
    return this.GetContainerForLCStep(session, LCStepID, false);
  }

  public IDBObject GetContainerForLCLevel(object session, Guid LCLevelGuid, bool createIfNotExist)
  {
    return this.GetContainer(session, LCLevelGuid, createIfNotExist, ContainerService.operationType.LCLevel);
  }

  public IDBObject GetContainerForLCLevel(object session, Guid LCLevelGuid)
  {
    return this.GetContainerForLCLevel(session, LCLevelGuid, false);
  }

  public IDBObject GetContainerForLCLevel(object session, int LCLevelID, bool createIfNotExist)
  {
    Guid LCLevelGuid = MetaDataHelper.GetLCLevelGuid(LCLevelID);
    if (LCLevelGuid == Guid.Empty)
    {
      IDBLifecycleLevelType lifecycleLevel = this.GetUserSession(session).GetLifecycleLevel(LCLevelID, false);
      LCLevelGuid = lifecycleLevel == null ? Guid.Empty : lifecycleLevel.GUID;
    }
    return !(LCLevelGuid != Guid.Empty) ? (IDBObject) null : this.GetContainerForLCLevel(session, LCLevelGuid, createIfNotExist);
  }

  public IDBObject GetContainerForLCLevel(object session, int LCLevelID)
  {
    return this.GetContainerForLCLevel(session, LCLevelID, false);
  }

  public void DeleteContainerForObjectType(object session, Guid objectTypeGuid)
  {
    this.GetContainerForObjectType(session, objectTypeGuid, false)?.Delete(0L);
  }

  public void DeleteContainerForLCLevel(object session, Guid LCLevelGuid)
  {
    this.GetContainerForLCLevel(session, LCLevelGuid, false)?.Delete(0L);
  }

  public void DeleteContainerForLCStep(object session, Guid LCStepGuid)
  {
    this.GetContainerForLCStep(session, LCStepGuid, false)?.Delete(0L);
  }

  public void DeleteContainerForLCStepObjectType(
    object session,
    Guid LCStepGuid,
    Guid ObjectTypeGuid)
  {
    this.GetContainerForLCStepObjectType(session, LCStepGuid, ObjectTypeGuid, false)?.Delete(0L);
  }

  internal void DeleteContainerFromCache(long containerID)
  {
    this.RemoveContainerFromCache(this._containerObjectType, containerID);
    this.RemoveContainerFromCache(this._containerLCStep, containerID);
    this.RemoveContainerFromCache(this._containerLCLevel, containerID);
    foreach (KeyValuePair<string, long> keyValuePair in this._containerLCStepObjectType.Where<KeyValuePair<string, long>>((System.Func<KeyValuePair<string, long>, bool>) (kvp => kvp.Value.Equals(containerID))))
      this._containerLCStepObjectType.TryRemove(keyValuePair.Key, out long _);
  }

  private void RemoveContainerFromCache(ConcurrentDictionary<Guid, long> dict, long containerID)
  {
    foreach (KeyValuePair<Guid, long> keyValuePair in dict.Where<KeyValuePair<Guid, long>>((System.Func<KeyValuePair<Guid, long>, bool>) (kvp => kvp.Value.Equals(containerID))))
      dict.TryRemove(keyValuePair.Key, out long _);
  }

  public void ReloadCache(IUserSession userSession) => this.LoadCache(userSession);

  private enum operationType
  {
    ObjectType,
    LCStep,
    LCLevel,
    LCStepObjectType,
  }
}
