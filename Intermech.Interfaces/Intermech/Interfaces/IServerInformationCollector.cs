
// Type: Intermech.Interfaces.IServerInformationCollector
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Collections.Generic;
using System.IO;


namespace Intermech.Interfaces
{
    /// <summary>Служба для сбора информации на серверной стороне.</summary>
    public interface IServerInformationCollector
    {
      /// <summary>
      /// собрать информацию с сервера для отправки в техподдержку
      /// </summary>
      /// <returns></returns>
      InformationNode CollectServerInformation();

      /// <summary>получить список имён лог файлов на сервере</summary>
      /// <returns></returns>
      List<FileInfo> LogFiles();

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      byte[] ReadLogFile(string logFileName);

      /// <summary>
      /// Обрезать файл лога до размера в 1 МБ и вернуть на клиент готовый массив байт
      /// </summary>
      /// <param name="logFileName"></param>
      /// <returns></returns>
      byte[] TruncateLogFile(string logFileName);
    }
}
