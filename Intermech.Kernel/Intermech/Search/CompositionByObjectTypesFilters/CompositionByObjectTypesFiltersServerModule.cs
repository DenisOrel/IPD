// Decompiled with JetBrains decompiler
// Type: Intermech.Search.CompositionByObjectTypesFilters.CompositionByObjectTypesFiltersServerModule
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces.Server;
using System;


namespace Intermech.Search.CompositionByObjectTypesFilters;

public sealed class CompositionByObjectTypesFiltersServerModule
{
  private ICustomServices _customServices;
  private CompositionByObjectTypesFiltersModule _module = new CompositionByObjectTypesFiltersModule();

  public CompositionByObjectTypesFiltersServerModule(ICustomServices customServices)
  {
    this._customServices = customServices != null ? customServices : throw new ArgumentNullException(nameof (customServices));
  }

  public void Load()
  {
    this._module.Load();
    this._customServices.AddService(typeof (ICompositionByObjectTypesFiltersServerService), (object) new CompositionByObjectTypesFiltersServerService(ServiceLocator.Get<ICompositionByObjectTypesFilterXmlConverter>()));
  }

  public void Unload()
  {
    this._module.Unload();
    this._customServices.RemoveService(typeof (ICompositionByObjectTypesFiltersServerService));
  }
}
