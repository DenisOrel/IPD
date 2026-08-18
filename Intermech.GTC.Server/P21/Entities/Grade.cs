// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.Grade
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

public class Grade(string keyStr, string paramsStr) : BaseObject(keyStr, paramsStr), IGrade, IBaseObject
{
  public static string EntityName = "GRADE";
  private ISubstrate _substrate;
  private ICoating _coating;
  private IMaterialDesignation[] _workpieceMaterial;
  private ICuttingCondition[] _cuttingCondition;
  private string _identifier;
  private string _standartDesignation;

  public override void SetParams(IEntityObjects entityObjects)
  {
    this.Coating = this.ParamsArr.Length == 6 ? (ICoating) entityObjects.Get(this.ParamsArr[0]) : throw new Exception("Неверное количество параметров " + this.ParamStr);
    this.CuttingCondition = ((IEnumerable<string>) this.ParamsArr[1].Split(',')).Select<string, ICuttingCondition>((Func<string, ICuttingCondition>) (itemStr => (ICuttingCondition) entityObjects.Get(itemStr))).ToArray<ICuttingCondition>();
    this.Identifier = this.ParamsArr[2];
    this.StandartDesignation = this.ParamsArr[3];
    this.Substrate = (ISubstrate) entityObjects.Get(this.ParamsArr[4]);
    this.WorkpieceMaterial = ((IEnumerable<string>) this.ParamsArr[5].Split(',')).Select<string, IMaterialDesignation>((Func<string, IMaterialDesignation>) (itemStr => (IMaterialDesignation) entityObjects.Get(itemStr))).ToArray<IMaterialDesignation>();
  }

  public ISubstrate Substrate
  {
    get
    {
      this.Used = true;
      return this._substrate;
    }
    private set => this._substrate = value;
  }

  public ICoating Coating
  {
    get
    {
      this.Used = true;
      return this._coating;
    }
    private set => this._coating = value;
  }

  public IMaterialDesignation[] WorkpieceMaterial
  {
    get
    {
      this.Used = true;
      return this._workpieceMaterial;
    }
    private set => this._workpieceMaterial = value;
  }

  public ICuttingCondition[] CuttingCondition
  {
    get
    {
      this.Used = true;
      return this._cuttingCondition;
    }
    private set => this._cuttingCondition = value;
  }

  public string Identifier
  {
    get
    {
      this.Used = true;
      return this._identifier;
    }
    private set => this._identifier = value;
  }

  public string StandartDesignation
  {
    get
    {
      this.Used = true;
      return this._standartDesignation;
    }
    private set => this._standartDesignation = value;
  }
}
