// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.ExternalPicture
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using Intermech.GTC.Interfaces.Entities;
using System;

#nullable disable
namespace Intermech.GTC.Server.P21.Entities;

public class ExternalPicture(string keyStr, string paramsStr) : 
  BaseObject(keyStr, paramsStr),
  IExternalPicture,
  IBaseObject
{
  public static string EntityName = "EXTERNAL_PICTURE";
  private string _description;
  private string _modelId;
  private IDigitalFile _isDefinedAs;
  private ICartesianCoordinateSpace2D _isDefinedIn;

  public override void SetParams(IEntityObjects entityObjects)
  {
    this.Description = this.ParamsArr.Length == 4 ? this.ParamsArr[0] : throw new Exception("Неверное количество параметров " + this.ParamStr);
    this.IsDefinedAs = (IDigitalFile) entityObjects.Get(this.ParamsArr[1]);
    this.IsDefinedIn = (ICartesianCoordinateSpace2D) entityObjects.Get(this.ParamsArr[2]);
    this.ModelId = this.ParamsArr[3];
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

  public string ModelId
  {
    get
    {
      this.Used = true;
      return this._modelId;
    }
    private set => this._modelId = value;
  }

  public IDigitalFile IsDefinedAs
  {
    get
    {
      this.Used = true;
      return this._isDefinedAs;
    }
    private set => this._isDefinedAs = value;
  }

  public ICartesianCoordinateSpace2D IsDefinedIn
  {
    get
    {
      this.Used = true;
      return this._isDefinedIn;
    }
    private set => this._isDefinedIn = value;
  }
}
