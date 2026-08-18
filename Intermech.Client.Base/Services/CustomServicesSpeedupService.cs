using Intermech.Interfaces.Client;
using System;


namespace Intermech.Services
{
    /// <summary>
    /// Реализация клиентского сервиса, ускоряющего получение сервисов сервера приложений.
    /// Реализация должна быть thread safe.
    /// </summary>
    public class CustomServicesSpeedupService : ICustomServicesSpeedupService
    {
      private readonly IMServerService imserverService;

      /// <summary>Создает объект.</summary>
      /// <param name="imserverService">Сервис головного объекта сервера приложений</param>
      /// <exception cref="T:System.ArgumentNullException">паратмер <paramref name="imserverService" /> содержит null</exception>
      public CustomServicesSpeedupService(IMServerService imserverService)
      {
        this.imserverService = imserverService ?? throw new ArgumentNullException(nameof (imserverService));
      }

      /// <summary>Возвращает сервис сервера приложений</summary>
      /// <param name="serviceType">Тип сервиса</param>
      /// <returns>Объект сервиса или null</returns>
      public object GetCustomService(Type serviceType)
      {
        return this.imserverService.GetCustomService(serviceType);
      }
    }
}
