
// Type: Intermech.Search.Configuration.ConfigurationOptionKey
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Search.Configuration
{
    public sealed class ConfigurationOptionKey
    {
      private string _keyString;

      public ConfigurationOptionKey(string keyString)
      {
        this._keyString = keyString != null ? keyString : throw new ArgumentNullException(nameof (keyString));
        string[] strArray = keyString.Split('/');
        this.Module = strArray.Length == 3 ? strArray[0] : throw new ArgumentException();
        this.Section = strArray[1];
        this.Name = strArray[2];
      }

      public string Module { get; private set; }

      public string Section { get; private set; }

      public string Name { get; private set; }

      public static explicit operator ConfigurationOptionKey(string keyString)
      {
        return new ConfigurationOptionKey(keyString);
      }

      public static implicit operator string(ConfigurationOptionKey key) => key._keyString;

      public static bool operator ==(ConfigurationOptionKey left, ConfigurationOptionKey right)
      {
        return object.Equals((object) left, (object) right);
      }

      public static bool operator !=(ConfigurationOptionKey left, ConfigurationOptionKey right)
      {
        return !(left == right);
      }

      public override bool Equals(object obj)
      {
        if (obj == (object) this)
          return true;
        ConfigurationOptionKey configurationOptionKey = obj as ConfigurationOptionKey;
        return configurationOptionKey != (ConfigurationOptionKey) null && configurationOptionKey._keyString == this._keyString;
      }

      public override int GetHashCode() => this._keyString.GetHashCode();

      public override string ToString() => this._keyString;
    }
}
