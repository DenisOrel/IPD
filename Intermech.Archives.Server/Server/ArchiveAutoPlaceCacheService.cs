// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.Server.ArchiveAutoPlaceCacheService
// Assembly: Intermech.Archives.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2799C6CB-9B1D-4DB5-A12D-8C5FBFCAD6E5
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Archives.Server.dll

using Intermech.Archives.Common;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Timers;

#nullable disable
namespace Intermech.Archives.Server;

internal class ArchiveAutoPlaceCacheService : LongLifeObject, IArchiveAutoPlaceCacheService
{
  private ConcurrentDictionary<long, TypesAndUsers> _archiveAutoPlaceSettingsStorage;
  private readonly Dictionary<TypesAndUsers, Tuple<long, DateTime>> _archiveForUserTypeStorage;
  private readonly Dictionary<long, Tuple<Dictionary<long, int>, Dictionary<long, int>, DateTime>> _userGroupsAndUnitsStorage;
  private readonly Dictionary<int, int> _docTypesWeights;
  private Timer _cleanCacheTimer;
  private const int TimerInterval = 28800000;
  internal ArchiveAutoPlaceCacheSynchronizer ServersSynchronizer;

  public ArchiveAutoPlaceCacheService()
  {
    this._archiveAutoPlaceSettingsStorage = new ConcurrentDictionary<long, TypesAndUsers>();
    this._archiveForUserTypeStorage = new Dictionary<TypesAndUsers, Tuple<long, DateTime>>();
    this._userGroupsAndUnitsStorage = new Dictionary<long, Tuple<Dictionary<long, int>, Dictionary<long, int>, DateTime>>();
    this._docTypesWeights = new Dictionary<int, int>();
    this._cleanCacheTimer = new Timer(28800000.0)
    {
      AutoReset = true
    };
    this._cleanCacheTimer.Elapsed += new ElapsedEventHandler(this.ClearCaches);
    this._cleanCacheTimer.Start();
  }

  public void FillCache()
  {
    ConcurrentDictionary<long, TypesAndUsers> concurrentDictionary = new ConcurrentDictionary<long, TypesAndUsers>();
    if (!(ServerServices.GetService(typeof (IDBTimedEvents)) is IDBTimedEvents service))
      return;
    IUserSession session = (IUserSession) null;
    try
    {
      session = service.GetSystemSessionTemporaryClone(nameof (ArchiveAutoPlaceCacheService));
      List<long> longList = this.ReadAllArchiveIDs(session);
      foreach (IDBObject archiveObj in session.GetObjects(longList.ToArray(), false))
      {
        List<long> userIDs = ArchiveAutoPlaceCacheService.ReadUsersIDs(archiveObj);
        TypesAndUsers typesAndUsers = new TypesAndUsers(this.ReadDocTypesIDs(archiveObj), userIDs);
        concurrentDictionary.AddOrUpdate(archiveObj.ObjectID, typesAndUsers, (Func<long, TypesAndUsers, TypesAndUsers>) ((key, oldValue) => typesAndUsers));
      }
      this._archiveAutoPlaceSettingsStorage = concurrentDictionary;
      this.FillDocTypeWeightsFromBase();
    }
    finally
    {
      session?.Logout(nameof (ArchiveAutoPlaceCacheService));
    }
  }

  public void SaveAutoPlaceSettingsInCache(
    long archiveID,
    List<int> docTypesIDs,
    List<long> usersIDs)
  {
    TypesAndUsers typesAndUsers = new TypesAndUsers(docTypesIDs, usersIDs);
    this._archiveAutoPlaceSettingsStorage.AddOrUpdate(archiveID, typesAndUsers, (Func<long, TypesAndUsers, TypesAndUsers>) ((key, oldValue) => typesAndUsers));
    this.ClearCaches((object) this, (ElapsedEventArgs) null);
    if (!(ServerServices.GetService(typeof (IDBTimedEvents)) is IDBTimedEvents service))
      return;
    IUserSession userSession = (IUserSession) null;
    try
    {
      userSession = service.GetSystemSessionTemporaryClone(nameof (ArchiveAutoPlaceCacheService));
      this.ServersSynchronizer.FireReloadCacheEvent(string.Empty, (userSession as UserSession).DataManager);
    }
    finally
    {
      userSession?.Logout(nameof (ArchiveAutoPlaceCacheService));
    }
  }

