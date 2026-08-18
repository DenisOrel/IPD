
// Type: Intermech.Runtime.ComInterop.LocalServer.ComProcessReferenceCounter
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Diagnostics;
using System.Threading;


namespace Intermech.Runtime.ComInterop.LocalServer
{
    /// <summary>
    /// Счетчик ссылок для процесса приложения. Реализация является thread safe.
    /// </summary>
    internal sealed class ComProcessReferenceCounter : IReferenceCounter
    {
      private int refCount;

      public void Increment()
      {
        Interlocked.Increment(ref this.refCount);
        this.RaiseChanged();
      }

      public void Decrement()
      {
        int num = Interlocked.Decrement(ref this.refCount);
        this.RaiseChanged();
        if (num != 0)
          return;
        this.RaiseReleased();
      }

      public void Decrement(int value)
      {
        if (value <= 0)
          throw new ArgumentOutOfRangeException(nameof (value));
        for (int index = 0; index < value; ++index)
          this.Decrement();
      }

      public int Value
      {
        [DebuggerStepThrough] get => Volatile.Read(ref this.refCount);
      }

      private void RaiseChanged()
      {
        EventHandler changed = this.Changed;
        if (changed == null)
          return;
        changed((object) this, EventArgs.Empty);
      }

      private void RaiseReleased()
      {
        EventHandler released = this.Released;
        if (released == null)
          return;
        released((object) this, EventArgs.Empty);
      }

      public event EventHandler Changed;

      public event EventHandler Released;
    }
}
