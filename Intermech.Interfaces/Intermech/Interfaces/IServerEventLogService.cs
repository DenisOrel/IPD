
// Type: Intermech.Interfaces.IServerEventLogService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса для записи сообщений в лог-файлы сервера приложений.
    /// </summary>
    public interface IServerEventLogService
    {
      /// <summary>Записывает сообщение в лог-файл сервера приложений.</summary>
      /// <param name="text">Текст сообщения</param>
      /// <param name="traceFileName">Имя файла трассировки</param>
      void AddToTrace(string text, string traceFileName = null);

      /// <summary>Записывает сообщение в лог-файл сервера приложений.</summary>
      /// <param name="text">Текст сообщения</param>
      /// <param name="traceLevel">Уровень трассировки, при котором сообщение будет записано в файл</param>
      /// <param name="traceFileName">Имя файла трассировки</param>
      void AddToTrace(string text, int traceLevel, string traceFileName = null);
    }
}
