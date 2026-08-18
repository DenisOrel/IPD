// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfLoadedDocumentLinkAnnotation
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Primitives;
using System.Drawing;


namespace Syncfusion.Pdf.Interactive
{
    public class PdfLoadedDocumentLinkAnnotation : PdfLoadedStyledAnnotation
    {
      private PdfCrossTable m_crossTable;

      internal PdfLoadedDocumentLinkAnnotation(
        PdfDictionary dictionary,
        PdfCrossTable crossTable,
        RectangleF rectangle)
        : base(dictionary, crossTable)
      {
        this.Dictionary = dictionary;
        this.m_crossTable = crossTable;
      }

      private PdfDestination GetDestination()
      {
        PdfDestination destination1 = (PdfDestination) null;
        if (this.Dictionary.ContainsKey("Dest"))
        {
          IPdfPrimitive pdfPrimitive = this.CrossTable.GetObject(this.Dictionary["Dest"]);
          PdfArray pdfArray = pdfPrimitive as PdfArray;
          PdfName name1 = pdfPrimitive as PdfName;
          PdfString name2 = pdfPrimitive as PdfString;
          if (this.CrossTable.Document is PdfLoadedDocument document)
          {
            if (name1 != (PdfName) null)
              pdfArray = document.GetNamedDestination(name1);
            else if (name2 != null)
              pdfArray = document.GetNamedDestination(name2);
          }
          PdfReferenceHolder pointer = pdfArray[0] as PdfReferenceHolder;
          if (pointer == (PdfReferenceHolder) null && pdfArray[0] is PdfNumber)
          {
            PdfPageBase page = (this.CrossTable.Document as PdfLoadedDocument).Pages[(pdfArray[0] as PdfNumber).IntValue];
            PdfName pdfName = pdfArray[1] as PdfName;
            if (pdfName != (PdfName) null)
            {
              if (pdfName.Value == "XYZ")
              {
                PdfNumber pdfNumber1 = pdfArray[2] as PdfNumber;
                PdfNumber pdfNumber2 = pdfArray[3] as PdfNumber;
                PdfNumber pdfNumber3 = pdfArray[4] as PdfNumber;
                float y = pdfNumber2 == null ? 0.0f : page.Size.Height - pdfNumber2.FloatValue;
                float x = pdfNumber1 == null ? 0.0f : pdfNumber1.FloatValue;
                destination1 = new PdfDestination(page, new PointF(x, y));
                if (pdfNumber3 != null)
                  destination1.Zoom = pdfNumber3.FloatValue;
                if (pdfNumber1 == null || pdfNumber2 == null || pdfNumber3 == null)
                  destination1.SetValidation(false);
              }
            }
            else if (page != null)
            {
              destination1 = new PdfDestination(page);
              destination1.Mode = PdfDestinationMode.FitToPage;
            }
          }
          if (pointer != (PdfReferenceHolder) null)
          {
            PdfPageBase page = (this.CrossTable.Document as PdfLoadedDocument).Pages.GetPage(this.CrossTable.GetObject((IPdfPrimitive) pointer) as PdfDictionary);
            PdfName pdfName = pdfArray[1] as PdfName;
            if (pdfName != (PdfName) null)
            {
              if (pdfName.Value == "XYZ")
              {
                PdfNumber pdfNumber4 = pdfArray[2] as PdfNumber;
                PdfNumber pdfNumber5 = pdfArray[3] as PdfNumber;
                PdfNumber pdfNumber6 = pdfArray[4] as PdfNumber;
                float y = pdfNumber5 == null ? 0.0f : page.Size.Height - pdfNumber5.FloatValue;
                float x = pdfNumber4 == null ? 0.0f : pdfNumber4.FloatValue;
                destination1 = new PdfDestination(page, new PointF(x, y));
                if (pdfNumber6 != null)
                  destination1.Zoom = pdfNumber6.FloatValue;
                if (pdfNumber4 == null || pdfNumber5 == null || pdfNumber6 == null)
                  destination1.SetValidation(false);
              }
              return destination1;
            }
            if (page != null && pdfName.Value == "Fit")
            {
              destination1 = new PdfDestination(page);
              destination1.Mode = PdfDestinationMode.FitToPage;
            }
          }
          return destination1;
        }
        if (this.Dictionary.ContainsKey("A") && destination1 == null)
        {
          IPdfPrimitive pdfPrimitive = (this.CrossTable.GetObject(this.Dictionary["A"]) as PdfDictionary)["D"];
          if ((object) (pdfPrimitive as PdfReferenceHolder) != null)
            pdfPrimitive = (pdfPrimitive as PdfReferenceHolder).Object;
          PdfArray pdfArray = pdfPrimitive as PdfArray;
          PdfName name3 = pdfPrimitive as PdfName;
          PdfString name4 = pdfPrimitive as PdfString;
          if (this.CrossTable.Document is PdfLoadedDocument document)
          {
            if (name3 != (PdfName) null)
              pdfArray = document.GetNamedDestination(name3);
            else if (name4 != null)
              pdfArray = document.GetNamedDestination(name4);
          }
          if (pdfArray == null)
            return destination1;
          PdfReferenceHolder pointer = pdfArray[0] as PdfReferenceHolder;
          PdfPageBase page = (PdfPageBase) null;
          if (pointer != (PdfReferenceHolder) null)
            page = (this.CrossTable.Document as PdfLoadedDocument).Pages.GetPage(this.CrossTable.GetObject((IPdfPrimitive) pointer) as PdfDictionary);
          PdfName pdfName = pdfArray[1] as PdfName;
          if (pdfName.Value == "FitBH" || pdfName.Value == "FitH")
          {
            float y = !(pdfArray[2] is PdfNumber pdfNumber) ? 0.0f : page.Size.Height - pdfNumber.FloatValue;
            PdfDestination destination2 = new PdfDestination(page, new PointF(0.0f, y));
            if (pdfNumber == null)
              destination2.SetValidation(false);
            return destination2;
          }
          if (pdfName.Value == "XYZ")
          {
            PdfNumber pdfNumber7 = pdfArray[2] as PdfNumber;
            PdfNumber pdfNumber8 = pdfArray[3] as PdfNumber;
            PdfNumber pdfNumber9 = pdfArray[4] as PdfNumber;
            if (page != null)
            {
              float y = pdfNumber8 == null ? 0.0f : page.Size.Height - pdfNumber8.FloatValue;
              float x = pdfNumber7 == null ? 0.0f : pdfNumber7.FloatValue;
              destination1 = new PdfDestination(page, new PointF(x, y));
              if (pdfNumber9 != null)
                destination1.Zoom = pdfNumber9.FloatValue;
              if (pdfNumber7 != null && pdfNumber8 != null && pdfNumber9 != null)
                return destination1;
              destination1.SetValidation(false);
            }
            return destination1;
          }
          if (pdfName.Value == "FitR")
          {
            if (pdfArray.Count == 6)
            {
              PdfNumber pdfNumber10 = pdfArray[2] as PdfNumber;
              PdfNumber pdfNumber11 = pdfArray[3] as PdfNumber;
              PdfNumber pdfNumber12 = pdfArray[4] as PdfNumber;
              PdfNumber pdfNumber13 = pdfArray[5] as PdfNumber;
              destination1 = new PdfDestination(page, new RectangleF(pdfNumber10.FloatValue, pdfNumber11.FloatValue, pdfNumber12.FloatValue, pdfNumber13.FloatValue));
            }
            return destination1;
          }
          if (page != null && pdfName.Value == "Fit")
          {
            destination1 = new PdfDestination(page);
            destination1.Mode = PdfDestinationMode.FitToPage;
          }
        }
        return destination1;
      }

      public PdfDestination Destination
      {
        get => this.GetDestination();
        set => this.Dictionary.SetProperty("Dest", (IPdfWrapper) value);
      }
    }
}
