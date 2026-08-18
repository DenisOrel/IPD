// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Tables.CellLayoutEventArgs
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using System;
using System.Drawing;

#nullable disable
namespace Syncfusion.Pdf.Tables;

public abstract class CellLayoutEventArgs : EventArgs
{
  private RectangleF m_bounds;
  private int m_cellIndex;
  private PdfGraphics m_graphics;
  private int m_rowIndex;
  private string m_value;

  internal CellLayoutEventArgs(
    PdfGraphics graphics,
    int rowIndex,
    int cellInder,
    RectangleF bounds,
    string value)
  {
    if (graphics == null)
      throw new ArgumentNullException(nameof (graphics));
    this.m_rowIndex = rowIndex;
    this.m_cellIndex = cellInder;
    this.m_value = value;
    this.m_bounds = bounds;
    this.m_graphics = graphics;
  }

  public RectangleF Bounds => this.m_bounds;

  public int CellIndex => this.m_cellIndex;

  public PdfGraphics Graphics => this.m_graphics;

  public int RowIndex => this.m_rowIndex;

  public string Value => this.m_value;
}
