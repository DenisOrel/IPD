// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfCheckBoxField
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using System.Drawing;


namespace Syncfusion.Pdf.Interactive
{
    public class PdfCheckBoxField(PdfPageBase page, string name) : PdfCheckFieldBase(page, name)
    {
      private bool m_checked;

      internal override void Draw()
      {
        base.Draw();
        PaintParams paintParams = new PaintParams(this.Bounds, this.BackBrush, this.ForeBrush, this.BorderPen, this.BorderStyle, this.BorderWidth, this.ShadowBrush, this.RotationAngle);
        PdfCheckFieldState state = PdfCheckFieldState.Checked;
        if (!this.Checked)
          state = PdfCheckFieldState.Unchecked;
        FieldPainter.DrawCheckBox(this.Page.Graphics, paintParams, this.StyleToString(this.Style), state);
      }

      protected override void DrawAppearance()
      {
        base.DrawAppearance();
        PaintParams paintParams = new PaintParams(new RectangleF(PointF.Empty, this.Size), this.BackBrush, this.ForeBrush, this.BorderPen, this.BorderStyle, this.BorderWidth, this.ShadowBrush, this.RotationAngle);
        FieldPainter.DrawCheckBox(this.Widget.ExtendedAppearance.Normal.On.Graphics, paintParams, this.StyleToString(this.Style), PdfCheckFieldState.Checked, this.Font);
        FieldPainter.DrawCheckBox(this.Widget.ExtendedAppearance.Normal.Off.Graphics, paintParams, this.StyleToString(this.Style), PdfCheckFieldState.Unchecked, this.Font);
        FieldPainter.DrawCheckBox(this.Widget.ExtendedAppearance.Pressed.On.Graphics, paintParams, this.StyleToString(this.Style), PdfCheckFieldState.PressedChecked, this.Font);
        FieldPainter.DrawCheckBox(this.Widget.ExtendedAppearance.Pressed.Off.Graphics, paintParams, this.StyleToString(this.Style), PdfCheckFieldState.PressedUnchecked, this.Font);
      }

      internal override void Save()
      {
        base.Save();
        if (this.Form == null)
          return;
        if (!this.Checked)
          this.Widget.AppearanceState = "Off";
        else
          this.Widget.AppearanceState = "Yes";
      }

      public bool Checked
      {
        get => this.m_checked;
        set
        {
          if (this.m_checked == value)
            return;
          this.m_checked = value;
          if (this.m_checked)
            this.Dictionary.SetName("V", "Yes");
          else
            this.Dictionary.Remove("V");
        }
      }
    }
}
