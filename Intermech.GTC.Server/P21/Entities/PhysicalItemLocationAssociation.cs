// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.PhysicalItemLocationAssociation
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using Intermech.GTC.Interfaces.Entities;
using System;

#nullable disable
namespace Intermech.GTC.Server.P21.Entities;

public class PhysicalItemLocationAssociation(string keyStr, string paramsStr) : 
  BaseObject(keyStr, paramsStr),
  IPhysicalItemLocationAssociation,
  IBaseObject
{
  public static string EntityName = "PHYSICAL_ITEM_LOCATION_ASSOCIATION";
  private IPhysicalItem _locatedItem;
  private ILocation _location;

  public override void SetParams(IEntityObjects entityObjects)
  {
    this.LocatedItem = this.ParamsArr.Length == 2 ? (IPhysicalItem) entityObjects.Get(this.ParamsArr[0]) : throw new Exception("Неверное количество параметров " + this.ParamStr);
    this.Location = (ILocation) entityObjects.Get(this.ParamsArr[1]);
  }

  public IPhysicalItem LocatedItem
  {
    get
    {
      this.Used = true;
      return this._locatedItem;
    }
    private set => this._locatedItem = value;
  }

  public ILocation Location
  {
    get
    {
      this.Used = true;
      return this._location;
    }
    private set => this._location = value;
  }
}
