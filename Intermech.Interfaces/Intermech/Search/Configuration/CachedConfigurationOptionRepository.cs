
// Type: Intermech.Search.Configuration.CachedConfigurationOptionRepository
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Search.Configuration
{
    public sealed class CachedConfigurationOptionRepository : IConfigurationOptionRepository
    {
      private Dictionary<ConfigurationOptionKey, object> _dictionary = new Dictionary<ConfigurationOptionKey, object>();

      public CachedConfigurationOptionRepository(IConfigurationOptionRepository repository)
      {
        this.Repository = repository != null ? repository : throw new ArgumentNullException(nameof (repository));
      }

      public IConfigurationOptionRepository Repository { get; private set; }

      public event EventHandler<ConfigurationOptionChangedEventArgs> OptionChanged;

      public void AddOrUpdate(ConfigurationOptionKey optionKey, object optionValue, DBConfigMode? mode = null)
      {
        if (optionKey == (ConfigurationOptionKey) null)
          throw new ArgumentNullException(nameof (optionKey));
        this.Repository.AddOrUpdate(optionKey, optionValue);
        ConfigurationOptionInfo configurationOptionInfo = ServiceLocator.Get<IConfigurationOptionInfoProvider>().Get(optionKey);
        if (!mode.HasValue)
          mode = new DBConfigMode?(configurationOptionInfo.Mode);
        DBConfigMode? nullable = mode;
        DBConfigMode dbConfigMode = DBConfigMode.UserOnly;
        if (nullable.GetValueOrDefault() == dbConfigMode & nullable.HasValue)
        {
          lock (this._dictionary)
          {
            if (this._dictionary.ContainsKey(optionKey))
              this._dictionary[optionKey] = optionValue;
            else
              this._dictionary.Add(optionKey, optionValue);
          }
        }
        EventHandler<ConfigurationOptionChangedEventArgs> optionChanged = this.OptionChanged;
        if (optionChanged == null)
          return;
        optionChanged((object) this, new ConfigurationOptionChangedEventArgs(optionKey, optionValue));
      }

      public object Find(ConfigurationOptionKey optionKey, DBConfigMode? mode = null)
      {
        ConfigurationOptionInfo configurationOptionInfo = !(optionKey == (ConfigurationOptionKey) null) ? ServiceLocator.Get<IConfigurationOptionInfoProvider>().Get(optionKey) : throw new ArgumentNullException(nameof (optionKey));
        if (!mode.HasValue)
          mode = new DBConfigMode?(configurationOptionInfo.Mode);
        DBConfigMode? nullable = mode;
        DBConfigMode dbConfigMode = DBConfigMode.UserOnly;
        if (!(nullable.GetValueOrDefault() == dbConfigMode & nullable.HasValue))
          return this.Repository.Find(optionKey, mode);
        if (this._dictionary.ContainsKey(optionKey))
          return this._dictionary[optionKey];
        object obj = this.Repository.Find(optionKey, mode);
        this._dictionary.Add(optionKey, obj);
        return obj;
      }
    }
}
