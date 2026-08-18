
// Type: Intermech.Data.EntityDb.Common.NonUniqueOrderedDirectIndex`1
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;


namespace Intermech.Data.EntityDb.Common
{
    public sealed class NonUniqueOrderedDirectIndex<TKey> : 
      IDirectIndex<TKey>,
      IIndexKeyScanner<TKey>,
      IIndexRangeScanner<TKey>
    {
      private readonly IComparer<TKey> indexKeyComparer;
      private readonly List<TKey> keys;
      private readonly List<EntitySet> values;

      public NonUniqueOrderedDirectIndex(IComparer<TKey> indexKeyComparer)
      {
        this.indexKeyComparer = indexKeyComparer != null ? indexKeyComparer : throw new ArgumentNullException(nameof (indexKeyComparer));
        this.keys = new List<TKey>();
        this.values = new List<EntitySet>();
      }

      public void AddValue(IEntity entity, TKey indexKey)
      {
        int index1 = this.keys.BinarySearch(indexKey, this.indexKeyComparer);
        EntitySet entitySet;
        if (index1 < 0)
        {
          entitySet = new EntitySet();
          int index2 = ~index1;
          this.keys.Insert(index2, indexKey);
          this.values.Insert(index2, entitySet);
        }
        else
          entitySet = this.values[index1];
        entitySet.Add(entity);
      }

      public void RemoveValue(IEntity entity, TKey indexKey)
      {
        int index = this.keys.BinarySearch(indexKey, this.indexKeyComparer);
        if (index < 0)
          return;
        EntitySet entitySet = this.values[index];
        entitySet.Remove(entity);
        if (entitySet.Count != 0)
          return;
        this.keys.RemoveAt(index);
        this.values.RemoveAt(index);
      }

      public int GetKeyCount() => this.keys.Count;

      public EntitySet ScanEntities(TKey indexKey)
      {
        int index = this.keys.BinarySearch(indexKey, this.indexKeyComparer);
        return index >= 0 ? this.values[index] : new EntitySet();
      }

      public IEnumerable<KeyValuePair<TKey, EntitySet>> ScanRangeTo(TKey indexKey, bool inclusive)
      {
        int lastKeyIdx = this.GetLastKeyIndex(indexKey, inclusive);
        for (int i = 0; i <= lastKeyIdx; ++i)
          yield return new KeyValuePair<TKey, EntitySet>(this.keys[i], this.values[i]);
      }

      public IEnumerable<KeyValuePair<TKey, EntitySet>> ScanRangeFrom(TKey indexKey, bool inclusive)
      {
        for (int i = this.GetFirstKeyIndex(indexKey, inclusive); i < this.keys.Count; ++i)
          yield return new KeyValuePair<TKey, EntitySet>(this.keys[i], this.values[i]);
      }

      public IEnumerable<KeyValuePair<TKey, EntitySet>> ScanRange(
        TKey fromKey,
        bool fromInclusive,
        TKey toKey,
        bool toInclusive)
      {
        int firstKeyIndex = this.GetFirstKeyIndex(fromKey, fromInclusive);
        int lastKeyIdx = this.GetLastKeyIndex(toKey, toInclusive);
        for (int i = firstKeyIndex; i <= lastKeyIdx; ++i)
          yield return new KeyValuePair<TKey, EntitySet>(this.keys[i], this.values[i]);
      }

      private int GetFirstKeyIndex(TKey fromKey, bool fromInclusive)
      {
        int num = this.keys.BinarySearch(fromKey, this.indexKeyComparer);
        if (num < 0)
          return 0;
        return !fromInclusive ? num + 1 : num;
      }

      private int GetLastKeyIndex(TKey toKey, bool toInclusive)
      {
        int num = this.keys.BinarySearch(toKey, this.indexKeyComparer);
        return !toInclusive ? num - 1 : num;
      }
    }
}
