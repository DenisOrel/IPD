// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.PdfRectangleArea
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System.Drawing;

#nullable disable
namespace Syncfusion.Pdf.Graphics;

public abstract class PdfRectangleArea : PdfFillElement
{
  private RectangleF m_rect;

  protected PdfRectangleArea()
  {
  }

  protected PdfRectangleArea(RectangleF rectangle)
    : this()
  {
    this.m_rect = rectangle;
  }

  protected PdfRectangleArea(PdfPen pen, PdfBrush brush, RectangleF rectangle)
    : base(pen, brush)
  {
    this.m_rect = rectangle;
  }

  protected PdfRectangleArea(float x, float y, float width, float height)
    : this()
  {
    this.m_rect = new RectangleF(x, y, width, height);
  }

  protected PdfRectangleArea(
    PdfPen pen,
    PdfBrush brush,
    float x,
    float y,
    float width,
    float height)
    : base(pen, brush)
  {
    this.m_rect = new RectangleF(x, y, width, height);
  }

  protected override RectangleF GetBoundsInternal() => this.Bounds;

  public RectangleF Bounds
  {
    get => this.m_rect;
    set => this.m_rect = value;
  }

  public float Height
  {
    get => this.m_rect.Height;
    set => this.m_rect.Height = value;
  }

  public SizeF Size
  {
    get => this.m_rect.Size;
    set => this.m_rect.Size = value;
  }

  public float Width
  {
    get => this.m_rect.Width;
    set => this.m_rect.Width = value;
  }

  public float X
  {
    get => this.m_rect.X;
    set => this.m_rect.X = value;
  }

  public float Y
  {
    get => this.m_rect.Y;
    set => this.m_rect.Y = value;
  }
}
