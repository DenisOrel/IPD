
// Type: Intermech.Interfaces.Plugins.IPlugin
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Interfaces.Plugins
{
    /// <summary>Описывает сборку с модулями расширения</summary>
    public interface IPlugin : IDisposable
    {
      /// <summary>Полный путь сборки</summary>
      string Location { get; }

      /// <summary>Имя сборки</summary>
      string Name { get; }

      /// <summary>Коллекция модулей расширения</summary>
      IList<IPackage> Packages { get; }

      /// <summary>
      /// Возвращает признак, что сведения о плагине необходимо сохранить в файле конфигурации для автоматической загрузки плагина при следующем запуске приложения
      /// </summary>
      bool AutoReload { get; }
    }
}
