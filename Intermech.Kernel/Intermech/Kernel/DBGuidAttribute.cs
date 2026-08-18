// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBGuidAttribute
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using System;
using System.Data;


namespace Intermech.Kernel;

internal class DBGuidAttribute : DBAdditionalAttribute, IDBGuidAttribute
{
  public DBGuidAttribute(
    UserSession uSession,
    DataRow attributeTypeRow,
    DataTable valuesTable,
    int values_index,
    DBAttributable parent)
    : base(uSession, attributeTypeRow, valuesTable, values_index, parent)
  {
  }

  public DBGuidAttribute(
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
    get => throw new OperationNotApplicableException();
    set => throw new OperationNotApplicableException();
  }

  public override DateTime AsDateTime
  {
    get => throw new OperationNotApplicableException();
    set => throw new OperationNotApplicableException();
  }

  public override double AsDouble
  {
    get => throw new OperationNotApplicableException();
    set => throw new OperationNotApplicableException();
  }

  public override long AsInteger
  {
    get => throw new OperationNotApplicableException();
    set => throw new OperationNotApplicableException();
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

  public override string AsString
  {
    get => base.AsString;
    set
    {
      if (!(value != this.AsString))
        return;
      if (value != string.Empty)
      {
        Guid guid;
        try
        {
          guid = new Guid(value);
        }
        catch
        {
          throw new KernelExceptionID(sc_12523.ssp_appserver_12524(1721567250), (object) value, (object) this.Name);
        }
        base.AsString = guid.ToString();
      }
      else
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

  bool IDBGuidAttribute.IsSystemGUID
  {
    get => this.AsString != string.Empty && SystemGUIDs.IsSystemGUID(this.AsString);
  }

  Guid IDBGuidAttribute.GUID
  {
    get => new Guid(this.AsString);
    set
    {
      if (!(this.AsString == string.Empty) && !(value != this.GUID))
        return;
      base.AsString = value.ToString();
    }
  }
}
