
// Type: Intermech.Memoization.ConstantMemoizer`1
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Threading;
using System;


namespace Intermech.Memoization
{
    public class ConstantMemoizer<TResult>
    {
      private readonly Func<TResult> function;
      private readonly IStateMonitor stateMonitor;
      private readonly ISyncRoot syncRoot;
      private object seqNum;
      private TResult value;

      public ConstantMemoizer(Func<TResult> function, IStateMonitor stateMonitor, ISyncRoot syncRoot)
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
      }

      public TResult Invoke()
      {
        this.syncRoot.Lock();
        try
        {
          if (this.stateMonitor.AnyWritersSince(this.seqNum))
          {
            this.seqNum = this.stateMonitor.WriterSeqNum;
            this.value = this.function();
          }
          return this.value;
        }
        finally
        {
          this.syncRoot.Unlock();
        }
      }

      public static Func<TResult> Wrap(
        Func<TResult> function,
        IStateMonitor stateMonitor,
        ISyncRoot syncRoot)
      {
        return new Func<TResult>(new ConstantMemoizer<TResult>(function, stateMonitor, syncRoot).Invoke);
      }
    }
}
