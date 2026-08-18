
// Type: Intermech.Diagnostics.SuppressedExceptions
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;


namespace Intermech.Diagnostics
{
    /// <summary>
    /// Вспомогательные утилиты, упрощающие работу с подавленными исключениями.
    /// </summary>
    public static class SuppressedExceptions
    {
      private static readonly TraceSwitch suppressedErrorsSwitch = new TraceSwitch("Intermech.Diagnostics.SuppressedErrors", "", "0");

      /// <summary>
      /// Выводит в журнал трассировки информацию о подавленном исключении.
      /// </summary>
      /// <param name="exception">Объект исключения</param>
      /// <param name="exceptionLocation">Место в коде, где произошло исключение. Как правило, это имя метода, бросившего исключение</param>
      /// <exception cref="T:ArgumentNullException">Параметры <paramref name="exception" />, <paramref name="exceptionLocation" /> не должны быть равны null</exception>
      [ExcludeFromCodeCoverage]
      public static void TraceException(Exception exception, string exceptionLocation)
      {
        if (exception == null)
          throw new ArgumentNullException(nameof (exception));
        if (exceptionLocation == null)
          throw new ArgumentNullException(nameof (exceptionLocation));
        if (SuppressedExceptions.suppressedErrorsSwitch.TraceError)
        {
          Trace.WriteLine($"A exception '{exception.GetType()}' occured at {exceptionLocation}. This exception is suppressed.");
          if (SuppressedExceptions.suppressedErrorsSwitch.TraceVerbose)
          {
            Trace.WriteLine(exception.Message);
            Trace.WriteLine(exception.StackTrace);
          }
        }
        EventHandler<SuppressedExceptionEventArgs> onException = SuppressedExceptions.OnException;
        if (onException == null)
          return;
        SuppressedExceptionEventArgs e = new SuppressedExceptionEventArgs(exception);
        onException((object) null, e);
      }

      /// <summary>
      /// Позволяет выполнить указанный код и получить подавленное исключение, если оно было сброшено.
      /// </summary>
      /// <param name="code">Код для выполнения</param>
      /// <returns>Подавленное исключение или null, если исключение не было сброшено</returns>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="code" /> не должен быть равен null</exception>
      public static Exception TryCaptureException(Action code)
      {
        if (code == null)
          throw new ArgumentNullException(nameof (code));
        Exception suppressedException = (Exception) null;
        EventHandler<SuppressedExceptionEventArgs> eventHandler = (EventHandler<SuppressedExceptionEventArgs>) ((sender, e) => suppressedException = e.Exception);
        SuppressedExceptions.OnException += eventHandler;
        try
        {
          code();
        }
        finally
        {
          SuppressedExceptions.OnException -= eventHandler;
        }
        return suppressedException;
      }

      /// <summary>
      /// Позволяет выполнить указанный код и получить результат выполнения кода и подавленное исключение, если оно было сброшено.
      /// </summary>
      /// <param name="action">Код для выполнения</param>
      /// <returns>Кортеж и результата выполнения кода и подавленного исключения. Ссылка на исключение может быть равна null, если исключение не было сброшено</returns>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="code" /> не должен быть равен null</exception>
      public static Tuple<TResult, Exception> TryCaptureException<TResult>(Func<TResult> code)
      {
        if (code == null)
          throw new ArgumentNullException(nameof (code));
        TResult result = default (TResult);
        Exception suppressedException = (Exception) null;
        EventHandler<SuppressedExceptionEventArgs> eventHandler = (EventHandler<SuppressedExceptionEventArgs>) ((sender, e) => suppressedException = e.Exception);
        SuppressedExceptions.OnException += eventHandler;
        try
        {
          result = code();
        }
        finally
        {
          SuppressedExceptions.OnException -= eventHandler;
        }
        return Tuple.Create(result, suppressedException);
      }

      /// <summary>Событие регистрации подавленного исключения.</summary>
      public static event EventHandler<SuppressedExceptionEventArgs> OnException;
    }
}
