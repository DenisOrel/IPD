// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.PdfColorMask
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml


namespace Syncfusion.Pdf.Graphics
{
    public class PdfColorMask : PdfMask
    {
      private PdfColor m_endColor;
      private PdfColor m_startColor;

      public PdfColorMask(PdfColor startColor, PdfColor endColor)
      {
        this.m_endColor = endColor;
        this.m_startColor = startColor;
      }

      public PdfColor EndColor
      {
        get => this.m_endColor;
        set => this.m_endColor = value;
      }

      public PdfColor StartColor
      {
        get => this.m_startColor;
        set => this.m_startColor = value;
      }
    }
}
