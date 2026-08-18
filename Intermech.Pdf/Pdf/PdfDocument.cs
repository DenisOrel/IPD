// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PdfDocument
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.ColorSpace;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Primitives;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.IO;


namespace Syncfusion.Pdf
{
    public class PdfDocument : PdfDocumentBase
    {
      internal static PdfConformanceLevel ConformanceLevel;
      internal const float DefaultMargin = 40f;
      private PdfDocumentActions m_actions;
      private PdfAttachmentCollection m_attachments;
      private bool m_bPageLabels;
      private bool m_bWasEncrypted;
      private PdfColorSpace m_colorSpace;
      private static bool m_enableCache = true;
      private bool m_isPdfViewerDocumentDisable;
      private PdfBookmarkBase m_outlines;
      private PdfDocumentPageCollection m_pages;
      private PdfDocumentTemplate m_pageTemplate;
      internal static PrivateFontCollection m_privateFonts = new PrivateFontCollection();
      private PdfDocument.ProgressEventHandler m_progressDelegade;
      private PdfSectionCollection m_sections;
      private PdfPageSettings m_settings;
      private static readonly PdfCacheCollection s_cache = new PdfCacheCollection();
      private static object s_cacheLock = new object();
      private static PdfFont s_defaultFont = (PdfFont) null;

      public event PdfDocument.ProgressEventHandler SaveProgress
      {
        add
        {
          this.m_progressDelegade = Delegate.Combine((Delegate) this.m_progressDelegade, (Delegate) value) as PdfDocument.ProgressEventHandler;
          if (this.m_progressDelegade == null)
            return;
          this.SetProgress();
        }
        remove
        {
          this.m_progressDelegade = Delegate.Remove((Delegate) this.m_progressDelegade, (Delegate) value) as PdfDocument.ProgressEventHandler;
          if (this.m_progressDelegade != null)
            return;
          this.ResetProgress();
        }
      }

      public PdfDocument()
        : this(false)
      {
      }

      public PdfDocument(PdfConformanceLevel conformance)
        : this()
      {
        PdfDocument.ConformanceLevel = conformance;
        if (this.Conformance == PdfConformanceLevel.Pdf_A1B)
        {
          this.FileStructure.CrossReferenceType = PdfCrossReferenceType.CrossReferenceTable;
          this.FileStructure.Version = PdfVersion.Version1_4;
          this.SetDocumentColorProfile();
        }
        else
        {
          if (conformance != PdfConformanceLevel.Pdf_X1A2001)
            return;
          this.FileStructure.Version = PdfVersion.Version1_3;
          this.FileStructure.CrossReferenceType = PdfCrossReferenceType.CrossReferenceTable;
          this.DocumentInformation.XmpMetadata.ToString();
          this.DocumentInformation.ApplyPdfXConformance();
          this.Catalog.ApplyPdfXConformance();
        }
      }

      internal PdfDocument(bool isMerging)
      {
        this.m_isPdfViewerDocumentDisable = true;
        if (PdfDocumentBase.IsSecurityGranted)
          PdfDocument.ValidateLicense();
        PdfMainObjectCollection moc = new PdfMainObjectCollection();
        this.SetMainObjectCollection(moc);
        this.SetCrossTable(new PdfCrossTable()
        {
          IsMerging = isMerging,
          Document = (PdfDocumentBase) this
        });
        PdfCatalog pdfCatalog = new PdfCatalog();
        this.SetCatalog(pdfCatalog);
        moc.Add((IPdfPrimitive) pdfCatalog);
        if (!isMerging)
          pdfCatalog.Position = -1;
        this.m_sections = new PdfSectionCollection(this);
        this.m_pages = new PdfDocumentPageCollection(this);
        pdfCatalog.Pages = this.m_sections;
      }

