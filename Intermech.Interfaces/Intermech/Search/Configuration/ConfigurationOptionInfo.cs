
// Type: Intermech.Search.Configuration.ConfigurationOptionInfo
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Search.Configuration
{
    public sealed class ConfigurationOptionInfo
    {
      public ConfigurationOptionInfo(Type type)
      {
        if (type == (Type) null)
          throw new ArgumentNullException(nameof (type));
        if (type.IsValueType)
          this.DefaultValue = Activator.CreateInstance(type);
        this.Mode = DBConfigMode.UserOnly;
        this.Type = type;
      }

      public string Category { get; set; }

      public object DefaultValue { get; set; }

      public string Description { get; set; }

      public string DisplayName { get; set; }

      public bool CheckAdmin { get; set; }

      public bool RequestAdminRights { get; set; }

      public DBConfigMode Mode { get; set; }

      public ConfigurationOptionKey Key { get; set; }

      public Type Type { get; private set; }

      public Type TypeConverter { get; set; }

      public Type Editor { get; set; }

      public string Page { get; set; }

      public string ImageKey { get; set; }

      public Func<object> CustomGetHandler { get; set; }

      public Action<object> CustomSetHandler { get; set; }
    }
}
