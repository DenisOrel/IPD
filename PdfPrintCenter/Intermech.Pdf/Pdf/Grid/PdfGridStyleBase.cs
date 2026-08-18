// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Grid.PdfGridStyleBase
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using System;

#nullable disable
namespace Syncfusion.Pdf.Grid;

public abstract class PdfGridStyleBase : ICloneable
{
  private PdfBrush m_backgroundBrush;
  private PdfFont m_font;
  private PdfBrush m_textBrush;
  private PdfPen m_textPen;

  public object Clone() => this.MemberwiseClone();

  public PdfBrush BackgroundBrush
  {
    get => this.m_backgroundBrush;
    set => this.m_backgroundBrush = value;
  }

  public PdfFont Font
  {
    get => this.m_font;
    set => this.m_font = value;
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
