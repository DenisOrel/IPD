// Decompiled with JetBrains decompiler
// Type: Intermech.Search.GroupAttributesChanging.GroupAttributesChangingServerModule
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces.Server;
using System;


namespace Intermech.Search.GroupAttributesChanging;

public sealed class GroupAttributesChangingServerModule
{
  private ICustomServices _customServices;

  public GroupAttributesChangingServerModule(ICustomServices customServices)
  {
    this._customServices = customServices != null ? customServices : throw new ArgumentNullException(nameof (customServices));
  }

  public void Load()
  {
    this._customServices.AddService(typeof (IGroupAttributesChangingServerService), (object) new GroupAttributesChangingServerService());
  }

  public void Unload()
  {
    this._customServices.RemoveService(typeof (IGroupAttributesChangingServerService));
  }
}
