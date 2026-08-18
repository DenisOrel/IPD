// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.StringValue
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using Intermech.GTC.Interfaces.Entities;
using System;

#nullable disable
namespace Intermech.GTC.Server.P21.Entities;

public class StringValue(string keyStr, string paramsStr) : 
  BaseObject(keyStr, paramsStr),
  IStringValue,
  IPropertyValue,
  IBaseObject
{
  public static string EntityName = "STRING_VALUE";
  private IMultiLanguageString _valueSpecification;
  private string _valueName;

  public override void SetParams(IEntityObjects entityObjects)
  {
    this.ValueName = this.ParamsArr.Length == 2 ? this.ParamsArr[0] : throw new Exception("Неверное количество параметров " + this.ParamStr);
    this.ValueSpecification = (IMultiLanguageString) entityObjects.Get(this.ParamsArr[1]);
  }

  public IMultiLanguageString ValueSpecification
  {
    get
    {
      this.Used = true;
      return this._valueSpecification;
    }
    private set => this._valueSpecification = value;
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
