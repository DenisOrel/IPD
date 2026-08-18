// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.ColorSpace.PdfICCColor
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;

#nullable disable
namespace Syncfusion.Pdf.ColorSpace;

public class PdfICCColor : PdfExtendedColor
{
  private PdfICCColorSpace m_colorspaces;
  private double[] m_components;

  public PdfICCColor(PdfColorSpaces colorspace)
    : base(colorspace)
  {
    this.m_colorspaces = colorspace as PdfICCColorSpace;
  }

  public double[] ColorComponents
  {
    get => this.m_components;
    set
    {
      if (value == null)
        throw new ArgumentNullException(nameof (ColorComponents), "ColorComponents array cannot be null.");
      PdfICCColorSpace colorSpace = this.ColorSpace as PdfICCColorSpace;
      if (value.Length != (int) colorSpace.ColorComponents)
        throw new ArgumentOutOfRangeException(nameof (ColorComponents), "Array length must match the number of color components defined on the underlying ICC colorspace.");
      this.m_components = value;
    }
  }

  internal PdfICCColorSpace ColorSpaces => this.m_colorspaces;
}
