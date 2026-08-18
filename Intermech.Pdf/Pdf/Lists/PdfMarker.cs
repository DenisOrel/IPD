// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Lists.PdfMarker
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;


namespace Syncfusion.Pdf.Lists
{
    public abstract class PdfMarker
    {
      private PdfListMarkerAlignment m_alignment;
      private PdfBrush m_brush;
      private PdfFont m_font;
      private PdfStringFormat m_format;
      private PdfPen m_pen;

      public PdfListMarkerAlignment Alignment
      {
        get => this.m_alignment;
        set => this.m_alignment = value;
      }

      public PdfBrush Brush
      {
        get => this.m_brush;
        set => this.m_brush = value;
      }

      public PdfFont Font
      {
        get => this.m_font;
        set => this.m_font = value;
      }

      public PdfPen Pen
      {
        get => this.m_pen;
        set => this.m_pen = value;
      }

      internal bool RightToLeft => this.m_alignment == PdfListMarkerAlignment.Right;

      public PdfStringFormat StringFormat
      {
        get => this.m_format;
        set => this.m_format = value;
      }
    }
}
