// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Grid.PdfGridLayouter
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;

#nullable disable
namespace Syncfusion.Pdf.Grid;

internal class PdfGridLayouter : ElementLayouter
{
  private int m_cellEndIndex;
  private int m_cellStartIndex;
  private List<int[]> m_columnRanges;
  private RectangleF m_currentBounds;
  private PdfGraphics m_currentGraphics;
  private PdfPage m_currentPage;
  private SizeF m_currentPageBounds;
  private int m_currentRowIndex;
  private float m_newheight;
  private int m_repeatRowIndex;
  private PointF m_startLocation;

  internal PdfGridLayouter(PdfGrid grid)
    : base((PdfLayoutElement) grid)
  {
    this.m_columnRanges = new List<int[]>();
    this.m_repeatRowIndex = -1;
  }

  private bool CheckIfDefaultFormat(PdfStringFormat format)
  {
    PdfStringFormat pdfStringFormat = new PdfStringFormat();
    return format.Alignment == pdfStringFormat.Alignment && (double) format.CharacterSpacing == (double) pdfStringFormat.CharacterSpacing && format.ClipPath == pdfStringFormat.ClipPath && (double) format.FirstLineIndent == (double) pdfStringFormat.FirstLineIndent && (double) format.HorizontalScalingFactor == (double) pdfStringFormat.HorizontalScalingFactor && format.LineAlignment == pdfStringFormat.LineAlignment && format.LineLimit == pdfStringFormat.LineLimit && (double) format.LineSpacing == (double) pdfStringFormat.LineSpacing && format.MeasureTrailingSpaces == pdfStringFormat.MeasureTrailingSpaces && format.NoClip == pdfStringFormat.NoClip && (double) format.ParagraphIndent == (double) pdfStringFormat.ParagraphIndent && format.RightToLeft == pdfStringFormat.RightToLeft && format.SubSuperScript == pdfStringFormat.SubSuperScript && (double) format.WordSpacing == (double) pdfStringFormat.WordSpacing && format.WordWrap == pdfStringFormat.WordWrap;
  }

  private void DetermineColumnDrawRanges()
  {
    int num1 = 0;
    int num2 = 0;
    float num3 = 0.0f;
    float width = this.m_currentBounds.Width;
    for (int index1 = 0; index1 < this.Grid.Columns.Count; ++index1)
    {
      num3 += this.Grid.Columns[index1].Width;
      if ((double) num3 > (double) width)
      {
        float num4 = 0.0f;
        for (int index2 = num1; index2 <= index1; ++index2)
        {
          num4 += this.Grid.Columns[index2].Width;
          if ((double) num4 <= (double) width)
            num2 = index2;
          else
            break;
        }
        this.m_columnRanges.Add(new int[2]{ num1, num2 });
        num1 = num2 + 1;
        num3 = num2 < index1 ? this.Grid.Columns[index1].Width : 0.0f;
      }
    }
    this.m_columnRanges.Add(new int[2]
    {
      num1,
      this.Grid.Columns.Count - 1
    });
  }

