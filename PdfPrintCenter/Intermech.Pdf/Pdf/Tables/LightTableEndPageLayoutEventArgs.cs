// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Tables.LightTableEndPageLayoutEventArgs
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;

#nullable disable
namespace Syncfusion.Pdf.Tables;

public class LightTableEndPageLayoutEventArgs : EndPageLayoutEventArgs
{
  private int m_endRow;
  private int m_startRow;

  internal LightTableEndPageLayoutEventArgs(
    PdfLightTableLayoutResult result,
    int startRow,
    int endRow)
    : base((PdfLayoutResult) result)
  {
    this.m_startRow = startRow;
    this.m_endRow = endRow;
  }

  public int EndRowIndex => this.m_endRow;

  public int StartRowIndex => this.m_startRow;
}
