// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBDoubleAttribute
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Data;
using System.Globalization;


namespace Intermech.Kernel;

internal class DBDoubleAttribute : DBNumericAttribute
{
  public DBDoubleAttribute(
    UserSession uSession,
    DataRow attributeTypeRow,
    DataTable valuesTable,
    int values_index,
    DBAttributable parent)
    : base(uSession, attributeTypeRow, valuesTable, values_index, parent)
  {
  }

  public DBDoubleAttribute(
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
        return this._ValuesTable[this.Index]["F_DOUBLE_VALUE"] == DBNull.Value;
      object calculatedValue = this.GetCalculatedValue((DBAttribute) null);
      return calculatedValue == DBNull.Value || calculatedValue == null;
    }
  }

  public override bool AsBoolean
  {
    get => this.AsDouble != 0.0;
    set
    {
      if (value)
        this.AsDouble = 1.0;
      else
        this.AsDouble = 0.0;
    }
  }

  public override DateTime AsDateTime
  {
    get => throw new OperationNotApplicableException();
    set => throw new OperationNotApplicableException();
  }

  public override long AsInteger
  {
    get => Convert.ToInt64(this.AsDouble);
    set => this.AsDouble = Convert.ToDouble(value);
  }

  public override string AsString
  {
    get => this.AsDouble.ToString();
    set => this.AsDouble = Convert.ToDouble(value);
  }

  protected override void SetDefaultValue(object defValue)
  {
    base.SetDefaultValue(defValue);
    if (defValue == null || defValue == DBNull.Value || !(defValue.ToString() != string.Empty))
      return;
    this.SetCalculatedValue((object) Convert.ToDouble(defValue, (IFormatProvider) CultureInfo.InvariantCulture), true);
  }

  public override object Value
  {
    get => this.IsNull ? (object) DBNull.Value : (object) this.AsDouble;
    set
    {
      if (value == null || value == DBNull.Value || value.ToString() == string.Empty)
        this.Clear();
      else
        this.AsDouble = Convert.ToDouble(value);
    }
  }

  protected override string GetInViewFieldName(AttributeValueField fldType)
  {
    return fldType == AttributeValueField.Double ? "F" + this.AttributeID.ToString() : string.Empty;
  }
}
