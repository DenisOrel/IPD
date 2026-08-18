// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.SpecificItemClassification
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

public class SpecificItemClassification(string keyStr, string paramsStr) : 
  BaseObject(keyStr, paramsStr),
  ISpecificItemClassification,
  IBaseObject
{
  public static string EntityName = "SPECIFIC_ITEM_CLASSIFICATION";
  private string _classificationName;
  private string _description;
  private IBaseObject[] _associatedItem;

  public override void SetParams(IEntityObjects entityObjects)
  {
    this.AssociatedItem = this.ParamsArr.Length == 3 ? ((IEnumerable<string>) this.ParamsArr[0].Split(',')).Select<string, IBaseObject>(new Func<string, IBaseObject>(entityObjects.Get)).ToArray<IBaseObject>() : throw new Exception("Неверное количество параметров " + this.ParamStr);
    this.ClassificationName = this.ParamsArr[1];
    this.Description = this.ParamsArr[2];
  }

  public string ClassificationName
  {
    get
    {
      this.Used = true;
      return this._classificationName;
    }
    private set => this._classificationName = value;
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

  public IBaseObject[] AssociatedItem
  {
    get
    {
      this.Used = true;
      return this._associatedItem;
    }
    private set => this._associatedItem = value;
  }
}
