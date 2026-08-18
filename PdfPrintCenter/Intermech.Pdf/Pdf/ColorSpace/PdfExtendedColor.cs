// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.ColorSpace.PdfExtendedColor
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

#nullable disable
namespace Syncfusion.Pdf.ColorSpace;

public abstract class PdfExtendedColor
{
  protected PdfColorSpaces m_colorspace;

  public PdfExtendedColor(PdfColorSpaces colorspace) => this.m_colorspace = colorspace;

  public PdfColorSpaces ColorSpace => this.m_colorspace;
}
