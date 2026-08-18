// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PdfSectionPageCountField
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Primitives;
using System.Drawing;

#nullable disable
namespace Syncfusion.Pdf;

public class PdfSectionPageCountField : PdfMultipleNumberValueField
{
  public PdfSectionPageCountField()
  {
  }

  public PdfSectionPageCountField(PdfFont font)
    : base(font)
  {
  }

  public PdfSectionPageCountField(PdfFont font, PdfBrush brush)
    : base(font, brush)
  {
  }

  public PdfSectionPageCountField(PdfFont font, RectangleF bounds)
    : base(font, bounds)
  {
  }

  protected internal override string GetValue(PdfGraphics graphics)
  {
    string str = (string) null;
    if (graphics.Page is PdfPage)
      return PdfNumbersConvertor.Convert(PdfDynamicField.GetPageFromGraphics(graphics).Section.Count, this.NumberStyle);
    if (graphics.Page is PdfLoadedPage)
    {
      PdfLoadedPage pageFromGraphics = PdfDynamicField.GetLoadedPageFromGraphics(graphics);
      PdfLoadedPage page = graphics.Page as PdfLoadedPage;
      PdfDocumentBase document = page.Document;
      PdfDictionary catalog = (PdfDictionary) page.Document.Catalog;
      PdfArray pdfArray1 = (page.CrossTable.GetObject(catalog["Pages"]) as PdfDictionary)["Kids"] as PdfArray;
      for (int index1 = 0; index1 < pdfArray1.Count; ++index1)
      {
        PdfReferenceHolder pdfReferenceHolder = new PdfReferenceHolder((IPdfWrapper) page);
        PdfDictionary pdfDictionary = (pdfArray1[index1] as PdfReferenceHolder).Object as PdfDictionary;
        if (pdfDictionary["Type"].ToString() == "/Pages")
        {
          PdfArray pdfArray2 = page.CrossTable.GetObject(pdfDictionary["Kids"]) as PdfArray;
          for (int index2 = 0; index2 < pdfArray2.Count; ++index2)
          {
            PdfReferenceHolder pointer = pdfArray2[index2] as PdfReferenceHolder;
            if ((pageFromGraphics.CrossTable.GetObject((IPdfPrimitive) pointer) as PdfDictionary).Equals((object) pageFromGraphics.Dictionary))
              str = PdfNumbersConvertor.Convert(pdfArray2.Count, this.NumberStyle);
          }
        }
      }
    }
    return str;
  }
}
