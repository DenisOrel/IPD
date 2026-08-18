// Decompiled with JetBrains decompiler
// Type: GridViewExtensions.GridFiltersControl
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using GridViewExtensions.GridFilterFactories;
using GridViewExtensions.GridFilters;
using Intermech.Imbase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace GridViewExtensions;

internal class GridFiltersControl : UserControl, ISupportInitialize
{
  private Dictionary<DataGridViewColumn, IGridFilter> _columnToGridFilterList;
  private IGridFilterFactory _filterFactory;
  private LogicalOperators _operator;
  private RefreshMode _autoRefreshMode;
  private bool _refreshDisabled;
  private string _lastRowFilter = "";
  private TextBox _refBox;
  private Label _lblFilter;
  private System.ComponentModel.Container components;
  private DataGridView _grid;
  private FilterErrorModes _messageErrorMode = FilterErrorModes.General;
  private FilterErrorModes _consoleErrorMode;
  private int _initCounter;
  private StringDictionary _baseFilters;
  private bool _baseFilterEnabled = true;
  private LogicalOperators _baseFilterOperator;

  internal event EventHandler AfterFiltersChanged;

  internal event EventHandler BeforeFiltersChanging;

  internal event GridFilterEventHandler GridFilterBound;

  internal event GridFilterEventHandler GridFilterUnbound;

  internal GridFiltersControl()
  {
    this.InitializeComponent();
    this._columnToGridFilterList = new Dictionary<DataGridViewColumn, IGridFilter>();
    this._baseFilters = new StringDictionary();
    this.FilterFactory = (IGridFilterFactory) new DefaultGridFilterFactory();
    this.RecreateGridFilters();
  }

  private void InitializeComponent()
  {
    this._refBox = new TextBox();
    this._lblFilter = new Label();
    this.SuspendLayout();
    this._refBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this._refBox.Location = new Point(344, 0);
    this._refBox.Name = "_refBox";
    this._refBox.Size = new Size(40, 20);
    this._refBox.TabIndex = 0;
    this._refBox.Text = "textBox1";
    this._refBox.Visible = false;
    this._lblFilter.Dock = DockStyle.Left;
    this._lblFilter.Location = new Point(0, 0);
    this._lblFilter.Name = "_lblFilter";
    this._lblFilter.Size = new Size(100, 24);
    this._lblFilter.TabIndex = 1;
    this._lblFilter.Text = "Filter";
    this._lblFilter.TextAlign = ContentAlignment.MiddleLeft;
    this.Controls.Add((Control) this._lblFilter);
    this.Controls.Add((Control) this._refBox);
    this.Name = nameof (GridFiltersControl);
    this.Size = new Size(384, 24);
    this.ResumeLayout(false);
  }

  public override RightToLeft RightToLeft
  {
    get => base.RightToLeft;
    set
    {
      try
      {
        ++this._initCounter;
        base.RightToLeft = value;
      }
      finally
      {
        --this._initCounter;
      }
      this.RecreateGridFilters();
    }
  }

