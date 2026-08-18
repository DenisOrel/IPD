
// Type: Intermech.Client.Core.Organizer.CalendarRendererItemBoundsEventArgs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Drawing;


namespace Intermech.Client.Core.Organizer;

public class CalendarRendererItemBoundsEventArgs : CalendarRendererItemEventArgs
{
  private Rectangle _bounds;
  private bool _isFirst;
  private bool _isLast;

  /// <summary>Creates a new Event</summary>
  /// <param name="original"></param>
  /// <param name="bounds"></param>
  /// <param name="isFirst"></param>
  /// <param name="isLast"></param>
  internal CalendarRendererItemBoundsEventArgs(
    CalendarRendererItemEventArgs original,
    Rectangle bounds,
    bool isFirst,
    bool isLast)
    : base((CalendarRendererEventArgs) original, original.Item)
  {
    this._isFirst = isFirst;
    this._isLast = isLast;
    this._bounds = bounds;
  }

  /// <summary>Gets the bounds of the item to be rendered.</summary>
  /// <remarks>
  /// Items may have more than one bounds due to week segmentation.
  /// </remarks>
  public Rectangle Bounds => this._bounds;

  /// <summary>
  /// Gets a value indicating if the bounds are the first of the item.
  /// </summary>
  /// <remarks>
  /// Items may have more than one bounds due to week segmentation.
  /// </remarks>
  public bool IsFirst => this._isFirst;

  /// <summary>
  /// Gets a value indicating if the bounds are the last of the item.
  /// </summary>
  /// <remarks>
  /// Items may have more than one bounds due to week segmentation.
  /// </remarks>
  public bool IsLast
  {
    get => this._isLast;
    set => this._isLast = value;
  }
}
