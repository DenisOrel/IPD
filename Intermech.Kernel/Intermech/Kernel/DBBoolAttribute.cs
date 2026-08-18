// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBBoolAttribute
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Data;


namespace Intermech.Kernel;

internal class DBBoolAttribute : DBNumericAttribute
{
  public DBBoolAttribute(
    UserSession uSession,
    DataRow attributeTypeRow,
    DataTable valuesTable,
    int values_index,
    DBAttributable parent)
    : base(uSession, attributeTypeRow, valuesTable, values_index, parent)
  {
  }

  public DBBoolAttribute(
    UserSession uSession,
    DataRow attributeTypeRow,
    DataTable valuesTable,
    int values_index,
    bool temporary,
    DBAttributable parent)
    : base(uSession, attributeTypeRow, valuesTable, values_index, temporary, parent)
  {
  }

  public override long AsInteger
  {
    get
    {
      long asInteger = base.AsInteger;
      if (asInteger != 0L)
        asInteger = 1L;
      return asInteger;
    }
    set
    {
      if (value != 0L)
        base.AsInteger = 1L;
      else
        base.AsInteger = 0L;
    }
  }

  public override string AsString
  {
    get
    {
      return base.AsInteger == 0L ? LocalizationHolder.rm.GetString("Kernel_233") : LocalizationHolder.rm.GetString("Kernel_234");
    }
    set
    {
      if (value.ToUpper() == LocalizationHolder.rm.GetString("Kernel_235") || value.ToUpper() == "TRUE" || value.ToUpper() == LocalizationHolder.rm.GetString("Kernel_236"))
      {
        base.AsInteger = 1L;
      }
      else
      {
        if (!(value.ToUpper() == LocalizationHolder.rm.GetString("Kernel_237")) && !(value.ToUpper() == "FALSE") && !(value.ToUpper() == LocalizationHolder.rm.GetString("Kernel_238")))
          throw new KernelException(string.Format(LocalizationHolder.rm.GetString(sc_12501.ssp_appserver_12502()), (object) value, (object) this.Name));
        base.AsInteger = 0L;
      }
    }
  }

  public override double AsDouble
  {
    get => Convert.ToDouble(this.AsInteger);
    set => this.AsInteger = Convert.ToInt64(value);
  }

  public override DateTime AsDateTime
  {
    get => throw new OperationNotApplicableException();
    set => throw new OperationNotApplicableException();
  }

  public override bool IsNull => this._ValuesTable[this.Index]["F_INTEGER_VALUE"] == DBNull.Value;

  protected override void SetDefaultValue(object defValue)
  {
    base.SetDefaultValue(defValue);
    if (defValue == null || defValue == DBNull.Value || !(defValue.ToString() != string.Empty))
      return;
    this.SetCalculatedValue((object) Convert.ToBoolean(defValue), true);
  }

  public override object Value
  {
    get => this.IsNull ? (object) DBNull.Value : (object) this.AsBoolean;
    set
    {
      if (value == null || value == DBNull.Value || value.ToString() == string.Empty)
        this.Clear();
      else
        this.AsBoolean = Convert.ToBoolean(value);
    }
  }

  protected override string GetInViewFieldName(AttributeValueField fldType)
  {
    return fldType == AttributeValueField.Integer ? "F" + this.AttributeID.ToString() : string.Empty;
  }

  internal override void SetCalculatedValue(object newValue, bool postedWrite)
  {
    base.SetCalculatedValue((object) Convert.ToInt64(newValue), postedWrite);
  }
}
