
// Type: Intermech.Diagnostics.FatalExceptionGenerator
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Threading;


namespace Intermech.Diagnostics
{
    /// <summary>
    /// Класс для имитации падения необработанного исключения в приложении.
    /// </summary>
    public sealed class FatalExceptionGenerator
    {
      /// <summary>
      /// Бросает необработанное исключение в фоновом потоке приложения.
      /// </summary>
      public void Throw() => this.Throw(new Exception("Unhandled test exception"));

      /// <summary>
      /// Бросает необработанное исключение в фоновом потоке приложения.
      /// </summary>
      /// <param name="exception">Бросаемое исключение</param>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="exception" /> не должен быть равен null</exception>
      public void Throw(Exception exception)
      {
        Thread thread = exception != null ? new Thread((ThreadStart) (() =>
        {
          Thread.Yield();
          throw exception;
        })) : throw new ArgumentNullException(nameof (exception));
        thread.Name = this.GetType().Name;
        thread.IsBackground = true;
        thread.Start();
        thread.Join(1000);
      }
    }
}
