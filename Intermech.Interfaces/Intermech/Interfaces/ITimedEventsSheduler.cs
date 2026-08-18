
// Type: Intermech.Interfaces.ITimedEventsSheduler
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Data;


namespace Intermech.Interfaces
{
    /// <summary>Интерфейс для назначения расписания запуска событий</summary>
    public interface ITimedEventsSheduler
    {
      /// <summary>Возвращает таблицу со списком назначенных событий</summary>
      /// <param name="sessionGuid">Гуид сесии</param>
      DataTable GetEventsTable(Guid sessionGuid);

      /// <summary>Удаляет из базы указанные события</summary>
      /// <param name="eventIDs">Массив с идентификаторами событий</param>
      /// <param name="sessionGuid">Гуид сесии</param>
      void DeleteEvents(Guid sessionGuid, int[] eventIDs);

      /// <summary>Создаёт событие</summary>
      /// <param name="sessionGuid">Гуид сесии</param>
      /// <param name="properties">Свойства создаваемого события. Обязательны для заполнения следующие поля: ServiceGuid, EventKind, Name.
      /// Остальные поля будут при необходимости проинициализированы самой службой-обработчиком события. </param>
      /// <returns>Строка из таблицы IMS_TIMED_EVENTS с описанием созданного события</returns>
      TimedEventProperties AddEvent(Guid sessionGuid, TimedEventProperties properties);

      /// <summary>Изменяет свойства события</summary>
      /// <param name="sessionGuid">Гуид сесии</param>
      /// <param name="properties">Новые свойства события</param>
      /// <returns>Строка из таблицы IMS_TIMED_EVENTS с описанием изменённого события</returns>
      TimedEventProperties EditEvent(Guid sessionGuid, TimedEventProperties properties);

      /// <summary>Инициирует немедленное срабатывание события</summary>
      /// <param name="sessionGuid">Гуид сесии</param>
      /// <param name="eventID">Ид. события</param>
      void RunEvent(Guid sessionGuid, int eventID);

      /// <summary>Изменяет имя приоритетного сервера обработки событий</summary>
      /// <param name="sessionGuid">Гуид сесии</param>
      /// <param name="serverName">Имя сервера, обрабатывающего очередь событий</param>
      void SetPrimaryServer(Guid sessionGuid, string serverName);

      /// <summary>
      /// Возвращает имя приоритетного сервера обработки событий
      /// </summary>
      /// <param name="sessionGuid">Гуид сесии</param>
      /// <returns>Имя приоритетного сервера обработки событий</returns>
      string GetPrimaryServer(Guid sessionGuid);

      /// <summary>
      /// Возвращает массив с описанием зарегистрированных на сервере событий, которые можно вызывать по расписанию.
      /// </summary>
      /// <returns>Массив свойств обработчиков событий</returns>
      ScheduledEventHandlerInfo[] GetScheduledEventHandlers();
    }
}
