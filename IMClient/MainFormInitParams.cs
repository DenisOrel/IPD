
// Type: IMClient.MainFormInitParams




using Intermech.ApplicationModel;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Plugins;
using Ninject;
using System;


namespace IMClient
{
    internal sealed class MainFormInitParams
    {
      [Inject]
      public ISharedLibraryInitializerService SharedLibraryInitializer { get; set; }

      [Inject]
      public IMServerService IMServerService { get; set; }

      [Inject]
      public Func<SessionPluginsLoader> CreateSessionPluginLoader { get; set; }

      [Inject]
      public Func<PersonalPluginsLoader> CreatePersonalPluginLoader { get; set; }

      [Optional]
      [Inject]
      public Action<PluginManager> PluginManagerConfigureAction { get; set; }
    }
}
