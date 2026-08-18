// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Discussions.DiscussionsServerModule
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces.Server;
using System;


namespace Intermech.Search.Discussions;

public sealed class DiscussionsServerModule
{
  private ICustomServices _customServices;

  public DiscussionsServerModule(IServiceProvider serviceProvider)
  {
    this._customServices = serviceProvider != null ? (ICustomServices) serviceProvider.GetService(typeof (ICustomServices)) : throw new ArgumentNullException(nameof (serviceProvider));
  }

  public void Load()
  {
    this._customServices.AddService(typeof (IDiscussionsRemoteFacadeServerService), (object) new StandardDiscussionsRemoteFacadeServerService((IDiscussionsRemoteFacade) new StandardDiscussionsRemoteFacade()));
  }
}
