// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBMeasureAttribute
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Data;


namespace Intermech.Kernel;

internal class DBMeasureAttribute : DBAdditionalAttribute, IDBMeasureAttribute
{
  public DBMeasureAttribute(
    UserSession uSession,
    DataRow attributeTypeRow,
    DataTable valuesTable,
    int values_index,
    DBAttributable parent)
    : base(uSession, attributeTypeRow, valuesTable, values_index, parent)
  {
    this._AutoSaveHistory = false;
  }

  public DBMeasureAttribute(
    UserSession uSession,
    DataRow attributeTypeRow,
    DataTable valuesTable,
    int values_index,
    bool temporary,
    DBAttributable parent)
    : base(uSession, attributeTypeRow, valuesTable, values_index, temporary, parent)
  {
    this._AutoSaveHistory = false;
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

  public override long AsInteger
  {
    get => Convert.ToInt64(base.AsDouble);
    set => throw new OperationNotApplicableException();
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

  public override string AsString
  {
    get => base.AsString;
    set
    {
      if (!(this.AsString != value))
        return;
      if (value == string.Empty)
      {
        this.Clear();
      }
      else
      {
        long defaultMeasureId = (this.AttributeType as IDBMeasureAttributeType).DefaultMeasureID;
        if (defaultMeasureId > 0L)
          ((IDBMeasureAttribute) this).Value = MeasureHelper.ConvertToMeasuredValue(value, MeasureHelper.FindDescriptor(defaultMeasureId), true);
        else
          ((IDBMeasureAttribute) this).Value = MeasureHelper.ConvertToMeasuredValue(value);
      }
    }
  }

  public long MeasureID
  {
    get => base.AsInteger;
    set
    {
      base.AsString = this.IsCompatible(value) ? MeasureHelper.ConvertToString(base.AsDouble, value, true) : throw new KernelException(string.Format(LocalizationHolder.rm.GetString(sc_12530.ssp_appserver_12531()), (object) this.Name, (object) value));
      this.SaveHistoryValues(true);
    }
  }

  public override double AsDouble
  {
    get => base.AsDouble;
    set => throw new OperationNotApplicableException();
  }

  protected override string ValidationRuleFormula
  {
    get => (this.AttributeType as IDBMeasureAttributeType).RuleFormula;
  }

  private void SetMeasuredValue(MeasuredValue mValue)
  {
  }

  MeasuredValue IDBMeasureAttribute.Value
  {
    get => new MeasuredValue(base.AsDouble, base.AsInteger, this.AsString);
    set
    {
      if (!MeasureHelper.IsNewValue(value, ((IDBMeasureAttribute) this).Value))
        return;
      if (value.MeasureID == 0L)
      {
        this.Clear();
      }
      else
      {
        long measureId1 = value.MeasureID;
        MeasureDescriptor descriptor1 = MeasureHelper.FindDescriptor(value);
        if (descriptor1.Empty)
          throw new KernelExceptionID(sc_12530.ssp_appserver_12532(776714282), (object) measureId1);
        IDBMeasureAttributeType attributeType = this.AttributeType as IDBMeasureAttributeType;
        attributeType.ValidateMuID(measureId1);
        if (attributeType.ConvertToDefaultMeasure && attributeType.DefaultMeasureID > 0L)
        {
          value = MeasureHelper.ConvertToMeasuredValue(value, attributeType.DefaultMeasureID);
          long defaultMeasureId = attributeType.DefaultMeasureID;
        }
        double num1 = value.Value * descriptor1.K;
        double num2 = value.Value;
        string shortName = descriptor1.ShortName;
        string str = string.Empty;
        long measureId2 = MeasureHelper.FindBaseValue(descriptor1).MeasureID;
        string muShortName;
        if (value.Caption != string.Empty && MeasureHelper.Instance.ParseString(value.Caption, out double _, out muShortName, false) && muShortName != string.Empty)
        {
          if (attributeType.ShortNameInString || value.MeasureID != attributeType.DefaultMeasureID)
          {
            MeasureDescriptor descriptor2 = MeasureHelper.FindDescriptor(muShortName);
            if (!descriptor2.Empty && descriptor2.PhysicalQuantityID == descriptor1.PhysicalQuantityID)
              str = value.Caption;
          }
          else
            str = num2.ToString("#################0.#################");
        }
        if (str == string.Empty)
          str = attributeType.ShortNameInString || value.MeasureID != attributeType.DefaultMeasureID ? $"{num2.ToString("#################0.#################")} {shortName}" : num2.ToString("#################0.#################");
        object obj1 = this._ValuesTable[this.Index]["F_INTEGER_VALUE"];
        object obj2 = this._ValuesTable[this.Index]["F_STRING_VALUE"];
        this.UserSession.StartTransaction();
        try
        {
          this.ValidateMultiValueWrite("F_STRING_VALUE", (object) str);
          this.DirectSetValues((object) str, (object) measureId2, (object) null, (object) null);
          base.AsDouble = num1;
          this.SaveHistoryValues(true);
          this.ParentObject.AfterSetComplexAttributeValue((IDBAttribute) this);
          this.GenerateDelayedNotification((object) str);
          this.UserSession.Commit();
        }
        catch
        {
          this.UserSession.Rollback();
          this._ValuesTable[this.Index]["F_INTEGER_VALUE"] = obj1;
          this._ValuesTable[this.Index]["F_STRING_VALUE"] = obj2;
          throw;
        }
      }
    }
  }

  public override object Value
  {
    get => this.IsNull ? (object) DBNull.Value : (object) ((IDBMeasureAttribute) this).Value;
    set
    {
      if (value == null || value == DBNull.Value || value.ToString() == string.Empty)
      {
        this.Clear();
      }
      else
      {
        switch (value)
        {
          case MeasuredValue _:
            ((IDBMeasureAttribute) this).Value = (MeasuredValue) value;
            break;
          case string _:
            this.AsString = value.ToString();
            break;
          default:
            throw new OperationNotApplicableException();
        }
      }
    }
  }

  public string MeasureShortName => MeasureHelper.FindDescriptor(base.AsInteger).ShortName;

  public string MeasureName => MeasureHelper.FindDescriptor(base.AsInteger).LongName;

  public bool IsCompatible(long aMeasureID)
  {
    MeasureDescriptor descriptor = MeasureHelper.FindDescriptor(aMeasureID);
    if (descriptor.Empty)
      return false;
    return this.AttributeType.SizeType <= 0L || descriptor.PhysicalQuantityID == this.AttributeType.SizeType;
  }

  protected override string GetInViewFieldName(AttributeValueField fldType)
  {
    string inViewFieldName;
    switch (fldType)
    {
      case AttributeValueField.Integer:
        inViewFieldName = $"F{this.AttributeID.ToString()}ID";
        break;
      case AttributeValueField.Double:
        inViewFieldName = $"F{this.AttributeID.ToString()}ID2";
        break;
      case AttributeValueField.String:
        inViewFieldName = "F" + this.AttributeID.ToString();
        break;
      default:
        inViewFieldName = string.Empty;
        break;
    }
    return inViewFieldName;
  }

  protected override string GetDescription() => this.AsString;

  protected override void SetDefaultValue(object defValue)
  {
    if (defValue == null || defValue == DBNull.Value || !(defValue.ToString() != string.Empty))
      return;
    MeasuredValue measuredValue = MeasureHelper.ConvertToMeasuredValue(defValue is double num ? num.ToString("#################0.#################") : defValue.ToString());
    MeasureDescriptor descriptor = MeasureHelper.FindDescriptor(measuredValue);
    this.UserSession.StartTransaction();
    try
    {
      this.DirectSetValues((object) measuredValue.Caption, (object) MeasureHelper.GetBaseMeasureID(descriptor.PhysicalQuantityID), (object) (measuredValue.Value * descriptor.K), (object) null);
      this.ParentObject.AfterSetComplexAttributeValue((IDBAttribute) this);
      this.UserSession.Commit();
    }
    catch
    {
      this.UserSession.Rollback();
      throw;
    }
  }
}
