
// Type: Intermech.Interfaces.Configuration.IPersistableConfigurationManager
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.IO;


namespace Intermech.Interfaces.Configuration
{
    /// <summary>
    /// Расширение интерфейса IConfigurationManager, которое позволяет сохранять и восстанавливать конфигурацию.
    /// </summary>
    public interface IPersistableConfigurationManager : IConfigurationManager
    {
      void Load(Stream stream);

      void Save(Stream stream);
    }
}
