// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.PdfDrawElement
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

#nullable disable
namespace Syncfusion.Pdf.Graphics;

public abstract class PdfDrawElement : PdfShapeElement
{
  private PdfPen m_pen;

  protected PdfDrawElement()
  {
  }

  protected PdfDrawElement(PdfPen pen)
    : this()
  {
    this.m_pen = pen;
  }

  protected virtual PdfPen GetPen() => this.m_pen != null ? this.m_pen : PdfPens.Black;

  public PdfPen Pen
  {
    get => this.m_pen;
    set => this.m_pen = value;
  }
}
