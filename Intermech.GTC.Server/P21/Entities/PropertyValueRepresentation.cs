// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.PropertyValueRepresentation
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using Intermech.GTC.Interfaces.Entities;
using System;

#nullable disable
namespace Intermech.GTC.Server.P21.Entities;

public class PropertyValueRepresentation(string keyStr, string paramsStr) : 
  BaseObject(keyStr, paramsStr),
  IPropertyValueRepresentation,
  IBaseObject
{
  public static string EntityName = "PROPERTY_VALUE_REPRESENTATION";
  private IProperty _definition;
  private IPropertyValue _specifiedValue;

  public override void SetParams(IEntityObjects entityObjects)
  {
    this.Definition = this.ParamsArr.Length == 5 ? (IProperty) entityObjects.Get(this.ParamsArr[0]) : throw new Exception("Неверное количество параметров " + this.ParamStr);
    this.SpecifiedValue = (IPropertyValue) entityObjects.Get(this.ParamsArr[3]);
  }

  public IProperty Definition
  {
    get
    {
      this.Used = true;
      return this._definition;
    }
    private set => this._definition = value;
  }

  public IPropertyValue SpecifiedValue
  {
    get
    {
      this.Used = true;
      return this._specifiedValue;
    }
    private set => this._specifiedValue = value;
  }
}
