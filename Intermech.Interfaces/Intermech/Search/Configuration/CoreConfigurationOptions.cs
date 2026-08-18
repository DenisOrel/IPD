
// Type: Intermech.Search.Configuration.CoreConfigurationOptions
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Search.Configuration
{
    public static class CoreConfigurationOptions
    {
      private static LazyService<IConfigurationOptionRepository> _configurationOptionRepository = new LazyService<IConfigurationOptionRepository>();

      public static bool UI_MinimizeContextMenu
      {
        get
        {
          return (bool) ServiceLocator.Get<IConfigurationOptionRepository>().Find(ConfigurationOptionKeys.UI_MinimizeContextMenu);
        }
        set
        {
          ServiceLocator.Get<IConfigurationOptionRepository>().AddOrUpdate(ConfigurationOptionKeys.UI_MinimizeContextMenu, (object) value);
        }
      }

      public static bool UI_OpenNearMode
      {
        get
        {
          return (bool) ServiceLocator.Get<IConfigurationOptionRepository>().Find(ConfigurationOptionKeys.UI_OpenNearMode);
        }
        set
        {
          ServiceLocator.Get<IConfigurationOptionRepository>().AddOrUpdate(ConfigurationOptionKeys.UI_OpenNearMode, (object) value);
        }
      }

      public static long UI_MinimizedContextMenuCommandsCount
      {
        get
        {
          return (long) ServiceLocator.Get<IConfigurationOptionRepository>().Find(ConfigurationOptionKeys.UI_MinimizedContextMenuCommandsCount);
        }
      }

      public static bool UI_UseSearchSelectionMode
      {
        get
        {
          return (bool) CoreConfigurationOptions._configurationOptionRepository.Value.Find(ConfigurationOptionKeys.UI_UseSearchSelectionMode);
        }
        set
        {
          CoreConfigurationOptions._configurationOptionRepository.Value.AddOrUpdate(ConfigurationOptionKeys.UI_UseSearchSelectionMode, (object) value);
        }
      }
    }
}
