
// Type: Intermech.Controls.OleContainer.LVCOLUMN
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.Runtime.InteropServices;


namespace Intermech.Controls.OleContainer;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public class LVCOLUMN
{
  public int mask;
  public int fmt;
  public int cx;
  public IntPtr pszText;
  public int cchTextMax;
  public int iSubItem;
  public int iImage;
  public int iOrder;
}
