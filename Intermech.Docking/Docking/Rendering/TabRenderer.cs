
// Type: Intermech.Docking.Rendering.TabRenderer
// Assembly: Intermech.Docking, Version=4.0.25.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F97F850-2D29-46D1-A3D7-6B2A02E86D46
:\IPS\Client\Intermech.Docking.dll

using System;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace Intermech.Docking.Rendering;

internal class TabRenderer
{
  public static void DrawTabStripTab(
    Graphics g,
    Rectangle bounds,
    Image image,
    string text,
    Font font,
    Color backColor,
    Color foreColor,
    Brush brush)
  {
    Rectangle rect = bounds;
    rect.Inflate(-1, 0);
    --rect.Height;
    using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new Point(rect.X, rect.Y - 1), new Point(rect.X, rect.Bottom), backColor, foreColor))
      g.FillRectangle((Brush) linearGradientBrush, rect);
    Point[] points = new Point[6]
    {
      new Point(bounds.Left, bounds.Top),
      new Point(bounds.Left, bounds.Bottom - 3),
      new Point(bounds.Left + 2, bounds.Bottom - 1),
      new Point(bounds.Right - 3, bounds.Bottom - 1),
      new Point(bounds.Right - 1, bounds.Bottom - 3),
      new Point(bounds.Right - 1, bounds.Top)
    };
    g.DrawLines(SystemPens.ControlDark, points);
    if (bounds.Width >= 24)
      g.DrawImage(image, new Rectangle(bounds.X + 4, bounds.Y + 2, image.Width, image.Height));
    bounds.X += 23;
    bounds.Width -= 25;
    if (bounds.Width <= 8)
      return;
    g.DrawString(text, font, brush, (RectangleF) bounds, EverettRenderer.StandardStringFormat);
  }

  public static void DrawCollapsedTab(
    Graphics g,
    Rectangle bounds,
    DockSide dockSide,
    Image image,
    string text,
    Font font,
    Color backColor,
    Color foreColor,
    Brush textBrush,
    bool vertical)
  {
    bool flag = false;
    Point[] points = new Point[6];
    switch (dockSide)
    {
      case DockSide.Top:
        points[0] = new Point(bounds.Left, bounds.Top);
        points[1] = new Point(bounds.Right, bounds.Top);
        points[2] = new Point(bounds.Right, bounds.Bottom - 2);
        points[3] = new Point(bounds.Right - 2, bounds.Bottom);
        points[4] = new Point(bounds.Left + 2, bounds.Bottom);
        points[5] = new Point(bounds.Left, bounds.Bottom - 2);
        break;
      case DockSide.Bottom:
        points[0] = new Point(bounds.Left + 2, bounds.Top);
        points[1] = new Point(bounds.Right - 2, bounds.Top);
        points[2] = new Point(bounds.Right, bounds.Top + 2);
        points[3] = new Point(bounds.Right, bounds.Bottom);
        points[4] = new Point(bounds.Left, bounds.Bottom);
        points[5] = new Point(bounds.Left, bounds.Top + 2);
        break;
      case DockSide.Left:
        points[0] = new Point(bounds.Left, bounds.Top);
        points[1] = new Point(bounds.Right - 2, bounds.Top);
        points[2] = new Point(bounds.Right, bounds.Top + 2);
        points[3] = new Point(bounds.Right, bounds.Bottom - 2);
        points[4] = new Point(bounds.Right - 2, bounds.Bottom);
        points[5] = new Point(bounds.Left, bounds.Bottom);
        flag = true;
        break;
      case DockSide.Right:
        points[0] = new Point(bounds.Left + 2, bounds.Top);
        points[1] = new Point(bounds.Right, bounds.Top);
        points[2] = new Point(bounds.Right, bounds.Bottom);
        points[3] = new Point(bounds.Left + 2, bounds.Bottom);
        points[4] = new Point(bounds.Left, bounds.Bottom - 2);
        points[5] = new Point(bounds.Left, bounds.Top + 2);
        flag = true;
        break;
    }
    LinearGradientMode linearGradientMode = dockSide == DockSide.Left || dockSide == DockSide.Right ? LinearGradientMode.Horizontal : LinearGradientMode.Vertical;
    using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(bounds, backColor, foreColor, linearGradientMode))
      g.FillPolygon((Brush) linearGradientBrush, points);
    g.DrawPolygon(SystemPens.ControlDark, points);
    bounds.Inflate(-2, -2);
    if (flag)
      bounds.Offset(0, 1);
    else
      bounds.Offset(1, 0);
    if (image != null)
      g.DrawImage(image, new Rectangle(bounds.Left, bounds.Top, image.Width, image.Height));
    if (text.Length == 0)
      return;
    int num = vertical ? 21 : 23;
    if (flag)
    {
      bounds.Offset(0, num);
      g.DrawString(text, font, textBrush, (RectangleF) bounds, EverettRenderer.GetStandardVerticalStringFormat());
    }
    else
    {
      bounds.Offset(num, 0);
      g.DrawString(text, font, textBrush, (RectangleF) bounds, EverettRenderer.StandardStringFormat);
    }
  }

  public static void DrawDocumentStripTab(
    Graphics g,
    Rectangle bounds,
    Image image,
    string text,
    Font font,
    Color A_5,
    Color A_6,
    Brush A_7,
    Color A_8,
    Color A_9,
    Color A_10,
    bool selected,
    int tabSize,
    int tabExtra,
    StringFormat stringFormat,
    int deltaClose)
  {
    using (Pen pen = new Pen(A_8))
    {
      g.DrawLine(pen, bounds.Left, bounds.Bottom - 2, bounds.Left + 1, bounds.Bottom - 2);
      g.DrawLine(pen, bounds.Left + 1, bounds.Bottom - 2, bounds.Left + tabSize - 3, bounds.Top + 2);
      g.DrawLine(pen, bounds.Left + tabSize - 3, bounds.Top + 2, bounds.Left + tabSize - 2, bounds.Top + 2);
      g.DrawLine(pen, bounds.Left + tabSize - 1, bounds.Top + 1, bounds.Left + tabSize, bounds.Top + 1);
      g.DrawLine(pen, bounds.Left + tabSize + 1, bounds.Top, bounds.Right - 3, bounds.Top);
      g.DrawLine(pen, bounds.Right - 3, bounds.Top, bounds.Right - 1, bounds.Top + 2);
      g.DrawLine(pen, bounds.Right - 1, bounds.Top + 2, bounds.Right - 1, bounds.Bottom - 2);
    }
    using (Pen pen = new Pen(A_9))
    {
      g.DrawLine(pen, bounds.Left + 2, bounds.Bottom - 2, bounds.Left + tabSize - 3, bounds.Top + 3);
      g.DrawLine(pen, bounds.Left + tabSize - 3, bounds.Top + 3, bounds.Left + tabSize - 2, bounds.Top + 3);
      g.DrawLine(pen, bounds.Left + tabSize - 1, bounds.Top + 2, bounds.Left + tabSize, bounds.Top + 2);
      g.DrawLine(pen, bounds.Left + tabSize + 1, bounds.Top + 1, bounds.Right - 4, bounds.Top + 1);
    }
    using (Pen pen = new Pen(A_10))
    {
      g.DrawLine(pen, bounds.Right - 3, bounds.Top + 1, bounds.Right - 2, bounds.Top + 2);
      g.DrawLine(pen, bounds.Right - 2, bounds.Top + 2, bounds.Right - 2, bounds.Bottom - 2);
    }
    Point[] points = new Point[5]
    {
      new Point(bounds.Left + 2, bounds.Bottom - 1),
      new Point(bounds.Left + tabSize - 3, bounds.Top + 4),
      new Point(bounds.Left + tabSize + 1, bounds.Top + 2),
      new Point(bounds.Right - 2, bounds.Top + 2),
      new Point(bounds.Right - 2, bounds.Bottom - 1)
    };
    using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(bounds, A_5, A_6, LinearGradientMode.Vertical))
      g.FillPolygon((Brush) linearGradientBrush, points);
    if (selected)
    {
      using (Pen pen = new Pen(A_6))
        g.DrawLine(pen, bounds.Left, bounds.Bottom - 1, bounds.Right - 1, bounds.Bottom - 1);
    }
    bounds.X += tabExtra;
    bounds.Width -= tabExtra;
    if (image != null)
    {
      g.DrawImage(image, bounds.X + 4, bounds.Y + 2, image.Width, 16 /*0x10*/);
      int num = RendererBase.ImageWidth(image) + 4;
      bounds.X += num;
      bounds.Width -= num;
    }
    bounds.Width -= deltaClose;
    if (bounds.Width <= 8)
      return;
    string tabTextSeparator = DockingConsts.TabTextSeparator;
    string str = string.Empty;
    int length = text.IndexOf(tabTextSeparator);
    if (length >= 0)
    {
      str = text.Substring(length + tabTextSeparator.Length);
      text = length <= 0 ? string.Empty : text.Substring(0, length);
    }
    Font font1 = (Font) null;
    SolidBrush solidBrush = (SolidBrush) null;
    if (!string.IsNullOrEmpty(str))
    {
      font1 = new Font(font, FontStyle.Regular);
      solidBrush = new SolidBrush(Color.Red);
    }
    SizeF sizeF = g.MeasureString(str, font1, 999, stringFormat);
    int int32 = Convert.ToInt32(sizeF.Width);
    sizeF = g.MeasureString(text, font, 999, stringFormat);
    int width = Convert.ToInt32(sizeF.Width) + 10;
    RectangleF layoutRectangle1 = new RectangleF((float) bounds.X, (float) bounds.Y, (float) width, (float) bounds.Height);
    RectangleF layoutRectangle2 = new RectangleF((float) (bounds.X + width), (float) bounds.Y, (float) int32, (float) bounds.Height);
    if (!string.IsNullOrEmpty(str))
    {
      g.DrawString(text, font, SystemBrushes.ControlText, layoutRectangle1, stringFormat);
      g.DrawString(str, font1, SystemBrushes.ControlText, layoutRectangle2, stringFormat);
      font1.Dispose();
      solidBrush.Dispose();
    }
    else
      g.DrawString(text, font, SystemBrushes.ControlText, (RectangleF) bounds, stringFormat);
  }
}
