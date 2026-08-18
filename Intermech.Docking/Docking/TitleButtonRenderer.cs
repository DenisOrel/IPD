
// Type: Intermech.Docking.TitleButtonRenderer
// Assembly: Intermech.Docking, Version=4.0.25.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F97F850-2D29-46D1-A3D7-6B2A02E86D46
:\IPS\Client\Intermech.Docking.dll

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Intermech.Docking;

internal class TitleButtonRenderer
{
  public static void DrawCloseButtonBackground(Graphics g, Rectangle aBounds, DrawItemState state)
  {
    SmoothingMode smoothingMode = g.SmoothingMode;
    g.SmoothingMode = SmoothingMode.HighQuality;
    try
    {
      if ((state & DrawItemState.HotLight) != DrawItemState.HotLight)
        return;
      Rectangle rect = new Rectangle(aBounds.Left + 1, aBounds.Top + 2, 11, 11);
      using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect, Color.FromArgb(215, 75, 75), Color.FromArgb(246, 96 /*0x60*/, 96 /*0x60*/), LinearGradientMode.Vertical))
        g.FillRectangle((Brush) linearGradientBrush, rect);
      using (Pen pen = new Pen(Color.FromArgb(181, 60, 60)))
        g.DrawRectangle(pen, rect);
    }
    finally
    {
      g.SmoothingMode = smoothingMode;
    }
  }

  public static void DrawDocClose(Graphics g, Rectangle bounds, Pen pen)
  {
    int num1 = bounds.Left + bounds.Width / 2 - 1;
    int num2 = bounds.Top + bounds.Height / 2 - 1;
    g.DrawLine(pen, num1 - 3, num2 - 3, num1 + 4, num2 + 4);
    g.DrawLine(pen, num1 - 2, num2 - 3, num1 + 4, num2 + 3);
    g.DrawLine(pen, num1 - 3, num2 - 2, num1 + 3, num2 + 4);
    g.DrawLine(pen, num1 + 4, num2 - 3, num1 - 3, num2 + 4);
    g.DrawLine(pen, num1 + 3, num2 - 3, num1 - 3, num2 + 3);
    g.DrawLine(pen, num1 + 4, num2 - 2, num1 - 2, num2 + 4);
  }

  private static void DrawScroll(Graphics g, Point[] points, Color c, bool enabled)
  {
    using (Pen pen = new Pen(c))
      g.DrawPolygon(pen, points);
    if (!enabled)
      return;
    using (SolidBrush solidBrush = new SolidBrush(c))
      g.FillPolygon((Brush) solidBrush, points);
  }

  public static void DrawRightScroll(
    Graphics graphics,
    Rectangle bounds,
    Color color,
    bool enabled)
  {
    int num1 = bounds.Left + bounds.Width / 2;
    int num2 = bounds.Top + bounds.Height / 2;
    Point[] points = new Point[3]
    {
      new Point(num1 - 2, num2 - 5),
      new Point(num1 + 2, num2 - 1),
      new Point(num1 - 2, num2 + 3)
    };
    TitleButtonRenderer.DrawScroll(graphics, points, color, enabled);
  }

  public static void DrawPin(Graphics g, Rectangle bounds, Pen pen, bool toggle)
  {
    int num1 = bounds.Left + bounds.Width / 2;
    int num2 = bounds.Top + bounds.Height / 2;
    if (toggle)
    {
      g.DrawLine(pen, num1 - 5, num2, num1 - 2, num2);
      g.DrawLine(pen, num1 - 2, num2 - 3, num1 - 2, num2 + 3);
      g.DrawLine(pen, num1 - 2, num2 - 2, num1 + 4, num2 - 2);
      g.DrawLine(pen, num1 - 2, num2 + 1, num1 + 4, num2 + 1);
      g.DrawLine(pen, num1 - 2, num2 + 2, num1 + 4, num2 + 2);
      g.DrawLine(pen, num1 + 4, num2 - 2, num1 + 4, num2 + 2);
    }
    else
    {
      int num3 = num2 - 1;
      int num4 = num1 - 1;
      g.DrawLine(pen, num4 - 3, num3 + 2, num4 + 3, num3 + 2);
      g.DrawLine(pen, num4 - 2, num3 - 3, num4 - 2, num3 + 2);
      g.DrawLine(pen, num4 - 2, num3 - 3, num4 + 2, num3 - 3);
      g.DrawLine(pen, num4 + 1, num3 - 3, num4 + 1, num3 + 2);
      g.DrawLine(pen, num4 + 2, num3 - 3, num4 + 2, num3 + 2);
      g.DrawLine(pen, num4, num3 + 2, num4, num3 + 5);
    }
  }

  public static void DrawClose(Graphics g, Rectangle bounds, Pen pen)
  {
    int num1 = bounds.Left + bounds.Width / 2 - 1;
    int num2 = bounds.Top + bounds.Height / 2 + 1;
    g.DrawLine(pen, num1 - 3, num2 - 4, num1 + 3, num2 + 2);
    g.DrawLine(pen, num1 - 2, num2 - 4, num1 + 4, num2 + 2);
    g.DrawLine(pen, num1 - 3, num2 + 2, num1 + 3, num2 - 4);
    g.DrawLine(pen, num1 - 2, num2 + 2, num1 + 4, num2 - 4);
  }

  public static void DrawDocList(Graphics g, Rectangle bounds, Pen pen)
  {
    int num1 = bounds.Left + bounds.Width / 2 - 1;
    int num2 = bounds.Top + bounds.Height / 2;
    g.DrawLine(pen, num1 - 3, num2 - 4, num1 + 3, num2 - 4);
    g.DrawLine(pen, num1 - 3, num2 - 3, num1 + 3, num2 - 3);
    g.DrawLine(pen, num1 - 3, num2 - 1, num1 + 3, num2 - 1);
    g.DrawLine(pen, num1 - 2, num2, num1 + 2, num2);
    g.DrawLine(pen, num1 - 1, num2 + 1, num1 + 1, num2 + 1);
    g.DrawLine(pen, num1, num2 + 2, num1, num2 + 1);
  }

  public static void DrawLeftScroll(Graphics g, Rectangle bounds, Color color, bool enabled)
  {
    int num1 = bounds.Left + bounds.Width / 2;
    int num2 = bounds.Top + bounds.Height / 2;
    Point[] points = new Point[3]
    {
      new Point(num1 + 2, num2 - 5),
      new Point(num1 - 2, num2 - 1),
      new Point(num1 + 2, num2 + 3)
    };
    TitleButtonRenderer.DrawScroll(g, points, color, enabled);
  }
}
