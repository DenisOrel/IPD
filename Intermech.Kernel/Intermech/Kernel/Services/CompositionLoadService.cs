// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.CompositionLoadService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Compositions.CompositionService;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using Intermech.Kernel.Services.Compositions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;


namespace Intermech.Kernel.Services;

public class CompositionLoadService : LongLifeObject, ICompositionLoadService, ITypedInfoService
{
  private long _modificationId;

  private IUserSession GetUserSession(object session)
  {
    switch (session)
    {
      case IUserSession userSession:
        return userSession;
      case Guid sessionGUID:
        return UserSession.GetSessionByID(sessionGUID);
      case string g:
        return UserSession.GetSessionByID(new Guid(g));
      default:
        return (IUserSession) null;
    }
  }

  protected internal DataTable InternalLoadComposition(
    IUserSession session,
    object projCond,
    IEnumerable<ColumnDescriptor> columns,
    IDBRelationCollection comp,
    IEnumerable<ConditionStructure> conditions,
    HybridDictionary dbParamTags,
    params int[] childObjectTypes)
  {
    ColumnDescriptor[] array = columns.ToArray<ColumnDescriptor>();
    ConditionStructure[] conditionStructureArray = new ConditionStructure[1]
    {
      !(projCond is long[] numArray) || numArray.Length != 1 ? new ConditionStructure(-21, RelationalOperators.In, projCond, LogicalOperators.NONE, 0, true) : new ConditionStructure(-21, RelationalOperators.Equal, (object) numArray[0], LogicalOperators.NONE, 0, true)
    };
    if (conditions != null && conditions.Any<ConditionStructure>())
      conditionStructureArray = ConditionStructure.Join(conditions.ToArray<ConditionStructure>(), conditionStructureArray);
    DBRecordSetParams paramSet = new DBRecordSetParams(conditionStructureArray, array);
    if (dbParamTags != null)
      paramSet.Tags = dbParamTags;
    try
    {
      ((DBRecordSet) comp)._CurrentModificationID = this._modificationId;
      comp.ChildObjectTypes = childObjectTypes == null || childObjectTypes.Length == 0 ? (IList<int>) null : (IList<int>) childObjectTypes;
      return comp.Select(paramSet);
    }
    finally
    {
      ((DBRecordSet) comp)._CurrentModificationID = 0L;
      this._modificationId = 0L;
    }
  }

  private DataTable InternalLoadComposition(
    object session,
    long projId,
    int relationTypeId,
    IEnumerable<ColumnDescriptor> columns,
    string filtrationOwnerId,
    VersionsRule rule,
    IEnumerable<ConditionStructure> conditions,
    params int[] childObjectTypes)
  {
    return this.InternalLoadComposition1(session, projId, relationTypeId, columns, filtrationOwnerId, rule, conditions, (HybridDictionary) null, childObjectTypes);
  }

  private DataTable InternalLoadComposition1(
    object session,
    long projId,
    int relationTypeId,
    IEnumerable<ColumnDescriptor> columns,
    string filtrationOwnerId,
    VersionsRule rule,
    IEnumerable<ConditionStructure> conditions,
    HybridDictionary dbParamTags,
    params int[] childObjectTypes)
  {
    if (relationTypeId == -1)
      return (DataTable) null;
    IUserSession userSession = this.GetUserSession(session) ?? throw new KernelExceptionID(sc_13895.ssp_appserver_13896(1484247747), (object) "CompositionLoadService.InternalLoadComposition");
    long[] projIds = new long[1]{ projId };
    int[] relTypeIds = new int[1]{ relationTypeId };
    IEnumerable<ColumnDescriptor> columns1 = columns;
    string filtrationOwnerId1 = filtrationOwnerId;
    VersionsRule rule1 = rule;
    IEnumerable<ConditionStructure> conditions1 = conditions;
    Dictionary<long, HybridDictionary> dbParamTags1;
    if (dbParamTags == null)
    {
      dbParamTags1 = (Dictionary<long, HybridDictionary>) null;
    }
    else
    {
      dbParamTags1 = new Dictionary<long, HybridDictionary>();
      dbParamTags1.Add(projId, dbParamTags);
    }
    int[] numArray = childObjectTypes;
    return this.InternalLoadComplexCompositions(userSession, (IEnumerable<long>) projIds, (IEnumerable<int>) relTypeIds, columns1, filtrationOwnerId1, rule1, conditions1, (IDictionary<long, HybridDictionary>) dbParamTags1, numArray);
  }

  public DataTable InternalLoadCompositions(
    object usrSession,
    long projId,
    IEnumerable<ColumnDescriptor> columns,
    string filtrationOwnerId,
    VersionsRule rule,
    params int[] childObjectTypes)
  {
    IUserSession userSession = this.GetUserSession(usrSession);
    if (userSession == null)
      throw new KernelExceptionID(sc_13895.ssp_appserver_13897(1465970100), (object) "CompositionLoadService.InternalLoadCompositions");
    if (projId == 0L)
      return (DataTable) null;
    List<int> visibleRelations = ServiceUtils.GetService<ICompositionsAutomaticSortingService>((object) userSession, true).GetAutosortRule((object) userSession.SessionGUID, false).GetObjectTypeVisibleRelations(userSession.GetObjectInfo(projId).ObjectTypeID, true);
    return this.InternalLoadCompositions((object) userSession, projId, (IEnumerable<int>) visibleRelations, columns, filtrationOwnerId, rule, childObjectTypes);
  }

