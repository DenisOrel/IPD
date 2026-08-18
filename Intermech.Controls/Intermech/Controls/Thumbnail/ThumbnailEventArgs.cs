
// Type: Intermech.Controls.Thumbnail.ThumbnailEventArgs
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.Drawing;


namespace Intermech.Controls.Thumbnail;

/// <summary>
/// 
/// </summary>
public class ThumbnailEventArgs : EventArgs
{
  private int _itemIndex;
  private Point _pos;

  /// <summary>Конструктор.</summary>
  /// <param name="itemIndex"></param>
  public ThumbnailEventArgs(int itemIndex)
  {
    this._itemIndex = itemIndex;
    this._pos = Point.Empty;
  }

  /// <summary>Конструктор.</summary>
  /// <param name="itemIndex"></param>
  /// <param name="pos"></param>
  public ThumbnailEventArgs(int itemIndex, Point pos)
  {
    this._itemIndex = itemIndex;
    this._pos = pos;
  }

  /// <summary>
  /// 
  /// </summary>
  public Point Pos => this._pos;

  /// <summary>
  /// 
  /// </summary>
  public int ItemIndex => this._itemIndex;
}
