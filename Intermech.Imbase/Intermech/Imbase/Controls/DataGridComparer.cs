// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Controls.DataGridComparer
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Controls;

public class DataGridComparer : IComparer
{
  private DataGridView _grid;
  private string _sort;
  private List<DataGridComparer.SortColumnDefn> _sortedColumns;
  private int _maxSortColumns;

  public int MaxSortColumns
  {
    get => this._sortedColumns.Capacity;
    set
    {
      if (this._sortedColumns.Count > value)
        this._sortedColumns.RemoveRange(value - 1, this._sortedColumns.Count);
      this._sortedColumns.Capacity = value;
    }
  }

  public string SortOrderDescription
  {
    get
    {
      StringBuilder stringBuilder = new StringBuilder("Sorted by ");
      foreach (DataGridComparer.SortColumnDefn sortedColumn in this._sortedColumns)
      {
        stringBuilder.Append(this._grid.Columns[(int) sortedColumn.colNum].HeaderText);
        stringBuilder.Append(sortedColumn.ascending ? " ASC, " : " DESC, ");
      }
      stringBuilder.Length -= 2;
      return stringBuilder.ToString();
    }
  }

  public string SortString
  {
    get => this._sort;
    set
    {
      if (!(this._sort != value))
        return;
      string[] strArray = value.Split(new char[4]
      {
        ',',
        ' ',
        '[',
        ']'
      }, StringSplitOptions.RemoveEmptyEntries);
      this._sortedColumns.Clear();
      int length = strArray.Length;
      DataColumnCollection columns1 = (this._grid.DataSource as DataView).Table.Columns;
      DataGridViewColumnCollection columns2 = this._grid.Columns;
      for (int index = 0; index < length; index += 2)
      {
        if (columns1.IndexOf(strArray[index]) > -1)
        {
          DataGridComparer.SortColumnDefn sortColumnDefn = new DataGridComparer.SortColumnDefn(columns2[strArray[index]].Index, SortOrder.Ascending);
          if (strArray[index + 1] == "DESC")
            sortColumnDefn.ascending = false;
          this._sortedColumns.Add(sortColumnDefn);
        }
      }
      this.SetSortString(this._grid.DataSource as DataView);
    }
  }

  public DataGridComparer(DataGridView datagrid)
  {
    this._grid = datagrid;
    this._maxSortColumns = 0;
    this._sortedColumns = new List<DataGridComparer.SortColumnDefn>(this._maxSortColumns);
  }

  public int Compare(DataGridViewCellCollection lhs, DataGridViewCellCollection rhs)
  {
    foreach (DataGridComparer.SortColumnDefn sortedColumn in this._sortedColumns)
    {
      int num = Comparer<object>.Default.Compare(lhs[(int) sortedColumn.colNum].Value, rhs[(int) sortedColumn.colNum].Value);
      if (num != 0)
        return sortedColumn.ascending ? num : -num;
    }
    return 0;
  }

  public SortOrder SetSortColumn(int columnIndex, Keys ModifierKeys)
  {
    bool keepSamePriority = (ModifierKeys & Keys.Control) == Keys.Control;
    DataGridComparer.SortColumnDefn sortColumnDefn = new DataGridComparer.SortColumnDefn();
    bool flag = false;
    if (this._sortedColumns.Count > 0 && !keepSamePriority)
    {
      foreach (DataGridComparer.SortColumnDefn sortedColumn in this._sortedColumns)
      {
        this._grid.Columns[(int) sortedColumn.colNum].HeaderCell.SortGlyphDirection = SortOrder.None;
        if ((int) sortedColumn.colNum == columnIndex)
        {
          sortColumnDefn = sortedColumn;
          flag = true;
        }
      }
      this._sortedColumns.Clear();
      if (flag)
        this._sortedColumns.Add(sortColumnDefn);
    }
    int index = this._sortedColumns.FindIndex((Predicate<DataGridComparer.SortColumnDefn>) (cd => (int) cd.colNum == columnIndex));
    if (index != -1)
      return this.ReverseSort(keepSamePriority, index);
    if (this._maxSortColumns > 0 && this._sortedColumns.Count == this._sortedColumns.Capacity)
      this._sortedColumns.RemoveAt(this._sortedColumns.Count - 1);
    if (columnIndex == -1)
      return SortOrder.None;
    sortColumnDefn = new DataGridComparer.SortColumnDefn(columnIndex, SortOrder.Ascending);
    if (keepSamePriority)
      this._sortedColumns.Add(sortColumnDefn);
    else
      this._sortedColumns.Insert(0, sortColumnDefn);
    this._grid.Columns[(int) sortColumnDefn.colNum].SortMode = DataGridViewColumnSortMode.Programmatic;
    this._grid.Columns[(int) sortColumnDefn.colNum].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
    return SortOrder.Ascending;
  }

  internal void Clear()
  {
    this._sort = string.Empty;
    this._sortedColumns.Clear();
  }

  internal void SetSortString(DataView dataView)
  {
    StringBuilder stringBuilder = new StringBuilder();
    foreach (DataGridComparer.SortColumnDefn sortedColumn in this._sortedColumns)
    {
      DataGridViewColumn column = this._grid.Columns[(int) sortedColumn.colNum];
      if (!string.IsNullOrEmpty(column.DataPropertyName))
      {
        stringBuilder.Append($"[{column.DataPropertyName}]");
        stringBuilder.Append(sortedColumn.ascending ? " ASC, " : " DESC, ");
      }
    }
    if (stringBuilder.Length > 2)
      stringBuilder.Length -= 2;
    this._sort = stringBuilder.ToString();
    dataView.Sort = this._sort;
    foreach (DataGridComparer.SortColumnDefn sortedColumn in this._sortedColumns)
    {
      DataGridViewColumn column = this._grid.Columns[(int) sortedColumn.colNum];
      column.SortMode = DataGridViewColumnSortMode.Programmatic;
      column.HeaderCell.SortGlyphDirection = sortedColumn.ascending ? SortOrder.Ascending : SortOrder.Descending;
    }
  }

  private SortOrder ReverseSort(bool keepSamePriority, int sortPriority)
  {
    DataGridComparer.SortColumnDefn sortedColumn = this._sortedColumns[sortPriority];
    if (sortPriority == 0 | keepSamePriority)
    {
      sortedColumn.ascending = !sortedColumn.ascending;
      this._sortedColumns[sortPriority] = sortedColumn;
      SortOrder sortOrder = sortedColumn.ascending ? SortOrder.Ascending : SortOrder.Descending;
      this._grid.Columns[(int) sortedColumn.colNum].HeaderCell.SortGlyphDirection = sortOrder;
      return sortOrder;
    }
    for (int index = sortPriority; index > 0; --index)
      this._sortedColumns[index] = this._sortedColumns[index - 1];
    sortedColumn.ascending = true;
    this._sortedColumns[0] = sortedColumn;
    SortOrder sortOrder1 = SortOrder.Ascending;
    this._grid.Columns[(int) sortedColumn.colNum].HeaderCell.SortGlyphDirection = sortOrder1;
    return sortOrder1;
  }

  public int Compare(object x, object y)
  {
    return this.Compare((x as DataGridViewRow).Cells, (y as DataGridViewRow).Cells);
  }

  private struct SortColumnDefn
  {
    internal short colNum;
    internal bool ascending;

    internal SortColumnDefn(int columnNum, SortOrder sortOrder)
    {
      this.colNum = Convert.ToInt16(columnNum);
      this.ascending = sortOrder != SortOrder.Descending;
    }
  }
}
