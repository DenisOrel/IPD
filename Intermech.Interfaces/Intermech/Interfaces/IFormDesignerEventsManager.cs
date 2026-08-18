
// Type: Intermech.Interfaces.IFormDesignerEventsManager
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections;
using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>Интерфейс для регистрации дополнительных событий.</summary>
    public interface IFormDesignerEventsManager : IEnumerable<FormDesignerAction>, IEnumerable
    {
      /// <summary>Получить обработчик.</summary>
      /// <param name="eventGuid">Глобальный идентификатор события</param>
      /// <returns>Обработчик события (если такое есть), либо null - если нет</returns>
      IFormDesignerEventHandlerBase GetEvent(Guid eventGuid);

      /// <summary>Список событий по его типу.</summary>
      /// <param name="eventType">Тип события</param>
      /// <returns></returns>
      Dictionary<Guid, FormDesignerAction> GetEvents(Type eventType);

      /// <summary>Получить описание события.</summary>
      /// <param name="eventGuid">Глобальный идентификатор события</param>
      /// <returns>Описание события</returns>
      FormDesignerAction GetInfo(Guid eventGuid);

      /// <summary>Регистрация события.</summary>
      /// <param name="eventType">Тип события</param>
      /// <param name="eventGuid">Глобальный идентификатор события</param>
      /// <param name="eventName">Наименование события</param>
      /// <param name="eventHandler">Обработчик события</param>
      void RegisterEvent(
        Type eventType,
        Guid eventGuid,
        string eventName,
        IFormDesignerEventHandlerBase eventHandler);

      /// <summary>Регистрация события.</summary>
      /// <param name="eventType">Тип события</param>
      /// <param name="action">Действие</param>
      /// <param name="eventHandler">Обработчик события</param>
      void RegisterEvent(
        Type eventType,
        FormDesignerAction action,
        IFormDesignerEventHandlerBase eventHandler);

      /// <summary>Удаление события.</summary>
      /// <param name="eventType">Тип события</param>
      /// <param name="eventGuid">Глобальный идентификатор события</param>
      void UnregisterEvent(Type eventType, Guid eventGuid);

      /// <summary>
      /// 
      /// </summary>
      event FormDesignerEventHandler DataLoadCompleted;
    }
}
