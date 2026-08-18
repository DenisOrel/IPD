
// Type: IMClient.RemovablePluginView




using Intermech.Interfaces.Plugins;
using Intermech.Search;
using System.ComponentModel;


namespace IMClient
{
    internal sealed class RemovablePluginView
    {
      private PluginManager _manager;
      private IPlugin _plugin;

      public RemovablePluginView(PluginManager manager, IPlugin plugin)
      {
        this._manager = manager;
        this._plugin = plugin;
      }

      [TypeConverter(typeof (YesNoBooleanConverter))]
      [CustomDisplayName("Attribute.Server_5")]
      [CustomDescription("Attribute.Server_6")]
      public bool AutoReload
      {
        get => this._plugin.AutoReload;
        set
        {
          if (this._manager == null)
            return;
          this._manager.LoadNextTime(this._plugin, value);
        }
      }

      [CustomDescription("Attribute.Server_2")]
      [CustomDisplayName("Attribute.Server_1")]
      public string Location => this._plugin.Location;

      [CustomDescription("Attribute.Server_4")]
      [CustomDisplayName("Attribute.Server_3")]
      public string Name => this._plugin.Name;
    }
}
