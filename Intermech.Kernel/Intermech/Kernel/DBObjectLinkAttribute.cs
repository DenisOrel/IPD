// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBObjectLinkAttribute
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using System;
using System.Data;


namespace Intermech.Kernel;

internal class DBObjectLinkAttribute : DBLinkAttribute, IDBObjectLinkAttribute
{
  public DBObjectLinkAttribute(
    UserSession uSession,
    DataRow attributeTypeRow,
    DataTable valuesTable,
    int values_index,
    DBAttributable parent)
    : base(uSession, attributeTypeRow, valuesTable, values_index, parent)
  {
  }

  public DBObjectLinkAttribute(
    UserSession uSession,
    DataRow attributeTypeRow,
    DataTable valuesTable,
    int values_index,
    bool temporary,
    DBAttributable parent)
    : base(uSession, attributeTypeRow, valuesTable, values_index, temporary, parent)
  {
  }

  protected override string LinkTableName => "IMS_OBJECT_LINKS";

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
        IDBObject dbObject;
        if (this.IsCreationMode)
        {
          dbObject = this.UserSession.GetObject(value, false) ?? this.UserSession.GetObject(-value, true);
        }
        else
        {
          dbObject = this.UserSession.GetObject(value);
          if (dbObject.IsCreationMode)
            throw new KernelExceptionID(sc_12545.ssp_appserver_12546(1737936154));
        }
        this.SetDBObject(dbObject);
      }
    }
  }

  internal override void InsertIntoObjectLink(long toObjectID, bool insertMode, int sign)
  {
    if (this.TemporaryAttribute || !this.IsObjectAttribute)
      return;
    if (insertMode || this.IsNull || Convert.ToInt64(this._ValuesTable[this.Index]["F_INTEGER_VALUE"]) == 0L)
    {
      try
      {
        this.UserSession.DataManager.ExecuteNonQuery("INSERT INTO IMS_OBJECT_LINKS (F_OBJECT_ID, F_ATTRIBUTE_ID, F_INLIST_ID, F_TOOBJECT_ID) VALUES (:objID, :attrID, :listID, :toObjID)", this.UserSession.DataManager.Parameter("objID", (object) (this.DBObjectID * (long) sign)), this.UserSession.DataManager.Parameter("attrID", (object) this.AttributeID), this.UserSession.DataManager.Parameter("listID", (object) this.Index), this.UserSession.DataManager.Parameter("toObjID", (object) toObjectID));
      }
      catch
      {
      }
    }
    else
      this.UserSession.DataManager.ExecuteNonQuery("UPDATE IMS_OBJECT_LINKS SET F_TOOBJECT_ID = :toObjID WHERE F_OBJECT_ID = :objID AND F_ATTRIBUTE_ID = :attrID AND F_INLIST_ID = :listID", this.UserSession.DataManager.Parameter("objID", (object) this.DBObjectID), this.UserSession.DataManager.Parameter("attrID", (object) this.AttributeID), this.UserSession.DataManager.Parameter("listID", (object) this.Index), this.UserSession.DataManager.Parameter("toObjID", (object) toObjectID));
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
    if (flag && dbObject.ObjectID < 0L)
      dbObject = this.UserSession.GetObject(-dbObject.ObjectID);
    long toObjectID = Math.Abs(dbObject.ObjectID);
    if (this._Attributes == null || (this._Attributes.AssignMode & Consts.CheckOutMode) == 0)
      (this.AttributeType as IDBObjectLinkAttributeType).ValidateObjectType(objectType);
    this.UserSession.StartTransaction();
    try
    {
      if (flag)
        this.InsertIntoObjectLink(toObjectID, false, 1);
      this.DirectSetValue("F_STRING_VALUE", (object) dbObject.Caption);
      base.AsInteger = toObjectID;
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
    QuickObjectInfo objectInfo = this.UserSession.DBCache.GetObjectInfo(this.UserSession.DataManager, Convert.ToInt64(val));
    return objectInfo.Empty ? val.ToString() : objectInfo.Caption;
  }

  protected override IDBObject GetObjectInternal(bool throwException)
  {
    IDBObject objectInternal = this.UserSession.GetObject(this.AsInteger, throwException);
    if (objectInternal != null && objectInternal.CheckoutBy == this.UserSession.UserID)
      objectInternal = this.UserSession.GetObject(-objectInternal.ObjectID);
    return objectInternal;
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
        if (Math.Abs(value.ObjectID) == this.AsInteger)
          return;
        if (value.ObjectID < 0L && !this.IsCreationMode && value.IsCreationMode)
          throw new KernelExceptionID(sc_12545.ssp_appserver_12547(1227163930));
        this.SetDBObject(value);
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
      base.AsInteger = this.UserSession.UserID;
    }
  }

  protected override void SetDefaultValue(object defValue)
  {
    if (defValue is string && defValue.ToString() == Consts.CurrentUserFunction)
      defValue = (object) this.UserSession.UserID;
    base.SetDefaultValue(defValue);
    if (defValue == null || !(defValue.ToString() != string.Empty))
      return;
    if (defValue.ToString() == Consts.CurrentUserFunction)
      defValue = (object) this.UserSession.UserID;
    long int64 = Convert.ToInt64(defValue);
    QuickObjectInfo objectInfo = this.UserSession.DBCache.GetObjectInfo(this.UserSession.DataManager, int64);
    if (this._Attributes != null)
      this._Attributes.AddDeltaValue(this.AttributeID);
    this.CheckUniqueValue(new object[1]{ defValue }, true);
    if (!this.TemporaryAttribute)
    {
      this.UserSession.DataManager.ExecuteNonQuery($"UPDATE {this.ValuesTableName} SET F_INTEGER_VALUE = :val, F_STRING_VALUE = :capt WHERE {this.ValuesKeyName} = {this.DBObjectID} AND F_ATTRIBUTE_ID = {this.AttributeID} AND F_INLIST_ID = {this.Index}", this.UserSession.DataManager.Parameter("val", (object) int64), this.UserSession.DataManager.Parameter("capt", (object) objectInfo.Caption));
      this.UpdateViewValue(this.GetInViewFieldName(AttributeValueField.Integer), (object) int64, this.DBObjectID);
      this.UpdateViewValue(this.GetInViewFieldName(AttributeValueField.String), (object) objectInfo.Caption, this.DBObjectID);
    }
    this._ValuesTable[this.Index]["F_INTEGER_VALUE"] = (object) int64;
    this._ValuesTable[this.Index]["F_STRING_VALUE"] = (object) objectInfo.Caption;
    if (!this.IsCreationMode)
      this.InsertIntoObjectLink(int64, true, 1);
    this.ChangeComputedValues(true);
    this.ParentObject.AfterSetComplexAttributeValue((IDBAttribute) this);
  }

  protected override bool IsNullValue(object newValue)
  {
    return base.IsNullValue(newValue) || Convert.ToInt64(newValue) <= 0L;
  }

  public Guid DBObjectGUID
  {
    get
    {
      if (this.AsInteger <= 0L)
        return Guid.Empty;
      object obj = this.UserSession.DataManager.ExecuteScalar("SELECT F_GUID FROM IMS_GUID WHERE F_OBJECT_ID = :id", this.UserSession.DataManager.Parameter("id", (object) this.AsInteger));
      if (obj == null || obj == DBNull.Value)
        return Guid.Empty;
      return obj is Guid guid ? guid : new Guid(obj.ToString());
    }
    set
    {
      if (!(value != this.DBObjectGUID))
        return;
      this.SetDBObject(this.UserSession.GetObject(value, true));
    }
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
}
