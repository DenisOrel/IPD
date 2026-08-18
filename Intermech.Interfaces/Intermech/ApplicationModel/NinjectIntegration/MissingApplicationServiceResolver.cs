
// Type: Intermech.ApplicationModel.NinjectIntegration.MissingApplicationServiceResolver
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Ninject;
using System;


namespace Intermech.ApplicationModel.NinjectIntegration
{
    /// <summary>
    /// Реализует стратегию, расширяющую возможности ApplicationServiceContainer по поиску неявных определений сервисов.
    /// Данная стратегия выполняет поиск отсутствующих сервисов, используя IOC-контейнер.
    /// Реализация является thread safe.
    /// </summary>
    internal sealed class MissingApplicationServiceResolver : IApplicationServiceResolver
    {
      private IKernel iocContainer;

      /// <summary>Создает объект.</summary>
      /// <param name="iocContainer">IOC-контейнер приложения</param>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="iocContainer" /> не должен быть равен null</exception>
      public MissingApplicationServiceResolver(IKernel iocContainer)
      {
        this.iocContainer = iocContainer != null ? iocContainer : throw new ArgumentNullException(nameof (iocContainer));
      }

      /// <summary>
      /// Пытается найти требуемый сервис приложения, если его не удалось найти в контейнере сервисов приложения.
      /// В случае успеха найденный сервис приложения будет добавлен в контейнер сервисов приложения.
      /// </summary>
      /// <param name="serviceType">Тип сервиса приложений</param>
      /// <returns>Найденный сервис приложения или null</returns>
      public object TryResolve(Type serviceType)
      {
        return this.iocContainer.CanResolve(serviceType) ? this.iocContainer.TryGet(serviceType) : (object) null;
      }
    }
}
