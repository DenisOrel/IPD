
// Type: Intermech.Controls.OleContainer.MENUITEMINFO_T_RW
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.Runtime.InteropServices;


namespace Intermech.Controls.OleContainer;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public class MENUITEMINFO_T_RW
{
  public int cbSize;
  public int fMask;
  public int fType;
  public int fState;
  public int wID;
  public IntPtr hSubMenu;
  public IntPtr hbmpChecked;
  public IntPtr hbmpUnchecked;
  public IntPtr dwItemData;
  public IntPtr dwTypeData;
  public int cch;
  public IntPtr hbmpItem;

  public MENUITEMINFO_T_RW() => this.cbSize = Marshal.SizeOf(typeof (MENUITEMINFO_T_RW));
}
