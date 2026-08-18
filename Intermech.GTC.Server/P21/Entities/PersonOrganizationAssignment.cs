// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.PersonOrganizationAssignment
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

public class PersonOrganizationAssignment(string keyStr, string paramsStr) : 
  BaseObject(keyStr, paramsStr),
  IPersonOrganizationAssignment,
  IBaseObject
{
  public static string EntityName = "PERSON_ORGANIZATION_ASSIGNMENT";
  private string _description;
  private string _role;
  private IBaseObject _assignedPersonOrganization;
  private IBaseObject[] _isAppliedTo;

  public override void SetParams(IEntityObjects entityObjects)
  {
    this.AssignedPersonOrganization = this.ParamsArr.Length == 4 ? entityObjects.Get(this.ParamsArr[0]) : throw new Exception("Неверное количество параметров " + this.ParamStr);
    this.Description = this.ParamsArr[1];
    this.IsAppliedTo = ((IEnumerable<string>) this.ParamsArr[2].Split(',')).Select<string, IBaseObject>(new Func<string, IBaseObject>(entityObjects.Get)).ToArray<IBaseObject>();
    this.Role = this.ParamsArr[3];
  }

  public string Description
  {
    get
    {
      this.Used = true;
      return this._description;
    }
    private set => this._description = value;
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

  public IBaseObject AssignedPersonOrganization
  {
    get
    {
      this.Used = true;
      return this._assignedPersonOrganization;
    }
    private set => this._assignedPersonOrganization = value;
  }

  public IBaseObject[] IsAppliedTo
  {
    get
    {
      this.Used = true;
      return this._isAppliedTo;
    }
    private set => this._isAppliedTo = value;
  }
}
