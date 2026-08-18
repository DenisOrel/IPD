
// Type: Intermech.Docking.ShowControlContextMenuEventArgs
// Assembly: Intermech.Docking, Version=4.0.25.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F97F850-2D29-46D1-A3D7-6B2A02E86D46
:\IPS\Client\Intermech.Docking.dll

using System.Drawing;


namespace Intermech.Docking;

public class ShowControlContextMenuEventArgs : DockControlEventArgs
{
  private Point _position;

  internal ShowControlContextMenuEventArgs(DockControl dc, Point pos)
    : base(dc)
  {
    this._position = pos;
  }

  public Point Position => this._position;
}
