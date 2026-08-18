
// Type: Intermech.Interfaces.ITimedEventProperties
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Интерфейс для свойств временных событий</summary>
    public interface ITimedEventProperties
    {
      /// <summary>
      /// Возвращает дату следующего запуска задачи (для периодических событий)
      /// </summary>
      /// <param name="timeZoneOffset">Часовой пояс для текущей сессия пользователя</param>
      /// <param name="currentUtcDateTime">Текущее UTC время</param>
      /// <returns>Дата следующего срабатывания события в формате UTC</returns>
      DateTime GetNextUtcDate(TimeSpan timeZoneOffset, DateTime currentUtcDateTime);
    }
}
