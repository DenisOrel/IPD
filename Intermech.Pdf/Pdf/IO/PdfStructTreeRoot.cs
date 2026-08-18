// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.IO.PdfStructTreeRoot
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System.Drawing;


namespace Syncfusion.Pdf.IO
{
    internal class PdfStructTreeRoot : PdfDictionary
    {
      private RectangleF m_BBoxBounds;
      private PdfArray m_childSTR;
      private static int m_id;
      private PdfPageBase m_pdfPage;

      public PdfStructTreeRoot()
      {
        this["Type"] = (IPdfPrimitive) new PdfName("StructTreeRoot");
        PdfStructTreeRoot.m_id = 0;
        this.m_childSTR = new PdfArray();
        this["K"] = (IPdfPrimitive) this.m_childSTR;
      }

      internal int Add(string structType, string altText, RectangleF bounds)
      {
        PdfDictionary pdfDictionary1 = new PdfDictionary();
        pdfDictionary1["S"] = (IPdfPrimitive) new PdfName(structType);
        pdfDictionary1["P"] = (IPdfPrimitive) new PdfReferenceHolder((IPdfPrimitive) this);
        pdfDictionary1["K"] = (IPdfPrimitive) new PdfNumber(PdfStructTreeRoot.m_id++);
        pdfDictionary1["Lang"] = (IPdfPrimitive) new PdfString("English");
        if (structType != "P")
          pdfDictionary1["Alt"] = (IPdfPrimitive) new PdfString(altText);
        if (this.m_pdfPage != null)
          pdfDictionary1["Pg"] = (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) this.m_pdfPage);
        PdfDictionary pdfDictionary2 = new PdfDictionary();
        pdfDictionary2["BBox"] = (IPdfPrimitive) new PdfArray(new float[4]
        {
          bounds.X,
          bounds.Y,
          bounds.Width,
          bounds.Height
        });
        if (structType == "P" && bounds != RectangleF.Empty)
          pdfDictionary1["A"] = (IPdfPrimitive) pdfDictionary2;
        this.m_childSTR.Add((IPdfPrimitive) new PdfReferenceHolder((IPdfPrimitive) pdfDictionary1));
        this["ParentTree"] = (IPdfPrimitive) new PdfReferenceHolder((IPdfPrimitive) new PdfDictionary()
        {
          ["Nums"] = (IPdfPrimitive) new PdfArray()
          {
            (IPdfPrimitive) new PdfNumber(0),
            (IPdfPrimitive) new PdfReferenceHolder((IPdfPrimitive) this.m_childSTR)
          }
        });
        this["ParentTreeNextKey"] = (IPdfPrimitive) new PdfNumber(1);
        return PdfStructTreeRoot.m_id - 1;
      }

      internal int Add(string structType, string altText, PdfPageBase page, RectangleF bounds)
      {
        this.m_pdfPage = page;
        this.m_BBoxBounds = bounds;
        int num = this.Add(structType, altText, this.m_BBoxBounds);
        this.m_pdfPage = (PdfPageBase) null;
        return num;
      }
    }
}
