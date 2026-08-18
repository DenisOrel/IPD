// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfComboBoxField
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using System.Drawing;


namespace Syncfusion.Pdf.Interactive
{
    public class PdfComboBoxField : PdfListField
    {
      private bool m_editable;

      internal PdfComboBoxField()
      {
      }

      public PdfComboBoxField(PdfPageBase page, string name)
        : base(page, name)
      {
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
          string s = string.Empty;
          if (this.SelectedIndex != -1)
            s = this.SelectedItem.Text;
          FieldPainter.DrawComboBox(template.Graphics, paintParams);
          template.Graphics.DrawString(s, font, this.ForeBrush, bounds, this.StringFormat);
          this.Page.Graphics.DrawPdfTemplate(template, this.Bounds.Location, bounds.Size);
        }
      }

      protected override void DrawAppearance(PdfTemplate template)
      {
        base.DrawAppearance(template);
        PaintParams paintParams = new PaintParams(new RectangleF(PointF.Empty, this.Size), this.BackBrush, this.ForeBrush, this.BorderPen, this.BorderStyle, this.BorderWidth, this.ShadowBrush, this.RotationAngle);
        FieldPainter.DrawComboBox(template.Graphics, paintParams);
      }

      protected override void Initialize()
      {
        base.Initialize();
        this.Flags |= FieldFlags.Combo;
      }

      public bool Editable
      {
        get => this.m_editable;
        set
        {
          if (this.m_editable == value)
            return;
          this.m_editable = value;
          if (this.m_editable)
            this.Flags |= FieldFlags.Edit;
          else
            this.Flags &= FieldFlags.Edit;
        }
      }
    }
}
