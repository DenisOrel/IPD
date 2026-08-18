// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBStringAttribute
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Data;


namespace Intermech.Kernel;

internal class DBStringAttribute : DBAdditionalAttribute
{
  public DBStringAttribute(
    UserSession uSession,
    DataRow attributeTypeRow,
    DataTable valuesTable,
    int values_index,
    DBAttributable parent)
    : base(uSession, attributeTypeRow, valuesTable, values_index, parent)
  {
  }

  public DBStringAttribute(
    UserSession uSession,
    DataRow attributeTypeRow,
    DataTable valuesTable,
    int values_index,
    bool temporary,
    DBAttributable parent)
    : base(uSession, attributeTypeRow, valuesTable, values_index, temporary, parent)
  {
  }

  public override bool AsBoolean
  {
    get
    {
      string asString = base.AsString;
      if (asString == LocalizationHolder.rm.GetString("Kernel_249"))
        return true;
      return !(asString == LocalizationHolder.rm.GetString("Kernel_250")) && Convert.ToBoolean(base.AsString);
    }
    set
    {
      if (value)
        this.AsString = LocalizationHolder.rm.GetString("Kernel_249");
      else
        this.AsString = LocalizationHolder.rm.GetString("Kernel_250");
    }
  }

  public override DateTime AsDateTime
  {
    get => Convert.ToDateTime(base.AsString);
    set => this.AsString = Convert.ToString(value);
  }

  public override double AsDouble
  {
    get => Convert.ToDouble(base.AsString);
    set => this.AsString = Convert.ToString(value);
  }

  public override long AsInteger
  {
    get => Convert.ToInt64(base.AsString);
    set => this.AsString = Convert.ToString(value);
  }

  public override bool IsNull
  {
    get
    {
      if (this.AttributeType.Computed != ComputeValueModes.JITValue)
        return this._ValuesTable[this.Index]["F_STRING_VALUE"] == DBNull.Value;
      object calculatedValue = this.GetCalculatedValue((DBAttribute) null);
      return calculatedValue == DBNull.Value || calculatedValue == null;
    }
  }

  protected override bool ValidateValue(object newValue)
  {
    if (newValue != null)
    {
      int int32 = Convert.ToInt32(this.AttributeType.SizeType);
      if (newValue.ToString().Length > int32)
        throw new KernelException(string.Format(LocalizationHolder.rm.GetString(sc_12570.ssp_appserver_12571()), (object) this.ObjectName, (object) int32));
    }
    return base.ValidateValue(newValue);
  }

  public override string AsString
  {
    get => base.AsString;
    set
    {
      this.ValidateValue((object) value);
      base.AsString = value;
    }
  }

  protected override void SetDefaultValue(object defValue)
  {
    base.SetDefaultValue(defValue);
    if (defValue == null || defValue == DBNull.Value)
      return;
    this.SetCalculatedValue((object) Convert.ToString(defValue), true);
  }

  public override object Value
  {
    get => this.IsNull ? (object) DBNull.Value : (object) this.AsString;
    set
    {
      if (value == null || value == DBNull.Value || value.ToString() == string.Empty)
        this.Clear();
      else
        this.AsString = Convert.ToString(value);
    }
  }

  protected override string GetInViewFieldName(AttributeValueField fldType)
  {
    return fldType == AttributeValueField.String ? "F" + this.AttributeID.ToString() : string.Empty;
  }

  protected override bool IsNullValue(object newValue)
  {
    return base.IsNullValue(newValue) || newValue.ToString() == string.Empty;
  }
}
