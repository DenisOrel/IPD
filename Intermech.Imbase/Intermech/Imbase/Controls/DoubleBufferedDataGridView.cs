// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Controls.DoubleBufferedDataGridView
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using GridViewExtensions;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Controls;

internal class DoubleBufferedDataGridView : DataGridView, IFilterTarget
{
  private DataGridComparer _columnSorter;
  internal IFilterTarget _filterTarget;

  [Category("Behavior")]
  [Description("The maximum number of columns that may be sorted by or 0 for no limit")]
  [DefaultValue(0)]
  [Browsable(true)]
  public int MaxSortColumns
  {
    get => this._columnSorter.MaxSortColumns;
    set
    {
      this._columnSorter.MaxSortColumns = value >= 0 ? value : throw new ArgumentOutOfRangeException("MaxSortColumns must be >= 0, set to 0 for no limit");
    }
  }

  public bool SortChanged { get; set; }

  public string SortOrderDescription => this._columnSorter.SortOrderDescription;

  internal void SetFilterTarget(IFilterTarget target) => this._filterTarget = target;

  public string RowFilter
  {
    set
    {
      if (this._filterTarget == null)
        return;
      this._filterTarget.RowFilter = value;
    }
    get => this._filterTarget != null ? this._filterTarget.RowFilter : string.Empty;
  }

  public bool CanSetFilter => this._filterTarget != null;

  private void SetFocusedRecordColor(bool isFocused)
  {
    if (isFocused)
    {
      this.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
      this.DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;
    }
    else
    {
      this.DefaultCellStyle.SelectionBackColor = SystemColors.ControlLight;
      this.DefaultCellStyle.SelectionForeColor = SystemColors.WindowText;
    }
  }

  public DoubleBufferedDataGridView()
  {
    this.DoubleBuffered = true;
    this._columnSorter = new DataGridComparer((DataGridView) this);
    this.SetFocusedRecordColor(false);
    this.SortChanged = false;
  }

  protected override void OnEnter(EventArgs e)
  {
    this.SetFocusedRecordColor(true);
    base.OnEnter(e);
  }

  protected override void OnLeave(EventArgs e)
  {
    this.SetFocusedRecordColor(false);
    base.OnLeave(e);
  }

  protected override void OnColumnHeaderMouseClick(DataGridViewCellMouseEventArgs e)
  {
    SortOrder sortOrder = this._columnSorter.SetSortColumn(e.ColumnIndex, Control.ModifierKeys);
    if (this.DataSource != null)
    {
      if (!(this.DataSource is DataView dataView) && this.DataSource is DataTable dataSource)
        dataView = dataSource.DefaultView;
      if (dataView != null)
        this._columnSorter.SetSortString(dataView);
    }
    else
      this.Sort((IComparer) this._columnSorter);
    this.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.Programmatic;
    this.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = sortOrder;
    base.OnColumnHeaderMouseClick(e);
    this.SortChanged = true;
  }

  protected override void OnDataBindingComplete(DataGridViewBindingCompleteEventArgs e)
  {
    base.OnDataBindingComplete(e);
  }

  public string GetSortString() => this._columnSorter.SortString;

  internal void ClearSort() => this._columnSorter.Clear();

  public void SetSortString(string value) => this._columnSorter.SortString = value;
}
