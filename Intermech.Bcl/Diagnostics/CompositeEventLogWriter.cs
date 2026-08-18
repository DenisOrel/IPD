
// Type: Intermech.Diagnostics.CompositeEventLogWriter
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;


namespace Intermech.Diagnostics
{
    /// <summary>Класс составного объекта для записи в журнал событий.</summary>
    public sealed class CompositeEventLogWriter : EventLogWriterBase
    {
      private List<IEventLogWriter> writers;

      /// <summary>Создает объект.</summary>
      /// <param name="writers">Коллекция используемых писателей</param>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="writers" /> не должен быть равен null</exception>
      public CompositeEventLogWriter(IEnumerable<IEventLogWriter> writers)
      {
        this.writers = writers != null ? new List<IEventLogWriter>(writers) : throw new ArgumentNullException(nameof (writers));
      }

      /// <summary>Записывает в журнал новое событие.</summary>
      /// <param name="message">Текст сообщения</param>
      /// <param name="itemType">Тип события</param>
      protected override void DoWriteMessage(string message, EventLogItemType itemType)
      {
        base.DoWriteMessage(message, itemType);
        foreach (IEventLogWriter writer in this.writers)
          writer.Write(message, itemType);
      }

      /// <summary>Записывает в журнал новое событие.</summary>
      /// <param name="item">Запись о событии</param>
      protected override void DoWriteItem(EventLogItem item)
      {
        base.DoWriteItem(item);
        foreach (IEventLogWriter writer in this.writers)
          writer.WriteItem(item);
      }
    }
}
