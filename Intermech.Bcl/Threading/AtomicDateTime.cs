
// Type: Intermech.Threading.AtomicDateTime
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Diagnostics;
using System.Threading;


namespace Intermech.Threading
{
    public struct AtomicDateTime
    {
      private long _ticks;

      public AtomicDateTime(DateTime initialValue) => this._ticks = initialValue.Ticks;

      public DateTime Value
      {
        [DebuggerStepThrough] get => new DateTime(Interlocked.Read(ref this._ticks));
        [DebuggerStepThrough] set => Interlocked.Exchange(ref this._ticks, value.Ticks);
      }

      public bool TryModify(DateTime oldValue, DateTime newValue)
      {
        long ticks1 = newValue.Ticks;
        long ticks2 = oldValue.Ticks;
        return Interlocked.CompareExchange(ref this._ticks, ticks1, ticks2) == ticks2;
      }
    }
}
