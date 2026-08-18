
// Type: Intermech.Threading.DataWriteLockSlim
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Threading;


namespace Intermech.Threading
{
    /// <summary>
    /// Упрощает использование ReaderWriterLockSlim для записи данных.
    /// </summary>
    public struct DataWriteLockSlim : IDisposable
    {
      private ReaderWriterLockSlim rwl;

      /// <summary>Получает блокировку на запись данных.</summary>
      /// <param name="rwl">Объект синхронизации</param>
      public DataWriteLockSlim(ReaderWriterLockSlim rwl)
      {
        this.rwl = rwl != null ? rwl : throw new ArgumentNullException(nameof (rwl));
        if (!this.rwl.TryEnterWriteLock(LockObjects.LockTimeout))
          throw new TimeoutException("Timeout expires before the lock request is granted.");
      }

      /// <summary>Снимает полученную ранее блокировку.</summary>
      public void Dispose()
      {
        if (this.rwl == null)
          return;
        this.rwl.ExitWriteLock();
        this.rwl = (ReaderWriterLockSlim) null;
      }
    }
}
