// Decompiled with JetBrains decompiler
// Type: Intermech.Search.ContextMenus.ContextMenuServerModule
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces.Server;


namespace Intermech.Search.ContextMenus;

public sealed class ContextMenuServerModule
{
  public void Load()
  {
    ServiceLocator.Get<ICustomServices>().AddService(typeof (IContextMenuServerService), (object) new ContextMenuServerService());
  }

  public void Unload()
  {
    ServiceLocator.Get<ICustomServices>().RemoveService(typeof (IContextMenuServerService));
  }
}
