
// Type: Intermech.Interfaces.Calendars.ISpecialCalendarDay
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.Calendars
{
    /// <summary>Специальный период календарных дней</summary>
    public interface ISpecialCalendarDay : ICalendarDay
    {
      /// <summary>Дата начала специального периода календарных дней</summary>
      DateTime PeriodStartDate { get; set; }

      /// <summary>Дата окончания специального периода календарных дней</summary>
      DateTime PeriodFinishDate { get; set; }

      /// <summary> Периодичность повторения специального дня </summary>
      DateRepeatRate DateRepeatRate { get; set; }

      /// <summary> Заблокировать автоматическое устранение конфликтов в периоде </summary>
      void LockCorrection();

      /// <summary> Разблокировать автоматическое устранение конфликтов в периоде </summary>
      void UnlockCorrection();
    }
}