  private PdfGridLayouter.RowLayoutResult DrawRow(PdfGridRow row)
  {
    PdfGridLayouter.RowLayoutResult result = new PdfGridLayouter.RowLayoutResult();
    float num1 = 0.0f;
    bool flag = false;
    if (row.RowSpanExists)
    {
      int val1 = 0;
      int index1 = this.Grid.Rows.IndexOf(row);
      if (index1 == -1)
      {
        index1 = this.Grid.Headers.IndexOf(row);
        if (index1 != -1)
          flag = true;
      }
      foreach (PdfGridCell cell in row.Cells)
        val1 = Math.Max(val1, cell.RowSpan);
      for (int index2 = index1; index2 < index1 + val1; ++index2)
        num1 += flag ? this.Grid.Headers[index2].Height : this.Grid.Rows[index2].Height;
      if ((double) num1 > (double) this.m_currentBounds.Height)
      {
        num1 = 0.0f;
        foreach (PdfGridCell cell in row.Cells)
        {
          int rowSpan = cell.RowSpan;
          for (int index3 = index1; index3 < index1 + rowSpan; ++index3)
          {
            num1 += flag ? this.Grid.Headers[index3].Height : this.Grid.Rows[index3].Height;
            if ((double) this.m_currentBounds.Y + (double) num1 > (double) this.m_currentPageBounds.Height)
            {
              num1 -= flag ? this.Grid.Headers[index3].Height : this.Grid.Rows[index3].Height;
              for (int index4 = 0; index4 < this.Grid.Rows[index1].Cells.Count; ++index4)
              {
                int num2 = index3 - index1;
                if (!flag && this.Grid.Rows[index1].Cells[index4].RowSpan == rowSpan)
                {
                  this.Grid.Rows[index1].Cells[index4].RowSpan = num2 == 0 ? 1 : num2;
                  this.Grid.Rows[index3].Cells[index4].RowSpan = rowSpan - num2;
                  this.Grid.Rows[index3].Cells[index4].StringFormat = this.Grid.Rows[index1].Cells[index4].StringFormat;
                  this.Grid.Rows[index3].Cells[index4].Style = this.Grid.Rows[index1].Cells[index4].Style;
                  this.Grid.Rows[index3].Cells[index4].ColumnSpan = this.Grid.Rows[index1].Cells[index4].ColumnSpan;
                  this.Grid.Rows[index3].Cells[index4].Value = this.Grid.Rows[index1].Cells[index4].Value;
                  this.Grid.Rows[index3 - 1].RowSpanExists = false;
                  this.Grid.Rows[index3].Cells[index4].IsRowMergeContinue = false;
                  this.Grid.Rows[index3].Cells[index4].IsRowMergeStart = true;
                }
                else if (flag && this.Grid.Headers[index1].Cells[index4].RowSpan == rowSpan)
                {
                  this.Grid.Headers[index1].Cells[index4].RowSpan = num2 == 0 ? 1 : num2;
                  this.Grid.Headers[index3].Cells[index4].RowSpan = rowSpan - num2;
                  this.Grid.Headers[index3].Cells[index4].StringFormat = this.Grid.Headers[index1].Cells[index4].StringFormat;
                  this.Grid.Headers[index3].Cells[index4].Style = this.Grid.Headers[index1].Cells[index4].Style;
                  this.Grid.Headers[index3].Cells[index4].ColumnSpan = this.Grid.Headers[index1].Cells[index4].ColumnSpan;
                  this.Grid.Headers[index3].Cells[index4].Value = this.Grid.Headers[index1].Cells[index4].Value;
                  this.Grid.Headers[index3 - 1].RowSpanExists = false;
                  this.Grid.Headers[index3].Cells[index4].IsRowMergeContinue = false;
                  this.Grid.Headers[index3].Cells[index4].IsRowMergeStart = true;
                }
              }
              break;
            }
          }
          num1 = 0.0f;
        }
      }
    }
    float height = (double) row.RowBreakHeight > 0.0 ? row.RowBreakHeight : row.Height;
    if ((double) height > (double) this.m_currentPageBounds.Height)
    {
      if (this.Grid.AllowRowBreakAcrossPages)
      {
        result.IsFinish = true;
        this.DrawRowWithBreak(ref result, row, height);
        return result;
      }
      result.IsFinish = false;
      this.DrawRow(ref result, row, height);
      return result;
    }
    if ((double) this.m_currentBounds.Y + (double) height > (double) this.m_currentPageBounds.Height || (double) this.m_currentBounds.Y + (double) num1 > (double) this.m_currentPageBounds.Height)
    {
      if (this.m_repeatRowIndex > -1 && this.m_repeatRowIndex == row.RowIndex)
      {
        if (this.Grid.AllowRowBreakAcrossPages)
        {
          result.IsFinish = true;
          this.DrawRowWithBreak(ref result, row, height);
          return result;
        }
        result.IsFinish = false;
        this.DrawRow(ref result, row, height);
        return result;
      }
      result.IsFinish = false;
      return result;
    }
    result.IsFinish = true;
    this.DrawRow(ref result, row, height);
    return result;
  }

