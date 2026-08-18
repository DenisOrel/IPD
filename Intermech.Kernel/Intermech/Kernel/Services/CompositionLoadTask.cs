// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.CompositionLoadTask
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Compositions.CompositionService;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using Intermech.Kernel.Services.Compositions.Loading;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;


namespace Intermech.Kernel.Services;

public class CompositionLoadTask
{
  public const string _cnt_fld_PartObjID = "F_PART_OBJ_ID";
  public const string _cnt_fld_PhysicalQuantityID = "_colPhysicalQuantityID";
  protected IUserSession _session;
  protected ICompositionLoadService _compositionService;
  protected CompositionLoadingParams _loadingParams;
  protected HashSet<long> _taskObjectCache;
  protected HashSet<int> _searchObjectTypeCache;
  protected HashSet<int> _expandObjectTypeCache;
  protected bool _relManualSortAllowed = true;
  protected HybridDictionary _dbTags;
  protected int _col_idx_ID = -1;
  protected int _col_idx_ObjID = -1;
  protected int _col_idx_ObjectType = -1;
  protected int _col_idx_QuantityNum = -1;
  protected int _col_idx_ProjID = -1;
  protected int _col_idx_PrjLink = -1;
  protected int _col_idx_RelType = -1;
  protected int _col_idx_PartID = -1;
  protected int _col_idx_PartObjectID = -1;
  protected int _col_idx_PhysicalQuantityID = -1;
  protected List<ColumnDescriptor> _columns;
  protected List<int> _relationColumns;
  protected bool _needCalcQuantity;
  protected bool _needBlockConfigureComposition = true;
  protected long _modificationID;
  protected HashSet<Tuple<long, long>> _resultRelationIDs;
  protected bool _hasObjectCustomAttrs;
  protected IList<int> _serviceColumns = (IList<int>) new List<int>();
  protected CompositionLoadTask.ObjTypeHelper _objTypeHelper;
  protected Dictionary<int, bool> _expediencyTypesCache = new Dictionary<int, bool>();
  protected Dictionary<long, long> _measureDescriptors = new Dictionary<long, long>();
  protected Dictionary<string, MeasuredValue> _measureCache = new Dictionary<string, MeasuredValue>();

  private IList<DataRow> RecursiveNodes(
    Dictionary<long, ObjectMeasuredInfo> rootObjects,
    List<CompositionRootPath> paths)
  {
    List<DataRow> resultDataRows = new List<DataRow>();
    if (this.Terminated)
      return (IList<DataRow>) resultDataRows;
    List<long> rootObjectIDs = new List<long>(rootObjects.Count);
    List<int> list = new List<int>(rootObjects.Count);
    foreach (KeyValuePair<long, ObjectMeasuredInfo> rootObject in rootObjects)
    {
      rootObjectIDs.Add(rootObject.Key);
      list.Add(rootObject.Value.ObjectType);
    }
    GenericListHelper.MakeUnique<int>(list);
    List<int> enabledRelationTypes = this._objTypeHelper.GetEnabledRelationTypes(list.ToArray(), this._loadingParams.Composition);
    bool flag = true;
    if (!this._needBlockConfigureComposition && this._loadingParams.Composition)
    {
      foreach (int searchRelationType in this._loadingParams.SearchRelationTypes)
      {
        if (enabledRelationTypes.Contains(searchRelationType) && MetaDataHelper.IsPdmConfigurableRelationType(searchRelationType))
        {
          flag = false;
          break;
        }
      }
    }
    if (flag)
    {
      if (this._loadingParams.DbParams != null)
      {
        if (rootObjectIDs.Count == 1)
          this._loadingParams.DbParams.TryGetValue(rootObjectIDs[0], out this._dbTags);
        else
          this._loadingParams.DbParams.TryGetValue(long.MaxValue, out this._dbTags);
      }
      else
        this._dbTags = new HybridDictionary();
      IList<DataRow> levelDataRows = (IList<DataRow>) this.RecursiveLevel(rootObjects, rootObjectIDs, 1, paths);
      if (levelDataRows != null && levelDataRows.Count > 0)
      {
        int physicalQuantityId = this._col_idx_PhysicalQuantityID;
        this.HandleLevelTable(ref resultDataRows, levelDataRows, physicalQuantityId);
      }
    }
    else
    {
      List<DataRow> levelDataRows = new List<DataRow>();
      foreach (KeyValuePair<long, ObjectMeasuredInfo> rootObject in rootObjects)
      {
        CompositionRootPath path = new CompositionRootPath(rootObject.Key);
        if (this._loadingParams.DbParams != null)
          this._loadingParams.DbParams.TryGetValue(rootObject.Key, out this._dbTags);
        else
          this._dbTags = new HybridDictionary();
        IList<DataRow> collection = (IList<DataRow>) this.RecursiveNode(rootObject.Key, rootObject.Value, 1, path);
        if (collection != null && collection.Count > 0)
          levelDataRows.AddRange((IEnumerable<DataRow>) collection);
      }
      if (levelDataRows.Count > 0)
      {
        int physicalQuantityId = this._col_idx_PhysicalQuantityID;
        this.HandleLevelTable(ref resultDataRows, (IList<DataRow>) levelDataRows, physicalQuantityId);
      }
    }
    return (IList<DataRow>) resultDataRows;
  }

  private List<DataRow> RecursiveNode(
    long rootObjectId,
    ObjectMeasuredInfo rootObjectInfo,
    int currentLevel,
    CompositionRootPath path)
  {
    Dictionary<long, ObjectMeasuredInfo> allChildObjects;
    List<long> allChildObjectIDs;
    List<CompositionRootPath> allChildPaths;
    List<DataRow> dataRowList = this.RecursiveNodeData(rootObjectId, rootObjectInfo, currentLevel, path, out allChildObjects, out allChildObjectIDs, out allChildPaths);
    if ((this._loadingParams.LoadLevels == -1 ? 1 : (this._loadingParams.LoadLevels > currentLevel ? 1 : 0)) != 0 && allChildObjectIDs.Count > 0)
      dataRowList.AddRange((IEnumerable<DataRow>) this.RecursiveLevel(allChildObjects, allChildObjectIDs, currentLevel + 1, allChildPaths));
    return dataRowList;
  }

