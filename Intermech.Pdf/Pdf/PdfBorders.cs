// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PdfBorders
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;


namespace Syncfusion.Pdf
{
    public class PdfBorders
    {
      private PdfPen m_bottom;
      private PdfPen m_left;
      private PdfPen m_right;
      private PdfPen m_top;

      public PdfBorders()
      {
        PdfPen pdfPen1 = new PdfPen(new PdfColor((byte) 0, (byte) 0, (byte) 0));
        pdfPen1.DashStyle = PdfDashStyle.Solid;
        PdfPen pdfPen2 = new PdfPen(new PdfColor((byte) 0, (byte) 0, (byte) 0));
        pdfPen2.DashStyle = PdfDashStyle.Solid;
        PdfPen pdfPen3 = new PdfPen(new PdfColor((byte) 0, (byte) 0, (byte) 0));
        pdfPen3.DashStyle = PdfDashStyle.Solid;
        PdfPen pdfPen4 = new PdfPen(new PdfColor((byte) 0, (byte) 0, (byte) 0));
        pdfPen4.DashStyle = PdfDashStyle.Solid;
        this.m_left = pdfPen1;
        this.m_right = pdfPen2;
        this.m_top = pdfPen3;
        this.m_bottom = pdfPen4;
      }

      public PdfPen All
      {
        set => this.m_left = this.m_right = this.m_top = this.m_bottom = value;
      }

      public PdfPen Bottom
      {
        get => this.m_bottom;
        set => this.m_bottom = value;
      }

      public static PdfBorders Default => new PdfBorders();

      internal bool IsAll
      {
        get => this.m_left == this.m_right && this.m_left == this.m_top && this.m_left == this.m_bottom;
      }

      public PdfPen Left
      {
        get => this.m_left;
        set => this.m_left = value;
      }

      public PdfPen Right
      {
        get => this.m_right;
        set => this.m_right = value;
      }

      public PdfPen Top
      {
        get => this.m_top;
        set => this.m_top = value;
      }
    }
}
