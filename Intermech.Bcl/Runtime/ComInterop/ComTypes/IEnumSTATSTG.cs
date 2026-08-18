
// Type: Intermech.Runtime.ComInterop.ComTypes.IEnumSTATSTG
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace Intermech.Runtime.ComInterop.ComTypes
{
    [Guid("0000000D-0000-0000-C000-000000000046")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    internal interface IEnumSTATSTG
    {
      [MethodImpl(MethodImplOptions.PreserveSig)]
      uint Next(uint celt, [MarshalAs(UnmanagedType.LPArray), Out] System.Runtime.InteropServices.ComTypes.STATSTG[] rgelt, out uint pceltFetched);

      void Skip(uint celt);

      void Reset();

      [return: MarshalAs(UnmanagedType.Interface)]
      IEnumSTATSTG Clone();
    }
}
