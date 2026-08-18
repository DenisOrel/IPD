
// Type: Intermech.Data.EntityDb.Common.NonUniqueInverseIndex`1
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System.Collections.Generic;


namespace Intermech.Data.EntityDb.Common
{
    public sealed class NonUniqueInverseIndex<TKey> : IInverseIndex<TKey>
    {
      private readonly Dictionary<long, HashSet<TKey>> idx;

      public NonUniqueInverseIndex() => this.idx = new Dictionary<long, HashSet<TKey>>();

      public void AddValue(long entityId, TKey indexKey)
      {
        HashSet<TKey> keySet;
        if (!this.idx.TryGetValue(entityId, out keySet))
        {
          keySet = new HashSet<TKey>();
          this.idx.Add(entityId, keySet);
        }
        keySet.Add(indexKey);
      }

      public void RemoveValue(long entityId, TKey indexKey)
      {
        HashSet<TKey> keySet;
        if (!this.idx.TryGetValue(entityId, out keySet))
          return;
        keySet.Remove(indexKey);
        if (keySet.Count != 0)
          return;
        this.idx.Remove(entityId);
      }

      public void RemoveAllValues(long entityId) => this.idx.Remove(entityId);

      public IEnumerable<TKey> EnumerateKeys(long entityId)
      {
        HashSet<TKey> keySet;
        return this.idx.TryGetValue(entityId, out keySet) ? (IEnumerable<TKey>) keySet : (IEnumerable<TKey>) new TKey[0];
      }
    }
}
