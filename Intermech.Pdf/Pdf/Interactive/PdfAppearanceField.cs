// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfAppearanceField
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;


namespace Syncfusion.Pdf.Interactive
{
    public abstract class PdfAppearanceField : PdfStyledField
    {
      protected PdfAppearanceField()
      {
      }

      protected PdfAppearanceField(PdfPageBase page, string name)
        : base(page, name)
      {
      }

      internal override void Draw() => base.Draw();

      protected virtual void DrawAppearance(PdfTemplate template)
      {
      }

      internal override void Save()
      {
        base.Save();
        if (this.Form == null || this.Form.NeedAppearances || this.Widget.GetAppearance() != null)
          return;
        this.DrawAppearance(this.Widget.Appearance.Normal);
      }

      public PdfAppearance Appearance => this.Widget.Appearance;
    }
}
