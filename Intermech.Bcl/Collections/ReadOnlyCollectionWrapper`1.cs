
// Type: Intermech.Collections.ReadOnlyCollectionWrapper`1
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Collections
{
    public class ReadOnlyCollectionWrapper<T> : 
      ICollection<T>,
      IEnumerable<T>,
      IEnumerable,
      ICollectionWrapper<T>
    {
      private ICollection<T> items;

      public ReadOnlyCollectionWrapper(ICollection<T> items)
      {
        this.items = items != null ? items : throw new ArgumentNullException(nameof (items));
      }

      ICollection<T> ICollectionWrapper<T>.Unwrap() => this.items;

      public void Add(T item) => throw new NotSupportedException();

      public bool Remove(T item) => throw new NotSupportedException();

      public void Clear() => throw new NotSupportedException();

      public IEnumerator<T> GetEnumerator() => this.items.GetEnumerator();

      IEnumerator IEnumerable.GetEnumerator() => this.items.GetEnumerator();

      public bool Contains(T item) => this.items.Contains(item);

      public void CopyTo(T[] array, int arrayIndex) => this.items.CopyTo(array, arrayIndex);

      public int Count
      {
        [DebuggerStepThrough] get => this.items.Count;
      }

      public bool IsReadOnly
      {
        [DebuggerStepThrough] get => true;
      }
    }
}
