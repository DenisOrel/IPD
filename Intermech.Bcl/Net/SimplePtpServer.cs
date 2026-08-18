
// Type: Intermech.Net.SimplePtpServer
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Threading;


namespace Intermech.Net
{
    /// <summary>
    /// Реализует сервер для высокоточной синхронизации времени. За основу взят протокол PTP, работающий в режиме peer-to-peer (IEEE 1588 Precision Time Protocol).
    /// </summary>
    public class SimplePtpServer : ISimplePtpServer
    {
      private readonly TimeSpan fakeOffset;

      /// <summary>Создает объект.</summary>
      public SimplePtpServer()
      {
      }

      /// <summary>
      /// Создает объект и позволяет указать дополнительное смещение времени сервера относительно системного времени. Используется для unit-тестирования.
      /// </summary>
      /// <param name="fakeOffset">Искусственное смещение времени сервера относительно системного времени</param>
      public SimplePtpServer(TimeSpan fakeOffset) => this.fakeOffset = fakeOffset;

      /// <summary>
      /// Обрабатывает запрос на получение задержки времени клиента относительно времени сервера.
      /// </summary>
      /// <param name="t1">Время отправки запроса от клиента в UTC</param>
      /// <returns>Ответ сервера</returns>
      public SimplePtpDelayResponse DelayRequest(DateTime t1)
      {
        DateTime t2 = DateTime.UtcNow + this.fakeOffset;
        Thread.Sleep(10);
        DateTime t3 = DateTime.UtcNow + this.fakeOffset;
        return new SimplePtpDelayResponse(t2, t3);
      }
    }
}
