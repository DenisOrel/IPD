// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.Organization
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using Intermech.GTC.Interfaces.Entities;
using System;

#nullable disable
namespace Intermech.GTC.Server.P21.Entities;

public class Organization(string keyStr, string paramsStr) : BaseObject(keyStr, paramsStr), IOrganization, IBaseObject
{
  public static string EntityName = "ORGANIZATION";
  private string _deliveryAdress = string.Empty;
  private string _id = string.Empty;
  private string _organizationName = string.Empty;
  private string _organizationType = string.Empty;
  private string _postalAdress = string.Empty;
  private string _visitorAdress = string.Empty;

  public override void SetParams(IEntityObjects entityObjects)
  {
    this.DeliveryAdress = this.ParamsArr.Length == 6 ? this.ParamsArr[0] : throw new Exception("Неверное количество параметров " + this.ParamStr);
    this.Id = this.ParamsArr[1];
    this.OrganizationName = this.ParamsArr[2];
    this.OrganizationType = this.ParamsArr[3];
    this.PostalAdress = this.ParamsArr[4];
    this.VisitorAdress = this.ParamsArr[5];
  }

  public string DeliveryAdress
  {
    get
    {
      this.Used = true;
      return this._deliveryAdress;
    }
    private set => this._deliveryAdress = value;
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

  public string OrganizationName
  {
    get
    {
      this.Used = true;
      return this._organizationName;
    }
    private set => this._organizationName = value;
  }

  public string OrganizationType
  {
    get
    {
      this.Used = true;
      return this._organizationType;
    }
    private set => this._organizationType = value;
  }

  public string PostalAdress
  {
    get
    {
      this.Used = true;
      return this._postalAdress;
    }
    private set => this._postalAdress = value;
  }

  public string VisitorAdress
  {
    get
    {
      this.Used = true;
      return this._visitorAdress;
    }
    private set => this._visitorAdress = value;
  }
}
