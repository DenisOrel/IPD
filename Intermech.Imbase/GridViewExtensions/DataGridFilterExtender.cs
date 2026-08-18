// Decompiled with JetBrains decompiler
// Type: GridViewExtensions.DataGridFilterExtender
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using GridViewExtensions.GridFilterFactories;
using Intermech.Imbase;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace GridViewExtensions;

public class DataGridFilterExtender : Component, ISupportInitialize
{
  private System.ComponentModel.Container components;
  private DataGridView _grid;
  private Control _currentParent;
  private GridFiltersControl _filters;
  private FilterPosition _filterPosition;
  private bool _autoAdjustGrid;
  private bool _initializing;

  public event EventHandler AfterFiltersChanged;

  public event EventHandler BeforeFiltersChanging;

  public event GridFilterEventHandler GridFilterBound;

  public event GridFilterEventHandler GridFilterUnbound;

  public DataGridFilterExtender(IContainer container)
  {
    container.Add((IComponent) this);
    this.InitializeComponent();
    this._filters = new GridFiltersControl();
    this.FilterFactory = (IGridFilterFactory) new DefaultGridFilterFactory();
  }

  public DataGridFilterExtender()
  {
    this.InitializeComponent();
    this._filters = new GridFiltersControl();
    this.FilterFactory = (IGridFilterFactory) new DefaultGridFilterFactory();
  }

