// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.CuttingCondition
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using Intermech.GTC.Interfaces.Entities;
using System;

#nullable disable
namespace Intermech.GTC.Server.P21.Entities;

public class CuttingCondition(string keyStr, string paramsStr) : 
  BaseObject(keyStr, paramsStr),
  ICuttingCondition,
  IBaseObject
{
  public static string EntityName = "CUTTING_CONDITION";
  private string _conditionName;

  public override void SetParams(IEntityObjects entityObjects)
  {
    this.ConditionName = this.ParamsArr.Length == 1 ? this.ParamsArr[0] : throw new Exception("Неверное количество параметров " + this.ParamStr);
  }

  public string ConditionName
  {
    get
    {
      this.Used = true;
      return this._conditionName;
    }
    private set => this._conditionName = value;
  }
}
