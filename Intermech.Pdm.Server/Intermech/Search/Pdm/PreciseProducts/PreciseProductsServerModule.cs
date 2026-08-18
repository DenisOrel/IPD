// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.PreciseProducts.PreciseProductsServerModule
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using Intermech.Interfaces.Server;

#nullable disable
namespace Intermech.Search.Pdm.PreciseProducts;

internal sealed class PreciseProductsServerModule
{
  public void Load()
  {
    if (!(ServerServices.GetService(typeof (ICustomServices)) is ICustomServices service))
      return;
    service.AddService(typeof (IPreciseProductsServerService), (object) new PreciseProductsServerService());
  }

  public void Unload()
  {
    if (!(ServerServices.GetService(typeof (ICustomServices)) is ICustomServices service))
      return;
    service.RemoveService(typeof (IPreciseProductsServerService));
  }
}
