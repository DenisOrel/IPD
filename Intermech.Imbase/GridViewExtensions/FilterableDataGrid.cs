// Decompiled with JetBrains decompiler
// Type: GridViewExtensions.FilterableDataGrid
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using GridViewExtensions.GridFilterFactories;
using GridViewExtensions.GridFilters;
using Intermech.Imbase;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace GridViewExtensions;

public class FilterableDataGrid : UserControl
{
  private DataGridView _grid;
  private DataGridFilterExtender _extender;
  private IContainer components;

  public event EventHandler AfterFiltersChanged;

  public event EventHandler BeforeFiltersChanging;

  public event GridFilterEventHandler GridFilterBound;

  public event GridFilterEventHandler GridFilterUnbound;

  public FilterableDataGrid()
  {
    this.InitializeComponent();
    this.RepositionGrid();
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    DefaultGridFilterFactory gridFilterFactory = new DefaultGridFilterFactory();
    this._grid = new DataGridView();
    this._extender = new DataGridFilterExtender(this.components);
    ((ISupportInitialize) this._grid).BeginInit();
    this._extender.BeginInit();
    this.SuspendLayout();
    this._grid.Location = new Point(0, 24);
    this._grid.Name = "_grid";
    this._grid.Size = new Size(496, 352);
    this._grid.TabIndex = 0;
    this._grid.MouseDown += new MouseEventHandler(this.OnMouseDown);
    this._grid.KeyDown += new KeyEventHandler(this.OnKeyDown);
    this._grid.MouseMove += new MouseEventHandler(this.OnMouseMove);
    this._grid.PreviewKeyDown += new PreviewKeyDownEventHandler(this.OnPreviewKeyDown);
    this._grid.MouseEnter += new EventHandler(this.OnMouseEnter);
    this._grid.MouseHover += new EventHandler(this.OnMouseHover);
    this._grid.MouseLeave += new EventHandler(this.OnMouseLeave);
    this._grid.KeyUp += new KeyEventHandler(this.OnKeyUp);
    this._grid.MouseUp += new MouseEventHandler(this.OnMouseUp);
    this._grid.KeyPress += new KeyPressEventHandler(this.OnKeyPress);
    this._grid.DoubleClick += new EventHandler(this.OnDoubleClick);
    this._extender.DataGridView = this._grid;
    gridFilterFactory.CreateDistinctGridFilters = false;
    gridFilterFactory.DefaultGridFilterType = typeof (TextGridFilter);
    gridFilterFactory.DefaultShowDateInBetweenOperator = false;
    gridFilterFactory.DefaultShowNumericInBetweenOperator = false;
    gridFilterFactory.HandleEnumerationTypes = true;
    gridFilterFactory.MaximumDistinctValues = 20;
    this._extender.FilterFactory = (IGridFilterFactory) gridFilterFactory;
    this._extender.GridFilterBound += new GridFilterEventHandler(this.OnGridFilterBound);
    this._extender.GridFilterUnbound += new GridFilterEventHandler(this.OnGridFilterUnbound);
    this._extender.AfterFiltersChanged += new EventHandler(this.OnAfterFiltersChanged);
    this._extender.BeforeFiltersChanging += new EventHandler(this.OnBeforeFiltersChanging);
    this.Controls.Add((Control) this._grid);
    this.Name = nameof (FilterableDataGrid);
    this.Size = new Size(496, 376);
    ((ISupportInitialize) this._grid).EndInit();
    this._extender.EndInit();
    this.ResumeLayout(false);
  }

  [Browsable(true)]
  [DefaultValue(RefreshMode.OnInput)]
  [Description("Specifies if the view automatically refreshes to reflect changes in the grid filter controls.")]
  public RefreshMode AutoRefreshMode
  {
    get => this._extender.AutoRefreshMode;
    set => this._extender.AutoRefreshMode = value;
  }

  [Browsable(false)]
  public DataGridView EmbeddedDataGridView => this._grid;

  [Browsable(true)]
  [DefaultValue(FilterPosition.Top)]
  [Description("Gets and sets the position of the filter GUI elements.")]
  public FilterPosition FilterBoxPosition
  {
    get => this._extender.FilterBoxPosition;
    set
    {
      this._extender.FilterBoxPosition = value;
      this.RepositionGrid();
    }
  }

  [Browsable(true)]
  [DefaultValue("Filter")]
  [Description("Gets and sets the text for the filter label.")]
  public string FilterText
  {
    get => this._extender.FilterText;
    set => this._extender.FilterText = value;
  }

  [Browsable(true)]
  [DefaultValue(null)]
  [Description("Gets and sets factory instance which should be used to create grid filters.")]
  public IGridFilterFactory FilterFactory
  {
    get => this._extender.FilterFactory;
    set => this._extender.FilterFactory = value;
  }

