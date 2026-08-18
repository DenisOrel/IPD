
// Type: Intermech.Diagnostics.IMethodCallFormatter
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml


namespace Intermech.Diagnostics
{
    /// <summary>
    /// Позволяет реализовать объект для преобразование аргументов вызываемых методов в строковое представление.
    /// Данное преобразование используется при трассировке вызываемых методов.
    /// </summary>
    public interface IMethodCallFormatter
    {
      /// <summary>
      /// Выполняет преобразование аргумента метода в текстовое представление.
      /// </summary>
      /// <param name="argument">Значение аргумента вызванного метода</param>
      /// <returns>Строковое представление аргумента</returns>
      string FormatArgument(object argument);
    }
}