  public DataTable InternalLoadCompositions(
    object usrSession,
    long projId,
    IEnumerable<int> visibleRelTypes,
    IEnumerable<ColumnDescriptor> columns,
    string filtrationOwnerId,
    VersionsRule rule,
    params int[] childObjectTypes)
  {
    return this.InternalLoadComplexCompositions(this.GetUserSession(usrSession) ?? throw new KernelExceptionID(sc_13895.ssp_appserver_13898(553098297), (object) "CompositionLoadService.InternalLoadCompositions"), (IEnumerable<long>) new long[1]
    {
      projId
    }, visibleRelTypes, columns, filtrationOwnerId, rule, (IEnumerable<ConditionStructure>) null, (IDictionary<long, HybridDictionary>) null, childObjectTypes);
  }

  public DataTable InternalLoadComplexCompositions(
    object usrSession,
    IEnumerable<long> projIDs,
    int relTypeId,
    IEnumerable<ColumnDescriptor> columns,
    string filtrationOwnerId,
    VersionsRule rule,
    params int[] childObjectTypes)
  {
    return this.InternalLoadComplexCompositions(this.GetUserSession(usrSession) ?? throw new KernelExceptionID(sc_13895.ssp_appserver_13899(1526101827), (object) "CompositionLoadService.InternalLoadCompositions"), projIDs, (IEnumerable<int>) new int[1]
    {
      relTypeId
    }, columns, filtrationOwnerId, rule, (IEnumerable<ConditionStructure>) null, (IDictionary<long, HybridDictionary>) null, childObjectTypes);
  }

  private DataTable InternalLoadComplexCompositions(
    IUserSession userSession,
    IEnumerable<long> projIds,
    IEnumerable<int> relTypeIds,
    IEnumerable<ColumnDescriptor> columns,
    string filtrationOwnerId,
    VersionsRule rule,
    IEnumerable<ConditionStructure> conditions,
    IDictionary<long, HybridDictionary> dbParamTags,
    params int[] childObjectTypes)
  {
    if (userSession == null)
      throw new KernelExceptionID(sc_13895.ssp_appserver_13900(242042914), (object) "CompositionLoadService.InternalLoadCompositions");
    if (projIds == null || !projIds.Any<long>())
      return (DataTable) null;
    if (columns == null || !columns.Any<ColumnDescriptor>())
      return (DataTable) null;
    if (relTypeIds == null || !relTypeIds.Any<int>())
      return (DataTable) null;
    CompositionLoadingParams loadingParams = new CompositionLoadingParams((IEnumerable<ObjInfoItem>) ObjInfoHelper.GetObjectInfoList(projIds), childObjectTypes == null || !((IEnumerable<int>) childObjectTypes).Any<int>() ? (IEnumerable<int>) (int[]) null : (IEnumerable<int>) childObjectTypes, (IEnumerable<int>) null, relTypeIds, columns, conditions, true, false, 1, rule, filtrationOwnerId, dbParamTags);
    return this.LoadComplexCompositions((object) userSession, loadingParams);
  }

