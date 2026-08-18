// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Kernel.ToolServicesModule
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.ApplicationModel;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using Intermech.Tools.Integrators;
using Intermech.Tools.LaunchActions;


namespace Intermech.Tools.Kernel;

internal sealed class ToolServicesModule : InitializerModule
{
  private ToolSettingsCacheSynchronizer toolSettingsCacheSynchronizer;

  protected override void DoInitialize()
  {
    base.DoInitialize();
    IServerSynchronizersManager service1 = ServiceUtils.GetService<IServerSynchronizersManager>((object) ApplicationServices.Container, true);
    IntegratorSettingsCacheManager service2 = ServiceUtils.GetService<IntegratorSettingsCacheManager>((object) ServerServices.ServiceContainer, true);
    ICustomServices service3 = ServiceUtils.GetService<ICustomServices>((object) ServerServices.ServiceContainer, true);
    UserSession systemSession = this.AllocateSystemSession(nameof (ToolServicesModule));
    try
    {
      this.toolSettingsCacheSynchronizer = new ToolSettingsCacheSynchronizer();
      service1.RegisterSynchronizer((IServerSynchronizer) this.toolSettingsCacheSynchronizer);
      ToolSecurityService toolSecurityService = new ToolSecurityService((IUserSession) systemSession);
      IntegratorService serviceInstance1 = new IntegratorService((IUserSession) systemSession, toolSecurityService, service2, this.toolSettingsCacheSynchronizer);
      LaunchActionService serviceInstance2 = new LaunchActionService((IUserSession) systemSession, toolSecurityService);
      ServerServices.AddService(typeof (IIntegratorServer), (object) serviceInstance1);
      service3.AddService(typeof (IIntegratorServer), (object) serviceInstance1);
      ServerServices.AddService(typeof (ILaunchActionServer), (object) serviceInstance2);
      service3.AddService(typeof (ILaunchActionServer), (object) serviceInstance2);
      ServerServices.AddService(typeof (IToolSecurity), (object) toolSecurityService);
      service3.AddService(typeof (IToolSecurity), (object) toolSecurityService);
    }
    finally
    {
      systemSession.Logout(nameof (ToolServicesModule));
    }
  }

  protected override void DoShutdown()
  {
    ICustomServices service = ServiceUtils.GetService<ICustomServices>((object) ServerServices.ServiceContainer, true);
    ServerServices.RemoveService(typeof (IToolSecurity));
    service.RemoveService(typeof (IToolSecurity));
    service.RemoveService(typeof (ILaunchActionServer));
    ServerServices.RemoveService(typeof (ILaunchActionServer));
    service.RemoveService(typeof (IIntegratorServer));
    ServerServices.RemoveService(typeof (IIntegratorServer));
    base.DoShutdown();
  }

  private UserSession AllocateSystemSession(string sessionName)
  {
    return (UserSession) ServiceUtils.GetService<IDBTimedEvents>((object) ServerServices.ServiceContainer, true).GetSystemSessionPermanentClone(sessionName);
  }
}
