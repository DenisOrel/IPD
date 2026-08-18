// Decompiled with JetBrains decompiler
// Type: Intermech.ConnectionBroker.AppServersConfigSection
// Assembly: Intermech.ConnectionBroker.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0BC7C3AD-D0E0-4C57-9DE7-799988ABDB14
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.ConnectionBroker.Server.dll

using System.Configuration;

#nullable disable
namespace Intermech.ConnectionBroker;

public class AppServersConfigSection : ConfigurationSection
{
  [ConfigurationProperty("Servers")]
  public ServersCollection ServerItems => (ServersCollection) this["Servers"];
}
