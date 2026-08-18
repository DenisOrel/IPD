
// Type: Intermech.Interfaces.WebPortal.IPublishCompositionService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Interfaces.WebPortal
{
    /// <summary>
    /// Интерфейс на серверную службу для получения состава публикуемых объектов
    /// </summary>
    public interface IPublishCompositionService : ICustomCompositionService
    {
      /// <summary>Получить состав для публикации на портал</summary>
      /// <param name="userSessionGuid">GUID сессии</param>
      /// <param name="selectGUID">GUID, по которому клиентская программа сможет обращаться к серверному потоку, разворачивающему состав</param>
      /// <param name="rootObjectIDs">Корневые объекты, состав которых надо получить</param>
      /// <param name="publishType">Типы публикации</param>
      /// <param name="options">Опции получения публикуемого состава</param>
      void Select(
        Guid userSessionGuid,
        Guid selectGUID,
        List<long> rootObjectIDs,
        ExtendedPublishOptions options,
        PublishType publishType,
        bool throwException);

      /// <summary>
      /// Зарегистрировать тип, объекты которого всегда публикуются на портал целиком
      /// </summary>
      void RegisterIncludeObjectsAlwaysObjectType(int objectType);

      /// <summary>
      /// Список типов, объекты которых всегда публикуются на портал целиком
      /// </summary>
      List<int> IncludeObjectsAlwaysObjectTypeIDs { get; }
    }
}
