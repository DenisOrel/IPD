// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.ColorSpace.PdfIndexedColor
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml


namespace Syncfusion.Pdf.ColorSpace
{
    public class PdfIndexedColor(PdfIndexedColorSpace colorspace) : PdfExtendedColor((PdfColorSpaces) colorspace)
    {
      private int m_colorIndex;

      public int SelectColorIndex
      {
        get => this.m_colorIndex;
        set => this.m_colorIndex = value;
      }
    }
}