  public long GetArchiveIdFromCaсhe(int docTypeID, long userID, Guid sessionGuid)
  {
    lock (this._archiveForUserTypeStorage)
    {
      foreach (KeyValuePair<TypesAndUsers, Tuple<long, DateTime>> keyValuePair in this._archiveForUserTypeStorage)
      {
        if (keyValuePair.Key.UserIDs[0] == userID && keyValuePair.Key.DocTypeIDs[0] == docTypeID)
          return keyValuePair.Value.Item1;
      }
    }
    Dictionary<long, int> collection1 = new Dictionary<long, int>();
    Dictionary<long, int> collection2 = new Dictionary<long, int>();
    lock (this._userGroupsAndUnitsStorage)
    {
      foreach (KeyValuePair<long, Tuple<Dictionary<long, int>, Dictionary<long, int>, DateTime>> keyValuePair in this._userGroupsAndUnitsStorage)
      {
        if (keyValuePair.Key == userID)
        {
          collection1.AddRange<KeyValuePair<long, int>>((IEnumerable<KeyValuePair<long, int>>) keyValuePair.Value.Item1);
          collection2.AddRange<KeyValuePair<long, int>>((IEnumerable<KeyValuePair<long, int>>) keyValuePair.Value.Item2);
        }
      }
      if (collection1.Count == 0)
      {
        if (collection2.Count == 0)
        {
          collection1.AddRange<KeyValuePair<long, int>>((IEnumerable<KeyValuePair<long, int>>) this.FillGroupWeightsFromBase(sessionGuid, userID));
          collection1.AddRange<KeyValuePair<long, int>>((IEnumerable<KeyValuePair<long, int>>) this.FillDepartmentsWeightsFromBase(sessionGuid, userID));
          this._userGroupsAndUnitsStorage.Add(userID, new Tuple<Dictionary<long, int>, Dictionary<long, int>, DateTime>(collection1, collection2, DateTime.Now));
        }
      }
    }
    List<ArchiveAutoPlaceCacheService.FindedUserTypeResult> source = new List<ArchiveAutoPlaceCacheService.FindedUserTypeResult>();
    foreach (KeyValuePair<long, TypesAndUsers> keyValuePair in this._archiveAutoPlaceSettingsStorage)
    {
      lock (keyValuePair.Value)
      {
        List<long> userIds = keyValuePair.Value.UserIDs;
        List<long> list1 = userIds.Intersect<long>((IEnumerable<long>) collection1.Keys).ToList<long>();
        List<long> list2 = userIds.Intersect<long>((IEnumerable<long>) collection2.Keys).ToList<long>();
        if (list1.Count <= 0)
        {
          if (list2.Count <= 0)
            continue;
        }
        List<int> docTypeIds = keyValuePair.Value.DocTypeIDs;
        List<int> objectTypeParentsId = MetaDataHelper.GetObjectTypeParentsID(docTypeID);
        objectTypeParentsId.Add(docTypeID);
        List<int> second = objectTypeParentsId;
        List<int> list3 = docTypeIds.Intersect<int>((IEnumerable<int>) second).ToList<int>();
        if (list3.Any<int>())
        {
          ArchiveAutoPlaceCacheService.FindedUserTypeResult findedUserTypeResult = new ArchiveAutoPlaceCacheService.FindedUserTypeResult();
          findedUserTypeResult.ArchiveID = keyValuePair.Key;
          foreach (long key in list1)
          {
            int num;
            if (collection1.TryGetValue(key, out num) && (findedUserTypeResult.UserGroupWeight == -1 || findedUserTypeResult.UserGroupWeight > num))
            {
              findedUserTypeResult.UserGroupWeight = num;
              findedUserTypeResult.UserGroupID = key;
            }
          }
          foreach (long key in list2)
          {
            int num;
            if (collection2.TryGetValue(key, out num) && (findedUserTypeResult.DepartmentWeight == -1 || findedUserTypeResult.DepartmentWeight > num))
            {
              findedUserTypeResult.DepartmentWeight = num;
              findedUserTypeResult.DepartmentID = key;
            }
          }
          lock (this._docTypesWeights)
          {
            foreach (int key in list3)
            {
              int num;
              if (this._docTypesWeights.TryGetValue(key, out num) && (findedUserTypeResult.DocTypeWeight == -1 || findedUserTypeResult.DocTypeWeight < num))
              {
                findedUserTypeResult.DocTypeWeight = num;
                findedUserTypeResult.DocTypeID = key;
              }
            }
          }
          source.Add(findedUserTypeResult);
        }
      }
    }
    if (source.Count == 0)
      return 0;
    source.Sort();
    ArchiveAutoPlaceCacheService.FindedUserTypeResult findedUserTypeResult1 = source.Last<ArchiveAutoPlaceCacheService.FindedUserTypeResult>();
    lock (this._archiveForUserTypeStorage)
      this._archiveForUserTypeStorage.Add(new TypesAndUsers(new List<int>()
      {
        docTypeID
      }, new List<long>() { userID }), new Tuple<long, DateTime>(findedUserTypeResult1.ArchiveID, DateTime.Now));
    return findedUserTypeResult1.ArchiveID;
  }

