// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.StandaloneView.Module
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.ApplicationModel;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.StandaloneView;


namespace Intermech.Kernel.Services.StandaloneView;

public sealed class Module : InitializerModule
{
  private ICustomServices customServices;
  private StandaloneViewServerService settingsService;

  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.customServices = ServiceUtils.GetService<ICustomServices>((object) ServerServices.ServiceContainer, true);
    this.RegisterSettingsService();
  }

  private void RegisterSettingsService()
  {
    this.settingsService = new StandaloneViewServerService();
    ServerServices.AddService(typeof (IStandaloneViewServerService), (object) this.settingsService);
    this.customServices.AddService(typeof (IStandaloneViewServerService), (object) this.settingsService);
  }

  protected override void DoShutdown()
  {
    base.DoShutdown();
    this.UnregisterSettingsService();
    this.customServices = (ICustomServices) null;
  }

  private void UnregisterSettingsService()
  {
    if (this.settingsService == null)
      return;
    ServerServices.RemoveService(typeof (IStandaloneViewServerService));
    this.customServices.RemoveService(typeof (IStandaloneViewServerService));
    this.settingsService = (StandaloneViewServerService) null;
  }
}
