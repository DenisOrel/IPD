// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.DocumentContentProperty
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

public class DocumentContentProperty(string keyStr, string paramsStr) : 
  BaseObject(keyStr, paramsStr),
  IDocumentContentProperty,
  IBaseObject
{
  public static string EntityName = "DOCUMENT_CONTENT_PROPERTY";
  private string _detailLevel;
  private string _geometryType;
  private ILanguage[] _languages;

  public override void SetParams(IEntityObjects entityObjects)
  {
    this.DetailLevel = this.ParamsArr.Length == 4 ? this.ParamsArr[0] : throw new Exception("Неверное количество параметров " + this.ParamStr);
    this.GeometryType = this.ParamsArr[1];
    this.Languages = ((IEnumerable<string>) this.ParamsArr[2].Split(',')).Select<string, ILanguage>((Func<string, ILanguage>) (itemStr => (ILanguage) entityObjects.Get(itemStr))).ToArray<ILanguage>();
  }

  public string DetailLevel
  {
    get
    {
      this.Used = true;
      return this._detailLevel;
    }
    private set => this._detailLevel = value;
  }

  public string GeometryType
  {
    get
    {
      this.Used = true;
      return this._geometryType;
    }
    private set => this._geometryType = value;
  }

  public ILanguage[] Languages
  {
    get
    {
      this.Used = true;
      return this._languages;
    }
    private set => this._languages = value;
  }
}
