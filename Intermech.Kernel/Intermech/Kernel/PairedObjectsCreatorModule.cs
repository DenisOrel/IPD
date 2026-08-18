// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.PairedObjectsCreatorModule
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.ApplicationModel;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;


namespace Intermech.Kernel;

public sealed class PairedObjectsCreatorModule : InitializerModule
{
  private PairedObjectsCreatorService pairedObjectsService;

  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.pairedObjectsService = new PairedObjectsCreatorService(ServiceUtils.GetService<IEventLogHelper>((object) ServerServices.ServiceContainer, true));
    ServerServices.AddService(typeof (IPairedObjectsCreatorService), (object) this.pairedObjectsService);
  }

  protected override void DoShutdown()
  {
    base.DoShutdown();
    if (this.pairedObjectsService == null)
      return;
    ServerServices.RemoveService(typeof (IPairedObjectsCreatorService));
    this.pairedObjectsService = (PairedObjectsCreatorService) null;
  }
}
