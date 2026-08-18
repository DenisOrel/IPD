// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.ItemDefinition
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using Intermech.GTC.Interfaces.Entities;
using System;

#nullable disable
namespace Intermech.GTC.Server.P21.Entities;

public class ItemDefinition(string keyStr, string paramsStr) : 
  BaseObject(keyStr, paramsStr),
  IItemDefinition,
  IBaseObject
{
  public static string EntityName = "ITEM_DEFINITION";
  private string _id;
  private string _name;
  private IItemVersion _associatedItemVersion;

  public override void SetParams(IEntityObjects entityObjects)
  {
    this.AssociatedItemVersion = this.ParamsArr.Length == 4 ? (IItemVersion) entityObjects.Get(this.ParamsArr[0]) : throw new Exception("Неверное количество параметров " + this.ParamStr);
    this.Id = this.ParamsArr[2];
    this.Name = this.ParamsArr[3];
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

  public string Name
  {
    get
    {
      this.Used = true;
      return this._name;
    }
    private set => this._name = value;
  }

  public IItemVersion AssociatedItemVersion
  {
    get
    {
      this.Used = true;
      return this._associatedItemVersion;
    }
    private set => this._associatedItemVersion = value;
  }
}
