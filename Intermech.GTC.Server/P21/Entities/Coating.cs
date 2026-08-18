// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.Coating
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using Intermech.GTC.Interfaces.Entities;
using System;

#nullable disable
namespace Intermech.GTC.Server.P21.Entities;

public class Coating(string keyStr, string paramsStr) : BaseObject(keyStr, paramsStr), ICoating, IBaseObject
{
  public static string EntityName = "COATING";
  private string _coatingName;
  private string _coatingProcess;

  public override void SetParams(IEntityObjects entityObjects)
  {
    this.CoatingName = this.ParamsArr.Length == 2 ? this.ParamsArr[0] : throw new Exception("Неверное количество параметров " + this.ParamStr);
    this.CoatingProcess = this.ParamsArr[1];
  }

  public string CoatingName
  {
    get
    {
      this.Used = true;
      return this._coatingName;
    }
    private set => this._coatingName = value;
  }

  public string CoatingProcess
  {
    get
    {
      this.Used = true;
      return this._coatingProcess;
    }
    private set => this._coatingProcess = value;
  }
}
