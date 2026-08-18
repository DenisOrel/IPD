// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PdfSectionCollection
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Primitives;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;


namespace Syncfusion.Pdf
{
    public class PdfSectionCollection : IPdfWrapper, IEnumerable
    {
      private PdfNumber m_count;
      private PdfDocument m_document;
      private PdfDictionary m_pages;
      private PdfArray m_sectionCollection;
      private List<PdfSection> m_sections = new List<PdfSection>();
      internal const int RotateFactor = 90;

      internal PdfSectionCollection(PdfDocument document)
      {
        this.m_document = document != null ? document : throw new ArgumentNullException(nameof (document));
        this.Initialize();
      }

      public PdfSection Add()
      {
        PdfSection section = new PdfSection(this.m_document);
        this.Add(section);
        return section;
      }

      private int Add(PdfSection section)
      {
        PdfReferenceHolder element = section != null ? this.CheckSection(section) : throw new ArgumentNullException(nameof (section));
        this.m_sections.Add(section);
        section.Parent = this;
        this.m_sectionCollection.Add((IPdfPrimitive) element);
        return this.m_sections.IndexOf(section);
      }

      private void BeginSave(object sender, SavePdfPrimitiveEventArgs e)
      {
        this.m_count.IntValue = this.CountPages();
        this.SetPageSettings(this.m_pages, this.m_document.PageSettings);
      }

      private PdfReferenceHolder CheckSection(PdfSection section)
      {
        PdfReferenceHolder element = new PdfReferenceHolder((IPdfWrapper) section);
        return !this.m_sectionCollection.Contains((IPdfPrimitive) element) ? element : throw new ArgumentException("The object can't be added twice to the collection.", nameof (section));
      }

      internal void Clear()
      {
        foreach (PdfSection pdfSection in this)
          pdfSection.Clear();
        if (this.m_pages != null)
          this.m_pages.Clear();
        if (this.m_sectionCollection != null)
          this.m_sectionCollection.Clear();
        if (this.m_sections != null)
          this.m_sections.Clear();
        this.m_pages = (PdfDictionary) null;
        this.m_sectionCollection = (PdfArray) null;
        this.m_sections = (List<PdfSection>) null;
        this.m_document = (PdfDocument) null;
      }

      public bool Contains(PdfSection section)
      {
        if (section == null)
          throw new ArgumentNullException(nameof (section));
        return this.IndexOf(section) >= 0;
      }

      private int CountPages()
      {
        int num = 0;
        foreach (PdfSection pdfSection in this)
          num += pdfSection.Count;
        return num;
      }

      public IEnumerator GetEnumerator()
      {
        return (IEnumerator) new PdfSectionCollection.PdfSectionEnumerator(this);
      }

      public int IndexOf(PdfSection section)
      {
        return this.m_sectionCollection.IndexOf((IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) section));
      }

      private void Initialize()
      {
        this.m_count = new PdfNumber(0);
        this.m_sectionCollection = new PdfArray();
        this.m_pages = new PdfDictionary();
        this.m_pages.BeginSave += new SavePdfPrimitiveEventHandler(this.BeginSave);
        this.m_pages["Type"] = (IPdfPrimitive) new PdfName("Pages");
        this.m_pages["Kids"] = (IPdfPrimitive) this.m_sectionCollection;
        this.m_pages["Count"] = (IPdfPrimitive) this.m_count;
        this.m_pages["Resources"] = (IPdfPrimitive) new PdfDictionary();
        this.SetPageSettings(this.m_pages, this.m_document.PageSettings);
      }

      public void Insert(int index, PdfSection section)
      {
        if (index < 0 || index >= this.Count)
          throw new IndexOutOfRangeException();
        PdfReferenceHolder element = this.CheckSection(section);
        this.m_sectionCollection.Insert(index, (IPdfPrimitive) element);
      }

      internal void OnPageSaving(PdfPage page) => this.Document.OnPageSave(page);

      internal void PageLabelsSet() => this.Document.PageLabelsSet();

      internal void ResetProgress()
      {
        foreach (PdfSection pdfSection in this)
          pdfSection.ResetProgress();
      }

      private void SetPageSettings(PdfDictionary container, PdfPageSettings pageSettings)
      {
        if (container == null)
          throw new ArgumentNullException(nameof (container));
        if (pageSettings == null)
          throw new ArgumentNullException(nameof (pageSettings));
        RectangleF rectangle = new RectangleF(PointF.Empty, pageSettings.Size);
        if (PdfDocument.ConformanceLevel != PdfConformanceLevel.Pdf_X1A2001)
          container["MediaBox"] = (IPdfPrimitive) PdfArray.FromRectangle(rectangle);
        int rotate = (int) pageSettings.Rotate;
        if (pageSettings.Unit == PdfGraphicsUnit.Point)
          return;
        float num = new PdfUnitConvertor().ConvertUnits(1f, pageSettings.Unit, PdfGraphicsUnit.Point);
        container["UserUnit"] = (IPdfPrimitive) new PdfNumber(num);
      }

      internal void SetProgress()
      {
        foreach (PdfSection pdfSection in this)
          pdfSection.SetProgress();
      }

      public int Count => this.m_sections.Count;

      internal PdfDocument Document => this.m_document;

      public PdfSection this[int index]
      {
        get
        {
          return index >= 0 && index < this.Count ? this.m_sections[index] : throw new IndexOutOfRangeException();
        }
      }

      IPdfPrimitive IPdfWrapper.Element => (IPdfPrimitive) this.m_pages;

      private struct PdfSectionEnumerator : IEnumerator
      {
        private PdfSectionCollection m_sectionCollection;
        private int m_currentIndex;

        internal PdfSectionEnumerator(PdfSectionCollection sectionCollection)
        {
          this.m_sectionCollection = sectionCollection != null ? sectionCollection : throw new ArgumentNullException(nameof (sectionCollection));
          this.m_currentIndex = -1;
        }

        public object Current
        {
          get
          {
            this.CheckIndex();
            return (object) this.m_sectionCollection[this.m_currentIndex];
          }
        }

        public bool MoveNext()
        {
          ++this.m_currentIndex;
          return this.m_currentIndex < this.m_sectionCollection.Count;
        }

        public void Reset() => this.m_currentIndex = -1;

        private void CheckIndex()
        {
          if (this.m_currentIndex < 0 || this.m_currentIndex >= this.m_sectionCollection.Count)
            throw new IndexOutOfRangeException();
        }
      }
    }
}
