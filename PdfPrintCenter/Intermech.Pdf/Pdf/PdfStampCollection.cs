// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PdfStampCollection
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Collections;

#nullable disable
namespace Syncfusion.Pdf;

public class PdfStampCollection : PdfCollection
{
  public int Add(PdfPageTemplateElement template)
  {
    return template != null ? this.List.Add((object) template) : throw new ArgumentNullException(nameof (template));
  }

  public PdfPageTemplateElement Add(float x, float y, float width, float height)
  {
    PdfPageTemplateElement template = new PdfPageTemplateElement(x, y, width, height);
    this.Add(template);
    return template;
  }

  public void Clear() => this.List.Clear();

  public bool Contains(PdfPageTemplateElement template)
  {
    return template != null ? this.List.Contains((object) template) : throw new ArgumentNullException(nameof (template));
  }

  public new IEnumerator GetEnumerator()
  {
    return (IEnumerator) new PdfStampCollection.PdfPageTemplateEnumerator(this);
  }

  public void Insert(int index, PdfPageTemplateElement template)
  {
    if (template == null)
      throw new ArgumentNullException(nameof (template));
    this.List.Insert(index, (object) template);
  }

  public void Remove(PdfPageTemplateElement template)
  {
    if (template == null)
      throw new ArgumentNullException(nameof (template));
    this.List.Remove((object) template);
  }

  public void RemoveAt(int index) => this.List.RemoveAt(index);

  public PdfPageTemplateElement this[int index] => this.List[index] as PdfPageTemplateElement;

  private struct PdfPageTemplateEnumerator : IEnumerator
  {
    private PdfStampCollection m_stamps;
    private int m_currentIndex;

    internal PdfPageTemplateEnumerator(PdfStampCollection stamps)
    {
      this.m_stamps = stamps != null ? stamps : throw new ArgumentNullException(nameof (stamps));
      this.m_currentIndex = -1;
    }

    public object Current
    {
      get
      {
        this.CheckIndex();
        return (object) this.m_stamps[this.m_currentIndex];
      }
    }

    public bool MoveNext()
    {
      ++this.m_currentIndex;
      return this.m_currentIndex < this.m_stamps.Count;
    }

    public void Reset() => this.m_currentIndex = -1;

    private void CheckIndex()
    {
      if (this.m_currentIndex < 0 || this.m_currentIndex >= this.m_stamps.Count)
        throw new IndexOutOfRangeException();
    }
  }
}
