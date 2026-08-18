// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Lists.PdfList
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using System;
using System.Drawing;


namespace Syncfusion.Pdf.Lists
{
    public abstract class PdfList : PdfLayoutElement
    {
      protected static readonly char[] c_splitChars = new char[1]
      {
        '\n'
      };
      private PdfBrush m_brush;
      private PdfFont m_font;
      private PdfStringFormat m_format;
      private float m_indent;
      private PdfListItemCollection m_items;
      private PdfPen m_pen;
      private float m_textIndent;

      public event BeginItemLayoutEventHandler BeginItemLayout;

      public event EndItemLayoutEventHandler EndItemLayout;

      internal PdfList()
      {
        this.m_indent = 10f;
        this.m_textIndent = 5f;
      }

      internal PdfList(PdfFont font)
      {
        this.m_indent = 10f;
        this.m_textIndent = 5f;
        this.Font = font;
      }

      internal PdfList(PdfListItemCollection items)
      {
        this.m_indent = 10f;
        this.m_textIndent = 5f;
        this.m_items = items != null ? items : throw new ArgumentException("Items collection can't be null", nameof (items));
      }

      protected static PdfListItemCollection CreateItems(string text)
      {
        return text != null ? new PdfListItemCollection(text.Split(PdfList.c_splitChars)) : throw new ArgumentNullException(nameof (text));
      }

      public override void Draw(PdfGraphics graphics, float x, float y)
      {
        new PdfListLayouter(this).Layout(graphics, x, y);
      }

      protected override void DrawInternal(PdfGraphics graphics)
      {
        new PdfListLayouter(this).Layout(graphics, PointF.Empty);
      }

      protected override PdfLayoutResult Layout(PdfLayoutParams param)
      {
        return new PdfListLayouter(this).Layout(param);
      }

      internal void OnBeginItemLayout(BeginItemLayoutEventArgs args)
      {
        if (!this.RiseBeginItemLayout)
          return;
        this.BeginItemLayout((object) this, args);
      }

      internal void OnEndItemLayout(EndItemLayoutEventArgs args)
      {
        if (!this.RiseEndItemLayout)
          return;
        this.EndItemLayout((object) this, args);
      }

      public PdfBrush Brush
      {
        get => this.m_brush;
        set => this.m_brush = value;
      }

      public PdfFont Font
      {
        get => this.m_font;
        set => this.m_font = value != null ? value : throw new ArgumentNullException("font");
      }

      public float Indent
      {
        get => this.m_indent;
        set => this.m_indent = value;
      }

      public PdfListItemCollection Items
      {
        get
        {
          if (this.m_items == null)
            this.m_items = new PdfListItemCollection();
          return this.m_items;
        }
      }

      public PdfPen Pen
      {
        get => this.m_pen;
        set => this.m_pen = value;
      }

      internal bool RiseBeginItemLayout => this.BeginItemLayout != null;

      internal bool RiseEndItemLayout => this.EndItemLayout != null;

      public PdfStringFormat StringFormat
      {
        get => this.m_format;
        set => this.m_format = value;
      }

      public float TextIndent
      {
        get => this.m_textIndent;
        set => this.m_textIndent = value;
      }
    }
}
