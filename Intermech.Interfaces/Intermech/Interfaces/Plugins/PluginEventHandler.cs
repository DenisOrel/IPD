
// Type: Intermech.Interfaces.Plugins.PluginEventHandler
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces.Plugins
{
    /// <summary>Делегат для событий менеджера плагинов.</summary>
    /// <param name="sender">Источник события. Как правило, это менеджер плагинов</param>
    /// <param name="e">Аргументы события</param>
    public delegate void PluginEventHandler(object sender, PluginEventArgs e);
}
