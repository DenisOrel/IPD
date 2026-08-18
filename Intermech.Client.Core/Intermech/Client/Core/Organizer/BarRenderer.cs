
// Type: Intermech.Client.Core.Organizer.BarRenderer
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
public class BarRenderer
{
  private ColorTable _colorTable = new ColorTable();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="g"></param>
  /// <param name="bounds"></param>
  public void DrawBackground(Graphics g, Rectangle bounds)
  {
    --bounds.Width;
    --bounds.Height;
    using (Brush brush = (Brush) new SolidBrush(Color.White))
    {
      using (Pen pen = new Pen(this._colorTable.DarkBorder))
      {
        g.FillRectangle(brush, bounds);
        g.DrawRectangle(pen, bounds);
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="g"></param>
  /// <param name="bounds"></param>
  public void DrawHeader(Graphics g, Rectangle bounds)
  {
    ColorBlend colorBlend = new ColorBlend();
    colorBlend.Colors = new Color[2]
    {
      this._colorTable.HeaderBgDark,
      this._colorTable.HeaderBgLight
    };
    colorBlend.Positions = new float[2]{ 0.0f, 1f };
    bounds.Height = bounds.Height == 0 ? 1 : bounds.Height;
    bounds.Width = bounds.Width == 0 ? 1 : bounds.Width;
    using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new Point(bounds.Left, bounds.Bottom), new Point(bounds.Left, bounds.Top), Color.White, Color.Black))
    {
      linearGradientBrush.InterpolationColors = colorBlend;
      g.FillRectangle((Brush) linearGradientBrush, bounds);
    }
    using (Pen pen = new Pen(this._colorTable.HeaderBgInnerBorder))
    {
      g.DrawLine(pen, new Point(bounds.Left, bounds.Top), new Point(bounds.Right - 1, bounds.Top));
      g.DrawLine(pen, new Point(bounds.Left, bounds.Top), new Point(bounds.Left, bounds.Bottom));
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="g"></param>
  /// <param name="bounds"></param>
  /// <param name="text"></param>
  /// <param name="font"></param>
  public void DrawHeaderText(Graphics g, Rectangle bounds, string text, Font font)
  {
    TextRenderer.DrawText((IDeviceContext) g, text, font, bounds, this._colorTable.Text, TextFormatFlags.EndEllipsis | TextFormatFlags.SingleLine | TextFormatFlags.VerticalCenter);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="g"></param>
  /// <param name="bounds"></param>
  public void DrawSmallButtonRegion(Graphics g, Rectangle bounds)
  {
    ColorBlend colorBlend = new ColorBlend();
    colorBlend.Colors = new Color[4]
    {
      this._colorTable.ButtonLight,
      this._colorTable.ButtonDark,
      this._colorTable.ButtonHighlightDark,
      this._colorTable.ButtonHighlightLight
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
    using (Pen pen = new Pen(this._colorTable.DarkBorder))
      g.DrawLine(pen, bounds.Left, bounds.Top, bounds.Right, bounds.Top);
  }
}
