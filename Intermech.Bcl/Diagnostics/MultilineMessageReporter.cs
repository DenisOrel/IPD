
// Type: Intermech.Diagnostics.MultilineMessageReporter
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;


namespace Intermech.Diagnostics
{
    /// <summary>
    /// Реализует декоратор, позволяющий визуально отделить многострочное сообщение от окружающего текста.
    /// Если сообщение состоит из одной строки, то просто выводится эта строка. Если же сообщение состоит из нескольких строк, то
    /// вторая и последующие строки выводятся со смещением, а после сообщения добавляется разделитель.
    /// </summary>
    public class MultilineMessageReporter : MessageReporterBase
    {
      private IMessageReporter messageReporter;
      private List<string> textBuffer;

      /// <summary>Создает объект.</summary>
      /// <param name="messageReporter">Объект для вывода многострочных сообщений</param>
      /// <exception cref="T:System.ArgumentNullException">messageReporter</exception>
      public MultilineMessageReporter(IMessageReporter messageReporter)
      {
        this.messageReporter = messageReporter != null ? messageReporter : throw new ArgumentNullException(nameof (messageReporter));
        this.textBuffer = new List<string>(8);
      }

      /// <summary>
      /// Выводит строку текста текущего сообщения. Вывод текста может быть отложен до момента, пока сообщение не будет завершено с помощью метода <see cref="M:EndMessage" />.
      /// </summary>
      /// <param name="text">Текст сообщения</param>
      protected override void DoWriteLine(string text) => this.textBuffer.Add(text);

      /// <summary>Завершает текущее сообщение.</summary>
      protected override void DoEndMessage()
      {
        base.DoEndMessage();
        if (this.textBuffer.Count == 0)
          return;
        try
        {
          this.WriteMessage();
        }
        finally
        {
          this.textBuffer.Clear();
        }
      }

      private void WriteMessage()
      {
            MessageFormat notSupportedValue = this.DetectMessageFormat();
        switch (notSupportedValue)
        {
          case MultilineMessageReporter.MessageFormat.Singleline:
            this.WriteSingleLineMessage();
            break;
          case MultilineMessageReporter.MessageFormat.SimpleMultiline:
            this.WriteSimpleMultilineMessage();
            break;
          case MultilineMessageReporter.MessageFormat.RichMultiline:
            this.WriteRichMultilineMessage();
            break;
          default:
            throw new NotSupportedEnumException((Enum) notSupportedValue);
        }
      }

      private MessageFormat DetectMessageFormat()
      {
        if (this.textBuffer.Count == 1)
          return MultilineMessageReporter.MessageFormat.Singleline;
        return this.textBuffer.Count < 5 && !this.MessageHasDelimiters() ? MultilineMessageReporter.MessageFormat.SimpleMultiline : MultilineMessageReporter.MessageFormat.RichMultiline;
      }

      private bool MessageHasDelimiters()
      {
        foreach (string str in this.textBuffer)
        {
          if (string.IsNullOrEmpty(str.Trim()))
            return true;
        }
        return false;
      }

      private void WriteSingleLineMessage() => this.messageReporter.WriteLine(this.textBuffer[0]);

      private void WriteSimpleMultilineMessage()
      {
        this.messageReporter.WriteLine(this.textBuffer[0]);
        for (int index = 1; index < this.textBuffer.Count; ++index)
          this.messageReporter.WriteLine("  " + this.textBuffer[index]);
      }

      private void WriteRichMultilineMessage()
      {
        this.messageReporter.WriteLine(">> " + this.textBuffer[0]);
        for (int index = 1; index < this.textBuffer.Count; ++index)
          this.messageReporter.WriteLine("   " + this.textBuffer[index]);
        this.messageReporter.WriteLine("");
        this.messageReporter.WriteLine($"======= Конец сообщения =======");
        this.messageReporter.WriteLine("");
      }

      private enum MessageFormat
      {
        Singleline,
        SimpleMultiline,
        RichMultiline,
      }
    }
}
