
// Type: Intermech.Controls.OleContainer.IEnumSTATSTG
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace Intermech.Controls.OleContainer;

[Guid("0000000d-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[ComImport]
public interface IEnumSTATSTG
{
  [MethodImpl(MethodImplOptions.PreserveSig)]
  uint Next(uint celt, [MarshalAs(UnmanagedType.LPArray), Out] STATSTG[] rgelt, out uint pceltFetched);

  void Skip(uint celt);

  void Reset();

  [return: MarshalAs(UnmanagedType.Interface)]
  IEnumSTATSTG Clone();
}
