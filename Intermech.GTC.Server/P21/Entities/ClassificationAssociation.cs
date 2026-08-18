// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.ClassificationAssociation
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using Intermech.GTC.Interfaces.Entities;
using System;

#nullable disable
namespace Intermech.GTC.Server.P21.Entities;

public class ClassificationAssociation(string keyStr, string paramsStr) : 
  BaseObject(keyStr, paramsStr),
  IClassificationAssociation,
  IBaseObject
{
  public static string EntityName = "CLASSIFICATION_ASSOCIATION";
  private IGeneralClassification _associatedClassification;
  private string _definitional;
  private string _role;
  private IBaseObject _classifiedElement;

  public override void SetParams(IEntityObjects entityObjects)
  {
    this.AssociatedClassification = this.ParamsArr.Length == 4 ? (IGeneralClassification) entityObjects.Get(this.ParamsArr[0]) : throw new Exception("Неверное количество параметров " + this.ParamStr);
    this.ClassifiedElement = entityObjects.Get(this.ParamsArr[1]);
    this.Definitional = this.ParamsArr[2];
    this.Role = this.ParamsArr[3];
  }

  public IGeneralClassification AssociatedClassification
  {
    get
    {
      this.Used = true;
      return this._associatedClassification;
    }
    private set => this._associatedClassification = value;
  }

  public string Definitional
  {
    get
    {
      this.Used = true;
      return this._definitional;
    }
    private set => this._definitional = value;
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

  public IBaseObject ClassifiedElement
  {
    get
    {
      this.Used = true;
      return this._classifiedElement;
    }
    private set => this._classifiedElement = value;
  }
}
