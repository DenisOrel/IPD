// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.IpsXml.Converter.TechcardToIps.App.IpsConnector
// Assembly: Intermech.XmlExchange.IpsXml.Converter.TechcardToIps.App, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EB4A0A0B-E62B-4D21-A944-3B5D877E45CE
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.IpsXml.Converter.TechcardToIps.App.exe

using Intermech.Client.Specialized;

#nullable disable
namespace Intermech.XmlExchange.IpsXml.Converter.TechcardToIps.App;

public static class IpsConnector
{
  public static ClientApplicationLifecycleHandler Connect(
    string loginName,
    string password,
    string userRole)
  {
    ClientApplicationLifecycleHandler lifecycleHandler = new ClientApplicationLifecycleHandler((IClientApplicationHost) new ConverterClientApplicationHost(loginName, password, userRole));
    lifecycleHandler.Initialize();
    return lifecycleHandler;
  }
}
