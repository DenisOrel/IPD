
// Type: Intermech.Interfaces.Plugins.IPluginManager
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Interfaces.Plugins
{
    /// <summary>Интерфейс менеджера плагинов.</summary>
    public interface IPluginManager
    {
      /// <summary>
      /// Загружает плагин и инициализирует все модули расширения в плагине.
      /// </summary>
      /// <param name="fileName">Путь к файлу плагина</param>
      /// <returns>Объект плагина</returns>
      IPlugin Load(string fileName);

      /// <summary>
      /// Загружает плагин и инициализирует все модули расширения в плагине.
      /// </summary>
      /// <param name="fileName">Путь к файлу плагина</param>
      /// <param name="autoReload">Признак, что сведения о плагине необходимо сохранить в файле конфигурации для автоматической загрузки плагина при следующем запуске приложения</param>
      /// <returns>Объект плагина</returns>
      IPlugin Load(string fileName, bool autoReload);

      /// <summary>
      /// Выгружает плагин. Метод завершает работу всех модулей расширения в плагине и освобождает все выделенные ресурсы.
      /// Физически сборка плагина из памяти приложения не выгружается, так как это не поддерживается исполняющей средой.
      /// </summary>
      /// <param name="plugin">Объект плагина</param>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="plugin" /> не должен быть равен null</exception>
      void Unload(IPlugin plugin);

      /// <summary>Возвращает коллекцию загруженных плагинов.</summary>
      IList<IPlugin> Plugins { get; }

      /// <summary>Событие успешной загрузки и инициализации плагина.</summary>
      event PluginEventHandler PluginAdded;

      /// <summary>Событие успешной выгрузки плагина.</summary>
      event PluginEventHandler PluginRemoved;

      /// <summary>Событие завершения загрузки всех плагинов.</summary>
      event EventHandler LoadComplete;

      /// <summary>
      /// Флаг указывает режим загрузки плагина ( Ручной или Автоматический )
      /// </summary>
      bool AutoLoad { get; }

      /// <summary>Возвращает true, если загрузка плагинов завершена.</summary>
      bool IsLoadComplete { get; }
    }
}
