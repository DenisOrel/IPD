
// Type: Intermech.Search.Configuration.IConfigurationOptionRepository
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Search.Configuration
{
    public interface IConfigurationOptionRepository
    {
      void AddOrUpdate(ConfigurationOptionKey optionKey, object optionValue, DBConfigMode? mode = null);

      object Find(ConfigurationOptionKey optionKey, DBConfigMode? mode = null);

      event EventHandler<ConfigurationOptionChangedEventArgs> OptionChanged;
    }
}
