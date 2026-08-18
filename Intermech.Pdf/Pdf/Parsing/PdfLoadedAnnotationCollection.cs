// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Parsing.PdfLoadedAnnotationCollection
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Primitives;
using System;
using System.Collections;
using System.Drawing;


namespace Syncfusion.Pdf.Parsing
{
    public class PdfLoadedAnnotationCollection : PdfAnnotationCollection
    {
      private PdfLoadedPage m_page;

      internal PdfLoadedAnnotationCollection(PdfLoadedPage page)
      {
        this.m_page = page != null ? page : throw new ArgumentException(nameof (page));
        int index = 0;
        for (int count = this.m_page.TerminalAnnotation.Count; index < count; ++index)
        {
          PdfAnnotation annotation = this.GetAnnotation(index);
          if (annotation != null)
            this.DoAdd(annotation);
        }
        this.Page = this.m_page;
      }

      public override int Add(PdfAnnotation annotation)
      {
        if (annotation == null)
          throw new ArgumentNullException(nameof (annotation));
        if (annotation is PdfTextMarkupAnnotation)
          (annotation as PdfTextMarkupAnnotation).SetQuadPoints(this.m_page.Size);
        return this.DoAdd(annotation);
      }

      private PdfAnnotation CreateAnnotationStates(PdfDictionary dictionary, PdfCrossTable crossTable)
      {
        PdfTextMarkupAnnotation annotationStates = new PdfTextMarkupAnnotation();
        annotationStates.SetPage((PdfPageBase) this.m_page);
        return (PdfAnnotation) annotationStates;
      }

      private PdfAnnotation CreateCaretAnnotation(PdfDictionary dictionary, PdfCrossTable crossTable)
      {
        PdfTextMarkupAnnotation caretAnnotation = new PdfTextMarkupAnnotation();
        caretAnnotation.SetPage((PdfPageBase) this.m_page);
        return (PdfAnnotation) caretAnnotation;
      }

      private PdfAnnotation CreateDocumentLinkAnnotation(
        PdfDictionary dictionary,
        PdfCrossTable crossTable,
        RectangleF rect)
      {
        PdfLoadedDocumentLinkAnnotation documentLinkAnnotation = new PdfLoadedDocumentLinkAnnotation(dictionary, crossTable, rect);
        documentLinkAnnotation.SetPage((PdfPageBase) this.m_page);
        return (PdfAnnotation) documentLinkAnnotation;
      }

      private PdfAnnotation CreateFileAttachmentAnnotation(
        PdfDictionary dictionary,
        PdfCrossTable crossTable,
        RectangleF rect,
        string filename)
      {
        PdfLoadedAttachmentAnnotation attachmentAnnotation = new PdfLoadedAttachmentAnnotation(dictionary, crossTable, rect, filename);
        attachmentAnnotation.SetPage((PdfPageBase) this.m_page);
        return (PdfAnnotation) attachmentAnnotation;
      }

      private PdfAnnotation CreateFileLinkAnnotation(
        PdfDictionary dictionary,
        PdfCrossTable crossTable,
        RectangleF rect,
        string filename)
      {
        PdfLoadedFileLinkAnnotation fileLinkAnnotation = new PdfLoadedFileLinkAnnotation(dictionary, crossTable, rect, filename);
        fileLinkAnnotation.SetPage((PdfPageBase) this.m_page);
        return (PdfAnnotation) fileLinkAnnotation;
      }

      private PdfAnnotation CreateFileRemoteGoToLinkAnnotation(
        PdfDictionary dictionary,
        PdfCrossTable crossTable,
        PdfString fileName,
        PdfArray destination,
        RectangleF rect)
      {
        PdfLoadedFileLinkAnnotation toLinkAnnotation = new PdfLoadedFileLinkAnnotation(dictionary, crossTable, destination, rect, fileName.Value.ToString());
        toLinkAnnotation.SetPage((PdfPageBase) this.m_page);
        return (PdfAnnotation) toLinkAnnotation;
      }

      private PdfAnnotation CreateFreeTextAnnotation(PdfDictionary dictionary, PdfCrossTable crossTable)
      {
        PdfTextMarkupAnnotation freeTextAnnotation = new PdfTextMarkupAnnotation();
        freeTextAnnotation.SetPage((PdfPageBase) this.m_page);
        return (PdfAnnotation) freeTextAnnotation;
      }

