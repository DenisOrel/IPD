// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Tables.PdfRowCollection
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

#nullable disable
namespace Syncfusion.Pdf.Tables;

public class PdfRowCollection : PdfCollection
{
  internal PdfRowCollection()
  {
  }

  public void Add(PdfRow row) => this.List.Add((object) row);

  public void Add(object[] values) => this.List.Add((object) new PdfRow(values));

  public PdfRow this[int index] => this.List[index] as PdfRow;
}
