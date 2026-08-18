
// Type: Intermech.Cache.CacheManager
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Cache.Performance;
using System;
using System.Collections;
using System.Collections.Specialized;


namespace Intermech.Cache
{
    /// <summary>Реализует менеджер кэша.</summary>
    public class CacheManager : ICacheManager
    {
      private IStorage storage;
      private ILimitedStorage limitedStorage;
      private IPackedStorage packedStorage;
      private IReplacementPolicy policy;
      private IMonitor monitor;
      private IDictionary items;
      private PerformanceCounterCollection counters;

      /// <summary>Создает менеджер кэша.</summary>
      /// <param name="storage">Хранилище элементов кэша</param>
      public CacheManager(IStorage storage)
        : this(storage, (IReplacementPolicy) null, (IMonitor) null)
      {
      }

      /// <summary>Создает менеджер кэша.</summary>
      /// <param name="storage">Хранилище элементов кэша</param>
      /// <param name="policy">
      /// Алгоритм замещения элементов в кэше. Должен быть отличен от null, если хранилище
      /// имеет ограниченный объем (т.е. реализует интерфейс <see cref="T:Intermech.Cache.ILimitedStorage" />
      /// </param>
      public CacheManager(IStorage storage, IReplacementPolicy policy)
        : this(storage, policy, (IMonitor) null)
      {
      }

      /// <summary>Создает менеджер кэша.</summary>
      /// <param name="storage">Хранилище элементов кэша</param>
      /// <param name="monitor">Монитор состояния элементов в кэше</param>
      public CacheManager(IStorage storage, IMonitor monitor)
        : this(storage, (IReplacementPolicy) null, monitor)
      {
      }

      /// <summary>Создает менеджер кэша.</summary>
      /// <param name="storage">Хранилище элементов кэша</param>
      /// <param name="policy">
      /// Алгоритм замещения элементов в кэше. Должен быть отличен от null, если хранилище
      /// имеет ограниченный объем (т.е. реализует интерфейс <see cref="T:Intermech.Cache.ILimitedStorage" />
      /// </param>
      /// <param name="monitor">Монитор состояния элементов в кэше</param>
      public CacheManager(IStorage storage, IReplacementPolicy policy, IMonitor monitor)
      {
        if (storage == null)
          throw new ArgumentNullException(nameof (storage), Resources.GetString("E_StorageIsNull"));
        if (storage is ILimitedStorage && ((ILimitedStorage) storage).LimitsEnabled && policy == null)
          throw new ArgumentException(Resources.GetString("E_PolicyIsNull"), nameof (storage));
        this.storage = storage;
        this.limitedStorage = storage as ILimitedStorage;
        this.packedStorage = storage as IPackedStorage;
        this.policy = policy;
        this.monitor = monitor;
        this.items = (IDictionary) new HybridDictionary();
        this.counters = new PerformanceCounterCollection();
      }

      /// <summary>
      /// Добавляет новый элемент в кэш. Если в кэше уже есть элемент с
      /// указанным ключем, то он будет предварительно удален.
      /// </summary>
      /// <param name="key">Уникальный ключ элемента в кэше</param>
      /// <param name="data">Элемент</param>
      public void Add(object key, object data)
      {
        Validator.CheckKey(key);
        Validator.CheckData(data);
        lock (this)
          this.InternalAdd(key, data, (BeforeRemoveEventHandler) null, (AfterRemoveEventHandler) null);
      }

      /// <summary>
      /// Добавляет новый элемент в кэш. Если в кэше уже есть элемент с
      /// указанным ключем, то он будет предварительно удален.
      /// </summary>
      /// <param name="key">Уникальный ключ элемента в кэше</param>
      /// <param name="data">Элемент</param>
      /// <param name="beforeRemove">Делегат метода, который будет вызван перед удалением элемента из кэша</param>
      /// <param name="afterRemove">Делегат метода, который будет вызван после удаления элемента из кэша</param>
      public void Add(
        object key,
        object data,
        BeforeRemoveEventHandler beforeRemove,
        AfterRemoveEventHandler afterRemove)
      {
        Validator.CheckKey(key);
        Validator.CheckData(data);
        lock (this)
          this.InternalAdd(key, data, beforeRemove, afterRemove, (IExpiration[]) null);
      }

