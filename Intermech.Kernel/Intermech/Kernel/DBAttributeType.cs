// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBAttributeType
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Expressions;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;


namespace Intermech.Kernel;

public class DBAttributeType : 
  DBMetadataExtensions,
  IDBAttributeType,
  IDBGuid,
  IDBSubjectArea,
  IDBLanguage,
  IDeletable,
  IDBLifecycleLevel,
  IDBSecurity
{
  protected int _AttributeID;
  protected List<FieldTypes> _ConvertList = new List<FieldTypes>();
  protected string _ValueFieldName = "F_STRING_VALUE";
  protected string _PossibleValueFieldName = "F_STRING_VALUE";
  protected string _TextFieldName = "F_STRING_VALUE";
  internal Type _DataType = typeof (string);
  protected bool _CanStorePossibleValues = true;
  protected bool _ComputableAttribute;
  protected bool _UniquedAttribute;
  protected RelationalOperators[] _EnabledOperators;
  protected FieldTypes[] CompatibleTypes;
  internal AttributeTypeProperties _PreventedProperties;
  private static Dictionary<ActionType, bool> metadataActions = new Dictionary<ActionType, bool>(7);
  private string _LastEventNote;

  static DBAttributeType()
  {
    DBAttributeType.metadataActions.Add(ActionType.GetAccess, false);
    DBAttributeType.metadataActions.Add(ActionType.SetAccess, false);
    DBAttributeType.metadataActions.Add(ActionType.EditProperties, false);
    DBAttributeType.metadataActions.Add(ActionType.Delete, false);
    DBAttributeType.metadataActions.Add(ActionType.Write, true);
    DBAttributeType.metadataActions.Add(ActionType.List, true);
  }

  public DBAttributeType(UserSession uSession, DataRow aAttributeRow)
    : base(uSession)
  {
    this._AttributeID = Convert.ToInt32(aAttributeRow["F_ATTRIBUTE_ID"]);
    this.paramsTable.Create(aAttributeRow);
    this._PreventedProperties = new AttributeTypeProperties();
    this._PreventedProperties.AttributeID = 0;
    this.InitSecurityOptions(3, (long) this._AttributeID);
    this.SetMDExtensionsType(this._AttributeID, -1, -1);
  }

  protected override void InitSecurityOptions(int aCategoryType, long aCategoryID)
  {
    this.InitStaticSecurityOptions(aCategoryType, aCategoryID, DBAttributeType.metadataActions);
  }

  internal virtual bool CompareValues(object value1, object value2)
  {
    return CompareValuesHelper.CompareObjectValues(value1, value2);
  }

  public virtual bool IsVirtualAttribute => false;

  public virtual string GetSQL(string mainTableName) => string.Empty;

  public override ActionCategory GetActionCategory(ActionType actionType)
  {
    return actionType == ActionType.EditProperties || actionType == ActionType.Delete ? ActionCategory.Admin : base.GetActionCategory(actionType);
  }

  public static bool CanSkipInit(FieldTypes ft)
  {
    return ft != FieldTypes.ftAutoInc && ft != FieldTypes.ftBlob && ft != FieldTypes.ftFile && ft != FieldTypes.ftMemo && ft != FieldTypes.ftShortBlob;
  }

  internal static bool CanQuickCopy(FieldTypes ft)
  {
    return ft != FieldTypes.ftBlob && ft != FieldTypes.ftFile && ft != FieldTypes.ftMemo && ft != FieldTypes.ftShortBlob;
  }

  protected virtual void ClearValues(string fldName)
  {
    IDbDataParameter dbDataParameter = this.UserSession.DataManager.Parameter("attrID", (object) this.AttributeID);
    List<string> objectAttrsTables = this.UserSession.DBCache.GetObjectAttrsTables();
    objectAttrsTables.Add("IMS_RELATION_ATTRS");
    objectAttrsTables.Add("IMS_OBJ_SNAPATTRS");
    objectAttrsTables.Add("IMS_REL_SNAPATTRS");
    for (int index = 0; index < objectAttrsTables.Count; ++index)
    {
      try
      {
        this.UserSession.DataManager.ExecuteNonQuery($"UPDATE {objectAttrsTables[index]} SET {fldName} = NULL WHERE F_ATTRIBUTE_ID = :attrID", dbDataParameter);
      }
      catch (Exception ex)
      {
        this.UserSession.EventLog.AddToTrace($"ClearValues for {objectAttrsTables[index]}: {ex.Message}", Consts.traceError, string.Empty);
      }
    }
  }

  public virtual bool CanUseInFormula
  {
    get
    {
      return this.AttributeType == FieldTypes.ftObjectLink || this.AttributeType == FieldTypes.ftObjectLinkByID || this.AttributeType == FieldTypes.ftMemo || this.AttributeType == FieldTypes.ftBoolean || this.ComputableAttribute;
    }
  }

  public bool CanStorePossibleValues => this._CanStorePossibleValues;

  internal virtual string[] IndexFieldNames
  {
    get
    {
      return new string[1]
      {
        "F" + this.AttributeID.ToString()
      };
    }
  }

  public string[] FieldNames
  {
    get => AttributeCacheHelper.GetAtributeFieldNames(this.AttributeType, this.AttributeID);
  }

  public virtual RelationalOperators[] EnabledOperators
  {
    get
    {
      return this.MultipleValued == MultiValueModes.MultiValues || this.MultipleValued == MultiValueModes.MultiValuesFromList || this.MultipleValued == MultiValueModes.SingleValueFromList ? AttributeCacheHelper.GetMultiValuesRelationalOperators(this.AttributeType == FieldTypes.ftFile || this.AttributeType == FieldTypes.ftString) : this._EnabledOperators;
    }
  }

  public void ValidateRelationalOperator(
    RelationalOperators relOperator,
    bool enableGreaterLessOperators,
    ColumnContents content)
  {
    if ((!enableGreaterLessOperators || relOperator != RelationalOperators.Greater && relOperator != RelationalOperators.Less && relOperator != RelationalOperators.Empty && relOperator != RelationalOperators.NotEmpty && relOperator != RelationalOperators.NotExistsOrEmpty && relOperator != RelationalOperators.Equal) && Array.IndexOf<RelationalOperators>(this.GetEnabledOperators(content), relOperator) < 0)
      throw new KernelExceptionID(sc_12586.ssp_appserver_12587(1838236243), (object) RelationalOperatorsHelper.GetCaption(relOperator), (object) this.Name);
  }

  public RelationalOperators[] GetEnabledOperators(ColumnContents content)
  {
    RelationalOperators[] enabledOperators;
    if (content == ColumnContents.String && (this.MultipleValued == MultiValueModes.MultiValues || this.MultipleValued == MultiValueModes.MultiValuesFromList))
      enabledOperators = AttributeCacheHelper.GetMultiValuesRelationalOperators(true);
    else if (content == ColumnContents.Text || this.MultipleValued == MultiValueModes.MultiValues || this.MultipleValued == MultiValueModes.MultiValuesFromList || this.MultipleValued == MultiValueModes.SingleValueFromList)
    {
      enabledOperators = this.EnabledOperators;
    }
    else
    {
      switch (content)
      {
        case ColumnContents.ID:
          enabledOperators = new RelationalOperators[14]
          {
            RelationalOperators.Empty,
            RelationalOperators.NotExistsOrEmpty,
            RelationalOperators.Equal,
            RelationalOperators.Greater,
            RelationalOperators.GreaterOrEqual,
            RelationalOperators.Less,
            RelationalOperators.LessOrEqual,
            RelationalOperators.NotEmpty,
            RelationalOperators.NotEqual,
            RelationalOperators.Between,
            RelationalOperators.NotBetween,
            RelationalOperators.In,
            RelationalOperators.NotIn,
            RelationalOperators.AttributeExists
          };
          break;
        case ColumnContents.Date:
          enabledOperators = new RelationalOperators[14]
          {
            RelationalOperators.Empty,
            RelationalOperators.NotExistsOrEmpty,
            RelationalOperators.Equal,
            RelationalOperators.LastNDays,
            RelationalOperators.NextNDays,
            RelationalOperators.Greater,
            RelationalOperators.GreaterOrEqual,
            RelationalOperators.Less,
            RelationalOperators.LessOrEqual,
            RelationalOperators.NotEmpty,
            RelationalOperators.NotEqual,
            RelationalOperators.Between,
            RelationalOperators.NotBetween,
            RelationalOperators.AttributeExists
          };
          break;
        case ColumnContents.Value:
          enabledOperators = new RelationalOperators[14]
          {
            RelationalOperators.Empty,
            RelationalOperators.NotExistsOrEmpty,
            RelationalOperators.Equal,
            RelationalOperators.Greater,
            RelationalOperators.GreaterOrEqual,
            RelationalOperators.Less,
            RelationalOperators.LessOrEqual,
            RelationalOperators.NotEmpty,
            RelationalOperators.NotEqual,
            RelationalOperators.Between,
            RelationalOperators.NotBetween,
            RelationalOperators.In,
            RelationalOperators.NotIn,
            RelationalOperators.AttributeExists
          };
          break;
        case ColumnContents.String:
          enabledOperators = new RelationalOperators[19]
          {
            RelationalOperators.Empty,
            RelationalOperators.NotExistsOrEmpty,
            RelationalOperators.Equal,
            RelationalOperators.NotEmpty,
            RelationalOperators.NotEqual,
            RelationalOperators.EndString,
            RelationalOperators.StartString,
            RelationalOperators.Substring,
            RelationalOperators.StringTemplate,
            RelationalOperators.NotEndString,
            RelationalOperators.Greater,
            RelationalOperators.GreaterOrEqual,
            RelationalOperators.Less,
            RelationalOperators.LessOrEqual,
            RelationalOperators.AttributeExists,
            RelationalOperators.NotStartString,
            RelationalOperators.NotSubstring,
            RelationalOperators.In,
            RelationalOperators.NotIn
          };
          break;
        default:
          enabledOperators = this.EnabledOperators;
          break;
      }
    }
    return enabledOperators;
  }

  public string ValueFieldName => this._ValueFieldName;

  public string PossibleValueFieldName => this._PossibleValueFieldName;

  public string TextFieldName => this._TextFieldName;

  public virtual bool IsGridable
  {
    get
    {
      return this.AttributeType != FieldTypes.ftPassword && this.Computed != ComputeValueModes.IndexValue;
    }
  }

  public bool ComputableAttribute => this._ComputableAttribute;

  public bool UniquedAttribute => this._UniquedAttribute;

  public override string ObjectName
  {
    get => string.Format(LocalizationHolder.rm.GetString("Kernel_38"), (object) this.Name);
  }

  protected virtual string GetNullOperator() => $"({this._ValueFieldName} IS NULL)";

  private void ThrowNotNullObjectFoundException(DataTable tbl)
  {
    long[] objectsID = new long[tbl.Rows.Count];
    for (int index = 0; index < tbl.Rows.Count; ++index)
      objectsID[index] = Convert.ToInt64(tbl.Rows[index][0]);
    throw new ObjectsFoundException(string.Format(sc_12586.ssp_appserver_12588(), (object) this.Name, (object) tbl.Rows.Count), $"Объекты с пустым значением атрибута '{this.Name}':", objectsID);
  }

  internal void ValidateNotNull(int objectTypeID, int relationTypeID)
  {
    string nullOperator = this.GetNullOperator();
    string str = "IMS_OBJECT_ATTRS";
    StringBuilder stringBuilder = new StringBuilder();
    int index1;
    if (objectTypeID < 0)
    {
      if (relationTypeID < 0)
      {
        DataTable table = this.UserSession.DBCache.GetTable("IMS_ATTR4OBJ_TYPES");
        index1 = this.AttributeID;
        string filterExpression = "F_ATTRIBUTE_ID = " + index1.ToString();
        DataRow[] dataRowArray = table.Select(filterExpression);
        for (index1 = 0; index1 < dataRowArray.Length; ++index1)
        {
          DataRow dataRow = dataRowArray[index1];
          if ((Convert.ToInt32(dataRow["F_OPTIONS"]) & 8) == 0)
            stringBuilder.Append(dataRow["F_OBJECT_TYPE"].ToString() + ",");
        }
        if (stringBuilder.Length > 0)
        {
          stringBuilder[stringBuilder.Length - 1] = ')';
          stringBuilder.Insert(0, "AND B.F_OBJECT_TYPE NOT IN (");
        }
      }
    }
    else
    {
      stringBuilder.Append("AND B.F_OBJECT_TYPE = " + objectTypeID.ToString());
      if (MetaDataHelper.IsLocalObjectType(objectTypeID))
        str = "IMV_A" + objectTypeID.ToString();
    }
    if (stringBuilder.Length > 0)
    {
      DataTable tbl = this.UserSession.DataManager.ExecuteDataTable(string.Format("SELECT DISTINCT A.F_OBJECT_ID FROM {4} A, IMS_OBJECTS B WHERE F_ATTRIBUTE_ID = {0} AND {1} AND B.F_OBJECT_ID = A.F_OBJECT_ID AND B.F_OBJECT_ID > 0 AND B.F_OBJECT_VER_TYPE <> -1 AND B.F_LEVEL_ID <> {2} {3}", (object) this.AttributeID, (object) nullOperator, (object) this.UserSession.IdentHelper.DeletedID, (object) stringBuilder.ToString(), (object) str));
      if (tbl.Rows.Count > 0)
        this.ThrowNotNullObjectFoundException(tbl);
    }
    if (objectTypeID < 0 && relationTypeID < 0)
    {
      DataRow[] dataRowArray = this.UserSession.DBCache.GetTable("IMS_OBJECT_TYPES").Select("F_ANY_ATTRIBUTES = 1");
      for (int index2 = 0; index2 < dataRowArray.Length; ++index2)
      {
        if ((Convert.ToInt32(dataRowArray[index2]["F_OPTIONS"]) & 16 /*0x10*/) == 16 /*0x10*/)
        {
          DBObjectType objectType = this.UserSession.GetObjectType(Convert.ToInt32(dataRowArray[index2]["F_OBJECT_TYPE"])) as DBObjectType;
          if (objectType.Attributes.GetAttributeByID(this.AttributeID) == null)
          {
            DataTable tbl = this.UserSession.DataManager.ExecuteDataTable(string.Format("SELECT DISTINCT A.F_OBJECT_ID FROM {3} A, IMS_OBJECTS B WHERE F_ATTRIBUTE_ID = {0} AND {1} AND B.F_OBJECT_ID = A.F_OBJECT_ID AND B.F_OBJECT_ID > 0 AND B.F_LEVEL_ID <> {2}", (object) this.AttributeID, (object) nullOperator, (object) this.UserSession.IdentHelper.DeletedID, (object) objectType.AttributesTableName));
            if (tbl.Rows.Count > 0)
              this.ThrowNotNullObjectFoundException(tbl);
          }
        }
      }
    }
    stringBuilder.Length = 0;
    if (relationTypeID < 0)
    {
      if (objectTypeID < 0)
      {
        DataTable table = this.UserSession.DBCache.GetTable("IMS_ATTR4RELATION_TYPES");
        index1 = this.AttributeID;
        string filterExpression = "F_ATTRIBUTE_ID = " + index1.ToString();
        DataRow[] dataRowArray = table.Select(filterExpression);
        for (index1 = 0; index1 < dataRowArray.Length; ++index1)
        {
          DataRow dataRow = dataRowArray[index1];
          if ((Convert.ToInt32(dataRow["F_OPTIONS"]) & 8) == 0)
            stringBuilder.Append(dataRow["F_RELATION_TYPE"].ToString() + ",");
        }
        if (stringBuilder.Length > 0)
        {
          stringBuilder[stringBuilder.Length - 1] = ')';
          stringBuilder.Insert(0, "AND B.F_RELATION_TYPE NOT IN (");
        }
      }
    }
    else
      stringBuilder.Append("AND B.F_RELATION_TYPE = " + relationTypeID.ToString());
    if (stringBuilder.Length <= 0)
      return;
    DataTable dataTable = this.UserSession.DataManager.ExecuteDataTable($"SELECT DISTINCT A.F_PRJLINK_ID FROM IMS_RELATION_ATTRS A, IMS_RELATIONS B WHERE F_ATTRIBUTE_ID = {this.AttributeID} AND {nullOperator} AND B.F_PRJLINK_ID = A.F_PRJLINK_ID {stringBuilder.ToString()}", this.UserSession.DataManager.Parameter("del_date", (object) DateTime.UtcNow));
    if (dataTable.Rows.Count > 0)
    {
      long[] relationsID = new long[dataTable.Rows.Count];
      for (int index3 = 0; index3 < dataTable.Rows.Count; ++index3)
        relationsID[index3] = Convert.ToInt64(dataTable.Rows[index3][0]);
      throw new RelationsFoundException(string.Format(sc_12586.ssp_appserver_12589(), (object) this.Name, (object) dataTable.Rows.Count), $"Связи с пустым значением атрибута '{this.Name}':", relationsID);
    }
  }

  private void CheckChangeEnable(string propertyID)
  {
    if (!this.UserSession.CanChangeObjectElement(3, (object) this.AttributeID, ObligatoryElementKeys.GetKeyForObjectProperty(propertyID)))
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_906"), (object) DataSetProcessor.GetCaption(propertyID)));
  }

  internal void ValidateOptions(AttributeOptions options)
  {
    if ((options & AttributeOptions.AddToGlobalIndex) != AttributeOptions.AddToGlobalIndex)
      return;
    if (this.AttributeType != FieldTypes.ftString && this.AttributeType != FieldTypes.ftMemo && this.AttributeType != FieldTypes.ftFile && this.AttributeType != FieldTypes.ftObjectLink)
      throw new KernelExceptionID(sc_12586.ssp_appserver_12591(84485193), (object) EnumDescConverter.GetEnumDescription((Enum) this.AttributeType));
    if (this.Computed != ComputeValueModes.NotComputableValue)
      throw new KernelExceptionID(sc_12586.ssp_appserver_12592(1193142899));
  }

  public AttributeOptions Options
  {
    get => (AttributeOptions) Convert.ToInt32(this.paramsTable[36]);
    set
    {
      if (this.Options == value)
        return;
      this._LastEventNote = LocalizationHolder.rm.GetString("Kernel_39") + AttributeOptionsHelper.GetCaptions(value);
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, this._LastEventNote);
      this.CheckAccess(ActionType.EditProperties);
      foreach (AttributeOptions optionsFlag in (AttributeOptions[]) Enum.GetValues(typeof (AttributeOptions)))
      {
        if ((value & optionsFlag) != (this.Options & optionsFlag) && !this.UserSession.CanChangeObjectElement(3, (object) this.AttributeID, ObligatoryElementKeys.GetKeyForObjectOptionsFlag((int) optionsFlag)))
          throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_906"), (object) EnumDescConverter.GetEnumDescription((Enum) optionsFlag)));
      }
      this.UserSession.StartTransaction();
      try
      {
        if ((value & AttributeOptions.DisableNulls) == AttributeOptions.DisableNulls && (this.Options & AttributeOptions.DisableNulls) == AttributeOptions.None)
          this.ValidateNotNull(-1, -1);
        this.ValidateOptions(value);
        bool flag = false;
        if ((value & AttributeOptions.AddToGlobalIndex) == AttributeOptions.AddToGlobalIndex)
        {
          if ((this.Options & AttributeOptions.AddToGlobalIndex) == AttributeOptions.None)
          {
            this.UserSession.GlobalIndex.AddToQueue((IDBAttributeType) this);
            flag = true;
          }
        }
        else if ((this.Options & AttributeOptions.AddToGlobalIndex) == AttributeOptions.AddToGlobalIndex)
          this.UserSession.GlobalIndex.DeleteFromIndex((IDBAttributeType) this);
        if ((value & AttributeOptions.DisableSplitIndexValue) == AttributeOptions.DisableSplitIndexValue)
        {
          if ((value & AttributeOptions.AddToGlobalIndex) == AttributeOptions.None)
            throw new KernelExceptionID(398, (object) AttributeOptionsHelper.GetCaption(AttributeOptions.AddToGlobalIndex));
          if (this.AttributeType != FieldTypes.ftString)
            throw new KernelExceptionID(397);
          if ((this.Options & AttributeOptions.DisableSplitIndexValue) == AttributeOptions.None && !flag)
            this.UserSession.GlobalIndex.AddToQueue((IDBAttributeType) this);
        }
        else if ((this.Options & AttributeOptions.DisableSplitIndexValue) == AttributeOptions.DisableSplitIndexValue && !flag)
          this.UserSession.GlobalIndex.AddToQueue((IDBAttributeType) this);
        this.UserSession.DataManager.ExecuteNonQuery(sc_12586.ssp_appserver_12593(), this.UserSession.DataManager.Parameter("optID", (object) Convert.ToInt32((object) value)), this.UserSession.DataManager.Parameter(sc_12586.ssp_appserver_12594(), (object) this.AttributeID));
        this.UserSession.DBCache.ChangeTableValue("F_ATTRIBUTE_ID = " + this.AttributeID.ToString(), "IMS_ATTRIBUTES", "F_OPTIONS", (object) Convert.ToInt32((object) value), (IUserSession) this.UserSession);
        this.paramsTable[36] = (object) Convert.ToInt32((object) value);
        this.UserSession.Commit();
        (this.UserSession.DBCache as CacheDataset).SetAttrProperties(new Attribute4ID(this.AttributeID, -1, -1), this.OptimizationMode, value);
      }
      catch (Exception ex)
      {
        this.UserSession.Rollback();
        this.CloseEvent(EventID, EventlogRecordType.Error, LocalizationHolder.rm.GetString("Kernel_40") + ex.Message);
        throw;
      }
    }
  }

  public virtual string Mask
  {
    get => this.paramsTable[35].ToString();
    set
    {
      if (!(this.Mask != value))
        return;
      if (value == null)
        value = string.Empty;
      this._LastEventNote = value != string.Empty ? LocalizationHolder.rm.GetString("Kernel_41") + value : LocalizationHolder.rm.GetString("Kernel_42");
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, this._LastEventNote);
      this.CheckAccess(ActionType.EditProperties);
      this.CheckChangeEnable("F_MASK");
      this.UserSession.StartTransaction();
      try
      {
        this.UserSession.DataManager.ExecuteNonQuery(sc_12586.ssp_appserver_12595(), this.UserSession.DataManager.Parameter("attrID", (object) this.AttributeID), this.UserSession.DataManager.Parameter("val", (object) value));
        this.UserSession.DBCache.ChangeTableValue("F_ATTRIBUTE_ID = " + this.AttributeID.ToString(), "IMS_ATTRIBUTES", "F_MASK", (object) value, (IUserSession) this.UserSession);
        this.paramsTable[35] = (object) value;
        this.UserSession.Commit();
      }
      catch (Exception ex)
      {
        this.UserSession.Rollback();
        string str = LocalizationHolder.rm.GetString("Kernel_43") + ex.Message;
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        throw new KernelException(str, ex);
      }
    }
  }

  public AttributeTypeProperties PropertiesStructure
  {
    get
    {
      AttributeTypeProperties atProperties = new AttributeTypeProperties(this.AttributeID, this.Name, this.ShortName, this.Alias, this.Note, this.AttributeType, this.DefaultValue, this.MultipleValued, this.Computed, this.SizeType, this.Formula, this.UniqueMode, this.LevelID, this.LanguageID, this.SubjectAreas, this.GUID, this.OptimizationMode, this.IsContent, this.Options, this.Mask, this.MasterAttributeID, this.SourceAttributeID);
      this.DoGetPropertiesStructure(ref atProperties);
      return atProperties;
    }
    set
    {
      if (this.AttributeID != value.AttributeID)
        throw new KernelException(sc_12586.ssp_appserver_12596() + value.AttributeID.ToString());
      this._PreventedProperties = value;
      this.UserSession.StartTransaction();
      try
      {
        this.Name = value.Name;
        this.ShortName = value.ShortName;
        this.Alias = value.Alias;
        this.Note = value.Note;
        if (this.AttributeType != value.FieldType)
        {
          object newValue = this.paramsTable[105];
          this.paramsTable[105] = (object) Convert.ToInt32(value.SizeType);
          this.UserSession.DBCache.ChangeTableValue("F_ATTRIBUTE_ID = " + this._AttributeID.ToString(), "IMS_ATTRIBUTES", "F_SIZE_TYPE", (object) Convert.ToInt32(value.SizeType), (IUserSession) this.UserSession);
          try
          {
            this.AttributeType = value.FieldType;
          }
          finally
          {
            this.paramsTable[105] = newValue;
            this.UserSession.DBCache.ChangeTableValue("F_ATTRIBUTE_ID = " + this._AttributeID.ToString(), "IMS_ATTRIBUTES", "F_SIZE_TYPE", newValue, (IUserSession) this.UserSession);
          }
          DBAttributeType attributeType = this.UserSession.GetAttributeType(this.AttributeID) as DBAttributeType;
          if (value.PossibleValues != null)
            attributeType.SetPossibleValues(value.PossibleValues);
          attributeType.MultipleValued = value.MultiValueMode;
          attributeType.Computed = value.Computed;
          attributeType.SizeType = value.SizeType;
          attributeType.DefaultValue = value.DefaultValue;
          attributeType.Formula = value.Formula;
          attributeType.UniqueMode = value.Unique;
          attributeType.LevelID = value.LevelID;
          attributeType.LanguageID = value.LanguageID;
          attributeType.SubjectAreas = value.AreaID;
          attributeType.GUID = value.AttributeGuid;
          attributeType.OptimizationMode = value.OptimizationMode;
          attributeType.IsContent = value.IsContent;
          attributeType.Options = value.Options;
          attributeType.Mask = value.Mask;
          attributeType.MasterAttributeID = value.MasterAttributeID;
          attributeType.SourceAttributeID = value.SourceAttributeID;
        }
        else
        {
          if (value.PossibleValues != null)
            this.SetPossibleValues(value.PossibleValues);
          this.SizeType = value.SizeType;
          this.DefaultValue = value.DefaultValue;
          this.Computed = value.Computed;
          this.Formula = value.Formula;
          this.MultipleValued = value.MultiValueMode;
          this.UniqueMode = value.Unique;
          this.LevelID = value.LevelID;
          this.LanguageID = value.LanguageID;
          this.SubjectAreas = value.AreaID;
          this.GUID = value.AttributeGuid;
          this.OptimizationMode = value.OptimizationMode;
          this.IsContent = value.IsContent;
          this.Options = value.Options;
          this.Mask = value.Mask;
          this.MasterAttributeID = value.MasterAttributeID;
          this.SourceAttributeID = value.SourceAttributeID;
        }
        this.DoSetPropertiesStructure(value);
        this.UserSession.Commit();
      }
      catch (Exception ex)
      {
        this.UserSession.Rollback();
        EventlogRecordType auditType = !(ex is AccessDeniedException) ? EventlogRecordType.Error : EventlogRecordType.AccessDenied;
        if (this._LastEventNote != null)
          this.AddEvent(0L, ActionType.EditProperties, auditType, $"{this._LastEventNote}{Environment.NewLine} Ошибка: {ex.Message}");
        throw;
      }
    }
  }

  protected virtual void DoSetPropertiesStructure(AttributeTypeProperties value)
  {
  }

  protected virtual void DoGetPropertiesStructure(ref AttributeTypeProperties atProperties)
  {
  }

  private void UpdateViewValue(
    string targetTable,
    string sourceTable,
    string targetField,
    string sourceField,
    string keyField)
  {
    try
    {
      this.UserSession.DataManager.SetAdminCommandTimeout();
      this.UserSession.DataManager.ExecuteNonQuery(string.Format("UPDATE {0} SET {1} = (SELECT {2} FROM {3} WHERE {3}.{4} = {0}.{4} AND {3}.F_ATTRIBUTE_ID = {5} AND {3}.F_INLIST_ID = 0)", (object) targetTable, (object) targetField, (object) sourceField, (object) sourceTable, (object) keyField, (object) this.AttributeID));
    }
    finally
    {
      this.UserSession.DataManager.SetNormalCommandTimeout();
    }
  }

  internal void UpdateViewFields(
    string targetTable,
    string attributesTable,
    string[] fieldNames,
    string keyField)
  {
    this.UpdateViewValue(targetTable, attributesTable, fieldNames[0], this.TextFieldName, keyField);
    if (fieldNames.Length > 1)
      this.UpdateViewValue(targetTable, attributesTable, fieldNames[1], "F_INTEGER_VALUE", keyField);
    if (fieldNames.Length > 2)
      this.UpdateViewValue(targetTable, attributesTable, fieldNames[2], "F_DOUBLE_VALUE", keyField);
    if (fieldNames.Length <= 3)
      return;
    this.UpdateViewValue(targetTable, attributesTable, fieldNames[3], "F_DATE_VALUE", keyField);
  }

  internal string[] GetViews4Modify()
  {
    List<string> stringList = new List<string>();
    if (this.OptimizationMode != OptimizationModes.Write)
      stringList.Add("IMS_OBJECTS_VIEW");
    foreach (DataRow row in (InternalDataCollectionBase) this.UserSession.GetObjectTypeCollection(-2).Select(string.Empty).Rows)
    {
      int int32 = Convert.ToInt32(row["F_OBJECT_TYPE"]);
      switch ((this.UserSession.DBCache as CacheDataset).GetOptimizationMode(this.AttributeID, int32, -1))
      {
        case OptimizationModes.Read:
        case OptimizationModes.Seek:
          stringList.Add("IMV_O" + int32.ToString());
          continue;
        default:
          continue;
      }
    }
    foreach (DataRow row in (InternalDataCollectionBase) this.UserSession.GetRelationTypeCollection().Select(string.Empty).Rows)
    {
      int int32 = Convert.ToInt32(row["F_RELATION_TYPE"]);
      switch ((this.UserSession.DBCache as CacheDataset).GetOptimizationMode(this.AttributeID, -1, int32))
      {
        case OptimizationModes.Read:
        case OptimizationModes.Seek:
          stringList.Add("IMV_R" + int32.ToString());
          continue;
        default:
          continue;
      }
    }
    return stringList.ToArray();
  }

  private void RestructViewsAfterChangeDataType(
    FieldTypes oldType,
    FieldTypes newType,
    string[] oldIndexNames)
  {
    IDbManager db = this.UserSession.DataManager;
    string[] old_fieldNames = AttributeCacheHelper.GetAtributeFieldNames(oldType, this.AttributeID);
    string[] indexNames = this.IndexFieldNames;
    string[] fieldNames = this.FieldNames;
    foreach (DataRow row in (InternalDataCollectionBase) this.UserSession.GetObjectTypeCollection(-2).Select(string.Empty).Rows)
    {
      int int32 = Convert.ToInt32(row["F_OBJECT_TYPE"]);
      OptimizationModes optimizationMode = (this.UserSession.DBCache as CacheDataset).GetOptimizationMode(this.AttributeID, int32, -1);
      switch (optimizationMode)
      {
        case OptimizationModes.Read:
        case OptimizationModes.Seek:
          RecreateAttrFields("IMV_O" + int32.ToString(), optimizationMode, "F_OBJECT_ID", this.UserSession.DBCache.GetAttributesTableName(int32));
          continue;
        default:
          continue;
      }
    }
    foreach (DataRow row in (InternalDataCollectionBase) this.UserSession.GetRelationTypeCollection().Select(string.Empty).Rows)
    {
      int int32 = Convert.ToInt32(row["F_RELATION_TYPE"]);
      OptimizationModes optimizationMode = (this.UserSession.DBCache as CacheDataset).GetOptimizationMode(this.AttributeID, -1, int32);
      switch (optimizationMode)
      {
        case OptimizationModes.Read:
        case OptimizationModes.Seek:
          RecreateAttrFields("IMV_R" + int32.ToString(), optimizationMode, "F_PRJLINK_ID", "IMS_RELATION_ATTRS");
          continue;
        default:
          continue;
      }
    }

    void RecreateAttrFields(
      string viewName,
      OptimizationModes mode,
      string keyField,
      string attributesTable)
    {
      if (mode == OptimizationModes.Seek)
      {
        foreach (string oldIndexName in oldIndexNames)
        {
          try
          {
            db.ExecuteNonQuery(db.DataProvider.GetDropIndexSQL(viewName, oldIndexName, SortOrders.ASC));
          }
          catch
          {
          }
        }
      }
      foreach (string oldFieldName in old_fieldNames)
        db.ExecuteNonQuery(db.DataProvider.GetDropColumnsSQL(viewName, oldFieldName));
      db.ExecuteNonQuery(db.DataProvider.GetAddColumnsSQL(viewName, this.ColumnSQL));
      this.UpdateViewFields(viewName, attributesTable, fieldNames, keyField);
      if (mode != OptimizationModes.Seek)
        return;
      foreach (string indexName in indexNames)
        db.ExecuteNonQuery(db.DataProvider.GetIndexSQL(viewName, indexName, SortOrders.ASC));
    }
  }

  public OptimizationModes OptimizationMode
  {
    get => (OptimizationModes) Convert.ToInt32(this.paramsTable[44]);
    set
    {
      if (this.OptimizationMode == value)
        return;
      this._LastEventNote = LocalizationHolder.rm.GetString("Kernel_44") + OptimizationModesHelper.GetCaption(value);
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, this._LastEventNote);
      this.CheckAccess(ActionType.EditProperties);
      this.CheckChangeEnable("F_INVIEW");
      IDbManager dataManager = this.UserSession.DataManager;
      string[] indexFieldNames = this.IndexFieldNames;
      string[] fieldNames = this.FieldNames;
      if (fieldNames == null)
      {
        if (value != OptimizationModes.Write)
          throw new KernelExceptionID(sc_12586.ssp_appserver_12597(311875783), (object) OptimizationModesHelper.GetCaption(value), (object) this.TypeCaption);
      }
      else
      {
        this.UserSession.StartTransaction();
        try
        {
          if (this.OptimizationMode == OptimizationModes.Seek)
          {
            foreach (string fldName in indexFieldNames)
            {
              try
              {
                dataManager.ExecuteNonQuery(dataManager.DataProvider.GetDropIndexSQL("IMS_OBJECTS_VIEW", fldName, SortOrders.ASC));
              }
              catch (Exception ex)
              {
                this.EventHelper.AddToTrace(string.Format(LocalizationHolder.rm.GetString("Kernel_45"), (object) dataManager.DataProvider.GetDropIndexSQL("IMS_OBJECTS_VIEW", fldName, SortOrders.ASC), (object) ex.Message), Consts.traceAlways, "sql_errors.log");
              }
              try
              {
                dataManager.ExecuteNonQuery(dataManager.DataProvider.GetDropIndexSQL("IMS_OBJECTS_VIEW", fldName, SortOrders.DESC));
              }
              catch (Exception ex)
              {
                this.EventHelper.AddToTrace(string.Format(LocalizationHolder.rm.GetString("Kernel_46"), (object) dataManager.DataProvider.GetDropIndexSQL("IMS_OBJECTS_VIEW", fldName, SortOrders.DESC), (object) ex.Message), Consts.traceAlways, "sql_errors.log");
              }
            }
          }
          if (value == OptimizationModes.Write)
          {
            foreach (string columnName in fieldNames)
              dataManager.ExecuteNonQuery(dataManager.DataProvider.GetDropColumnsSQL("IMS_OBJECTS_VIEW", columnName));
          }
          else if (this.OptimizationMode == OptimizationModes.Write)
          {
            dataManager.ExecuteNonQuery(dataManager.DataProvider.GetAddColumnsSQL("IMS_OBJECTS_VIEW", this.ColumnSQL));
            this.UpdateViewValue("IMS_OBJECTS_VIEW", "IMS_OBJECT_ATTRS", fieldNames[0], this.TextFieldName, "F_OBJECT_ID");
            if (fieldNames.Length > 1)
              this.UpdateViewValue("IMS_OBJECTS_VIEW", "IMS_OBJECT_ATTRS", fieldNames[1], "F_INTEGER_VALUE", "F_OBJECT_ID");
            if (fieldNames.Length > 2)
              this.UpdateViewValue("IMS_OBJECTS_VIEW", "IMS_OBJECT_ATTRS", fieldNames[2], "F_DOUBLE_VALUE", "F_OBJECT_ID");
            if (fieldNames.Length > 3)
              this.UpdateViewValue("IMS_OBJECTS_VIEW", "IMS_OBJECT_ATTRS", fieldNames[3], "F_DATE_VALUE", "F_OBJECT_ID");
          }
          if (value == OptimizationModes.Seek)
          {
            foreach (string fldName in indexFieldNames)
              dataManager.ExecuteNonQuery(dataManager.DataProvider.GetIndexSQL("IMS_OBJECTS_VIEW", fldName, SortOrders.ASC));
          }
          dataManager.ExecuteNonQuery(sc_12586.ssp_appserver_12598(), this.UserSession.DataManager.Parameter("attrID", (object) this.AttributeID), this.UserSession.DataManager.Parameter("val", (object) Convert.ToInt32((object) value)));
          this.UserSession.DBCache.ChangeTableValue("F_ATTRIBUTE_ID = " + this.AttributeID.ToString(), "IMS_ATTRIBUTES", "F_INVIEW", (object) Convert.ToInt32((object) value), (IUserSession) this.UserSession);
          this.paramsTable[44] = (object) Convert.ToInt32((object) value);
          this.UserSession.Commit();
          (this.UserSession.DBCache as CacheDataset).SetAttrProperties(new Attribute4ID(this.AttributeID, -1, -1), value, this.Options);
        }
        catch (Exception ex)
        {
          this.UserSession.Rollback();
          this.CloseEvent(EventID, EventlogRecordType.Error, LocalizationHolder.rm.GetString("Kernel_47") + ex.Message);
          throw;
        }
      }
    }
  }

  public string ValidationRule
  {
    get => string.Empty;
    set
    {
    }
  }

  public virtual void DoAfterCreate()
  {
    (this.EventHelper as EventLogHelper).OnAfterCreateAttributeType((IDBAttributeType) this, (IUserSession) this.UserSession);
  }

  public int AttributeID => this._AttributeID;

  public string Name
  {
    get => this.paramsTable[80 /*0x50*/].ToString();
    set
    {
      if (!(this.Name != value))
        return;
      this._LastEventNote = value != string.Empty ? LocalizationHolder.rm.GetString("Kernel_48") + value : LocalizationHolder.rm.GetString("Kernel_49");
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, this._LastEventNote);
      this.CheckAccess(ActionType.EditProperties);
      this.CheckChangeEnable("F_NAME");
      this.UserSession.StartTransaction();
      try
      {
        string oldValue = $"[{this.Name}]";
        SqlHelper.ValidateEmptyValue(value, LocalizationHolder.rm.GetString("Kernel_50"));
        SqlHelper.ValidateFieldLength(LocalizationHolder.rm.GetString("MDName"), value.Length, Consts.MaxObjectNameLength);
        if (this.UserSession.DBCache.GetTable("IMS_ATTRIBUTES").Select($"F_NAME = {SqlHelper.QString(value)} AND F_ATTRIBUTE_ID <> {this.AttributeID.ToString()}").Length != 0)
          throw new KernelException(sc_12586.ssp_appserver_12599());
        this.UserSession.DataManager.ExecuteNonQuery(sc_12586.ssp_appserver_12600(), this.UserSession.DataManager.Parameter("attrID", (object) this.AttributeID), this.UserSession.DataManager.Parameter("val", (object) value));
        ICacheDataset dbCache = this.UserSession.DBCache;
        int index = this.AttributeID;
        string filterStr = "F_ATTRIBUTE_ID = " + index.ToString();
        string newValue1 = value;
        UserSession userSession = this.UserSession;
        dbCache.ChangeTableValue(filterStr, "IMS_ATTRIBUTES", "F_NAME", (object) newValue1, (IUserSession) userSession);
        this.paramsTable[80 /*0x50*/] = (object) value;
        DataTable dataTable = this.UserSession.DBCache.GetTable("IMS_FORMULA_ATTRS").Copy();
        index = this.AttributeID;
        string filterExpression = "F_ATTRIBUTE_ID = " + index.ToString();
        DataRow[] dataRowArray1 = dataTable.Select(filterExpression);
        string newValue2 = $"[{value}]";
        DataRow[] dataRowArray2 = dataRowArray1;
        for (index = 0; index < dataRowArray2.Length; ++index)
        {
          DataRow dataRow = dataRowArray2[index];
          int int32 = Convert.ToInt32(dataRow["F_FORMULA_ID"]);
          if (Convert.ToInt32(dataRow["F_OBJECT_TYPE"]) > -1)
          {
            IDBAttributeType4 attributeById = this.UserSession.GetObjectType(Convert.ToInt32(dataRow["F_OBJECT_TYPE"])).Attributes.GetAttributeByID(int32, false);
            if (attributeById != null)
            {
              attributeById.Formula = attributeById.Formula.Replace(oldValue, newValue2);
              attributeById.ValidationRule = attributeById.ValidationRule.Replace(oldValue, newValue2);
            }
          }
          else if (Convert.ToInt32(dataRow["F_RELATION_TYPE"]) > -1)
          {
            IDBAttributeType4 attributeById = this.UserSession.GetRelationType(Convert.ToInt32(dataRow["F_RELATION_TYPE"])).Attributes.GetAttributeByID(int32, false);
            if (attributeById != null)
            {
              attributeById.Formula = attributeById.Formula.Replace(oldValue, newValue2);
              attributeById.ValidationRule = attributeById.ValidationRule.Replace(oldValue, newValue2);
            }
          }
          else
          {
            IDBAttributeType attributeType = this.UserSession.GetAttributeType(int32, false);
            if (attributeType != null)
              attributeType.Formula = attributeType.Formula.Replace(oldValue, newValue2);
          }
        }
        this.UserSession.Commit();
      }
      catch (Exception ex)
      {
        this.UserSession.Rollback();
        string str = LocalizationHolder.rm.GetString("Kernel_51");
        if (ex.Message.IndexOf("IMS_ATTRIBUTES_NAME") >= 0)
        {
          string message = string.Format(LocalizationHolder.rm.GetString("Kernel_52"), (object) value);
          this.CloseEvent(EventID, EventlogRecordType.Error, str + message);
          throw new AlreadyExistsException(message, ex);
        }
        this.CloseEvent(EventID, EventlogRecordType.Error, str + ex.Message);
        throw;
      }
    }
  }

  public string ShortName
  {
    get => this.paramsTable[79].ToString();
    set
    {
      if (!(this.ShortName != value))
        return;
      this._LastEventNote = value != string.Empty ? LocalizationHolder.rm.GetString("Kernel_53") + value : LocalizationHolder.rm.GetString("Kernel_54");
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, this._LastEventNote);
      this.CheckAccess(ActionType.EditProperties);
      this.CheckChangeEnable("F_SHORT_NAME");
      SqlHelper.ValidateFieldLength(LocalizationHolder.rm.GetString("MDShortName"), value.Length, Consts.MaxShortNameLength);
      try
      {
        this.UserSession.DataManager.ExecuteNonQuery(sc_12586.ssp_appserver_12601(), this.UserSession.DataManager.Parameter("attrID", (object) this.AttributeID), this.UserSession.DataManager.Parameter("val", (object) value));
        this.UserSession.DBCache.ChangeTableValue("F_ATTRIBUTE_ID = " + this.AttributeID.ToString(), "IMS_ATTRIBUTES", "F_SHORT_NAME", (object) value, (IUserSession) this.UserSession);
        this.paramsTable[79] = (object) value;
      }
      catch (Exception ex)
      {
        this.CloseEvent(EventID, EventlogRecordType.Error, LocalizationHolder.rm.GetString("Kernel_55") + ex.Message);
        throw;
      }
    }
  }

  public string Alias
  {
    get => this.paramsTable[78].ToString();
    set
    {
      value = value.Trim();
      if (!(this.Alias != value))
        return;
      this._LastEventNote = value != string.Empty ? LocalizationHolder.rm.GetString("Kernel_56") + value : LocalizationHolder.rm.GetString("Kernel_57");
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, this._LastEventNote);
      this.CheckAccess(ActionType.EditProperties);
      this.CheckChangeEnable("F_ALIAS");
      try
      {
        if (value != string.Empty)
        {
          DataTable table = this.UserSession.DBCache.GetTable("IMS_ATTRIBUTES");
          lock (table)
          {
            bool caseSensitive = table.CaseSensitive;
            table.CaseSensitive = true;
            try
            {
              DataRow[] dataRowArray = table.Select($"F_ALIAS = {SqlHelper.QString(value)} AND F_ATTRIBUTE_ID <> {this.AttributeID.ToString()}");
              if (dataRowArray.Length != 0)
                throw new KernelException(string.Format(LocalizationHolder.rm.GetString(sc_12586.ssp_appserver_12602()), (object) value, (object) this.Name, (object) dataRowArray[0]["F_NAME"].ToString()));
            }
            finally
            {
              table.CaseSensitive = caseSensitive;
            }
          }
        }
        this.UserSession.DataManager.ExecuteNonQuery("UPDATE IMS_ATTRIBUTES SET F_ALIAS = :val WHERE F_ATTRIBUTE_ID = :attrID", this.UserSession.DataManager.Parameter("attrID", (object) this.AttributeID), this.UserSession.DataManager.Parameter(sc_12586.ssp_appserver_12603(), (object) value));
        this.UserSession.DBCache.ChangeTableValue("F_ATTRIBUTE_ID = " + this.AttributeID.ToString(), "IMS_ATTRIBUTES", "F_ALIAS", (object) value, (IUserSession) this.UserSession);
        this.paramsTable[78] = (object) value;
      }
      catch (Exception ex)
      {
        this.CloseEvent(EventID, EventlogRecordType.Error, LocalizationHolder.rm.GetString("Kernel_59") + ex.Message);
        throw;
      }
    }
  }

  public string Note
  {
    get => this.paramsTable[92].ToString();
    set
    {
      if (!(this.Note != value))
        return;
      this._LastEventNote = value != string.Empty ? LocalizationHolder.rm.GetString("Kernel_60") + value : LocalizationHolder.rm.GetString("Kernel_61");
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, this._LastEventNote);
      SqlHelper.ValidateFieldLength(LocalizationHolder.rm.GetString("MDNote"), value.Length, Consts.MaxStringSize);
      this.CheckAccess(ActionType.EditProperties);
      this.CheckChangeEnable("F_NOTE");
      try
      {
        this.UserSession.DataManager.ExecuteNonQuery(sc_12586.ssp_appserver_12604(), this.UserSession.DataManager.Parameter("attrID", (object) this.AttributeID), this.UserSession.DataManager.Parameter("val", (object) value));
        this.UserSession.DBCache.ChangeTableValue("F_ATTRIBUTE_ID = " + this.AttributeID.ToString(), "IMS_ATTRIBUTES", "F_NOTE", (object) value, (IUserSession) this.UserSession);
        this.paramsTable[92] = (object) value;
      }
      catch (Exception ex)
      {
        this.CloseEvent(EventID, EventlogRecordType.Error, LocalizationHolder.rm.GetString("Kernel_62") + ex.Message);
        throw;
      }
    }
  }

  public virtual string TypeCaption => AttributesTypeHelper.GetCaption(this.AttributeType);

  public bool IsCompatibleType(FieldTypes newType)
  {
    return newType == this.AttributeType || this._ConvertList.IndexOf(newType) >= 0;
  }

  protected virtual void ValidateChangeAttributeType(FieldTypes newType)
  {
    if (!this.IsCompatibleType(newType))
    {
      string str1 = string.Empty;
      if (this._ConvertList.Count > 0)
      {
        str1 = LocalizationHolder.rm.GetString("Kernel_63");
        for (int index = 0; index < this._ConvertList.Count; ++index)
        {
          string str2 = str1 + AttributesTypeHelper.GetCaption(this._ConvertList[index]);
          str1 = index != this._ConvertList.Count - 1 ? str2 + "," : str2 + ".";
        }
      }
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString(sc_12586.ssp_appserver_12605()), (object) this.TypeCaption, (object) AttributesTypeHelper.GetCaption(newType), (object) str1));
    }
  }

  public FieldTypes AttributeType
  {
    get => (FieldTypes) Convert.ToInt32(this.paramsTable[77]);
    set
    {
      if (this.AttributeType == value)
        return;
      this._LastEventNote = LocalizationHolder.rm.GetString("Kernel_870") + EnumDescConverter.GetEnumDescription((Enum) value);
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, this._LastEventNote);
      this.CheckAccess(ActionType.EditProperties);
      if (value == FieldTypes.ftExternalLink)
        throw new KernelException(sc_12586.ssp_appserver_12606());
      this.CheckChangeEnable("F_ATTRIBUTE_TYPE");
      (this.EventHelper as EventLogHelper).OnBeforeChangeAttributeDataType((IDBAttributeType) this, value, (IUserSession) this.UserSession);
      this.UserSession.StartTransaction();
      try
      {
        FieldTypes attributeType1 = this.AttributeType;
        string[] indexFieldNames = this.IndexFieldNames;
        if (this.MultipleValued == MultiValueModes.MultiValuesFromList || this.MultipleValued == MultiValueModes.SingleValueFromList)
          this.ConvertPossibleValues(value);
        this.ValidateChangeAttributeType(value);
        (this.EventHelper as EventLogHelper).OnChangeAttributeDataType((IDBAttributeType) this, value, (IUserSession) this.UserSession);
        OptimizationModes optimizationMode = this.OptimizationMode;
        this.OptimizationMode = OptimizationModes.Write;
        this.UserSession.DataManager.ExecuteNonQuery(sc_12586.ssp_appserver_12607(), this.UserSession.DataManager.Parameter("attrID", (object) this.AttributeID), this.UserSession.DataManager.Parameter("val", (object) (int) value));
        this.DefaultValue = (object) string.Empty;
        this.UserSession.DBCache.ChangeTableValue("F_ATTRIBUTE_ID = " + this.AttributeID.ToString(), "IMS_ATTRIBUTES", "F_ATTRIBUTE_TYPE", (object) (int) value, (IUserSession) this.UserSession);
        this.paramsTable[77] = (object) (int) value;
        DBAttributeType attributeType2 = this.UserSession.GetAttributeType(this.AttributeID) as DBAttributeType;
        attributeType2.OptimizationMode = optimizationMode;
        if (this.NeedRebuildView4ChangeAttrType(value))
          attributeType2.RestructViewsAfterChangeDataType(attributeType1, value, indexFieldNames);
        this.UserSession.Commit();
        this.Deleted = true;
      }
      catch (Exception ex)
      {
        this.UserSession.Rollback();
        this.CloseEvent(EventID, EventlogRecordType.Error, LocalizationHolder.rm.GetString("Kernel_65") + ex.Message);
        throw new KernelExceptionID(sc_12586.ssp_appserver_12608(1703743029), (object) ex.Message);
      }
    }
  }

  protected void ClearValidatingRule()
  {
    foreach (DataRow dataRow in this.UserSession.DBCache.GetTable("IMS_ATTR4OBJ_TYPES").Select("F_ATTRIBUTE_ID = " + this.AttributeID.ToString()))
    {
      if (dataRow["F_VALIDATION_RULE"] != null && dataRow["F_VALIDATION_RULE"].ToString() != string.Empty && Convert.ToInt32(dataRow["F_PUBLIC"]) != 2)
      {
        IDBObjectType objectType = this.UserSession.GetObjectType(Convert.ToInt32(dataRow["F_OBJECT_TYPE"]), false);
        if (objectType != null)
          objectType.Attributes.GetAttributeByID(this.AttributeID).ValidationRule = string.Empty;
      }
    }
    foreach (DataRow dataRow in this.UserSession.DBCache.GetTable("IMS_ATTR4RELATION_TYPES").Select("F_ATTRIBUTE_ID = " + this.AttributeID.ToString()))
    {
      if (dataRow["F_VALIDATION_RULE"] != null && dataRow["F_VALIDATION_RULE"].ToString() != string.Empty)
      {
        IDBRelationType relationType = this.UserSession.GetRelationType(Convert.ToInt32(dataRow["F_RELATION_TYPE"]), false);
        if (relationType != null)
          relationType.Attributes.GetAttributeByID(this.AttributeID).ValidationRule = string.Empty;
      }
    }
  }

  protected virtual bool NeedRebuildView4ChangeAttrType(FieldTypes newType) => true;

  private void ConvertPossibleValues(FieldTypes value)
  {
    if (!this.CanStorePossibleValues || this.GetPossibleValues().Rows.Count <= 0)
      return;
    string empty1 = string.Empty;
    string empty2 = string.Empty;
    string empty3 = string.Empty;
    List<FieldTypes> convertList = new List<FieldTypes>();
    RelationalOperators[] enabledOperators = (RelationalOperators[]) null;
    bool computableAttribute = false;
    AttributeCacheHelper.GetAttributeTypeValues(value, this._AttributeID, ref empty1, ref empty2, ref convertList, ref enabledOperators, ref computableAttribute, ref empty3);
    if (!(this._PossibleValueFieldName != empty3))
      return;
    this.DoConvertPossibleValues(value, empty1);
    this.UserSession.DBCache.ReloadTables((IUserSession) this.UserSession, this.UserSession.DataManager, "IMS_POSSIBLE_VALUES");
  }

  protected virtual void DoConvertPossibleValues(FieldTypes value, string newFieldName)
  {
    IDbManager dataManager = this.UserSession.DataManager;
    try
    {
      dataManager.ExecuteNonQuery(string.Format(sc_12586.ssp_appserver_12609(), (object) newFieldName, (object) this.PossibleValueFieldName), dataManager.Parameter("attrID", (object) this.AttributeID));
    }
    catch
    {
      dataManager.ExecuteNonQuery(sc_12586.ssp_appserver_12610(), dataManager.Parameter("attrID", (object) this.AttributeID));
    }
  }

  public virtual void ValidateDefaultValue(object newValue)
  {
    if (newValue != null && newValue.ToString() != string.Empty)
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString(sc_12586.ssp_appserver_12611()), (object) this.TypeCaption));
  }

  public virtual object DefaultValue
  {
    get => this.paramsTable[104];
    set
    {
      if (this.CompareValues(this.DefaultValue, value))
        return;
      string empty = string.Empty;
      if (this.DefaultValue != null)
        empty = Convert.ToString(this.DefaultValue, (IFormatProvider) CultureInfo.InvariantCulture);
      string str = empty;
      this._LastEventNote = value != null ? LocalizationHolder.rm.GetString("Kernel_67") + value : LocalizationHolder.rm.GetString("Kernel_68");
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, this._LastEventNote);
      this.CheckAccess(ActionType.EditProperties);
      this.CheckChangeEnable("F_DEFAULT_VALUE");
      try
      {
        this.ValidateDefaultValue(value);
        if (value != null && value.ToString() != string.Empty && (this.MultipleValued == MultiValueModes.MultiValuesFromList || this.MultipleValued == MultiValueModes.SingleValueFromList))
        {
          object[] possibleValuesArray = this.GetPossibleValuesArray();
          bool flag = false;
          foreach (object obj in possibleValuesArray)
          {
            if (Convert.ToString(obj, (IFormatProvider) CultureInfo.InvariantCulture) == Convert.ToString(value, (IFormatProvider) CultureInfo.InvariantCulture))
            {
              flag = true;
              break;
            }
          }
          if (!flag)
            throw new KernelExceptionID(sc_12586.ssp_appserver_12612(964904920), (object) this.Name, value);
        }
        string newValue = value != null ? Convert.ToString(value, (IFormatProvider) CultureInfo.InvariantCulture) : string.Empty;
        this.UserSession.DataManager.ExecuteNonQuery("UPDATE IMS_ATTRIBUTES SET F_DEFAULT_VALUE = :val WHERE F_ATTRIBUTE_ID = :attrID", this.UserSession.DataManager.Parameter("attrID", (object) this.AttributeID), this.UserSession.DataManager.Parameter(sc_12586.ssp_appserver_12613(), (object) newValue));
        this.UserSession.DBCache.ChangeTableValue("F_ATTRIBUTE_ID = " + this.AttributeID.ToString(), "IMS_ATTRIBUTES", "F_DEFAULT_VALUE", (object) newValue, (IUserSession) this.UserSession);
        this.paramsTable[104] = (object) newValue;
        if (value == null)
          return;
        foreach (DataRow dataRow in this.UserSession.DBCache.GetTable("IMS_ATTR4OBJ_TYPES").Select($"F_ATTRIBUTE_ID = {this.AttributeID} AND F_PUBLIC <> {2}"))
        {
          IDBAttributeType4 attributeById = this.UserSession.GetObjectType(Convert.ToInt32(dataRow["F_OBJECT_TYPE"])).Attributes.GetAttributeByID(this.AttributeID);
          if (attributeById.DefaultValue == null)
          {
            if (str == string.Empty)
              attributeById.DefaultValue = value;
          }
          else if (str == Convert.ToString(attributeById.DefaultValue, (IFormatProvider) CultureInfo.InvariantCulture))
            attributeById.DefaultValue = value;
        }
        foreach (DataRow dataRow in this.UserSession.DBCache.GetTable("IMS_ATTR4RELATION_TYPES").Select("F_ATTRIBUTE_ID = " + this.AttributeID.ToString()))
        {
          IDBAttributeType4 attributeById = this.UserSession.GetRelationType(Convert.ToInt32(dataRow["F_RELATION_TYPE"])).Attributes.GetAttributeByID(this.AttributeID);
          if (attributeById.DefaultValue == null)
          {
            if (str == string.Empty)
              attributeById.DefaultValue = value;
          }
          else if (str == Convert.ToString(attributeById.DefaultValue, (IFormatProvider) CultureInfo.InvariantCulture))
            attributeById.DefaultValue = value;
        }
      }
      catch (Exception ex)
      {
        this.CloseEvent(EventID, EventlogRecordType.Error, LocalizationHolder.rm.GetString("Kernel_69") + ex.Message);
        throw;
      }
    }
  }

  private DataTable CalculateNotPossibleValues(string tblName)
  {
    string str1 = !(tblName == "IMS_RELATION_ATTRS") ? "F_OBJECT_ID" : "F_PRJLINK_ID";
    string str2 = string.Empty;
    if (this.PossibleValueFieldName == "F_STRING_VALUE")
      str2 = "AND (A.F_STRING_VALUE <> '') ";
    return this.UserSession.DataManager.ExecuteDataTable(string.Format("SELECT DISTINCT {4} FROM {2} A WHERE (A.F_ATTRIBUTE_ID = {0}) AND (A.{1} IS NOT NULL) {3}AND (NOT EXISTS(SELECT * FROM IMS_POSSIBLE_VALUES B WHERE B.F_ATTRIBUTE_ID = {0} AND B.{1} = A.{1}))", (object) this.AttributeID, (object) this.PossibleValueFieldName, (object) tblName, (object) str2, (object) str1));
  }

  private void ThrowNotPossibleValueObjectFoundException(DataTable tbl)
  {
    long[] objectsID = new long[tbl.Rows.Count];
    for (int index = 0; index < tbl.Rows.Count; ++index)
      objectsID[index] = Convert.ToInt64(tbl.Rows[index][0]);
    throw new ObjectsFoundException(string.Format(sc_12586.ssp_appserver_12614(), (object) this.Name, (object) tbl.Rows.Count), $"Объекты с недопустимыми значениями атрибута '{this.Name}':", objectsID);
  }

  private void ThrowMultipleValuedObjectFoundException(DataTable tbl)
  {
    long[] objectsID = new long[tbl.Rows.Count];
    for (int index = 0; index < tbl.Rows.Count; ++index)
      objectsID[index] = Convert.ToInt64(tbl.Rows[index][0]);
    throw new ObjectsFoundException(string.Format(sc_12586.ssp_appserver_12615(), (object) this.Name, (object) tbl.Rows.Count), $"Объекты с несколькими значениями атрибута '{this.Name}':", objectsID);
  }

  public MultiValueModes MultipleValued
  {
    get => (MultiValueModes) Convert.ToInt32(this.paramsTable[58]);
    set
    {
      if (this.MultipleValued == value)
        return;
      this._LastEventNote = LocalizationHolder.rm.GetString("Kernel_70") + MultiValueModesHelper.GetCaption(value);
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, this._LastEventNote);
      this.CheckAccess(ActionType.EditProperties);
      this.CheckChangeEnable("F_MULTIPLE_VALUED");
      try
      {
        if (value == MultiValueModes.MultiValuesFromList || value == MultiValueModes.SingleValueFromList)
        {
          if (!this._CanStorePossibleValues)
            throw new KernelException(string.Format(LocalizationHolder.rm.GetString(sc_12586.ssp_appserver_12616()), (object) this.TypeCaption));
          if (this.MultipleValued == MultiValueModes.MultiValues || this.MultipleValued == MultiValueModes.SingleValue)
          {
            DataTable notPossibleValues1 = this.CalculateNotPossibleValues("IMS_OBJECT_ATTRS");
            if (notPossibleValues1.Rows.Count > 0)
              this.ThrowNotPossibleValueObjectFoundException(notPossibleValues1);
            DataTable notPossibleValues2 = this.CalculateNotPossibleValues("IMS_RELATION_ATTRS");
            if (notPossibleValues2.Rows.Count > 0)
            {
              long[] relationsID = new long[notPossibleValues2.Rows.Count];
              for (int index = 0; index < notPossibleValues2.Rows.Count; ++index)
                relationsID[index] = Convert.ToInt64(notPossibleValues2.Rows[index][0]);
              throw new RelationsFoundException(string.Format(sc_12586.ssp_appserver_12617(), (object) this.Name, (object) notPossibleValues2.Rows.Count), $"Связи с недопустимыми значениями атрибута '{this.Name}':", relationsID);
            }
            DataTable table = this.UserSession.DBCache.GetTable("IMS_OBJECT_TYPES");
            for (int index = 0; index < table.Rows.Count; ++index)
            {
              if ((Convert.ToInt32(table.Rows[index]["F_OPTIONS"]) & 16 /*0x10*/) == 16 /*0x10*/)
              {
                IDBObjectType objectType = this.UserSession.GetObjectType(Convert.ToInt32(table.Rows[index]["F_OBJECT_TYPE"]));
                if (objectType.AnyAttributes || objectType.GetAttributeType(this.AttributeID) != null)
                {
                  DataTable notPossibleValues3 = this.CalculateNotPossibleValues((objectType as DBObjectType).AttributesTableName);
                  if (notPossibleValues3.Rows.Count > 0)
                    this.ThrowNotPossibleValueObjectFoundException(notPossibleValues3);
                }
              }
            }
          }
        }
        if ((value == MultiValueModes.SingleValue || value == MultiValueModes.SingleValueFromList) && (this.MultipleValued == MultiValueModes.MultiValues || this.MultipleValued == MultiValueModes.MultiValuesFromList))
        {
          DataTable tbl1 = this.UserSession.DataManager.ExecuteDataTable(sc_12586.ssp_appserver_12619(), this.UserSession.DataManager.Parameter("attrID", (object) this.AttributeID));
          if (tbl1.Rows.Count > 0)
            this.ThrowMultipleValuedObjectFoundException(tbl1);
          DataTable dataTable = this.UserSession.DataManager.ExecuteDataTable(sc_12586.ssp_appserver_12620(), this.UserSession.DataManager.Parameter("attrID", (object) this.AttributeID));
          if (dataTable.Rows.Count > 0)
          {
            long[] relationsID = new long[dataTable.Rows.Count];
            for (int index = 0; index < dataTable.Rows.Count; ++index)
              relationsID[index] = Convert.ToInt64(dataTable.Rows[index][0]);
            throw new RelationsFoundException(string.Format(sc_12586.ssp_appserver_12621(), (object) this.Name, (object) dataTable.Rows.Count), $"Связи с несколькими значениями атрибута '{this.Name}':", relationsID);
          }
          DataTable table = this.UserSession.DBCache.GetTable("IMS_OBJECT_TYPES");
          for (int index = 0; index < table.Rows.Count; ++index)
          {
            if ((Convert.ToInt32(table.Rows[index]["F_OPTIONS"]) & 16 /*0x10*/) == 16 /*0x10*/)
            {
              IDBObjectType objectType = this.UserSession.GetObjectType(Convert.ToInt32(table.Rows[index]["F_OBJECT_TYPE"]));
              if (objectType.AnyAttributes || objectType.GetAttributeType(this.AttributeID) != null)
              {
                DataTable tbl2 = this.UserSession.DataManager.ExecuteDataTable($"SELECT DISTINCT F_OBJECT_ID FROM {(objectType as DBObjectType).AttributesTableName} WHERE F_ATTRIBUTE_ID = :attrID AND F_INLIST_ID > 0", this.UserSession.DataManager.Parameter("attrID", (object) this.AttributeID));
                if (tbl2.Rows.Count > 0)
                  this.ThrowMultipleValuedObjectFoundException(tbl2);
              }
            }
          }
        }
        this.UserSession.DataManager.ExecuteNonQuery(sc_12586.ssp_appserver_12623(), this.UserSession.DataManager.Parameter("val", (object) Convert.ToInt32((object) value)), this.UserSession.DataManager.Parameter("attrID", (object) this.AttributeID));
        this.UserSession.DBCache.ChangeTableValue("F_ATTRIBUTE_ID = " + this.AttributeID.ToString(), "IMS_ATTRIBUTES", "F_MULTIPLE_VALUED", (object) Convert.ToInt32((object) value), (IUserSession) this.UserSession);
        this.paramsTable[58] = (object) Convert.ToInt32((object) value);
        if (value != MultiValueModes.MultiValues && value != MultiValueModes.SingleValue || this.GetPossibleValuesRows().Length == 0)
          return;
        this.UserSession.DataManager.ExecuteNonQuery("DELETE FROM IMS_POSSIBLE_VALUES WHERE F_ATTRIBUTE_ID = :attrID", this.UserSession.DataManager.Parameter("attrID", (object) this.AttributeID));
        this.UserSession.DBCache.ReloadTables((IUserSession) this.UserSession, this.UserSession.DataManager, "IMS_POSSIBLE_VALUES");
      }
      catch (Exception ex)
      {
        this.CloseEvent(EventID, EventlogRecordType.Error, LocalizationHolder.rm.GetString("Kernel_74") + ex.Message);
        throw;
      }
    }
  }

  private void RecomputeObjectAttribute(DataTable tbl)
  {
    foreach (DataRow row in (InternalDataCollectionBase) tbl.Rows)
    {
      if (this.UserSession.GetObject(Convert.ToInt64(row[0])).Attributes.FindByID(this.AttributeID) is DBAttribute byId)
        byId.Compute(false);
    }
  }

  internal void RecomputeValues(int objectTypeID, int relationTypeID)
  {
    IDbManager dataManager = this.UserSession.DataManager;
    string str1 = string.Empty;
    string str2 = "IMS_OBJECT_ATTRS";
    if (objectTypeID > -1)
    {
      IDBObjectType objectType = this.UserSession.GetObjectType(objectTypeID);
      if (objectType.IsLocalType)
        str2 = (objectType as DBObjectType).AttributesTableName;
      else
        str1 = " AND O.F_OBJECT_TYPE = " + objectTypeID.ToString();
    }
    else
      str1 = sc_12586.ssp_appserver_12624();
    this.RecomputeObjectAttribute(dataManager.ExecuteDataTable(string.Format("SELECT A.F_OBJECT_ID FROM {3} A, IMS_OBJECTS O WHERE A.F_ATTRIBUTE_ID = {0} AND O.F_OBJECT_ID = A.F_OBJECT_ID{1} AND O.F_LEVEL_ID <> {2}", (object) this.AttributeID, (object) str1, (object) this.UserSession.IdentHelper.DeletedID, (object) str2)));
    if (objectTypeID > -1)
    {
      DataTable table = this.UserSession.DBCache.GetTable("IMS_OBJECT_TYPES");
      for (int index = 0; index < table.Rows.Count; ++index)
      {
        if ((Convert.ToInt32(table.Rows[index]["F_OPTIONS"]) & 16 /*0x10*/) == 16 /*0x10*/)
        {
          IDBObjectType objectType = this.UserSession.GetObjectType(Convert.ToInt32(table.Rows[index]["F_OBJECT_TYPE"]));
          if (objectType.AnyAttributes && objectType.GetAttributeType(this.AttributeID) == null)
            this.RecomputeObjectAttribute(dataManager.ExecuteDataTable(string.Format("SELECT A.F_OBJECT_ID FROM {1} A, IMS_OBJECTS O WHERE A.F_ATTRIBUTE_ID = {0} AND O.F_OBJECT_ID = A.F_OBJECT_ID AND O.F_LEVEL_ID <> {2}", (object) this.AttributeID, (object) (objectType as DBObjectType).AttributesTableName, (object) this.UserSession.IdentHelper.DeletedID)));
        }
      }
    }
    string str3 = relationTypeID <= -1 ? sc_12586.ssp_appserver_12625() : " AND R.F_RELATION_TYPE = " + relationTypeID.ToString();
    foreach (DataRow row in (InternalDataCollectionBase) dataManager.ExecuteDataTable(string.Format("SELECT A.F_PRJLINK_ID FROM IMS_RELATION_ATTRS A, IMS_RELATIONS R WHERE A.F_ATTRIBUTE_ID = {0} AND R.F_PRJLINK_ID = A.F_PRJLINK_ID{1} ", (object) this.AttributeID, (object) str3, (object) dataManager.DataProvider.Now)).Rows)
    {
      if (this.UserSession.GetRelation(Convert.ToInt64(row[0])).Attributes.FindByID(this.AttributeID) is DBAttribute byId)
        byId.Compute(false);
    }
  }

  internal void CheckJITValue(ComputeValueModes val)
  {
    if (val == ComputeValueModes.JITValue)
      throw new KernelException("Атрибуты, вычисляемые в момент чтения данных, более не поддерживаются системой. Используйте атрибуты, вычисляемые в момент изменения данных.");
  }

  public ComputeValueModes Computed
  {
    get => (ComputeValueModes) Convert.ToInt32(this.paramsTable[107]);
    set
    {
      if (this.Computed == value)
        return;
      ComputeValueModes computed = this.Computed;
      this._LastEventNote = LocalizationHolder.rm.GetString("Kernel_75") + ComputeValueModesHelper.GetCaption(value);
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, this._LastEventNote);
      this.CheckAccess(ActionType.EditProperties);
      this.CheckChangeEnable("F_COMPUTED");
      this.UserSession.StartTransaction();
      try
      {
        this.CheckJITValue(value);
        if ((value == ComputeValueModes.JITValue || value == ComputeValueModes.StoredValue || value == ComputeValueModes.IndexValue) && !this._ComputableAttribute)
          throw new KernelException(string.Format(LocalizationHolder.rm.GetString(sc_12586.ssp_appserver_12626()), (object) this.TypeCaption));
        this.UserSession.DataManager.ExecuteNonQuery(sc_12586.ssp_appserver_12627(), this.UserSession.DataManager.Parameter("attrID", (object) this.AttributeID), this.UserSession.DataManager.Parameter("val", (object) Convert.ToInt32((object) value)));
        this.UserSession.DBCache.ChangeTableValue("F_ATTRIBUTE_ID = " + this.AttributeID.ToString(), "IMS_ATTRIBUTES", "F_COMPUTED", (object) Convert.ToInt32((object) value), (IUserSession) this.UserSession);
        this.paramsTable[107] = (object) Convert.ToInt32((object) value);
        if (value == ComputeValueModes.StoredValue || value == ComputeValueModes.IndexValue)
          this.RecomputeValues(-1, -1);
        this.UserSession.Commit();
      }
      catch (Exception ex)
      {
        this.UserSession.Rollback();
        this.CloseEvent(EventID, EventlogRecordType.Error, LocalizationHolder.rm.GetString("Kernel_77") + ex.Message);
        this.UserSession.DBCache.ChangeTableValue("F_ATTRIBUTE_ID = " + this.AttributeID.ToString(), "IMS_ATTRIBUTES", "F_COMPUTED", (object) Convert.ToInt32((object) computed), (IUserSession) this.UserSession);
        this.paramsTable[107] = (object) Convert.ToInt32((object) computed);
        throw;
      }
    }
  }

  public bool IsContent
  {
    get => Convert.ToInt32(this.paramsTable[39]) == 1;
    set
    {
      if (this.IsContent == value)
        return;
      this._LastEventNote = LocalizationHolder.rm.GetString("Kernel_78") + Consts.ConvertBoolToString(value);
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, this._LastEventNote);
      this.CheckAccess(ActionType.EditProperties);
      this.CheckChangeEnable("F_CONTENT");
      this.UserSession.StartTransaction();
      try
      {
        this.UserSession.DataManager.ExecuteNonQuery(sc_12586.ssp_appserver_12628(), this.UserSession.DataManager.Parameter("val", (object) Convert.ToInt32(value ? 1 : 0)), this.UserSession.DataManager.Parameter("attrID", (object) this.AttributeID));
        this.UserSession.DBCache.ChangeTableValue("F_ATTRIBUTE_ID = " + this.AttributeID.ToString(), "IMS_ATTRIBUTES", "F_CONTENT", (object) (value ? 1 : 0), (IUserSession) this.UserSession);
        this.paramsTable[39] = (object) (value ? 1 : 0);
        this.UserSession.Commit();
      }
      catch (Exception ex)
      {
        this.UserSession.Rollback();
        this.CloseEvent(EventID, EventlogRecordType.Error, ex.Message);
        this.UserSession.DBCache.ChangeTableValue("F_ATTRIBUTE_ID = " + this.AttributeID.ToString(), "IMS_ATTRIBUTES", "F_CONTENT", (object) Convert.ToInt32(!value), (IUserSession) this.UserSession);
        this.paramsTable[39] = (object) Convert.ToInt32(!value);
        throw;
      }
    }
  }

  public UniqueValueModes UniqueMode
  {
    get => (UniqueValueModes) Convert.ToInt32(this.paramsTable[59]);
    set
    {
      if (this.UniqueMode == value)
        return;
      this._LastEventNote = LocalizationHolder.rm.GetString("Kernel_79") + UniqueValueModesHelper.GetCaption(value);
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, this._LastEventNote);
      this.CheckAccess(ActionType.EditProperties);
      this.CheckChangeEnable("F_UNIQUE");
      try
      {
        if (value != UniqueValueModes.NotUnique && !this._UniquedAttribute)
          throw new KernelException(string.Format(LocalizationHolder.rm.GetString(sc_12586.ssp_appserver_12629()), (object) this.TypeCaption));
        this.UserSession.DataManager.ExecuteNonQuery(sc_12586.ssp_appserver_12630(), this.UserSession.DataManager.Parameter("val", (object) Convert.ToInt32((object) value)), this.UserSession.DataManager.Parameter("attrID", (object) this.AttributeID));
        this.UserSession.DBCache.ChangeTableValue("F_ATTRIBUTE_ID = " + this.AttributeID.ToString(), "IMS_ATTRIBUTES", "F_UNIQUE", (object) Convert.ToInt32((object) value), (IUserSession) this.UserSession);
        this.paramsTable[59] = (object) Convert.ToInt32((object) value);
      }
      catch (Exception ex)
      {
        this.CloseEvent(EventID, EventlogRecordType.Error, LocalizationHolder.rm.GetString("Kernel_81") + ex.Message);
        throw;
      }
    }
  }

  protected virtual void ValidatePossibleValue(object possibleValue)
  {
    this.ValidateDefaultValue(possibleValue);
  }

  private bool ComparePossibleValues(object value1, object value2, string fldName)
  {
    if (value1 != DBNull.Value && value2 != DBNull.Value)
    {
      switch (fldName)
      {
        case "F_INTEGER_VALUE":
          value1 = (object) Convert.ToInt64(value1);
          value2 = (object) Convert.ToInt64(value2);
          break;
        case "F_DOUBLE_VALUE":
          value1 = (object) Convert.ToDouble(value1);
          value2 = (object) Convert.ToDouble(value2);
          break;
      }
    }
    return value1.Equals(value2);
  }

  public void SetNewPossibleValues(DataTable valuesTable)
  {
    this.SetPossibleValuesFromScript(valuesTable);
  }

  internal void SetPossibleValuesFromScript(DataTable valuesTable)
  {
    DataTable possibleValues = this.GetPossibleValues();
    valuesTable.Columns.Add("F_OID", typeof (int));
    for (int index1 = 0; index1 < valuesTable.Rows.Count; ++index1)
    {
      bool flag = false;
      for (int index2 = 0; index2 < possibleValues.Rows.Count; ++index2)
      {
        if (this.ComparePossibleValues(valuesTable.Rows[index1][1], possibleValues.Rows[index2][1], possibleValues.Columns[1].ColumnName))
        {
          valuesTable.Rows[index1][valuesTable.Columns.Count - 1] = possibleValues.Rows[index2][0];
          flag = true;
          break;
        }
      }
      if (!flag)
      {
        valuesTable.Rows[index1][0] = (object) DBNull.Value;
        valuesTable.Rows[index1][valuesTable.Columns.Count - 1] = (object) DBNull.Value;
      }
    }
    valuesTable.AcceptChanges();
    this.SetPossibleValues(valuesTable, -1, -1);
  }

  public virtual void SetPossibleValues(DataTable valuesTable, int objectType, int relationType)
  {
    if (valuesTable == null)
      throw new KernelException("valuesTable == null");
    IDbManager dataManager = this.UserSession.DataManager;
    if (valuesTable.Rows.Count == 0 && valuesTable.Columns.IndexOf("F_OID") < 0)
      valuesTable.Columns.Add("F_OID", typeof (int));
    object extendedProperty = valuesTable.ExtendedProperties[(object) "modify_date"];
    if (extendedProperty != null)
    {
      DateTime second1 = Convert.ToDateTime(extendedProperty, (IFormatProvider) CultureInfo.InvariantCulture).TruncateToSecond();
      DateTime second2 = Convert.ToDateTime(dataManager.ExecuteScalar("SELECT F_MODIFY_DATE FROM IMS_METADATA WHERE F_TABLE_NAME = 'IMS_POSSIBLE_VALUES'")).TruncateToSecond();
      if (!second1.Equals(second2))
        throw new KernelException("Попытка выполнить изменение списка допустимых значений атрибута с устаревшими метаданными. Перегрузите клиентское приложение и повторите попытку.");
      DataRow[] dataRowArray = this.UserSession.DBCache.GetTable("IMS_METADATA").Select("F_TABLE_NAME = 'IMS_POSSIBLE_VALUES'");
      if (dataRowArray.Length != 0 && Convert.ToDateTime(dataRowArray[0]["F_MODIFY_DATE"]).TruncateToSecond() != second2)
        this.UserSession.DBCache.ReloadTables((IUserSession) this.UserSession, dataManager, "IMS_POSSIBLE_VALUES");
    }
    string str1 = string.Empty;
    if (objectType > -1)
    {
      IDBObjectType objectType1 = this.UserSession.GetObjectType(objectType);
      str1 = string.Format(LocalizationHolder.rm.GetString("Kernel_82"), (object) objectType1.ObjectTypeName);
    }
    if (relationType > -1)
    {
      IDBRelationType relationType1 = this.UserSession.GetRelationType(relationType);
      str1 = string.Format(LocalizationHolder.rm.GetString("Kernel_871"), (object) relationType1.Description);
    }
    DataTable possibleValues = this.GetPossibleValues(objectType, relationType);
    bool flag1 = false;
    int columnIndex = valuesTable.Columns.IndexOf("F_OID");
    if (valuesTable.Rows.Count == possibleValues.Rows.Count && possibleValues.Columns.Contains(this.PossibleValueFieldName))
    {
      for (int index = 0; index < valuesTable.Rows.Count; ++index)
      {
        if (!AttributesTypeHelper.EqualValues(possibleValues.Rows[index][this.PossibleValueFieldName], valuesTable.Rows[index][this.PossibleValueFieldName], this.PossibleValueFieldName) || !AttributesTypeHelper.EqualValues(possibleValues.Rows[index]["F_DESCRIPTION"], valuesTable.Rows[index]["F_DESCRIPTION"], "F_STRING_VALUE"))
        {
          flag1 = true;
          break;
        }
      }
    }
    else
      flag1 = true;
    if (!flag1)
    {
      if (columnIndex > 0)
      {
        for (int index = 0; index < valuesTable.Rows.Count; ++index)
        {
          if (valuesTable.Rows[index][0] == DBNull.Value || valuesTable.Rows[index][columnIndex] == DBNull.Value)
          {
            flag1 = true;
            break;
          }
          if (!valuesTable.Rows[index][columnIndex].Equals(valuesTable.Rows[index][0]))
          {
            flag1 = true;
            break;
          }
        }
      }
      if (!flag1)
        return;
    }
    if (columnIndex < 0 && possibleValues.Rows.Count > 0)
      throw new KernelException(string.Format(sc_12586.ssp_appserver_12631(), (object) this.Name));
    long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, LocalizationHolder.rm.GetString("Kernel_83") + str1);
    this.CheckAccess(ActionType.EditProperties);
    if (!this.UserSession.CanChangeObjectElement(3, (object) this.AttributeID, ObligatoryElementKeys.GetKeyForObjectProperty("F_POSSIBLE_VALUES")))
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_906"), (object) LocalizationHolder.rm.GetString("Kernel_907")));
    this.UserSession.StartTransaction();
    try
    {
      IDbDataParameter dbDataParameter1 = dataManager.Parameter("attrID", (object) this.AttributeID);
      for (int index1 = 0; index1 < possibleValues.Rows.Count; ++index1)
      {
        bool flag2 = true;
        for (int index2 = 0; index2 < valuesTable.Rows.Count; ++index2)
        {
          if (valuesTable.Rows[index2][columnIndex] != DBNull.Value && CompareValuesHelper.CompareIntValues(valuesTable.Rows[index2][columnIndex], possibleValues.Rows[index1][0]))
          {
            flag2 = false;
            break;
          }
        }
        if (flag2)
        {
          for (int index3 = 0; index3 < valuesTable.Rows.Count; ++index3)
          {
            if (this.CompareValues(possibleValues.Rows[index1][1], valuesTable.Rows[index3][1]))
            {
              flag2 = false;
              break;
            }
          }
        }
        if (flag2)
        {
          this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.Information, "Удаляется допустимое значение: " + possibleValues.Rows[index1][1].ToString());
          (this.EventHelper as EventLogHelper).OnDeleteAttributePossibleValue((IDBAttributeType) this, possibleValues.Rows[index1][1]);
          IDbDataParameter dbDataParameter2 = dataManager.Parameter("val1", possibleValues.Rows[index1][1]);
          List<string> objectAttrsTables = this.UserSession.DBCache.GetObjectAttrsTables();
          List<long> longList = new List<long>();
          for (int index4 = 0; index4 < objectAttrsTables.Count; ++index4)
          {
            DataTable dataTable = dataManager.ExecuteDataTable(string.Format("SELECT DISTINCT F_OBJECT_ID FROM {2} WHERE F_ATTRIBUTE_ID = :attrID AND {0} = :val1 AND EXISTS(SELECT * FROM IMS_OBJECTS WHERE IMS_OBJECTS.F_OBJECT_ID = {2}.F_OBJECT_ID AND F_LEVEL_ID <> {1})", (object) this.PossibleValueFieldName, (object) this.UserSession.IdentHelper.DeletedID, (object) objectAttrsTables[index4]), dbDataParameter1, dbDataParameter2);
            for (int index5 = 0; index5 < dataTable.Rows.Count; ++index5)
              longList.Add(Convert.ToInt64(dataTable.Rows[index5][0]));
          }
          if (longList.Count > 0)
            throw new ObjectsFoundException($"Нельзя удалять допустимое значение '{possibleValues.Rows[index1][1]}', т.к. в базе данных существует {longList.Count} объект(ов) с таким значением атрибута '{this.Name}'.", string.Format(sc_12586.ssp_appserver_12632(), possibleValues.Rows[index1][1], (object) this.Name), longList.ToArray());
          DataTable dataTable1 = dataManager.ExecuteDataTable($"SELECT DISTINCT F_PRJLINK_ID FROM IMS_RELATION_ATTRS WHERE F_ATTRIBUTE_ID = :attrID AND {this.PossibleValueFieldName} = :val1", dbDataParameter1, dbDataParameter2);
          if (dataTable1.Rows.Count > 0)
          {
            long[] relationsID = new long[dataTable1.Rows.Count];
            for (int index6 = 0; index6 < dataTable1.Rows.Count; ++index6)
              relationsID[index6] = Convert.ToInt64(dataTable1.Rows[index6][0]);
            throw new RelationsFoundException($"Нельзя удалять допустимое значение '{possibleValues.Rows[index1][1]}', т.к. в базе данных существует {relationsID.Length} связи(ей) с таким значением атрибута '{this.Name}'.", string.Format(sc_12586.ssp_appserver_12633(), possibleValues.Rows[index1][1], (object) this.Name), relationsID);
          }
        }
      }
      dataManager.ExecuteNonQuery(string.Format(sc_12586.ssp_appserver_12634(), (object) objectType, (object) relationType), dbDataParameter1);
      string commandText = string.Format(sc_12586.ssp_appserver_12635(), (object) this.PossibleValueFieldName, (object) objectType, (object) relationType);
      int num = 0;
      IDbDataParameter dbDataParameter3 = dataManager.Parameter("id", (object) num);
      foreach (DataRow row in (InternalDataCollectionBase) valuesTable.Rows)
      {
        this.ValidatePossibleValue(row[1]);
        dbDataParameter3.Value = (object) num++;
        dataManager.ExecuteNonQuery(commandText, dbDataParameter1, dbDataParameter3, dataManager.Parameter("val", row[1]), dataManager.Parameter("descr", row[2]));
      }
      this.UserSession.DBCache.ReloadTables((IUserSession) this.UserSession, dataManager, "IMS_POSSIBLE_VALUES");
      if (possibleValues.Rows.Count > 0)
      {
        foreach (DataRow row1 in (InternalDataCollectionBase) valuesTable.Rows)
        {
          if (row1[columnIndex] != DBNull.Value)
          {
            Convert.ToInt32(row1[0]);
            int int32 = Convert.ToInt32(row1[columnIndex]);
            for (int index7 = 0; index7 < possibleValues.Rows.Count; ++index7)
            {
              if (Convert.ToInt32(possibleValues.Rows[index7][0]) == int32)
              {
                DataRow row2 = possibleValues.Rows[index7];
                if (!this.CompareValues(row2[1], row1[1]))
                {
                  this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.Information, $"Допустимое значение {row2[1]} меняется на значение {row1[1]}");
                  if (Convert.ToString(this.DefaultValue) == row2[1].ToString())
                    this.DefaultValue = row1[1];
                  this.UserSession.DBCache.EnterReadLocker();
                  try
                  {
                    foreach (DataRow dataRow in this.UserSession.DBCache.GetTable("IMS_ATTR4OBJ_TYPES").Select("F_ATTRIBUTE_ID = " + this.AttributeID.ToString()))
                    {
                      if (dataRow["F_DEFAULT_VALUE"].ToString() == row2[1].ToString() && Convert.ToInt32(dataRow["F_PUBLIC"]) != 2)
                        this.UserSession.GetObjectType(Convert.ToInt32(dataRow["F_OBJECT_TYPE"])).Attributes.GetAttributeByID(this.AttributeID).DefaultValue = row1[1];
                    }
                    foreach (DataRow dataRow in this.UserSession.DBCache.GetTable("IMS_ATTR4RELATION_TYPES").Select("F_ATTRIBUTE_ID = " + this.AttributeID.ToString()))
                    {
                      if (dataRow["F_DEFAULT_VALUE"].ToString() == row2[1].ToString())
                        this.UserSession.GetRelationType(Convert.ToInt32(dataRow["F_RELATION_TYPE"])).Attributes.GetAttributeByID(this.AttributeID).DefaultValue = row1[1];
                    }
                  }
                  finally
                  {
                    this.UserSession.DBCache.ExitReadLocker();
                  }
                  IDbDataParameter dbDataParameter4 = dataManager.Parameter("val", row2[1]);
                  IDBObject dbObject = (IDBObject) null;
                  List<string> objectAttrsTables = this.UserSession.DBCache.GetObjectAttrsTables();
                  for (int index8 = 0; index8 < objectAttrsTables.Count; ++index8)
                  {
                    foreach (DataRow row3 in (InternalDataCollectionBase) dataManager.ExecuteDataTable($"SELECT F_OBJECT_ID, F_INLIST_ID FROM {objectAttrsTables[index8]} WHERE F_ATTRIBUTE_ID = :attrID AND {this.PossibleValueFieldName} = :val ORDER BY F_OBJECT_ID", dbDataParameter1, dbDataParameter4).Rows)
                    {
                      long int64 = Convert.ToInt64(row3[0]);
                      if (dbObject == null || dbObject.ObjectID != int64)
                        dbObject = this.UserSession.GetObject(int64);
                      if (dbObject.GetAttributeByID(this.AttributeID) is DBAttribute attributeById)
                      {
                        attributeById.ValidatingOn = false;
                        attributeById.Index = Convert.ToInt32(row3[1]);
                        attributeById.Value = row1[1];
                      }
                    }
                  }
                  IDBRelation dbRelation = (IDBRelation) null;
                  IEnumerator enumerator = dataManager.ExecuteDataTable($"SELECT F_PRJLINK_ID, F_INLIST_ID FROM IMS_RELATION_ATTRS WHERE F_ATTRIBUTE_ID = :attrID AND {this.PossibleValueFieldName} = :val ORDER BY F_PRJLINK_ID", dbDataParameter1, dbDataParameter4).Rows.GetEnumerator();
                  try
                  {
                    while (enumerator.MoveNext())
                    {
                      DataRow current = (DataRow) enumerator.Current;
                      long int64 = Convert.ToInt64(current[0]);
                      if (dbRelation == null || dbRelation.RelationID != int64)
                        dbRelation = this.UserSession.GetRelation(int64);
                      if (dbRelation.GetAttributeByID(this.AttributeID) is DBAttribute attributeById)
                      {
                        attributeById.ValidatingOn = false;
                        attributeById.Index = Convert.ToInt32(current[1]);
                        attributeById.Value = row1[1];
                      }
                    }
                    break;
                  }
                  finally
                  {
                    if (enumerator is IDisposable disposable)
                      disposable.Dispose();
                  }
                }
              }
            }
          }
        }
      }
      this.UserSession.Commit();
    }
    catch (Exception ex)
    {
      string str2 = LocalizationHolder.rm.GetString("Kernel_84") + ex.Message;
      this.UserSession.Rollback();
      this.UserSession.DBCache.ReloadTables((IUserSession) this.UserSession, this.UserSession.DataManager, "IMS_POSSIBLE_VALUES");
      this.CloseEvent(EventID, EventlogRecordType.Error, str2);
      throw new KernelException(str2, ex);
    }
  }

  public virtual void SetPossibleValues(DataTable valuesTable)
  {
    this.SetPossibleValues(valuesTable, -1, -1);
  }

  internal DataRow[] GetPossibleValuesRows(int objectType, int relationType)
  {
    return this.UserSession.DBCache.GetTable("IMS_POSSIBLE_VALUES").Select($"F_ATTRIBUTE_ID = {this._AttributeID} AND F_OBJECT_TYPE = {objectType} AND F_RELATION_TYPE = {relationType}", "F_INLIST_ID");
  }

  public DataRow[] GetPossibleValuesRows() => this.GetPossibleValuesRows(-1, -1);

  public DataTable GetPossibleValues(int objectType, int relationType)
  {
    DataTable possibleValues = (this.UserSession.DBCache as CacheDataset).GetPossibleValuesTable(this._PossibleValueFieldName).Clone();
    foreach (DataRow possibleValuesRow in this.GetPossibleValuesRows(objectType, relationType))
      possibleValues.Rows.Add(possibleValuesRow["F_INLIST_ID"], possibleValuesRow[this.PossibleValueFieldName], possibleValuesRow["F_DESCRIPTION"]);
    possibleValues.AcceptChanges();
    return possibleValues;
  }

  internal object[] GetPossibleValuesArray(int objectType, int relationType)
  {
    DataTable possibleValues = this.GetPossibleValues(objectType, relationType);
    object[] possibleValuesArray = new object[possibleValues.Rows.Count];
    for (int index = 0; index < possibleValuesArray.Length; ++index)
      possibleValuesArray[index] = possibleValues.Rows[index][1];
    return possibleValuesArray;
  }

  public object[] GetPossibleValuesArray() => this.GetPossibleValuesArray(-1, -1);

  public virtual DataTable GetPossibleValues() => this.GetPossibleValues(-1, -1);

  internal void GetPossibleValueDescription(object val, ref string result)
  {
    string description = this.UserSession.DBCache.GetDescription(this.AttributeID, val);
    if (!(description != string.Empty))
      return;
    result = description;
  }

  protected void CheckMaxSize(long newvalue, long maxsize)
  {
    if (newvalue > maxsize)
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString(sc_12586.ssp_appserver_12636()), (object) newvalue, (object) this.Name, (object) this.AttributeType, (object) maxsize));
  }

  protected void CheckZeroSize(long newValue)
  {
    if (newValue < 1L)
      throw new KernelExceptionID(377, (object) this.Name);
  }

  public virtual void ValidateSizeType(long newValue)
  {
    if (newValue < 0L)
      throw new KernelException(LocalizationHolder.rm.GetString(sc_12586.ssp_appserver_12637()));
  }

  public long SizeType
  {
    get => Convert.ToInt64(this.paramsTable[105]);
    set
    {
      long int64 = Convert.ToInt64(this.paramsTable[105]);
      if (int64 == value)
        return;
      int attributeType = (int) this.AttributeType;
      IDbManager dataManager = this.UserSession.DataManager;
      this._LastEventNote = LocalizationHolder.rm.GetString("Kernel_87") + int64.ToString();
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, this._LastEventNote);
      this.CheckAccess(ActionType.EditProperties);
      if (this.AttributeType == FieldTypes.ftString)
      {
        if (!this.UserSession.CanChangeObjectElement(3, (object) this.AttributeID, ObligatoryElementKeys.GetKeyForObjectProperty("F_SIZE_TYPE")) && value < this.SizeType)
          throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_906"), (object) DataSetProcessor.GetCaption("F_SIZE_TYPE")));
      }
      else
        this.CheckChangeEnable("F_SIZE_TYPE");
      this.UserSession.StartTransaction();
      try
      {
        this.ValidateSizeType(value);
        dataManager.ExecuteNonQuery(sc_12586.ssp_appserver_12638(), this.UserSession.DataManager.Parameter("attrID", (object) this.AttributeID), this.UserSession.DataManager.Parameter("val", (object) value));
        this.paramsTable[105] = (object) value;
        this.UserSession.Commit();
        this.UserSession.DBCache.ChangeTableValue("F_ATTRIBUTE_ID = " + this._AttributeID.ToString(), "IMS_ATTRIBUTES", "F_SIZE_TYPE", (object) value, (IUserSession) this.UserSession);
      }
      catch (Exception ex)
      {
        this.UserSession.Rollback();
        this.CloseEvent(EventID, EventlogRecordType.Error, ex.Message);
        throw;
      }
    }
  }

  public virtual string SizeTypeDescription => this.SizeType.ToString();

  public virtual string DefaultValueDescription
  {
    get => this.DefaultValue == null ? string.Empty : this.DefaultValue.ToString();
  }

  public int[] GetRelatedFormulaAttributes()
  {
    DataRow[] dataRowArray = this.UserSession.DBCache.GetTable("IMS_FORMULA_ATTRS").Select(string.Format("F_FORMULA_ID = {0} AND F_OBJECT_TYPE = -1 AND F_RELATION_TYPE = -1 AND F_MODE_ID = " + Consts.Attribute4Formula.ToString(), (object) this.AttributeID));
    int[] formulaAttributes = new int[dataRowArray.Length];
    for (int index = 0; index < dataRowArray.Length; ++index)
      formulaAttributes[index] = Convert.ToInt32(dataRowArray[index]["F_ATTRIBUTE_ID"]);
    return formulaAttributes;
  }

  internal virtual void ValidateFormula(
    string newFormula,
    ArrayList enabledAttrs,
    ArrayList consistAttrs,
    int modeID)
  {
    ExpressionVariablesCollection variables;
    using (Parser parser = new Parser())
    {
      parser.AutoDetectVariables = true;
      parser.Validate = false;
      variables = parser.Parse(newFormula).Variables;
    }
    for (int index = 0; index < variables.Count; ++index)
    {
      if (!(variables[index].Name.ToUpper() == "VALUE"))
      {
        DataRow[] dataRowArray = this.UserSession.DBCache.GetTable("IMS_ATTRIBUTES").Select("F_NAME = " + SqlHelper.QString(variables[index].Name));
        int anAttributeType = dataRowArray.Length != 0 ? Convert.ToInt32(dataRowArray[0]["F_ATTRIBUTE_ID"]) : throw new KernelExceptionID(sc_12586.ssp_appserver_12639(612070729), (object) variables[index].Name.ToString());
        if (consistAttrs.IndexOf((object) anAttributeType) < 0)
        {
          if (anAttributeType == this.AttributeID && modeID != Consts.Attribute4ValidationRule)
            throw new KernelExceptionID(sc_12586.ssp_appserver_12640(943696966));
          DBAttributeType attributeType = this.UserSession.GetAttributeType(anAttributeType) as DBAttributeType;
          if (!attributeType.CanUseInFormula)
            throw new KernelExceptionID(sc_12586.ssp_appserver_12641(1547972868), (object) attributeType.Name, (object) attributeType.TypeCaption);
          consistAttrs.Add((object) anAttributeType);
        }
      }
    }
    if (enabledAttrs == null)
      return;
    foreach (int consistAttr in consistAttrs)
    {
      if (consistAttr > 0 && enabledAttrs.IndexOf((object) consistAttr) < 0)
        throw new KernelExceptionID(sc_12586.ssp_appserver_12642(1888357030), (object) this.UserSession.GetAttributeType(consistAttr).Name, (object) this.Name);
    }
  }

  internal string TransposeFormula(string formula, int modeID)
  {
    if (modeID == Consts.Attribute4ValidationRule && formula.Length > 0 && formula.ToUpper() == "NOT NULL")
      formula = "[Value] <> ''";
    return formula;
  }

  internal void SaveFormulaLinks(
    int objectTypeID,
    int relationTypeID,
    string newFormula,
    int modeID,
    bool reloadMetadata)
  {
    IDbManager dataManager = this.UserSession.DataManager;
    ArrayList consistAttrs = new ArrayList();
    ArrayList enabledAttrs = (ArrayList) null;
    if (newFormula.Trim() != string.Empty)
    {
      if (objectTypeID > 0)
      {
        IDBObjectType objectType = this.UserSession.GetObjectType(objectTypeID);
        if (!objectType.AnyAttributes)
        {
          DataTable dataTable = objectType.Attributes.Select(string.Empty);
          enabledAttrs = new ArrayList();
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
            enabledAttrs.Add((object) Convert.ToInt32(row["F_ATTRIBUTE_ID"]));
        }
      }
      else if (relationTypeID > 0)
      {
        DataTable dataTable = this.UserSession.GetRelationType(relationTypeID).Attributes.Select(string.Empty);
        enabledAttrs = new ArrayList();
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          enabledAttrs.Add((object) Convert.ToInt32(row["F_ATTRIBUTE_ID"]));
      }
      this.ValidateFormula(newFormula, enabledAttrs, consistAttrs, modeID);
      if (modeID != Consts.Attribute4ValidationRule)
      {
        foreach (int aID in consistAttrs)
          this.ValidateCycleFormula(aID, objectTypeID, relationTypeID, modeID);
      }
    }
    IDbDataParameter dbDataParameter1 = dataManager.Parameter("formulaID", (object) this.AttributeID);
    IDbDataParameter dbDataParameter2 = dataManager.Parameter("objTypeID", (object) objectTypeID);
    IDbDataParameter dbDataParameter3 = dataManager.Parameter("relTypeID", (object) relationTypeID);
    IDbDataParameter dbDataParameter4 = dataManager.Parameter(nameof (modeID), (object) modeID);
    dataManager.ExecuteNonQuery(sc_12586.ssp_appserver_12643(), dbDataParameter1, dbDataParameter2, dbDataParameter3, dbDataParameter4);
    string commandText = sc_12586.ssp_appserver_12644();
    IDbDataParameter dbDataParameter5 = dataManager.Parameter("val", (object) 0);
    foreach (int num in consistAttrs)
    {
      dbDataParameter5.Value = (object) num;
      dataManager.ExecuteNonQuery(commandText, dbDataParameter1, dbDataParameter2, dbDataParameter3, dbDataParameter4, dbDataParameter5);
    }
    if (!reloadMetadata)
      return;
    this.UserSession.DBCache.ReloadTables((IUserSession) this.UserSession, dataManager, "IMS_FORMULA_ATTRS");
  }

  private void ValidateCycleFormula(int aID, int objectTypeID, int relationTypeID, int modeID)
  {
    if (aID == this.AttributeID)
      throw new KernelExceptionID(sc_12586.ssp_appserver_12645(1041874427), (object) this.Name);
    DataTable table = this.UserSession.DBCache.GetTable("IMS_FORMULA_ATTRS");
    string filterExpression = $"F_FORMULA_ID = {aID} AND F_OBJECT_TYPE = {objectTypeID} AND F_RELATION_TYPE = {relationTypeID} AND F_MODE_ID = {modeID}";
    foreach (DataRow dataRow in table.Select(filterExpression))
      this.ValidateCycleFormula(Convert.ToInt32(dataRow["F_ATTRIBUTE_ID"]), objectTypeID, relationTypeID, modeID);
  }

  public string Formula
  {
    get => this.paramsTable[73].ToString();
    set
    {
      if (!(this.Formula != value))
        return;
      if (value == null)
        value = string.Empty;
      this._LastEventNote = LocalizationHolder.rm.GetString("Kernel_88") + this.Formula;
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, this._LastEventNote);
      this.CheckAccess(ActionType.EditProperties);
      this.CheckChangeEnable("F_FORMULA");
      this.UserSession.StartTransaction();
      try
      {
        value = this.TransposeFormula(value, Consts.Attribute4Formula);
        this.SaveFormulaLinks(-1, -1, value, Consts.Attribute4Formula, true);
        this.UserSession.DataManager.ExecuteNonQuery(sc_12586.ssp_appserver_12646(), this.UserSession.DataManager.Parameter("attrID", (object) this.AttributeID), this.UserSession.DataManager.Parameter("val", (object) value));
        this.UserSession.DBCache.ChangeTableValue("F_ATTRIBUTE_ID = " + this.AttributeID.ToString(), "IMS_ATTRIBUTES", "F_FORMULA", (object) value, (IUserSession) this.UserSession);
        this.paramsTable[73] = (object) value;
        if (this.Computed == ComputeValueModes.StoredValue || this.Computed == ComputeValueModes.IndexValue)
          this.RecomputeValues(-1, -1);
        this.UserSession.Commit();
      }
      catch (Exception ex)
      {
        this.UserSession.Rollback();
        string str = LocalizationHolder.rm.GetString("Kernel_89") + ex.Message;
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        if (!(ex is AccessDeniedException))
          throw new KernelException(str, ex);
        throw;
      }
    }
  }

  public Guid GUID
  {
    get => new Guid(this.paramsTable[76].ToString());
    set
    {
      if (!(value != this.GUID))
        return;
      this._LastEventNote = LocalizationHolder.rm.GetString("Kernel_90") + value.ToString();
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, this._LastEventNote);
      this.CheckAccess(ActionType.EditProperties);
      if (!this.UserSession.CanChangeObject(3, (object) this.AttributeID))
        throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_906"), (object) DataSetProcessor.GetCaption("F_GUID")));
      try
      {
        if (!this.UserSession.DeveloperMode)
          throw new KernelExceptionID(sc_12586.ssp_appserver_12647(664643064));
        this.UserSession.DataManager.ExecuteNonQuery("UPDATE IMS_ATTRIBUTES SET F_GUID = :val WHERE F_ATTRIBUTE_ID = :attrID", this.UserSession.DataManager.Parameter("attrID", (object) this.AttributeID), this.UserSession.DataManager.Parameter(sc_12586.ssp_appserver_12648(), (object) value.ToString()));
        this.UserSession.DBCache.ChangeTableValue("F_ATTRIBUTE_ID = " + this.AttributeID.ToString(), "IMS_ATTRIBUTES", "F_GUID", (object) value, (IUserSession) this.UserSession);
        this.paramsTable[76] = (object) value;
      }
      catch (Exception ex)
      {
        string str = LocalizationHolder.rm.GetString("Kernel_91") + ex.Message;
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        throw new KernelException(str, ex);
      }
    }
  }

  public bool IsSystemGUID => SystemGUIDs.IsSystemGUID(this.GUID);

  public string SubjectAreas
  {
    get => this.paramsTable[89].ToString();
    set
    {
      if (!(this.SubjectAreas != value))
        return;
      IDBSubjectAreaCollection subjectAreaCollection = this.UserSession.GetSubjectAreaCollection();
      this._LastEventNote = LocalizationHolder.rm.GetString("Kernel_92") + subjectAreaCollection.GetAreasCaption(value);
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, this._LastEventNote);
      this.CheckAccess(ActionType.EditProperties);
      this.CheckChangeEnable("F_AREA_ID");
      try
      {
        subjectAreaCollection.ValidateAriasString(value);
        this.UserSession.DataManager.ExecuteNonQuery(sc_12586.ssp_appserver_12649(), this.UserSession.DataManager.Parameter("attrID", (object) this.AttributeID), this.UserSession.DataManager.Parameter("val", (object) value));
        this.UserSession.DBCache.ChangeTableValue("F_ATTRIBUTE_ID = " + this.AttributeID.ToString(), "IMS_ATTRIBUTES", "F_AREA_ID", (object) value, (IUserSession) this.UserSession);
        this.paramsTable[89] = (object) value;
      }
      catch (Exception ex)
      {
        string str = LocalizationHolder.rm.GetString("Kernel_93") + ex.Message;
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        throw new KernelException(str, ex);
      }
    }
  }

  public string SubjectAreasCaption
  {
    get => this.UserSession.GetSubjectAreaCollection().GetAreasCaption(this.SubjectAreas);
  }

  public string LanguageID
  {
    get => this.paramsTable[69].ToString().TrimEnd();
    set
    {
      if (!(this.LanguageID != value))
        return;
      IDBLanguageType language = this.UserSession.GetLanguage(value);
      this._LastEventNote = LocalizationHolder.rm.GetString("Kernel_94") + language.LanguageName;
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, this._LastEventNote);
      this.CheckAccess(ActionType.EditProperties);
      this.CheckChangeEnable("F_LANGUAGE_ID");
      try
      {
        this.UserSession.DataManager.ExecuteNonQuery(sc_12586.ssp_appserver_12650(), this.UserSession.DataManager.Parameter("attrID", (object) this.AttributeID), this.UserSession.DataManager.Parameter("val", (object) value));
        this.UserSession.DBCache.ChangeTableValue("F_ATTRIBUTE_ID = " + this.AttributeID.ToString(), "IMS_ATTRIBUTES", "F_LANGUAGE_ID", (object) value, (IUserSession) this.UserSession);
        this.paramsTable[69] = (object) value;
      }
      catch (Exception ex)
      {
        string str = LocalizationHolder.rm.GetString("Kernel_95") + ex.Message;
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        throw new KernelException(str, ex);
      }
    }
  }

  public string LanguageName
  {
    get
    {
      return this.LanguageID == string.Empty ? string.Empty : this.UserSession.GetLanguage(this.LanguageID).LanguageName;
    }
  }

  public bool IsDefaultLanguage
  {
    get
    {
      return this.LanguageID == string.Empty || this.UserSession.GetLanguage(this.LanguageID).IsDefaultLanguage;
    }
  }

  public int[] GetGroupsList()
  {
    ArrayList arrayList = new ArrayList();
    foreach (DataRow dataRow in this.UserSession.DBCache.GetTable("IMS_ATTR_IN_GROUPS").Select("F_ATTRIBUTE_ID = " + this._AttributeID.ToString()))
      arrayList.Add((object) Convert.ToInt32(dataRow["F_GROUP_ID"]));
    return (int[]) arrayList.ToArray(typeof (int));
  }

  internal string GetLinksCaption(string fieldName)
  {
    StringBuilder stringBuilder = new StringBuilder(string.Empty);
    foreach (DataRow dataRow in this.UserSession.DBCache.GetTable("IMS_ATTRIBUTES").Select($"{fieldName} = {this.AttributeID.ToString()}"))
      stringBuilder.Append($"'{dataRow["F_NAME"]}', ");
    foreach (DataRow dataRow in this.UserSession.DBCache.GetTable("IMS_ATTR4OBJ_TYPES").Select($"{fieldName} = {this.AttributeID.ToString()}"))
      stringBuilder.AppendFormat("'{0}.{1}', ", (object) this.UserSession.GetObjectType(Convert.ToInt32(dataRow["F_OBJECT_TYPE"])).ObjectTypeName, (object) this.UserSession.GetAttributeType(Convert.ToInt32(dataRow["F_ATTRIBUTE_ID"])).Name);
    foreach (DataRow dataRow in this.UserSession.DBCache.GetTable("IMS_ATTR4RELATION_TYPES").Select($"{fieldName} = {this.AttributeID.ToString()}"))
      stringBuilder.AppendFormat("'{0}.{1}', ", (object) this.UserSession.GetRelationType(Convert.ToInt32(dataRow["F_RELATION_TYPE"])).Description, (object) this.UserSession.GetAttributeType(Convert.ToInt32(dataRow["F_ATTRIBUTE_ID"])).Name);
    if (stringBuilder.Length > 0)
      stringBuilder.Length -= 2;
    return stringBuilder.ToString();
  }

  public int Delete(long DeleteMode)
  {
    this.UserSession.ValidateSystemDelete((object) this, string.Format(LocalizationHolder.rm.GetString("Kernel_96"), (object) this.Name));
    IDbManager dataManager = this.UserSession.DataManager;
    IDbDataParameter dbDataParameter = dataManager.Parameter("at1", (object) this.AttributeID);
    long EventID = this.AddEvent(0L, ActionType.Delete, EventlogRecordType.AccessDenied);
    this.CheckAccess(ActionType.Delete);
    if (!this.UserSession.CanChangeObject(3, (object) this.AttributeID))
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_908"), (object) this.Name));
    this.UserSession.StartTransaction();
    try
    {
      (this.EventHelper as EventLogHelper).OnBeforeDeleteAttributeType((IDBAttributeType) this, (IUserSession) this.UserSession);
      this.UserSession.DBCache.ReloadOldTables(dataManager);
      int num = Convert.ToInt32(dataManager.ExecuteScalar(sc_12586.ssp_appserver_12651() + this._AttributeID.ToString())) <= 0 ? Convert.ToInt32(dataManager.ExecuteScalar(sc_12586.ssp_appserver_12653() + this._AttributeID.ToString())) : throw new KernelException(string.Format(LocalizationHolder.rm.GetString(sc_12586.ssp_appserver_12652()), (object) this.Name));
      if (num > 0)
        throw new KernelException(string.Format(LocalizationHolder.rm.GetString(sc_12586.ssp_appserver_12654()), (object) this.Name, (object) num));
      string linksCaption1 = this.GetLinksCaption("F_SOURCE_ID");
      if (linksCaption1 != string.Empty)
        throw new KernelExceptionID(sc_12586.ssp_appserver_12655(263242740), (object) this.Name, (object) linksCaption1);
      if (this.AttributeType == FieldTypes.ftObjectLink)
      {
        string linksCaption2 = this.GetLinksCaption("F_MASTER_ID");
        if (linksCaption2 != string.Empty)
          throw new KernelExceptionID(sc_12586.ssp_appserver_12656(1070638918), (object) this.Name, (object) linksCaption2);
      }
      object obj = dataManager.ExecuteScalar($"{sc_12586.ssp_appserver_12657()}{Convert.ToInt32((object) FieldTypes.ftObjectLink).ToString()} AND F_FORMULA = {SqlHelper.QString(this._AttributeID.ToString())}");
      if (obj != DBNull.Value)
      {
        IDBAttributeType attributeType = this.UserSession.GetAttributeType(Convert.ToInt32(obj));
        throw new KernelException(string.Format(LocalizationHolder.rm.GetString(sc_12586.ssp_appserver_12658()), (object) this.Name, (object) attributeType.Name));
      }
      if ((DeleteMode & (long) Consts.DeleteInstances) == 0L)
      {
        List<string> objectAttrsTables = this.UserSession.DBCache.GetObjectAttrsTables();
        DataTable toTable = (DataTable) null;
        for (int index = 0; index < objectAttrsTables.Count; ++index)
        {
          DataTable dataTable = dataManager.ExecuteDataTable($"SELECT DISTINCT F_OBJECT_ID FROM {objectAttrsTables[index]} WHERE F_ATTRIBUTE_ID = :at1", dbDataParameter);
          if (dataTable.Rows.Count > 0)
          {
            if (toTable == null)
              toTable = dataTable;
            else
              SqlHelper.AssignRows(toTable, (IEnumerable<DataRow>) dataTable.Select());
          }
        }
        if (toTable != null)
        {
          long[] objectsID = new long[toTable.Rows.Count];
          for (int index = 0; index < toTable.Rows.Count; ++index)
            objectsID[index] = Convert.ToInt64(toTable.Rows[index][0]);
          throw new ObjectsFoundException(string.Format(sc_12586.ssp_appserver_12659(), (object) this.Name, (object) toTable.Rows.Count), $"Объекты, у которых присутствует атрибут '{this.Name}':", objectsID);
        }
        DataTable dataTable1 = dataManager.ExecuteDataTable("SELECT F_PRJLINK_ID FROM IMS_RELATION_ATTRS WHERE F_ATTRIBUTE_ID = :at1 AND F_INLIST_ID = 0", dbDataParameter);
        if (dataTable1.Rows.Count > 0)
        {
          long[] relationsID = new long[dataTable1.Rows.Count];
          for (int index = 0; index < dataTable1.Rows.Count; ++index)
            relationsID[index] = Convert.ToInt64(dataTable1.Rows[index][0]);
          throw new RelationsFoundException(string.Format(sc_12586.ssp_appserver_12660(), (object) this.Name, (object) dataTable1.Rows.Count), $"Связи, у которых присутствует атрибут '{this.Name}':", relationsID);
        }
      }
      else
      {
        if (this.IsContent)
          throw new KernelExceptionID(sc_12586.ssp_appserver_12661(63832841), (object) this.Name);
        string commandText1 = sc_12586.ssp_appserver_12662();
        foreach (DataRow row in (InternalDataCollectionBase) dataManager.ExecuteDataTable(commandText1, dbDataParameter).Rows)
        {
          IDBRelation relation = this.UserSession.GetRelation(Convert.ToInt64(row[0]), false);
          if (relation != null)
          {
            IDBAttribute attributeById = relation.GetAttributeByID(this.AttributeID);
            if (attributeById != null)
              (attributeById as DBAttribute).Purge(false);
          }
          else
            dataManager.ExecuteNonQuery("DELETE FROM IMS_RELATION_ATTRS WHERE F_PRJLINK_ID = :relID AND F_ATTRIBUTE_ID = :at1", dataManager.Parameter("relID", (object) Convert.ToInt64(row[0])), dbDataParameter);
        }
        List<string> objectAttrsTables = this.UserSession.DBCache.GetObjectAttrsTables();
        for (int index = 0; index < objectAttrsTables.Count; ++index)
        {
          string commandText2 = $"SELECT DISTINCT A.F_OBJECT_ID FROM {objectAttrsTables[index]} A WHERE A.F_ATTRIBUTE_ID = :at1";
          foreach (DataRow row in (InternalDataCollectionBase) dataManager.ExecuteDataTable(commandText2, dataManager.Parameter("at1", (object) this.AttributeID)).Rows)
          {
            IDBObject dbObject = this.UserSession.GetObject(Convert.ToInt64(row[0]), false);
            if (dbObject != null)
            {
              IDBAttribute attributeById = dbObject.GetAttributeByID(this.AttributeID);
              if (attributeById != null)
                (attributeById as DBAttribute).Purge(false);
            }
            else
              dataManager.ExecuteNonQuery($"DELETE FROM {objectAttrsTables[index]} WHERE F_OBJECT_ID = :objID AND F_ATTRIBUTE_ID = :at1", dataManager.Parameter("objID", (object) Convert.ToInt64(row[0])), dbDataParameter);
          }
        }
        dataManager.ExecuteNonQuery("DELETE FROM IMS_OBJ_SNAPATTRS WHERE F_ATTRIBUTE_ID = :at1", dbDataParameter);
        dataManager.ExecuteNonQuery("DELETE FROM IMS_REL_SNAPATTRS WHERE F_ATTRIBUTE_ID = :at1", dbDataParameter);
      }
      foreach (int groups in this.GetGroupsList())
        this.UserSession.GetAttributesGroup(groups).ExcludeAttribute(this._AttributeID);
      dataManager.ExecuteNonQuery(sc_12586.ssp_appserver_12663() + this._AttributeID.ToString());
      dataManager.ExecuteNonQuery(sc_12586.ssp_appserver_12664() + this._AttributeID.ToString());
      this.UserSession.DBCache.DeleteRecords("IMS_ATTRIBUTES", "F_ATTRIBUTE_ID = " + this._AttributeID.ToString(), (IUserSession) this.UserSession);
      this.DeleteMDExtensions();
      this.UserSession.Commit();
      (this.UserSession.DBCache as CacheDataset).SetAttrProperties(new Attribute4ID(this.AttributeID, -1, -1), OptimizationModes.NotFound, AttributeOptions.None);
      if (this.OptimizationMode == OptimizationModes.Seek)
      {
        string[] indexFieldNames = this.IndexFieldNames;
        if (indexFieldNames != null)
        {
          foreach (string fldName in indexFieldNames)
          {
            try
            {
              dataManager.ExecuteNonQuery(dataManager.DataProvider.GetDropIndexSQL("IMS_OBJECTS_VIEW", fldName, SortOrders.ASC));
            }
            catch (Exception ex)
            {
              this.EventHelper.AddToTrace(string.Format(LocalizationHolder.rm.GetString("Kernel_102"), (object) dataManager.DataProvider.GetDropIndexSQL("IMS_OBJECTS_VIEW", fldName, SortOrders.ASC), (object) ex.Message), Consts.traceAlways, "sql_errors.log");
            }
            try
            {
              dataManager.ExecuteNonQuery(dataManager.DataProvider.GetDropIndexSQL("IMS_OBJECTS_VIEW", fldName, SortOrders.DESC));
            }
            catch (Exception ex)
            {
              this.EventHelper.AddToTrace(string.Format(LocalizationHolder.rm.GetString("Kernel_103"), (object) dataManager.DataProvider.GetDropIndexSQL("IMS_OBJECTS_VIEW", fldName, SortOrders.DESC), (object) ex.Message), Consts.traceAlways, "sql_errors.log");
            }
          }
        }
      }
      if (this.OptimizationMode != OptimizationModes.Write)
      {
        string[] fieldNames = this.FieldNames;
        if (fieldNames != null)
        {
          foreach (string columnName in fieldNames)
            dataManager.ExecuteNonQuery(dataManager.DataProvider.GetDropColumnsSQL("IMS_OBJECTS_VIEW", columnName));
        }
      }
      this.Deleted = true;
      (this.EventHelper as EventLogHelper).OnAfterDeleteAttributeType((IDBAttributeType) this, (IUserSession) this.UserSession);
    }
    catch (Exception ex)
    {
      this.UserSession.Rollback();
      if (ex.Message.IndexOf("FK_FORMULA_ATTRS2") >= 0)
      {
        DataTable table = this.UserSession.DBCache.GetTable("IMS_FORMULA_ATTRS");
        int index = this.AttributeID;
        string filterExpression = "F_ATTRIBUTE_ID = " + index.ToString();
        DataRow[] dataRowArray1 = table.Select(filterExpression);
        this.CloseEvent(EventID, EventlogRecordType.Error, string.Format(LocalizationHolder.rm.GetString("Kernel_104"), (object) this.Name));
        StringBuilder stringBuilder = new StringBuilder(string.Format(LocalizationHolder.rm.GetString("Kernel_105"), (object) this.Name));
        DataRow[] dataRowArray2 = dataRowArray1;
        for (index = 0; index < dataRowArray2.Length; ++index)
        {
          DataRow dataRow = dataRowArray2[index];
          stringBuilder.AppendFormat("'{0}'", (object) this.UserSession.GetAttributeType(Convert.ToInt32(dataRow["F_FORMULA_ID"])).Name);
          if (dataRow["F_OBJECT_TYPE"] != DBNull.Value)
          {
            int int32 = Convert.ToInt32(dataRow["F_OBJECT_TYPE"]);
            if (int32 > -1)
            {
              try
              {
                stringBuilder.AppendFormat(LocalizationHolder.rm.GetString("Kernel_106"), (object) this.UserSession.GetObjectType(int32).ObjectTypeName);
              }
              catch
              {
              }
            }
          }
          if (dataRow["F_RELATION_TYPE"] != DBNull.Value)
          {
            int int32 = Convert.ToInt32(dataRow["F_RELATION_TYPE"]);
            if (int32 > -1)
            {
              try
              {
                stringBuilder.AppendFormat(LocalizationHolder.rm.GetString("Kernel_872"), (object) this.UserSession.GetRelationType(int32).Description);
              }
              catch
              {
              }
            }
          }
          stringBuilder.Append("\n");
        }
        throw new KernelException(stringBuilder.ToString());
      }
      this.CloseEvent(EventID, EventlogRecordType.Error, string.Format(LocalizationHolder.rm.GetString("Kernel_107"), (object) this.Name, (object) ex.Message));
      throw;
    }
    return 0;
  }

  internal virtual string ColumnSQL => "F" + this.AttributeID.ToString();

  public virtual void ValidateAssign(IDBAttributeType source)
  {
    if (this.CompatibleTypes != null)
    {
      bool flag = false;
      for (int index = 0; index < this.CompatibleTypes.Length; ++index)
      {
        if (this.CompatibleTypes[index] == source.AttributeType)
        {
          flag = true;
          break;
        }
      }
      if (!flag)
        throw new KernelExceptionID(sc_12586.ssp_appserver_12665(194437189), (object) this.Name, (object) AttributesTypeHelper.GetCaption(source.AttributeType));
    }
    if ((this.MultipleValued == MultiValueModes.SingleValue || this.MultipleValued == MultiValueModes.SingleValueFromList) && (source.MultipleValued == MultiValueModes.MultiValues || source.MultipleValued == MultiValueModes.MultiValuesFromList))
      throw new KernelExceptionID(sc_12586.ssp_appserver_12666(1672519714), (object) this.Name, (object) source.Name);
    if ((this.MultipleValued == MultiValueModes.SingleValueFromList || this.MultipleValued == MultiValueModes.MultiValuesFromList) && (source.MultipleValued == MultiValueModes.MultiValues || source.MultipleValued == MultiValueModes.SingleValue))
      throw new KernelExceptionID(sc_12586.ssp_appserver_12667(164158673), (object) this.Name, (object) source.Name);
  }

  public int SourceAttributeID
  {
    get => Convert.ToInt32(this.paramsTable[173]);
    set
    {
      if (this.SourceAttributeID == value)
        return;
      string str = value != 0 ? this.UserSession.GetAttributeType(value, true).Name : LocalizationHolder.rm.GetString("Kernel_108");
      this._LastEventNote = string.Format(LocalizationHolder.rm.GetString("Kernel_109"), (object) str);
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, this._LastEventNote);
      this.CheckAccess(ActionType.EditProperties);
      this.CheckChangeEnable("F_SOURCE_ID");
      this.UserSession.StartTransaction();
      try
      {
        if (value != 0)
        {
          if (this.MasterAttributeID == 0)
            throw new KernelExceptionID(sc_12586.ssp_appserver_12668(633011741));
          IDBAttributeType attributeType = this.UserSession.GetAttributeType(value, true);
          if (attributeType.AttributeType != this.AttributeType)
            throw new KernelExceptionID(sc_12586.ssp_appserver_12669(930740366));
          this.ValidateAssign(attributeType);
        }
        this.UserSession.DataManager.ExecuteNonQuery(sc_12586.ssp_appserver_12670(), this.UserSession.DataManager.Parameter("attrID", (object) this.AttributeID), this.UserSession.DataManager.Parameter("val", (object) value));
        this.UserSession.DBCache.ChangeTableValue("F_ATTRIBUTE_ID = " + this.AttributeID.ToString(), "IMS_ATTRIBUTES", "F_SOURCE_ID", (object) value, (IUserSession) this.UserSession);
        this.paramsTable[173] = (object) value;
        this.UserSession.Commit();
      }
      catch (Exception ex)
      {
        this.UserSession.Rollback();
        this.CloseEvent(EventID, EventlogRecordType.Error, LocalizationHolder.rm.GetString("Kernel_110") + ex.Message);
        throw;
      }
    }
  }

  public int MasterAttributeID
  {
    get => Convert.ToInt32(this.paramsTable[172]);
    set
    {
      if (this.MasterAttributeID == value)
        return;
      string name;
      if (value == 0)
      {
        name = LocalizationHolder.rm.GetString("Kernel_111");
      }
      else
      {
        IDBAttributeType attributeType = this.UserSession.GetAttributeType(value, true);
        name = attributeType.Name;
        if (attributeType.MultipleValued == MultiValueModes.MultiValues || attributeType.MultipleValued == MultiValueModes.MultiValuesFromList)
          throw new KernelExceptionID(sc_12586.ssp_appserver_12671(933386191), (object) name);
      }
      this._LastEventNote = string.Format(LocalizationHolder.rm.GetString("Kernel_112"), (object) name);
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, this._LastEventNote);
      this.CheckAccess(ActionType.EditProperties);
      this.CheckChangeEnable("F_MASTER_ID");
      this.UserSession.StartTransaction();
      try
      {
        if (value != 0)
        {
          if (this.UserSession.GetAttributeType(value, true).AttributeType != FieldTypes.ftObjectLink)
            throw new KernelExceptionID(sc_12586.ssp_appserver_12672(1925357294));
        }
        else
          this.SourceAttributeID = 0;
        this.UserSession.DataManager.ExecuteNonQuery(sc_12586.ssp_appserver_12673(), this.UserSession.DataManager.Parameter("attrID", (object) this.AttributeID), this.UserSession.DataManager.Parameter("val", (object) value));
        this.UserSession.DBCache.ChangeTableValue("F_ATTRIBUTE_ID = " + this.AttributeID.ToString(), "IMS_ATTRIBUTES", "F_MASTER_ID", (object) value, (IUserSession) this.UserSession);
        this.paramsTable[172] = (object) value;
        this.UserSession.Commit();
      }
      catch (Exception ex)
      {
        this.UserSession.Rollback();
        this.CloseEvent(EventID, EventlogRecordType.Error, LocalizationHolder.rm.GetString("Kernel_113") + ex.Message);
        throw;
      }
    }
  }

  public int LevelID
  {
    get => Convert.ToInt32(this.paramsTable[72]);
    set
    {
      if (this.LevelID == value)
        return;
      string str1 = value != 0 ? this.UserSession.GetLifecycleLevel(value).LevelName : LocalizationHolder.rm.GetString("Kernel_114");
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, LocalizationHolder.rm.GetString("Kernel_115") + str1);
      this.CheckAccess(ActionType.EditProperties);
      this.CheckChangeEnable("F_LEVEL_ID");
      try
      {
        this.UserSession.DataManager.ExecuteNonQuery(sc_12586.ssp_appserver_12674(), this.UserSession.DataManager.Parameter("attrID", (object) this.AttributeID), this.UserSession.DataManager.Parameter("val", (object) value));
        this.UserSession.DBCache.ChangeTableValue("F_ATTRIBUTE_ID = " + this.AttributeID.ToString(), "IMS_ATTRIBUTES", "F_LEVEL_ID", (object) value, (IUserSession) this.UserSession);
        this.paramsTable[72] = (object) value;
      }
      catch (Exception ex)
      {
        string str2 = LocalizationHolder.rm.GetString("Kernel_116") + ex.Message;
        this.CloseEvent(EventID, EventlogRecordType.Error, str2);
        throw new KernelException(str2, ex);
      }
    }
  }

  public string LevelName
  {
    get
    {
      return this.LevelID == 0 ? string.Empty : this.UserSession.GetLifecycleLevel(this.LevelID).LevelName;
    }
  }

  public string Litera
  {
    get
    {
      return this.LevelID == 0 ? string.Empty : this.UserSession.GetLifecycleLevel(this.LevelID).Litera;
    }
  }

  public byte[] LevelIcon
  {
    get
    {
      return this.LevelID == 0 ? new byte[0] : this.UserSession.GetLifecycleLevel(this.LevelID).LevelIcon;
    }
  }
}