  private void DrawRow(ref PdfGridLayouter.RowLayoutResult result, PdfGridRow row, float height)
  {
    PointF location = this.m_currentBounds.Location;
    result.Bounds = new RectangleF(location, SizeF.Empty);
    height = this.ReCalculateHeight(row, height);
    for (int cellStartIndex = this.m_cellStartIndex; cellStartIndex <= this.m_cellEndIndex; ++cellStartIndex)
    {
      bool cancelSubsequentSpans = cellStartIndex > this.m_cellEndIndex + 1 && row.Cells[cellStartIndex].ColumnSpan > 1;
      if (!cancelSubsequentSpans)
      {
        for (int index = 1; index < row.Cells[cellStartIndex].ColumnSpan; ++index)
          row.Cells[cellStartIndex + index].IsCellMergeContinue = true;
      }
      SizeF size = new SizeF(this.Grid.Columns[cellStartIndex].Width, height);
      if (!this.CheckIfDefaultFormat(this.Grid.Columns[cellStartIndex].Format) && this.CheckIfDefaultFormat(row.Cells[cellStartIndex].StringFormat))
        row.Cells[cellStartIndex].StringFormat = this.Grid.Columns[cellStartIndex].Format;
      PdfStringLayoutResult stringLayoutResult = row.Cells[cellStartIndex].Draw(this.m_currentGraphics, new RectangleF(location, size), cancelSubsequentSpans);
      if (row.Grid.Style.AllowHorizontalOverflow && (row.Cells[cellStartIndex].ColumnSpan > this.m_cellEndIndex || cellStartIndex + row.Cells[cellStartIndex].ColumnSpan > this.m_cellEndIndex + 1) && this.m_cellEndIndex < row.Cells.Count - 1)
        row.RowOverflowIndex = this.m_cellEndIndex;
      if (row.Grid.Style.AllowHorizontalOverflow && row.RowOverflowIndex > 0 && (row.Cells[cellStartIndex].ColumnSpan > this.m_cellEndIndex || cellStartIndex + row.Cells[cellStartIndex].ColumnSpan > this.m_cellEndIndex + 1) && row.Cells[cellStartIndex].ColumnSpan - this.m_cellEndIndex + cellStartIndex - 1 > 0)
      {
        row.Cells[row.RowOverflowIndex + 1].Value = (object) stringLayoutResult?.m_remainder;
        row.Cells[row.RowOverflowIndex + 1].StringFormat = row.Cells[cellStartIndex].StringFormat;
        row.Cells[row.RowOverflowIndex + 1].Style = row.Cells[cellStartIndex].Style;
        row.Cells[row.RowOverflowIndex + 1].ColumnSpan = row.Cells[cellStartIndex].ColumnSpan - this.m_cellEndIndex + cellStartIndex - 1;
      }
      location.X += this.Grid.Columns[cellStartIndex].Width;
    }
    this.m_currentBounds.Y += height;
    result.Bounds = new RectangleF(result.Bounds.Location, new SizeF(location.X, location.Y));
  }

