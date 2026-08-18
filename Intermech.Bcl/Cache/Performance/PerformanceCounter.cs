
// Type: Intermech.Cache.Performance.PerformanceCounter
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml


namespace Intermech.Cache.Performance
{
    /// <summary>
    /// Базовый класс для создания счетчиков производительности.
    /// </summary>
    public abstract class PerformanceCounter : IPerformanceCounter
    {
      private string categoryName;
      private string counterName;
      private string measure;
      private string description;

      /// <summary>Создает счетчик производительности.</summary>
      /// <param name="categoryName">Название категории счетчика</param>
      /// <param name="counterName">Название счетчика</param>
      /// <param name="measure">Единица измерения счетчика</param>
      /// <param name="description">Описание счетчика</param>
      public PerformanceCounter(
        string categoryName,
        string counterName,
        string measure,
        string description)
      {
        this.categoryName = categoryName;
        this.counterName = counterName;
        this.measure = measure;
        this.description = description;
      }

      /// <summary>Возвращает название категории счетчика.</summary>
      public string CategoryName => this.categoryName;

      /// <summary>Возвращает название счетчика.</summary>
      public string CounterName => this.counterName;

      /// <summary>Возвращает единицу измерения счетчика.</summary>
      public string Measure => this.measure;

      /// <summary>Возвращает описание счетчика.</summary>
      public string Description => this.description;

      /// <summary>Увечичивает значение счетчика на единицу.</summary>
      /// <returns>Результирующее значение счетчика</returns>
      public virtual long Increment() => this.IncrementBy(1L);

      /// <summary>Уменьшает значение счетчика на единицу.</summary>
      /// <returns>Результирующее значение счетчика</returns>
      public virtual long Decrement() => this.IncrementBy(-1L);

      /// <summary>
      /// Увеличивает и уменьшает значение счетчика на указанную величину.
      /// </summary>
      /// <param name="value">Величина для увеличения (или уменьшения, если оно отрицательное)</param>
      /// <returns>Результирующее значение счетчика</returns>
      public abstract long IncrementBy(long value);

      /// <summary>
      /// Возвращает или устанавливает непосредственное (т.е. нерассчитанное) значение счетчика.
      /// </summary>
      public abstract long RawValue { get; set; }

      /// <summary>
      /// Возвращает рассчитанное значение счетчика, пригодное для оценки производительности.
      /// </summary>
      public abstract double Value { get; }
    }
}
