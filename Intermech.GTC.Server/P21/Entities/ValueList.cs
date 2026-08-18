// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.ValueList
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using Intermech.GTC.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.GTC.Server.P21.Entities;

public class ValueList(string keyStr, string paramsStr) : 
  BaseObject(keyStr, paramsStr),
  IValueList,
  IPropertyValue,
  IBaseObject
{
  public static string EntityName = "VALUE_LIST";
  private IPropertyValue[] _values;
  private string _valueName;

  public override void SetParams(IEntityObjects entityObjects)
  {
    this.ValueName = this.ParamsArr.Length == 2 ? this.ParamsArr[0] : throw new Exception("Неверное количество параметров " + this.ParamStr);
    this.Values = ((IEnumerable<string>) this.ParamsArr[1].Split(',')).Select<string, IPropertyValue>((Func<string, IPropertyValue>) (itemStr => (IPropertyValue) entityObjects.Get(itemStr))).ToArray<IPropertyValue>();
  }

  public IPropertyValue[] Values
  {
    get
    {
      this.Used = true;
      return this._values;
    }
    private set => this._values = value;
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