  private void DrawRowWithBreak(
    ref PdfGridLayouter.RowLayoutResult result,
    PdfGridRow row,
    float height)
  {
    PointF location = this.m_currentBounds.Location;
    result.Bounds = new RectangleF(location, SizeF.Empty);
    this.m_newheight = (double) row.RowBreakHeight > 0.0 ? this.m_currentPageBounds.Height : 0.0f;
    row.RowBreakHeight = this.m_currentBounds.Y + height - this.m_currentPageBounds.Height;
    foreach (PdfGridCell cell in row.Cells)
    {
      float num = cell.MeasureHeight();
      if ((double) num == (double) height && cell.Value is PdfGrid)
        row.RowBreakHeight = 0.0f;
      else if ((double) num == (double) height && !(cell.Value is PdfGrid))
        row.RowBreakHeight = this.m_currentBounds.Y + height - this.m_currentPageBounds.Height;
    }
    for (int cellStartIndex = this.m_cellStartIndex; cellStartIndex <= this.m_cellEndIndex; ++cellStartIndex)
    {
      bool cancelSubsequentSpans = row.Cells[cellStartIndex].ColumnSpan + cellStartIndex > this.m_cellEndIndex + 1 && row.Cells[cellStartIndex].ColumnSpan > 1;
      if (!cancelSubsequentSpans)
      {
        for (int index = 1; index < row.Cells[cellStartIndex].ColumnSpan; ++index)
          row.Cells[cellStartIndex + index].IsCellMergeContinue = true;
      }
      SizeF size = new SizeF(this.Grid.Columns[cellStartIndex].Width, (double) this.m_newheight > 0.0 ? this.m_newheight : this.m_currentPageBounds.Height);
      if (!this.CheckIfDefaultFormat(this.Grid.Columns[cellStartIndex].Format) && this.CheckIfDefaultFormat(row.Cells[cellStartIndex].StringFormat))
        row.Cells[cellStartIndex].StringFormat = this.Grid.Columns[cellStartIndex].Format;
      PdfStringLayoutResult stringLayoutResult = row.Cells[cellStartIndex].Draw(this.m_currentGraphics, new RectangleF(location, size), cancelSubsequentSpans);
      if ((double) row.RowBreakHeight > 0.0 && stringLayoutResult != null)
      {
        row.Cells[cellStartIndex].FinishedDrawingCell = false;
        row.Cells[cellStartIndex].RemainingString = stringLayoutResult.Remainder == null ? string.Empty : stringLayoutResult.Remainder;
      }
      result.IsFinish = !result.IsFinish ? result.IsFinish : row.Cells[cellStartIndex].FinishedDrawingCell;
      location.X += this.Grid.Columns[cellStartIndex].Width;
    }
    this.m_currentBounds.Y += (double) this.m_newheight > 0.0 ? this.m_newheight : height;
    result.Bounds = new RectangleF(result.Bounds.Location, new SizeF(location.X, location.Y));
  }

  private PdfGridLayoutFormat GetFormat(PdfLayoutFormat format)
  {
    PdfGridLayoutFormat format1 = format as PdfGridLayoutFormat;
    if (format != null && format1 == null)
      format1 = new PdfGridLayoutFormat(format);
    return format1;
  }

  private PdfGridLayoutResult GetLayoutResult()
  {
    return new PdfGridLayoutResult(this.m_currentPage, new RectangleF(this.m_startLocation, new SizeF(this.m_currentBounds.Width, this.m_currentBounds.Y - this.m_startLocation.Y)));
  }

  public PdfPage GetNextPage(PdfLayoutFormat format)
  {
    PdfSection section = this.m_currentPage.Section;
    int num = section.IndexOf(this.m_currentPage);
    PdfPage nextPage = num != section.Count - 1 ? section[num + 1] : section.Add();
    this.m_currentGraphics = nextPage.Graphics;
    this.m_currentBounds = new RectangleF(PointF.Empty, nextPage.GetClientSize());
    if (format.PaginateBounds != RectangleF.Empty)
    {
      ref RectangleF local1 = ref this.m_currentBounds;
      RectangleF paginateBounds = format.PaginateBounds;
      double x = (double) paginateBounds.X;
      local1.X = (float) x;
      ref RectangleF local2 = ref this.m_currentBounds;
      paginateBounds = format.PaginateBounds;
      double y = (double) paginateBounds.Y;
      local2.Y = (float) y;
      ref RectangleF local3 = ref this.m_currentBounds;
      paginateBounds = format.PaginateBounds;
      double height = (double) paginateBounds.Size.Height;
      local3.Height = (float) height;
    }
    return nextPage;
  }

  public void Layout(PdfGraphics graphics, PointF location)
  {
    RectangleF bounds = new RectangleF(location, SizeF.Empty);
    this.Layout(graphics, bounds);
  }

  public void Layout(PdfGraphics graphics, RectangleF bounds)
  {
    if (graphics == null)
      throw new ArgumentNullException(nameof (graphics));
    double width = (double) graphics.ClientSize.Width;
    double x = (double) bounds.X;
    PdfLayoutParams pdfLayoutParams = new PdfLayoutParams();
    pdfLayoutParams.Bounds = bounds;
    this.m_currentGraphics = graphics;
    this.LayoutInternal(pdfLayoutParams);
  }

