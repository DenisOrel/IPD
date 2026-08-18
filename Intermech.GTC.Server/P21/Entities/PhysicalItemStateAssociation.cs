// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.PhysicalItemStateAssociation
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using Intermech.GTC.Interfaces.Entities;
using System;

#nullable disable
namespace Intermech.GTC.Server.P21.Entities;

public class PhysicalItemStateAssociation(string keyStr, string paramsStr) : 
  BaseObject(keyStr, paramsStr),
  IPhysicalItemStateAssociation,
  IBaseObject
{
  public static string EntityName = "PHYSICAL_ITEM_STATE_ASSOCIATION";
  private string _role;
  private IPhysicalItemDefinition _associatedPhysicalItem;
  private IState _associatedState;

  public override void SetParams(IEntityObjects entityObjects)
  {
    this.AssociatedPhysicalItem = this.ParamsArr.Length == 3 ? (IPhysicalItemDefinition) entityObjects.Get(this.ParamsArr[0]) : throw new Exception("Неверное количество параметров " + this.ParamStr);
    this.AssociatedState = (IState) entityObjects.Get(this.ParamsArr[1]);
    this.Role = this.ParamsArr[2];
  }

  public string Role
  {
    get
    {
      this.Used = true;
      return this._role;
    }
    private set => this._role = value;
  }

  public IPhysicalItemDefinition AssociatedPhysicalItem
  {
    get
    {
      this.Used = true;
      return this._associatedPhysicalItem;
    }
    private set => this._associatedPhysicalItem = value;
  }

  public IState AssociatedState
  {
    get
    {
      this.Used = true;
      return this._associatedState;
    }
    private set => this._associatedState = value;
  }
}
