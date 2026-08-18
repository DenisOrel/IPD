// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Grid.PdfGrid
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

#nullable disable
namespace Syncfusion.Pdf.Grid;

public class PdfGrid : PdfLayoutElement
{
  private float initialWidth;
  private bool m_breakRow = true;
  private bool m_bRepeatHeader;
  private PdfGridColumnCollection m_columns;
  private string m_dataMember;
  private object m_dataSource;
  private PdfDataSource m_dsParser;
  private PdfGridHeaderCollection m_headers;
  private bool m_isChildGrid;
  private PdfLayoutFormat m_layoutFormat;
  private PdfGridCell m_parentCell;
  private PdfGridRowCollection m_rows;
  private SizeF m_size = SizeF.Empty;
  private PdfGridStyle m_style;

  public void Draw(PdfGraphics graphics, RectangleF bounds)
  {
    this.SetSpan();
    this.initialWidth = bounds.Width;
    new PdfGridLayouter(this).Layout(graphics, bounds);
  }

  public PdfGridLayoutResult Draw(PdfPage page, PointF location)
  {
    this.initialWidth = page.Graphics.ClientSize.Width;
    return (PdfGridLayoutResult) base.Draw(page, location);
  }

  public PdfGridLayoutResult Draw(PdfPage page, RectangleF bounds)
  {
    this.initialWidth = bounds.Width;
    return (PdfGridLayoutResult) base.Draw(page, bounds);
  }

  public void Draw(PdfGraphics graphics, PointF location, float width)
  {
    this.Draw(graphics, location.X, location.Y, width);
  }

  public PdfGridLayoutResult Draw(PdfPage page, PointF location, PdfGridLayoutFormat format)
  {
    this.initialWidth = page.Graphics.ClientSize.Width;
    return (PdfGridLayoutResult) this.Draw(page, location, (PdfLayoutFormat) format);
  }

  public PdfGridLayoutResult Draw(PdfPage page, RectangleF bounds, PdfGridLayoutFormat format)
  {
    this.initialWidth = bounds.Width;
    return (PdfGridLayoutResult) this.Draw(page, bounds, (PdfLayoutFormat) format);
  }

  public PdfGridLayoutResult Draw(PdfPage page, float x, float y)
  {
    this.initialWidth = page.Graphics.ClientSize.Width;
    return (PdfGridLayoutResult) base.Draw(page, x, y);
  }

  public void Draw(PdfGraphics graphics, float x, float y, float width)
  {
    this.initialWidth = width;
    RectangleF bounds = new RectangleF(x, y, width, 0.0f);
    this.Draw(graphics, bounds);
  }

  public PdfGridLayoutResult Draw(PdfPage page, float x, float y, PdfGridLayoutFormat format)
  {
    this.initialWidth = page.Graphics.ClientSize.Width;
    return (PdfGridLayoutResult) this.Draw(page, x, y, (PdfLayoutFormat) format);
  }

  public PdfGridLayoutResult Draw(PdfPage page, float x, float y, float width)
  {
    return this.Draw(page, x, y, width, (PdfGridLayoutFormat) null);
  }

  public PdfGridLayoutResult Draw(
    PdfPage page,
    float x,
    float y,
    float width,
    PdfGridLayoutFormat format)
  {
    RectangleF layoutRectangle = new RectangleF(x, y, width + x, 0.0f);
    this.initialWidth = layoutRectangle.Width;
    return (PdfGridLayoutResult) this.Draw(page, layoutRectangle, (PdfLayoutFormat) format);
  }

  protected override void DrawInternal(PdfGraphics graphics)
  {
    this.SetSpan();
    new PdfGridLayouter(this).Layout(graphics, PointF.Empty);
  }

  protected override PdfLayoutResult Layout(PdfLayoutParams param)
  {
    if ((double) param.Bounds.Width < 0.0)
      throw new ArgumentOutOfRangeException("Width");
    this.SetSpan();
    this.m_layoutFormat = param.Format;
    return new PdfGridLayouter(this).Layout(param);
  }

