
// Type: Intermech.Cache.Policies.Lru
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System.Collections;


namespace Intermech.Cache.Policies
{
    /// <summary>
    /// Реализует алгоритм замещения элементов кэша, известный как LRU (частный
    /// случай семейства алгоритмов LRU-k при k = 1). Он выталкивает элемент, к
    /// которому дольше всего не было обращений.
    /// </summary>
    public class Lru : IReplacementPolicy
    {
      private IDictionary keyToItem = (IDictionary) new Hashtable();
      private DoubleLinkedList items = new DoubleLinkedList();

      /// <summary>
      /// Добавляет новый элемент кэша в список элементов, которые
      /// должны обрабатываться алгоритмом.
      /// </summary>
      /// <param name="key">Уникальный ключ элемента в кэше</param>
      /// <param name="data">Элемент</param>
      /// <param name="usedSpace">Объем, который данные занимают в хранилище</param>
      public void Add(object key, object data, long usedSpace)
      {
        DoubleLinkedList.Item obj = new DoubleLinkedList.Item(key);
        this.keyToItem.Add(key, (object) obj);
        this.items.Head = obj;
      }

      /// <summary>
      /// Удаляет элемент кэша с указанным ключем из списка элементов,
      /// которые должны обрабатываться алгоритмом.
      /// </summary>
      /// <param name="key">Уникальный ключ элемента в кэше</param>
      public void Remove(object key)
      {
        DoubleLinkedList.Item obj = (DoubleLinkedList.Item) this.keyToItem[key];
        this.keyToItem.Remove(key);
        this.items.Remove(obj);
      }

      /// <summary>
      /// Удаляет все элементы из списка элементов, обрабатываемых
      /// алгоритмом.
      /// </summary>
      public void Flush()
      {
        this.keyToItem.Clear();
        this.items.Clear();
      }

      /// <summary>
      /// Уведомляет алгоритм, что к элементу кэша с указанным ключем
      /// было обращение.
      /// </summary>
      /// <param name="key">Уникальный ключ элемента в кэше</param>
      public void Notify(object key)
      {
        DoubleLinkedList.Item obj = (DoubleLinkedList.Item) this.keyToItem[key];
        if (this.items.Head == obj)
          return;
        this.items.Head = obj;
      }

      /// <summary>
      /// Возвращает ключ элемента, который может быт удален из
      /// заполненного кэша, для того чтобы освободить место для нового
      /// элемента.
      /// </summary>
      /// <returns>Ключ элемента</returns>
      public object GetKeyForEvict() => this.items.Tail.Value;
    }
}
