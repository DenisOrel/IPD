// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBObjectLinkByIDAttribute
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using System;
using System.Data;


namespace Intermech.Kernel;

internal class DBObjectLinkByIDAttribute : DBLinkAttribute
{
  public DBObjectLinkByIDAttribute(
    UserSession uSession,
    DataRow attributeTypeRow,
    DataTable valuesTable,
    int values_index,
    DBAttributable parent)
    : base(uSession, attributeTypeRow, valuesTable, values_index, parent)
  {
  }

  public DBObjectLinkByIDAttribute(
    UserSession uSession,
    DataRow attributeTypeRow,
    DataTable valuesTable,
    int values_index,
    bool temporary,
    DBAttributable parent)
    : base(uSession, attributeTypeRow, valuesTable, values_index, temporary, parent)
  {
  }

  protected override string LinkTableName => "IMS_ID_LINKS";

  public override long AsInteger
  {
    get => base.AsInteger;
    set
    {
      if (this.AsInteger == Math.Abs(value))
        return;
      if (value == 0L)
      {
        this.Clear();
      }
      else
      {
        IDBObject objectBaseVersionById = this.UserSession.GetObjectBaseVersionByID(value, true);
        if (objectBaseVersionById.IsCreationMode && !this.IsCreationMode)
          throw new KernelExceptionID(sc_12548.ssp_appserver_12549(368286251));
        this.SetDBObject(objectBaseVersionById);
      }
    }
  }

  public override string AsString
  {
    get => base.AsString;
    set
    {
      if (value == null || !(value.ToString() == Consts.CurrentUserFunction))
        throw new OperationNotApplicableException();
      base.AsInteger = this.UserSession.ID;
    }
  }

  internal override void InsertIntoObjectLink(long toID, bool insertMode, int sign)
  {
    if (this.TemporaryAttribute || !this.IsObjectAttribute)
      return;
    if (insertMode || this.IsNull || Convert.ToInt64(this._ValuesTable[this.Index]["F_INTEGER_VALUE"]) == 0L)
    {
      try
      {
        this.UserSession.DataManager.ExecuteNonQuery("INSERT INTO IMS_ID_LINKS (F_OBJECT_ID, F_ATTRIBUTE_ID, F_INLIST_ID, F_TO_ID) VALUES (:objID, :attrID, :listID, :toID)", this.UserSession.DataManager.Parameter("objID", (object) (this.DBObjectID * (long) sign)), this.UserSession.DataManager.Parameter("attrID", (object) this.AttributeID), this.UserSession.DataManager.Parameter("listID", (object) this.Index), this.UserSession.DataManager.Parameter(nameof (toID), (object) toID));
      }
      catch
      {
      }
    }
    else
      this.UserSession.DataManager.ExecuteNonQuery("UPDATE IMS_ID_LINKS SET F_TO_ID = :toObjID WHERE F_OBJECT_ID = :objID AND F_ATTRIBUTE_ID = :attrID AND F_INLIST_ID = :listID", this.UserSession.DataManager.Parameter("objID", (object) this.DBObjectID), this.UserSession.DataManager.Parameter("attrID", (object) this.AttributeID), this.UserSession.DataManager.Parameter("listID", (object) this.Index), this.UserSession.DataManager.Parameter("toObjID", (object) toID));
  }

  protected override void DoClear()
  {
    base.DoClear();
    this.DeleteInLinkTable(true);
  }

  private void SetDBObject(IDBObject dbObject)
  {
    int objectType = dbObject.ObjectType;
    bool flag = !this.IsCreationMode;
    long id = dbObject.ID;
    if (this._Attributes == null || (this._Attributes.AssignMode & Consts.CheckOutMode) == 0)
      (this.AttributeType as IDBObjectLinkByIDAttributeType).ValidateObjectType(objectType);
    this.UserSession.StartTransaction();
    try
    {
      if (flag)
        this.InsertIntoObjectLink(id, false, 1);
      this.DirectSetValue("F_STRING_VALUE", (object) dbObject.Caption);
      base.AsInteger = id;
      this.SaveHistoryValues(true);
      this.ParentObject.AfterSetComplexAttributeValue((IDBAttribute) this);
      this.UserSession.Commit();
    }
    catch
    {
      this.UserSession.Rollback();
      throw;
    }
  }

  protected override string GetValueDescription(object val)
  {
    IDBObject objectBaseVersionById = this.UserSession.GetObjectBaseVersionByID(Convert.ToInt64(val), false);
    return objectBaseVersionById == null ? val.ToString() : objectBaseVersionById.Caption;
  }

