// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.EffectivityAssignment
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using Intermech.GTC.Interfaces.Entities;
using System;

#nullable disable
namespace Intermech.GTC.Server.P21.Entities;

public class EffectivityAssignment(string keyStr, string paramsStr) : 
  BaseObject(keyStr, paramsStr),
  IEffectivityAssignment,
  IBaseObject
{
  public static string EntityName = "EFFECTIVITY_ASSIGNMENT";
  private bool? _effectivityIndication;
  private string _role;
  private IBaseObject _effectiveElement;
  private IEffectivity _assignedEffectivity;

  public override void SetParams(IEntityObjects entityObjects)
  {
    this.AssignedEffectivity = this.ParamsArr.Length == 4 ? (IEffectivity) entityObjects.Get(this.ParamsArr[0]) : throw new Exception("Неверное количество параметров " + this.ParamStr);
    this.EffectiveElement = entityObjects.Get(this.ParamsArr[1]);
    this.EffectivityIndication = BoolConverter.Convert(this.ParamsArr[2]);
    this.Role = this.ParamsArr[3];
  }

  public bool? EffectivityIndication
  {
    get
    {
      this.Used = true;
      return this._effectivityIndication;
    }
    private set => this._effectivityIndication = value;
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

  public IBaseObject EffectiveElement
  {
    get
    {
      this.Used = true;
      return this._effectiveElement;
    }
    private set => this._effectiveElement = value;
  }

  public IEffectivity AssignedEffectivity
  {
    get
    {
      this.Used = true;
      return this._assignedEffectivity;
    }
    private set => this._assignedEffectivity = value;
  }
}
