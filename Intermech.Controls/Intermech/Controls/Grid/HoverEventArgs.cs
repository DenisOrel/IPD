
// Type: Intermech.Controls.Grid.HoverEventArgs
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;


namespace Intermech.Controls.Grid;

public class HoverEventArgs : EventArgs
{
  private int _itemIndex;
  private int _columnIndex;
  private ListRegion _region;
  private HoverType _hoverType;

  public HoverEventArgs(HoverType hovertype, int itemindex, int columnindex, ListRegion region)
  {
    this._region = region;
    this._itemIndex = itemindex;
    this._columnIndex = columnindex;
    this._hoverType = hovertype;
  }

  public HoverType HoverType => this._hoverType;

  public ListRegion Region => this._region;

  public int ItemIndex => this._itemIndex;

  public int ColumnIndex => this._columnIndex;
}
