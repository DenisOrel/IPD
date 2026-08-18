// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.GeneralClassification
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using Intermech.GTC.Interfaces.Entities;
using System;

#nullable disable
namespace Intermech.GTC.Server.P21.Entities;

public class GeneralClassification(string keyStr, string paramsStr) : 
  BaseObject(keyStr, paramsStr),
  IGeneralClassification,
  IBaseObject
{
  public static string EntityName = "GENERAL_CLASSIFICATION";
  private IBaseObject _classificationSource;
  private IMultiLanguageString _description;
  private string _id;
  private string _versionId;

  public override void SetParams(IEntityObjects entityObjects)
  {
    this.ClassificationSource = this.ParamsArr.Length == 5 ? entityObjects.Get(this.ParamsArr[0]) : throw new Exception("Неверное количество параметров " + this.ParamStr);
    this.Description = (IMultiLanguageString) entityObjects.Get(this.ParamsArr[1]);
    this.Id = this.ParamsArr[2];
    this.VersionId = this.ParamsArr[4];
  }

  public IBaseObject ClassificationSource
  {
    get
    {
      this.Used = true;
      return this._classificationSource;
    }
    private set => this._classificationSource = value;
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

  public string VersionId
  {
    get
    {
      this.Used = true;
      return this._versionId;
    }
    private set => this._versionId = value;
  }
}
