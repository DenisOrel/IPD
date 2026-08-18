
// Type: Intermech.Controls.Grid.ClickEventArgs
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;


namespace Intermech.Controls.Grid;

public class ClickEventArgs : EventArgs
{
  private int _itemIndex;
  private int _columnIndex;

  /// <summary>Constructor</summary>
  /// <param name="itemindex"></param>
  /// <param name="columnindex"></param>
  public ClickEventArgs(int itemindex, int columnindex)
  {
    this._itemIndex = itemindex;
    this._columnIndex = columnindex;
  }

  /// <summary>Index of item clicked</summary>
  public int ItemIndex => this._itemIndex;

  /// <summary>Index of column clicked</summary>
  public int ColumnIndex => this._columnIndex;
}
