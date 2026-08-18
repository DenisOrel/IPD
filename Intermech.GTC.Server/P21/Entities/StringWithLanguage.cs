// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.StringWithLanguage
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using Intermech.GTC.Interfaces.Entities;
using System;

#nullable disable
namespace Intermech.GTC.Server.P21.Entities;

public class StringWithLanguage(string keyStr, string paramsStr) : 
  BaseObject(keyStr, paramsStr),
  IStringWithLanguage,
  IBaseObject
{
  public static string EntityName = "STRING_WITH_LANGUAGE";
  private string _contents;
  private ILanguage _languageSpecification;

  public override void SetParams(IEntityObjects entityObjects)
  {
    this.Contents = this.ParamsArr.Length == 2 ? this.ParamsArr[0] : throw new Exception("Неверное количество параметров " + this.ParamStr);
    this.LanguageSpecification = (ILanguage) entityObjects.Get(this.ParamsArr[1]);
  }

  public string Contents
  {
    get
    {
      this.Used = true;
      return this._contents;
    }
    private set => this._contents = value;
  }

  public ILanguage LanguageSpecification
  {
    get
    {
      this.Used = true;
      return this._languageSpecification;
    }
    private set => this._languageSpecification = value;
  }
}
