
// Type: Intermech.Collections.OrderedList`1
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections;
using System.Collections.Generic;


namespace Intermech.Collections
{
    public class OrderedList<T> : ICollection<T>, IEnumerable<T>, IEnumerable, IList<T>
    {
      private readonly List<T> items;
      private readonly IComparer<T> comparer;

      public OrderedList()
        : this((IComparer<T>) Comparer<T>.Default)
      {
      }

      public OrderedList(int capacity)
        : this(capacity, (IComparer<T>) Comparer<T>.Default)
      {
      }

      public OrderedList(IComparer<T> comparer)
        : this(4, comparer)
      {
      }

      public OrderedList(int capacity, IComparer<T> comparer)
      {
        if (comparer == null)
          throw new ArgumentNullException(nameof (comparer));
        this.items = new List<T>(capacity);
        this.comparer = comparer;
      }

      public OrderedList(IEnumerable<T> collection)
        : this(OrderedList<T>.DetectCapacity(collection), (IComparer<T>) Comparer<T>.Default)
      {
      }

      public OrderedList(IEnumerable<T> collection, IComparer<T> comparer)
        : this(OrderedList<T>.DetectCapacity(collection), comparer)
      {
        this.AddRange<T>(collection);
      }

      private static int DetectCapacity(IEnumerable<T> collection)
      {
        return !(collection is ICollection<T> objs) ? 4 : objs.Count;
      }

      public int Capacity
      {
        get => this.items.Capacity;
        set => this.items.Capacity = value;
      }

      public void AddRange(IEnumerable<T> items)
      {
        foreach (T obj in items)
          CollectionUtils.AddSorted(this.items, obj, this.comparer);
      }

      public void Add(T item) => this.AddOrGetIndex(item);

      public int AddOrGetIndex(T item) => CollectionUtils.AddSorted(this.items, item, this.comparer);

      public void Clear() => this.items.Clear();

      public bool Contains(T item) => this.items.BinarySearch(item, this.comparer) >= 0;

      public void CopyTo(T[] array, int arrayIndex) => this.items.CopyTo(array, arrayIndex);

      public int Count => this.items.Count;

      public bool IsReadOnly => false;

      public bool Remove(T item) => CollectionUtils.RemoveSorted(this.items, item, this.comparer);

      public IEnumerator<T> GetEnumerator() => (IEnumerator<T>) this.items.GetEnumerator();

      IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.items.GetEnumerator();

      public void Insert(int index, T item)
      {
        if (index >= this.Count)
        {
          this.Add(item);
        }
        else
        {
          int index1 = this.items.BinarySearch(item, this.comparer);
          if (index1 < 0)
          {
            index1 = ~index1;
            if (index1 == index)
            {
              this.items.Insert(index1, item);
              return;
            }
          }
          if (index1 != index)
            throw new InvalidOperationException("Bad index to insert item.");
        }
      }

      public int IndexOf(T item) => this.items.BinarySearch(item, this.comparer);

      public void RemoveAt(int index) => this.items.RemoveAt(index);

      public T this[int index]
      {
        get => this.items[index];
        set => this.Insert(index, value);
      }
    }
}
