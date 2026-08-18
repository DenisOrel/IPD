
// Type: Intermech.Controls.OleContainer.DIBSECTION
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.Runtime.InteropServices;


namespace Intermech.Controls.OleContainer;

[StructLayout(LayoutKind.Sequential)]
public class DIBSECTION
{
  public BITMAP dsBm;
  public BITMAPINFOHEADER dsBmih;
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
  public int[] dsBitfields;
  public IntPtr dshSection;
  public int dsOffset;
}
