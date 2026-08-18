
// Type: Intermech.Interfaces.ScheduledEventHandlerInfo
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Класс с описанием свойств службы-обработчика событий</summary>
    [Serializable]
    public class ScheduledEventHandlerInfo
    {
      /// <summary>Глобальный идентификатор обработчика событий</summary>
      public Guid ServiceGuid;
      /// <summary>Наименование события</summary>
      public string EventName;

      public ScheduledEventHandlerInfo(Guid serviceGuid, string eventName)
      {
        this.ServiceGuid = serviceGuid;
        this.EventName = eventName;
      }
    }
}
