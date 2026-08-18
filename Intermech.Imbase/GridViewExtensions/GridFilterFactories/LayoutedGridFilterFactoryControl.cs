// Decompiled with JetBrains decompiler
// Type: GridViewExtensions.GridFilterFactories.LayoutedGridFilterFactoryControl
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using GridViewExtensions.GridFilters;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace GridViewExtensions.GridFilterFactories;

public class LayoutedGridFilterFactoryControl : UserControl, IGridFilterFactory
{
  private LayoutedPanel _layoutedPanel;
  private IGridFilterFactory _innerGridFilterFactory;
  private System.ComponentModel.Container components;
  private ArrayList _createdLabels;
  private ArrayList _createdControls;
  private bool _showEmptyGridFilters;

  public LayoutedGridFilterFactoryControl()
  {
    this.InitializeComponent();
    this._layoutedPanel = new LayoutedPanel();
    this._layoutedPanel.Dock = DockStyle.Fill;
    this.Controls.Add((Control) this._layoutedPanel);
    this.InnerGridFilterFactory = (IGridFilterFactory) new DefaultGridFilterFactory();
  }

  protected override void Dispose(bool disposing)
  {
    this.InnerGridFilterFactory = (IGridFilterFactory) null;
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.Name = "LayoutedFilterFactoryControl";
    this.Size = new Size(456, 296);
  }

  [Browsable(false)]
  public IGridFilterFactory InnerGridFilterFactory
  {
    get => this._innerGridFilterFactory;
    set
    {
      if (this._innerGridFilterFactory != null)
        this._innerGridFilterFactory.Changed -= new EventHandler(this.OnGridFilterFactoryChanged);
      this._innerGridFilterFactory = value;
      if (this._innerGridFilterFactory != null)
        this._innerGridFilterFactory.Changed += new EventHandler(this.OnGridFilterFactoryChanged);
      this.OnChanged();
    }
  }

  [Browsable(true)]
  [DefaultValue(40)]
  [Description("Gets and sets the minimum width for the controls. If the panel isn't big enough scrollbars will be created.")]
  public int ControlsMinimumWidth
  {
    get => this._layoutedPanel.ControlsMinimumWidth;
    set => this._layoutedPanel.ControlsMinimumWidth = value;
  }

  [Browsable(true)]
  [DefaultValue(0)]
  [Description("Gets and sets the horizontal space between the labels and controls.")]
  public int HorizontalSpacing
  {
    get => this._layoutedPanel.HorizontalSpacing;
    set => this._layoutedPanel.HorizontalSpacing = value;
  }

  [Browsable(true)]
  [DefaultValue(4)]
  [Description("Gets and sets the vertical space between the rows.")]
  public int VerticalSpacing
  {
    get => this._layoutedPanel.VerticalSpacing;
    set => this._layoutedPanel.VerticalSpacing = value;
  }

  [Browsable(true)]
  [DefaultValue(false)]
  [Description("Gets and sets whether the labels are aligned to the right or to the left.")]
  public bool RightAlignLabels
  {
    get => this._layoutedPanel.RightAlignLabels;
    set => this._layoutedPanel.RightAlignLabels = value;
  }

  [Browsable(true)]
  [DefaultValue(false)]
  [Description("Gets and sets whether EmptyGridFilter instances should be shown.")]
  public bool ShowEmptyGridFilters
  {
    get => this._showEmptyGridFilters;
    set
    {
      this._showEmptyGridFilters = value;
      this.OnChanged();
    }
  }

  public void HasChanged() => this.OnChanged();

  private void OnChanged()
  {
    EventHandler changed = this.Changed;
    if (changed == null)
      return;
    changed((object) this, EventArgs.Empty);
  }

  private void OnGridFilterFactoryChanged(object sender, EventArgs e) => this.OnChanged();

  private void OnGridFilterFactoryGridFilterCreated(GridFilterEventArgs args)
  {
    GridFilterEventHandler gridFilterCreated = this.GridFilterCreated;
    if (gridFilterCreated == null)
      return;
    gridFilterCreated((object) this, args);
  }

  public event EventHandler Changed;

  public event GridFilterEventHandler GridFilterCreated;

  public void BeginGridFilterCreation()
  {
    if (this._innerGridFilterFactory == null)
      return;
    this._innerGridFilterFactory.BeginGridFilterCreation();
    this._createdLabels = new ArrayList();
    this._createdControls = new ArrayList();
  }

  public void EndGridFilterCreation()
  {
    if (this._innerGridFilterFactory == null)
      return;
    this._innerGridFilterFactory.EndGridFilterCreation();
    if (this._createdLabels == null || this._createdControls == null)
      return;
    Label[] labels = new Label[this._createdLabels.Count];
    this._createdLabels.CopyTo((Array) labels);
    Control[] controls = new Control[this._createdControls.Count];
    this._createdControls.CopyTo((Array) controls);
    this._layoutedPanel.Fill(labels, controls);
    this._createdLabels = (ArrayList) null;
    this._createdControls = (ArrayList) null;
  }

  public IGridFilter CreateGridFilter(DataGridViewColumn column)
  {
    if (this._innerGridFilterFactory == null)
      return (IGridFilter) new EmptyGridFilter();
    IGridFilter gridFilter1 = this._innerGridFilterFactory.CreateGridFilter(column);
    gridFilter1.UseCustomFilterPlacement = true;
    GridFilterEventArgs args = new GridFilterEventArgs(column, gridFilter1);
    this.OnGridFilterFactoryGridFilterCreated(args);
    IGridFilter gridFilter2 = args.GridFilter;
    if (!gridFilter2.UseCustomFilterPlacement || this._createdLabels == null || this._createdControls == null || gridFilter2 is EmptyGridFilter && !this._showEmptyGridFilters)
      return gridFilter2;
    Label label = new Label();
    label.Text = column.HeaderText + ":";
    this._createdLabels.Add((object) label);
    this._createdControls.Add((object) gridFilter2.FilterControl);
    return gridFilter2;
  }
}
