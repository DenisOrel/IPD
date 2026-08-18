// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.ClientSessionSpeedupServicesNinjectModule
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Ninject;
using Ninject.Activation;
using Ninject.Modules;
using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Модуль привязок для IOC-контейнера, обеспечивающий создание ускоряющий клиентских сервисов.
/// </summary>
public sealed class ClientSessionSpeedupServicesNinjectModule : NinjectModule
{
  public override void Load()
  {
    this.Bind<IDBConfigurations, IDBConfigurationsSpeedupService>().To<DBConfigurationsSpeedupService>().InSingletonScope();
    this.Bind<SessionPoolOptionalServices>().ToMethod(new Func<IContext, SessionPoolOptionalServices>(this.CreateSessionPoolOptionalServices));
    this.Bind<UserSessionSpeedupServices>().ToMethod(new Func<IContext, UserSessionSpeedupServices>(this.CreateSessionPoolSpeedupServices));
  }

  private SessionPoolOptionalServices CreateSessionPoolOptionalServices(IContext context)
  {
    return new SessionPoolOptionalServices();
  }

  private UserSessionSpeedupServices CreateSessionPoolSpeedupServices(IContext context)
  {
    return new UserSessionSpeedupServices()
    {
      CustomServices = context.Kernel.TryGet<ICustomServicesSpeedupService>(),
      DBConfigurations = context.Kernel.TryGet<IDBConfigurationsSpeedupService>(),
      ClientMetadataCache = context.Kernel.TryGet<IClientMetadataCache>()
    };
  }
}
