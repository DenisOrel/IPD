// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.RealizedItemAssociation
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using Intermech.GTC.Interfaces.Entities;
using System;

#nullable disable
namespace Intermech.GTC.Server.P21.Entities;

public class RealizedItemAssociation(string keyStr, string paramsStr) : 
  BaseObject(keyStr, paramsStr),
  IRealizedItemAssociation,
  IBaseObject
{
  public static string EntityName = "REALIZED_ITEM_ASSOCIATION";
  private IItemVersion _realizedItemVersion;
  private IPhysicalItem _physicalItem;

  public override void SetParams(IEntityObjects entityObjects)
  {
    this.PhysicalItem = this.ParamsArr.Length == 2 ? (IPhysicalItem) entityObjects.Get(this.ParamsArr[0]) : throw new Exception("Неверное количество параметров " + this.ParamStr);
    this.RealizedItemVersion = (IItemVersion) entityObjects.Get(this.ParamsArr[1]);
  }

  public IItemVersion RealizedItemVersion
  {
    get
    {
      this.Used = true;
      return this._realizedItemVersion;
    }
    private set => this._realizedItemVersion = value;
  }

  public IPhysicalItem PhysicalItem
  {
    get
    {
      this.Used = true;
      return this._physicalItem;
    }
    private set => this._physicalItem = value;
  }
}
