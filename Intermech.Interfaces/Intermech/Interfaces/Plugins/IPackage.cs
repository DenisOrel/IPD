
// Type: Intermech.Interfaces.Plugins.IPackage
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.Plugins
{
    /// <summary>Интерфейс модуля расширения IPS.</summary>
    public interface IPackage
    {
      /// <summary>Активирует модуль расширения.</summary>
      /// <param name="serviceProvider">Контейнер сервисов</param>
      /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="serviceProvider" /> не должен быть равен null</exception>
      void Load(IServiceProvider serviceProvider);

      /// <summary>Завершает работу модуля расширения.</summary>
      void Unload();

      /// <summary>Название модуля расширения.</summary>
      string Name { get; }
    }
}
