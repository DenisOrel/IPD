
// Type: Intermech.Diagnostics.IEventLogWriter
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml


namespace Intermech.Diagnostics
{
    /// <summary>
    /// Интерфейс объекта для записи сообщений в журнал событий.
    /// </summary>
    public interface IEventLogWriter
    {
      /// <summary>Записывает в журнал новое событие.</summary>
      /// <param name="message">Текст сообщения</param>
      void Write(string message);

      /// <summary>Записывает в журнал новое событие.</summary>
      /// <param name="message">Текст сообщения</param>
      /// <param name="itemType">Тип события</param>
      void Write(string message, EventLogItemType itemType);

      /// <summary>Записывает в журнал новое событие.</summary>
      /// <param name="item">Запись о событии</param>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="item" /> не должен быть равен null</exception>
      void WriteItem(EventLogItem item);
    }
}
