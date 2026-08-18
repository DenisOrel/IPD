// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Lists.PdfListItemCollection
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using System;


namespace Syncfusion.Pdf.Lists
{
    public class PdfListItemCollection : PdfCollection
    {
      public PdfListItemCollection()
      {
      }

      public PdfListItemCollection(string[] items)
        : this()
      {
        if (items == null)
          throw new ArgumentNullException(nameof (items));
        foreach (string text in items)
          this.Add(text);
      }

      public int Add(PdfListItem item)
      {
        return item != null ? this.List.Add((object) item) : throw new ArgumentNullException(nameof (item));
      }

      public PdfListItem Add(string text)
      {
        PdfListItem pdfListItem = text != null ? new PdfListItem(text) : throw new ArgumentNullException(nameof (text));
        this.List.Add((object) pdfListItem);
        return pdfListItem;
      }

      public int Add(PdfListItem item, float itemIndent)
      {
        item.TextIndent = itemIndent;
        return this.Add(item);
      }

      public PdfListItem Add(string text, PdfFont font)
      {
        if (text == null)
          throw new ArgumentNullException(nameof (text));
        PdfListItem pdfListItem = font != null ? new PdfListItem(text, font) : throw new ArgumentNullException(nameof (font));
        this.List.Add((object) pdfListItem);
        return pdfListItem;
      }

      public PdfListItem Add(string text, float itemIndent)
      {
        PdfListItem pdfListItem = this.Add(text);
        pdfListItem.TextIndent = itemIndent;
        return pdfListItem;
      }

      public PdfListItem Add(string text, PdfFont font, float itemIndent)
      {
        PdfListItem pdfListItem = this.Add(text, font);
        pdfListItem.TextIndent = itemIndent;
        return pdfListItem;
      }

      public void Clear() => this.List.Clear();

      public int IndexOf(PdfListItem item)
      {
        return item != null ? this.List.IndexOf((object) item) : throw new ArgumentNullException(nameof (item));
      }

      public void Insert(int index, PdfListItem item)
      {
        if (index < 0 || index >= this.Count)
          throw new ArgumentException("The index should be less than item's count or more or equal to 0", nameof (index));
        if (item == null)
          throw new ArgumentNullException(nameof (item));
        this.List.Insert(index, (object) item);
      }

      public void Insert(int index, PdfListItem item, float itemIndent)
      {
        item.TextIndent = itemIndent;
        this.List.Insert(index, (object) item);
      }

      public void Remove(PdfListItem item)
      {
        if (item == null)
          throw new ArgumentNullException(nameof (item));
        if (!this.List.Contains((object) item))
          throw new ArgumentException("The list doesn't contain this item", nameof (item));
        this.List.Remove((object) item);
      }

      public void RemoveAt(int index)
      {
        if (index < 0 || index >= this.Count)
          throw new ArgumentException("The index should be less than item's count or more or equal to 0", nameof (index));
        this.List.RemoveAt(index);
      }

      public PdfListItem this[int index]
      {
        get
        {
          return index >= 0 && index < this.Count ? (PdfListItem) this.List[index] : throw new IndexOutOfRangeException("The index should be less than item's count or more or equel to 0");
        }
      }
    }
}
