// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Lists.PdfListItem
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using System;


namespace Syncfusion.Pdf.Lists
{
    public class PdfListItem
    {
      private PdfBrush m_brush;
      private PdfFont m_font;
      private PdfStringFormat m_format;
      private PdfList m_list;
      private PdfPen m_pen;
      private string m_text;
      private float m_textIndent;

      public PdfListItem()
        : this(string.Empty)
      {
      }

      public PdfListItem(string text)
        : this(text, (PdfFont) null, (PdfStringFormat) null, (PdfPen) null, (PdfBrush) null)
      {
      }

      public PdfListItem(string text, PdfFont font)
        : this(text, font, (PdfStringFormat) null, (PdfPen) null, (PdfBrush) null)
      {
      }

      public PdfListItem(string text, PdfFont font, PdfStringFormat format)
        : this(text, font, format, (PdfPen) null, (PdfBrush) null)
      {
      }

      public PdfListItem(
        string text,
        PdfFont font,
        PdfStringFormat format,
        PdfPen pen,
        PdfBrush brush)
      {
        this.m_text = text != null ? text : throw new ArgumentNullException(nameof (text));
        this.m_font = font;
        this.m_format = format;
        this.m_pen = pen;
        this.m_brush = brush;
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

      public PdfStringFormat StringFormat
      {
        get => this.m_format;
        set => this.m_format = value;
      }

      public PdfList SubList
      {
        get => this.m_list;
        set => this.m_list = value;
      }

      public string Text
      {
        get => this.m_text;
        set => this.m_text = value != null ? value : throw new ArgumentNullException("text");
      }

      public float TextIndent
      {
        get => this.m_textIndent;
        set => this.m_textIndent = value;
      }
    }
}
