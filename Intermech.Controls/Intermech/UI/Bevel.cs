
// Type: Intermech.UI.Bevel
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.UI;

public class Bevel : Control
{
  private BevelStyle _style;
  private BevelShape _shape;
  private Graphics _g;

  public Bevel()
  {
    this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
    this.SetStyle(ControlStyles.Opaque, false);
    this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
    this.SetStyle(ControlStyles.StandardDoubleClick, false);
    this.SetStyle(ControlStyles.Selectable, false);
    this._style = BevelStyle.Lowered;
    this._shape = BevelShape.Box;
    this.Height = 50;
    this.Width = 50;
  }

  [Category("Appearance")]
  [Localizable(true)]
  [DefaultValue(0)]
  public BevelShape Shape
  {
    get => this._shape;
    set
    {
      if (this._shape == value)
        return;
      this._shape = value;
      this.Invalidate();
    }
  }

  [Category("Appearance")]
  [Localizable(true)]
  [DefaultValue(0)]
  public BevelStyle Style
  {
    get => this._style;
    set
    {
      if (this._style == value)
        return;
      this._style = value;
      this.Invalidate();
    }
  }

  private void BevelRect(Rectangle r, Pen p1, Pen p2)
  {
    this.BevelLine(p1, r.Left, r.Bottom, r.Left, r.Top);
    this.BevelLine(p1, r.Right, r.Top, r.Left, r.Top);
    this.BevelLine(p2, r.Right, r.Top, r.Right, r.Bottom);
    this.BevelLine(p2, r.Left, r.Bottom, r.Right, r.Bottom);
  }

  private void BevelLine(Pen pen, int x1, int y1, int x2, int y2)
  {
    this._g.DrawLine(pen, x1, y1, x2, y2);
  }

  protected override void OnPaint(PaintEventArgs e)
  {
    base.OnPaint(e);
    this._g = e.Graphics;
    Pen pen1;
    Pen pen2;
    if (this._style == BevelStyle.Lowered)
    {
      pen1 = SystemPens.ControlDark;
      pen2 = SystemPens.ControlLightLight;
    }
    else
    {
      pen2 = SystemPens.ControlDark;
      pen1 = SystemPens.ControlLightLight;
    }
    switch (this._shape)
    {
      case BevelShape.Box:
        this.BevelRect(new Rectangle(0, 0, this.Width - 1, this.Height - 1), pen1, pen2);
        break;
      case BevelShape.Frame:
        this.BevelRect(new Rectangle(1, 1, this.Width - 2, this.Height - 2), pen2, pen2);
        this.BevelRect(new Rectangle(0, 0, this.Width - 2, this.Height - 2), pen1, pen1);
        break;
      case BevelShape.TopLine:
        this.BevelLine(pen1, 0, 0, this.Width, 0);
        this.BevelLine(pen2, 0, 1, this.Width, 1);
        break;
      case BevelShape.BottomLine:
        this.BevelLine(pen1, 0, this.Height - 2, this.Width, this.Height - 2);
        this.BevelLine(pen2, 0, this.Height - 1, this.Width, this.Height - 1);
        break;
      case BevelShape.LeftLine:
        this.BevelLine(pen1, 0, 0, 0, this.Height);
        this.BevelLine(pen2, 1, 0, 1, this.Height);
        break;
      case BevelShape.RightLine:
        this.BevelLine(pen1, this.Width - 2, 0, this.Width - 2, this.Height);
        this.BevelLine(pen2, this.Width - 1, 0, this.Width - 1, this.Height);
        break;
    }
  }
}
