// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.Property
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

public class Property(string keyStr, string paramsStr) : BaseObject(keyStr, paramsStr), IProperty, IBaseObject
{
  public static string EntityName = "PROPERTY";
  private IUnit[] _allowedUnit;
  private IMultiLanguageString _description;
  private string _id;
  private PropertyType _propertyType;
  private string _versionId;
  private IBaseObject _propertySource;

  public override void SetParams(IEntityObjects entityObjects)
  {
    if (this.ParamsArr.Length != 6)
      throw new Exception("Неверное количество параметров " + this.ParamStr);
    this.AllowedUnit = ((IEnumerable<string>) this.ParamsArr[0].Split(',')).Select<string, IUnit>((Func<string, IUnit>) (itemStr => (IUnit) entityObjects.Get(itemStr))).ToArray<IUnit>();
    this.Description = (IMultiLanguageString) entityObjects.Get(this.ParamsArr[1]);
    this.Id = this.ParamsArr[2];
    this.PropertySource = entityObjects.Get(this.ParamsArr[3]);
    PropertyType result;
    if (Enum.TryParse<PropertyType>(this.ParamsArr[4], out result))
      this.PropertyType = result;
    this.VersionId = this.ParamsArr[5];
  }

  public IUnit[] AllowedUnit
  {
    get
    {
      this.Used = true;
      return this._allowedUnit;
    }
    private set => this._allowedUnit = value;
  }

  public IMultiLanguageString Description
  {
    get
    {
      this.Used = true;
      return this._description;
    }
    private set => this._description = value;
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

  public PropertyType PropertyType
  {
    get
    {
      this.Used = true;
      return this._propertyType;
    }
    private set => this._propertyType = value;
  }

  public string VersionId
  {
    get
    {
      this.Used = true;
      return this._versionId;
    }
    private set => this._versionId = value;
  }

  public IBaseObject PropertySource
  {
    get
    {
      this.Used = true;
      return this._propertySource;
    }
    private set => this._propertySource = value;
  }
}
