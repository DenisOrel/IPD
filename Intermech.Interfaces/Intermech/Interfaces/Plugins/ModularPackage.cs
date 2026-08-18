
// Type: Intermech.Interfaces.Plugins.ModularPackage
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.ApplicationModel;
using System;


namespace Intermech.Interfaces.Plugins
{
    /// <summary>
    /// Базовый класс для модуля расширения IPS, который сам состоит из отдельных подмодулей.
    /// </summary>
    public abstract class ModularPackage : AbstractPackage
    {
      private readonly InitializerModuleGroup subModules;

      /// <summary>Создает объект.</summary>
      /// <param name="name">Имя модуля расширения</param>
      /// <exception cref="T:System.ArgumentNullException">name</exception>
      public ModularPackage(string name)
        : base(name)
      {
        this.subModules = new InitializerModuleGroup();
        this.InitializeSubModulesContainer(this.subModules);
      }

      /// <summary>Активирует модуль расширения.</summary>
      /// <param name="serviceProvider">Контейнер сервисов</param>
      /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="serviceProvider" /> не должен быть равен null</exception>
      public override void Load(IServiceProvider serviceProvider)
      {
        base.Load(serviceProvider);
        this.CreateSubModules(this.subModules);
        this.subModules.Initialize();
      }

      /// <summary>Завершает работу модуля расширения.</summary>
      public override void Unload()
      {
        base.Unload();
        this.subModules.Shutdown();
      }

      /// <summary>
      /// Инициализирует контейнер для подмодулей, из которых состоит этот модуль расширения.
      /// </summary>
      /// <param name="subModules">Контейнер для подмодулей, из которых состоит этот модуль расширения</param>
      protected virtual void InitializeSubModulesContainer(InitializerModuleGroup subModules)
      {
      }

      /// <summary>
      /// Создает и регистрирует отдельные подмодули, из которых состоит этот модуль расширения.
      /// </summary>
      /// <param name="subModules">Контейнер для подмодулей, из которых состоит этот модуль расширения</param>
      protected virtual void CreateSubModules(InitializerModuleGroup subModules)
      {
      }
    }
}
