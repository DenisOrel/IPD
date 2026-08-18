// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.CAttributeTypeInfo
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Получатель инфы о типе атрибута</summary>
internal class CAttributeTypeInfo : MetadataInfoObject, IDBAttributeTypeInfo
{
  private bool _ComputableAttribute;
  private string _TextFieldName = "F_STRING_VALUE";
  private string _ValueFieldName = "F_STRING_VALUE";
  private string _PossibleValueFieldName = "F_STRING_VALUE";
  private RelationalOperators[] _EnabledOperators;
  private List<FieldTypes> _ConvertList = new List<FieldTypes>();
  private bool extPropertiesInited;

  public CAttributeTypeInfo(MetadataInfoParentContext serviceContext, int metadataID)
    : base(serviceContext, metadataID)
  {
    this._AttributeTypeID = metadataID;
  }

  private void InitExtendedProperties()
  {
    if (this.extPropertiesInited)
      return;
    AttributeCacheHelper.GetAttributeTypeValues((FieldTypes) Convert.ToInt32(this.paramsTable[0]["F_ATTRIBUTE_TYPE"]), this._AttributeTypeID, ref this._ValueFieldName, ref this._TextFieldName, ref this._ConvertList, ref this._EnabledOperators, ref this._ComputableAttribute, ref this._PossibleValueFieldName);
    this.extPropertiesInited = true;
  }

  protected override string DBTableName => "IMS_ATTRIBUTES";

  protected override string MetadataNotFoundMessage
  {
    get => $"Атрибут номер {this.MetadataID} не найден.";
  }

  public override string ObjectName => $"Атрибут '{this.Name}'";

  public int AttributeID
  {
    [DebuggerStepThrough] get => this.MetadataID;
  }

  public AttributeOptions Options
  {
    [DebuggerStepThrough] get
    {
      return (AttributeOptions) Convert.ToInt32(this.paramsTable[0]["F_OPTIONS"]);
    }
  }

  public string Mask
  {
    [DebuggerStepThrough] get => this.paramsTable[0]["F_MASK"].ToString();
  }

  public string Name
  {
    [DebuggerStepThrough] get => this.paramsTable[0]["F_NAME"].ToString();
  }

  public string ShortName
  {
    [DebuggerStepThrough] get => this.paramsTable[0]["F_SHORT_NAME"].ToString();
  }

  public string Alias
  {
    [DebuggerStepThrough] get => this.paramsTable[0]["F_ALIAS"].ToString();
  }

  public string Note
  {
    [DebuggerStepThrough] get => this.paramsTable[0]["F_NOTE"].ToString();
  }

  public FieldTypes AttributeType
  {
    [DebuggerStepThrough] get
    {
      return (FieldTypes) Convert.ToInt32(this.paramsTable[0]["F_ATTRIBUTE_TYPE"]);
    }
  }

  public object DefaultValue
  {
    [DebuggerStepThrough] get
    {
      object obj = this.paramsTable[0]["F_DEFAULT_VALUE"];
      if (obj == DBNull.Value || obj == null || obj.ToString() == "")
        return (object) DBNull.Value;
      return this.AttributeType == FieldTypes.ftDouble ? (object) Convert.ToDouble(obj, (IFormatProvider) CultureInfo.InvariantCulture) : obj;
    }
  }

  public string DefaultValueDescription
  {
    [DebuggerStepThrough] get
    {
      return this.DefaultValue == null ? string.Empty : this.DefaultValue.ToString();
    }
  }

  public MultiValueModes MultipleValued
  {
    [DebuggerStepThrough] get
    {
      return (MultiValueModes) Convert.ToInt32(this.paramsTable[0]["F_MULTIPLE_VALUED"]);
    }
  }

  public ComputeValueModes Computed
  {
    [DebuggerStepThrough] get
    {
      return (ComputeValueModes) Convert.ToInt32(this.paramsTable[0]["F_COMPUTED"]);
    }
  }

  public long SizeType
  {
    [DebuggerStepThrough] get => Convert.ToInt64(this.paramsTable[0]["F_SIZE_TYPE"]);
  }

  public string SizeTypeDescription
  {
    [DebuggerStepThrough] get => this.SizeType.ToString();
  }

  public string Formula
  {
    [DebuggerStepThrough] get => this.paramsTable[0]["F_FORMULA"].ToString();
  }

  public UniqueValueModes UniqueMode
  {
    [DebuggerStepThrough] get
    {
      return (UniqueValueModes) Convert.ToInt32(this.paramsTable[0]["F_UNIQUE"]);
    }
  }

  public AttributeTypeProperties PropertiesStructure
  {
    [DebuggerStepThrough] get
    {
      AttributeTypeProperties propertiesStructure = new AttributeTypeProperties(this.AttributeID, this.Name, this.ShortName, this.Alias, this.Note, this.AttributeType, this.DefaultValue, this.MultipleValued, this.Computed, this.SizeType, this.Formula, this.UniqueMode, this.LevelID, this.LanguageID, this.SubjectAreas, this.GUID, this.OptimizationMode, this.IsContent, this.Options, this.Mask, this.MasterAttributeID, this.SourceAttributeID);
      string str = this.AttributeType != FieldTypes.ftMeasured ? (this.AttributeType != FieldTypes.ftObjectLink ? string.Empty : "OBJ_LINKS_ID") : "MU_PHYSICAL_ID";
      if (str != string.Empty)
      {
        long[] mdValuesInt64 = this.GetMDValuesInt64(str);
        if (mdValuesInt64.Length != 0)
          propertiesStructure.MetadataExtensions[(object) str] = (object) mdValuesInt64;
      }
      return propertiesStructure;
    }
  }

