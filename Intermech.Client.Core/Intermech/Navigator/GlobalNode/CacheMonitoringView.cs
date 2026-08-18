
// Type: Intermech.Navigator.GlobalNode.CacheMonitoringView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using DevExpress.IM.Utils;
using DevExpress.IM.XtraGrid;
using DevExpress.IM.XtraGrid.Columns;
using DevExpress.IM.XtraGrid.Views.Base;
using DevExpress.IM.XtraGrid.Views.Grid;
using Intermech.Cache.Performance;
using Intermech.CacheServices;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Navigator.GlobalNode;

/// <summary>Summary description for CacheMonitoringView.</summary>
internal class CacheMonitoringView : UserControl, IView
{
  private ICacheService cacheService;
  private DataTable counters;
  private Label lbCache;
  private ComboBox cbCache;
  private Timer tmUpdate;
  private GridControl gcCounters;
  private GridColumn gridColumn1;
  private GridColumn gridColumn2;
  private GridView gvColumns;
  private Label lbCounters;
  private GroupBox gbDescription;
  private Label lbDescription;
  private IContainer components;

  public CacheMonitoringView()
  {
    this.InitializeComponent();
    this.cacheService = (ICacheService) null;
    this.counters = new DataTable();
    this.counters.Columns.Add("F_COUNTER", typeof (string));
    this.counters.Columns.Add("F_VALUE", typeof (double));
    this.counters.Columns.Add("F_DESCRIPTION", typeof (string));
    this.gcCounters.DataSource = (object) this.counters;
  }

