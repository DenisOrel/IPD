// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.GeometricModelRelationshipWithTransformation
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using Intermech.GTC.Interfaces.Entities;
using System;

#nullable disable
namespace Intermech.GTC.Server.P21.Entities;

public class GeometricModelRelationshipWithTransformation(string keyStr, string paramsStr) : 
  BaseObject(keyStr, paramsStr),
  IGeometricModelRelationshipWithTransformation,
  IBaseObject
{
  public static string EntityName = "GEOMETRIC_MODEL_RELATIONSHIP_WITH_TRANSFORMATION";
  private string _description;
  private string _relationType;
  private IBaseObject _modelPlacement;
  private IBaseObject _relating;
  private IBaseObject _related;

  public override void SetParams(IEntityObjects entityObjects)
  {
    this.Description = this.ParamsArr.Length == 5 ? this.ParamsArr[0] : throw new Exception("Неверное количество параметров " + this.ParamStr);
    this.ModelPlacement = entityObjects.Get(this.ParamsArr[1]);
    this.Related = entityObjects.Get(this.ParamsArr[2]);
    this.Relating = entityObjects.Get(this.ParamsArr[3]);
    this.RelationType = this.ParamsArr[4];
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

  public IBaseObject ModelPlacement
  {
    get
    {
      this.Used = true;
      return this._modelPlacement;
    }
    private set => this._modelPlacement = value;
  }

  public IBaseObject Relating
  {
    get
    {
      this.Used = true;
      return this._relating;
    }
    private set => this._relating = value;
  }

  public IBaseObject Related
  {
    get
    {
      this.Used = true;
      return this._related;
    }
    private set => this._related = value;
  }
}
