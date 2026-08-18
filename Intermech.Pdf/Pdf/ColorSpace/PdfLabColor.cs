// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.ColorSpace.PdfLabColor
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;


namespace Syncfusion.Pdf.ColorSpace
{
    public class PdfLabColor(PdfColorSpaces colorspace) : PdfExtendedColor(colorspace)
    {
      private double m_a;
      private double m_b;
      private double m_l;

      public double A
      {
        get => this.m_a;
        set
        {
          PdfLabColorSpace colorSpace = this.ColorSpace as PdfLabColorSpace;
          if (colorSpace.Range != null && (value < colorSpace.Range[0] || value > colorSpace.Range[1]))
            throw new ArgumentOutOfRangeException(nameof (A), "a* component must be in the range defined by the Lab colorspace.");
          this.m_a = value;
        }
      }

      public double B
      {
        get => this.m_b;
        set
        {
          PdfLabColorSpace colorSpace = this.ColorSpace as PdfLabColorSpace;
          if (colorSpace.Range != null && (value < colorSpace.Range[2] || value > colorSpace.Range[3]))
            throw new ArgumentOutOfRangeException(nameof (B), "b* component must be in the range defined by the Lab colorspace.");
          this.m_b = value;
        }
      }

      public double L
      {
        get => this.m_l;
        set
        {
          this.m_l = value >= 0.0 && value <= 100.0 ? value : throw new ArgumentOutOfRangeException(nameof (L), "L must be between 0 and 100");
        }
      }
    }
}
