// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Tables.LightTableLayouter
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using System;
using System.Drawing;


namespace Syncfusion.Pdf.Tables
{
    internal class LightTableLayouter : ElementLayouter
    {
      private float m_cellSpacing;
      private float[] m_cellWidths;
      private RectangleF m_currentBounds;
      private PdfGraphics m_currentGraphics;
      private PdfPage m_currentPage;
      private SizeF m_currentPageBounds;
      private int m_dropIndex;
      private int m_endColumn;
      private PdfStringLayoutResult[] m_latestTextResults;
      private int m_previousRowIndex;
      private string[] m_row;
      private int[] m_spanMap;
      private int m_startColumn;

      internal LightTableLayouter(PdfLightTable table)
        : base((PdfLayoutElement) table)
      {
        this.m_previousRowIndex = -1;
      }

      private static float ApplyBordersToHeight(float height, float borderWidth, bool overlapped)
      {
        if (overlapped)
          height -= borderWidth;
        else
          height -= borderWidth * 2f;
        if ((double) height < 0.0)
          height = 0.0f;
        return height;
      }

      private string[] CropRow(string[] row)
      {
        string[] destinationArray = row;
        if (row != null && (this.m_endColumn != 0 || this.m_startColumn != 0))
        {
          int length = this.m_endColumn - this.m_startColumn + 1;
          destinationArray = new string[length];
          Array.Copy((Array) row, this.m_startColumn, (Array) destinationArray, 0, length);
        }
        return destinationArray;
      }

      private float DetermineRowHeight(
        PdfLayoutParams param,
        int rowIndex,
        string[] row,
        RectangleF rowBouds,
        out PdfStringLayoutResult[] results,
        PdfCellStyle cs)
      {
        int length = row.Length;
        float height = 0.0f;
        if (this.m_currentPage != null)
          height = Math.Min(this.m_currentPageBounds.Height - rowBouds.Y, rowBouds.Height);
        SizeF sizeF = new SizeF(this.m_cellWidths[0], height);
        float width = cs.BorderPen.Width;
        float cellPadding = this.Table.Style.CellPadding;
        bool overlapped = this.Table.Style.BorderOverlapStyle == PdfBorderOverlapStyle.Overlap;
        float rowHeight = 0.0f;
        sizeF.Height = LightTableLayouter.ApplyBordersToHeight(sizeF.Height, width, overlapped);
        if ((double) cellPadding > 0.0)
          sizeF.Height = LightTableLayouter.ApplyBordersToHeight(sizeF.Height, cellPadding, false);
        results = new PdfStringLayoutResult[length];
        PdfColumnCollection columns = this.Table.Columns;
        for (int index = 0; index < length; ++index)
        {
          PdfStringLayoutResult stringLayoutResult;
          if (this.m_spanMap != null && this.m_spanMap[index] < 0)
          {
            stringLayoutResult = new PdfStringLayoutResult();
            stringLayoutResult.m_actualSize = SizeF.Empty;
          }
          else
          {
            string text1 = row[index];
            sizeF.Width = this.GetCellWidth(index);
            sizeF.Width = LightTableLayouter.ApplyBordersToHeight(sizeF.Width, width, overlapped);
            if ((double) cellPadding > 0.0)
              sizeF.Width = LightTableLayouter.ApplyBordersToHeight(sizeF.Width, cellPadding, false);
            if (text1 != null)
            {
              if (text1.Equals(string.Empty))
                text1 = " ";
              if (this.m_previousRowIndex != rowIndex)
                text1 = PdfGraphics.NormalizeText(cs.Font, text1);
            }
            else
              text1 = string.Empty;
            PdfStringLayouter pdfStringLayouter = new PdfStringLayouter();
            PdfStringFormat pdfStringFormat = columns[index].StringFormat ?? cs.StringFormat;
            string text2 = text1;
            PdfFont font = cs.Font;
            PdfStringFormat format = pdfStringFormat;
            SizeF size = sizeF;
            stringLayoutResult = pdfStringLayouter.Layout(text2, font, format, size);
            bool flag = param.Format != null && param.Format.Break == PdfLayoutBreakType.FitElement;
            string remainder = stringLayoutResult.Remainder;
            if ((!this.Table.AllowRowBreakAcrossPages ? !string.IsNullOrEmpty(remainder) : flag & !string.IsNullOrEmpty(remainder)) && this.m_dropIndex != rowIndex)
            {
              this.DropToNextPage(results, length, row);
              this.m_dropIndex = rowIndex;
              rowHeight = 0.0f;
              break;
            }
            if ((double) sizeF.Height > 0.0 || this.m_currentPage == null)
            {
              rowHeight = Math.Max(stringLayoutResult.ActualSize.Height, rowHeight);
            }
            else
            {
              stringLayoutResult = new PdfStringLayoutResult();
              stringLayoutResult.m_remainder = text1;
              stringLayoutResult.m_actualSize = SizeF.Empty;
            }
          }
          results[index] = stringLayoutResult;
        }
        this.m_previousRowIndex = rowIndex;
        if ((double) rowHeight <= 0.0)
          return rowHeight;
        if (this.m_currentPage != null)
          rowHeight = Math.Min(rowBouds.Height, rowHeight);
        if ((double) cellPadding > 0.0)
          rowHeight = LightTableLayouter.ApplyBordersToHeight(rowHeight, -cellPadding, false);
        return LightTableLayouter.ApplyBordersToHeight(rowHeight, -width, overlapped);
      }

