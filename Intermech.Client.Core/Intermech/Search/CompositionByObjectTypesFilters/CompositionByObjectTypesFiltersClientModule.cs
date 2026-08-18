
// Type: Intermech.Search.CompositionByObjectTypesFilters.CompositionByObjectTypesFiltersClientModule
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;


namespace Intermech.Search.CompositionByObjectTypesFilters;

public sealed class CompositionByObjectTypesFiltersClientModule
{
  private IFactory _factory;
  private CompositionByObjectTypesFiltersModule _module = new CompositionByObjectTypesFiltersModule();

  public CompositionByObjectTypesFiltersClientModule(IFactory factory)
  {
    this._factory = factory != null ? factory : throw new ArgumentNullException(nameof (factory));
  }

  public void Load()
  {
    this._module.Load();
    CompositionByObjectTypesFiltersClientService service = new CompositionByObjectTypesFiltersClientService(ServiceLocator.Get<ICompositionByObjectTypesFilterXmlConverter>());
    ServiceLocator.Register<ICompositionByObjectTypesFiltersClientService>((ICompositionByObjectTypesFiltersClientService) service);
    this._factory.AddViewsProvider((IViewsProvider) new CompositionByObjectTypesFiltersViewsProvider());
    service.ConvertFiltersFromUserConfigurationFileToObjects();
  }

  public void Unload()
  {
    this._module.Unload();
    ServiceLocator.Unregister<ICompositionByObjectTypesFiltersClientService>();
  }
}
