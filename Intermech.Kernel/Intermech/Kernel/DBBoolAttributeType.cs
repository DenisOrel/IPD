// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBBoolAttributeType
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel;

internal class DBBoolAttributeType : DBAttributeType
{
  public DBBoolAttributeType(UserSession uSession, DataRow attributeRow)
    : base(uSession, attributeRow)
  {
    AttributeCacheHelper.GetAttributeTypeValues(FieldTypes.ftBoolean, this._AttributeID, ref this._ValueFieldName, ref this._TextFieldName, ref this._ConvertList, ref this._EnabledOperators, ref this._ComputableAttribute, ref this._PossibleValueFieldName);
    this._DataType = typeof (long);
    this.CompatibleTypes = new FieldTypes[4]
    {
      FieldTypes.ftBoolean,
      FieldTypes.ftString,
      FieldTypes.ftInteger,
      FieldTypes.ftAutoInc
    };
  }

  internal override string ColumnSQL
  {
    get => $"{base.ColumnSQL} {this.UserSession.DataManager.DataProvider.SMALLINTType}";
  }

  public override void ValidateSizeType(long newValue)
  {
    base.ValidateSizeType(newValue);
    this.CheckMaxSize(newValue, (long) Consts.MaxNumericSize);
  }

  public override void ValidateDefaultValue(object newValue)
  {
    if (newValue == null || !(newValue.ToString() != string.Empty) || !(newValue.ToString().ToUpper() != Consts.TrueValue) || !(newValue.ToString().ToUpper() != Consts.FalseValue) || !(newValue.ToString().ToUpper() != Consts.YesValue.ToUpper()) || !(newValue.ToString().ToUpper() != Consts.NoValue.ToUpper()))
      return;
    Convert.ToBoolean(newValue);
  }

  public override string SizeTypeDescription => string.Empty;

  public override object DefaultValue
  {
    get
    {
      object defaultValue = base.DefaultValue;
      if (defaultValue == DBNull.Value || defaultValue.ToString() == string.Empty)
        return (object) null;
      if (defaultValue.ToString().ToUpper() == Consts.TrueValue)
        return (object) true;
      if (defaultValue.ToString().ToUpper() == Consts.FalseValue)
        return (object) false;
      if (defaultValue.ToString() == "0")
        return (object) false;
      return defaultValue.ToString() == "1" ? (object) true : (object) Convert.ToBoolean(defaultValue);
    }
  }

  protected override void ValidateChangeAttributeType(FieldTypes newType)
  {
    base.ValidateChangeAttributeType(newType);
    IDbManager dataManager = this.UserSession.DataManager;
    switch (newType)
    {
      case FieldTypes.ftString:
        List<string> objectAttrsTables1 = this.UserSession.DBCache.GetObjectAttrsTables();
        objectAttrsTables1.Add("IMS_RELATION_ATTRS");
        objectAttrsTables1.Add("IMS_OBJ_SNAPATTRS");
        objectAttrsTables1.Add("IMS_REL_SNAPATTRS");
        for (int index = 0; index < objectAttrsTables1.Count; ++index)
        {
          dataManager.ExecuteNonQuery($"UPDATE {objectAttrsTables1[index]} SET F_STRING_VALUE = {SqlHelper.QString(LocalizationHolder.rm.GetString("Kernel_120"))} WHERE F_ATTRIBUTE_ID = {this.AttributeID} AND F_INTEGER_VALUE <> 0");
          dataManager.ExecuteNonQuery($"UPDATE {objectAttrsTables1[index]} SET F_STRING_VALUE = {SqlHelper.QString(LocalizationHolder.rm.GetString("Kernel_121"))} WHERE F_ATTRIBUTE_ID = {this.AttributeID} AND F_INTEGER_VALUE = 0");
        }
        this.ClearValues("F_INTEGER_VALUE");
        break;
      case FieldTypes.ftInteger:
        List<string> objectAttrsTables2 = this.UserSession.DBCache.GetObjectAttrsTables();
        objectAttrsTables2.Add("IMS_RELATION_ATTRS");
        objectAttrsTables2.Add("IMS_OBJ_SNAPATTRS");
        objectAttrsTables2.Add("IMS_REL_SNAPATTRS");
        for (int index = 0; index < objectAttrsTables2.Count; ++index)
          dataManager.ExecuteNonQuery($"UPDATE {objectAttrsTables2[index]} SET F_INTEGER_VALUE = 1 WHERE F_ATTRIBUTE_ID = {this.AttributeID} AND F_INTEGER_VALUE <> 0");
        break;
      case FieldTypes.ftDouble:
        List<string> objectAttrsTables3 = this.UserSession.DBCache.GetObjectAttrsTables();
        objectAttrsTables3.Add("IMS_RELATION_ATTRS");
        objectAttrsTables3.Add("IMS_OBJ_SNAPATTRS");
        objectAttrsTables3.Add("IMS_REL_SNAPATTRS");
        for (int index = 0; index < objectAttrsTables3.Count; ++index)
        {
          dataManager.ExecuteNonQuery($"UPDATE {objectAttrsTables3[index]} SET F_DOUBLE_VALUE = 1 WHERE F_ATTRIBUTE_ID = {this.AttributeID} AND F_INTEGER_VALUE <> 0");
          dataManager.ExecuteNonQuery($"UPDATE {objectAttrsTables3[index]} SET F_DOUBLE_VALUE = 0 WHERE F_ATTRIBUTE_ID = {this.AttributeID} AND F_INTEGER_VALUE = 0");
        }
        this.ClearValues("F_INTEGER_VALUE");
        break;
    }
  }

  internal override bool CompareValues(object value1, object value2)
  {
    return CompareValuesHelper.CompareBoolValues(value1, value2);
  }
}
