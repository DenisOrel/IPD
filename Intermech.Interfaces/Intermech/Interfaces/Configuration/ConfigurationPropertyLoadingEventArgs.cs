
// Type: Intermech.Interfaces.Configuration.ConfigurationPropertyLoadingEventArgs
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces.Configuration
{
    /// <summary>
    /// Класс аргументов для события, которое вызывается в процессе загрузки из stream свойства конфигурации.
    /// </summary>
    public class ConfigurationPropertyLoadingEventArgs : ConfigurationPropertyEventArgs
    {
      /// <summary>Создает объект.</summary>
      /// <param name="configuration">Объект конфигурации, загружаемой из stream</param>
      /// <param name="propertyName">Имя свойства</param>
      /// <param name="propertyValue">Значение свойства</param>
      public ConfigurationPropertyLoadingEventArgs(
        IConfiguration configuration,
        string propertyName,
        string propertyValue)
        : base(configuration, propertyName, propertyValue)
      {
        this.CanAdd = true;
      }

      /// <summary>
      /// Возвращает или задает признак, управляющий добавлением значения свойства в конфигурацию.
      /// По умолчанию значение свойства равно true. Если значение свойства равно false, то свойство
      /// будет проигнорировано и не будет добавлено в конфигурацию.
      /// </summary>
      public bool CanAdd { get; set; }
    }
}
