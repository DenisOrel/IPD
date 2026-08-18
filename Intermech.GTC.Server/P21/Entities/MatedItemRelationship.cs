// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.MatedItemRelationship
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

public class MatedItemRelationship(string keyStr, string paramsStr) : 
  BaseObject(keyStr, paramsStr),
  IMatedItemRelationship,
  IBaseObject
{
  public static string EntityName = "MATED_ITEM_RELATIONSHIP";
  private IItemInstance[] _matingMaterial;
  private IMatingAssociation _relating;
  private IMatingAssociation _related;

  public override void SetParams(IEntityObjects entityObjects)
  {
    if (this.ParamsArr.Length != 3)
      throw new Exception("Неверное количество параметров " + this.ParamStr);
    this.MatingMaterial = ((IEnumerable<string>) this.ParamsArr[0].Split(',')).Select<string, IItemInstance>((Func<string, IItemInstance>) (itemStr => (IItemInstance) entityObjects.Get(itemStr))).ToArray<IItemInstance>();
    this.Related = (IMatingAssociation) entityObjects.Get(this.ParamsArr[1]);
    this.Relating = (IMatingAssociation) entityObjects.Get(this.ParamsArr[2]);
  }

  public IItemInstance[] MatingMaterial
  {
    get
    {
      this.Used = true;
      return this._matingMaterial;
    }
    private set => this._matingMaterial = value;
  }

  public IMatingAssociation Relating
  {
    get
    {
      this.Used = true;
      return this._relating;
    }
    private set => this._relating = value;
  }

  public IMatingAssociation Related
  {
    get
    {
      this.Used = true;
      return this._related;
    }
    private set => this._related = value;
  }
}
