
// Type: Intermech.Controls.OleContainer.TOOLINFO_TOOLTIP
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.Runtime.InteropServices;


namespace Intermech.Controls.OleContainer;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public class TOOLINFO_TOOLTIP
{
  public int cbSize;
  public int uFlags;
  public IntPtr hwnd;
  public IntPtr uId;
  public RECT rect;
  public IntPtr hinst;
  public IntPtr lpszText;
  public IntPtr lParam;

  public TOOLINFO_TOOLTIP() => this.cbSize = Marshal.SizeOf(typeof (TOOLINFO_TOOLTIP));
}
