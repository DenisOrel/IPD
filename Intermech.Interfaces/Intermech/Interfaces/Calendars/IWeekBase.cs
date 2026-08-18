
// Type: Intermech.Interfaces.Calendars.IWeekBase
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;


namespace Intermech.Interfaces.Calendars
{
    /// <summary>Календарная неделя</summary>
    public interface IWeekBase
    {
      /// <summary>Получить день недели по стандартному системному типу DayOfWeek</summary>
      [NotNull]
      IWeekDayInfo GetDayOfWeek(DayOfWeek dayOfWeek);

      /// <summary>Список дней недели</summary>
      [NotNull]
      [ItemNotNull]
      IReadOnlyList<IWeekDayInfo> WeekDays { get; }

      /// <summary>Понедельник</summary>
      [NotNull]
      IWeekDayInfo Monday { get; }

      /// <summary>Вторник</summary>
      [NotNull]
      IWeekDayInfo Tuesday { get; }

      /// <summary>Среда</summary>
      [NotNull]
      IWeekDayInfo Wednesday { get; }

      /// <summary>Четверг</summary>
      [NotNull]
      IWeekDayInfo Thursday { get; }

      /// <summary>Пятница</summary>
      [NotNull]
      IWeekDayInfo Friday { get; }

      /// <summary>Суббота</summary>
      [NotNull]
      IWeekDayInfo Saturday { get; }

      /// <summary>Воскресенье</summary>
      [NotNull]
      IWeekDayInfo Sunday { get; }

      /// <summary>Получить день недели (начиная с понедельника)</summary>
      [NotNull]
      IWeekDayInfo this[WeekDay param] { get; }
    }
}
