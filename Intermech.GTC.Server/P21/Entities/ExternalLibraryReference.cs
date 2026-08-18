// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.ExternalLibraryReference
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using Intermech.GTC.Interfaces.Entities;
using System;

#nullable disable
namespace Intermech.GTC.Server.P21.Entities;

public class ExternalLibraryReference(string keyStr, string paramsStr) : 
  BaseObject(keyStr, paramsStr),
  IExternalLibraryReference,
  IBaseObject
{
  public static string EntityName = "EXTERNAL_LIBRARY_REFERENCE";
  private IMultiLanguageString _description;
  private string _externalId;
  private string _libraryType;

  public override void SetParams(IEntityObjects entityObjects)
  {
    this.Description = this.ParamsArr.Length == 3 ? (IMultiLanguageString) entityObjects.Get(this.ParamsArr[0]) : throw new Exception("Неверное количество параметров " + this.ParamStr);
    this.ExternalId = this.ParamsArr[1];
    this.LibraryType = this.ParamsArr[2];
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

  public string ExternalId
  {
    get
    {
      this.Used = true;
      return this._externalId;
    }
    private set => this._externalId = value;
  }

  public string LibraryType
  {
    get
    {
      this.Used = true;
      return this._libraryType;
    }
    private set => this._libraryType = value;
  }
}
