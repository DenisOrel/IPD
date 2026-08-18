// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.ColorSpace.PdfSeparationColor
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

#nullable disable
namespace Syncfusion.Pdf.ColorSpace;

public class PdfSeparationColor : PdfExtendedColor
{
  private double m_tint;

  public PdfSeparationColor(PdfColorSpaces colorspace)
    : base(colorspace)
  {
    this.m_tint = 1.0;
  }

  public double Tint
  {
    get => this.m_tint;
    set => this.m_tint = value;
  }
}
