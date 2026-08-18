// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfExtendedAppearance
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;


namespace Syncfusion.Pdf.Interactive
{
    public class PdfExtendedAppearance : IPdfWrapper
    {
      private PdfDictionary m_dictionary = new PdfDictionary();
      private PdfAppearanceState m_mouseHover;
      private PdfAppearanceState m_normal;
      private PdfAppearanceState m_pressed;

      public PdfAppearanceState MouseHover
      {
        get
        {
          if (this.m_mouseHover == null)
          {
            this.m_mouseHover = new PdfAppearanceState();
            this.m_dictionary.SetProperty("R", (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) this.m_mouseHover));
          }
          return this.m_mouseHover;
        }
      }

      public PdfAppearanceState Normal
      {
        get
        {
          if (this.m_normal == null)
          {
            this.m_normal = new PdfAppearanceState();
            this.m_dictionary.SetProperty("N", (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) this.m_normal));
          }
          return this.m_normal;
        }
      }

      public PdfAppearanceState Pressed
      {
        get
        {
          if (this.m_pressed == null)
          {
            this.m_pressed = new PdfAppearanceState();
            this.m_dictionary.SetProperty("D", (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) this.m_pressed));
          }
          return this.m_pressed;
        }
      }

      IPdfPrimitive IPdfWrapper.Element => (IPdfPrimitive) this.m_dictionary;
    }
}
