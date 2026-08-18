
// Type: Intermech.Net.SimplePtpClient
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Collections;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;


namespace Intermech.Net
{
    /// <summary>
    /// Реализует клиент для высокоточной синхронизации времени. За основу взят протокол PTP, работающий в режиме peer-to-peer (IEEE 1588 Precision Time Protocol).
    /// </summary>
    public class SimplePtpClient
    {
      private ClientTimeDelay result;

      /// <summary>Создает объект.</summary>
      public SimplePtpClient() => this.Reset();

      /// <summary>Возвращает результаты измерения.</summary>
      public ClientTimeDelay Result
      {
        [DebuggerStepThrough] get => this.result;
      }

      /// <summary>
      /// Очищает объект, удаляя результаты предыдущих обращений к серверу.
      /// </summary>
      public void Reset() => this.result = ClientTimeDelay.Zero;

      /// <summary>
      /// Выполняет обращение к серверу и расчитывает мгновенное задержку времени клиента относительно серверного времени, а также задержку сети.
      /// Вычисление выполняется за одно обращение к серверу.
      /// </summary>
      /// <param name="server">Объект сервера</param>
      public void CalculateInstantDelay(ISimplePtpServer server)
      {
        if (server == null)
          throw new ArgumentNullException(nameof (server));
        Thread.Yield();
        long timestamp1 = Stopwatch.GetTimestamp();
            Timestamps makeRequest = this.MakeRequests(server, 1)[0];
        long timestamp2 = Stopwatch.GetTimestamp();
        TimeSpan delay = SimplePtpClient.CalculateDelay(makeRequest);
        TimeSpan netlag = SimplePtpClient.CalculateNetlag(makeRequest);
        TimeSpan rtt = TimeSpan.FromSeconds((double) (timestamp2 - timestamp1) / (double) Stopwatch.Frequency);
        this.result = new ClientTimeDelay(delay, TimeSpan.Zero, netlag, TimeSpan.Zero, rtt);
      }

      /// <summary>
      /// Выполняет обращение к серверу и расчитывает задержку времени клиента относительно серверного времени, а также задержку сети.
      /// усредняя значения нескольких обращений к серверу.
      /// </summary>
      /// <param name="server">Объект сервера</param>
      public void CalculateMeanDelay(ISimplePtpServer server)
      {
        if (server == null)
          throw new ArgumentNullException(nameof (server));
        Thread.Yield();
        long timestamp1 = Stopwatch.GetTimestamp();
        List<Timestamps> timestampsList = this.MakeRequests(server, 7);
        long timestamp2 = Stopwatch.GetTimestamp();
        List<TimeSpan> timeSpanList1 = timestampsList.ConvertAll(new Converter<Timestamps, TimeSpan>(SimplePtpClient.CalculateDelay));
        List<TimeSpan> timeSpanList2 = timestampsList.ConvertAll(new Converter<Timestamps, TimeSpan>(SimplePtpClient.CalculateNetlag));
        TimeSpan meanTime = SimplePtpClient.Shorten(CollectionUtils.FoldLeft(TimeSpan.Zero, (IEnumerable<TimeSpan>) timeSpanList1, (Func<TimeSpan, TimeSpan, TimeSpan>) ((acc, value) => acc + value)), (double) timeSpanList1.Count);
        TimeSpan timeSpan = SimplePtpClient.Shorten(CollectionUtils.FoldLeft(TimeSpan.Zero, (IEnumerable<TimeSpan>) timeSpanList2, (Func<TimeSpan, TimeSpan, TimeSpan>) ((acc, value) => acc + value)), (double) timeSpanList2.Count);
        TimeSpan sd1 = SimplePtpClient.CalculateSD((ICollection<TimeSpan>) timeSpanList1, meanTime);
        TimeSpan sd2 = SimplePtpClient.CalculateSD((ICollection<TimeSpan>) timeSpanList2, timeSpan);
        TimeSpan rtt = TimeSpan.FromSeconds((double) (timestamp2 - timestamp1) / (double) Stopwatch.Frequency);
        this.result = new ClientTimeDelay(meanTime, sd1, timeSpan, sd2, rtt);
      }

      private List<Timestamps> MakeRequests(ISimplePtpServer server, int count)
      {
        List<Timestamps> timestampsList = new List<Timestamps>(count);
        for (int index = 0; index < timestampsList.Capacity; ++index)
        {
          DateTime utcNow1 = DateTime.UtcNow;
          SimplePtpDelayResponse ptpDelayResponse = server.DelayRequest(utcNow1);
          DateTime utcNow2 = DateTime.UtcNow;
                Timestamps timestamps = new Timestamps(utcNow1, ptpDelayResponse.T2, ptpDelayResponse.T3, utcNow2);
          timestampsList.Add(timestamps);
        }
        return timestampsList;
      }

      private static TimeSpan CalculateDelay(Timestamps tsPacket)
      {
        return SimplePtpClient.Shorten(tsPacket.T2 - tsPacket.T1 - (tsPacket.T4 - tsPacket.T3), 2.0);
      }

      private static TimeSpan CalculateNetlag(Timestamps tsPacket)
      {
        return SimplePtpClient.Shorten(tsPacket.T2 - tsPacket.T1 + (tsPacket.T4 - tsPacket.T3), 2.0);
      }

      private static TimeSpan CalculateSD(ICollection<TimeSpan> items, TimeSpan meanTime)
      {
        return TimeSpan.FromTicks((long) Math.Round(Math.Sqrt((double) CollectionUtils.FoldLeft(0L, (IEnumerable<TimeSpan>) items, (Func<long, TimeSpan, long>) ((acc, value) => acc + (value.Ticks - meanTime.Ticks) * (value.Ticks - meanTime.Ticks))) / (double) items.Count)));
      }

      private static TimeSpan Shorten(TimeSpan value, double times)
      {
        return TimeSpan.FromTicks((long) Math.Round((double) value.Ticks / times));
      }

      private sealed class Timestamps
      {
        public readonly DateTime T1;
        public readonly DateTime T2;
        public readonly DateTime T3;
        public readonly DateTime T4;

        public Timestamps(DateTime t1, DateTime t2, DateTime t3, DateTime t4)
        {
          this.T1 = t1;
          this.T2 = t2;
          this.T3 = t3;
          this.T4 = t4;
        }
      }
    }
}
