
// Type: Intermech.Interfaces.StringKeyValueIPS
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Класс для пары Ключ - Значение</summary>
    public class StringKeyValueIPS : ICloneable
    {
      public string Key;
      public string Value;

      public StringKeyValueIPS(string key, string value)
      {
        this.Key = key;
        this.Value = value;
      }

      public StringKeyValueIPS Clone() => new StringKeyValueIPS(this.Key, this.Value);

      object ICloneable.Clone() => (object) new StringKeyValueIPS(this.Key, this.Value);
    }
}
