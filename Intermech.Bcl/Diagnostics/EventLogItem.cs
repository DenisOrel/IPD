
// Type: Intermech.Diagnostics.EventLogItem
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Diagnostics;


namespace Intermech.Diagnostics
{
    /// <summary>Запись о событии для журнала событий.</summary>
    public class EventLogItem
    {
      private string messageText;
      private EventLogItemType itemType;

      /// <summary>Создает объект.</summary>
      public EventLogItem()
      {
        this.messageText = string.Empty;
        this.itemType = EventLogItemType.Information;
      }

      /// <summary>Создает объект.</summary>
      /// <param name="messageText">Текст сообщения</param>
      /// <param name="itemType">Тип события</param>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="messageText" /> не должен быть равен null</exception>
      public EventLogItem(string messageText, EventLogItemType itemType)
      {
        this.messageText = messageText != null ? messageText : throw new ArgumentNullException(nameof (messageText));
        this.itemType = itemType;
      }

      /// <summary>Возвращает или задает текст сообщения.</summary>
      /// <exception cref="T:ArgumentNullException">Текст сообщения не должен быть равен null</exception>
      public string MessageText
      {
        [DebuggerStepThrough] get => this.messageText;
        [DebuggerStepThrough] set
        {
          this.messageText = value != null ? value : throw new ArgumentNullException(nameof (value));
        }
      }

      /// <summary>Возвращает или задает тип записи в журнале событий.</summary>
      public EventLogItemType ItemType
      {
        [DebuggerStepThrough] get => this.itemType;
        [DebuggerStepThrough] set => this.itemType = value;
      }
    }
}
