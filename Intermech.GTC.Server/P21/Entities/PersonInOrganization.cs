// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.PersonInOrganization
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using Intermech.GTC.Interfaces.Entities;
using System;

#nullable disable
namespace Intermech.GTC.Server.P21.Entities;

public class PersonInOrganization(string keyStr, string paramsStr) : 
  BaseObject(keyStr, paramsStr),
  IPersonInOrganization,
  IBaseObject
{
  public static string EntityName = "PERSON_IN_ORGANIZATION";
  private string _id;
  private string _location;
  private string _role;
  private IOrganization _organization;
  private IPerson _person;

  public override void SetParams(IEntityObjects entityObjects)
  {
    this.Organization = this.ParamsArr.Length == 5 ? (IOrganization) entityObjects.Get(this.ParamsArr[0]) : throw new Exception("Неверное количество параметров " + this.ParamStr);
    this.Person = (IPerson) entityObjects.Get(this.ParamsArr[1]);
    this.Id = this.ParamsArr[2];
    this.Location = this.ParamsArr[3];
    this.Role = this.ParamsArr[4];
  }

  public string Id
  {
    get
    {
      this.Used = true;
      return this._id;
    }
    private set => this._id = value;
  }

  public string Location
  {
    get
    {
      this.Used = true;
      return this._location;
    }
    private set => this._location = value;
  }

  public string Role
  {
    get
    {
      this.Used = true;
      return this._role;
    }
    private set => this._role = value;
  }

  public IOrganization Organization
  {
    get
    {
      this.Used = true;
      return this._organization;
    }
    private set => this._organization = value;
  }

  public IPerson Person
  {
    get
    {
      this.Used = true;
      return this._person;
    }
    private set => this._person = value;
  }
}
