// Decompiled with JetBrains decompiler
// Type: Intermech.ConnectionBroker.ServersCollection
// Assembly: Intermech.ConnectionBroker.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0BC7C3AD-D0E0-4C57-9DE7-799988ABDB14
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.ConnectionBroker.Server.dll

using System.Configuration;

#nullable disable
namespace Intermech.ConnectionBroker;

[ConfigurationCollection(typeof (ServerElement))]
public class ServersCollection : ConfigurationElementCollection
{
  protected override ConfigurationElement CreateNewElement()
  {
    return (ConfigurationElement) new ServerElement();
  }

  protected override object GetElementKey(ConfigurationElement element)
  {
    return (object) ((ServerElement) element).ServerName;
  }

  public ServerElement this[int idx] => (ServerElement) this.BaseGet(idx);
}
