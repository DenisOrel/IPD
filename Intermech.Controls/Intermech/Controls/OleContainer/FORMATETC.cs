
// Type: Intermech.Controls.OleContainer.FORMATETC
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.Runtime.InteropServices;


namespace Intermech.Controls.OleContainer;

public struct FORMATETC
{
  [MarshalAs(UnmanagedType.U2)]
  public short cfFormat;
  public IntPtr ptd;
  [MarshalAs(UnmanagedType.U4)]
  public DVASPECT dwAspect;
  public int lindex;
  [MarshalAs(UnmanagedType.U4)]
  public TYMED tymed;
}
