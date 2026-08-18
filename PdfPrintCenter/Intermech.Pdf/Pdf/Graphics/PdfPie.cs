// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.PdfPie
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Drawing;

#nullable disable
namespace Syncfusion.Pdf.Graphics;

public class PdfPie : PdfEllipsePart
{
  protected PdfPie()
  {
  }

  public PdfPie(RectangleF rectangle, float startAngle, float sweepAngle)
    : base(rectangle, startAngle, sweepAngle)
  {
  }

  public PdfPie(PdfBrush brush, RectangleF rectangle, float startAngle, float sweepAngle)
    : this(rectangle, startAngle, sweepAngle)
  {
    this.Brush = brush;
  }

  public PdfPie(PdfPen pen, RectangleF rectangle, float startAngle, float sweepAngle)
    : base(pen, (PdfBrush) null, rectangle, startAngle, sweepAngle)
  {
  }

  public PdfPie(float width, float height, float startAngle, float sweepAngle)
    : this(0.0f, 0.0f, width, height, startAngle, sweepAngle)
  {
  }

  public PdfPie(PdfBrush brush, float width, float height, float startAngle, float sweepAngle)
    : this(brush, 0.0f, 0.0f, width, height, startAngle, sweepAngle)
  {
  }

  public PdfPie(
    PdfPen pen,
    PdfBrush brush,
    RectangleF rectangle,
    float startAngle,
    float sweepAngle)
    : this(rectangle, startAngle, sweepAngle)
  {
    this.Pen = pen;
    this.Brush = brush;
  }

  public PdfPie(PdfPen pen, float width, float height, float startAngle, float sweepAngle)
    : this(pen, 0.0f, 0.0f, width, height, startAngle, sweepAngle)
  {
  }

  public PdfPie(
    PdfPen pen,
    PdfBrush brush,
    float width,
    float height,
    float startAngle,
    float sweepAngle)
    : this(pen, brush, 0.0f, 0.0f, width, height, startAngle, sweepAngle)
  {
  }

  public PdfPie(float x, float y, float width, float height, float startAngle, float sweepAngle)
    : base(x, y, width, height, startAngle, sweepAngle)
  {
  }

  public PdfPie(
    PdfBrush brush,
    float x,
    float y,
    float width,
    float height,
    float startAngle,
    float sweepAngle)
    : this(x, y, width, height, startAngle, sweepAngle)
  {
    this.Brush = brush;
  }

  public PdfPie(
    PdfPen pen,
    float x,
    float y,
    float width,
    float height,
    float startAngle,
    float sweepAngle)
    : base(pen, (PdfBrush) null, x, y, width, height, startAngle, sweepAngle)
  {
  }

  public PdfPie(
    PdfPen pen,
    PdfBrush brush,
    float x,
    float y,
    float width,
    float height,
    float startAngle,
    float sweepAngle)
    : this(x, y, width, height, startAngle, sweepAngle)
  {
    this.Pen = pen;
    this.Brush = brush;
  }

  protected override void DrawInternal(PdfGraphics graphics)
  {
    if (graphics == null)
      throw new ArgumentNullException(nameof (graphics));
    graphics.DrawPie(this.GetPen(), this.Brush, this.Bounds, this.StartAngle, this.SweepAngle);
  }
}
