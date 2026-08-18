
// Type: Intermech.ApplicationModel.ApplicationEventLogService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.ApplicationModel
{
    /// <summary>
    /// Сервис приложения, который предоставляет доступ ко всем журналам событий приложения. Реализация является thread safe.
    /// </summary>
    public class ApplicationEventLogService : IApplicationEventLogService
    {
      private IEventLogWriter fileLog;
      private IEventLogWriter systemLog;
      private IEventLogWriter defaultLog;
      private CompositeEventLogWriter compositeLog;

      /// <summary>Создает объект.</summary>
      /// <param name="fileLog">Писатель в файловый журнал событий приложения. Реализация должна быть thread safe</param>
      /// <param name="systemLog">Писатель в системный журнал событий приложения. Реализация должна быть thread safe</param>
      /// <param name="defaultLog">Писатель в журнал событий приложения по умолчанию. Значение параметра должно совпадать с  <paramref name="fileLog" /> или с <paramref name="systemLog" /></param>
      /// <exception cref="T:ArgumentNullException">Параметры <paramref name="fileLog" />, <paramref name="systemLog" />, <paramref name="defaultLog" /> не должны быть равны null</exception>
      public ApplicationEventLogService(
        IEventLogWriter fileLog,
        IEventLogWriter systemLog,
        IEventLogWriter defaultLog)
      {
        if (fileLog == null)
          throw new ArgumentNullException(nameof (fileLog));
        if (systemLog == null)
          throw new ArgumentNullException(nameof (systemLog));
        if (defaultLog == null)
          throw new ArgumentNullException(nameof (defaultLog));
        if (defaultLog != fileLog && defaultLog != systemLog)
          throw new ArgumentException("Журнал событий по умолчанию должен соответствовать либо файловому, либо системному журналу событий приложения.", nameof (defaultLog));
        this.fileLog = fileLog;
        this.systemLog = systemLog;
        this.defaultLog = defaultLog;
        this.compositeLog = new CompositeEventLogWriter((IEnumerable<IEventLogWriter>) new IEventLogWriter[2]
        {
          this.fileLog,
          this.systemLog
        });
      }

      /// <summary>
      /// Возвращает составной объект для записи одновременно во все журналы событий приложения.
      /// </summary>
      public IEventLogWriter AllLogs
      {
        [DebuggerStepThrough] get => (IEventLogWriter) this.compositeLog;
      }

      /// <summary>
      /// Возвращает объект для записи в журнал событий приложения, используемый по умолчанию.
      /// </summary>
      public IEventLogWriter DefaultLog
      {
        [DebuggerStepThrough] get => this.defaultLog;
      }

      /// <summary>
      /// Возвращает объект для записи в файловый журнал событий приложения.
      /// </summary>
      public IEventLogWriter FileLog
      {
        [DebuggerStepThrough] get => this.fileLog;
      }

      /// <summary>
      /// Возвращает объект для записи в системный журнал событий приложения.
      /// </summary>
      public IEventLogWriter SystemLog
      {
        [DebuggerStepThrough] get => this.systemLog;
      }
    }
}
