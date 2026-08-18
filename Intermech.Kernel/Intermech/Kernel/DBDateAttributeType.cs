// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBDateAttributeType
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;


namespace Intermech.Kernel;

internal class DBDateAttributeType : DBAttributeType
{
  public DBDateAttributeType(UserSession uSession, DataRow attributeRow)
    : base(uSession, attributeRow)
  {
    AttributeCacheHelper.GetAttributeTypeValues(FieldTypes.ftDateTime, this._AttributeID, ref this._ValueFieldName, ref this._TextFieldName, ref this._ConvertList, ref this._EnabledOperators, ref this._ComputableAttribute, ref this._PossibleValueFieldName);
    this._DataType = typeof (DateTime);
    this.CompatibleTypes = new FieldTypes[2]
    {
      FieldTypes.ftString,
      FieldTypes.ftDateTime
    };
  }

  internal override string ColumnSQL
  {
    get => $"{base.ColumnSQL} {this.UserSession.DataManager.DataProvider.DATEType}";
  }

  public override void ValidateSizeType(long newValue)
  {
    base.ValidateSizeType(newValue);
    this.CheckMaxSize(newValue, (long) Consts.MaxNumericSize);
  }

  public override void ValidateDefaultValue(object newValue)
  {
    if (newValue == null || !(newValue.ToString() != string.Empty) || !(newValue.ToString() != Consts.CurrentDateFunction))
      return;
    Convert.ToDateTime(newValue, (IFormatProvider) CultureInfo.InvariantCulture);
  }

  public override object DefaultValue
  {
    get
    {
      object defaultValue = base.DefaultValue;
      if (defaultValue == DBNull.Value || defaultValue.ToString() == string.Empty)
        return (object) null;
      return defaultValue.ToString() != null ? defaultValue : (object) Convert.ToDateTime(defaultValue, (IFormatProvider) CultureInfo.InvariantCulture);
    }
  }

  public override string SizeTypeDescription
  {
    get
    {
      return this.SizeType == 0L ? LocalizationHolder.rm.GetString("Kernel_132") : this.UserSession.DBCache.GetObjectInfo(this.UserSession.DataManager, this.SizeType).Caption;
    }
  }

  private void ConvertValues(string tblName)
  {
    this.UserSession.DataManager.ExecuteNonQuery($"UPDATE {tblName} SET F_STRING_VALUE = CAST(F_DATE_VALUE AS {this.UserSession.DataManager.DataProvider.NVARCHARType(Consts.CastStringSize)}) WHERE F_ATTRIBUTE_ID = {this.AttributeID}");
  }

  protected override void ValidateChangeAttributeType(FieldTypes newType)
  {
    base.ValidateChangeAttributeType(newType);
    if (newType != FieldTypes.ftString)
      return;
    List<string> objectAttrsTables = this.UserSession.DBCache.GetObjectAttrsTables();
    objectAttrsTables.Add("IMS_RELATION_ATTRS");
    objectAttrsTables.Add("IMS_OBJ_SNAPATTRS");
    objectAttrsTables.Add("IMS_REL_SNAPATTRS");
    for (int index = 0; index < objectAttrsTables.Count; ++index)
      this.ConvertValues(objectAttrsTables[index]);
    this.ClearValues("F_DATE_VALUE");
  }

  internal override bool CompareValues(object value1, object value2)
  {
    return CompareValuesHelper.CompareDateTimeValues(value1, value2);
  }
}
