// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.CartesianPoint
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

public class CartesianPoint(string keyStr, string paramsStr) : 
  BaseObject(keyStr, paramsStr),
  ICartesianPoint,
  IBaseObject
{
  public static string EntityName = "CARTESIAN_POINT";
  private double[] _coordinates;

  public override void SetParams(IEntityObjects entityObjects)
  {
    this.Coordinates = this.ParamsArr.Length == 1 ? ((IEnumerable<string>) this.ParamsArr[0].Split(',')).Select<string, double>(new Func<string, double>(Convert.ToDouble)).ToArray<double>() : throw new Exception("Неверное количество параметров " + this.ParamStr);
  }

  public double[] Coordinates
  {
    get
    {
      this.Used = true;
      return this._coordinates;
    }
    private set => this._coordinates = value;
  }
}
