
// Type: Intermech.Search.ObjectListFilters.ObjectListFiltersClientModule
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;


namespace Intermech.Search.ObjectListFilters;

public sealed class ObjectListFiltersClientModule
{
  public void Load()
  {
    ServiceLocator.Register<IObjectListFiltersClientService>((IObjectListFiltersClientService) new ObjectListFiltersClientService(ServiceLocator.Get<INotificationService>()));
  }

  public void Unload() => ServiceLocator.Unregister<IObjectListFiltersClientService>();
}
