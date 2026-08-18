// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.State
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using Intermech.GTC.Interfaces.Entities;
using System;

#nullable disable
namespace Intermech.GTC.Server.P21.Entities;

public class State(string keyStr, string paramsStr) : BaseObject(keyStr, paramsStr), IState, IBaseObject
{
  public static string EntityName = "STATE";
  private string _name;

  public override void SetParams(IEntityObjects entityObjects)
  {
    this.Name = this.ParamsArr.Length == 1 ? this.ParamsArr[0] : throw new Exception("Неверное количество параметров " + this.ParamStr);
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
}
