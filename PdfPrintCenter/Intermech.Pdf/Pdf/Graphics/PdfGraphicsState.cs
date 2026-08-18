// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.PdfGraphicsState
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;

#nullable disable
namespace Syncfusion.Pdf.Graphics;

public class PdfGraphicsState
{
  private PdfBrush m_brush;
  private float m_characterSpacing;
  private PdfColorSpace m_colorSpace;
  private PdfFont m_font;
  private PdfGraphics m_graphics;
  private PdfTransformationMatrix m_matrix;
  private PdfPen m_pen;
  private TextRenderingMode m_textRenderingMode;
  private float m_textScaling;
  private float m_wordSpacing;

  private PdfGraphicsState() => this.m_textScaling = 100f;

  internal PdfGraphicsState(PdfGraphics graphics, PdfTransformationMatrix matrix)
  {
    this.m_textScaling = 100f;
    if (graphics == null)
      throw new ArgumentNullException(nameof (graphics));
    if (matrix == null)
      throw new ArgumentNullException(nameof (matrix));
    this.m_graphics = graphics;
    this.m_matrix = matrix;
  }

  internal PdfBrush Brush
  {
    get => this.m_brush;
    set => this.m_brush = value;
  }

  internal float CharacterSpacing
  {
    get => this.m_characterSpacing;
    set => this.m_characterSpacing = value;
  }

  internal PdfColorSpace ColorSpace
  {
    get => this.m_colorSpace;
    set => this.m_colorSpace = value;
  }

  internal PdfFont Font
  {
    get => this.m_font;
    set => this.m_font = value;
  }

  internal PdfGraphics Graphics => this.m_graphics;

  internal PdfTransformationMatrix Matrix => this.m_matrix;

  internal PdfPen Pen
  {
    get => this.m_pen;
    set => this.m_pen = value;
  }

  internal TextRenderingMode TextRenderingMode
  {
    get => this.m_textRenderingMode;
    set => this.m_textRenderingMode = value;
  }

  internal float TextScaling
  {
    get => this.m_textScaling;
    set => this.m_textScaling = value;
  }

  internal float WordSpacing
  {
    get => this.m_wordSpacing;
    set => this.m_wordSpacing = value;
  }
}