  private List<DataRow> RecursiveNodeData(
    long rootObjectId,
    ObjectMeasuredInfo rootObjectInfo,
    int currentLevel,
    CompositionRootPath path,
    out Dictionary<long, ObjectMeasuredInfo> allChildObjects,
    out List<long> allChildObjectIDs,
    out List<CompositionRootPath> allChildPaths)
  {
    allChildPaths = new List<CompositionRootPath>();
    allChildObjectIDs = new List<long>();
    allChildObjects = new Dictionary<long, ObjectMeasuredInfo>();
    List<DataRow> dataRowList = new List<DataRow>();
    if (this.Terminated || rootObjectId == 0L || rootObjectId == -1L || rootObjectInfo == null)
      return dataRowList;
    List<int> bList = new List<int>();
    if (this._expandObjectTypeCache == null || this._expandObjectTypeCache.Contains(rootObjectInfo.ObjectType) || this._taskObjectCache.Contains(rootObjectId))
      bList.Add(rootObjectInfo.ObjectType);
    List<int> enabledRelationTypes = this._objTypeHelper.GetEnabledRelationTypes(bList.ToArray(), this._loadingParams.Composition);
    if (enabledRelationTypes.Count == 0)
      return dataRowList;
    bool flag1 = this._loadingParams.LoadLevels == -1 || this._loadingParams.LoadLevels > currentLevel;
    RelationPair dbTag = this._dbTags != null ? this._dbTags[(object) "{78D53C74-3CF7-4F48-94FC-80C4FCB0BA77}"] as RelationPair : (RelationPair) null;
    long userId = this._session.UserID;
    foreach (int searchRelationType in this._loadingParams.SearchRelationTypes)
    {
      if (enabledRelationTypes.Contains(searchRelationType))
      {
        List<int> resultData;
        GenericListHelper.GetDifference<int>((IList<int>) this._objTypeHelper.GetEnabledObjectTypes(searchRelationType, this._loadingParams.Composition), (IList<int>) bList, GenericListHelper.SearchMode.smExistInBoth, out resultData);
        if (resultData != null && resultData.Count != 0 && resultData.Contains(rootObjectInfo.ObjectType))
        {
          if (this._dbTags == null)
            this._dbTags = new HybridDictionary();
          this._dbTags[(object) "{004511C2-5AA8-4831-B60A-7CD17C1A2D88}"] = (object) new Dictionary<long, int>()
          {
            {
              rootObjectId,
              rootObjectInfo.ObjectType
            }
          };
          DataTable levelData = this.GetLevelData(new List<ObjInfoIDItem>()
          {
            new ObjInfoIDItem(rootObjectId, rootObjectInfo.ObjectType, rootObjectInfo.ID)
          }, searchRelationType, 1);
          this._dbTags[(object) "{004511C2-5AA8-4831-B60A-7CD17C1A2D88}"] = (object) null;
          if (levelData != null && levelData.Rows.Count != 0)
          {
            int count = levelData.Rows.Count;
            bool flag2 = !this._needBlockConfigureComposition && this._loadingParams.Composition && MetaDataHelper.IsPdmConfigurableRelationType(searchRelationType);
            Dictionary<long, ObjectMeasuredInfo> rootObjectIDs = new Dictionary<long, ObjectMeasuredInfo>(1)
            {
              {
                rootObjectId,
                rootObjectInfo
              }
            };
            Dictionary<long, ObjectMeasuredInfo> childObjects = new Dictionary<long, ObjectMeasuredInfo>(count);
            List<long> longList = new List<long>(count);
            Dictionary<Int96, DataRow> childObjectRows = new Dictionary<Int96, DataRow>(count);
            int colPhysicalQuantityId = levelData.Columns.IndexOf("_colPhysicalQuantityID");
            List<DataRow> collection1 = new List<DataRow>(count);
            if (flag1 && !flag2 && allChildPaths.Capacity < allChildPaths.Count + count)
              allChildPaths.Capacity = allChildPaths.Count + count;
            for (int index = 0; index < levelData.Rows.Count; ++index)
            {
              DataRow row = levelData.Rows[index];
              RelationPair relationPair = dbTag != null ? new RelationPair(dbTag.Handle, dbTag.TOP_OBJECT_ID, dbTag.TOP_OBJECT_TYPE, Convert.ToInt64(row[this._col_idx_PrjLink]), userId, Convert.ToInt64(row[this._col_idx_ObjID]), searchRelationType, Convert.ToInt32(row[this._col_idx_ObjectType])) : (RelationPair) null;
              if (this._dbTags == null)
                this._dbTags = new HybridDictionary();
              this._dbTags[(object) "{78D53C74-3CF7-4F48-94FC-80C4FCB0BA77}"] = (object) relationPair;
              long objectId;
              try
              {
                ObjectMeasuredInfo objectInfo;
                Int96 keyId;
                bool isLoop;
                if (this.HandleRow(rootObjectIDs, childObjects, longList, childObjectRows, row, colPhysicalQuantityId, out objectId, out objectInfo, out keyId, path, out isLoop))
                {
                  collection1.Add(row);
                  if (keyId != null)
                    childObjectRows.Add(keyId, row);
                }
                if (!isLoop & flag1 & flag2)
                {
                  if (this.CheckCompositionExpediency(objectInfo.ObjectType))
                  {
                    IList<DataRow> collection2 = (IList<DataRow>) this.RecursiveNode(objectId, objectInfo, currentLevel + 1, new CompositionRootPath(objectId, path));
                    if (collection2 != null)
                      collection1.AddRange((IEnumerable<DataRow>) collection2);
                  }
                }
              }
              finally
              {
                this._dbTags[(object) "{78D53C74-3CF7-4F48-94FC-80C4FCB0BA77}"] = (object) dbTag;
              }
              if (flag1 && !flag2)
                allChildPaths.Add(new CompositionRootPath(objectId, path));
            }
            if (flag1 && !flag2)
            {
              allChildObjectIDs.AddRange((IEnumerable<long>) longList);
              for (int index = 0; index < longList.Count; ++index)
              {
                if (!allChildObjects.ContainsKey(longList[index]))
                  allChildObjects.Add(longList[index], childObjects[longList[index]]);
              }
            }
            dataRowList.AddRange((IEnumerable<DataRow>) collection1);
          }
        }
      }
    }
    Thread.Sleep(0);
    return dataRowList;
  }

  private List<DataRow> RecursiveLevel(
    Dictionary<long, ObjectMeasuredInfo> rootObjects,
    List<long> rootObjectIDs,
    int currentLevel,
    List<CompositionRootPath> paths)
  {
    Dictionary<long, ObjectMeasuredInfo> allChildObjects;
    List<long> allChildObjectIDs;
    List<CompositionRootPath> allChildPaths;
    List<DataRow> dataRowList = this.RecursiveLevelData(rootObjects, rootObjectIDs, currentLevel, paths, out allChildObjects, out allChildObjectIDs, out allChildPaths);
    if ((this._loadingParams.LoadLevels == -1 ? 1 : (this._loadingParams.LoadLevels > currentLevel ? 1 : 0)) != 0 && allChildObjectIDs.Count > 0)
    {
      rootObjects = (Dictionary<long, ObjectMeasuredInfo>) null;
      rootObjectIDs = (List<long>) null;
      paths = (List<CompositionRootPath>) null;
      dataRowList.AddRange((IEnumerable<DataRow>) this.RecursiveLevel(allChildObjects, allChildObjectIDs, currentLevel + 1, allChildPaths));
    }
    return dataRowList;
  }

