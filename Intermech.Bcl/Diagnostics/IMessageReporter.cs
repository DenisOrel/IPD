
// Type: Intermech.Diagnostics.IMessageReporter
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml


namespace Intermech.Diagnostics
{
    /// <summary>
    /// Интерфейс объекта для вывода многострочных сообщений в текстовый журнал.
    /// </summary>
    public interface IMessageReporter
    {
      /// <summary>
      /// Выводит строку текста текущего сообщения. Вывод текста может быть отложен до момента, пока сообщение не будет завершено с помощью метода <see cref="M:EndMessage" />.
      /// </summary>
      /// <param name="text">Текст сообщения</param>
      /// <exception cref="T:ArgumentNullException">text</exception>
      void WriteLine(string text);

      /// <summary>Завершает текущее сообщение.</summary>
      void EndMessage();
    }
}
