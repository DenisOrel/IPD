// Decompiled with JetBrains decompiler
// Type: Intermech.ExternalSystemIntegration.Server.ConfigObject
// Assembly: Intermech.ExternalSystemIntegration.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: DA51A3A9-E549-4754-B561-351EB1444903
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.ExternalSystemIntegration.Server.dll

using Intermech.ExternalSystemIntegration.Interfaces;
using Intermech.Interfaces;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

#nullable disable
namespace Intermech.ExternalSystemIntegration.Server;

public class ConfigObject : 
  DBObject,
  IConfigObject,
  IDBObject,
  IDBAttributable,
  IDBSessionable,
  IPluginsData
{
  public ConfigObject(UserSession uSession)
    : base(uSession)
  {
  }

  public ConfigObject(UserSession uSession, DataTable objectsTable)
    : base(uSession, objectsTable)
  {
  }

  public string ConfigName
  {
    get => this.GetConfigName();
    set => this.SetConfigName(value);
  }

  public string LinkObjGuid
  {
    get => this.GetLinkObjGuid();
    set => this.SetLinkObjGuid(value);
  }

  public string[] AttributeComprasion
  {
    get => this.GetAttributeComprasion();
    set => this.SetAttributeComprasion(value);
  }

  public long SchemeTransfLink
  {
    get => this.GetSchemeTransfLink();
    set => this.SetSchemeTransfLink(value);
  }

  public long ObjTypeSettingItemObjectID => this.GetObjTypeSttingItemObjectID();

  protected virtual string GetConfigName()
  {
    string configName = "";
    IDBAttribute byId = this.Attributes.FindByID(Const.NameAttrTypeID);
    if (byId != null)
      configName = byId.AsString;
    return configName;
  }

  protected virtual void SetConfigName(string value)
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

  protected virtual string[] GetAttributeComprasion()
  {
    string[] attributeComprasion = new string[0];
    IDBAttribute byId = this.Attributes.FindByID(Const.AttributeComprasionAttrTypeID);
    if (byId != null)
      attributeComprasion = ((IEnumerable<object>) byId.Values).Select<object, string>((System.Func<object, string>) (x => x.ToString())).ToArray<string>();
    return attributeComprasion;
  }

  protected virtual void SetAttributeComprasion(string[] value)
  {
    if (value.Length == 0)
      return;
    IDBAttribute byId = this.Attributes.FindByID(Const.AttributeComprasionAttrTypeID);
    if (byId == null)
      return;
    byId.Values = (object[]) value;
  }

  protected virtual long GetSchemeTransfLink() => throw new NotImplementedException();

  protected virtual void SetSchemeTransfLink(long value) => throw new NotImplementedException();

  private long GetObjTypeSttingItemObjectID()
  {
    long sttingItemObjectId = 0;
    if (this.Attributes.FindByID(Const.LinkObjectAttrTypeID) != null)
    {
      DataTable source = this.UserSession.GetObjectCollection(Const.TypeSettingItemObjTypeID).Select(new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(Const.LinkObjectAttrTypeID, RelationalOperators.Equal, (object) this.LinkObjGuid, LogicalOperators.NONE, 0, false)
      }, new object[1]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID
      }, (object[]) null, (SortOrders[]) null, 0L, (object) null, -1, true, "MyObjects"));
      if (source.Rows.Count > 0)
        sttingItemObjectId = Convert.ToInt64(source.AsEnumerable().First<DataRow>()[0]);
    }
    return sttingItemObjectId;
  }
}
