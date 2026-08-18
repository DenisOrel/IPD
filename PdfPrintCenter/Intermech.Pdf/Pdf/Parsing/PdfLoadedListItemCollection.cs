// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Parsing.PdfLoadedListItemCollection
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System;

#nullable disable
namespace Syncfusion.Pdf.Parsing;

public class PdfLoadedListItemCollection : PdfCollection
{
  private PdfLoadedChoiceField m_field;

  internal PdfLoadedListItemCollection(PdfLoadedChoiceField field) => this.m_field = field;

  public int Add(PdfLoadedListItem item)
  {
    if (item == null)
      throw new ArgumentNullException(nameof (item));
    PdfArray items = this.GetItems();
    PdfArray array = this.GetArray(item);
    items.Add((IPdfPrimitive) array);
    this.m_field.Dictionary.SetProperty("Opt", (IPdfPrimitive) items);
    return this.List.Add((object) item);
  }

  internal int AddItem(PdfLoadedListItem item)
  {
    return item != null ? this.List.Add((object) item) : throw new ArgumentNullException(nameof (item));
  }

  public void Clear()
  {
    PdfArray items = this.GetItems();
    items.Clear();
    this.m_field.Dictionary.SetProperty("Opt", (IPdfPrimitive) items);
    this.List.Clear();
  }

  private PdfArray GetArray(PdfLoadedListItem item)
  {
    PdfArray array = new PdfArray();
    if (item.Value != string.Empty)
      array.Add((IPdfPrimitive) new PdfString(item.Value));
    if (item.Text != string.Empty)
      array.Add((IPdfPrimitive) new PdfString(item.Text));
    return array;
  }

  private PdfArray GetItems()
  {
    PdfArray items = new PdfArray();
    if (this.m_field.Dictionary.ContainsKey("Opt"))
      items = this.m_field.CrossTable.GetObject(this.m_field.Dictionary["Opt"]) as PdfArray;
    return items;
  }

  public void Insert(int index, PdfLoadedListItem item)
  {
    if (index < 0 || index > this.List.Count)
      throw new IndexOutOfRangeException(nameof (index));
    if (item == null)
      throw new ArgumentNullException(nameof (item));
    PdfArray items = this.GetItems();
    PdfArray array = this.GetArray(item);
    items.Insert(index, (IPdfPrimitive) array);
    this.m_field.Dictionary.SetProperty("Opt", (IPdfPrimitive) items);
    this.List.Insert(index, (object) item);
  }

  public void RemoveAt(int index)
  {
    if (index < 0 || index > this.List.Count)
      throw new IndexOutOfRangeException(nameof (index));
    PdfArray items = this.GetItems();
    items.RemoveAt(index);
    this.m_field.Dictionary.SetProperty("Opt", (IPdfPrimitive) items);
    this.List.RemoveAt(index);
  }

  public PdfLoadedListItem this[int index]
  {
    get
    {
      if (index < 0 || index >= this.List.Count)
        throw new IndexOutOfRangeException("Index");
      return this.List[index] as PdfLoadedListItem;
    }
  }
}
