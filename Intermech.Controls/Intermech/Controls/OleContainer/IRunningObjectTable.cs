
// Type: Intermech.Controls.OleContainer.IRunningObjectTable
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace Intermech.Controls.OleContainer;

[Guid("00000010-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[ComImport]
public interface IRunningObjectTable
{
  void Register(int grfFlags, [MarshalAs(UnmanagedType.Interface)] object punkObject, IMoniker pmkObjectName, IntPtr pdwRegister);

  void Revoke(int dwRegister);

  [MethodImpl(MethodImplOptions.PreserveSig)]
  int IsRunning(IMoniker pmkObjectName);

  [MethodImpl(MethodImplOptions.PreserveSig)]
  int GetObject(IMoniker pmkObjectName, [MarshalAs(UnmanagedType.Interface)] out object ppunkObject);

  void NoteChangeTime(int dwRegister, ref FILETIME pfiletime);

  [MethodImpl(MethodImplOptions.PreserveSig)]
  int GetTimeOfLastChange(IMoniker pmkObjectName, out FILETIME pfiletime);

  void EnumRunning(out IEnumMoniker ppenumMoniker);
}
