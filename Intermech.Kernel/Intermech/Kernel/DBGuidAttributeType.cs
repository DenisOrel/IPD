// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBGuidAttributeType
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Data;


namespace Intermech.Kernel;

internal class DBGuidAttributeType : DBAttributeType
{
  public DBGuidAttributeType(UserSession uSession, DataRow attributeRow)
    : base(uSession, attributeRow)
  {
    AttributeCacheHelper.GetAttributeTypeValues(FieldTypes.ftGuid, this._AttributeID, ref this._ValueFieldName, ref this._TextFieldName, ref this._ConvertList, ref this._EnabledOperators, ref this._ComputableAttribute, ref this._PossibleValueFieldName);
    this._UniquedAttribute = true;
    this.CompatibleTypes = new FieldTypes[2]
    {
      FieldTypes.ftGuid,
      FieldTypes.ftString
    };
  }

  internal override string ColumnSQL
  {
    get => $"{base.ColumnSQL} {this.UserSession.DataManager.DataProvider.NVARCHARType(36)}";
  }

  public override void ValidateDefaultValue(object newValue)
  {
    if (newValue == null || !(newValue.ToString() != string.Empty))
      return;
    Guid guid = new Guid(newValue.ToString());
  }

  public override void ValidateSizeType(long newValue)
  {
    base.ValidateSizeType(newValue);
    this.CheckMaxSize(newValue, (long) Consts.MaxNumericSize);
  }

  public override string SizeTypeDescription => string.Empty;

  internal override bool CompareValues(object value1, object value2)
  {
    return CompareValuesHelper.CompareStringValues(value1, value2);
  }
}
