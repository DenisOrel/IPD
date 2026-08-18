// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Server.DBFormObject
// Assembly: Intermech.FormDesigner.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ABD17B9B-52A2-4551-9041-386497DBE670
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.FormDesigner.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.FormDesigner.Server;

public class DBFormObject : 
  DBObject,
  IFormDBObject,
  IDBObject,
  IDBAttributable,
  IDBSessionable,
  IPluginsData
{
  private int GLOBAL_OBJ_ID_TYPE;
  private int GLOBAL_REL_ID_TYPE;
  private int FORMULA_ID_TYPE;

  public DBFormObject(UserSession uSession, DataTable objectParams)
    : base(uSession, objectParams)
  {
    this.GLOBAL_OBJ_ID_TYPE = this.UserSession.IdentHelper.GetAttributeID("cad00149-306c-11d8-b4e9-00304f19f545");
    this.GLOBAL_REL_ID_TYPE = this.UserSession.IdentHelper.GetAttributeID("cad0014a-306c-11d8-b4e9-00304f19f545");
    this.FORMULA_ID_TYPE = this.UserSession.IdentHelper.GetAttributeID("cad00064-306c-11d8-b4e9-00304f19f545");
  }

  private void ChangeFormInfoInCache(
    IDBAttribute attribute,
    object newValue,
    bool bIgnoreFormulaAttr)
  {
    if (attribute == null)
      return;
    int attributeId = attribute.AttributeID;
    if (attributeId != this.GLOBAL_OBJ_ID_TYPE && attributeId != this.GLOBAL_REL_ID_TYPE && (attributeId != this.FORMULA_ID_TYPE || bIgnoreFormulaAttr) || this.IsCreationMode || !(ServerServices.ServiceContainer.GetService(typeof (IFormDesignerService)) is FormDesignerService service))
      return;
    Guid sessionGuid = attribute.Session.SessionGUID;
    object[] values = attribute.Values;
    int index = attribute.Index;
    string oldValue = Convert.ToString(values == null || index <= -1 || values.Length <= index ? (object) null : values[index]);
    string newValue1 = Convert.ToString(newValue);
    Dictionary<Guid, ValueInfo> dictionary;
    if (attributeId == this.GLOBAL_OBJ_ID_TYPE)
      dictionary = service.AttrObjValueCache;
    else if (attributeId == this.GLOBAL_REL_ID_TYPE)
    {
      dictionary = service.AttrRelValueCache;
    }
    else
    {
      service.AddToCache((IDBObject) this, attribute);
      return;
    }
    if (dictionary == null)
      return;
    ValueInfo valueInfo = dictionary.ContainsKey(sessionGuid) ? dictionary[sessionGuid] : new ValueInfo(sessionGuid, this.ObjectID);
    valueInfo.AddValueInfo(oldValue, newValue1);
    dictionary[sessionGuid] = valueInfo;
  }

  protected override void AfterSetCaption()
  {
    base.AfterSetCaption();
    if (!(ServerServices.ServiceContainer.GetService(typeof (IFormDesignerService)) is IFormDesignerService service))
      return;
    service.ChangeFormsCaption((IDBObject) this);
  }

  protected override void DoCommitCreation()
  {
    base.DoCommitCreation();
    if (!(ServerServices.ServiceContainer.GetService(typeof (IFormDesignerService)) is FormDesignerService service))
      return;
    service.AddToCache((IDBObject) this);
  }

  protected override void DoDelete()
  {
    base.DoDelete();
    if (ServerServices.ServiceContainer.GetService(typeof (IFormDesignerService)) is FormDesignerService service1)
      service1.RemoveFromCache((IDBObject) this);
    if (!(ServerServices.ServiceContainer.GetService(typeof (IServerFormsCache)) is IServerFormsCache service2))
      return;
    service2.Remove(this.ObjectID);
  }

  protected override void DoBeforeSetAdditionalAttributeValue(
    IDBAttribute attribute,
    object newValue)
  {
    base.DoBeforeSetAdditionalAttributeValue(attribute, newValue);
    this.ChangeFormInfoInCache(attribute, newValue, false);
  }

  protected override void DoBeforeDeleteAdditionalAttributeValue(IDBAttribute attribute)
  {
    base.DoBeforeDeleteAdditionalAttributeValue(attribute);
    this.ChangeFormInfoInCache(attribute, (object) null, true);
  }

  public void AddToCache()
  {
  }

  public void ChangeCheckInInfo()
  {
    if (ServerServices.ServiceContainer.GetService(typeof (IFormDesignerService)) is FormDesignerService service1)
      service1.CheckInForm((IDBObject) this);
    if (!(ServerServices.ServiceContainer.GetService(typeof (IServerFormsCache)) is IServerFormsCache service2))
      return;
    service2.CheckIn(this.ObjectID);
  }

  public void ChangeCheckOutInfo()
  {
    if (ServerServices.ServiceContainer.GetService(typeof (IFormDesignerService)) is FormDesignerService service1)
      service1.CheckOutForm((IDBObject) this);
    if (!(ServerServices.ServiceContainer.GetService(typeof (IServerFormsCache)) is IServerFormsCache service2))
      return;
    service2.CheckOut(this.ObjectID);
  }

  public void RemoveFromCache()
  {
  }

  public void UndoCheckOutInfo()
  {
    IDBObject iDBObj = this.UserSession.GetObject(this.ObjectID);
    if (iDBObj == null)
      return;
    if (ServerServices.ServiceContainer.GetService(typeof (IFormDesignerService)) is FormDesignerService service1)
      service1.UndoCheckOutForm(iDBObj);
    if (!(ServerServices.ServiceContainer.GetService(typeof (IServerFormsCache)) is IServerFormsCache service2))
      return;
    service2.UndoCheckOut(iDBObj.ObjectID);
  }
}
