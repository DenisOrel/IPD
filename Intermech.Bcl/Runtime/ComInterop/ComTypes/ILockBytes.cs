
// Type: Intermech.Runtime.ComInterop.ComTypes.ILockBytes
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Runtime.InteropServices;


namespace Intermech.Runtime.ComInterop.ComTypes
{
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("0000000A-0000-0000-C000-000000000046")]
    [ComImport]
    internal interface ILockBytes
    {
      void ReadAt(long ulOffset, IntPtr pv, int cb, IntPtr pcbRead);

      void WriteAt(long ulOffset, IntPtr pv, int cb, IntPtr pcbWritten);

      void Flush();

      void SetSize(long cb);

      void LockRegion(long libOffset, long cb, int dwLockType);

      void UnlockRegion(long libOffset, long cb, int dwLockType);

      void Stat(out System.Runtime.InteropServices.ComTypes.STATSTG pstatstg, STATFLAG grfStatFlag);
    }
}
