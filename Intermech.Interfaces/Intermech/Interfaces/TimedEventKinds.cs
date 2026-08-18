
// Type: Intermech.Interfaces.TimedEventKinds
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;


namespace Intermech.Interfaces
{
    /// <summary>Периодичность события</summary>
    public enum TimedEventKinds
    {
      /// <summary>Разовое событие</summary>
      [CustomDescription("EventKindsOnce")] Once,
      /// <summary>Ежечасно</summary>
      [CustomDescription("EventKindsHourly")] Hourly,
      /// <summary>Ежедневно</summary>
      [CustomDescription("EventKindsDaily")] Daily,
      /// <summary>Еженедельно</summary>
      [CustomDescription("EventKindsWeekly")] Weekly,
      /// <summary>Ежемесячно</summary>
      [CustomDescription("EventKindsMonthly")] Monthly,
      /// <summary>Ежегодно</summary>
      [CustomDescription("EventKindsYearly")] Yearly,
      /// <summary>Ежеминутно</summary>
      [CustomDescription("EventKindsMinutely")] Minutely,
    }
}
