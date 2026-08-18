// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PdfCollection
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Collections;

#nullable disable
namespace Syncfusion.Pdf;

public class PdfCollection : IEnumerable
{
  private System.Collections.Generic.List<object> m_list = new System.Collections.Generic.List<object>();

  internal void CopyTo(IPdfWrapper[] array, int index)
  {
    if (array == null)
      throw new ArgumentNullException(nameof (array));
    if (index < 0)
      throw new ArgumentOutOfRangeException(nameof (index));
    this.m_list.CopyTo((object[]) array, index);
  }

  public IEnumerator GetEnumerator() => (IEnumerator) this.m_list.GetEnumerator();

  public int Count => this.m_list.Count;

  protected IList List => (IList) this.m_list;
}
