// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.AssemblyAssociation
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using Intermech.GTC.Interfaces.Entities;
using System;

#nullable disable
namespace Intermech.GTC.Server.P21.Entities;

public class AssemblyAssociation(string keyStr, string paramsStr) : 
  BaseObject(keyStr, paramsStr),
  IAssemblyAssociation,
  IBaseObject
{
  public static string EntityName = "ASSEMBLY_ASSOCIATION";
  private IItemInstance _related;
  private IAssemblyDefinition _relating;
  private IGeometricModelRelationshipWithTransformation _placement;

  public override void SetParams(IEntityObjects entityObjects)
  {
    this.Placement = this.ParamsArr.Length == 3 ? (IGeometricModelRelationshipWithTransformation) entityObjects.Get(this.ParamsArr[0]) : throw new Exception("Неверное количество параметров " + this.ParamStr);
    this.Related = (IItemInstance) entityObjects.Get(this.ParamsArr[1]);
    this.Relating = (IAssemblyDefinition) entityObjects.Get(this.ParamsArr[2]);
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

  public IAssemblyDefinition Relating
  {
    get
    {
      this.Used = true;
      return this._relating;
    }
    private set => this._relating = value;
  }

  public IGeometricModelRelationshipWithTransformation Placement
  {
    get
    {
      this.Used = true;
      return this._placement;
    }
    private set => this._placement = value;
  }
}