  /// <summary>Clean up any resources being used.</summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (CacheMonitoringView));
    this.lbCache = new Label();
    this.cbCache = new ComboBox();
    this.tmUpdate = new Timer(this.components);
    this.gcCounters = new GridControl();
    this.gvColumns = new GridView();
    this.gridColumn1 = new GridColumn();
    this.gridColumn2 = new GridColumn();
    this.lbCounters = new Label();
    this.gbDescription = new GroupBox();
    this.lbDescription = new Label();
    this.gcCounters.BeginInit();
    this.gvColumns.BeginInit();
    this.gbDescription.SuspendLayout();
    this.SuspendLayout();
    this.lbCache.AccessibleDescription = (string) null;
    this.lbCache.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.lbCache, "lbCache");
    this.lbCache.Font = (Font) null;
    this.lbCache.Name = "lbCache";
    this.cbCache.AccessibleDescription = (string) null;
    this.cbCache.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.cbCache, "cbCache");
    this.cbCache.BackgroundImage = (Image) null;
    this.cbCache.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbCache.Font = (Font) null;
    this.cbCache.Name = "cbCache";
    this.cbCache.SelectedIndexChanged += new EventHandler(this.cbCache_SelectedIndexChanged);
    this.tmUpdate.Interval = 1000;
    this.tmUpdate.Tick += new EventHandler(this.tmUpdate_Tick);
    this.gcCounters.AccessibleDescription = (string) null;
    this.gcCounters.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.gcCounters, "gcCounters");
    this.gcCounters.BackgroundImage = (Image) null;
    this.gcCounters.EmbeddedNavigator.AccessibleDescription = (string) null;
    this.gcCounters.EmbeddedNavigator.AccessibleName = (string) null;
    this.gcCounters.EmbeddedNavigator.Anchor = (AnchorStyles) componentResourceManager.GetObject("gcCounters.EmbeddedNavigator.Anchor");
    this.gcCounters.EmbeddedNavigator.BackgroundImage = (Image) null;
    this.gcCounters.EmbeddedNavigator.BackgroundImageLayout = (ImageLayout) componentResourceManager.GetObject("gcCounters.EmbeddedNavigator.BackgroundImageLayout");
    this.gcCounters.EmbeddedNavigator.Dock = (DockStyle) componentResourceManager.GetObject("gcCounters.EmbeddedNavigator.Dock");
    this.gcCounters.EmbeddedNavigator.ImeMode = (ImeMode) componentResourceManager.GetObject("gcCounters.EmbeddedNavigator.ImeMode");
    this.gcCounters.EmbeddedNavigator.Name = "";
    this.gcCounters.Font = (Font) null;
    this.gcCounters.MainView = (BaseView) this.gvColumns;
    this.gcCounters.Name = "gcCounters";
    componentResourceManager.ApplyResources((object) this.gvColumns, "gvColumns");
    this.gvColumns.Columns.AddRange(new GridColumn[2]
    {
      this.gridColumn1,
      this.gridColumn2
    });
    this.gvColumns.GridControl = this.gcCounters;
    this.gvColumns.Name = "gvColumns";
    this.gvColumns.OptionsBehavior.Editable = false;
    this.gvColumns.OptionsCustomization.AllowFilter = false;
    this.gvColumns.OptionsCustomization.AllowGroup = false;
    this.gvColumns.OptionsView.ShowGroupPanel = false;
    this.gvColumns.FocusedRowChanged += new FocusedRowChangedEventHandler(this.gvColumns_FocusedRowChanged);
    componentResourceManager.ApplyResources((object) this.gridColumn1, "gridColumn1");
    this.gridColumn1.Name = "gridColumn1";
    this.gridColumn1.Options = ColumnOptions.CanMoved | ColumnOptions.CanResized | ColumnOptions.CanSorted | ColumnOptions.ReadOnly;
    this.gridColumn1.VisibleIndex = 0;
    componentResourceManager.ApplyResources((object) this.gridColumn2, "gridColumn2");
    this.gridColumn2.DisplayFormat.FormatType = FormatType.Numeric;
    this.gridColumn2.Name = "gridColumn2";
    this.gridColumn2.Options = ColumnOptions.CanMoved | ColumnOptions.CanResized | ColumnOptions.CanSorted | ColumnOptions.ReadOnly;
    this.gridColumn2.VisibleIndex = 1;
    this.lbCounters.AccessibleDescription = (string) null;
    this.lbCounters.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.lbCounters, "lbCounters");
    this.lbCounters.Font = (Font) null;
    this.lbCounters.Name = "lbCounters";
    this.gbDescription.AccessibleDescription = (string) null;
    this.gbDescription.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.gbDescription, "gbDescription");
    this.gbDescription.BackgroundImage = (Image) null;
    this.gbDescription.Controls.Add((Control) this.lbDescription);
    this.gbDescription.Font = (Font) null;
    this.gbDescription.Name = "gbDescription";
    this.gbDescription.TabStop = false;
    this.lbDescription.AccessibleDescription = (string) null;
    this.lbDescription.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.lbDescription, "lbDescription");
    this.lbDescription.FlatStyle = FlatStyle.System;
    this.lbDescription.Font = (Font) null;
    this.lbDescription.Name = "lbDescription";
    this.AccessibleDescription = (string) null;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.BackgroundImage = (Image) null;
    this.Controls.Add((Control) this.gbDescription);
    this.Controls.Add((Control) this.lbCounters);
    this.Controls.Add((Control) this.gcCounters);
    this.Controls.Add((Control) this.cbCache);
    this.Controls.Add((Control) this.lbCache);
    this.Font = (Font) null;
    this.Name = nameof (CacheMonitoringView);
    this.gcCounters.EndInit();
    this.gvColumns.EndInit();
    this.gbDescription.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  public void Initialize(ISelectedItems items, System.IServiceProvider services)
  {
    this.cbCache.Items.AddRange((object[]) ((ICacheServices) ServicesManager.GetService(typeof (ICacheServices))).Names);
    this.cbCache.SelectedIndex = 0;
  }

  public void Activate(IView previousView)
  {
    if (this.tmUpdate.Enabled)
      return;
    this.tmUpdate.Start();
  }

  public void Deactivate(IView nextView)
  {
    if (nextView != null)
      return;
    if (this.tmUpdate.Enabled)
      this.tmUpdate.Stop();
    this.CacheService = (ICacheService) null;
  }

  private void cbCache_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.CacheService = ((ICacheServices) ServicesManager.GetService(typeof (ICacheServices))).GetService(this.cbCache.SelectedItem.ToString());
  }

  private void tmUpdate_Tick(object sender, EventArgs e)
  {
    if (this.CacheService == null)
      return;
    PerformanceCounterCollection performanceCounters = this.cacheService.PerformanceCounters;
    lock (performanceCounters)
    {
      this.counters.BeginLoadData();
      try
      {
        for (int index = 0; index < performanceCounters.Count; ++index)
          this.counters.Rows[index][1] = (object) performanceCounters[index].Value;
        this.counters.AcceptChanges();
      }
      finally
      {
        this.counters.EndLoadData();
      }
    }
  }

  private void gvColumns_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
  {
    DataRow dataRow = this.gvColumns.GetDataRow(e.FocusedRowHandle);
    this.lbDescription.Text = dataRow == null ? string.Empty : Convert.ToString(dataRow[2]);
  }

  public string Caption => LocalizationHolder.rm.GetString("Client.Core_611");

  public int OrderID => 1010;

  public int ImageIndex => -1;

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  private ICacheService CacheService
  {
    get => this.cacheService;
    set
    {
      if (this.cacheService == value)
        return;
      this.cacheService = value;
      if (this.cacheService == null)
      {
        this.counters.Rows.Clear();
      }
      else
      {
        PerformanceCounterCollection performanceCounters = this.cacheService.PerformanceCounters;
        lock (performanceCounters)
        {
          this.counters.BeginLoadData();
          try
          {
            this.counters.Rows.Clear();
            for (int index = 0; index < performanceCounters.Count; ++index)
            {
              IPerformanceCounter performanceCounter = performanceCounters[index];
              this.counters.Rows.Add((object) $"{performanceCounter.CounterName} ({performanceCounter.Measure})", (object) performanceCounter.Value, (object) performanceCounter.Description);
            }
            this.counters.AcceptChanges();
          }
          finally
          {
            this.counters.EndLoadData();
          }
        }
      }
    }
  }
}
