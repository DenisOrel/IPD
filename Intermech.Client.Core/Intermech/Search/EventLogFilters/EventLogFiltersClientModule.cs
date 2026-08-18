
// Type: Intermech.Search.EventLogFilters.EventLogFiltersClientModule
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Search.EventLogFilters;

public sealed class EventLogFiltersClientModule
{
  public void Load()
  {
    ServiceLocator.Register<IEventLogFiltersClientService>((IEventLogFiltersClientService) new EventLogFiltersClientService());
  }

  public void Unload() => ServiceLocator.Unregister<IEventLogFiltersClientService>();
}
