// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBObjectCollection
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Contexts;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using Intermech.Kernel.Services;
using Intermech.Localization;
using Intermech.Pools;
using Intermech.Search.Data.Modifiers;
using Intermech.Text;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;


namespace Intermech.Kernel;

public class DBObjectCollection : 
  DBRecordSet,
  IDBObjectCollection,
  IDBRecords,
  IDBSessionable,
  IDBAttributableCollection
{
  private List<int> _DisabledProtoRelationTypes;
  private List<int> _DisabledPrototypeAttributes;
  private SynchronicReleaseCreateVersionModifier _synchronicReleaseCreateVersionModifier;

  public DBObjectCollection(UserSession uSession, int objectType)
    : base(uSession, objectType)
  {
    this._DBObjectTableName = "IMS_OBJECTS";
    this._DBKeyField = "F_OBJECT_ID";
    this.SetAttributesTable(objectType);
    this._DBKeyFieldID = Convert.ToInt32((object) ObligatoryObjectAttributes.F_OBJECT_ID);
    this.InitSecurityOptions(1, 0L);
    this._synchronicReleaseCreateVersionModifier = new SynchronicReleaseCreateVersionModifier(uSession, ServiceUtils.GetService<IPairedObjectsCreatorService>((object) ServerServices.ServiceContainer, true));
  }

  private void SetAttributesTable(int objTypeID)
  {
    if (objTypeID > -1)
    {
      DBObjectType objectType = this.UserSession.GetObjectType(objTypeID) as DBObjectType;
      if (objectType.IsLocalType)
        this._DBAttributesTableName = objectType.AttributesTableName;
      else
        this._DBAttributesTableName = "IMS_OBJECT_ATTRS";
    }
    else
      this._DBAttributesTableName = "IMS_OBJECT_ATTRS";
  }

  protected override void InitSecurityOptions(int aCategoryType, long aCategoryID)
  {
    base.InitSecurityOptions(aCategoryType, aCategoryID);
    this.AccessActions.Add(ActionType.Create, false);
    this.AccessActions.Add(ActionType.CreateChildItem, false);
  }

  protected override AttributeSourceTypes AutoAttributeSourceTypes
  {
    [DebuggerStepThrough] get => AttributeSourceTypes.Object;
  }

  public override string ObjectName
  {
    get
    {
      if (this._objectName != null)
        return this._objectName;
      return this.ObjectTypeID < 0 ? (this._objectName = LocalizationHolder.rm.GetString("Kernel_421")) : (this._objectName = string.Format(LocalizationHolder.rm.GetString("Kernel_422"), (object) this.UserSession.GetObjectType(this.ObjectTypeID).ObjectTypeName));
    }
  }

  protected override IDBAttributeType[] GetColumnsCollection(
    ref DBRecordSetParams pars,
    bool failIfNotFound)
  {
    this._CaptionAttributeName = (string) null;
    IDBObjectType dbObjectType = (IDBObjectType) null;
    if (pars.Columns == null)
    {
      dbObjectType = this.ObjectTypeID >= 0 ? this.UserSession.GetObjectType(this.ObjectTypeID) : throw new KernelExceptionID(sc_13375.ssp_appserver_13376(1430954544));
      DataTable dataTable = dbObjectType.Attributes.Select("");
      for (int index = dataTable.Rows.Count - 1; index > -1; --index)
      {
        if (!dbObjectType.Attributes.GetAttributeByID(Convert.ToInt32(dataTable.Rows[index]["F_ATTRIBUTE_ID"])).IsGridable)
          dataTable.Rows[index].Delete();
      }
      dataTable.AcceptChanges();
      pars.Columns = new object[dataTable.Rows.Count + 1];
      pars.Columns[0] = (object) Convert.ToInt32((object) ObligatoryObjectAttributes.F_OBJECT_ID);
      for (int index = 0; index < dataTable.Rows.Count; ++index)
        pars.Columns[index + 1] = (object) Convert.ToInt32(dataTable.Rows[index]["F_ATTRIBUTE_ID"]);
    }
    if (this.ObjectTypeID >= 0)
    {
      if (dbObjectType == null)
        dbObjectType = this.UserSession.GetObjectType(this.ObjectTypeID);
      int captionAttribute = dbObjectType.CaptionAttribute;
      if (captionAttribute > 0)
        this._CaptionAttributeName = this.UserSession.GetAttributeType(captionAttribute).Name;
    }
    IDBAttributeType[] columnsCollection = dbObjectType == null ? base.GetColumnsCollection(ref pars, failIfNotFound) : dbObjectType.Attributes.GetAttributeTypeList(pars.Columns, failIfNotFound);
    for (int index = 0; index < columnsCollection.Length; ++index)
    {
      if (columnsCollection[index].AttributeID < 0 && ObligatoryObjectAttributesHelper.GetAttributeSourceType((ObligatoryObjectAttributes) columnsCollection[index].AttributeID) != AttributeSourceTypes.Object)
        throw new KernelException(string.Format(sc_13375.ssp_appserver_13377(), (object) ObligatoryObjectAttributesHelper.GetCaption((ObligatoryObjectAttributes) columnsCollection[index].AttributeID)));
    }
    return columnsCollection;
  }

  protected override void ConfigureQueryBuilder(ConditionStructure[] conditions)
  {
    base.ConfigureQueryBuilder(conditions);
    this.UserSession.QueryBuilder.TypeFilter = this.GetObjectsFilter(this.ObjectTypeID, string.Empty);
    this.UserSession.QueryBuilder.ObjectAttributesTable = this._DBAttributesTableName;
  }

  public int ObjectTypeID
  {
    get => this._RecordsTypeID;
    set
    {
      if (this.ObjectTypeID == value)
        return;
      if (value > -1 && this.UserSession.DBCache.GetTable("IMS_OBJECT_TYPES").Rows.Find((object) value) == null)
        throw new KernelException(string.Format(LocalizationHolder.rm.GetString(sc_13375.ssp_appserver_13378()), (object) value));
      this._RecordsTypeID = value <= -1 || !this.LocalTypesMode ? value : throw new KernelException("Использование недопустимого условия фильтрации в режиме поиска среди объектов глобальных и локальных типов.");
      this._objectName = (string) null;
      this.SetAttributesTable(value);
    }
  }

  private void FillCaptionID(DataTable tbl)
  {
    if (this._CaptionAttributeName == null)
      return;
    for (int index = 0; index < tbl.Columns.Count; ++index)
    {
      if (tbl.Columns[index].ColumnName.ToUpper() == this._CaptionAttributeName.ToUpper())
      {
        tbl.ExtendedProperties.Add((object) "Caption", (object) index);
        break;
      }
    }
  }

  private void SetMainTable(int typeID)
  {
    string[] updateTables = this.UserSession.DBCache.GetUpdateTables(-1, typeID, -1);
    if (updateTables == null || typeID == -1 || Array.IndexOf<string>(updateTables, "IMV_O" + typeID.ToString()) < 0)
    {
      this.UserSession.QueryBuilder.OptimizedTypeID = -1;
      if (this.LocalTypesMode)
        this.UserSession.QueryBuilder.SystemTableName = "IMS_OBJECTS";
      else
        this.UserSession.QueryBuilder.SystemTableName = "IMS_OBJECTS_VIEW";
    }
    else
    {
      this.UserSession.QueryBuilder.OptimizedTypeID = typeID;
      this.UserSession.QueryBuilder.SystemTableName = "IMV_O" + typeID.ToString();
    }
    this.UserSession.QueryBuilder.IDstruct.ObjectTypeID = this.UserSession.QueryBuilder.OptimizedTypeID;
  }

  private void ConfigureOptimizer()
  {
    if (this.ObjectTypeID > -1)
      this.SetMainTable(this.ObjectTypeID);
    else
      this.SetMainTable(-1);
  }

  private int[] GetSelectorTypes(LocalTypesSelector typeSelector)
  {
    List<int> oTypes;
    switch (typeSelector)
    {
      case LocalTypesByObjectRefSelector _:
        oTypes = new List<int>();
        LocalTypesByObjectRefSelector objectRefSelector = typeSelector as LocalTypesByObjectRefSelector;
        List<IDbDataParameter> dbDataParameterList = new List<IDbDataParameter>();
        string str = "";
        int num1 = 0;
        foreach (long objectId in objectRefSelector.ObjectIDs)
        {
          ++num1;
          string parameterName = "objID" + num1.ToString();
          dbDataParameterList.Add(this.UserSession.DataManager.Parameter(parameterName, (object) objectId));
          if (str != "")
            str += ",";
          str = $"{str}:{parameterName}";
        }
        dbDataParameterList.Add(this.UserSession.DataManager.Parameter("attrID", (object) objectRefSelector.AttributeID));
        DataTable dataTable = this.UserSession.DataManager.ExecuteDataTable($"SELECT DISTINCT O.F_OBJECT_TYPE FROM IMS_OBJECT_LINKS L, IMS_OBJECTS O WHERE L.F_TOOBJECT_ID in ({str}) AND L.F_ATTRIBUTE_ID = :attrID AND O.F_OBJECT_ID = L.F_OBJECT_ID", dbDataParameterList.ToArray());
        for (int index = 0; index < dataTable.Rows.Count; ++index)
        {
          if (this.ObjectTypeID <= 0 || MetaDataHelper.IsObjectTypeChildOf(Convert.ToInt32(dataTable.Rows[index][0]), this.ObjectTypeID))
            oTypes.Add(Convert.ToInt32(dataTable.Rows[index][0]));
        }
        break;
      case LocalTypesByObjectIDsSelector _:
        oTypes = new List<int>();
        LocalTypesByObjectIDsSelector objectIdsSelector = typeSelector as LocalTypesByObjectIDsSelector;
        IDbManager dataManager = this.UserSession.DataManager;
        int capacity = objectIdsSelector.ObjectIDs.Length;
        if (capacity > dataManager.DataProvider.MaximumINOperands)
          capacity = dataManager.DataProvider.MaximumINOperands;
        List<IDbDataParameter> preparedParams = new List<IDbDataParameter>(capacity);
        using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate(capacity))
        {
          StringBuilder sb = objectPoolScope.Object;
          int num2 = 0;
          for (int index = 0; index < objectIdsSelector.ObjectIDs.Length; ++index)
          {
            sb.AppendFormat(":par{0},", (object) index);
            preparedParams.Add(dataManager.Parameter("par" + index.ToString(), (object) objectIdsSelector.ObjectIDs[index]));
            if (++num2 >= dataManager.DataProvider.MaximumINOperands)
            {
              this.AppendOTypes(oTypes, sb, preparedParams);
              num2 = 0;
              preparedParams.Clear();
              sb.Clear();
            }
          }
          if (preparedParams.Count > 0)
          {
            this.AppendOTypes(oTypes, sb, preparedParams);
            break;
          }
          break;
        }
      case LocalTypesList _:
        return (typeSelector as LocalTypesList).TypeIDs;
      default:
        this.UserSession.GetObjectType(this.ObjectTypeID, true);
        oTypes = MetaDataHelper.GetObjectTypeChildrenIDRecursive(this.ObjectTypeID);
        break;
    }
    return oTypes.ToArray();
  }

  private void AppendOTypes(
    List<int> oTypes,
    StringBuilder sb,
    List<IDbDataParameter> preparedParams)
  {
    --sb.Length;
    DataTable dataTable = this.UserSession.DataManager.ExecuteDataTable($"SELECT DISTINCT F_OBJECT_TYPE FROM IMS_OBJECTS WHERE F_OBJECT_ID IN ({sb.ToString()})", preparedParams.ToArray());
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      int int32 = Convert.ToInt32(dataTable.Rows[index][0]);
      if ((this.ObjectTypeID <= 0 || MetaDataHelper.IsObjectTypeChildOf(int32, this.ObjectTypeID)) && oTypes.IndexOf(int32) < 0)
        oTypes.Add(int32);
    }
  }

  public virtual DataTable SelectByTagSelector(
    DBRecordSetParams paramSet,
    LocalTypesSelector typeSelector)
  {
    DataTable toTable1 = (DataTable) null;
    int[] selectorTypes = this.GetSelectorTypes(typeSelector);
    paramSet.Tags[(object) "LocalTypesSelector"] = (object) null;
    bool flag = paramSet.SortColumns != null && paramSet.Orders != null && paramSet.RecordCount != 0;
    DBRecordSetParams dbRecordSetParams;
    if (flag)
    {
      dbRecordSetParams = new DBRecordSetParams(paramSet);
      dbRecordSetParams.Orders = (SortOrders[]) null;
      dbRecordSetParams.SortColumns = (object[]) null;
      dbRecordSetParams.SortContents = (ColumnContents[]) null;
      dbRecordSetParams.SortSources = (AttributeSourceTypes[]) null;
    }
    else
      dbRecordSetParams = paramSet;
    for (int index = 0; index < selectorTypes.Length; ++index)
    {
      IMSObjectType objectType = MetaDataHelper.GetObjectType(selectorTypes[index]);
      if (objectType.VersionsMode != ObjectVersionModes.Abstract && objectType.IsLocalType)
      {
        IDBObjectCollection objectCollection = this.UserSession.GetObjectCollection(selectorTypes[index]);
        if (toTable1 == null)
        {
          toTable1 = objectCollection.Select(dbRecordSetParams.Clone());
        }
        else
        {
          DataTable fromTable = objectCollection.Select(dbRecordSetParams.Clone());
          if (fromTable.Rows.Count > 0)
          {
            if (paramSet.RecordCount == 0)
            {
              if (toTable1.Rows.Count > 0)
                toTable1.Rows[0][0] = (object) (Convert.ToInt64(toTable1.Rows[0][0]) + Convert.ToInt64(fromTable.Rows[0][0]));
            }
            else
              DataSetProcessor.AddTable(toTable1, fromTable, true);
          }
        }
      }
    }
    if (toTable1 == null)
    {
      if (this.ObjectTypeID > 0)
        return this.Select(paramSet);
      throw new KernelException("Local object types not specified.");
    }
    if (flag)
    {
      using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate())
      {
        StringBuilder stringBuilder = objectPoolScope.Object;
        for (int index1 = 0; index1 < paramSet.SortColumns.Length; ++index1)
        {
          if (paramSet.Orders[index1] != SortOrders.NONE)
          {
            int attributeId = this.UserSession.EventLogHelper.GetAttributeID(paramSet.SortColumns[index1]);
            for (int index2 = 0; index2 < paramSet.Columns.Length; ++index2)
            {
              if (attributeId == this.UserSession.EventLogHelper.GetAttributeID(paramSet.Columns[index2]))
              {
                stringBuilder.AppendFormat("{0} {1},", (object) toTable1.Columns[index2].ColumnName, (object) paramSet.Orders[index1]);
                break;
              }
            }
          }
        }
        if (stringBuilder.Length > 0)
        {
          --stringBuilder.Length;
          DataTable toTable2 = toTable1.Clone();
          DataSetProcessor.AssignRows(toTable2, (IEnumerable<DataRow>) toTable1.Select(string.Empty, stringBuilder.ToString()));
          toTable1 = toTable2;
        }
      }
    }
    return toTable1;
  }

  public override DataTable Select(DBRecordSetParams paramSet)
  {
    DataTable source = paramSet.Tags == null || paramSet.Tags[(object) "LocalTypesSelector"] == null ? this.Select(paramSet, this._CheckAccess) : this.SelectByTagSelector(paramSet, paramSet.Tags[(object) "LocalTypesSelector"] as LocalTypesSelector);
    ElementStatusesService.PrepareElementStatusesColumn(ref source, DBRecordSet.GetElementsStatusesColumnIdx(ref paramSet));
    if (source != null)
      source.RemotingFormat = SerializationFormat.Binary;
    return source;
  }

  public DataTable SelectWithLocalObjects(DBRecordSetParams paramSet)
  {
    DataTable toTable = this.Select(paramSet);
    DataTable dataTable = this.ObjectTypeID != -1 ? this.UserSession.GetObjectTypeCollection(this.ObjectTypeID).SelectRecursive(string.Empty) : this.UserSession.GetObjectTypeCollection(-2).Select(string.Empty);
    if (dataTable.Rows.Count > 0)
    {
      for (int index = 0; index < dataTable.Rows.Count; ++index)
      {
        if ((Convert.ToInt32(dataTable.Rows[index]["F_OPTIONS"]) & 16 /*0x10*/) == 16 /*0x10*/)
        {
          DataTable fromTable = this.UserSession.GetObjectCollection(Convert.ToInt32(dataTable.Rows[index]["F_OBJECT_TYPE"])).Select(paramSet);
          if (fromTable.Rows.Count > 0)
            DataSetProcessor.AddTable(toTable, fromTable, true);
        }
      }
    }
    return toTable;
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
            paramSet.Conditions[index].AttributeSource = AttributeSourceTypes.Object;
            break;
          case AttributeSourceTypes.Relation:
            throw new KernelExceptionID(sc_13375.ssp_appserver_13379(373181502), (object) this.UserSession.GetAttributeType(Convert.ToInt32(paramSet.Conditions[index].Attribute)).Name);
          case AttributeSourceTypes.Events:
            throw new KernelExceptionID(sc_13375.ssp_appserver_13380(752559386), (object) this.UserSession.GetAttributeType(Convert.ToInt32(paramSet.Conditions[index].Attribute)).Name);
        }
        int int32 = Convert.ToInt32(paramSet.Conditions[index].Attribute);
        if (int32 < 0)
        {
          switch (ObligatoryObjectAttributesHelper.GetAttributeSourceType((ObligatoryObjectAttributes) int32))
          {
            case AttributeSourceTypes.Object:
            case AttributeSourceTypes.Other:
              if (int32 == -15)
              {
                if (!this._ShowAllModifications)
                  this._MustTurnAllModificationsModeOFF = true;
                this._ShowAllModifications = true;
                break;
              }
              break;
            default:
              throw new KernelExceptionID(sc_13375.ssp_appserver_13381(1629353539), (object) this.UserSession.GetAttributeType(int32).Name);
          }
        }
        if (this.LocalTypesMode)
          this.ValidateLocalTypeAttribute((object) int32);
      }
    }
    if (paramSet.ColumnsInfo != null)
    {
      for (int index = 0; index < paramSet.ColumnsInfo.Length; ++index)
      {
        if (!(paramSet.ColumnsInfo[index].AttributeID is int))
          paramSet.ColumnsInfo[index].AttributeID = (object) (this.EventHelper as EventLogHelper).GetAttributeID(paramSet.ColumnsInfo[index].AttributeID, false);
        switch (paramSet.ColumnsInfo[index].AttributeSource)
        {
          case AttributeSourceTypes.Auto:
            paramSet.ColumnsInfo[index].AttributeSource = AttributeSourceTypes.Object;
            break;
          case AttributeSourceTypes.Relation:
            throw new KernelExceptionID(sc_13375.ssp_appserver_13382(1095329785), (object) this.UserSession.GetAttributeType(Convert.ToInt32(paramSet.ColumnsInfo[index].AttributeID)).Name);
          case AttributeSourceTypes.Events:
            throw new KernelExceptionID(sc_13375.ssp_appserver_13383(1244784606), (object) this.UserSession.GetAttributeType(Convert.ToInt32(paramSet.ColumnsInfo[index].AttributeID)).Name);
        }
      }
    }
    return paramSet;
  }

  private DBRecordSetParams PrepareParams(DBRecordSetParams paramSet)
  {
    this._AddedColumns.Clear();
    List<ColumnDescriptor> columnDescriptorList = new List<ColumnDescriptor>(0);
    this._visibilityFiltration = false;
    if (DBRecordSet._attrVisibility <= 0)
      DBRecordSet._attrVisibility = this.UserSession.IdentHelper.GetAttributeID("cad0062f-306c-11d8-b4e9-00304f19f545");
    bool flag1 = false;
    bool flag2 = false;
    if (this.ObjectTypeID != -1 && this.UserSession.EnabledVisibilityFiltration && paramSet.Tags != null && paramSet.Tags[(object) "{7FB30639-2F65-4407-B78E-523547B1B133}"] != null && paramSet.Tags[(object) "{7FB30639-2F65-4407-B78E-523547B1B133}"].Equals((object) true) && !this.UserSession.IsAdmin)
      flag2 = true;
    if (flag2)
    {
      IMSAttribute4ObjectType attribute4ObjectType = MetaDataHelper.GetAttribute4ObjectType(this.ObjectTypeID, DBRecordSet._attrVisibility);
      flag1 = attribute4ObjectType != null && (attribute4ObjectType.OptimizationMode == OptimizationModes.Read || attribute4ObjectType.OptimizationMode == OptimizationModes.Seek);
    }
    if (flag1)
    {
      this._visibilityFiltration = true;
      if (!this.AttributeColumnExists(paramSet, (object) DBRecordSet._attrVisibility, ColumnNameMapping.Index, AttributeSourceTypes.Object))
        columnDescriptorList.Add(new ColumnDescriptor((object) DBRecordSet._attrVisibility, AttributeSourceTypes.Object, ColumnContents.String, ColumnNameMapping.Index, SortOrders.NONE, 0));
      if (!this.AttributeColumnExists(paramSet, (object) ObligatoryObjectAttributes.F_OWNER_ID, ColumnNameMapping.Index, AttributeSourceTypes.Object))
        columnDescriptorList.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OWNER_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0));
    }
    if (columnDescriptorList.Count > 0)
      paramSet.AddColumnDescriptors(columnDescriptorList.ToArray(), this._AddedColumns);
    return paramSet;
  }

  protected override DBRecordSetParams? OnBeforeRecordsSelect(DBRecordSetParams oldParameters)
  {
    BeforeObjectsCollectionSelectEventArgs args = new BeforeObjectsCollectionSelectEventArgs(this.ObjectTypeID, oldParameters, (IUserSession) this.UserSession);
    (this.EventHelper as EventLogHelper).OnBeforeRecordsSelect((object) this, (BeforeRecordsSelectEventArgs) args);
    return args.NewParameters;
  }

  public virtual DataTable Select(DBRecordSetParams paramSet, bool checkAccess)
  {
    this._CheckAccess = checkAccess;
    if (paramSet.Conditions != null)
    {
      for (int index = 0; index < paramSet.Conditions.Length; ++index)
      {
        if (paramSet.Conditions[index].RelationalOperator == RelationalOperators.ObjectTypeFilter)
        {
          if (this.ObjectTypeID == -1)
          {
            this.ObjectTypeID = Convert.ToInt32(paramSet.Conditions[index].Value);
          }
          else
          {
            int int32 = Convert.ToInt32(paramSet.Conditions[index].Value);
            if (int32 != this.ObjectTypeID)
            {
              int objectTypeParentId;
              for (objectTypeParentId = this.UserSession.DBCache.GetObjectTypeParentID(int32); objectTypeParentId != -1; objectTypeParentId = this.UserSession.DBCache.GetObjectTypeParentID(objectTypeParentId))
              {
                if (objectTypeParentId == this.ObjectTypeID)
                {
                  this.ObjectTypeID = int32;
                  break;
                }
              }
              if (objectTypeParentId == -1)
                throw new KernelExceptionID(sc_13375.ssp_appserver_13384(563161795), (object) this.UserSession.GetObjectType(this.ObjectTypeID).ObjectTypeName, (object) this.UserSession.GetObjectType(int32).ObjectTypeName);
            }
          }
          paramSet.Conditions[index].RelationalOperator = RelationalOperators.NOP;
        }
      }
    }
    this.ConfigureOptimizer();
    paramSet = this.PrepareParams(paramSet);
    DataTable source = this.Select(ref paramSet);
    try
    {
      ElementStatusesService.PrepareElementStatusesColumn(ref source, DBRecordSet.GetElementsStatusesColumnIdx(ref paramSet));
      this.FillCaptionID(source);
      if (source != null)
      {
        if (this._visibilityFiltration)
          DBRecordSet.ObjectsVisibilityFiltration.Filtrate(this.UserSession, source, (DBRecordSet) this, DBRecordSet.AttributeColumnIndex(paramSet, (object) DBRecordSet._attrVisibility, AttributeSourceTypes.Object), DBRecordSet.AttributeColumnIndex(paramSet, (object) ObligatoryObjectAttributes.F_OWNER_ID, AttributeSourceTypes.Object));
      }
    }
    finally
    {
      if (this._AddedColumns.Count > 0 && source != null && source.Columns.Count >= this._AddedColumns.Count)
      {
        for (int index = this._AddedColumns.Count - 1; index >= 0; --index)
          source.Columns.RemoveAt(this._AddedColumns[index]);
      }
    }
    return source;
  }

  private IDBObject CreateObjectInternal(
    long id,
    int objectType,
    IDBObject prototype,
    Guid versionGuid)
  {
    if (versionGuid == Guid.Empty)
      versionGuid = Guid.NewGuid();
    this.UserSession.StartTransaction();
    try
    {
      (this.EventHelper as EventLogHelper).OnBeginCreateObject(versionGuid, objectType, prototype, (IUserSession) this.UserSession);
      IDBObject objectInternal = this.CreateObject(id, objectType, prototype, versionGuid);
      if (objectInternal.ParentVersionID != -1L)
      {
        using (UserSessionContext.CaptureSession(this.UserSession.SessionGUID))
          this._synchronicReleaseCreateVersionModifier.Apply(objectInternal, prototype);
      }
      (this.EventHelper as EventLogHelper).OnEndCreateObject(objectInternal, prototype, (IUserSession) this.UserSession);
      this.UserSession.Commit();
      return objectInternal;
    }
    catch
    {
      try
      {
        (this.EventHelper as EventLogHelper).OnCancelCreateObject(versionGuid, objectType, prototype, (IUserSession) this.UserSession);
      }
      finally
      {
        this.UserSession.Rollback();
      }
      throw;
    }
  }

  protected virtual EditingContextMode GetEditingContextMode()
  {
    return this.UserSession.EditingContextMode;
  }

  protected virtual IDBObject CreateObject(
    long id,
    int objectType,
    IDBObject prototype,
    Guid versionGuid)
  {
    if (versionGuid == Guid.Empty)
      throw new ArgumentException("Значение глобального идентификатора версии объекта не задано.", nameof (versionGuid));
    if (ServerConsts.CreateObjectLogging)
    {
      this.UserSession.EventLogHelper.AddToTrace(id <= 0L ? $"Создание объекта типа '{this.UserSession.GetObjectType(objectType).ObjectTypeName}'" : $"Создание версии объекта ID={id} типа '{this.UserSession.GetObjectType(objectType).ObjectTypeName}'", "CreateObject.log");
      if (prototype != null)
        this.UserSession.EventLogHelper.AddToTrace($"По прототипу объекта {prototype.NameInMessages}  ObjectID = {(object) prototype.ObjectID}", "CreateObject.log");
      this.UserSession.EventLogHelper.AddToTrace("-----------------------------------------------", "CreateObject.log");
      this.UserSession.EventLogHelper.AddToTrace(Environment.StackTrace, "CreateObject.log");
      this.UserSession.EventLogHelper.AddToTrace("-----------------------------------------------", "CreateObject.log");
    }
    long num1 = 0;
    long num2 = 0;
    long num3 = 0;
    int num4 = 0;
    IDbManager dataManager = this.UserSession.DataManager;
    IDBObjectType objectType1 = this.UserSession.GetObjectType(objectType);
    int firstStep = this.UserSession.GetLifecycleStepCollection(objectType).GetFirstStep();
    (objectType1 as DBObjectType).CheckAccess(ActionType.CreateChildItem);
    if (prototype != null && prototype.ID == id && (prototype as IDBLifecycleLevel).LevelID == this.UserSession.IdentHelper.AnnulmentLevelID)
      throw new KernelExceptionID(402);
    this.UserSession.StartTransaction();
    try
    {
      if (objectType1.Versionable == ObjectVersionModes.Abstract)
        throw new KernelExceptionID(sc_13375.ssp_appserver_13385(1423168225), (object) objectType1.ObjectTypeName);
      long currentProjectId = (objectType1.Options & ObjectTypeOptions.CurrentProjectEnabled) != ObjectTypeOptions.CurrentProjectEnabled ? 0L : this.UserSession.CurrentProjectID;
      string new_caption;
      if (prototype != null)
      {
        new_caption = prototype.Caption;
        if (prototype.ID != id)
        {
          if ((objectType1.Options & ObjectTypeOptions.DisablePrototyping) == ObjectTypeOptions.DisablePrototyping)
            throw new KernelExceptionID(sc_13375.ssp_appserver_13386(1520606616), (object) objectType1.ObjectTypeName);
          new_caption = this.CheckPrototypeCaption(prototype, objectType1, new_caption);
        }
        else if (prototype.ObjectModifyMode == ObjectModifyModes.CantModify)
          throw new KernelExceptionID(sc_13375.ssp_appserver_13387(2141236712), (object) prototype.NameInMessages, (object) (prototype as DBObject).LCStepObject.LCName);
      }
      else
        new_caption = string.Empty;
      bool flag = !MetaDataHelper.IsObjectTypeEditingContext(objectType) && MetaDataHelper.MustAppendVersionToEditingContext((IUserSession) this.UserSession, objectType, new Func<EditingContextMode>(this.GetEditingContextMode));
      IDBEditingContextsObject editingContextsObject = flag ? this.UserSession.GetObject(this.UserSession.EditingContextID, false) as IDBEditingContextsObject : (IDBEditingContextsObject) null;
      if (editingContextsObject != null)
      {
        if (prototype != null && editingContextsObject.FindObjectByID(id, true) != null)
        {
          editingContextsObject.ResetCache();
          EditingContextsObjectVersion objectById = editingContextsObject.FindObjectByID(id, true);
          if (objectById != null)
            throw new KernelExceptionID(338, (object) prototype.NameInMessages, (object) prototype.ObjectID, (object) objectById.F_OBJECT_ID).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(prototype.ObjectID), (ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(objectById.F_OBJECT_ID));
        }
        if (!editingContextsObject.SimpleContext & flag)
          num2 = Math.Abs(editingContextsObject.LinkedContextNumber);
      }
      dataManager.ExecuteSpNonQuery("IMS_ADD_OBJECT", dataManager.Parameter("inID", (object) id), dataManager.Parameter("inOBJECT_TYPE", (object) objectType), dataManager.Parameter("inOWNER_ID", (object) this.UserSession.UserID), dataManager.Parameter("inLC_STEP", (object) firstStep), dataManager.Parameter("inGUID", (object) versionGuid), dataManager.Parameter("inOBJECT_VER_TYPE", (object) -1), dataManager.Parameter("inCAPTION", (object) new_caption), dataManager.Parameter("inMODIFY_DATE", (object) null), dataManager.Parameter("inCREATE_DATE", (object) null), dataManager.Parameter("inPROJECT_ID", (object) currentProjectId), dataManager.Parameter("inMODIFICATION_ID", (object) num2), dataManager.Parameter("inSITE_ID", (object) string.Empty), dataManager.Parameter("inCREATOR_ID", (object) this.UserSession.UserID), dataManager.OutputParameter("outOBJECT_ID", (object) num1), dataManager.OutputParameter("outID", (object) num3), dataManager.OutputParameter("outVERSION_ID", (object) num4));
      long int64_1 = Convert.ToInt64(dataManager.GetOutputParameterValue("outOBJECT_ID"));
      long int64_2 = Convert.ToInt64(dataManager.GetOutputParameterValue("outID"));
      if (this.UserSession.DBCache.IsInhertitedFrom(objectType1.ObjectType, this.UserSession.IdentHelper.ProjectsTypeID))
        dataManager.ExecuteNonQuery("UPDATE IMS_OBJECTS SET F_PROJECT_ID = :newID1 WHERE F_OBJECT_ID = :newID", dataManager.Parameter("newID1", (object) Math.Abs(int64_1)), dataManager.Parameter("newID", (object) int64_1));
      if (!this.UserSession.IsSystemSession && this.UserSession.SecurityLevel > 0 && (objectType1.Options & ObjectTypeOptions.MandateAccess) == ObjectTypeOptions.MandateAccess)
        dataManager.ExecuteNonQuery("UPDATE IMS_OBJECTS SET F_ACCESS = :acLevel WHERE F_OBJECT_ID = :newID", dataManager.Parameter("acLevel", (object) this.UserSession.SecurityLevel), dataManager.Parameter("newID", (object) int64_1));
      if (id == 0L)
      {
        Guid guid = Guid.NewGuid();
        dataManager.ExecuteNonQuery("INSERT INTO IMS_GUID_RESOLVE (F_GUID, F_ID, F_CATEGORY_TYPE) VALUES (:guid, :id, :typ)", dataManager.Parameter("guid", (object) guid.ToString()), dataManager.Parameter(nameof (id), (object) int64_2), dataManager.Parameter("typ", (object) 2));
      }
      IDBObject dbObject = this.UserSession.GetObject(int64_1);
      (dbObject as DBAttributable).SetAttributesState(Consts.CreateMode);
      try
      {
        List<IMSAttribute4ObjectType> attribute4ObjectTypeList = MetaDataHelper.GetAttribute4ObjectTypeList(objectType);
        if (prototype != null)
          this.CreateObject_CopyAttributes(prototype, dbObject, id);
        else
          (dbObject as DBObject).InitNewObligatoryAttributes(attribute4ObjectTypeList);
        for (int index = 0; index < attribute4ObjectTypeList.Count; ++index)
        {
          if ((attribute4ObjectTypeList[index].Required == RequiredModes.Auto || attribute4ObjectTypeList[index].Required == RequiredModes.AutoRequired) && (attribute4ObjectTypeList[index].LevelID == 0 || attribute4ObjectTypeList[index].LevelID == (dbObject as IDBLifecycleLevel).LevelID))
            (dbObject.Attributes as DBAttributeCollection).AddAttribute(attribute4ObjectTypeList[index].AttributeID, false, false);
        }
        long num5 = -1;
        if (prototype != null)
        {
          if (id == prototype.ID)
          {
            DBRelationCollection relationCollection = this.UserSession.GetRelationCollection(-1) as DBRelationCollection;
            relationCollection._CheckCreateRules = false;
            num5 = prototype.ObjectID;
            this.CreateObject_CopyVersionRelations(dbObject, prototype, relationCollection);
            (dbObject as DBObject).CopySearchWorkFiles();
          }
          else
          {
            this.CreateObject_CopyObjectRelations(dbObject, prototype);
            int attributeId = this.UserSession.IdentHelper.GetAttributeID("cadd9668-306c-11d8-b4e9-00304f19f545");
            if (MetaDataHelper.GetAttribute4ObjectType(dbObject.ObjectType, attributeId) != null && !prototype.IsCreationMode)
              dbObject.Attributes.AddAttribute(attributeId, false, new object[1]
              {
                (object) Math.Abs(prototype.ObjectID)
              });
          }
        }
        (dbObject as DBObject)._ParentVersionID = num5 == -1L ? num5 : Math.Abs(num5);
        (dbObject as DBObject).DoAfterCreate();
        (this.EventHelper as EventLogHelper).OnAfterCreateObject(dbObject, prototype, (IUserSession) this.UserSession);
      }
      finally
      {
        (dbObject as DBAttributable).ClearAttributesState(Consts.CreateMode);
      }
      this.UserSession.AddToModificationsHistory((CategoryValue) new ModificationEvent(1, dbObject.ObjectID, ActionType.Create, dbObject.ObjectType));
      this.UserSession.AddToCreationLog(1, dbObject.ObjectID);
      this.UserSession.Commit();
      return dbObject;
    }
    catch
    {
      this.UserSession.Rollback();
      throw;
    }
  }

  protected virtual string CheckPrototypeCaption(
    IDBObject prototype,
    IDBObjectType objType,
    string new_caption)
  {
    if (objType.CaptionAttribute > 0)
    {
      IDBAttribute byId = prototype.Attributes.FindByID(objType.CaptionAttribute);
      if (byId != null && (byId.AttributeType.Options & AttributeOptions.DontCopyPrototypeValue) == AttributeOptions.DontCopyPrototypeValue)
        new_caption = string.Empty;
    }
    return new_caption;
  }

  protected virtual void CreateObject_CopyAttributes(
    IDBObject prototype,
    IDBObject newobject,
    long id)
  {
    if (this._DisabledPrototypeAttributes != null)
      (newobject.Attributes as DBAttributeCollection)._SkipAttributesList = this._DisabledPrototypeAttributes;
    if (prototype.ObjectType == newobject.ObjectType)
    {
      if (id == prototype.ID)
      {
        if ((prototype.Attributes as DBAttributeCollection).QuickAddAttributes(newobject.ObjectID, false, true, false))
          return;
        newobject.Attributes.Assign(prototype.Attributes, 1024 /*0x0400*/);
      }
      else
        newobject.Attributes.Assign(prototype.Attributes, Consts.CreateMode);
    }
    else
      newobject.Attributes.AssignPossibleAttributes(prototype.Attributes, Consts.CreateMode);
  }

  protected virtual void CreateObject_CopyVersionRelations(
    IDBObject newObject,
    IDBObject prototype,
    DBRelationCollection rels)
  {
    if (newObject == null || prototype == null || rels == null)
      return;
    rels._AssignMode = 1024 /*0x0400*/;
    IDbManager dataManager = this.UserSession.DataManager;
    DataTable tbl = dataManager.ExecuteDataTable("SELECT * FROM IMS_RELATIONS R WHERE R.F_PROJ_ID = :p1 AND EXISTS(SELECT O.F_OBJECT_ID FROM IMS_OBJECTS O WHERE O.F_ID = R.F_PART_ID AND O.F_OBJECT_VER_TYPE > -1)", dataManager.Parameter("p1", (object) prototype.ObjectID));
    this.UserSession.GetRelationsApplicabilityCollection();
    for (int index = 0; index < tbl.Rows.Count; ++index)
    {
      IDBRelation relation = this.UserSession.GetRelation(tbl, index);
      IDBRelationsApplicability applicability = (relation as DBRelation).Applicability;
      if (applicability != null && (applicability.Options & ApplicabilityOptions.DisableCopy2Version) == ApplicabilityOptions.None)
      {
        NewRelationProperties props = new NewRelationProperties(relation, newObject.ObjectID, relation.GUID);
        props.BeginDate = DateTime.MinValue;
        rels.RelationTypeID = relation.RelationType;
        this.CreateObject_CopyVersionRelations(newObject, prototype, rels, props);
      }
    }
    dataManager.ExecuteNonQuery("INSERT INTO IMS_VERSIONS_TREE (F_PARENT_ID, F_OBJECT_ID) VALUES (:p1, :p2)", dataManager.Parameter("p1", (object) Math.Abs(prototype.ObjectID)), dataManager.Parameter("p2", (object) -newObject.ObjectID));
  }

  protected virtual IDBRelation CreateObject_CopyVersionRelations(
    IDBObject newObject,
    IDBObject prototype,
    DBRelationCollection rels,
    NewRelationProperties props)
  {
    return rels?.Create(props);
  }

  protected virtual void CreateObject_CopyObjectRelations(IDBObject newObject, IDBObject prototype)
  {
    if (newObject == null || prototype == null)
      return;
    DataRow[] dataRowArray = this.UserSession.GetRelationsApplicabilityCollection().GetApplicabilitiesList(-1, -1, newObject.ObjectType).Select("F_CLONE_RELATIONS <> 0", "F_RELATION_TYPE");
    int relTypeID = -1;
    List<int> objTypeIDs = new List<int>();
    foreach (DataRow dataRow in dataRowArray)
    {
      int int32 = Convert.ToInt32(dataRow["F_RELATION_TYPE"]);
      if (relTypeID != int32)
      {
        if (relTypeID != -1)
          this.CreateObject_CopyObjectRelations(relTypeID, objTypeIDs, newObject, prototype);
        relTypeID = int32;
        objTypeIDs.Clear();
      }
      objTypeIDs.Add(Convert.ToInt32(dataRow["F_OBJECT_TYPE"]));
    }
    if (relTypeID == -1)
      return;
    this.CreateObject_CopyObjectRelations(relTypeID, objTypeIDs, newObject, prototype);
  }

  public void CopyObjectRelations(
    int relTypeID,
    List<int> objTypeIDs,
    IDBObject newObject,
    IDBObject prototype)
  {
    this.CreateObject_CopyObjectRelations(relTypeID, objTypeIDs, newObject, prototype);
  }

  public void SetDisabledPrototypeRelationTypes(List<int> relationTypes)
  {
    this._DisabledProtoRelationTypes = relationTypes;
  }

  public void SetDisabledPrototypeAttributes(List<int> attributes)
  {
    this._DisabledPrototypeAttributes = attributes;
  }

  protected virtual void CreateObject_CopyObjectRelations(
    int relTypeID,
    List<int> objTypeIDs,
    IDBObject newObject,
    IDBObject prototype)
  {
    if (newObject == null || prototype == null || this._DisabledProtoRelationTypes != null && this._DisabledProtoRelationTypes.IndexOf(relTypeID) >= 0 || relTypeID == -1 || objTypeIDs == null || objTypeIDs.Count == 0)
      return;
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(-7, RelationalOperators.In, (object) objTypeIDs.ToArray(), LogicalOperators.NONE, 0, false)
    }, new object[7]
    {
      (object) ObligatoryObjectAttributes.F_PRJLINK_ID,
      (object) ObligatoryObjectAttributes.F_PROJ_ID,
      (object) ObligatoryObjectAttributes.F_PART_ID,
      (object) ObligatoryObjectAttributes.F_RELATION_TYPE,
      (object) ObligatoryObjectAttributes.F_CREATE_DATE,
      (object) ObligatoryObjectAttributes.F_PRJ_GUID,
      (object) ObligatoryObjectAttributes.F_OBJECT_TYPE
    });
    paramSet.ColumnNames = new ColumnNameMapping[7]
    {
      ColumnNameMapping.FieldName,
      ColumnNameMapping.FieldName,
      ColumnNameMapping.FieldName,
      ColumnNameMapping.FieldName,
      ColumnNameMapping.FieldName,
      ColumnNameMapping.FieldName,
      ColumnNameMapping.FieldName
    };
    DBRelationCollection relationCollection = this.UserSession.GetRelationCollection(relTypeID) as DBRelationCollection;
    relationCollection.LocalTypesMode = true;
    relationCollection._NeedCheckCycleLinks = false;
    relationCollection._CheckCreateRules = false;
    relationCollection._AssignMode = 8192 /*0x2000*/;
    IDBRelationsApplicabilityCollection applicabilityCollection = this.UserSession.GetRelationsApplicabilityCollection();
    DataTable tbl = relationCollection.ConsistFrom(paramSet, prototype.ObjectID);
    for (int index = 0; index < tbl.Rows.Count; ++index)
    {
      DataRow row = tbl.Rows[index];
      int int32 = Convert.ToInt32(row[6]);
      if (objTypeIDs.Contains(int32) || applicabilityCollection.GetApplicability(relationCollection.RelationTypeID, int32, newObject.ObjectType).CloneChildRelations)
      {
        IDBRelation relation = this.UserSession.GetRelation(tbl, index);
        this.CheckRelationAttributes(relationCollection.Create(new NewRelationProperties(relation.RelationID, newObject.ObjectID, Convert.ToInt64(row[2]), DateTime.MinValue)
        {
          PrototypeRelation = relation
        }), relation);
      }
    }
  }

  protected virtual void CheckRelationAttributes(IDBRelation newrel, IDBRelation oldrel)
  {
  }

  public IDBObject Create(int objectType)
  {
    return this.CreateObjectInternal(0L, objectType, (IDBObject) null, Guid.NewGuid());
  }

  public IDBObject Create(Guid versionGuid)
  {
    return this.CreateObjectInternal(0L, this.ObjectTypeID, (IDBObject) null, versionGuid);
  }

  public IDBObject Create()
  {
    return this.ObjectTypeID >= 0 ? this.Create(this.ObjectTypeID) : throw new KernelException(LocalizationHolder.rm.GetString(sc_13375.ssp_appserver_13389()));
  }

  public IDBObject Create(IDBObject prototype)
  {
    return this.CreateObjectInternal(0L, this.ObjectTypeID <= -1 ? prototype.ObjectType : this.ObjectTypeID, prototype, Guid.NewGuid());
  }

  public IDBObject Create(long prototypeID) => this.Create(this.UserSession.GetObject(prototypeID));

  public IDBObject CreateVersion(long objectID)
  {
    IDBObject prototype = this.UserSession.GetObject(objectID);
    if (prototype.SiteID.Length > 0 && (prototype as DBObject).ReadonlyPublishedObject(false))
      throw new KernelExceptionID(sc_13375.ssp_appserver_13390(1225815094), (object) prototype.NameInMessages);
    int num = this.ObjectTypeID <= 0 ? prototype.ObjectType : this.ObjectTypeID;
    IDBObjectType objectType = this.UserSession.GetObjectType(num);
    if (objectType.Versionable == ObjectVersionModes.SingleVersion)
      throw new KernelExceptionID(sc_13375.ssp_appserver_13391(1299459084), (object) objectType.ObjectTypeName);
    return this.CreateObjectInternal(prototype.ID, num, prototype, Guid.NewGuid());
  }

  private long[] GetCreatedObjects(IDBObject firstObject, int firstIndex)
  {
    CategoryValue[] creationLog = this.UserSession.GetCreationLog();
    List<long> longList = new List<long>();
    longList.Add(firstObject.ObjectID);
    for (int index = firstIndex; index < creationLog.Length; ++index)
    {
      if (creationLog[index].CategoryType == 1 && creationLog[index].ActionID == ActionType.Create && creationLog[index].CategoryID != firstObject.ObjectID)
        longList.Add(creationLog[index].CategoryID);
    }
    return longList.ToArray();
  }

  public long[] CreateVersionEx(long objectID)
  {
    int creationLogLength = this.UserSession.GetCreationLogLength();
    bool flag = !this.UserSession.InCreationLogMode;
    if (flag)
      this.UserSession.StartCreationLog();
    try
    {
      long[] createdObjects = this.GetCreatedObjects(this.CreateVersion(objectID), creationLogLength);
      if (flag)
        this.UserSession.CommitCreationLog();
      return createdObjects;
    }
    catch
    {
      if (flag)
        this.UserSession.RollBackCreationLog();
      throw;
    }
  }

  public long[] CreateEx(long prototypeID)
  {
    int creationLogLength = this.UserSession.GetCreationLogLength();
    bool flag = !this.UserSession.InCreationLogMode;
    if (flag)
      this.UserSession.StartCreationLog();
    try
    {
      long[] createdObjects = this.GetCreatedObjects(this.Create(prototypeID), creationLogLength);
      if (flag)
        this.UserSession.CommitCreationLog();
      return createdObjects;
    }
    catch
    {
      if (flag)
        this.UserSession.RollBackCreationLog();
      throw;
    }
  }

  public long[] CreateEx(int objectType)
  {
    int creationLogLength = this.UserSession.GetCreationLogLength();
    bool flag = !this.UserSession.InCreationLogMode;
    if (flag)
      this.UserSession.StartCreationLog();
    try
    {
      long[] createdObjects = this.GetCreatedObjects(this.Create(objectType), creationLogLength);
      if (flag)
        this.UserSession.CommitCreationLog();
      return createdObjects;
    }
    catch
    {
      if (flag)
        this.UserSession.RollBackCreationLog();
      throw;
    }
  }

  public long[] CreateEx(Guid versionGuid)
  {
    int creationLogLength = this.UserSession.GetCreationLogLength();
    bool flag = !this.UserSession.InCreationLogMode;
    if (flag)
      this.UserSession.StartCreationLog();
    try
    {
      long[] createdObjects = this.GetCreatedObjects(this.Create(versionGuid), creationLogLength);
      if (flag)
        this.UserSession.CommitCreationLog();
      return createdObjects;
    }
    catch
    {
      if (flag)
        this.UserSession.RollBackCreationLog();
      throw;
    }
  }

  public long[] CreateEx()
  {
    int creationLogLength = this.UserSession.GetCreationLogLength();
    bool flag = !this.UserSession.InCreationLogMode;
    if (flag)
      this.UserSession.StartCreationLog();
    try
    {
      long[] createdObjects = this.GetCreatedObjects(this.Create(), creationLogLength);
      if (flag)
        this.UserSession.CommitCreationLog();
      return createdObjects;
    }
    catch
    {
      if (flag)
        this.UserSession.RollBackCreationLog();
      throw;
    }
  }

  protected override object GetElement(long id) => (object) this.UserSession.GetObject(id);

  protected override bool AddDeletedObjectsFilter
  {
    get
    {
      bool deletedObjectsFilter = base.AddDeletedObjectsFilter;
      if (deletedObjectsFilter && this.ObjectTypeID > -1)
      {
        IDBObjectType objectType = this.UserSession.GetObjectType(this.ObjectTypeID);
        if (objectType.IsLocalType && objectType.LifetimeReserve == 0)
          deletedObjectsFilter = false;
      }
      return deletedObjectsFilter;
    }
  }

  public override bool LocalTypesMode
  {
    set
    {
      base.LocalTypesMode = !(this.ObjectTypeID >= 0 & value) ? value : throw new KernelExceptionID(456);
      if (!value)
        return;
      this.UserSession.QueryBuilder.SystemTableName = "IMS_OBJECTS";
    }
  }

  public bool TrashMode
  {
    get => this._TrashMode;
    set => this._TrashMode = value;
  }

  public override DataTable GetAllValues(int attributeID)
  {
    IDBAttributeType attributeType = this.UserSession.GetAttributeType(attributeID);
    (attributeType as IDBSecurity).CheckAccess(ActionType.List);
    string str = string.Empty;
    if (this.ObjectTypeID >= 0 && !this.UserSession.GetObjectType(this.ObjectTypeID).IsLocalType)
    {
      List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(this.ObjectTypeID);
      StringBuilder stringBuilder = new StringBuilder(200);
      for (int index = 0; index < childrenIdRecursive.Count; ++index)
        stringBuilder.AppendFormat("{0},", (object) childrenIdRecursive[index]);
      --stringBuilder.Length;
      str = $" AND EXISTS(SELECT O.F_OBJECT_ID FROM IMS_OBJECTS O WHERE O.F_OBJECT_ID = A.F_OBJECT_ID AND O.F_OBJECT_TYPE IN ({stringBuilder.ToString()}))";
    }
    return this.UserSession.DataManager.ExecuteDataTable($"SELECT A.F_OBJECT_ID, A.F_INLIST_ID, A.{attributeType.ValueFieldName} FROM {this._DBAttributesTableName} A WHERE A.F_ATTRIBUTE_ID = :attrID{str} ORDER BY A.F_OBJECT_ID, A.F_INLIST_ID", this.UserSession.DataManager.Parameter("attrID", (object) attributeID));
  }

  public DataTable GetAttributeValues(int attrID, bool allFields)
  {
    string str = string.Empty;
    if (this.ObjectTypeID == -1)
      throw new KernelException("Вызов функции GetAttributeValues для нетипизированной коллекции объектов.");
    if (!this.UserSession.GetObjectType(this.ObjectTypeID).IsLocalType)
      str = $" AND EXISTS(SELECT * FROM IMS_OBJECTS WHERE IMS_OBJECTS.F_OBJECT_ID = {this.DBAttributesTableName}.F_OBJECT_ID AND IMS_OBJECTS.F_OBJECT_TYPE = {this.ObjectTypeID})";
    IDbManager dataManager = this.UserSession.DataManager;
    IDBAttributeType attributeType = this.UserSession.GetAttributeType(attrID);
    (attributeType as IDBSecurity).CheckAccess(ActionType.List);
    string commandText = $"SELECT {(!allFields ? $"{this.DBKeyField}, F_ATTRIBUTE_ID, F_INLIST_ID, {attributeType.ValueFieldName}" : "*")} FROM {this.DBAttributesTableName} WHERE F_ATTRIBUTE_ID = {attrID}{str}";
    return dataManager.ExecuteDataTable(commandText);
  }

  public DataTable GetAttributeValues(Guid attrGuid, bool allFields)
  {
    return this.GetAttributeValues(MetaDataHelper.GetAttributeID((object) attrGuid), allFields);
  }
}
