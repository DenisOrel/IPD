
// Type: Intermech.Cache.Expirations.SlidingTime
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Cache.Expirations
{
    /// <summary>
    /// Реализует алгоритм устаревания элементов кэша, при котором элемент
    /// устаревает, если время между двумя последовательными обращениями к нему
    /// превышает заданный интервал.
    /// Проверка факта устаревания будет выполняться при каждом обращении к
    /// контролируемому элементу кэша.
    /// </summary>
    public class SlidingTime : IExpiration
    {
      /// <summary>
      /// Продолжительность интервала между двумя последовательными обращениями
      /// к контролируемому элементу кэша.
      /// </summary>
      private TimeSpan duration;
      /// <summary>Дата и время последнего обращения к элементу кэша.</summary>
      private DateTime lastAccessTimeUtc;

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
        this.lastAccessTimeUtc = DateTime.UtcNow;
      }

      /// <summary>
      /// Возвращает true, если контролируемый элемент кэша устарел.
      /// </summary>
      public bool HasExpired
      {
        get => this.lastAccessTimeUtc.Ticks + this.duration.Ticks < DateTime.UtcNow.Ticks;
      }

      /// <summary>
      /// Вызывается каждый раз, когда пользователь обращается к контролируемому
      /// элементу кэша.
      /// </summary>
      public void Notify() => this.lastAccessTimeUtc = DateTime.UtcNow;
    }
}
