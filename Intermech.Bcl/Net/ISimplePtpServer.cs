
// Type: Intermech.Net.ISimplePtpServer
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Net
{
    /// <summary>
    /// Интерфейс сервера для высокоточной синхронизации времени. За основу взят протокол PTP, работающий в режиме peer-to-peer (IEEE 1588 Precision Time Protocol).
    /// </summary>
    public interface ISimplePtpServer
    {
      /// <summary>
      /// Обрабатывает запрос на получение задержки времени клиента относительно времени сервера.
      /// </summary>
      /// <param name="t1">Время отправки запроса от клиента в UTC</param>
      /// <returns>Ответ сервера</returns>
      SimplePtpDelayResponse DelayRequest(DateTime t1);
    }
}
