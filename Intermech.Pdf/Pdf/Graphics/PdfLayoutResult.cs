// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.PdfLayoutResult
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System.Drawing;


namespace Syncfusion.Pdf.Graphics
{
    public class PdfLayoutResult
    {
      private RectangleF m_bounds;
      private PdfPage m_page;

      internal PdfLayoutResult(PdfPage page, RectangleF bounds)
      {
        this.m_page = page;
        this.m_bounds = bounds;
      }

      public RectangleF Bounds => this.m_bounds;

      public PdfPage Page => this.m_page;
    }
}
