
// Type: Intermech.Diagnostics.ThreadBoundMethodCallFormatter
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Threading;


namespace Intermech.Diagnostics
{
    /// <summary>
    /// Предоставляет потокобезопасную реализацию преобразования аргументов вызываемых методов в строковое представление.
    /// Данное преобразование используется при трассировке вызываемых методов.
    /// </summary>
    public sealed class ThreadBoundMethodCallFormatter : IMethodCallFormatter
    {
      private ThreadLocal<IMethodCallFormatter> threadBoundFormatter;

      /// <summary>Создает объект.</summary>
      /// <param name="createFunction">Функция для создания экземпляров объектов, реализующих преобразование</param>
      /// <exception cref="T:ArgumentNullException">createFunction</exception>
      public ThreadBoundMethodCallFormatter(Func<IMethodCallFormatter> createFunction)
      {
        this.threadBoundFormatter = createFunction != null ? new ThreadLocal<IMethodCallFormatter>(createFunction) : throw new ArgumentNullException(nameof (createFunction));
      }

      /// <summary>
      /// Выполняет преобразование аргумента метода в текстовое представление.
      /// </summary>
      /// <param name="argument">Значение аргумента вызванного метода</param>
      /// <returns>Строковое представление аргумента</returns>
      public string FormatArgument(object argument)
      {
        return this.threadBoundFormatter.Value.FormatArgument(argument);
      }
    }
}
