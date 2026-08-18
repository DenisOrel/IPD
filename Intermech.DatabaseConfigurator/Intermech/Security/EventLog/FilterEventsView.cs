// Decompiled with JetBrains decompiler
// Type: Intermech.Security.EventLog.FilterEventsView
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.EventLog;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using TenTec.Windows.iGridLib;

#nullable disable
namespace Intermech.Security.EventLog;

public class FilterEventsView : EventsView
{
  private static int iconIndex = -1;
  private bool refreshOnActivate;

  public FilterEventsView()
  {
    if (FilterEventsView.iconIndex != -1 || ChildrenView._namedImageList == null)
      return;
    using (MemoryStream memoryStream = FilterEventsView.ResourcesAccess.LoadResurce("EventLogFiltered.ico"))
    {
      using (Icon icon = new Icon((Stream) memoryStream))
        ChildrenView._namedImageList.Add(icon, "imgEventLogFilteredIcon");
    }
    FilterEventsView.iconIndex = ChildrenView._namedImageList.ImageIndex("imgEventLogFilteredIcon");
  }

  protected override bool ShowFiltersComboBox => false;

  public override void Initialize(ISelectedItems items, System.IServiceProvider services)
  {
    base.Initialize(items, services);
  }

  public override void Activate(IView previousView)
  {
    base.Activate(previousView);
    this._notificationService.Unsubscribe("FilterChanged", new NotificationEventHandler(this.OnFilterChanged));
    this._notificationService.Subscribe("FilterChanged", new NotificationEventHandler(this.OnFilterChanged));
    if (previousView == null)
      return;
    if (!this.refreshOnActivate)
      return;
    try
    {
      this.ReloadItems();
    }
    finally
    {
      this.refreshOnActivate = false;
    }
  }

  public override void Deactivate(IView nextView)
  {
    base.Deactivate(nextView);
    if (nextView != null)
      return;
    this._notificationService.Unsubscribe("FilterChanged", new NotificationEventHandler(this.OnFilterChanged));
  }

  public override string Caption => LocalizationHolder.rm.GetString("DatabaseConfigurator_104");

  public override int OrderID => 20;

  public override int ImageIndex => FilterEventsView.iconIndex;

