
// Type: Intermech.Controls.OleContainer.IStream
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.Runtime.InteropServices;


namespace Intermech.Controls.OleContainer;

[Guid("0000000C-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[ComImport]
public interface IStream
{
  int Read([In] IntPtr buf, [In] int len);

  int Write([In] IntPtr buf, [In] int len);

  [return: MarshalAs(UnmanagedType.I8)]
  long Seek([MarshalAs(UnmanagedType.I8), In] long dlibMove, [In] int dwOrigin);

  void SetSize([MarshalAs(UnmanagedType.I8), In] long libNewSize);

  [return: MarshalAs(UnmanagedType.I8)]
  long CopyTo([MarshalAs(UnmanagedType.Interface), In] IStream pstm, [MarshalAs(UnmanagedType.I8), In] long cb, [MarshalAs(UnmanagedType.LPArray), Out] long[] pcbRead);

  void Commit([In] int grfCommitFlags);

  void Revert();

  void LockRegion([MarshalAs(UnmanagedType.I8), In] long libOffset, [MarshalAs(UnmanagedType.I8), In] long cb, [In] int dwLockType);

  void UnlockRegion([MarshalAs(UnmanagedType.I8), In] long libOffset, [MarshalAs(UnmanagedType.I8), In] long cb, [In] int dwLockType);

  void Stat([In] IntPtr pStatstg, [In] int grfStatFlag);

  [return: MarshalAs(UnmanagedType.Interface)]
  IStream Clone();
}