      private PdfStringLayoutResult DrawCell(
        PdfStringLayoutResult layoutResult,
        RectangleF bounds,
        int rowIndex,
        int cellIndex,
        PdfCellStyle cs,
        bool ignoreColumnFormat)
      {
        PdfGraphics pdfGraphics = this.m_currentPage != null ? this.m_currentPage.Graphics : this.m_currentGraphics;
        bool flag = this.Table.Style.BorderOverlapStyle == PdfBorderOverlapStyle.Overlap;
        float cellPadding = this.Table.Style.CellPadding;
        PdfPen borderPen = cs.BorderPen;
        PdfBrush backgroundBrush = cs.BackgroundBrush;
        if (this.m_spanMap != null && this.m_spanMap[cellIndex] == -1)
          return new PdfStringLayoutResult();
        if (!flag)
          bounds = LightTableLayouter.PreserveForBorder(bounds, borderPen, PdfBorderOverlapStyle.Overlap);
        if (backgroundBrush != null)
        {
          float alpha = this.GetAlpha(backgroundBrush);
          pdfGraphics.Save();
          pdfGraphics.SetTransparency(alpha);
          pdfGraphics.DrawRectangle((PdfPen) null, backgroundBrush, bounds);
          pdfGraphics.Restore();
        }
        if (borderPen != null)
        {
          float alpha = (float) borderPen.Color.A / (float) byte.MaxValue;
          pdfGraphics.Save();
          pdfGraphics.SetTransparency(alpha);
          pdfGraphics.DrawRectangle(borderPen, (PdfBrush) null, bounds);
          pdfGraphics.Restore();
        }
        bounds = LightTableLayouter.PreserveForBorder(bounds, borderPen, PdfBorderOverlapStyle.Overlap);
        if ((double) cellPadding > 0.0)
        {
          bounds.X += cellPadding;
          bounds.Y += cellPadding;
          bounds.Width -= cellPadding * 2f;
          bounds.Height -= cellPadding * 2f;
        }
        if (!layoutResult.Empty)
        {
          PdfColumn column = this.Table.Columns[cellIndex];
          PdfStringFormat format = (ignoreColumnFormat ? cs.StringFormat : column.StringFormat) ?? cs.StringFormat;
          RectangleF layoutRectangle = bounds;
          RectangleF rectangleF = pdfGraphics.CheckCorrectLayoutRectangle(layoutResult.ActualSize, layoutRectangle.X, layoutRectangle.Y, format);
          if ((double) layoutRectangle.Width <= 0.0)
          {
            layoutRectangle.X = rectangleF.X;
            layoutRectangle.Width = rectangleF.Width;
          }
          if ((double) layoutRectangle.Height <= 0.0)
          {
            layoutRectangle.Y = rectangleF.Y;
            layoutRectangle.Height = rectangleF.Height;
          }
          pdfGraphics.DrawStringLayoutResult(layoutResult, cs.Font, cs.TextPen, cs.TextBrush, layoutRectangle, format);
        }
        return layoutResult;
      }