      /// <summary>
      /// Добавляет новый элемент в кэш. Если в кэше уже есть элемент с
      /// указанным ключем, то он будет предварительно удален.
      /// </summary>
      /// <param name="key">Уникальный ключ элемента в кэше</param>
      /// <param name="data">Элемент</param>
      /// <param name="expirations">Массив объектов, которые будут контролировать устаревание элемента</param>
      public void Add(object key, object data, params IExpiration[] expirations)
      {
        Validator.CheckKey(key);
        Validator.CheckData(data);
        Validator.CheckExpirations(expirations);
        lock (this)
          this.InternalAdd(key, data, (BeforeRemoveEventHandler) null, (AfterRemoveEventHandler) null, expirations);
      }

      /// <summary>
      /// Добавляет новый элемент в кэш. Если в кэше уже есть элемент с
      /// указанным ключем, то он будет предварительно удален.
      /// </summary>
      /// <param name="key">Уникальный ключ элемента в кэше</param>
      /// <param name="data">Элемент</param>
      /// <param name="beforeRemove">Делегат метода, который будет вызван перед удалением элемента из кэша</param>
      /// <param name="afterRemove">Делегат метода, который будет вызван после удаления элемента из кэша</param>
      /// <param name="expirations">Массив объектов, которые будут контролировать устаревание элемента</param>
      public void Add(
        object key,
        object data,
        BeforeRemoveEventHandler beforeRemove,
        AfterRemoveEventHandler afterRemove,
        params IExpiration[] expirations)
      {
        Validator.CheckKey(key);
        Validator.CheckData(data);
        Validator.CheckExpirations(expirations);
        lock (this)
          this.InternalAdd(key, data, beforeRemove, afterRemove, expirations);
      }

      /// <summary>Удалает указанный элемент из кэша.</summary>
      /// <param name="key">Уникальный ключ элемента в кэше.</param>
      public void Remove(object key)
      {
        Validator.CheckKey(key);
        lock (this)
        {
          if (!this.items.Contains(key))
            return;
          this.InternalRemove(key, RemoveCause.Removed);
        }
      }

      /// <summary>Очищает кэш, удаляя все помещенные в него элементы.</summary>
      public void Flush()
      {
        lock (this)
        {
          foreach (DictionaryEntry dictionaryEntry in this.items)
          {
            CacheItem cacheItem = (CacheItem) dictionaryEntry.Value;
            if (cacheItem.BeforeRemove != null || cacheItem.AfterRemove != null)
              this.InternalRemove(dictionaryEntry.Key, RemoveCause.Flushed);
          }
          this.items.Clear();
          this.storage.Flush();
          if (this.policy != null)
            this.policy.Flush();
          if (this.monitor == null)
            return;
          this.monitor.Flush();
        }
      }

      /// <summary>
      /// Возвращает элемент из кэша. Если элемент отсутствует в кэше, то результатом будет null.
      /// </summary>
      /// <param name="key">Уникальный ключ элемента</param>
      /// <returns>Элемент</returns>
      public object this[object key] => this.GetData(key);

      /// <summary>
      /// Возвращает элемент из кэша. Если элемент отсутствует в кэше, то результатом будет null.
      /// </summary>
      /// <param name="key">Уникальный ключ элемента в кэше</param>
      /// <returns>Элемент</returns>
      public object GetData(object key)
      {
        Validator.CheckKey(key);
        lock (this)
        {
          CacheItem cacheItem = (CacheItem) this.items[key];
          if (cacheItem != null)
          {
            if (cacheItem.Expirations != null)
            {
              for (int index = 0; index < cacheItem.Expirations.Length; ++index)
              {
                if (cacheItem.Expirations[index].HasExpired)
                {
                  this.InternalRemove(key, RemoveCause.Expired);
                  return (object) null;
                }
              }
              for (int index = 0; index < cacheItem.Expirations.Length; ++index)
                cacheItem.Expirations[index].Notify();
            }
            if (this.policy != null)
              this.policy.Notify(key);
            return this.GetDataFromStorage(key);
          }
        }
        return (object) null;
      }

