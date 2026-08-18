
// Type: Intermech.ApplicationModel.NinjectIntegration.NinjectOptionalService`1
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Ninject;
using System;
using System.Diagnostics;


namespace Intermech.ApplicationModel.NinjectIntegration
{
    /// <summary>
    /// Провайдер сервиса, получение которого не является гарантированным.
    /// </summary>
    /// <typeparam name="T">Тип сервиса, предоставляемого провайдером</typeparam>
    internal sealed class NinjectOptionalService<T> : IOptionalService<T>
    {
      private IKernel iocContainer;

      /// <summary>Создает объект.</summary>
      /// <param name="iocContainer">IOC-контейнер приложения</param>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="iocContainer" /> не должен быть равен null</exception>
      public NinjectOptionalService(IKernel iocContainer)
      {
        this.iocContainer = iocContainer != null ? iocContainer : throw new ArgumentNullException(nameof (iocContainer));
      }

      /// <summary>Возвращает IOC-контейнер приложения.</summary>
      private IKernel IOCContainer
      {
        [DebuggerStepThrough] get => this.iocContainer;
      }

      /// <summary>
      /// Возвращает объект сервиса или нулевое значение для данного типа объекта, если объект не может быть получен.
      /// </summary>
      /// <returns>Объект или нулевое значение для данного типа объектов</returns>
      public T TryGet() => this.IOCContainer.TryGet<T>();
    }
}
