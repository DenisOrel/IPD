
// Type: Intermech.Data.KeyValueStores.RwlReadScope
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Threading;


namespace Intermech.Data.KeyValueStores
{
    public struct RwlReadScope : IDisposable
    {
      private ReaderWriterLockSlim rwl;

      internal RwlReadScope(ReaderWriterLockSlim rwl) => this.rwl = rwl;

      public void Dispose()
      {
        if (this.rwl == null)
          return;
        this.rwl.ExitReadLock();
        this.rwl = (ReaderWriterLockSlim) null;
      }
    }
}
