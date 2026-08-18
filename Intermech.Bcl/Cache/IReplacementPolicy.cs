
// Type: Intermech.Cache.IReplacementPolicy
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml


namespace Intermech.Cache
{
    /// <summary>
    /// Позволяет реализовать различные алгоритмы замещения объектов в кэше для
    /// поддержания фиксированного размера кэша.
    /// </summary>
    public interface IReplacementPolicy
    {
      /// <summary>
      /// Добавляет новый элемент кэша в список элементов, которые
      /// должны обрабатываться алгоритмом.
      /// </summary>
      /// <param name="key">Уникальный ключ элемента в кэше</param>
      /// <param name="data">Элемент</param>
      /// <param name="usedSpace">Объем, который данные занимают в хранилище</param>
      void Add(object key, object data, long usedSpace);

      /// <summary>
      /// Удаляет элемент кэша с указанным ключем из списка элементов,
      /// которые должны обрабатываться алгоритмом.
      /// </summary>
      /// <param name="key">Уникальный ключ элемента в кэше</param>
      void Remove(object key);

      /// <summary>
      /// Удаляет все элементы из списка элементов, обрабатываемых
      /// алгоритмом.
      /// </summary>
      void Flush();

      /// <summary>
      /// Уведомляет алгоритм, что к элементу кэша с указанным ключем
      /// было обращение.
      /// </summary>
      /// <param name="key">Уникальный ключ элемента в кэше</param>
      void Notify(object key);

      /// <summary>
      /// Возвращает ключ элемента, который может быт удален из
      /// заполненного кэша, для того чтобы освободить место для нового
      /// элемента.
      /// </summary>
      /// <returns>Ключ элемента</returns>
      object GetKeyForEvict();
    }
}
