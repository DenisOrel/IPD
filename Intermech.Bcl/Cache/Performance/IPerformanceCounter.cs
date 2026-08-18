
// Type: Intermech.Cache.Performance.IPerformanceCounter
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml


namespace Intermech.Cache.Performance
{
    /// <summary>Позволяет реализовать счетчик производительности.</summary>
    public interface IPerformanceCounter
    {
      /// <summary>Возвращает название категории счетчика.</summary>
      string CategoryName { get; }

      /// <summary>Возвращает название счетчика.</summary>
      string CounterName { get; }

      /// <summary>Возвращает единицу измерения счетчика.</summary>
      string Measure { get; }

      /// <summary>Возвращает описание счетчика.</summary>
      string Description { get; }

      /// <summary>Увечичивает значение счетчика на единицу.</summary>
      /// <returns>Результирующее значение счетчика</returns>
      long Increment();

      /// <summary>Уменьшает значение счетчика на единицу.</summary>
      /// <returns>Результирующее значение счетчика</returns>
      long Decrement();

      /// <summary>
      /// Увеличивает и уменьшает значение счетчика на указанную величину.
      /// </summary>
      /// <param name="value">Величина для увеличения (или уменьшения, если оно отрицательное)</param>
      /// <returns>Результирующее значение счетчика</returns>
      long IncrementBy(long value);

      /// <summary>
      /// Возвращает или устанавливает непосредственное (т.е. нерассчитанное) значение счетчика.
      /// </summary>
      long RawValue { get; set; }

      /// <summary>
      /// Возвращает рассчитанное значение счетчика, пригодное для оценки производительности.
      /// </summary>
      double Value { get; }
    }
}
