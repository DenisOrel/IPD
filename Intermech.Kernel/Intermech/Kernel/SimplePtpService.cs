// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.SimplePtpService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Net;
using System;


namespace Intermech.Kernel;

public sealed class SimplePtpService : LongLifeObject, ISimplePtpServer
{
  private readonly SimplePtpServer serverImpl;

  internal SimplePtpService() => this.serverImpl = new SimplePtpServer();

  public SimplePtpDelayResponse DelayRequest(DateTime t1) => this.serverImpl.DelayRequest(t1);

  public static void Install()
  {
    ICustomServices service = ServiceUtils.GetService<ICustomServices>((object) ServerServices.ServiceContainer, true);
    SimplePtpService serviceInstance1 = new SimplePtpService();
    ServerServices.AddService(typeof (ISimplePtpServer), (object) serviceInstance1);
    Type serviceType = typeof (ISimplePtpServer);
    SimplePtpService serviceInstance2 = serviceInstance1;
    service.AddService(serviceType, (object) serviceInstance2);
  }
}