  protected override PdfLayoutResult LayoutInternal(PdfLayoutParams param)
  {
    if (param == null)
      throw new ArgumentNullException(nameof (param));
    this.GetFormat(param.Format);
    this.m_currentPage = param.Page;
    this.m_currentPageBounds = this.m_currentPage != null ? this.m_currentPage.GetClientSize() : this.m_currentGraphics.ClientSize;
    this.m_currentGraphics = this.m_currentPage != null ? this.m_currentPage.Graphics : this.m_currentGraphics;
    this.m_currentBounds = new RectangleF(param.Bounds.Location, this.m_currentGraphics.ClientSize);
    ref RectangleF local = ref this.m_currentBounds;
    RectangleF bounds = param.Bounds;
    double width;
    if ((double) bounds.Width <= 0.0)
    {
      width = (double) this.m_currentBounds.Width;
    }
    else
    {
      bounds = param.Bounds;
      width = (double) bounds.Width;
    }
    local.Width = (float) width;
    bounds = param.Bounds;
    this.m_startLocation = bounds.Location;
    if (!this.Grid.Style.AllowHorizontalOverflow)
    {
      this.Grid.MeasureColumnsWidth(this.m_currentBounds);
      this.m_columnRanges.Add(new int[2]
      {
        0,
        this.Grid.Columns.Count - 1
      });
    }
    else
    {
      this.Grid.MeasureColumnsWidth();
      this.DetermineColumnDrawRanges();
    }
    return (PdfLayoutResult) this.LayoutOnPage(param);
  }

