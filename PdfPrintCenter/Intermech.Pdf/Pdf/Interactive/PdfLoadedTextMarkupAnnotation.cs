// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfLoadedTextMarkupAnnotation
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Primitives;
using System;
using System.Drawing;

#nullable disable
namespace Syncfusion.Pdf.Interactive;

public class PdfLoadedTextMarkupAnnotation : PdfLoadedStyledAnnotation
{
  private PdfColor m_color;
  private PdfCrossTable m_crossTable;
  private PdfDictionary m_dictionary;
  private PdfTextMarkupAnnotationType m_TextMarkupAnnotationType;

  internal PdfLoadedTextMarkupAnnotation(
    PdfDictionary dictionary,
    PdfCrossTable crossTable,
    RectangleF rectagle)
    : base(dictionary, crossTable)
  {
    this.m_dictionary = dictionary;
    this.m_crossTable = crossTable;
  }

  private PdfTextMarkupAnnotationType GetTextMarkupAnnotation(string aType)
  {
    PdfTextMarkupAnnotationType markupAnnotation = PdfTextMarkupAnnotationType.Highlight;
    string str = aType;
    switch (str)
    {
      case null:
        return markupAnnotation;
      case "Highlight":
        return PdfTextMarkupAnnotationType.Highlight;
      default:
        if (!(str != "Squiggly"))
          return PdfTextMarkupAnnotationType.Squiggly;
        if (str == "StrikeOut")
          return PdfTextMarkupAnnotationType.StrikeOut;
        return str != "Underline" ? markupAnnotation : PdfTextMarkupAnnotationType.Underline;
    }
  }

  private PdfTextMarkupAnnotationType GetTextMarkupAnnotationType()
  {
    return this.GetTextMarkupAnnotation((this.Dictionary["Subtype"] as PdfName).Value.ToString());
  }

  private PdfColor GetTextMarkupColor()
  {
    PdfColorSpace colorSpace = PdfColorSpace.RGB;
    PdfColor empty = PdfColor.Empty;
    PdfArray pdfArray = !this.Dictionary.ContainsKey("C") ? empty.ToArray(colorSpace) : this.Dictionary["C"] as PdfArray;
    return new PdfColor((pdfArray[0] as PdfNumber).FloatValue, (pdfArray[1] as PdfNumber).FloatValue, (pdfArray[2] as PdfNumber).FloatValue);
  }

  public void SetTitleText(string text)
  {
    if (text == null)
      throw new ArgumentNullException(nameof (text));
    if (text == string.Empty)
      throw new ArgumentException("The text can't be empty");
    if (!(this.Text != text))
      return;
    PdfString pdfString = new PdfString(text);
    this.Dictionary.SetString("T", text);
    this.Changed = true;
  }

  public PdfTextMarkupAnnotationType TextMarkupAnnotationType
  {
    get => this.GetTextMarkupAnnotationType();
    set
    {
      this.m_TextMarkupAnnotationType = value;
      this.Dictionary.SetName("Subtype", this.m_TextMarkupAnnotationType.ToString());
    }
  }

  public PdfColor TextMarkupColor
  {
    get => this.GetTextMarkupColor();
    set
    {
      PdfArray primitive = new PdfArray();
      this.m_color = value;
      primitive.Insert(0, (IPdfPrimitive) new PdfNumber((float) this.m_color.R / (float) byte.MaxValue));
      primitive.Insert(1, (IPdfPrimitive) new PdfNumber((float) this.m_color.G / (float) byte.MaxValue));
      primitive.Insert(2, (IPdfPrimitive) new PdfNumber((float) this.m_color.B / (float) byte.MaxValue));
      this.Dictionary.SetProperty("C", (IPdfPrimitive) primitive);
    }
  }
}
