
// Type: Intermech.Memoization.ListScanMemoizer`2
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Threading;
using System;
using System.Collections.Generic;


namespace Intermech.Memoization
{
    public class ListScanMemoizer<TKey, TValue>
    {
      private readonly Func<TKey, TValue> function;
      private readonly IStateMonitor stateMonitor;
      private readonly ISyncRoot syncRoot;
      private readonly LinkedList<KeyValuePair<TKey, TValue>> valueList;
      private readonly IEqualityComparer<TKey> keyComparer;
      private object seqNum;

      public ListScanMemoizer(
        Func<TKey, TValue> function,
        IStateMonitor stateMonitor,
        ISyncRoot syncRoot)
      {
        if (function == null)
          throw new ArgumentNullException(nameof (function));
        if (stateMonitor == null)
          throw new ArgumentNullException(nameof (stateMonitor));
        if (syncRoot == null)
          throw new ArgumentNullException(nameof (syncRoot));
        this.function = function;
        this.stateMonitor = stateMonitor;
        this.syncRoot = syncRoot;
        this.valueList = new LinkedList<KeyValuePair<TKey, TValue>>();
        this.keyComparer = (IEqualityComparer<TKey>) EqualityComparer<TKey>.Default;
        this.seqNum = (object) null;
      }

      public TValue Invoke(TKey key)
      {
        this.syncRoot.Lock();
        try
        {
          if (this.stateMonitor.AnyWritersSince(this.seqNum))
          {
            this.seqNum = this.stateMonitor.WriterSeqNum;
            this.valueList.Clear();
          }
          if (this.valueList.Count == 0)
            return this.MakeNewValue(key);
          foreach (KeyValuePair<TKey, TValue> keyValuePair in this.valueList)
          {
            if (this.keyComparer.Equals(key, keyValuePair.Key))
              return keyValuePair.Value;
          }
          return this.MakeNewValue(key);
        }
        finally
        {
          this.syncRoot.Unlock();
        }
      }

      private TValue MakeNewValue(TKey key)
      {
        TValue obj = this.function(key);
        this.valueList.AddLast(new KeyValuePair<TKey, TValue>(key, obj));
        return obj;
      }

      public static Func<TKey, TValue> Wrap(
        Func<TKey, TValue> function,
        IStateMonitor stateMonitor,
        ISyncRoot syncRoot)
      {
        return new Func<TKey, TValue>(new ListScanMemoizer<TKey, TValue>(function, stateMonitor, syncRoot).Invoke);
      }
    }
}
