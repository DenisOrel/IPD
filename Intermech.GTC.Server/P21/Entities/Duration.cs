// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.Duration
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using Intermech.GTC.Interfaces.Entities;
using System;

#nullable disable
namespace Intermech.GTC.Server.P21.Entities;

public class Duration(string keyStr, string paramsStr) : BaseObject(keyStr, paramsStr), IDuration, IBaseObject
{
  public static string EntityName = "DURATION";
  private string _time;
  private string _timeUnit;

  public override void SetParams(IEntityObjects entityObjects)
  {
    this.Time = this.ParamsArr.Length == 2 ? this.ParamsArr[0] : throw new Exception("Неверное количество параметров " + this.ParamStr);
    this.TimeUnit = this.ParamsArr[1];
  }

  public string Time
  {
    get
    {
      this.Used = true;
      return this._time;
    }
    private set => this._time = value;
  }

  public string TimeUnit
  {
    get
    {
      this.Used = true;
      return this._timeUnit;
    }
    private set => this._timeUnit = value;
  }
}
