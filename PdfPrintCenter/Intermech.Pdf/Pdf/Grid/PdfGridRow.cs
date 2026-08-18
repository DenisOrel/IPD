// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Grid.PdfGridRow
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using System;

#nullable disable
namespace Syncfusion.Pdf.Grid;

public class PdfGridRow
{
  private bool m_bColumnSpanExists;
  private bool m_bRowSpanExists;
  private PdfGridCellCollection m_cells;
  private PdfGrid m_grid;
  private PdfLayoutResult m_gridResult;
  private float m_height = float.MinValue;
  private float m_rowBreakHeight;
  private int m_rowOverflowIndex;
  private PdfGridRowStyle m_style;
  private float m_width = float.MinValue;

  public PdfGridRow(PdfGrid grid) => this.m_grid = grid;

  public void ApplyStyle(PdfGridCellStyle cellStyle)
  {
    foreach (PdfGridCell cell in this.Cells)
      cell.Style = cellStyle;
  }

  private float MeasureHeight()
  {
    float val1 = this.Cells[0].Height;
    foreach (PdfGridCell cell in this.Cells)
    {
      val1 = cell.ColumnSpan == 1 || cell.RowSpan == 1 ? Math.Max(val1, cell.Height) : Math.Min(val1, cell.Height);
      cell.Height = val1;
    }
    return val1;
  }

  private float MeasureWidth()
  {
    float num = 0.0f;
    foreach (PdfGridColumn column in this.Grid.Columns)
      num += column.Width;
    return num;
  }

  public PdfGridCellCollection Cells
  {
    get
    {
      if (this.m_cells == null)
        this.m_cells = new PdfGridCellCollection(this);
      return this.m_cells;
    }
  }

  internal bool ColumnSpanExists
  {
    get => this.m_bColumnSpanExists;
    set => this.m_bColumnSpanExists = value;
  }

  internal PdfGrid Grid
  {
    get => this.m_grid;
    set => this.m_grid = value;
  }

  public float Height
  {
    get
    {
      if ((double) this.m_height == -3.4028234663852886E+38)
        this.m_height = this.MeasureHeight();
      return this.m_height;
    }
    set => this.m_height = value;
  }

  internal PdfLayoutResult NestedGridLayoutResult
  {
    get => this.m_gridResult;
    set => this.m_gridResult = value;
  }

  internal float RowBreakHeight
  {
    get => this.m_rowBreakHeight;
    set => this.m_rowBreakHeight = value;
  }

  internal int RowIndex => this.Grid.Rows.IndexOf(this);

  internal int RowOverflowIndex
  {
    get => this.m_rowOverflowIndex;
    set => this.m_rowOverflowIndex = value;
  }

  internal bool RowSpanExists
  {
    get => this.m_bRowSpanExists;
    set => this.m_bRowSpanExists = value;
  }

  public PdfGridRowStyle Style
  {
    get
    {
      if (this.m_style == null)
        this.m_style = new PdfGridRowStyle();
      return this.m_style;
    }
    set => this.m_style = value;
  }

  internal float Width
  {
    get
    {
      if ((double) this.m_width == -3.4028234663852886E+38)
        this.m_width = this.MeasureWidth();
      return this.m_width;
    }
  }
}
