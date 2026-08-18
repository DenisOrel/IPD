
// Type: Intermech.Diagnostics.EventLogWriters
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Diagnostics;


namespace Intermech.Diagnostics
{
    /// <summary>
    /// Фабрика объектов для записи в журналы событий приложения различной природы.
    /// </summary>
    public static class EventLogWriters
    {
      private static readonly NullEventLogWriter nullWriter = new NullEventLogWriter();

      /// <summary>Создает объект для записи событий в текстовый файл.</summary>
      /// <param name="filePath">Путь к файлу журнала событий</param>
      /// <returns>Объект для записи событий в текстовый файл</returns>
      /// <exception cref="T:ArgumentException">Параметр <paramref name="filePath" /> не должен быть пуст или равен null</exception>
      public static TextFileEventLogWriter CreateTextFileWriter(string filePath)
      {
        return new TextFileEventLogWriter(filePath);
      }

      /// <summary>
      /// Создает объект для записи событий в журнал операционной системы.
      /// </summary>
      /// <param name="eventLogType">Тип системного журнала событий</param>
      /// <param name="sourceName">Имя источника событий, отображаемое в журнале событий. Как правило, это название приложения</param>
      /// <returns>Объект для записи событий в журнал операционной системы</returns>
      /// <exception cref="T:ArgumentException">Параметр <paramref name="sourceName" /> не должен быть равен null или пустой строке</exception>
      public static IEventLogWriter CreateSystemLogWriter(
        SystemEventLogType eventLogType,
        string sourceName)
      {
        if (eventLogType != SystemEventLogType.Application)
          throw new NotSupportedEnumException((Enum) eventLogType);
        WindowsApplicationEventLogWriter writer = new WindowsApplicationEventLogWriter(sourceName);
        EventLogWriters.ConfigureSystemLogWriter(writer);
        return (IEventLogWriter) writer;
      }

      private static void ConfigureSystemLogWriter(WindowsApplicationEventLogWriter writer)
      {
        writer.SilentMode = true;
      }

      /// <summary>
      /// Создает обертку вокруг указанного объекта, делая его потокобезопасным.
      /// </summary>
      /// <param name="eventLogWriter">Объект для записи в журнал событий приложения</param>
      /// <returns>Потокобезопасный объект для записи в журнал событий приложения</returns>
      public static EventLogWriterSyncWrapper Synchronized(IEventLogWriter eventLogWriter)
      {
        return new EventLogWriterSyncWrapper(eventLogWriter);
      }

      /// <summary>
      /// Возвращает объект для записи в журнал событий, который можно использовать в качестве заглушки, если запись в журнал событий не требуется.
      /// </summary>
      public static IEventLogWriter Null
      {
        [DebuggerStepThrough] get => (IEventLogWriter) EventLogWriters.nullWriter;
      }
    }
}
