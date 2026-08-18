// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfListFieldItemCollection
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System;

#nullable disable
namespace Syncfusion.Pdf.Interactive;

public class PdfListFieldItemCollection : PdfCollection, IPdfWrapper
{
  private PdfArray m_items = new PdfArray();

  public int Add(PdfListFieldItem item)
  {
    return item != null ? this.DoAdd(item) : throw new ArgumentNullException(nameof (item));
  }

  public void Clear()
  {
    this.m_items.Clear();
    this.List.Clear();
  }

  public bool Contains(PdfListFieldItem item) => this.List.Contains((object) item);

  private int DoAdd(PdfListFieldItem item)
  {
    this.m_items.Add(((IPdfWrapper) item).Element);
    return this.List.Add((object) item);
  }

  private void DoInsert(int index, PdfListFieldItem item)
  {
    this.m_items.Insert(index, ((IPdfWrapper) item).Element);
    this.List.Insert(index, (object) item);
  }

  private void DoRemove(PdfListFieldItem item)
  {
    int index = this.List.IndexOf((object) item);
    this.m_items.RemoveAt(index);
    this.List.RemoveAt(index);
  }

  private void DoRemoveAt(int index)
  {
    this.m_items.RemoveAt(index);
    this.List.RemoveAt(index);
  }

  public int IndexOf(PdfListFieldItem item) => this.List.IndexOf((object) item);

  public void Insert(int index, PdfListFieldItem item)
  {
    if (item == null)
      throw new ArgumentNullException(nameof (item));
    this.DoInsert(index, item);
  }

  public void Remove(PdfListFieldItem item)
  {
    if (item == null)
      throw new ArgumentNullException(nameof (item));
    if (!this.List.Contains((object) item))
      return;
    this.DoRemove(item);
  }

  public void RemoveAt(int index)
  {
    if (index < 0 || index >= this.List.Count)
      throw new ArgumentNullException(nameof (index));
    this.DoRemoveAt(index);
  }

  public PdfListFieldItem this[int index] => (PdfListFieldItem) this.List[index];

  IPdfPrimitive IPdfWrapper.Element => (IPdfPrimitive) this.m_items;
}
