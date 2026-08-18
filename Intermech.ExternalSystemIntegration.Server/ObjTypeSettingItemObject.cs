// Decompiled with JetBrains decompiler
// Type: Intermech.ExternalSystemIntegration.Server.ObjTypeSettingItemObject
// Assembly: Intermech.ExternalSystemIntegration.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: DA51A3A9-E549-4754-B561-351EB1444903
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.ExternalSystemIntegration.Server.dll

using Intermech.ExternalSystemIntegration.Interfaces;
using Intermech.Interfaces;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using System;
using System.Data;
using System.Linq;

#nullable disable
namespace Intermech.ExternalSystemIntegration.Server;

internal class ObjTypeSettingItemObject : 
  DBObject,
  IObjTypeSettingItemObject,
  IDBObject,
  IDBAttributable,
  IDBSessionable,
  IPluginsData
{
  public ObjTypeSettingItemObject(UserSession uSession)
    : base(uSession)
  {
  }

  public ObjTypeSettingItemObject(UserSession uSession, DataTable objectsTable)
    : base(uSession, objectsTable)
  {
  }

  private long[] GetObjectsByLinkedAttr(int ObjTypeID)
  {
    long[] objectsByLinkedAttr = new long[0];
    if (this.Attributes.FindByID(Const.LinkObjectAttrTypeID) != null)
    {
      DataTable source = this.UserSession.GetObjectCollection(ObjTypeID).Select(new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(Const.LinkObjectAttrTypeID, RelationalOperators.Equal, (object) this.LinkObjGuid, LogicalOperators.NONE, 0, false)
      }, new object[1]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID
      }, (object[]) null, (SortOrders[]) null, 0L, (object) null, -1, true, "MyObjects"));
      if (source.Rows.Count > 0)
        objectsByLinkedAttr = source.AsEnumerable().Select<DataRow, long>((System.Func<DataRow, long>) (row => row.Field<long>(0))).ToArray<long>();
    }
    return objectsByLinkedAttr;
  }

  public override string Caption
  {
    get => this.GetObjTypeSettingItemName();
    set => this.SetObjTypeSettingItemName(value);
  }

  public override void DoAfterCreate()
  {
    base.DoAfterCreate();
    this.LinkObjGuid = Guid.NewGuid().ToString();
  }

  protected override void DoDelete()
  {
    UserSession userSession = this.UserSession;
    foreach (long config in this.Configs)
      userSession.GetObject(config).Delete(0L);
    base.DoDelete();
  }

  public string ObjTypeGUID
  {
    get => this.GetObjectTypeGuid();
    set => this.SetObjectTypeGuid(value);
  }

  public string LinkObjGuid
  {
    get => this.GetLinkObjGuid();
    protected set => this.SetLinkObjGuid(value);
  }

  public long[] Configs => this.GetConfigs();

  public long[] ResponceConfigs => this.GetResponceConfigs();

  public long[] RequestConfigs => this.GetRequestConfigs();

  protected virtual string GetObjTypeSettingItemName()
  {
    string typeSettingItemName = "";
    IDBAttribute byId = this.Attributes.FindByID(Const.NameAttrTypeID);
    if (byId != null)
      typeSettingItemName = byId.AsString;
    return typeSettingItemName;
  }

  protected virtual void SetObjTypeSettingItemName(string value)
  {
    IDBAttribute byId = this.Attributes.FindByID(Const.NameAttrTypeID);
    if (byId == null)
      return;
    byId.AsString = value;
  }

  protected virtual string GetLinkObjGuid()
  {
    string linkObjGuid = "";
    IDBAttribute byId = this.Attributes.FindByID(Const.LinkObjectAttrTypeID);
    if (byId != null)
      linkObjGuid = byId.AsString;
    return linkObjGuid;
  }

  protected virtual void SetLinkObjGuid(string value)
  {
    IDBAttribute byId = this.Attributes.FindByID(Const.LinkObjectAttrTypeID);
    if (byId == null)
      return;
    byId.AsString = value;
  }

  protected virtual string GetObjectTypeGuid()
  {
    string objectTypeGuid = "";
    IDBAttribute byId = this.Attributes.FindByID(Const.ObjectTypeIDAttrTypeID);
    if (byId != null)
      objectTypeGuid = byId.AsString;
    return objectTypeGuid;
  }

  protected virtual void SetObjectTypeGuid(string AValue)
  {
    IDBAttribute byId = this.Attributes.FindByID(Const.ObjectTypeIDAttrTypeID);
    if (byId == null)
      return;
    byId.Value = (object) AValue;
  }

  protected virtual long[] GetConfigs()
  {
    return this.GetObjectsByLinkedAttr(Const.ConfigElementObjTypeID);
  }

  protected virtual long[] GetResponceConfigs()
  {
    return this.GetObjectsByLinkedAttr(Const.ResponceConfigObjTypeID);
  }

  protected virtual long[] GetRequestConfigs()
  {
    return this.GetObjectsByLinkedAttr(Const.RequestConfigObjTypeID);
  }
}