  private List<DataRow> RecursiveLevelData(
    Dictionary<long, ObjectMeasuredInfo> rootObjects,
    List<long> rootObjectIDs,
    int currentLevel,
    List<CompositionRootPath> paths,
    out Dictionary<long, ObjectMeasuredInfo> allChildObjects,
    out List<long> allChildObjectIDs,
    out List<CompositionRootPath> allChildPaths)
  {
    List<DataRow> dataRowList = new List<DataRow>(0);
    allChildPaths = new List<CompositionRootPath>();
    allChildObjectIDs = new List<long>();
    allChildObjects = new Dictionary<long, ObjectMeasuredInfo>();
    if (this.Terminated || rootObjectIDs == null || rootObjects == null)
      return dataRowList;
    List<int> bList = this._expandObjectTypeCache != null ? rootObjects.Where<KeyValuePair<long, ObjectMeasuredInfo>>((System.Func<KeyValuePair<long, ObjectMeasuredInfo>, bool>) (item => this._expandObjectTypeCache.Contains(item.Value.ObjectType) || this._taskObjectCache.Contains(item.Key))).Select<KeyValuePair<long, ObjectMeasuredInfo>, int>((System.Func<KeyValuePair<long, ObjectMeasuredInfo>, int>) (item => item.Value.ObjectType)).Distinct<int>().ToList<int>() : rootObjects.Values.Select<ObjectMeasuredInfo, int>((System.Func<ObjectMeasuredInfo, int>) (item => item.ObjectType)).Distinct<int>().ToList<int>();
    List<int> enabledRelationTypes = this._objTypeHelper.GetEnabledRelationTypes(bList.ToArray(), this._loadingParams.Composition);
    if (enabledRelationTypes.Count == 0)
      return dataRowList;
    bool flag1 = this._loadingParams.LoadLevels == -1 || this._loadingParams.LoadLevels > currentLevel;
    List<int> allEnabledObjTypes4RelType = new List<int>();
    List<int> resultData;
    foreach (int searchRelationType in this._loadingParams.SearchRelationTypes)
    {
      if (enabledRelationTypes.Contains(searchRelationType))
      {
        GenericListHelper.GetDifference<int>((IList<int>) this._objTypeHelper.GetEnabledObjectTypes(searchRelationType, this._loadingParams.Composition), (IList<int>) bList, GenericListHelper.SearchMode.smExistInBoth, out resultData);
        if (resultData != null && resultData.Count != 0)
          allEnabledObjTypes4RelType.AddRange((IEnumerable<int>) resultData);
      }
    }
    if (allEnabledObjTypes4RelType.Count == 0)
      return dataRowList;
    GenericListHelper.MakeUnique<int>(allEnabledObjTypes4RelType);
    long[] array = rootObjects.Where<KeyValuePair<long, ObjectMeasuredInfo>>((System.Func<KeyValuePair<long, ObjectMeasuredInfo>, bool>) (item => allEnabledObjTypes4RelType.BinarySearch(item.Value.ObjectType) >= 0)).Select<KeyValuePair<long, ObjectMeasuredInfo>, long>((System.Func<KeyValuePair<long, ObjectMeasuredInfo>, long>) (item => item.Key)).ToArray<long>();
    bool enForceSave = false;
    if (((UserSession) this._session).DataManager.DataProvider.Name == "Sql")
      enForceSave = ((IEnumerable<long>) array).Count<long>() > ((UserSession) this._session).DataManager.DataProvider.MaximumINOperands / 2;
    INConditionValue inConditionValue = (this._session as UserSession).QueryBuilder.StartINCondition((object) (this._loadingParams.Composition ? -21 : -22), (Array) array, enForceSave);
    try
    {
      Dictionary<int, List<int>> relType2ObjTypeLevelCache = (Dictionary<int, List<int>>) null;
      if (inConditionValue != null && (this._hasObjectCustomAttrs || this._loadingParams.SearchRelationTypes.Count<int>() > 4))
        (this.CompositionService as CompositionLoadService).GetPresentCompositionTypes((object) this._session, allChildObjectIDs.Count > (this._session as UserSession).DataManager.DataProvider.MaximumINOperands ? (object) inConditionValue : (object) array, (IEnumerable<int>) this._loadingParams.SearchRelationTypes.ToArray<int>(), this._loadingParams.Composition, out relType2ObjTypeLevelCache);
      if (relType2ObjTypeLevelCache != null && relType2ObjTypeLevelCache.Count == 0)
        return dataRowList;
      Dictionary<long, int> dictionary1 = new Dictionary<long, int>(rootObjects.Count);
      List<ObjInfoIDItem> objInfoIdItemList = new List<ObjInfoIDItem>(rootObjects.Count);
      Dictionary<long, int> dictionary2 = new Dictionary<long, int>(rootObjects.Count);
      Dictionary<long, List<CompositionRootPath>> dictionary3 = new Dictionary<long, List<CompositionRootPath>>(paths.Count);
      for (int index = 0; index < paths.Count; ++index)
      {
        List<CompositionRootPath> compositionRootPathList;
        if (!dictionary3.TryGetValue(paths[index].ObjectID, out compositionRootPathList))
        {
          compositionRootPathList = new List<CompositionRootPath>(1);
          dictionary3.Add(paths[index].ObjectID, compositionRootPathList);
        }
        compositionRootPathList.Add(paths[index]);
      }
      List<int> intList1 = (List<int>) null;
      foreach (int searchRelationType in this._loadingParams.SearchRelationTypes)
      {
        if (enabledRelationTypes.Contains(searchRelationType))
        {
          List<int> intList2 = (List<int>) null;
          if (relType2ObjTypeLevelCache == null || relType2ObjTypeLevelCache.TryGetValue(searchRelationType, out intList2))
          {
            if (this._searchObjectTypeCache != null && intList2 != null && currentLevel == this._loadingParams.LoadLevels)
              intList2 = intList2.Where<int>(new System.Func<int, bool>(this._searchObjectTypeCache.Contains)).ToList<int>();
            if (intList2 == null || intList2.Count != 0)
            {
              if (intList1 != null)
                resultData = intList1;
              else
                GenericListHelper.GetDifference<int>((IList<int>) this._objTypeHelper.GetEnabledObjectTypes(searchRelationType, this._loadingParams.Composition), (IList<int>) bList, GenericListHelper.SearchMode.smExistInBoth, out resultData);
              if (resultData != null && resultData.Count != 0)
              {
                objInfoIdItemList.Clear();
                dictionary1.Clear();
                bool flag2 = false;
                int num1;
                foreach (long rootObjectId in rootObjectIDs)
                {
                  ObjectMeasuredInfo objectMeasuredInfo;
                  if (rootObjects.TryGetValue(rootObjectId, out objectMeasuredInfo) && resultData.Contains(objectMeasuredInfo.ObjectType))
                  {
                    if (!dictionary1.TryGetValue(rootObjectId, out num1))
                    {
                      objInfoIdItemList.Add(new ObjInfoIDItem(rootObjectId, objectMeasuredInfo.ObjectType, objectMeasuredInfo.ID));
                      num1 = 1;
                    }
                    else
                    {
                      ++num1;
                      flag2 = true;
                    }
                    dictionary1[rootObjectId] = num1;
                  }
                }
                if (objInfoIdItemList.Count != 0)
                {
                  if (inConditionValue != null && inConditionValue.SelectKey == 0L)
                    inConditionValue.Values = (Array) objInfoIdItemList.Select<ObjInfoIDItem, long>((System.Func<ObjInfoIDItem, long>) (item => item.ObjectID)).ToArray<long>();
                  if (this._dbTags == null)
                    this._dbTags = new HybridDictionary();
                  dictionary2.Clear();
                  foreach (ObjInfoIDItem objInfoIdItem in objInfoIdItemList)
                    dictionary2[objInfoIdItem.ObjectID] = objInfoIdItem.ObjTypeID;
                  this._dbTags[(object) "{004511C2-5AA8-4831-B60A-7CD17C1A2D88}"] = (object) dictionary2;
                  DataTable levelData = this.GetLevelData(objInfoIdItemList, searchRelationType, 1, inConditionValue, intList2);
                  this._dbTags[(object) "{004511C2-5AA8-4831-B60A-7CD17C1A2D88}"] = (object) null;
                  if (levelData != null && levelData.Rows.Count != 0)
                  {
                    DataTable dataTable = levelData;
                    if (flag2)
                    {
                      int count1 = levelData.Rows.Count;
                      int count2 = levelData.Columns.Count;
                      object[] buffer = new object[count2];
                      for (int index1 = 0; index1 < count1; ++index1)
                      {
                        DataRow row = levelData.Rows[index1];
                        long int64 = Convert.ToInt64(row[this._loadingParams.Composition ? this._col_idx_ProjID : this._col_idx_PartObjectID]);
                        if (dictionary1.TryGetValue(int64, out num1) && num1 > 1)
                        {
                          DataSetProcessor.CopyDataToBuffer(row, buffer, count2);
                          for (int index2 = 1; index2 < num1; ++index2)
                            levelData.Rows.Add(buffer);
                        }
                      }
                      levelData.AcceptChanges();
                    }
                    int count = levelData.Rows.Count;
                    List<long> longList1 = new List<long>(count);
                    Dictionary<long, ObjectMeasuredInfo> childObjects = new Dictionary<long, ObjectMeasuredInfo>(count);
                    Dictionary<Int96, DataRow> childObjectRows = new Dictionary<Int96, DataRow>(count);
                    int colPhysicalQuantityId = levelData.Columns.IndexOf("_colPhysicalQuantityID");
                    List<DataRow> collection = new List<DataRow>(count);
                    dataTable.BeginLoadData();
                    try
                    {
                      if (allChildPaths.Capacity - allChildPaths.Count < count)
                        allChildPaths.Capacity = allChildPaths.Count + count;
                      Dictionary<CompositionRootPath, List<long>> dictionary4 = new Dictionary<CompositionRootPath, List<long>>();
                      for (int index3 = 0; index3 < dataTable.Rows.Count; ++index3)
                      {
                        DataRow row = dataTable.Rows[index3];
                        long int64_1 = Convert.ToInt64(row[this._loadingParams.Composition ? this._col_idx_ProjID : this._col_idx_PartObjectID]);
                        List<CompositionRootPath> compositionRootPathList;
                        if (dictionary3.TryGetValue(int64_1, out compositionRootPathList))
                        {
                          long int64_2 = Convert.ToInt64(row[this._col_idx_ObjID]);
                          CompositionRootPath compositionRootPath;
                          if (compositionRootPathList.Count > 1)
                          {
                            int num2 = int.MaxValue;
                            int index4 = -1;
                            for (int index5 = 0; index5 < compositionRootPathList.Count; ++index5)
                            {
                              List<long> longList2;
                              if (!dictionary4.TryGetValue(compositionRootPathList[index5], out longList2))
                              {
                                dictionary4.Add(compositionRootPathList[index5], new List<long>());
                                index4 = index5;
                                break;
                              }
                              int num3 = 0;
                              for (int index6 = 0; index6 < longList2.Count; ++index6)
                              {
                                if (longList2[index6] == int64_2)
                                  ++num3;
                              }
                              if (num3 < num2)
                              {
                                num2 = num3;
                                index4 = index5;
                              }
                            }
                            compositionRootPath = compositionRootPathList[index4];
                            dictionary4[compositionRootPath].Add(int64_2);
                          }
                          else
                            compositionRootPath = compositionRootPathList[0];
                          long objectId;
                          Int96 keyId;
                          bool isLoop;
                          if (this.HandleRow(rootObjects, childObjects, longList1, childObjectRows, row, colPhysicalQuantityId, out objectId, out ObjectMeasuredInfo _, out keyId, compositionRootPath, out isLoop))
                          {
                            collection.Add(row);
                            if (keyId != null)
                              childObjectRows.Add(keyId, row);
                          }
                          if (!isLoop)
                            allChildPaths.Add(new CompositionRootPath(objectId, compositionRootPath));
                        }
                      }
                      if (flag1 && childObjects.Count > 0)
                      {
                        if (allChildObjectIDs.Count == 0)
                          allChildObjectIDs = longList1;
                        else
                          allChildObjectIDs.AddRange((IEnumerable<long>) longList1);
                        if (allChildObjects.Count == 0)
                          allChildObjects = new Dictionary<long, ObjectMeasuredInfo>(longList1.Count);
                        foreach (long key in longList1)
                          allChildObjects[key] = childObjects[key];
                      }
                      if (dataRowList.Count == 0)
                        dataRowList = collection;
                      else
                        dataRowList.AddRange((IEnumerable<DataRow>) collection);
                    }
                    finally
                    {
                      dataTable.EndLoadData();
                    }
                  }
                }
              }
            }
          }
        }
      }
    }
    finally
    {
      (this._session as UserSession).QueryBuilder.StopINCondition(inConditionValue);
    }
    Thread.Sleep(0);
    return dataRowList;
  }

