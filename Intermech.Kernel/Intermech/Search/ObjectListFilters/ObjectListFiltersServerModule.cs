// Decompiled with JetBrains decompiler
// Type: Intermech.Search.ObjectListFilters.ObjectListFiltersServerModule
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces.Server;
using System;


namespace Intermech.Search.ObjectListFilters;

public sealed class ObjectListFiltersServerModule
{
  private ICustomServices _customServices;

  public ObjectListFiltersServerModule(ICustomServices customServices)
  {
    this._customServices = customServices != null ? customServices : throw new ArgumentNullException(nameof (customServices));
  }

  public void Load()
  {
    this._customServices.AddService(typeof (IObjectListFiltersServerService), (object) new ObjectListFiltersServerService());
  }

  public void Unload()
  {
    this._customServices.RemoveService(typeof (IObjectListFiltersServerService));
  }
}