  public Dictionary<long, TypesAndUsers> FindArchiveSettingsIntersections(
    long archID,
    List<int> typeIDs,
    List<long> userIDs,
    out List<int> wrongTypeIDs,
    out List<long> wrongUsersIDs)
  {
    Dictionary<long, TypesAndUsers> settingsIntersections = new Dictionary<long, TypesAndUsers>();
    wrongTypeIDs = new List<int>();
    wrongUsersIDs = new List<long>();
    foreach (long key in (IEnumerable<long>) this._archiveAutoPlaceSettingsStorage.Keys)
    {
      TypesAndUsers typesAndUsers;
      if (key != archID && this._archiveAutoPlaceSettingsStorage.TryGetValue(key, out typesAndUsers))
      {
        lock (typesAndUsers)
        {
          List<int> docTypeIds = typesAndUsers.DocTypeIDs;
          List<long> userIds = typesAndUsers.UserIDs;
          List<int> second = typeIDs;
          List<int> list1 = docTypeIds.Intersect<int>((IEnumerable<int>) second).ToList<int>();
          List<long> list2 = userIds.Intersect<long>((IEnumerable<long>) userIDs).ToList<long>();
          if (list1.Any<int>())
          {
            if (list2.Any<long>())
            {
              settingsIntersections.Add(key, new TypesAndUsers(list1, list2));
              wrongTypeIDs.AddRange((IEnumerable<int>) list1);
              wrongUsersIDs.AddRange((IEnumerable<long>) list2);
            }
          }
        }
      }
    }
    wrongTypeIDs = wrongTypeIDs.Distinct<int>().ToList<int>();
    wrongUsersIDs = wrongUsersIDs.Distinct<long>().ToList<long>();
    return settingsIntersections;
  }

  public void DeleteArchiveFromCache(long archObjectID)
  {
    this._archiveAutoPlaceSettingsStorage.TryRemove(archObjectID, out TypesAndUsers _);
  }

  private List<int> ReadDocTypesIDs(IDBObject archiveObj)
  {
    List<int> intList = new List<int>();
    IDBAttribute attributeByGuid = archiveObj.GetAttributeByGuid(ConstsHolder.AutoPlaceDocTypesAttrGuid);
    if (attributeByGuid != null && (attributeByGuid.ValuesCount != 1 || !attributeByGuid.IsNull))
    {
      string[] descriptions = attributeByGuid.Descriptions;
      for (int index = 0; index < descriptions.Length; ++index)
      {
        if (!string.IsNullOrEmpty(descriptions[index]))
        {
          int objectTypeId = MetaDataHelper.GetObjectTypeID(new Guid(descriptions[index]));
          if (objectTypeId != -1)
            intList.Add(objectTypeId);
        }
      }
    }
    return intList;
  }

  private static List<long> ReadUsersIDs(IDBObject archiveObj)
  {
    List<long> longList = new List<long>();
    IDBAttribute attributeByGuid = archiveObj.GetAttributeByGuid(ConstsHolder.UsersCanAutoPlaceDocsAttrGuid);
    if (attributeByGuid != null && (attributeByGuid.ValuesCount != 1 || !attributeByGuid.IsNull))
    {
      for (int index = 0; index < attributeByGuid.ValuesCount; ++index)
      {
        if (!(attributeByGuid.Values[index] is DBNull))
        {
          long int64 = Convert.ToInt64(attributeByGuid.Values[index]);
          if (int64 != 0L)
            longList.Add(int64);
        }
      }
    }
    return longList;
  }

