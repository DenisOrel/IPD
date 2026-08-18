
// Type: Intermech.Collections.HashedList`1
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;


namespace Intermech.Collections
{
    public class HashedList<T> : List<T>
    {
      [NotNull]
      private readonly Dictionary<T, int> _indexes = new Dictionary<T, int>();
      public bool SkipDuplicates;

      public new void Add([NotNull] T item)
      {
        if (this.SkipDuplicates && this.Contains(item))
          return;
        base.Add(item);
        this._indexes.Add(item, this.Count - 1);
      }

      public new void AddRange([NotNull] IEnumerable<T> enumeration)
      {
        foreach (T obj in enumeration)
          this.Add(obj);
      }

      public void Remove([NotNull] T item)
      {
        this._indexes.Remove(item);
        base.Remove(item);
      }

      public new void RemoveAt(int index)
      {
        this._indexes.Remove(this[index]);
        base.RemoveAt(index);
      }

      public void RemoveAll([NotNull] Predicate<T> match)
      {
        throw new NotImplementedException(nameof (RemoveAll));
      }

      public new void RemoveRange(int index, int count)
      {
        throw new NotImplementedException(nameof (RemoveRange));
      }

      public new int IndexOf(T item) => throw new NotImplementedException(nameof (IndexOf));

      public new void Clear()
      {
        this._indexes.Clear();
        base.Clear();
      }

      public new bool Contains([NotNull] T item) => this._indexes.ContainsKey(item);

      [CanBeNull]
      public T FindByHash([NotNull] T item)
      {
        int index;
        return !this._indexes.TryGetValue(item, out index) ? default (T) : this[index];
      }
    }
}
