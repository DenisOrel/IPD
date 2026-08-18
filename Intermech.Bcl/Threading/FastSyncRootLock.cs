
// Type: Intermech.Threading.FastSyncRootLock
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Threading;


namespace Intermech.Threading
{
    public struct FastSyncRootLock : IDisposable
    {
      private object syncRoot;

      public FastSyncRootLock(object syncRoot)
      {
        this.syncRoot = syncRoot;
        if (syncRoot == null)
          return;
        Monitor.Enter(syncRoot);
      }

      public void Dispose()
      {
        if (this.syncRoot == null)
          return;
        Monitor.Exit(this.syncRoot);
        this.syncRoot = (object) null;
      }
    }
}
