// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Lists.PdfUnorderedMarker
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using System;
using System.Drawing;


namespace Syncfusion.Pdf.Lists
{
    public class PdfUnorderedMarker : PdfMarker
    {
      private PdfImage m_image;
      private SizeF m_size;
      private PdfUnorderedMarkerStyle m_style;
      private PdfTemplate m_template;
      private string m_text;
      private PdfFont m_unicodeFont;

      public PdfUnorderedMarker(PdfImage image)
      {
        this.Image = image;
        this.m_style = PdfUnorderedMarkerStyle.CustomImage;
      }

      public PdfUnorderedMarker(PdfTemplate template)
      {
        this.Template = template;
        this.m_style = PdfUnorderedMarkerStyle.CustomTemplate;
      }

      public PdfUnorderedMarker(PdfUnorderedMarkerStyle style) => this.m_style = style;

      public PdfUnorderedMarker(string text, PdfFont font)
      {
        this.Font = font;
        this.Text = text;
        this.m_style = PdfUnorderedMarkerStyle.CustomString;
      }

      internal void Draw(PdfGraphics graphics, PointF point, PdfBrush brush, PdfPen pen)
      {
        PdfTemplate template = new PdfTemplate(this.m_size);
        switch (this.m_style)
        {
          case PdfUnorderedMarkerStyle.CustomImage:
            template.Graphics.DrawImage(this.m_image, 1f, 1f, this.m_size.Width - 2f, this.m_size.Height - 2f);
            break;
          case PdfUnorderedMarkerStyle.CustomTemplate:
            template = new PdfTemplate(this.m_size);
            template.Graphics.DrawPdfTemplate(this.m_template, PointF.Empty, this.m_size);
            break;
          default:
            PointF empty = PointF.Empty;
            if (pen != null)
            {
              empty.X += pen.Width;
              empty.Y += pen.Width;
            }
            template.Graphics.DrawString(this.GetStyledText(), this.m_unicodeFont, pen, brush, empty);
            break;
        }
        graphics.DrawPdfTemplate(template, point);
      }

      internal void Draw(PdfPage page, PointF point, PdfBrush brush, PdfPen pen)
      {
        this.Draw(page.Graphics, point, brush, pen);
      }

      internal string GetStyledText()
      {
        string empty = string.Empty;
        switch (this.m_style)
        {
          case PdfUnorderedMarkerStyle.Disk:
            return "l";
          case PdfUnorderedMarkerStyle.Square:
            return "n";
          case PdfUnorderedMarkerStyle.Asterisk:
            return "]";
          case PdfUnorderedMarkerStyle.Circle:
            return "m";
          default:
            return empty;
        }
      }

      public PdfImage Image
      {
        get => this.m_image;
        set
        {
          this.m_image = value != null ? value : throw new ArgumentNullException("image");
          this.m_style = PdfUnorderedMarkerStyle.CustomImage;
        }
      }

      internal SizeF Size
      {
        get => this.m_size;
        set => this.m_size = value;
      }

      public PdfUnorderedMarkerStyle Style
      {
        get => this.m_style;
        set => this.m_style = value;
      }

      public PdfTemplate Template
      {
        get => this.m_template;
        set
        {
          this.m_template = value != null ? value : throw new ArgumentNullException("template");
          this.m_style = PdfUnorderedMarkerStyle.CustomTemplate;
        }
      }

      public string Text
      {
        get => this.m_text;
        set
        {
          this.m_text = value != null ? value : throw new ArgumentNullException("text");
          this.m_style = PdfUnorderedMarkerStyle.CustomString;
        }
      }

      internal PdfFont UnicodeFont
      {
        get => this.m_unicodeFont;
        set => this.m_unicodeFont = value;
      }
    }
}
