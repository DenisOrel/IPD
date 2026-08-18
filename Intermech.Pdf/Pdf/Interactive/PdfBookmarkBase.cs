// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfBookmarkBase
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Primitives;
using System;
using System.Collections;


namespace Syncfusion.Pdf.Interactive
{
    public class PdfBookmarkBase : IPdfWrapper, IEnumerable
    {
      private System.Collections.Generic.List<PdfBookmark> bookmark;
      private System.Collections.Generic.List<PdfBookmarkBase> m_booklist;
      private PdfCrossTable m_crossTable;
      private PdfDictionary m_dictionary;
      private bool m_isExpanded;
      private System.Collections.Generic.List<PdfBookmarkBase> m_list;

      internal PdfBookmarkBase()
      {
        this.m_list = new System.Collections.Generic.List<PdfBookmarkBase>();
        this.m_dictionary = new PdfDictionary();
        this.m_crossTable = new PdfCrossTable();
      }

      internal PdfBookmarkBase(PdfDictionary dictionary, PdfCrossTable crossTable)
      {
        this.m_list = new System.Collections.Generic.List<PdfBookmarkBase>();
        this.m_dictionary = new PdfDictionary();
        this.m_crossTable = new PdfCrossTable();
        this.m_dictionary = dictionary;
        if (crossTable == null)
          return;
        this.m_crossTable = crossTable;
      }

      public PdfBookmark Add(string title)
      {
        if (title == null)
          throw new ArgumentNullException(nameof (title));
        PdfBookmark previous = this.Count < 1 ? (PdfBookmark) null : this[this.Count - 1];
        PdfBookmark pdfBookmark = new PdfBookmark(title, this, previous, (PdfBookmark) null);
        if (previous != null)
          previous.Next = pdfBookmark;
        this.List.Add((PdfBookmarkBase) pdfBookmark);
        this.UpdateFields();
        return pdfBookmark;
      }

      public void Clear()
      {
        this.List.Clear();
        if (this.m_booklist == null)
          return;
        this.m_booklist.Clear();
      }

      public bool Contains(PdfBookmark outline) => this.List.Contains((PdfBookmarkBase) outline);

      private void GetBookmarkCollection(System.Collections.Generic.List<PdfBookmark> pageBookmarks, System.Collections.Generic.List<PdfBookmark> bookmarks)
      {
        if (pageBookmarks == null)
          return;
        foreach (object pageBookmark in pageBookmarks)
          bookmarks.Add(pageBookmark as PdfBookmark);
      }

      private PdfLoadedBookmark GetFirstBookMark(PdfBookmarkBase bookmark)
      {
        PdfLoadedBookmark firstBookMark = (PdfLoadedBookmark) null;
        PdfDictionary dictionary = bookmark.Dictionary;
        if (dictionary.ContainsKey("First"))
          firstBookMark = new PdfLoadedBookmark(this.CrossTable.GetObject(dictionary["First"]) as PdfDictionary, this.CrossTable);
        return firstBookMark;
      }

      public PdfBookmark Insert(int index, string title)
      {
        if (title == null)
          throw new ArgumentNullException(nameof (title));
        if (index < 0 || index > this.Count)
          throw new IndexOutOfRangeException();
        if (title == null)
          throw new ArgumentNullException(nameof (title));
        if (index == this.Count)
          return this.Add(title);
        PdfBookmark next = this[index];
        PdfBookmark previous = index == 0 ? (PdfBookmark) null : this[index - 1];
        PdfBookmark pdfBookmark = new PdfBookmark(title, this, previous, next);
        this.List.Insert(index, (PdfBookmarkBase) pdfBookmark);
        if (previous != null)
          previous.Next = pdfBookmark;
        next.Previous = pdfBookmark;
        this.UpdateFields();
        return pdfBookmark;
      }

