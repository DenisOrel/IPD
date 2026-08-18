
// Type: Intermech.Cache.Performance.AverageNumberOfItems
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Cache.Performance
{
    /// <summary>
    /// Реализует счетчик производительности, который показывает среднее значение количества или
    /// объема ресурса.
    /// </summary>
    public class AverageNumberOfItems : PerformanceCounter, IPerformanceCounter
    {
      private long startTime;
      private long currValue;

      /// <summary>Создает счетчик производительности</summary>
      /// <param name="categoryName">Название категории счетчика</param>
      /// <param name="counterName">Название счетчика</param>
      /// <param name="measure">Единица измерения счетчика</param>
      /// <param name="description">Описание счетчика</param>
      public AverageNumberOfItems(
        string categoryName,
        string counterName,
        string measure,
        string description)
        : base(categoryName, counterName, measure, description)
      {
        this.startTime = (long) Environment.TickCount;
        this.currValue = 0L;
      }

      /// <summary>
      /// Увеличивает и уменьшает значение счетчика на указанную величину.
      /// </summary>
      /// <param name="value">Величина для увеличения (или уменьшения, если оно отрицательное)</param>
      /// <returns>Результирующее значение счетчика</returns>
      public override long IncrementBy(long value)
      {
        lock (this)
        {
          this.currValue += value;
          return this.currValue;
        }
      }

      /// <summary>
      /// Возвращает или устанавливает непосредственное (т.е. нерассчитанное) значение счетчика.
      /// </summary>
      public override long RawValue
      {
        get
        {
          lock (this)
            return this.currValue;
        }
        set
        {
          lock (this)
            this.currValue = value;
        }
      }

      /// <summary>
      /// Возвращает рассчитанное значение счетчика, пригодное для оценки производительности.
      /// </summary>
      public override double Value
      {
        get
        {
          lock (this)
          {
            double num = (double) Math.Abs((long) Environment.TickCount - this.startTime);
            return num == 0.0 ? (double) this.currValue : (double) this.currValue / num * 1000.0;
          }
        }
      }
    }
}
