
// Type: Intermech.DisposeUtils
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;


namespace Intermech
{
    /// <summary>
    /// Содержит сервисные методы для работы с disposable объектами.
    /// </summary>
    public static class DisposeUtils
    {
      private static readonly TraceSwitch suppressedErrors = new TraceSwitch("DisposeUtils.SuppressedErrors", "", "0");

      /// <summary>
      /// Освобождает ресурсы объекта, если он реализует IDisposable.
      /// </summary>
      /// <param name="obj">Ссылка на объект. Может быть null</param>
      public static void TryDispose(object obj)
      {
        if (obj == null || !(obj is IDisposable disposable))
          return;
        disposable.Dispose();
      }

      /// <summary>
      /// Освобождает ресурсы объекта. Если при этом произойдет исключение, то оно будет подавлено и, опционально, выведено в журнал трассировки приложения.
      /// </summary>
      /// <param name="obj">Ссылка на объект. Может быть null</param>
      public static void SafelyDispose(IDisposable obj)
      {
        if (obj == null)
          return;
        try
        {
          obj.Dispose();
        }
        catch (Exception ex)
        {
          DisposeUtils.TraceDisposeException(obj, ex);
        }
      }

      [ExcludeFromCodeCoverage]
      private static void TraceDisposeException(IDisposable obj, Exception x)
      {
        if (!DisposeUtils.suppressedErrors.TraceError)
          return;
        Trace.WriteLine($"A exception occured in method Dispose() at object of type '{obj.GetType()}'. This exception is suppressed.");
        if (!DisposeUtils.suppressedErrors.TraceVerbose)
          return;
        Trace.WriteLine(x.Message);
        Trace.WriteLine(x.StackTrace);
      }
    }
}