      /// <summary>
      /// Возвращает массив ключей всех элементов, помещенных в кэш.
      /// </summary>
      /// <returns>Массив ключей элементов в кэше</returns>
      public object[] GetKeys()
      {
        lock (this)
        {
          object[] keys = new object[this.items.Keys.Count];
          this.items.Keys.CopyTo((Array) keys, 0);
          return keys;
        }
      }

      /// <summary>
      /// Возвращает контейнер, содержащий метаданные для указанного элемента
      /// кэша.
      /// </summary>
      /// <param name="key">Уникальный ключ элемента в кэше</param>
      /// <returns>Контейнер с метаданными элемента</returns>
      public CacheItem GetItem(object key)
      {
        Validator.CheckKey(key);
        lock (this)
          return (CacheItem) this.items[key];
      }

      /// <summary>
      /// Возвращает коллекцию счетчиков производительности кэша, позволяющих оценивать
      /// эффективность его работы.
      /// </summary>
      public PerformanceCounterCollection PerformanceCounters
      {
        get
        {
          lock (this)
            return this.counters;
        }
      }

      /// <summary>Выполняет вставку элемента в кэш.</summary>
      /// <param name="key">Уникальный ключ элемента в кэше</param>
      /// <param name="data">Элемент</param>
      /// <param name="beforeRemove">Делегат метода, который будет вызван перед удалением элемента из кэша</param>
      /// <param name="afterRemove">Делегат метода, который будет вызван после удаления элемента из кэша</param>
      /// <param name="expirations">Массив объектов, которые будут контролировать устаревание элемента</param>
      private void InternalAdd(
        object key,
        object data,
        BeforeRemoveEventHandler beforeRemove,
        AfterRemoveEventHandler afterRemove,
        params IExpiration[] expirations)
      {
        if (this.items.Contains(key))
          this.InternalRemove(key, RemoveCause.Removed);
        if (this.storage is IPackedStorage storage)
          data = storage.PackObject(key, data);
        long usedSpace = -1;
        if (this.limitedStorage != null && this.limitedStorage.LimitsEnabled)
        {
          usedSpace = this.limitedStorage.EstimateSpace(data);
          if (usedSpace > this.limitedStorage.TotalSpace)
            throw new InvalidOperationException(Resources.GetString("E_DataIsTooLarge"));
          while (this.limitedStorage.FreeSpace < usedSpace)
          {
            object keyForEvict = this.policy.GetKeyForEvict();
            if (keyForEvict != null)
              this.InternalRemove(keyForEvict, RemoveCause.Evicted);
          }
        }
        this.items.Add(key, (object) new CacheItem(key, beforeRemove, afterRemove, expirations));
        this.storage.Add(key, data);
        if (this.policy != null)
          this.policy.Add(key, data, usedSpace);
        if (this.monitor == null || expirations == null)
          return;
        this.monitor.Add(key, expirations);
      }

      /// <summary>Выполняет удаление элемента из кэша.</summary>
      /// <param name="key">Уникальный ключ элемента в кэше.</param>
      /// <param name="cause">Причина, по которой элемент удаляется.</param>
      private void InternalRemove(object key, RemoveCause cause)
      {
        CacheItem cacheItem = (CacheItem) this.items[key];
        if (cacheItem.BeforeRemove != null)
        {
          object dataFromStorage = this.GetDataFromStorage(key);
          cacheItem.BeforeRemove(key, dataFromStorage, cause);
        }
        if (cause != RemoveCause.Flushed)
          this.items.Remove(key);
        this.storage.Remove(key);
        if (this.policy != null)
          this.policy.Remove(key);
        if (cacheItem.Expirations != null && this.monitor != null)
          this.monitor.Remove(key);
        if (cacheItem.AfterRemove == null)
          return;
        cacheItem.AfterRemove(key, cause);
      }

      private object GetDataFromStorage(object key)
      {
        object packedData = this.storage.GetData(key);
        if (this.packedStorage != null)
          packedData = this.packedStorage.UnpackObject(key, packedData);
        return packedData;
      }
    }
}
