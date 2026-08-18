// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Grid.PdfGridCellStyle
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;


namespace Syncfusion.Pdf.Grid
{
    public class PdfGridCellStyle : PdfGridRowStyle
    {
      private PdfImage m_backgroundImage;
      private PdfBorders m_borders = PdfBorders.Default;
      private PdfEdges m_edges;
      private PdfStringFormat m_format;

      private PdfStringFormat GetDefaultFormat()
      {
        return new PdfStringFormat()
        {
          Alignment = PdfTextAlignment.Left,
          LineAlignment = PdfVerticalAlignment.Middle
        };
      }

      public PdfImage BackgroundImage
      {
        get => this.m_backgroundImage;
        set => this.m_backgroundImage = value;
      }

      public PdfBorders Borders
      {
        get => this.m_borders;
        set => this.m_borders = value;
      }

      internal PdfEdges Edges
      {
        get
        {
          if (this.m_edges == null)
            this.m_edges = new PdfEdges();
          return this.m_edges;
        }
        set => this.m_edges = value;
      }

      public PdfStringFormat StringFormat
      {
        get => this.m_format;
        set => this.m_format = value;
      }
    }
}