      private PdfAnnotation CreateInkAnnotation(
        PdfDictionary dictionary,
        PdfCrossTable crossTable,
        RectangleF rect)
      {
        PdfLoadedInkAnnotation inkAnnotation = new PdfLoadedInkAnnotation(dictionary, crossTable, rect);
        inkAnnotation.SetPage((PdfPageBase) this.m_page);
        return (PdfAnnotation) inkAnnotation;
      }

      private PdfAnnotation CreateLineAnnotation(
        PdfDictionary dictionary,
        PdfCrossTable crossTable,
        RectangleF rect,
        string text)
      {
        PdfLoadedLineAnnotation lineAnnotation = new PdfLoadedLineAnnotation(dictionary, crossTable, rect, text);
        lineAnnotation.SetPage((PdfPageBase) this.m_page);
        return (PdfAnnotation) lineAnnotation;
      }

      private PdfAnnotation CreateLinkAnnotation(
        PdfDictionary dictionary,
        PdfCrossTable crossTable,
        RectangleF rect,
        string text)
      {
        PdfLoadedUriAnnotation linkAnnotation = new PdfLoadedUriAnnotation(dictionary, crossTable, rect, text);
        linkAnnotation.SetPage((PdfPageBase) this.m_page);
        return (PdfAnnotation) linkAnnotation;
      }

      private PdfAnnotation CreateLnkAnnotation(
        PdfDictionary dictionary,
        PdfCrossTable crossTable,
        RectangleF rect,
        string filename)
      {
        PdfLoadedFileLinkAnnotation lnkAnnotation = new PdfLoadedFileLinkAnnotation(dictionary, crossTable, rect, filename);
        lnkAnnotation.SetPage((PdfPageBase) this.m_page);
        return (PdfAnnotation) lnkAnnotation;
      }

      private PdfAnnotation CreateMarkupAnnotation(
        PdfDictionary dictionary,
        PdfCrossTable crossTable,
        RectangleF rect)
      {
        PdfLoadedTextMarkupAnnotation markupAnnotation = new PdfLoadedTextMarkupAnnotation(dictionary, crossTable, rect);
        markupAnnotation.SetPage((PdfPageBase) this.m_page);
        return (PdfAnnotation) markupAnnotation;
      }

      private PdfAnnotation CreateMovieAnnotation(PdfDictionary dictionary, PdfCrossTable crossTable)
      {
        PdfTextMarkupAnnotation movieAnnotation = new PdfTextMarkupAnnotation();
        movieAnnotation.SetPage((PdfPageBase) this.m_page);
        return (PdfAnnotation) movieAnnotation;
      }

      private PdfAnnotation CreatePolygonandPolylineAnnotation(
        PdfDictionary dictionary,
        PdfCrossTable crossTable)
      {
        PdfTextMarkupAnnotation polylineAnnotation = new PdfTextMarkupAnnotation();
        polylineAnnotation.SetPage((PdfPageBase) this.m_page);
        return (PdfAnnotation) polylineAnnotation;
      }

      private PdfAnnotation CreatePopupAnnotation(
        PdfDictionary dictionary,
        PdfCrossTable crossTable,
        RectangleF rect,
        string text)
      {
        PdfLoadedPopupAnnotation popupAnnotation = new PdfLoadedPopupAnnotation(dictionary, crossTable, rect, text);
        popupAnnotation.SetPage((PdfPageBase) this.m_page);
        return (PdfAnnotation) popupAnnotation;
      }

      private PdfAnnotation CreatePrinterMarkAnnotation(
        PdfDictionary dictionary,
        PdfCrossTable crossTable)
      {
        PdfTextMarkupAnnotation printerMarkAnnotation = new PdfTextMarkupAnnotation();
        printerMarkAnnotation.SetPage((PdfPageBase) this.m_page);
        return (PdfAnnotation) printerMarkAnnotation;
      }

      private PdfAnnotation CreateRubberStampAnnotation(
        PdfDictionary dictionary,
        PdfCrossTable crossTable,
        RectangleF rect,
        string text)
      {
        PdfLoadedRubberStampAnnotation rubberStampAnnotation = new PdfLoadedRubberStampAnnotation(dictionary, crossTable, rect, text);
        rubberStampAnnotation.SetPage((PdfPageBase) this.m_page);
        return (PdfAnnotation) rubberStampAnnotation;
      }

