// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBObjectTypeCollection
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel;

internal class DBObjectTypeCollection : 
  DBCollection,
  IDBObjectTypeCollection,
  IDBCollection,
  IDBSecurity
{
  private int _ParentTypeID = -1;
  private static Dictionary<ActionType, bool> metadataActions = new Dictionary<ActionType, bool>(10);

  public DBObjectTypeCollection(UserSession uSession, int parentTypeID, bool filterRecs)
    : base(uSession, filterRecs)
  {
    this._ParentTypeID = parentTypeID;
    this.ParentID = (object) parentTypeID;
    this._DBTableName = "IMS_OBJECT_TYPES";
    this._DBKeyField = "F_OBJECT_TYPE";
    this._AreaSupport = filterRecs;
    this._LanguageSupport = false;
    this._SelectFromCache = true;
    this.InitSecurityOptions(4, 0L);
  }

  static DBObjectTypeCollection()
  {
    DBObjectTypeCollection.metadataActions.Add(ActionType.GetAccess, false);
    DBObjectTypeCollection.metadataActions.Add(ActionType.SetAccess, false);
    DBObjectTypeCollection.metadataActions.Add(ActionType.View, true);
    DBObjectTypeCollection.metadataActions.Add(ActionType.Create, false);
  }

  public override object ParentID
  {
    get => base.ParentID;
    set
    {
      int int32 = Convert.ToInt32(value);
      this._ParentTypeID = int32 <= -1 ? int32 : this.UserSession.GetObjectType(int32, true).ObjectType;
      base.ParentID = (object) this._ParentTypeID;
    }
  }

  public DataTable SelectRecursive(string orderBy, params object[] addInfo)
  {
    if (this._ParentTypeID <= -1)
      throw new KernelExceptionID(sc_13480.ssp_appserver_13481(1173340023));
    DataTable dataTable = this.Select(orderBy, addInfo);
    DataTable toTable = dataTable.Copy();
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      DataRow[] fromRows = this.UserSession.GetObjectTypeCollection(Convert.ToInt32(row["F_OBJECT_TYPE"]), this._Filtering).SelectRecursive(orderBy, addInfo).Select();
      SqlHelper.AssignRows(toTable, (IEnumerable<DataRow>) fromRows);
    }
    return toTable;
  }

  public override DataTable Select(string orderBy, params object[] addInfo)
  {
    return ObjectTypesCacheHelper.AddInfoToTable(this.UserSession.CacheDataSet, base.Select(orderBy, addInfo), addInfo);
  }

  public DataTable GetUsedByAttribute(int attributeID)
  {
    DataTable usedByAttribute = ObjectTypesCacheHelper.GetUsedByAttribute(this.UserSession.DBCache.GetTable("IMS_ATTR4OBJ_TYPES"), this.UserSession.DBCache.GetTable("IMS_OBJECT_TYPES"), attributeID);
    this.FillCaptions(usedByAttribute);
    return usedByAttribute;
  }

  protected override void InitSecurityOptions(int aCategoryType, long aCategoryID)
  {
    this.InitStaticSecurityOptions(aCategoryType, aCategoryID, DBObjectTypeCollection.metadataActions);
  }

  public override string ObjectName => LocalizationHolder.rm.GetString("Kernel_476");

  protected override string GetParentSQL(object parentID)
  {
    if (this._SelectFromCache)
      return ObjectTypesCacheHelper.GetParentSQL(this.UserSession.DBCache.GetTable("IMS_OBJTYPES_TREE"), (int) parentID, (int[]) null);
    if ((int) parentID == -2)
      return "";
    return (int) parentID > -1 ? $"(EXISTS(SELECT * FROM IMS_OBJTYPES_TREE WHERE IMS_OBJTYPES_TREE.F_PARENT_ID = {parentID.ToString()} AND IMS_OBJTYPES_TREE.F_OBJECT_TYPE = IMS_OBJECT_TYPES.F_OBJECT_TYPE))" : "(NOT EXISTS(SELECT * FROM IMS_OBJTYPES_TREE WHERE IMS_OBJTYPES_TREE.F_OBJECT_TYPE = IMS_OBJECT_TYPES.F_OBJECT_TYPE))";
  }

  public int Create(ObjectTypeProperties typeProperties) => this.Create(typeProperties, true);

  public int Create(ObjectTypeProperties typeProperties, bool CreateLCScrema)
  {
    IDbManager dataManager = this.UserSession.DataManager;
    this._LastEventID = this.AddEvent(0L, ActionType.Create, EventlogRecordType.AccessDenied, string.Format(LocalizationHolder.rm.GetString("Kernel_477"), (object) typeProperties.ObjectTypeName));
    this.UserSession.StartTransaction();
    try
    {
      this.CheckAccess(ActionType.Create);
      if (typeProperties.ObjectTypeGuid == Guid.Empty)
        typeProperties.ObjectTypeGuid = Guid.NewGuid();
      if (typeProperties.AreaID != "")
        this.UserSession.GetSubjectAreaCollection().ValidateAriasString(typeProperties.AreaID);
      SqlHelper.ValidateEmptyValue(typeProperties.ObjectInstanceName, LocalizationHolder.rm.GetString("Kernel_478"));
      SqlHelper.ValidateEmptyValue(typeProperties.ObjectTypeName, LocalizationHolder.rm.GetString("Kernel_479"));
      SqlHelper.ValidateFieldLength(LocalizationHolder.rm.GetString("MDShortName"), typeProperties.ObjectTypeShortName.Length, Consts.MaxShortNameLength);
      SqlHelper.ValidateFieldLength(LocalizationHolder.rm.GetString("MDName"), typeProperties.ObjectTypeName.Length, Consts.MaxObjectNameLength);
      SqlHelper.ValidateFieldLength(LocalizationHolder.rm.GetString("MDObjectName"), typeProperties.ObjectInstanceName.Length, Consts.MaxObjectNameLength);
      if (typeProperties.ObjectTypeShortName != string.Empty && this.UserSession.DBCache.GetTable("IMS_OBJECT_TYPES").Select("F_SHORT_NAME = " + SqlHelper.QString(typeProperties.ObjectTypeShortName)).Length != 0)
        throw new KernelExceptionID(sc_13480.ssp_appserver_13482(426719528), (object) typeProperties.ObjectTypeShortName);
      this.UserSession.GetRelationType(typeProperties.DefaultRelation);
      dataManager.ExecuteSpNonQuery("IMS_ADD_OBJECT_TYPE", dataManager.Parameter("inOBJ_TYPE_NAME", (object) typeProperties.ObjectTypeName), dataManager.Parameter("inOBJ_NAME", (object) typeProperties.ObjectInstanceName), dataManager.Parameter("inVERSIONABLE", (object) Convert.ToInt32((object) typeProperties.Versionable)), dataManager.Parameter("inNOTE", (object) typeProperties.Note), dataManager.Parameter("inDEFAULT_RELATION", (object) typeProperties.DefaultRelation), dataManager.Parameter("inGUID", (object) typeProperties.ObjectTypeGuid.ToString()), dataManager.Parameter("inAREA_ID", (object) typeProperties.AreaID), dataManager.Parameter("inSHORT_NAME", (object) typeProperties.ObjectTypeShortName), dataManager.OutputParameter("outOBJECT_TYPE", (object) typeProperties.ObjectType));
      typeProperties.ObjectType = Convert.ToInt32(dataManager.GetOutputParameterValue("outOBJECT_TYPE"));
      DataTable dataTable = dataManager.ExecuteDataTable("SELECT * FROM IMS_OBJECT_TYPES WHERE F_OBJECT_TYPE = " + typeProperties.ObjectType.ToString());
      if (dataTable.Rows.Count != 1)
        throw new KernelException(string.Format(LocalizationHolder.rm.GetString(sc_13480.ssp_appserver_13483()), (object) typeProperties.ObjectType));
      this.UserSession.DBCache.AddRow("IMS_OBJECT_TYPES", dataTable.Rows[0], (IUserSession) this.UserSession);
      IDBObjectType objectType = this.UserSession.GetObjectType(typeProperties.ObjectType);
      (objectType as DBObjectType).SetCreatorAccess();
      if (this._ParentTypeID >= 0)
        objectType.ParentTypeID = this._ParentTypeID;
      objectType.AnyAttributes = typeProperties.AnyAttributes;
      objectType.CaptionAttribute = typeProperties.CaptionAttribute;
      objectType.PublicLC = typeProperties.PublicLCSchema;
      objectType.LifetimeReserve = typeProperties.LifetimeReserve;
      objectType.SchemaID = typeProperties.SchemaID;
      objectType.Options = typeProperties.Options;
      (this.UserSession.DBCache as CacheDataset).ObjecTypeGUIDs[(object) typeProperties.ObjectType] = (object) (objectType as IDBGuid).GUID;
      (objectType as DBObjectType).RebuildView(-1, objectType.IsLocalType, true);
      if ((this.UserSession.DBCache as CacheDataset).AttributesInViewsHash[(object) new Attribute4ID(-1, typeProperties.ObjectType, -1)] == null)
      {
        string[] tables;
        if (objectType.IsLocalType)
          tables = new string[1]
          {
            "IMV_O" + typeProperties.ObjectType.ToString()
          };
        else
          tables = new string[1]{ "IMS_OBJECTS_VIEW" };
        (this.UserSession.DBCache as CacheDataset).AttributesInViewsHash[(object) new Attribute4ID(-1, typeProperties.ObjectType, -1)] = (object) new Attribute4Props(OptimizationModes.Write, tables, AttributeOptions.None);
      }
      (ServerServices.GetService(typeof (IEventLogHelper)) as IEventLogHelper).CloseEvent(this._LastEventID, 0L, (long) typeProperties.ObjectType, string.Format(LocalizationHolder.rm.GetString("Kernel_481"), (object) typeProperties.ObjectTypeName), "", EventlogRecordType.AccessGranted, (IUserSession) this.UserSession);
      (this.EventHelper as EventLogHelper).OnAfterCreateObjectType(objectType, (IUserSession) this.UserSession);
      this.UserSession.Commit();
      return typeProperties.ObjectType;
    }
    catch (Exception ex)
    {
      this.UserSession.Rollback();
      string str = string.Format(LocalizationHolder.rm.GetString("Kernel_482"), (object) typeProperties.ObjectTypeName, (object) ex.Message);
      if (ex.Message.IndexOf("IMS_OBJECT_T_OBJ_TYPE_NAME") >= 0)
        str = string.Format(LocalizationHolder.rm.GetString("Kernel_483"), (object) typeProperties.ObjectTypeName);
      this.CloseEvent(this._LastEventID, EventlogRecordType.Error, str);
      if (!(ex is AccessDeniedException))
        throw new KernelException(str, ex);
      throw;
    }
  }

  internal int Create(DataRow row, int parentID)
  {
    IDbManager dataManager = this.UserSession.DataManager;
    int num = 0;
    SqlHelper.ValidateEmptyValue(row["F_OBJ_NAME"].ToString(), LocalizationHolder.rm.GetString("Kernel_484"));
    SqlHelper.ValidateEmptyValue(row["F_OBJ_TYPE_NAME"].ToString(), LocalizationHolder.rm.GetString("Kernel_485"));
    if (row["F_SHORT_NAME"].ToString() != string.Empty && this.UserSession.DBCache.GetTable("IMS_OBJECT_TYPES").Select("F_SHORT_NAME = " + SqlHelper.QString(row["F_SHORT_NAME"].ToString())).Length != 0)
      throw new KernelExceptionID(sc_13480.ssp_appserver_13484(1888180505), (object) row["F_SHORT_NAME"].ToString());
    dataManager.ExecuteSpNonQuery("IMS_ADD_OBJECT_TYPE", dataManager.Parameter("inOBJ_TYPE_NAME", (object) row["F_OBJ_TYPE_NAME"].ToString()), dataManager.Parameter("inOBJ_NAME", (object) row["F_OBJ_NAME"].ToString()), dataManager.Parameter("inVERSIONABLE", (object) Convert.ToInt32(row["F_VERSIONABLE"])), dataManager.Parameter("inNOTE", (object) row["F_NOTE"].ToString()), dataManager.Parameter("inDEFAULT_RELATION", (object) Convert.ToInt32(row["F_DEFAULT_RELATION"])), dataManager.Parameter("inGUID", (object) row["F_GUID"].ToString()), dataManager.Parameter("inAREA_ID", (object) row["F_AREA_ID"].ToString()), dataManager.Parameter("inSHORT_NAME", (object) row["F_SHORT_NAME"].ToString()), dataManager.OutputParameter("outOBJECT_TYPE", (object) num));
    int int32 = Convert.ToInt32(dataManager.GetOutputParameterValue("outOBJECT_TYPE"));
    if (row["F_ICON"] == DBNull.Value)
      dataManager.ExecuteNonQuery("UPDATE IMS_OBJECT_TYPES SET F_OPTIONS = :opt1, F_DEL_TIME = :deltime, F_PUBLIC_LC = :lcID, F_ANY_ATTRIBUTES = :anyAttr, F_CAPTION_ATTRIBUTE = :captionID, F_SCHEMA_ID = :schemaID, F_ICON = NULL WHERE F_OBJECT_TYPE = :typeID", dataManager.Parameter("opt1", (object) Convert.ToInt32(row["F_OPTIONS"])), dataManager.Parameter("deltime", (object) Convert.ToInt32(row["F_DEL_TIME"])), dataManager.Parameter("lcID", (object) Convert.ToInt32(row["F_PUBLIC_LC"])), dataManager.Parameter("anyAttr", (object) Convert.ToInt32(row["F_ANY_ATTRIBUTES"])), dataManager.Parameter("captionID", (object) Convert.ToInt32(row["F_CAPTION_ATTRIBUTE"])), dataManager.Parameter("schemaID", (object) Convert.ToInt32(row["F_SCHEMA_ID"])), dataManager.Parameter("typeID", (object) int32));
    else
      dataManager.ExecuteNonQuery("UPDATE IMS_OBJECT_TYPES SET F_OPTIONS = :opt1, F_DEL_TIME = :deltime, F_PUBLIC_LC = :lcID, F_ANY_ATTRIBUTES = :anyAttr, F_CAPTION_ATTRIBUTE = :captionID, F_SCHEMA_ID = :schemaID, F_ICON = :iconka WHERE F_OBJECT_TYPE = :typeID", dataManager.Parameter("opt1", (object) Convert.ToInt32(row["F_OPTIONS"])), dataManager.Parameter("deltime", (object) Convert.ToInt32(row["F_DEL_TIME"])), dataManager.Parameter("lcID", (object) Convert.ToInt32(row["F_PUBLIC_LC"])), dataManager.Parameter("anyAttr", (object) Convert.ToInt32(row["F_ANY_ATTRIBUTES"])), dataManager.Parameter("captionID", (object) Convert.ToInt32(row["F_CAPTION_ATTRIBUTE"])), dataManager.Parameter("schemaID", (object) Convert.ToInt32(row["F_SCHEMA_ID"])), dataManager.Parameter("iconka", row["F_ICON"]), dataManager.Parameter("typeID", (object) int32));
    if (parentID > 0)
    {
      dataManager.ExecuteNonQuery("INSERT INTO IMS_OBJTYPES_TREE (F_PARENT_ID, F_OBJECT_TYPE) VALUES (:p1, :p2)", dataManager.Parameter("p1", (object) parentID), dataManager.Parameter("p2", (object) int32));
      dataManager.ExecuteNonQuery("INSERT INTO IMS_ATTR4OBJ_TYPES (F_ATTRIBUTE_ID, F_OBJECT_TYPE, F_PUBLIC, F_REQUIRED, F_VALIDATION_RULE, F_COMPUTED, F_FORMULA, F_UNIQUE, F_LEVEL_ID, F_DEFAULT_VALUE, F_INVIEW, F_CONTENT, F_OPTIONS, F_MASK, F_MASTER_ID, F_SOURCE_ID) SELECT F_ATTRIBUTE_ID, :typeID, 2, F_REQUIRED, F_VALIDATION_RULE, F_COMPUTED, F_FORMULA, F_UNIQUE, F_LEVEL_ID, F_DEFAULT_VALUE, F_INVIEW, F_CONTENT, F_OPTIONS, F_MASK, F_MASTER_ID, F_SOURCE_ID FROM IMS_ATTR4OBJ_TYPES WHERE (F_OBJECT_TYPE = :parentID) AND (F_PUBLIC = 1 OR F_PUBLIC = 2)", dataManager.Parameter("typeID", (object) int32), dataManager.Parameter(nameof (parentID), (object) parentID));
    }
    return int32;
  }

  internal void UpdateAttribute(DataRow row)
  {
    IDbManager dataManager = this.UserSession.DataManager;
    string str = row["F_FORMULA"].ToString();
    row["F_VALIDATION_RULE"].ToString();
    int int32 = Convert.ToInt32(row["F_ATTRIBUTE_ID"]);
    IDbDataParameter dbDataParameter1 = dataManager.Parameter("objType", (object) Convert.ToInt32(row["F_OBJECT_TYPE"]));
    IDbDataParameter dbDataParameter2 = dataManager.Parameter("attrID", (object) int32);
    dataManager.ExecuteNonQuery("UPDATE IMS_ATTR4OBJ_TYPES SET F_PUBLIC = :pub1, F_REQUIRED = :req1, F_VALIDATION_RULE = :vRule, F_COMPUTED = :comput1, F_FORMULA = :formul, F_UNIQUE = :uniq , F_LEVEL_ID = :level1, F_DEFAULT_VALUE = :defVal, F_INVIEW = :inview, F_CONTENT = :cont, F_OPTIONS  = :opt, F_MASK = :mask1, F_MASTER_ID = :master1 , F_SOURCE_ID = :sourc WHERE F_ATTRIBUTE_ID = :attrID AND F_OBJECT_TYPE = :objType", dbDataParameter2, dbDataParameter1, dataManager.Parameter("pub1", (object) Convert.ToInt32(row["F_PUBLIC"])), dataManager.Parameter("req1", (object) Convert.ToInt32(row["F_REQUIRED"])), dataManager.Parameter("vRule", (object) row["F_VALIDATION_RULE"].ToString()), dataManager.Parameter("comput1", (object) Convert.ToInt32(row["F_COMPUTED"])), dataManager.Parameter("formul", (object) str), dataManager.Parameter("uniq", (object) Convert.ToInt32(row["F_UNIQUE"])), dataManager.Parameter("level1", (object) Convert.ToInt32(row["F_LEVEL_ID"])), dataManager.Parameter("defVal", (object) row["F_DEFAULT_VALUE"].ToString()), dataManager.Parameter("inview", (object) Convert.ToInt32(row["F_INVIEW"])), dataManager.Parameter("cont", (object) Convert.ToInt32(row["F_CONTENT"])), dataManager.Parameter("opt", (object) Convert.ToInt32(row["F_OPTIONS"])), dataManager.Parameter("mask1", (object) row["F_MASK"].ToString()), dataManager.Parameter("master1", (object) Convert.ToInt32(row["F_MASTER_ID"])), dataManager.Parameter("sourc", (object) Convert.ToInt32(row["F_SOURCE_ID"])));
    if (!(str == string.Empty))
      return;
    dataManager.ExecuteNonQuery("DELETE FROM IMS_FORMULA_ATTRS WHERE F_OBJECT_TYPE = :objType AND F_ATTRIBUTE_ID = :attrID", dbDataParameter1, dbDataParameter2);
  }

  internal void CreateAttribute(DataRow row)
  {
    IDbManager dataManager = this.UserSession.DataManager;
    string newFormula = row["F_FORMULA"].ToString();
    string str = row["F_VALIDATION_RULE"].ToString();
    int int32 = Convert.ToInt32(row["F_ATTRIBUTE_ID"]);
    IDbDataParameter dbDataParameter1 = dataManager.Parameter("objType", (object) Convert.ToInt32(row["F_OBJECT_TYPE"]));
    IDbDataParameter dbDataParameter2 = dataManager.Parameter("attrID", (object) int32);
    dataManager.ExecuteNonQuery("INSERT INTO IMS_ATTR4OBJ_TYPES (F_ATTRIBUTE_ID, F_OBJECT_TYPE, F_PUBLIC, F_REQUIRED, F_VALIDATION_RULE, F_COMPUTED, F_FORMULA, F_UNIQUE, F_LEVEL_ID, F_DEFAULT_VALUE, F_INVIEW, F_CONTENT, F_OPTIONS, F_MASK, F_MASTER_ID, F_SOURCE_ID) VALUES (:attrID, :objType, :pub1, :req1, :vRule, :comput1, :formul, :uniq, :level1, :defVal, :inview, :cont, :opt, :mask1, :master1, :sourc)", dbDataParameter2, dbDataParameter1, dataManager.Parameter("pub1", (object) Convert.ToInt32(row["F_PUBLIC"])), dataManager.Parameter("req1", (object) Convert.ToInt32(row["F_REQUIRED"])), dataManager.Parameter("vRule", (object) row["F_VALIDATION_RULE"].ToString()), dataManager.Parameter("comput1", (object) Convert.ToInt32(row["F_COMPUTED"])), dataManager.Parameter("formul", (object) newFormula), dataManager.Parameter("uniq", (object) Convert.ToInt32(row["F_UNIQUE"])), dataManager.Parameter("level1", (object) Convert.ToInt32(row["F_LEVEL_ID"])), dataManager.Parameter("defVal", (object) row["F_DEFAULT_VALUE"].ToString()), dataManager.Parameter("inview", (object) Convert.ToInt32(row["F_INVIEW"])), dataManager.Parameter("cont", (object) Convert.ToInt32(row["F_CONTENT"])), dataManager.Parameter("opt", (object) Convert.ToInt32(row["F_OPTIONS"])), dataManager.Parameter("mask1", (object) row["F_MASK"].ToString()), dataManager.Parameter("master1", (object) Convert.ToInt32(row["F_MASTER_ID"])), dataManager.Parameter("sourc", (object) Convert.ToInt32(row["F_SOURCE_ID"])));
    if (newFormula != string.Empty)
    {
      IDbDataParameter dbDataParameter3 = dataManager.Parameter("mode1", (object) Consts.Attribute4Formula);
      ArrayList consistAttrs = new ArrayList();
      (this.UserSession.GetAttributeType(int32) as DBAttributeType).ValidateFormula(newFormula, (ArrayList) null, consistAttrs, Consts.Attribute4Formula);
      IDbDataParameter dbDataParameter4 = dataManager.Parameter("dependAttrID", (object) 0);
      for (int index = 0; index < consistAttrs.Count; ++index)
      {
        dbDataParameter4.Value = (object) Convert.ToInt32(consistAttrs[index]);
        dataManager.ExecuteNonQuery("INSERT INTO IMS_FORMULA_ATTRS (F_OBJECT_TYPE, F_RELATION_TYPE, F_FORMULA_ID, F_ATTRIBUTE_ID, F_MODE_ID) VALUES (:objType, -1, :attrID, :dependAttrID, :mode1)", dbDataParameter1, dbDataParameter2, dbDataParameter4, dbDataParameter3);
      }
    }
    if (!(str != string.Empty))
      return;
    string[] strArray = str.Split(',');
    if (strArray.Length == 0 || !(strArray[0] != string.Empty))
      return;
    IDbDataParameter dbDataParameter5 = dataManager.Parameter("mode1", (object) Consts.Attribute4ValidationRule);
    ArrayList consistAttrs1 = new ArrayList();
    (this.UserSession.GetAttributeType(int32) as DBAttributeType).ValidateFormula(strArray[0], (ArrayList) null, consistAttrs1, Consts.Attribute4ValidationRule);
    IDbDataParameter dbDataParameter6 = dataManager.Parameter("dependAttrID", (object) 0);
    for (int index = 0; index < consistAttrs1.Count; ++index)
    {
      dbDataParameter6.Value = (object) Convert.ToInt32(consistAttrs1[index]);
      dataManager.ExecuteNonQuery("INSERT INTO IMS_FORMULA_ATTRS (F_OBJECT_TYPE, F_RELATION_TYPE, F_FORMULA_ID, F_ATTRIBUTE_ID, F_MODE_ID) VALUES (:objType, -1, :attrID, :dependAttrID, :mode1)", dbDataParameter1, dbDataParameter2, dbDataParameter6, dbDataParameter5);
    }
  }

  internal void CommitTypesCreation(int[] objectTypes)
  {
    this.UserSession.DBCache.ReloadTables((IUserSession) this.UserSession, this.UserSession.DataManager, "IMS_OBJTYPES_TREE", "IMS_OBJECT_TYPES", "IMS_ATTR4OBJ_TYPES", "IMS_FORMULA_ATTRS");
    UserSession userSession = this.UserSession.Clone(nameof (CommitTypesCreation)) as UserSession;
    try
    {
      for (int index = 0; index < objectTypes.Length; ++index)
        (userSession.GetObjectType(objectTypes[index]) as DBObjectType).RebuildViewWithoutData();
    }
    finally
    {
      userSession.Logout(nameof (CommitTypesCreation));
    }
  }

  public DataTable GetTypesHierarchy() => this.UserSession.DBCache.GetTable("IMS_OBJTYPES_TREE");

  public bool CanViewObjects()
  {
    return (int) this.ParentID > 0 ? (this.UserSession.GetObjectType((int) this.ParentID) as IDBSecurity).CheckAccess(ActionType.View, true, false) : this.CheckAccess(ActionType.View, true, false);
  }
}
