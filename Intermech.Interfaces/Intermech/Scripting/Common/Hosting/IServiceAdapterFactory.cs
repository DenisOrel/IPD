
// Type: Intermech.Scripting.Common.Hosting.IServiceAdapterFactory
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Scripting.Common.Hosting
{
    /// <summary>
    /// Интерфейс фабрики адаптеров обращений к сервисам приложения.
    /// С помощью таких адаптеров сценарии могут обращаться к сервисам приложения из изолированных AppDomain.
    /// Реализация интерфейса должна быть thread safe.
    /// </summary>
    public interface IServiceAdapterFactory
    {
      /// <summary>
      /// Создает адаптер для указанного сервиса приложения, если это возможно.
      /// </summary>
      /// <param name="service">Тип сервиса</param>
      /// <param name="externalCache">Кэш, который можно использовать для хранения адаптеров</param>
      /// <returns>Адаптер сервиса или null</returns>
      /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="service" /> не должен быть равен null; параметр <paramref name="externalCache" /> не должен быть равен null</exception>
      MarshalByRefObject TryCreateServiceAdapter(
        Type service,
        IDictionary<string, object> externalCache);
    }
}
