
// Type: Intermech.Data.EntityDb.Common.UniqueEqualityDirectIndex`1
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;


namespace Intermech.Data.EntityDb.Common
{
    public sealed class UniqueEqualityDirectIndex<TKey> : IDirectIndex<TKey>, IIndexKeyScanner<TKey>
    {
      private readonly IEqualityComparer<TKey> indexKeyComparer;
      private readonly Dictionary<TKey, IEntity> idx;

      public UniqueEqualityDirectIndex(IEqualityComparer<TKey> indexKeyComparer)
      {
        this.indexKeyComparer = indexKeyComparer != null ? indexKeyComparer : throw new ArgumentNullException(nameof (indexKeyComparer));
        this.idx = new Dictionary<TKey, IEntity>(indexKeyComparer);
      }

      public void AddValue(IEntity entity, TKey indexKey) => this.idx.Add(indexKey, entity);

      public void RemoveValue(IEntity entity, TKey indexKey) => this.idx.Remove(indexKey);

      public int GetKeyCount() => this.idx.Count;

      public EntitySet ScanEntities(TKey indexKey)
      {
        EntitySet entitySet = new EntitySet();
        IEntity entity;
        if (this.idx.TryGetValue(indexKey, out entity))
          entitySet.Add(entity);
        return entitySet;
      }
    }
}
