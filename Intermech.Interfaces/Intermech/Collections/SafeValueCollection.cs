
// Type: Intermech.Collections.SafeValueCollection
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.Collections.Generic;


namespace Intermech.Collections
{
    [Obsolete("Use the class Intermech.Interfaces.Data.PropertyContainer instead of this.", true)]
    public sealed class SafeValueCollection
    {
      private Dictionary<string, object> values = new Dictionary<string, object>();

      public void Set(string name, object value)
      {
        SafeValueCollection.CheckPropName(name);
        SafeValueCollection.CheckPropValue(name, value);
        this.values[name] = value;
      }

      public bool Contains(string name)
      {
        SafeValueCollection.CheckPropName(name);
        return this.values.ContainsKey(name);
      }

      public bool Contains<T>(string name) where T : class
      {
        SafeValueCollection.CheckPropName(name);
        return (object) this.Get<T>(name, default (T)) != null;
      }

      public bool Contains<T>(string name, T value)
      {
        SafeValueCollection.CheckPropName(name);
        SafeValueCollection.CheckPropValue(name, (object) value);
        return object.Equals((object) this.Get<T>(name, default (T)), (object) value);
      }

      public T Get<T>(string name)
      {
        SafeValueCollection.CheckPropName(name);
        object obj;
        if (this.values.TryGetValue(name, out obj))
          return this.ChangeType<T>(obj);
        throw new Exception(string.Format(LocalizationHolder.rm.GetString("Interfaces_751"), (object) name));
      }

      public T Get<T>(string name, T defaultValue)
      {
        SafeValueCollection.CheckPropName(name);
        object obj;
        return this.values.TryGetValue(name, out obj) ? this.ChangeType<T>(obj) : defaultValue;
      }

      private T ChangeType<T>(object value)
      {
        if (typeof (T) == typeof (object))
          return (T) value;
        if (value.GetType() == typeof (T))
          return (T) value;
        return value is IConvertible ? (T) Convert.ChangeType(value, typeof (T)) : throw new InvalidCastException(string.Format(LocalizationHolder.rm.GetString("Interfaces_752"), value, (object) typeof (T)));
      }

      public T Retrieve<T>(string name)
      {
        SafeValueCollection.CheckPropName(name);
        T obj = this.Get<T>(name);
        this.Remove(name);
        return obj;
      }

      public T Retrieve<T>(string name, T defaultValue)
      {
        SafeValueCollection.CheckPropName(name);
        T obj = this.Get<T>(name, defaultValue);
        this.Remove(name);
        return obj;
      }

      public void Remove(string name)
      {
        SafeValueCollection.CheckPropName(name);
        this.values.Remove(name);
      }

      private static void CheckPropName(string name)
      {
        if (string.IsNullOrEmpty(name))
          throw new ArgumentException(LocalizationHolder.rm.GetString("Interfaces_753"), nameof (name));
      }

      private static void CheckPropValue(string name, object value)
      {
        if (value == null)
          throw new ArgumentNullException(nameof (value), string.Format(LocalizationHolder.rm.GetString("Interfaces_754"), (object) name));
      }
    }
}
