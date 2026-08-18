// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBDoubleAttributeType
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;


namespace Intermech.Kernel;

internal class DBDoubleAttributeType : DBAttributeType
{
  public DBDoubleAttributeType(UserSession uSession, DataRow attributeRow)
    : base(uSession, attributeRow)
  {
    AttributeCacheHelper.GetAttributeTypeValues(FieldTypes.ftDouble, this._AttributeID, ref this._ValueFieldName, ref this._TextFieldName, ref this._ConvertList, ref this._EnabledOperators, ref this._ComputableAttribute, ref this._PossibleValueFieldName);
    this._DataType = typeof (double);
    this._UniquedAttribute = true;
    this.CompatibleTypes = new FieldTypes[5]
    {
      FieldTypes.ftDouble,
      FieldTypes.ftInteger,
      FieldTypes.ftAutoInc,
      FieldTypes.ftMeasured,
      FieldTypes.ftString
    };
  }

  internal override string ColumnSQL
  {
    get => $"{base.ColumnSQL} {this.UserSession.DataManager.DataProvider.FLOATType}";
  }

  public override void ValidateSizeType(long newValue)
  {
    base.ValidateSizeType(newValue);
    this.CheckMaxSize(newValue, (long) Consts.MaxNumericSize);
  }

  public override string SizeTypeDescription => string.Empty;

  public override void ValidateDefaultValue(object newValue)
  {
    if (newValue == null || !(newValue.ToString() != string.Empty))
      return;
    Convert.ToDouble(newValue, (IFormatProvider) CultureInfo.InvariantCulture);
  }

  public override object DefaultValue
  {
    set
    {
      if (value != null && value.ToString() != string.Empty)
        base.DefaultValue = (object) Convert.ToString(value, (IFormatProvider) CultureInfo.InvariantCulture);
      else
        base.DefaultValue = value;
    }
    get
    {
      object defaultValue = base.DefaultValue;
      return defaultValue == DBNull.Value || defaultValue == null || defaultValue.ToString() == string.Empty ? (object) DBNull.Value : (object) Convert.ToDouble(defaultValue, (IFormatProvider) CultureInfo.InvariantCulture);
    }
  }

  private void ConvertValues(string convert_str, string tblName, FieldTypes newType)
  {
    if (newType == FieldTypes.ftBoolean)
    {
      this.UserSession.DataManager.ExecuteNonQuery($"UPDATE {tblName} SET F_INTEGER_VALUE = 1 WHERE F_ATTRIBUTE_ID = {this.AttributeID} AND F_DOUBLE_VALUE <> 0");
      this.UserSession.DataManager.ExecuteNonQuery($"UPDATE {tblName} SET F_INTEGER_VALUE = 0 WHERE F_ATTRIBUTE_ID = {this.AttributeID} AND F_DOUBLE_VALUE = 0");
    }
    else
      this.UserSession.DataManager.ExecuteNonQuery($"UPDATE {tblName} SET {convert_str} WHERE F_ATTRIBUTE_ID = {this.AttributeID}");
  }

  protected override void ValidateChangeAttributeType(FieldTypes newType)
  {
    base.ValidateChangeAttributeType(newType);
    if (FieldTypes.ftMeasured == newType)
    {
      IDBObject dbObject = this.SizeType > 0L ? this.UserSession.GetObject(this.SizeType) : throw new KernelExceptionID(362, (object) this.Name);
      if (dbObject.ObjectType != this.UserSession.IdentHelper.PhysicValueTypeID)
        throw new KernelExceptionID(363, (object) dbObject.NameInMessages, (object) dbObject.ObjectID).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(dbObject.ObjectID));
      MeasureDescriptor defaultMeasure = MeasureHelper.GetDefaultMeasure(this.SizeType);
      long baseMeasureId = MeasureHelper.GetBaseMeasureID(this.SizeType);
      if (baseMeasureId < 0L)
        throw new KernelException(string.Format(sc_12685.ssp_appserver_12686(), (object) dbObject.NameInMessages));
      MeasureHelper.FindDescriptor(baseMeasureId);
      if (defaultMeasure == null)
        throw new KernelExceptionID(364, (object) dbObject.NameInMessages);
      List<string> objectAttrsTables = this.UserSession.DBCache.GetObjectAttrsTables();
      objectAttrsTables.Add("IMS_RELATION_ATTRS");
      objectAttrsTables.Add("IMS_OBJ_SNAPATTRS");
      objectAttrsTables.Add("IMS_REL_SNAPATTRS");
      IDbManager dataManager = this.UserSession.DataManager;
      for (int index = 0; index < objectAttrsTables.Count; ++index)
      {
        dataManager.ExecuteNonQuery(string.Format("UPDATE {0} SET F_STRING_VALUE = CAST(F_DOUBLE_VALUE AS {1}){3}' {2}' WHERE F_ATTRIBUTE_ID = :attrID AND F_DOUBLE_VALUE IS NOT NULL", (object) objectAttrsTables[index], (object) this.UserSession.DataManager.DataProvider.NVARCHARType(Consts.CastStringSize), (object) defaultMeasure.ShortName, (object) this.UserSession.DataManager.DataProvider.ConcatStringOperator), dataManager.Parameter("attrID", (object) this.AttributeID));
        dataManager.ExecuteNonQuery($"UPDATE {objectAttrsTables[index]} SET F_DOUBLE_VALUE = F_DOUBLE_VALUE * :koef, F_INTEGER_VALUE = :baseID WHERE F_ATTRIBUTE_ID = :attrID AND F_DOUBLE_VALUE IS NOT NULL", dataManager.Parameter("attrID", (object) this.AttributeID), dataManager.Parameter("koef", (object) defaultMeasure.K), dataManager.Parameter("baseID", (object) baseMeasureId));
      }
    }
    else
    {
      string convert_str = string.Empty;
      if (FieldTypes.ftString == newType)
        convert_str = $"F_STRING_VALUE = CAST(F_DOUBLE_VALUE AS {this.UserSession.DataManager.DataProvider.NVARCHARType(Convert.ToInt32(this.SizeType))})";
      else if (FieldTypes.ftInteger == newType)
        convert_str = "F_INTEGER_VALUE = CAST(F_DOUBLE_VALUE AS INTEGER)";
      List<string> objectAttrsTables = this.UserSession.DBCache.GetObjectAttrsTables();
      objectAttrsTables.Add("IMS_RELATION_ATTRS");
      objectAttrsTables.Add("IMS_OBJ_SNAPATTRS");
      objectAttrsTables.Add("IMS_REL_SNAPATTRS");
      for (int index = 0; index < objectAttrsTables.Count; ++index)
        this.ConvertValues(convert_str, objectAttrsTables[index], newType);
      this.ClearValues("F_DOUBLE_VALUE");
    }
  }

  internal override bool CompareValues(object value1, object value2)
  {
    return CompareValuesHelper.CompareFloatValues(value1, value2);
  }
}
