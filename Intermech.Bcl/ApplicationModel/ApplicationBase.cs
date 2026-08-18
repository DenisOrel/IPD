using Intermech.Runtime;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.ApplicationModel
{
    /// <summary>Базовый класс для приложений.</summary>
    public class ApplicationBase
    {
      private List<string> arguments;
      private SilentActionInvoker silentActions;
      private int exitCode;
      private bool isRunning;

      /// <summary>Создает объект приложения.</summary>
      /// <param name="arguments">Аргументы приложения</param>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="arguments" /> не должен быть равен null</exception>
      public ApplicationBase(IList<string> arguments)
      {
        this.arguments = arguments != null ? new List<string>((IEnumerable<string>) arguments) : throw new ArgumentNullException(nameof (arguments));
        this.silentActions = SilentActionInvoker.Default;
      }

      /// <summary>Возвращает список аргументов приложения.</summary>
      public List<string> Arguments
      {
        [DebuggerStepThrough] get => this.arguments;
      }

      /// <summary>Возвращает или задает код завершения приложения.</summary>
      public int ExitCode
      {
        [DebuggerStepThrough] get => this.exitCode;
        [DebuggerStepThrough] set => this.exitCode = value;
      }

      /// <summary>
      /// Возвращает признак, что приложение выполняется в данный момент.
      /// </summary>
      public bool IsRunning
      {
        [DebuggerStepThrough] get => this.isRunning;
        [DebuggerStepThrough] private set => this.isRunning = value;
      }

      /// <summary>Выполняет приложение.</summary>
      public void Run()
      {
        if (this.IsRunning)
          throw new InvalidOperationException("Program is already running.");
        this.ExitCode = 0;
        this.IsRunning = true;
        try
        {
          this.DoRun();
          this.InvokeSilently((Action) (() => this.DoCleanup(false)), "DoCleanup(false)");
          this.IsRunning = false;
        }
        catch (Exception ex)
        {
          this.InvokeSilently((Action) (() => this.DoReportUnhandledException(ex)), "DoReportUnhandledException(exception)");
          this.InvokeSilently((Action) (() => this.DoCleanup(true)), "DoCleanup(true)");
          this.IsRunning = false;
          if (this.ExitCode == 0)
            this.ExitCode = 1;
          this.InvokeSilently(new Action(this.DoReportUnexpectedExit), "DoReportUnexpectedExit()");
        }
      }

      /// <summary>Реализует выполнение приложения.</summary>
      protected virtual void DoRun()
      {
      }

      /// <summary>
      /// Реализует освобождение ресурсов приложения перед завершением работы. Метод вызывается как при нормальном завершении приложения,
      /// так и в случае необработанного исключения в процессе выполнения приложения. Реализация метода должна учитывать, что он
      /// может быть вызван для частично инициализированного приложения.
      /// </summary>
      /// <param name="errorMode">Признак завершения работы приложения из-за необработанного исключения</param>
      protected virtual void DoCleanup(bool errorMode)
      {
      }

      /// <summary>
      /// Позволяет сообщить пользователю о возникновении необработанного исключения при выполнении приложения.
      /// Метод вызывается сразу после обнаружения аварийной ситуации.
      /// </summary>
      /// <param name="exception">Объект исключения</param>
      protected virtual void DoReportUnhandledException(Exception exception)
      {
        this.TryGetExceptionDisplayService()?.ShowException(exception);
      }

      /// <summary>
      /// Позволяет сообщить пользователю о неожиданном завершении приложения из-за необработанного исключения.
      /// Метод вызывается после освобождения всех ресурсов приложения.
      /// </summary>
      protected virtual void DoReportUnexpectedExit()
      {
      }

      /// <summary>
      /// Выполняет указанный метод или блок кода с контролем необработанных исключений. Если при выполнении произойдет необработанное исключение,
      /// оно будет подавлено, и, если требуется, информация об этом событии будет записана в журнал трассировки.
      /// </summary>
      /// <param name="action">Выполняемый метод или блок кода</param>
      /// <param name="exceptionLocation">Описание места падения исключения, используется только в случае падения исключения. Значение параметра может быть равно null, в этом случае место падения будет вычислено автоматически</param>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="action" /> не должен быть равен null</exception>
      protected internal void InvokeSilently(Action action, string exceptionLocation = null)
      {
        this.silentActions.Invoke(action, exceptionLocation);
      }

      /// <summary>
      /// Возвращает сервис для отображения исключительных ситуаций, если он доступен.
      /// </summary>
      /// <returns>Объект сервиса или null</returns>
      protected virtual IExceptionDisplayService TryGetExceptionDisplayService()
      {
        return (IExceptionDisplayService) null;
      }
    }
}
