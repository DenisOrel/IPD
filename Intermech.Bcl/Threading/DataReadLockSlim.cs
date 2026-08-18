
// Type: Intermech.Threading.DataReadLockSlim
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Threading;


namespace Intermech.Threading
{
    /// <summary>
    /// Упрощает использование ReaderWriterLockSlim для чтения данных.
    /// </summary>
    public struct DataReadLockSlim : IDisposable
    {
      private ReaderWriterLockSlim rwl;

      /// <summary>Получает блокировку на чтение данных.</summary>
      /// <param name="rwl">Объект синхронизации</param>
      public DataReadLockSlim(ReaderWriterLockSlim rwl)
      {
        this.rwl = rwl != null ? rwl : throw new ArgumentNullException(nameof (rwl));
        if (!this.rwl.TryEnterReadLock(LockObjects.LockTimeout))
          throw new TimeoutException("Timeout expires before the lock request is granted.");
      }

      /// <summary>Снимает полученную ранее блокировку.</summary>
      public void Dispose()
      {
        if (this.rwl == null)
          return;
        this.rwl.ExitReadLock();
        this.rwl = (ReaderWriterLockSlim) null;
      }
    }
}
