// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.DocumentCreationProperty
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using Intermech.GTC.Interfaces.Entities;
using System;

#nullable disable
namespace Intermech.GTC.Server.P21.Entities;

public class DocumentCreationProperty(string keyStr, string paramsStr) : 
  BaseObject(keyStr, paramsStr),
  IDocumentCreationProperty,
  IBaseObject
{
  public static string EntityName = "DOCUMENT_CREATION_PROPERTY";
  private string _creatingInterface;
  private string _creatingSystem;
  private string _operatingSystem;

  public override void SetParams(IEntityObjects entityObjects)
  {
    this.CreatingInterface = this.ParamsArr.Length == 3 ? this.ParamsArr[0] : throw new Exception("Неверное количество параметров " + this.ParamStr);
    this.CreatingSystem = this.ParamsArr[1];
    this.OperatingSystem = this.ParamsArr[2];
  }

  public string CreatingInterface
  {
    get
    {
      this.Used = true;
      return this._creatingInterface;
    }
    private set => this._creatingInterface = value;
  }

  public string CreatingSystem
  {
    get
    {
      this.Used = true;
      return this._creatingSystem;
    }
    private set => this._creatingSystem = value;
  }

  public string OperatingSystem
  {
    get
    {
      this.Used = true;
      return this._operatingSystem;
    }
    private set => this._operatingSystem = value;
  }
}
