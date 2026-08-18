
// Type: Intermech.Docking.Rendering.SmallTabControlRenderer
// Assembly: Intermech.Docking, Version=4.0.25.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F97F850-2D29-46D1-A3D7-6B2A02E86D46
:\IPS\Client\Intermech.Docking.dll

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Docking.Rendering;

public class SmallTabControlRenderer : TabControlRenderer
{
  internal static Color _defaultActivePageColor = SystemColors.Window;
  private Color _activePageColor = SmallTabControlRenderer._defaultActivePageColor;

  [Description("Цвет активной в данный момент закладки")]
  public Color ActivePageColor
  {
    get => this._activePageColor;
    set => this._activePageColor = value;
  }

  public override string ToString() => "SmallTab";

  public override void DrawTabControlTab(
    Graphics graphics,
    Rectangle bounds,
    Image image,
    string text,
    Font font,
    Color backColor,
    Color foreColor,
    DrawItemState state,
    bool drawSeparator,
    Intermech.Docking.TabAlignment alignment,
    bool flat)
  {
    bool top = alignment == Intermech.Docking.TabAlignment.Top;
    if (!top)
    {
      ++bounds.Y;
      --bounds.Height;
    }
    else
      bounds.Y -= 3;
    bounds.Height += 3;
    if ((state & DrawItemState.Selected) == DrawItemState.Selected)
    {
      this.DrawTab(graphics, bounds, image, text, font, this.ActivePageColor, this.ActivePageColor, SystemBrushes.ControlText, state, top, flat);
    }
    else
    {
      this.DrawTab(graphics, bounds, image, text, font, backColor, SystemColors.ControlLightLight, SystemBrushes.ControlText, state, top, flat);
      if (!drawSeparator)
        return;
      if (top)
        graphics.DrawLine(SystemPens.ControlDark, bounds.Right - 1, bounds.Top + 2, bounds.Right - 1, bounds.Bottom - 6);
      else
        graphics.DrawLine(SystemPens.ControlDark, bounds.Right - 1, bounds.Top + 3, bounds.Right - 1, bounds.Bottom - 2);
    }
  }

  public override int TabControlTabHeight => Control.DefaultFont.Height;
}
