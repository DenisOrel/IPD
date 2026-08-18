// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PdfSectionPageCollection
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Collections;

#nullable disable
namespace Syncfusion.Pdf;

public class PdfSectionPageCollection : IEnumerable
{
  private PdfSection m_section;

  private PdfSectionPageCollection()
  {
  }

  internal PdfSectionPageCollection(PdfSection section)
  {
    this.m_section = section != null ? section : throw new ArgumentNullException(nameof (section));
  }

  public PdfPage Add() => this.m_section.Add();

  public void Add(PdfPage page)
  {
    if (page == null)
      throw new ArgumentNullException(nameof (page));
    this.m_section.Add(page);
  }

  public void Clear() => this.m_section = (PdfSection) null;

  public bool Contains(PdfPage page)
  {
    return page != null ? this.m_section.Contains(page) : throw new ArgumentNullException(nameof (page));
  }

  public int IndexOf(PdfPage page)
  {
    return page != null ? this.m_section.IndexOf(page) : throw new ArgumentNullException(nameof (page));
  }

  public void Insert(int index, PdfPage page)
  {
    if (page == null)
      throw new ArgumentNullException(nameof (page));
    if (index < 0 && index > this.Count)
      throw new ArgumentOutOfRangeException(nameof (index));
    this.m_section.Insert(index, page);
  }

  public void Remove(PdfPage page)
  {
    if (page == null)
      throw new ArgumentNullException(nameof (page));
    this.m_section.Remove(page);
  }

  public void RemoveAt(int index)
  {
    if (index < 0 && index > this.Count)
      throw new ArgumentOutOfRangeException(nameof (index));
    this.m_section.RemoveAt(index);
  }

  IEnumerator IEnumerable.GetEnumerator() => this.m_section.GetEnumerator();

  public int Count => this.m_section.Count;

  public PdfPage this[int index]
  {
    get
    {
      return index >= 0 || index <= this.Count ? this.m_section[index] : throw new ArgumentOutOfRangeException(nameof (index));
    }
  }
}
