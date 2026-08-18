
// Type: Intermech.Interfaces.Calendars.ICalendarBase
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;


namespace Intermech.Interfaces.Calendars
{
    /// <summary>Заготовка календаря</summary>
    public interface ICalendarBase
    {
      /// <summary>Стандартная неделя данного календаря</summary>
      [NotNull]
      IWeekBase StandardWeek { get; }

      /// <summary>Список всех специальных периодов календарных дней</summary>
      [NotNull]
      [ItemNotNull]
      IReadOnlyList<ISpecialCalendarDay> SpecialCalendarDays { get; }

      /// <summary> День начала рабочей недели</summary>
      DayOfWeek WeekStartDay { get; set; }

      /// <summary> Месяц начала финансового года</summary>
      Month YearStartMonth { get; set; }

      /// <summary> Часы начала рабочего времени</summary>
      int DefaultStartHour { get; set; }

      /// <summary> Минуты начала рабочего времени</summary>
      int DefaultStartMinute { get; set; }

      /// <summary> Часы окончания рабочего времени</summary>
      int DefaultFinishHour { get; set; }

      /// <summary> Минуты начала рабочего времени</summary>
      int DefaultFinishMinute { get; set; }

      /// <summary> Рабочих часов в дне</summary>
      double HoursInDay { get; set; }

      /// <summary> Рабочих часов в неделе</summary>
      double HoursInWeek { get; set; }

      /// <summary> Рабочих дней в месяце</summary>
      int DaysInMonth { get; set; }

      /// <summary>Рабочие периоды стандартного рабочего дня</summary>
      [NotNull]
      [ItemNotNull]
      IReadOnlyList<IWorkTimePeriod> StandardWorkPeriods { get; }

      /// <summary>Сохранение параметров в блоб-атрибут</summary>
      void SaveParams([NotNull] IBlobWriter blobWriter);

      /// <summary>Получить интерфейс описания настроек дня по календарной дате</summary>
      [NotNull]
      ICalendarDay GetDayByDate(DateTime day);

      /// <summary>Получить описание настроек СПЕЦИАЛЬНЫХ дней пересекающихся с календарным периодом</summary>
      [NotNull]
      [ItemNotNull]
      IReadOnlyList<ISpecialCalendarDay> GetSpecialDaysInPeriod(
        DateTime periodStart,
        DateTime periodFinish);

      /// <summary>Получить описание настроек ВСЕХ дней пересекающихся с календарным периодом</summary>
      [NotNull]
      [ItemNotNull]
      IReadOnlyList<ICalendarDay> GetDaysInPeriod(DateTime periodStart, DateTime periodFinish);
    }
}
