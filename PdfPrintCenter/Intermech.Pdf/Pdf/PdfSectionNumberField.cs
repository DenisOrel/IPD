// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PdfSectionNumberField
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Primitives;
using System.Drawing;

#nullable disable
namespace Syncfusion.Pdf;

public class PdfSectionNumberField : PdfMultipleNumberValueField
{
  public PdfSectionNumberField()
  {
  }

  public PdfSectionNumberField(PdfFont font)
    : base(font)
  {
  }

  public PdfSectionNumberField(PdfFont font, PdfBrush brush)
    : base(font, brush)
  {
  }

  public PdfSectionNumberField(PdfFont font, RectangleF bounds)
    : base(font, bounds)
  {
  }

  protected internal override string GetValue(PdfGraphics graphics)
  {
    string str = (string) null;
    if (graphics.Page is PdfPage)
    {
      PdfPage page = graphics.Page as PdfPage;
      if (!(page.Section.m_document is PdfLoadedDocument))
        return PdfNumbersConvertor.Convert(page.Document.Sections.IndexOf(page.Section) + 1, this.NumberStyle);
      PdfReferenceHolder pointer = page.Dictionary["Parent"] as PdfReferenceHolder;
      PdfDictionary pdfDictionary = page.Section.m_document.CrossTable.GetObject((IPdfPrimitive) pointer) as PdfDictionary;
      PdfLoadedDocument document = page.Section.m_document as PdfLoadedDocument;
      PdfArray pdfArray = (document.CrossTable.GetObject(document.Catalog["Pages"]) as PdfDictionary)["Kids"] as PdfArray;
      for (int index = 0; index < pdfArray.Count; ++index)
      {
        if (((pdfArray[index] as PdfReferenceHolder).Object as PdfDictionary).Equals((object) pdfDictionary))
          str = PdfNumbersConvertor.Convert(index + 1, this.NumberStyle);
      }
      return str;
    }
    if (graphics.Page is PdfLoadedPage)
    {
      PdfLoadedPage page = graphics.Page as PdfLoadedPage;
      PdfDocumentBase document = page.Document;
      PdfArray pdfArray = (page.CrossTable.GetObject(page.Document.Catalog["Pages"]) as PdfDictionary)["Kids"] as PdfArray;
      int objNum = (int) (page.Dictionary["Parent"] as PdfReferenceHolder).Reference.ObjNum;
      for (int index = 0; index < pdfArray.Count; ++index)
      {
        PdfReferenceHolder pdfReferenceHolder = pdfArray[index] as PdfReferenceHolder;
        if (pdfReferenceHolder.Reference != (PdfReference) null && (int) pdfReferenceHolder.Reference.ObjNum == objNum)
          str = PdfNumbersConvertor.Convert(index + 1, this.NumberStyle);
      }
    }
    return str;
  }
}
