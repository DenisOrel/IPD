
// Type: Intermech.Threading.AtomicInt32
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System.Diagnostics;
using System.Threading;


namespace Intermech.Threading
{
    public struct AtomicInt32(int initialValue)
    {
      private int _value = initialValue;

      public int Value
      {
        [DebuggerStepThrough] get => this._value;
        [DebuggerStepThrough] set => Interlocked.Exchange(ref this._value, value);
      }

      public bool TryModify(int oldValue, int newValue)
      {
        return Interlocked.CompareExchange(ref this._value, newValue, oldValue) == oldValue;
      }

      public int Increment() => Interlocked.Increment(ref this._value);

      public int Decrement() => Interlocked.Decrement(ref this._value);
    }
}
