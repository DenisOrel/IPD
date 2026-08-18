// Decompiled with JetBrains decompiler
// Type: Intermech.Search.PasswordChange.PasswordChangeServerModule
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using System;


namespace Intermech.Search.PasswordChange;

public sealed class PasswordChangeServerModule
{
  private readonly ICustomServices _customServices;
  private readonly IMServer _server;

  public PasswordChangeServerModule(IServiceProvider serviceProvider)
  {
    this._customServices = serviceProvider != null ? (ICustomServices) serviceProvider.GetService(typeof (ICustomServices)) : throw new ArgumentNullException(nameof (serviceProvider));
    this._server = (IMServer) serviceProvider.GetService(typeof (IMServer));
  }

  public void Load()
  {
    this._customServices.AddService(typeof (IPasswordChangeRemoteFacadeServerService), (object) new StandardPasswordChangeRemoteFacadeServerService((IPasswordChangeRemoteFacade) new StandardPasswordChangeRemoteFacade(this._server)));
  }
}
