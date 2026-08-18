// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfCheckFieldBase
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Primitives;


namespace Syncfusion.Pdf.Interactive
{
    public class PdfCheckFieldBase : PdfStyledField
    {
      private PdfTemplate m_checkedTemplate;
      private PdfTemplate m_pressedCheckedTemplate;
      private PdfTemplate m_pressedUncheckedTemplate;
      private PdfCheckBoxStyle m_style;
      private PdfTemplate m_uncheckedTemplate;

      internal PdfCheckFieldBase()
      {
      }

      public PdfCheckFieldBase(PdfPageBase page, string name)
        : base(page, name)
      {
      }

      private void CreateTemplate(ref PdfTemplate template)
      {
        if (template == null)
          template = new PdfTemplate(this.Size);
        else
          template.Reset(this.Size);
      }

      internal override void Draw() => base.Draw();

      protected virtual void DrawAppearance()
      {
      }

      protected override void Initialize()
      {
        base.Initialize();
        this.Dictionary.SetProperty("FT", (IPdfPrimitive) new PdfName("Btn"));
      }

      private void ReleaseTemplate(PdfTemplate template)
      {
        if (template == null)
          return;
        template.Reset();
        this.Widget.ExtendedAppearance = (PdfExtendedAppearance) null;
      }

      internal override void Save()
      {
        base.Save();
        if (this.Form != null)
        {
          this.CreateTemplate(ref this.m_checkedTemplate);
          this.CreateTemplate(ref this.m_uncheckedTemplate);
          this.CreateTemplate(ref this.m_pressedCheckedTemplate);
          this.CreateTemplate(ref this.m_pressedUncheckedTemplate);
          this.Widget.ExtendedAppearance.Normal.On = this.m_checkedTemplate;
          this.Widget.ExtendedAppearance.Normal.Off = this.m_uncheckedTemplate;
          this.Widget.ExtendedAppearance.Pressed.On = this.m_pressedCheckedTemplate;
          this.Widget.ExtendedAppearance.Pressed.Off = this.m_pressedUncheckedTemplate;
          this.DrawAppearance();
        }
        else
        {
          this.ReleaseTemplate(this.m_checkedTemplate);
          this.ReleaseTemplate(this.m_uncheckedTemplate);
          this.ReleaseTemplate(this.m_pressedCheckedTemplate);
          this.ReleaseTemplate(this.m_pressedUncheckedTemplate);
        }
      }

      protected string StyleToString(PdfCheckBoxStyle style)
      {
        switch (style)
        {
          case PdfCheckBoxStyle.Circle:
            return "l";
          case PdfCheckBoxStyle.Cross:
            return "8";
          case PdfCheckBoxStyle.Diamond:
            return "u";
          case PdfCheckBoxStyle.Square:
            return "n";
          case PdfCheckBoxStyle.Star:
            return "H";
          default:
            return "4";
        }
      }

      internal PdfTemplate CheckedTemplate
      {
        get => this.m_checkedTemplate;
        set => this.m_checkedTemplate = value;
      }

      internal PdfTemplate PressedCheckedTemplate
      {
        get => this.m_pressedCheckedTemplate;
        set => this.m_pressedCheckedTemplate = value;
      }

      internal PdfTemplate PressedUncheckedTemplate
      {
        get => this.m_pressedUncheckedTemplate;
        set => this.m_pressedUncheckedTemplate = value;
      }

      public PdfCheckBoxStyle Style
      {
        get => this.m_style;
        set
        {
          if (this.m_style == value)
            return;
          this.m_style = value;
          this.Widget.WidgetAppearance.NormalCaption = this.StyleToString(this.m_style);
        }
      }

      internal PdfTemplate UncheckedTemplate
      {
        get => this.m_uncheckedTemplate;
        set => this.m_uncheckedTemplate = value;
      }
    }
}