  public string LanguageID
  {
    [DebuggerStepThrough] get => this.paramsTable[0]["F_LANGUAGE_ID"].ToString().TrimEnd();
  }

  public string SubjectAreas
  {
    [DebuggerStepThrough] get => this.paramsTable[0]["F_AREA_ID"].ToString();
  }

  public int LevelID
  {
    [DebuggerStepThrough] get => Convert.ToInt32(this.paramsTable[0]["F_LEVEL_ID"]);
  }

  public string TextFieldName
  {
    [DebuggerStepThrough] get
    {
      this.InitExtendedProperties();
      return this._TextFieldName;
    }
  }

  public string ValueFieldName
  {
    [DebuggerStepThrough] get
    {
      this.InitExtendedProperties();
      return this._ValueFieldName;
    }
  }

  public string PossibleValueFieldName
  {
    [DebuggerStepThrough] get
    {
      this.InitExtendedProperties();
      return this._PossibleValueFieldName;
    }
  }

  public bool IsGridable
  {
    [DebuggerStepThrough] get
    {
      return this.AttributeType != FieldTypes.ftPassword && this.Computed != ComputeValueModes.IndexValue;
    }
  }

  public RelationalOperators[] EnabledOperators
  {
    [DebuggerStepThrough] get
    {
      if (this.MultipleValued == MultiValueModes.MultiValues || this.MultipleValued == MultiValueModes.MultiValuesFromList || this.MultipleValued == MultiValueModes.SingleValueFromList)
        return AttributeCacheHelper.GetMultiValuesRelationalOperators(this.AttributeType == FieldTypes.ftFile || this.AttributeType == FieldTypes.ftString);
      this.InitExtendedProperties();
      return this._EnabledOperators;
    }
  }

  public virtual string ValidationRule
  {
    [DebuggerStepThrough] get => string.Empty;
  }

  public bool ComputableAttribute
  {
    [DebuggerStepThrough] get
    {
      this.InitExtendedProperties();
      return this._ComputableAttribute;
    }
  }

  public OptimizationModes OptimizationMode
  {
    [DebuggerStepThrough] get
    {
      return (OptimizationModes) Convert.ToInt32(this.paramsTable[0]["F_INVIEW"]);
    }
  }

  public string[] FieldNames
  {
    [DebuggerStepThrough] get
    {
      return new string[1]
      {
        "F" + this.AttributeID.ToString()
      };
    }
  }

  public bool IsContent
  {
    [DebuggerStepThrough] get => Convert.ToInt32(this.paramsTable[0]["F_CONTENT"]) == 1;
  }

  public int SourceAttributeID
  {
    [DebuggerStepThrough] get => Convert.ToInt32(this.paramsTable[0]["F_SOURCE_ID"]);
  }

  public int MasterAttributeID
  {
    [DebuggerStepThrough] get => Convert.ToInt32(this.paramsTable[0]["F_MASTER_ID"]);
  }

  public int[] GetGroupsList()
  {
    DataRow[] dataRowArray = this.ServiceContext.ClientCache.GetTable("IMS_ATTR_IN_GROUPS").Select("F_ATTRIBUTE_ID = " + this.AttributeID.ToString());
    List<int> intList = new List<int>(dataRowArray.Length);
    foreach (DataRow dataRow in dataRowArray)
      intList.Add(Convert.ToInt32(dataRow["F_GROUP_ID"]));
    return intList.ToArray();
  }

  public DataTable GetPossibleValues()
  {
    if (Convert.ToInt32(this.paramsTable[0]["F_ATTRIBUTE_TYPE"]) == 15)
      return (DataTable) null;
    DataTable table = this.ServiceContext.ClientCache.GetTable("IMS_POSSIBLE_VALUES");
    DataRow[] fromRows = table.Select($"F_ATTRIBUTE_ID = {this.AttributeID} AND F_OBJECT_TYPE = -1 AND F_RELATION_TYPE = -1", "F_INLIST_ID");
    DataTable toTable = new DataTable("IMS_POSSIBLE_VALUES");
    DataColumn column1 = new DataColumn(table.Columns["F_INLIST_ID"].ColumnName, table.Columns["F_INLIST_ID"].DataType);
    DataColumn column2 = new DataColumn(table.Columns[this.PossibleValueFieldName].ColumnName, table.Columns[this.PossibleValueFieldName].DataType);
    DataColumn column3 = new DataColumn(table.Columns["F_DESCRIPTION"].ColumnName, table.Columns["F_DESCRIPTION"].DataType);
    toTable.Columns.Add(column1);
    toTable.Columns.Add(column2);
    toTable.Columns.Add(column3);
    DataSetProcessor.AssignRows(toTable, (IEnumerable<DataRow>) fromRows);
    return toTable;
  }

  public object[] GetPossibleValuesArray()
  {
    this.InitExtendedProperties();
    DataTable possibleValues = this.GetPossibleValues();
    object[] possibleValuesArray = new object[possibleValues.Rows.Count];
    for (int index = 0; index < possibleValuesArray.Length; ++index)
      possibleValuesArray[index] = possibleValues.Rows[index][this.PossibleValueFieldName];
    return possibleValuesArray;
  }

  public DataRow[] GetPossibleValuesRows()
  {
    return this.ServiceContext.ClientCache.GetTable("IMS_POSSIBLE_VALUES").Select($"F_ATTRIBUTE_ID = {this.AttributeID} AND F_OBJECT_TYPE = -1 AND F_RELATION_TYPE = -1", "F_INLIST_ID");
  }

  public bool IsCompatibleType(FieldTypes newType)
  {
    this.InitExtendedProperties();
    return this._ConvertList.IndexOf(newType) >= 0;
  }
}
