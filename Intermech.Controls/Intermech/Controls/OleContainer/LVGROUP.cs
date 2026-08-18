
// Type: Intermech.Controls.OleContainer.LVGROUP
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.Runtime.InteropServices;


namespace Intermech.Controls.OleContainer;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public class LVGROUP
{
  public uint cbSize;
  public uint mask;
  public IntPtr pszHeader;
  public int cchHeader;
  public IntPtr pszFooter;
  public int cchFooter;
  public int iGroupId;
  public uint stateMask;
  public uint state;
  public uint uAlign;

  public override string ToString()
  {
    return $"LVGROUP: header = {this.pszHeader.ToString()}, iGroupId = {this.iGroupId.ToString()}";
  }

  public LVGROUP() => this.cbSize = (uint) Marshal.SizeOf(typeof (LVGROUP));
}
