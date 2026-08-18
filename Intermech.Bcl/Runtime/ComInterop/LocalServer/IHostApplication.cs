
// Type: Intermech.Runtime.ComInterop.LocalServer.IHostApplication
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;


namespace Intermech.Runtime.ComInterop.LocalServer
{
    /// <summary>
    /// Интерфейс объекта для связи COM-сервера с приложением.
    /// </summary>
    public interface IHostApplication
    {
      /// <summary>Возвращает идентификатор приложения COM-сервера.</summary>
      Guid HostId { get; }

      /// <summary>
      /// Возвращает путь к исполняемому файлу приложения COM-сервера в абсолютной форме.
      /// </summary>
      string ExecutablePath { get; }

      /// <summary>
      /// Возвращает коллекцию аргументов запуска приложения COM-сервера.
      /// </summary>
      ICollection<string> GetCommandLineArguments();
    }
}
