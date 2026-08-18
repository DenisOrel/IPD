// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.Fonts.PdfFontMetrics
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;

#nullable disable
namespace Syncfusion.Pdf.Graphics.Fonts;

internal class PdfFontMetrics : ICloneable
{
  public float Ascent;
  public float Descent;
  public int FirstChar;
  public float Height;
  public int LastChar;
  public int LineGap;
  private WidthTable m_widthTable;
  public string Name;
  public string PostScriptName;
  public float Size;
  public float SubScriptSizeFactor;
  public float SuperscriptSizeFactor;

  public object Clone()
  {
    PdfFontMetrics pdfFontMetrics = (PdfFontMetrics) this.MemberwiseClone();
    pdfFontMetrics.WidthTable = this.WidthTable.Clone();
    return (object) pdfFontMetrics;
  }

  public float GetAscent(PdfStringFormat format)
  {
    return this.Ascent * (1f / 1000f) * this.GetSize(format);
  }

  public float GetDescent(PdfStringFormat format)
  {
    return this.Descent * (1f / 1000f) * this.GetSize(format);
  }

  public float GetHeight(PdfStringFormat format)
  {
    return (double) this.GetDescent(format) < 0.0 ? this.GetAscent(format) - this.GetDescent(format) + this.GetLineGap(format) : this.GetAscent(format) + this.GetDescent(format) + this.GetLineGap(format);
  }

  public float GetLineGap(PdfStringFormat format)
  {
    return (float) this.LineGap * (1f / 1000f) * this.GetSize(format);
  }

  public float GetSize(PdfStringFormat format)
  {
    float size = this.Size;
    if (format != null)
    {
      switch (format.SubSuperScript)
      {
        case PdfSubSuperScript.SuperScript:
          return size / this.SuperscriptSizeFactor;
        case PdfSubSuperScript.SubScript:
          return size / this.SubScriptSizeFactor;
      }
    }
    return size;
  }

  public WidthTable WidthTable
  {
    get => this.m_widthTable;
    set => this.m_widthTable = value;
  }
}
