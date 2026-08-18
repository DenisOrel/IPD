
// Type: Intermech.Data.KeyValueStores.InMemoryKeyValueStoreParameters`2
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Data.KeyValueStores
{
    public class InMemoryKeyValueStoreParameters<TKey, TValue> : ICloneable where TKey : IEquatable<TKey>
    {
      private int transactionTimeout;
      private List<InMemoryKeyValueStoreView<TKey, TValue>> views;

      public InMemoryKeyValueStoreParameters()
      {
        this.transactionTimeout = (int) Math.Round(TimeSpan.FromSeconds(30.0).TotalMilliseconds);
        this.views = new List<InMemoryKeyValueStoreView<TKey, TValue>>();
      }

      private InMemoryKeyValueStoreParameters(
        InMemoryKeyValueStoreParameters<TKey, TValue> other)
      {
        this.transactionTimeout = other.TransactionTimeout;
        this.views = new List<InMemoryKeyValueStoreView<TKey, TValue>>((IEnumerable<InMemoryKeyValueStoreView<TKey, TValue>>) other.Views);
      }

      public int TransactionTimeout
      {
        [DebuggerStepThrough] get => this.transactionTimeout;
        [DebuggerStepThrough] set
        {
          this.transactionTimeout = value > 0 ? value : throw new ArgumentOutOfRangeException(nameof (value));
        }
      }

      public List<InMemoryKeyValueStoreView<TKey, TValue>> Views
      {
        [DebuggerStepThrough] get => this.views;
      }

      public InMemoryKeyValueStoreParameters<TKey, TValue> Clone()
      {
        return new InMemoryKeyValueStoreParameters<TKey, TValue>(this);
      }

      object ICloneable.Clone() => (object) this.Clone();
    }
}
