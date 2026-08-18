// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Parsing.PdfLoadedListFieldItemCollection
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;

#nullable disable
namespace Syncfusion.Pdf.Parsing;

public class PdfLoadedListFieldItemCollection : PdfCollection
{
  internal void Add(PdfLoadedListFieldItem item)
  {
    if (item == null)
      throw new NullReferenceException(nameof (item));
    this.List.Add((object) item);
  }

  public PdfLoadedListFieldItem this[int index]
  {
    get
    {
      return index >= 0 && index < this.Count ? this.List[index] as PdfLoadedListFieldItem : throw new IndexOutOfRangeException(nameof (index));
    }
  }
}
