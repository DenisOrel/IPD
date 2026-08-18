
// Type: Intermech.Client.Core.Organizer.SplitterRenderer
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Drawing;
using System.Drawing.Drawing2D;


namespace Intermech.Client.Core.Organizer;

/// <summary>
/// 
/// </summary>
public class SplitterRenderer
{
  private ColorTable _colorTbl = new ColorTable();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="g"></param>
  /// <param name="bounds"></param>
  public void DrawBackground(Graphics g, Rectangle bounds)
  {
    bool flag = bounds.Width > bounds.Height;
    ColorBlend colorBlend = new ColorBlend();
    colorBlend.Colors = new Color[2]
    {
      this._colorTbl.SplitterLight,
      this._colorTbl.SplitterDark
    };
    colorBlend.Positions = new float[2]{ 0.0f, 1f };
    bounds.Height = bounds.Height == 0 ? 1 : bounds.Height;
    bounds.Width = bounds.Width == 0 ? 1 : bounds.Width;
    using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(flag ? new Point(0, bounds.Top) : new Point(bounds.Left, 0), flag ? new Point(0, bounds.Bottom) : new Point(bounds.Right, 0), Color.White, Color.Black))
    {
      linearGradientBrush.InterpolationColors = colorBlend;
      g.FillRectangle((Brush) linearGradientBrush, bounds);
    }
    int x = bounds.Right - bounds.Width / 2;
    int y = bounds.Bottom - bounds.Height / 2;
    using (SolidBrush solidBrush = new SolidBrush(this._colorTbl.DarkBorder))
    {
      if (flag)
      {
        g.FillRectangle((Brush) solidBrush, x - 8, y - 1, 2, 2);
        g.FillRectangle((Brush) solidBrush, x - 4, y - 1, 2, 2);
        g.FillRectangle((Brush) solidBrush, x, y - 1, 2, 2);
        g.FillRectangle((Brush) solidBrush, x + 4, y - 1, 2, 2);
        g.FillRectangle((Brush) solidBrush, x + 8, y - 1, 2, 2);
        solidBrush.Color = this._colorTbl.SplitterHighlights;
        g.FillRectangle((Brush) solidBrush, x - 7, y, 2, 2);
        g.FillRectangle((Brush) solidBrush, x - 3, y, 2, 2);
        g.FillRectangle((Brush) solidBrush, x + 1, y, 2, 2);
        g.FillRectangle((Brush) solidBrush, x + 5, y, 2, 2);
        g.FillRectangle((Brush) solidBrush, x + 9, y, 2, 2);
      }
      else
      {
        g.FillRectangle((Brush) solidBrush, x - 1, y - 8, 2, 2);
        g.FillRectangle((Brush) solidBrush, x - 1, y - 4, 2, 2);
        g.FillRectangle((Brush) solidBrush, x - 1, y, 2, 2);
        g.FillRectangle((Brush) solidBrush, x - 1, y + 4, 2, 2);
        g.FillRectangle((Brush) solidBrush, x - 1, y + 8, 2, 2);
        solidBrush.Color = this._colorTbl.SplitterHighlights;
        g.FillRectangle((Brush) solidBrush, x, y - 7, 2, 2);
        g.FillRectangle((Brush) solidBrush, x, y - 3, 2, 2);
        g.FillRectangle((Brush) solidBrush, x, y + 1, 2, 2);
        g.FillRectangle((Brush) solidBrush, x, y + 5, 2, 2);
        g.FillRectangle((Brush) solidBrush, x, y + 9, 2, 2);
      }
      solidBrush.Color = this._colorTbl.DarkBorder;
      using (Pen pen = new Pen((Brush) solidBrush, 1f))
        g.DrawLine(pen, new Point(bounds.Left, bounds.Top), new Point(bounds.Right, bounds.Top));
    }
  }
}
