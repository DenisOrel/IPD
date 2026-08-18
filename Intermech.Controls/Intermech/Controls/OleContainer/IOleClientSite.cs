
// Type: Intermech.Controls.OleContainer.IOleClientSite
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace Intermech.Controls.OleContainer;

[ComVisible(true)]
[Guid("00000118-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IOleClientSite
{
  [MethodImpl(MethodImplOptions.PreserveSig)]
  [return: MarshalAs(UnmanagedType.I4)]
  int SaveObject();

  [MethodImpl(MethodImplOptions.PreserveSig)]
  [return: MarshalAs(UnmanagedType.I4)]
  int GetMoniker([MarshalAs(UnmanagedType.U4), In] int dwAssign, [MarshalAs(UnmanagedType.U4), In] int dwWhichMoniker, [MarshalAs(UnmanagedType.Interface)] out object ppmk);

  [MethodImpl(MethodImplOptions.PreserveSig)]
  [return: MarshalAs(UnmanagedType.I4)]
  int GetContainer([MarshalAs(UnmanagedType.Interface)] out IOleContainer container);

  [MethodImpl(MethodImplOptions.PreserveSig)]
  [return: MarshalAs(UnmanagedType.I4)]
  int ShowObject();

  [MethodImpl(MethodImplOptions.PreserveSig)]
  [return: MarshalAs(UnmanagedType.I4)]
  int OnShowWindow([MarshalAs(UnmanagedType.I4), In] int fShow);

  [MethodImpl(MethodImplOptions.PreserveSig)]
  [return: MarshalAs(UnmanagedType.I4)]
  int RequestNewObjectLayout();
}
