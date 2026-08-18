
// Type: Intermech.Diagnostics.MessageReporterBase
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Diagnostics
{
    /// <summary>
    /// Базовый класс объектов для вывода многострочных сообщений в текстовый журнал.
    /// </summary>
    public abstract class MessageReporterBase : IMessageReporter
    {
      private readonly string[] LinesSplitter = new string[1]
      {
        Environment.NewLine
      };

      /// <summary>
      /// Выводит строку текста текущего сообщения. Вывод текста может быть отложен до момента, пока сообщение не будет завершено с помощью метода <see cref="M:EndMessage" />.
      /// </summary>
      /// <param name="text">Текст сообщения</param>
      /// <exception cref="T:ArgumentNullException">text</exception>
      public void WriteLine(string text)
      {
        if (text == null)
          throw new ArgumentNullException(nameof (text));
        if (text.EndsWith(Environment.NewLine))
          text = text.Remove(text.Length - Environment.NewLine.Length);
        if (text.Contains(Environment.NewLine))
        {
          foreach (string text1 in text.Split(this.LinesSplitter, StringSplitOptions.None))
            this.DoWriteLine(text1);
        }
        else
          this.DoWriteLine(text);
      }

      /// <summary>
      /// Выводит строку текста текущего сообщения. Вывод текста может быть отложен до момента, пока сообщение не будет завершено с помощью метода <see cref="M:EndMessage" />.
      /// </summary>
      /// <param name="text">Текст сообщения</param>
      protected abstract void DoWriteLine(string text);

      /// <summary>Завершает текущее сообщение.</summary>
      public void EndMessage() => this.DoEndMessage();

      /// <summary>Завершает текущее сообщение.</summary>
      protected virtual void DoEndMessage()
      {
      }
    }
}
