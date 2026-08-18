using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Pools;
using Intermech.Text;
using System;
using System.Text;
using System.Threading;


namespace Intermech.Client
{
    public sealed class UserSessionExceptionsReporter
    {
      private IServerEventLogService serverEventLog;

      public UserSessionExceptionsReporter(IServerEventLogService serverEventLog)
      {
        this.serverEventLog = serverEventLog != null ? serverEventLog : throw new ArgumentNullException(nameof (serverEventLog));
      }

      /// <summary>
      /// Выполняет запись в серверный лог исключения защиты от многопоточного доступа к сессиям.
      /// </summary>
      /// <param name="exception">Объект исключения</param>
      /// <param name="threadConflictException">Объект исключения конфликта потоков. Он либо совпадает с <paramref name="exception" />, либо вложен в него через InnerException</param>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="exception" /> не должен быть равен null; параметр <paramref name="threadConflictException" /> не должен быть равен null</exception>
      public void ReportExceptionToServerLog(
        Exception exception,
        UserSessionThreadConflictException threadConflictException)
      {
        if (exception == null)
          throw new ArgumentNullException(nameof (exception));
        if (threadConflictException == null)
          throw new ArgumentNullException(nameof (threadConflictException));
        if (exception.IsSavedToLogFile())
          return;
        this.serverEventLog.AddToTrace(this.CreateLogMessage(exception, (Exception) threadConflictException), Consts.traceAlways, "session_thread_errors.log");
        exception.SetSavedToLogFileFlag(new bool?(true));
      }

      /// <summary>
      /// Выполняет запись в серверный лог исключения от некорректного управления ресурсами сессий.
      /// </summary>
      /// <param name="exception">Объект исключения</param>
      /// <param name="sessionManagementException">Объект исключения управления ресурсами. Он либо совпадает с <paramref name="exception" />, либо вложен в него через InnerException</param>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="exception" /> не должен быть равен null; параметр <paramref name="sessionManagementException" /> не должен быть равен null</exception>
      public void ReportExceptionToServerLog(
        Exception exception,
        UserSessionProtectionException sessionManagementException)
      {
        if (exception == null)
          throw new ArgumentNullException(nameof (exception));
        if (sessionManagementException == null)
          throw new ArgumentNullException(nameof (sessionManagementException));
        if (exception.IsSavedToLogFile())
          return;
        this.serverEventLog.AddToTrace(this.CreateLogMessage(exception, (Exception) sessionManagementException), Consts.traceAlways, "session_management.log");
        exception.SetSavedToLogFileFlag(new bool?(true));
      }

      private string CreateLogMessage(Exception exception, Exception specialException)
      {
        using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate(2048 /*0x0800*/))
        {
          StringBuilder stringBuilder = objectPoolScope.Object;
          stringBuilder.AppendLine(ExceptionServices.GetExtendedExceptionText(exception));
          stringBuilder.AppendFormat("Exception rethrown at [Client Thread ID: {0}, Client Thread Name: '{1}']", (object) Thread.CurrentThread.ManagedThreadId, (object) Thread.CurrentThread.Name).AppendLine();
          stringBuilder.AppendLine("Client stack trace:");
          stringBuilder.AppendLine(Environment.StackTrace);
          return stringBuilder.ToString();
        }
      }
    }
}
