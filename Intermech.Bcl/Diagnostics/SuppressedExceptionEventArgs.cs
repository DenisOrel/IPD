
// Type: Intermech.Diagnostics.SuppressedExceptionEventArgs
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Diagnostics;


namespace Intermech.Diagnostics
{
    /// <summary>
    /// Аргументы для события регистрации подавленного исключения.
    /// </summary>
    public sealed class SuppressedExceptionEventArgs : EventArgs
    {
      private Exception exception;

      /// <summary>Создает объект.</summary>
      /// <param name="exception">Объект подавленного исключения</param>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="exception" /> не должен быть равен null</exception>
      public SuppressedExceptionEventArgs(Exception exception)
      {
        this.exception = exception != null ? exception : throw new ArgumentNullException(nameof (exception));
      }

      /// <summary>Возвращает объект подавленного исключения.</summary>
      public Exception Exception
      {
        [DebuggerStepThrough] get => this.exception;
      }
    }
}
