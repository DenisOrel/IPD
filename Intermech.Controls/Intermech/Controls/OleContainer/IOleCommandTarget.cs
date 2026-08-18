
// Type: Intermech.Controls.OleContainer.IOleCommandTarget
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace Intermech.Controls.OleContainer;

[ComVisible(true)]
[Guid("B722BCCB-4E68-101B-A2BC-00AA00404770")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[ComImport]
public interface IOleCommandTarget
{
  [MethodImpl(MethodImplOptions.PreserveSig)]
  [return: MarshalAs(UnmanagedType.I4)]
  int QueryStatus(ref Guid pguidCmdGroup, int cCmds, [In, Out] OLECMD prgCmds, [In, Out] IntPtr pCmdText);

  [MethodImpl(MethodImplOptions.PreserveSig)]
  [return: MarshalAs(UnmanagedType.I4)]
  int Exec(ref Guid pguidCmdGroup, int nCmdID, int nCmdexecopt, [MarshalAs(UnmanagedType.LPArray), In] object[] pvaIn, int pvaOut);
}
