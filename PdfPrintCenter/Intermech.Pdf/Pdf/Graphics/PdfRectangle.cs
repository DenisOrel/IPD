// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.PdfRectangle
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Drawing;

#nullable disable
namespace Syncfusion.Pdf.Graphics;

public class PdfRectangle : PdfRectangleArea
{
  private PdfRectangle()
  {
  }

  public PdfRectangle(RectangleF rectangle)
    : base(rectangle)
  {
  }

  public PdfRectangle(PdfBrush brush, RectangleF rectangle)
    : base((PdfPen) null, brush, rectangle)
  {
  }

  public PdfRectangle(PdfPen pen, RectangleF rectangle)
    : base(pen, (PdfBrush) null, rectangle)
  {
  }

  public PdfRectangle(float width, float height)
    : this(0.0f, 0.0f, width, height)
  {
  }

  public PdfRectangle(PdfBrush brush, float width, float height)
    : this(brush, 0.0f, 0.0f, width, height)
  {
  }

  public PdfRectangle(PdfPen pen, PdfBrush brush, RectangleF rectangle)
    : base(pen, brush, rectangle)
  {
  }

  public PdfRectangle(PdfPen pen, float width, float height)
    : this(pen, 0.0f, 0.0f, width, height)
  {
  }

  public PdfRectangle(PdfPen pen, PdfBrush brush, float width, float height)
    : this(pen, brush, 0.0f, 0.0f, width, height)
  {
  }

  public PdfRectangle(float x, float y, float width, float height)
    : base(x, y, width, height)
  {
  }

  public PdfRectangle(PdfBrush brush, float x, float y, float width, float height)
    : base((PdfPen) null, brush, x, y, width, height)
  {
  }

  public PdfRectangle(PdfPen pen, float x, float y, float width, float height)
    : base(pen, (PdfBrush) null, x, y, width, height)
  {
  }

  public PdfRectangle(PdfPen pen, PdfBrush brush, float x, float y, float width, float height)
    : base(pen, brush, x, y, width, height)
  {
  }

  protected override void DrawInternal(PdfGraphics graphics)
  {
    if (graphics == null)
      throw new ArgumentNullException(nameof (graphics));
    graphics.DrawRectangle(this.GetPen(), this.Brush, this.Bounds);
  }
}
