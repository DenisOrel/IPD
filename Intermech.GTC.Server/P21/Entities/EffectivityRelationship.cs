// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.EffectivityRelationship
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using Intermech.GTC.Interfaces.Entities;
using System;

#nullable disable
namespace Intermech.GTC.Server.P21.Entities;

public class EffectivityRelationship(string keyStr, string paramsStr) : 
  BaseObject(keyStr, paramsStr),
  IEffectivityRelationship,
  IBaseObject
{
  public static string EntityName = "EFFECTIVITY_RELATIONSHIP";
  private string _description;
  private string _relationType;
  private IEffectivity _relating;
  private IEffectivity _related;

  public override void SetParams(IEntityObjects entityObjects)
  {
    this.Description = this.ParamsArr.Length == 4 ? this.ParamsArr[0] : throw new Exception("Неверное количество параметров " + this.ParamStr);
    this.Related = (IEffectivity) entityObjects.Get(this.ParamsArr[1]);
    this.Relating = (IEffectivity) entityObjects.Get(this.ParamsArr[2]);
    this.RelationType = this.ParamsArr[3];
  }

  public string Description
  {
    get
    {
      this.Used = true;
      return this._description;
    }
    private set => this._description = value;
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

  public IEffectivity Relating
  {
    get
    {
      this.Used = true;
      return this._relating;
    }
    private set => this._relating = value;
  }

  public IEffectivity Related
  {
    get
    {
      this.Used = true;
      return this._related;
    }
    private set => this._related = value;
  }
}
