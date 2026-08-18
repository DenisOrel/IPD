
// Type: Intermech.Interfaces.Configuration.Configuration
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;


namespace Intermech.Interfaces.Configuration
{
    [DebuggerDisplay("Name = {Name}")]
    internal class Configuration : IConfiguration
    {
      private ConfigurationCollection _configurations;
      private string _name;
      private ArrayList _properties;

      public event PropertyChangedEventHandler PropertyChanged;

      public Configuration(string name)
      {
        this._properties = new ArrayList();
        this._configurations = new ConfigurationCollection();
        this._name = name;
      }

      public IConfiguration Add(string name)
      {
        IConfiguration configuration = (IConfiguration) new Intermech.Interfaces.Configuration.Configuration(name);
        this._configurations.Add(configuration);
        return configuration;
      }

      public void Clear()
      {
        this._configurations.Clear();
        this._properties.Clear();
      }

      public void RemoveProperty(string name)
      {
        this._properties.Remove((object) this.GetPropertyItem(name));
        this.OnPropertyChanged(new PropertyChangedEventArgs(name));
      }

      public string GetProperty(string name)
      {
        IConfigurationProperty propertyItem = this.GetPropertyItem(name);
        return propertyItem == null ? string.Empty : propertyItem.Value;
      }

      private IConfigurationProperty GetPropertyItem(string name)
      {
        foreach (IConfigurationProperty property in this._properties)
        {
          if (property.Name == name)
            return property;
        }
        return (IConfigurationProperty) null;
      }

      public bool HasProperty(string name) => this.GetPropertyItem(name) != null;

      protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
      {
        if (this.PropertyChanged == null)
          return;
        this.PropertyChanged((object) this, e);
      }

      public void Remove(IConfiguration configuration) => this._configurations.Remove(configuration);

      public IConfiguration[] Select(string name)
      {
        ArrayList arrayList = new ArrayList();
        foreach (IConfiguration configuration in this._configurations)
        {
          if (name == configuration.Name)
            arrayList.Add((object) configuration);
        }
        IConfiguration[] configurationArray = new IConfiguration[arrayList.Count];
        arrayList.CopyTo((Array) configurationArray, 0);
        return configurationArray;
      }

      public IConfiguration Open(string name)
      {
        foreach (IConfiguration configuration in this._configurations)
        {
          if (name == configuration.Name)
            return configuration;
        }
        return (IConfiguration) null;
      }

      public int Count => this._configurations.Count;

      public void SetProperty(string name, string value)
      {
        IConfigurationProperty propertyItem = this.GetPropertyItem(name);
        if (propertyItem != null && !(propertyItem.Value != value))
          return;
        if (propertyItem != null)
          this._properties.Remove((object) propertyItem);
        this._properties.Add((object) new Intermech.Interfaces.Configuration.Configuration.ConfigurationProperty(name, value));
        this.OnPropertyChanged(new PropertyChangedEventArgs(name));
      }

      public IConfigurationCollection Configurations => (IConfigurationCollection) this._configurations;

      public string Name => this._name;

      public IEnumerable Properties => (IEnumerable) this._properties;

      [DebuggerDisplay("Name = {Name}, Value = {Value}")]
      private class ConfigurationProperty : IConfigurationProperty
      {
        private string _name;
        private string _value;

        public ConfigurationProperty(string propertyName, string propertyValue)
        {
          this._name = propertyName;
          this._value = propertyValue;
        }

        public string Name => this._name;

        public string Value => this._value;
      }
    }
}
