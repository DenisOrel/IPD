
// Type: Intermech.Runtime.ComInterop.ComTypes.IClassFactory
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Runtime.InteropServices;


namespace Intermech.Runtime.ComInterop.ComTypes
{
    [Guid("00000001-0000-0000-C000-000000000046")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    public interface IClassFactory
    {
      [return: MarshalAs(UnmanagedType.Interface)]
      object CreateInstance([MarshalAs(UnmanagedType.Interface)] object pUnkOuter, [MarshalAs(UnmanagedType.LPStruct)] Guid riid);

      void LockServer(int lockFlag);
    }
}
