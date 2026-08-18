// Decompiled with JetBrains decompiler
// Type: Intermech.Pdm.Server.RelVisObserverService
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Pdm;
using Intermech.Interfaces.Pdm.RelationVisualizer;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Threading;

#nullable disable
namespace Intermech.Pdm.Server;

internal class RelVisObserverService : LongLifeObject, IRelVisObserverService
{
  private static Dictionary<int, List<int>> ParentRelTypes4ObjTypeCache = new Dictionary<int, List<int>>();
  private static readonly string _relTypeCAD = "cadd94da-306c-11d8-b4e9-00304f19f545";
  private static readonly string _showStructureLinks = "RELVISSHOWSTRUCTURELINKS";
  private static readonly string _showAssociativeLinks = "RELVISSHOWASSOCIATIVELINKS";
  private ConcurrentDictionary<long, TaskInfo> taskDict = new ConcurrentDictionary<long, TaskInfo>();
  private long taskCounter;

  public DataTable[] GetParentTree(
    long projVID,
    long projId,
    string filtrationOwnerId,
    ICompositionsAutosortRule rule,
    int objType,
    Guid userSession,
    HybridDictionary dict)
  {
    IUserSession userSession1 = this.ToUserSession((object) userSession);
    IDBRelationsApplicabilityCollection applicabilityCollection = userSession1.GetRelationsApplicabilityCollection();
    Dictionary<long, List<long>> childVersionCache = new Dictionary<long, List<long>>();
    childVersionCache.Add(projId, new List<long>()
    {
      projVID
    });
    List<RelVisInfo> relVisInfoList = new List<RelVisInfo>();
    this.AddPart2Dictionary(rule, new RelVisInfo(true), relVisInfoList, objType, projVID, RelVisPred.RelVisLayers.ParentTree, applicabilityCollection);
    return this.BuildParentTree(relVisInfoList, userSession1, filtrationOwnerId, rule, applicabilityCollection, childVersionCache, dict)?.ToArray();
  }

  public DataTable[] GetChildTree(
    long projID,
    string filtrationOwnerId,
    ICompositionsAutosortRule rule,
    int objType,
    Guid userSession,
    bool showHiddenObjects,
    bool showHiddenSostav,
    HybridDictionary dict)
  {
    return this.GetChildTree(projID, filtrationOwnerId, rule, objType, userSession, -1, showHiddenObjects, showHiddenSostav, dict);
  }

  public DataTable[] GetChildTree(
    long projID,
    string filtrationOwnerId,
    ICompositionsAutosortRule rule,
    int objType,
    Guid userSession,
    int levels,
    bool showHiddenObjects,
    bool showHiddenSostav,
    HybridDictionary dict)
  {
    IUserSession userSession1 = this.ToUserSession((object) userSession);
    IDBRelationsApplicabilityCollection applicabilityCollection = userSession1.GetRelationsApplicabilityCollection();
    List<RelVisInfo> relVisInfoList = new List<RelVisInfo>();
    this.AddPart2Dictionary(rule, new RelVisInfo(true), relVisInfoList, objType, projID, RelVisPred.RelVisLayers.ChildTree, applicabilityCollection);
    return this.BuildChildTree(relVisInfoList, userSession1, filtrationOwnerId, rule, applicabilityCollection, 0, levels, showHiddenObjects, showHiddenSostav, dict).ToArray();
  }

  public static int GetElementCount(Dictionary<int, List<long>> dict)
  {
    int elementCount = 0;
    foreach (KeyValuePair<int, List<long>> keyValuePair in dict)
      elementCount += keyValuePair.Value.Count;
    return elementCount;
  }

  public static int GetDistinctElementsCount(Dictionary<int, List<long>> dict)
  {
    HashSet<long> longSet = new HashSet<long>();
    foreach (List<long> longList in dict.Values)
    {
      foreach (long num in longList)
      {
        if (!longSet.Contains(num))
          longSet.Add(num);
      }
    }
    return longSet.Count;
  }