      public void Remove(string title)
      {
        if (title == null)
          throw new ArgumentNullException(nameof (title));
        int index1 = -1;
        if (this.bookmark == null)
        {
          this.bookmark = new System.Collections.Generic.List<PdfBookmark>();
          if (this.m_crossTable.Document is PdfLoadedDocument)
            (this.m_crossTable.Document as PdfLoadedDocument).CreateBookmarkDestinationDictionary();
          for (int index2 = 0; index2 < this.List.Count; ++index2)
          {
            if (!(this.List[index2] is PdfBookmark))
              throw new Exception("bookmark");
            this.bookmark.Add(this.List[index2] as PdfBookmark);
            if (this.List[index2].List.Count != 0)
            {
              for (int index3 = 0; index3 < this.List[index2].List.Count; ++index3)
                this.bookmark.Add(this.List[index2].List[index3] as PdfBookmark);
            }
          }
          if (this.m_booklist == null)
          {
            this.m_booklist = new System.Collections.Generic.List<PdfBookmarkBase>();
            for (int index4 = 0; index4 < this.bookmark.Count; ++index4)
              this.m_booklist.Add((PdfBookmarkBase) this.bookmark[index4]);
          }
        }
        for (int index5 = 0; index5 < this.bookmark.Count; ++index5)
        {
          if (this.bookmark[index5] is PdfLoadedBookmark)
          {
            if ((this.bookmark[index5] as PdfLoadedBookmark).Title.Equals(title))
            {
              index1 = index5;
              break;
            }
          }
          else if (this.bookmark[index5] != null && this.bookmark[index5].Title.Equals(title))
          {
            index1 = index5;
            break;
          }
        }
        this.RemoveAt(index1);
      }

      public void RemoveAt(int index)
      {
        if (this.bookmark == null)
        {
          this.bookmark = new System.Collections.Generic.List<PdfBookmark>();
          (this.m_crossTable.Document as PdfLoadedDocument).CreateBookmarkDestinationDictionary();
          for (int index1 = 0; index1 < this.List.Count; ++index1)
          {
            if (!(this.List[index1] is PdfBookmark))
              throw new Exception("bookmark");
            this.bookmark.Add(this.List[index1] as PdfBookmark);
          }
          if (this.m_booklist == null)
          {
            this.m_booklist = new System.Collections.Generic.List<PdfBookmarkBase>();
            for (int index2 = 0; index2 < this.bookmark.Count; ++index2)
              this.m_booklist.Add((PdfBookmarkBase) this.bookmark[index2]);
          }
        }
        if (index < 0 || index >= this.bookmark.Count)
          throw new ArgumentOutOfRangeException();
        if (index >= this.List.Count && index >= this.bookmark.Count)
          throw new ArgumentOutOfRangeException();
        if (this.bookmark[index] != null)
        {
          PdfBookmark pdfBookmark = this.bookmark[index];
          if (index == 0)
          {
            if (pdfBookmark.Dictionary.ContainsKey("Next"))
              this.m_dictionary.SetProperty("First", pdfBookmark.Dictionary["Next"]);
            else if (!pdfBookmark.Dictionary.ContainsKey("Prev"))
            {
              if (this.List.Count > 1)
              {
                this.m_dictionary.SetProperty("First", pdfBookmark.Dictionary["First"]);
              }
              else
              {
                this.m_dictionary.Remove("First");
                this.m_dictionary.Remove("Last");
              }
            }
            else
              this.m_dictionary.SetProperty("First", pdfBookmark.Dictionary["Next"]);
          }
          else if (pdfBookmark.Parent != null && pdfBookmark.Previous == null && pdfBookmark.Next != null)
          {
            pdfBookmark.Parent.Dictionary.SetProperty("First", pdfBookmark.Dictionary["Next"]);
            pdfBookmark.Next.Dictionary.Remove("Prev");
          }
          else if (pdfBookmark.Parent != null && pdfBookmark.Previous != null && pdfBookmark.Next != null)
          {
            pdfBookmark.Previous.Dictionary.SetProperty("Next", pdfBookmark.Dictionary["Next"]);
            PdfReferenceHolder pointer = pdfBookmark.Dictionary["Next"] as PdfReferenceHolder;
            if (pointer != (PdfReferenceHolder) null)
              (this.m_crossTable.GetObject((IPdfPrimitive) pointer) as PdfDictionary).SetProperty("Prev", pdfBookmark.Dictionary["Prev"]);
          }
          else if (pdfBookmark.Parent != null && pdfBookmark.Previous != null && pdfBookmark.Next == null)
          {
            pdfBookmark.Previous.Dictionary.Remove("Next");
            pdfBookmark.Parent.Dictionary.SetProperty("Last", pdfBookmark.Dictionary["Prev"]);
          }
          else
            pdfBookmark.Parent.Dictionary.Remove("First");
        }
        else if (this.bookmark[index] is PdfLoadedBookmark)
        {
          PdfLoadedBookmark pdfLoadedBookmark = this.bookmark[index] as PdfLoadedBookmark;
          if (index == 0)
          {
            if (pdfLoadedBookmark.Dictionary.ContainsKey("Next"))
              this.m_dictionary.SetProperty("First", pdfLoadedBookmark.Dictionary["Next"]);
            else if (!pdfLoadedBookmark.Dictionary.ContainsKey("Prev"))
            {
              if (this.List.Count > 1)
              {
                this.m_dictionary.SetProperty("First", pdfLoadedBookmark.Dictionary["First"]);
              }
              else
              {
                this.m_dictionary.Remove("First");
                this.m_dictionary.Remove("Last");
              }
            }
            else
              this.m_dictionary.SetProperty("First", pdfLoadedBookmark.Dictionary["Next"]);
          }
          else if (pdfLoadedBookmark.Parent != null && pdfLoadedBookmark.Previous == null && pdfLoadedBookmark.Next != null)
          {
            pdfLoadedBookmark.Parent.Dictionary.SetProperty("First", pdfLoadedBookmark.Dictionary["Next"]);
            pdfLoadedBookmark.Next.Dictionary.Remove("Prev");
          }
          else if (pdfLoadedBookmark.Parent != null && pdfLoadedBookmark.Previous != null && pdfLoadedBookmark.Next != null)
          {
            pdfLoadedBookmark.Previous.Dictionary.SetProperty("Next", pdfLoadedBookmark.Dictionary["Next"]);
            PdfReferenceHolder pointer = pdfLoadedBookmark.Dictionary["Next"] as PdfReferenceHolder;
            if (pointer != (PdfReferenceHolder) null)
              (this.m_crossTable.GetObject((IPdfPrimitive) pointer) as PdfDictionary).SetProperty("Prev", pdfLoadedBookmark.Dictionary["Prev"]);
          }
          else if (pdfLoadedBookmark.Parent != null && pdfLoadedBookmark.Previous != null && pdfLoadedBookmark.Next == null)
          {
            pdfLoadedBookmark.Previous.Dictionary.Remove("Next");
            pdfLoadedBookmark.Parent.Dictionary.SetProperty("Last", pdfLoadedBookmark.Dictionary["Prev"]);
          }
          else
            pdfLoadedBookmark.Parent.Dictionary.Remove("First");
        }
        this.m_list.RemoveAt(index);
        this.bookmark.RemoveAt(index);
        this.m_booklist.RemoveAt(index);
      }

