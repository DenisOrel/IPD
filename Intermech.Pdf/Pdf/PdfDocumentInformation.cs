// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PdfDocumentInformation
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using Syncfusion.Pdf.Xmp;
using System;


namespace Syncfusion.Pdf
{
    public class PdfDocumentInformation : IPdfWrapper
    {
      private string m_author;
      private PdfCatalog m_catalog;
      private DateTime m_creationDate;
      private string m_creator;
      private PdfDictionary m_dictionary;
      private string m_keywords;
      private DateTime m_modificationDate;
      private string m_producer;
      private string m_subject;
      private string m_title;
      private XmpMetadata m_xmp;

      internal PdfDocumentInformation(PdfCatalog catalog)
      {
        this.m_creationDate = DateTime.Now;
        if (catalog == null)
          throw new ArgumentNullException(nameof (catalog));
        this.m_dictionary = new PdfDictionary();
        if (PdfDocument.ConformanceLevel != PdfConformanceLevel.Pdf_A1B)
          this.m_dictionary.SetDateTime(nameof (CreationDate), this.m_creationDate);
        this.m_catalog = catalog;
      }

      internal PdfDocumentInformation(PdfDictionary dictionary, PdfCatalog catalog)
      {
        this.m_creationDate = DateTime.Now;
        if (dictionary == null)
          throw new ArgumentNullException(nameof (dictionary));
        if (catalog == null)
          throw new ArgumentNullException(nameof (catalog));
        this.m_dictionary = dictionary;
        this.m_catalog = catalog;
      }

      internal void ApplyPdfXConformance()
      {
        this.Dictionary["GTS_PDFXConformance"] = (IPdfPrimitive) new PdfString("PDF/X-1a:2001");
        this.Dictionary["Trapped"] = (IPdfPrimitive) new PdfName("False");
        this.Dictionary["GTS_PDFXVersion"] = (IPdfPrimitive) new PdfString("PDF/X-1:2001");
        this.ModificationDate = DateTime.Now;
        if (!(this.Title == string.Empty))
          return;
        this.Title = " ";
      }

      public string Author
      {
        get
        {
          if (!(this.m_dictionary[nameof (Author)] is PdfString pdfString))
            return this.m_author = string.Empty;
          this.m_author = pdfString.Value;
          return this.m_author;
        }
        set
        {
          if (!(this.m_author != value))
            return;
          this.m_author = value;
          this.m_dictionary.SetString(nameof (Author), this.m_author);
        }
      }

      public DateTime CreationDate
      {
        get
        {
          if (!(this.m_dictionary[nameof (CreationDate)] is PdfString dateTimeString))
            return this.m_creationDate = DateTime.Now;
          this.m_creationDate = this.m_dictionary.GetDateTime(dateTimeString);
          return this.m_creationDate;
        }
        set
        {
          if (!(this.m_creationDate != value))
            return;
          this.m_creationDate = value;
          this.m_dictionary.SetDateTime(nameof (CreationDate), this.m_creationDate);
        }
      }

      public string Creator
      {
        get
        {
          if (!(this.m_dictionary[nameof (Creator)] is PdfString pdfString))
            return this.m_creator = string.Empty;
          this.m_creator = pdfString.Value;
          return this.m_creator;
        }
        set
        {
          if (!(this.m_creator != value))
            return;
          this.m_creator = value;
          this.m_dictionary.SetString(nameof (Creator), this.m_creator);
        }
      }

      internal PdfDictionary Dictionary => this.m_dictionary;

      public string Keywords
      {
        get
        {
          if (!(this.m_dictionary[nameof (Keywords)] is PdfString pdfString))
            return this.m_keywords = string.Empty;
          this.m_keywords = pdfString.Value;
          return this.m_keywords;
        }
        set
        {
          if (!(value != this.m_keywords))
            return;
          this.m_keywords = value;
          this.m_dictionary.SetString(nameof (Keywords), this.m_keywords);
        }
      }

      public DateTime ModificationDate
      {
        get
        {
          if (!(this.m_dictionary["ModDate"] is PdfString dateTimeString))
            return this.m_creationDate = DateTime.Now;
          this.m_modificationDate = this.m_dictionary.GetDateTime(dateTimeString);
          return this.m_modificationDate;
        }
        set
        {
          if (!(this.m_modificationDate != value))
            return;
          this.m_modificationDate = value;
          this.m_dictionary.SetDateTime("ModDate", this.m_modificationDate);
        }
      }

      public string Producer
      {
        get
        {
          if (!(this.m_dictionary[nameof (Producer)] is PdfString pdfString))
            return this.m_producer = string.Empty;
          this.m_producer = pdfString.Value;
          return this.m_producer;
        }
        set
        {
          if (!(this.m_producer != value))
            return;
          this.m_producer = value;
          this.m_dictionary.SetString(nameof (Producer), this.m_producer);
        }
      }

      public string Subject
      {
        get
        {
          if (!(this.m_dictionary[nameof (Subject)] is PdfString pdfString))
            return this.m_subject = string.Empty;
          this.m_subject = pdfString.Value;
          return this.m_subject;
        }
        set
        {
          if (!(this.m_subject != value))
            return;
          this.m_subject = value;
          this.m_dictionary.SetString(nameof (Subject), this.m_subject);
        }
      }

      IPdfPrimitive IPdfWrapper.Element => (IPdfPrimitive) this.m_dictionary;

      public string Title
      {
        get
        {
          if (!(this.m_dictionary[nameof (Title)] is PdfString pdfString))
            return this.m_title = string.Empty;
          this.m_title = pdfString.Value;
          return this.m_title;
        }
        set
        {
          if (!(this.m_title != value))
            return;
          this.m_title = value;
          this.m_dictionary.SetString(nameof (Title), this.m_title);
        }
      }

      public XmpMetadata XmpMetadata
      {
        get
        {
          if (this.m_xmp == null)
          {
            if (this.m_catalog.Metadata == null)
            {
              this.m_xmp = new XmpMetadata(this.m_catalog.Pages.Document.DocumentInformation);
              this.m_catalog.SetProperty("Metadata", (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) this.m_xmp));
            }
            else
              this.m_xmp = this.m_catalog.Metadata;
          }
          return this.m_xmp;
        }
      }
    }
}
