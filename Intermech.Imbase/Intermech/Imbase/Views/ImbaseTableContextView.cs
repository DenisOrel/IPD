// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Views.ImbaseTableContextView
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.DataFormats;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System.ComponentModel;
using System.Drawing;
using TenTec.Windows.iGridLib;

#nullable disable
namespace Intermech.Imbase.Views;

public class ImbaseTableContextView : ObjectsViewBase
{
  internal static int _imageIndex = -1;
  internal IImbaseSelector _selectorService;

  public ImbaseTableContextView()
  {
    this._selectorService = ServicesManager.GetService(typeof (IImbaseSelector)) as IImbaseSelector;
  }

  public override int OrderID => 0;

  public override ContentType ViewContentType => ContentType.NonFolders;

  public override int ImageIndex => ImbaseTableContextView._imageIndex;

  public override void Activate(IView previousView)
  {
    base.Activate(previousView);
    if (this._selectorService == null || !(this._node.GetData(this._nodeID, typeof (IDBObjectID)) is IDBObjectID data))
      return;
    this._selectorService.ContextObjectId = data.Value;
  }

  public override void Deactivate(IView nextView)
  {
    base.Deactivate(nextView);
    if (nextView == null || this._selectorService == null)
      return;
    this._selectorService.ContextObjectId = -1L;
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ImbaseTableContextView));
    ((ISupportInitialize) this._grid).BeginInit();
    ((ISupportInitialize) this._pictureBox).BeginInit();
    this.SuspendLayout();
    this._toolBar.AccessibleDescription = (string) null;
    this._toolBar.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this._toolBar, "tbViewBar");
    this._toolBar.BackgroundImage = (Image) null;
    this._toolBar.Font = (Font) null;
    this._toolTip.SetToolTip((System.Windows.Forms.Control) this._toolBar, componentResourceManager.GetString("tbViewBar.ToolTip"));
    componentResourceManager.ApplyResources((object) this._embeddedViewsDropDownMenuItem, "btViewNames");
    componentResourceManager.ApplyResources((object) this._toggleManualSortingButtonItem, "btClearSorting");
    this._grid.AccessibleDescription = (string) null;
    this._grid.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this._grid, "grid");
    this._grid.BackgroundImage = (Image) null;
    this._grid.DefaultAutoGroupRow.Height = 21;
    this._grid.DefaultRow.Height = (int) componentResourceManager.GetObject("resource.Height");
    this._grid.DefaultRow.Key = (string) null;
    this._grid.DefaultRow.NormalCellHeight = (int) componentResourceManager.GetObject("resource.NormalCellHeight");
    this._grid.Font = (Font) null;
    this._grid.FrozenArea.ColCount = 1;
    this._grid.FrozenArea.SortFrozenRows = true;
    this._grid.GroupBox.BackColor = SystemColors.GrayText;
    this._grid.GroupBox.HintBackColor = SystemColors.GrayText;
    this._grid.GroupBox.HintForeColor = SystemColors.ControlText;
    this._grid.GroupBox.Text = componentResourceManager.GetString("grid.GroupBox.Text");
    this._grid.GroupBox.Visible = true;
    this._grid.Header.AutoHeightFlags = iGHdrAutoHeightFlags.OnAddCol | iGHdrAutoHeightFlags.OnRemoveCol | iGHdrAutoHeightFlags.OnShowCol | iGHdrAutoHeightFlags.OnContentsChange | iGHdrAutoHeightFlags.OnThemeChange | iGHdrAutoHeightFlags.OnResizeCol;
    this._grid.Header.Height = (int) componentResourceManager.GetObject("grid.Header.Height");
    this._grid.LayoutObject.Flags = iGLayoutFlags.Grouping | iGLayoutFlags.Sorting | iGLayoutFlags.ColVisibility | iGLayoutFlags.ColWidth | iGLayoutFlags.ColOrder;
    this._toolTip.SetToolTip((System.Windows.Forms.Control) this._grid, componentResourceManager.GetString("grid.ToolTip"));
    componentResourceManager.ApplyResources((object) this._collapseAllGroupsButtonItem, "btCollapseAll");
    componentResourceManager.ApplyResources((object) this._expandAllGroupsButtonItem, "btExpandAll");
    this._pageViewsManager.AccessibleDescription = (string) null;
    this._pageViewsManager.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this._pageViewsManager, "ViewsManager");
    this._pageViewsManager.BackgroundImage = (Image) null;
    this._pageViewsManager.Font = (Font) null;
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
    this._gridHeaderMenuBar.AccessibleDescription = (string) null;
    this._gridHeaderMenuBar.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this._gridHeaderMenuBar, "menuHeader");
    this._gridHeaderMenuBar.BackgroundImage = (Image) null;
    this._gridHeaderMenuBar.Font = (Font) null;
    this._toolTip.SetToolTip((System.Windows.Forms.Control) this._gridHeaderMenuBar, componentResourceManager.GetString("menuHeader.ToolTip"));
    componentResourceManager.ApplyResources((object) this._gridHeaderContextMenuBarItem, "contextMenuHeader");
    componentResourceManager.ApplyResources((object) this._changeGridColumnsMenuButtonItem, "mnpSetupColumns");
    componentResourceManager.ApplyResources((object) this._collapseAllGroupsExpectGroupsWithFocusedItemsButtonItem, "btCollapseAndShow");
    this._pictureBox.AccessibleDescription = (string) null;
    this._pictureBox.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this._pictureBox, "pictureView");
    this._pictureBox.BackgroundImage = (Image) null;
    this._pictureBox.Font = (Font) null;
    this._pictureBox.ImageLocation = (string) null;
    this._toolTip.SetToolTip((System.Windows.Forms.Control) this._pictureBox, componentResourceManager.GetString("pictureView.ToolTip"));
    this.AccessibleDescription = (string) null;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.BackgroundImage = (Image) null;
    this.Font = (Font) null;
    this.Name = nameof (ImbaseTableContextView);
    this._toolTip.SetToolTip((System.Windows.Forms.Control) this, componentResourceManager.GetString("$this.ToolTip"));
    ((ISupportInitialize) this._grid).EndInit();
    ((ISupportInitialize) this._pictureBox).EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
