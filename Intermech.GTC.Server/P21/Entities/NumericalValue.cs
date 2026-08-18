// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.NumericalValue
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using Intermech.GTC.Interfaces.Entities;
using System;

#nullable disable
namespace Intermech.GTC.Server.P21.Entities;

public class NumericalValue(string keyStr, string paramsStr) : 
  BaseObject(keyStr, paramsStr),
  INumericalValue,
  IPropertyValue,
  IBaseObject
{
  public static string EntityName = "NUMERICAL_VALUE";
  private string _significantDigits;
  private double _valueComponent;
  private IUnit _unitComponent;
  private string _valueName;

  public override void SetParams(IEntityObjects entityObjects)
  {
    this.ValueName = this.ParamsArr.Length == 4 ? this.ParamsArr[0] : throw new Exception("Неверное количество параметров " + this.ParamStr);
    this.SignificantDigits = this.ParamsArr[1];
    this.UnitComponent = (IUnit) entityObjects.Get(this.ParamsArr[2]);
    double result;
    this.ValueComponent = double.TryParse(this.ParamsArr[3], out result) ? result : 0.0;
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

  public double ValueComponent
  {
    get
    {
      this.Used = true;
      return this._valueComponent;
    }
    private set => this._valueComponent = value;
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
