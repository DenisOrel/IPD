// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.PlusMinusBounds
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using Intermech.GTC.Interfaces.Entities;
using System;

#nullable disable
namespace Intermech.GTC.Server.P21.Entities;

public class PlusMinusBounds(string keyStr, string paramsStr) : 
  BaseObject(keyStr, paramsStr),
  IPlusMinusBounds,
  IBaseObject
{
  public static string EntityName = "PLUS_MINUS_BOUNDS";
  private string _lowerBound;
  private string _significationDigit;
  private string _upperBound;
  private string _valueDetermination;

  public override void SetParams(IEntityObjects entityObjects)
  {
    this.LowerBound = this.ParamsArr.Length == 4 ? this.ParamsArr[0] : throw new Exception("Неверное количество параметров " + this.ParamStr);
    this.SignificationDigit = this.ParamsArr[1];
    this.UpperBound = this.ParamsArr[2];
    this.ValueDetermination = this.ParamsArr[3];
  }

  public string LowerBound
  {
    get
    {
      this.Used = true;
      return this._lowerBound;
    }
    private set => this._lowerBound = value;
  }

  public string SignificationDigit
  {
    get
    {
      this.Used = true;
      return this._significationDigit;
    }
    private set => this._significationDigit = value;
  }

  public string UpperBound
  {
    get
    {
      this.Used = true;
      return this._upperBound;
    }
    private set => this._upperBound = value;
  }

  public string ValueDetermination
  {
    get
    {
      this.Used = true;
      return this._valueDetermination;
    }
    private set => this._valueDetermination = value;
  }
}
