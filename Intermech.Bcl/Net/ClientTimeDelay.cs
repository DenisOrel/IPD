
// Type: Intermech.Net.ClientTimeDelay
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Net
{
    /// <summary>
    /// Содержит результаты вычисления задержки времени клиента относительно времени сервера.
    /// </summary>
    public sealed class ClientTimeDelay
    {
      private static readonly ClientTimeDelay zero = new ClientTimeDelay();
      private readonly TimeSpan value;
      private readonly TimeSpan valueSD;
      private readonly TimeSpan netlag;
      private readonly TimeSpan netlagSD;
      private readonly TimeSpan rtt;

      internal ClientTimeDelay(
        TimeSpan value,
        TimeSpan valueSD,
        TimeSpan netlag,
        TimeSpan netlagSD,
        TimeSpan rtt)
      {
        this.value = value;
        this.valueSD = valueSD;
        this.netlag = netlag;
        this.netlagSD = netlagSD;
        this.rtt = rtt;
      }

      private ClientTimeDelay()
        : this(TimeSpan.Zero, TimeSpan.Zero, TimeSpan.Zero, TimeSpan.Zero, TimeSpan.Zero)
      {
      }

      /// <summary>
      /// Возвращает задержку времени клиента относительно времени сервера. Если эту задержку добавить к системному времени клиента, то время клиента и сервера будет
      /// синхронизировано.
      /// </summary>
      public TimeSpan Value => this.value;

      /// <summary>
      /// Возвращает среднеквадратичное отклонение для задержки времени клиента.
      /// </summary>
      public TimeSpan ValueSD => this.valueSD;

      /// <summary>
      /// Возвращает задержку сети - время прохождения сетевого пакета от клиента к серверу.
      /// </summary>
      public TimeSpan NetworkLag => this.netlag;

      /// <summary>
      /// Возвращает среднеквадратичное отклонение для задержки сети.
      /// </summary>
      public TimeSpan NetworkLagSD => this.netlagSD;

      /// <summary>
      /// Возвращает суммарное время, затраченное на обращения к серверу.
      /// </summary>
      public TimeSpan RoundtripTime => this.rtt;

      /// <summary>
      /// Возвращает пустой результат измерений. Он используется в случаях, когда действительное измерение еще не выполнялось.
      /// </summary>
      public static ClientTimeDelay Zero => ClientTimeDelay.zero;

      /// <summary>
      /// Возвращает строковое представление, где все временные величины выражены в миллисекундах.
      /// </summary>
      /// <param name="delay">Результы измерения задержки клиентского времени относительно серверного</param>
      /// <returns>Строковое представление</returns>
      public string ToMillisecondsText()
      {
        return $"задержка времени = {this.Value.TotalMilliseconds:0.000}мс (SD = {this.ValueSD.TotalMilliseconds:0.000}мс), задержка сети = {this.NetworkLag.TotalMilliseconds:0.000}мс (SD = {this.NetworkLagSD.TotalMilliseconds:0.000}мс), RTT = {this.RoundtripTime.TotalMilliseconds:0.000}мс";
      }
    }
}
