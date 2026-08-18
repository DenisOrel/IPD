// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfAnnotation
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Primitives;
using System;
using System.Drawing;


namespace Syncfusion.Pdf.Interactive
{
    public abstract class PdfAnnotation : IPdfWrapper
    {
      private PdfAnnotationFlags m_annotationFlags;
      private PdfAnnotationBorder m_border;
      private PdfColor m_color;
      private PdfDictionary m_dictionary;
      private PdfPage m_page;
      private RectangleF m_rectangle;
      private string m_text;

      internal PdfAnnotation()
      {
        this.m_color = PdfColor.Empty;
        this.m_rectangle = RectangleF.Empty;
        this.m_text = string.Empty;
        this.m_dictionary = new PdfDictionary();
        this.Initialize();
      }

      protected PdfAnnotation(RectangleF bounds)
      {
        this.m_color = PdfColor.Empty;
        this.m_rectangle = RectangleF.Empty;
        this.m_text = string.Empty;
        this.m_dictionary = new PdfDictionary();
        this.Initialize();
        this.Bounds = bounds;
      }

      protected PdfAnnotation(PdfPageBase page, string text)
      {
        this.m_color = PdfColor.Empty;
        this.m_rectangle = RectangleF.Empty;
        this.m_text = string.Empty;
        this.m_dictionary = new PdfDictionary();
        this.Initialize();
        this.m_page = page as PdfPage;
        this.m_text = text;
        this.m_dictionary.SetProperty("Contents", (IPdfPrimitive) new PdfString(text));
      }

      internal PdfAnnotation(PdfDictionary dictionary, PdfCrossTable crossTable, RectangleF bounds)
      {
        this.m_color = PdfColor.Empty;
        this.m_rectangle = RectangleF.Empty;
        this.m_text = string.Empty;
        this.m_dictionary = new PdfDictionary();
        this.Initialize();
        this.Bounds = bounds;
      }

      internal virtual void ApplyText(string text)
      {
        this.m_text = text;
        this.Dictionary.SetProperty("Contents", (IPdfPrimitive) new PdfString(text));
      }

      private void Dictionary_BeginSave(object sender, SavePdfPrimitiveEventArgs ars) => this.Save();

      protected virtual void Initialize()
      {
        this.m_dictionary.BeginSave += new SavePdfPrimitiveEventHandler(this.Dictionary_BeginSave);
        this.m_dictionary.SetProperty("Type", (IPdfPrimitive) new PdfName("Annot"));
      }

      protected virtual void Save()
      {
        if ((this.GetType().ToString().Contains("Pdf3DAnnotation") || this.GetType().ToString().Contains("PdfAttachmentAnnotation") || this.GetType().ToString().Contains("PdfSoundAnnotation") || this.GetType().ToString().Contains("PdfActionAnnotation")) && (PdfDocument.ConformanceLevel == PdfConformanceLevel.Pdf_A1B || PdfDocument.ConformanceLevel == PdfConformanceLevel.Pdf_X1A2001))
          throw new PdfConformanceException("The specified annotation type is not supported by PDF/A1-B standard document.");
        if (this.m_border != null)
          this.m_dictionary.SetProperty("Border", (IPdfWrapper) this.m_border);
        RectangleF rectangle = new RectangleF(this.m_rectangle.X, this.m_rectangle.Bottom, this.m_rectangle.Width, this.m_rectangle.Height);
        if (this.m_page != null)
          rectangle.Location = this.m_page.Section.PointToNativePdf(this.Page, rectangle.Location);
        this.m_dictionary.SetProperty("Rect", (IPdfPrimitive) PdfArray.FromRectangle(rectangle));
      }

      internal void SetLocation(PointF location) => this.m_rectangle.Location = location;

      internal void SetPage(PdfPageBase page)
      {
        this.m_page = page as PdfPage;
        if (this.m_page == null)
          return;
        this.m_dictionary.SetProperty("P", (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) this.m_page));
      }

      internal void SetSize(SizeF size) => this.m_rectangle.Size = size;

      public PdfAnnotationFlags AnnotationFlags
      {
        get => this.m_annotationFlags;
        set
        {
          if (this.m_annotationFlags == value)
            return;
          this.m_annotationFlags = value;
          this.m_dictionary.SetNumber("F", (int) this.m_annotationFlags);
        }
      }

      public PdfAnnotationBorder Border
      {
        get
        {
          if (this.m_border == null)
            this.m_border = new PdfAnnotationBorder();
          return this.m_border;
        }
        set
        {
          this.m_border = value;
          this.Dictionary.SetProperty(nameof (Border), (IPdfWrapper) this.m_border);
        }
      }

      public RectangleF Bounds
      {
        get => this.m_rectangle;
        set
        {
          if (!(this.m_rectangle != value))
            return;
          this.m_rectangle = value;
        }
      }

      public PdfColor Color
      {
        get => this.m_color;
        set
        {
          if (!(this.m_color != value))
            return;
          this.m_color = value;
          PdfColorSpace colorSpace = PdfColorSpace.RGB;
          if (this.Page != null)
            colorSpace = this.Page.Section.Parent.Document.ColorSpace;
          this.m_dictionary.SetProperty("C", (IPdfPrimitive) this.m_color.ToArray(colorSpace));
        }
      }

      internal PdfDictionary Dictionary
      {
        get => this.m_dictionary;
        set => this.m_dictionary = value;
      }

      public PointF Location
      {
        get => this.m_rectangle.Location;
        set
        {
          this.m_rectangle = this.Bounds;
          this.m_rectangle.Location = value;
          this.Dictionary.SetProperty("Rect", (IPdfPrimitive) PdfArray.FromRectangle(this.m_rectangle));
        }
      }

      public PdfPage Page => this.m_page;

      public SizeF Size
      {
        get => this.m_rectangle.Size;
        set
        {
          this.m_rectangle = this.Bounds;
          this.m_rectangle.Size = value;
          this.Dictionary.SetProperty("Rect", (IPdfPrimitive) PdfArray.FromRectangle(this.m_rectangle));
        }
      }

      IPdfPrimitive IPdfWrapper.Element => (IPdfPrimitive) this.m_dictionary;

      public string Text
      {
        get => this.m_text;
        set
        {
          if (value == null)
            throw new ArgumentNullException(nameof (Text));
          if (!(this.m_text != value))
            return;
          this.m_text = value;
          this.Dictionary.SetString("Contents", this.m_text);
        }
      }
    }
}