      internal void ReproduceTree()
      {
        PdfLoadedBookmark pdfLoadedBookmark = this.GetFirstBookMark(this);
        for (bool flag = pdfLoadedBookmark != null; flag && pdfLoadedBookmark.m_dictionary != null; flag = pdfLoadedBookmark != null)
        {
          pdfLoadedBookmark.SetParent(this);
          this.m_list.Add((PdfBookmarkBase) pdfLoadedBookmark);
          pdfLoadedBookmark = pdfLoadedBookmark.Next as PdfLoadedBookmark;
        }
      }

      IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.List.GetEnumerator();

      private void UpdateFields()
      {
        if (this.Count > 0)
        {
          this.m_dictionary.SetNumber("Count", !this.IsExpanded ? -this.List.Count : this.List.Count);
          this.m_dictionary.SetProperty("First", (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) this[0]));
          this.m_dictionary.SetProperty("Last", (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) this[this.Count - 1]));
        }
        else
          this.m_dictionary.Clear();
        this.m_dictionary.Modify();
      }

      public int Count
      {
        get
        {
          if (this.m_crossTable.Document is PdfLoadedDocument && this.m_booklist == null)
          {
            this.m_booklist = new System.Collections.Generic.List<PdfBookmarkBase>();
            for (int index = 0; index < this.List.Count; ++index)
              this.m_booklist.Add(this.List[index]);
          }
          return this.List.Count;
        }
      }

      internal PdfCrossTable CrossTable => this.m_crossTable;

      internal PdfDictionary Dictionary => this.m_dictionary;

      internal bool IsExpanded
      {
        get
        {
          if (!this.Dictionary.ContainsKey("Count"))
            return this.m_isExpanded;
          return (this.Dictionary["Count"] as PdfNumber).IntValue >= 0;
        }
        set
        {
          this.m_isExpanded = value;
          if (this.Count <= 0)
            return;
          this.m_dictionary.SetNumber("Count", !this.m_isExpanded ? -this.List.Count : this.List.Count);
        }
      }

      public PdfBookmark this[int index] => this.List[index] as PdfBookmark;

      internal virtual System.Collections.Generic.List<PdfBookmarkBase> List => this.m_list;

      IPdfPrimitive IPdfWrapper.Element => (IPdfPrimitive) this.m_dictionary;
    }
}
