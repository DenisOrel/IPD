
// Type: Intermech.ApplicationModel.NinjectIntegration.SharedLibraryInitializerService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Ninject;
using System;
using System.Diagnostics;


namespace Intermech.ApplicationModel.NinjectIntegration
{
    /// <summary>
    /// Сервис инициализации общих библиотек приложения.
    /// Реализация является thread safe.
    /// </summary>
    internal sealed class SharedLibraryInitializerService : ISharedLibraryInitializerService
    {
      private IKernel iocContainer;
      private IInitializerModuleFactory initializerModuleFactory;

      /// <summary>Создает объект.</summary>
      /// <param name="iocContainer">IOC-контейнер приложения</param>
      /// <param name="initializerModuleFactory">Фабрика модулей инициализации</param>
      /// <exception cref="T:ArgumentNullException">Параметры <paramref name="iocContainer" />, <paramref name="initializerModuleFactory" /> не должны быть равны null</exception>
      public SharedLibraryInitializerService(
        IKernel iocContainer,
        IInitializerModuleFactory initializerModuleFactory)
      {
        if (iocContainer == null)
          throw new ArgumentNullException(nameof (iocContainer));
        if (initializerModuleFactory == null)
          throw new ArgumentNullException(nameof (initializerModuleFactory));
        this.iocContainer = iocContainer;
        this.initializerModuleFactory = initializerModuleFactory;
      }

      /// <summary>Возвращает IOC-контейнер приложения.</summary>
      private IKernel IOCContainer
      {
        [DebuggerStepThrough] get => this.iocContainer;
      }

      /// <summary>Возвращает фабрику модулей инициализации.</summary>
      public IInitializerModuleFactory InitializerModuleFactory
      {
        [DebuggerStepThrough] get => this.initializerModuleFactory;
      }
    }
}
