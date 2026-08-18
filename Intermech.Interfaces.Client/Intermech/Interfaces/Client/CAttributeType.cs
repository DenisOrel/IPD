// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.CAttributeType
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Класс, реализующий работу с атрибутом как с типом.</summary>
internal class CAttributeType : 
  CMetadataExtentions,
  IDBAttributeType,
  IDBSubjectArea,
  IDBLanguage,
  IDeletable,
  IDBLifecycleLevel,
  IDBSecurity
{
  private bool _ComputableAttribute;
  private string _TextFieldName = "F_STRING_VALUE";
  private string _ValueFieldName = "F_STRING_VALUE";
  private string _PossibleValueFieldName = "F_STRING_VALUE";
  private RelationalOperators[] _EnabledOperators;
  private List<FieldTypes> _ConvertList = new List<FieldTypes>();

  public CAttributeType(ClientSession uSession, int aAttributeID)
    : base(uSession, aAttributeID)
  {
    this._AttributeTypeID = aAttributeID;
    this.InitOptions(3, (long) aAttributeID, "IMS_ATTRIBUTES", LocalizationHolder.rm.GetString("Interfaces.Client_116"));
    AttributeCacheHelper.GetAttributeTypeValues((FieldTypes) Convert.ToInt32(this.paramsTable[0]["F_ATTRIBUTE_TYPE"]), aAttributeID, ref this._ValueFieldName, ref this._TextFieldName, ref this._ConvertList, ref this._EnabledOperators, ref this._ComputableAttribute, ref this._PossibleValueFieldName);
  }

  public override object GetServerObject()
  {
    this._clientSession.Guard.ValidateCall();
    return (object) this._clientSession.Session.GetAttributeType(this._id);
  }

  public void ValidateAssign(IDBAttributeType source)
  {
    this._clientSession.Guard.ValidateCall();
    this._clientSession.Session.GetAttributeType(this._id).ValidateAssign(source);
  }

  internal DataRow[] GetPossibleValuesRows(int objectType, int relationType)
  {
    return this._clientSession.ClientCache.GetTable("IMS_POSSIBLE_VALUES").Select($"F_ATTRIBUTE_ID = {this._id} AND F_OBJECT_TYPE = {objectType} AND F_RELATION_TYPE = {relationType}", "F_INLIST_ID");
  }

  public DataRow[] GetPossibleValuesRows()
  {
    this._clientSession.Guard.ValidateCall();
    return this.GetPossibleValuesRows(-1, -1);
  }

  public FieldTypes AttributeType
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return (FieldTypes) Convert.ToInt32(this.paramsTable[0]["F_ATTRIBUTE_TYPE"]);
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (this.AttributeType == value)
        return;
      this._clientSession.Session.GetAttributeType(this._id).AttributeType = value;
      this.ReloadClientCache();
    }
  }

  public string ShortName
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.paramsTable[0]["F_SHORT_NAME"].ToString();
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (!(this.ShortName != value))
        return;
      this._clientSession.Session.GetAttributeType(this._id).ShortName = value;
      this.ReloadClientCache();
    }
  }

  public OptimizationModes OptimizationMode
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return (OptimizationModes) Convert.ToInt32(this.paramsTable[0]["F_INVIEW"]);
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (this.OptimizationMode == value)
        return;
      this._clientSession.Session.GetAttributeType(this._id).OptimizationMode = value;
      this.ReloadClientCache();
    }
  }

  public string TextFieldName
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._TextFieldName;
    }
  }

  public string SizeTypeDescription
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.SizeType.ToString();
    }
  }

  public MultiValueModes MultipleValued
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return (MultiValueModes) Convert.ToInt32(this.paramsTable[0]["F_MULTIPLE_VALUED"]);
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (this.MultipleValued == value)
        return;
      this._clientSession.Session.GetAttributeType(this._id).MultipleValued = value;
      this.ReloadClientCache();
    }
  }

  public string ValueFieldName
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      if (this._ValueFieldName == null)
        this._ValueFieldName = this._clientSession.Session.GetAttributeType(this._id).ValueFieldName;
      return this._ValueFieldName;
    }
  }

  public string PossibleValueFieldName
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._PossibleValueFieldName;
    }
  }

  public UniqueValueModes UniqueMode
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return (UniqueValueModes) Convert.ToInt32(this.paramsTable[0]["F_UNIQUE"]);
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (this.UniqueMode == value)
        return;
      this._clientSession.Session.GetAttributeType(this._id).UniqueMode = value;
      this.ReloadClientCache();
    }
  }

  public string ValidationRule
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return "";
    }
    [DebuggerStepThrough] set => this._clientSession.Guard.ValidateCall();
  }

  public int LevelID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return Convert.ToInt32(this.paramsTable[0]["F_LEVEL_ID"]);
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (this.LevelID == value)
        return;
      this._clientSession.Session.GetAttributeType(this._id).LevelID = value;
      this.ReloadClientCache();
    }
  }

  public int AttributeID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._id;
    }
  }

  public DataTable GetPossibleValues()
  {
    this._clientSession.Guard.ValidateCall();
    if (Convert.ToInt32(this.paramsTable[0]["F_ATTRIBUTE_TYPE"]) == 15)
      return (DataTable) null;
    DataRow[] fromRows = this._clientSession.ClientCache.GetTable("IMS_POSSIBLE_VALUES").Select($"F_ATTRIBUTE_ID = {this._id} AND F_OBJECT_TYPE = -1 AND F_RELATION_TYPE = -1", "F_INLIST_ID");
    DataTable toTable = new DataTable("IMS_POSSIBLE_VALUES");
    DataColumn column1 = new DataColumn(this._clientSession.ClientCache.GetTable("IMS_POSSIBLE_VALUES").Columns["F_INLIST_ID"].ColumnName, this._clientSession.ClientCache.GetTable("IMS_POSSIBLE_VALUES").Columns["F_INLIST_ID"].DataType);
    DataColumn column2 = new DataColumn(this._clientSession.ClientCache.GetTable("IMS_POSSIBLE_VALUES").Columns[this.PossibleValueFieldName].ColumnName, this._clientSession.ClientCache.GetTable("IMS_POSSIBLE_VALUES").Columns[this.PossibleValueFieldName].DataType);
    DataColumn column3 = new DataColumn(this._clientSession.ClientCache.GetTable("IMS_POSSIBLE_VALUES").Columns["F_DESCRIPTION"].ColumnName, this._clientSession.ClientCache.GetTable("IMS_POSSIBLE_VALUES").Columns["F_DESCRIPTION"].DataType);
    toTable.Columns.Add(column1);
    toTable.Columns.Add(column2);
    toTable.Columns.Add(column3);
    DataSetProcessor.AssignRows(toTable, (IEnumerable<DataRow>) fromRows);
    return toTable;
  }

  public string[] FieldNames
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return new string[1]
      {
        "F" + this.AttributeID.ToString()
      };
    }
  }

  public string Formula
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.paramsTable[0]["F_FORMULA"].ToString();
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (!(this.Formula != value))
        return;
      this._clientSession.Session.GetAttributeType(this._id).Formula = value;
      this.ReloadClientCache();
    }
  }

  public bool ComputableAttribute
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._ComputableAttribute;
    }
  }

  public long SizeType
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return Convert.ToInt64(this.paramsTable[0]["F_SIZE_TYPE"]);
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (Convert.ToInt64(this.paramsTable[0]["F_SIZE_TYPE"]) == value)
        return;
      this._clientSession.Session.GetAttributeType(this._id).SizeType = value;
      this.ReloadClientCache();
    }
  }

  public int MasterAttributeID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return Convert.ToInt32(this.paramsTable[0]["F_MASTER_ID"]);
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (Convert.ToInt32(this.paramsTable[0]["F_MASTER_ID"]) == value)
        return;
      this._clientSession.Session.GetAttributeType(this._id).MasterAttributeID = value;
      this.ReloadClientCache();
    }
  }

  public int SourceAttributeID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return Convert.ToInt32(this.paramsTable[0]["F_SOURCE_ID"]);
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (Convert.ToInt32(this.paramsTable[0]["F_SOURCE_ID"]) == value)
        return;
      this._clientSession.Session.GetAttributeType(this._id).SourceAttributeID = value;
      this.ReloadClientCache();
    }
  }

  public bool IsGridable
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.AttributeType != FieldTypes.ftPassword && this.Computed != ComputeValueModes.IndexValue;
    }
  }

  public virtual void SetNewPossibleValues(DataTable valuesTable)
  {
    this._clientSession.Guard.ValidateCall();
    this._clientSession.Session.GetAttributeType(this._id).SetNewPossibleValues(valuesTable);
    this.ReloadClientCache();
  }

  /// <summary>Присваивает атрибуту список допустимых значений</summary>
  public virtual void SetPossibleValues(DataTable valuesTable)
  {
    this._clientSession.Guard.ValidateCall();
    this._clientSession.Session.GetAttributeType(this._id).SetPossibleValues(valuesTable);
    this.ReloadClientCache();
  }

  public RelationalOperators[] EnabledOperators
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      if (this.MultipleValued == MultiValueModes.MultiValues || this.MultipleValued == MultiValueModes.MultiValuesFromList || this.MultipleValued == MultiValueModes.SingleValueFromList)
        return AttributeCacheHelper.GetMultiValuesRelationalOperators(this.AttributeType == FieldTypes.ftFile || this.AttributeType == FieldTypes.ftString);
      if (this._EnabledOperators == null)
        this._EnabledOperators = this._clientSession.Session.GetAttributeType(this._id).EnabledOperators;
      return this._EnabledOperators;
    }
  }

  public string Name
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.paramsTable[0]["F_NAME"].ToString();
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (!(this.Name != value))
        return;
      this._clientSession.Session.GetAttributeType(this._id).Name = value;
      this.ReloadClientCache();
    }
  }

  public object[] GetPossibleValuesArray()
  {
    this._clientSession.Guard.ValidateCall();
    DataTable possibleValues = this.GetPossibleValues();
    object[] possibleValuesArray = new object[possibleValues.Rows.Count];
    for (int index = 0; index < possibleValuesArray.Length; ++index)
      possibleValuesArray[index] = possibleValues.Rows[index][this.PossibleValueFieldName];
    return possibleValuesArray;
  }

  public object DefaultValue
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      object obj = this.paramsTable[0]["F_DEFAULT_VALUE"];
      if (obj == DBNull.Value || obj == null || obj.ToString() == "")
        return (object) DBNull.Value;
      return this.AttributeType == FieldTypes.ftDouble ? (object) Convert.ToDouble(obj, (IFormatProvider) CultureInfo.InvariantCulture) : obj;
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (this.DefaultValue == value)
        return;
      this._clientSession.Session.GetAttributeType(this._id).DefaultValue = value;
      this.ReloadClientCache();
    }
  }

  public int[] GetGroupsList()
  {
    this._clientSession.Guard.ValidateCall();
    ArrayList arrayList = new ArrayList();
    foreach (DataRow dataRow in this._clientSession.ClientCache.GetTable("IMS_ATTR_IN_GROUPS").Select("F_ATTRIBUTE_ID = " + this._id.ToString()))
      arrayList.Add((object) Convert.ToInt32(dataRow["F_GROUP_ID"]));
    return (int[]) arrayList.ToArray(typeof (int));
  }

  public int Delete(long DeleteMode)
  {
    this._clientSession.Guard.ValidateCall();
    int num = this._clientSession.Session.GetAttributeType(this._id, true).Delete(DeleteMode);
    this.ReloadClientCache();
    return num;
  }

  public bool IsContent
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return Convert.ToInt32(this.paramsTable[0]["F_CONTENT"]) == 1;
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (this.IsContent == value)
        return;
      this._clientSession.Session.GetAttributeType(this._id).IsContent = value;
      this.ReloadClientCache();
    }
  }

  public ComputeValueModes Computed
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return (ComputeValueModes) Convert.ToInt32(this.paramsTable[0]["F_COMPUTED"]);
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (this.Computed == value)
        return;
      this._clientSession.Session.GetAttributeType(this._id).Computed = value;
      this.ReloadClientCache();
    }
  }

  public string DefaultValueDescription
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.DefaultValue == null ? "" : this.DefaultValue.ToString();
    }
  }

  public string Alias
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.paramsTable[0]["F_ALIAS"].ToString();
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      value = value.Trim();
      if (!(this.Alias != value))
        return;
      this._clientSession.Session.GetAttributeType(this._id).Alias = value;
      this.ReloadClientCache();
    }
  }

  public bool IsCompatibleType(FieldTypes newType)
  {
    this._clientSession.Guard.ValidateCall();
    return this._ConvertList.IndexOf(newType) >= 0;
  }

  public void ValidateSizeType(long newValue) => this._clientSession.Guard.ValidateCall();

  public AttributeOptions Options
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return (AttributeOptions) Convert.ToInt32(this.paramsTable[0]["F_OPTIONS"]);
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (this.Options == value)
        return;
      this._clientSession.Session.GetAttributeType(this._id).Options = value;
      this.ReloadClientCache();
    }
  }

  public AttributeTypeProperties PropertiesStructure
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      AttributeTypeProperties atProperties = new AttributeTypeProperties(this.AttributeID, this.Name, this.ShortName, this.Alias, this.Note, this.AttributeType, this.DefaultValue, this.MultipleValued, this.Computed, this.SizeType, this.Formula, this.UniqueMode, this.LevelID, this.LanguageID, this.SubjectAreas, this.GUID, this.OptimizationMode, this.IsContent, this.Options, this.Mask, this.MasterAttributeID, this.SourceAttributeID);
      this.DoGetPropertiesStructure(ref atProperties);
      if (this.AttributeID == -8)
        atProperties.MetadataExtensions[(object) "OBJ_LINKS_ID"] = (object) new int[1]
        {
          this._clientSession.IdentHelper.UsersTypeID
        };
      return atProperties;
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (this._clientSession.Session.GetAttributeType(this._id).PropertiesStructure.AreaID != value.AreaID || this._clientSession.Session.GetAttributeType(this._id).PropertiesStructure.LanguageID != value.LanguageID)
        this._clientSession.ClientCache.ClearVisibleList(3);
      this._clientSession.Session.GetAttributeType(this._id).PropertiesStructure = value;
      this.ReloadClientCache();
    }
  }

  /// <summary>
  /// Метод вызывается в момент чтения структуры свойств атрибута (сразу после отработки базового метода).
  /// Позволяет выполнить дополнительное заполнение свойств (например, расширенными метаданными)
  /// </summary>
  protected virtual void DoGetPropertiesStructure(ref AttributeTypeProperties atProperties)
  {
  }

  public string Note
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.paramsTable[0]["F_NOTE"].ToString();
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (!(this.Note != value))
        return;
      this._clientSession.Session.GetAttributeType(this._id).Note = value;
      this.ReloadClientCache();
    }
  }

  public string Mask
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.paramsTable[0]["F_MASK"].ToString();
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (!(this.Mask != value))
        return;
      this._clientSession.Session.GetAttributeType(this._id).Mask = value;
      this.ReloadClientCache();
    }
  }

  public RelationalOperators[] GetEnabledOperators(ColumnContents content)
  {
    this._clientSession.Guard.ValidateCall();
    return this._clientSession.Session.GetAttributeType(this._id).GetEnabledOperators(content);
  }

  /// <summary>
  /// Возвращает список идентификаторов атрибутов, использующихся в формуле для вычисления значения данного атрибута.
  /// Если атрибут не вычисляемый, то возвращает массив нулевой длины.
  /// </summary>
  public int[] GetRelatedFormulaAttributes()
  {
    this._clientSession.Guard.ValidateCall();
    DataRow[] dataRowArray = this._clientSession.ClientCache.GetTable("IMS_FORMULA_ATTRS").Select(string.Format("F_FORMULA_ID = {0} AND F_OBJECT_TYPE = -1 AND F_RELATION_TYPE = -1 AND F_MODE_ID = " + Consts.Attribute4Formula.ToString(), (object) this.AttributeID));
    int[] formulaAttributes = new int[dataRowArray.Length];
    for (int index = 0; index < dataRowArray.Length; ++index)
      formulaAttributes[index] = Convert.ToInt32(dataRowArray[index]["F_ATTRIBUTE_ID"]);
    return formulaAttributes;
  }

  public string SubjectAreasCaption
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._clientSession.Session.GetSubjectAreaCollection().GetAreasCaption(this.SubjectAreas);
    }
  }

  public string SubjectAreas
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.paramsTable[0]["F_AREA_ID"].ToString();
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (!(this.SubjectAreas != value))
        return;
      (this._clientSession.Session.GetAttributeType(this._id) as IDBSubjectArea).SubjectAreas = value;
      this._clientSession.ClientCache.ClearVisibleList(3);
      this.ReloadClientCache();
    }
  }

  public string LanguageName
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.LanguageID == string.Empty ? string.Empty : this._clientSession.GetLanguage(this.LanguageID).LanguageName;
    }
  }

  public string LanguageID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.paramsTable[0]["F_LANGUAGE_ID"].ToString().TrimEnd();
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (!(this.LanguageID != value))
        return;
      (this._clientSession.Session.GetAttributeType(this._id) as IDBLanguage).LanguageID = value;
      this._clientSession.ClientCache.ClearVisibleList(3);
      this.ReloadClientCache();
    }
  }

  public bool IsDefaultLanguage
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.LanguageID == string.Empty || this._clientSession.GetLanguage(this.LanguageID).IsDefaultLanguage;
    }
  }

  /// <summary>Литера уровня продвижения.</summary>
  public string Litera
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._clientSession.GetLifecycleLevel(this.LevelID).Litera;
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      throw new OperationNotApplicableException();
    }
  }

  public byte[] LevelIcon
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._clientSession.GetLifecycleLevel(this.LevelID).LevelIcon;
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      throw new OperationNotApplicableException();
    }
  }

  public string LevelName
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._clientSession.GetLifecycleLevel(this.LevelID).LevelName;
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      throw new OperationNotApplicableException();
    }
  }

  public string ObjectName
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return string.Format(LocalizationHolder.rm.GetString("Interfaces.Client_3"), (object) this.Name);
    }
  }

  public bool CheckAccess(ActionType rightID, bool aThrowACException)
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetAttributeType(this._id) as IDBSecurity).CheckAccess(rightID, aThrowACException);
  }

  public bool CheckAccess(ActionType rightID)
  {
    this._clientSession.Guard.ValidateCall();
    return this.CheckAccess(rightID, true);
  }

  public bool CheckAccess(ActionType rightID, bool defaultAccess, bool aThrowACException)
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetAttributeType(this._id) as IDBSecurity).CheckAccess(rightID, defaultAccess, aThrowACException);
  }

  public bool CheckAccess(ActionType rightID, bool defaultAccess, CheckAccessFlags flags)
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetAttributeType(this._id) as IDBSecurity).CheckAccess(rightID, defaultAccess, flags);
  }

  public bool IsAccessTypeDeny
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return (this._clientSession.Session.GetAttributeType(this._id) as IDBSecurity).IsAccessTypeDeny;
    }
  }

  public bool IsLastDefault
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return (this._clientSession.Session.GetAttributeType(this._id) as IDBSecurity).IsLastDefault;
    }
  }

  public CategoryDescriptor Descriptor
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return new CategoryDescriptor(this._CategoryType, this._CategoryID);
    }
  }

  public DataTable GetAccessList(out ActionProperties[] actions, out QuickObjectInfo[] users)
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetAttributeType(this._id) as IDBSecurity).GetAccessList(out actions, out users);
  }

  public void SetAccess(DataTable accessList, params object[] AddInfo)
  {
    this._clientSession.Guard.ValidateCall();
    this._clientSession.ClientCache.ClearVisibleList(3);
    (this._clientSession.Session.GetAttributeType(this._id) as IDBSecurity).SetAccess(accessList, AddInfo);
  }

  public IDBSecurity[] GetRelatedSecurity()
  {
    this._clientSession.Guard.ValidateCall();
    return (this._clientSession.Session.GetAttributeType(this._id) as IDBSecurity).GetRelatedSecurity();
  }

  public void RestoreAdminAccess()
  {
    this._clientSession.Guard.ValidateCall();
    (this._clientSession.Session.GetAttributeType(this._id) as IDBSecurity).RestoreAdminAccess();
  }
}
