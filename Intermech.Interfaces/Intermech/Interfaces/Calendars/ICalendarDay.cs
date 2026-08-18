
// Type: Intermech.Interfaces.Calendars.ICalendarDay
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Diagnostics;
using System.Collections.Generic;


namespace Intermech.Interfaces.Calendars
{
    /// <summary>Настройки календарного дня</summary>
    public interface ICalendarDay
    {
      /// <summary> Тип дня (стандартный рабочий, выходной, нестандартный рабочий) </summary>
      DayType DayType { get; set; }

      /// <summary>Список рабочих периодов</summary>
      [NotNull]
      [ItemNotNull]
      IReadOnlyList<IWorkTimePeriod> WorkTimePeriods { get; }
    }
}
