// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.Transformation3D
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using Intermech.GTC.Interfaces.Entities;
using System;

#nullable disable
namespace Intermech.GTC.Server.P21.Entities;

public class Transformation3D(string keyStr, string paramsStr) : 
  BaseObject(keyStr, paramsStr),
  ITransformation3D,
  IBaseObject
{
  public static string EntityName = "TRANSFORMATION_3D";
  private ICartesianPoint _localOrigin;
  private IDirection _axis1;

  public override void SetParams(IEntityObjects entityObjects)
  {
    this.Axis1 = this.ParamsArr.Length == 4 ? (IDirection) entityObjects.Get(this.ParamsArr[0]) : throw new Exception("Неверное количество параметров " + this.ParamStr);
    this.LocalOrigin = (ICartesianPoint) entityObjects.Get(this.ParamsArr[2]);
  }

  public ICartesianPoint LocalOrigin
  {
    get
    {
      this.Used = true;
      return this._localOrigin;
    }
    private set => this._localOrigin = value;
  }

  public IDirection Axis1
  {
    get
    {
      this.Used = true;
      return this._axis1;
    }
    private set => this._axis1 = value;
  }
}
