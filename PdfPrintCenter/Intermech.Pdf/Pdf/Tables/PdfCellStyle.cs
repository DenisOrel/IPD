// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Tables.PdfCellStyle
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using System;

#nullable disable
namespace Syncfusion.Pdf.Tables;

public class PdfCellStyle
{
  private PdfBrush m_backgroundBrush;
  private PdfPen m_borderPen;
  private PdfFont m_font;
  private PdfStringFormat m_stringFormat;
  private PdfBrush m_textBrush;
  private PdfPen m_textPen;

  public PdfCellStyle()
  {
    this.m_textBrush = PdfBrushes.Black;
    this.m_borderPen = PdfPens.Black;
  }

  public PdfCellStyle(PdfFont font, PdfBrush fontBrush, PdfPen borderPen)
  {
    if (font == null)
      throw new ArgumentNullException(nameof (font));
    if (fontBrush == null)
      throw new ArgumentNullException(nameof (fontBrush));
    if (borderPen == null)
      throw new ArgumentNullException(nameof (borderPen));
    this.m_font = font;
    this.m_textBrush = fontBrush;
    this.m_borderPen = borderPen;
  }

  public PdfBrush BackgroundBrush
  {
    get => this.m_backgroundBrush;
    set => this.m_backgroundBrush = value;
  }

  public PdfPen BorderPen
  {
    get => this.m_borderPen;
    set => this.m_borderPen = value;
  }

  public PdfFont Font
  {
    get
    {
      if (this.m_font == null)
        this.m_font = PdfDocument.DefaultFont;
      return this.m_font;
    }
    set => this.m_font = value != null ? value : throw new ArgumentNullException(nameof (Font));
  }

  public PdfStringFormat StringFormat
  {
    get => this.m_stringFormat;
    set => this.m_stringFormat = value;
  }

  public PdfBrush TextBrush
  {
    get => this.m_textBrush;
    set => this.m_textBrush = value;
  }

  public PdfPen TextPen
  {
    get => this.m_textPen;
    set => this.m_textPen = value;
  }
}