      private bool DrawRow(
        PdfLayoutParams param,
        ref int rowIndex,
        string[] row,
        RectangleF rowBouds,
        out float rowHeight,
        bool isHeader,
        out bool stop)
      {
        int length = this.m_cellWidths.Length;
        PdfStringLayoutResult[] results = (PdfStringLayoutResult[]) null;
        bool hasOwnStyle;
        PdfCellStyle cellStyle = this.GetCellStyle(rowIndex, isHeader, out hasOwnStyle);
        BeginRowLayoutEventArgs rowLayoutEventArgs = this.RaiseBeforeRowLayout(rowIndex, cellStyle);
        bool flag1 = false;
        this.m_spanMap = (int[]) null;
        rowHeight = 0.0f;
        if (rowLayoutEventArgs != null)
        {
          stop = rowLayoutEventArgs.Cancel;
          flag1 = rowLayoutEventArgs.Skip;
          this.m_spanMap = rowLayoutEventArgs.ColumnSpanMap;
          cellStyle = rowLayoutEventArgs.CellStyle;
          this.ValidateSpanMap();
          rowHeight = Math.Max(rowLayoutEventArgs.MinimalHeight, rowHeight);
        }
        else
          stop = false;
        if (!stop)
        {
          float rowHeight1 = this.DetermineRowHeight(param, rowIndex, row, rowBouds, out results, cellStyle);
          rowHeight = (double) rowHeight1 <= 0.0 ? rowHeight1 : Math.Max(rowHeight1, rowHeight);
          this.m_latestTextResults = results;
        }
        if ((double) rowHeight <= 0.0 | stop)
          return this.IsIncomplete(results) | (double) this.m_currentPageBounds.Height - (double) rowBouds.Y <= 0.0;
        rowBouds.Height = rowHeight;
        if ((double) rowBouds.Y + (double) rowBouds.Height > (double) this.m_currentPageBounds.Height && this.m_currentPage != null)
          return true;
        bool flag2 = false;
        RectangleF bounds = rowBouds;
        PdfGraphics graphics = this.m_currentPage != null ? this.m_currentPage.Graphics : this.m_currentGraphics;
        int num1 = 0;
        if (!flag1)
        {
          for (int cellIndex = 0; cellIndex < length; ++cellIndex)
          {
            bounds.Width = this.GetCellWidth(cellIndex);
            int num2 = this.m_spanMap == null ? 0 : (this.m_spanMap[cellIndex] < 0 ? 1 : 0);
            string str = row[cellIndex];
            bool flag3 = false;
            if (num2 == 0)
            {
              bounds.X += this.m_cellSpacing;
              BeginCellLayoutEventArgs cellLayoutEventArgs = this.RaiseBeforeCellLayout(graphics, rowIndex, cellIndex, bounds, str);
              if (cellLayoutEventArgs != null)
                flag3 = cellLayoutEventArgs.Skip;
              if (flag3)
                ++num1;
            }
            PdfStringLayoutResult layoutResult = results[cellIndex];
            if (!flag1 && !flag3 && !layoutResult.Empty)
            {
              bool ignoreColumnFormat = false;
              if (rowLayoutEventArgs != null)
                ignoreColumnFormat = rowLayoutEventArgs.IgnoreColumnFormat;
              if (isHeader & hasOwnStyle)
                ignoreColumnFormat = true;
              layoutResult = this.DrawCell(layoutResult, bounds, rowIndex, cellIndex, cellStyle, ignoreColumnFormat);
            }
            if (num2 == 0)
              this.RaiseAfterCellLayout(graphics, rowIndex, cellIndex, bounds, str);
            string remainder = layoutResult.Remainder;
            if (remainder != null && remainder != string.Empty)
              flag2 = true;
            row[cellIndex] = remainder;
            if (num2 == 0)
              bounds.X += bounds.Width;
          }
        }
        else
          rowHeight = 0.0f;
        if (num1 == length)
          rowHeight = 0.0f;
        if (!flag2)
        {
          this.m_row = (string[]) null;
          ++rowIndex;
        }
        else
          this.m_row = row;
        stop = this.RaiseAfterRowLayout(rowIndex, !flag2, rowBouds);
        return flag2;
      }

