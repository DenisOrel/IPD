
// Type: Intermech.ApplicationModel.IApplicationEventLogService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Diagnostics;


namespace Intermech.ApplicationModel
{
    /// <summary>
    /// Интерфейс сервиса приложения, который предоставляет доступ ко всем журналам событий приложения.
    /// </summary>
    public interface IApplicationEventLogService
    {
      /// <summary>
      /// Возвращает составной объект для записи одновременно во все журналы событий приложения.
      /// </summary>
      IEventLogWriter AllLogs { get; }

      /// <summary>
      /// Возвращает объект для записи в журнал событий приложения, используемый по умолчанию.
      /// </summary>
      IEventLogWriter DefaultLog { get; }

      /// <summary>
      /// Возвращает объект для записи в файловый журнал событий приложения.
      /// </summary>
      IEventLogWriter FileLog { get; }

      /// <summary>
      /// Возвращает объект для записи в системный журнал событий приложения.
      /// </summary>
      IEventLogWriter SystemLog { get; }
    }
}
