
// Type: Intermech.Interfaces.Calendars.IUserCalendar
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Diagnostics;
using System;


namespace Intermech.Interfaces.Calendars
{
    /// <summary>Календарь хранящийся как объект типа "Пользователь"</summary>
    public interface IUserCalendar : ICalendarInDB, ICalendarBase
    {
      /// <summary>Идентификатор пользователя</summary>
      [NotEmpty]
      long UserID { get; }

      /// <summary>Дата принятия на работу</summary>
      DateTime? HireDate { get; }

      /// <summary>Дата Увольнения</summary>
      DateTime? FireDate { get; }
    }
}
