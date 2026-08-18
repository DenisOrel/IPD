
// Type: Intermech.Controls.OleContainer.TV_ITEM
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.Runtime.InteropServices;


namespace Intermech.Controls.OleContainer;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public struct TV_ITEM
{
  public int mask;
  public IntPtr hItem;
  public int state;
  public int stateMask;
  public IntPtr pszText;
  public int cchTextMax;
  public int iImage;
  public int iSelectedImage;
  public int cChildren;
  public IntPtr lParam;
}
