// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfDocumentActions
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System;


namespace Syncfusion.Pdf.Interactive
{
    public class PdfDocumentActions : IPdfWrapper
    {
      private PdfAction m_afterOpen;
      private PdfJavaScriptAction m_afterPrint;
      private PdfJavaScriptAction m_afterSave;
      private PdfJavaScriptAction m_beforeClose;
      private PdfJavaScriptAction m_beforePrint;
      private PdfJavaScriptAction m_beforeSave;
      private PdfCatalog m_catalog;
      private PdfDictionary m_dictionary = new PdfDictionary();

      internal PdfDocumentActions(PdfCatalog catalog)
      {
        if (catalog == null)
          throw new ArgumentNullException(nameof (catalog));
        if (PdfDocument.ConformanceLevel == PdfConformanceLevel.Pdf_A1B)
          throw new PdfConformanceException("Usage of Javascript are not allowed by the PDF/A1-B standard");
        this.m_catalog = catalog;
      }

      public PdfAction AfterOpen
      {
        get => this.m_afterOpen;
        set
        {
          if (value == this.m_afterOpen)
            return;
          this.m_afterOpen = value;
          PdfDictionary.SetProperty((PdfDictionary) this.m_catalog, "OpenAction", (IPdfWrapper) this.m_afterOpen);
        }
      }

      public PdfJavaScriptAction AfterPrint
      {
        get => this.m_afterPrint;
        set
        {
          if (value == this.m_afterPrint)
            return;
          this.m_afterPrint = value;
          this.m_dictionary.SetProperty("DP", (IPdfWrapper) this.m_afterPrint);
        }
      }

      public PdfJavaScriptAction AfterSave
      {
        get => this.m_afterSave;
        set
        {
          if (value == this.m_afterSave)
            return;
          this.m_afterSave = value;
          this.m_dictionary.SetProperty("DS", (IPdfWrapper) this.m_afterSave);
        }
      }

      public PdfJavaScriptAction BeforeClose
      {
        get => this.m_beforeClose;
        set
        {
          if (value == this.m_beforeClose)
            return;
          this.m_beforeClose = value;
          this.m_dictionary.SetProperty("WC", (IPdfWrapper) this.m_beforeClose);
        }
      }

      public PdfJavaScriptAction BeforePrint
      {
        get => this.m_beforePrint;
        set
        {
          if (value == this.m_beforePrint)
            return;
          this.m_beforePrint = value;
          this.m_dictionary.SetProperty("WP", (IPdfWrapper) this.m_beforePrint);
        }
      }

      public PdfJavaScriptAction BeforeSave
      {
        get => this.m_beforeSave;
        set
        {
          if (value == this.m_beforeSave)
            return;
          this.m_beforeSave = value;
          this.m_dictionary.SetProperty("WS", (IPdfWrapper) this.m_beforeSave);
        }
      }

      IPdfPrimitive IPdfWrapper.Element => (IPdfPrimitive) this.m_dictionary;
    }
}
