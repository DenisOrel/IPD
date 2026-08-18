// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Mbom.MbomServerModule
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using Intermech.Interfaces.Server;

#nullable disable
namespace Intermech.Search.Mbom;

public sealed class MbomServerModule
{
  public void Load()
  {
    ServiceLocator.Get<ICustomServices>().AddService(typeof (IMbomServerService), (object) new MbomServerService());
  }

  public void Unload()
  {
    ServiceLocator.Get<ICustomServices>().RemoveService(typeof (IMbomServerService));
  }
}
