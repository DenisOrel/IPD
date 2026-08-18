
// Type: Intermech.Data.KeyValueStores.IKeyValueStore`2
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;


namespace Intermech.Data.KeyValueStores
{
    public interface IKeyValueStore<TKey, TValue> where TKey : IEquatable<TKey>
    {
      TValue TryGetByKey(TKey key);

      List<TKey> GetKeys();

      List<TValue> GetAll();

      void Add(TKey key, TValue value);

      void Update(TKey key, TValue item);

      void Remove(TKey key);

      int Count { get; }

      ICommitableObject BeginTransaction(bool canWrite = true);

      CommitableObjectScope BeginTransactionScope(bool canWrite = true);

      bool InTransaction { get; }
    }
}
