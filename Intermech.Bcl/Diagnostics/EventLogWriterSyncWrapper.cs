
// Type: Intermech.Diagnostics.EventLogWriterSyncWrapper
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Diagnostics
{
    /// <summary>
    /// Обертка для объектов типа IEventLogWriter, позволяющая сделать их thread safe.
    /// </summary>
    public sealed class EventLogWriterSyncWrapper : EventLogWriterBase
    {
      private IEventLogWriter writer;
      private object syncRoot;

      /// <summary>Создает объект.</summary>
      /// <param name="writer">Писатель, которого необходимо сделать thread safe</param>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="writer" /> не должен быть равен null</exception>
      public EventLogWriterSyncWrapper(IEventLogWriter writer)
      {
        this.writer = writer != null ? writer : throw new ArgumentNullException(nameof (writer));
        this.syncRoot = new object();
      }

      /// <summary>Возвращает объект, скрытый за оберткой.</summary>
      /// <returns>Объект, скрытый за оберткой</returns>
      public IEventLogWriter Unwrap() => this.writer;

      /// <summary>Записывает в журнал новое событие.</summary>
      /// <param name="message">Текст сообщения</param>
      /// <param name="itemType">Тип события</param>
      protected override void DoWriteMessage(string message, EventLogItemType itemType)
      {
        base.DoWriteMessage(message, itemType);
        lock (this.syncRoot)
          this.writer.Write(message, itemType);
      }

      /// <summary>Записывает в журнал новое событие.</summary>
      /// <param name="item">Запись о событии</param>
      protected override void DoWriteItem(EventLogItem item)
      {
        base.DoWriteItem(item);
        lock (this.syncRoot)
          this.writer.WriteItem(item);
      }
    }
}