  private SizeF Measure()
  {
    float height = 0.0f;
    float width = this.Columns.Width;
    foreach (PdfGridRow header in this.Headers)
      height += header.Height;
    foreach (PdfGridRow row in (List<PdfGridRow>) this.Rows)
      height += row.Height;
    return new SizeF(width, height);
  }

  internal void MeasureColumnsWidth()
  {
    float[] numArray = new float[this.Columns.Count];
    float val1 = 0.0f;
    if (this.Headers.Count > 0)
    {
      int index1 = 0;
      for (int count1 = this.Headers[0].Cells.Count; index1 < count1; ++index1)
      {
        int index2 = 0;
        for (int count2 = this.Headers.Count; index2 < count2; ++index2)
        {
          float val2 = (double) this.initialWidth > 0.0 ? Math.Min(this.initialWidth, this.Headers[index2].Cells[index1].Width) : this.Headers[index2].Cells[index1].Width;
          val1 = Math.Max(val1, val2);
        }
        numArray[index1] = val1;
      }
    }
    int index3 = 0;
    for (int count3 = this.Columns.Count; index3 < count3; ++index3)
    {
      int index4 = 0;
      for (int count4 = this.Rows.Count; index4 < count4; ++index4)
      {
        float val2_1 = (double) this.initialWidth > 0.0 ? Math.Min(this.initialWidth, this.Rows[index4].Cells[index3].Width) : this.Rows[index4].Cells[index3].Width;
        float val2_2 = Math.Max(numArray[index3], Math.Max(val1, val2_1));
        val1 = Math.Max(this.Columns[index3].Width, val2_2);
      }
      numArray[index3] = val1;
      val1 = 0.0f;
    }
    int index5 = 0;
    for (int count = this.Columns.Count; index5 < count; ++index5)
    {
      if ((double) this.Columns[index5].Width < 0.0)
        this.Columns[index5].Width = numArray[index5];
    }
  }

  internal void MeasureColumnsWidth(RectangleF bounds)
  {
    float[] defaultWidths = this.Columns.GetDefaultWidths(bounds.Width - bounds.X);
    int index = 0;
    for (int count = this.Columns.Count; index < count; ++index)
    {
      if ((double) this.Columns[index].Width < 0.0)
        this.Columns[index].Width = defaultWidths[index];
    }
  }

  private void PopulateGrid()
  {
    if (this.m_dsParser != null)
    {
      int index1 = 0;
      this.Rows.Clear();
      while (index1 < this.m_dsParser.RowCount)
      {
        PdfGridRow row1 = new PdfGridRow(this);
        string[] row2 = this.m_dsParser.GetRow(ref index1);
        for (int index2 = 0; index2 < this.m_dsParser.ColumnCount; ++index2)
          row1.Cells.Add(new PdfGridCell(row1)
          {
            Value = (object) row2[index2]
          });
        this.Rows.Add(row1);
      }
    }
    for (int index = 0; index < this.m_dsParser.ColumnCount; ++index)
      this.Columns.Add(new PdfGridColumn(this));
  }

  private void PopulateHeader()
  {
    this.Headers.Clear();
    string[] columnCaptions = this.m_dsParser.ColumnCaptions;
    if (columnCaptions == null)
      return;
    PdfGridRow row = new PdfGridRow(this);
    for (int index = 0; index < this.m_dsParser.ColumnCount; ++index)
      row.Cells.Add(new PdfGridCell(row)
      {
        Value = (object) columnCaptions[index]
      });
    this.Headers.Add(row);
  }

