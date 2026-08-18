
// Type: Intermech.Interfaces.Calendars.IWorkTimePeriod
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.Calendars
{
    /// <summary>Период рабочего времени</summary>
    public interface IWorkTimePeriod
    {
      /// <summary> Часы начала периода </summary>
      int StartHours { get; set; }

      /// <summary> Минуты начала периода </summary>
      int StartMinutes { get; set; }

      /// <summary> Часы окончания периода </summary>
      int FinishHours { get; set; }

      /// <summary> Минуты окончания периода </summary>
      int FinishMinutes { get; set; }

      /// <summary>Продолжительности периода</summary>
      TimeSpan Duration { get; set; }

      /// <summary>Проверка корректности периода
      /// (прежде всего что время начала меньше времени окончания)</summary>
      bool IsCorrect { get; }
    }
}
