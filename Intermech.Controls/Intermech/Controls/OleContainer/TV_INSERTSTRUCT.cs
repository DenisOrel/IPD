
// Type: Intermech.Controls.OleContainer.TV_INSERTSTRUCT
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.Runtime.InteropServices;


namespace Intermech.Controls.OleContainer;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public struct TV_INSERTSTRUCT
{
  public IntPtr hParent;
  public IntPtr hInsertAfter;
  public int item_mask;
  public IntPtr item_hItem;
  public int item_state;
  public int item_stateMask;
  public IntPtr item_pszText;
  public int item_cchTextMax;
  public int item_iImage;
  public int item_iSelectedImage;
  public int item_cChildren;
  public IntPtr item_lParam;
  public int item_iIntegral;
}