  private void SetDataSource()
  {
    Array dataSource1 = this.m_dataSource as Array;
    DataSet dataSource2 = this.m_dataSource as DataSet;
    DataColumn dataSource3 = this.m_dataSource as DataColumn;
    DataTable dataSource4 = this.m_dataSource as DataTable;
    DataView dataSource5 = this.m_dataSource as DataView;
    PdfDataSource pdfDataSource = (PdfDataSource) null;
    if (dataSource1 != null)
      pdfDataSource = new PdfDataSource(dataSource1);
    else if (dataSource3 != null)
      pdfDataSource = new PdfDataSource(dataSource3);
    else if (dataSource4 != null)
      pdfDataSource = new PdfDataSource(dataSource4);
    else if (dataSource5 != null)
      pdfDataSource = new PdfDataSource(dataSource5);
    else if (dataSource2 != null)
      pdfDataSource = new PdfDataSource(dataSource2, this.m_dataMember);
    this.m_dsParser = pdfDataSource;
    this.PopulateHeader();
    this.PopulateGrid();
  }

  private void SetSpan()
  {
    int num1 = 1;
    int num2 = 0;
    int index1 = 0;
    for (int count1 = this.Headers.Count; index1 < count1; ++index1)
    {
      PdfGridRow header = this.Headers[index1];
      int index2 = 0;
      for (int count2 = header.Cells.Count; index2 < count2; ++index2)
      {
        PdfGridCell cell = header.Cells[index2];
        if (!cell.IsCellMergeContinue && !cell.IsRowMergeContinue && (cell.ColumnSpan > 1 || cell.RowSpan > 1))
        {
          if (cell.ColumnSpan + index2 > header.Cells.Count)
            throw new ArgumentException($"Invalid span specified at row {index2.ToString()} column {index1.ToString()}");
          if (cell.RowSpan + index1 > this.Rows.Count)
            throw new ArgumentException($"Invalid span specified at row {index2.ToString()} column {index1.ToString()}");
          if (cell.ColumnSpan > 1 && cell.RowSpan > 1)
          {
            int columnSpan1 = cell.ColumnSpan;
            int rowSpan = cell.RowSpan;
            int index3 = index2;
            int index4 = index1;
            cell.IsCellMergeStart = true;
            cell.IsRowMergeStart = true;
            for (; columnSpan1 > 1; --columnSpan1)
            {
              ++index3;
              header.Cells[index3].IsCellMergeContinue = true;
            }
            int index5 = index2;
            int columnSpan2 = cell.ColumnSpan;
            while (rowSpan > 1)
            {
              ++index4;
              this.Headers[index4].Cells[index2].IsRowMergeContinue = true;
              --rowSpan;
              for (; columnSpan2 > 1; --columnSpan2)
              {
                ++index5;
                this.Headers[index4].Cells[index5].IsCellMergeContinue = true;
              }
              columnSpan2 = cell.ColumnSpan;
              index5 = index2;
            }
          }
          else if (cell.ColumnSpan > 1 && cell.RowSpan == 1)
          {
            int columnSpan = cell.ColumnSpan;
            int index6 = index2;
            cell.IsCellMergeStart = true;
            for (; columnSpan > 1; --columnSpan)
            {
              ++index6;
              header.Cells[index6].IsCellMergeContinue = true;
            }
          }
          else if (cell.ColumnSpan == 1 && cell.RowSpan > 1)
          {
            int rowSpan = cell.RowSpan;
            int index7 = index1;
            for (; rowSpan > 1; --rowSpan)
            {
              ++index7;
              this.Headers[index7].Cells[index2].IsRowMergeContinue = true;
            }
          }
        }
      }
    }
    int num3 = num1 = 1;
    int num4 = num2 = 0;
    int index8 = 0;
    for (int count3 = this.Rows.Count; index8 < count3; ++index8)
    {
      PdfGridRow row = this.Rows[index8];
      int index9 = 0;
      for (int count4 = row.Cells.Count; index9 < count4; ++index9)
      {
        PdfGridCell cell = row.Cells[index9];
        if (!cell.IsCellMergeContinue && !cell.IsRowMergeContinue && (cell.ColumnSpan > 1 || cell.RowSpan > 1))
        {
          if (cell.ColumnSpan + index9 > row.Cells.Count)
            throw new ArgumentException($"Invalid span specified at row {index9.ToString()} column {index8.ToString()}");
          if (cell.RowSpan + index8 > this.Rows.Count)
            throw new ArgumentException($"Invalid span specified at row {index9.ToString()} column {index8.ToString()}");
          if (cell.ColumnSpan > 1 && cell.RowSpan > 1)
          {
            int columnSpan3 = cell.ColumnSpan;
            int rowSpan = cell.RowSpan;
            int index10 = index9;
            int index11 = index8;
            cell.IsCellMergeStart = true;
            cell.IsRowMergeStart = true;
            for (; columnSpan3 > 1; --columnSpan3)
            {
              ++index10;
              row.Cells[index10].IsCellMergeContinue = true;
            }
            int index12 = index9;
            int columnSpan4 = cell.ColumnSpan;
            while (rowSpan > 1)
            {
              ++index11;
              this.Rows[index11].Cells[index9].IsRowMergeContinue = true;
              --rowSpan;
              for (; columnSpan4 > 1; --columnSpan4)
              {
                ++index12;
                this.Rows[index11].Cells[index12].IsCellMergeContinue = true;
              }
              columnSpan4 = cell.ColumnSpan;
              index12 = index9;
            }
          }
          else if (cell.ColumnSpan > 1 && cell.RowSpan == 1)
          {
            int columnSpan = cell.ColumnSpan;
            int index13 = index9;
            cell.IsCellMergeStart = true;
            for (; columnSpan > 1; --columnSpan)
            {
              ++index13;
              row.Cells[index13].IsCellMergeContinue = true;
            }
          }
          else if (cell.ColumnSpan == 1 && cell.RowSpan > 1)
          {
            int rowSpan = cell.RowSpan;
            int index14 = index8;
            for (; rowSpan > 1; --rowSpan)
            {
              ++index14;
              this.Rows[index14].Cells[index9].IsRowMergeContinue = true;
            }
          }
        }
      }
    }
  }

