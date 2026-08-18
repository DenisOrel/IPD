// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.PlibPropertyReference
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using Intermech.GTC.Interfaces.Entities;
using System;

#nullable disable
namespace Intermech.GTC.Server.P21.Entities;

public class PlibPropertyReference(string keyStr, string paramsStr) : 
  BaseObject(keyStr, paramsStr),
  IPlibPropertyReference,
  IBaseObject
{
  public static string EntityName = "PLIB_PROPERTY_REFERENCE";
  private string _code;
  private string _version;
  private IPlibClassReference _nameScope;

  public override void SetParams(IEntityObjects entityObjects)
  {
    this.Code = this.ParamsArr.Length == 3 ? this.ParamsArr[0] : throw new Exception("Неверное количество параметров " + this.ParamStr);
    this.NameScope = (IPlibClassReference) entityObjects.Get(this.ParamsArr[1]);
    this.Version = this.ParamsArr[2];
  }

  public string Code
  {
    get
    {
      this.Used = true;
      return this._code;
    }
    private set => this._code = value;
  }

  public string Version
  {
    get
    {
      this.Used = true;
      return this._version;
    }
    private set => this._version = value;
  }

  public IPlibClassReference NameScope
  {
    get
    {
      this.Used = true;
      return this._nameScope;
    }
    private set => this._nameScope = value;
  }
}