      private void DropToNextPage(PdfStringLayoutResult[] results, int count, string[] row)
      {
        for (int index = 0; index < count; ++index)
          results[index] = new PdfStringLayoutResult()
          {
            m_remainder = row[index],
            m_actualSize = SizeF.Empty
          };
      }

      private float GetAlpha(PdfBrush brush)
      {
        PdfSolidBrush pdfSolidBrush = brush as PdfSolidBrush;
        PdfLinearGradientBrush linearGradientBrush = brush as PdfLinearGradientBrush;
        float alpha = 1f;
        if (pdfSolidBrush != null)
          return (float) pdfSolidBrush.Color.A / (float) byte.MaxValue;
        if (linearGradientBrush == null)
          return alpha;
        PdfColor pdfColor1 = new PdfColor((byte) 0, (byte) 0, (byte) 0);
        PdfColor pdfColor2 = new PdfColor((byte) 0, (byte) 0, (byte) 0);
        PdfColor[] linearColors = linearGradientBrush.LinearColors;
        if (linearColors != null)
        {
          pdfColor1 = linearColors[0];
          pdfColor2 = linearColors[1];
        }
        if (pdfColor1.IsEmpty && pdfColor2.IsEmpty || pdfColor1.A == (byte) 0 && pdfColor2.A == (byte) 0)
          pdfColor1 = linearGradientBrush.InterpolationColors.Colors[0];
        return (float) pdfColor1.A / (float) byte.MaxValue;
      }

      private PdfCellStyle GetCellStyle(int rowIndex, bool isHeader, out bool hasOwnStyle)
      {
        PdfLightTableStyle style = this.Table.Style;
        hasOwnStyle = false;
        PdfCellStyle cellStyle;
        if (isHeader)
        {
          cellStyle = style.HeaderStyle;
          hasOwnStyle = true;
        }
        else
          cellStyle = (rowIndex & 1) <= 0 ? style.DefaultStyle : style.AlternateStyle;
        if (cellStyle == null)
        {
          cellStyle = style.DefaultStyle;
          hasOwnStyle = false;
        }
        return cellStyle;
      }

      private float GetCellWidth(int cellIndex)
      {
        float cellWidth = this.m_cellWidths[cellIndex];
        if (this.m_spanMap != null && this.m_spanMap.Length == this.m_cellWidths.Length)
        {
          int span = this.m_spanMap[cellIndex];
          if (span <= 1)
            return cellWidth;
          int length = this.m_spanMap.Length;
          int num = span + cellIndex;
          float cellSpacing = this.Table.Style.CellSpacing;
          for (int index = cellIndex + 1; index < num && index < length; ++index)
          {
            cellWidth += this.m_cellWidths[index] + cellSpacing;
            this.m_spanMap[index] = -1;
          }
        }
        return cellWidth;
      }

