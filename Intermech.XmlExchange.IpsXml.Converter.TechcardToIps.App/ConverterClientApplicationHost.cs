// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.IpsXml.Converter.TechcardToIps.App.ConverterClientApplicationHost
// Assembly: Intermech.XmlExchange.IpsXml.Converter.TechcardToIps.App, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EB4A0A0B-E62B-4D21-A944-3B5D877E45CE
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.IpsXml.Converter.TechcardToIps.App.exe

using Intermech.Client.Specialized;
using System;

#nullable disable
namespace Intermech.XmlExchange.IpsXml.Converter.TechcardToIps.App;

internal class ConverterClientApplicationHost : IClientApplicationHost
{
  private SimpleSessionPoolLoginInfo _loginInfo = new SimpleSessionPoolLoginInfo();

  public ConverterClientApplicationHost(string loginName, string password, string roleName)
  {
    this._loginInfo.LoginName = loginName;
    this._loginInfo.Password = password;
    this._loginInfo.RoleName = roleName;
  }

  public Func<SimpleSessionPoolLoginInfo> LoginInfoProvider
  {
    get => (Func<SimpleSessionPoolLoginInfo>) (() => this._loginInfo);
  }
}
