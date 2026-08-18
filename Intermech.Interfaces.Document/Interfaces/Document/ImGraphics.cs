// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.ImGraphics
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

#nullable disable
namespace Intermech.Interfaces.Document;

public class ImGraphics
{
  protected Graphics g;

  public ImGraphics()
  {
  }

  public ImGraphics(Graphics g) => this.g = g;

  public Graphics InternalGraphics
  {
    get => this.g;
    set => this.g = value;
  }

  public virtual Matrix Transform
  {
    get => this.g.Transform;
    set => this.g.Transform = value;
  }

  public virtual Matrix Transform1
  {
    get => this.g.Transform;
    set => this.g.Transform = value;
  }

  public virtual void ResetTransform() => this.g.ResetTransform();

  public virtual void RotateTransform(float angle) => this.g.RotateTransform(angle);

  public virtual void TranslateTransform(float dx, float dy) => this.g.TranslateTransform(dx, dy);

  public virtual void ScaleTransform(float sx, float sy) => this.g.ScaleTransform(sx, sy);

  public virtual void SetClip(Rectangle rect) => this.g.SetClip(rect);

  public virtual void SetClip(RectangleF rect) => this.g.SetClip(rect);

  public virtual GraphicsUnit PageUnit
  {
    get => this.g.PageUnit;
    set => this.g.PageUnit = value;
  }

  public virtual GraphicsState Save() => this.g.Save();

  public virtual void Restore(GraphicsState gstate) => this.g.Restore(gstate);

  public virtual void MultiplyTransform(Matrix matrix) => this.g.MultiplyTransform(matrix);

  public virtual void MultiplyTransform(Matrix matrix, System.Drawing.Drawing2D.MatrixOrder order)
  {
    this.g.MultiplyTransform(matrix, order);
  }

  public virtual CompositingQuality CompositingQuality
  {
    get => this.g.CompositingQuality;
    set => this.g.CompositingQuality = value;
  }

  public virtual void FillRectangle(Brush brush, Rectangle rect)
  {
    this.g.FillRectangle(brush, rect);
  }

  public virtual void FillRectangle(Brush brush, RectangleF rect)
  {
    this.g.FillRectangle(brush, rect);
  }

  public virtual float DpiX => this.g.DpiX;

  public virtual float DpiY => this.g.DpiY;

  public virtual void DrawLine(Pen pen, Point pt1, Point pt2) => this.g.DrawLine(pen, pt1, pt2);

  public virtual void DrawLine(Pen pen, PointF pt1, PointF pt2) => this.g.DrawLine(pen, pt1, pt2);

  public virtual void DrawImage(Image image, Point point) => this.g.DrawImage(image, point);

  public virtual void DrawImage(Image image, PointF point) => this.g.DrawImage(image, point);

  public virtual void DrawRectangle(Pen pen, float x, float y, float width, float height)
  {
    this.g.DrawRectangle(pen, x, y, width, height);
  }

  public virtual RectangleF ClipBounds => this.g.ClipBounds;

  public virtual void DrawImage(
    Image image,
    PointF[] destPoints,
    RectangleF srcRect,
    GraphicsUnit srcUnit,
    ImageAttributes imageAttr)
  {
    this.g.DrawImage(image, destPoints, srcRect, srcUnit, imageAttr);
  }

  public virtual void DrawImage(
    Image image,
    PointF[] destPoints,
    RectangleF srcRect,
    GraphicsUnit srcUnit)
  {
    this.g.DrawImage(image, destPoints, srcRect, srcUnit);
  }

  public virtual void DrawPath(Pen pen, GraphicsPath path) => this.g.DrawPath(pen, path);

  public virtual void DrawArc(Pen pen, RectangleF rect, float startAngle, float sweepAngle)
  {
    this.g.DrawArc(pen, rect, startAngle, sweepAngle);
  }

  public virtual void SetClip(Rectangle rect, CombineMode combineMode)
  {
    this.g.SetClip(rect, combineMode);
  }

  public virtual void SetClip(Region region, CombineMode combineMode)
  {
    this.g.SetClip(region, combineMode);
  }

  public virtual void DrawImage(Image image, RectangleF rect) => this.g.DrawImage(image, rect);

  public virtual void DrawImageUnscaled(Image image, int x, int y)
  {
    this.g.DrawImageUnscaled(image, x, y);
  }

  public virtual void DrawImage(
    Image image,
    Rectangle destRect,
    int srcX,
    int srcY,
    int srcWidth,
    int srcHeight,
    GraphicsUnit srcUnit,
    ImageAttributes imageAttr)
  {
    this.g.DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight, srcUnit, imageAttr);
  }

  public virtual SizeF MeasureString(
    string text,
    Font font,
    SizeF layoutArea,
    StringFormat stringFormat,
    out int charactersFitted,
    out int linesFilled)
  {
    return this.g.MeasureString(text, font, layoutArea, stringFormat, out charactersFitted, out linesFilled);
  }

  public virtual void DrawString(
    string s,
    Font font,
    Brush brush,
    RectangleF layoutRectangle,
    StringFormat format)
  {
    this.g.DrawString(s, font, brush, layoutRectangle, format);
  }

  public virtual Region Clip
  {
    get => this.g.Clip;
    set => this.g.Clip = value;
  }
}
