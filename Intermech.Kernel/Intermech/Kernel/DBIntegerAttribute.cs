// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBIntegerAttribute
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Data;


namespace Intermech.Kernel;

internal class DBIntegerAttribute : DBNumericAttribute
{
  public DBIntegerAttribute(
    UserSession uSession,
    DataRow attributeTypeRow,
    DataTable valuesTable,
    int values_index,
    DBAttributable parent)
    : base(uSession, attributeTypeRow, valuesTable, values_index, parent)
  {
  }

  public DBIntegerAttribute(
    UserSession uSession,
    DataRow attributeTypeRow,
    DataTable valuesTable,
    int values_index,
    bool temporary,
    DBAttributable parent)
    : base(uSession, attributeTypeRow, valuesTable, values_index, temporary, parent)
  {
  }

  public override bool IsNull
  {
    get
    {
      if (this.AttributeType.Computed != ComputeValueModes.JITValue)
        return this._ValuesTable[this.Index]["F_INTEGER_VALUE"] == DBNull.Value;
      object calculatedValue = this.GetCalculatedValue((DBAttribute) null);
      return calculatedValue == DBNull.Value || calculatedValue == null;
    }
  }

  public override bool AsBoolean
  {
    get => this.AsInteger != 0L;
    set
    {
      if (value)
        this.AsInteger = 1L;
      else
        this.AsInteger = 0L;
    }
  }

  public override double AsDouble
  {
    get => Convert.ToDouble(this.AsInteger);
    set => this.AsInteger = Convert.ToInt64(value);
  }

  public override string AsString
  {
    get => this.AsInteger.ToString();
    set => this.AsInteger = Convert.ToInt64(value);
  }

  protected override void SetDefaultValue(object defValue)
  {
    base.SetDefaultValue(defValue);
    if (defValue == null || defValue == DBNull.Value || !(defValue.ToString() != string.Empty))
      return;
    this.SetCalculatedValue((object) Convert.ToInt64(defValue), true);
  }

  public override object Value
  {
    get => this.IsNull ? (object) DBNull.Value : (object) this.AsInteger;
    set
    {
      if (value == null || value == DBNull.Value || value.ToString() == string.Empty)
        this.Clear();
      else
        this.AsInteger = Convert.ToInt64(value);
    }
  }

  protected override string GetInViewFieldName(AttributeValueField fldType)
  {
    return fldType == AttributeValueField.Integer ? "F" + this.AttributeID.ToString() : string.Empty;
  }
}
