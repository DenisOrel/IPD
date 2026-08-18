
// Type: IMClient.ServerObjectsView




using Intermech.Bars;
using Intermech.Controls.Grid;
using Intermech.Docking;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace IMClient
{
    internal class ServerObjectsView : DockControl, ICommandTarget
    {
      private ListGrid _grid;
      private System.IServiceProvider _serviceProvider;
      private int _sortColumn;
      private SortDirection _sortDirection;

      public ServerObjectsView(System.IServiceProvider provider)
      {
        this._serviceProvider = provider;
        this.InitializeComponent();
        INamedImageList service1 = (INamedImageList) this._serviceProvider.GetService(typeof (INamedImageList));
        if (service1 != null)
          this.TabImageIndex = service1.ImageIndex("imgServerObjects");
        IConfigurationManager service2 = (IConfigurationManager) this._serviceProvider.GetService(typeof (IConfigurationManager));
        if (service2 == null)
          return;
        this.LoadConfiguration(service2);
        service2.ConfigurationBeforeSave += new ConfigurationBeforeSaveEventHandler(this.SaveConfiguration);
      }

      private void InitializeComponent()
      {
        ListColumn listColumn1 = new ListColumn();
        ListColumn listColumn2 = new ListColumn();
        ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ServerObjectsView));
        this._grid = new ListGrid();
        this.SuspendLayout();
        this._grid.AlternateBackground = Color.PowderBlue;
        this._grid.AutoHeight = false;
        this._grid.BackColor = SystemColors.Control;
        this._grid.BorderWidth = 4;
        listColumn1.Name = "_count";
        listColumn1.NumericSort = true;
        listColumn1.Text = LocalizationHolder.rm.GetString("IMClient_88");
        listColumn1.TextAlignment = ContentAlignment.MiddleRight;
        listColumn1.Width = 160 /*0xA0*/;
        listColumn2.Name = "_type";
        listColumn2.Text = LocalizationHolder.rm.GetString("IMClient_89");
        listColumn2.Width = 600;
        this._grid.Columns.AddRange(new ListColumn[2]
        {
          listColumn1,
          listColumn2
        });
        componentResourceManager.ApplyResources((object) this._grid, "_grid");
        this._grid.ForeColor = SystemColors.ControlText;
        this._grid.GridColor = Color.Silver;
        this._grid.HeaderHeight = 22;
        this._grid.HeaderStyle = HeaderStyle.Flat;
        this._grid.HotItemTracking = true;
        this._grid.HotTrackingColor = Color.DeepSkyBlue;
        this._grid.ImageList = (ImageList) null;
        this._grid.ItemHeight = 19;
        this._grid.Name = "_grid";
        this._grid.SelectedTextColor = Color.White;
        this._grid.SelectionColor = Color.DarkBlue;
        this._grid.SuperFlatHeaderColor = Color.White;
        this._grid.ColumnClick += new ListGrid.ClickedEventHandler(this.Grid_ColumnClick);
        this.AllowedStates = DockLocation.Left | DockLocation.Right | DockLocation.Top | DockLocation.Bottom;
        this.BorderStyle = Intermech.Docking.Rendering.BorderStyle.Flat;
        this.Controls.Add((Control) this._grid);
        this.Guid = ViewGuids.ServerObjectsView_Guid;
        this.HideOnClose = true;
        this.Name = nameof (ServerObjectsView);
        this.ShowHint = DockState.DockBottomAutoHide;
        componentResourceManager.ApplyResources((object) this, "$this");
        this.Tag = (object) "";
        this.BeforeFirstShown += new EventHandler(this.ServerObjects_BeforeFirstShown);
        this.ResumeLayout(false);
      }

      private void LoadConfiguration(IConfigurationManager configurationManager)
      {
        IConfiguration configuration = configurationManager.Open("ServerObjects");
        if (configuration == null)
          return;
        foreach (ListColumn column in (CollectionBase) this._grid.Columns)
        {
          if (configuration.HasProperty(column.Name))
            column.Width = int.Parse(configuration.GetProperty(column.Name));
        }
      }

      private void SaveConfiguration(IConfigurationManager configurationManager)
      {
        IConfiguration configuration = configurationManager.Create("ServerObjects");
        foreach (ListColumn column in (CollectionBase) this._grid.Columns)
          configuration.SetProperty(column.Name, column.Width.ToString());
      }

      private void RefreshData()
      {
        if (!((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IRemotingInfoService)) is IRemotingInfoService customService))
          return;
        List<Tuple<string, int>> objectsStatistics = customService.GetMarshalledObjectsStatistics();
        Intermech.Controls.Grid.ListItem[] items = new Intermech.Controls.Grid.ListItem[objectsStatistics.Count];
        int num1 = 0;
        foreach (Tuple<string, int> tuple in objectsStatistics)
        {
          Intermech.Controls.Grid.ListItem listItem = new Intermech.Controls.Grid.ListItem(this._grid);
          int num2 = tuple.Item2;
          listItem.SubItems[0].Text = num2.ToString();
          listItem.SubItems[0].Value = (double) num2;
          listItem.SubItems[1].Text = tuple.Item1;
          items[num1++] = listItem;
        }
        this._grid.Items.Clear();
        this._grid.Items.AddRange(items);
        this._grid.Columns[this._sortColumn].LastSortState = this._sortDirection;
        this._grid.SortColumn(this._sortColumn, false);
      }

      public bool Execute(ICommandState commandState)
      {
        if (!(commandState.CommandName == "Refresh"))
          return false;
        this.RefreshData();
        return true;
      }

      public bool QueryStatus(ICommandState commandState)
      {
        if (!(commandState.CommandName == "Refresh"))
          return false;
        commandState.Enabled = true;
        return true;
      }

      private void ServerObjects_BeforeFirstShown(object sender, EventArgs e) => this.RefreshData();

      private void Grid_ColumnClick(object source, ClickEventArgs e)
      {
        this._sortColumn = e.ColumnIndex;
        if (this._grid.Columns[this._sortColumn].LastSortState == SortDirection.Ascending)
          this._sortDirection = SortDirection.Descending;
        else
          this._sortDirection = SortDirection.Ascending;
      }
    }
}