  private PdfGridLayoutResult LayoutOnPage(PdfLayoutParams param)
  {
    PdfGridLayoutFormat format = this.GetFormat(param.Format);
    PdfGridLayoutResult gridLayoutResult = (PdfGridLayoutResult) null;
    Dictionary<PdfPage, int[]> layoutedPages = new Dictionary<PdfPage, int[]>();
    PdfPage page = param.Page;
    foreach (int[] columnRange in this.m_columnRanges)
    {
      this.m_cellStartIndex = columnRange[0];
      this.m_cellEndIndex = columnRange[1];
      if (this.RaiseBeforePageLayout(this.m_currentPage, ref this.m_currentBounds, ref this.m_currentRowIndex))
      {
        gridLayoutResult = new PdfGridLayoutResult(this.m_currentPage, this.m_currentBounds);
        break;
      }
      foreach (PdfGridRow header in this.Grid.Headers)
      {
        double y1 = (double) this.m_currentBounds.Y;
        PdfGridLayouter.RowLayoutResult rowLayoutResult = this.DrawRow(header);
        double y2 = (double) this.m_currentBounds.Y;
        bool flag;
        if (y1 == y2)
        {
          flag = true;
          this.m_repeatRowIndex = this.Grid.Rows.IndexOf(header);
        }
        else
          flag = false;
        if (!rowLayoutResult.IsFinish && page != null && format.Layout != PdfLayoutType.OnePage & flag)
        {
          this.m_startLocation.X = this.m_currentBounds.X;
          this.m_currentPage = this.GetNextPage((PdfLayoutFormat) format);
          this.m_startLocation.Y = this.m_currentBounds.Y;
          if (format.PaginateBounds == RectangleF.Empty)
            this.m_currentBounds.X += this.m_startLocation.X;
          this.DrawRow(header);
        }
      }
      int num1 = 0;
      int count = this.Grid.Rows.Count;
      foreach (PdfGridRow row in (List<PdfGridRow>) this.Grid.Rows)
      {
        ++num1;
        double y3 = (double) this.m_currentBounds.Y;
        if (this.m_currentPage != null && !layoutedPages.ContainsKey(this.m_currentPage))
          layoutedPages.Add(this.m_currentPage, columnRange);
        PdfGridLayouter.RowLayoutResult rowLayoutResult = this.DrawRow(row);
        double y4 = (double) this.m_currentBounds.Y;
        bool flag;
        if (y3 == y4)
        {
          flag = true;
          this.m_repeatRowIndex = this.Grid.Rows.IndexOf(row);
        }
        else
        {
          flag = false;
          this.m_repeatRowIndex = -1;
        }
        while (!rowLayoutResult.IsFinish && page != null)
        {
          PdfGridLayoutResult layoutResult = this.GetLayoutResult();
          if (page != this.m_currentPage && row.Grid.IsChildGrid && row.Grid.ParentCell != null)
          {
            RectangleF rectangleF1;
            ref RectangleF local1 = ref rectangleF1;
            RectangleF rectangleF2 = format.PaginateBounds;
            PointF location = rectangleF2.Location;
            rectangleF2 = param.Bounds;
            double width1 = (double) rectangleF2.Width;
            rectangleF2 = layoutResult.Bounds;
            double height = (double) rectangleF2.Height;
            SizeF size = new SizeF((float) width1, (float) height);
            local1 = new RectangleF(location, size);
            ref RectangleF local2 = ref rectangleF1;
            double x1 = (double) local2.X;
            rectangleF2 = param.Bounds;
            double x2 = (double) rectangleF2.X;
            local2.X = (float) (x1 + x2);
            PdfGridCell cell;
            for (int index = 0; index < row.Cells.Count; index = index + (cell.ColumnSpan - 1) + 1)
            {
              cell = row.Cells[index];
              float width2 = 0.0f;
              if (cell.ColumnSpan > 1)
              {
                for (; index < cell.ColumnSpan; ++index)
                  width2 += row.Grid.Columns[index].Width;
              }
              else
                width2 = Math.Max(cell.Width, row.Grid.Columns[index].Width);
              cell.DrawCellBorders(ref this.m_currentGraphics, new RectangleF(rectangleF1.Location, new SizeF(width2, rectangleF1.Height)));
              rectangleF1.X += width2;
            }
          }
          if (!(this.RaisePageLayouted((PdfLayoutResult) layoutResult).Cancel | flag))
          {
            if (this.Grid.AllowRowBreakAcrossPages)
            {
              this.m_currentPage = this.GetNextPage((PdfLayoutFormat) format);
              double y5 = (double) this.m_currentBounds.Y;
              rowLayoutResult = this.DrawRow(row);
            }
            else
            {
              if (!this.Grid.AllowRowBreakAcrossPages && num1 < count)
              {
                this.m_currentPage = this.GetNextPage((PdfLayoutFormat) format);
                break;
              }
              if (num1 >= count)
                break;
            }
          }
          else
            break;
        }
        if (!rowLayoutResult.IsFinish && page != null && format.Layout != PdfLayoutType.OnePage & flag)
        {
          this.m_startLocation.X = this.m_currentBounds.X;
          this.m_currentPage = this.GetNextPage((PdfLayoutFormat) format);
          if (!this.RaiseBeforePageLayout(this.m_currentPage, ref this.m_currentBounds, ref this.m_currentRowIndex))
          {
            this.m_startLocation.Y = this.m_currentBounds.Y;
            if (format.PaginateBounds == RectangleF.Empty)
              this.m_currentBounds.X += this.m_startLocation.X;
            if (this.Grid.RepeatHeader)
            {
              foreach (PdfGridRow header in this.Grid.Headers)
                this.DrawRow(header);
            }
            this.DrawRow(row);
          }
          else
            break;
        }
        if (row.NestedGridLayoutResult != null)
        {
          this.m_currentPage = row.NestedGridLayoutResult.Page;
          this.m_currentGraphics = this.m_currentPage.Graphics;
          RectangleF rectangleF = row.NestedGridLayoutResult.Bounds;
          this.m_startLocation = rectangleF.Location;
          ref RectangleF local3 = ref this.m_currentBounds;
          rectangleF = row.NestedGridLayoutResult.Bounds;
          double bottom = (double) rectangleF.Bottom;
          local3.Y = (float) bottom;
          if (page != this.m_currentPage)
          {
            PdfSection section = this.m_currentPage.Section;
            int num2 = section.IndexOf(page) + 1;
            int num3 = section.IndexOf(this.m_currentPage);
            for (int index1 = num2; index1 < num3 + 1; ++index1)
            {
              PdfGraphics graphics = section[index1].Graphics;
              rectangleF = format.PaginateBounds;
              PointF location = rectangleF.Location;
              double num4;
              if (index1 != num3)
              {
                num4 = (double) this.m_currentBounds.Height - (double) location.Y;
              }
              else
              {
                rectangleF = row.NestedGridLayoutResult.Bounds;
                num4 = (double) rectangleF.Height;
              }
              float height = (float) num4;
              if (row.Grid.IsChildGrid && row.Grid.ParentCell != null)
              {
                ref PointF local4 = ref location;
                double x3 = (double) local4.X;
                rectangleF = param.Bounds;
                double x4 = (double) rectangleF.X;
                local4.X = (float) (x3 + x4);
              }
              PdfGridCell cell;
              for (int index2 = 0; index2 < row.Cells.Count; index2 = index2 + (cell.ColumnSpan - 1) + 1)
              {
                cell = row.Cells[index2];
                float width = 0.0f;
                if (cell.ColumnSpan > 1)
                {
                  for (; index2 < cell.ColumnSpan; ++index2)
                    width += row.Grid.Columns[index2].Width;
                }
                else
                  width = Math.Max(cell.Width, row.Grid.Columns[index2].Width);
                cell.DrawCellBorders(ref graphics, new RectangleF(location, new SizeF(width, height)));
                location.X += width;
              }
            }
            page = this.m_currentPage;
          }
        }
      }
      if (this.m_columnRanges.IndexOf(columnRange) < this.m_columnRanges.Count - 1 && page != null && format.Layout != PdfLayoutType.OnePage)
        this.m_currentPage = this.GetNextPage((PdfLayoutFormat) format);
    }
    PdfGridLayoutResult layoutResult1 = this.GetLayoutResult();
    if (this.Grid.Style.AllowHorizontalOverflow && this.Grid.Style.HorizontalOverflowType == PdfHorizontalOverflowType.NextPage)
      this.ReArrangePages(layoutedPages);
    this.RaisePageLayouted((PdfLayoutResult) layoutResult1);
    return layoutResult1;
  }

