
// Type: Intermech.Controls.Thumbnail.BoundsEventArgs
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.Drawing;


namespace Intermech.Controls.Thumbnail;

public class BoundsEventArgs : EventArgs
{
  private Rectangle _bounds;
  public static readonly BoundsEventArgs EmptyBounds = new BoundsEventArgs();

  protected BoundsEventArgs() => this._bounds = Rectangle.Empty;

  public BoundsEventArgs(Rectangle bounds) => this._bounds = bounds;

  public Rectangle Bounds => this._bounds;
}
