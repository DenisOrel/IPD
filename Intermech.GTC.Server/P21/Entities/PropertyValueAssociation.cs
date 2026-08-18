// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.PropertyValueAssociation
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using Intermech.GTC.Interfaces.Entities;
using System;

#nullable disable
namespace Intermech.GTC.Server.P21.Entities;

public class PropertyValueAssociation(string keyStr, string paramsStr) : 
  BaseObject(keyStr, paramsStr),
  IPropertyValueAssociation,
  IBaseObject
{
  public static string EntityName = "PROPERTY_VALUE_ASSOCIATION";
  private IPropertyValueRepresentation _describingPropertyValue;
  private IItemDefinition _describedElement;

  public override void SetParams(IEntityObjects entityObjects)
  {
    this.DescribedElement = this.ParamsArr.Length == 5 ? (IItemDefinition) entityObjects.Get(this.ParamsArr[1]) : throw new Exception("Неверное количество параметров " + this.ParamStr);
    this.DescribingPropertyValue = (IPropertyValueRepresentation) entityObjects.Get(this.ParamsArr[2]);
  }

  public IPropertyValueRepresentation DescribingPropertyValue
  {
    get
    {
      this.Used = true;
      return this._describingPropertyValue;
    }
    private set => this._describingPropertyValue = value;
  }

  public IItemDefinition DescribedElement
  {
    get
    {
      this.Used = true;
      return this._describedElement;
    }
    private set => this._describedElement = value;
  }
}
