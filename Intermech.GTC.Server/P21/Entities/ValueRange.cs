// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.ValueRange
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using Intermech.GTC.Interfaces.Entities;
using System;

#nullable disable
namespace Intermech.GTC.Server.P21.Entities;

public class ValueRange(string keyStr, string paramsStr) : 
  BaseObject(keyStr, paramsStr),
  IValueRange,
  IPropertyValue,
  IBaseObject
{
  public static string EntityName = "VALUE_RANGE";
  private string _significantDigits;
  private string _lowerLimit;
  private string _upperLimit;
  private IUnit _unitComponent;
  private string _valueName;

  public override void SetParams(IEntityObjects entityObjects)
  {
    this.ValueName = this.ParamsArr.Length == 5 ? this.ParamsArr[0] : throw new Exception("Неверное количество параметров " + this.ParamStr);
    this.SignificantDigits = this.ParamsArr[1];
    this.UnitComponent = (IUnit) entityObjects.Get(this.ParamsArr[2]);
    this.LowerLimit = this.ParamsArr[3];
    this.UpperLimit = this.ParamsArr[4];
  }

  public string SignificantDigits
  {
    get
    {
      this.Used = true;
      return this._significantDigits;
    }
    private set => this._significantDigits = value;
  }

  public string LowerLimit
  {
    get
    {
      this.Used = true;
      return this._lowerLimit;
    }
    private set => this._lowerLimit = value;
  }

  public string UpperLimit
  {
    get
    {
      this.Used = true;
      return this._upperLimit;
    }
    private set => this._upperLimit = value;
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
