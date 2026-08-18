
// Type: Intermech.Threading.DedicatedThreadControlBlock
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Diagnostics;


namespace Intermech.Threading
{
    internal sealed class DedicatedThreadControlBlock : IDisposable, IDedicatedThreadControlBlock
    {
      private ThreadMailbox<DedicatedThreadTask> mailbox;
      private bool isDisposed;

      public DedicatedThreadControlBlock() => this.mailbox = new ThreadMailbox<DedicatedThreadTask>();

      public void Dispose()
      {
        if (this.isDisposed)
          return;
        try
        {
          this.mailbox.Dispose();
        }
        finally
        {
          this.isDisposed = true;
        }
      }

      public bool IsDisposed
      {
        [DebuggerStepThrough] get => this.isDisposed;
      }

      public ThreadMailbox<DedicatedThreadTask> Mailbox
      {
        [DebuggerStepThrough] get => this.mailbox;
      }
    }
}