  private long InternalFindCompositionParentObject(
    object usrSession,
    long partId,
    int relationTypeId,
    string filtrationOwnerId)
  {
    IUserSession userSession = this.GetUserSession(usrSession);
    if (userSession == null)
      throw new KernelExceptionID(sc_13895.ssp_appserver_13901(643374958), (object) "CompositionLoadService.InternalLoadCompositions");
    List<ColumnDescriptor> columnDescriptorList = new List<ColumnDescriptor>(1);
    columnDescriptorList.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PROJ_ID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0));
    DataTable dataTable = (DataTable) null;
    if (partId == 0L || relationTypeId == -1)
      return 0;
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[0], columnDescriptorList.ToArray());
    IDBRelationCollection relationCollection = userSession.GetRelationCollection(relationTypeId, filtrationOwnerId);
    if (relationCollection != null)
    {
      try
      {
        ((DBRecordSet) relationCollection)._CurrentModificationID = this._modificationId;
        dataTable = relationCollection.EntersInVersion(paramSet, partId);
      }
      finally
      {
        ((DBRecordSet) relationCollection)._CurrentModificationID = 0L;
        this._modificationId = 0L;
      }
    }
    return dataTable == null || dataTable.Rows.Count <= 0 ? 0L : DataSetProcessor.GetInt64Value(dataTable.Rows[0], 0, 0L);
  }

  private long InternalFindCompositionParentObject(
    object usrSession,
    long partId,
    int relationTypeId,
    VersionsRule rule)
  {
    IUserSession userSession = this.GetUserSession(usrSession);
    if (userSession == null)
      throw new KernelExceptionID(sc_13895.ssp_appserver_13902(1322625565), (object) "CompositionLoadService.InternalLoadCompositions");
    List<ColumnDescriptor> columnDescriptorList = new List<ColumnDescriptor>(1);
    columnDescriptorList.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PROJ_ID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0));
    DataTable dataTable = (DataTable) null;
    if (partId == 0L || relationTypeId == -1)
      return 0;
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[0], columnDescriptorList.ToArray());
    IDBRelationCollection relationCollection = userSession.GetRelationCollection(relationTypeId);
    if (relationCollection != null)
    {
      try
      {
        relationCollection.FiltrationRule = rule;
        ((DBRecordSet) relationCollection)._CurrentModificationID = this._modificationId;
        dataTable = relationCollection.EntersInVersion(paramSet, partId);
      }
      finally
      {
        ((DBRecordSet) relationCollection)._CurrentModificationID = 0L;
        this._modificationId = 0L;
      }
    }
    return dataTable == null || dataTable.Rows.Count <= 0 ? 0L : DataSetProcessor.GetInt64Value(dataTable.Rows[0], 0, 0L);
  }

  private List<long> InternalLoadCompositionObjects(
    object usrSession,
    long projId,
    int relationTypeId,
    string filtrationOwnerId,
    params int[] childObjectTypes)
  {
    if (this.GetUserSession(usrSession) == null)
      throw new KernelExceptionID(sc_13895.ssp_appserver_13903(1326820200), (object) "CompositionLoadService.InternalLoadCompositionObjects");
    List<long> longList = new List<long>();
    DataTable dataTable = this.InternalLoadComposition(usrSession, projId, relationTypeId, (IEnumerable<ColumnDescriptor>) new List<ColumnDescriptor>(1)
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0)
    }, filtrationOwnerId, (VersionsRule) null, (IEnumerable<ConditionStructure>) null, childObjectTypes);
    if (dataTable == null)
      return longList;
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      long int64Value = DataSetProcessor.GetInt64Value(dataTable.Rows[index], 0, 0L);
      if (longList.IndexOf(int64Value) < 0 && int64Value != 0L)
        longList.Add(int64Value);
    }
    return longList;
  }

  private List<long> InternalLoadCompositionObjects(
    object usrSession,
    long projId,
    int relationTypeId,
    VersionsRule rule,
    params int[] childObjectTypes)
  {
    if (this.GetUserSession(usrSession) == null)
      throw new KernelExceptionID(sc_13895.ssp_appserver_13904(789239980), (object) "CompositionLoadService.InternalLoadCompositionObjects");
    List<long> longList = new List<long>();
    DataTable dataTable = this.InternalLoadComposition(usrSession, projId, relationTypeId, (IEnumerable<ColumnDescriptor>) new List<ColumnDescriptor>(1)
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0)
    }, string.Empty, rule, (IEnumerable<ConditionStructure>) null, childObjectTypes);
    if (dataTable == null)
      return longList;
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      long int64Value = DataSetProcessor.GetInt64Value(dataTable.Rows[index], 0, 0L);
      if (longList.IndexOf(int64Value) < 0 && int64Value != 0L)
        longList.Add(int64Value);
    }
    return longList;
  }

  private List<TypedObjectInfo> InternalLoadCompositionTypedObjects(
    object usrSession,
    long projId,
    int relationTypeId,
    string filtrationOwnerId,
    params int[] childObjectTypes)
  {
    if (this.GetUserSession(usrSession) == null)
      throw new KernelExceptionID(sc_13895.ssp_appserver_13905(1112647472), (object) "CompositionLoadService.InternalLoadCompositionObjects");
    List<TypedObjectInfo> typedObjectInfoList = new List<TypedObjectInfo>();
    DataTable dataTable = this.InternalLoadComposition(usrSession, projId, relationTypeId, (IEnumerable<ColumnDescriptor>) new List<ColumnDescriptor>(2)
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0)
    }, filtrationOwnerId, (VersionsRule) null, (IEnumerable<ConditionStructure>) null, childObjectTypes);
    if (dataTable != null)
    {
      for (int index = 0; index < dataTable.Rows.Count; ++index)
      {
        long int64Value = DataSetProcessor.GetInt64Value(dataTable.Rows[index], 0, 0L);
        int int32Value = DataSetProcessor.GetInt32Value(dataTable.Rows[index], 1, -1);
        TypedObjectInfo typedObjectInfo = new TypedObjectInfo(int64Value, int32Value);
        if (typedObjectInfoList.IndexOf(typedObjectInfo) < 0 && int64Value != 0L && int32Value != -1)
          typedObjectInfoList.Add(typedObjectInfo);
      }
    }
    return typedObjectInfoList;
  }

  private List<TypedObjectInfo> InternalLoadCompositionTypedObjects(
    object usrSession,
    long projId,
    int relationTypeId,
    VersionsRule rule,
    params int[] childObjectTypes)
  {
    if (this.GetUserSession(usrSession) == null)
      throw new KernelExceptionID(sc_13895.ssp_appserver_13906(2085467413), (object) "CompositionLoadService.InternalLoadCompositionObjects");
    List<TypedObjectInfo> typedObjectInfoList = new List<TypedObjectInfo>();
    DataTable dataTable = this.InternalLoadComposition(usrSession, projId, relationTypeId, (IEnumerable<ColumnDescriptor>) new List<ColumnDescriptor>(2)
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0)
    }, string.Empty, rule, (IEnumerable<ConditionStructure>) null, childObjectTypes);
    if (dataTable == null)
      return typedObjectInfoList;
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      long int64Value = DataSetProcessor.GetInt64Value(dataTable.Rows[index], 0, 0L);
      int int32Value = DataSetProcessor.GetInt32Value(dataTable.Rows[index], 1, -1);
      TypedObjectInfo typedObjectInfo = new TypedObjectInfo(int64Value, int32Value);
      if (typedObjectInfoList.IndexOf(typedObjectInfo) < 0 && int64Value != 0L && int32Value != -1)
        typedObjectInfoList.Add(typedObjectInfo);
    }
    return typedObjectInfoList;
  }

  public DataTable LoadComposition(
    object usrSession,
    long projId,
    int relationTypeId,
    IEnumerable<ColumnDescriptor> columns,
    string filtrationOwnerId,
    params int[] childObjectTypes)
  {
    return this.InternalLoadComposition(usrSession, projId, relationTypeId, columns, filtrationOwnerId, (VersionsRule) null, (IEnumerable<ConditionStructure>) null, childObjectTypes);
  }

  public DataTable LoadComposition(
    object usrSession,
    long projId,
    int relationTypeId,
    IEnumerable<ColumnDescriptor> columns,
    string filtrationOwnerId,
    HybridDictionary dbParamTags,
    params int[] childObjectTypes)
  {
    return this.InternalLoadComposition1(usrSession, projId, relationTypeId, columns, filtrationOwnerId, (VersionsRule) null, (IEnumerable<ConditionStructure>) null, dbParamTags, childObjectTypes);
  }

  public DataTable LoadComposition(
    object usrSession,
    long projId,
    int relationTypeId,
    IEnumerable<ColumnDescriptor> columns,
    string filtrationOwnerId,
    IEnumerable<ConditionStructure> conditions,
    params int[] childObjectTypes)
  {
    return this.InternalLoadComposition(usrSession, projId, relationTypeId, columns, filtrationOwnerId, (VersionsRule) null, conditions, childObjectTypes);
  }

  public DataTable LoadComposition(
    object usrSession,
    long projId,
    int relationTypeId,
    IEnumerable<ColumnDescriptor> columns,
    VersionsRule rule,
    params int[] childObjectTypes)
  {
    return this.InternalLoadComposition(usrSession, projId, relationTypeId, columns, string.Empty, rule, (IEnumerable<ConditionStructure>) null, childObjectTypes);
  }

  public DataTable LoadComposition(
    object usrSession,
    long projId,
    int relationTypeId,
    IEnumerable<ColumnDescriptor> columns,
    VersionsRule rule,
    IEnumerable<ConditionStructure> conditions,
    params int[] childObjectTypes)
  {
    return this.InternalLoadComposition(usrSession, projId, relationTypeId, columns, string.Empty, rule, conditions, childObjectTypes);
  }

  public DataTable LoadCompositions(
    object usrSession,
    long projId,
    IEnumerable<ColumnDescriptor> columns,
    string filtrationOwnerId,
    params int[] childObjectTypes)
  {
    return this.InternalLoadCompositions(usrSession, projId, columns, filtrationOwnerId, (VersionsRule) null, childObjectTypes);
  }

  public DataTable LoadCompositions(
    object usrSession,
    long projId,
    IEnumerable<ColumnDescriptor> columns,
    VersionsRule rule,
    params int[] childObjectTypes)
  {
    return this.InternalLoadCompositions(usrSession, projId, columns, string.Empty, rule, childObjectTypes);
  }

  public DataTable LoadCompositions(
    object usrSession,
    long projId,
    IEnumerable<int> visibleRelTypes,
    IEnumerable<ColumnDescriptor> columns,
    string filtrationOwnerId,
    params int[] childObjectTypes)
  {
    return this.InternalLoadCompositions(usrSession, projId, visibleRelTypes, columns, filtrationOwnerId, (VersionsRule) null, childObjectTypes);
  }

  public DataTable LoadCompositions(
    object usrSession,
    long projId,
    IEnumerable<int> visibleRelTypes,
    IEnumerable<ColumnDescriptor> columns,
    VersionsRule rule,
    params int[] childObjectTypes)
  {
    return this.InternalLoadCompositions(usrSession, projId, visibleRelTypes, columns, string.Empty, rule, childObjectTypes);
  }

  public DataTable LoadComplexCompositions(
    object usrSession,
    IEnumerable<long> projIDs,
    int relTypeId,
    IEnumerable<ColumnDescriptor> columns,
    string filtrationOwnerId,
    params int[] childObjectTypes)
  {
    return this.InternalLoadComplexCompositions(usrSession, projIDs, relTypeId, columns, filtrationOwnerId, (VersionsRule) null, childObjectTypes);
  }

  public DataTable LoadComplexCompositions(
    object usrSession,
    IEnumerable<long> projIDs,
    int relTypeId,
    IEnumerable<ColumnDescriptor> columns,
    VersionsRule rule,
    params int[] childObjectTypes)
  {
    return this.InternalLoadComplexCompositions(usrSession, projIDs, relTypeId, columns, string.Empty, rule, childObjectTypes);
  }

  public long FindCompositionParentObject(
    object usrSession,
    long partId,
    int relationTypeId,
    string filtrationOwnerId)
  {
    return this.InternalFindCompositionParentObject(usrSession, partId, relationTypeId, filtrationOwnerId);
  }

  public long FindCompositionParentObject(
    object usrSession,
    long partId,
    int relationTypeId,
    VersionsRule rule)
  {
    return this.InternalFindCompositionParentObject(usrSession, partId, relationTypeId, rule);
  }

  public List<long> LoadCompositionObjects(
    object usrSession,
    long projId,
    int relationTypeId,
    string filtrationOwnerId,
    params int[] childObjectTypes)
  {
    return this.InternalLoadCompositionObjects(usrSession, projId, relationTypeId, filtrationOwnerId, childObjectTypes);
  }

  public List<long> LoadCompositionObjects(
    object usrSession,
    long projId,
    int relationTypeId,
    VersionsRule rule,
    params int[] childObjectTypes)
  {
    return this.InternalLoadCompositionObjects(usrSession, projId, relationTypeId, rule, childObjectTypes);
  }

  public List<TypedObjectInfo> LoadCompositionTypedObjects(
    object usrSession,
    long projId,
    int relationTypeId,
    string filtrationOwnerId,
    params int[] childObjectTypes)
  {
    return this.InternalLoadCompositionTypedObjects(usrSession, projId, relationTypeId, filtrationOwnerId, childObjectTypes);
  }

  public List<TypedObjectInfo> LoadCompositionTypedObjects(
    object usrSession,
    long projId,
    int relationTypeId,
    VersionsRule rule,
    params int[] childObjectTypes)
  {
    return this.InternalLoadCompositionTypedObjects(usrSession, projId, relationTypeId, rule, childObjectTypes);
  }

  public DataTable LoadCompositionApplicability(
    object usrSession,
    long partId,
    int relationTypeId,
    IEnumerable<ColumnDescriptor> columns,
    string filtrationOwnerId,
    params int[] childObjectTypes)
  {
    return this.InternalLoadCompositionApplicability(usrSession, partId, relationTypeId, columns, filtrationOwnerId, childObjectTypes);
  }

  private DataTable InternalLoadCompositionApplicability(
    object usrSession,
    long partId,
    int relationTypeId,
    IEnumerable<ColumnDescriptor> columns,
    string filtrationOwnerId,
    params int[] childObjectTypes)
  {
    IUserSession userSession = this.GetUserSession(usrSession);
    if (userSession == null)
      throw new KernelExceptionID(sc_13895.ssp_appserver_13907(1447400820), (object) "CompositionLoadService.InternalLoadCompositions");
    if (partId == 0L || relationTypeId == -1)
      return (DataTable) null;
    CompositionLoadingParams loadingParams = new CompositionLoadingParams((IEnumerable<ObjInfoItem>) SomeTypedInfoHelper<ObjInfoItem>.GetItemInfoList((IEnumerable<long>) new long[1]
    {
      partId
    }), (IEnumerable<int>) childObjectTypes, (IEnumerable<int>) null, (IEnumerable<int>) new int[1]
    {
      relationTypeId
    }, columns, (IEnumerable<ConditionStructure>) null, false, false, 1, (VersionsRule) null, filtrationOwnerId);
    return this.LoadComplexCompositions((object) userSession, loadingParams);
  }

  public DataTable LoadComposition(
    object usrSession,
    long objectId,
    int objectType,
    IEnumerable<int> searchRelationTypes,
    IEnumerable<int> searchObjectTypes,
    IEnumerable<ColumnDescriptor> columns,
    bool composition,
    bool grouping,
    VersionsRule rule,
    IEnumerable<ConditionStructure> conditions,
    string filtrationOwnerId,
    HybridDictionary tags,
    int loadLevels)
  {
    return this.LoadComposition(usrSession, objectId, objectType, searchRelationTypes, searchObjectTypes, columns, composition, grouping, rule, conditions, filtrationOwnerId, tags, loadLevels, (IEnumerable<int>) null);
  }

  public DataTable LoadComposition(
    object userSession,
    long objectId,
    int objectType,
    IEnumerable<int> searchRelationTypes,
    IEnumerable<int> searchObjectTypes,
    IEnumerable<ColumnDescriptor> columns,
    bool composition,
    bool grouping,
    VersionsRule rule,
    IEnumerable<ConditionStructure> conditions,
    string filtrationOwnerId,
    HybridDictionary tags,
    int loadLevels,
    IEnumerable<int> expandObjectTypes)
  {
    if (this.GetUserSession(userSession) == null)
      throw new KernelExceptionID(sc_13895.ssp_appserver_13908(1346857969), (object) "CompositionLoadService.InternalLoadCompositions");
    return this.LoadComplexCompositions(userSession, new CompositionLoadingParams((IEnumerable<ObjInfoItem>) new ObjInfoItem[1]
    {
      new ObjInfoItem(objectId, objectType)
    }, searchObjectTypes, expandObjectTypes, searchRelationTypes, columns, conditions, composition, grouping, loadLevels, rule, filtrationOwnerId)
    {
      DbParams = (IDictionary<long, HybridDictionary>) new Dictionary<long, HybridDictionary>()
      {
        {
          objectId,
          this.PrepareTags(tags, objectId, objectType)
        }
      }
    });
  }

  private HybridDictionary PrepareTags(HybridDictionary tags, long objectId, int objectType)
  {
    if (tags == null)
      tags = new HybridDictionary();
    tags[(object) "{004511C2-5AA8-4831-B60A-7CD17C1A2D88}"] = (object) new Dictionary<long, int>()
    {
      {
        objectId,
        objectType
      }
    };
    return tags;
  }

  public DataTable LoadComplexCompositions(
    object usrSession,
    IEnumerable<ObjInfoItem> objects,
    IEnumerable<int> searchRelationTypes,
    IEnumerable<int> searchObjectTypes,
    IEnumerable<ColumnDescriptor> columns,
    bool composition,
    bool grouping,
    VersionsRule rule,
    IEnumerable<ConditionStructure> conditions,
    string filtrationOwnerId,
    Dictionary<long, HybridDictionary> tags,
    int loadLevels)
  {
    return this.LoadComplexCompositions(usrSession, objects, searchRelationTypes, searchObjectTypes, columns, composition, grouping, rule, conditions, filtrationOwnerId, tags, loadLevels, (IEnumerable<int>) null);
  }

  public DataTable LoadComplexCompositions(
    object usrSession,
    IEnumerable<ObjInfoItem> objects,
    IEnumerable<int> searchRelationTypes,
    IEnumerable<int> searchObjectTypes,
    IEnumerable<ColumnDescriptor> columns,
    bool composition,
    bool grouping,
    VersionsRule rule,
    IEnumerable<ConditionStructure> conditions,
    string filtrationOwnerId,
    Dictionary<long, HybridDictionary> tags,
    int loadLevels,
    IEnumerable<int> expandObjectTypes)
  {
    if (this.GetUserSession(usrSession) == null)
      throw new KernelExceptionID(sc_13895.ssp_appserver_13909(2003973456), (object) "CompositionLoadService.InternalLoadCompositions");
    return this.LoadComplexCompositions(usrSession, new CompositionLoadingParams(objects, searchObjectTypes, expandObjectTypes, searchRelationTypes, columns, conditions, composition, grouping, loadLevels, rule, filtrationOwnerId, (IDictionary<long, HybridDictionary>) tags));
  }

  public DataTable LoadComplexCompositions(
    object userSession,
    CompositionLoadingParams loadingParams)
  {
    IUserSession userSession1 = this.GetUserSession(userSession);
    DataTable dataTable = userSession1 != null ? new CompositionTaskBooster(userSession1, (ICompositionLoadService) this).Execute(loadingParams) : throw new KernelExceptionID(sc_13895.ssp_appserver_13910(1215762727), (object) "CompositionLoadService.InternalLoadCompositions");
    return loadingParams.DataFilter == null ? dataTable : loadingParams.DataFilter.Execute(userSession1, dataTable);
  }

  public List<int> GetPresentCompositionTypes(
    object usrSession,
    IEnumerable<long> ids,
    int relationTypeId,
    bool composition)
  {
    Dictionary<int, List<int>> relType2ObjTypeLevelCache;
    if (!this.GetPresentCompositionTypes(usrSession, (object) ids, (IEnumerable<int>) new int[1]
    {
      relationTypeId
    }, (composition ? 1 : 0) != 0, out relType2ObjTypeLevelCache) || relType2ObjTypeLevelCache == null)
      return (List<int>) null;
    if (relationTypeId != -1)
    {
      List<int> compositionTypes;
      relType2ObjTypeLevelCache.TryGetValue(relationTypeId, out compositionTypes);
      return compositionTypes;
    }
    List<int> list = new List<int>(relType2ObjTypeLevelCache.Values.Sum<List<int>>((System.Func<List<int>, int>) (item => item.Count)));
    foreach (KeyValuePair<int, List<int>> keyValuePair in relType2ObjTypeLevelCache)
      list.AddRange((IEnumerable<int>) keyValuePair.Value);
    GenericListHelper.MakeUnique<int>(list);
    return list;
  }

  internal bool GetPresentCompositionTypes(
    object userSession,
    object objectCond,
    IEnumerable<int> relationTypeIds,
    bool composition,
    out Dictionary<int, List<int>> relType2ObjTypeLevelCache)
  {
    relType2ObjTypeLevelCache = new Dictionary<int, List<int>>();
    if (!(this.GetUserSession(userSession) is UserSession userSession1))
      throw new KernelExceptionID(sc_13895.ssp_appserver_13911(1299484497), (object) "CompositionLoadService.InternalLoadComposition");
    if (objectCond == null)
      return false;
    if (!(relationTypeIds is int[] numArray1))
      numArray1 = relationTypeIds != null ? relationTypeIds.ToArray<int>() : (int[]) null;
    int[] numArray2 = numArray1;
    if (numArray2 == null || !((IEnumerable<int>) numArray2).Any<int>())
      return false;
    string str1 = composition ? "SELECT DISTINCT R.F_RELATION_TYPE, O.F_OBJECT_TYPE FROM IMS_OBJECTS O, IMS_RELATIONS R WHERE O.F_ID = R.F_PART_ID" : "SELECT DISTINCT R.F_RELATION_TYPE, O.F_OBJECT_TYPE FROM IMS_OBJECTS O, IMS_OBJECTS O1, IMS_RELATIONS R WHERE O.F_OBJECT_ID = R.F_PROJ_ID AND O1.F_ID = R.F_PART_ID";
    IDbManager dataManager = userSession1.DataManager;
    StringBuilder stringBuilder1 = (StringBuilder) null;
    List<IDbDataParameter> collection = new List<IDbDataParameter>(10);
    if (numArray2.Length == 1)
    {
      if (((IEnumerable<int>) numArray2).First<int>() != -1)
      {
        str1 += " AND R.F_RELATION_TYPE = :rt0";
        collection.Add(dataManager.Parameter(":rt0", (object) ((IEnumerable<int>) numArray2).First<int>()));
      }
    }
    else
    {
      List<int> intList = new List<int>((IEnumerable<int>) numArray2);
      for (int count = intList.Count; count < 10; ++count)
        intList.Add(-1);
      stringBuilder1 = new StringBuilder();
      stringBuilder1.Clear();
      for (int index = 0; index < intList.Count; ++index)
      {
        string parameterName = ":rt" + (object) index;
        collection.Add(dataManager.Parameter(parameterName, (object) intList[index]));
        if (index > 0)
          stringBuilder1.Append(',');
        stringBuilder1.Append(parameterName);
      }
      str1 += $" AND R.F_RELATION_TYPE IN ({stringBuilder1})";
    }
    string str2 = composition ? "R.F_PROJ_ID" : "O1.F_OBJECT_ID";
    DataTable toTable = (DataTable) null;
    bool flag = false;
    if (objectCond is INConditionValue inConditionValue && inConditionValue.SelectKey != 0L)
    {
      collection.Add(dataManager.Parameter("pid", (object) inConditionValue.SelectKey));
      toTable = dataManager.ExecuteDataTable(str1 + $" AND {str2} IN (SELECT F_VALUE FROM {inConditionValue.TmpTableName} WHERE F_KEY = :pid) ", collection.ToArray());
    }
    else
    {
      IList list = inConditionValue == null ? (IList) (objectCond as long[]) ?? (objectCond is IEnumerable<long> source ? (IList) source.ToList<long>() : (IList) new long[1]) : (IList) inConditionValue.Values;
      if (list.Count == 1)
      {
        collection.Add(dataManager.Parameter("pid", list[0]));
        toTable = dataManager.ExecuteDataTable(str1 + $" AND {str2} = :pid", collection.ToArray());
      }
      else
      {
        List<IDbDataParameter> dbDataParameterList = new List<IDbDataParameter>();
        string format = str1 + $" AND {str2} IN ({"{0}"})";
        int num = 0;
        StringBuilder stringBuilder2 = stringBuilder1 ?? new StringBuilder();
        stringBuilder2.Clear();
        for (int index = 0; index < list.Count; ++index)
        {
          if (num >= dataManager.DataProvider.MaximumINOperands)
          {
            dbDataParameterList.AddRange((IEnumerable<IDbDataParameter>) collection);
            DataTable fromTable = dataManager.ExecuteDataTable(string.Format(format, (object) stringBuilder2), dbDataParameterList.ToArray());
            if (toTable == null)
              toTable = fromTable;
            else
              DataSetProcessor.AddTable(toTable, fromTable, false);
            num = 0;
            stringBuilder2.Clear();
            dbDataParameterList.Clear();
            flag = true;
          }
          if (num > 0)
            stringBuilder2.Append(',');
          string parameterName = ":p" + (object) num;
          dbDataParameterList.Add(dataManager.Parameter(parameterName, list[index]));
          stringBuilder2.Append(parameterName);
          ++num;
        }
        if (stringBuilder2.Length > 0)
        {
          for (int index = num; index < dataManager.DataProvider.MaximumINOperands; ++index)
          {
            if (index > 0)
              stringBuilder2.Append(',');
            string parameterName = ":p" + (object) index;
            dbDataParameterList.Add(dataManager.Parameter(parameterName, (object) -1L));
            stringBuilder2.Append(parameterName);
            if (index % 250 == 0)
              break;
          }
          dbDataParameterList.AddRange((IEnumerable<IDbDataParameter>) collection);
          DataTable fromTable = dataManager.ExecuteDataTable(string.Format(format, (object) stringBuilder2), dbDataParameterList.ToArray());
          if (toTable == null)
            toTable = fromTable;
          else
            DataSetProcessor.AddTable(toTable, fromTable, false);
        }
      }
    }
    if (toTable == null || toTable.Rows.Count <= 0)
      return false;
    for (int index = 0; index < toTable.Rows.Count; ++index)
    {
      int int32_1 = Convert.ToInt32(toTable.Rows[index][0]);
      int int32_2 = Convert.ToInt32(toTable.Rows[index][1]);
      List<int> intList;
      if (!relType2ObjTypeLevelCache.TryGetValue(int32_1, out intList))
      {
        intList = new List<int>() { int32_2 };
        relType2ObjTypeLevelCache.Add(int32_1, intList);
      }
      else if (flag)
      {
        if (intList.IndexOf(int32_2) < 0)
          intList.Add(int32_2);
      }
      else
        intList.Add(int32_2);
    }
    return true;
  }

  public List<ObjInfoItem> UpdateUnknownTypes(
    IEnumerable<ObjInfoItem> objInfoList,
    object usrSession)
  {
    return this.UpdateUnknownInfo(objInfoList, usrSession);
  }

  public List<ObjInfoItem> UpdateUnknownInfo(
    IEnumerable<ObjInfoItem> objInfoList,
    object usrSession)
  {
    if (!(this.GetUserSession(usrSession) is UserSession userSession))
      throw new KernelExceptionID(sc_13895.ssp_appserver_13912(42837842), (object) "CompositionLoadService.InternalLoadCompositions");
    List<ObjInfoItem> objInfoItemList = new List<ObjInfoItem>();
    List<ObjInfoItem> objInfoList1 = new List<ObjInfoItem>();
    bool flag = false;
    foreach (ObjInfoItem objInfo1 in objInfoList)
    {
      if (objInfo1.ObjectID != 0L && objInfo1.HasEmptyInfo)
      {
        objInfoItemList.Add(objInfo1);
        QuickObjectInfo objInfo2;
        if (((CacheDataset) userSession.DBCache)._ObjectsInfoCacheWrapper.TryGetValue(objInfo1.ObjectID, out objInfo2))
        {
          objInfo1.CopyFrom(objInfo2);
        }
        else
        {
          objInfoList1.Add(objInfo1);
          flag = flag || objInfo1 is IObjInfoID;
        }
      }
    }
    if (objInfoList1.Count == 0)
      return objInfoItemList;
    if (objInfoList1.Count <= 2)
    {
      foreach (ObjInfoItem objInfoItem in objInfoList1)
      {
        QuickObjectInfo objectInfo = userSession.GetObjectInfo(objInfoItem.ObjectID);
        objInfoItem.CopyFrom(objectInfo);
      }
      return objInfoItemList;
    }
    DataTable toTable = (DataTable) null;
    INConditionValue cValue = userSession.QueryBuilder.StartINCondition((object) -2, (Array) ObjInfoHelper.GetObjectIDs((IEnumerable<ObjInfoItem>) objInfoList1).ToArray(), false);
    int columnIndex = -1;
    try
    {
      List<string> values = new List<string>(3);
      if (flag)
      {
        columnIndex = 2 + values.Count;
        values.Add(", O.F_ID");
      }
      string str = $"SELECT O.F_OBJECT_ID, O.F_OBJECT_TYPE{string.Join(" ", (IEnumerable<string>) values)} FROM IMS_OBJECTS O WHERE O.F_OBJECT_ID ";
      IDbManager dataManager = userSession.DataManager;
      List<IDbDataParameter> collection = new List<IDbDataParameter>(10);
      if (cValue != null && cValue.SelectKey != 0L)
      {
        collection.Add(dataManager.Parameter("pid", (object) cValue.SelectKey));
        toTable = dataManager.ExecuteDataTable(str + $" IN (SELECT F_VALUE FROM {cValue.TmpTableName} WHERE F_KEY = :pid) ", collection.ToArray());
      }
      else
      {
        List<IDbDataParameter> dbDataParameterList = new List<IDbDataParameter>();
        string format = str + " IN ({0})";
        int num = 0;
        StringBuilder stringBuilder = new StringBuilder();
        for (int index = 0; index < objInfoList1.Count; ++index)
        {
          if (num >= dataManager.DataProvider.MaximumINOperands)
          {
            dbDataParameterList.AddRange((IEnumerable<IDbDataParameter>) collection);
            DataTable fromTable = dataManager.ExecuteDataTable(string.Format(format, (object) stringBuilder), dbDataParameterList.ToArray());
            if (toTable == null)
              toTable = fromTable;
            else
              DataSetProcessor.AddTable(toTable, fromTable, false);
            num = 0;
            stringBuilder.Clear();
            dbDataParameterList.Clear();
          }
          else
          {
            if (num > 0)
              stringBuilder.Append(',');
            string parameterName = ":p" + (object) num;
            dbDataParameterList.Add(dataManager.Parameter(parameterName, (object) objInfoList1[index].ObjectID));
            stringBuilder.Append(parameterName);
            ++num;
          }
        }
        if (stringBuilder.Length > 0)
        {
          if (num > 1)
          {
            for (int index = num; index < dataManager.DataProvider.MaximumINOperands; ++index)
            {
              if (index > 0)
                stringBuilder.Append(',');
              string parameterName = ":p" + (object) index;
              dbDataParameterList.Add(dataManager.Parameter(parameterName, (object) -1L));
              stringBuilder.Append(parameterName);
              if (index % 250 == 0)
                break;
            }
          }
          dbDataParameterList.AddRange((IEnumerable<IDbDataParameter>) collection);
          DataTable fromTable = dataManager.ExecuteDataTable(string.Format(format, (object) stringBuilder), dbDataParameterList.ToArray());
          if (toTable == null)
            toTable = fromTable;
          else
            DataSetProcessor.AddTable(toTable, fromTable, false);
        }
      }
    }
    finally
    {
      userSession.QueryBuilder.StopINCondition(cValue);
    }
    if (toTable == null || toTable.Rows.Count == 0)
      return (List<ObjInfoItem>) null;
    Dictionary<long, DataRow> dictionary = new Dictionary<long, DataRow>(toTable.Rows.Count);
    foreach (DataRow row in (InternalDataCollectionBase) toTable.Rows)
      dictionary.Add(Convert.ToInt64(row[0]), row);
    foreach (ObjInfoItem objInfoItem in objInfoList1)
    {
      DataRow dataRow;
      if (dictionary.TryGetValue(objInfoItem.ObjectID, out dataRow))
      {
        objInfoItem.ObjTypeID = Convert.ToInt32(dataRow[1]);
        if (objInfoItem is IObjInfoID)
          (objInfoItem as IObjInfoID).ID = Convert.ToInt64(dataRow[columnIndex]);
      }
    }
    return objInfoItemList;
  }
}
