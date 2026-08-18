// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Parsing.PdfLoadedPageEnumerator
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Collections;

#nullable disable
namespace Syncfusion.Pdf.Parsing;

public class PdfLoadedPageEnumerator : IEnumerator
{
  private PdfLoadedPageCollection m_collection;
  private int m_index = -1;

  public PdfLoadedPageEnumerator(PdfLoadedPageCollection collection)
  {
    this.m_collection = collection != null ? collection : throw new ArgumentNullException(nameof (collection));
  }

  public bool MoveNext()
  {
    ++this.m_index;
    return this.m_index < this.m_collection.Count;
  }

  public void Reset() => this.m_index = -1;

  public object Current
  {
    get
    {
      if (this.m_index < 0 && this.m_index >= this.m_collection.Count)
        throw new InvalidOperationException("The index is out of range.");
      return (object) this.m_collection[this.m_index];
    }
  }
}
