
// Type: Intermech.Controls.OleContainer.IMarshal
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace Intermech.Controls.OleContainer;

[Guid("00000003-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[ComImport]
public interface IMarshal
{
  [MethodImpl(MethodImplOptions.PreserveSig)]
  int GetUnmarshalClass(
    ref Guid riid,
    [MarshalAs(UnmanagedType.Interface)] object pv,
    int dwDestContext,
    IntPtr pvDestContext,
    int mshlflags,
    out Guid pCid);

  [MethodImpl(MethodImplOptions.PreserveSig)]
  int GetMarshalSizeMax(
    ref Guid riid,
    [MarshalAs(UnmanagedType.Interface)] object pv,
    int dwDestContext,
    IntPtr pvDestContext,
    int mshlflags,
    out int pSize);

  [MethodImpl(MethodImplOptions.PreserveSig)]
  int MarshalInterface(
    [MarshalAs(UnmanagedType.Interface)] object pStm,
    ref Guid riid,
    [MarshalAs(UnmanagedType.Interface)] object pv,
    int dwDestContext,
    IntPtr pvDestContext,
    int mshlflags);

  [MethodImpl(MethodImplOptions.PreserveSig)]
  int UnmarshalInterface([MarshalAs(UnmanagedType.Interface)] object pStm, ref Guid riid, [MarshalAs(UnmanagedType.Interface)] out object ppv);

  [MethodImpl(MethodImplOptions.PreserveSig)]
  int ReleaseMarshalData([MarshalAs(UnmanagedType.Interface)] object pStm);

  [MethodImpl(MethodImplOptions.PreserveSig)]
  int DisconnectObject(int dwReserved);
}
