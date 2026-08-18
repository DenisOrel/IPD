// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Tables.PdfLightTableLayoutResult
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using System.Drawing;


namespace Syncfusion.Pdf.Tables
{
    public class PdfLightTableLayoutResult : PdfLayoutResult
    {
      private PdfStringLayoutResult[] m_cellResults;
      private int m_rowIndex;

      internal PdfLightTableLayoutResult(
        PdfPage page,
        RectangleF bounds,
        int rowIndex,
        PdfStringLayoutResult[] cellResults)
        : base(page, bounds)
      {
        this.m_rowIndex = rowIndex;
        this.m_cellResults = cellResults;
      }

      internal PdfStringLayoutResult[] CellResults => this.m_cellResults;

      public int LastRowIndex => this.m_rowIndex;
    }
}
