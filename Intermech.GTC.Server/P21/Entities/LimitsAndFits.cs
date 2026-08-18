// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.LimitsAndFits
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using Intermech.GTC.Interfaces.Entities;
using System;

#nullable disable
namespace Intermech.GTC.Server.P21.Entities;

public class LimitsAndFits(string keyStr, string paramsStr) : BaseObject(keyStr, paramsStr), ILimitsAndFits, IBaseObject
{
  public static string EntityName = "LIMITS_AND_FITS";
  private string _deviation;
  private string _fittingType;
  private string _grade;

  public override void SetParams(IEntityObjects entityObjects)
  {
    this.Deviation = this.ParamsArr.Length == 3 ? this.ParamsArr[0] : throw new Exception("Неверное количество параметров " + this.ParamStr);
    this.FittingType = this.ParamsArr[1];
    this.Grade = this.ParamsArr[2];
  }

  public string Deviation
  {
    get
    {
      this.Used = true;
      return this._deviation;
    }
    private set => this._deviation = value;
  }

  public string FittingType
  {
    get
    {
      this.Used = true;
      return this._fittingType;
    }
    private set => this._fittingType = value;
  }

  public string Grade
  {
    get
    {
      this.Used = true;
      return this._grade;
    }
    private set => this._grade = value;
  }
}
