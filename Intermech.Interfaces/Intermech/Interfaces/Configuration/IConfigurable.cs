
// Type: Intermech.Interfaces.Configuration.IConfigurable
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces.Configuration
{
    /// <summary>
    /// Интерфейс, поддерживающий чтение и сохранение настроек
    /// </summary>
    public interface IConfigurable
    {
      /// <summary>
      /// Чтение предварительно сохраненных настроек из конфигурационного
      /// файла
      /// </summary>
      /// <param name="configurationManager"></param>
      void LoadConfiguration(IConfigurationManager configurationManager);

      /// <summary>Сохранение настроек в конфигурационном файле</summary>
      /// <param name="configurationManager"></param>
      void SaveConfiguration(IConfigurationManager configurationManager);
    }
}
