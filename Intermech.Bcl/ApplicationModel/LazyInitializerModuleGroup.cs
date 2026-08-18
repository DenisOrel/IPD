
// Type: Intermech.ApplicationModel.LazyInitializerModuleGroup
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.ApplicationModel
{
    /// <summary>
    /// Базовый класс для групп модулей инициализации, использующих ленивое создание модулей.
    /// </summary>
    public class LazyInitializerModuleGroup : InitializerModule
    {
      private IInitializerModuleFactory moduleFactory;
      private List<Type> moduleTypes;
      private List<InitializerModule> initializedModules;

      /// <summary>Создает объект.</summary>
      /// <param name="moduleFactory">Фабрика модулей инициализации</param>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="moduleFactory" /> не должен быть равен null</exception>
      public LazyInitializerModuleGroup(IInitializerModuleFactory moduleFactory)
      {
        this.moduleFactory = moduleFactory != null ? moduleFactory : throw new ArgumentNullException(nameof (moduleFactory));
        this.moduleTypes = new List<Type>();
      }

      /// <summary>Возвращает фабрику модулей инициализации.</summary>
      protected IInitializerModuleFactory ModuleFactory
      {
        [DebuggerStepThrough] get => this.moduleFactory;
      }

      /// <summary>
      /// Добавляет модуль в группу в конец последовательности инициализации модулей.
      /// Метод может быть вызван только у неинициализированной группы.
      /// Создание экземпляров модулей будет выполнено только при инициализации группы.
      /// </summary>
      /// <typeparam name="TModule">Тип модуля</typeparam>
      /// <exception cref="T:System.InvalidOperationException">Группа уже была инициализирована.</exception>
      public void Add<TModule>() where TModule : InitializerModule
      {
        this.RequireNotInitialized();
        this.moduleTypes.Add(typeof (TModule));
      }

      /// <summary>
      /// Выполняет инициализацию объектов и сервисов, предоставляемых модулем.
      /// </summary>
      protected override void DoInitialize()
      {
        base.DoInitialize();
        if (this.initializedModules == null)
          this.initializedModules = new List<InitializerModule>(this.moduleTypes.Capacity);
        foreach (Type moduleType in this.moduleTypes)
        {
          InitializerModule initializerModule = this.ModuleFactory.Create(moduleType);
          initializerModule.Initialize();
          this.initializedModules.Add(initializerModule);
        }
      }

      /// <summary>
      /// Завершает работу объектов и сервисов, предоставленных модулем.
      /// Если свойство модуля IsInitialized возвращает false, то DoShutdown вызван как реакция на необработанное исключение при инициализации модуля.
      /// </summary>
      protected override void DoShutdown()
      {
        if (this.initializedModules != null && this.initializedModules.Count != 0)
        {
          this.initializedModules.Reverse();
          foreach (InitializerModule initializedModule in this.initializedModules)
            initializedModule.Shutdown();
          this.initializedModules.Clear();
        }
        base.DoShutdown();
      }
    }
}
