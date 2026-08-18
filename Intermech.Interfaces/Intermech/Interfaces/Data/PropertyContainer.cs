
// Type: Intermech.Interfaces.Data.PropertyContainer
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.Collections.Generic;


namespace Intermech.Interfaces.Data
{
    [Serializable]
    public sealed class PropertyContainer
    {
      private Dictionary<string, object> valueTable;

      public PropertyContainer() => this.valueTable = new Dictionary<string, object>();

      public bool Contains(string name)
      {
        this.CheckName(name);
        return this.valueTable.ContainsKey(name);
      }

      public T Get<T>(string name) => this.Get<T>(name, default (T));

      public T Get<T>(string name, T defaultValue)
      {
        this.CheckName(name);
        object obj;
        return this.valueTable.TryGetValue(name, out obj) ? (T) obj : defaultValue;
      }

      public void Put<T>(string name, T value)
      {
        this.CheckName(name);
        object obj = (object) value;
        if (obj != null)
          this.valueTable[name] = obj;
        else
          this.valueTable.Remove(name);
      }

      public void Remove(string name)
      {
        this.CheckName(name);
        this.valueTable.Remove(name);
      }

      private void CheckName(string name)
      {
        if (string.IsNullOrEmpty(name))
          throw new ArgumentException(LocalizationHolder.rm.GetString("Interfaces_753"), nameof (name));
      }
    }
}
