
// Type: Intermech.Threading.AtomicInt64
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System.Diagnostics;
using System.Threading;


namespace Intermech.Threading
{
    public struct AtomicInt64(long initialValue)
    {
      private long _value = initialValue;

      public long Value
      {
        [DebuggerStepThrough] get => Interlocked.Read(ref this._value);
        [DebuggerStepThrough] set => Interlocked.Exchange(ref this._value, value);
      }

      public bool TryModify(long oldValue, long newValue)
      {
        return Interlocked.CompareExchange(ref this._value, newValue, oldValue) == oldValue;
      }

      public long Increment() => Interlocked.Increment(ref this._value);

      public long Decrement() => Interlocked.Decrement(ref this._value);
    }
}
