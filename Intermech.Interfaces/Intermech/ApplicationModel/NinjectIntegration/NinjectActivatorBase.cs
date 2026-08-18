
// Type: Intermech.ApplicationModel.NinjectIntegration.NinjectActivatorBase
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Ninject;
using System;
using System.Diagnostics;


namespace Intermech.ApplicationModel.NinjectIntegration
{
    /// <summary>
    /// Базовый класс для активаторов объектов  с поддержкой внедрения зависимостей (Dependency Injection).
    /// В качестве контейнера используется Ninject.
    /// Реализация является thread safe.
    /// </summary>
    internal abstract class NinjectActivatorBase
    {
      private IKernel iocContainer;

      /// <summary>Создает объект.</summary>
      /// <param name="iocContainer">IOC-контейнер приложения</param>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="iocContainer" /> не должен быть равен null</exception>
      protected NinjectActivatorBase(IKernel iocContainer)
      {
        this.iocContainer = iocContainer != null ? iocContainer : throw new ArgumentNullException(nameof (iocContainer));
      }

      /// <summary>Возвращает IOC-контейнер приложения.</summary>
      protected IKernel IOCContainer
      {
        [DebuggerStepThrough] get => this.iocContainer;
      }

      protected object DoCreateInstance(Type objectType)
      {
        return !(objectType == (Type) null) ? this.IOCContainer.Get(objectType) : throw new ArgumentNullException(nameof (objectType));
      }
    }
}
