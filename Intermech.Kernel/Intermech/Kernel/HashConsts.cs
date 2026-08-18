// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.HashConsts
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;


namespace Intermech.Kernel;

public class HashConsts
{
  private static bool compatible;

  public static bool Compatible => HashConsts.compatible;

  public static void Init(IUserSession session)
  {
    HashConsts.compatible = session.Configurations.ReadBool("KERNEL", "SIGNS", "COMPATIBLE", false, DBConfigMode.GlobalOnly);
  }
}
