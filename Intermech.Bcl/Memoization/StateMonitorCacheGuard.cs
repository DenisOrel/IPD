
// Type: Intermech.Memoization.StateMonitorCacheGuard
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Memoization
{
    public class StateMonitorCacheGuard
    {
      private readonly IStateMonitor stateMonitor;
      private object writeSeqNum;

      public StateMonitorCacheGuard(IStateMonitor stateMonitor)
      {
        this.stateMonitor = stateMonitor != null ? stateMonitor : throw new ArgumentNullException(nameof (stateMonitor));
      }

      public void CheckCache()
      {
        if (!this.stateMonitor.AnyWritersSince(this.writeSeqNum))
          return;
        if (this.ResetCache != null)
          this.ResetCache((object) this, EventArgs.Empty);
        this.writeSeqNum = this.stateMonitor.WriterSeqNum;
      }

      public event EventHandler ResetCache;
    }
}
