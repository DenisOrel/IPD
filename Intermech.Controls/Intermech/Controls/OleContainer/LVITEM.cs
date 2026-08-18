
// Type: Intermech.Controls.OleContainer.LVITEM
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.Runtime.InteropServices;


namespace Intermech.Controls.OleContainer;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public struct LVITEM
{
  public int mask;
  public int iItem;
  public int iSubItem;
  public int state;
  public int stateMask;
  public string pszText;
  public int cchTextMax;
  public int iImage;
  public IntPtr lParam;
  public int iIndent;
  public int iGroupId;
  public int cColumns;
  public IntPtr puColumns;

  public void Reset()
  {
    this.pszText = (string) null;
    this.mask = 0;
    this.iItem = 0;
    this.iSubItem = 0;
    this.stateMask = 0;
    this.state = 0;
    this.cchTextMax = 0;
    this.iImage = 0;
    this.lParam = IntPtr.Zero;
    this.iIndent = 0;
    this.iGroupId = 0;
    this.cColumns = 0;
    this.puColumns = IntPtr.Zero;
  }

  public override string ToString()
  {
    return $"LVITEM: pszText = {this.pszText}, iItem = {this.iItem.ToString()}, iSubItem = {this.iSubItem.ToString()}, state = {this.state.ToString()}, iGroupId = {this.iGroupId.ToString()}, cColumns = {this.cColumns.ToString()}";
  }
}
