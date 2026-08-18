
// Type: Intermech.Interfaces.ILogFile
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>Интерфейс на файловый лог</summary>
    public interface ILogFile
    {
      /// <summary>Запись сообщения в лог</summary>
      /// <param name="message">Сообщение</param>
      void WriteMessage(string message);

      /// <summary>Закрывает лог</summary>
      void Close();
    }
}
