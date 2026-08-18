// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.PhysicalItemStructureAssociation
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using Intermech.GTC.Interfaces.Entities;
using System;

#nullable disable
namespace Intermech.GTC.Server.P21.Entities;

public class PhysicalItemStructureAssociation(string keyStr, string paramsStr) : 
  BaseObject(keyStr, paramsStr),
  IPhysicalItemStructureAssociation,
  IBaseObject
{
  public static string EntityName = "PHYSICAL_ITEM_STRUCTURE_ASSOCIATION";
  private IPhysicalItemDefinition _related;
  private IPhysicalItemDefinition _relating;

  public override void SetParams(IEntityObjects entityObjects)
  {
    this.Related = this.ParamsArr.Length == 2 ? (IPhysicalItemDefinition) entityObjects.Get(this.ParamsArr[0]) : throw new Exception("Неверное количество параметров " + this.ParamStr);
    this.Relating = (IPhysicalItemDefinition) entityObjects.Get(this.ParamsArr[1]);
  }

  public IPhysicalItemDefinition Related
  {
    get
    {
      this.Used = true;
      return this._related;
    }
    private set => this._related = value;
  }

  public IPhysicalItemDefinition Relating
  {
    get
    {
      this.Used = true;
      return this._relating;
    }
    private set => this._relating = value;
  }
}
