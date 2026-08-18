
// Type: Intermech.Memoization.SimpleStateMonitor
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System.Threading;


namespace Intermech.Memoization
{
    public sealed class SimpleStateMonitor : IStateMonitor
    {
      private const int HAS_STATE = 1;
      private const int NO_STATE = 0;
      private int state;
      private volatile int seqNum;

      public SimpleStateMonitor()
        : this(true)
      {
      }

      public SimpleStateMonitor(bool hasState)
      {
        this.state = hasState ? 1 : 0;
        this.seqNum = 1;
      }

      public void UpdateState()
      {
        if (Interlocked.CompareExchange(ref this.state, 1, 0) != 1)
          return;
        ++this.seqNum;
      }

      public bool AnyWritersSince(object seqNum) => seqNum == null || (int) seqNum < this.seqNum;

      public object WriterSeqNum => (object) this.seqNum;
    }
}
