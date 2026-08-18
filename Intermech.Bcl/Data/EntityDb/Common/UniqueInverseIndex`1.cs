
// Type: Intermech.Data.EntityDb.Common.UniqueInverseIndex`1
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System.Collections.Generic;


namespace Intermech.Data.EntityDb.Common
{
    public sealed class UniqueInverseIndex<TKey> : IInverseIndex<TKey>
    {
      private readonly Dictionary<long, TKey> idx;

      public UniqueInverseIndex() => this.idx = new Dictionary<long, TKey>();

      public void AddValue(long entityId, TKey indexKey) => this.idx.Add(entityId, indexKey);

      public void RemoveValue(long entityId, TKey indexKey) => this.RemoveAllValues(entityId);

      public void RemoveAllValues(long entityId) => this.idx.Remove(entityId);

      public IEnumerable<TKey> EnumerateKeys(long entityId)
      {
        List<TKey> keyList = new List<TKey>();
        TKey key;
        if (this.idx.TryGetValue(entityId, out key))
          keyList.Add(key);
        return (IEnumerable<TKey>) keyList;
      }
    }
}
