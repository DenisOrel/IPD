
// Type: Intermech.Runtime.SilentActionInvoker
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Diagnostics;
using System;
using System.Diagnostics;


namespace Intermech.Runtime
{
    /// <summary>
    /// Сервисный объект, реализующий возможность выполнить указанный метод или блок кода, подавив все исключения.
    /// При необходимости, подавленные исключения могут быть выведены в журнал трассировки приложения.
    /// </summary>
    public sealed class SilentActionInvoker
    {
      private static readonly SilentActionInvoker defaultInstance = new SilentActionInvoker();

      /// <summary>
      /// Выполняет указанный метод или блок кода с контролем необработанных исключений. Если при выполнении произойдет необработанное исключение,
      /// оно будет подавлено, и, если требуется, информация об этом событии будет записана в журнал трассировки.
      /// </summary>
      /// <param name="action">Выполняемый метод или блок кода</param>
      /// <param name="exceptionLocation">Описание места падения исключения, используется только в случае падения исключения. Значение параметра может быть равно null, в этом случае место падения будет вычислено автоматически</param>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="action" /> не должен быть равен null</exception>
      public void Invoke(Action action, string exceptionLocation = null)
      {
        if (action == null)
          throw new ArgumentNullException(nameof (action));
        try
        {
          action();
        }
        catch (Exception ex)
        {
          if (exceptionLocation == null)
            exceptionLocation = $"{action.Method.DeclaringType}.{action.Method.Name}()";
          string exceptionLocation1 = exceptionLocation;
          SuppressedExceptions.TraceException(ex, exceptionLocation1);
        }
      }

      /// <summary>
      /// Возвращает общедоступный экземпляр объекта, используемый по умолчанию.
      /// </summary>
      public static SilentActionInvoker Default
      {
        [DebuggerStepThrough] get => SilentActionInvoker.defaultInstance;
      }
    }
}
