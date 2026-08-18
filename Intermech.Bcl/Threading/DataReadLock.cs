
// Type: Intermech.Threading.DataReadLock
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Threading;


namespace Intermech.Threading
{
    /// <summary>
    /// Упрощает использование ReaderWriterLock для чтения данных.
    /// </summary>
    public class DataReadLock : IDisposable
    {
      private ReaderWriterLock rwl;

      /// <summary>Получает блокировку на чтение данных.</summary>
      /// <param name="rwl">Объект синхронизации</param>
      public DataReadLock(ReaderWriterLock rwl)
      {
        this.rwl = rwl;
        this.rwl.AcquireReaderLock(-1);
      }

      /// <summary>Снимает полученную ранее блокировку.</summary>
      public void Dispose() => this.rwl.ReleaseReaderLock();

      internal ReaderWriterLock Rwl => this.rwl;
    }
}