  private bool RaiseBeforePageLayout(
    PdfPage currentPage,
    ref RectangleF currentBounds,
    ref int currentRow)
  {
    bool flag = false;
    if (this.Element.RaiseBeginPageLayout)
    {
      PdfGridBeginPageLayoutEventArgs e = new PdfGridBeginPageLayoutEventArgs(currentBounds, currentPage, currentRow);
      this.Element.OnBeginPageLayout((BeginPageLayoutEventArgs) e);
      flag = e.Cancel;
      currentBounds = e.Bounds;
      currentRow = e.StartRowIndex;
    }
    return flag;
  }

  private PdfGridEndPageLayoutEventArgs RaisePageLayouted(PdfLayoutResult result)
  {
    PdfGridEndPageLayoutEventArgs e = new PdfGridEndPageLayoutEventArgs(result);
    if (this.Element.RaiseEndPageLayout)
      this.Element.OnEndPageLayout((EndPageLayoutEventArgs) e);
    return e;
  }

  private void ReArrangePages(Dictionary<PdfPage, int[]> layoutedPages)
  {
    PdfDocument document = this.m_currentPage.Document;
    List<PdfPage> pdfPageList = new List<PdfPage>();
    foreach (PdfPage key in layoutedPages.Keys)
    {
      key.Section = (PdfSection) null;
      pdfPageList.Add(key);
      document.Pages.Remove(key);
    }
    for (int index1 = 0; index1 < layoutedPages.Count; ++index1)
    {
      int index2 = index1;
      int num = layoutedPages.Count / this.m_columnRanges.Count;
      for (; index2 < layoutedPages.Count; index2 += num)
      {
        PdfPage page = pdfPageList[index2];
        if (document.Pages.IndexOf(page) == -1)
          document.Pages.Add(page);
      }
    }
  }

  private float ReCalculateHeight(PdfGridRow row, float height)
  {
    float num = 0.0f;
    for (int cellStartIndex = this.m_cellStartIndex; cellStartIndex <= this.m_cellEndIndex; ++cellStartIndex)
    {
      if (!string.IsNullOrEmpty(row.Cells[cellStartIndex].RemainingString))
        num = Math.Max(num, row.Cells[cellStartIndex].MeasureHeight());
    }
    return Math.Max(height, num);
  }

  internal PdfGrid Grid => this.Element as PdfGrid;

  internal class RowLayoutResult
  {
    private bool m_bIsFinished;
    private RectangleF m_layoutedBounds;

    public RectangleF Bounds
    {
      get => this.m_layoutedBounds;
      set => this.m_layoutedBounds = value;
    }

    public bool IsFinish
    {
      get => this.m_bIsFinished;
      set => this.m_bIsFinished = value;
    }
  }
}
