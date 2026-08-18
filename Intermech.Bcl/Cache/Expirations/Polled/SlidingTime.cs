
// Type: Intermech.Cache.Expirations.Polled.SlidingTime
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Cache.Expirations.Polled
{
    /// <summary>
    /// Реализует алгоритм устаревания элементов кэша, при котором элемент
    /// устаревает, если время между двумя последовательными обращениями к нему
    /// превышает заданный интервал.
    /// Проверка факта устаревания будет выполняться с некоторой периодичностью
    /// монитором кэша, выполняющимся в фоновом потоке.
    /// </summary>
    public class SlidingTime : IExpiration, IPolledExpiration
    {
      /// <summary>
      /// Продолжительность интервала между двумя последовательными обращениями
      /// к контролируемому элементу кэша.
      /// </summary>
      private TimeSpan duration;
      /// <summary>Дата и время последнего обращения к элементу кэша.</summary>
      private DateTime lastAccessTime;
      /// <summary>Признак того, что контролируемый элемент устарел.</summary>
      private bool expired;

      /// <summary>
      /// Создает объект, который будет контролировать устаревание элемента
      /// кэша.
      /// </summary>
      /// <param name="duration">
      /// Продолжительность интервала между двумя последовательными обращениями
      /// к контролируемому элементу кэша
      /// </param>
      public SlidingTime(TimeSpan duration)
      {
        this.duration = duration.Ticks >= 1L ? duration : throw new ArgumentOutOfRangeException(nameof (duration), Resources.GetString("E_SlidingDuration"));
        this.lastAccessTime = DateTime.UtcNow;
        this.expired = false;
      }

      /// <summary>
      /// Возвращает true, если контролируемый элемент кэша устарел.
      /// </summary>
      public bool HasExpired => this.expired;

      /// <summary>
      /// Вызывается каждый раз, когда пользователь обращается к контролируемому
      /// элементу кэша.
      /// </summary>
      public void Notify() => this.lastAccessTime = DateTime.UtcNow;

      /// <summary>
      /// Выполняет проверку условия, от которого зависит устаревание элемента.
      /// </summary>
      public void CheckExpired()
      {
        this.expired = this.lastAccessTime.Ticks + this.duration.Ticks < DateTime.UtcNow.Ticks;
      }
    }
}
