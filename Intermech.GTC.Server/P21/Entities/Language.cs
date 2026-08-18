// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.Language
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using Intermech.GTC.Interfaces.Entities;
using System;

#nullable disable
namespace Intermech.GTC.Server.P21.Entities;

public class Language(string keyStr, string paramsStr) : BaseObject(keyStr, paramsStr), ILanguage, IBaseObject
{
  public static string EntityName = "LANGUAGE";
  private string _countryCode;
  private string _languageCode;

  public override void SetParams(IEntityObjects entityObjects)
  {
    this.CountryCode = this.ParamsArr.Length == 2 ? this.ParamsArr[0] : throw new Exception("Неверное количество параметров " + this.ParamStr);
    this.LanguageCode = this.ParamsArr[1];
  }

  public string CountryCode
  {
    get
    {
      this.Used = true;
      return this._countryCode;
    }
    private set => this._countryCode = value;
  }

  public string LanguageCode
  {
    get
    {
      this.Used = true;
      return this._languageCode;
    }
    private set => this._languageCode = value;
  }
}
