
// Type: Intermech.Cache.IMonitor
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml


namespace Intermech.Cache
{
    /// <summary>
    /// Позволяет реализовать различные схемы проверки устаревания элементов в
    /// кэше, основанные на периодическом опросе источника данных или получении
    /// событий обновления от источника данных.
    /// </summary>
    public interface IMonitor
    {
      /// <summary>
      /// Добавляет в список контролиремых монитором элементов новый элемент.
      /// </summary>
      /// <param name="key">Уникальный ключ элемента в кэше</param>
      /// <param name="expirations">Массив объектов, с помощью которых кэш определяет устаревание элементов</param>
      void Add(object key, IExpiration[] expirations);

      /// <summary>
      /// Удалает из списка контролируемых монитором элементов указанный элемент.
      /// </summary>
      /// <param name="key">Уникальный ключ элемента в кэше</param>
      void Remove(object key);

      /// <summary>Очищает список контролируемых монитором элементов.</summary>
      void Flush();
    }
}
