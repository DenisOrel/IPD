// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBAutoincrementAttributeType
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel;

internal class DBAutoincrementAttributeType : DBAttributeType
{
  public DBAutoincrementAttributeType(UserSession uSession, DataRow attributeRow)
    : base(uSession, attributeRow)
  {
    AttributeCacheHelper.GetAttributeTypeValues(FieldTypes.ftAutoInc, this._AttributeID, ref this._ValueFieldName, ref this._TextFieldName, ref this._ConvertList, ref this._EnabledOperators, ref this._ComputableAttribute, ref this._PossibleValueFieldName);
    this._DataType = typeof (long);
    this._CanStorePossibleValues = false;
    this._UniquedAttribute = true;
    this.CompatibleTypes = new FieldTypes[5]
    {
      FieldTypes.ftAutoInc,
      FieldTypes.ftInteger,
      FieldTypes.ftDouble,
      FieldTypes.ftObjectLink,
      FieldTypes.ftString
    };
  }

  internal override string ColumnSQL
  {
    get => $"{base.ColumnSQL} {this.UserSession.DataManager.DataProvider.INTEGERType}";
  }

  public override void DoAfterCreate()
  {
    this.UserSession.DataManager.ExecuteNonQuery(this.UserSession.DataManager.DataProvider.CreateGeneratorString($"IMT_A{this.AttributeID.ToString()}_GEN", 1L, 1));
    base.DoAfterCreate();
  }

  public override void ValidateSizeType(long newValue)
  {
    base.ValidateSizeType(newValue);
    this.CheckMaxSize(newValue, (long) Consts.MaxNumericSize);
  }

  protected override void ValidateChangeAttributeType(FieldTypes newType)
  {
    base.ValidateChangeAttributeType(newType);
    string str = string.Empty;
    switch (newType)
    {
      case FieldTypes.ftString:
        str = $"{sc_12676.ssp_appserver_12678()}{this.UserSession.DataManager.DataProvider.NVARCHARType(Convert.ToInt32(this.SizeType))})";
        break;
      case FieldTypes.ftDouble:
        str = sc_12676.ssp_appserver_12677();
        break;
    }
    if (!(str != string.Empty))
      return;
    IDbManager dataManager = this.UserSession.DataManager;
    List<string> objectAttrsTables = this.UserSession.DBCache.GetObjectAttrsTables();
    objectAttrsTables.Add("IMS_RELATION_ATTRS");
    objectAttrsTables.Add("IMS_OBJ_SNAPATTRS");
    objectAttrsTables.Add("IMS_REL_SNAPATTRS");
    for (int index = 0; index < objectAttrsTables.Count; ++index)
      dataManager.ExecuteNonQuery($"UPDATE {objectAttrsTables[index]} SET {str} WHERE F_ATTRIBUTE_ID = {this.AttributeID}");
    this.ClearValues("F_INTEGER_VALUE");
  }

  public override string SizeTypeDescription => string.Empty;

  internal override bool CompareValues(object value1, object value2)
  {
    return CompareValuesHelper.CompareIntValues(value1, value2);
  }
}
