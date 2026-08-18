
// Type: Intermech.Memoization.TableLookupMemoizer`2
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Threading;
using System;
using System.Collections.Generic;


namespace Intermech.Memoization
{
    public class TableLookupMemoizer<TKey, TValue>
    {
      private readonly Func<TKey, TValue> function;
      private readonly IStateMonitor stateMonitor;
      private readonly ISyncRoot syncRoot;
      private object seqNum;
      private Dictionary<TKey, TValue> valueTable;

      public TableLookupMemoizer(
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
        this.seqNum = (object) null;
        this.valueTable = new Dictionary<TKey, TValue>();
      }

      public TValue Invoke(TKey key)
      {
        this.syncRoot.Lock();
        try
        {
          if (this.stateMonitor.AnyWritersSince(this.seqNum))
          {
            this.seqNum = this.stateMonitor.WriterSeqNum;
            this.valueTable.Clear();
          }
          if (this.valueTable.Count == 0)
            return this.MakeNewValue(key);
          TValue obj;
          if (!this.valueTable.TryGetValue(key, out obj))
            obj = this.MakeNewValue(key);
          return obj;
        }
        finally
        {
          this.syncRoot.Unlock();
        }
      }

      private TValue MakeNewValue(TKey key)
      {
        TValue obj = this.function(key);
        this.valueTable.Add(key, obj);
        return obj;
      }

      public static Func<TKey, TValue> Wrap(
        Func<TKey, TValue> function,
        IStateMonitor stateMonitor,
        ISyncRoot syncRoot)
      {
        return new Func<TKey, TValue>(new TableLookupMemoizer<TKey, TValue>(function, stateMonitor, syncRoot).Invoke);
      }
    }
}
