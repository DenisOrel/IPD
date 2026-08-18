
// Type: Intermech.Interfaces.Calendars.ICalendarsService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Diagnostics;
using System;


namespace Intermech.Interfaces.Calendars
{
    /// <summary>Сервис для работы с календарями</summary>
    public interface ICalendarsService
    {
      /// <summary>Получить интерфейс календаря по его идентификатору</summary>
      /// <exception cref="T:Intermech.Interfaces.Calendars.CalendarNotFoundException">Если календарь с указанным идентификатором не найден</exception>
      [NotNull]
      [Obsolete("Use GetCalendar(IUserSession userSession, long calendarID, throwIfNotFound = true)!")]
      ICalendar GetCalendar([NotEmpty] long calendarID, [NotNull] IUserSession iUserSession);

      /// <summary>Получить интерфейс календаря по его идентификатору</summary>
      /// <exception cref="T:Intermech.Interfaces.Calendars.CalendarNotFoundException">Если календарь с указанным идентификатором не найден и <see cref="!:throwIfNotFound" />==true</exception>
      [ContractAnnotation("throwIfNotFound:false => CanBeNull; => NotNull")]
      ICalendar GetCalendar([NotNull] IUserSession userSession, [NotEmpty] long calendarID, bool throwIfNotFound = true);
    }
}
