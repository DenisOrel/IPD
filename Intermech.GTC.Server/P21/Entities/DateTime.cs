// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.DateTime
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using Intermech.GTC.Interfaces.Entities;
using System;

#nullable disable
namespace Intermech.GTC.Server.P21.Entities;

public class DateTime(string keyStr, string paramsStr) : BaseObject(keyStr, paramsStr), IDateTime, IBaseObject
{
  public static string EntityName = "DATE_TIME";
  private string _date;
  private string _time;

  public override void SetParams(IEntityObjects entityObjects)
  {
    this.Date = this.ParamsArr.Length == 2 ? this.ParamsArr[0] : throw new Exception("Неверное количество параметров " + this.ParamStr);
    this.Time = this.ParamsArr[1];
  }

  public string Date
  {
    get
    {
      this.Used = true;
      return this._date;
    }
    private set => this._date = value;
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
}
