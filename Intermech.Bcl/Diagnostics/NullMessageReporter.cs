
// Type: Intermech.Diagnostics.NullMessageReporter
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Diagnostics;


namespace Intermech.Diagnostics
{
    /// <summary>
    /// Реализация нейтрального (null) объекта для вывода многострочных сообщений в текстовый журнал.
    /// </summary>
    public sealed class NullMessageReporter : IMessageReporter
    {
      private static readonly NullMessageReporter defaultInstance = new NullMessageReporter();

      /// <summary>
      /// Выводит строку текста текущего сообщения. Вывод текста может быть отложен до момента, пока сообщение не будет завершено с помощью метода <see cref="M:EndMessage" />.
      /// </summary>
      /// <param name="text">Текст сообщения</param>
      /// <exception cref="T:ArgumentNullException">text</exception>
      public void WriteLine(string text)
      {
        if (text == null)
          throw new ArgumentNullException(nameof (text));
      }

      /// <summary>Завершает текущее сообщение.</summary>
      public void EndMessage()
      {
      }

      /// <summary>
      /// Возвращает экземпляр объекта, используемый по умолчанию.
      /// </summary>
      public static NullMessageReporter Default
      {
        [DebuggerStepThrough] get => NullMessageReporter.defaultInstance;
      }
    }
}