  protected override void Dispose(bool disposing)
  {
    this.DataGridView = (DataGridView) null;
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  protected override void OnResize(EventArgs e)
  {
    base.OnResize(e);
    this.RepositionGridFilters();
  }

  protected override void OnRightToLeftChanged(EventArgs e)
  {
    base.OnRightToLeftChanged(e);
    this.RepositionGridFilters();
  }

  internal DataGridView DataGridView
  {
    get => this._grid;
    set
    {
      if (this._grid != null)
      {
        this._grid.DataSourceChanged -= new EventHandler(this.OnDataSourceChanged);
        this._grid.DataMemberChanged -= new EventHandler(this.OnDataSourceChanged);
        this._grid.ColumnWidthChanged -= new DataGridViewColumnEventHandler(this.OnGridColumnsChanged);
        this._grid.ColumnDisplayIndexChanged -= new DataGridViewColumnEventHandler(this.OnGridColumnsChanged);
        this._grid.ColumnAdded -= new DataGridViewColumnEventHandler(this.OnGridColumnsAddedRemoved);
        this._grid.ColumnRemoved -= new DataGridViewColumnEventHandler(this.OnGridColumnsAddedRemoved);
        this._grid.ColumnStateChanged -= new DataGridViewColumnStateChangedEventHandler(this.OnGridColumnsStateChanged);
        this._grid.Scroll -= new ScrollEventHandler(this.OnGridScroll);
      }
      this._grid = value;
      if (this._grid != null)
      {
        this._grid.DataSourceChanged += new EventHandler(this.OnDataSourceChanged);
        this._grid.DataMemberChanged += new EventHandler(this.OnDataSourceChanged);
        this._grid.ColumnWidthChanged += new DataGridViewColumnEventHandler(this.OnGridColumnsChanged);
        this._grid.ColumnDisplayIndexChanged += new DataGridViewColumnEventHandler(this.OnGridColumnsChanged);
        this._grid.ColumnAdded += new DataGridViewColumnEventHandler(this.OnGridColumnsAddedRemoved);
        this._grid.ColumnRemoved += new DataGridViewColumnEventHandler(this.OnGridColumnsAddedRemoved);
        this._grid.ColumnStateChanged += new DataGridViewColumnStateChangedEventHandler(this.OnGridColumnsStateChanged);
        this._grid.Scroll += new ScrollEventHandler(this.OnGridScroll);
      }
      this.RecreateGridFilters();
    }
  }

  internal RefreshMode AutoRefreshMode
  {
    get => this._autoRefreshMode;
    set
    {
      this._autoRefreshMode = value;
      this.RecreateRowFilter();
    }
  }

  internal string FilterText
  {
    get => this._lblFilter.Text;
    set => this._lblFilter.Text = value;
  }

  internal IGridFilterFactory FilterFactory
  {
    get => this._filterFactory;
    set
    {
      if (this._filterFactory != null)
        this._filterFactory.Changed -= new EventHandler(this.OnFilterFactoryChanged);
      this._filterFactory = value;
      if (this._filterFactory == null)
        this._filterFactory = (IGridFilterFactory) new DefaultGridFilterFactory();
      this._filterFactory.Changed += new EventHandler(this.OnFilterFactoryChanged);
      this.RecreateGridFilters();
    }
  }

  internal LogicalOperators Operator
  {
    get => this._operator;
    set
    {
      this._operator = value;
      this.RecreateRowFilter();
    }
  }

  internal bool FilterTextVisible
  {
    get => this._lblFilter.Visible;
    set => this._lblFilter.Visible = value;
  }

  internal FilterErrorModes MessageErrorMode
  {
    get => this._messageErrorMode;
    set => this._messageErrorMode = value;
  }

  internal FilterErrorModes ConsoleErrorMode
  {
    get => this._consoleErrorMode;
    set => this._consoleErrorMode = value;
  }

  internal StringDictionary BaseFilters => this._baseFilters;

  internal LogicalOperators BaseFilterOperator
  {
    get => this._baseFilterOperator;
    set
    {
      this._baseFilterOperator = value;
      this.RecreateRowFilter();
    }
  }

  internal bool BaseFilterEnabled
  {
    get => this._baseFilterEnabled;
    set
    {
      this._baseFilterEnabled = value;
      this.RecreateRowFilter();
    }
  }

  internal string CurrentTableBaseFilter
  {
    get => !this.HasView ? (string) null : this._baseFilters[this.GetTableName()];
    set
    {
      if (!this.HasView)
        return;
      this._baseFilters[this.GetTableName()] = value;
      this.RecreateRowFilter();
    }
  }

  internal void ClearFilters()
  {
    try
    {
      this._refreshDisabled = true;
      foreach (IGridFilter gridFilter in this._columnToGridFilterList.Values)
        gridFilter.Clear();
    }
    finally
    {
      this._refreshDisabled = false;
    }
    this.RecreateRowFilter();
  }

  internal void SetFilters(ConditionItem[] filters)
  {
    try
    {
      this._refreshDisabled = true;
      for (int index = 0; index < this._grid.Columns.Count; ++index)
      {
        DataGridViewColumn column = this._grid.Columns[index];
        if (column.Tag != null)
        {
          int int32 = Convert.ToInt32(column.Tag);
          ConditionItem conitionItem = this.FindConitionItem(filters, int32);
          IGridFilter columnToGridFilter = this._columnToGridFilterList[column];
          if (conitionItem != null)
            columnToGridFilter.SetFilter(conitionItem);
          else
            columnToGridFilter.Clear();
        }
      }
    }
    finally
    {
      this._refreshDisabled = false;
    }
    this.RecreateRowFilter();
  }

  private ConditionItem FindConitionItem(ConditionItem[] filters, int attId)
  {
    if (filters == null || filters.Length == 0)
      return (ConditionItem) null;
    int length = filters.Length;
    for (int index = 0; index < length; ++index)
    {
      if (filters[index].AttId == attId)
        return filters[index];
    }
    return (ConditionItem) null;
  }

  internal ConditionItem[] GetFilters()
  {
    List<ConditionItem> conditionItemList = new List<ConditionItem>();
    for (int index = 0; index < this._grid.Columns.Count; ++index)
    {
      DataGridViewColumn column = this._grid.Columns[index];
      if (column.Tag != null)
      {
        int int32 = Convert.ToInt32(column.Tag);
        IGridFilter columnToGridFilter = this._columnToGridFilterList[column];
        if (columnToGridFilter.HasFilter)
        {
          ConditionItem filter = columnToGridFilter.GetFilter($"[{column.DataPropertyName}]");
          if (filter != null)
          {
            filter.AttId = int32;
            conditionItemList.Add(filter);
          }
        }
      }
    }
    return conditionItemList.ToArray();
  }

  internal void RefreshFilters()
  {
    this._lastRowFilter = "_";
    this.RecreateRowFilter(true);
  }

  internal GridFilterCollection GetGridFilters()
  {
    return this._grid.Columns == null || this._columnToGridFilterList == null ? (GridFilterCollection) null : new GridFilterCollection((IList) this._grid.Columns, this._columnToGridFilterList);
  }

  internal static IBindingListView GetViewFromDataSource(object dataSource, string dataMember)
  {
    switch (dataSource)
    {
      case null:
        return (IBindingListView) null;
      case IBindingListView _:
        return dataSource as IBindingListView;
      case DataTable _:
        return (IBindingListView) (dataSource as DataTable).DefaultView;
      case DataSet _:
        DataTable table = (dataSource as DataSet).Tables[dataMember];
        return table != null ? (IBindingListView) table.DefaultView : (IBindingListView) null;
      default:
        return (IBindingListView) null;
    }
  }

  private bool HasView
  {
    get
    {
      return this._grid != null && this._grid.DataSource != null && (this._grid.DataSource is DataTable || this._grid.DataSource is DataView || this._grid.DataSource is BindingSource || this._grid.DataSource is IBindingListView);
    }
  }

  private void SetRowFilter(string rowFilter)
  {
    this.OnBeforeFiltersChanging(EventArgs.Empty);
    try
    {
      if (this._grid == null || this._grid.DataSource == null)
        return;
      if (this._grid is IFilterTarget grid && grid.CanSetFilter)
        grid.RowFilter = rowFilter;
      else if (this._grid.DataSource is DataTable)
        ((DataTable) this._grid.DataSource).DefaultView.RowFilter = rowFilter;
      else if (this._grid.DataSource is DataView)
        ((DataView) this._grid.DataSource).RowFilter = rowFilter;
      else if (this._grid.DataSource is BindingSource)
      {
        ((BindingSource) this._grid.DataSource).Filter = rowFilter;
      }
      else
      {
        if (!(this._grid.DataSource is IBindingListView))
          return;
        ((IBindingListView) this._grid.DataSource).Filter = rowFilter;
      }
    }
    finally
    {
      this.OnAfterFiltersChanged(EventArgs.Empty);
    }
  }

  private string GetTableName()
  {
    if (this._grid != null && this._grid.DataSource != null)
    {
      string name = (string) null;
      if (this.GetDataSourceName(this._grid.DataSource, ref name))
        return name;
      if (this._grid.DataSource is BindingSource)
        return this.GetDataSourceName(((BindingSource) this._grid.DataSource).DataSource, ref name) ? name : ((BindingSource) this._grid.DataSource).DataMember;
      if (this._grid.DataSource is IBindingListView)
        return this._grid.DataSource.GetType().Name;
    }
    return (string) null;
  }

  private bool GetDataSourceName(object dataSource, ref string name)
  {
    if (this._grid.DataSource is DataTable)
    {
      name = ((DataTable) this._grid.DataSource).TableName;
      return true;
    }
    if (this._grid.DataSource is DataView)
    {
      name = ((DataView) this._grid.DataSource).Table.TableName;
      return true;
    }
    if (!(this._grid.DataSource is IBindingListView))
      return false;
    name = this._grid.DataSource.GetType().Name;
    return true;
  }

  private void RecreateGridFilters()
  {
    if (this._initCounter > 0)
      return;
    foreach (DataGridViewColumn key in this._columnToGridFilterList.Keys)
    {
      IGridFilter columnToGridFilter = this._columnToGridFilterList[key];
      columnToGridFilter.Changed -= new EventHandler(this.OnFilterChanged);
      columnToGridFilter.FilterControl.KeyPress -= new KeyPressEventHandler(this.OnFilterControlKeyPress);
      columnToGridFilter.FilterControl.Leave -= new EventHandler(this.OnFilterControlLeave);
      if (this.Controls.Contains(columnToGridFilter.FilterControl))
      {
        this.Controls.Remove(columnToGridFilter.FilterControl);
        columnToGridFilter.FilterControl.Dispose();
      }
      this.OnGridFilterUnbound(new GridFilterEventArgs(key, columnToGridFilter));
    }
    this._columnToGridFilterList.Clear();
    this.Height = this._refBox.Height;
    if (this._grid == null)
      return;
    int rowHeadersWidth = this._grid.RowHeadersVisible ? this._grid.RowHeadersWidth : 0;
    this._lblFilter.Width = rowHeadersWidth;
    if (!this.HasView)
    {
      this._refBox.Visible = true;
      this._refBox.Left = rowHeadersWidth + 1;
      this._refBox.Width = this.Width - rowHeadersWidth - 1;
    }
    else
    {
      this._refBox.Visible = false;
      this._filterFactory.BeginGridFilterCreation();
      try
      {
        for (int index = 0; index < this._grid.Columns.Count; ++index)
        {
          DataGridViewColumn column = this._grid.Columns[index];
          System.Type valueType = column.ValueType;
          IGridFilter gridFilter = column.Visible ? this._filterFactory.CreateGridFilter(column) : (IGridFilter) new EmptyGridFilter();
          if (!gridFilter.UseCustomFilterPlacement)
          {
            gridFilter.FilterControl.Top = 0;
            gridFilter.FilterControl.Height = this.Height;
            gridFilter.FilterControl.Visible = false;
            this.Controls.Add(gridFilter.FilterControl);
            gridFilter.FilterControl.BringToFront();
          }
          gridFilter.Changed += new EventHandler(this.OnFilterChanged);
          gridFilter.FilterControl.KeyPress += new KeyPressEventHandler(this.OnFilterControlKeyPress);
          gridFilter.FilterControl.Leave += new EventHandler(this.OnFilterControlLeave);
          this._columnToGridFilterList.Add(column, gridFilter);
          this.OnGridFilterBound(new GridFilterEventArgs(column, gridFilter));
        }
      }
      finally
      {
        this._filterFactory.EndGridFilterCreation();
      }
      this.RepositionGridFilters();
    }
  }

  private void RepositionGridFilters()
  {
    if (this._initCounter > 0 || this._grid == null || this._grid.Columns == null)
      return;
    if (this._grid.Columns.Count == 0)
      return;
    try
    {
      this.SuspendLayout();
      int rowHeadersWidth = this._grid.RowHeadersVisible ? this._grid.RowHeadersWidth : 0;
      int num1 = this._grid.RowHeadersVisible ? this._grid.RowHeadersWidth - 1 : 0;
      int num2 = rowHeadersWidth;
      if (num1 > 0)
      {
        this._lblFilter.Width = num1;
        this._lblFilter.Visible = true;
        ++num2;
        if (base.RightToLeft == RightToLeft.Yes)
        {
          if (this._lblFilter.Dock != DockStyle.Right)
            this._lblFilter.Dock = DockStyle.Right;
        }
        else if (this._lblFilter.Dock != DockStyle.Left)
          this._lblFilter.Dock = DockStyle.Left;
      }
      else if (this._lblFilter.Visible)
        this._lblFilter.Visible = false;
      List<DataGridViewColumn> sortedColumns = this.SortedColumns;
      for (int index = 0; index < sortedColumns.Count; ++index)
      {
        DataGridViewColumn key = sortedColumns[index];
        if (this._columnToGridFilterList.ContainsKey(key))
        {
          IGridFilter columnToGridFilter = this._columnToGridFilterList[key];
          if (columnToGridFilter != null && !columnToGridFilter.UseCustomFilterPlacement)
          {
            if (!key.Visible)
            {
              if (columnToGridFilter.FilterControl.Visible)
              {
                columnToGridFilter.FilterControl.Visible = false;
                continue;
              }
              continue;
            }
            int x = num2 - this._grid.HorizontalScrollingOffset;
            int width = key.Width + (index == 0 ? 1 : 0);
            if (x < rowHeadersWidth)
            {
              width -= rowHeadersWidth - x;
              x = rowHeadersWidth;
            }
            if (x + width > this.Width)
              width = this.Width - x;
            if (width < 4)
            {
              if (columnToGridFilter.FilterControl.Visible)
                columnToGridFilter.FilterControl.Visible = false;
            }
            else
            {
              if (base.RightToLeft == RightToLeft.Yes)
                x = this.Width - x - width;
              if (columnToGridFilter.FilterControl.Left != x || columnToGridFilter.FilterControl.Width != width)
                columnToGridFilter.FilterControl.SetBounds(x, 0, width, 0, BoundsSpecified.X | BoundsSpecified.Width);
              if (!columnToGridFilter.FilterControl.Visible)
                columnToGridFilter.FilterControl.Visible = true;
            }
          }
        }
        num2 += key.Width + (index == 0 ? 1 : 0);
      }
    }
    finally
    {
      this.ResumeLayout();
    }
    this.RecreateRowFilter();
    this.Invalidate();
  }

  private void RecreateRowFilter() => this.RecreateRowFilter(false);

  private void RecreateRowFilter(bool ignoreAutoRefresh)
  {
    if (this._refreshDisabled || this._columnToGridFilterList.Count == 0 || this._initCounter > 0 || this._autoRefreshMode == RefreshMode.Off && !ignoreAutoRefresh)
      return;
    try
    {
      string str1 = this._operator == LogicalOperators.And ? " And " : " Or ";
      string rowFilter;
      switch (this._operator)
      {
        case LogicalOperators.And:
        case LogicalOperators.Or:
          rowFilter = "";
          using (List<DataGridViewColumn>.Enumerator enumerator = this.SortedColumns.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              DataGridViewColumn current = enumerator.Current;
              if (this._columnToGridFilterList.ContainsKey(current))
              {
                IGridFilter columnToGridFilter = this._columnToGridFilterList[current];
                if (columnToGridFilter == null)
                  return;
                if (columnToGridFilter.HasFilter && current.Visible)
                {
                  string filterText = columnToGridFilter.GetFilterText($"[{current.DataPropertyName}]");
                  rowFilter = rowFilter + (rowFilter.Length <= 0 || filterText.Length <= 0 ? "" : str1) + filterText;
                }
              }
            }
            break;
          }
        default:
          rowFilter = "";
          break;
      }
      string currentTableBaseFilter = this.CurrentTableBaseFilter;
      if ((currentTableBaseFilter == null ? 0 : (currentTableBaseFilter.Length > 0 ? 1 : 0)) != 0 && this._baseFilterEnabled)
      {
        string str2 = this._baseFilterOperator == LogicalOperators.And ? " And " : " Or ";
        if (rowFilter.Length > 0)
          rowFilter = $"({rowFilter}){str2}({this.CurrentTableBaseFilter})";
        else
          rowFilter += this.CurrentTableBaseFilter;
      }
      if (!(this._lastRowFilter != rowFilter))
        return;
      this._lastRowFilter = rowFilter;
      this.SetRowFilter(rowFilter);
    }
    catch (Exception ex)
    {
      string messageFromMode1 = this.GetMessageFromMode(this._consoleErrorMode, ex);
      if (messageFromMode1.Length > 0)
        Console.WriteLine(messageFromMode1);
      string messageFromMode2 = this.GetMessageFromMode(this._messageErrorMode, ex);
      if (messageFromMode2.Length <= 0)
        return;
      int num = (int) MessageBox.Show(messageFromMode2, "Filter", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }
  }

  private List<DataGridViewColumn> SortedColumns
  {
    get
    {
      List<DataGridViewColumn> sortedColumns = new List<DataGridViewColumn>();
      DataGridViewColumn dataGridViewColumnStart = this._grid.Columns.GetFirstColumn(DataGridViewElementStates.None);
      if (dataGridViewColumnStart == null)
        return sortedColumns;
      sortedColumns.Add(dataGridViewColumnStart);
      while ((dataGridViewColumnStart = this._grid.Columns.GetNextColumn(dataGridViewColumnStart, DataGridViewElementStates.None, DataGridViewElementStates.None)) != null)
        sortedColumns.Add(dataGridViewColumnStart);
      return sortedColumns;
    }
  }

  private string GetMessageFromMode(FilterErrorModes mode, Exception exc)
  {
    string messageFromMode = "";
    if ((mode & FilterErrorModes.General) == FilterErrorModes.General)
      messageFromMode += "Invalid filter specified.";
    if ((mode & FilterErrorModes.ExceptionMessage) == FilterErrorModes.ExceptionMessage)
      messageFromMode = messageFromMode + (messageFromMode.Length > 0 ? "\n" : "") + exc.Message;
    if ((mode & FilterErrorModes.StackTrace) == FilterErrorModes.StackTrace)
      messageFromMode = messageFromMode + (messageFromMode.Length > 0 ? "\n" : "") + exc.StackTrace;
    return messageFromMode;
  }

  private void OnFilterFactoryChanged(object sender, EventArgs e) => this.RecreateGridFilters();

  private void OnDataSourceChanged(object sender, EventArgs e)
  {
    this._lastRowFilter = "";
    if (this._grid.Handle.ToInt32() <= 0)
      return;
    this._grid.BeginInvoke((Delegate) new MethodInvoker(this.RecreateGridFilters));
  }

  private void OnGridScroll(object sender, ScrollEventArgs e)
  {
    if (e.ScrollOrientation != ScrollOrientation.HorizontalScroll)
      return;
    this.RepositionGridFilters();
  }

  private void OnColumnStyleWidthChanged(object sender, EventArgs e)
  {
    this.RepositionGridFilters();
  }

  private void OnGridColumnsChanged(object sender, DataGridViewColumnEventArgs e)
  {
    this.RepositionGridFilters();
  }

  private void OnGridColumnsAddedRemoved(object sender, DataGridViewColumnEventArgs e)
  {
    this.RecreateGridFilters();
  }

  private void OnGridColumnsStateChanged(object sender, DataGridViewColumnStateChangedEventArgs e)
  {
    if (e.StateChanged != DataGridViewElementStates.Visible)
      return;
    this.RepositionGridFilters();
  }

  private void OnFilterChanged(object sender, EventArgs e)
  {
    if (this._autoRefreshMode != RefreshMode.OnInput)
      return;
    this.RecreateRowFilter();
  }

  private void OnFilterControlLeave(object sender, EventArgs e)
  {
    if (this._autoRefreshMode != RefreshMode.OnLeave && this._autoRefreshMode != RefreshMode.OnEnterOrLeave)
      return;
    this.RefreshFilters();
  }

  private void OnFilterControlKeyPress(object sender, KeyPressEventArgs e)
  {
    if (e.KeyChar != '\r' || this._autoRefreshMode != RefreshMode.OnEnter && this._autoRefreshMode != RefreshMode.OnEnterOrLeave)
      return;
    this.RefreshFilters();
  }

  protected virtual void OnBeforeFiltersChanging(EventArgs e)
  {
    EventHandler beforeFiltersChanging = this.BeforeFiltersChanging;
    if (beforeFiltersChanging == null)
      return;
    beforeFiltersChanging((object) this, e);
  }

  protected virtual void OnAfterFiltersChanged(EventArgs e)
  {
    EventHandler afterFiltersChanged = this.AfterFiltersChanged;
    if (afterFiltersChanged == null)
      return;
    afterFiltersChanged((object) this, e);
  }

  protected virtual void OnGridFilterBound(GridFilterEventArgs e)
  {
    GridFilterEventHandler gridFilterBound = this.GridFilterBound;
    if (gridFilterBound == null)
      return;
    gridFilterBound((object) this, e);
  }

  protected virtual void OnGridFilterUnbound(GridFilterEventArgs e)
  {
    GridFilterEventHandler gridFilterUnbound = this.GridFilterUnbound;
    if (gridFilterUnbound == null)
      return;
    gridFilterUnbound((object) this, e);
  }

  public void BeginInit() => ++this._initCounter;

  public void EndInit() => --this._initCounter;
}
