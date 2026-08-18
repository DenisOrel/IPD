
// Type: Intermech.Interfaces.Configuration.ConfigurationPropertyEventArgs
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.Configuration
{
    /// <summary>Класс аргументов для событий свойств конфигураций.</summary>
    public class ConfigurationPropertyEventArgs : EventArgs
    {
      /// <summary>Создает объект.</summary>
      /// <param name="configuration">Объект конфигурации</param>
      /// <param name="propertyName">Имя свойства</param>
      /// <param name="propertyValue">Значение свойства</param>
      public ConfigurationPropertyEventArgs(
        IConfiguration configuration,
        string propertyName,
        string propertyValue)
      {
        if (configuration == null)
          throw new ArgumentNullException(nameof (configuration));
        if (propertyName == null)
          throw new ArgumentNullException(nameof (propertyName));
        this.Configuration = configuration;
        this.Name = propertyName;
        this.Value = propertyValue;
      }

      /// <summary>Возвращает объект конфигурации.</summary>
      public IConfiguration Configuration { get; }

      /// <summary>Возвращает имя свойства.</summary>
      public string Name { get; }

      /// <summary>Возвращает или задает значение свойства.</summary>
      public string Value { get; set; }
    }
}
