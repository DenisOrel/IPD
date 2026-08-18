// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfLoadedStyledAnnotation
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Primitives;
using System;
using System.Drawing;

#nullable disable
namespace Syncfusion.Pdf.Interactive;

public class PdfLoadedStyledAnnotation : PdfLoadedAnnotation
{
  private PdfColor m_color;
  private PdfCrossTable m_crossTable;
  private PdfDictionary m_dictionary;
  private PdfSound m_sound;
  private string m_text;

  internal PdfLoadedStyledAnnotation(PdfDictionary dictionary, PdfCrossTable crossTable)
    : base(dictionary, crossTable)
  {
    this.m_dictionary = dictionary;
    this.m_crossTable = crossTable;
  }

  private PdfAnnotationFlags GetAnnotationFlags()
  {
    PdfAnnotationFlags annotationFlags = PdfAnnotationFlags.Default;
    if (this.Dictionary.ContainsKey("F"))
      annotationFlags = (PdfAnnotationFlags) (PdfLoadedAnnotation.GetValue(this.Dictionary, this.m_crossTable, "F", false) as PdfNumber).IntValue;
    return annotationFlags;
  }

  private PdfAnnotationBorder GetBorder()
  {
    PdfAnnotationBorder border = (PdfAnnotationBorder) null;
    if (this.Dictionary.ContainsKey("Border"))
    {
      PdfArray pdfArray = this.Dictionary["Border"] as PdfArray;
      float floatValue1 = (pdfArray[0] as PdfNumber).FloatValue;
      float floatValue2 = (pdfArray[1] as PdfNumber).FloatValue;
      float floatValue3 = (pdfArray[2] as PdfNumber).FloatValue;
      border = new PdfAnnotationBorder(floatValue1, floatValue2, floatValue3);
      border.Width = floatValue3;
      border.HorizontalRadius = floatValue1;
      border.VerticalRadius = floatValue2;
    }
    return border;
  }

  private RectangleF GetBounds(PdfDictionary dictionary, PdfCrossTable crossTable)
  {
    PdfArray pdfArray = (PdfArray) null;
    if (dictionary.ContainsKey("Kids"))
    {
      PdfDictionary widgetAnnotation = this.GetWidgetAnnotation(dictionary, crossTable);
      if (widgetAnnotation.ContainsKey("Rect"))
        pdfArray = crossTable.GetObject(widgetAnnotation["Rect"]) as PdfArray;
    }
    else if (dictionary.ContainsKey("Rect"))
      pdfArray = crossTable.GetObject(dictionary["Rect"]) as PdfArray;
    return pdfArray.ToRectangle();
  }

  private PdfColor GetColor()
  {
    PdfColorSpace colorSpace = PdfColorSpace.RGB;
    PdfArray pdfArray = !this.Dictionary.ContainsKey("C") ? this.m_color.ToArray(colorSpace) : this.Dictionary["C"] as PdfArray;
    return new PdfColor((pdfArray[0] as PdfNumber).FloatValue, (pdfArray[1] as PdfNumber).FloatValue, (pdfArray[2] as PdfNumber).FloatValue);
  }

  private PdfPage GetLoadedPage()
  {
    PdfPageBase loadedPage = (PdfPageBase) this.Page;
    if (loadedPage == null)
    {
      PdfLoadedDocument document = this.CrossTable.Document as PdfLoadedDocument;
      PdfDictionary pdfDictionary = this.GetWidgetAnnotation(this.Dictionary, this.CrossTable) ?? this.Dictionary;
      if (pdfDictionary.ContainsKey("P"))
      {
        if (this.CrossTable.GetObject(pdfDictionary["P"]) is PdfDictionary dic)
          loadedPage = document.Pages.GetPage(dic);
      }
      else
      {
        PdfReference reference = this.CrossTable.GetReference((IPdfPrimitive) pdfDictionary);
        foreach (PdfLoadedPage page in document.Pages)
        {
          PdfArray annots = page.GetAnnots();
          if (annots != null)
          {
            for (int index = 0; index < annots.Count; ++index)
            {
              if ((annots[index] as PdfReferenceHolder).Reference == reference)
              {
                loadedPage = (PdfPageBase) page;
                return loadedPage as PdfPage;
              }
            }
          }
        }
      }
    }
    return loadedPage as PdfPage;
  }

  private string GetText()
  {
    if (!this.Dictionary.ContainsKey("Contents"))
      return " ";
    return (this.Dictionary["Contents"] as PdfString).Value.ToString().Trim('/');
  }

  public new PdfAnnotationFlags AnnotationFlags
  {
    get => this.GetAnnotationFlags();
    set
    {
      base.AnnotationFlags = value;
      this.Changed = true;
    }
  }

  public new PdfAnnotationBorder Border
  {
    get => this.GetBorder();
    set
    {
      base.Border = value;
      this.Changed = true;
    }
  }

  public new RectangleF Bounds
  {
    get
    {
      RectangleF bounds = this.GetBounds(this.Dictionary, this.CrossTable);
      if (this.Page != null)
      {
        bounds.Y = this.Page.Size.Height - (bounds.Y + bounds.Height);
        return bounds;
      }
      bounds.Y += bounds.Height;
      return bounds;
    }
    set
    {
      RectangleF rectangleF = value;
      if (rectangleF == RectangleF.Empty)
        throw new ArgumentNullException("rectangle");
      float height = this.Page.Size.Height;
      PdfNumber[] pdfNumberArray = new PdfNumber[4]
      {
        new PdfNumber(rectangleF.X),
        new PdfNumber(height - (rectangleF.Y + rectangleF.Height)),
        new PdfNumber(rectangleF.X + rectangleF.Width),
        new PdfNumber(height - rectangleF.Y)
      };
      PdfDictionary pdfDictionary = this.Dictionary;
      if (!pdfDictionary.ContainsKey("Rect"))
        pdfDictionary = this.GetWidgetAnnotation(this.Dictionary, this.CrossTable);
      pdfDictionary.SetArray("Rect", (IPdfPrimitive[]) pdfNumberArray);
      this.Changed = true;
    }
  }

  public new PdfColor Color
  {
    get => this.GetColor();
    set
    {
      base.Color = value;
      this.m_color = value;
    }
  }

  public new PointF Location
  {
    get => this.Bounds.Location;
    set => this.Bounds = new RectangleF(value, this.Bounds.Size);
  }

  public new SizeF Size
  {
    get => this.Bounds.Size;
    set => this.Bounds = new RectangleF(this.Bounds.Location, value);
  }

  public new string Text
  {
    get => this.GetText();
    set
    {
      base.Text = value;
      this.m_text = value;
    }
  }
}
