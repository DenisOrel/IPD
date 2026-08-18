
// Type: Intermech.Diagnostics.TextFileEventLogWriter
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Text;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;


namespace Intermech.Diagnostics
{
    /// <summary>
    /// Класс для записи в журнал событий в виде простого текстового файла.
    /// </summary>
    public class TextFileEventLogWriter : EventLogWriterBase, IDisposable
    {
      private string filePath;
      private StreamWriter writer;
      private bool isDisposed;

      /// <summary>Создает объект.</summary>
      /// <param name="filePath">Путь к файлу журнала событий</param>
      /// <exception cref="T:ArgumentException">Параметр <paramref name="filePath" /> не должен быть пуст или равен null</exception>
      public TextFileEventLogWriter(string filePath)
      {
        this.filePath = !string.IsNullOrEmpty(filePath) ? filePath : throw new ArgumentException("Не задать путь к файлу журнала событий.", nameof (filePath));
        this.writer = new StreamWriter((Stream) new FileStream(this.filePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite), Encoding.UTF8);
      }

      /// <summary>Освобождает ресурсы объекта.</summary>
      public void Dispose()
      {
        if (this.isDisposed)
          return;
        try
        {
          this.DisposeInternal();
        }
        finally
        {
          this.isDisposed = true;
        }
      }

      private void DisposeInternal()
      {
        if (this.writer == null)
          return;
        this.writer.Dispose();
        this.writer = (StreamWriter) null;
      }

      /// <summary>
      /// Возвращает признак, что ресурсы объекта были освобождены, а сам объект использовать больше нельзя.
      /// </summary>
      public bool IsDisposed
      {
        [DebuggerStepThrough] get => this.isDisposed;
        [DebuggerStepThrough] private set => this.isDisposed = value;
      }

      private void RequireNotDisposed()
      {
        if (this.IsDisposed)
          throw new ObjectDisposedException(this.GetType().FullName);
      }

      /// <summary>Записывает в журнал новое событие.</summary>
      /// <param name="message">Текст сообщения</param>
      /// <param name="itemType">Тип события</param>
      protected override void DoWriteMessage(string message, EventLogItemType itemType)
      {
        base.DoWriteMessage(message, itemType);
        this.WriteMessageTextLines(message, itemType);
        this.writer.Flush();
      }

      /// <summary>Записывает в журнал новое событие.</summary>
      /// <param name="item">Запись о событии</param>
      protected override void DoWriteItem(EventLogItem item)
      {
        base.DoWriteItem(item);
        this.WriteMessageTextLines(item.MessageText, item.ItemType);
        this.writer.Flush();
      }

      private void WriteMessageTextLines(string message, EventLogItemType itemType)
      {
        if (message.IndexOf('\n') >= 0)
        {
          this.WriteMultiLineMessageTextLines(message, itemType);
        }
        else
        {
          this.WriteFirstLinePrefix(itemType);
          this.writer.WriteLine(message);
        }
      }

      private void WriteMultiLineMessageTextLines(string message, EventLogItemType itemType)
      {
        bool flag = true;
        foreach (StringView enumerateTextLine in message.EnumerateTextLines(Environment.NewLine))
        {
          if (enumerateTextLine.StartIndex == message.Length && enumerateTextLine.IsEmpty)
            break;
          if (flag)
          {
            flag = false;
            this.WriteFirstLinePrefix(itemType);
          }
          else
            this.writer.Write('\t');
          this.WriteMultiLineMessageLine(message, enumerateTextLine);
          this.writer.WriteLine();
        }
      }

      private void WriteMultiLineMessageLine(string message, StringView lineView)
      {
        int num = lineView.StartIndex + lineView.Length - 1;
        for (int startIndex = lineView.StartIndex; startIndex <= num; ++startIndex)
          this.writer.Write(message[startIndex]);
      }

      private void WriteFirstLinePrefix(EventLogItemType itemType)
      {
        this.writer.Write((object) DateTime.Now.TruncateToSecond());
        this.writer.Write(',');
        this.writer.Write(' ');
        this.writer.Write(this.EventLogItemTypeToString(itemType));
        this.writer.Write('\t');
      }

      private string EventLogItemTypeToString(EventLogItemType itemType)
      {
        switch (itemType)
        {
          case EventLogItemType.Error:
            return "Error";
          case EventLogItemType.Warning:
            return "Warning";
          case EventLogItemType.Information:
            return "Info";
          default:
            throw new NotSupportedEnumException((Enum) itemType);
        }
      }
    }
}
