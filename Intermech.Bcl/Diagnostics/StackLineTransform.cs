
// Type: Intermech.Diagnostics.StackLineTransform
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Diagnostics
{
    /// <summary>
    /// Реализует базовый класс для преобразований строк stack trace. Такие преобразования используются для добавления дополнительной технической инфомации,
    /// деобфускации stack trace и т.д.
    /// </summary>
    public class StackLineTransform
    {
      /// <summary>
      /// Выполняет преобразование указанной строки stack trace.
      /// </summary>
      /// <param name="sourceLine">Исходный текст строки</param>
      /// <param name="targetLine">Построитель результирующей строки, содержащей результаты преобразования</param>
      /// <exception cref="T:System.ArgumentNullException">Аргумент метода не указан</exception>
      public void ApplyTransform(string sourceLine, StackLineBuilder targetLine)
      {
        if (sourceLine == null)
          throw new ArgumentNullException(nameof (sourceLine));
        if (targetLine == null)
          throw new ArgumentNullException(nameof (targetLine));
        this.DoTransform(sourceLine, targetLine);
      }

      /// <summary>
      /// Реализует преобразование указанной строки stack trace.
      /// </summary>
      /// <param name="sourceLine">Исходный текст строки</param>
      /// <param name="targetLine">Построитель результирующей строки, содержащей результаты преобразования</param>
      protected virtual void DoTransform(string sourceLine, StackLineBuilder targetLine)
      {
      }
    }
}
