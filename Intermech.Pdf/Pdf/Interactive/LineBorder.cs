// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.LineBorder
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;


namespace Syncfusion.Pdf.Interactive
{
    public class LineBorder : IPdfWrapper
    {
      private PdfBorderStyle m_borderStyle;
      private int m_borderWidth;
      private int m_dashArray;
      private PdfDictionary m_dictionary = new PdfDictionary();

      public LineBorder()
      {
        this.m_dictionary.SetProperty("Type", (IPdfPrimitive) new PdfName("Border"));
      }

      private string StyleToString(PdfBorderStyle style)
      {
        switch (style)
        {
          case PdfBorderStyle.Dashed:
            return "D";
          case PdfBorderStyle.Beveled:
            return "B";
          case PdfBorderStyle.Inset:
            return "I";
          case PdfBorderStyle.Underline:
            return "U";
          default:
            return "S";
        }
      }

      public PdfBorderStyle BorderStyle
      {
        get => this.m_borderStyle;
        set
        {
          this.m_borderStyle = value;
          this.m_dictionary.SetName("S", this.StyleToString(this.m_borderStyle));
        }
      }

      public int BorderWidth
      {
        get => this.m_borderWidth;
        set
        {
          this.m_borderWidth = value;
          this.m_dictionary.SetNumber("W", this.m_borderWidth);
        }
      }

      public int DashArray
      {
        get => this.m_dashArray;
        set
        {
          this.m_dashArray = value;
          PdfArray primitive = new PdfArray();
          primitive.Insert(0, (IPdfPrimitive) new PdfNumber(this.m_dashArray));
          primitive.Insert(1, (IPdfPrimitive) new PdfNumber(this.m_dashArray));
          this.m_dictionary.SetProperty("D", (IPdfPrimitive) primitive);
        }
      }

      IPdfPrimitive IPdfWrapper.Element => (IPdfPrimitive) this.m_dictionary;
    }
}
