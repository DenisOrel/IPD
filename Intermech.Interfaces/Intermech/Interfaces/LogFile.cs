
// Type: Intermech.Interfaces.LogFile
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.IO;


namespace Intermech.Interfaces
{
    /// <summary>Файловый лог</summary>
    public class LogFile : ILogFile
    {
      /// <summary>
      /// Записывать перед строкой сообщения время добавления его в лог
      /// </summary>
      private bool _insertTime;
      /// <summary>
      /// Записывать в лог строки информирующие об обкрытии и закрытии лога
      /// </summary>
      private bool _includeSysLines;
      /// <summary>Имя файла</summary>
      private string _fileName = string.Empty;

      /// <summary>Конструктор</summary>
      /// <param name="fileName">Полный путь и имя файла лога</param>
      public LogFile(string fileName)
        : this(fileName, false, false, false)
      {
      }

      /// <summary>Конструктор</summary>
      /// <param name="fileName">Полный путь и имя файла лога</param>
      /// <param name="insertTime">Записывать перед строкой сообщения время добавления его в лог</param>
      /// <param name="append">Определяет, требуется ли добавить в файл данные. Если файл существует и значение параметра append равно false, файл перезаписывается. Если файл существует и значение параметра append равно true, в файл добавляются данные. В противном случае создается новый файл.</param>
      /// <param name="includeSysLines">Записывать в лог строки информирующие об обкрытии и закрытии лога</param>
      public LogFile(string fileName, bool insertTime, bool append, bool includeSysLines)
      {
        using (StreamWriter streamWriter = new StreamWriter(fileName, append))
        {
          streamWriter.WriteLine($"------ Log File opened at {DateTime.UtcNow} UTC ------");
          this._insertTime = insertTime;
          this._includeSysLines = includeSysLines;
        }
        this._fileName = fileName;
      }

      /// <summary>Запись сообщения в лог</summary>
      /// <param name="message"></param>
      public void WriteMessage(string message)
      {
        using (StreamWriter streamWriter = new StreamWriter(this._fileName, true))
          streamWriter.WriteLine(this._insertTime ? $"{DateTime.UtcNow}: {message}" : message);
      }

      /// <summary>Закрытие файла лога</summary>
      public void Close()
      {
        using (StreamWriter streamWriter = new StreamWriter(this._fileName, true))
          streamWriter.WriteLine($"========== Log File closed at {DateTime.UtcNow} UTC ==========");
      }
    }
}
