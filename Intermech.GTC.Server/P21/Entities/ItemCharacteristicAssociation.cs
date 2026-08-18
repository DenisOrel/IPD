// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.ItemCharacteristicAssociation
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using Intermech.GTC.Interfaces.Entities;
using System;

#nullable disable
namespace Intermech.GTC.Server.P21.Entities;

public class ItemCharacteristicAssociation(string keyStr, string paramsStr) : 
  BaseObject(keyStr, paramsStr),
  IItemCharacteristicAssociation,
  IBaseObject
{
  public static string EntityName = "ITEM_CHARACTERISTIC_ASSOCIATION";
  private string _relationType;
  private IItemDefinition _associatedItem;
  private IGrade _associatedCharacteristic;

  public override void SetParams(IEntityObjects entityObjects)
  {
    this.AssociatedCharacteristic = this.ParamsArr.Length == 3 ? (IGrade) entityObjects.Get(this.ParamsArr[0]) : throw new Exception("Неверное количество параметров " + this.ParamStr);
    this.AssociatedItem = (IItemDefinition) entityObjects.Get(this.ParamsArr[1]);
    this.RelationType = this.ParamsArr[2];
  }

  public string RelationType
  {
    get
    {
      this.Used = true;
      return this._relationType;
    }
    private set => this._relationType = value;
  }

  public IItemDefinition AssociatedItem
  {
    get
    {
      this.Used = true;
      return this._associatedItem;
    }
    private set => this._associatedItem = value;
  }

  public IGrade AssociatedCharacteristic
  {
    get
    {
      this.Used = true;
      return this._associatedCharacteristic;
    }
    private set => this._associatedCharacteristic = value;
  }
}
