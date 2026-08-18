
// Type: Intermech.Interfaces.Configuration.IConfigurationManager
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces.Configuration
{
    /// <summary>Summary description for IConfigurationManager.</summary>
    public interface IConfigurationManager
    {
      event ConfigurationLoadedEventHandler ConfigurationLoaded;

      event ConfigurationBeforeSaveEventHandler ConfigurationBeforeSave;

      IConfiguration Create(string name);

      void Delete(string name);

      IConfiguration Open(string name);

      IConfigurationCollection Configurations { get; }
    }
}
