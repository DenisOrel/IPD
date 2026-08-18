
// Type: Intermech.Interfaces.TimeTableHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces.Calendars;
using Intermech.Localization;
using System;


namespace Intermech.Interfaces
{
    public class TimeTableHelper
    {
      /// <summary>Сокращенные названия дней недели</summary>
      public static string[] DaysOfWeekShortNames = new string[7]
      {
        LocalizationHolder.rm.GetString("Interfaces_776"),
        LocalizationHolder.rm.GetString("Interfaces_777"),
        LocalizationHolder.rm.GetString("Interfaces_778"),
        LocalizationHolder.rm.GetString("Interfaces_779"),
        LocalizationHolder.rm.GetString("Interfaces_780"),
        LocalizationHolder.rm.GetString("Interfaces_781"),
        LocalizationHolder.rm.GetString("Interfaces_782")
      };
      /// <summary>Сокращенные названия месяцев</summary>
      public static string[] MonthsShortNames = new string[12]
      {
        LocalizationHolder.rm.GetString("Interfaces_783"),
        LocalizationHolder.rm.GetString("Interfaces_784"),
        LocalizationHolder.rm.GetString("Interfaces_785"),
        LocalizationHolder.rm.GetString("Interfaces_786"),
        LocalizationHolder.rm.GetString("Interfaces_787"),
        LocalizationHolder.rm.GetString("Interfaces_788"),
        LocalizationHolder.rm.GetString("Interfaces_789"),
        LocalizationHolder.rm.GetString("Interfaces_790"),
        LocalizationHolder.rm.GetString("Interfaces_791"),
        LocalizationHolder.rm.GetString("Interfaces_792"),
        LocalizationHolder.rm.GetString("Interfaces_793"),
        LocalizationHolder.rm.GetString("Interfaces_794")
      };

      /// <summary>
      /// Функция равенство текущего момента времени и времени в расписании
      /// </summary>
      public static bool EqualTime(ICalendar calendar, ProcessTime time)
      {
        DateTime now = DateTime.Now;
        if (time.BeginDateTime > now)
          return false;
        bool flag = false;
        switch (time.Period)
        {
          case TimePeriod.OneTime:
            if (TimeTableHelper.DateEqual(time.BeginDateTime, now))
            {
              flag = true;
              break;
            }
            break;
          case TimePeriod.EveryDay:
            if (time.BeginDateTime.Hour == now.Hour && time.BeginDateTime.Minute == now.Minute && time.DayExecution != EveryDayExecution.None)
            {
              if (time.DayExecution == EveryDayExecution.EveryDay)
              {
                flag = true;
                break;
              }
              ICalendarDay dayByDate = calendar.GetDayByDate(now);
              if (dayByDate.DayType == DayType.Holiday && time.DayExecution == EveryDayExecution.OnHolidays || (dayByDate.DayType == DayType.NonStandardWork || dayByDate.DayType == DayType.StandardWork) && time.DayExecution == EveryDayExecution.OnWorkdays)
              {
                flag = true;
                break;
              }
              break;
            }
            break;
          case TimePeriod.EveryWeek:
            if (time.BeginDateTime.Hour == now.Hour && time.BeginDateTime.Minute == now.Minute && Array.IndexOf<int>(time.DaysOfWeek, (int) now.DayOfWeek) >= 0)
            {
              flag = true;
              break;
            }
            break;
          case TimePeriod.EveryMonth:
            if (time.BeginDateTime.Hour == now.Hour && time.BeginDateTime.Minute == now.Minute && time.DayOfMonth == now.Day && Array.IndexOf<int>(time.Months, now.Month) >= 0)
            {
              flag = true;
              break;
            }
            break;
        }
        return flag;
      }

      /// <summary>Сравнение дат с точностью до минуты</summary>
      private static bool DateEqual(DateTime date1, DateTime date2)
      {
        return date1.Year == date2.Year && date1.Month == date2.Month && date1.Day == date2.Day && date1.Hour == date2.Hour && date1.Minute == date2.Minute;
      }
    }
}
