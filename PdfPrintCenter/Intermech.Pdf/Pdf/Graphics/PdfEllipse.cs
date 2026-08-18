// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.PdfEllipse
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Drawing;

#nullable disable
namespace Syncfusion.Pdf.Graphics;

public class PdfEllipse : PdfRectangleArea
{
  protected PdfEllipse()
  {
  }

  public PdfEllipse(RectangleF rectangle)
    : base(rectangle)
  {
  }

  public PdfEllipse(PdfBrush brush, RectangleF rectangle)
    : base((PdfPen) null, brush, rectangle)
  {
  }

  public PdfEllipse(PdfPen pen, RectangleF rectangle)
    : base(pen, (PdfBrush) null, rectangle)
  {
  }

  public PdfEllipse(float width, float height)
    : this(0.0f, 0.0f, width, height)
  {
  }

  public PdfEllipse(PdfBrush brush, float width, float height)
    : this(brush, 0.0f, 0.0f, width, height)
  {
  }

  public PdfEllipse(PdfPen pen, PdfBrush brush, RectangleF rectangle)
    : base(pen, brush, rectangle)
  {
  }

  public PdfEllipse(PdfPen pen, float width, float height)
    : this(pen, 0.0f, 0.0f, width, height)
  {
  }

  public PdfEllipse(PdfPen pen, PdfBrush brush, float width, float height)
    : this(pen, brush, 0.0f, 0.0f, width, height)
  {
  }

  public PdfEllipse(float x, float y, float width, float height)
    : base(x, y, width, height)
  {
  }

  public PdfEllipse(PdfBrush brush, float x, float y, float width, float height)
    : base((PdfPen) null, brush, x, y, width, height)
  {
  }

  public PdfEllipse(PdfPen pen, float x, float y, float width, float height)
    : base(pen, (PdfBrush) null, x, y, width, height)
  {
  }

  public PdfEllipse(PdfPen pen, PdfBrush brush, float x, float y, float width, float height)
    : base(pen, brush, x, y, width, height)
  {
  }

  protected override void DrawInternal(PdfGraphics graphics)
  {
    if (graphics == null)
      throw new ArgumentNullException(nameof (graphics));
    graphics.DrawEllipse(this.GetPen(), this.Brush, this.Bounds);
  }

  public PointF Center => new PointF(this.X + this.RadiusX, this.Y + this.RadiusY);

  public float RadiusX => this.Width / 2f;

  public float RadiusY => this.Height / 2f;
}
