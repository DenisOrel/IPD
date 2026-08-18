// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfListBoxField
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using System.Drawing;


namespace Syncfusion.Pdf.Interactive
{
    public class PdfListBoxField(PdfPageBase page, string name) : PdfListField(page, name)
    {
      private bool m_multiselect;

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
          FieldPainter.DrawListBox(template.Graphics, paintParams, this.Items, new int[1]
          {
            this.SelectedIndex
          }, font, this.StringFormat);
          this.Page.Graphics.DrawPdfTemplate(template, this.Bounds.Location, bounds.Size);
        }
      }

      protected override void DrawAppearance(PdfTemplate template)
      {
        base.DrawAppearance(template);
        PaintParams paintParams = new PaintParams(new RectangleF(PointF.Empty, this.Size), this.BackBrush, this.ForeBrush, this.BorderPen, this.BorderStyle, this.BorderWidth, this.ShadowBrush, this.RotationAngle);
        PdfFont font = this.Font ?? (PdfFont) new PdfStandardFont(PdfFontFamily.TimesRoman, 12f);
        FieldPainter.DrawListBox(template.Graphics, paintParams, this.Items, new int[1]
        {
          this.SelectedIndex
        }, font, this.StringFormat);
      }

      protected override void Initialize() => base.Initialize();

      public bool MultiSelect
      {
        get => this.m_multiselect;
        set
        {
          if (this.m_multiselect == value)
            return;
          this.m_multiselect = value;
          if (this.m_multiselect)
            this.Flags |= FieldFlags.MultiSelect;
          else
            this.Flags -= FieldFlags.MultiSelect;
        }
      }
    }
}