  private List<long> ReadAllArchiveIDs(IUserSession session)
  {
    List<long> longList = new List<long>();
    session.ShowPersonalObjects = true;
    DBRecordSetParams dbRecordSetParams = new DBRecordSetParams((ConditionStructure[]) null, new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    });
    DataTable dataTable = session.ObjectsSelect(ConstsHolder.ArcTypeID, dbRecordSetParams);
    if (dataTable != null && dataTable.Rows.Count > 0)
    {
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        if (row[0] != DBNull.Value)
        {
          long int64 = Convert.ToInt64(row[0]);
          if (int64 != 0L)
            longList.Add(int64);
        }
      }
    }
    return longList;
  }

  private void ClearCaches(object sender, ElapsedEventArgs e)
  {
    switch (sender)
    {
      case ArchiveAutoPlaceCacheService _:
        this.ClearTemporaryCashesCompletely();
        break;
      case Timer _:
        this.ClearTemporaryCachesFromOldData();
        break;
    }
  }

  private void ClearTemporaryCashesCompletely()
  {
    lock (this._archiveForUserTypeStorage)
      this._archiveForUserTypeStorage.Clear();
    lock (this._userGroupsAndUnitsStorage)
      this._userGroupsAndUnitsStorage.Clear();
  }

  private void ClearTemporaryCachesFromOldData()
  {
    lock (this._archiveForUserTypeStorage)
    {
      Dictionary<TypesAndUsers, Tuple<long, DateTime>> collection = new Dictionary<TypesAndUsers, Tuple<long, DateTime>>();
      collection.AddRange<KeyValuePair<TypesAndUsers, Tuple<long, DateTime>>>((IEnumerable<KeyValuePair<TypesAndUsers, Tuple<long, DateTime>>>) this._archiveForUserTypeStorage);
      foreach (KeyValuePair<TypesAndUsers, Tuple<long, DateTime>> keyValuePair in collection)
      {
        if (DateTime.Now.Subtract(keyValuePair.Value.Item2).TotalMilliseconds > 28800000.0)
          this._archiveForUserTypeStorage.Remove(keyValuePair.Key);
      }
    }
    lock (this._userGroupsAndUnitsStorage)
    {
      Dictionary<long, Tuple<Dictionary<long, int>, Dictionary<long, int>, DateTime>> collection = new Dictionary<long, Tuple<Dictionary<long, int>, Dictionary<long, int>, DateTime>>();
      collection.AddRange<KeyValuePair<long, Tuple<Dictionary<long, int>, Dictionary<long, int>, DateTime>>>((IEnumerable<KeyValuePair<long, Tuple<Dictionary<long, int>, Dictionary<long, int>, DateTime>>>) this._userGroupsAndUnitsStorage);
      foreach (KeyValuePair<long, Tuple<Dictionary<long, int>, Dictionary<long, int>, DateTime>> keyValuePair in collection)
      {
        if (DateTime.Now.Subtract(keyValuePair.Value.Item3).TotalMilliseconds > 28800000.0)
          this._userGroupsAndUnitsStorage.Remove(keyValuePair.Key);
      }
    }
  }

  private void FillDocTypeWeightsFromBase()
  {
    lock (this._docTypesWeights)
    {
      this._docTypesWeights.Clear();
      int i = 0;
      this._docTypesWeights.Add(ConstsHolder.DocTypeID, i);
      this.AddDocTypeChildrensWeights(ConstsHolder.DocTypeID, i);
    }
  }

  private void AddDocTypeChildrensWeights(int docTypeID, int i)
  {
    ++i;
    foreach (int num in MetaDataHelper.GetObjectTypeChildrenID(docTypeID))
    {
      this._docTypesWeights.Add(num, i);
      this.AddDocTypeChildrensWeights(num, i);
    }
  }

  private Dictionary<long, int> FillDepartmentsWeightsFromBase(Guid sessionGuid, long userID)
  {
    Dictionary<long, int> departmentsDict = new Dictionary<long, int>();
    if (!(UserSession.GetSessionByID(sessionGuid) is UserSession sessionById))
      return departmentsDict;
    int weight1 = 0;
    this.AddParentDepartments(departmentsDict, userID, weight1, (IUserSession) sessionById);
    List<long> list = ((IEnumerable<long>) sessionById.DBSecurity.GetGroupsListRecursive()).ToList<long>();
    list.Remove(sessionById.RoleID);
    list.Remove(userID);
    int weight2 = weight1 + 1;
    foreach (long objectID in list)
      this.AddParentDepartments(departmentsDict, objectID, weight2, (IUserSession) sessionById);
    return departmentsDict;
  }

  private void AddParentDepartments(
    Dictionary<long, int> departmentsDict,
    long objectID,
    int weight,
    IUserSession session)
  {
    IDBRelationCollection relationCollection = session.GetRelationCollection(MetaDataHelper.GetRelationTypeID(new Guid("cad00022-306c-11d8-b4e9-00304f19f545")));
    relationCollection.ObjectTypeID = MetaDataHelper.GetObjectTypeID("cadd9232-306c-11d8-b4e9-00304f19f545");
    DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[1]
    {
      new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0)
    })
    {
      Tags = new HybridDictionary()
    };
    paramSet.Tags[(object) "{7FB30639-2F65-4407-B78E-523547B1B133}"] = (object) false;
    DataTable dataTable = relationCollection.EntersInVersion(paramSet, objectID);
    if (dataTable == null)
      return;
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      long int64Value = DataSetProcessor.GetInt64Value(dataTable.Rows[index][0], 0L);
      if (int64Value != 0L && !departmentsDict.ContainsKey(int64Value))
      {
        departmentsDict.Add(int64Value, weight);
        this.AddParentDepartments(departmentsDict, int64Value, weight + 1, session);
      }
    }
  }

  private Dictionary<long, int> FillGroupWeightsFromBase(Guid sessionGuid, long userID)
  {
    Dictionary<long, int> groupsDict = new Dictionary<long, int>()
    {
      {
        userID,
        0
      }
    };
    if (!(UserSession.GetSessionByID(sessionGuid) is UserSession sessionById))
      return groupsDict;
    List<long> list = ((IEnumerable<long>) sessionById.DBSecurity.GetGroupsList()).ToList<long>();
    list.Remove(sessionById.RoleID);
    list.Remove(userID);
    foreach (long num in list)
    {
      int weight = 1;
      if (!groupsDict.Keys.Contains<long>(num))
      {
        groupsDict.Add(num, weight);
        this.AddParentGroups(groupsDict, num, weight, sessionById);
      }
    }
    return groupsDict;
  }

  private void AddParentGroups(
    Dictionary<long, int> groupsDict,
    long groupID,
    int weight,
    UserSession session)
  {
    ++weight;
    IDBRelationCollection relationCollection = session.GetRelationCollection(MetaDataHelper.GetRelationTypeID(new Guid("cad00022-306c-11d8-b4e9-00304f19f545")));
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(-7, RelationalOperators.Equal, (object) session.IdentHelper.GroupsTypeID, LogicalOperators.NONE, 0, true)
    }, new ColumnDescriptor[1]
    {
      new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0)
    })
    {
      Tags = new HybridDictionary()
    };
    paramSet.Tags[(object) "{7FB30639-2F65-4407-B78E-523547B1B133}"] = (object) false;
    DataTable dataTable = relationCollection.EntersInVersion(paramSet, groupID);
    if (dataTable == null)
      return;
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      long int64Value = DataSetProcessor.GetInt64Value(dataTable.Rows[index][0], 0L);
      if (int64Value != 0L && !groupsDict.ContainsKey(int64Value))
      {
        groupsDict.Add(int64Value, weight);
        this.AddParentGroups(groupsDict, int64Value, weight, session);
      }
    }
  }

  protected class FindedUserTypeResult : 
    IComparable<ArchiveAutoPlaceCacheService.FindedUserTypeResult>
  {
    public long ArchiveID;
    public long UserGroupID;
    public int DocTypeID = -1;
    public long DepartmentID;
    public int DepartmentWeight = -1;
    public int UserGroupWeight = -1;
    public int DocTypeWeight = -1;

    public int CompareTo(
      ArchiveAutoPlaceCacheService.FindedUserTypeResult obj)
    {
      if (this.UserGroupWeight > -1 && obj.UserGroupWeight == -1)
        return 1;
      if (this.UserGroupWeight == -1 && obj.UserGroupWeight > -1 || this.UserGroupWeight > obj.UserGroupWeight)
        return -1;
      if (this.UserGroupWeight < obj.UserGroupWeight)
        return 1;
      if (this.UserGroupWeight != -1 && this.UserGroupWeight == obj.UserGroupWeight)
      {
        if (this.DocTypeWeight > obj.DocTypeWeight)
          return 1;
        return this.DocTypeWeight < obj.DocTypeWeight ? -1 : 0;
      }
      if (this.UserGroupWeight == -1 && obj.UserGroupWeight == -1)
      {
        if (this.DepartmentWeight < obj.DepartmentWeight)
          return 1;
        if (this.DepartmentWeight > obj.DepartmentWeight)
          return -1;
        if (this.DepartmentWeight == obj.DepartmentWeight)
        {
          if (this.DocTypeWeight > obj.DocTypeWeight)
            return 1;
          return this.DocTypeWeight < obj.DocTypeWeight ? -1 : 0;
        }
      }
      return 0;
    }
  }
}