  private bool HandleLevelTable(
    ref List<DataRow> resultDataRows,
    IList<DataRow> levelDataRows,
    int colPhysicalQuantityId)
  {
    if (resultDataRows == null)
      throw new ArgumentNullException(nameof (resultDataRows));
    if (levelDataRows == null)
      throw new ArgumentNullException(nameof (levelDataRows));
    if (levelDataRows.Count == 0)
      return true;
    if (!this._loadingParams.Grouping)
    {
      if (levelDataRows is List<DataRow> dataRowList && resultDataRows.Count == 0)
        resultDataRows = dataRowList;
      else
        resultDataRows.AddRange((IEnumerable<DataRow>) levelDataRows);
      return true;
    }
    Dictionary<Int96, DataRow> dictionary = new Dictionary<Int96, DataRow>(levelDataRows.Count);
    foreach (DataRow levelDataRow in (IEnumerable<DataRow>) levelDataRows)
    {
      long int64 = Convert.ToInt64(levelDataRow[this._col_idx_ObjID]);
      long id = 0;
      MeasuredValue measuredValue = (MeasuredValue) null;
      if (this._needCalcQuantity)
      {
        string str = Convert.ToString(levelDataRow[this._col_idx_QuantityNum]);
        if (str != string.Empty && !this._measureCache.TryGetValue(str, out measuredValue))
        {
          measuredValue = MeasureHelper.ConvertToMeasuredValue(str, false);
          this._measureCache.Add(str, measuredValue);
        }
        if (measuredValue != null && !this._measureDescriptors.TryGetValue(measuredValue.MeasureID, out id))
        {
          MeasureDescriptor descriptor = MeasureHelper.FindDescriptor(measuredValue);
          if (descriptor != null)
          {
            id = descriptor.PhysicalQuantityID;
            this._measureDescriptors.Add(measuredValue.MeasureID, descriptor.PhysicalQuantityID);
          }
        }
      }
      Int96 key = new Int96(id, int64);
      DataRow row;
      if (dictionary.TryGetValue(key, out row))
      {
        if (this._needCalcQuantity && Convert.ToInt64(levelDataRow[colPhysicalQuantityId]) != 0L)
        {
          string str = Convert.ToString(row[this._col_idx_QuantityNum]);
          MeasuredValue operand1 = (MeasuredValue) null;
          if (str != string.Empty && !this._measureCache.TryGetValue(str, out operand1))
          {
            operand1 = MeasureHelper.ConvertToMeasuredValue(str, false);
            this._measureCache.Add(str, operand1);
          }
          row[this._col_idx_QuantityNum] = (object) MeasureHelper.Add(operand1, measuredValue).ToString();
        }
        this.ClearRelationAttributes(row);
      }
      else
      {
        resultDataRows.Add(levelDataRow);
        dictionary.Add(key, levelDataRow);
      }
    }
    return true;
  }

  private bool HandleRow(
    Dictionary<long, ObjectMeasuredInfo> rootObjectIDs,
    Dictionary<long, ObjectMeasuredInfo> childObjects,
    List<long> childObjectIDs,
    Dictionary<Int96, DataRow> childObjectRows,
    DataRow row,
    int colPhysicalQuantityId,
    out long objectId,
    out ObjectMeasuredInfo objectInfo,
    out Int96 keyId,
    CompositionRootPath path,
    out bool isLoop)
  {
    bool flag1 = false;
    MeasuredValue operand2 = (MeasuredValue) null;
    long id = 0;
    keyId = (Int96) null;
    objectInfo = (ObjectMeasuredInfo) null;
    objectId = Convert.ToInt64(row[this._col_idx_ObjID]);
    int int32 = Convert.ToInt32(row[this._col_idx_ObjectType]);
    isLoop = path.Contains(objectId);
    long partObjectId = this._loadingParams.Composition ? objectId : Convert.ToInt64(row[this._col_idx_PartObjectID]);
    if (!this._loadingParams.Grouping && !this.CheckRelation(Convert.ToInt64(row[this._col_idx_PrjLink]), partObjectId))
    {
      isLoop = true;
      return false;
    }
    if (this._searchObjectTypeCache != null && !this._searchObjectTypeCache.Contains(int32) && !this.CheckCompositionExpediency(int32))
    {
      objectInfo = new ObjectMeasuredInfo(int32, Convert.ToInt64(row[this._col_idx_ID]), new ShortMeasuredValue(0L, 0.0, (Dictionary<long, long>) null));
      return false;
    }
    if (this._needCalcQuantity)
    {
      string str = Convert.ToString(row[this._col_idx_QuantityNum]);
      if (str != string.Empty && !this._measureCache.TryGetValue(str, out operand2))
      {
        operand2 = MeasureHelper.ConvertToMeasuredValue(str, false);
        this._measureCache.Add(str, operand2);
      }
      if (operand2 != null)
      {
        if (!this._measureDescriptors.TryGetValue(operand2.MeasureID, out id))
          id = ShortMeasuredValue.GetPhysicalQuantityId(operand2.MeasureID);
        long key = this._loadingParams.Composition ? Convert.ToInt64(row[this._col_idx_ProjID]) : Convert.ToInt64(row[this._col_idx_PartObjectID]);
        List<ShortMeasuredValue> quantities = rootObjectIDs[key].Quantities;
        if (quantities.Count > 1)
        {
          operand2 = (MeasuredValue) null;
          id = 0L;
        }
        else
        {
          if (!quantities[0].RootValue)
          {
            operand2 = MeasureHelper.Multiply(quantities[0].ToMeasuredValue(), operand2, false);
            if (operand2 != null && operand2.MeasureID == 0L)
              operand2 = (MeasuredValue) null;
          }
          if (operand2 == null)
            id = 0L;
        }
        row[this._col_idx_QuantityNum] = operand2 != null ? (object) operand2.ToString() : (object) DBNull.Value;
      }
    }
    if (!childObjects.TryGetValue(objectId, out objectInfo))
    {
      flag1 = true;
      objectInfo = new ObjectMeasuredInfo(int32, Convert.ToInt64(row[this._col_idx_ID]), operand2 != null ? new ShortMeasuredValue(operand2, this._measureDescriptors) : new ShortMeasuredValue(0L, 0.0, (Dictionary<long, long>) null));
      childObjects.Add(objectId, objectInfo);
      if (!isLoop)
        childObjectIDs.Add(objectId);
      keyId = new Int96(id, objectId);
    }
    else if (this._needCalcQuantity)
    {
      bool flag2 = false;
      for (int index = 0; index < objectInfo.Quantities.Count; ++index)
      {
        if (objectInfo.Quantities[index].PhysicalQuantityID == id)
        {
          if (id != 0L)
          {
            MeasuredValue measuredValue = MeasureHelper.Add(objectInfo.Quantities[index].ToMeasuredValue(), operand2);
            objectInfo.Quantities[index].MeasureID = measuredValue.MeasureID;
            objectInfo.Quantities[index].Value = measuredValue.Value;
            DataRow childObjectRow = childObjectRows[new Int96(id, objectId)];
            this.ClearRelationAttributes(childObjectRow);
            childObjectRow[this._col_idx_QuantityNum] = (object) measuredValue.ToString();
          }
          flag2 = true;
          break;
        }
      }
      if (!flag2)
      {
        objectInfo.Quantities.Add(operand2 != null ? new ShortMeasuredValue(operand2, this._measureDescriptors) : new ShortMeasuredValue(0L, 0.0, (Dictionary<long, long>) null));
        keyId = new Int96(id, objectId);
        flag1 = true;
      }
    }
    else if (this._loadingParams.Grouping && childObjectRows != null)
    {
      this.ClearRelationAttributes(childObjectRows[new Int96(id, objectId)]);
    }
    else
    {
      flag1 = true;
      if (!isLoop)
        childObjectIDs.Add(objectId);
    }
    if (flag1 && colPhysicalQuantityId != -1 && this._needCalcQuantity)
      row[colPhysicalQuantityId] = (object) id;
    return flag1;
  }

