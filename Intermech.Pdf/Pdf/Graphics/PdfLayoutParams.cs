// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.PdfLayoutParams
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System.Drawing;


namespace Syncfusion.Pdf.Graphics
{
    public class PdfLayoutParams
    {
      private RectangleF m_bounds;
      private PdfLayoutFormat m_format;
      private PdfPage m_page;

      public RectangleF Bounds
      {
        get => this.m_bounds;
        set => this.m_bounds = value;
      }

      public PdfLayoutFormat Format
      {
        get => this.m_format;
        set => this.m_format = value;
      }

      public PdfPage Page
      {
        get => this.m_page;
        set => this.m_page = value;
      }
    }
}