      internal override void AddFields(
        PdfLoadedDocument ldDoc,
        PdfPageBase newPage,
        List<PdfField> fields)
      {
        if (ldDoc.Catalog.ContainsKey("AcroForm"))
        {
          PdfDictionary pdfDictionary1 = (PdfDictionary) null;
          if ((object) (ldDoc.Catalog["AcroForm"] as PdfReferenceHolder) != null)
            pdfDictionary1 = (ldDoc.Catalog["AcroForm"] as PdfReferenceHolder).Object as PdfDictionary;
          else if (ldDoc.Catalog["AcroForm"] is PdfDictionary)
            pdfDictionary1 = ldDoc.Catalog["AcroForm"] as PdfDictionary;
          if (pdfDictionary1 != null)
          {
            if (pdfDictionary1.ContainsKey("DR"))
            {
              if (pdfDictionary1["DR"] is PdfDictionary pdfDictionary5 && pdfDictionary5.ContainsKey("Font"))
              {
                PdfDictionary pdfDictionary2 = pdfDictionary5["Font"] as PdfDictionary;
                if (this.Form.Dictionary != null)
                {
                  if (this.Form.Dictionary.ContainsKey("DR"))
                  {
                    PdfDictionary pdfDictionary3 = (this.Form.Dictionary["DR"] as PdfDictionary)["Font"] as PdfDictionary;
                    foreach (KeyValuePair<PdfName, IPdfPrimitive> keyValuePair in (this.EnableMemoryOptimization ? pdfDictionary2.Clone(this.CrossTable) as PdfDictionary : pdfDictionary2).Items)
                    {
                      if (!pdfDictionary3.Items.ContainsKey(keyValuePair.Key))
                        pdfDictionary3.Items.Add(keyValuePair.Key, keyValuePair.Value);
                    }
                    pdfDictionary3.Modify();
                  }
                  else
                  {
                    PdfResources primitive = new PdfResources(!this.EnableMemoryOptimization ? pdfDictionary5 : pdfDictionary5.Clone(this.CrossTable) as PdfDictionary);
                    this.Form.Resources = primitive;
                    this.Form.Dictionary.SetProperty("DR", (IPdfPrimitive) primitive);
                    this.Form.Dictionary.Modify();
                  }
                }
              }
              else if ((object) (pdfDictionary1["DR"] as PdfReferenceHolder) != null && (pdfDictionary1["DR"] as PdfReferenceHolder).Object is PdfDictionary baseDictionary && baseDictionary.ContainsKey("Font"))
              {
                Dictionary<PdfName, IPdfPrimitive> items = (baseDictionary["Font"] as PdfDictionary).Items;
                if (this.Form.Dictionary != null)
                {
                  if (this.Form.Dictionary.ContainsKey("DR"))
                  {
                    PdfDictionary pdfDictionary4 = (this.Form.Dictionary["DR"] as PdfDictionary)["Font"] as PdfDictionary;
                    foreach (KeyValuePair<PdfName, IPdfPrimitive> keyValuePair in items)
                    {
                      if (!pdfDictionary4.Items.ContainsKey(keyValuePair.Key))
                        pdfDictionary4.Items.Add(keyValuePair.Key, keyValuePair.Value);
                    }
                    pdfDictionary4.Modify();
                  }
                  else
                  {
                    PdfResources primitive = new PdfResources(baseDictionary);
                    this.Form.Resources = primitive;
                    this.Form.Dictionary.SetProperty("DR", (IPdfPrimitive) primitive);
                    this.Form.Dictionary.Modify();
                  }
                }
              }
            }
            this.Form.SetAppearanceDictionary = ldDoc.Form.SetAppearanceDictionary;
            this.Form.NeedAppearances = ldDoc.Form.NeedAppearances;
          }
        }
        int index = 0;
        for (int count = fields.Count; index < count; ++index)
        {
          if (!this.EnableMemoryOptimization && fields[index].Dictionary.ContainsKey("P"))
            fields[index].Dictionary.Remove("P");
          this.Form.Fields.Add(fields[index], newPage);
        }
        if (!this.EnableMemoryOptimization || this.Form == null || this.Form.Fields.Count <= 0 || ldDoc.Form == null)
          return;
        PdfReferenceHolder pdfReferenceHolder = ldDoc.Catalog["AcroForm"] as PdfReferenceHolder;
        if (!(pdfReferenceHolder != (PdfReferenceHolder) null) || ldDoc.CrossTable.PageCorrespondance.ContainsKey((IPdfPrimitive) pdfReferenceHolder.Reference))
          return;
        PdfReference reference = this.CrossTable.GetReference((IPdfPrimitive) this.Form.Dictionary);
        ldDoc.CrossTable.PageCorrespondance.Add((IPdfPrimitive) pdfReferenceHolder.Reference, (object) reference);
      }

      private void CheckPagesPresence()
      {
        if (this.Pages.Count != 0)
          return;
        this.Pages.Add();
      }

      public object Clone()
      {
        if (this.CrossTable.EncryptorDictionary != null)
          throw new ArgumentException("Can't clone the Encrypted document");
        return this.MemberwiseClone();
      }

      internal override PdfPageBase ClonePage(
        PdfLoadedDocument ldDoc,
        PdfPageBase page,
        List<PdfArray> destinations)
      {
        return this.Pages.Add(ldDoc, page, destinations);
      }

