
// Type: Intermech.Controls.OleContainer.IEnumSTATDATA
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace Intermech.Controls.OleContainer;

[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[Guid("00000103-0000-0000-C000-000000000046")]
[ComImport]
public interface IEnumSTATDATA
{
  [MethodImpl(MethodImplOptions.PreserveSig)]
  int Next(int celt, [MarshalAs(UnmanagedType.LPArray), Out] STATDATA[] rgelt, [MarshalAs(UnmanagedType.LPArray, SizeConst = 1), Out] int[] pceltFetched);

  [MethodImpl(MethodImplOptions.PreserveSig)]
  int Skip(int celt);

  [MethodImpl(MethodImplOptions.PreserveSig)]
  int Reset();

  void Clone(out IEnumSTATDATA newEnum);
}
