
// Type: Intermech.Controls.OleContainer.tagEXCEPINFO
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.Runtime.InteropServices;


namespace Intermech.Controls.OleContainer;

[StructLayout(LayoutKind.Sequential)]
public class tagEXCEPINFO
{
  [MarshalAs(UnmanagedType.U2)]
  public short wCode;
  [MarshalAs(UnmanagedType.U2)]
  public short wReserved;
  [MarshalAs(UnmanagedType.BStr)]
  public string bstrSource;
  [MarshalAs(UnmanagedType.BStr)]
  public string bstrDescription;
  [MarshalAs(UnmanagedType.BStr)]
  public string bstrHelpFile;
  [MarshalAs(UnmanagedType.U4)]
  public int dwHelpContext;
  public IntPtr pvReserved;
  public IntPtr pfnDeferredFillIn;
  [MarshalAs(UnmanagedType.U4)]
  public int scode;
}
