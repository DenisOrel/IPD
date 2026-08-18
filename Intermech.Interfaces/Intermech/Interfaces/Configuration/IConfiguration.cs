
// Type: Intermech.Interfaces.Configuration.IConfiguration
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Collections;
using System.ComponentModel;


namespace Intermech.Interfaces.Configuration
{
    /// <summary>Summary description for IConfiguration.</summary>
    /// <summary>Summary description for IConfiguration.</summary>
    public interface IConfiguration
    {
      event PropertyChangedEventHandler PropertyChanged;

      /// <summary>Добавляет узел в список концигураций</summary>
      /// <param name="name">имя узла</param>
      /// <returns></returns>
      IConfiguration Add(string name);

      /// <summary>Удаляет параметры и входящие узлы</summary>
      void Clear();

      /// <summary>Удаляет</summary>
      /// <param name="name"></param>
      /// <returns></returns>
      void RemoveProperty(string name);

      string GetProperty(string name);

      bool HasProperty(string name);

      void Remove(IConfiguration configuration);

      IConfiguration[] Select(string name);

      IConfiguration Open(string name);

      void SetProperty(string name, string value);

      IConfigurationCollection Configurations { get; }

      string Name { get; }

      IEnumerable Properties { get; }

      int Count { get; }
    }
}