      public override void Close(bool completely)
      {
        if (completely && this.Form != null && this.EnableMemoryOptimization)
          this.Form.Clear();
        if (completely && this.EnableMemoryOptimization)
        {
          this.m_off = (PdfArray) null;
          this.m_on = (PdfArray) null;
          this.m_order = (PdfArray) null;
          if (this.m_outlines != null)
            this.m_outlines.Clear();
          this.m_progressDelegade = (PdfDocument.ProgressEventHandler) null;
          this.m_sublayer = (PdfArray) null;
          if (this.m_pages != null)
            this.m_pages.Clear();
          if (this.m_sections != null)
            this.m_sections.Clear();
          PdfDocument.s_defaultFont = (PdfFont) null;
        }
        base.Close(completely);
        PdfDocument.ConformanceLevel = PdfConformanceLevel.None;
        this.m_pageTemplate = (PdfDocumentTemplate) null;
        this.m_attachments = (PdfAttachmentCollection) null;
        this.m_pages = (PdfDocumentPageCollection) null;
        this.m_sections = (PdfSectionCollection) null;
        this.m_settings = (PdfPageSettings) null;
        this.m_outlines = (PdfBookmarkBase) null;
        this.m_bPageLabels = false;
        this.m_bWasEncrypted = false;
        this.m_actions = (PdfDocumentActions) null;
        PdfDocument.m_privateFonts = (PrivateFontCollection) null;
        GC.WaitForPendingFinalizers();
      }

      internal override PdfForm GetForm() => this.Form;

      internal void OnPageSave(PdfPage page)
      {
        if (this.m_progressDelegade == null)
          return;
        this.OnSaveProgress(new ProgressEventArgs(this.Pages.IndexOf(page), this.Pages.Count));
      }

      protected virtual void OnSaveProgress(ProgressEventArgs arguments)
      {
        if (this.m_progressDelegade == null)
          return;
        this.m_progressDelegade((object) this, arguments);
      }

      internal void PageLabelsSet() => this.m_bPageLabels = true;

      private void ProcessPageLabels()
      {
        if (!this.m_bPageLabels)
          return;
        if (!(this.Catalog["PageLabels"] is PdfDictionary pdfDictionary))
        {
          pdfDictionary = new PdfDictionary();
          this.Catalog["PageLabels"] = (IPdfPrimitive) pdfDictionary;
        }
        PdfArray pdfArray = new PdfArray();
        pdfDictionary["Nums"] = (IPdfPrimitive) pdfArray;
        int num = 0;
        foreach (PdfSection section in this.Sections)
        {
          PdfPageLabel pdfPageLabel = section.PageLabel ?? new PdfPageLabel();
          pdfArray.Add((IPdfPrimitive) new PdfNumber(num));
          pdfArray.Add(((IPdfWrapper) pdfPageLabel).Element);
          num += section.Count;
        }
      }

      private void ResetProgress() => this.Sections.ResetProgress();

      public override void Save(Stream stream)
      {
        if (stream == null)
          throw new ArgumentNullException(nameof (stream));
        this.CheckPagesPresence();
        if (this.Conformance == PdfConformanceLevel.Pdf_A1B)
          this.DocumentInformation.XmpMetadata.ToString();
        PdfWriter writer = new PdfWriter(stream);
        writer.Document = (PdfDocumentBase) this;
        if (this.m_outlines != null && this.m_outlines.Count < 1)
          this.Catalog.Remove("Outlines");
        if (this.FileStructure.TaggedPdf)
        {
          this.Catalog["Lang"] = (IPdfPrimitive) new PdfString("en");
          if (!this.Catalog.ContainsKey("MarkInfo"))
            this.Catalog["MarkInfo"] = (IPdfPrimitive) new PdfDictionary();
          (this.Catalog["MarkInfo"] as PdfDictionary)["Marked"] = (IPdfPrimitive) new PdfBoolean(true);
        }
        this.ProcessPageLabels();
        this.CrossTable.Save(writer);
        if (this.m_progressDelegade != null)
        {
          int count = this.Pages.Count;
          this.OnSaveProgress(new ProgressEventArgs(count, count));
        }
        this.OnDocumentSaved(new DocumentSavedEventArgs(writer));
        PdfDocument.ConformanceLevel = PdfConformanceLevel.None;
        writer.Close();
      }