  protected override IDBObject GetObjectInternal(bool throwException)
  {
    IDBObject objectBaseVersionById = this.UserSession.GetObjectBaseVersionByID(this.AsInteger, throwException);
    if (objectBaseVersionById != null && objectBaseVersionById.CheckoutBy == this.UserSession.UserID && objectBaseVersionById.ObjectID > 0L)
      objectBaseVersionById = this.UserSession.GetObject(-objectBaseVersionById.ObjectID);
    return objectBaseVersionById;
  }

  public override IDBObject DBObject
  {
    get => this.IsNull ? (IDBObject) null : this.GetObjectInternal(true);
    set
    {
      if (value == null)
      {
        this.Clear();
      }
      else
      {
        if (value.ID == this.AsInteger)
          return;
        if (value.ObjectID < 0L && !this.IsCreationMode && value.IsCreationMode)
          throw new KernelExceptionID(sc_12548.ssp_appserver_12550(1487011073));
        this.SetDBObject(value);
      }
    }
  }

  protected override void SetDefaultValue(object defValue)
  {
    if (defValue is string && defValue.ToString() == Consts.CurrentUserFunction)
      defValue = (object) this.UserSession.ID;
    base.SetDefaultValue(defValue);
    if (defValue == null || !(defValue.ToString() != string.Empty))
      return;
    if (defValue.ToString() == Consts.CurrentUserFunction)
      defValue = (object) this.UserSession.ID;
    long int64 = Convert.ToInt64(defValue);
    IDBObject objectBaseVersionById = this.UserSession.GetObjectBaseVersionByID(int64, true);
    if (this._Attributes != null)
      this._Attributes.AddDeltaValue(this.AttributeID);
    this.CheckUniqueValue(new object[1]{ defValue }, true);
    if (!this.TemporaryAttribute)
    {
      this.UserSession.DataManager.ExecuteNonQuery($"UPDATE {this.ValuesTableName} SET F_INTEGER_VALUE = :val, F_STRING_VALUE = :capt WHERE {this.ValuesKeyName} = {this.DBObjectID} AND F_ATTRIBUTE_ID = {this.AttributeID} AND F_INLIST_ID = {this.Index}", this.UserSession.DataManager.Parameter("val", (object) int64), this.UserSession.DataManager.Parameter("capt", (object) objectBaseVersionById.Caption));
      this.UpdateViewValue(this.GetInViewFieldName(AttributeValueField.Integer), (object) int64, this.DBObjectID);
      this.UpdateViewValue(this.GetInViewFieldName(AttributeValueField.String), (object) objectBaseVersionById.Caption, this.DBObjectID);
    }
    this._ValuesTable[this.Index]["F_INTEGER_VALUE"] = (object) int64;
    this._ValuesTable[this.Index]["F_STRING_VALUE"] = (object) objectBaseVersionById.Caption;
    if (!this.IsCreationMode)
      this.InsertIntoObjectLink(int64, true, 1);
    this.ChangeComputedValues(true);
    this.ParentObject.AfterSetComplexAttributeValue((IDBAttribute) this);
  }

  public override object Value
  {
    set
    {
      switch (value)
      {
        case IDBObject _:
          this.SetDBObject(value as IDBObject);
          return;
        case Guid guid:
          this.DBObjectGUID = guid;
          return;
        case string _:
          if (value.ToString() == Consts.CurrentUserFunction)
          {
            this.SetDBObject(this.UserSession.GetObject(this.UserSession.UserID));
            return;
          }
          break;
      }
      base.Value = value;
    }
  }

  public Guid DBObjectGUID
  {
    get
    {
      if (this.AsInteger <= 0L)
        return Guid.Empty;
      object obj = this.UserSession.DataManager.ExecuteScalar("SELECT F_GUID FROM IMS_GUID_RESOLVE WHERE F_ID = :id AND F_CATEGORY_TYPE = " + 2.ToString(), this.UserSession.DataManager.Parameter("id", (object) this.AsInteger));
      if (obj == null || obj == DBNull.Value)
        return Guid.Empty;
      return obj is Guid guid ? guid : new Guid(obj.ToString());
    }
    set
    {
      if (!(value != this.DBObjectGUID))
        return;
      object obj = this.UserSession.DataManager.ExecuteScalar("SELECT F_ID FROM IMS_GUID_RESOLVE WHERE F_GUID = :guid_par", this.UserSession.DataManager.Parameter("guid_par", (object) value));
      if (obj == null || obj == DBNull.Value)
        throw new KernelExceptionID(sc_12548.ssp_appserver_12551(193773716), (object) value.ToString());
      this.SetDBObject(this.UserSession.GetObjectBaseVersionByID(Convert.ToInt64(obj), true));
    }
  }
}
