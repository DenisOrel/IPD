
// Type: Intermech.Cache.DoubleLinkedList
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Cache
{
    /// <summary>
    /// Реализует двусвязный список объектов. На основе таких списков построены
    /// некоторые алгоритмы замещения содержимого кэша (например, LRU).
    /// </summary>
    internal class DoubleLinkedList
    {
      private Item head;
      private Item tail;

      /// <summary>Создает пустой двусвязный список.</summary>
      public DoubleLinkedList() => this.Clear();

      /// <summary>Очищает список, удаляя все элементы.</summary>
      public void Clear()
      {
        this.head = (Item) null;
        this.tail = (Item) null;
      }

      /// <summary>Удаляет элемент из списка.</summary>
      /// <param name="item">Элемент списка</param>
      public void Remove(Item item)
      {
        if (item.prev == null)
        {
          if (this.head == item)
            this.head = item.next;
        }
        else if (item.prev.next == item)
          item.prev.next = item.next;
        if (item.next == null)
        {
          if (this.tail != item)
            return;
          this.tail = item.prev;
        }
        else
        {
          if (item.next.prev != item)
            return;
          item.next.prev = item.prev;
        }
      }

      /// <summary>Возвращает или устанавливает первый элемент списка.</summary>
      /// <returns>Элемент списка</returns>
      public Item Head
      {
        get => this.head;
        set
        {
          if (this.head == value)
            return;
          if (value == null)
            throw new ArgumentNullException(nameof (value), Resources.GetString("E_HeadCannotBeNull"));
          this.Remove(value);
          if (this.head != null)
            this.head.prev = value;
          else
            this.tail = value;
          this.head = value;
        }
      }

      /// <summary>
      /// Возвращает или устанавливает последний элемент списка.
      /// </summary>
      /// <returns>Элемент списка</returns>
      public Item Tail
      {
        get => this.tail;
        set
        {
          if (this.tail == value)
            return;
          if (value == null)
            throw new ArgumentNullException(nameof (value), Resources.GetString("E_TailCannotBeNull"));
          this.Remove(value);
          if (this.tail != null)
            this.tail.next = value;
          else
            this.head = value;
          this.tail = value;
        }
      }

      /// <summary>Реализует элемент двусвязного списка объектов.</summary>
      public class Item
      {
        private object value;
        internal Item prev;
        internal Item next;

        public Item(object value)
        {
          this.value = value;
          this.prev = (Item) null;
          this.next = (Item) null;
        }

        public object Value
        {
          get => this.value;
          set => this.value = value;
        }

        public Item Prev => this.prev;

        public Item Next => this.next;
      }
    }
}