  private void ClearRelationAttributes(DataRow row)
  {
    if (row == null || this._relationColumns == null || this._relationColumns.Count == 0 || row[this._col_idx_PrjLink] == DBNull.Value)
      return;
    for (int index = 0; index < this._relationColumns.Count; ++index)
      row[this._relationColumns[index]] = (object) DBNull.Value;
  }

  private bool CheckCompositionExpediency(int objectType)
  {
    if (this._searchObjectTypeCache == null || this._searchObjectTypeCache.Count<int>() == 0)
      return true;
    bool flag;
    if (this._expediencyTypesCache.TryGetValue(objectType, out flag))
      return flag;
    List<int> searched = new List<int>();
    flag = this.CheckCompositionExpediencyRecursive(objectType, searched);
    this._expediencyTypesCache.Add(objectType, flag);
    return flag;
  }

  private bool CheckCompositionExpediencyRecursive(int objectType, List<int> searched)
  {
    searched.Add(objectType);
    List<int> applicability = this.GetApplicability(objectType);
    if (applicability != null && applicability.Count > 0)
    {
      for (int index = 0; index < applicability.Count; ++index)
      {
        if (this._searchObjectTypeCache.Contains(applicability[index]) || searched.IndexOf(applicability[index]) < 0 && this.CheckCompositionExpediencyRecursive(applicability[index], searched))
          return true;
      }
    }
    return false;
  }

  private List<int> GetApplicability(int objectType)
  {
    List<int> applicability = new List<int>();
    DataTable applicabilitiesList = this._session.GetRelationsApplicabilityCollection().GetApplicabilitiesList(this._loadingParams.SearchRelationTypes.Count<int>() == 1 ? this._loadingParams.SearchRelationTypes.First<int>() : -1, this._loadingParams.Composition ? -1 : objectType, this._loadingParams.Composition ? objectType : -1);
    for (int index1 = 0; index1 < applicabilitiesList.Rows.Count; ++index1)
    {
      if (Convert.ToInt32(applicabilitiesList.Rows[index1]["F_MIN_LINKS"]) != -1 && (this._loadingParams.SearchRelationTypes.Count<int>() <= 1 || this._loadingParams.SearchRelationTypes.Contains<int>(Convert.ToInt32(applicabilitiesList.Rows[index1]["F_RELATION_TYPE"]))))
      {
        int parentTypeID = this._loadingParams.Composition ? Convert.ToInt32(applicabilitiesList.Rows[index1]["F_OBJECT_TYPE"]) : Convert.ToInt32(applicabilitiesList.Rows[index1]["F_INOBJECT_TYPE"]);
        if (applicability.IndexOf(parentTypeID) < 0)
        {
          applicability.Add(parentTypeID);
          List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(parentTypeID);
          if (childrenIdRecursive != null && childrenIdRecursive.Count > 0)
          {
            for (int index2 = 0; index2 < childrenIdRecursive.Count; ++index2)
            {
              if (applicability.IndexOf(childrenIdRecursive[index2]) < 0)
                applicability.Add(childrenIdRecursive[index2]);
            }
          }
        }
      }
    }
    return applicability;
  }

  private DataTable GetLevelData(
    List<ObjInfoIDItem> sourceObjItems,
    int relationTypeId,
    int countLevels,
    INConditionValue inCondValue = null,
    List<int> nextLevelObjTypes = null)
  {
    if (sourceObjItems == null || sourceObjItems.Count == 0)
      return (DataTable) null;
    if (this._columns == null || this._columns.Count == 0)
      return (DataTable) null;
    DBRelationCollection relationCollection = (DBRelationCollection) this._session.GetRelationCollection(relationTypeId, this._loadingParams.FiltrationOwnerId);
    relationCollection._isManualSortingAllowed = this._relManualSortAllowed;
    if (this._loadingParams.VersionsRule != null)
      relationCollection.FiltrationRule = this._loadingParams.VersionsRule;
    DataTable levelData = this.LoadLevelData((IEnumerable<ObjInfoIDItem>) sourceObjItems, (IDBRelationCollection) relationCollection, countLevels, 1, inCondValue, nextLevelObjTypes);
    if (levelData != null && this._needCalcQuantity)
    {
      this._col_idx_PhysicalQuantityID = levelData.Columns.Count;
      levelData.Columns.Add(new DataColumn("_colPhysicalQuantityID", typeof (long)));
    }
    return levelData;
  }

  private DataTable LoadLevelData(
    IEnumerable<ObjInfoIDItem> sourceObjItems,
    IDBRelationCollection comp,
    int countLevels,
    int curLevel,
    INConditionValue inCondValue = null,
    List<int> nextLevelObjTypes = null)
  {
    IEnumerable<long> longs = sourceObjItems.Select<ObjInfoIDItem, long>((System.Func<ObjInfoIDItem, long>) (item => item.ObjectID));
    object sourceObjCond;
    List<int> intList;
    if (inCondValue != null)
    {
      sourceObjCond = (object) inCondValue;
      intList = nextLevelObjTypes;
    }
    else
    {
      if (!(longs is long[] numArray))
        numArray = longs.ToArray<long>();
      sourceObjCond = (object) numArray;
      if (!this._hasObjectCustomAttrs)
      {
        intList = (List<int>) null;
      }
      else
      {
        intList = this.CompositionService.GetPresentCompositionTypes((object) this._session, longs, comp.RelationTypeID, this._loadingParams.Composition);
        if (intList == null)
          return (DataTable) null;
      }
    }
    if (intList != null)
    {
      if (this._searchObjectTypeCache != null && curLevel == this._loadingParams.LoadLevels)
        intList = intList.Where<int>(new System.Func<int, bool>(this._searchObjectTypeCache.Contains)).ToList<int>();
      if (intList.Count == 0)
        return (DataTable) null;
    }
    DataTable toTable = (DataTable) null;
    if (!this._hasObjectCustomAttrs)
    {
      DBRelationCollection comp1 = (DBRelationCollection) comp;
      comp1.LocalTypesMode = true;
      try
      {
        toTable = this.InternalLoadLevelData(sourceObjItems, sourceObjCond, (IDBRelationCollection) comp1);
      }
      finally
      {
        comp1.LocalTypesMode = false;
      }
    }
    else
    {
      if (intList == null)
        return (DataTable) null;
      List<int> nonLocalTypeIds;
      List<int> localTypeIds;
      CompositionLoadTask.ExtractLocalObjectTypes((IEnumerable<int>) intList, out nonLocalTypeIds, out localTypeIds);
      if (nonLocalTypeIds.Count != 0)
        toTable = this.InternalLoadLevelData(sourceObjItems, sourceObjCond, comp);
      if (localTypeIds.Count != 0)
      {
        DataTable fromTable = this.InternalLoadLevelData(sourceObjItems, sourceObjCond, comp, localTypeIds.ToArray());
        if (toTable == null)
          toTable = fromTable;
        else
          DataSetProcessor.AddTable(toTable, fromTable, false);
      }
    }
    if (toTable == null)
      return (DataTable) null;
    if (toTable.Rows.Count > 0 && (curLevel == -1 || curLevel < countLevels))
    {
      List<ObjInfoIDItem> sourceObjItems1 = new List<ObjInfoIDItem>(toTable.Rows.Count);
      Dictionary<long, int> dictionary = new Dictionary<long, int>(toTable.Rows.Count);
      for (int index = 0; index < toTable.Rows.Count; ++index)
      {
        long int64 = Convert.ToInt64(toTable.Rows[index][this._col_idx_ObjID]);
        if (!dictionary.ContainsKey(int64))
        {
          int int32 = Convert.ToInt32(toTable.Rows[index][this._col_idx_ObjectType]);
          dictionary.Add(int64, int32);
          sourceObjItems1.Add(new ObjInfoIDItem(int64, int32, Convert.ToInt64(toTable.Rows[index][this._col_idx_ID])));
        }
      }
      this._dbTags[(object) "{004511C2-5AA8-4831-B60A-7CD17C1A2D88}"] = (object) dictionary;
      DataTable fromTable = this.LoadLevelData((IEnumerable<ObjInfoIDItem>) sourceObjItems1, comp, countLevels, curLevel + 1);
      this._dbTags[(object) "{004511C2-5AA8-4831-B60A-7CD17C1A2D88}"] = (object) null;
      if (fromTable != null && fromTable.Rows.Count > 0)
        DataSetProcessor.AddTable(toTable, fromTable, true);
    }
    return toTable;
  }

