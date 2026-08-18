// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Parsing.PdfLoadedRadioButtonItemCollection
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

#nullable disable
namespace Syncfusion.Pdf.Parsing;

public class PdfLoadedRadioButtonItemCollection : PdfLoadedStateItemCollection
{
  internal void Add(PdfLoadedRadioButtonItem item) => this.Add((PdfLoadedStateItem) item);

  internal int IndexOf(PdfLoadedRadioButtonItem item) => this.IndexOf((PdfLoadedStateItem) item);

  public PdfLoadedRadioButtonItem this[int index] => base[index] as PdfLoadedRadioButtonItem;
}
