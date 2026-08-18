// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.ValueLimit
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using Intermech.GTC.Interfaces.Entities;
using System;

#nullable disable
namespace Intermech.GTC.Server.P21.Entities;

public class ValueLimit(string keyStr, string paramsStr) : 
  BaseObject(keyStr, paramsStr),
  IValueLimit,
  IPropertyValue,
  IBaseObject
{
  public static string EntityName = "VALUE_LIMIT";
  private string _significantDigit;
  private string _limit;
  private string _limitQualifier;
  private IUnit _unitComponent;
  private string _valueName;

  public override void SetParams(IEntityObjects entityObjects)
  {
    this.ValueName = this.ParamsArr.Length == 5 ? this.ParamsArr[0] : throw new Exception("Неверное количество параметров " + this.ParamStr);
    this.SignificantDigit = this.ParamsArr[1];
    this.UnitComponent = (IUnit) entityObjects.Get(this.ParamsArr[2]);
    this.Limit = this.ParamsArr[3];
    this.LimitQualifier = this.ParamsArr[4];
  }

  public string SignificantDigit
  {
    get
    {
      this.Used = true;
      return this._significantDigit;
    }
    private set => this._significantDigit = value;
  }

  public string Limit
  {
    get
    {
      this.Used = true;
      return this._limit;
    }
    private set => this._limit = value;
  }

  public string LimitQualifier
  {
    get
    {
      this.Used = true;
      return this._limitQualifier;
    }
    private set => this._limitQualifier = value;
  }

  public IUnit UnitComponent
  {
    get
    {
      this.Used = true;
      return this._unitComponent;
    }
    private set => this._unitComponent = value;
  }

  public string ValueName
  {
    get
    {
      this.Used = true;
      return this._valueName;
    }
    private set => this._valueName = value;
  }
}
