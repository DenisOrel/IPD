// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBLinkAttribute
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Data;


namespace Intermech.Kernel;

internal abstract class DBLinkAttribute : DBAdditionalAttribute
{
  public DBLinkAttribute(
    UserSession uSession,
    DataRow attributeTypeRow,
    DataTable valuesTable,
    int values_index,
    DBAttributable parent)
    : base(uSession, attributeTypeRow, valuesTable, values_index, parent)
  {
    this._AutoSaveHistory = false;
  }

  public DBLinkAttribute(
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

  protected override string GetDescription() => this.AsString;

  protected virtual string LinkTableName => string.Empty;

  protected override void SetDefaultValue(object defValue)
  {
    base.SetDefaultValue(defValue);
    if (defValue == null || defValue == DBNull.Value || !(defValue.ToString() != string.Empty))
      return;
    this.SetCalculatedValue((object) Convert.ToInt64(defValue), true);
  }

  internal override void ValidateRule(int attributeID, object newValue)
  {
  }

  public override bool IsNull => this._ValuesTable[this.Index]["F_INTEGER_VALUE"] == DBNull.Value;

  public override bool AsBoolean
  {
    get => Convert.ToBoolean(this.AsString);
    set => throw new OperationNotApplicableException();
  }

  public override DateTime AsDateTime
  {
    get => Convert.ToDateTime(this.AsString);
    set => throw new OperationNotApplicableException();
  }

  public override double AsDouble
  {
    get => Convert.ToDouble(this.AsInteger);
    set => throw new OperationNotApplicableException();
  }

  protected virtual void DeleteInLinkTable(bool clearMode)
  {
    if (this.TemporaryAttribute || !this.IsObjectAttribute)
      return;
    this.UserSession.DataManager.ExecuteNonQuery($"DELETE FROM {this.LinkTableName} WHERE F_OBJECT_ID = :objID AND F_ATTRIBUTE_ID = :attrID AND F_INLIST_ID = :listID", this.UserSession.DataManager.Parameter("objID", (object) this.DBObjectID), this.UserSession.DataManager.Parameter("attrID", (object) this.AttributeID), this.UserSession.DataManager.Parameter("listID", (object) this.Index));
    if (clearMode)
      return;
    this.UserSession.DataManager.ExecuteNonQuery($"UPDATE {this.LinkTableName} SET F_INLIST_ID = F_INLIST_ID - 1 WHERE F_OBJECT_ID = :objID AND F_ATTRIBUTE_ID = :attrID AND F_INLIST_ID > :index1", this.UserSession.DataManager.Parameter("objID", (object) this.DBObjectID), this.UserSession.DataManager.Parameter("attrID", (object) this.AttributeID), this.UserSession.DataManager.Parameter("index1", (object) this.Index));
  }

  protected override void DoDeleteValue()
  {
    this.DeleteInLinkTable(false);
    base.DoDeleteValue();
  }

  private void DeleteAllInLinkTable()
  {
    if (this.TemporaryAttribute || !this.IsObjectAttribute || this.IsCreationMode)
      return;
    this.UserSession.DataManager.ExecuteNonQuery($"DELETE FROM {this.LinkTableName} WHERE F_OBJECT_ID = :objID AND F_ATTRIBUTE_ID = :attrID", this.UserSession.DataManager.Parameter("objID", (object) this.DBObjectID), this.UserSession.DataManager.Parameter("attrID", (object) this.AttributeID));
  }

  protected override int DoDelete()
  {
    this.DeleteAllInLinkTable();
    return base.DoDelete();
  }

  internal override void Purge(bool purgeOwner)
  {
    this.DeleteAllInLinkTable();
    base.Purge(purgeOwner);
  }

  protected override string GetInViewFieldName(AttributeValueField fldType)
  {
    string inViewFieldName;
    switch (fldType)
    {
      case AttributeValueField.Integer:
        inViewFieldName = $"F{this.AttributeID.ToString()}ID";
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

  public override object Value
  {
    get
    {
      return this._ValuesTable[this.Index]["F_INTEGER_VALUE"] == DBNull.Value || this._ValuesTable[this.Index]["F_INTEGER_VALUE"] == null ? (object) DBNull.Value : (object) this.AsInteger;
    }
    set
    {
      if (value == null || value == DBNull.Value)
        this.Clear();
      else
        this.AsInteger = Convert.ToInt64(value);
    }
  }

  public abstract IDBObject DBObject { get; set; }

  internal abstract void InsertIntoObjectLink(long toObjectID, bool insertMode, int sign);

  protected abstract IDBObject GetObjectInternal(bool throwException);

  public virtual IDBObject GetObject(bool throwException)
  {
    IDBObject objectInternal = this.GetObjectInternal(throwException);
    return !(objectInternal == null & throwException) ? objectInternal : throw new ObjectNotFoundException(this.AsInteger);
  }
}
