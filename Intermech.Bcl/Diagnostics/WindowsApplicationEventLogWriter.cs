
// Type: Intermech.Diagnostics.WindowsApplicationEventLogWriter
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Diagnostics;


namespace Intermech.Diagnostics
{
    /// <summary>
    /// Класс для записи в системный журнал событий Application.
    /// </summary>
    public sealed class WindowsApplicationEventLogWriter : EventLogWriterBase
    {
      private const string ApplicationLogName = "Application";
      private string sourceName;
      private bool isEventSourceRegistered;

      /// <summary>Создает объект.</summary>
      /// <param name="sourceName">Имя источника событий, отображаемое в журнале событий. Как правило, это название приложения</param>
      /// <exception cref="T:ArgumentException">Параметр <paramref name="sourceName" /> не должен быть равен null или пустой строке</exception>
      public WindowsApplicationEventLogWriter(string sourceName)
      {
        this.sourceName = !string.IsNullOrEmpty(sourceName) ? sourceName : throw new ArgumentException("Не задано имя источника событий.", nameof (sourceName));
      }

      /// <summary>Записывает в журнал новое событие.</summary>
      /// <param name="message">Текст сообщения</param>
      /// <param name="itemType">Тип события</param>
      protected override void DoWriteMessage(string message, EventLogItemType itemType)
      {
        base.DoWriteMessage(message, itemType);
        this.LazyRegisterEventSource();
        using (EventLog eventLog = new EventLog())
        {
          eventLog.Source = this.sourceName;
          eventLog.WriteEntry(message, this.RecordTypeToEntryType(itemType));
        }
      }

      /// <summary>Записывает в журнал новое событие.</summary>
      /// <param name="item">Запись о событии</param>
      protected override void DoWriteItem(EventLogItem item)
      {
        base.DoWriteItem(item);
        this.LazyRegisterEventSource();
        using (EventLog eventLog = new EventLog())
        {
          eventLog.Source = this.sourceName;
          eventLog.WriteEntry(item.MessageText, this.RecordTypeToEntryType(item.ItemType));
        }
      }

      private EventLogEntryType RecordTypeToEntryType(EventLogItemType recordType)
      {
        switch (recordType)
        {
          case EventLogItemType.Error:
            return EventLogEntryType.Error;
          case EventLogItemType.Warning:
            return EventLogEntryType.Warning;
          case EventLogItemType.Information:
            return EventLogEntryType.Information;
          default:
            throw new NotSupportedEnumException((Enum) recordType);
        }
      }

      private void LazyRegisterEventSource()
      {
        if (this.isEventSourceRegistered)
          return;
        this.isEventSourceRegistered = true;
        this.RegisterEventSource();
      }

      private void RegisterEventSource()
      {
        if (EventLog.SourceExists(this.sourceName) && EventLog.LogNameFromSourceName(this.sourceName, ".") != "Application")
          EventLog.DeleteEventSource(this.sourceName);
        if (EventLog.SourceExists(this.sourceName))
          return;
        EventLog.CreateEventSource(this.sourceName, "Application");
      }
    }
}
