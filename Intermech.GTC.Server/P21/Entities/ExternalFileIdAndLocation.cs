// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.ExternalFileIdAndLocation
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using Intermech.GTC.Interfaces.Entities;
using System;

#nullable disable
namespace Intermech.GTC.Server.P21.Entities;

public class ExternalFileIdAndLocation(string keyStr, string paramsStr) : 
  BaseObject(keyStr, paramsStr),
  IExternalFileIdAndLocation,
  IBaseObject
{
  public static string EntityName = "EXTERNAL_FILE_ID_AND_LOCATION";
  private string _externalId;
  private IDocumentLocationProperty _location;

  public override void SetParams(IEntityObjects entityObjects)
  {
    this.ExternalId = this.ParamsArr.Length == 2 ? this.ParamsArr[0] : throw new Exception("Неверное количество параметров " + this.ParamStr);
    this.Location = (IDocumentLocationProperty) entityObjects.Get(this.ParamsArr[1]);
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

  public IDocumentLocationProperty Location
  {
    get
    {
      this.Used = true;
      return this._location;
    }
    private set => this._location = value;
  }
}