      private PdfAnnotation CreateScreenAnnotation(
        PdfDictionary dictionary,
        PdfCrossTable crossTable,
        RectangleF rect)
      {
        PdfLoadedTextMarkupAnnotation screenAnnotation = new PdfLoadedTextMarkupAnnotation(dictionary, crossTable, rect);
        screenAnnotation.SetPage((PdfPageBase) this.m_page);
        return (PdfAnnotation) screenAnnotation;
      }

      private PdfAnnotation CreateSoundAnnotation(
        PdfDictionary dictionary,
        PdfCrossTable crossTable,
        RectangleF rect)
      {
        PdfLoadedSoundAnnotation soundAnnotation = new PdfLoadedSoundAnnotation(dictionary, crossTable, rect);
        soundAnnotation.SetPage((PdfPageBase) this.m_page);
        return (PdfAnnotation) soundAnnotation;
      }

      private PdfAnnotation CreateSquareandCircleAnnotation(
        PdfDictionary dictionary,
        PdfCrossTable crossTable)
      {
        PdfTextMarkupAnnotation circleAnnotation = new PdfTextMarkupAnnotation();
        circleAnnotation.SetPage((PdfPageBase) this.m_page);
        return (PdfAnnotation) circleAnnotation;
      }

      private PdfAnnotation CreateTextAnnotation(PdfDictionary dictionary, PdfCrossTable crossTable)
      {
        PdfTextMarkupAnnotation textAnnotation = new PdfTextMarkupAnnotation();
        textAnnotation.SetPage((PdfPageBase) this.m_page);
        return (PdfAnnotation) textAnnotation;
      }

      private PdfAnnotation CreateTextMarkupAnnotation(
        PdfDictionary dictionary,
        PdfCrossTable crossTable)
      {
        PdfTextMarkupAnnotation markupAnnotation = new PdfTextMarkupAnnotation();
        markupAnnotation.SetPage((PdfPageBase) this.m_page);
        return (PdfAnnotation) markupAnnotation;
      }

      private PdfAnnotation CreateTextWebLinkAnnotation(
        PdfDictionary dictionary,
        PdfCrossTable crossTable,
        string text)
      {
        PdfLoadedTextWebLinkAnnotation webLinkAnnotation = new PdfLoadedTextWebLinkAnnotation(dictionary, crossTable, text);
        webLinkAnnotation.SetPage((PdfPageBase) this.m_page);
        return (PdfAnnotation) webLinkAnnotation;
      }

      private PdfAnnotation CreateTrapNetworkAnnotation(
        PdfDictionary dictionary,
        PdfCrossTable crossTable)
      {
        PdfTextMarkupAnnotation networkAnnotation = new PdfTextMarkupAnnotation();
        networkAnnotation.SetPage((PdfPageBase) this.m_page);
        return (PdfAnnotation) networkAnnotation;
      }

      private PdfAnnotation CreateWatermarkAnnotation(
        PdfDictionary dictionary,
        PdfCrossTable crossTable)
      {
        PdfTextMarkupAnnotation watermarkAnnotation = new PdfTextMarkupAnnotation();
        watermarkAnnotation.SetPage((PdfPageBase) this.m_page);
        return (PdfAnnotation) watermarkAnnotation;
      }

      private PdfAnnotation CreateWidgetAnnotation(
        PdfDictionary dictionary,
        PdfCrossTable crossTable,
        RectangleF rect)
      {
        PdfLoadedWidgetAnnotation widgetAnnotation = new PdfLoadedWidgetAnnotation(dictionary, crossTable, rect);
        widgetAnnotation.SetPage((PdfPageBase) this.m_page);
        return (PdfAnnotation) widgetAnnotation;
      }

      protected override int DoAdd(PdfAnnotation annot)
      {
        if (annot == null)
          throw new ArgumentNullException("annotation");
        annot.SetPage((PdfPageBase) this.m_page);
        PdfArray primitive = !this.m_page.Dictionary.ContainsKey("Annots") ? new PdfArray() : this.m_page.CrossTable.GetObject(this.m_page.Dictionary["Annots"]) as PdfArray;
        PdfReferenceHolder element = new PdfReferenceHolder((IPdfWrapper) annot);
        if (!primitive.Contains((IPdfPrimitive) element))
        {
          primitive.Add((IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) annot));
          this.m_page.Dictionary.SetProperty("Annots", (IPdfPrimitive) primitive);
        }
        return base.DoAdd(annot);
      }

