// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.CAttributeType4Category
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Kernel.Search;
using System;
using System.Data;
using System.Diagnostics;
using System.Globalization;

#nullable disable
namespace Intermech.Interfaces.Client;

internal abstract class CAttributeType4Category : 
  MarshalByRefObject,
  IDeletable,
  IDBGuid,
  IDBSubjectArea,
  IDBLanguage
{
  protected int typeID;
  protected int attributeID;
  protected ClientSession _clientSession;
  protected IDBAttributeType attrType;
  /// <summary>
  /// Таблица с параметрами, которые могут хранится в данных объектах.
  /// </summary>
  protected HybridTable paramsTable = new HybridTable();

  public CAttributeType4Category(ClientSession session, DataRow row, string keyName)
  {
    this.typeID = Convert.ToInt32(row[keyName]);
    this.attributeID = Convert.ToInt32(row["F_ATTRIBUTE_ID"]);
    this._clientSession = session;
    this.attrType = this._clientSession.GetAttributeType(this.attributeID, true);
    this.paramsTable.Create(row);
  }

  protected void ReloadCache()
  {
    this._clientSession.ClientCache.ClearVisibleList(3);
    this._clientSession.ClientCache.ReloadCache(this._clientSession.Session);
  }

  protected virtual IDBAttributeType4 attribute4
  {
    [DebuggerStepThrough] get => (IDBAttributeType4) null;
  }

  public int AttributeID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.attributeID;
    }
  }

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
      if (this.attribute4.Options == value)
        return;
      this.attribute4.Options = value;
      this.paramsTable[0]["F_OPTIONS"] = (object) Convert.ToInt32((object) value);
      this.ReloadCache();
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
      if (!(this.attribute4.Mask != value))
        return;
      this.attribute4.Mask = value;
      this.paramsTable[0]["F_MASK"] = (object) Convert.ToInt32(value);
      this.ReloadCache();
    }
  }

  public string Name
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.attrType.Name;
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      throw new OperationNotApplicableException();
    }
  }

  public string ShortName
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.attrType.ShortName;
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      throw new OperationNotApplicableException();
    }
  }

  public string Alias
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.attrType.Alias;
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      throw new OperationNotApplicableException();
    }
  }

  public string Note
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.attrType.Note;
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      throw new OperationNotApplicableException();
    }
  }

  public FieldTypes AttributeType
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.attrType.AttributeType;
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      throw new OperationNotApplicableException();
    }
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
      if (this.attribute4.DefaultValue == value)
        return;
      this.attribute4.DefaultValue = value;
      this.paramsTable[0]["F_DEFAULT_VALUE"] = value;
      this.ReloadCache();
    }
  }

  public string DefaultValueDescription
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.attrType.DefaultValueDescription;
    }
  }

  public MultiValueModes MultipleValued
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.attrType.MultipleValued;
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      throw new OperationNotApplicableException();
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
      if (this.attribute4.Computed == value)
        return;
      this.attribute4.Computed = value;
      this.paramsTable[0]["F_COMPUTED"] = (object) (int) value;
      this.ReloadCache();
    }
  }

  public long SizeType
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.attrType.SizeType;
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      throw new OperationNotApplicableException();
    }
  }

  public string SizeTypeDescription
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.attrType.SizeTypeDescription;
    }
  }

  public string Formula
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      object obj = this.paramsTable[0]["F_FORMULA"];
      return obj == DBNull.Value ? "" : Convert.ToString(obj);
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (!(this.attribute4.Formula != value))
        return;
      this.attribute4.Formula = value;
      this.paramsTable[0]["F_FORMULA"] = (object) value;
      this.ReloadCache();
    }
  }

  public virtual UniqueValueModes UniqueMode
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return (UniqueValueModes) Convert.ToInt32(this.paramsTable[0]["F_UNIQUE"]);
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (this.attribute4.UniqueMode == value)
        return;
      this.attribute4.UniqueMode = value;
      this.paramsTable[0]["F_UNIQUE"] = (object) (int) value;
      this.ReloadCache();
    }
  }

  public void ValidateSizeType(long newValue)
  {
    this._clientSession.Guard.ValidateCall();
    this.attrType.ValidateSizeType(newValue);
  }

  public void SetNewPossibleValues(DataTable valuesTable)
  {
    this._clientSession.Guard.ValidateCall();
    this.attribute4.SetNewPossibleValues(valuesTable);
  }

  public void SetPossibleValues(DataTable valuesTable)
  {
    this._clientSession.Guard.ValidateCall();
    this.attribute4.SetPossibleValues(valuesTable);
  }

  public DataTable GetPossibleValues()
  {
    this._clientSession.Guard.ValidateCall();
    return this.attrType.GetPossibleValues();
  }

  public object[] GetPossibleValuesArray()
  {
    this._clientSession.Guard.ValidateCall();
    return this.attrType.GetPossibleValuesArray();
  }

  public DataRow[] GetPossibleValuesRows()
  {
    this._clientSession.Guard.ValidateCall();
    return this.attrType.GetPossibleValuesRows();
  }

  public AttributeTypeProperties PropertiesStructure
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return new AttributeTypeProperties(this.AttributeID, this.Name, this.ShortName, this.Alias, this.Note, this.AttributeType, this.DefaultValue, this.MultipleValued, this.Computed, this.SizeType, this.Formula, this.UniqueMode, this.LevelID, this.LanguageID, this.SubjectAreas, this.GUID, this.OptimizationMode, this.IsContent, this.Options, this.Mask, this.MasterAttributeID, this.SourceAttributeID);
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      throw new OperationNotApplicableException();
    }
  }

  public int Delete(long DeleteMode)
  {
    this._clientSession.Guard.ValidateCall();
    int num = this.attribute4.Delete(DeleteMode);
    this.ReloadCache();
    return num;
  }

  public int LevelID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.attrType.LevelID;
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      throw new OperationNotApplicableException();
    }
  }

  public bool IsCompatibleType(FieldTypes newType)
  {
    this._clientSession.Guard.ValidateCall();
    return this.attrType.IsCompatibleType(newType);
  }

  public string TextFieldName
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.attrType.TextFieldName;
    }
  }

  public string ValueFieldName
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.attrType.ValueFieldName;
    }
  }

  public string PossibleValueFieldName
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.attrType.PossibleValueFieldName;
    }
  }

  public bool IsGridable
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.attrType.IsGridable;
    }
  }

  public RelationalOperators[] EnabledOperators
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.attrType.EnabledOperators;
    }
  }

  public virtual string ValidationRule
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      object obj = this.paramsTable[0]["F_VALIDATION_RULE"];
      return obj == null ? "" : obj.ToString();
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (!(this.attribute4.ValidationRule != value))
        return;
      this.attribute4.ValidationRule = value;
      this.paramsTable[0]["F_VALIDATION_RULE"] = (object) value;
      this.ReloadCache();
    }
  }

  public bool ComputableAttribute
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.attrType.ComputableAttribute;
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
      if (this.attribute4.OptimizationMode == value)
        return;
      this.attribute4.OptimizationMode = value;
      this.paramsTable[0]["F_INVIEW"] = (object) (int) value;
      this.ReloadCache();
    }
  }

  public string[] FieldNames
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.attrType.FieldNames;
    }
  }

  public int[] GetGroupsList()
  {
    this._clientSession.Guard.ValidateCall();
    return this.attrType.GetGroupsList();
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
      if (this.attribute4.IsContent == value)
        return;
      this.attribute4.IsContent = value;
      this.paramsTable[0]["F_CONTENT"] = (object) (value ? 1 : 0);
      this.ReloadCache();
    }
  }

  public void ValidateAssign(IDBAttributeType source)
  {
    this._clientSession.Guard.ValidateCall();
    this.attrType.ValidateAssign(source);
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
      if (this.attribute4.SourceAttributeID == value)
        return;
      this.attribute4.SourceAttributeID = value;
      this.paramsTable[0]["F_SOURCE_ID"] = (object) value;
      this.ReloadCache();
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
      if (this.attribute4.MasterAttributeID == value)
        return;
      this.attribute4.MasterAttributeID = value;
      this.paramsTable[0]["F_MASTER_ID"] = (object) value;
      this.ReloadCache();
    }
  }

  public RelationalOperators[] GetEnabledOperators(ColumnContents content)
  {
    this._clientSession.Guard.ValidateCall();
    return this.attrType.GetEnabledOperators(content);
  }

  public Guid GUID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return (this.attrType as IDBGuid).GUID;
    }
  }

  public bool IsSystemGUID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return (this.attrType as IDBGuid).IsSystemGUID;
    }
  }

  public string SubjectAreas
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return (this.attrType as IDBSubjectArea).SubjectAreas;
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      throw new OperationNotApplicableException();
    }
  }

  public string SubjectAreasCaption
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._clientSession.GetSubjectAreaCollection().GetAreasCaption(this.SubjectAreas);
    }
  }

  public string LanguageID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return (this.attrType as IDBLanguage).LanguageID;
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      throw new OperationNotApplicableException();
    }
  }

  public string LanguageName
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return (this.attrType as IDBLanguage).LanguageName;
    }
  }

  public bool IsDefaultLanguage
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return (this.attrType as IDBLanguage).IsDefaultLanguage;
    }
  }
}
