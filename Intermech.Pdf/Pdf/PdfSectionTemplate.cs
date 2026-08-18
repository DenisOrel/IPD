// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PdfSectionTemplate
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml


namespace Syncfusion.Pdf
{
    public class PdfSectionTemplate : PdfDocumentTemplate
    {
      private bool m_bottom;
      private bool m_left;
      private bool m_right;
      private bool m_stamp;
      private bool m_top;

      public PdfSectionTemplate()
      {
        this.m_left = this.m_top = this.m_right = this.m_bottom = this.m_stamp = true;
      }

      public bool ApplyDocumentBottomTemplate
      {
        get => this.m_bottom;
        set => this.m_bottom = value;
      }

      public bool ApplyDocumentLeftTemplate
      {
        get => this.m_left;
        set => this.m_left = value;
      }

      public bool ApplyDocumentRightTemplate
      {
        get => this.m_right;
        set => this.m_right = value;
      }

      public bool ApplyDocumentStamps
      {
        get => this.m_stamp;
        set => this.m_stamp = value;
      }

      public bool ApplyDocumentTopTemplate
      {
        get => this.m_top;
        set => this.m_top = value;
      }
    }
}
