
// Type: Intermech.Interfaces.AddToTraceRecord
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Сообщение для записи в лог-файл сервера приложений.</summary>
    [Serializable]
    public sealed class AddToTraceRecord
    {
      /// <summary>Создает объект</summary>
      /// <param name="text">Текст сообщения</param>
      /// <param name="traceLevel">Уровень трассировки, при котором сообщение будет записано в файл</param>
      /// <param name="traceFileName">Имя файла трассировки</param>
      /// <param name="computerName">Имя компьютера, который записывает сообщение</param>
      /// <param name="userName">Имя пользователя, который записывает сообщение</param>
      public AddToTraceRecord(
        string text,
        int traceLevel,
        string traceFileName = null,
        string computerName = null,
        string userName = null)
      {
        this.Text = text;
        this.TraceLevel = traceLevel;
        this.TraceFileName = traceFileName;
        this.ComputerName = computerName;
        this.UserName = userName;
      }

      public string Text { get; private set; }

      public int TraceLevel { get; private set; }

      public string TraceFileName { get; private set; }

      public string ComputerName { get; private set; }

      public string UserName { get; private set; }
    }
}
