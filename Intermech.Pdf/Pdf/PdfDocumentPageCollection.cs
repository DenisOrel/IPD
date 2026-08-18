// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PdfDocumentPageCollection
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Primitives;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;


namespace Syncfusion.Pdf
{
    public class PdfDocumentPageCollection : IEnumerable
    {
      internal int count;
      private PdfDocument m_document;
      private Dictionary<PdfPage, int> m_pageCollectionIndex = new Dictionary<PdfPage, int>();

      public event PageAddedEventHandler PageAdded;

      internal PdfDocumentPageCollection(PdfDocument document)
      {
        this.m_document = document != null ? document : throw new ArgumentNullException(nameof (document));
      }

      public PdfPage Add()
      {
        PdfPage page = new PdfPage();
        this.Add(page);
        return page;
      }

      private void Add(PdfLoadedPage page) => throw new NotImplementedException();

      internal void Add(PdfPage page)
      {
        if (page == null)
          throw new ArgumentNullException(nameof (page));
        PdfSection pdfSection = this.GetLastSection();
        if (this.GetLastSection().PageSettings.Orientation != this.m_document.PageSettings.Orientation)
        {
          pdfSection = this.m_document.Sections.Add();
          pdfSection.PageSettings.Orientation = this.m_document.PageSettings.Orientation;
        }
        if (!this.m_pageCollectionIndex.ContainsKey(page))
          this.m_pageCollectionIndex.Add(page, this.count++);
        pdfSection.Add(page);
      }

      internal PdfPageBase Add(PdfLoadedDocument ldDoc, PdfPageBase page, List<PdfArray> destinations)
      {
        if (ldDoc == null)
          throw new ArgumentNullException(nameof (ldDoc));
        if (page == null)
          throw new ArgumentNullException(nameof (page));
        PdfSection pdfSection;
        if (this.CanPageFitLastSection(page))
        {
          pdfSection = this.GetLastSection();
        }
        else
        {
          pdfSection = this.m_document.Sections.Add();
          PdfPageSettings pageSettings = pdfSection.PageSettings;
          pageSettings.Size = page.Size;
          pageSettings.Orientation = page.Orientation;
          pageSettings.Rotate = page.Rotation;
          pageSettings.Margins.All = 0.0f;
          pageSettings.Origin = page.Origin;
        }
        PdfPage key = pdfSection.Add();
        this.m_pageCollectionIndex.Add(key, this.count++);
        if (page.Dictionary["CropBox"] is PdfArray primitive1)
          key.Dictionary.SetProperty("CropBox", (IPdfPrimitive) primitive1);
        SizeF size = key.Size;
        if (page.Dictionary.ContainsKey("MediaBox") && page.Dictionary["MediaBox"] is PdfArray primitive2)
        {
          key.Dictionary.SetProperty("MediaBox", (IPdfPrimitive) primitive2);
          SizeF sizeF = new SizeF((primitive2[2] as PdfNumber).FloatValue, (primitive2[3] as PdfNumber).FloatValue);
        }
        if (page.Contents.Count > 0)
        {
          foreach (IPdfPrimitive content in page.Contents)
          {
            IPdfPrimitive element = !this.m_document.EnableMemoryOptimization ? content : content.Clone(this.m_document.CrossTable);
            key.Contents.Add(element);
          }
          PdfResources res = !this.m_document.EnableMemoryOptimization ? page.GetResources() : new PdfResources(page.GetResources().Clone(this.m_document.CrossTable) as PdfDictionary);
          key.Dictionary["Resources"] = (IPdfPrimitive) res;
          key.SetResources(res);
        }
        if (!this.m_document.EnableMemoryOptimization)
          key.ImportAnnotations(ldDoc, page, destinations);
        return (PdfPageBase) key;
      }

      private bool CanPageFitLastSection(PdfPageBase page) => false;

      internal void Clear()
      {
        foreach (PdfPage pdfPage in this)
        {
          this.Remove(pdfPage);
          this.m_pageCollectionIndex.Remove(pdfPage);
          pdfPage.Clear();
        }
        this.m_pageCollectionIndex.Clear();
        this.m_pageCollectionIndex = (Dictionary<PdfPage, int>) null;
        this.m_document = (PdfDocument) null;
      }

