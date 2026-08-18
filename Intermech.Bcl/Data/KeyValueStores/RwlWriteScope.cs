
// Type: Intermech.Data.KeyValueStores.RwlWriteScope
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Threading;


namespace Intermech.Data.KeyValueStores
{
    internal struct RwlWriteScope : IDisposable
    {
      private ReaderWriterLockSlim rwl;

      internal RwlWriteScope(ReaderWriterLockSlim rwl) => this.rwl = rwl;

      public void Dispose()
      {
        if (this.rwl == null)
          return;
        this.rwl.ExitWriteLock();
        this.rwl = (ReaderWriterLockSlim) null;
      }
    }
}
