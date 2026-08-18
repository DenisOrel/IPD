
// Type: Intermech.Threading.AtomicBoolean
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System.Diagnostics;
using System.Threading;


namespace Intermech.Threading
{
    public struct AtomicBoolean(bool initialValue)
    {
      private int _flagValue = AtomicBoolean.ToFlagValue(initialValue);

      public bool Value
      {
        [DebuggerStepThrough] get => this._flagValue != 0;
        [DebuggerStepThrough] set
        {
          Interlocked.Exchange(ref this._flagValue, AtomicBoolean.ToFlagValue(value));
        }
      }

      public bool TryModify(bool oldValue, bool newValue)
      {
        int flagValue1 = AtomicBoolean.ToFlagValue(newValue);
        int flagValue2 = AtomicBoolean.ToFlagValue(oldValue);
        return Interlocked.CompareExchange(ref this._flagValue, flagValue1, flagValue2) == flagValue2;
      }

      private static int ToFlagValue(bool value) => !value ? 0 : 1;
    }
}
