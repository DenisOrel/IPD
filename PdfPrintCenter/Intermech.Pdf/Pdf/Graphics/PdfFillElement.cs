// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.PdfFillElement
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

#nullable disable
namespace Syncfusion.Pdf.Graphics;

public abstract class PdfFillElement : PdfDrawElement
{
  private PdfBrush m_brush;

  protected PdfFillElement()
  {
  }

  protected PdfFillElement(PdfBrush brush)
    : this()
  {
    this.m_brush = brush;
  }

  protected PdfFillElement(PdfPen pen)
    : base(pen)
  {
  }

  protected PdfFillElement(PdfPen pen, PdfBrush brush)
    : this(pen)
  {
    this.m_brush = brush;
  }

  protected override PdfPen GetPen()
  {
    return this.m_brush == null && this.Pen == null ? PdfPens.Black : this.Pen;
  }

  public PdfBrush Brush
  {
    get => this.m_brush;
    set => this.m_brush = value;
  }
}
