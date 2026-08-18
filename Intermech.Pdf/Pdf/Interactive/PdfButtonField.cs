// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfButtonField
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
    public class PdfButtonField : PdfAppearanceField
    {
      private string m_text;

      internal PdfButtonField() => this.m_text = string.Empty;

      public PdfButtonField(PdfPageBase page, string name)
        : base(page, name)
      {
        this.m_text = string.Empty;
        this.StringFormat.Alignment = PdfTextAlignment.Center;
        this.Widget.WidgetAppearance.NormalCaption = name;
        this.Widget.TextAlignment = PdfTextAlignment.Center;
      }

      public void AddPrintAction()
      {
        PdfDictionary primitive = new PdfDictionary();
        primitive.SetProperty("N", (IPdfPrimitive) new PdfName("Print"));
        primitive.SetProperty("S", (IPdfPrimitive) new PdfName("Named"));
        (((this.Dictionary["Kids"] as PdfArray)[0] as PdfReferenceHolder).Object as PdfDictionary).SetProperty("A", (IPdfPrimitive) primitive);
      }

      internal override void Draw()
      {
        base.Draw();
        if (this.Widget.GetAppearance() != null)
        {
          this.Page.Graphics.DrawPdfTemplate(this.Appearance.Normal, this.Location);
        }
        else
        {
          RectangleF bounds = this.Bounds with
          {
            Location = PointF.Empty
          };
          PdfFont font = this.Font ?? PdfDocument.DefaultFont;
          PaintParams paintParams = new PaintParams(bounds, this.BackBrush, this.ForeBrush, this.BorderPen, this.BorderStyle, this.BorderWidth, this.ShadowBrush, this.RotationAngle);
          PdfTemplate template = new PdfTemplate(bounds.Size);
          FieldPainter.DrawButton(template.Graphics, paintParams, this.Text, font, this.StringFormat);
          this.Page.Graphics.DrawPdfTemplate(template, this.Bounds.Location, bounds.Size);
        }
      }

      protected override void DrawAppearance(PdfTemplate template)
      {
        base.DrawAppearance(template);
        PaintParams paintParams = new PaintParams(new RectangleF(PointF.Empty, this.Size), this.BackBrush, this.ForeBrush, this.BorderPen, this.BorderStyle, this.BorderWidth, this.ShadowBrush, this.RotationAngle);
        FieldPainter.DrawButton(template.Graphics, paintParams, this.Text, this.GetFont(), this.StringFormat);
      }

      protected void DrawPressedAppearance(PdfTemplate template)
      {
        PaintParams paintParams = new PaintParams(new RectangleF(PointF.Empty, this.Size), this.BackBrush, this.ForeBrush, this.BorderPen, this.BorderStyle, this.BorderWidth, this.ShadowBrush, this.RotationAngle);
        FieldPainter.DrawPressedButton(template.Graphics, paintParams, this.Text, this.GetFont(), this.StringFormat);
      }

      protected override void Initialize()
      {
        base.Initialize();
        this.Dictionary.SetProperty("FT", (IPdfPrimitive) new PdfName("Btn"));
        this.BackColor = new PdfColor(byte.MaxValue, (byte) 211, (byte) 211, (byte) 211);
        this.Flags |= FieldFlags.PushButton;
      }

      internal override void Save()
      {
        base.Save();
        if (this.Form == null || this.Form.NeedAppearances || this.Widget.Appearance.GetPressedTemplate() != null)
          return;
        this.DrawPressedAppearance(this.Widget.Appearance.Pressed);
      }

      public string Text
      {
        get => this.m_text;
        set
        {
          if (value == null)
            throw new ArgumentNullException(nameof (Text));
          if (!(this.m_text != value))
            return;
          this.m_text = value;
          this.Widget.WidgetAppearance.NormalCaption = this.m_text;
        }
      }
    }
}
