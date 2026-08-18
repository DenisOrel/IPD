
// Type: Intermech.Interfaces.Plugins.PluginEventArgs
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Diagnostics;


namespace Intermech.Interfaces.Plugins
{
    /// <summary>Аргументы для событий менеджера плагинов.</summary>
    public class PluginEventArgs : EventArgs
    {
      private IPlugin _plugin;

      /// <summary>Создает объект.</summary>
      /// <param name="plugin">Объект плагина</param>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="plugin" /> не должен быть равен null</exception>
      public PluginEventArgs(IPlugin plugin)
      {
        this._plugin = plugin != null ? plugin : throw new ArgumentNullException(nameof (plugin));
      }

      /// <summary>Возвращает объект плагина.</summary>
      public IPlugin Plugin
      {
        [DebuggerStepThrough] get => this._plugin;
      }
    }
}
