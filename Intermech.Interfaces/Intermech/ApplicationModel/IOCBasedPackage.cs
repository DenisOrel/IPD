
// Type: Intermech.ApplicationModel.IOCBasedPackage
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.ApplicationModel.NinjectIntegration;
using Intermech.Interfaces.Plugins;
using Ninject;
using Ninject.Modules;
using Ninject.Syntax;
using System;
using System.Diagnostics;


namespace Intermech.ApplicationModel
{
    /// <summary>
    /// Базовый класс для модуля расширения IPS с поддержкой интеграции с IOC-контейнером.
    /// </summary>
    public abstract class IOCBasedPackage : AbstractPackage
    {
      private IKernel globalIOCContainer;
      private IKernel localIOCContainer;
      private LazyInitializerModuleGroup subModules;

      /// <summary>Создает объект.</summary>
      /// <param name="createParameters">Параметры создания модуля расширения</param>
      /// <param name="name">Имя модуля расширения</param>
      /// <exception cref="T:System.ArgumentNullException">Параметры <paramref name="createParameters" />, <paramref name="name" /> не должны быть равны null</exception>
      protected IOCBasedPackage(IOCBasedPackageParameters createParameters, string name)
        : base(name)
      {
        this.globalIOCContainer = createParameters != null ? createParameters.IOCContainer : throw new ArgumentNullException(nameof (createParameters));
      }

      /// <summary>Активирует модуль расширения.</summary>
      /// <param name="serviceProvider">Контейнер сервисов</param>
      /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="serviceProvider" /> не должен быть равен null</exception>
      public sealed override void Load(IServiceProvider serviceProvider)
      {
        base.Load(serviceProvider);
        this.DoInitializeIOCContainer();
        this.DoInitializePackage();
        this.DoLoad();
      }

      /// <summary>Инициализирует модуль расширения.</summary>
      protected virtual void DoInitializePackage()
      {
      }

      /// <summary>Активирует модуль расширения.</summary>
      protected virtual void DoLoad()
      {
        this.subModules = this.IOCContainer.Get<LazyInitializerModuleGroup>();
        this.CreateSubModules(this.subModules);
        this.subModules.Initialize();
      }

      /// <summary>Завершает работу модуля расширения.</summary>
      public sealed override void Unload()
      {
        this.DoUnload();
        this.DoRemoveIOCContainer();
        base.Unload();
      }

      /// <summary>Завершает работу модуля расширения.</summary>
      protected virtual void DoUnload()
      {
        if (this.subModules == null)
          return;
        this.subModules.Shutdown();
        this.subModules = (LazyInitializerModuleGroup) null;
      }

      /// <summary>Возвращает IOC-контейнер этого модуля расширения.</summary>
      protected IKernel IOCContainer
      {
        [DebuggerStepThrough] get => this.localIOCContainer;
      }

      /// <summary>
      /// Возвращает IOC-контейнер приложения.
      /// Используется в тех случаях, когда модуль расширения должен предоставить сервис,
      /// доступный на уровне всего приложения.
      /// </summary>
      protected IKernel GlobalIOCContainer => this.globalIOCContainer;

      /// <summary>
      /// Инициализирует IOC-контейнер сразу после его создания.
      /// </summary>
      protected virtual void DoInitializeIOCContainer()
      {
        this.localIOCContainer = (IKernel) new Ninject.Extensions.ChildKernel.ChildKernel((IResolutionRoot) this.globalIOCContainer, Array.Empty<INinjectModule>());
        this.localIOCContainer.Load((INinjectModule) new PackageNinjectModule());
      }

      /// <summary>
      /// Освобождает IOC-контейнер и все связанные с ним ресурсы.
      /// </summary>
      protected virtual void DoRemoveIOCContainer()
      {
        if (this.localIOCContainer == null)
          return;
        this.localIOCContainer.Dispose();
        this.localIOCContainer = (IKernel) null;
      }

      /// <summary>
      /// Добавляет в контейнер подмодулей типы подмодулей, из которых состоит этот модуль расширения.
      /// Создание и инициализация каждого из подмодулей будут выполнены в порядке их добавления в контейнер.
      /// </summary>
      /// <param name="subModules">Контейнер для подмодулей, из которых состоит этот модуль расширения</param>
      protected virtual void CreateSubModules(LazyInitializerModuleGroup subModules)
      {
      }
    }
}
