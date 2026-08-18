
// Type: Intermech.Threading.DataWriteLock
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Localization;
using System;
using System.Threading;


namespace Intermech.Threading
{
    /// <summary>
    /// Упрощает использование ReaderWriterLock для записи данных.
    /// </summary>
    public class DataWriteLock : IDisposable
    {
      private ReaderWriterLock rwl;
      private bool upgraded;
      private LockCookie lc;

      /// <summary>Получает блокировку на запись данных.</summary>
      /// <param name="rwl">Объект синхронизации</param>
      public DataWriteLock(ReaderWriterLock rwl)
      {
        this.rwl = rwl;
        this.rwl.AcquireWriterLock(-1);
      }

      /// <summary>
      /// Позволяет получить блокировку на запись данных из кода, уже имеющего блокировку на чтение.
      /// </summary>
      /// <param name="dataReadLock"></param>
      public DataWriteLock(DataReadLock dataReadLock)
        : this(dataReadLock, false)
      {
      }

      /// <summary>
      /// Позволяет получить блокировку на запись данных из кода, уже имеющего блокировку на чтение.
      /// </summary>
      /// <param name="dataReadLock"></param>
      /// <param name="sameRevision">Указывает, что блокировка на запись должна быть получена для той же
      /// ревизии данных, что и блокировка на чтение</param>
      /// <exception cref="T:System.Threading.SynchronizationLockException">Блокировка на запись не была
      /// получена, т.к. данные были изменены другим потоком</exception>
      public DataWriteLock(DataReadLock dataReadLock, bool sameRevision)
      {
        this.rwl = dataReadLock.Rwl;
        int writerSeqNum = this.rwl.WriterSeqNum;
        this.lc = this.rwl.UpgradeToWriterLock(-1);
        if (sameRevision && this.rwl.AnyWritersSince(writerSeqNum))
        {
          this.rwl.DowngradeFromWriterLock(ref this.lc);
          throw new SynchronizationLockException(LocalizationHolder.rm.GetString("Interfaces_118"));
        }
        this.upgraded = true;
      }

      /// <summary>Снимает полученную ранее блокировку.</summary>
      public void Dispose()
      {
        if (this.upgraded)
          this.rwl.DowngradeFromWriterLock(ref this.lc);
        else
          this.rwl.ReleaseWriterLock();
      }
    }
}
