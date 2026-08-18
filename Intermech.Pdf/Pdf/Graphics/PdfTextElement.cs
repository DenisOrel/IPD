// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.PdfTextElement
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Drawing;


namespace Syncfusion.Pdf.Graphics
{
    public class PdfTextElement : PdfLayoutElement
    {
      private PdfBrush m_brush;
      private PdfFont m_font;
      private PdfStringFormat m_format;
      private PdfPen m_pen;
      private string m_text;
      private string m_value;

      public PdfTextElement()
      {
        this.m_text = string.Empty;
        this.m_value = string.Empty;
      }

      public PdfTextElement(string text)
      {
        this.m_text = string.Empty;
        this.m_value = string.Empty;
        this.m_text = text != null ? text : throw new ArgumentNullException(nameof (text));
        text = PdfStandardFont.Convert(text);
        this.m_value = text;
      }

      public PdfTextElement(string text, PdfFont font)
        : this(text)
      {
        this.m_font = font != null ? font : throw new ArgumentNullException(nameof (font));
        if (this.m_font is PdfStandardFont)
          this.m_value = PdfStandardFont.Convert(this.m_text);
        else
          this.m_value = this.m_text;
      }

      public PdfTextElement(string text, PdfFont font, PdfBrush brush)
        : this(text, font)
      {
        this.m_brush = brush;
      }

      public PdfTextElement(string text, PdfFont font, PdfPen pen)
        : this(text, font)
      {
        this.m_pen = pen;
      }

      public PdfTextElement(
        string text,
        PdfFont font,
        PdfPen pen,
        PdfBrush brush,
        PdfStringFormat format)
        : this(text, font, pen)
      {
        this.m_brush = brush;
        this.m_format = format;
      }

      public PdfTextLayoutResult Draw(PdfPage page, PointF location, PdfLayoutFormat format)
      {
        RectangleF layoutRectangle = new RectangleF(location, SizeF.Empty);
        return this.Draw(page, layoutRectangle, format);
      }

      public PdfTextLayoutResult Draw(PdfPage page, RectangleF layoutRectangle, PdfLayoutFormat format)
      {
        if (page == null)
          throw new ArgumentNullException(nameof (page));
        return this.Layout(new PdfLayoutParams()
        {
          Page = page,
          Bounds = layoutRectangle,
          Format = format != null ? format : new PdfLayoutFormat()
        }) as PdfTextLayoutResult;
      }

      public PdfTextLayoutResult Draw(
        PdfPage page,
        PointF location,
        float width,
        PdfLayoutFormat format)
      {
        RectangleF layoutRectangle = new RectangleF(location.X, location.Y, width, 0.0f);
        return this.Draw(page, layoutRectangle, format);
      }

      protected override void DrawInternal(PdfGraphics graphics)
      {
        if (graphics == null)
          throw new ArgumentNullException(nameof (graphics));
        if (this.Font == null)
          throw new ArgumentNullException("Font can't be null");
        graphics.DrawString(this.Value, this.Font, this.Pen, this.GetBrush(), PointF.Empty, this.StringFormat);
      }

      internal PdfBrush GetBrush() => this.m_brush != null ? this.m_brush : PdfBrushes.Black;

      protected override PdfLayoutResult Layout(PdfLayoutParams param)
      {
        if (param == null)
          throw new ArgumentNullException(nameof (param));
        if (this.Font == null)
          throw new ArgumentNullException("Font can't be null");
        return new TextLayouter(this).Layout(param);
      }

      public PdfBrush Brush
      {
        get => this.m_brush;
        set => this.m_brush = value;
      }

      public PdfFont Font
      {
        get => this.m_font;
        set
        {
          this.m_font = value != null ? value : throw new ArgumentNullException(nameof (Font));
          if (this.m_font is PdfStandardFont && this.m_text != null)
            this.m_value = PdfStandardFont.Convert(this.m_text);
          else
            this.m_value = this.m_text;
        }
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

      public string Text
      {
        get => this.m_text;
        set
        {
          if (value == null)
            throw new ArgumentNullException(nameof (Text));
          this.m_value = this.m_font == null || this.m_font is PdfStandardFont ? PdfStandardFont.Convert(value) : value;
          this.m_text = value;
        }
      }

      internal string Value => this.m_value;
    }
}