      private PdfLightTableLayoutFormat GetFormat(PdfLayoutFormat format)
      {
        PdfLightTableLayoutFormat format1 = format as PdfLightTableLayoutFormat;
        if (format != null && format1 == null)
          format1 = new PdfLightTableLayoutFormat(format);
        return format1;
      }

      private PdfLightTableLayoutResult GetLayoutResult(LightTableLayouter.PageLayoutResult pageResult)
      {
        return new PdfLightTableLayoutResult(pageResult != null ? pageResult.Page : this.m_currentPage, pageResult != null ? pageResult.Bounds : RectangleF.Empty, pageResult.LastRowIndex, this.m_latestTextResults);
      }

      private string[] GetRow(int startRowIndex, PdfLayoutParams param)
      {
        return this.m_row != null ? this.m_row : this.CropRow(this.Table.GetNextRow(ref startRowIndex));
      }

      private float[] GetWidths(RectangleF bounds)
      {
        int num1 = this.m_endColumn - this.m_startColumn + 1;
        PdfLightTableStyle style = this.Table.Style;
        PdfPen borderPen = style.BorderPen;
        float num2 = borderPen == null ? 0.0f : borderPen.Width;
        if (style.BorderOverlapStyle == PdfBorderOverlapStyle.Inside)
          num2 *= 2f;
        return this.Table.Columns.GetWidths(bounds.Width - style.CellSpacing * (float) (num1 + 1) - num2, this.m_startColumn, this.m_endColumn);
      }

      private bool IsIncomplete(PdfStringLayoutResult[] results)
      {
        bool flag = false;
        if (results == null)
          return true;
        foreach (PdfStringLayoutResult result in results)
        {
          if (result.Remainder != null && result.Remainder != string.Empty)
            return true;
        }
        return flag;
      }

      public void Layout(PdfGraphics graphics, PointF location)
      {
        RectangleF boundaries = new RectangleF(location, SizeF.Empty);
        this.Layout(graphics, boundaries);
      }

      public void Layout(PdfGraphics graphics, RectangleF boundaries)
      {
        if (graphics == null)
          throw new ArgumentNullException(nameof (graphics));
        if ((double) graphics.ClientSize.Height < 0.0)
          boundaries.Y += graphics.ClientSize.Height;
        double width = (double) graphics.ClientSize.Width;
        double x = (double) boundaries.X;
        PdfLayoutParams pdfLayoutParams = new PdfLayoutParams();
        pdfLayoutParams.Bounds = boundaries;
        this.m_currentGraphics = graphics;
        this.LayoutInternal(pdfLayoutParams);
      }

