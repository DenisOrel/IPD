// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.MatingAssociation
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using Intermech.GTC.Interfaces.Entities;
using System;

#nullable disable
namespace Intermech.GTC.Server.P21.Entities;

public class MatingAssociation(string keyStr, string paramsStr) : 
  BaseObject(keyStr, paramsStr),
  IMatingAssociation,
  IBaseObject
{
  public static string EntityName = "MATING_ASSOCIATION";
  private IItemInstance _related;
  private IMatingDefinition _relating;

  public override void SetParams(IEntityObjects entityObjects)
  {
    this.Related = this.ParamsArr.Length == 3 ? (IItemInstance) entityObjects.Get(this.ParamsArr[1]) : throw new Exception("Неверное количество параметров " + this.ParamStr);
    this.Relating = (IMatingDefinition) entityObjects.Get(this.ParamsArr[2]);
  }

  public IItemInstance Related
  {
    get
    {
      this.Used = true;
      return this._related;
    }
    private set => this._related = value;
  }

  public IMatingDefinition Relating
  {
    get
    {
      this.Used = true;
      return this._relating;
    }
    private set => this._relating = value;
  }
}
