
// Type: Intermech.Interfaces.Configuration.ConfigurationCollection
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections;


namespace Intermech.Interfaces.Configuration
{
    internal class ConfigurationCollection : IConfigurationCollection, ICollection, IEnumerable
    {
      private ArrayList _configurations;

      public ConfigurationCollection() => this._configurations = new ArrayList();

      public void Add(IConfiguration configuration) => this._configurations.Add((object) configuration);

      public void Clear() => this._configurations.Clear();

      public void CopyTo(Array array, int index) => this._configurations.CopyTo(array, index);

      public IEnumerator GetEnumerator() => this._configurations.GetEnumerator();

      public void Remove(IConfiguration configuration)
      {
        this._configurations.Remove((object) configuration);
      }

      public void RemoveAt(int index) => this._configurations.RemoveAt(index);

      public int Count => this._configurations.Count;

      public bool IsSynchronized => false;

      public IConfiguration this[int index] => (IConfiguration) this._configurations[index];

      public IConfiguration this[string name]
      {
        get
        {
          foreach (IConfiguration configuration in this._configurations)
          {
            if (string.Compare(configuration.Name, name) == 0)
              return configuration;
          }
          return (IConfiguration) null;
        }
      }

      public object SyncRoot => (object) null;
    }
}
