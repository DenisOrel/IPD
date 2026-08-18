// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.PdfTextLayoutResult
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System.Drawing;


namespace Syncfusion.Pdf.Graphics
{
    public class PdfTextLayoutResult : PdfLayoutResult
    {
      private RectangleF m_lastLineBounds;
      private string m_remainder;

      internal PdfTextLayoutResult(
        PdfPage page,
        RectangleF bounds,
        string remainder,
        RectangleF lastLineBounds)
        : base(page, bounds)
      {
        this.m_remainder = remainder;
        this.m_lastLineBounds = lastLineBounds;
      }

      public RectangleF LastLineBounds => this.m_lastLineBounds;

      public string Remainder => this.m_remainder;
    }
}
