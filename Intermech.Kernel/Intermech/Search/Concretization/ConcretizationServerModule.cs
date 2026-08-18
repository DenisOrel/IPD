// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Concretization.ConcretizationServerModule
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces.Server;


namespace Intermech.Search.Concretization;

public sealed class ConcretizationServerModule
{
  public void Load()
  {
    ServiceLocator.Get<ICustomServices>().AddService(typeof (IConcretizationServerService), (object) new ConcretizationServerService());
  }

  public void Unload()
  {
    ServiceLocator.Get<ICustomServices>().RemoveService(typeof (IConcretizationServerService));
  }
}
