
// Type: Intermech.Diagnostics.EventLogWriterBase
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Diagnostics;


namespace Intermech.Diagnostics
{
    /// <summary>Базовый класс объекта для записи в журнал событий.</summary>
    public class EventLogWriterBase : IEventLogWriter
    {
      private bool silentMode;

      /// <summary>Создает объект.</summary>
      public EventLogWriterBase() => this.silentMode = true;

      /// <summary>
      /// Возвращает или задает признак тихого режима работы, при котором подавляются все исключения при записи события в системный журнал.
      /// По умолчанию тихий режим включен.
      /// </summary>
      public bool SilentMode
      {
        [DebuggerStepThrough] get => this.silentMode;
        [DebuggerStepThrough] set => this.silentMode = value;
      }

      /// <summary>Записывает в журнал новое событие.</summary>
      /// <param name="message">Текст сообщения</param>
      public void Write(string message) => this.Write(message, EventLogItemType.Information);

      /// <summary>Записывает в журнал новое событие.</summary>
      /// <param name="message">Текст сообщения</param>
      /// <param name="itemType">Тип события</param>
      public void Write(string message, EventLogItemType itemType)
      {
        if (string.IsNullOrEmpty(message))
          return;
        try
        {
          this.DoWriteMessage(message, itemType);
        }
        catch (Exception ex)
        {
          if (!this.SilentMode)
            throw;
          SuppressedExceptions.TraceException(ex, "EventLogWriterBase.Write()");
        }
      }

      /// <summary>Записывает в журнал новое событие.</summary>
      /// <param name="item">Запись о событии</param>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="item" /> не должен быть равен null</exception>
      public void WriteItem(EventLogItem item)
      {
        if (item == null)
          throw new ArgumentNullException(nameof (item));
        if (string.IsNullOrEmpty(item.MessageText))
          return;
        try
        {
          this.DoWriteItem(item);
        }
        catch (Exception ex)
        {
          if (!this.SilentMode)
            throw;
          SuppressedExceptions.TraceException(ex, "EventLogWriterBase.WriteItem()");
        }
      }

      /// <summary>Записывает в журнал новое событие.</summary>
      /// <param name="message">Текст сообщения</param>
      /// <param name="itemType">Тип события</param>
      protected virtual void DoWriteMessage(string message, EventLogItemType itemType)
      {
      }

      /// <summary>Записывает в журнал новое событие.</summary>
      /// <param name="item">Запись о событии</param>
      protected virtual void DoWriteItem(EventLogItem item)
      {
      }
    }
}
