// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Dictionary.DictionaryServiceHolder
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces.Dictionary;
using Intermech.Interfaces.Server;


namespace Intermech.Kernel.Dictionary;

internal static class DictionaryServiceHolder
{
  public static string SystemSessionName = "Intermech.Kernel.Dictionary";
  public static string DictHeader = "Intermech.Dictionary.";
  public static string DictListHeader = "Intermech.Dictionary";

  public static void RegisterService()
  {
    if (ServerServices.GetService(typeof (IDictionaryServerService)) != null)
      return;
    DictionaryService serviceInstance = new DictionaryService();
    ServerServices.AddService(typeof (IDictionaryServerService), (object) serviceInstance);
    (ServerServices.GetService(typeof (ICustomServices)) as ICustomServices).AddService(typeof (IDictionaryService), (object) serviceInstance);
  }
}
