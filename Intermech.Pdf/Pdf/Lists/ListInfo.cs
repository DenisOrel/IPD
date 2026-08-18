// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Lists.ListInfo
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;


namespace Syncfusion.Pdf.Lists
{
    internal class ListInfo
    {
      private PdfBrush m_brush;
      private PdfFont m_font;
      private PdfStringFormat m_format;
      private int m_index;
      private PdfList m_list;
      private string m_number;
      private PdfPen m_pen;
      internal float MarkerWidth;

      internal ListInfo(PdfList list, int index)
        : this(list, index, string.Empty)
      {
      }

      internal ListInfo(PdfList list, int index, string number)
      {
        this.m_list = list;
        this.m_index = index;
        this.m_number = number;
      }

      internal PdfBrush Brush
      {
        get => this.m_brush;
        set => this.m_brush = value;
      }

      internal PdfFont Font
      {
        get => this.m_font;
        set => this.m_font = value;
      }

      internal PdfStringFormat Format
      {
        get => this.m_format;
        set => this.m_format = value;
      }

      internal int Index
      {
        get => this.m_index;
        set => this.m_index = value;
      }

      internal PdfList List
      {
        get => this.m_list;
        set => this.m_list = value;
      }

      internal string Number
      {
        get => this.m_number;
        set => this.m_number = value;
      }

      internal PdfPen Pen
      {
        get => this.m_pen;
        set => this.m_pen = value;
      }
    }
}
