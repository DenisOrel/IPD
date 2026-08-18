
// Type: Intermech.Cache.Storages.InMemoryStorage
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections;
using System.Collections.Specialized;


namespace Intermech.Cache.Storages
{
    /// <summary>
    /// Реализует хранилище данных элементов кэша в оперативной памяти. Данное
    /// хранилище допускает использование объектов любых типов в качестве ключей
    /// и данных элементов. Объем любого элемента, помещаемого в хранилище,
    /// всегда оценивается равным 1. Поэтому максимальный объем хранилища - это
    /// максимальное количество элементов, которые можно поместить в хранилище.
    /// </summary>
    public class InMemoryStorage : IStorage, ILimitedStorage
    {
      /// <summary>
      /// Максимально допустимое количество элементов в хранилище.
      /// </summary>
      private long totalSpace;
      /// <summary>Количество свободных слотов в хранилище.</summary>
      private long freeSpace;
      /// <summary>Таблица с данными элементов, помещенных в хранилище.</summary>
      private IDictionary data;

      /// <summary>
      /// Создает хранилище элементов кэша неограниченного размера.
      /// </summary>
      public InMemoryStorage()
        : this(long.MaxValue)
      {
      }

      /// <summary>Создает хранилище элементов кэша.</summary>
      /// <param name="totalSpace">Максимальное количество элементов, которое можно поместить в хранилище</param>
      public InMemoryStorage(long totalSpace)
      {
        this.totalSpace = totalSpace > 0L ? totalSpace : throw new ArgumentOutOfRangeException(nameof (totalSpace), Resources.GetString("E_StoreTotalSpace"));
        this.freeSpace = this.totalSpace;
        this.data = (IDictionary) new HybridDictionary();
      }

      /// <summary>
      /// Возвращает true, если у хранилища включен режим ограничения объема.
      /// </summary>
      public bool LimitsEnabled => this.totalSpace != long.MaxValue;

      /// <summary>Возвращает объем хранилища.</summary>
      public long TotalSpace => this.totalSpace;

      /// <summary>Возвращает объем свободного пространства в хранилище.</summary>
      public long FreeSpace => this.freeSpace;

      /// <summary>
      /// Возвращает объем, который займет элемент после помещения в кэш.
      /// </summary>
      /// <param name="data">Элемент</param>
      /// <returns>Объем элемента</returns>
      public long EstimateSpace(object data) => 1;

      /// <summary>Добавляет элемент с указанным ключем в хранилище.</summary>
      /// <param name="key">Уникальный ключ элемента в кэше</param>
      /// <param name="data">Элемент</param>
      public void Add(object key, object data)
      {
        Validator.CheckKey(key);
        Validator.CheckData(data);
        this.data.Add(key, data);
        --this.freeSpace;
      }

      /// <summary>Удаляет из хранилища элемент с указанным ключем.</summary>
      /// <param name="key">Уникальный ключ элемента в кэше</param>
      public void Remove(object key)
      {
        Validator.CheckKey(key);
        this.data.Remove(key);
        ++this.freeSpace;
      }

      /// <summary>Очищает хранилище, удаляя все элементы.</summary>
      public void Flush()
      {
        this.data.Clear();
        this.freeSpace = this.totalSpace;
      }

      /// <summary>
      /// Возвращает из хранилища элемент с указанным ключем. Если элемента с указанным ключем
      /// нет в хранилище, то результатом будет null.
      /// </summary>
      /// <param name="key">Уникальный ключ элемента в кэше</param>
      /// <returns>Элемент</returns>
      public object GetData(object key)
      {
        Validator.CheckKey(key);
        return this.data[key];
      }
    }
}
