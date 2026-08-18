// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBAttributeTypeCollection
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel;

internal class DBAttributeTypeCollection : 
  BasicAttributeTypeCollection,
  IDBAttributeTypeCollection,
  IDBCollection,
  IDBSecurity
{
  private static Dictionary<ActionType, bool> metadataActions = new Dictionary<ActionType, bool>(4);

  public DBAttributeTypeCollection(UserSession uSession, bool filterRecs)
    : base(uSession, filterRecs)
  {
    this._DBTableName = "IMS_ATTRIBUTES";
    this._DBKeyField = "F_ATTRIBUTE_ID";
    this._AreaSupport = filterRecs;
    this._LanguageSupport = filterRecs;
    this.InitSecurityOptions(3, 0L);
    this.ParentID = (object) 0;
  }

  static DBAttributeTypeCollection()
  {
    DBAttributeTypeCollection.metadataActions.Add(ActionType.GetAccess, false);
    DBAttributeTypeCollection.metadataActions.Add(ActionType.SetAccess, false);
    DBAttributeTypeCollection.metadataActions.Add(ActionType.Create, false);
    DBAttributeTypeCollection.metadataActions.Add(ActionType.Delete, false);
  }

  protected override void InitSecurityOptions(int aCategoryType, long aCategoryID)
  {
    this.InitStaticSecurityOptions(aCategoryType, aCategoryID, DBAttributeTypeCollection.metadataActions);
  }

  public override bool AnyAttributes => true;

  public override AttributeSourceTypes CollectionSourceType => AttributeSourceTypes.Auto;

  public override string ObjectName => LocalizationHolder.rm.GetString("Kernel_134");

  protected override string GetParentSQL(object parentID)
  {
    return AttributeCacheHelper.GetAttributesForParentSQL(this.UserSession.DBCache.GetTable("IMS_ATTR_IN_GROUPS"), parentID);
  }

  protected override IDBAttributeType GetAttributeType4(int attrID, bool failIfNotFound)
  {
    return this.UserSession.GetAttributeType(attrID, failIfNotFound);
  }

  public override object ParentID
  {
    get => base.ParentID;
    set
    {
      if (this.ParentID == value)
        return;
      this._SelectFromCache = true;
      if (Convert.ToInt32(value) > 0 && this.UserSession.DBCache.GetTable("IMS_ATTR_GROUPS").Rows.Find(value) == null)
        throw new KernelException(LocalizationHolder.rm.GetString(sc_12431.ssp_appserver_12432()) + value.ToString());
      base.ParentID = value;
    }
  }

  internal void CommitFastCreation()
  {
    this.UserSession.DBCache.ReloadTables((IUserSession) this.UserSession, this.UserSession.DataManager, "IMS_ATTRIBUTES", "IMS_POSSIBLE_VALUES", "IMS_FORMULA_ATTRS", "IMS_ATTR_IN_GROUPS");
  }

  internal int CreateFast(AttributeTypeProperties attrProperties)
  {
    int num1 = 0;
    IDbManager dataManager = this.UserSession.DataManager;
    this.UserSession.StartTransaction();
    try
    {
      SqlHelper.ValidateEmptyValue(attrProperties.Name, LocalizationHolder.rm.GetString("Kernel_136"));
      if (this.UserSession.DBCache.GetTable("IMS_ATTRIBUTES").Select("F_NAME = " + SqlHelper.QString(attrProperties.Name)).Length != 0)
        throw new KernelException(sc_12431.ssp_appserver_12433());
      dataManager.ExecuteSpNonQuery(sc_12431.ssp_appserver_12434(), dataManager.Parameter("inNAME", (object) attrProperties.Name), dataManager.Parameter("inSHORT_NAME", (object) attrProperties.ShortName), dataManager.Parameter("inALIAS", (object) attrProperties.Alias), dataManager.Parameter("inNOTE", (object) attrProperties.Note), dataManager.Parameter("inATTRIBUTE_TYPE", (object) Convert.ToInt32((object) attrProperties.FieldType)), dataManager.Parameter("inDEFAULT_VALUE", attrProperties.DefaultValue), dataManager.Parameter("inMULTIPLE_VALUED", (object) Convert.ToInt32((object) attrProperties.MultiValueMode)), dataManager.Parameter("inCOMPUTED", (object) Convert.ToInt32((object) attrProperties.Computed)), dataManager.Parameter("inSIZE_TYPE", (object) attrProperties.SizeType), dataManager.Parameter("inLEVEL_ID", (object) attrProperties.LevelID), dataManager.Parameter("inFORMULA", (object) attrProperties.Formula), dataManager.Parameter("inLANGUAGE_ID", (object) attrProperties.LanguageID), dataManager.Parameter("inGUID", (object) attrProperties.AttributeGuid.ToString()), dataManager.Parameter("inAREA_ID", (object) attrProperties.AreaID), dataManager.OutputParameter("outATTRIBUTE_ID", (object) num1));
      int int32 = Convert.ToInt32(dataManager.GetOutputParameterValue("outATTRIBUTE_ID"));
      IDbDataParameter dbDataParameter1 = dataManager.Parameter("attrID", (object) int32);
      string valueFieldName = "F_STRING_VALUE";
      string possibleValueFieldName = "F_STRING_VALUE";
      string empty = string.Empty;
      List<FieldTypes> convertList = new List<FieldTypes>();
      RelationalOperators[] enabledOperators = (RelationalOperators[]) null;
      bool computableAttribute = false;
      AttributeCacheHelper.GetAttributeTypeValues(attrProperties.FieldType, int32, ref valueFieldName, ref empty, ref convertList, ref enabledOperators, ref computableAttribute, ref possibleValueFieldName);
      if (attrProperties.PossibleValues != null)
      {
        string commandText = string.Format(sc_12431.ssp_appserver_12435(), (object) possibleValueFieldName, (object) -1, (object) -1);
        int num2 = 0;
        IDbDataParameter dbDataParameter2 = dataManager.Parameter("id", (object) num2);
        foreach (DataRow row in (InternalDataCollectionBase) attrProperties.PossibleValues.Rows)
        {
          dbDataParameter2.Value = (object) num2++;
          dataManager.ExecuteNonQuery(commandText, dbDataParameter1, dbDataParameter2, dataManager.Parameter("val", row[1]), dataManager.Parameter("descr", row[2]));
        }
      }
      dataManager.ExecuteNonQuery("UPDATE IMS_ATTRIBUTES SET F_UNIQUE = :val_uni, F_CONTENT = :val_con, F_MASK = :val_mask, F_OPTIONS = :val_opt, F_MASTER_ID = :val_master, F_SOURCE_ID = :val_source " + sc_12431.ssp_appserver_12436(), dataManager.Parameter("val_uni", (object) Convert.ToInt32((object) attrProperties.Unique)), dataManager.Parameter("val_con", (object) Convert.ToInt32(attrProperties.IsContent ? 1 : 0)), dataManager.Parameter("val_mask", (object) attrProperties.Mask), dataManager.Parameter("val_opt", (object) Convert.ToInt32((object) attrProperties.Options)), dataManager.Parameter("val_master", (object) attrProperties.MasterAttributeID), dataManager.Parameter("val_source", (object) attrProperties.SourceAttributeID), dbDataParameter1);
      if (attrProperties.Formula != string.Empty)
      {
        IDbDataParameter dbDataParameter3 = dataManager.Parameter("mode1", (object) Consts.Attribute4Formula);
        ArrayList consistAttrs = new ArrayList();
        (this.UserSession.GetAttributeType(int32) as DBAttributeType).ValidateFormula(attrProperties.Formula, (ArrayList) null, consistAttrs, Consts.Attribute4Formula);
        IDbDataParameter dbDataParameter4 = dataManager.Parameter("dependAttrID", (object) 0);
        for (int index = 0; index < consistAttrs.Count; ++index)
        {
          dbDataParameter4.Value = (object) Convert.ToInt32(consistAttrs[index]);
          dataManager.ExecuteNonQuery("INSERT INTO IMS_FORMULA_ATTRS (F_OBJECT_TYPE, F_RELATION_TYPE, F_FORMULA_ID, F_ATTRIBUTE_ID, F_MODE_ID) VALUES (-1, -1, :attrID, :dependAttrID, :mode1)", dbDataParameter1, dbDataParameter4, dbDataParameter3);
        }
      }
      DataTable table = this.UserSession.DBCache.GetTable("IMS_ATTRIBUTES");
      DataRow row1 = table.NewRow();
      row1["F_ATTRIBUTE_ID"] = (object) int32;
      row1["F_NAME"] = (object) attrProperties.Name;
      row1["F_SHORT_NAME"] = (object) attrProperties.ShortName;
      row1["F_NOTE"] = (object) attrProperties.Note;
      row1["F_ALIAS"] = (object) attrProperties.Alias;
      row1["F_ATTRIBUTE_TYPE"] = (object) (int) attrProperties.FieldType;
      row1["F_DEFAULT_VALUE"] = attrProperties.DefaultValue;
      row1["F_MULTIPLE_VALUED"] = (object) (int) attrProperties.MultiValueMode;
      row1["F_COMPUTED"] = (object) (int) attrProperties.Computed;
      row1["F_SIZE_TYPE"] = (object) attrProperties.SizeType;
      row1["F_LEVEL_ID"] = (object) attrProperties.LevelID;
      row1["F_FORMULA"] = (object) attrProperties.Formula;
      row1["F_LANGUAGE_ID"] = (object) attrProperties.LanguageID;
      row1["F_GUID"] = (object) attrProperties.AttributeGuid.ToString();
      row1["F_AREA_ID"] = (object) attrProperties.AreaID;
      row1["F_UNIQUE"] = (object) (int) attrProperties.Unique;
      row1["F_INVIEW"] = (object) (int) attrProperties.OptimizationMode;
      row1["F_CONTENT"] = (object) (attrProperties.IsContent ? 1 : 0);
      row1["F_MASK"] = (object) attrProperties.Mask;
      row1["F_OPTIONS"] = (object) (int) attrProperties.Options;
      row1["F_MASTER_ID"] = (object) attrProperties.MasterAttributeID;
      row1["F_SOURCE_ID"] = (object) attrProperties.SourceAttributeID;
      table.Rows.Add(row1);
      table.AcceptChanges();
      (DBAttributeTypeService.GetDBAttributeType(this.UserSession, row1) as DBAttributeType).DoAfterCreate();
      this.UserSession.Commit();
      return int32;
    }
    catch (Exception ex)
    {
      LocalizationHolder.rm.GetString(sc_12431.ssp_appserver_12437());
      if (ex.Message.IndexOf("IMS_ATTRIBUTES_NAME") >= 0)
      {
        string message = string.Format(LocalizationHolder.rm.GetString(sc_12431.ssp_appserver_12438()), (object) attrProperties.Name);
        this.UserSession.Rollback();
        throw new AlreadyExistsException(message);
      }
      this.UserSession.Rollback();
      throw;
    }
  }

  public int Create(AttributeTypeProperties attrProperties)
  {
    int num = 0;
    IDbManager dataManager = this.UserSession.DataManager;
    this._LastEventID = this.AddEvent(0L, ActionType.Create, EventlogRecordType.AccessDenied, string.Format(LocalizationHolder.rm.GetString("Kernel_139"), (object) attrProperties.Name));
    this.CheckAccess(ActionType.Create);
    if (attrProperties.FieldType == FieldTypes.ftExternalLink)
      throw new KernelException(sc_12431.ssp_appserver_12439());
    this.UserSession.StartTransaction();
    try
    {
      if (attrProperties.FieldType == FieldTypes.ftSystem)
        throw new KernelExceptionID(sc_12431.ssp_appserver_12440(708334665));
      if (attrProperties.AttributeGuid == Guid.Empty)
        attrProperties.AttributeGuid = Guid.NewGuid();
      if (attrProperties.LanguageID != "")
        this.UserSession.GetLanguage(attrProperties.LanguageID);
      if (attrProperties.AreaID != "")
        this.UserSession.GetSubjectAreaCollection().ValidateAriasString(attrProperties.AreaID);
      if (attrProperties.LevelID != 0)
        this.UserSession.GetLifecycleLevel(attrProperties.LevelID);
      SqlHelper.ValidateEmptyValue(attrProperties.Name, LocalizationHolder.rm.GetString("Kernel_140"));
      if (attrProperties.FieldType == FieldTypes.ftMeasured && attrProperties.SizeType == 0L)
        attrProperties.SizeType = -1L;
      if (this.UserSession.DBCache.GetTable("IMS_ATTRIBUTES").Select("F_NAME = " + SqlHelper.QString(attrProperties.Name)).Length != 0)
        throw new KernelException(sc_12431.ssp_appserver_12441());
      dataManager.ExecuteSpNonQuery(sc_12431.ssp_appserver_12442(), dataManager.Parameter("inNAME", (object) attrProperties.Name), dataManager.Parameter("inSHORT_NAME", (object) ""), dataManager.Parameter("inALIAS", (object) ""), dataManager.Parameter("inNOTE", (object) attrProperties.Note), dataManager.Parameter("inATTRIBUTE_TYPE", (object) Convert.ToInt32((object) attrProperties.FieldType)), dataManager.Parameter("inDEFAULT_VALUE", (object) ""), dataManager.Parameter("inMULTIPLE_VALUED", (object) Convert.ToInt32((object) MultiValueModes.SingleValue)), dataManager.Parameter("inCOMPUTED", (object) Convert.ToInt32((object) ComputeValueModes.NotComputableValue)), dataManager.Parameter("inSIZE_TYPE", (object) short.MinValue), dataManager.Parameter("inLEVEL_ID", (object) attrProperties.LevelID), dataManager.Parameter("inFORMULA", (object) ""), dataManager.Parameter("inLANGUAGE_ID", (object) attrProperties.LanguageID), dataManager.Parameter("inGUID", (object) attrProperties.AttributeGuid.ToString()), dataManager.Parameter("inAREA_ID", (object) attrProperties.AreaID), dataManager.OutputParameter("outATTRIBUTE_ID", (object) num));
      int int32 = Convert.ToInt32(dataManager.GetOutputParameterValue("outATTRIBUTE_ID"));
      DataTable dataTable = dataManager.ExecuteDataTable("SELECT * FROM IMS_ATTRIBUTES WHERE F_ATTRIBUTE_ID = " + int32.ToString());
      if (dataTable.Rows.Count != 1)
        throw new AttributeNotFoundException(int32, 0L);
      this.UserSession.DBCache.AddRow("IMS_ATTRIBUTES", dataTable.Rows[0], (IUserSession) this.UserSession);
      DBAttributeType attributeType = this.UserSession.GetAttributeType(int32) as DBAttributeType;
      attributeType.SetCreatorAccess();
      attributeType.LoggingOn = false;
      attrProperties.AttributeID = int32;
      attributeType._PreventedProperties = attrProperties;
      if (attrProperties.PossibleValues != null)
        attributeType.SetPossibleValues(attrProperties.PossibleValues);
      attributeType.ShortName = attrProperties.ShortName;
      attributeType.Alias = attrProperties.Alias;
      attributeType.SizeType = attrProperties.SizeType;
      attributeType.MultipleValued = attrProperties.MultiValueMode;
      attributeType.Computed = attrProperties.Computed;
      attributeType.DefaultValue = attrProperties.DefaultValue;
      attributeType.Formula = attrProperties.Formula;
      attributeType.UniqueMode = attrProperties.Unique;
      attributeType.OptimizationMode = attrProperties.OptimizationMode;
      attributeType.IsContent = attrProperties.IsContent;
      attributeType.Options = attrProperties.Options;
      attributeType.Mask = attrProperties.Mask;
      attributeType.MasterAttributeID = attrProperties.MasterAttributeID;
      attributeType.SourceAttributeID = attrProperties.SourceAttributeID;
      attributeType.DoAfterCreate();
      (ServerServices.GetService(typeof (IEventLogHelper)) as IEventLogHelper).CloseEvent(this._LastEventID, 0L, (long) int32, string.Format(LocalizationHolder.rm.GetString("Kernel_141"), (object) attrProperties.Name), "", EventlogRecordType.AccessGranted, (IUserSession) this.UserSession);
      if (Convert.ToInt32(this.ParentID) > 0)
        this.UserSession.GetAttributesGroup(Convert.ToInt32(this.ParentID)).IncludeAttribute(int32);
      this.UserSession.Commit();
      return int32;
    }
    catch (Exception ex)
    {
      string str = LocalizationHolder.rm.GetString(sc_12431.ssp_appserver_12443());
      if (ex.Message.IndexOf("IMS_ATTRIBUTES_NAME") >= 0)
      {
        string message = string.Format(LocalizationHolder.rm.GetString(sc_12431.ssp_appserver_12444()), (object) attrProperties.Name);
        this.UserSession.Rollback();
        this.CloseEvent(this._LastEventID, EventlogRecordType.Error, str + message);
        throw new AlreadyExistsException(message);
      }
      this.UserSession.Rollback();
      this.CloseEvent(this._LastEventID, EventlogRecordType.Error, str + ex.Message);
      throw;
    }
  }

  public AttributeTypePropertiesValidator GetValidator(FieldTypes fldtype)
  {
    AttributeTypePropertiesValidator validator = new AttributeTypePropertiesValidator();
    string str = AttributeCacheHelper.FillValidator(ref validator, fldtype, this.UserSession.AreaID, this.UserSession.DBCache.GetTable("IMS_ATTRIBUTES"));
    validator.PossibleValuesTable = !(str == "") ? this.UserSession.DataManager.ExecuteDataTable($"SELECT F_INLIST_ID, {str}, F_DESCRIPTION FROM IMS_POSSIBLE_VALUES WHERE F_ATTRIBUTE_ID = 0 AND F_OBJECT_TYPE = 0 AND F_RELATION_TYPE = 0") : (DataTable) null;
    return validator;
  }

  public AttributeTypePropertiesValidator GetValidatorForObjectType(int attributeID)
  {
    return AttributeCacheHelper.GetValidatorForObjectType(this.UserSession.GetAttributeType(attributeID), this.UserSession.GetAttributeTypeCollection(-1));
  }

  public AttributeTypePropertiesValidator GetValidatorForRelationType(int attributeID)
  {
    return this.GetValidatorForObjectType(attributeID);
  }

  public override DataTable Select(string orderBy, params object[] addInfo)
  {
    DataTable dataTable = base.Select(orderBy, addInfo);
    if (addInfo != null)
    {
      dataTable = AttributeCacheHelper.AddInfoToTable(dataTable, addInfo, (IUserSession) this.UserSession);
      this.FillCaptions(dataTable);
    }
    return dataTable;
  }

  protected override void DeleteNotVisibleRows(DataTable table)
  {
    base.DeleteNotVisibleRows(table);
    if (!this._Filtering)
      return;
    int columnIndex1 = table.Columns.IndexOf("F_OPTIONS");
    int columnIndex2 = table.Columns.IndexOf("F_COMPUTED");
    for (int index = table.Rows.Count - 1; index >= 0; --index)
    {
      AttributeOptions int32 = (AttributeOptions) Convert.ToInt32(table.Rows[index][columnIndex1]);
      if ((int32 & AttributeOptions.Internal) == AttributeOptions.Internal || (int32 & AttributeOptions.LocalImbaseAttribute) == AttributeOptions.LocalImbaseAttribute || Convert.ToInt32(table.Rows[index][columnIndex2]) == 3)
        table.Rows.Remove(table.Rows[index]);
    }
    table.AcceptChanges();
  }
}
