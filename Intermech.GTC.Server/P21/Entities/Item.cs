// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.Item
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using Intermech.GTC.Interfaces.Entities;
using System;

#nullable disable
namespace Intermech.GTC.Server.P21.Entities;

public class Item(string keyStr, string paramsStr) : BaseObject(keyStr, paramsStr), IItem, IBaseObject
{
  public static string EntityName = "ITEM";
  private IMultiLanguageString _description;
  private string _id;
  private IMultiLanguageString _name;

  public override void SetParams(IEntityObjects entityObjects)
  {
    this.Description = this.ParamsArr.Length == 3 ? (IMultiLanguageString) entityObjects.Get(this.ParamsArr[0]) : throw new Exception("Неверное количество параметров " + this.ParamStr);
    this.Id = this.ParamsArr[1];
    this.Name = (IMultiLanguageString) entityObjects.Get(this.ParamsArr[2]);
  }

  public IMultiLanguageString Description
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

  public IMultiLanguageString Name
  {
    get
    {
      this.Used = true;
      return this._name;
    }
    private set => this._name = value;
  }
}
