
// Type: Intermech.Threading.ThreadMailbox`1
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Concurrent;
using System.Diagnostics;


namespace Intermech.Threading
{
    internal sealed class ThreadMailbox<T> : IDisposable where T : class
    {
      private BlockingCollection<T> collection;
      private bool isDisposed;

      public ThreadMailbox() => this.collection = new BlockingCollection<T>(1);

      public void Dispose()
      {
        if (this.isDisposed)
          return;
        try
        {
          this.collection.Dispose();
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

      public bool TryPut(T newValue, int timeout)
      {
        if (timeout < 0)
          throw new ArgumentOutOfRangeException(nameof (timeout));
        return this.collection.TryAdd(newValue, timeout);
      }

      public T TryGet(int timeout)
      {
        if (timeout < 0)
          throw new ArgumentOutOfRangeException(nameof (timeout));
        T obj;
        return this.collection.TryTake(out obj, timeout) ? obj : default (T);
      }
    }
}
