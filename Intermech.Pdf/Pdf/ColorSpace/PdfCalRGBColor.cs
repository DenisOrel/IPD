// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.ColorSpace.PdfCalRGBColor
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;


namespace Syncfusion.Pdf.ColorSpace
{
    public class PdfCalRGBColor(PdfColorSpaces colorspace) : PdfExtendedColor(colorspace)
    {
      private double m_blue;
      private double m_green;
      private double m_red;

      public double Blue
      {
        get => this.m_blue;
        set
        {
          this.m_blue = value >= 0.0 && value <= 1.0 ? value : throw new ArgumentOutOfRangeException(nameof (Blue), "Blue level must be between 0 and 1");
        }
      }

      public double Green
      {
        get => this.m_green;
        set
        {
          this.m_green = value >= 0.0 && value <= 1.0 ? value : throw new ArgumentOutOfRangeException(nameof (Green), "Green level must be between 0 and 1");
        }
      }

      public double Red
      {
        get => this.m_red;
        set
        {
          this.m_red = value >= 0.0 && value <= 1.0 ? value : throw new ArgumentOutOfRangeException(nameof (Red), "Red level must be between 0 and 1");
        }
      }
    }
}
