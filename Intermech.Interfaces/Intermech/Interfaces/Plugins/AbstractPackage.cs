
// Type: Intermech.Interfaces.Plugins.AbstractPackage
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Diagnostics;


namespace Intermech.Interfaces.Plugins
{
    /// <summary>Базовый класс для модуля расширения IPS.</summary>
    public abstract class AbstractPackage : IPackage
    {
      private readonly string name;

      /// <summary>Создает объект.</summary>
      /// <param name="name">Имя модуля расширения</param>
      /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="name" /> не должен быть равен null</exception>
      protected AbstractPackage(string name)
      {
        this.name = name != null ? name : throw new ArgumentNullException(nameof (name));
      }

      /// <summary>Активирует модуль расширения.</summary>
      /// <param name="serviceProvider">Контейнер сервисов</param>
      /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="serviceProvider" /> не должен быть равен null</exception>
      public virtual void Load(IServiceProvider serviceProvider)
      {
        if (serviceProvider == null)
          throw new ArgumentNullException(nameof (serviceProvider));
      }

      /// <summary>Завершает работу модуля расширения.</summary>
      public virtual void Unload()
      {
      }

      /// <summary>Возвращает имя модуля расширения.</summary>
      public string Name
      {
        [DebuggerStepThrough] get => this.name;
      }
    }
}
