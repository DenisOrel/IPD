
// Type: Intermech.Controls.OleContainer.IOleContainer
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System.Runtime.InteropServices;


namespace Intermech.Controls.OleContainer;

[ComVisible(true)]
[Guid("0000011B-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IOleContainer
{
  void ParseDisplayName([MarshalAs(UnmanagedType.Interface), In] object pbc, [MarshalAs(UnmanagedType.BStr), In] string pszDisplayName, [MarshalAs(UnmanagedType.LPArray), Out] int[] pchEaten, [MarshalAs(UnmanagedType.LPArray), Out] object[] ppmkOut);

  void EnumObjects([MarshalAs(UnmanagedType.U4), In] int grfFlags, [MarshalAs(UnmanagedType.LPArray), Out] object[] ppenum);

  void LockContainer([MarshalAs(UnmanagedType.I4), In] int fLock);
}