  protected override void Dispose(bool disposing)
  {
    if (this._filters != null)
    {
      this.RemoveFilterControl();
      this._filters.Dispose();
      this._filters = (GridFiltersControl) null;
    }
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent() => this.components = new System.ComponentModel.Container();

  [Browsable(true)]
  [DefaultValue(RefreshMode.OnInput)]
  [Description("Specifies if the view automatically refreshes to reflect changes in the grid filter controls.")]
  public RefreshMode AutoRefreshMode
  {
    get => this._filters.AutoRefreshMode;
    set => this._filters.AutoRefreshMode = value;
  }

  [Browsable(true)]
  [DefaultValue(FilterPosition.Top)]
  [Description("Gets and sets the position of the filter GUI elements.")]
  public FilterPosition FilterBoxPosition
  {
    get => this._filterPosition;
    set
    {
      if (this._filterPosition == value)
        return;
      if (this._autoAdjustGrid)
        this.AdjustGridPosition(this._filterPosition, value);
      this._filterPosition = value;
      this.AdjustFilterControlToGrid();
    }
  }

  [Browsable(true)]
  [DefaultValue(false)]
  [Description("Sets whether the bounds of the extended DataGridView should be set automatically depending on where the filters are displayed, so that the totally covered area by grid and filters is always the same.")]
  public bool AutoAdjustGridPosition
  {
    get => this._autoAdjustGrid;
    set
    {
      if (this._autoAdjustGrid == value)
        return;
      this._autoAdjustGrid = value;
      if (this._autoAdjustGrid)
        this.AdjustGridPosition(FilterPosition.Off, this._filterPosition);
      else
        this.AdjustGridPosition(this._filterPosition, FilterPosition.Off);
    }
  }

  [Browsable(true)]
  [DefaultValue("Filter")]
  [Description("Gets and sets the text for the filter label.")]
  public string FilterText
  {
    get => this._filters.FilterText;
    set => this._filters.FilterText = value;
  }

  [Browsable(true)]
  [DefaultValue(true)]
  [Description("Gets and sets whether the filter label should be visible.")]
  public bool FilterTextVisible
  {
    get => this._filters.FilterTextVisible;
    set => this._filters.FilterTextVisible = value;
  }

  [Browsable(false)]
  public Rectangle ControlBounds => this._filters != null ? this._filters.Bounds : Rectangle.Empty;

  [Browsable(false)]
  public int NeededControlHeight => this._filters != null ? this._filters.Height : 0;

  [Browsable(true)]
  [DefaultValue(null)]
  public IGridFilterFactory FilterFactory
  {
    get => this._filters.FilterFactory;
    set => this._filters.FilterFactory = value;
  }

  [Browsable(true)]
  [DefaultValue(LogicalOperators.And)]
  public LogicalOperators Operator
  {
    get => this._filters.Operator;
    set => this._filters.Operator = value;
  }

  [Browsable(true)]
  [DefaultValue(FilterErrorModes.General)]
  [Description("Specifies what information is shown to the user if an error in the builded filter criterias occurs.")]
  public FilterErrorModes MessageErrorMode
  {
    get => this._filters.MessageErrorMode;
    set => this._filters.MessageErrorMode = value;
  }

  [Browsable(true)]
  [DefaultValue(FilterErrorModes.Off)]
  [Description("Specifies what information is printed to the console if an error in the builded filter criterias occurs.")]
  public FilterErrorModes ConsoleErrorMode
  {
    get => this._filters.ConsoleErrorMode;
    set => this._filters.ConsoleErrorMode = value;
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public StringDictionary BaseFilters => this._filters.BaseFilters;

  [Browsable(true)]
  [DefaultValue(LogicalOperators.And)]
  [Description("Operator which should be used to combine the base filter with the automatically created filters.")]
  public LogicalOperators BaseFilterOperator
  {
    get => this._filters.BaseFilterOperator;
    set => this._filters.BaseFilterOperator = value;
  }

  [Browsable(true)]
  [DefaultValue(true)]
  [Description("Gets or sets whether base filters should be used when refreshing the filter criteria.")]
  public bool BaseFilterEnabled
  {
    get => this._filters.BaseFilterEnabled;
    set => this._filters.BaseFilterEnabled = value;
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public string CurrentTableBaseFilter
  {
    get => this._filters.CurrentTableBaseFilter;
    set => this._filters.CurrentTableBaseFilter = value;
  }

  [Browsable(true)]
  [Description("Gets and sets the grid which should be extended.")]
  public DataGridView DataGridView
  {
    get => this._grid;
    set
    {
      if (this._grid == value)
        return;
      this.RemoveFilterControl();
      if (this._grid != null)
      {
        this._grid.LocationChanged -= new EventHandler(this.OnGridLocationChanged);
        this._grid.Resize -= new EventHandler(this.OnGridResize);
        this._grid.ParentChanged -= new EventHandler(this.OnGridParentChanged);
      }
      this._grid = value;
      this._filters.DataGridView = this._grid;
      this.AdjustFilterControlToGrid();
      this.AddFilterControl();
      if (this._autoAdjustGrid)
        this.AdjustGridPosition(FilterPosition.Off, this._filterPosition);
      if (this._grid == null)
        return;
      this._grid.LocationChanged += new EventHandler(this.OnGridLocationChanged);
      this._grid.Resize += new EventHandler(this.OnGridResize);
      this._grid.ParentChanged += new EventHandler(this.OnGridParentChanged);
    }
  }

  public GridFilterCollection GetGridFilters() => this._filters.GetGridFilters();

  public void ClearFilters() => this._filters.ClearFilters();

  public ConditionItem[] GetFilters() => this._filters.GetFilters();

  public void SetFilters(ConditionItem[] filters)
  {
    if (filters == null)
      return;
    this._filters.SetFilters(filters);
  }

  public void RefreshFilters() => this._filters.RefreshFilters();

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

  private void AdjustGridPosition(FilterPosition fromPosition, FilterPosition toPosition)
  {
    if (this._grid == null || this._filters == null || fromPosition == toPosition || this._initializing)
      return;
    int top = this._grid.Top;
    int height = this._grid.Height;
    switch (fromPosition)
    {
      case FilterPosition.Top:
        top -= this._filters.Height;
        height += this._filters.Height;
        break;
      case FilterPosition.Bottom:
        height += this._filters.Height;
        break;
    }
    switch (toPosition)
    {
      case FilterPosition.Top:
        top += this._filters.Height;
        height -= this._filters.Height;
        break;
      case FilterPosition.Bottom:
        height -= this._filters.Height;
        break;
    }
    AnchorStyles anchor = this._grid.Anchor;
    this._grid.Anchor = AnchorStyles.Top | AnchorStyles.Left;
    this._grid.SetBounds(0, top, 0, height, BoundsSpecified.Y | BoundsSpecified.Height);
    this._grid.Anchor = anchor;
  }

  private void RemoveFilterControl()
  {
    if (this._currentParent == null)
      return;
    this._filters.AfterFiltersChanged -= new EventHandler(this.OnAfterFiltersChanged);
    this._filters.BeforeFiltersChanging -= new EventHandler(this.OnBeforeFiltersChanging);
    this._filters.GridFilterBound -= new GridFilterEventHandler(this.OnGridFilterBound);
    this._filters.GridFilterUnbound -= new GridFilterEventHandler(this.OnGridFilterUnbound);
    this._currentParent.Controls.Remove((Control) this._filters);
    this._currentParent.BackColorChanged -= new EventHandler(this.OnColorsChanged);
    this._currentParent.ForeColorChanged -= new EventHandler(this.OnColorsChanged);
    this._currentParent = (Control) null;
  }

  private void AddFilterControl()
  {
    this.RemoveFilterControl();
    if (this._grid == null)
      return;
    if (this._grid.Parent != null)
    {
      if (this._currentParent != null)
      {
        this._currentParent.BackColorChanged -= new EventHandler(this.OnColorsChanged);
        this._currentParent.ForeColorChanged -= new EventHandler(this.OnColorsChanged);
      }
      this._currentParent = this._grid.Parent;
      this._currentParent.BackColorChanged += new EventHandler(this.OnColorsChanged);
      this._currentParent.ForeColorChanged += new EventHandler(this.OnColorsChanged);
      this._grid.Parent.Controls.Add((Control) this._filters);
      this._filters.BringToFront();
      this._filters.AfterFiltersChanged += new EventHandler(this.OnAfterFiltersChanged);
      this._filters.BeforeFiltersChanging += new EventHandler(this.OnBeforeFiltersChanging);
      this._filters.GridFilterBound += new GridFilterEventHandler(this.OnGridFilterBound);
      this._filters.GridFilterUnbound += new GridFilterEventHandler(this.OnGridFilterUnbound);
    }
    this.AdjustFilterControlToGrid();
  }

  private void AdjustFilterControlToGrid()
  {
    if (this._grid == null || this._filters == null || this._grid.Parent == null)
      return;
    switch (this._filterPosition)
    {
      case FilterPosition.Top:
        this._filters.Top = this._grid.Top - this._filters.Height;
        this._filters.Left = this._grid.Left;
        this._filters.Width = this._grid.Width;
        this._filters.BackColor = this._grid.Parent.BackColor;
        this._filters.ForeColor = this._grid.Parent.ForeColor;
        this._filters.Visible = true;
        break;
      case FilterPosition.Bottom:
        this._filters.Top = this._grid.Bottom + 1;
        this._filters.Left = this._grid.Left;
        this._filters.Width = this._grid.Width;
        this._filters.BackColor = this._grid.Parent.BackColor;
        this._filters.ForeColor = this._grid.Parent.ForeColor;
        this._filters.Visible = true;
        break;
      default:
        this._filters.Visible = false;
        break;
    }
  }

  private void OnGridLocationChanged(object sender, EventArgs e)
  {
    this.AdjustFilterControlToGrid();
  }

  private void OnGridResize(object sender, EventArgs e) => this.AdjustFilterControlToGrid();

  private void OnGridParentChanged(object sender, EventArgs e) => this.AddFilterControl();

  private void OnColorsChanged(object sender, EventArgs e) => this.AdjustFilterControlToGrid();

  private void OnAfterFiltersChanged(object sender, EventArgs e) => this.OnAfterFiltersChanged(e);

  private void OnBeforeFiltersChanging(object sender, EventArgs e)
  {
    this.OnBeforeFiltersChanging(e);
  }

  private void OnGridFilterBound(object sender, GridFilterEventArgs e) => this.OnGridFilterBound(e);

  private void OnGridFilterUnbound(object sender, GridFilterEventArgs e)
  {
    this.OnGridFilterUnbound(e);
  }

  public void BeginInit()
  {
    this._initializing = true;
    if (this._filters == null)
      return;
    this._filters.BeginInit();
  }

  public void EndInit()
  {
    this._initializing = false;
    if (this._filters == null)
      return;
    this._filters.EndInit();
  }
}