  private DataTable InternalLoadLevelData(
    IEnumerable<ObjInfoIDItem> sourceObjItems,
    object sourceObjCond,
    IDBRelationCollection comp,
    params int[] localTypes)
  {
    return !this._loadingParams.Composition ? this.InternalLoadApplicabilityLevel(sourceObjItems, sourceObjCond, comp, localTypes) : this.InternalLoadComposionLevel(sourceObjItems, sourceObjCond, comp, localTypes);
  }

  private DataTable InternalLoadComposionLevel(
    IEnumerable<ObjInfoIDItem> sourceObjItems,
    object sourceObjCond,
    IDBRelationCollection comp,
    params int[] localTypes)
  {
    return (this.CompositionService as CompositionLoadService).InternalLoadComposition(this._session, sourceObjCond, (IEnumerable<ColumnDescriptor>) this._columns, comp, this._loadingParams.Conditions, this._dbTags, localTypes);
  }

  private DataTable InternalLoadApplicabilityLevel(
    IEnumerable<ObjInfoIDItem> sourceObjItems,
    object sourceObjCond,
    IDBRelationCollection comp,
    params int[] localTypes)
  {
    List<List<ObjInfoIDItem>> objInfoIdItemListList = new List<List<ObjInfoIDItem>>();
    foreach (ObjInfoIDItem objInfoIdItem in (IEnumerable<ObjInfoIDItem>) sourceObjItems.OrderBy<ObjInfoIDItem, long>((System.Func<ObjInfoIDItem, long>) (item => item.ID)))
    {
      bool flag = false;
      for (int index = 0; index < objInfoIdItemListList.Count; ++index)
      {
        List<ObjInfoIDItem> objInfoIdItemList = objInfoIdItemListList[index];
        if (objInfoIdItemList[objInfoIdItemList.Count - 1].ID != objInfoIdItem.ID)
        {
          objInfoIdItemList.Add(objInfoIdItem);
          flag = true;
          break;
        }
      }
      if (!flag)
        objInfoIdItemListList.Add(new List<ObjInfoIDItem>()
        {
          objInfoIdItem
        });
    }
    DataTable toTable = (DataTable) null;
    for (int index = 0; index < objInfoIdItemListList.Count; ++index)
    {
      DataTable fromTable = this.InternalLoadApplicability((IEnumerable<ObjInfoIDItem>) objInfoIdItemListList[index], (object) null, comp, localTypes);
      if (toTable == null)
        toTable = fromTable;
      else
        DataSetProcessor.AddTable(toTable, fromTable, false);
    }
    return toTable;
  }