  public bool AllowRowBreakAcrossPages
  {
    get => this.m_breakRow;
    set => this.m_breakRow = value;
  }

  public PdfGridColumnCollection Columns
  {
    get
    {
      if (this.m_columns == null)
        this.m_columns = new PdfGridColumnCollection(this);
      return this.m_columns;
    }
  }

  public string DataMember
  {
    get => this.m_dataMember;
    set
    {
      if (value == null && !(this.m_dataMember != value))
        return;
      this.m_dataMember = value;
      this.SetDataSource();
    }
  }

  public object DataSource
  {
    get => this.m_dataSource;
    set
    {
      if (value == null || value == this.m_dataSource)
        return;
      this.m_dataSource = value;
      this.Columns.Clear();
      this.SetDataSource();
    }
  }

  public PdfGridHeaderCollection Headers
  {
    get
    {
      if (this.m_headers == null)
        this.m_headers = new PdfGridHeaderCollection(this);
      return this.m_headers;
    }
  }

  internal bool IsChildGrid
  {
    get => this.m_isChildGrid;
    set => this.m_isChildGrid = value;
  }

  internal PdfGridRow LastRow
  {
    get => this.Rows.Count > 0 ? this.Rows[this.Rows.Count - 1] : (PdfGridRow) null;
  }

  internal PdfLayoutFormat LayoutFormat => this.m_layoutFormat;

  internal PdfGridCell ParentCell
  {
    get => this.m_parentCell;
    set => this.m_parentCell = value;
  }

  public bool RepeatHeader
  {
    get => this.m_bRepeatHeader;
    set => this.m_bRepeatHeader = value;
  }

  public PdfGridRowCollection Rows
  {
    get
    {
      if (this.m_rows == null)
        this.m_rows = new PdfGridRowCollection(this);
      return this.m_rows;
    }
  }

  internal SizeF Size
  {
    get
    {
      if (this.m_size == SizeF.Empty)
        this.m_size = this.Measure();
      return this.m_size;
    }
  }

  public PdfGridStyle Style
  {
    get
    {
      if (this.m_style == null)
        this.m_style = new PdfGridStyle();
      return this.m_style;
    }
    set => this.m_style = value;
  }
}