  private static Dictionary<int, List<long>>[] SeparateLimitedDictionary(
    Dictionary<int, List<long>> dict)
  {
    List<Dictionary<int, List<long>>> dictionaryList = new List<Dictionary<int, List<long>>>();
    Dictionary<int, List<long>> dictionary = new Dictionary<int, List<long>>();
    int num1 = 0;
    int index = 0;
    foreach (KeyValuePair<int, List<long>> keyValuePair in dict)
    {
      foreach (long num2 in keyValuePair.Value)
      {
        if (dictionaryList.Count <= index)
          dictionaryList.Add(new Dictionary<int, List<long>>());
        if (!dictionaryList[index].ContainsKey(keyValuePair.Key))
          dictionaryList[index].Add(keyValuePair.Key, new List<long>());
        if (!dictionaryList[index][keyValuePair.Key].Contains(num2))
        {
          dictionaryList[index][keyValuePair.Key].Add(num2);
          ++num1;
          if (num1 > 990)
          {
            num1 = 0;
            ++index;
          }
        }
      }
    }
    dict.Clear();
    return dictionaryList.ToArray();
  }

  private DataTable InternalLoadCompositionApplicability(
    IUserSession session,
    long partID,
    int relationTypeID,
    List<ColumnDescriptor> columns,
    string filtrationOwnerID,
    params int[] childObjectTypes)
  {
    if (session == null)
      throw new KernelExceptionID(sc_17056.ssp_appserver_17057(910347530), (object) "CompositionLoadService.InternalLoadCompositions");
    DataTable dataTable = (DataTable) null;
    if (partID == 0L || relationTypeID == -1)
      return (DataTable) null;
    ColumnDescriptor[] array = columns.ToArray();
    object[] objArray = new object[0];
    SortOrders[] sortOrdersArray = new SortOrders[0];
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[0], array);
    if (ServerServices.GetService(typeof (ICompositionLoadService)) is ICompositionLoadService service)
    {
      IUserSession userSession = session;
      long[] ids = new long[1]{ partID };
      int relationTypeId = relationTypeID;
      List<int> compositionTypes = service.GetPresentCompositionTypes((object) userSession, (IEnumerable<long>) ids, relationTypeId, false);
      childObjectTypes = compositionTypes != null ? compositionTypes.ToArray() : (int[]) null;
    }
    IDBRelationCollection relationCollection = session.GetRelationCollection(relationTypeID, filtrationOwnerID);
    if (relationCollection != null)
    {
      if (childObjectTypes != null && childObjectTypes.Length != 0)
        relationCollection.ChildObjectTypes = (IList<int>) new List<int>((IEnumerable<int>) childObjectTypes);
      if (paramSet.Tags == null)
        paramSet.Tags = new HybridDictionary();
      dataTable = relationCollection.EntersInVersion(paramSet, partID);
    }
    return dataTable;
  }

  private List<DataTable> BuildParentTree(
    List<RelVisInfo> partIdDict,
    IUserSession userSession,
    string filtrationOwnerId,
    ICompositionsAutosortRule rule,
    IDBRelationsApplicabilityCollection idbRelationAppCollect,
    Dictionary<long, List<long>> childVersionCache,
    HybridDictionary dict)
  {
    List<DataTable> dataTableList = new List<DataTable>();
    if (partIdDict.Count == 0)
      return dataTableList;
    int attributeTypeId1 = MetaDataHelper.GetAttributeTypeID(new Guid("cad00267-306c-11d8-b4e9-00304f19f545"));
    DataTable unitedRelList = (DataTable) null;
    List<long> longList = new List<long>();
    List<RelVisInfo> relVisInfoList = new List<RelVisInfo>();
    foreach (RelVisInfo relVisInfo in partIdDict)
    {
      long objectId = relVisInfo.ObjectID;
      int relationTypeId = relVisInfo.RelationTypeID;
      bool flag1 = true;
      IMSRelationType relationType = MetaDataHelper.GetRelationType(relationTypeId);
      if (relationType != null && !relationType.AnyAttributes && MetaDataHelper.GetAttribute4RelationType(relationTypeId, attributeTypeId1) == null)
        flag1 = false;
      List<ColumnDescriptor> columns = new List<ColumnDescriptor>();
      columns.Add(new ColumnDescriptor((object) -21, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
      columns.Add(new ColumnDescriptor((object) -22, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
      columns.Add(new ColumnDescriptor((object) -7, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
      columns.Add(new ColumnDescriptor((object) -50, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
      columns.Add(new ColumnDescriptor((object) -23, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
      columns.Add(new ColumnDescriptor((object) -20, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
      columns.Add(new ColumnDescriptor((object) -77, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0));
      columns.Add(new ColumnDescriptor((object) -9, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
      if (flag1)
        columns.Add(new ColumnDescriptor((object) attributeTypeId1, AttributeSourceTypes.Relation, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0));
      int attributeTypeId2 = MetaDataHelper.GetAttributeTypeID(new Guid(RelVisObserverService._relTypeCAD));
      columns.Add(new ColumnDescriptor((object) attributeTypeId2, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0));
      List<int> intList = new List<int>();
      List<int> parentTypes = this.GetParentTypes(relVisInfo, idbRelationAppCollect);
      DataTable dataTable = this.InternalLoadCompositionApplicability(userSession, objectId, relationTypeId, columns, filtrationOwnerId, parentTypes.ToArray());
      if (!dataTable.Columns.Contains("F_OBJECT_ID"))
      {
        dataTable.Columns.Add(new DataColumn("F_OBJECT_ID", typeof (int)));
        for (int index = 0; index < dataTable.Rows.Count; ++index)
          dataTable.Rows[index]["F_OBJECT_ID"] = (object) objectId;
      }
      if (dataTable != null)
      {
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          long int64 = Convert.ToInt64(row["F_PROJ_ID"]);
          Convert.ToInt64(row["F_PART_ID"]);
          if (!longList.Contains(int64))
            longList.Add(int64);
        }
      }
      if (dataTable != null)
      {
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          long int64 = Convert.ToInt64(row["F_PROJ_ID"]);
          Convert.ToInt64(row["F_PART_ID"]);
          int int32 = Convert.ToInt32(row["F_OBJECT_TYPE"]);
          this.AddPart2Dictionary(rule, relVisInfo, relVisInfoList, int32, int64, RelVisPred.RelVisLayers.ParentTree, idbRelationAppCollect);
        }
        bool flag2 = true;
        if (dict.Contains((object) RelVisObserverService._showStructureLinks))
          flag2 = Convert.ToBoolean(dict[(object) RelVisObserverService._showStructureLinks]);
        bool flag3 = true;
        if (dict.Contains((object) RelVisObserverService._showAssociativeLinks))
          flag3 = Convert.ToBoolean(dict[(object) RelVisObserverService._showAssociativeLinks]);
        if (dataTable != null)
        {
          for (int index = dataTable.Rows.Count - 1; index >= 0; --index)
          {
            DataRow row = dataTable.Rows[index];
            long int64 = Convert.ToInt64(row["F_PROJ_ID"]);
            Convert.ToInt64(row["F_PART_ID"]);
            int int32_1 = Convert.ToInt32(row["F_OBJECT_TYPE"]);
            bool flag4 = true;
            if (flag2 ^ flag3)
            {
              object obj = row[RelVisObserverService._relTypeCAD];
              int int32_2 = obj != DBNull.Value ? Convert.ToInt32(obj) : 0;
              flag4 = flag3 && int32_2 == 1 || flag2 && int32_2 == 0;
            }
            if (flag4)
              this.AddPart2Dictionary(rule, relVisInfo, relVisInfoList, int32_1, int64, RelVisPred.RelVisLayers.ParentTree, idbRelationAppCollect);
            else
              dataTable.Rows.RemoveAt(index);
          }
        }
        unitedRelList = this.MergeTables(unitedRelList, dataTable);
      }
    }
    Dictionary<long, List<long>> childVersionCache1 = new Dictionary<long, List<long>>();
    foreach (DataRow row in (InternalDataCollectionBase) unitedRelList.Rows)
    {
      long int64_1 = Convert.ToInt64(row["F_PROJ_ID"]);
      long int64_2 = Convert.ToInt64(row["F_PART_ID"]);
      Convert.ToInt32(row["F_OBJECT_TYPE"]);
      if (!childVersionCache1.ContainsKey(int64_2))
        childVersionCache1.Add(int64_2, new List<long>());
      if (!childVersionCache1[int64_2].Contains(int64_1))
        childVersionCache1[int64_2].Add(int64_1);
    }
    if (unitedRelList != null)
      dataTableList.Add(unitedRelList);
    if (relVisInfoList.Count > 0)
    {
      List<DataTable> collection = this.BuildParentTree(relVisInfoList, userSession, filtrationOwnerId, rule, idbRelationAppCollect, childVersionCache1, dict);
      dataTableList.AddRange((IEnumerable<DataTable>) collection);
    }
    return dataTableList;
  }

  private List<int> GetParentTypes(
    RelVisInfo info,
    IDBRelationsApplicabilityCollection idbRelationAppCollect)
  {
    int objectTypeId = info.ObjectTypeID;
    List<int> intList1 = new List<int>();
    if (idbRelationAppCollect != null)
    {
      DataTable applicabilitiesList = idbRelationAppCollect.GetApplicabilitiesList(info.RelationTypeID, objectTypeId, -1);
      if (applicabilitiesList != null)
      {
        foreach (DataRow row in (InternalDataCollectionBase) applicabilitiesList.Rows)
        {
          int int32 = Convert.ToInt32(row["F_INOBJECT_TYPE"]);
          if (!intList1.Contains(int32))
            intList1.Add(int32);
        }
      }
    }
    List<int> intList2 = new List<int>();
    foreach (int parentTypeID in intList1)
      intList2.AddRange((IEnumerable<int>) MetaDataHelper.GetLocalObjectTypeChildrenIDRecursive(parentTypeID));
    List<int> parentTypes = new List<int>();
    foreach (int num in intList2)
    {
      if (MetaDataHelper.IsLocalObjectType(num) && MetaDataHelper.GetObjectType(num).VersionsMode != ObjectVersionModes.Abstract)
        parentTypes.Add(num);
    }
    return parentTypes;
  }

  private void UpdateParentLevel(
    IUserSession userSession,
    DataTable relTable,
    List<long> parentIdList,
    Dictionary<long, List<long>> childVersionCache)
  {
    if (parentIdList.Count == 0 || relTable == null)
      return;
    DBRecordSetParams paramSet = new DBRecordSetParams(new List<ConditionStructure>()
    {
      new ConditionStructure(-2, RelationalOperators.In, (object) parentIdList.ToArray(), LogicalOperators.NONE, 0, false)
    }.ToArray(), new List<ColumnDescriptor>()
    {
      new ColumnDescriptor((object) -3, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
      new ColumnDescriptor((object) -2, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
      new ColumnDescriptor((object) -7, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
      new ColumnDescriptor((object) -50, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
      new ColumnDescriptor((object) -77, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0),
      new ColumnDescriptor((object) -9, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.FieldName, SortOrders.NONE, 0)
    }.ToArray());
    DataTable table = userSession.GetObjectCollection(-1).Select(paramSet);
    if (!relTable.Columns.Contains("F_OBJECT_TYPE"))
      relTable.Columns.Add(new DataColumn("F_OBJECT_TYPE", typeof (int)));
    if (!relTable.Columns.Contains("CAPTION"))
      relTable.Columns.Add(new DataColumn("CAPTION", typeof (string)));
    if (!relTable.Columns.Contains("F_ID"))
      relTable.Columns.Add(new DataColumn("F_ID", typeof (long)));
    if (!relTable.Columns.Contains("F_LEVEL_ID"))
      relTable.Columns.Add(new DataColumn("F_LEVEL_ID", typeof (int)));
    for (int index1 = 0; index1 < relTable.Rows.Count; ++index1)
    {
      DataRow row1 = relTable.Rows[index1];
      long int64_1 = Convert.ToInt64(row1["F_PROJ_ID"]);
      long int64_2 = Convert.ToInt64(row1["F_PART_ID"]);
      DataRow dataRowsById = this.FindDataRowsByID(table, int64_1);
      if (dataRowsById != null)
      {
        int int32_1 = Convert.ToInt32(dataRowsById["F_OBJECT_TYPE"]);
        string str = Convert.ToString(dataRowsById["CAPTION"]);
        int int32_2 = Convert.ToInt32(dataRowsById["F_LEVEL_ID"]);
        long int32_3 = (long) Convert.ToInt32(dataRowsById["F_ID"]);
        row1["F_OBJECT_TYPE"] = (object) int32_1;
        row1["CAPTION"] = (object) str;
        row1["F_LEVEL_ID"] = (object) int32_2;
        row1["F_ID"] = (object) int32_3;
        for (int index2 = 0; index2 < childVersionCache[int64_2].Count; ++index2)
        {
          long num = childVersionCache[int64_2][index2];
          if (index2 > 0)
          {
            DataRow row2 = relTable.NewRow();
            row2.ItemArray = row1.ItemArray;
            row1["F_PART_ID"] = (object) num;
            relTable.Rows.InsertAt(row2, index1);
            ++index1;
          }
          else
            row1["F_PART_ID"] = (object) num;
        }
      }
    }
  }

  private List<int> GetChildTypes(RelVisInfo info)
  {
    return MetaDataHelper.GetApplicabilityChildObjectTypesID(info.ObjectTypeID, info.RelationTypeID);
  }

  private List<DataTable> BuildChildTree(
    List<RelVisInfo> projIDDict,
    IUserSession userSession,
    string filtrationOwnerId,
    ICompositionsAutosortRule rule,
    IDBRelationsApplicabilityCollection idbRelationAppCollect,
    int curLevel,
    int levels,
    bool showHiddenObjects,
    bool showHiddenSostav,
    HybridDictionary dict)
  {
    List<DataTable> dataTableList = new List<DataTable>();
    if (levels > 0 && curLevel >= levels || projIDDict.Count == 0)
      return dataTableList;
    MetaDataHelper.GetAttributeTypeID(new Guid("cad00267-306c-11d8-b4e9-00304f19f545"));
    List<ColumnDescriptor> columns = new List<ColumnDescriptor>();
    columns.Add(new ColumnDescriptor((object) -20, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
    columns.Add(new ColumnDescriptor((object) -23, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
    columns.Add(new ColumnDescriptor((object) -21, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
    columns.Add(new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
    columns.Add(new ColumnDescriptor((object) -7, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
    columns.Add(new ColumnDescriptor((object) -77, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0));
    columns.Add(new ColumnDescriptor((object) -9, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
    columns.Add(new ColumnDescriptor((object) -50, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
    int attributeTypeId1 = MetaDataHelper.GetAttributeTypeID(new Guid("cad00267-306c-11d8-b4e9-00304f19f545"));
    columns.Add(new ColumnDescriptor((object) attributeTypeId1, AttributeSourceTypes.Relation, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0));
    int attributeTypeId2 = MetaDataHelper.GetAttributeTypeID(new Guid(RelVisObserverService._relTypeCAD));
    columns.Add(new ColumnDescriptor((object) attributeTypeId2, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0));
    List<RelVisInfo> relVisInfoList = new List<RelVisInfo>();
    List<long> longList = new List<long>();
    DataTable dataTable = (DataTable) null;
    foreach (RelVisInfo relVisInfo in projIDDict)
    {
      long objectId = relVisInfo.ObjectID;
      int relationTypeId = relVisInfo.RelationTypeID;
      List<int> childTypes = this.GetChildTypes(relVisInfo);
      ICompositionLoadService service = ServerServices.GetService(typeof (ICompositionLoadService)) as ICompositionLoadService;
      DataTable table = (DataTable) null;
      if (service != null)
        table = service.LoadComposition((object) userSession, objectId, relationTypeId, (IEnumerable<ColumnDescriptor>) columns, filtrationOwnerId, dict, childTypes.ToArray());
      bool flag1 = true;
      if (dict.Contains((object) RelVisObserverService._showStructureLinks))
        flag1 = Convert.ToBoolean(dict[(object) RelVisObserverService._showStructureLinks]);
      bool flag2 = true;
      if (dict.Contains((object) RelVisObserverService._showAssociativeLinks))
        flag2 = Convert.ToBoolean(dict[(object) RelVisObserverService._showAssociativeLinks]);
      if (table != null)
      {
        for (int index = table.Rows.Count - 1; index >= 0; --index)
        {
          DataRow row = table.Rows[index];
          long int64 = Convert.ToInt64(row["F_OBJECT_ID"]);
          int int32_1 = Convert.ToInt32(row["F_OBJECT_TYPE"]);
          bool flag3 = true;
          if (flag1 ^ flag2)
          {
            object obj = row[RelVisObserverService._relTypeCAD];
            int int32_2 = obj != DBNull.Value ? Convert.ToInt32(obj) : 0;
            flag3 = flag2 && int32_2 == 1 || flag1 && int32_2 == 0;
          }
          if (flag3)
            this.AddPart2Dictionary(rule, relVisInfo, relVisInfoList, int32_1, int64, RelVisPred.RelVisLayers.ChildTree, idbRelationAppCollect);
          else
            table.Rows.RemoveAt(index);
        }
      }
      if (dataTable == null)
        dataTable = table;
      else
        dataTable.Merge(table);
    }
    if (dataTable != null)
      dataTableList.Add(dataTable);
    if (relVisInfoList.Count > 0)
    {
      List<DataTable> collection = this.BuildChildTree(relVisInfoList, userSession, filtrationOwnerId, rule, idbRelationAppCollect, curLevel + 1, levels, showHiddenObjects, showHiddenSostav, dict);
      dataTableList.AddRange((IEnumerable<DataTable>) collection);
    }
    return dataTableList;
  }

  private DataTable MergeTables(DataTable unitedRelList, DataTable dataTable)
  {
    if (unitedRelList == null)
      unitedRelList = dataTable;
    else
      unitedRelList.Merge(dataTable);
    return unitedRelList;
  }

  private DataRow FindDataRowsByID(DataTable table, long Id)
  {
    foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
    {
      if (Convert.ToInt64(row["F_OBJECT_ID"]) == Id)
        return row;
    }
    return (DataRow) null;
  }

  private IUserSession ToUserSession(object usObject)
  {
    switch (usObject)
    {
      case IUserSession _:
        return usObject as IUserSession;
      case Guid sessionGUID:
        return UserSession.GetSessionByID(sessionGUID);
      default:
        return (IUserSession) null;
    }
  }

  private List<int> GetParentRelationTypes4ObjType(
    int objType,
    IDBRelationsApplicabilityCollection idbRelationAppCollect)
  {
    List<int> relationTypes4ObjType1 = (List<int>) null;
    if (RelVisObserverService.ParentRelTypes4ObjTypeCache.TryGetValue(objType, out relationTypes4ObjType1))
      return relationTypes4ObjType1;
    List<int> relationTypes4ObjType2 = new List<int>();
    if (idbRelationAppCollect != null)
    {
      DataTable applicabilitiesList = idbRelationAppCollect.GetApplicabilitiesList(-1, objType, -1);
      if (applicabilitiesList != null)
      {
        foreach (DataRow row in (InternalDataCollectionBase) applicabilitiesList.Rows)
        {
          int int32_1 = Convert.ToInt32(row["F_RELATION_TYPE"]);
          int int32_2 = Convert.ToInt32(row["F_INOBJECT_TYPE"]);
          if (!relationTypes4ObjType2.Contains(int32_1))
          {
            IDBRelationsApplicability applicability = idbRelationAppCollect.GetApplicability(int32_1, objType, int32_2);
            if (applicability == null || applicability.ApplicabilityMode != ApplicabilityModes.Disabled)
              relationTypes4ObjType2.Add(int32_1);
          }
        }
      }
    }
    RelVisObserverService.ParentRelTypes4ObjTypeCache.Add(objType, relationTypes4ObjType2);
    return relationTypes4ObjType2;
  }

  private List<int> GetRelationTypes4ObjTypeId(ICompositionsAutosortRule rule, int objTypeId)
  {
    return rule.GetObjectTypeVisibleRelations(objTypeId, true);
  }

  private List<int> GetRelationTypes4ObjTypeId(ICompositionsAutosortRule rule, List<int> objTypes)
  {
    List<int> relationTypes4ObjTypeId = new List<int>();
    foreach (int objType in objTypes)
    {
      foreach (int num in this.GetRelationTypes4ObjTypeId(rule, objType))
      {
        if (!relationTypes4ObjTypeId.Contains(num))
          relationTypes4ObjTypeId.Add(num);
      }
    }
    return relationTypes4ObjTypeId;
  }

  private void AddPart2Dictionary(
    ICompositionsAutosortRule rule,
    RelVisInfo parentInfo,
    List<RelVisInfo> dict,
    int objTypeId,
    long objVerId,
    RelVisPred.RelVisLayers layer,
    IDBRelationsApplicabilityCollection idbRelationAppCollect)
  {
    List<int> intList = new List<int>();
    foreach (int relationTypeID in layer != RelVisPred.RelVisLayers.ChildTree ? this.GetParentRelationTypes4ObjType(objTypeId, idbRelationAppCollect) : this.GetRelationTypes4ObjTypeId(rule, objTypeId))
    {
      if (!parentInfo.ParentIds.Contains(objVerId))
      {
        RelVisInfo relVisInfo = new RelVisInfo(objVerId, objTypeId, relationTypeID);
        relVisInfo.ParentIds.AddRange((IEnumerable<long>) parentInfo.ParentIds);
        relVisInfo.ParentIds.Add(objVerId);
        if (!dict.Contains(relVisInfo))
          dict.Add(relVisInfo);
      }
    }
  }

  public long StartBuildChildTree(
    long projID,
    string filtrationOwnerId,
    ICompositionsAutosortRule rule,
    int objType,
    Guid userSession,
    int levels,
    bool showHiddenObjects,
    bool showHiddenSostav,
    HybridDictionary dict)
  {
    long num = Interlocked.Increment(ref this.taskCounter);
    TaskInfo parameter = new TaskInfo(true, num);
    parameter.SetParms(projID, filtrationOwnerId, rule, objType, userSession, dict, levs: levels, hiddObjs: showHiddenObjects, hiddSost: showHiddenSostav);
    parameter.thread = new Thread(new ParameterizedThreadStart(this._DoBuildChilds));
    this.taskDict.TryAdd(num, parameter);
    parameter.thread.Start((object) parameter);
    return num;
  }

  public long StartBuildParentTree(
    long projVID,
    long projId,
    string filtrationOwnerId,
    ICompositionsAutosortRule rule,
    int objType,
    Guid userSession,
    HybridDictionary dict)
  {
    long num = Interlocked.Increment(ref this.taskCounter);
    TaskInfo parameter = new TaskInfo(false, num);
    parameter.SetParms(projId, filtrationOwnerId, rule, objType, userSession, dict, projVID);
    parameter.thread = new Thread(new ParameterizedThreadStart(this._DoBuidParents));
    this.taskDict.TryAdd(num, parameter);
    parameter.thread.Start((object) parameter);
    return num;
  }

  public RelVisState GetTaskStatus(long taskId)
  {
    TaskInfo taskInfo = (TaskInfo) null;
    if (!this.taskDict.TryGetValue(taskId, out taskInfo))
      return RelVisState.Unknown;
    lock (taskInfo)
      return taskInfo.state;
  }

  public void KillTask(long taskId)
  {
    TaskInfo taskInfo = (TaskInfo) null;
    if (!this.taskDict.TryGetValue(taskId, out taskInfo))
      return;
    lock (taskInfo)
    {
      if (taskInfo.thread != null)
      {
        if (taskInfo.thread.IsAlive)
          taskInfo.thread.Abort();
      }
    }
    this.taskDict.TryRemove(taskId, out taskInfo);
  }

  public DataTable[] GetTaskResult(long taskId)
  {
    TaskInfo taskInfo = (TaskInfo) null;
    if (!this.taskDict.TryGetValue(taskId, out taskInfo))
      return (DataTable[]) null;
    lock (taskInfo)
      return taskInfo.result;
  }

  private void _DoBuildChilds(object o)
  {
    if (!(o is TaskInfo taskInfo))
      return;
    try
    {
      lock (taskInfo)
        taskInfo.state = RelVisState.Working;
      taskInfo.result = this.GetChildTree(taskInfo.projId, taskInfo.filtOwnerId, taskInfo.rule, taskInfo.objType, taskInfo.userSession, taskInfo.showHiddenObjs, taskInfo.showHiddenSostav, taskInfo.dict);
    }
    catch (Exception ex)
    {
      lock (taskInfo)
        taskInfo.state = RelVisState.Error;
      if (!(ex is ThreadAbortException))
      {
        if (ex.InnerException != null)
        {
          if (ex.InnerException is ThreadAbortException)
            goto label_17;
        }
        throw;
      }
    }
label_17:
    lock (taskInfo)
    {
      if (taskInfo.state == RelVisState.Error)
        return;
      taskInfo.state = RelVisState.Ready;
    }
  }

  private void _DoBuidParents(object o)
  {
    if (!(o is TaskInfo taskInfo))
      return;
    try
    {
      lock (taskInfo)
        taskInfo.state = RelVisState.Working;
      taskInfo.result = this.GetParentTree(taskInfo.projVId, taskInfo.projId, taskInfo.filtOwnerId, taskInfo.rule, taskInfo.objType, taskInfo.userSession, taskInfo.dict);
    }
    catch (Exception ex)
    {
      lock (taskInfo)
        taskInfo.state = RelVisState.Error;
      if (!(ex is ThreadAbortException))
      {
        if (ex.InnerException != null)
        {
          if (ex.InnerException is ThreadAbortException)
            goto label_17;
        }
        throw;
      }
    }
label_17:
    lock (taskInfo)
    {
      if (taskInfo.state == RelVisState.Error)
        return;
      taskInfo.state = RelVisState.Ready;
    }
  }
}