  private DataTable InternalLoadApplicability(
    IEnumerable<ObjInfoIDItem> sourceObjItems,
    object sourceObjCond,
    IDBRelationCollection comp,
    params int[] rootObjectTypes)
  {
    ColumnDescriptor[] array = this._columns.ToArray();
    object[] objArray = new object[0];
    SortOrders[] sortOrdersArray = new SortOrders[0];
    ConditionStructure[] conditionStructureArray = new ConditionStructure[1]
    {
      sourceObjItems.Count<ObjInfoIDItem>() == 1 ? new ConditionStructure(-22, RelationalOperators.Equal, (object) sourceObjItems.First<ObjInfoIDItem>().ID, LogicalOperators.NONE, 0, true) : new ConditionStructure(-22, RelationalOperators.In, sourceObjCond != null ? sourceObjCond : (object) sourceObjItems.Select<ObjInfoIDItem, long>((System.Func<ObjInfoIDItem, long>) (item => item.ID)).ToArray<long>(), LogicalOperators.NONE, 0, true)
    };
    if (this._loadingParams.Conditions != null && this._loadingParams.Conditions.Count<ConditionStructure>() > 0)
      conditionStructureArray = ConditionStructure.Join(this._loadingParams.Conditions.ToArray<ConditionStructure>(), conditionStructureArray);
    DBRecordSetParams paramSet = new DBRecordSetParams(conditionStructureArray, array);
    Dictionary<long, long> dictionary = sourceObjItems.ToDictionary<ObjInfoIDItem, long, long>((System.Func<ObjInfoIDItem, long>) (item => item.ID), (System.Func<ObjInfoIDItem, long>) (item => item.ObjectID));
    this._dbTags[(object) "{2C7E989F-0EAF-40CC-80FD-16EF1D9090B3}"] = (object) dictionary;
    DataTable dataTable;
    try
    {
      paramSet.Tags = this._dbTags;
      ((DBRecordSet) comp)._CurrentModificationID = this._modificationID;
      comp.ChildObjectTypes = rootObjectTypes.Length == 0 ? (IList<int>) null : (IList<int>) new List<int>((IEnumerable<int>) rootObjectTypes);
      (comp as DBRelationCollection).JoinDefaultFieldName = "F_PROJ_ID";
      (comp as DBRelationCollection).FunctionID = SelectFunction.EntersInVersion;
      dataTable = comp.Select(paramSet);
    }
    finally
    {
      ((DBRecordSet) comp)._CurrentModificationID = 0L;
      this._modificationID = 0L;
      this._dbTags[(object) "{2C7E989F-0EAF-40CC-80FD-16EF1D9090B3}"] = (object) null;
    }
    if (dataTable == null)
      return dataTable;
    this._col_idx_PartObjectID = dataTable.Columns.Count;
    dataTable.Columns.Add(new DataColumn("F_PART_OBJ_ID", typeof (long)));
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      long num;
      if (dictionary.TryGetValue(Convert.ToInt64(dataTable.Rows[index][this._col_idx_PartID]), out num))
        dataTable.Rows[index][this._col_idx_PartObjectID] = (object) num;
    }
    return dataTable;
  }

  protected void InitializeData()
  {
    this._objTypeHelper = new CompositionLoadTask.ObjTypeHelper(this._session);
  }

  protected virtual DataTable DoExecute(
    CompositionLoadTask.CompositionCustomMethods method)
  {
    IList<DataRow> fromRows = this.ProceedTaskMethod(method);
    if (fromRows == null)
      return (DataTable) null;
    DataTable dataTable = fromRows[0].Table.Clone();
    DataSetProcessor.AssignRows(dataTable, (IEnumerable<DataRow>) fromRows, true, true);
    this.RemoveServiceColumns(dataTable);
    return dataTable;
  }

  protected virtual IList<DataRow> ProceedTaskMethod(
    CompositionLoadTask.CompositionCustomMethods method)
  {
    if (this._columns == null || this._columns.Count == 0)
      return (IList<DataRow>) null;
    this._objTypeHelper.ClearCache();
    this._expediencyTypesCache.Clear();
    this._measureDescriptors.Clear();
    this._measureCache.Clear();
    this.FillServiceColumns();
    this.CheckAttributes();
    this._needBlockConfigureComposition = true;
    if (this._session.EnabledPdmConfigurator)
    {
      this._needBlockConfigureComposition = false;
      if (!string.IsNullOrEmpty(this._loadingParams.FiltrationOwnerId))
      {
        FiltrationSettings filtrationSettings = ((IClientVersionRulesCacheService) this._session.GetCustomService(typeof (IVersionRulesCacheService))).GetFiltrationSettings((object) this._session, this._loadingParams.FiltrationOwnerId, true);
        if (filtrationSettings != null && filtrationSettings.Tags != null && filtrationSettings.Tags[(object) "{0422E069-0A1D-4235-85E8-C52C3516CFC1}"] != null)
          this._needBlockConfigureComposition = (bool) filtrationSettings.Tags[(object) "{0422E069-0A1D-4235-85E8-C52C3516CFC1}"];
      }
    }
    this._needCalcQuantity = this._col_idx_QuantityNum >= 0 && this._loadingParams.Grouping && this._loadingParams.LoadLevels == -1;
    if (!this._loadingParams.Grouping)
      this._resultRelationIDs = new HashSet<Tuple<long, long>>();
    IList<DataRow> dataRows;
    try
    {
      this.ValidateQuantityFieldType();
      this.ValidateQuantityInCycleRelations();
      dataRows = method();
      this.FilterObjectTypeRows(dataRows);
    }
    finally
    {
      this._objTypeHelper.ClearCache();
      this._expediencyTypesCache.Clear();
      this._measureDescriptors.Clear();
      this._measureCache.Clear();
      if (this._resultRelationIDs != null)
        this._resultRelationIDs = (HashSet<Tuple<long, long>>) null;
    }
    return dataRows;
  }

  protected virtual IList<DataRow> ProceedNodesMethod()
  {
    List<ObjInfoItem> objInfoList = new List<ObjInfoItem>(this._loadingParams.Objects);
    if (!this._loadingParams.Composition)
    {
      for (int index = 0; index < objInfoList.Count; ++index)
      {
        if (!(objInfoList[index] is IObjInfoID))
          objInfoList[index] = (ObjInfoItem) new ObjInfoIDItem((TypedInfoItem) objInfoList[index]);
      }
    }
    ServiceUtils.GetService<ITypedInfoService>((object) this._session, true).UpdateUnknownInfo((IEnumerable<ObjInfoItem>) objInfoList, (object) this._session.SessionGUID);
    Dictionary<long, ObjectMeasuredInfo> rootObjects = new Dictionary<long, ObjectMeasuredInfo>(objInfoList.Count);
    List<CompositionRootPath> paths = new List<CompositionRootPath>();
    for (int index = 0; index < objInfoList.Count; ++index)
    {
      if (!rootObjects.ContainsKey(objInfoList[index].ObjectID))
      {
        rootObjects.Add(objInfoList[index].ObjectID, new ObjectMeasuredInfo(objInfoList[index].ObjTypeID, objInfoList[index] is IObjInfoID ? (objInfoList[index] as IObjInfoID).ID : 0L, new ShortMeasuredValue()));
        paths.Add(new CompositionRootPath(objInfoList[index].ObjectID));
      }
    }
    IList<DataRow> dataRowList = this.RecursiveNodes(rootObjects, paths);
    return dataRowList == null || dataRowList.Count == 0 ? (IList<DataRow>) null : dataRowList;
  }

  private bool CheckRelation(long prjLinkId, long partObjectId)
  {
    Tuple<long, long> tuple = new Tuple<long, long>(prjLinkId, partObjectId);
    if (this._resultRelationIDs.Contains(tuple))
      return false;
    this._resultRelationIDs.Add(tuple);
    return true;
  }

  protected virtual void FillServiceColumns()
  {
    this._serviceColumns.Clear();
    int attributeTypeId = MetaDataHelper.GetAttributeTypeID("cad00267-306c-11d8-b4e9-00304f19f545");
    this._relationColumns = (List<int>) null;
    if (this._loadingParams.Grouping)
      this._relationColumns = new List<int>();
    this._col_idx_ID = -1;
    this._col_idx_ObjID = -1;
    this._col_idx_ObjectType = -1;
    this._col_idx_QuantityNum = -1;
    this._col_idx_ProjID = -1;
    this._col_idx_PrjLink = -1;
    this._col_idx_RelType = -1;
    this._col_idx_PartID = -1;
    this._col_idx_PartObjectID = -1;
    if (this._columns == null || this._columns.Count == 0)
      return;
    EventLogHelper service = ServerServices.GetService(typeof (IEventLogHelper)) as EventLogHelper;
    for (int index = 0; index < this._columns.Count; ++index)
    {
      bool flag = false;
      int attributeId = service.GetAttributeID(this._columns[index].AttributeID, false);
      switch (attributeId)
      {
        case -10000:
          continue;
        case -21:
          this._col_idx_ProjID = index;
          break;
        case -20:
          this._col_idx_PrjLink = index;
          break;
        case -7:
          this._col_idx_ObjectType = index;
          break;
        case -3:
          this._col_idx_ID = index;
          break;
        case -2:
          this._col_idx_ObjID = index;
          break;
        default:
          if (attributeId == attributeTypeId && (this._columns[index].AttributeSource == AttributeSourceTypes.Auto || this._columns[index].AttributeSource == AttributeSourceTypes.Relation))
          {
            this._col_idx_QuantityNum = index;
            flag = true;
            break;
          }
          switch (attributeId)
          {
            case -23:
              this._col_idx_RelType = index;
              break;
            case -22:
              this._col_idx_PartID = index;
              break;
          }
          break;
      }
      if (!flag && this._loadingParams.Grouping && attributeId != -23 && (this._columns[index].AttributeSource == AttributeSourceTypes.Auto || this._columns[index].AttributeSource == AttributeSourceTypes.Relation))
        this._relationColumns.Add(index);
    }
    if (this._col_idx_ID == -1)
    {
      this._col_idx_ID = this._columns.Count;
      this._serviceColumns.Add(this._col_idx_ID);
      this._columns.Add(new ColumnDescriptor((object) -3, AttributeSourceTypes.Object, ColumnContents.Text, this._columns[0].ColumnName, SortOrders.NONE, 0));
    }
    if (this._col_idx_ObjID == -1)
    {
      this._col_idx_ObjID = this._columns.Count;
      this._serviceColumns.Add(this._col_idx_ObjID);
      this._columns.Add(new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.Text, this._columns[0].ColumnName, SortOrders.NONE, 0));
    }
    if (this._col_idx_ObjectType == -1)
    {
      this._col_idx_ObjectType = this._columns.Count;
      this._serviceColumns.Add(this._col_idx_ObjectType);
      this._columns.Add(new ColumnDescriptor((object) -7, AttributeSourceTypes.Object, ColumnContents.Text, this._columns[0].ColumnName, SortOrders.NONE, 0));
    }
    if (this._col_idx_ProjID == -1)
    {
      this._col_idx_ProjID = this._columns.Count;
      this._serviceColumns.Add(this._col_idx_ProjID);
      this._columns.Add(new ColumnDescriptor((object) -21, AttributeSourceTypes.Relation, ColumnContents.Text, this._columns[0].ColumnName, SortOrders.NONE, 0));
    }
    if (this._col_idx_PartID == -1 && !this._loadingParams.Composition)
    {
      this._col_idx_PartID = this._columns.Count;
      this._serviceColumns.Add(this._col_idx_PartID);
      this._columns.Add(new ColumnDescriptor((object) -22, AttributeSourceTypes.Relation, ColumnContents.Text, this._columns[0].ColumnName, SortOrders.NONE, 0));
    }
    if (this._col_idx_PrjLink != -1)
      return;
    this._col_idx_PrjLink = this._columns.Count;
    this._serviceColumns.Add(this._col_idx_PrjLink);
    this._columns.Add(new ColumnDescriptor((object) -20, AttributeSourceTypes.Relation, ColumnContents.Text, this._columns[0].ColumnName, SortOrders.NONE, 0));
  }

  protected virtual void RemoveServiceColumns(DataTable resultTable)
  {
    if (resultTable == null)
      return;
    if (this._needCalcQuantity)
    {
      resultTable.Columns.Remove("_colPhysicalQuantityID");
      this._col_idx_PhysicalQuantityID = -1;
    }
    for (int index = this._serviceColumns.Count - 1; index >= 0; --index)
      resultTable.Columns.RemoveAt(this._serviceColumns[index]);
  }

  protected virtual void CheckAttributes()
  {
    this._relManualSortAllowed = true;
    if (this._loadingParams.LoadLevels != 1 || this._loadingParams.SearchRelationTypes.Count<int>() > 1)
    {
      for (int index = 0; index < this._columns.Count; ++index)
      {
        ColumnDescriptor column = this._columns[index];
        if (column.Sort != SortOrders.NONE)
        {
          column.Sort = SortOrders.NONE;
          this._columns[index] = column;
        }
      }
      this._relManualSortAllowed = false;
    }
    this.CheckCustomAttributes();
  }

  protected virtual void CheckCustomAttributes()
  {
    Dictionary<AttributeSourceTypes, bool> type2CustomAttr;
    if (!SqlHelper.HasCustomAttributes(new DBRecordSetParams(this._loadingParams.Conditions != null ? this._loadingParams.Conditions.ToArray<ConditionStructure>() : (ConditionStructure[]) null, this._columns != null ? this._columns.ToArray() : (ColumnDescriptor[]) null), AttributeSourceTypes.Relation, out type2CustomAttr))
      return;
    type2CustomAttr.TryGetValue(AttributeSourceTypes.Object, out this._hasObjectCustomAttrs);
    if (this._hasObjectCustomAttrs || this._loadingParams.Conditions == null)
      return;
    foreach (ConditionStructure condition in this._loadingParams.Conditions)
    {
      if (condition.RelationalOperator == RelationalOperators.ObjectTypeFilter)
      {
        this._hasObjectCustomAttrs = true;
        break;
      }
    }
  }

  private void ValidateQuantityFieldType()
  {
    if (this._col_idx_QuantityNum < 0 || !this._loadingParams.Grouping)
      return;
    switch (this._columns[this._col_idx_QuantityNum].Contents)
    {
      case ColumnContents.Text:
        break;
      case ColumnContents.String:
        break;
      default:
        throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_1173"), (object) Convert.ToString((object) this._columns[this._col_idx_QuantityNum].Contents)));
    }
  }

  protected virtual void ValidateQuantityInCycleRelations()
  {
    if (!this._needCalcQuantity)
      return;
    foreach (int searchRelationType in this._loadingParams.SearchRelationTypes)
    {
      IMSRelationType relationType = MetaDataHelper.GetRelationType(searchRelationType);
      if (relationType != null && (relationType.Options & RelationTypeOptions.EnableCycleRelations) == RelationTypeOptions.EnableCycleRelations)
        throw new Exception($"Для получения {(this._loadingParams.Composition ? (object) "состава" : (object) "применяемости")} используется тип связи {relationType.Description}, для которого разрешено создание циклических связей. Подсчет количества в этом случае невозможен.");
    }
  }

  protected virtual void FilterObjectTypeRows(IList<DataRow> dataRows)
  {
    if (dataRows == null || dataRows.Count == 0 || this._searchObjectTypeCache == null || this._searchObjectTypeCache.Count == 0)
      return;
    for (int index = dataRows.Count - 1; index >= 0; --index)
    {
      if (!this._searchObjectTypeCache.Contains(Convert.ToInt32(dataRows[index][this._col_idx_ObjectType])))
        dataRows.RemoveAt(index);
    }
  }

  public CompositionLoadTask(IUserSession session, ICompositionLoadService compositionService)
  {
    this._session = session ?? throw new ArgumentNullException(nameof (session));
    this._compositionService = compositionService ?? throw new ArgumentNullException(nameof (compositionService));
    this.InitializeData();
  }

  public DataTable Execute(CompositionLoadingParams loadingParams)
  {
    if (loadingParams == null)
      throw new ArgumentNullException(nameof (loadingParams));
    lock (this)
    {
      this._loadingParams = loadingParams;
      this._taskObjectCache = new HashSet<long>((IEnumerable<long>) ObjInfoHelper.GetObjectIDs(loadingParams.Objects));
      this._searchObjectTypeCache = loadingParams.SearchObjectTypes != null ? new HashSet<int>(loadingParams.SearchObjectTypes) : (HashSet<int>) null;
      this._expandObjectTypeCache = loadingParams.ExpandObjectTypes != null ? new HashSet<int>(loadingParams.ExpandObjectTypes) : (HashSet<int>) null;
      this._columns = loadingParams.Columns != null ? new List<ColumnDescriptor>(loadingParams.Columns) : (List<ColumnDescriptor>) null;
      return this.DoExecute(new CompositionLoadTask.CompositionCustomMethods(this.ProceedNodesMethod));
    }
  }

  public bool Terminated { get; set; }

  public ICompositionLoadService CompositionService
  {
    [DebuggerStepThrough] get => this._compositionService;
  }

  internal static bool ExtractLocalObjectTypes(
    IEnumerable<int> objectTypeIds,
    out List<int> nonLocalTypeIds,
    out List<int> localTypeIds)
  {
    nonLocalTypeIds = new List<int>();
    localTypeIds = new List<int>();
    if (objectTypeIds == null || !objectTypeIds.Any<int>())
      return false;
    foreach (int objectTypeId in objectTypeIds)
    {
      if (MetaDataHelper.IsLocalObjectType(objectTypeId))
        localTypeIds.Add(objectTypeId);
      else
        nonLocalTypeIds.Add(objectTypeId);
    }
    return true;
  }

  protected internal class ObjTypeHelper
  {
    private readonly IUserSession _session;
    private Dictionary<int, HashSet<int>> _objType2RelTypeUpCache = new Dictionary<int, HashSet<int>>();
    private Dictionary<int, HashSet<int>> _objType2RelTypeDownCache = new Dictionary<int, HashSet<int>>();

    private HashSet<int> GetApplicabilityRelationTypes(int objType, bool composition)
    {
      if (objType == -1)
        return (HashSet<int>) null;
      Dictionary<int, HashSet<int>> dictionary = composition ? this._objType2RelTypeDownCache : this._objType2RelTypeUpCache;
      HashSet<int> applicabilityRelationTypes1;
      if (dictionary.TryGetValue(objType, out applicabilityRelationTypes1))
        return applicabilityRelationTypes1;
      HashSet<int> result = new HashSet<int>();
      this.CollectRelationTypes(this._session.GetRelationsApplicabilityCollection().GetApplicabilitiesList(-1, composition ? -1 : objType, composition ? objType : -1), result);
      int objectTypeParentId = MetaDataHelper.GetObjectTypeParentID(objType);
      if (objectTypeParentId != -1)
      {
        HashSet<int> applicabilityRelationTypes2 = this.GetApplicabilityRelationTypes(objectTypeParentId, composition);
        result.UnionWith((IEnumerable<int>) applicabilityRelationTypes2);
      }
      dictionary[objType] = result;
      return result;
    }

    private void CollectRelationTypes(DataTable applicabilityTable, HashSet<int> result)
    {
      if (applicabilityTable == null || applicabilityTable.Rows.Count == 0)
        return;
      foreach (DataRow row in (InternalDataCollectionBase) applicabilityTable.Rows)
      {
        if (Convert.ToInt32(row["F_MIN_LINKS"]) != -1)
        {
          int int32 = Convert.ToInt32(row["F_RELATION_TYPE"]);
          if (!result.Contains(int32))
            result.Add(int32);
        }
      }
    }

    public ObjTypeHelper(IUserSession session)
    {
      this._session = session ?? throw new ArgumentNullException(nameof (session));
    }

    public List<int> GetEnabledRelationTypes(int[] objTypes, bool composition)
    {
      HashSet<int> source = new HashSet<int>();
      foreach (int objType in new HashSet<int>((IEnumerable<int>) objTypes))
      {
        HashSet<int> applicabilityRelationTypes = this.GetApplicabilityRelationTypes(objType, composition);
        if (applicabilityRelationTypes != null)
          source.UnionWith((IEnumerable<int>) applicabilityRelationTypes);
      }
      return source.ToList<int>();
    }

    public List<int> GetEnabledObjectTypes(int relTypeId, bool composition)
    {
      List<int> enabledObjectTypes = new List<int>();
      if (relTypeId == -1)
        return enabledObjectTypes;
      foreach (KeyValuePair<int, HashSet<int>> keyValuePair in composition ? this._objType2RelTypeDownCache : this._objType2RelTypeUpCache)
      {
        if (keyValuePair.Value.Contains(relTypeId))
          enabledObjectTypes.Add(keyValuePair.Key);
      }
      return enabledObjectTypes;
    }

    public void ClearCache()
    {
      this._objType2RelTypeUpCache.Clear();
      this._objType2RelTypeDownCache.Clear();
    }
  }

  protected internal delegate IList<DataRow> CompositionCustomMethods();
}
