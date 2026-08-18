// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.CustomServices
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.ApplicationModel;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using System;
using System.ComponentModel.Design;


namespace Intermech.Kernel.Services;

public class CustomServices : ICustomServices
{
  private ApplicationServiceContainer container;

  public CustomServices() => this.container = new ApplicationServiceContainer();

  public void AddService(Type serviceType, object serviceInstance)
  {
    this.container.AddService(serviceType, serviceInstance);
  }

  public void AddService(Type serviceType, ServiceCreatorCallback callback)
  {
    this.container.AddService(serviceType, callback);
  }

  public void RemoveService(Type serviceType) => this.container.RemoveService(serviceType);

  public object GetService(Type serviceType)
  {
    object service1 = this.container.GetService(serviceType);
    if (service1 != null)
      return service1;
    object service2 = ApplicationServices.Container.GetService(serviceType);
    return service2 != null && service2 is MarshalByRefObject && this.IsClientVisible(service2) ? service2 : (object) null;
  }

  private bool IsClientVisible(object serverService)
  {
    object[] customAttributes = serverService.GetType().GetCustomAttributes(typeof (ServerServiceAttribute), false);
    return customAttributes.Length != 0 && ((ServerServiceAttribute) customAttributes[0]).ClientVisible;
  }
}
