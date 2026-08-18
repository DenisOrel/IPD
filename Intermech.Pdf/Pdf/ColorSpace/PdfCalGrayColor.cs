// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.ColorSpace.PdfCalGrayColor
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;


namespace Syncfusion.Pdf.ColorSpace
{
    public class PdfCalGrayColor(PdfColorSpaces colorspace) : PdfExtendedColor(colorspace)
    {
      private double m_gray;

      public double Gray
      {
        get => this.m_gray;
        set
        {
          this.m_gray = value >= 0.0 && value <= 1.0 ? value : throw new ArgumentOutOfRangeException(nameof (Gray), "Gray level must be between 0 and 1");
        }
      }
    }
}
