
// Type: Intermech.Search.Configuration.ConfigurationOptionChangedEventArgs
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Search.Configuration
{
    public sealed class ConfigurationOptionChangedEventArgs : EventArgs
    {
      public ConfigurationOptionChangedEventArgs(ConfigurationOptionKey optionKey, object newValue)
      {
        this.OptionKey = !(optionKey == (ConfigurationOptionKey) null) ? optionKey : throw new ArgumentNullException(nameof (optionKey));
        this.NewValue = newValue;
      }

      public ConfigurationOptionKey OptionKey { get; private set; }

      public object NewValue { get; private set; }
    }
}
