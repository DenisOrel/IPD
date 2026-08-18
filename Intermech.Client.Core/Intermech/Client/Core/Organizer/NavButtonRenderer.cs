
// Type: Intermech.Client.Core.Organizer.NavButtonRenderer
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Intermech.Client.Core.Organizer;

/// <summary>
/// 
/// </summary>
public class NavButtonRenderer
{
  private ColorTable _colorTbl = new ColorTable();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="g"></param>
  /// <param name="bounds"></param>
  /// <param name="active"></param>
  /// <param name="inputState"></param>
  public void DrawBackground(Graphics g, Rectangle bounds, bool active, InputState inputState)
  {
    ColorBlend colorBlend = new ColorBlend();
    if (!active && inputState == InputState.Normal)
      colorBlend.Colors = new Color[4]
      {
        this._colorTbl.ButtonLight,
        this._colorTbl.ButtonDark,
        this._colorTbl.ButtonHighlightDark,
        this._colorTbl.ButtonHighlightLight
      };
    else if (!active && inputState == InputState.Hovered)
      colorBlend.Colors = new Color[4]
      {
        this._colorTbl.ButtonHoveredLight,
        this._colorTbl.ButtonHoveredDark,
        this._colorTbl.ButtonHoveredHighlightDark,
        this._colorTbl.ButtonHoveredHighlightLight
      };
    else if (active && inputState == InputState.Normal)
      colorBlend.Colors = new Color[4]
      {
        this._colorTbl.ButtonActiveLight,
        this._colorTbl.ButtonActiveDark,
        this._colorTbl.ButtonActiveHighlightDark,
        this._colorTbl.ButtonActiveHighlightLight
      };
    else if (inputState == InputState.Clicked || active && inputState == InputState.Hovered)
      colorBlend.Colors = new Color[4]
      {
        this._colorTbl.ButtonClickedLight,
        this._colorTbl.ButtonClickedDark,
        this._colorTbl.ButtonClickedHighlightDark,
        this._colorTbl.ButtonClickedHighlightLight
      };
    colorBlend.Positions = new float[4]
    {
      0.0f,
      0.62f,
      0.62f,
      1f
    };
    bounds.Height = bounds.Height == 0 ? 1 : bounds.Height;
    bounds.Width = bounds.Width == 0 ? 1 : bounds.Width;
    using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new Point(bounds.Left, bounds.Bottom), new Point(bounds.Left, bounds.Top), Color.White, Color.Black))
    {
      linearGradientBrush.InterpolationColors = colorBlend;
      g.FillRectangle((Brush) linearGradientBrush, bounds);
    }
    using (Pen pen = new Pen(this._colorTbl.DarkBorder))
      g.DrawLine(pen, bounds.Left, bounds.Top, bounds.Right, bounds.Top);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="g"></param>
  /// <param name="location"></param>
  /// <param name="image"></param>
  public void DrawImage(Graphics g, Point location, Image image)
  {
    if (image == null)
      return;
    g.DrawImage(image, location);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="g"></param>
  /// <param name="bounds"></param>
  /// <param name="font"></param>
  /// <param name="text"></param>
  public void DrawText(Graphics g, Rectangle bounds, Font font, string text)
  {
    TextRenderer.DrawText((IDeviceContext) g, text, font, bounds, this._colorTbl.Text, TextFormatFlags.EndEllipsis | TextFormatFlags.SingleLine | TextFormatFlags.VerticalCenter);
  }
}