      protected override void DoClear()
      {
        int index = 0;
        for (int count = this.List.Count; index < count; ++index)
        {
          if (this.List[index] is PdfLoadedAnnotation annot)
            this.m_page.RemoveFromDictionaries((PdfAnnotation) annot);
        }
      }

      protected override void DoInsert(int index, PdfAnnotation annot)
      {
        if (index < 0 || index > this.List.Count)
          throw new IndexOutOfRangeException();
        if (annot == null)
          throw new ArgumentNullException("annotation");
        annot.SetPage((PdfPageBase) this.m_page);
        if (!(annot is PdfLoadedAnnotation))
        {
          PdfArray primitive = !this.m_page.Dictionary.ContainsKey("Annots") ? new PdfArray() : this.m_page.CrossTable.GetObject(this.m_page.Dictionary["Annots"]) as PdfArray;
          primitive.Insert(index, (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) annot));
          this.m_page.Dictionary.SetProperty("Annots", (IPdfPrimitive) primitive);
        }
        base.DoInsert(index, annot);
      }

      protected override void DoRemove(PdfAnnotation annot)
      {
        if (annot == null)
          throw new ArgumentNullException("annotation");
        this.m_page.RemoveFromDictionaries(annot);
        base.DoRemove(annot);
      }

      protected override void DoRemoveAt(int index)
      {
        if (index < 0 || index > this.List.Count)
          throw new IndexOutOfRangeException();
        PdfAnnotation annot = this.List[index] as PdfAnnotation;
        if (annot is PdfLoadedAnnotation)
          this.m_page.RemoveFromDictionaries(annot);
        base.DoRemoveAt(index);
      }

      internal bool FindAnnotation(PdfArray arr)
      {
        if (arr == null)
          return false;
        for (int index1 = 0; index1 < arr.Count; ++index1)
        {
          if (arr[index1] is PdfArray)
          {
            PdfArray pdfArray = arr[index1] as PdfArray;
            for (int index2 = 0; index2 < pdfArray.Count; ++index2)
            {
              if ((pdfArray[index2] as PdfNumber).IntValue > 0)
                return false;
            }
          }
          else if ((arr[index1] as PdfNumber).IntValue > 0)
            return false;
        }
        return true;
      }

      private PdfAnnotation GetAnnotation(int index)
      {
        PdfDictionary dictionary = this.m_page.TerminalAnnotation[index];
        PdfCrossTable crossTable = this.m_page.CrossTable;
        PdfAnnotation annotation = (PdfAnnotation) null;
        PdfLoadedAnnotationTypes annotationType = this.GetAnnotationType(PdfLoadedAnnotation.GetValue(dictionary, crossTable, "Subtype", true) as PdfName, dictionary, crossTable);
        if (PdfCrossTable.Dereference(dictionary["Rect"]) is PdfArray pdfArray1)
        {
          RectangleF rectangle = pdfArray1.ToRectangle();
          string empty = string.Empty;
          if (dictionary.ContainsKey("Contents"))
            empty = (dictionary["Contents"] as PdfString).Value.ToString();
          switch (annotationType)
          {
            case PdfLoadedAnnotationTypes.Highlight:
            case PdfLoadedAnnotationTypes.Underline:
            case PdfLoadedAnnotationTypes.StrikeOut:
            case PdfLoadedAnnotationTypes.Squiggly:
              annotation = this.CreateMarkupAnnotation(dictionary, crossTable, rectangle);
              break;
            case PdfLoadedAnnotationTypes.AnnotationStates:
              annotation = this.CreateAnnotationStates(dictionary, crossTable);
              break;
            case PdfLoadedAnnotationTypes.TextAnnotation:
              annotation = this.CreateTextAnnotation(dictionary, crossTable);
              break;
            case PdfLoadedAnnotationTypes.LinkAnnotation:
              if (!dictionary.ContainsKey("A"))
              {
                annotation = this.CreateLinkAnnotation(dictionary, crossTable, rectangle, empty);
                break;
              }
              PdfDictionary pdfDictionary1 = new PdfDictionary();
              PdfArray pdfArray = new PdfArray();
              PdfDictionary pdfDictionary2 = PdfCrossTable.Dereference(dictionary["A"]) as PdfDictionary;
              if (pdfDictionary2.ContainsKey("S"))
              {
                PdfArray destination = PdfCrossTable.Dereference(pdfDictionary2["D"]) as PdfArray;
                if ((PdfCrossTable.Dereference(pdfDictionary2["S"]) as PdfName).Value == "GoToR")
                {
                  if (!(PdfCrossTable.Dereference(pdfDictionary2["F"]) is PdfString))
                  {
                    if (PdfCrossTable.Dereference(pdfDictionary2["F"]) is PdfDictionary)
                    {
                      PdfDictionary pdfDictionary3 = PdfCrossTable.Dereference(pdfDictionary2["F"]) as PdfDictionary;
                      if (pdfDictionary3.ContainsKey("F"))
                      {
                        PdfString fileName = pdfDictionary3["F"] as PdfString;
                        annotation = this.CreateFileRemoteGoToLinkAnnotation(dictionary, crossTable, fileName, destination, rectangle);
                        break;
                      }
                      break;
                    }
                    break;
                  }
                  PdfString fileName1 = PdfCrossTable.Dereference(pdfDictionary2["F"]) as PdfString;
                  annotation = this.CreateFileRemoteGoToLinkAnnotation(dictionary, crossTable, fileName1, destination, rectangle);
                  break;
                }
                break;
              }
              break;
            case PdfLoadedAnnotationTypes.DocumentLinkAnnotation:
              annotation = this.CreateDocumentLinkAnnotation(dictionary, crossTable, rectangle);
              break;
            case PdfLoadedAnnotationTypes.FileLinkAnnotation:
              PdfReferenceHolder pdfReferenceHolder = dictionary["A"] as PdfReferenceHolder;
              if (pdfReferenceHolder == (PdfReferenceHolder) null)
              {
                if (dictionary.ContainsKey("A"))
                {
                  PdfDictionary pdfDictionary4 = dictionary["A"] as PdfDictionary;
                  if (pdfDictionary4.ContainsKey("F"))
                  {
                    if (pdfDictionary4["F"] is PdfDictionary)
                      pdfDictionary4 = PdfCrossTable.Dereference(pdfDictionary4["F"]) as PdfDictionary;
                    else if ((object) (pdfDictionary4["F"] as PdfReferenceHolder) != null)
                      pdfDictionary4 = (pdfDictionary4["F"] as PdfReferenceHolder).Object as PdfDictionary;
                    PdfString pdfString = pdfDictionary4["F"] as PdfString;
                    annotation = this.CreateFileLinkAnnotation(dictionary, crossTable, rectangle, pdfString.Value.ToString());
                    break;
                  }
                  break;
                }
                break;
              }
              if (!(PdfCrossTable.Dereference((pdfReferenceHolder.Object as PdfDictionary)["F"]) is PdfDictionary pdfDictionary5))
                pdfDictionary5 = pdfReferenceHolder.Object as PdfDictionary;
              if (!pdfDictionary5.ContainsKey("F"))
              {
                if (pdfDictionary5.ContainsKey("UF"))
                {
                  PdfString pdfString = !(pdfDictionary5["UF"] is PdfString) ? (pdfDictionary5["UF"] as PdfReferenceHolder).Object as PdfString : pdfDictionary5["UF"] as PdfString;
                  annotation = this.CreateFileLinkAnnotation(dictionary, crossTable, rectangle, pdfString.Value.ToString());
                  break;
                }
                break;
              }
              PdfString pdfString1 = !(pdfDictionary5["F"] is PdfString) ? (pdfDictionary5["F"] as PdfReferenceHolder).Object as PdfString : pdfDictionary5["F"] as PdfString;
              annotation = this.CreateFileLinkAnnotation(dictionary, crossTable, rectangle, pdfString1.Value.ToString());
              break;
            case PdfLoadedAnnotationTypes.FreeTextAnnotation:
              annotation = this.CreateFreeTextAnnotation(dictionary, crossTable);
              break;
            case PdfLoadedAnnotationTypes.LineAnnotation:
              annotation = this.CreateLineAnnotation(dictionary, crossTable, rectangle, empty);
              break;
            case PdfLoadedAnnotationTypes.SquareandCircleAnnotation:
              annotation = this.CreateSquareandCircleAnnotation(dictionary, crossTable);
              break;
            case PdfLoadedAnnotationTypes.PolygonandPolylineAnnotation:
              annotation = this.CreatePolygonandPolylineAnnotation(dictionary, crossTable);
              break;
            case PdfLoadedAnnotationTypes.TextMarkupAnnotation:
              annotation = this.CreateTextMarkupAnnotation(dictionary, crossTable);
              break;
            case PdfLoadedAnnotationTypes.CaretAnnotation:
              annotation = this.CreateCaretAnnotation(dictionary, crossTable);
              break;
            case PdfLoadedAnnotationTypes.RubberStampAnnotation:
              annotation = this.CreateRubberStampAnnotation(dictionary, crossTable, rectangle, empty);
              break;
            case PdfLoadedAnnotationTypes.LnkAnnotation:
              PdfString pdfString2 = (PdfString) null;
              if (dictionary.ContainsKey("A"))
                pdfString2 = (PdfCrossTable.Dereference((PdfCrossTable.Dereference(dictionary["A"]) as PdfDictionary)["F"]) as PdfDictionary)["F"] as PdfString;
              annotation = this.CreateLnkAnnotation(dictionary, crossTable, rectangle, pdfString2.Value.Substring(1));
              break;
            case PdfLoadedAnnotationTypes.PopupAnnotation:
              annotation = this.CreatePopupAnnotation(dictionary, crossTable, rectangle, empty);
              break;
            case PdfLoadedAnnotationTypes.FileAttachmentAnnotation:
              PdfString pdfString3 = PdfCrossTable.Dereference((PdfCrossTable.Dereference(dictionary["FS"]) as PdfDictionary)["F"]) as PdfString;
              annotation = this.CreateFileAttachmentAnnotation(dictionary, crossTable, rectangle, pdfString3.Value.ToString());
              break;
            case PdfLoadedAnnotationTypes.SoundAnnotation:
              PdfCrossTable.Dereference(dictionary["Sound"]);
              annotation = this.CreateSoundAnnotation(dictionary, crossTable, rectangle);
              break;
            case PdfLoadedAnnotationTypes.MovieAnnotation:
              annotation = this.CreateMovieAnnotation(dictionary, crossTable);
              break;
            case PdfLoadedAnnotationTypes.ScreenAnnotation:
              annotation = this.CreateScreenAnnotation(dictionary, crossTable, rectangle);
              break;
            case PdfLoadedAnnotationTypes.WidgetAnnotation:
              annotation = this.CreateWidgetAnnotation(dictionary, crossTable, rectangle);
              break;
            case PdfLoadedAnnotationTypes.PrinterMarkAnnotation:
              annotation = this.CreatePrinterMarkAnnotation(dictionary, crossTable);
              break;
            case PdfLoadedAnnotationTypes.TrapNetworkAnnotation:
              annotation = this.CreateTrapNetworkAnnotation(dictionary, crossTable);
              break;
            case PdfLoadedAnnotationTypes.WatermarkAnnotation:
              annotation = this.CreateWatermarkAnnotation(dictionary, crossTable);
              break;
            case PdfLoadedAnnotationTypes.TextWebLinkAnnotation:
              annotation = this.CreateTextWebLinkAnnotation(dictionary, crossTable, empty);
              break;
            case PdfLoadedAnnotationTypes.InkAnnotation:
              annotation = this.CreateInkAnnotation(dictionary, crossTable, rectangle);
              break;
          }
          if (annotation is PdfLoadedAnnotation loadedAnnotation)
            loadedAnnotation.BeforeNameChanges += new PdfLoadedAnnotation.BeforeNameChangesEventHandler(this.ldAnnotation_NameChanded);
        }
        return annotation;
      }

      private int GetAnnotationIndex(string text)
      {
        int annotationIndex = -1;
        foreach (PdfAnnotation pdfAnnotation in (IEnumerable) this.List)
        {
          ++annotationIndex;
          if (!(pdfAnnotation.Text == text))
          {
            if (pdfAnnotation.Text != null || pdfAnnotation.Text != string.Empty)
            {
              if (pdfAnnotation.Text.Split('(')[0] == text)
                return annotationIndex;
            }
          }
          else
            break;
        }
        if (annotationIndex == this.List.Count - 1 && (this.List[this.List.Count - 1] as PdfLoadedAnnotation).Text != text)
          annotationIndex = -1;
        return annotationIndex;
      }

      private PdfLoadedAnnotationTypes GetAnnotationType(
        PdfName name,
        PdfDictionary dictionary,
        PdfCrossTable crossTable)
      {
        string str = name.Value;
        PdfLoadedAnnotationTypes annotationType = PdfLoadedAnnotationTypes.Null;
        if (PdfLoadedAnnotation.GetValue(dictionary, crossTable, "Subtype", true) is PdfNumber pdfNumber)
        {
          int intValue = pdfNumber.IntValue;
        }
        switch (str.ToLower())
        {
          case "fileattachment":
            return PdfLoadedAnnotationTypes.FileAttachmentAnnotation;
          case "highlight":
            return PdfLoadedAnnotationTypes.Highlight;
          case "ink":
            return PdfLoadedAnnotationTypes.InkAnnotation;
          case "line":
            return PdfLoadedAnnotationTypes.LineAnnotation;
          case "link":
            if (!dictionary.ContainsKey("A"))
            {
              switch ((dictionary["Subtype"] as PdfName).Value.ToString())
              {
                case "Link":
                  annotationType = PdfLoadedAnnotationTypes.DocumentLinkAnnotation;
                  break;
              }
              return annotationType;
            }
            name = (PdfCrossTable.Dereference(dictionary["A"]) as PdfDictionary)["S"] as PdfName;
            bool annotation = this.FindAnnotation(dictionary["Border"] as PdfArray);
            if (!(name.Value.ToString() == "URI"))
            {
              if (name.Value.ToString() == "Launch")
                return PdfLoadedAnnotationTypes.FileLinkAnnotation;
              if (name.Value.ToString() == "GoToR")
                return PdfLoadedAnnotationTypes.LinkAnnotation;
              if (name.Value.ToString() == "GoTo")
                annotationType = PdfLoadedAnnotationTypes.DocumentLinkAnnotation;
              return annotationType;
            }
            return annotation ? PdfLoadedAnnotationTypes.TextWebLinkAnnotation : PdfLoadedAnnotationTypes.LinkAnnotation;
          case "sound":
            return PdfLoadedAnnotationTypes.SoundAnnotation;
          case "squiggly":
            return PdfLoadedAnnotationTypes.Squiggly;
          case "stamp":
            return PdfLoadedAnnotationTypes.RubberStampAnnotation;
          case "strikeout":
            return PdfLoadedAnnotationTypes.StrikeOut;
          case "text":
            return PdfLoadedAnnotationTypes.PopupAnnotation;
          case "underline":
            return PdfLoadedAnnotationTypes.Underline;
          case "widget":
            return PdfLoadedAnnotationTypes.WidgetAnnotation;
          default:
            return annotationType;
        }
      }

      internal string GetCorrectName(string name)
      {
        System.Collections.Generic.List<string> stringList = new System.Collections.Generic.List<string>();
        foreach (PdfAnnotation pdfAnnotation in (IEnumerable) this.List)
          stringList.Add(pdfAnnotation.Text);
        string correctName = name;
        int num = 0;
        while (stringList.IndexOf(correctName) != -1)
        {
          correctName = name + (object) num;
          ++num;
        }
        return correctName;
      }

      internal bool IsValidName(string name)
      {
        foreach (PdfAnnotation pdfAnnotation in (IEnumerable) this.List)
        {
          if (pdfAnnotation.Text == name)
            return false;
        }
        return true;
      }

      private void ldAnnotation_NameChanded(string name)
      {
        if (!this.IsValidName(name))
          throw new ArgumentException("Annotation with the same name already exist");
      }

      public PdfAnnotation this[string text]
      {
        get
        {
          if (text == null)
            throw new ArgumentNullException(nameof (text));
          int index = !(text == string.Empty) ? this.GetAnnotationIndex(text) : throw new ArgumentException("Annotation text can't be empty");
          return index != -1 ? this[index] : throw new ArgumentException("Incorrect field name");
        }
      }

      public override PdfAnnotation this[int index]
      {
        get
        {
          int count = this.List.Count;
          PdfAnnotation pdfAnnotation = count >= 0 && index < count ? this.List[index] as PdfAnnotation : throw new IndexOutOfRangeException(nameof (index));
          (pdfAnnotation as PdfLoadedAnnotation).Page = this.Page;
          return pdfAnnotation;
        }
      }

      public PdfLoadedPage Page
      {
        get => this.m_page;
        set => this.m_page = value;
      }
    }
}
