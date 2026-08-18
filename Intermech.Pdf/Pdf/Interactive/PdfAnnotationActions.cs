// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfAnnotationActions
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;


namespace Syncfusion.Pdf.Interactive
{
    public class PdfAnnotationActions : IPdfWrapper
    {
      private PdfDictionary m_dictionary = new PdfDictionary();
      private PdfAction m_gotFocus;
      private PdfAction m_lostFocus;
      private PdfAction m_mouseDown;
      private PdfAction m_mouseEnter;
      private PdfAction m_mouseLeave;
      private PdfAction m_mouseUp;

      public PdfAction GotFocus
      {
        get => this.m_gotFocus;
        set
        {
          if (this.m_gotFocus == value)
            return;
          this.m_gotFocus = value;
          this.m_dictionary.SetProperty("Fo", (IPdfWrapper) this.m_gotFocus);
        }
      }

      public PdfAction LostFocus
      {
        get => this.m_lostFocus;
        set
        {
          if (this.m_lostFocus == value)
            return;
          this.m_lostFocus = value;
          this.m_dictionary.SetProperty("Bl", (IPdfWrapper) this.m_lostFocus);
        }
      }

      public PdfAction MouseDown
      {
        get => this.m_mouseDown;
        set
        {
          if (this.m_mouseDown == value)
            return;
          this.m_mouseDown = value;
          this.m_dictionary.SetProperty("D", (IPdfWrapper) this.m_mouseDown);
        }
      }

      public PdfAction MouseEnter
      {
        get => this.m_mouseEnter;
        set
        {
          if (this.m_mouseEnter == value)
            return;
          this.m_mouseEnter = value;
          this.m_dictionary.SetProperty("E", (IPdfWrapper) this.m_mouseEnter);
        }
      }

      public PdfAction MouseLeave
      {
        get => this.m_mouseLeave;
        set
        {
          if (this.m_mouseLeave == value)
            return;
          this.m_mouseLeave = value;
          this.m_dictionary.SetProperty("X", (IPdfWrapper) this.m_mouseLeave);
        }
      }

      public PdfAction MouseUp
      {
        get => this.m_mouseUp;
        set
        {
          if (this.m_mouseUp == value)
            return;
          this.m_mouseUp = value;
          this.m_dictionary.SetProperty("U", (IPdfWrapper) this.m_mouseUp);
        }
      }

      IPdfPrimitive IPdfWrapper.Element => (IPdfPrimitive) this.m_dictionary;
    }
}