      protected override PdfLayoutResult LayoutInternal(PdfLayoutParams param)
      {
        PdfLightTableLayoutFormat tableLayoutFormat = param != null ? this.GetFormat(param.Format) : throw new ArgumentNullException(nameof (param));
        if (tableLayoutFormat != null)
        {
          this.m_startColumn = tableLayoutFormat.StartColumnIndex;
          this.m_endColumn = tableLayoutFormat.EndColumnIndex;
        }
        if (this.m_endColumn == 0)
          this.m_endColumn = this.Table.Columns.Count - 1;
        if (this.m_endColumn < this.m_startColumn)
          throw new PdfLightTableException("End column index is less than start column index.");
        int count = this.Table.Columns.Count;
        if (this.m_startColumn < 0 || this.m_startColumn >= count || this.m_endColumn >= count || this.m_endColumn - this.m_startColumn > count)
          throw new PdfLightTableException("The selected columns are out of the existing range.");
        this.m_dropIndex = -2;
        this.m_row = (string[]) null;
        this.m_latestTextResults = (PdfStringLayoutResult[]) null;
        this.m_currentPage = param.Page;
        this.m_currentPageBounds = this.m_currentPage != null ? this.m_currentPage.GetClientSize() : this.m_currentGraphics.ClientSize;
        this.m_currentBounds = param.Bounds;
        LightTableLayouter.PageLayoutResult pageResult = (LightTableLayouter.PageLayoutResult) null;
        if ((double) this.m_currentBounds.Width <= 0.0)
        {
          float num = this.m_currentPageBounds.Width - this.m_currentBounds.X;
          this.m_currentBounds.Width = (double) num >= 0.0 ? num : throw new PdfLightTableException("Can't draw table outside of the page.");
        }
        param.Bounds = this.m_currentBounds;
        PdfLightTableStyle style = this.Table.Style;
        this.m_cellSpacing = style.CellSpacing;
        int currentRow = style.HeaderSource == PdfHeaderSource.Rows ? style.HeaderRowCount : 0;
        this.m_cellWidths = this.GetWidths(param.Bounds);
        bool isPageFirst = true;
        while (true)
        {
          do
          {
            bool flag = this.RaiseBeforePageLayout(this.m_currentPage, ref this.m_currentBounds, ref currentRow);
            if (!flag)
            {
              pageResult = this.LayoutOnPage(currentRow, param, isPageFirst);
              LightTableEndPageLayoutEventArgs pageLayoutEventArgs = this.RaisePageLayouted(pageResult);
              flag = pageLayoutEventArgs != null && pageLayoutEventArgs.Cancel;
            }
            if (flag || pageResult.Finish)
              return (PdfLayoutResult) this.GetLayoutResult(pageResult);
            this.m_currentPage = this.GetNextPage(this.m_currentPage);
            this.m_currentPageBounds = this.m_currentPage != null ? this.m_currentPage.GetClientSize() : this.m_currentGraphics.ClientSize;
            isPageFirst = false;
            currentRow = pageResult.LastRowIndex;
            this.m_currentBounds = this.GetPaginateBounds(param);
          }
          while ((double) this.m_currentBounds.Height != 0.0);
          this.m_currentBounds.Y = 0.0f;
        }
      }

