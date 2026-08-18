// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Factory
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using System;

#nullable disable
namespace Intermech.GTC.Server.P21;

public class Factory
{
  public static BaseObject Create(string typestr, string keyStr, string paramStr)
  {
    return (BaseObject) Activator.CreateInstance(EntityTypesCashe.GetEntityType(typestr), (object) keyStr, (object) paramStr);
  }
}
