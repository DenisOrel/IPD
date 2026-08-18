
// Type: Intermech.ApplicationModel.NinjectIntegration.NinjectInitializerModuleFactory
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Ninject;
using System;
using System.Diagnostics;


namespace Intermech.ApplicationModel.NinjectIntegration
{
    /// <summary>
    /// Фабрики модулей инициализации, использующая IOC-контейнер для создания объектов модулей.
    /// Реализация является thread safe.
    /// </summary>
    internal sealed class NinjectInitializerModuleFactory : IInitializerModuleFactory
    {
      private IKernel iocContainer;

      public NinjectInitializerModuleFactory(IKernel iocContainer) => this.iocContainer = iocContainer;

      private IKernel IOCContainer
      {
        [DebuggerStepThrough] get => this.iocContainer;
      }

      /// <summary>Создает модуль инициализации указанного типа.</summary>
      /// <typeparam name="TModule">Тип создаваемого модуля иницилизации</typeparam>
      /// <returns>Созданный модуль инициализации</returns>
      public TModule Create<TModule>() where TModule : InitializerModule
      {
        return this.IOCContainer.Get<TModule>();
      }

      /// <summary>Создает модуль инициализации указанного типа.</summary>
      /// <param name="moduleType">Тип модуля инициализации</param>
      /// <returns>Созданный модуль инициализации</returns>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="moduleType" /> не должен быть равен null</exception>
      public InitializerModule Create(Type moduleType)
      {
        return !(moduleType == (Type) null) ? (InitializerModule) this.IOCContainer.Get(moduleType) : throw new ArgumentNullException(nameof (moduleType));
      }
    }
}