      private LightTableLayouter.PageLayoutResult LayoutOnPage(
        int startRowIndex,
        PdfLayoutParams param,
        bool isPageFirst)
      {
        int rowIndex = startRowIndex;
        RectangleF rectangleF1 = this.m_currentBounds;
        if ((double) rectangleF1.Height == 0.0 && this.m_currentPage != null)
          rectangleF1.Height = this.m_currentPageBounds.Height - rectangleF1.Y;
        RectangleF rectangleF2 = rectangleF1;
        PdfLightTableStyle style = this.Table.Style;
        PdfPen borderPen = style.BorderPen;
        if (borderPen != null)
          rectangleF1 = LightTableLayouter.PreserveForBorder(rectangleF1, borderPen, style.BorderOverlapStyle);
        rectangleF1.Height -= this.m_cellSpacing;
        rectangleF1.Width -= this.m_cellSpacing;
        float rowHeight = 0.0f;
        LightTableLayouter.PageLayoutResult pageLayoutResult = new LightTableLayouter.PageLayoutResult();
        bool flag1 = false;
        bool isHeader = style.ShowHeader && (isPageFirst || style.RepeatHeader);
        bool flag2 = style.HeaderSource != PdfHeaderSource.Rows;
        int headerRowCount = style.HeaderRowCount;
        if (isHeader && !flag2)
        {
          if (headerRowCount > 0)
            rowIndex = 0;
          else
            isHeader = false;
        }
        string[] row1 = this.m_row;
        if (isHeader)
          this.m_row = (string[]) null;
        PdfGraphics pdfGraphics = this.m_currentPage != null ? this.m_currentPage.Graphics : this.m_currentGraphics;
        while (true)
        {
          do
          {
            string[] row2;
            if (isHeader & flag2)
            {
              rowIndex = -1;
              this.m_previousRowIndex = -2;
              string[] columnCaptions = this.Table.GetColumnCaptions();
              if (columnCaptions == null)
              {
                isHeader = false;
                rowIndex = startRowIndex;
                this.m_row = row1;
                continue;
              }
              row2 = this.CropRow(columnCaptions);
            }
            else
              row2 = this.GetRow(rowIndex, param);
            bool stop = row2 == null;
            if (row2 != null)
            {
              rectangleF1.Y += this.m_cellSpacing;
              rectangleF1.Height -= this.m_cellSpacing;
              bool flag3 = this.DrawRow(param, ref rowIndex, row2, rectangleF1, out rowHeight, isHeader, out stop);
              rectangleF1.Y += rowHeight;
              rectangleF1.Height -= rowHeight;
              stop |= flag3;
              flag1 = ((flag1 ? 1 : 0) | ((double) rowHeight > 0.0 ? 0 : (startRowIndex == rowIndex | isHeader ? 1 : 0))) != 0;
              stop |= flag1;
            }
            else
              pageLayoutResult.Finish = true;
            if (stop)
            {
              if ((double) rowHeight > 0.0)
                rectangleF1.Y += this.m_cellSpacing;
              if (borderPen != null)
                rectangleF1.Y += borderPen.Width;
              pageLayoutResult.Page = this.m_currentPage;
              pageLayoutResult.FirstRowIndex = startRowIndex;
              pageLayoutResult.LastRowIndex = rowIndex;
              pageLayoutResult.Bounds = rectangleF2;
              pageLayoutResult.Bounds.Height = rectangleF1.Y - rectangleF2.Y;
              RectangleF rectangleF3 = pageLayoutResult.Bounds;
              if (borderPen != null)
              {
                if (style.BorderOverlapStyle == PdfBorderOverlapStyle.Overlap)
                  rectangleF3.Height -= borderPen.Width / 2f;
                rectangleF3 = LightTableLayouter.PreserveForBorder(rectangleF3, borderPen, PdfBorderOverlapStyle.Overlap);
              }
              if (borderPen != null && (double) rectangleF3.Bottom < (double) this.m_currentPageBounds.Height)
              {
                float alpha = (float) borderPen.Color.A / (float) byte.MaxValue;
                pdfGraphics.Save();
                pdfGraphics.SetTransparency(alpha);
                pdfGraphics.DrawRectangle(borderPen, rectangleF3);
                pdfGraphics.Restore();
                goto label_32;
              }
              goto label_32;
            }
          }
          while (!isHeader || !flag2 && rowIndex < headerRowCount);
          isHeader = false;
          rowIndex = startRowIndex;
          this.m_row = row1;
        }
    label_32:
        bool flag4 = param.Format == null || param.Format.Layout == PdfLayoutType.OnePage;
        pageLayoutResult.Finish |= flag4;
        if (flag1 || this.m_row != null & isHeader)
          throw new PdfLightTableException("Can't draw table, because there is not enough space for it.");
        return pageLayoutResult;
      }

      private static RectangleF PreserveForBorder(
        RectangleF bounds,
        PdfPen pen,
        PdfBorderOverlapStyle overlapStyle)
      {
        if (pen != null)
        {
          float width = pen.Width;
          if (overlapStyle == PdfBorderOverlapStyle.Overlap)
          {
            float num = width / 2f;
            bounds.X += num;
            bounds.Y += num;
            bounds.Width -= width;
            bounds.Height -= width;
            return bounds;
          }
          if (overlapStyle != PdfBorderOverlapStyle.Inside)
            throw new ArgumentException("Unsupported overlap style.");
          float num1 = width * 2f;
          bounds.X += width;
          bounds.Y += width;
          bounds.Width -= num1;
          bounds.Height -= num1;
        }
        return bounds;
      }

      private void RaiseAfterCellLayout(
        PdfGraphics graphics,
        int rowIndex,
        int cellIndex,
        RectangleF bounds,
        string value)
      {
        if (!this.Table.RaiseEndCellLayout)
          return;
        this.Table.OnEndCellLayout(new EndCellLayoutEventArgs(graphics, rowIndex, cellIndex, bounds, value));
      }

