// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.ServerServices
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using System;
using System.ComponentModel.Design;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Server;

public sealed class ServerServices
{
  private static readonly IServiceContainer _services = (IServiceContainer) ApplicationServices.Container;

  public static void RemoveService(Type serviceType)
  {
    ServerServices._services.RemoveService(serviceType);
  }

  public static void AddService(Type serviceType, ServiceCreatorCallback callback)
  {
    ServerServices._services.AddService(serviceType, callback);
  }

  public static void AddService(Type serviceType, object serviceInstance)
  {
    ServerServices._services.AddService(serviceType, serviceInstance);
  }

  public static object GetService(Type serviceType)
  {
    return ServerServices._services.GetService(serviceType);
  }

  public static IServiceContainer ServiceContainer
  {
    [DebuggerStepThrough] get => ServerServices._services;
  }
}
