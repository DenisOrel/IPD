
// Type: Intermech.Interfaces.Calendars.ICalendar
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Diagnostics;


namespace Intermech.Interfaces.Calendars
{
    /// <summary>Календарь хранящийся как объект типа "Календарь"</summary>
    public interface ICalendar : ICalendarInDB, ICalendarBase
    {
      /// <summary>Идентификатор календаря</summary>
      [NotEmpty]
      long CalendarID { get; }

      /// <summary>Наименование календаря</summary>
      [NotNull]
      [NotWhitespace]
      string Name { get; }
    }
}
