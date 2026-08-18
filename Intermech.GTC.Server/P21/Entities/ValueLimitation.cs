// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.ValueLimitation
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using Intermech.GTC.Interfaces.Entities;
using System;

#nullable disable
namespace Intermech.GTC.Server.P21.Entities;

public class ValueLimitation(string keyStr, string paramsStr) : 
  BaseObject(keyStr, paramsStr),
  IValueLimitation,
  IBaseObject
{
  public static string EntityName = "VALUE_LIMITATION";
  private string _envelope;
  private INumericalValue _limitedValue;
  private IBaseObject _isDefinedBy;

  public override void SetParams(IEntityObjects entityObjects)
  {
    this.Envelope = this.ParamsArr.Length == 3 ? this.ParamsArr[0] : throw new Exception("Неверное количество параметров " + this.ParamStr);
    this.IsDefinedBy = entityObjects.Get(this.ParamsArr[1]);
    this.LimitedValue = (INumericalValue) entityObjects.Get(this.ParamsArr[2]);
  }

  public string Envelope
  {
    get
    {
      this.Used = true;
      return this._envelope;
    }
    private set => this._envelope = value;
  }

  public INumericalValue LimitedValue
  {
    get
    {
      this.Used = true;
      return this._limitedValue;
    }
    private set => this._limitedValue = value;
  }

  public IBaseObject IsDefinedBy
  {
    get
    {
      this.Used = true;
      return this._isDefinedBy;
    }
    private set => this._isDefinedBy = value;
  }
}
