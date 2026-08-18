// Decompiled with JetBrains decompiler
// Type: Intermech.PropertyEditors.LCStepPainter
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

#nullable disable
namespace Intermech.PropertyEditors;

public class LCStepPainter
{
  internal static Font CaptionFont = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point);
  internal static Font CommentFont = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point);
  private static int captionHeight = 22;

  public static Image PaintStep(
    Image image,
    string caption,
    string comment,
    LCStepPaintData paintData,
    int minWidth)
  {
    Bitmap bitmap1 = new Bitmap(400, 200, PixelFormat.Format32bppPArgb);
    Bitmap bitmap2 = (Bitmap) null;
    using (Graphics graphics = Graphics.FromImage((Image) bitmap1))
    {
      StringFormat format = new StringFormat();
      format.Trimming = StringTrimming.EllipsisCharacter;
      int width = graphics.MeasureString(caption, LCStepPainter.CaptionFont, 300, format).ToSize().Width + paintData.RadiusSize;
      int num1 = 0;
      int captionHeight = LCStepPainter.captionHeight;
      if (image != null)
      {
        num1 = image.Height - LCStepPainter.captionHeight + 2;
        if (num1 < 0)
          num1 = 0;
        width = width + 4 + image.Width;
      }
      if (width < minWidth)
        width = minWidth;
      int y1 = captionHeight + num1;
      Rectangle rect1 = new Rectangle(0, 0, 400, 200);
      using (SolidBrush solidBrush = new SolidBrush(Color.Transparent))
        graphics.FillRectangle((Brush) solidBrush, rect1);
      Rectangle bounds = new Rectangle(0, 0, width, LCStepPainter.captionHeight);
      int y2 = num1;
      bounds.Offset(0, y2);
      GraphicsPath path = new GraphicsPath();
      int radius = paintData.Radius;
      if (radius > 0)
      {
        int num2 = radius * 2;
        int num3 = radius;
        path.AddLine(bounds.Left + num3, bounds.Top, bounds.Right - num2 - 1, bounds.Top);
        path.AddArc(bounds.Right - num2 - 1, bounds.Top, num2, num2, 270f, 90f);
        path.AddLine(bounds.Right, bounds.Top + num3, bounds.Right, bounds.Bottom);
        path.AddLine(bounds.Right, bounds.Bottom, bounds.Left - 1, bounds.Bottom);
        path.AddArc(bounds.Left, bounds.Top, num2, num2, 180f, 90f);
      }
      else
      {
        int num4 = -radius;
        int num5 = width;
        int bottom = bounds.Bottom;
        path.AddLine(0, num4 + bounds.Top, num4, bounds.Top);
        path.AddLine(num4, bounds.Top, num5 - num4, bounds.Top);
        path.AddLine(num5 - num4, bounds.Top, num5, num4 + bounds.Top);
        path.AddLine(num5, num4 + bounds.Top, num5, bottom);
        path.AddLine(bounds.Right, bounds.Bottom, bounds.Left - 1, bounds.Bottom);
        path.AddLine(bounds.Left, bottom, bounds.Left, bounds.Top + num4);
      }
      graphics.SmoothingMode = SmoothingMode.AntiAlias;
      using (LinearGradientBrush brush = paintData.CreateBrush(bounds))
        graphics.FillPath((Brush) brush, path);
      int x = 2;
      if (image != null)
      {
        graphics.DrawImage(image, 4, 2);
        x += image.Width + 2;
      }
      using (SolidBrush solidBrush = new SolidBrush(paintData.CaptionColor))
      {
        SizeF sizeF = graphics.MeasureString(caption, LCStepPainter.CaptionFont, rect1.Width - x - 4, format);
        RectangleF layoutRectangle = new RectangleF((float) x, (float) (y2 + 4), (float) (rect1.Width - x - 4), sizeF.Height);
        graphics.DrawString(caption, LCStepPainter.CaptionFont, (Brush) solidBrush, layoutRectangle, format);
      }
      if (comment == null || comment.Length == 0)
        comment = "";
      Size size = graphics.MeasureString(comment, LCStepPainter.CommentFont, width, format).ToSize();
      Rectangle rectangle = new Rectangle(0, y1, width, size.Height + 4);
      using (LinearGradientBrush commentBrush = paintData.CreateCommentBrush(rectangle))
        graphics.FillRectangle((Brush) commentBrush, rectangle);
      using (Pen pen = new Pen(Color.White))
        graphics.DrawRectangle(pen, rectangle);
      rectangle.Inflate(-2, -2);
      graphics.DrawString(comment, LCStepPainter.CommentFont, SystemBrushes.ControlText, (RectangleF) rectangle, format);
      RectangleF rect2 = new RectangleF(0.0f, 0.0f, (float) (width + 4), (float) (y1 + rectangle.Height + 5));
      bitmap2 = bitmap1.Clone(rect2, bitmap1.PixelFormat);
    }
    bitmap1.Dispose();
    return (Image) bitmap2;
  }
}
