// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfFileAnnotation
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System.Drawing;


namespace Syncfusion.Pdf.Interactive
{
    public abstract class PdfFileAnnotation : PdfAnnotation
    {
      private PdfAppearance m_appearance;

      protected PdfFileAnnotation()
      {
      }

      protected PdfFileAnnotation(RectangleF rectangle)
        : base(rectangle)
      {
      }

      protected override void Save()
      {
        base.Save();
        if (this.m_appearance == null || this.m_appearance.Normal == null)
          return;
        this.Dictionary.SetProperty("AP", (IPdfWrapper) this.m_appearance);
      }

      public PdfAppearance Appearance
      {
        get
        {
          if (this.m_appearance == null)
            this.m_appearance = new PdfAppearance((PdfAnnotation) this);
          return this.m_appearance;
        }
        set
        {
          if (this.m_appearance == value)
            return;
          this.m_appearance = value;
        }
      }

      public abstract string FileName { get; set; }
    }
}
