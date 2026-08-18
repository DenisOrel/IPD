// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.CartesianCoordinateSpace3D
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

public class CartesianCoordinateSpace3D(string keyStr, string paramsStr) : 
  BaseObject(keyStr, paramsStr),
  ICartesianCoordinateSpace3D,
  IBaseObject
{
  public static string EntityName = "CARTESIAN_COORDINATE_SPACE_3D";
  private IUnit[] _unitOfValues;

  public override void SetParams(IEntityObjects entityObjects)
  {
    if (this.ParamsArr.Length != 1)
      throw new Exception("Неверное количество параметров " + this.ParamStr);
    this.UnitOfValues = ((IEnumerable<string>) this.ParamsArr[0].Split(',')).Select<string, IUnit>((Func<string, IUnit>) (itemStr => (IUnit) entityObjects.Get(itemStr))).ToArray<IUnit>();
  }

  public IUnit[] UnitOfValues
  {
    get
    {
      this.Used = true;
      return this._unitOfValues;
    }
    private set => this._unitOfValues = value;
  }
}
