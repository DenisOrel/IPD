
// Type: Intermech.Interfaces.Calendars.ICalendarInDB
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Diagnostics;


namespace Intermech.Interfaces.Calendars
{
    /// <summary>Календарь существующий в базе данных</summary>
    public interface ICalendarInDB : ICalendarBase
    {
      /// <summary>Сохранение параметров в объект</summary>
      void SaveParams([NotNull] IUserSession iUserSession, bool throwNotFound = true);
    }
}
