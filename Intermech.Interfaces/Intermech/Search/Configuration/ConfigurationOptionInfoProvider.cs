
// Type: Intermech.Search.Configuration.ConfigurationOptionInfoProvider
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Search.Configuration
{
    public sealed class ConfigurationOptionInfoProvider : IConfigurationOptionInfoProvider
    {
      private Dictionary<ConfigurationOptionKey, ConfigurationOptionInfo> _optionInfoDictionary = new Dictionary<ConfigurationOptionKey, ConfigurationOptionInfo>();

      public void Register(ConfigurationOptionInfo optionInfo)
      {
        if (optionInfo == null)
          throw new ArgumentNullException(nameof (optionInfo));
        if (this._optionInfoDictionary.ContainsKey(optionInfo.Key))
          throw new ArgumentException();
        this._optionInfoDictionary.Add(optionInfo.Key, optionInfo);
      }

      public void Register(List<ConfigurationOptionInfo> optionsInfo)
      {
        if (optionsInfo == null)
          throw new ArgumentNullException(nameof (optionsInfo));
        foreach (ConfigurationOptionInfo optionInfo in optionsInfo)
          this.Register(optionInfo);
      }

      public ConfigurationOptionInfo Get(ConfigurationOptionKey optionKey)
      {
        if (optionKey == (ConfigurationOptionKey) null)
          throw new ArgumentNullException(nameof (optionKey));
        return this._optionInfoDictionary.ContainsKey(optionKey) ? this._optionInfoDictionary[optionKey] : throw new ArgumentException();
      }

      public List<ConfigurationOptionInfo> GetOptionsInfo()
      {
        return this._optionInfoDictionary.Values.ToList<ConfigurationOptionInfo>();
      }

      public void RegisterEditor(ConfigurationOptionKey optionKey, Type editor)
      {
        if (optionKey == (ConfigurationOptionKey) null)
          throw new ArgumentNullException(nameof (optionKey));
        if (!this._optionInfoDictionary.ContainsKey(optionKey))
          throw new ArgumentException();
        this.Get(optionKey).Editor = !(editor == (Type) null) ? editor : throw new ArgumentNullException(nameof (editor));
      }
    }
}
