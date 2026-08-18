// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PdfAutomaticField
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using System;
using System.Drawing;


namespace Syncfusion.Pdf
{
    public abstract class PdfAutomaticField : PdfGraphicsElement
    {
      private RectangleF m_bounds;
      private PdfBrush m_brush;
      private PdfFont m_font;
      private PdfPen m_pen;
      private PdfStringFormat m_stringFormat;
      private SizeF m_templateSize;

      protected PdfAutomaticField()
      {
        this.m_bounds = RectangleF.Empty;
        this.m_templateSize = SizeF.Empty;
      }

      protected PdfAutomaticField(PdfFont font)
      {
        this.m_bounds = RectangleF.Empty;
        this.m_templateSize = SizeF.Empty;
        this.Font = font;
      }

      protected PdfAutomaticField(PdfFont font, PdfBrush brush)
      {
        this.m_bounds = RectangleF.Empty;
        this.m_templateSize = SizeF.Empty;
        this.Font = font;
        this.Brush = brush;
      }

      protected PdfAutomaticField(PdfFont font, RectangleF bounds)
      {
        this.m_bounds = RectangleF.Empty;
        this.m_templateSize = SizeF.Empty;
        this.Font = font;
        this.Bounds = bounds;
      }

      public override void Draw(PdfGraphics graphics, float x, float y)
      {
        base.Draw(graphics, x, y);
        graphics.AutomaticFields.Add(new PdfAutomaticFieldInfo(this, new PointF(x, y)));
      }

      protected override void DrawInternal(PdfGraphics graphics)
      {
      }

      protected PdfBrush GetBrush() => this.m_brush != null ? this.m_brush : PdfBrushes.Black;

      protected PdfFont GetFont() => this.m_font != null ? this.m_font : PdfDocument.DefaultFont;

      protected SizeF GetSize()
      {
        return (double) this.Bounds.Height != 0.0 && (double) this.Bounds.Width != 0.0 ? this.Size : this.m_templateSize;
      }

      protected internal abstract string GetValue(PdfGraphics graphics);

      protected internal virtual void PerformDraw(
        PdfGraphics graphics,
        PointF location,
        float scalingX,
        float scalingY)
      {
        if ((double) this.Bounds.Height != 0.0 && (double) this.Bounds.Width != 0.0)
          return;
        string text = this.GetValue(graphics);
        this.m_templateSize = this.GetFont().MeasureString(text, this.Size, this.StringFormat);
      }

      public RectangleF Bounds
      {
        get => this.m_bounds;
        set => this.m_bounds = value;
      }

      public PdfBrush Brush
      {
        get => this.m_brush;
        set => this.m_brush = value != null ? value : throw new ArgumentNullException(nameof (Brush));
      }

      public PdfFont Font
      {
        get => this.m_font;
        set => this.m_font = value != null ? value : throw new ArgumentNullException(nameof (Font));
      }

      public PointF Location
      {
        get => this.m_bounds.Location;
        set => this.m_bounds.Location = value;
      }

      public PdfPen Pen
      {
        get => this.m_pen;
        set => this.m_pen = value;
      }

      public SizeF Size
      {
        get => this.m_bounds.Size;
        set => this.m_bounds.Size = value;
      }

      public PdfStringFormat StringFormat
      {
        get => this.m_stringFormat;
        set => this.m_stringFormat = value;
      }
    }
}
