// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.CustomServerSynchronizer
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using System;


namespace Intermech.Kernel.Services;

public abstract class CustomServerSynchronizer : IServerSynchronizer
{
  public CustomServerSynchronizer(Guid serviceGuid, string serviceName)
  {
    if (serviceName == null)
      throw new ArgumentNullException(nameof (serviceName));
    this.ServiceGUID = serviceGuid;
    this.ServiceName = serviceName;
  }

  public Guid ServiceGUID { get; private set; }

  public string ServiceName { get; private set; }

  public IServerSynchronizersManager Manager { get; set; }

  protected bool IsRegistered => this.Manager != null;

  public abstract void ExecuteEvent(SynchonizerEventProperties eventProps, IUserSession session);

  protected SynchonizerEventProperties GetEventProps(string strInfo)
  {
    return new SynchonizerEventProperties(string.Empty, this.ServiceGUID, strInfo, true);
  }
}
