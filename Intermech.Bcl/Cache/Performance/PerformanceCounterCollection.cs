
// Type: Intermech.Cache.Performance.PerformanceCounterCollection
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System.Collections;


namespace Intermech.Cache.Performance
{
    /// <summary>Реализует коллекцию счетчиков производительности.</summary>
    public class PerformanceCounterCollection : CollectionBase
    {
      /// <summary>Добавляет новый счетчик в коллекцию.</summary>
      /// <param name="counter">Счетчик производительности</param>
      public void Add(IPerformanceCounter counter)
      {
        lock (this)
          this.List.Add((object) counter);
      }

      /// <summary>Удаляет счетчик из коллекции.</summary>
      /// <param name="counter">Счетчик производительности</param>
      public void Remove(IPerformanceCounter counter)
      {
        lock (this)
          this.List.Remove((object) counter);
      }

      /// <summary>
      /// Возвращает true, если указанный счетчик есть в коллекции.
      /// </summary>
      /// <param name="counter">Счетчик производительности</param>
      /// <returns>Признак наличия счетчика в коллекции</returns>
      public bool Contains(IPerformanceCounter counter) => this.IndexOf(counter) >= 0;

      /// <summary>
      /// Возвращает порядковый номер счетчика в коллекции или -1, если
      /// счетчика нет в коллекции.
      /// </summary>
      /// <param name="counter">Счетчик производительности</param>
      /// <returns>Порядковый номер счетчика</returns>
      public int IndexOf(IPerformanceCounter counter)
      {
        lock (this)
          return this.List.IndexOf((object) counter);
      }

      /// <summary>
      /// Возвращает счетчик производительности по его порядковому номеру.
      /// </summary>
      public IPerformanceCounter this[int index]
      {
        get
        {
          lock (this)
            return (IPerformanceCounter) this.List[index];
        }
      }
    }
}
