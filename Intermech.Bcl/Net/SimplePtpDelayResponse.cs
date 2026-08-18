
// Type: Intermech.Net.SimplePtpDelayResponse
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Net
{
    /// <summary>
    /// Реализует ответ сервера на запрос задержки времени клиента относительно времени сервера.
    /// </summary>
    [Serializable]
    public sealed class SimplePtpDelayResponse
    {
      private readonly DateTime t2;
      private readonly DateTime t3;

      /// <summary>Создает объект.</summary>
      /// <param name="t2">Время получения сервером запроса от клиента в UTC</param>
      /// <param name="t3">Время отправки сервером ответа клиенту в UTC</param>
      public SimplePtpDelayResponse(DateTime t2, DateTime t3)
      {
        this.t2 = t2;
        this.t3 = t3;
      }

      /// <summary>Время получения сервером запроса от клиента в UTC.</summary>
      public DateTime T2 => this.t2;

      /// <summary>Время отправки сервером ответа клиенту в UTC.</summary>
      public DateTime T3 => this.t3;
    }
}
