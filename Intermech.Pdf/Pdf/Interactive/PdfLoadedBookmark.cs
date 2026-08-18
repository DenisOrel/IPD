// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfLoadedBookmark
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Primitives;
using System.Drawing;


namespace Syncfusion.Pdf.Interactive
{
    public class PdfLoadedBookmark : PdfBookmark
    {
      internal PdfLoadedBookmark(PdfDictionary dictionary, PdfCrossTable crossTable)
        : base(dictionary, crossTable)
      {
      }

      private PdfColor GetColor()
      {
        PdfColor color = new PdfColor((byte) 0, (byte) 0, (byte) 0);
        if (this.Dictionary.ContainsKey("C"))
        {
          PdfArray pdfArray = this.CrossTable.GetObject(this.Dictionary["C"]) as PdfArray;
          color = new PdfColor((pdfArray[0] as PdfNumber).FloatValue, (pdfArray[1] as PdfNumber).FloatValue, (pdfArray[2] as PdfNumber).FloatValue);
        }
        return color;
      }

      private PdfDestination GetDestination()
      {
        if (this.Dictionary.ContainsKey("Dest") && base.Destination == null)
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
          if (pdfArray != null)
          {
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
                  base.Destination = new PdfDestination(page, new PointF(x, y));
                  if (pdfNumber3 != null)
                    base.Destination.Zoom = pdfNumber3.FloatValue;
                  if (pdfNumber1 == null || pdfNumber2 == null || pdfNumber3 == null)
                    base.Destination.SetValidation(false);
                }
              }
              else if (page != null)
              {
                base.Destination = new PdfDestination(page);
                base.Destination.Mode = PdfDestinationMode.FitToPage;
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
                  base.Destination = new PdfDestination(page, new PointF(x, y));
                  if (pdfNumber6 != null)
                    base.Destination.Zoom = pdfNumber6.FloatValue;
                  if (pdfNumber4 == null || pdfNumber5 == null || pdfNumber6 == null)
                    base.Destination.SetValidation(false);
                }
                else if (pdfName.Value == "FitR")
                {
                  PdfNumber pdfNumber7 = pdfArray[2] as PdfNumber;
                  PdfNumber pdfNumber8 = pdfArray[3] as PdfNumber;
                  PdfNumber pdfNumber9 = pdfArray[4] as PdfNumber;
                  PdfNumber pdfNumber10 = pdfArray[5] as PdfNumber;
                  base.Destination = new PdfDestination(page, new RectangleF(pdfNumber7.FloatValue, pdfNumber8.FloatValue, pdfNumber9.FloatValue, pdfNumber10.FloatValue));
                  base.Destination.Mode = PdfDestinationMode.FitR;
                }
              }
              else if (page != null && pdfName.Value == "Fit")
              {
                base.Destination = new PdfDestination(page);
                base.Destination.Mode = PdfDestinationMode.FitToPage;
              }
            }
          }
        }
        else if (this.Dictionary.ContainsKey("A") && base.Destination == null)
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
          if (pdfArray != null)
          {
            PdfReferenceHolder pointer = pdfArray[0] as PdfReferenceHolder;
            PdfPageBase page = (PdfPageBase) null;
            if (pointer != (PdfReferenceHolder) null)
              page = (this.CrossTable.Document as PdfLoadedDocument).Pages.GetPage(this.CrossTable.GetObject((IPdfPrimitive) pointer) as PdfDictionary);
            PdfName pdfName = pdfArray[1] as PdfName;
            if (pdfName.Value == "FitBH" || pdfName.Value == "FitH")
            {
              PdfNumber pdfNumber = pdfArray[2] as PdfNumber;
              if (page != null)
              {
                float y = pdfNumber == null ? 0.0f : page.Size.Height - pdfNumber.FloatValue;
                base.Destination = new PdfDestination(page, new PointF(0.0f, y));
                if (pdfNumber == null)
                  base.Destination.SetValidation(false);
              }
            }
            else if (pdfName.Value == "XYZ")
            {
              PdfNumber pdfNumber11 = pdfArray[2] as PdfNumber;
              PdfNumber pdfNumber12 = pdfArray[3] as PdfNumber;
              PdfNumber pdfNumber13 = pdfArray[4] as PdfNumber;
              if (page != null)
              {
                float y = pdfNumber12 == null ? 0.0f : page.Size.Height - pdfNumber12.FloatValue;
                float x = pdfNumber11 == null ? 0.0f : pdfNumber11.FloatValue;
                base.Destination = new PdfDestination(page, new PointF(x, y));
                if (pdfNumber13 != null)
                  base.Destination.Zoom = pdfNumber13.FloatValue;
                if (pdfNumber11 == null || pdfNumber12 == null || pdfNumber13 == null)
                  base.Destination.SetValidation(false);
              }
            }
            else if (page != null && pdfName.Value == "Fit")
            {
              base.Destination = new PdfDestination(page);
              base.Destination.Mode = PdfDestinationMode.FitToPage;
            }
          }
        }
        return base.Destination;
      }

      private PdfBookmark GetNext()
      {
        PdfBookmark next = (PdfBookmark) null;
        int index = this.Parent.List.IndexOf((PdfBookmarkBase) this) + 1;
        if (index < this.Parent.List.Count)
          return this.Parent.List[index] as PdfBookmark;
        if (this.Dictionary.ContainsKey("Next"))
          next = (PdfBookmark) new PdfLoadedBookmark(this.CrossTable.GetObject(this.Dictionary["Next"]) as PdfDictionary, this.CrossTable);
        return next;
      }

      private PdfBookmark GetPrevious()
      {
        PdfBookmark previous = (PdfBookmark) null;
        int index = this.List.IndexOf((PdfBookmarkBase) this) - 1;
        if (index >= 0)
          return this.List[index] as PdfBookmark;
        if (this.Dictionary.ContainsKey("Prev"))
          previous = (PdfBookmark) new PdfLoadedBookmark(this.CrossTable.GetObject(this.Dictionary["Prev"]) as PdfDictionary, this.CrossTable);
        return previous;
      }

      private PdfTextStyle GetTextStyle()
      {
        PdfTextStyle textStyle = PdfTextStyle.Regular;
        if (this.Dictionary.ContainsKey("F"))
        {
          int intValue = (this.CrossTable.GetObject(this.Dictionary["F"]) as PdfNumber).IntValue;
          textStyle |= (PdfTextStyle) intValue;
        }
        return textStyle;
      }

      private string GetTitle()
      {
        string empty = string.Empty;
        if (this.Dictionary.ContainsKey("Title"))
          empty = (this.CrossTable.GetObject(this.Dictionary["Title"]) as PdfString).Value;
        return empty;
      }

      private void SetColor(PdfColor color)
      {
        this.Dictionary.SetProperty("C", (IPdfPrimitive) new PdfArray(new float[3]
        {
          color.Red,
          color.Green,
          color.Blue
        }));
      }

      private void SetTextStyle(PdfTextStyle value)
      {
        this.Dictionary.SetNumber("F", (int) (this.GetTextStyle() | value));
      }

      public override PdfColor Color
      {
        get => this.GetColor();
        set => this.SetColor(value);
      }

      public override PdfDestination Destination
      {
        get => this.GetDestination();
        set => base.Destination = value;
      }

      internal override System.Collections.Generic.List<PdfBookmarkBase> List
      {
        get
        {
          System.Collections.Generic.List<PdfBookmarkBase> list = base.List;
          if (list.Count != 0)
            return list;
          this.ReproduceTree();
          return list;
        }
      }

      internal override PdfBookmark Next
      {
        get => this.GetNext();
        set => base.Next = value;
      }

      internal override PdfBookmarkBase Parent => base.Parent;

      internal override PdfBookmark Previous
      {
        get => this.GetPrevious();
        set => base.Previous = value;
      }

      public override PdfTextStyle TextStyle
      {
        get => this.GetTextStyle();
        set => this.SetTextStyle(value);
      }

      public override string Title
      {
        get => this.GetTitle();
        set => base.Title = value;
      }
    }
}
