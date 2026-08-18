// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.AliasIdentification
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using Intermech.GTC.Interfaces.Entities;
using System;

#nullable disable
namespace Intermech.GTC.Server.P21.Entities;

public class AliasIdentification(string keyStr, string paramsStr) : 
  BaseObject(keyStr, paramsStr),
  IAliasIdentification,
  IBaseObject
{
  public static string EntityName = "ALIAS_IDENTIFICATION";
  private string _aliasId;
  private string _aliasVersionId;
  private IMultiLanguageString _description;
  private IOrganization _aliasScope;
  private IBaseObject _isAppliedTo;

  public override void SetParams(IEntityObjects entityObjects)
  {
    this.AliasId = this.ParamsArr.Length == 5 ? this.ParamsArr[0] : throw new Exception("Неверное количество параметров " + this.ParamStr);
    this.AliasScope = (IOrganization) entityObjects.Get(this.ParamsArr[1]);
    this.AliasVersionId = this.ParamsArr[2];
    this.Description = (IMultiLanguageString) entityObjects.Get(this.ParamsArr[3]);
    this.IsAppliedTo = entityObjects.Get(this.ParamsArr[4]);
  }

  public string AliasId
  {
    get
    {
      this.Used = true;
      return this._aliasId;
    }
    private set => this._aliasId = value;
  }

  public string AliasVersionId
  {
    get
    {
      this.Used = true;
      return this._aliasVersionId;
    }
    private set => this._aliasVersionId = value;
  }

  public IMultiLanguageString Description
  {
    get
    {
      this.Used = true;
      return this._description;
    }
    private set => this._description = value;
  }

  public IOrganization AliasScope
  {
    get
    {
      this.Used = true;
      return this._aliasScope;
    }
    private set => this._aliasScope = value;
  }

  public IBaseObject IsAppliedTo
  {
    get
    {
      this.Used = true;
      return this._isAppliedTo;
    }
    private set => this._isAppliedTo = value;
  }
}
