
// Type: Intermech.ApplicationModel.IApplicationServiceResolver
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.ApplicationModel
{
    /// <summary>
    /// Интерфейс стратегии поиска сервиса приложения, используемую, если не удалось найти сервис в контейнере сервисов приложения.
    /// </summary>
    public interface IApplicationServiceResolver
    {
      /// <summary>
      /// Пытается найти требуемый сервис приложения, если его не удалось найти в контейнере сервисов приложения.
      /// В случае успеха найденный сервис приложения будет добавлен в контейнер сервисов приложения.
      /// </summary>
      /// <param name="serviceType">Тип сервиса приложений</param>
      /// <returns>Найденный сервис приложения или null</returns>
      object TryResolve(Type serviceType);
    }
}