      private void SetDocumentColorProfile()
      {
        PdfDictionary pdfDictionary = new PdfDictionary();
        pdfDictionary["Info"] = (IPdfPrimitive) new PdfString("sRGB IEC61966-2.1");
        pdfDictionary["S"] = (IPdfPrimitive) new PdfName("GTS_PDFA1");
        pdfDictionary["OutputConditionIdentifier"] = (IPdfPrimitive) new PdfString("custom");
        pdfDictionary["Type"] = (IPdfPrimitive) new PdfName("OutputIntent");
        pdfDictionary["OutputCondition"] = (IPdfPrimitive) new PdfString("");
        pdfDictionary["RegistryName"] = (IPdfPrimitive) new PdfString("");
        PdfICCColorProfile wrapper = new PdfICCColorProfile();
        pdfDictionary["DestOutputProfile"] = (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) wrapper);
        this.Catalog["OutputIntents"] = (IPdfPrimitive) new PdfArray()
        {
          (IPdfPrimitive) pdfDictionary
        };
      }

      private void SetProgress() => this.Sections.SetProgress();

      internal static void ValidateLicense()
      {
      }

      public PdfDocumentActions Actions
      {
        get
        {
          if (this.m_actions == null)
          {
            this.m_actions = new PdfDocumentActions(this.Catalog);
            this.Catalog["AA"] = ((IPdfWrapper) this.m_actions).Element;
          }
          return this.m_actions;
        }
      }

      public PdfAttachmentCollection Attachments
      {
        get
        {
          if (this.m_attachments == null)
          {
            this.m_attachments = new PdfAttachmentCollection();
            this.Catalog.Names.EmbeddedFiles = this.m_attachments;
          }
          return this.m_attachments;
        }
      }

      public override PdfBookmarkBase Bookmarks
      {
        get
        {
          if (this.m_outlines == null)
          {
            this.m_outlines = new PdfBookmarkBase();
            this.Catalog["Outlines"] = (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) this.m_outlines);
          }
          return this.m_outlines;
        }
      }

      internal static PdfCacheCollection Cache
      {
        get
        {
          lock (PdfDocument.s_cacheLock)
            return PdfDocument.s_cache == null ? new PdfCacheCollection() : PdfDocument.s_cache;
        }
      }

      public PdfColorSpace ColorSpace
      {
        get
        {
          return this.m_colorSpace != PdfColorSpace.RGB && this.m_colorSpace != PdfColorSpace.CMYK && this.m_colorSpace != PdfColorSpace.GrayScale ? PdfColorSpace.RGB : this.m_colorSpace;
        }
        set
        {
          if (value == PdfColorSpace.RGB || value == PdfColorSpace.CMYK || value == PdfColorSpace.GrayScale)
            this.m_colorSpace = value;
          else
            this.m_colorSpace = PdfColorSpace.RGB;
        }
      }

      public PdfConformanceLevel Conformance => PdfDocument.ConformanceLevel;

      internal static PdfFont DefaultFont
      {
        get
        {
          lock (PdfDocument.s_cacheLock)
          {
            if (PdfDocument.s_defaultFont == null)
              PdfDocument.s_defaultFont = (PdfFont) new PdfStandardFont(PdfFontFamily.Helvetica, 8f);
          }
          return PdfDocument.s_defaultFont;
        }
      }

      public static bool EnableCache
      {
        get => PdfDocument.m_enableCache;
        set => PdfDocument.m_enableCache = value;
      }

      public PdfForm Form
      {
        get
        {
          if (this.Catalog.Form == null)
            this.Catalog.Form = new PdfForm();
          return this.Catalog.Form;
        }
      }

      internal override bool IsPdfViewerDocumentDisable
      {
        get => this.m_isPdfViewerDocumentDisable;
        set => this.m_isPdfViewerDocumentDisable = value;
      }

      internal override int PageCount => this.Pages.Count;

      public PdfDocumentPageCollection Pages => this.m_pages;

      public PdfPageSettings PageSettings
      {
        get
        {
          if (this.m_settings == null)
            this.m_settings = new PdfPageSettings(40f);
          return this.m_settings;
        }
        set
        {
          this.m_settings = value != null ? value : throw new ArgumentNullException(nameof (PageSettings));
        }
      }

      internal static PrivateFontCollection PrivateFonts
      {
        get
        {
          if (PdfDocument.m_privateFonts == null)
            PdfDocument.m_privateFonts = new PrivateFontCollection();
          return PdfDocument.m_privateFonts;
        }
      }

      public PdfSectionCollection Sections => this.m_sections;

      public PdfDocumentTemplate Template
      {
        get
        {
          if (this.m_pageTemplate == null)
            this.m_pageTemplate = new PdfDocumentTemplate();
          return this.m_pageTemplate;
        }
        set => this.m_pageTemplate = value;
      }

      internal override bool WasEncrypted => this.m_bWasEncrypted;

      public delegate void ProgressEventHandler(object sender, ProgressEventArgs arguments);
    }
}
