// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.MultiLanguageString
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

public class MultiLanguageString(string keyStr, string paramsStr) : 
  BaseObject(keyStr, paramsStr),
  IMultiLanguageString,
  IBaseObject
{
  public static string EntityName = "MULTI_LANGUAGE_STRING";
  private IStringWithLanguage _primaryLanguageString;
  private IStringWithLanguage[] _additionalLanguageString;

  public override void SetParams(IEntityObjects entityObjects)
  {
    if (this.ParamsArr.Length != 2)
      throw new Exception("Неверное количество параметров " + this.ParamStr);
    this.AdditionalLanguageString = ((IEnumerable<string>) this.ParamsArr[0].Split(',')).Select<string, IStringWithLanguage>((Func<string, IStringWithLanguage>) (addLangStr => (IStringWithLanguage) entityObjects.Get(addLangStr.Trim()))).ToArray<IStringWithLanguage>();
    this.PrimaryLanguageString = (IStringWithLanguage) entityObjects.Get(this.ParamsArr[1]);
  }

  public IStringWithLanguage PrimaryLanguageString
  {
    get
    {
      this.Used = true;
      return this._primaryLanguageString;
    }
    private set => this._primaryLanguageString = value;
  }

  public IStringWithLanguage[] AdditionalLanguageString
  {
    get
    {
      this.Used = true;
      return this._additionalLanguageString;
    }
    private set => this._additionalLanguageString = value;
  }
}
