
// Type: Intermech.Data.EntityDb.Common.NonUniqueEqualityDirectIndex`1
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;


namespace Intermech.Data.EntityDb.Common
{
    public sealed class NonUniqueEqualityDirectIndex<TKey> : IDirectIndex<TKey>, IIndexKeyScanner<TKey>
    {
      private readonly IEqualityComparer<TKey> indexKeyComparer;
      private readonly Dictionary<TKey, EntitySet> idx;

      public NonUniqueEqualityDirectIndex(IEqualityComparer<TKey> indexKeyComparer)
      {
        this.indexKeyComparer = indexKeyComparer != null ? indexKeyComparer : throw new ArgumentNullException(nameof (indexKeyComparer));
        this.idx = new Dictionary<TKey, EntitySet>(indexKeyComparer);
      }

      public void AddValue(IEntity entity, TKey indexKey)
      {
        EntitySet entitySet;
        if (!this.idx.TryGetValue(indexKey, out entitySet))
        {
          entitySet = new EntitySet();
          this.idx.Add(indexKey, entitySet);
        }
        entitySet.Add(entity);
      }

      public void RemoveValue(IEntity entity, TKey indexKey)
      {
        EntitySet entitySet;
        if (!this.idx.TryGetValue(indexKey, out entitySet))
          return;
        entitySet.Remove(entity);
        if (entitySet.Count != 0)
          return;
        this.idx.Remove(indexKey);
      }

      public int GetKeyCount() => this.idx.Count;

      public EntitySet ScanEntities(TKey indexKey)
      {
        EntitySet entitySet;
        return this.idx.TryGetValue(indexKey, out entitySet) ? entitySet : new EntitySet();
      }
    }
}
