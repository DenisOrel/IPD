
// Type: Intermech.Controls.OleContainer.IEnumString
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace Intermech.Controls.OleContainer;

[Guid("00000101-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[ComImport]
public interface IEnumString
{
  [MethodImpl(MethodImplOptions.PreserveSig)]
  int Next(int celt, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1, ArraySubType = UnmanagedType.LPWStr), Out] string[] rgelt, IntPtr pceltFetched);

  [MethodImpl(MethodImplOptions.PreserveSig)]
  int Skip(int celt);

  void Reset();

  void Clone(out IEnumString ppenum);
}
