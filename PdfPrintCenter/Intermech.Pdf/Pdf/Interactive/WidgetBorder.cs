// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.WidgetBorder
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;

#nullable disable
namespace Syncfusion.Pdf.Interactive;

internal class WidgetBorder : IPdfWrapper
{
  private PdfDictionary m_dictionary = new PdfDictionary();
  private PdfBorderStyle m_style;
  private int m_width = 1;

  public WidgetBorder()
  {
    this.m_dictionary.SetProperty("Type", (IPdfPrimitive) new PdfName("Border"));
    this.m_dictionary.SetName("S", this.StyleToString(this.m_style));
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

  public PdfBorderStyle Style
  {
    get => this.m_style;
    set
    {
      this.m_style = value;
      this.m_dictionary.SetName("S", this.StyleToString(this.m_style));
    }
  }

  IPdfPrimitive IPdfWrapper.Element => (IPdfPrimitive) this.m_dictionary;

  public int Width
  {
    get => this.m_width;
    set
    {
      this.m_width = value;
      this.m_dictionary.SetNumber("W", this.m_width);
    }
  }
}
