
// Type: Intermech.ApplicationModel.IPSFatalExceptionLogger
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Diagnostics;


namespace Intermech.ApplicationModel
{
    /// <summary>
    /// Позволяет сохранить расширенную информацию о необработанном исключении в журнал событий приложения перед падением приложения.
    /// </summary>
    public class IPSFatalExceptionLogger : FatalExceptionLogger
    {
      /// <summary>Создает объект.</summary>
      /// <param name="eventLogWriter">Писатель в журнал событий приложения</param>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="eventLogWriter" /> не должен быть равен null</exception>
      public IPSFatalExceptionLogger(IEventLogWriter eventLogWriter)
        : base(eventLogWriter)
      {
        this.Recommendation = "Please contact the IPS administrator with the information below.";
      }
    }
}
