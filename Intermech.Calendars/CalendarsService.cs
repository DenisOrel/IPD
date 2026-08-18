
// Type: Intermech.Calendars.CalendarsService
// Assembly: Intermech.Calendars, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8CDCC38D-5D33-4955-8468-4C0264D69139
// Assembly location: D:\IPS\Client\Intermech.Calendars.dll
// XML documentation location: D:\IPS\Client\Intermech.Calendars.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Calendars;
using System;
using System.Runtime.CompilerServices;


namespace Intermech.Calendars
{
    /// <summary>Сервис календарей</summary>
    public class CalendarsService : LongLifeObject, ICalendarsService
    {
      public CalendarsService([NotNull] IUserSession userSession) => MetadataLoader.Init(userSession);

      /// <summary>Получить интерфейс календаря по его идентификатору</summary>
      [ContractAnnotation("throwIfNotFound:false => CanBeNull; => NotNull")]
      public ICalendar GetCalendar([NotNull] IUserSession userSession, [NotEmpty] long calendarID, bool throwIfNotFound = true)
      {
        return (ICalendar) CalendarLoader.GetCalendarByID(userSession, calendarID, throwIfNotFound);
      }

      /// <summary>Получить интерфейс календаря по его идентификатору</summary>
      [NotNull]
      [Obsolete("Use GetCalendar(IUserSession userSession, long calendarID, throwIfNotFound = true)!")]
      public ICalendar GetCalendar([NotEmpty] long calendarID, [NotNull] IUserSession userSession)
      {
        return this.GetCalendar(userSession, calendarID, true);
      }

      /// <summary>Получить интерфейс персонального календаря по идентификатору пользователя</summary>
      [ContractAnnotation("throwIfNotFound:false => CanBeNull; => NotNull")]
      public IUserCalendar GetUserCalendar([NotNull] IUserSession userSession, [NotEmpty] long userID, bool throwIfNotFound = true)
      {
        return (IUserCalendar) CalendarLoader.GetUserCalendar(userSession, userID, throwIfNotFound);
      }

      /// <summary>
      /// Преобразование стандартного .NET типа DayOfWeek (0-воскресенье ... 6 - суббота)
      /// во внутренний тип WeekDay (1-понедельник ... 7 - воскресенье)
      /// </summary>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static WeekDay DayOfWeekToWeekDay(DayOfWeek dayOfWeek)
      {
        int num = (int) dayOfWeek;
        return num <= 0 ? WeekDay.Sunday : (WeekDay) num;
      }
    }
}
