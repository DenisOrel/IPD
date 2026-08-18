
// Type: Intermech.Controls.OleContainer.PARAFORMAT
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System.Runtime.InteropServices;


namespace Intermech.Controls.OleContainer;

[StructLayout(LayoutKind.Sequential)]
public class PARAFORMAT
{
  public int cbSize;
  public int dwMask;
  public short wNumbering;
  public short wReserved;
  public int dxStartIndent;
  public int dxRightIndent;
  public int dxOffset;
  public short wAlignment;
  public short cTabCount;
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32 /*0x20*/)]
  public int[] rgxTabs;

  public PARAFORMAT() => this.cbSize = Marshal.SizeOf(typeof (PARAFORMAT));
}