      private int CountPages()
      {
        PdfSectionCollection sections = this.m_document.Sections;
        int num = 0;
        foreach (PdfSection pdfSection in sections)
          num += pdfSection.Count;
        return num;
      }

      public IEnumerator GetEnumerator()
      {
        return (IEnumerator) new PdfDocumentPageCollection.PdfPageEnumerator(this);
      }

      private PdfSection GetLastSection()
      {
        PdfSectionCollection sections = this.m_document.Sections;
        if (sections.Count == 0)
          sections.Add();
        return sections[sections.Count - 1];
      }

      private PdfPage GetPageByIndex(int index)
      {
        if (index < 0 || index >= this.Count)
          throw new ArgumentOutOfRangeException(nameof (index), "Value can not be less 0, equal or more than number of pages in the document.");
        int num = 0;
        int index1 = 0;
        for (int count1 = this.m_document.Sections.Count; index1 < count1; ++index1)
        {
          PdfSection section = this.m_document.Sections[index1];
          int count2 = section.Count;
          int index2 = index - num;
          if (index >= num && index2 < count2)
            return section[index2];
          num += count2;
        }
        return (PdfPage) null;
      }

      public int IndexOf(PdfPage page)
      {
        if (page == null)
          throw new ArgumentNullException(nameof (page));
        int num1 = -1;
        int num2 = 0;
        int index = 0;
        for (int count = this.m_document.Sections.Count; index < count; ++index)
        {
          PdfSection section = this.m_document.Sections[index];
          num1 = section.IndexOf(page);
          if (num1 >= 0)
            return num1 + num2;
          num2 += section.Count;
        }
        return num1;
      }

      public void Insert(int index, PdfPage page)
      {
        if (page == null)
          throw new ArgumentNullException(nameof (page));
        if (index < 0)
          throw new ArgumentOutOfRangeException(nameof (index));
        if (index > this.Count)
          throw new ArgumentOutOfRangeException(nameof (index), "Value can not be less 0, equal or more than number of pages in the document.");
        if (index == this.Count)
        {
          this.GetLastSection().Add(page);
        }
        else
        {
          int num = 0;
          int index1 = 0;
          for (int count = this.m_document.Sections.Count; index1 < count; ++index1)
          {
            PdfSection section = this.m_document.Sections[index1];
            for (int index2 = 0; index2 < section.Pages.Count; ++index2)
            {
              if (num == index)
              {
                section.Insert(index2, page);
                return;
              }
              ++num;
            }
          }
        }
      }

      internal void OnPageAdded(PageAddedEventArgs args)
      {
        if (this.PageAdded == null)
          return;
        this.PageAdded((object) this, args);
      }

      internal PdfSection Remove(PdfPage page)
      {
        if (page == null)
          throw new ArgumentNullException(nameof (page));
        PdfSection pdfSection = (PdfSection) null;
        int index = 0;
        for (int count = this.m_document.Sections.Count; index < count; ++index)
        {
          pdfSection = this.m_document.Sections[index];
          if (pdfSection.Pages.Contains(page))
          {
            pdfSection.Pages.Remove(page);
            return pdfSection;
          }
        }
        return pdfSection;
      }

      public int Count => this.CountPages();

      public PdfPage this[int index] => this.GetPageByIndex(index);

      internal Dictionary<PdfPage, int> PageCollectionIndex => this.m_pageCollectionIndex;

      private struct PdfPageEnumerator : IEnumerator
      {
        private PdfDocumentPageCollection m_pageCollection;
        private int m_currentIndex;

        internal PdfPageEnumerator(PdfDocumentPageCollection pageCollection)
        {
          this.m_pageCollection = pageCollection != null ? pageCollection : throw new ArgumentNullException(nameof (pageCollection));
          this.m_currentIndex = -1;
        }

        public object Current
        {
          get
          {
            this.CheckIndex();
            return (object) this.m_pageCollection[this.m_currentIndex];
          }
        }

        public bool MoveNext()
        {
          ++this.m_currentIndex;
          return this.m_currentIndex < this.m_pageCollection.Count;
        }

        public void Reset() => this.m_currentIndex = -1;

        private void CheckIndex()
        {
          if (this.m_currentIndex < 0 || this.m_currentIndex >= this.m_pageCollection.Count)
            throw new IndexOutOfRangeException();
        }
      }
    }
}
