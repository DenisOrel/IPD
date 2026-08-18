// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBRelationCollection
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Contexts;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.Server.DelayedNotifications;
using Intermech.Kernel.Relations;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Pools;
using Intermech.Search;
using Intermech.Search.Data;
using Intermech.Search.Data.Adapters;
using Intermech.Search.Utilities;
using Intermech.Text;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;


namespace Intermech.Kernel;

public class DBRelationCollection : 
  DBRecordSet,
  IDBRelationCollection,
  IDBRecords,
  IDBSessionable,
  IDBAttributableCollection,
  IServerDBRelationCollection,
  IDBJoinField
{
  private static readonly TimeSpan OneDay = new TimeSpan(0, 23, 59, 59, 999);
  private NewRelationProperties? _newRelationProperties;
  private int _addedColumnsCount;
  private VersionsRule _filtrationRule;
  private DBRelationCollectionFilter _filter;
  private bool _isManualSorting;
  internal bool _isManualSortingAllowed = true;

  public long _ProjectID { get; set; }

  public long _PartID { get; set; }

  public long _PartObjectID { get; set; }

  public int _ObjectTypeID { get; set; }

  public IList<int> _ChildObjectTypes { get; set; }

  internal bool _CheckCreateRules { get; set; }

  internal bool _NeedCheckCycleLinks { get; set; }

  public string JoinDefaultFieldName { get; set; }

  protected override AttributeSourceTypes AutoAttributeSourceTypes
  {
    [DebuggerStepThrough] get => AttributeSourceTypes.Relation;
  }

  public bool EnableFiltrateVersionsLog { get; set; }

  public SelectFunction FunctionID { get; set; }

  internal int _AssignMode { get; set; }

  public string FiltrationOwnerID { get; set; }

  public VersionsRule FiltrationRule { get; set; }

  public override string ObjectName
  {
    get
    {
      if (this._objectName != null)
        return this._objectName;
      return this._RecordsTypeID < 0 ? (this._objectName = LocalizationHolder.rm.GetString("Kernel_501")) : (this._objectName = string.Format(LocalizationHolder.rm.GetString("Kernel_502"), (object) this.UserSession.GetRelationType(this._RecordsTypeID).Description));
    }
  }

  public override bool LocalTypesMode
  {
    set
    {
      if (value)
        this.ObjectTypeID = -1;
      base.LocalTypesMode = value;
    }
  }

  public int ObjectTypeID
  {
    [DebuggerStepThrough] get => this._ObjectTypeID;
    set
    {
      if (this._ObjectTypeID == value)
        return;
      if (value > -1 && this.LocalTypesMode)
        throw new KernelException("Использование недопустимого условия фильтрации в режиме поиска среди объектов глобальных и локальных типов.");
      if (this._ObjectTypeID > 0 && this._ChildObjectTypes != null && this._ChildObjectTypes.Count == 1 && this._ChildObjectTypes[0] == this._ObjectTypeID)
        this._ChildObjectTypes = (IList<int>) null;
      this._ObjectTypeID = value;
    }
  }

  public IList<int> ChildObjectTypes
  {
    [DebuggerStepThrough] get => this._ChildObjectTypes;
    set
    {
      this._ChildObjectTypes = value;
      if (this._ChildObjectTypes == null || this._ChildObjectTypes.Count < 2)
        return;
      List<int> intList = new List<int>();
      for (int index = 0; index < this._ChildObjectTypes.Count; ++index)
      {
        if (intList.IndexOf(this._ChildObjectTypes[index]) < 0)
          intList.Add(this._ChildObjectTypes[index]);
      }
      this._ChildObjectTypes = (IList<int>) intList;
    }
  }

  public int RelationTypeID
  {
    [DebuggerStepThrough] get => this._RecordsTypeID;
    set
    {
      if (this._RecordsTypeID == value)
        return;
      this._RecordsTypeID = value;
      this._objectName = (string) null;
    }
  }

  public DBRelationCollection(UserSession userSession, int relationTypeID)
    : base(userSession, relationTypeID)
  {
    this._CheckCreateRules = true;
    this._DBAttributesTableName = "IMS_RELATION_ATTRS";
    this._DBKeyField = "F_PRJLINK_ID";
    this._DBKeyFieldID = Convert.ToInt32((object) ObligatoryObjectAttributes.F_PRJLINK_ID);
    this._DBObjectTableName = "IMS_RELATIONS";
    this._NeedCheckCycleLinks = true;
    this._ObjectTypeID = -1;
    this.FiltrationOwnerID = "cad001e2-306c-11d8-b4e9-00304f19f545";
    this.FunctionID = SelectFunction.Default;
    this.JoinDefaultFieldName = "F_PART_ID";
    this.InitSecurityOptions(5, 0L);
    this._filter = new DBRelationCollectionFilter(userSession);
  }

  public DBRelationCollection(
    UserSession userSession,
    int relationTypeID,
    string filtrationOwnerID)
    : this(userSession, relationTypeID)
  {
    this.FiltrationOwnerID = filtrationOwnerID;
  }

  protected override void ConfigureQueryBuilder(ConditionStructure[] conditions)
  {
    base.ConfigureQueryBuilder(conditions);
    if (this._ObjectTypeID > -1)
    {
      DBObjectType objectType = this.UserSession.GetObjectType(this._ObjectTypeID) as DBObjectType;
      if (objectType.IsLocalType)
        this.UserSession.QueryBuilder.ObjectAttributesTable = objectType.AttributesTableName;
      else
        this.UserSession.QueryBuilder.ObjectAttributesTable = "IMS_OBJECT_ATTRS";
    }
    else
      this.UserSession.QueryBuilder.ObjectAttributesTable = "IMS_OBJECT_ATTRS";
    string str1 = this.JoinDefaultFieldName;
    StringBuilder stringBuilder = new StringBuilder();
    if (this._PartID != -1L)
    {
      str1 = "F_PROJ_ID";
      stringBuilder.AppendFormat("{0}.F_PART_ID = :rc_partID AND ", (object) this.UserSession.QueryBuilder.RelationsTableAlias);
      this.UserSession.QueryBuilder.PreparedParams.Add(this.UserSession.DataManager.Parameter("rc_partID", (object) Math.Abs(this._PartID)));
    }
    if (this._ProjectID != -1L)
    {
      stringBuilder.AppendFormat("{0}.F_PROJ_ID = :rc_projID AND ", (object) this.UserSession.QueryBuilder.RelationsTableAlias);
      this.UserSession.QueryBuilder.PreparedParams.Add(this.UserSession.DataManager.Parameter("rc_projID", (object) this._ProjectID));
    }
    if (this._RecordsTypeID > -1 && this.UserSession.QueryBuilder.RelationsTypeID < 0)
      stringBuilder.AppendFormat("{0}.F_RELATION_TYPE = {1} AND ", (object) this.UserSession.QueryBuilder.RelationsTableAlias, (object) this._RecordsTypeID);
    if (this.LocalTypesMode)
      stringBuilder.AppendFormat("{0}.F_OBJECT_VER_TYPE <> -1 AND ", (object) this.UserSession.QueryBuilder.SystemTableAlias);
    stringBuilder.AppendFormat("({0}.F_CREATE_DATE <= :actualDate)", (object) this.UserSession.QueryBuilder.RelationsTableAlias);
    this.UserSession.QueryBuilder.PreparedParams.Add(this.UserSession.DataManager.Parameter("actualDate", (object) (this._ActualDate - this.UserSession.TimeZoneOffset)));
    if (this.UserSession.DataManager.DataProvider.Name == "Sql")
    {
      if (str1 == "F_PART_ID")
        stringBuilder.AppendFormat(" AND ({0}.F_ID = {1}.{2})", (object) this.UserSession.QueryBuilder.SystemTableAlias, (object) this.UserSession.QueryBuilder.RelationsTableAlias, (object) str1);
      else
        stringBuilder.AppendFormat(" AND ({0}.F_OBJECT_ID = {1}.{2})", (object) this.UserSession.QueryBuilder.SystemTableAlias, (object) this.UserSession.QueryBuilder.RelationsTableAlias, (object) str1);
    }
    if (this.ObjectTypeID == this.UserSession.IdentHelper.UsersTypeID && !this.UserSession.IsAdmin && !this._TrashMode)
      stringBuilder.AppendFormat(" AND ({0}.F_LEVEL_ID <> {1})", (object) this.UserSession.QueryBuilder.SystemTableAlias, (object) this.UserSession.IdentHelper.AnnulmentLevelID);
    this.UserSession.QueryBuilder.TypeFilter = stringBuilder.ToString();
    string objectsFilter = this.GetObjectsFilter(this.ObjectTypeID, string.Empty);
    if (objectsFilter != string.Empty)
      this.UserSession.QueryBuilder.TypeFilter = $"{this.UserSession.QueryBuilder.TypeFilter} AND {objectsFilter}";
    if (this._PartID != -1L || this._ProjectID != -1L || !(str1 == "F_PART_ID"))
      return;
    bool flag = true;
    if (conditions != null)
    {
      for (int index = 0; index < conditions.Length; ++index)
      {
        if (conditions[index].Attribute is int && Convert.ToInt32(conditions[index].Attribute) == -21 && conditions[index].RelationalOperator == RelationalOperators.In)
        {
          flag = false;
          break;
        }
      }
    }
    if (!flag)
      return;
    string str2 = this.GetObjectsFilter(this.ObjectTypeID, "PROJ_ID_TABLE");
    if (str2 != string.Empty)
      str2 = " AND " + str2;
    this.UserSession.QueryBuilder.TypeFilter += $" AND (EXISTS(SELECT PROJ_ID_TABLE.F_OBJECT_ID FROM IMS_OBJECTS PROJ_ID_TABLE WHERE (PROJ_ID_TABLE.F_OBJECT_ID = {this.UserSession.QueryBuilder.RelationsTableAlias}.F_PROJ_ID){str2}))";
  }

  protected override DBRecordSetParams PrepareAttributes(DBRecordSetParams paramSet)
  {
    paramSet = base.PrepareAttributes(paramSet);
    if (paramSet.Conditions != null)
    {
      for (int index = 0; index < paramSet.Conditions.Length; ++index)
      {
        switch (paramSet.Conditions[index].AttributeSource)
        {
          case AttributeSourceTypes.Auto:
            paramSet.Conditions[index].AttributeSource = !(paramSet.Conditions[index].Attribute is int) || (int) paramSet.Conditions[index].Attribute >= 0 ? AttributeSourceTypes.Relation : (ObligatoryObjectAttributesHelper.GetAttributeSourceType((ObligatoryObjectAttributes) paramSet.Conditions[index].Attribute) != AttributeSourceTypes.Relation ? AttributeSourceTypes.Object : AttributeSourceTypes.Relation);
            break;
          case AttributeSourceTypes.Events:
            throw new KernelExceptionID(sc_13556.ssp_appserver_13557(770280653));
        }
        if (this.LocalTypesMode && paramSet.Conditions[index].AttributeSource == AttributeSourceTypes.Object)
          this.ValidateLocalTypeAttribute(paramSet.Conditions[index].Attribute);
        if (paramSet.Conditions[index].RelationalOperator == RelationalOperators.ObjectTypeFilter)
        {
          this.ObjectTypeID = Convert.ToInt32(paramSet.Conditions[index].Value);
          paramSet.Conditions[index].RelationalOperator = RelationalOperators.NOP;
        }
      }
    }
    if (paramSet.ColumnsInfo != null)
    {
      for (int index = 0; index < paramSet.ColumnsInfo.Length; ++index)
      {
        if (!(paramSet.ColumnsInfo[index].AttributeID is int))
          paramSet.ColumnsInfo[index].AttributeID = (object) (this.EventHelper as EventLogHelper).GetAttributeID(paramSet.ColumnsInfo[index].AttributeID);
        switch (paramSet.ColumnsInfo[index].AttributeSource)
        {
          case AttributeSourceTypes.Auto:
            paramSet.ColumnsInfo[index].AttributeSource = AttributeSourceTypes.Relation;
            break;
          case AttributeSourceTypes.Events:
            throw new KernelExceptionID(sc_13556.ssp_appserver_13559(1461689681));
        }
      }
    }
    return paramSet;
  }

  protected override string GetFromSQL()
  {
    string fromSql;
    if (this.UserSession.DataManager.DataProvider.Name != "Sql")
    {
      string str = this.JoinDefaultFieldName;
      if (this._PartID != -1L)
        str = "F_PROJ_ID";
      fromSql = $" FROM {this.UserSession.QueryBuilder.RelationsTableName} {this.UserSession.QueryBuilder.RelationsTableAlias} JOIN {this.UserSession.QueryBuilder.SystemTableName} {this.UserSession.QueryBuilder.SystemTableAlias} ON {(!(str == "F_PART_ID") ? $" ({this.UserSession.QueryBuilder.SystemTableAlias}.F_OBJECT_ID = {this.UserSession.QueryBuilder.RelationsTableAlias}.{str})" : $" ({this.UserSession.QueryBuilder.SystemTableAlias}.F_ID = {this.UserSession.QueryBuilder.RelationsTableAlias}.{str})")} ";
    }
    else
      fromSql = $" FROM {this.UserSession.QueryBuilder.RelationsTableName} {this.UserSession.QueryBuilder.RelationsTableAlias}, {this.UserSession.QueryBuilder.SystemTableName} {this.UserSession.QueryBuilder.SystemTableAlias} ";
    return fromSql;
  }

  public override DataTable Select(DBRecordSetParams dbRecordSetParams)
  {
    long partID = -1;
    long projectVersionID = -1;
    DateTime actualDate = DateTime.UtcNow + this.UserSession.TimeZoneOffset;
    if (dbRecordSetParams.Conditions != null)
    {
      IEventLogHelper eventLogHelper = this.UserSession.EventLogHelper;
      for (int index = 0; index < dbRecordSetParams.Conditions.Length; ++index)
      {
        int attributeId = dbRecordSetParams.Conditions[index].Attribute == null ? 0 : eventLogHelper.GetAttributeID(dbRecordSetParams.Conditions[index].Attribute);
        if (attributeId < 0 && (dbRecordSetParams.Conditions[index].RelationalOperator == RelationalOperators.Equal || dbRecordSetParams.Conditions[index].RelationalOperator == RelationalOperators.In))
        {
          switch (attributeId)
          {
            case -43:
              actualDate = Convert.ToDateTime(dbRecordSetParams.Conditions[index].Value);
              dbRecordSetParams.Conditions[index].RelationalOperator = RelationalOperators.NOP;
              continue;
            case -22:
              if (dbRecordSetParams.Conditions[index].RelationalOperator == RelationalOperators.Equal)
              {
                partID = Convert.ToInt64(dbRecordSetParams.Conditions[index].Value);
                dbRecordSetParams.Conditions[index].RelationalOperator = RelationalOperators.NOP;
              }
              if (this.FunctionID == SelectFunction.Default)
              {
                this.FunctionID = SelectFunction.EntersIn;
                continue;
              }
              continue;
            case -21:
              if (dbRecordSetParams.Conditions[index].RelationalOperator == RelationalOperators.Equal)
              {
                projectVersionID = Convert.ToInt64(dbRecordSetParams.Conditions[index].Value);
                dbRecordSetParams.Conditions[index].RelationalOperator = RelationalOperators.NOP;
              }
              if (this.FunctionID == SelectFunction.Default)
              {
                this.FunctionID = SelectFunction.ConsistFrom;
                continue;
              }
              continue;
            default:
              continue;
          }
        }
      }
    }
    if (this.UserSession.Configurations.ReadBool("KERNEL", "PERFORMANCE", "UseHiddenComposition", true, DBConfigMode.GlobalOnly) && !this.UserSession.IsSystemSession && partID == -1L && projectVersionID == -1L && dbRecordSetParams.Conditions != null)
    {
      ConditionStructure inCondition = ((IEnumerable<ConditionStructure>) dbRecordSetParams.Conditions).FirstOrDefault<ConditionStructure>((System.Func<ConditionStructure, bool>) (o =>
      {
        if (o.RelationalOperator != RelationalOperators.In)
          return false;
        return object.Equals(o.Attribute, (object) ObligatoryObjectAttributes.F_PROJ_ID) || object.Equals(o.Attribute, (object) -21);
      }));
      if (inCondition.Value is long[] projectVersionIds)
      {
        dbRecordSetParams.Conditions = ((IEnumerable<ConditionStructure>) dbRecordSetParams.Conditions).Where<ConditionStructure>((System.Func<ConditionStructure, bool>) (condition => !condition.EqualsWithValues(inCondition))).ToArray<ConditionStructure>();
        if (dbRecordSetParams.Conditions.Length == 0)
          dbRecordSetParams.Conditions = (ConditionStructure[]) null;
        return this.FindComposition(projectVersionIds, dbRecordSetParams);
      }
      if (inCondition.Value is INConditionValue inConditionValue && inConditionValue.Values != null)
      {
        dbRecordSetParams.Conditions = ((IEnumerable<ConditionStructure>) dbRecordSetParams.Conditions).Where<ConditionStructure>((System.Func<ConditionStructure, bool>) (condition => !condition.EqualsWithValues(inCondition))).ToArray<ConditionStructure>();
        if (dbRecordSetParams.Conditions.Length == 0)
          dbRecordSetParams.Conditions = (ConditionStructure[]) null;
        return this.FindComposition(inConditionValue.Values.Cast<long>().ToArray<long>(), dbRecordSetParams);
      }
    }
    return this.Select(dbRecordSetParams, projectVersionID, partID, actualDate);
  }

  protected override object GetElement(long id) => (object) this.UserSession.GetRelation(id);

  protected override bool AddDeletedObjectsFilter => false;

  protected override IDBAttributeType[] GetColumnsCollection(
    ref DBRecordSetParams pars,
    bool failIfNotFound)
  {
    IDBAttributeType[] columnsCollection = this.RelationTypeID < 0 ? base.GetColumnsCollection(ref pars, failIfNotFound) : this.UserSession.GetRelationType(this.RelationTypeID).Attributes.GetAttributeTypeList(pars.Columns, failIfNotFound);
    if (this.LocalTypesMode)
    {
      for (int index = 0; index < columnsCollection.Length; ++index)
      {
        if (columnsCollection[index].AttributeType == FieldTypes.ftSystem && ObligatoryObjectAttributesHelper.GetAttributeSourceType((ObligatoryObjectAttributes) columnsCollection[index].AttributeID) == AttributeSourceTypes.Object)
          this.ValidateLocalTypeAttribute((object) columnsCollection[index].AttributeID);
      }
    }
    return columnsCollection;
  }

  public virtual DataTable Select(
    DBRecordSetParams dbRecordSetParams,
    long projectVersionID,
    long partID,
    DateTime actualDate)
  {
    return this.Select(dbRecordSetParams, projectVersionID, partID, actualDate, (IList<int>) null);
  }

  public virtual DataTable Select(
    DBRecordSetParams @params,
    long projectVersionID,
    long partID,
    DateTime actualDate,
    IList<int> childObjectTypeIds)
  {
    this.CheckParams(ref @params);
    this.Reset();
    this._ProjectID = projectVersionID;
    this._PartID = partID;
    this._ActualDate = actualDate;
    this.SetChildObjectTypeIds(childObjectTypeIds);
    CurrentEditingContext editingContextDataFromCallContext = this.PrepareParams(ref @params);
    List<DataRow> fromRows = new List<DataRow>();
    using (this.CreateCurrentEditingContextScope(editingContextDataFromCallContext))
    {
      DataTable dataTable1 = (DataTable) null;
      this.ConfigureFilter(ref @params);
      foreach (int childObjectType in (IEnumerable<int>) this._ChildObjectTypes)
      {
        int objectTypeId = this._ObjectTypeID;
        try
        {
          List<DataRow> source = new List<DataRow>();
          this._ObjectTypeID = childObjectType;
          this.ConfigureOptimizer(childObjectType);
          DataTable dataTable = base.Select(@params);
          IEnumerable<RelationObjectBase> relationObjects = this.CreateRelationObjects(dataTable, @params);
          if (dataTable1 == null)
          {
            dataTable1 = dataTable.Clone();
            dataTable1.BeginLoadData();
          }
          if ((this._PartObjectID == -1L || ObjectHelper.IsUnknownObjectVersionID(this._PartObjectID)) && (this._ProjectID == -1L || ObjectHelper.IsUnknownObjectVersionID(this._ProjectID)) && @params.Tags != null && @params.Tags.Contains((object) "{2C7E989F-0EAF-40CC-80FD-16EF1D9090B3}"))
          {
            if (!(@params.Tags[(object) "{2C7E989F-0EAF-40CC-80FD-16EF1D9090B3}"] is Dictionary<long, long> tag))
              throw new Exception("Значение Tags по ключу Intermech.Consts.FindApplicabilitiesForPartVersionsListParamsKey должно быть Dictionary<long, long>");
            if (@params.Tags == null)
              dictionary = new Dictionary<long, int>();
            else if (!(@params.Tags[(object) "{004511C2-5AA8-4831-B60A-7CD17C1A2D88}"] is Dictionary<long, int> dictionary))
              dictionary = new Dictionary<long, int>();
            Dictionary<long, int> optimizationDictionary = dictionary;
            this.CompleteOptimizationDictionary(optimizationDictionary, tag.Values.Distinct<long>().ToList<long>());
            foreach (KeyValuePair<long, List<RelationObjectBase>> keyValuePair in this.CreateDictionaryByPartID(relationObjects))
            {
              long partId = this._PartID;
              try
              {
                this._PartID = keyValuePair.Key;
                if (!tag.ContainsKey(keyValuePair.Key))
                  throw new Exception($"В дополнительных параметрах запроса Intermech.Consts.FindApplicabilitiesForPartVersionsListParamsKey не найдена версия объекта для c id = {keyValuePair.Key}");
                long partVersionID = tag[keyValuePair.Key];
                long partObjectId = this._PartObjectID;
                try
                {
                  this._PartObjectID = partVersionID;
                  this.SetPartVersionIDToFilter(partVersionID, optimizationDictionary);
                  IEnumerable<DataRow> collection = this.ApplyFilter((IEnumerable<RelationObjectBase>) keyValuePair.Value);
                  source.AddRange(collection);
                }
                finally
                {
                  this._PartObjectID = partObjectId;
                }
              }
              finally
              {
                this._PartID = partId;
              }
            }
          }
          else if ((this._ProjectID == -1L || ObjectHelper.IsUnknownObjectVersionID(this._ProjectID)) && this.HasInConditionForProjectVersionID(@params))
          {
            Dictionary<long, List<RelationObjectBase>> projectVersionId = this.CreateDictionaryByProjectVersionID(relationObjects);
            if (@params.Tags == null)
              dictionary = new Dictionary<long, int>();
            else if (!(@params.Tags[(object) "{004511C2-5AA8-4831-B60A-7CD17C1A2D88}"] is Dictionary<long, int> dictionary))
              dictionary = new Dictionary<long, int>();
            Dictionary<long, int> optimizationDictionary = dictionary;
            this.CompleteOptimizationDictionary(optimizationDictionary, projectVersionId.Keys.ToList<long>());
            foreach (KeyValuePair<long, List<RelationObjectBase>> keyValuePair in projectVersionId)
            {
              long projectId = this._ProjectID;
              try
              {
                this._ProjectID = keyValuePair.Key;
                this.SetProjectVersionIDToFilter(keyValuePair.Key, optimizationDictionary);
                IEnumerable<DataRow> collection = this.ApplyFilter((IEnumerable<RelationObjectBase>) keyValuePair.Value);
                source.AddRange(collection);
              }
              finally
              {
                this._ProjectID = projectId;
              }
            }
          }
          else if ((this._PartID == -1L || ObjectHelper.IsUnknownObjectID(this._PartID)) && this.HasInConditionForPartID(@params))
          {
            foreach (KeyValuePair<long, List<RelationObjectBase>> keyValuePair in this.CreateDictionaryByPartID(relationObjects))
            {
              long partId = this._PartID;
              try
              {
                this._PartID = keyValuePair.Key;
                IEnumerable<DataRow> collection = this.ApplyFilter((IEnumerable<RelationObjectBase>) keyValuePair.Value);
                source.AddRange(collection);
              }
              finally
              {
                this._PartID = partId;
              }
            }
          }
          else
          {
            IEnumerable<DataRow> collection = this.ApplyFilter(relationObjects);
            source.AddRange(collection);
          }
          fromRows.AddRange((IEnumerable<DataRow>) source.OrderBy<DataRow, int>((System.Func<DataRow, int>) (o => dataTable.Rows.IndexOf(o))));
        }
        finally
        {
          this._ObjectTypeID = objectTypeId;
        }
      }
      if (fromRows.Count < 5)
      {
        foreach (DataRow fromRow in fromRows)
          DataSetProcessor.AddRow(dataTable1, fromRow, false);
      }
      else
        DataSetProcessor.AssignRows(dataTable1, (IEnumerable<DataRow>) fromRows, false, true);
      dataTable1.EndLoadData();
      this.ApplyManualSorting(dataTable1, ref @params);
      this.PrepareResultDataTable(dataTable1, ref @params);
      this.Reset();
      this.FunctionID = SelectFunction.Default;
      return dataTable1;
    }
  }

  public DataTable FindComposition(long[] projectVersionIds, DBRecordSetParams recordSetParams)
  {
    if (projectVersionIds == null || projectVersionIds.Length == 0 || ObjectHelper.IsAnyUnknownObjectVersionID((IEnumerable<long>) projectVersionIds))
      throw new ArgumentException();
    DataTable composition = (DataTable) null;
    foreach (long projectID in ((IEnumerable<long>) projectVersionIds).Distinct<long>())
    {
      DataTable dataTable = this.ConsistFrom(recordSetParams, projectID);
      if (composition == null)
      {
        composition = dataTable;
      }
      else
      {
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          composition.Rows.Add(row.ItemArray);
      }
    }
    return composition;
  }

  public DataTable EntersIn(DBRecordSetParams dbRecordSetParams, long partID)
  {
    this.FunctionID = SelectFunction.EntersIn;
    return this.Select(dbRecordSetParams, -1L, partID, DateTime.UtcNow + this.UserSession.TimeZoneOffset);
  }

  public DataTable EntersIn(DBRecordSetParams dbRecordSetParams, long partID, bool recursive)
  {
    this.FunctionID = SelectFunction.EntersIn;
    return recursive ? this.SelectRecursive(dbRecordSetParams, -1L, partID, DateTime.UtcNow + this.UserSession.TimeZoneOffset) : this.Select(dbRecordSetParams, -1L, partID, DateTime.UtcNow + this.UserSession.TimeZoneOffset);
  }

  public DataTable EntersIn(
    DBRecordSetParams paramSet,
    long partID,
    bool recursive,
    DateTime actualDate)
  {
    this.FunctionID = SelectFunction.EntersIn;
    return recursive ? this.SelectRecursive(paramSet, -1L, partID, actualDate) : this.Select(paramSet, -1L, partID, actualDate);
  }

  public DataTable EntersInVersion(DBRecordSetParams paramSet, long objectID)
  {
    return this.EntersInVersion(paramSet, objectID, 0L);
  }

  public DataTable EntersInVersion(DBRecordSetParams paramSet, long objectID, long id)
  {
    this.FunctionID = SelectFunction.EntersInVersion;
    this._PartObjectID = objectID;
    long partID = id != 0L ? id : SqlHelper.GetIDByObjectID(objectID, this.UserSession.DataManager);
    this._EntersInUseAttrCompositionVersionID = false;
    if (this.RelationTypeID != -1)
    {
      IDBRelationType relationType = this.UserSession.GetRelationType(this.RelationTypeID);
      IDBAttribute4TypeCollection attribute4TypeCollection = (IDBAttribute4TypeCollection) null;
      IDBAttributeType4 dbAttributeType4 = (IDBAttributeType4) null;
      if (relationType != null)
        attribute4TypeCollection = relationType.Attributes;
      if (attribute4TypeCollection != null)
        dbAttributeType4 = attribute4TypeCollection.GetAttributeByID(Intermech.Search.Data.Filters.Constants.VersionIDInCompositionAttributeTypeID);
      this._EntersInUseAttrCompositionVersionID = dbAttributeType4 != null;
    }
    return this.Select(paramSet, -1L, partID, DateTime.UtcNow + this.UserSession.TimeZoneOffset);
  }

  public DataTable EntersInVersion(
    DBRecordSetParams dbRecordSetParams,
    long objectID,
    bool recursive,
    DateTime actualDate)
  {
    this.FunctionID = SelectFunction.EntersInVersion;
    this._PartObjectID = objectID;
    long idByObjectId = SqlHelper.GetIDByObjectID(objectID, this.UserSession.DataManager);
    this._EntersInUseAttrCompositionVersionID = false;
    if (this.RelationTypeID != -1)
    {
      IDBRelationType relationType = this.UserSession.GetRelationType(this.RelationTypeID);
      IDBAttribute4TypeCollection attribute4TypeCollection = (IDBAttribute4TypeCollection) null;
      IDBAttributeType4 dbAttributeType4 = (IDBAttributeType4) null;
      if (relationType != null)
        attribute4TypeCollection = relationType.Attributes;
      if (attribute4TypeCollection != null)
        dbAttributeType4 = attribute4TypeCollection.GetAttributeByID(Intermech.Search.Data.Filters.Constants.VersionIDInCompositionAttributeTypeID);
      this._EntersInUseAttrCompositionVersionID = dbAttributeType4 != null;
    }
    return recursive ? this.SelectRecursive(dbRecordSetParams, -1L, idByObjectId, actualDate) : this.Select(dbRecordSetParams, -1L, idByObjectId, actualDate);
  }

  public DataTable ConsistFrom(DBRecordSetParams paramSet, long projectID)
  {
    if (projectID == 0L || projectID == -1L)
      throw new KernelException(sc_13556.ssp_appserver_13560() + projectID.ToString());
    this.FunctionID = SelectFunction.ConsistFrom;
    return this.Select(paramSet, projectID, -1L, DateTime.UtcNow + this.UserSession.TimeZoneOffset);
  }

  public DataTable ConsistFrom(DBRecordSetParams paramSet, long projectID, bool recursive)
  {
    this.FunctionID = SelectFunction.ConsistFrom;
    return recursive ? this.SelectRecursive(paramSet, projectID, -1L, DateTime.UtcNow + this.UserSession.TimeZoneOffset) : this.Select(paramSet, projectID, -1L, DateTime.UtcNow + this.UserSession.TimeZoneOffset);
  }

  public DataTable ConsistFrom(
    DBRecordSetParams paramSet,
    long projectID,
    bool recursive,
    DateTime actualDate)
  {
    this.FunctionID = SelectFunction.ConsistFrom;
    return recursive ? this.SelectRecursive(paramSet, projectID, -1L, actualDate) : this.Select(paramSet, projectID, -1L, actualDate);
  }

  public DataTable ConsistFrom(
    DBRecordSetParams paramSet,
    long projectID,
    out bool invisibleExists)
  {
    if (ServerConsts.MandateAccess)
    {
      DBRelationCollection relationCollection = this.UserSession.GetRelationCollection(this.RelationTypeID, this.FiltrationOwnerID) as DBRelationCollection;
      if (this._ChildObjectTypes != null)
        relationCollection.ChildObjectTypes = this.ChildObjectTypes;
      else
        relationCollection.ObjectTypeID = this.ObjectTypeID;
      DataTable dataTable1 = this.ConsistFrom(paramSet, projectID);
      relationCollection._RevertAccessFiltration = true;
      DataTable dataTable2 = relationCollection.ConsistFrom(paramSet, projectID);
      invisibleExists = dataTable2.Rows.Count > 0;
      return dataTable1;
    }
    invisibleExists = false;
    return this.ConsistFrom(paramSet, projectID);
  }

  public bool IsObjectInFolders(long id, long[] parentsObjectID)
  {
    IDbManager dataManager = this.UserSession.DataManager;
    DataTable dataTable = dataManager.ExecuteDataTable("SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_ID = :id1", dataManager.Parameter("id1", (object) id));
    for (int index1 = 0; index1 < dataTable.Rows.Count; ++index1)
    {
      for (int index2 = 0; index2 < parentsObjectID.Length; ++index2)
      {
        if (Convert.ToInt64(dataTable.Rows[index1][0]) == parentsObjectID[index2])
          return true;
      }
    }
    bool flag = false;
    IDbDataParameter dbDataParameter1 = dataManager.Parameter("inPART_ID", (object) id);
    IDbDataParameter dbDataParameter2 = dataManager.Parameter("inRELATION_TYPE", (object) this.RelationTypeID);
    IDbDataParameter dbDataParameter3 = dataManager.Parameter("inFOR_DATE", (object) DateTime.UtcNow);
    IDbDataParameter dbDataParameter4 = dataManager.Parameter("inFindProjID", (object) 0L);
    IDbDataParameter dbDataParameter5 = dataManager.OutputParameter("outResult", (object) 0);
    for (int index = 0; index < parentsObjectID.Length; ++index)
    {
      dbDataParameter4.Value = (object) parentsObjectID[index];
      dataManager.ExecuteSpNonQuery("IMS_CHECK_IN_TREE_UP", dbDataParameter1, dbDataParameter2, dbDataParameter3, dbDataParameter4, dbDataParameter5);
      if (dbDataParameter5.Value != DBNull.Value && Convert.ToInt32(dbDataParameter5.Value) == 1)
      {
        flag = true;
        break;
      }
    }
    return flag;
  }

  internal void CheckCycleLinks(
    long projID,
    long partID,
    DateTime actualDate,
    long projObjectID,
    int partObjectTypeID)
  {
    if ((this.UserSession.GetRelationType(this._RecordsTypeID, true).Options & RelationTypeOptions.EnableCycleRelations) != RelationTypeOptions.None)
      return;
    if (Math.Abs(projID) == Math.Abs(partID))
      throw new KernelExceptionID(sc_13556.ssp_appserver_13561(478061654));
    if (!DBRelationCollection.Settings.CheckCycles)
      return;
    List<IMSObjectType> childObjectTypes = MetaDataHelper.GetApplicabilityChildObjectTypes(partObjectTypeID, this._RecordsTypeID);
    if (childObjectTypes == null || childObjectTypes.Count <= 0)
      return;
    IDbManager dataManager = this.UserSession.DataManager;
    int num = 0;
    dataManager.ExecuteSpNonQuery("IMS_CHECK_IN_TREE_DOWN", dataManager.Parameter("inPART_ID", (object) projID), dataManager.Parameter("inRELATION_TYPE", (object) this._RecordsTypeID), dataManager.Parameter("inFOR_DATE", (object) actualDate), dataManager.Parameter("inFindID", (object) partID), dataManager.OutputParameter("outResult", (object) num));
    if (Convert.ToInt32(dataManager.GetOutputParameterValue("outResult")) != 0)
      throw new KernelExceptionID(sc_13556.ssp_appserver_13562(292755305), (object) this.UserSession.GetObject(projObjectID).Caption, (object) this.UserSession.GetObjectByVersionsRule(partID, this.FiltrationOwnerID, true).Caption);
  }

  public virtual IDBRelation Create(
    DateTime beginDate,
    long projectID,
    long partID,
    long prjlinkID,
    long partObjectID,
    IDBRelation prototype,
    Guid relationGUID,
    AttributeValues[] vals = null)
  {
    if (this._RecordsTypeID < 0)
      throw new KernelExceptionID(sc_13556.ssp_appserver_13563(1682664464));
    beginDate -= this.UserSession.TimeZoneOffset;
    beginDate = beginDate.Date;
    DBRelationType relationType = this.UserSession.GetRelationType(this._RecordsTypeID) as DBRelationType;
    IDBObject dbObject1 = this.UserSession.GetObject(projectID);
    IDBObject partObject = (IDBObject) null;
    if ((relationType.Options & RelationTypeOptions.EnableCheckAnnulment) == RelationTypeOptions.EnableCheckAnnulment)
    {
      int levelId = (dbObject1 as IDBLifecycleLevel).LevelID;
      if (levelId != this.UserSession.IdentHelper.AnnulmentLevelID && levelId != this.UserSession.IdentHelper.KeepingLevelID)
      {
        DataTable allObjectVersions = this.UserSession.GetAllObjectVersions(partID, true, true, false, new string[2]
        {
          "F_OBJECT_ID",
          "F_LEVEL_ID"
        });
        bool flag = true;
        foreach (DataRow row in (InternalDataCollectionBase) allObjectVersions.Rows)
        {
          int int32 = Convert.ToInt32(row[1]);
          if (int32 != this.UserSession.IdentHelper.AnnulmentLevelID && int32 != this.UserSession.IdentHelper.KeepingLevelID)
          {
            flag = false;
            break;
          }
        }
        if (flag)
          throw new KernelExceptionID(459, (object) this.UserSession.GetObjectByID(partID, true).NameInMessages, (object) dbObject1.NameInMessages, (object) (dbObject1 as IDBLifecycleLevel).LevelName);
      }
    }
    int objectType = dbObject1.ObjectType;
    int num1 = !MetaDataHelper.HasObjectTypeGroupingRelTypes(objectType) ? 0 : (MetaDataHelper.HasRelationTypeGrouping(relationType.RelationType) ? 1 : 0);
    string note = partID.ToString();
    long EventID = prototype != null && prototype.ProjID == -projectID || this._AssignMode == 1024 /*0x0400*/ ? 0L : this.AddEvent(projectID, ActionType.AddLink, EventlogRecordType.AccessDenied, note);
    if (ServerConsts.CreateRelationLogging)
    {
      partObject = partObjectID == 0L || partObjectID == -1L ? this.UserSession.GetObjectByID(partID, false) : this.UserSession.GetObject(partObjectID, false);
      this.UserSession.EventLogHelper.AddToTrace($"Включение объекта '{(partObject != null ? (object) partObject.NameInMessages : (object) ("ID=" + partID.ToString()))}' в состав объекта '{dbObject1.NameInMessages}' связью типа '{relationType.Description}'.", "CreateObject.log");
      if (prototype != null)
        this.UserSession.EventLogHelper.AddToTrace($"По прототипу связи {(prototype as DBSessionable).ObjectName}  RelationGUID = {(object) prototype.GUID}", "CreateObject.log");
      if (!relationGUID.Equals(Guid.Empty))
        this.UserSession.EventLogHelper.AddToTrace("Глобальный идентификатор связи " + relationGUID.ToString(), "CreateObject.log");
      this.UserSession.EventLogHelper.AddToTrace("-----------------------------------------------", "CreateObject.log");
      this.UserSession.EventLogHelper.AddToTrace(Environment.StackTrace, "CreateObject.log");
      this.UserSession.EventLogHelper.AddToTrace("-----------------------------------------------", "CreateObject.log");
    }
    if (this._CheckCreateRules)
      relationType.CheckAccess(ActionType.AddLink);
    IDbManager dataManager = this.UserSession.DataManager;
    DataTable dataTable = dataManager.ExecuteDataTable("SELECT F_OBJECT_ID, F_OBJECT_TYPE, F_PROJECT_ID, F_LEVEL_ID, F_VERSION_ID, F_ACCESS FROM IMS_OBJECTS WHERE F_ID = :id", dataManager.Parameter("id", (object) partID));
    (this.EventHelper as EventLogHelper).OnBeforeCreateRelation(dbObject1, partID, beginDate, prjlinkID, (IUserSession) this.UserSession, (IDBRelationCollection) this, dataTable);
    (dbObject1 as DBObject).DoBeforeCreateRelation(this, partID, partObjectID, prjlinkID, prototype);
    this.UserSession.StartTransaction();
    try
    {
      long num2 = 0;
      bool flag1 = false;
      object obj1 = (object) null;
      bool flag2 = false;
      bool flag3 = false;
      bool flag4 = false;
      if (this._CheckCreateRules)
      {
        flag2 = this.UserSession.EnabledAutoSoftInstantiation;
        DataRow[] dataRowArray = dataTable.Select(string.Empty, "F_OBJECT_TYPE");
        int num3 = -1;
        long accessLevel = (long) dbObject1.AccessLevel;
        bool flag5 = false;
        if (this._NeedCheckCycleLinks)
          this.CheckCycleLinks(dbObject1.ID, partID, beginDate, dbObject1.ObjectID, Convert.ToInt32(dataRowArray[0]["F_OBJECT_TYPE"]));
        for (int index = 0; index < dataRowArray.Length; ++index)
        {
          if (Convert.ToInt32(dataRowArray[index][3]) != this.UserSession.IdentHelper.DeletedID)
          {
            flag5 = true;
            if (num3 != Convert.ToInt32(dataRowArray[index][1]))
            {
              num3 = Convert.ToInt32(dataRowArray[index][1]);
              IDBRelationsApplicability applicability = this.UserSession.GetRelationsApplicabilityCollection().GetApplicability(this._RecordsTypeID, num3, objectType);
              if (applicability == null)
              {
                if (dataRowArray.Length > 1)
                {
                  string str = Convert.ToInt64(dataRowArray[index][0]) <= 0L ? LocalizationHolder.rm.GetString("WorkCopy") : LocalizationHolder.rm.GetString("ArcCopy");
                  throw new KernelExceptionID(388, (object) string.Format(LocalizationHolder.rm.GetString(sc_13556.ssp_appserver_13564()), (object) str, dataRowArray[index][4], (object) this.UserSession.GetObjectType(num3).ObjectInstanceName, (object) this.UserSession.GetObjectType(objectType).ObjectInstanceName)).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(Convert.ToInt64(dataRowArray[index][4])));
                }
                throw new KernelExceptionID(sc_13556.ssp_appserver_13565(1736228602), (object) this.UserSession.GetObjectType(objectType).ObjectInstanceName, (object) this.UserSession.GetObjectType(num3).ObjectInstanceName);
              }
              if (applicability.ApplicabilityMode == ApplicabilityModes.Disabled)
              {
                if (dataRowArray.Length > 1)
                {
                  string str = Convert.ToInt64(dataRowArray[index][0]) <= 0L ? LocalizationHolder.rm.GetString("WorkCopy") : LocalizationHolder.rm.GetString("ArcCopy");
                  throw new KernelExceptionID(389, (object) string.Format(LocalizationHolder.rm.GetString(sc_13556.ssp_appserver_13566()), (object) str, dataRowArray[index][4], (object) this.UserSession.GetObjectType(num3).ObjectInstanceName, (object) this.UserSession.GetObjectType(objectType).ObjectInstanceName));
                }
                throw new KernelExceptionID(sc_13556.ssp_appserver_13567(1864509249), (object) this.UserSession.GetObjectType(objectType).ObjectInstanceName, (object) this.UserSession.GetObjectType(num3).ObjectInstanceName);
              }
              if (applicability.IsContent)
                flag1 = true;
              if (flag2)
                flag2 = (applicability.Options & ApplicabilityOptions.SoftInstantiation) == ApplicabilityOptions.SoftInstantiation;
              flag3 = (applicability.Options & ApplicabilityOptions.AutoInstantiation) == ApplicabilityOptions.AutoInstantiation;
              if ((applicability.Options & ApplicabilityOptions.EnableMultiLink) == ApplicabilityOptions.None)
              {
                object obj2 = dataManager.ExecuteScalar("SELECT F_PRJLINK_ID FROM IMS_RELATIONS WHERE F_PROJ_ID = :projID AND F_PART_ID = :partID AND F_RELATION_TYPE = :rtID AND (F_CREATE_DATE <= :cDate)", dataManager.Parameter("projID", (object) projectID), dataManager.Parameter(nameof (partID), (object) partID), dataManager.Parameter("rtID", (object) this._RecordsTypeID), dataManager.Parameter("cDate", (object) beginDate));
                if (obj2 != null && obj2 != DBNull.Value)
                {
                  long fetchedVersionId;
                  string objectCaptionByList = this.GetObjectCaptionByList(dataTable, out fetchedVersionId);
                  throw new KernelExceptionID(sc_13556.ssp_appserver_13568(980431205), (object) objectCaptionByList, (object) fetchedVersionId, (object) this.UserSession.GetObject(projectID).Caption, (object) projectID, (object) relationType.Description).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(fetchedVersionId), (ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(projectID));
                }
              }
              if (applicability.MaximumLinks < int.MaxValue)
              {
                IDBObjectCollection objectCollection = this.UserSession.GetObjectCollection(objectType);
                ConditionStructure conditionStructure1 = new ConditionStructure(0, RelationalOperators.ConsistFrom, (object) partID, LogicalOperators.AND, 0, true);
                ConditionStructure conditionStructure2 = new ConditionStructure(-3, RelationalOperators.NotEqual, (object) dbObject1.ID, LogicalOperators.NONE, 0, false);
                conditionStructure1.TypeID = (object) this._RecordsTypeID;
                DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[2]
                {
                  conditionStructure1,
                  conditionStructure2
                }, new object[1]{ (object) -2 });
                if (objectCollection.Select(paramSet).Rows.Count >= applicability.MaximumLinks)
                  throw new KernelExceptionID(sc_13556.ssp_appserver_13569(1026379808), (object) this.UserSession.GetObjectType(objectType).ObjectTypeName);
              }
              if (partObjectID < 0L && !flag4 && (applicability.Options & ApplicabilityOptions.CopyAttributes2Child) == ApplicabilityOptions.CopyAttributes2Child)
              {
                if (partObject == null)
                  partObject = this.UserSession.GetObject(partObjectID, false);
                if (partObject != null && partObject.IsCreationMode)
                  flag4 = true;
              }
            }
            if (relationType.MustCheckAccessLevel)
            {
              int int32 = Convert.ToInt32(dataRowArray[index][5]);
              if ((long) int32 > accessLevel)
              {
                IDBObject dbObject2 = this.UserSession.GetObject(Convert.ToInt64(dataRowArray[index][0]));
                string str = string.Format(LocalizationHolder.rm.GetString(sc_13556.ssp_appserver_13570()), (object) dbObject2.NameInMessages, (object) dbObject2.VersionID, (object) this.UserSession.DBCache.GetAccessCaption(int32), (object) dbObject1.NameInMessages, (object) dbObject1.VersionID, (object) this.UserSession.DBCache.GetAccessCaption(dbObject1.AccessLevel));
                throw new KernelExceptionID(sc_13556.ssp_appserver_13571(1225541529), (object) str);
              }
            }
            if (ServerConsts.SetProjectOnCreateRelation && dbObject1.ProjectID != 0L && (this.UserSession.GetObjectType(num3).Options & ObjectTypeOptions.CurrentProjectEnabled) == ObjectTypeOptions.CurrentProjectEnabled)
            {
              partObject = this.UserSession.GetObject(Convert.ToInt64(dataRowArray[index][0]));
              if ((partObjectID == 0L ? 1 : (partObject.ObjectID == partObjectID ? 1 : 0)) != 0 && partObject.ProjectID == 0L)
                partObject.ProjectID = dbObject1.ProjectID;
            }
          }
        }
        if (!flag5)
          throw new KernelExceptionID(328);
        obj1 = (object) !flag1;
      }
      if (flag2 && partObjectID == 0L)
        throw new KernelException($"Попытка создания исходящей связи типа '{this.UserSession.GetRelationType(this._RecordsTypeID).Description}' у объекта '{dbObject1.NameInMessages}'. Включен режим постоянной конкретизации связей, но версия дочернего объекта не указана.");
      if (relationGUID == Guid.Empty)
        relationGUID = Guid.NewGuid();
      long num4 = this.UserSession.UserID;
      if (prototype != null && ((this._AssignMode & Consts.CheckOutMode) == Consts.CheckOutMode || (this._AssignMode & 1024 /*0x0400*/) == 1024 /*0x0400*/))
        num4 = prototype.CreatorID;
      dataManager.ExecuteSpNonQuery("IMS_ADD_RELATION", dataManager.Parameter("inPRJLINK_ID", (object) 0L), dataManager.Parameter("inPROJ_ID", (object) projectID), dataManager.Parameter("inPART_ID", (object) partID), dataManager.Parameter("inRELATION_TYPE", (object) this._RecordsTypeID), dataManager.Parameter("inCREATE_DATE", (object) beginDate), dataManager.Parameter("inPRJ_GUID", (object) relationGUID.ToString()), dataManager.Parameter("inREL_CREATOR", (object) num4), dataManager.OutputParameter("outPRJLINK_ID", (object) num2));
      long int64 = Convert.ToInt64(dataManager.GetOutputParameterValue("outPRJLINK_ID"));
      IDBRelation relation = this.UserSession.GetRelation(int64);
      (relation as DBAttributable).SetAttributesState(Consts.CreateMode);
      (relation as DBRelation)._ProjObject = dbObject1;
      try
      {
        if (this._AssignMode == Consts.CheckOutMode || this._AssignMode == 1024 /*0x0400*/)
          (relation.Attributes as DBRelationAttributeCollection).ValidatingOn = false;
        (relation as DBRelation).InsertIntoView();
        if (prjlinkID > 0L || prjlinkID < -100L)
        {
          if (prototype == null)
            prototype = this.UserSession.GetRelation(prjlinkID);
          bool flag6 = false;
          if (this._AssignMode == Consts.CheckOutMode || this._AssignMode == 1024 /*0x0400*/ || this._AssignMode == 8192 /*0x2000*/)
            flag6 = (prototype.Attributes as DBAttributeCollection).QuickAddAttributes(int64, true, this._AssignMode == 1024 /*0x0400*/, this._AssignMode == 8192 /*0x2000*/);
          if (!flag6)
          {
            if (relation.RelationType == prototype.RelationType)
              (relation.Attributes as DBRelationAttributeCollection).Assign(prototype.Attributes, this._AssignMode);
            else
              (relation.Attributes as DBRelationAttributeCollection).AssignPossibleAttributes(prototype.Attributes, this._AssignMode);
          }
        }
        if (this._newRelationProperties.HasValue && this._newRelationProperties.Value.ValuesList != null)
        {
          for (int index = 0; index < this._newRelationProperties.Value.ValuesList.Length; ++index)
            relation.Attributes.AddAttribute(this._newRelationProperties.Value.ValuesList[index].AttributeID, false, this._newRelationProperties.Value.ValuesList[index].Values);
        }
        List<IMSAttribute4RelationType> relationTypeList = MetaDataHelper.GetAttribute4RelationTypeList(this._RecordsTypeID);
        for (int index = 0; index < relationTypeList.Count; ++index)
        {
          if (relationTypeList[index].Required == RequiredModes.Auto || relationTypeList[index].Required == RequiredModes.AutoRequired)
          {
            IDBAttribute dbAttribute = (relation.Attributes as DBAttributeCollection).AddAttribute(relationTypeList[index].AttributeID, false, false);
            if (dbAttribute.AttributeID == this.UserSession.IdentHelper.CompositionVersionID)
            {
              if (partObjectID == 0L)
              {
                if (dbAttribute.AsInteger == 0L)
                  throw new KernelExceptionID(sc_13556.ssp_appserver_13572(1369820789));
              }
              else
                dbAttribute.AsInteger = Math.Abs(partObjectID);
            }
          }
        }
        (relation as DBRelation)._PartObjectID = partObjectID;
        if (flag1 && (relation as DBRelation).IsCheckParentReadOnly)
        {
          (dbObject1 as DBObject).CheckEditMode(true, true, true);
          (dbObject1 as DBObject).SetModifyContentDate();
        }
        if (vals != null)
          (relation as DBRelation).SetAttributesValues(vals);
        if (flag2 | flag3 && partObjectID != 0L && relation.GetAttributeByID(this.UserSession.IdentHelper.CompositionVersionID) == null)
          relation.Attributes.AddAttribute(this.UserSession.IdentHelper.CompositionVersionID, false, new object[1]
          {
            (object) Math.Abs(partObjectID)
          });
        if (flag4)
          this.DoCopyAttrs2Child(dbObject1, partObject);
        (relation as DBRelation).DoAfterCreate(this._AssignMode);
        (dbObject1 as DBObject).DoAfterCreateRelation(relation);
        IEventLogHelper service = ServerServices.GetService(typeof (IEventLogHelper)) as IEventLogHelper;
        if (EventID > 0L)
          service.CloseEvent(EventID, projectID, int64, dbObject1.ID, (relation as DBRelation).ObjectName, "", EventlogRecordType.AccessGranted, (IUserSession) this.UserSession);
      }
      finally
      {
        (relation as DBAttributable).ClearAttributesState(Consts.CreateMode);
        (relation.Attributes as DBRelationAttributeCollection).ValidatingOn = true;
      }
      if (projectID > 0L)
      {
        if (obj1 == null)
          obj1 = (object) !(relation as DBRelation).Applicability.IsContent;
        if (prototype != null && prototype.ProjID == -projectID)
          obj1 = (object) false;
        if ((bool) obj1 && dbObject1.CheckoutBy != 0L && this.UserSession.GetObject(-projectID, false) != null)
          this.Create(new NewRelationProperties(relation, -projectID, relationGUID)
          {
            PartObjectID = partObjectID
          });
      }
      this.UserSession.AddDelayedNotification((DelayedNotification) new RelationDelayedNotification(this.UserSession.RealUserID, ActionType.AddLink, (AttributeValues[]) null, (relation as DBRelation).GetAttributes4Notification(), relation.RelationID, relation.RelationType, relation.ProjID, relation.PartID, relation.PartObjectID, string.Empty));
      this.UserSession.AddToModificationsHistory((CategoryValue) new RelationModificationEvent(5, relation.RelationID, ActionType.Create, relation.RelationType, relation.GUID, relation.ProjID));
      this.UserSession.AddToCreationLog(5, relation.RelationID);
      this.UserSession.Commit();
      return relation;
    }
    catch (Exception ex)
    {
      long fetchedVersionId;
      string str = string.Format(LocalizationHolder.rm.GetString("Kernel_503"), (object) this.GetObjectCaptionByList(dataTable, out fetchedVersionId), (object) fetchedVersionId, (object) dbObject1.NameInMessages, (object) dbObject1.ObjectID, (object) relationType.Description, (object) ex.Message);
      this.UserSession.Rollback();
      if (EventID > 0L)
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
      throw new KernelException(str, ex).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(dbObject1.ObjectID), (ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(fetchedVersionId));
    }
  }

  private void DoCopyAttrs2Child(IDBObject objProject, IDBObject partObject)
  {
    IDBObjectType objectTypeClass = (partObject as DBObject).ObjectTypeClass;
    for (int AttrIndex = 0; AttrIndex < objProject.Attributes.Count; ++AttrIndex)
    {
      DBAttribute attribute = objProject.Attributes[AttrIndex] as DBAttribute;
      if ((attribute.AttributeType.Options & AttributeOptions.CopyValues2ChildObject) == AttributeOptions.CopyValues2ChildObject && objectTypeClass.GetAttributeType(attribute.AttributeID) != null)
      {
        IDBAttribute attributeById = partObject.GetAttributeByID(attribute.AttributeID);
        if (attributeById != null)
          attributeById.Assign((IDBAttribute) attribute);
        else
          partObject.Attributes.AddAttribute(attribute.AttributeID, false, attribute.Values);
      }
    }
  }

  public IDBRelation Create(long projectID, long partObjectID)
  {
    long idByObjectId = SqlHelper.GetIDByObjectID(partObjectID, this.UserSession.DataManager);
    return this.Create(DateTime.UtcNow + this.UserSession.TimeZoneOffset, projectID, idByObjectId, 0L, partObjectID, (IDBRelation) null, Guid.Empty);
  }

  public IDBRelation Create(long projectID, long partObjectID, AttributeValues[] vals)
  {
    long idByObjectId = SqlHelper.GetIDByObjectID(partObjectID, this.UserSession.DataManager);
    return this.Create(DateTime.UtcNow + this.UserSession.TimeZoneOffset, projectID, idByObjectId, 0L, partObjectID, (IDBRelation) null, Guid.Empty, vals);
  }

  public IDBRelation Create(long projectID, long partObjectID, DateTime beginDate)
  {
    long idByObjectId = SqlHelper.GetIDByObjectID(partObjectID, this.UserSession.DataManager);
    return this.Create(beginDate, projectID, idByObjectId, 0L, partObjectID, (IDBRelation) null, Guid.Empty);
  }

  public IDBRelation Create(NewRelationProperties properties)
  {
    DateTime beginDate = !(properties.BeginDate == DateTime.MinValue) ? properties.BeginDate : DateTime.UtcNow + this.UserSession.TimeZoneOffset;
    if (properties.PartID == 0L)
      properties.PartID = SqlHelper.GetIDByObjectID(properties.PartObjectID, this.UserSession.DataManager);
    this.UserSession.StartTransaction();
    IDBRelation dbRelation;
    try
    {
      this._newRelationProperties = new NewRelationProperties?(properties);
      try
      {
        dbRelation = this.Create(beginDate, properties.ProjectObjectID, properties.PartID, properties.PrototypeRelationID, properties.PartObjectID, properties.PrototypeRelation, properties.RelationGUID, properties.ValuesList);
      }
      finally
      {
        this._newRelationProperties = new NewRelationProperties?();
      }
      this.UserSession.Commit();
    }
    catch
    {
      this.UserSession.Rollback();
      throw;
    }
    return dbRelation;
  }

  public long[] ConsistFromBlanks(long projectID)
  {
    IDbManager dataManager = this.UserSession.DataManager;
    string str = this.RelationTypeID >= 0 ? " AND R.F_RELATION_TYPE = " + this.RelationTypeID.ToString() : string.Empty;
    DataTable dataTable = dataManager.ExecuteDataTable($"SELECT O.F_OBJECT_ID FROM IMS_RELATIONS R, IMS_OBJECTS O WHERE R.F_PROJ_ID = :id{str} AND (R.F_CREATE_DATE <= {dataManager.DataProvider.Now}) AND O.F_ID = R.F_PART_ID AND O.F_OBJECT_VER_TYPE = -1", dataManager.Parameter("id", (object) projectID));
    long[] numArray = new long[dataTable.Rows.Count];
    for (int index = 0; index < dataTable.Rows.Count; ++index)
      numArray[index] = Convert.ToInt64(dataTable.Rows[index][0]);
    return numArray;
  }

  public int DeleteRelations(
    long projID,
    IList<Guid> relationsGUID,
    bool transactionMode,
    long deleteMode)
  {
    int num = 0;
    if (transactionMode)
      this.UserSession.StartTransaction();
    try
    {
      this.UserSession.StartLogHistory();
      try
      {
        for (int index = 0; index < relationsGUID.Count; ++index)
        {
          IDBRelation relation = this.UserSession.GetRelation(relationsGUID[index], projID, false);
          if (relation != null)
          {
            relation.Delete(deleteMode);
            ++num;
          }
        }
        if (transactionMode)
          this.UserSession.Commit();
      }
      finally
      {
        this.UserSession.StopLogHistory();
      }
    }
    catch
    {
      if (transactionMode)
        this.UserSession.Rollback();
      throw;
    }
    return num;
  }

  public string JoinFieldName
  {
    get => this.JoinDefaultFieldName;
    set
    {
      if (!(this.JoinDefaultFieldName != value))
        return;
      this.JoinDefaultFieldName = value;
    }
  }

  int IServerDBRelationCollection.AssignMode
  {
    get => this._AssignMode;
    set => this._AssignMode = value;
  }

  private void CheckParams(ref DBRecordSetParams @params)
  {
    this.CheckRelationType(ref @params);
    this.CheckLocalTypesMode(ref @params);
  }

  private void CheckRelationType(ref DBRecordSetParams @params)
  {
    if (this.RelationTypeID < 0 && @params.Columns == null)
      throw new KernelExceptionID(sc_13556.ssp_appserver_13573(292428211));
  }

  private void CheckLocalTypesMode(ref DBRecordSetParams @params)
  {
    if (!this.LocalTypesMode || @params.ColumnsInfo == null)
      return;
    for (int index = 0; index < @params.Columns.Length; ++index)
    {
      if (@params.ColumnsInfo[index].AttributeSource == AttributeSourceTypes.Object)
        this.ValidateLocalTypeAttribute(@params.Columns[index]);
    }
  }

  private ColumnDescriptor GetRelationIDColumn()
  {
    return new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PRJLINK_ID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 0);
  }

  private IEnumerable<RelationObjectBase> CreateRelationObjects(
    DataTable dataTable,
    DBRecordSetParams @params)
  {
    if (this._filter.Mode == DBRelationCollectionFilter.DBRelationCollectionFilterMode.Default)
      this.CorrectCompositionLCStepsAndLevels(@params, dataTable, this._filtrationRule);
    RecordSetParamsAdapter objectParamsAdapter = new RecordSetParamsAdapter(@params, AttributeSourceTypes.Object);
    RecordSetParamsAdapter relationParamsAdapter = new RecordSetParamsAdapter(@params, AttributeSourceTypes.Relation);
    IAttributeValueConverter attributeValueConverter = ServiceLocator.Get<IAttributeValueConverter>();
    return this.FunctionID == SelectFunction.Default || this.FunctionID == SelectFunction.ConsistFrom ? (IEnumerable<RelationObjectBase>) dataTable.Rows.Cast<DataRow>().Select<DataRow, CompositionPart>((System.Func<DataRow, CompositionPart>) (o => this.CreateCompositionPart(o, objectParamsAdapter, relationParamsAdapter, attributeValueConverter))) : (IEnumerable<RelationObjectBase>) dataTable.Rows.Cast<DataRow>().Select<DataRow, Applicability>((System.Func<DataRow, Applicability>) (o => this.CreateApplicability(o, objectParamsAdapter, relationParamsAdapter, attributeValueConverter)));
  }

  private IEnumerable<DataRow> ApplyFilter(IEnumerable<RelationObjectBase> relationObjects)
  {
    if (this.FunctionID == SelectFunction.Default || this.FunctionID == SelectFunction.ConsistFrom)
    {
      foreach (RelationObjectBase relationObjectBase in this._filter.Apply(relationObjects.Cast<CompositionPart>()))
        yield return (relationObjectBase.Relation.Attributes as AttributeCollectionDataRowAdapter).DataRow;
    }
    else
    {
      foreach (RelationObjectBase relationObjectBase in this._filter.Apply(relationObjects.Cast<Applicability>()))
        yield return (relationObjectBase.Relation.Attributes as AttributeCollectionDataRowAdapter).DataRow;
    }
  }

  private IEnumerable<DataRow> ApplyFilter(DataTable dataTable, DBRecordSetParams @params)
  {
    if (this._filter.Mode == DBRelationCollectionFilter.DBRelationCollectionFilterMode.Default)
      this.CorrectCompositionLCStepsAndLevels(@params, dataTable, this._filtrationRule);
    RecordSetParamsAdapter objectParamsAdapter = new RecordSetParamsAdapter(@params, AttributeSourceTypes.Object);
    RecordSetParamsAdapter relationParamsAdapter = new RecordSetParamsAdapter(@params, AttributeSourceTypes.Relation);
    IAttributeValueConverter attributeValueConverter = ServiceLocator.Get<IAttributeValueConverter>();
    if (this.FunctionID == SelectFunction.Default || this.FunctionID == SelectFunction.ConsistFrom)
    {
      foreach (RelationObjectBase relationObjectBase in this._filter.Apply(dataTable.Rows.Cast<DataRow>().Select<DataRow, CompositionPart>((System.Func<DataRow, CompositionPart>) (o => this.CreateCompositionPart(o, objectParamsAdapter, relationParamsAdapter, attributeValueConverter)))))
        yield return (relationObjectBase.Relation.Attributes as AttributeCollectionDataRowAdapter).DataRow;
    }
    else
    {
      foreach (RelationObjectBase relationObjectBase in this._filter.Apply(dataTable.Rows.Cast<DataRow>().Select<DataRow, Applicability>((System.Func<DataRow, Applicability>) (o => this.CreateApplicability(o, objectParamsAdapter, relationParamsAdapter, attributeValueConverter)))))
        yield return (relationObjectBase.Relation.Attributes as AttributeCollectionDataRowAdapter).DataRow;
    }
  }

  private List<ColumnDescriptor> GetRelationAttributeColumns()
  {
    List<ColumnDescriptor> attributeColumns = new List<ColumnDescriptor>();
    foreach (IMSAttribute4RelationType attribute4RelationType in MetaDataHelper.GetAttribute4RelationTypeList(this.RelationTypeID))
    {
      if (attribute4RelationType.IsGridable)
      {
        ColumnDescriptor columnDescriptor = new ColumnDescriptor((object) attribute4RelationType.AttributeID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 0);
        attributeColumns.Add(columnDescriptor);
      }
    }
    return attributeColumns;
  }

  private string GetObjectCaptionByList(DataTable part_versions, out long fetchedVersionId)
  {
    DataRow[] dataRowArray = part_versions.Select(string.Empty, "F_OBJECT_ID");
    if (dataRowArray.Length != 0)
    {
      fetchedVersionId = Convert.ToInt64(dataRowArray[dataRowArray.Length - 1][0]);
      IDBObject dbObject = this.UserSession.GetObject(fetchedVersionId, false);
      if (dbObject != null)
        return dbObject.NameInMessages;
      fetchedVersionId = 0L;
      return "Неизвестный объект";
    }
    fetchedVersionId = 0L;
    return "Неизвестный объект";
  }

  private DataTable SelectRecursiveLevel(
    DBRecordSetParams paramSet,
    long projectID,
    long partID,
    DateTime actualDate,
    int idColumn,
    bool isObjectID,
    List<long> idList,
    int prjLinkIndex)
  {
    SelectFunction functionId = this.FunctionID;
    DataTable table = this.Select(paramSet, projectID, partID, actualDate);
    DataTable toTable = table.Copy();
    this.FunctionID = functionId;
    RelationPair tag = paramSet.Tags != null ? paramSet.Tags[(object) "{78D53C74-3CF7-4F48-94FC-80C4FCB0BA77}"] as RelationPair : (RelationPair) null;
    int columnIndex1 = DBRecordSet.AttributeColumnIndex(paramSet, (object) -3, AttributeSourceTypes.Object, table);
    int columnIndex2 = DBRecordSet.AttributeColumnIndex(paramSet, (object) -2, AttributeSourceTypes.Object, table);
    int columnIndex3 = DBRecordSet.AttributeColumnIndex(paramSet, (object) -7, AttributeSourceTypes.Object, table);
    int columnIndex4 = DBRecordSet.AttributeColumnIndex(paramSet, (object) -21, AttributeSourceTypes.Relation, table);
    int columnIndex5 = DBRecordSet.AttributeColumnIndex(paramSet, (object) -20, AttributeSourceTypes.Relation, table);
    int columnIndex6 = DBRecordSet.AttributeColumnIndex(paramSet, (object) -23, AttributeSourceTypes.Relation, table);
    bool flag = MetaDataHelper.IsPdmPartiallyConfigurableRelationType(this.RelationTypeID) && columnIndex2 >= 0 && columnIndex3 >= 0 && columnIndex4 >= 0 && columnIndex5 >= 0 && columnIndex6 >= 0;
    foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
    {
      long num = Convert.ToInt64(row[idColumn]);
      if (!isObjectID)
      {
        string FiltrationRuleSettings = string.Empty;
        if (paramSet.Tags != null && paramSet.Tags[(object) "{7196FEC5-A048-4118-AF15-73BEEAA63A87}"] != null)
        {
          string stringValue = DataSetProcessor.GetStringValue(paramSet.Tags[(object) "{7196FEC5-A048-4118-AF15-73BEEAA63A87}"], string.Empty);
          string str = !string.IsNullOrEmpty(stringValue) ? stringValue : this.FiltrationOwnerID;
          FiltrationRuleSettings = !string.IsNullOrEmpty(str) ? str : string.Empty;
        }
        num = this.UserSession.GetObjectByVersionsRule(num, FiltrationRuleSettings, true).ObjectID;
      }
      if (projectID != 0L && projectID != -1L)
        projectID = num;
      else
        partID = columnIndex1 < 0 ? SqlHelper.GetIDByObjectID(num, this.UserSession.DataManager) : DataSetProcessor.GetInt64Value(row, columnIndex1, partID);
      DBRelationCollection relationCollection = this.UserSession.GetRelationCollection(this.RelationTypeID) as DBRelationCollection;
      relationCollection.FunctionID = this.FunctionID;
      string columnName;
      DataTable applicabilitiesList;
      if (this.FunctionID == SelectFunction.EntersIn || this.FunctionID == SelectFunction.EntersInVersion)
      {
        QuickObjectInfo objectInfo = this.UserSession.GetObjectInfo(num);
        columnName = "F_INOBJECT_TYPE";
        applicabilitiesList = this.UserSession.GetRelationsApplicabilityCollection().GetApplicabilitiesList(this.RelationTypeID, objectInfo.ObjectTypeID, -1);
      }
      else
      {
        QuickObjectInfo objectInfo = this.UserSession.GetObjectInfo(projectID);
        columnName = "F_OBJECT_TYPE";
        applicabilitiesList = this.UserSession.GetRelationsApplicabilityCollection().GetApplicabilitiesList(this.RelationTypeID, -1, objectInfo.ObjectTypeID);
      }
      List<int> intList = new List<int>();
      for (int index1 = 0; index1 < applicabilitiesList.Rows.Count; ++index1)
      {
        List<int> childrenIdRecursive = MetaDataHelper.GetLocalObjectTypeChildrenIDRecursive(Convert.ToInt32(applicabilitiesList.Rows[index1][columnName]));
        if (childrenIdRecursive != null && childrenIdRecursive.Count > 0)
        {
          for (int index2 = 0; index2 < childrenIdRecursive.Count; ++index2)
          {
            if (intList.IndexOf(childrenIdRecursive[index2]) < 0)
              intList.Add(childrenIdRecursive[index2]);
          }
        }
      }
      relationCollection.ChildObjectTypes = (IList<int>) intList;
      if (flag)
      {
        DataSetProcessor.GetInt64Value(row, columnIndex2, 0L);
        int int32Value1 = DataSetProcessor.GetInt32Value(row, columnIndex3, -1);
        long int64Value1 = DataSetProcessor.GetInt64Value(row, columnIndex4, 0L);
        long int64Value2 = DataSetProcessor.GetInt64Value(row, columnIndex5, 0L);
        int int32Value2 = DataSetProcessor.GetInt32Value(row, columnIndex6, -1);
        RelationPair relationPair = tag != null ? new RelationPair(tag.Handle, tag.TOP_OBJECT_ID, tag.TOP_OBJECT_TYPE, int64Value2, this.UserSession.UserID, int64Value1, int32Value2, int32Value1) : (RelationPair) null;
        paramSet.Tags[(object) "{78D53C74-3CF7-4F48-94FC-80C4FCB0BA77}"] = (object) relationPair;
      }
      try
      {
        long int64 = Convert.ToInt64(row[prjLinkIndex]);
        if (!idList.Contains(int64))
        {
          idList.Add(int64);
          DataRow[] fromRows = relationCollection.SelectRecursiveLevel(paramSet, projectID, partID, actualDate, idColumn, isObjectID, idList, prjLinkIndex).Select("");
          SqlHelper.AssignRows(toTable, (IEnumerable<DataRow>) fromRows);
        }
      }
      finally
      {
        if (flag)
          paramSet.Tags[(object) "{78D53C74-3CF7-4F48-94FC-80C4FCB0BA77}"] = (object) tag;
      }
    }
    return toTable;
  }

  private void SetMainTable(int relationTypeID, int partTypeID)
  {
    string[] array = (string[]) null;
    if (relationTypeID > -1)
      array = this.UserSession.DBCache.GetUpdateTables(-1, -1, relationTypeID);
    if (array == null || Array.IndexOf<string>(array, "IMV_R" + relationTypeID.ToString()) < 0)
    {
      this.UserSession.QueryBuilder.RelationsTypeID = -1;
      this.UserSession.QueryBuilder.RelationsTableName = "IMS_RELATIONS";
    }
    else
    {
      this.UserSession.QueryBuilder.RelationsTypeID = relationTypeID;
      this.UserSession.QueryBuilder.RelationsTableName = "IMV_R" + relationTypeID.ToString();
    }
    this.UserSession.QueryBuilder.relationsIDstruct.RelationTypeID = this.UserSession.QueryBuilder.RelationsTypeID;
    string[] updateTables = partTypeID <= -1 ? (string[]) null : this.UserSession.DBCache.GetUpdateTables(-1, partTypeID, -1);
    if (updateTables == null || Array.IndexOf<string>(updateTables, "IMV_O" + partTypeID.ToString()) < 0)
    {
      this.UserSession.QueryBuilder.OptimizedTypeID = -1;
      if (this.LocalTypesMode)
        this.UserSession.QueryBuilder.SystemTableName = "IMS_OBJECTS";
      else
        this.UserSession.QueryBuilder.SystemTableName = "IMS_OBJECTS_VIEW";
    }
    else
    {
      this.UserSession.QueryBuilder.OptimizedTypeID = partTypeID;
      this.UserSession.QueryBuilder.SystemTableName = "IMV_O" + partTypeID.ToString();
    }
    this.UserSession.QueryBuilder.IDstruct.ObjectTypeID = this.UserSession.QueryBuilder.OptimizedTypeID;
  }

  private void ConfigureOptimizer(int objectTypeID)
  {
    if (this.RelationTypeID > -1)
      this.SetMainTable(this.RelationTypeID, objectTypeID);
    else
      this.SetMainTable(-1, objectTypeID);
  }

  private CurrentEditingContext PrepareParams(ref DBRecordSetParams @params)
  {
    CurrentEditingContext editingContextData = this.GetEditingContextData(ref @params);
    this.PrepareSelectVersionRule(ref @params, ref editingContextData);
    this.PrepareActualDate();
    if (!this.ExistsMode)
      @params.RecordCount = -1;
    List<ColumnDescriptor> columnDescriptorList = new List<ColumnDescriptor>();
    if (@params.Columns == null)
    {
      columnDescriptorList.Add(this.GetRelationIDColumn());
      columnDescriptorList.AddRange((IEnumerable<ColumnDescriptor>) this.GetRelationAttributeColumns());
    }
    columnDescriptorList.AddRange((IEnumerable<ColumnDescriptor>) this.GetSortingColumns(ref @params));
    if (columnDescriptorList.Count > 0)
      this._addedColumnsCount += @params.AddColumnDescriptors(columnDescriptorList.ToArray(), (List<int>) null);
    return editingContextData;
  }

  private List<ColumnDescriptor> GetSortingColumns(ref DBRecordSetParams dbRecordSetParams)
  {
    List<ColumnDescriptor> sortingColumns = new List<ColumnDescriptor>();
    if (this._isManualSortingAllowed && this.RelationTypeID > -1 && (dbRecordSetParams.SortColumns == null || dbRecordSetParams.SortColumns.Length == 0 || dbRecordSetParams.SortColumns != null && dbRecordSetParams.SortColumns.Length == 1 && (this.EventHelper as EventLogHelper).GetAttributeID(dbRecordSetParams.SortColumns[0], false) == this.UserSession.IdentHelper.SortIndexID) && this.UserSession.IdentHelper.IsSortedRelationType(this.RelationTypeID))
    {
      this._isManualSorting = true;
      if (DBRecordSet.AttributeColumnExists(dbRecordSetParams, (object) ObligatoryObjectAttributes.CAPTION, AttributeSourceTypes.Object))
      {
        if (dbRecordSetParams.SortColumns == null)
        {
          dbRecordSetParams.SortColumns = new object[2]
          {
            (object) this.UserSession.IdentHelper.SortIndexID,
            (object) ObligatoryObjectAttributes.CAPTION
          };
          dbRecordSetParams.Orders = new SortOrders[2]
          {
            SortOrders.ASC,
            SortOrders.ASC
          };
        }
      }
      else if (dbRecordSetParams.SortColumns == null)
      {
        dbRecordSetParams.SortColumns = new object[1]
        {
          (object) this.UserSession.IdentHelper.SortIndexID
        };
        dbRecordSetParams.Orders = new SortOrders[1]
        {
          SortOrders.ASC
        };
      }
      bool flag = DBRecordSet.AttributeColumnExists(dbRecordSetParams, (object) this.UserSession.IdentHelper.SortIndexID, AttributeSourceTypes.Relation);
      if (!flag && this.UserSession.IdentHelper.IsSortedRelationType(this.RelationTypeID))
        flag = DBRecordSet.AttributeColumnExists(dbRecordSetParams, (object) this.UserSession.IdentHelper.SortIndexID, AttributeSourceTypes.Auto);
      if (!flag)
        sortingColumns.Add(new ColumnDescriptor((object) this.UserSession.IdentHelper.SortIndexID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0));
    }
    return sortingColumns;
  }

  private void CheckSelectFunctionConstraints()
  {
    if (this._UseAttrCompositionVersionID && this.FunctionID == SelectFunction.EntersIn)
    {
      this.FunctionID = SelectFunction.Default;
      throw new KernelExceptionID(sc_13556.ssp_appserver_13574(608454966));
    }
  }

  private void CheckFiltrationRule()
  {
    if (string.IsNullOrEmpty(this.FiltrationOwnerID) && this.FiltrationRule == null && this._UseAttrCompositionVersionID)
      throw new KernelException(LocalizationHolder.rm.GetString("Kernel_1009"));
  }

  private CurrentEditingContext GetFiltrationOverrideEditingContext(
    DBRecordSetParams dbRecordSetParams)
  {
    return dbRecordSetParams.Tags == null || !dbRecordSetParams.Tags.Contains((object) "{76094280-391F-44AC-8B7B-9B6DEA501110}") ? (CurrentEditingContext) null : dbRecordSetParams.Tags[(object) "{76094280-391F-44AC-8B7B-9B6DEA501110}"] as CurrentEditingContext;
  }

  private string GetSelectVersionRuleIDFromDBRecordSetParams(DBRecordSetParams dbRecordSetParams)
  {
    return dbRecordSetParams.Tags == null || dbRecordSetParams.Tags[(object) "{7196FEC5-A048-4118-AF15-73BEEAA63A87}"] == null ? string.Empty : (string) dbRecordSetParams.Tags[(object) "{7196FEC5-A048-4118-AF15-73BEEAA63A87}"];
  }

  private VersionsRule GetVersionsRule(string filtrationOwnerID)
  {
    IVersionRulesCacheService customService = this.UserSession.GetCustomService(typeof (IVersionRulesCacheService)) as IVersionRulesCacheService;
    switch (filtrationOwnerID)
    {
      case "cad001df-306c-11d8-b4e9-00304f19f545":
        return customService.LatestVersionsRule;
      case "cad001e0-306c-11d8-b4e9-00304f19f545":
      case "cad001e3-306c-11d8-b4e9-00304f19f545":
        return customService.AllVersionsRule;
      case "cad005aa-306c-11d8-b4e9-00304f19f545":
        return customService.GetDefaultVersionRule(this.UserSession.SessionGUID);
      case "cad005ac-306c-11d8-b4e9-00304f19f5455":
        return customService.AllConcreteVersionsRule;
      case "cad00601-306c-11d8-b4e9-00304f19f545":
        return customService.BaseVersionsRule;
      case "cad00602-306c-11d8-b4e9-00304f19f545":
        return customService.SequentialModificationsRule;
      default:
        return (VersionsRule) null;
    }
  }

  private void PrepareSelectVersionRule(
    ref DBRecordSetParams @params,
    ref CurrentEditingContext editingContextDataFromCallContext)
  {
    IVersionRulesCacheService customService = this.UserSession.GetCustomService(typeof (IVersionRulesCacheService)) as IVersionRulesCacheService;
    if (this.FiltrationRule != null)
    {
      this._filtrationRule = this.FiltrationRule;
    }
    else
    {
      if (string.IsNullOrEmpty(this.FiltrationOwnerID) || this.FiltrationOwnerID == "cad001e2-306c-11d8-b4e9-00304f19f545")
        this.FiltrationOwnerID = "cad005aa-306c-11d8-b4e9-00304f19f545";
      string fromRecordSetParams = CoreHelper.GetFiltrationOverrideOwnerIDFromRecordSetParams(@params);
      string str = string.IsNullOrEmpty(fromRecordSetParams) ? this.FiltrationOwnerID : fromRecordSetParams;
      this._filtrationRule = this.GetVersionsRule(str);
      if (this._filtrationRule == null)
      {
        FiltrationSettings filtrationSettings = customService.GetFiltrationSettings((object) this.UserSession, str, false);
        if (filtrationSettings != null)
        {
          if (@params.Tags == null && filtrationSettings.Tags != null && filtrationSettings.Tags.Count > 0)
            @params.Tags = (filtrationSettings.Clone() as FiltrationSettings).Tags;
          if (editingContextDataFromCallContext == null && this.UserSession.EditingContextSource == EditingContextSource.WindowContext)
            editingContextDataFromCallContext = filtrationSettings.EditingContext;
          bool RuleCompatible = true;
          bool RuleValid = true;
          bool VarsOutOfRange = true;
          this._filtrationRule = customService.GetFiltrationRule((object) this.UserSession, (IFiltrationSettings) filtrationSettings, ref RuleCompatible, ref RuleValid, ref VarsOutOfRange);
          if (((!RuleCompatible ? 1 : (!RuleValid ? 1 : 0)) | (VarsOutOfRange ? 1 : 0)) != 0)
            this._filtrationRule = customService.LatestVersionsRule;
        }
        else
        {
          Guid result;
          if (Guid.TryParse(str, out result))
          {
            IDBObject RuleObject = this.Session.GetObject(result);
            if (MetaDataHelper.IsObjectTypeChildOf(RuleObject.ObjectType, Intermech.Search.Data.Filters.Constants.SelectVersionRuleObjectTypeID))
              throw new OperationNotApplicableException();
            this._filtrationRule = new VersionsRule();
            this._filtrationRule.LoadFromObject(this.Session, RuleObject);
          }
        }
      }
    }
    if (this._filtrationRule == null)
      this._filtrationRule = customService.GetDefaultVersionRule(this.UserSession.SessionGUID);
    if (this._filtrationRule == null)
      return;
    if (@params.Tags == null)
      @params.Tags = new HybridDictionary();
    @params.Tags[(object) "{7196FEC5-A048-4118-AF15-73BEEAA63A87}"] = (object) this._filtrationRule;
  }

  private void PrepareActualDate()
  {
    if (this._filtrationRule == null || !(this._filtrationRule.ActualDate > DateTime.MinValue))
      return;
    this._ActualDate = this._filtrationRule.ActualDate.Date + DBRelationCollection.OneDay;
  }

  private CurrentEditingContextScope CreateCurrentEditingContextScope(
    CurrentEditingContext editingContextDataFromCallContext)
  {
    CurrentEditingContext editingContext = CurrentEditingContext.Dummy;
    if (editingContextDataFromCallContext != null)
      editingContext = editingContextDataFromCallContext;
    return new CurrentEditingContextScope(editingContext);
  }

  private CurrentEditingContext GetEditingContextData(ref DBRecordSetParams @params)
  {
    return CurrentEditingContextScope.TryGet() ?? DBRelationCollection.ParamsHelper.GetFiltrationOverrideEditingContext(ref @params);
  }

  private void ClearResultDataTable(DataTable dataTable)
  {
    if (this._addedColumnsCount <= 0 || dataTable == null || dataTable.Columns.Count < this._addedColumnsCount)
      return;
    for (int index = 0; index < this._addedColumnsCount; ++index)
      dataTable.Columns.RemoveAt(dataTable.Columns.Count - 1);
  }

  private void ApplyManualSorting(DataTable dataTable, ref DBRecordSetParams @params)
  {
    if (!this._isManualSorting)
      return;
    int attrColumnIndex = DBRecordSet.AttributeColumnIndex(@params, (object) Intermech.Search.Data.Filters.Constants.SortingAttributeTypeID, AttributeSourceTypes.Relation);
    if (attrColumnIndex < 0)
      attrColumnIndex = DBRecordSet.AttributeColumnIndex(@params, (object) Intermech.Search.Data.Filters.Constants.SortingAttributeTypeID, AttributeSourceTypes.Auto);
    DataSetProcessor.SortDataTableByIntegerAttribute(dataTable, attrColumnIndex);
  }

  private void ConfigureFilter(ref DBRecordSetParams @params)
  {
    this._filter.Configure(this.CreateFilterOptions(ref @params));
    this.ShowAllModifications = this._filter.DisableCoreFiltration;
    this.AddColumnsToParams((IEnumerable<ColumnDescriptor>) this._filter.Columns, ref @params);
  }

  private void AddColumnsToParams(
    IEnumerable<ColumnDescriptor> columns,
    ref DBRecordSetParams @params)
  {
    DBRecordSetParams prms = @params;
    IEnumerable<ColumnDescriptor> source = columns.Where<ColumnDescriptor>((System.Func<ColumnDescriptor, bool>) (o => !DBRecordSet.AttributeColumnExists(prms, o.AttributeID, o.AttributeSource)));
    this._addedColumnsCount += @params.AddColumnDescriptors(source.ToArray<ColumnDescriptor>(), (List<int>) null);
  }

  private void SetProjectVersionIDToFilter(
    long projectVersionID,
    Dictionary<long, int> optimizationDictionary)
  {
    int projectTypeID = -1;
    if (!ObjectHelper.IsUnknownObjectVersionID(projectVersionID) && (optimizationDictionary == null || !optimizationDictionary.TryGetValue(projectVersionID, out projectTypeID)))
    {
      IDBObject objectActualCopy = this.Session.GetObjectActualCopy(projectVersionID, false);
      if (objectActualCopy != null)
        projectTypeID = objectActualCopy.TypeID;
    }
    this._filter.SetProjectInfo(projectVersionID, projectTypeID);
  }

  private void SetPartVersionIDToFilter(
    long partVersionID,
    Dictionary<long, int> optimizationDictionary)
  {
    int partTypeID = -1;
    if (!ObjectHelper.IsUnknownObjectVersionID(partVersionID) && (optimizationDictionary == null || !optimizationDictionary.TryGetValue(partVersionID, out partTypeID)))
    {
      IDBObject objectActualCopy = this.Session.GetObjectActualCopy(partVersionID, false);
      if (objectActualCopy != null)
        partTypeID = objectActualCopy.TypeID;
    }
    this._filter.SetPartInfo(partVersionID, partTypeID);
  }

  private DBRelationCollectionFilter.DBRelationCollectionFilterOptions CreateFilterOptions(
    ref DBRecordSetParams @params)
  {
    long projectId = this._ProjectID != -1L ? this._ProjectID : 0L;
    int num1 = -1;
    long partObjectId = this._PartObjectID != -1L ? this._PartObjectID : 0L;
    int num2 = -1;
    if (@params.Tags == null)
      dictionary1 = (Dictionary<long, int>) null;
    else if (!(@params.Tags[(object) "{004511C2-5AA8-4831-B60A-7CD17C1A2D88}"] is Dictionary<long, int> dictionary1))
      dictionary1 = new Dictionary<long, int>();
    Dictionary<long, int> dictionary2 = dictionary1;
    if (!ObjectHelper.IsUnknownObjectVersionID(projectId) && (dictionary2 == null || !dictionary2.TryGetValue(projectId, out num1)))
    {
      QuickObjectInfo objectInfo = this.UserSession.GetObjectInfo(projectId);
      if (!objectInfo.Empty)
        num1 = objectInfo.ObjectTypeID;
    }
    if (!ObjectHelper.IsUnknownObjectVersionID(partObjectId) && (dictionary2 == null || !dictionary2.TryGetValue(partObjectId, out num2)))
    {
      QuickObjectInfo objectInfo = this.UserSession.GetObjectInfo(partObjectId);
      if (!objectInfo.Empty)
        num2 = objectInfo.ObjectTypeID;
    }
    DBRelationCollectionFilter.DBRelationCollectionFilterOptions filterOptions = new DBRelationCollectionFilter.DBRelationCollectionFilterOptions();
    filterOptions.BlokSeriesAndDatesFilters = CoreHelper.GetBlockSeriesAndDatesFromRecordSetParams(@params);
    filterOptions.ChildObjectTypeIds = this._ChildObjectTypes.ToList<int>();
    filterOptions.EditingContextVersionID = this.UserSession.EditingContextID;
    filterOptions.FillStatuses = DBRecordSet.AttributeColumnExists(@params, (object) ObligatoryObjectAttributes.F_ELEMENT_STATUSES, AttributeSourceTypes.Auto) || DBRecordSet.AttributeColumnExists(@params, (object) ObligatoryObjectAttributes.F_ELEMENT_STATUSES, AttributeSourceTypes.Object) || DBRecordSet.AttributeColumnExists(@params, (object) ObligatoryObjectAttributes.F_ELEMENT_STATUSES, AttributeSourceTypes.Relation);
    filterOptions.LocalTypesMode = this.LocalTypesMode;
    filterOptions.PartVersionID = partObjectId;
    filterOptions.PartTypeID = num2;
    filterOptions.ProjectVersionID = projectId;
    filterOptions.ProjectTypeID = num1;
    filterOptions.RelationTypeID = this.RelationTypeID;
    filterOptions.SelectFunction = this.FunctionID;
    filterOptions.SeriesDateSettingsHolder = CoreHelper.GetSeriesAndDatesSettingsHolderFromRecordSetParams(@params);
    filterOptions.ShowInvalidConcreteVersions = this.GetShowInvalidConcreteVersions();
    filterOptions.VersionRule = this._filtrationRule;
    filterOptions.UseStoredExplicitPartVersionID = DBRelationCollection.ParamsHelper.GetUseStoredExplicitPartVersionID(ref @params);
    return filterOptions;
  }

  private bool GetShowInvalidConcreteVersions()
  {
    try
    {
      return this.UserSession.Configurations.ReadBool("VersionsRule", "UISettings", "ShowInvalidConcreteVersions", false, DBConfigMode.UserOnly);
    }
    catch
    {
      return false;
    }
  }

  private CompositionPart CreateCompositionPart(
    DataRow dataRow,
    RecordSetParamsAdapter objectParamsAdapter,
    RecordSetParamsAdapter relationParamsAdapter,
    IAttributeValueConverter attributeValueConverter)
  {
    return new CompositionPart(new Relation((IAttributeCollection) new AttributeCollectionDataRowAdapter(dataRow, (IRecordSetParamsAdapter) relationParamsAdapter, attributeValueConverter)), new _Object((IAttributeCollection) new AttributeCollectionDataRowAdapter(dataRow, (IRecordSetParamsAdapter) objectParamsAdapter, attributeValueConverter)));
  }

  private Applicability CreateApplicability(
    DataRow dataRow,
    RecordSetParamsAdapter objectParamsAdapter,
    RecordSetParamsAdapter relationParamsAdapter,
    IAttributeValueConverter attributeValueConverter)
  {
    return new Applicability(new Relation((IAttributeCollection) new AttributeCollectionDataRowAdapter(dataRow, (IRecordSetParamsAdapter) relationParamsAdapter, attributeValueConverter)), new _Object((IAttributeCollection) new AttributeCollectionDataRowAdapter(dataRow, (IRecordSetParamsAdapter) objectParamsAdapter, attributeValueConverter)));
  }

  private void SetChildObjectTypeIds(IList<int> childObjectTypeIds)
  {
    if (childObjectTypeIds != null)
      this._ChildObjectTypes = childObjectTypeIds;
    if (this._ChildObjectTypes != null && this._ChildObjectTypes.Count != 0)
      return;
    this._ChildObjectTypes = (IList<int>) new List<int>(1);
    this._ChildObjectTypes.Add(this.ObjectTypeID);
  }

  private void Reset()
  {
    this._addedColumnsCount = 0;
    this._filtrationRule = (VersionsRule) null;
    this._isManualSorting = false;
  }

  private void PrepareResultDataTable(DataTable dataTable, ref DBRecordSetParams @params)
  {
    this.ClearResultDataTable(dataTable);
    dataTable.RemotingFormat = SerializationFormat.Binary;
    dataTable.ExtendedProperties[(object) "Eof"] = (object) true;
  }

  private DataTable SelectRecursive(
    DBRecordSetParams paramSet,
    long projectID,
    long partID,
    DateTime actualDate)
  {
    bool flag = false;
    if (paramSet.RecordCount == 0)
    {
      paramSet.RecordCount = -1;
      flag = true;
    }
    else if (paramSet.RecordCount != -1 && paramSet.RecordCount != -3)
      throw new KernelExceptionID(sc_13556.ssp_appserver_13575(166000108));
    this._SortOrder = string.Empty;
    int idColumn = -1;
    bool isObjectID = true;
    if (paramSet.Columns != null)
    {
      for (int index = 0; index < paramSet.Columns.Length; ++index)
      {
        int attributeId = (this.EventHelper as EventLogHelper).GetAttributeID(paramSet.Columns[index], true);
        if (attributeId == -2)
        {
          idColumn = index;
          break;
        }
        if (projectID != 0L && projectID != -1L)
        {
          if (attributeId == -22)
          {
            idColumn = index;
            isObjectID = false;
            break;
          }
        }
        else if (attributeId == -21)
        {
          idColumn = index;
          break;
        }
      }
    }
    if (idColumn < 0)
      throw new KernelExceptionID(sc_13556.ssp_appserver_13576(733821263));
    int prjLinkIndex = -1;
    int index1 = -1;
    for (int index2 = 0; index2 < paramSet.Columns.Length; ++index2)
    {
      if (this.UserSession.EventLogHelper.GetAttributeID(paramSet.Columns[index2]) == -20)
      {
        prjLinkIndex = index2;
        break;
      }
    }
    if (prjLinkIndex == -1)
    {
      List<int> AddedColumnsPos = new List<int>(1);
      if (paramSet.AddColumnDescriptors(new ColumnDescriptor[1]
      {
        new ColumnDescriptor((object) -20)
      }, AddedColumnsPos) > 0)
      {
        prjLinkIndex = AddedColumnsPos[0];
        index1 = prjLinkIndex;
      }
    }
    DataTable dataTable = this.SelectRecursiveLevel(paramSet, projectID, partID, actualDate, idColumn, isObjectID, new List<long>(), prjLinkIndex);
    if (index1 > -1)
      dataTable.Columns.RemoveAt(index1);
    if (flag)
    {
      int count = dataTable.Rows.Count;
      dataTable = new DataTable();
      dataTable.Columns.Add("COUNT", Type.GetType("System.int"));
      dataTable.Rows.Add((object) count);
    }
    else if (this._SortOrder != string.Empty)
    {
      int length = this._SortOrder.IndexOf(" ");
      string columnName = length >= 0 ? this._SortOrder.Substring(0, length) : this._SortOrder;
      if (dataTable.Columns.IndexOf(columnName) > -1)
      {
        DataRow[] fromRows = dataTable.Select(string.Empty, this._SortOrder);
        DataTable toTable = dataTable.Clone();
        SqlHelper.AssignRows(toTable, (IEnumerable<DataRow>) fromRows);
        dataTable = toTable;
      }
    }
    return dataTable;
  }

  private bool HasInConditionForProjectVersionID(DBRecordSetParams @params)
  {
    return @params.Conditions != null && ((IEnumerable<ConditionStructure>) @params.Conditions).Where<ConditionStructure>((System.Func<ConditionStructure, bool>) (o => (object.Equals(o.Attribute, (object) ObligatoryObjectAttributes.F_PROJ_ID) || object.Equals(o.Attribute, (object) -21)) && o.RelationalOperator == RelationalOperators.In && o.LogicalOperator != LogicalOperators.NOT)).Count<ConditionStructure>() > 0;
  }

  private bool HasInConditionForPartID(DBRecordSetParams @params)
  {
    return @params.Conditions != null && ((IEnumerable<ConditionStructure>) @params.Conditions).Where<ConditionStructure>((System.Func<ConditionStructure, bool>) (o => (object.Equals(o.Attribute, (object) ObligatoryObjectAttributes.F_PART_ID) || object.Equals(o.Attribute, (object) -22)) && o.RelationalOperator == RelationalOperators.In && o.LogicalOperator != LogicalOperators.NOT)).Count<ConditionStructure>() > 0;
  }

  private Dictionary<long, List<RelationObjectBase>> CreateDictionaryByProjectVersionID(
    IEnumerable<RelationObjectBase> relationObjects)
  {
    Dictionary<long, List<RelationObjectBase>> projectVersionId = new Dictionary<long, List<RelationObjectBase>>();
    foreach (RelationObjectBase relationObject in relationObjects)
    {
      List<RelationObjectBase> relationObjectBaseList = (List<RelationObjectBase>) null;
      projectVersionId.TryGetValue(relationObject.Relation.ProjectVersionID, out relationObjectBaseList);
      if (relationObjectBaseList == null)
      {
        relationObjectBaseList = new List<RelationObjectBase>();
        projectVersionId.Add(relationObject.Relation.ProjectVersionID, relationObjectBaseList);
      }
      relationObjectBaseList.Add(relationObject);
    }
    return projectVersionId;
  }

  private Dictionary<long, List<RelationObjectBase>> CreateDictionaryByPartID(
    IEnumerable<RelationObjectBase> relationObjects)
  {
    Dictionary<long, List<RelationObjectBase>> dictionaryByPartId = new Dictionary<long, List<RelationObjectBase>>();
    foreach (RelationObjectBase relationObject in relationObjects)
    {
      List<RelationObjectBase> relationObjectBaseList = (List<RelationObjectBase>) null;
      dictionaryByPartId.TryGetValue(relationObject.Relation.PartID, out relationObjectBaseList);
      if (relationObjectBaseList == null)
      {
        relationObjectBaseList = new List<RelationObjectBase>();
        dictionaryByPartId.Add(relationObject.Relation.PartID, relationObjectBaseList);
      }
      relationObjectBaseList.Add(relationObject);
    }
    return dictionaryByPartId;
  }

  private void CompleteOptimizationDictionary(
    Dictionary<long, int> optimizationDictionary,
    List<long> projectVersionIds)
  {
    IEnumerable<long> source = projectVersionIds.Where<long>((System.Func<long, bool>) (o => !optimizationDictionary.ContainsKey(o)));
    if (source.Count<long>() == 0)
      return;
    try
    {
      foreach (ObjInfoItem updateUnknownType in (this.Session.GetCustomService(typeof (ITypedInfoService)) as ITypedInfoService).UpdateUnknownTypes(source.Select<long, ObjInfoItem>((System.Func<long, ObjInfoItem>) (o => new ObjInfoItem(o))), (object) this.Session))
        optimizationDictionary[updateUnknownType.ObjectID] = updateUnknownType.ObjTypeID;
    }
    catch
    {
      IDBObjectCollection objectCollection = this.Session.GetObjectCollection(-1);
      DBRecordSetParams paramSet;
      // ISSUE: explicit reference operation
      ^ref paramSet = new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure()
        {
          Attribute = (object) ObligatoryObjectAttributes.F_OBJECT_ID,
          RelationalOperator = RelationalOperators.In,
          Value = (object) source.ToArray<long>(),
          SQL = ""
        }
      }, new object[2]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID,
        (object) ObligatoryObjectAttributes.F_OBJECT_TYPE
      });
      foreach (DataRow row in (InternalDataCollectionBase) objectCollection.Select(paramSet).Rows)
      {
        long int64Value = DataSetProcessor.GetInt64Value(row, 0, 0L);
        int int32Value = DataSetProcessor.GetInt32Value(row, 1, -1);
        optimizationDictionary[int64Value] = int32Value;
      }
    }
  }

  public List<long> QuickConsistFrom(long[] rootIDs, List<int> targetObjectTypeIDs)
  {
    List<long> result = new List<long>();
    this.QuickConsistFrom(rootIDs, targetObjectTypeIDs, result);
    return result;
  }

  internal void QuickConsistFrom(long[] rootIDs, List<int> targetObjectTypeIDs, List<long> result)
  {
    if (rootIDs.Length == 0)
      return;
    List<IDbDataParameter> dbDataParameterList = new List<IDbDataParameter>(rootIDs.Length);
    IDbManager dataManager = this.UserSession.DataManager;
    string str1 = string.Empty;
    string[] updateTables = this.UserSession.DBCache.GetUpdateTables(-1, -1, this.RelationTypeID);
    string str2;
    if (updateTables != null)
    {
      str2 = updateTables[0];
    }
    else
    {
      str2 = "IMS_RELATIONS";
      if (this.RelationTypeID >= 0)
      {
        str1 = " AND F_RELATION_TYPE = :relTypeID";
        dbDataParameterList.Add(dataManager.Parameter("relTypeID", (object) this.RelationTypeID));
      }
    }
    DataTable dataTable;
    using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate())
    {
      StringBuilder stringBuilder = objectPoolScope.Object;
      for (int index = 0; index < rootIDs.Length; ++index)
      {
        stringBuilder.AppendFormat(":prjID{0},", (object) index);
        dbDataParameterList.Add(dataManager.Parameter("prjID" + index.ToString(), (object) rootIDs[index]));
      }
      --stringBuilder.Length;
      dataTable = dataManager.ExecuteDataTable($"SELECT O.F_OBJECT_ID, O.F_OBJECT_TYPE FROM {str2} R, IMS_OBJECTS O WHERE R.F_PROJ_ID IN ({stringBuilder.ToString()}) AND O.F_ID = R.F_PART_ID{str1}", dbDataParameterList.ToArray());
    }
    if (dataTable.Rows.Count == 0)
      return;
    List<long> longList = new List<long>(dataTable.Rows.Count);
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      long int64 = Convert.ToInt64(dataTable.Rows[index][0]);
      if (longList.IndexOf(int64) < 0)
        longList.Add(int64);
      if ((targetObjectTypeIDs == null || targetObjectTypeIDs.IndexOf(Convert.ToInt32(dataTable.Rows[index][1])) >= 0) && result.IndexOf(int64) < 0)
        result.Add(int64);
    }
    this.QuickConsistFrom(longList.ToArray(), targetObjectTypeIDs, result);
  }

  private static class ParamsHelper
  {
    public static CurrentEditingContext GetFiltrationOverrideEditingContext(
      ref DBRecordSetParams @params)
    {
      return @params.Tags == null || !@params.Tags.Contains((object) "{76094280-391F-44AC-8B7B-9B6DEA501110}") ? (CurrentEditingContext) null : @params.Tags[(object) "{76094280-391F-44AC-8B7B-9B6DEA501110}"] as CurrentEditingContext;
    }

    public static bool GetUseStoredExplicitPartVersionID(ref DBRecordSetParams @params)
    {
      return @params.Tags != null && object.Equals(@params.Tags[(object) "{4534BBF7-86AF-4BCB-B7FF-C9AE40D28CB4}"], (object) true);
    }
  }

  internal static class Settings
  {
    public static readonly bool CheckCycles = true;

    static Settings()
    {
      string str = ConfigurationManager.AppSettings.Get("CheckCycleRelations");
      if (str == null)
        return;
      DBRelationCollection.Settings.CheckCycles = DBRelationCollection.Settings.ConvertToBoolean(str);
    }

    private static bool ConvertToBoolean(string value)
    {
      return !string.IsNullOrEmpty(value) && !(value == "0") && !(value.ToLower() == "false");
    }
  }
}