  private void OnFilterChanged(object sender, NotificationEventArgs e)
  {
    if (!(this._services.GetService(typeof (IViewsManager)) is IViewsManager service) || !(this.Node is INodeNotifications node) || node.Process(e, (object) null) != ProcessResult.RefreshNode)
      return;
    if (service.ActiveViewPage.Control == this)
      this.ReloadItems();
    else
      this.refreshOnActivate = true;
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FilterEventsView));
    ((ISupportInitialize) this._grid).BeginInit();
    ((ISupportInitialize) this._pictureBox).BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._toolBar, "tbViewBar");
    this._toolTip.SetToolTip((System.Windows.Forms.Control) this._toolBar, componentResourceManager.GetString("tbViewBar.ToolTip"));
    componentResourceManager.ApplyResources((object) this._embeddedViewsDropDownMenuItem, "btViewNames");
    componentResourceManager.ApplyResources((object) this._toggleManualSortingButtonItem, "btClearSorting");
    componentResourceManager.ApplyResources((object) this._grid, "grid");
    this._grid.DefaultAutoGroupRow.Height = 21;
    this._grid.DefaultRow.Height = (int) componentResourceManager.GetObject("resource.Height");
    this._grid.DefaultRow.Key = componentResourceManager.GetString("resource.Key");
    this._grid.DefaultRow.NormalCellHeight = (int) componentResourceManager.GetObject("resource.NormalCellHeight");
    this._grid.FrozenArea.ColCount = 1;
    this._grid.FrozenArea.SortFrozenRows = true;
    this._grid.GroupBox.BackColor = SystemColors.AppWorkspace;
    this._grid.GroupBox.HintBackColor = SystemColors.AppWorkspace;
    this._grid.GroupBox.HintForeColor = SystemColors.ControlText;
    this._grid.GroupBox.Text = componentResourceManager.GetString("grid.GroupBox.Text");
    this._grid.GroupBox.Visible = true;
    this._grid.Header.AutoHeightFlags = iGHdrAutoHeightFlags.OnAddCol | iGHdrAutoHeightFlags.OnRemoveCol | iGHdrAutoHeightFlags.OnShowCol | iGHdrAutoHeightFlags.OnContentsChange | iGHdrAutoHeightFlags.OnThemeChange | iGHdrAutoHeightFlags.OnResizeCol;
    this._grid.Header.Height = (int) componentResourceManager.GetObject("grid.Header.Height");
    this._grid.LayoutObject.Flags = iGLayoutFlags.Grouping | iGLayoutFlags.Sorting | iGLayoutFlags.ColVisibility | iGLayoutFlags.ColWidth | iGLayoutFlags.ColOrder;
    this._toolTip.SetToolTip((System.Windows.Forms.Control) this._grid, componentResourceManager.GetString("grid.ToolTip"));
    componentResourceManager.ApplyResources((object) this._collapseAllGroupsButtonItem, "btCollapseAll");
    componentResourceManager.ApplyResources((object) this._expandAllGroupsButtonItem, "btExpandAll");
    componentResourceManager.ApplyResources((object) this._pageViewsManager, "ViewsManager");
    this._toolTip.SetToolTip((System.Windows.Forms.Control) this._pageViewsManager, componentResourceManager.GetString("ViewsManager.ToolTip"));
    componentResourceManager.ApplyResources((object) this.buttonHeightSet, "buttonHeightSet");
    this.buttonHeightSet.Padding.Bottom = 0;
    this.buttonHeightSet.Padding.Left = 0;
    this.buttonHeightSet.Padding.Right = 0;
    this.buttonHeightSet.Padding.Top = 0;
    componentResourceManager.ApplyResources((object) this._filtersComboBoxItem, "listObjectsFiltration");
    this._filtersComboBoxItem.Padding.Bottom = 0;
    this._filtersComboBoxItem.Padding.Left = 1;
    this._filtersComboBoxItem.Padding.Right = 1;
    this._filtersComboBoxItem.Padding.Top = 0;
    componentResourceManager.ApplyResources((object) this._manualSortingSetupButtonItem, "btSetupSorting");
    componentResourceManager.ApplyResources((object) this._toggleGroupingButtonItem, "btClearGrouping");
    componentResourceManager.ApplyResources((object) this._refreshButtonItem, "btRefresh");
    componentResourceManager.ApplyResources((object) this._gridHeaderMenuBar, "menuHeader");
    this._toolTip.SetToolTip((System.Windows.Forms.Control) this._gridHeaderMenuBar, componentResourceManager.GetString("menuHeader.ToolTip"));
    componentResourceManager.ApplyResources((object) this._gridHeaderContextMenuBarItem, "contextMenuHeader");
    componentResourceManager.ApplyResources((object) this._changeGridColumnsMenuButtonItem, "mnpSetupColumns");
    componentResourceManager.ApplyResources((object) this._collapseAllGroupsExpectGroupsWithFocusedItemsButtonItem, "btCollapseAndShow");
    componentResourceManager.ApplyResources((object) this._pictureBox, "pictureView");
    this._toolTip.SetToolTip((System.Windows.Forms.Control) this._pictureBox, componentResourceManager.GetString("pictureView.ToolTip"));
    componentResourceManager.ApplyResources((object) this._currentVersionsRuleButtonItem, "buttonVersionsRule");
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Name = nameof (FilterEventsView);
    this._toolTip.SetToolTip((System.Windows.Forms.Control) this, componentResourceManager.GetString("$this.ToolTip"));
    ((ISupportInitialize) this._grid).EndInit();
    ((ISupportInitialize) this._pictureBox).EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private static class ResourcesAccess
  {
    private static string ResourcePath = "Intermech.DatabaseConfigurator.Resources.";

    public static MemoryStream LoadResurce(string ResourceName)
    {
      Stream stream = (Stream) null;
      try
      {
        stream = typeof (FilterEventsView.ResourcesAccess).Assembly.GetManifestResourceStream(FilterEventsView.ResourcesAccess.ResourcePath + ResourceName);
        if (stream == null)
          return new MemoryStream();
        byte[] buffer = new byte[stream.Length];
        MemoryStream memoryStream = new MemoryStream(buffer.Length);
        stream.Read(buffer, 0, buffer.Length);
        memoryStream.Write(buffer, 0, buffer.Length);
        memoryStream.Seek(0L, SeekOrigin.Begin);
        return memoryStream;
      }
      finally
      {
        stream?.Close();
      }
    }
  }
}
