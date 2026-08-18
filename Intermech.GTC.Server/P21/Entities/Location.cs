// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.Location
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using Intermech.GTC.Interfaces.Entities;
using System;

#nullable disable
namespace Intermech.GTC.Server.P21.Entities;

public class Location(string keyStr, string paramsStr) : BaseObject(keyStr, paramsStr), ILocation, IBaseObject
{
  public static string EntityName = "LOCATION";
  private string _locationId;
  private string _locationName;
  private string _locationType;

  public override void SetParams(IEntityObjects entityObjects)
  {
    this.LocationId = this.ParamsArr.Length == 3 ? this.ParamsArr[0] : throw new Exception("Неверное количество параметров " + this.ParamStr);
    this.LocationName = this.ParamsArr[1];
    this.LocationType = this.ParamsArr[2];
  }

  public string LocationId
  {
    get
    {
      this.Used = true;
      return this._locationId;
    }
    private set => this._locationId = value;
  }

  public string LocationName
  {
    get
    {
      this.Used = true;
      return this._locationName;
    }
    private set => this._locationName = value;
  }

  public string LocationType
  {
    get
    {
      this.Used = true;
      return this._locationType;
    }
    private set => this._locationType = value;
  }
}
