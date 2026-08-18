// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Parsing.PdfLoadeddStateItemCollection
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;

#nullable disable
namespace Syncfusion.Pdf.Parsing;

public class PdfLoadeddStateItemCollection : PdfCollection
{
  internal void Add(PdfLoadedStateItem item)
  {
    if (item == null)
      throw new NullReferenceException(nameof (item));
    this.List.Add((object) item);
  }

  internal int IndexOf(PdfLoadedStateItem item) => this.List.IndexOf((object) item);

  public PdfLoadedStateItem this[int index]
  {
    get
    {
      return index >= 0 && index < this.Count ? (PdfLoadedStateItem) (this.List[index] as PdfLoadedCheckBoxItem) : throw new IndexOutOfRangeException(nameof (index));
    }
  }
}
