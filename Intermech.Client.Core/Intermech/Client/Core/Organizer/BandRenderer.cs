
// Type: Intermech.Client.Core.Organizer.BandRenderer
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
public class BandRenderer
{
  private ColorTable _colorTbl = new ColorTable();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="g"></param>
  /// <param name="bounds"></param>
  public void DrawBackground(Graphics g, Rectangle bounds)
  {
    using (Brush brush = (Brush) new SolidBrush(this._colorTbl.Background))
      g.FillRectangle(brush, bounds);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="g"></param>
  /// <param name="bounds"></param>
  /// <param name="text"></param>
  /// <param name="font"></param>
  /// <param name="state"></param>
  public void DrawCollapsedBand(
    Graphics g,
    Rectangle bounds,
    string text,
    Font font,
    InputState state)
  {
    using (SolidBrush solidBrush = new SolidBrush(this._colorTbl.BandCollapsedBg))
    {
      switch (state)
      {
        case InputState.Clicked:
          solidBrush.Color = this._colorTbl.BandCollapsedClicked;
          break;
        case InputState.Hovered:
          solidBrush.Color = this._colorTbl.BandCollapsedFocused;
          break;
      }
      g.FillRectangle((Brush) solidBrush, bounds);
    }
    using (Pen pen = new Pen(this._colorTbl.DarkBorder))
    {
      g.DrawLine(pen, new Point(bounds.Left, bounds.Top), new Point(bounds.Right, bounds.Top));
      pen.Color = this._colorTbl.HeaderBgInnerBorder;
      if (state == InputState.Normal)
      {
        g.DrawLine(pen, new Point(bounds.Left, bounds.Top + 1), new Point(bounds.Right, bounds.Top + 1));
        g.DrawLine(pen, new Point(bounds.Left, bounds.Top + 1), new Point(bounds.Left, bounds.Bottom));
      }
    }
    using (Brush brush = (Brush) new SolidBrush(this._colorTbl.Text))
    {
      Point point = new Point(bounds.X + bounds.Width / 2 - 7, bounds.Y + bounds.Height / 2);
      Matrix transform = g.Transform;
      transform.RotateAt(270f, (PointF) point);
      g.Transform = transform;
      g.DrawString(text, font, brush, (PointF) point);
    }
  }
}
