
// Type: Intermech.Cache.Expirations.Polled.AbsoluteTime
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Cache.Expirations.Polled
{
    /// <summary>
    /// Реализует алгоритм устаревания элементов кэша, основанные на задании
    /// точного момента времени, когда элемент должен считаться устаревшим.
    /// Проверка факта устаревания будет выполняться с некоторой периодичностью
    /// монитором кэша, выполняющимся в фоновом потоке.
    /// </summary>
    public class AbsoluteTime : IExpiration, IPolledExpiration
    {
      /// <summary>
      /// Дата и время, указывающие момент устаревания элемента кэша.
      /// </summary>
      private DateTime absoluteTime;
      /// <summary>Признак того, что элемент устарел.</summary>
      private bool expired;

      /// <summary>
      /// Создает объект, который будет контролировать устаревание элемента
      /// кэша, основываясь на знание момента времени, когда элемент можно
      /// считать устаревшим.
      /// </summary>
      /// <param name="dateTime">Дата и время устаревания элемента кэша</param>
      public AbsoluteTime(DateTime dateTime)
      {
        this.absoluteTime = !(dateTime <= DateTime.Now) ? dateTime.ToUniversalTime() : throw new ArgumentOutOfRangeException(nameof (dateTime), Resources.GetString("E_AbsoluteDateTime"));
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
      public void Notify()
      {
      }

      /// <summary>
      /// Выполняет проверку условия, от которого зависит устаревание элемента.
      /// </summary>
      public void CheckExpired() => this.expired = DateTime.UtcNow.Ticks > this.absoluteTime.Ticks;
    }
}
