// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfStyledField
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Primitives;
using System;
using System.Drawing;


namespace Syncfusion.Pdf.Interactive
{
    public abstract class PdfStyledField : PdfField
    {
      private PdfFieldActions m_actions;
      private PdfTemplate m_appearanceTemplate;
      private PdfBrush m_backBrush;
      private PdfPen m_borderPen;
      private PdfFont m_font;
      private PdfBrush m_foreBrush;
      private PdfBrush m_shadowBrush;
      private PdfStringFormat m_stringFormat;
      private bool m_visible;
      private WidgetAnnotation m_widget;
      private const byte ShadowShift = 64 /*0x40*/;

      internal PdfStyledField() => this.m_visible = true;

      public PdfStyledField(PdfPageBase page, string name)
        : base(page, name)
      {
        this.m_visible = true;
        this.AddAnnotationToPage(page, (PdfAnnotation) this.Widget);
      }

      internal void AddAnnotationToPage(PdfPageBase page, PdfAnnotation widget)
      {
        if (page is PdfPage pdfPage)
        {
          pdfPage.Annotations.Add(widget);
        }
        else
        {
          PdfLoadedPage wrapper = page as PdfLoadedPage;
          PdfDictionary dictionary = wrapper.Dictionary;
          PdfArray primitive = !dictionary.ContainsKey("Annots") ? new PdfArray() : wrapper.CrossTable.GetObject(dictionary["Annots"]) as PdfArray;
          widget.Dictionary.SetProperty("P", (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) wrapper));
          primitive.Add((IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) widget));
          page.Dictionary.SetProperty("Annots", (IPdfPrimitive) primitive);
        }
      }

      internal void BoundsAtLoadedPage(PdfPageBase page, RectangleF value)
      {
        if (!(page is PdfLoadedPage))
          return;
        RectangleF rectangleF = value;
        rectangleF = new RectangleF(new PointF(rectangleF.X, page.Size.Height - (rectangleF.Bottom + rectangleF.Height)), rectangleF.Size);
        this.Widget.Bounds = rectangleF;
      }

      private void CreateBackBrush()
      {
        this.m_backBrush = (PdfBrush) new PdfSolidBrush(this.m_widget.WidgetAppearance.BackColor);
        PdfColor color = new PdfColor(this.m_widget.WidgetAppearance.BackColor);
        color.R = (int) color.R - 64 /*0x40*/ >= 0 ? (byte) ((uint) color.R - 64U /*0x40*/) : (byte) 0;
        color.G = (int) color.G - 64 /*0x40*/ >= 0 ? (byte) ((uint) color.G - 64U /*0x40*/) : (byte) 0;
        color.B = (int) color.B - 64 /*0x40*/ >= 0 ? (byte) ((uint) color.B - 64U /*0x40*/) : (byte) 0;
        this.m_shadowBrush = (PdfBrush) new PdfSolidBrush(color);
      }

      private void CreateBorderPen()
      {
        float width = (float) this.m_widget.WidgetBorder.Width;
        this.m_borderPen = new PdfPen(this.m_widget.WidgetAppearance.BorderColor, width);
        if (this.Widget.WidgetBorder.Style != PdfBorderStyle.Dashed)
          return;
        this.m_borderPen.DashStyle = PdfDashStyle.Custom;
        this.m_borderPen.DashPattern = new float[1]
        {
          3f / width
        };
      }

      protected override void DefineDefaultAppearance()
      {
        if (this.Form == null || this.m_font == null)
          return;
        this.m_widget.DefaultAppearance.FontName = this.Form.Resources.GetName((IPdfWrapper) this.m_font).Value;
        this.m_widget.DefaultAppearance.FontSize = this.m_font.Size;
      }

      internal override void Draw()
      {
        this.RemoveAnnoationFromPage(this.Page, (PdfAnnotation) this.Widget);
      }

      internal RectangleF GetBoundsAtLoadedPage(PdfPageBase page, RectangleF rect)
      {
        if (page is PdfLoadedPage)
          rect = new RectangleF(new PointF(rect.X, page.Size.Height - (rect.Bottom + rect.Height)), rect.Size);
        return rect;
      }

      protected PdfFont GetFont() => this.m_font != null ? this.m_font : PdfDocument.DefaultFont;

      protected override void Initialize()
      {
        base.Initialize();
        this.m_widget = new WidgetAnnotation();
        this.m_widget.Parent = (PdfField) this;
        this.m_foreBrush = (PdfBrush) new PdfSolidBrush(this.m_widget.DefaultAppearance.ForeColor);
        this.m_stringFormat = new PdfStringFormat(this.Widget.TextAlignment, PdfVerticalAlignment.Middle);
        this.CreateBorderPen();
        this.CreateBackBrush();
        this.Dictionary.SetProperty("Kids", (IPdfPrimitive) new PdfArray(new PdfArray()
        {
          (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) this.m_widget)
        }));
        this.Widget.DefaultAppearance.FontName = "TiRo";
      }

      internal void RemoveAnnoationFromPage(PdfPageBase page, PdfAnnotation widget)
      {
        if (page is PdfPage pdfPage)
        {
          pdfPage.Annotations.Remove(widget);
        }
        else
        {
          PdfLoadedPage wrapper = page as PdfLoadedPage;
          PdfDictionary dictionary = wrapper.Dictionary;
          PdfArray primitive = !dictionary.ContainsKey("Annots") ? new PdfArray() : wrapper.CrossTable.GetObject(dictionary["Annots"]) as PdfArray;
          widget.Dictionary.SetProperty("P", (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) wrapper));
          primitive.Remove((IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) widget));
          page.Dictionary.SetProperty("Annots", (IPdfPrimitive) primitive);
        }
      }

      public PdfFieldActions Actions
      {
        get
        {
          if (this.m_actions == null)
          {
            this.m_actions = new PdfFieldActions(this.Widget.Actions);
            this.Dictionary.SetProperty("AA", (IPdfWrapper) this.m_actions);
          }
          return this.m_actions;
        }
      }

      internal PdfTemplate AppearanceTemplate => this.m_appearanceTemplate;

      internal PdfBrush BackBrush => this.m_backBrush;

      public PdfColor BackColor
      {
        get => this.m_widget.WidgetAppearance.BackColor;
        set
        {
          this.m_widget.WidgetAppearance.BackColor = value;
          this.CreateBackBrush();
        }
      }

      public PdfColor BorderColor
      {
        get => this.m_widget.WidgetAppearance.BorderColor;
        set
        {
          this.m_widget.WidgetAppearance.BorderColor = value;
          this.CreateBorderPen();
        }
      }

      internal PdfPen BorderPen => this.m_borderPen;

      public PdfBorderStyle BorderStyle
      {
        get => this.Widget.WidgetBorder.Style;
        set
        {
          this.Widget.WidgetBorder.Style = value;
          this.CreateBorderPen();
        }
      }

      public int BorderWidth
      {
        get => this.m_widget.WidgetBorder.Width;
        set
        {
          if (this.m_widget.WidgetBorder.Width == value)
            return;
          this.m_widget.WidgetBorder.Width = value;
          this.CreateBorderPen();
        }
      }

      public virtual RectangleF Bounds
      {
        get => this.GetBoundsAtLoadedPage(this.Page, this.m_widget.Bounds);
        set
        {
          this.m_widget.Bounds = value;
          this.BoundsAtLoadedPage(this.Page, value);
        }
      }

      public PdfFont Font
      {
        get => this.m_font;
        set
        {
          if (value == null)
            throw new ArgumentNullException(nameof (Font));
          if (this.m_font == value)
            return;
          this.m_font = value;
          this.DefineDefaultAppearance();
        }
      }

      internal PdfBrush ForeBrush => this.m_foreBrush;

      public PdfColor ForeColor
      {
        get => this.m_widget.DefaultAppearance.ForeColor;
        set
        {
          this.m_widget.DefaultAppearance.ForeColor = value;
          this.m_foreBrush = (PdfBrush) new PdfSolidBrush(value);
        }
      }

      public PdfHighlightMode HighlightMode
      {
        get => this.m_widget.HighlightMode;
        set => this.m_widget.HighlightMode = value;
      }

      public PointF Location
      {
        get => this.m_widget.Location;
        set => this.m_widget.SetLocation(value);
      }

      internal PdfBrush ShadowBrush => this.m_shadowBrush;

      public SizeF Size
      {
        get => this.m_widget.Size;
        set => this.m_widget.SetSize(value);
      }

      internal PdfStringFormat StringFormat => this.m_stringFormat;

      public PdfTextAlignment TextAlignment
      {
        get => this.Widget.TextAlignment;
        set
        {
          if (this.Widget.TextAlignment == value)
            return;
          this.Widget.TextAlignment = value;
          this.m_stringFormat = new PdfStringFormat(value, PdfVerticalAlignment.Middle);
        }
      }

      public bool Visible
      {
        get => this.m_visible;
        set
        {
          if (this.m_visible == value || value)
            return;
          this.m_visible = value;
          this.m_widget.AnnotationFlags = PdfAnnotationFlags.Hidden;
        }
      }

      internal WidgetAnnotation Widget => this.m_widget;
    }
}
