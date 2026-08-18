
// Type: Intermech.Search.iGrid.iGridExtensions
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TenTec.Windows.iGridLib;


namespace Intermech.Search.iGrid;

public static class iGridExtensions
{
  public static void SetSelectedForAllCells(this iGRow row, bool selected)
  {
    if (row == null)
      throw new ArgumentNullException(nameof (row));
    foreach (iGCell cell in (IEnumerable) row.Cells)
      cell.Selected = selected;
  }

  public static bool IsAnyCellSelected(this iGRow row)
  {
    if (row == null)
      throw new ArgumentNullException(nameof (row));
    return row.Cells.Cast<iGCell>().Any<iGCell>((Func<iGCell, bool>) (o => o.Selected));
  }

  public static Rectangle GetCellBounds(this TenTec.Windows.iGridLib.iGrid grid, iGCell cell)
  {
    if (grid == null)
      throw new ArgumentNullException(nameof (grid));
    Rectangle cellBounds = cell != null ? cell.TextBounds : throw new ArgumentNullException(nameof (cell));
    Rectangle cellsAreaBounds = grid.CellsAreaBounds;
    if (cellBounds.Right > cellsAreaBounds.Right)
      cellBounds.Width -= cellBounds.Right - cellsAreaBounds.Right;
    if (cellBounds.Bottom > cellsAreaBounds.Bottom)
      cellBounds.Height -= cellBounds.Bottom - cellsAreaBounds.Bottom;
    cellBounds.Offset(grid.Location);
    return cellBounds;
  }

  public static NodeColumnSortOrder ConvertToNodeColumnSortOrder(this iGSortOrder sortOrder)
  {
    if (sortOrder == iGSortOrder.Ascending)
      return NodeColumnSortOrder.Ascending;
    return sortOrder != iGSortOrder.Descending ? NodeColumnSortOrder.None : NodeColumnSortOrder.Descending;
  }

  public static string GetCurrentColumnText(TenTec.Windows.iGridLib.iGrid grid)
  {
    return grid.CurCell == null || grid.CurCell.Col == null ? (string) null : grid.CurCell.Col.Text as string;
  }

  public static IEnumerable<Tuple<int, int, string>> GetCellValues(
    TenTec.Windows.iGridLib.iGrid grid,
    bool currentColumnOnly,
    bool fromBeggining,
    bool backward)
  {
    iGCell startCell = fromBeggining && !currentColumnOnly || grid.CurCell == null ? grid.Cells.Cast<iGCell>().FirstOrDefault<iGCell>() : grid.CurCell;
    if (startCell != null)
    {
      iGRow startRow = grid.Rows[startCell.RowIndex];
      int i;
      if (currentColumnOnly)
      {
        if (backward)
        {
          for (i = startRow.Index - 1; i >= 0; --i)
            yield return new Tuple<int, int, string>(i, startCell.ColIndex, grid.Cells[i, startCell.ColIndex].Text);
          for (i = grid.Rows.Count - 1; i >= startRow.Index; --i)
            yield return new Tuple<int, int, string>(i, startCell.ColIndex, grid.Cells[i, startCell.ColIndex].Text);
        }
        else
        {
          for (i = startRow.Index + 1; i < grid.Rows.Count; ++i)
            yield return new Tuple<int, int, string>(i, startCell.ColIndex, grid.Cells[i, startCell.ColIndex].Text);
          for (i = 0; i <= startRow.Index; ++i)
            yield return new Tuple<int, int, string>(i, startCell.ColIndex, grid.Cells[i, startCell.ColIndex].Text);
        }
      }
      else if (backward)
      {
        for (i = startCell.ColIndex - 1; i >= 0; --i)
          yield return new Tuple<int, int, string>(startRow.Index, i, startRow.Cells[i].Text);
        for (i = startRow.Index - 1; i >= 0; --i)
        {
          foreach (iGCell iGcell in grid.Rows[i].Cells.Cast<iGCell>().Reverse<iGCell>())
            yield return new Tuple<int, int, string>(i, iGcell.ColIndex, iGcell.Text);
        }
        for (i = grid.Rows.Count - 1; i >= startRow.Index; --i)
        {
          foreach (iGCell iGcell in grid.Rows[i].Cells.Cast<iGCell>().Reverse<iGCell>())
            yield return new Tuple<int, int, string>(i, iGcell.ColIndex, iGcell.Text);
        }
        for (i = grid.Cols.Count - 1; i >= startCell.ColIndex; --i)
          yield return new Tuple<int, int, string>(startRow.Index, i, startRow.Cells[i].Text);
      }
      else
      {
        for (i = startCell.ColIndex + 1; i < grid.Cols.Count; ++i)
          yield return new Tuple<int, int, string>(startRow.Index, i, startRow.Cells[i].Text);
        for (i = startRow.Index + 1; i < grid.Rows.Count; ++i)
        {
          foreach (iGCell cell in (IEnumerable) grid.Rows[i].Cells)
            yield return new Tuple<int, int, string>(i, cell.ColIndex, cell.Text);
        }
        for (i = 0; i < startRow.Index; ++i)
        {
          foreach (iGCell cell in (IEnumerable) grid.Rows[i].Cells)
            yield return new Tuple<int, int, string>(i, cell.ColIndex, cell.Text);
        }
        for (i = 0; i <= startCell.ColIndex; ++i)
          yield return new Tuple<int, int, string>(startRow.Index, i, startRow.Cells[i].Text);
      }
      startRow = (iGRow) null;
    }
  }

  public static void SelectCells(TenTec.Windows.iGridLib.iGrid grid, Tuple<int, int>[] cells)
  {
    Tuple<int, int> tuple = ((IEnumerable<Tuple<int, int>>) cells).LastOrDefault<Tuple<int, int>>();
    foreach (Tuple<int, int> cell1 in cells)
    {
      iGCell cell2 = grid.Cells[cell1.Item1, cell1.Item2];
      if (cell2 != null)
      {
        cell2.Selected = true;
        if (tuple == cell1)
          grid.CurCell = cell2;
      }
    }
  }
}
