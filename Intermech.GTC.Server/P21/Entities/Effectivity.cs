// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.Effectivity
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

public class Effectivity(string keyStr, string paramsStr) : BaseObject(keyStr, paramsStr), IEffectivity, IBaseObject
{
  public static string EntityName = "EFFECTIVITY";
  private string _description;
  private string _effectivityContext;
  private string _id;
  private string _versionId;
  private IOrganization[] _concernedOrganization;
  private IDateTime _startDefinition;
  private IDuration _period;

  public override void SetParams(IEntityObjects entityObjects)
  {
    if (this.ParamsArr.Length != 8)
      throw new Exception("Неверное количество параметров " + this.ParamStr);
    this.ConcernedOrganization = ((IEnumerable<string>) this.ParamsArr[0].Split(',')).Select<string, IOrganization>((Func<string, IOrganization>) (itemStr => (IOrganization) entityObjects.Get(itemStr))).ToArray<IOrganization>();
    this.Description = this.ParamsArr[1];
    this.EffectivityContext = this.ParamsArr[2];
    this.Id = this.ParamsArr[3];
    this.VersionId = this.ParamsArr[4];
    this.Period = (IDuration) entityObjects.Get(this.ParamsArr[5]);
    this.StartDefinition = (IDateTime) entityObjects.Get(this.ParamsArr[6]);
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

  public string EffectivityContext
  {
    get
    {
      this.Used = true;
      return this._effectivityContext;
    }
    private set => this._effectivityContext = value;
  }

  public string Id
  {
    get
    {
      this.Used = true;
      return this._id;
    }
    private set => this._id = value;
  }

  public string VersionId
  {
    get
    {
      this.Used = true;
      return this._versionId;
    }
    private set => this._versionId = value;
  }

  public IOrganization[] ConcernedOrganization
  {
    get
    {
      this.Used = true;
      return this._concernedOrganization;
    }
    private set => this._concernedOrganization = value;
  }

  public IDateTime StartDefinition
  {
    get
    {
      this.Used = true;
      return this._startDefinition;
    }
    private set => this._startDefinition = value;
  }

  public IDuration Period
  {
    get
    {
      this.Used = true;
      return this._period;
    }
    private set => this._period = value;
  }
}
