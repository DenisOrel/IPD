// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.Person
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using Intermech.GTC.Interfaces.Entities;
using System;

#nullable disable
namespace Intermech.GTC.Server.P21.Entities;

public class Person(string keyStr, string paramsStr) : BaseObject(keyStr, paramsStr), IPerson, IBaseObject
{
  public static string EntityName = "PERSON";
  private string _personName;
  private string _prefferedBuisnessAdress;

  public override void SetParams(IEntityObjects entityObjects)
  {
    this.PersonName = this.ParamsArr.Length == 2 ? this.ParamsArr[0] : throw new Exception("Неверное количество параметров " + this.ParamStr);
    this.PrefferedBuisnessAdress = this.ParamsArr[1];
  }

  public string PersonName
  {
    get
    {
      this.Used = true;
      return this._personName;
    }
    private set => this._personName = value;
  }

  public string PrefferedBuisnessAdress
  {
    get
    {
      this.Used = true;
      return this._prefferedBuisnessAdress;
    }
    private set => this._prefferedBuisnessAdress = value;
  }
}
