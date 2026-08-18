// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Tables.PdfRow
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

#nullable disable
namespace Syncfusion.Pdf.Tables;

public class PdfRow
{
  private object[] m_values;

  internal PdfRow()
  {
  }

  internal PdfRow(object[] values)
    : this()
  {
    this.m_values = values;
  }

  public object[] Values
  {
    get => this.m_values;
    set => this.m_values = value;
  }
}
