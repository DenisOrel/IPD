
// Type: Intermech.Cache.ICacheManager
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Cache.Performance;


namespace Intermech.Cache
{
    /// <summary>
    /// Предоставляет доступ к содержимому кэша, позволяя добавлять в него
    /// элементы, получать их и удалять.
    /// </summary>
    public interface ICacheManager
    {
      /// <summary>
      /// Добавляет новый элемент в кэш. Если в кэше уже есть элемент с
      /// указанным ключем, то он будет предварительно удален.
      /// </summary>
      /// <param name="key">Уникальный ключ элемента в кэше</param>
      /// <param name="data">Элемент</param>
      void Add(object key, object data);

      /// <summary>
      /// Добавляет новый элемент в кэш. Если в кэше уже есть элемент с
      /// указанным ключем, то он будет предварительно удален.
      /// </summary>
      /// <param name="key">Уникальный ключ элемента в кэше</param>
      /// <param name="data">Элемент</param>
      /// <param name="beforeRemove">Делегат метода, который будет вызван перед удалением элемента из кэша</param>
      /// <param name="afterRemove">Делегат метода, который будет вызван после удаления элемента из кэша</param>
      void Add(
        object key,
        object data,
        BeforeRemoveEventHandler beforeRemove,
        AfterRemoveEventHandler afterRemove);

      /// <summary>
      /// Добавляет новый элемент в кэш. Если в кэше уже есть элемент с
      /// указанным ключем, то он будет предварительно удален.
      /// </summary>
      /// <param name="key">Уникальный ключ элемента в кэше</param>
      /// <param name="data">Элемент</param>
      /// <param name="expirations">Массив объектов, которые будут контролировать устаревание элемента</param>
      void Add(object key, object data, params IExpiration[] expirations);

      /// <summary>
      /// Добавляет новый элемент в кэш. Если в кэше уже есть элемент с
      /// указанным ключем, то он будет предварительно удален.
      /// </summary>
      /// <param name="key">Уникальный ключ элемента в кэше</param>
      /// <param name="data">Элемент</param>
      /// <param name="beforeRemove">Делегат метода, который будет вызван перед удалением элемента из кэша</param>
      /// <param name="afterRemove">Делегат метода, который будет вызван после удаления элемента из кэша</param>
      /// <param name="expirations">Массив объектов, которые будут контролировать устаревание элемента</param>
      void Add(
        object key,
        object data,
        BeforeRemoveEventHandler beforeRemove,
        AfterRemoveEventHandler afterRemove,
        params IExpiration[] expirations);

      /// <summary>Удалает указанный элемент из кэша.</summary>
      /// <param name="key">Уникальный ключ элемента в кэше</param>
      void Remove(object key);

      /// <summary>Очищает кэш, удаляя все помещенные в него элементы.</summary>
      void Flush();

      /// <summary>
      /// Возвращает элемент из кэша. Если элемент отсутствует в кэше, то результатом будет null.
      /// </summary>
      /// <param name="key">Уникальный ключ элемента</param>
      /// <returns>Элемент</returns>
      object this[object key] { get; }

      /// <summary>
      /// Возвращает элемент из кэша. Если элемент отсутствует в кэше, то результатом будет null.
      /// </summary>
      /// <param name="key">Уникальный ключ элемента в кэше</param>
      /// <returns>Элемент</returns>
      object GetData(object key);

      /// <summary>
      /// Возвращает массив ключей всех элементов, помещенных в кэш.
      /// </summary>
      /// <returns>Массив ключей элементов в кэше</returns>
      object[] GetKeys();

      /// <summary>
      /// Возвращает контейнер, содержащий метаданные для указанного элемента
      /// кэша.
      /// </summary>
      /// <param name="key">Уникальный ключ элемента в кэше</param>
      /// <returns>Контейнер с метаданными элемента</returns>
      CacheItem GetItem(object key);

      /// <summary>
      /// Возвращает коллекцию счетчиков производительности кэша, позволяющих оценивать
      /// эффективность его работы.
      /// </summary>
      PerformanceCounterCollection PerformanceCounters { get; }
    }
}
