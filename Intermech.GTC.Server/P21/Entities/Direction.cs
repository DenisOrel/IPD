// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.Direction
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using Intermech.GTC.Interfaces.Entities;
using System;
using System.Linq;

#nullable disable
namespace Intermech.GTC.Server.P21.Entities;

public class Direction(string keyStr, string paramsStr) : BaseObject(keyStr, paramsStr), IDirection, IBaseObject
{
  public static string EntityName = "DIRECTION";
  private double[] _directionRatios;

  public override void SetParams(IEntityObjects entityObjects)
  {
    this.DirectionRatios = this.ParamsArr.Length == 1 ? this.ParamsArr[0].Select<char, double>(new Func<char, double>(Convert.ToDouble)).ToArray<double>() : throw new Exception("Неверное количество параметров " + this.ParamStr);
  }

  public double[] DirectionRatios
  {
    get
    {
      this.Used = true;
      return this._directionRatios;
    }
    private set => this._directionRatios = value;
  }
}
