// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBDateAttribute
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.WebPortal;
using System;
using System.Data;


namespace Intermech.Kernel;

internal class DBDateAttribute : DBAdditionalAttribute
{
  private bool DontSetContentDate;

  public DBDateAttribute(
    UserSession uSession,
    DataRow attributeTypeRow,
    DataTable valuesTable,
    int values_index,
    DBAttributable parent)
    : base(uSession, attributeTypeRow, valuesTable, values_index, parent)
  {
  }

  public DBDateAttribute(
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
        return this._ValuesTable[this.Index]["F_DATE_VALUE"] == DBNull.Value;
      object calculatedValue = this.GetCalculatedValue((DBAttribute) null);
      return calculatedValue == DBNull.Value || calculatedValue == null;
    }
  }

  public override bool AsBoolean
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

  public override string AsString
  {
    get
    {
      return this.AttributeType.Mask == Consts.OnlyDateFunction && !this.IsNull ? this.AsDateTime.ToString("d") : this.AsDateTime.ToString();
    }
    set
    {
      if (value.ToString() == Consts.CurrentDateFunction)
        this.AsDateTime = DateTime.UtcNow + this.UserSession.TimeZoneOffset;
      else
        this.AsDateTime = Convert.ToDateTime(value);
    }
  }

  protected override void SetDefaultValue(object defValue)
  {
    if (defValue == null || defValue == DBNull.Value || !(defValue.ToString() != string.Empty))
      return;
    DateTime result;
    if (DateTime.TryParse(defValue.ToString(), out result))
      this.SetCalculatedValue((object) result, true);
    else
      this.SetCalculatedValue((object) (DateTime.UtcNow + this.UserSession.TimeZoneOffset), true);
  }

  public override object Value
  {
    get => this.IsNull ? (object) DBNull.Value : (object) this.AsDateTime;
    set
    {
      if (value == null || value == DBNull.Value || value.ToString() == string.Empty)
        this.Clear();
      else if (value.ToString() == Consts.CurrentDateFunction)
        this.AsDateTime = DateTime.UtcNow + this.UserSession.TimeZoneOffset;
      else
        this.AsDateTime = Convert.ToDateTime(value);
    }
  }

  protected override string GetInViewFieldName(AttributeValueField fldType)
  {
    return fldType == AttributeValueField.Date ? "F" + this.AttributeID.ToString() : string.Empty;
  }

  internal void WriteContentDate()
  {
    if (this.DontSetContentDate)
      return;
    this.DirectSetValue("F_DATE_VALUE", (object) (DateTime.UtcNow + this.UserSession.TimeZoneOffset));
    (this.ParentObject as DBObject).DoSetPublicationFlag(PublicationNecessary.Object);
    if (this.IsObjectAttribute && (MetaDataHelper.GetObjectType(this.TypeID).Options & ObjectTypeOptions.AutoCreateSnapshots) == ObjectTypeOptions.AutoCreateSnapshots)
      this.UserSession.AddAutoSnaphotToQueue(this.DBObjectID);
    if (this.IsObjectAttribute)
    {
      IMSAttribute4ObjectType attribute4ObjectType = MetaDataHelper.GetAttribute4ObjectType(this.TypeID, this.UserSession.IdentHelper.AttributeLastEditorID);
      if (attribute4ObjectType != null && !attribute4ObjectType.IsContent)
      {
        IDBAttribute byId = this.ParentObject.Attributes.FindByID(this.UserSession.IdentHelper.AttributeLastEditorID);
        if (byId != null)
          byId.AsInteger = this.UserSession.UserID;
        else
          this.ParentObject.Attributes.AddAttribute(this.UserSession.IdentHelper.AttributeLastEditorID, false, new object[1]
          {
            (object) this.UserSession.UserID
          });
      }
    }
    this.DontSetContentDate = true;
  }

  protected override string GetDescription()
  {
    return this.AttributeType.Mask == Consts.OnlyDateFunction && !this.IsNull ? this.AsDateTime.ToString("d") : base.GetDescription();
  }
}
