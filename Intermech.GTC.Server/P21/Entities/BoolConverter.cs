// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.Entities.BoolConverter
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

#nullable disable
namespace Intermech.GTC.Server.P21.Entities;

internal class BoolConverter
{
  public static bool? Convert(string val)
  {
    bool? nullable = new bool?();
    if (val.Equals(".T."))
      nullable = new bool?(true);
    if (val.Equals(".F."))
      nullable = new bool?(false);
    if (val.Equals(".U."))
      nullable = new bool?();
    return nullable;
  }
}
