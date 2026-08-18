// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PdfFileStructure
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;


namespace Syncfusion.Pdf
{
    public class PdfFileStructure
    {
      private PdfCrossReferenceType m_crossReferenceType = PdfCrossReferenceType.CrossReferenceStream;
      private PdfFileFormat m_fileformat;
      private bool m_incrementalUpdate = true;
      private bool m_taggedPdf;
      private PdfVersion m_version = PdfVersion.Version1_5;

      internal event EventHandler TaggedPdfChanged;

      protected void OnTaggedPdfChanged(EventArgs e)
      {
        EventHandler taggedPdfChanged = this.TaggedPdfChanged;
        if (taggedPdfChanged == null)
          return;
        taggedPdfChanged((object) this, e);
      }

      public PdfCrossReferenceType CrossReferenceType
      {
        get => this.m_crossReferenceType;
        set => this.m_crossReferenceType = value;
      }

      internal PdfFileFormat FileFormat
      {
        get => this.m_fileformat;
        set => this.m_fileformat = value;
      }

      public bool IncrementalUpdate
      {
        get => this.m_incrementalUpdate;
        set => this.m_incrementalUpdate = value;
      }

      public bool TaggedPdf
      {
        get => this.m_taggedPdf;
        internal set
        {
          if (this.m_taggedPdf != value)
            this.m_taggedPdf = value;
          this.OnTaggedPdfChanged(new EventArgs());
        }
      }

      public PdfVersion Version
      {
        get => this.m_version;
        set
        {
          this.m_version = value;
          if (this.m_version > PdfVersion.Version1_3)
            return;
          this.m_crossReferenceType = PdfCrossReferenceType.CrossReferenceTable;
        }
      }
    }
}