      private bool RaiseAfterRowLayout(int rowIndex, bool isComplete, RectangleF rowBouds)
      {
        bool flag = false;
        if (this.Table.RaiseEndRowLayout)
        {
          EndRowLayoutEventArgs args = new EndRowLayoutEventArgs(rowIndex, isComplete, rowBouds);
          this.Table.OnEndRowLayout(args);
          flag = args.Cancel;
        }
        return flag;
      }

      private BeginCellLayoutEventArgs RaiseBeforeCellLayout(
        PdfGraphics graphics,
        int rowIndex,
        int cellIndex,
        RectangleF bounds,
        string value)
      {
        BeginCellLayoutEventArgs args = (BeginCellLayoutEventArgs) null;
        if (this.Table.RaiseBeginCellLayout)
        {
          args = new BeginCellLayoutEventArgs(graphics, rowIndex, cellIndex, bounds, value);
          this.Table.OnBeginCellLayout(args);
        }
        return args;
      }

      private bool RaiseBeforePageLayout(
        PdfPage currentPage,
        ref RectangleF currentBounds,
        ref int currentRow)
      {
        bool flag = false;
        if (this.Element.RaiseBeginPageLayout)
        {
          LightTableBeginPageLayoutEventArgs e = new LightTableBeginPageLayoutEventArgs(currentBounds, currentPage, currentRow);
          this.Element.OnBeginPageLayout((BeginPageLayoutEventArgs) e);
          flag = e.Cancel;
          currentBounds = e.Bounds;
          currentRow = e.StartRowIndex;
        }
        return flag;
      }

      private BeginRowLayoutEventArgs RaiseBeforeRowLayout(int rowIndex, PdfCellStyle cellStyle)
      {
        BeginRowLayoutEventArgs args = (BeginRowLayoutEventArgs) null;
        if (this.Table.RaiseBeginRowLayout)
        {
          args = new BeginRowLayoutEventArgs(rowIndex, cellStyle);
          this.Table.OnBeginRowLayout(args);
        }
        return args;
      }

      private LightTableEndPageLayoutEventArgs RaisePageLayouted(
        LightTableLayouter.PageLayoutResult pageResult)
      {
        LightTableEndPageLayoutEventArgs e = (LightTableEndPageLayoutEventArgs) null;
        if (this.Element.RaiseEndPageLayout)
        {
          PdfLightTableLayoutResult layoutResult = this.GetLayoutResult(pageResult);
          int lastRowIndex = pageResult.LastRowIndex;
          if (this.m_row == null)
            --lastRowIndex;
          int firstRowIndex = pageResult.FirstRowIndex;
          int endRow = lastRowIndex;
          e = new LightTableEndPageLayoutEventArgs(layoutResult, firstRowIndex, endRow);
          this.Element.OnEndPageLayout((EndPageLayoutEventArgs) e);
        }
        return e;
      }

      private void ValidateSpanMap()
      {
        if (this.m_spanMap == null)
          return;
        int length = this.m_spanMap.Length;
        for (int index1 = 0; index1 < length; ++index1)
        {
          int span = this.m_spanMap[index1];
          if (span > 1)
          {
            int num = span + index1;
            int index2;
            for (index2 = index1 + 1; index2 < num && index2 < length; ++index2)
              this.m_spanMap[index2] = -1;
            index1 = index2 - 1;
          }
          else if (span < 0)
            throw new PdfLightTableException("Invalid span map.");
        }
      }

      public PdfLightTable Table => this.Element as PdfLightTable;

      private class PageLayoutResult
      {
        public RectangleF Bounds;
        public bool Finish;
        public int FirstRowIndex;
        public int LastRowIndex;
        public PdfPage Page;
      }
    }
}
