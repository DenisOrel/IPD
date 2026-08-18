// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.ItemVersion
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using Intermech.GTC.Interfaces.Entities;
using System;

#nullable disable
namespace Intermech.GTC.Server.P21.Entities;

public class ItemVersion(string keyStr, string paramsStr) : BaseObject(keyStr, paramsStr), IItemVersion, IBaseObject
{
  public static string EntityName = "ITEM_VERSION";
  private string _description;
  private string _id;
  private IItem _associatedItem;

  public override void SetParams(IEntityObjects entityObjects)
  {
    this.AssociatedItem = this.ParamsArr.Length == 3 ? (IItem) entityObjects.Get(this.ParamsArr[0]) : throw new Exception("Неверное количество параметров " + this.ParamStr);
    this.Description = this.ParamsArr[1];
    this.Id = this.ParamsArr[2];
  }

  public string Description
  {
    get
    {
      this.Used = true;
      return this._description;
    }
    private set => this._description = value;
  }

  public string Id
  {
    get
    {
      this.Used = true;
      return this._id;
    }
    private set => this._id = value;
  }

  public IItem AssociatedItem
  {
    get
    {
      this.Used = true;
      return this._associatedItem;
    }
    private set => this._associatedItem = value;
  }
}
