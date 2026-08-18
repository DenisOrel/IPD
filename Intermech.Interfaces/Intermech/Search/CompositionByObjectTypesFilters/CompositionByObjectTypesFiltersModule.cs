
// Type: Intermech.Search.CompositionByObjectTypesFilters.CompositionByObjectTypesFiltersModule
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Search.CompositionByObjectTypesFilters
{
    public sealed class CompositionByObjectTypesFiltersModule
    {
      public void Load()
      {
        ServiceLocator.Register<ICompositionByObjectTypesFilterXmlConverter>((ICompositionByObjectTypesFilterXmlConverter) new CompositionByObjectTypesFilterXmlConverter());
      }

      public void Unload() => ServiceLocator.Unregister<ICompositionByObjectTypesFilterXmlConverter>();
    }
}
