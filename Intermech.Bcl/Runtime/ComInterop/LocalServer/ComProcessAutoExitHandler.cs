
// Type: Intermech.Runtime.ComInterop.LocalServer.ComProcessAutoExitHandler
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Diagnostics;
using System.Threading;


namespace Intermech.Runtime.ComInterop.LocalServer
{
    public abstract class ComProcessAutoExitHandler
    {
      private const int NO_EXIT = 0;
      private const int REQUEST_EXIT = 1;
      private ComServer comServer;
      private int exitFlag;

      protected ComProcessAutoExitHandler(ComServer comServer)
      {
        this.comServer = comServer != null ? comServer : throw new ArgumentNullException(nameof (comServer));
        this.comServer.Released += new EventHandler(this.OnComServerReleased);
        if (!TraceSwitches.General.TraceInfo)
          return;
        Trace.WriteLine(ComServerResources.Trace_AutoExitHandlerIsInstalled);
      }

      private void OnComServerReleased(object sender, EventArgs e)
      {
        if (Interlocked.CompareExchange(ref this.exitFlag, 1, 0) != 0)
          return;
        this.comServer.BlockClientRequests();
        this.RequestExit();
        if (!TraceSwitches.General.TraceInfo)
          return;
        Trace.WriteLine(ComServerResources.Trace_AutoExitIsRequested);
      }

      protected bool IsExitRequested
      {
        [DebuggerStepThrough] get => this.exitFlag == 1;
      }

      protected abstract void RequestExit();

      protected virtual void TraceExitEvent()
      {
        if (!TraceSwitches.General.TraceInfo)
          return;
        Trace.WriteLine(ComServerResources.Trace_AutoExitIsInvoked);
      }
    }
}
