// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.ExternalGeometricModel
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using Intermech.GTC.Interfaces.Entities;
using System;

#nullable disable
namespace Intermech.GTC.Server.P21.Entities;

public class ExternalGeometricModel(string keyStr, string paramsStr) : 
  BaseObject(keyStr, paramsStr),
  IExternalGeometricModel,
  IBaseObject
{
  public static string EntityName = "EXTERNAL_GEOMETRIC_MODEL";
  private string _description;
  private string _modelId;
  private string _modelExtent;
  private IDigitalFile _isDefinedAs;
  private ICartesianCoordinateSpace3D _isDefinedIn;

  public override void SetParams(IEntityObjects entityObjects)
  {
    this.Description = this.ParamsArr.Length == 5 ? this.ParamsArr[0] : throw new Exception("Неверное количество параметров " + this.ParamStr);
    this.IsDefinedAs = (IDigitalFile) entityObjects.Get(this.ParamsArr[1]);
    this.IsDefinedIn = (ICartesianCoordinateSpace3D) entityObjects.Get(this.ParamsArr[2]);
    this.ModelId = this.ParamsArr[3];
    this.ModelExtent = this.ParamsArr[4];
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

  public string ModelExtent
  {
    get
    {
      this.Used = true;
      return this._modelExtent;
    }
    private set => this._modelExtent = value;
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

  public ICartesianCoordinateSpace3D IsDefinedIn
  {
    get
    {
      this.Used = true;
      return this._isDefinedIn;
    }
    private set => this._isDefinedIn = value;
  }
}
