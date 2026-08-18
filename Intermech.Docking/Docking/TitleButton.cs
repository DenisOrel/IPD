
// Type: Intermech.Docking.TitleButton
// Assembly: Intermech.Docking, Version=4.0.25.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F97F850-2D29-46D1-A3D7-6B2A02E86D46
:\IPS\Client\Intermech.Docking.dll

using System.Drawing;


namespace Intermech.Docking;

internal class TitleButton
{
  public Rectangle _bounds;
  public bool _visible;
  public bool _enabled;
  public object _tag;

  public TitleButton()
  {
    this._bounds = Rectangle.Empty;
    this._visible = false;
    this._enabled = true;
    this._tag = (object) null;
  }
}