  [Browsable(true)]
  [DefaultValue(true)]
  [Description("Gets and sets whether the filter label should be visible.")]
  public bool FilterTextVisible
  {
    get => this._extender.FilterTextVisible;
    set => this._extender.FilterTextVisible = value;
  }

  [Browsable(true)]
  [DefaultValue(LogicalOperators.And)]
  [Description("The selected operator to combine the filter criterias.")]
  public LogicalOperators Operator
  {
    get => this._extender.Operator;
    set => this._extender.Operator = value;
  }

  [Browsable(true)]
  [DefaultValue(null)]
  [Description("The IBindingListView which should be initially displayed.")]
  public IBindingListView DataSource
  {
    get => this._grid.DataSource as IBindingListView;
    set
    {
      this._extender.BeginInit();
      this._grid.DataSource = (object) value;
      this._extender.EndInit();
    }
  }

  [Browsable(true)]
  [DefaultValue(FilterErrorModes.General)]
  [Description("Specifies what information is shown to the user if an error in the builded filter criterias occurs.")]
  public FilterErrorModes MessageErrorMode
  {
    get => this._extender.MessageErrorMode;
    set => this._extender.MessageErrorMode = value;
  }

  [Browsable(true)]
  [DefaultValue(FilterErrorModes.Off)]
  [Description("Specifies what information is printed to the console if an error in the builded filter criterias occurs.")]
  public FilterErrorModes ConsoleErrorMode
  {
    get => this._extender.ConsoleErrorMode;
    set => this._extender.ConsoleErrorMode = value;
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public StringDictionary BaseFilters => this._extender.BaseFilters;

  [Browsable(true)]
  [DefaultValue(LogicalOperators.And)]
  [Description("Operator which should be used to combine the base filter with the automatically created filters.")]
  public LogicalOperators BaseFilterOperator
  {
    get => this._extender.BaseFilterOperator;
    set => this._extender.BaseFilterOperator = value;
  }

  [Browsable(true)]
  [DefaultValue(true)]
  [Description("Gets or sets whether base filters should be used when refreshing the filter criteria.")]
  public bool BaseFilterEnabled
  {
    get => this._extender.BaseFilterEnabled;
    set => this._extender.BaseFilterEnabled = value;
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public string CurrentTableBaseFilter
  {
    get => this._extender.CurrentTableBaseFilter;
    set => this._extender.CurrentTableBaseFilter = value;
  }

  public GridFilterCollection GetGridFilters() => this._extender.GetGridFilters();

  public void ClearFilters() => this._extender.ClearFilters();

  public ConditionItem[] GetFilters() => this._extender.GetFilters();

  public void SetFilters(ConditionItem[] filters) => this._extender.SetFilters(filters);

  public void RefreshFilters() => this._extender.RefreshFilters();

  private void RepositionGrid()
  {
    int y = this._grid.Top;
    int height = this._grid.Height;
    int x = 0;
    int width = this.Width;
    switch (this._extender.FilterBoxPosition)
    {
      case FilterPosition.Top:
        y = this._extender.NeededControlHeight + 1;
        height = this.Height - y - 1;
        break;
      case FilterPosition.Bottom:
        y = 0;
        height = this.Height - this._extender.NeededControlHeight - 1;
        break;
      case FilterPosition.Off:
        y = 0;
        height = this.Height;
        break;
    }
    this._grid.SetBounds(x, y, width, height, BoundsSpecified.All);
  }

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

  private void OnMouseDown(object sender, MouseEventArgs e) => this.OnMouseDown(e);

  private void OnMouseEnter(object sender, EventArgs e) => this.OnMouseEnter(e);

  private void OnMouseHover(object sender, EventArgs e) => this.OnMouseHover(e);

  private void OnMouseLeave(object sender, EventArgs e) => this.OnMouseLeave(e);

  private void OnMouseMove(object sender, MouseEventArgs e) => this.OnMouseMove(e);

  private void OnMouseUp(object sender, MouseEventArgs e) => this.OnMouseUp(e);

  private void OnKeyDown(object sender, KeyEventArgs e) => this.OnKeyDown(e);

  private void OnKeyPress(object sender, KeyPressEventArgs e) => this.OnKeyPress(e);

  private void OnKeyUp(object sender, KeyEventArgs e) => this.OnKeyUp(e);

  private void OnPreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
  {
    this.OnPreviewKeyDown(e);
  }

  private void OnDoubleClick(object sender, EventArgs e) => this.OnDoubleClick(e);

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

  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this._extender = (DataGridFilterExtender) null;
      if (this.components != null)
        this.components.Dispose();
    }
    base.Dispose(disposing);
  }

  protected override void OnResize(EventArgs e)
  {
    base.OnResize(e);
    this.RepositionGrid();
  }
}
