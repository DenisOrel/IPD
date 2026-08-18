// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.ServiceHolder
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using System.Reflection;
using System.Resources;

#nullable disable
namespace Intermech.GTC.Server;

internal class ServiceHolder
{
  public static ResourceManager Rm = new ResourceManager("Intermech.GTC.Server.GtcServerResources", Assembly.GetExecutingAssembly());
}
