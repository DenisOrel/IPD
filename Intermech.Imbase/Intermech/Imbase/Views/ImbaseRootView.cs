// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Views.ImbaseRootView
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Client.Core;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;

#nullable disable
namespace Intermech.Imbase.Views;

public class ImbaseRootView : ObjectsViewBase
{
  private static int _imageIndex = -1;

  protected override void GridMouseDoubleClick(object sender, EventArgs e)
  {
    if (this._ioDispatcher == null || this._dataAdapter == null || this.DisableDoubleClicks)
      return;
    Point pos = new Point(((MouseEventArgs) e).X, ((MouseEventArgs) e).Y);
    iGColHdr iGcolHdr = this._grid.Header.Cells.FromPoint(pos.X, pos.Y);
    iGCell cellCursor = this.GetCellCursor(pos);
    int num = -1;
    if (cellCursor != null)
    {
      iGRow row = cellCursor.Row;
      int colIndex = cellCursor.ColIndex;
      num = cellCursor.ColIndex;
    }
    INodeID nodeAtCursor = this.GetNodeAtCursor(pos);
    if (iGcolHdr != null || cellCursor == null || cellCursor.Value == null || nodeAtCursor == null || num <= -1)
      return;
    NodeIDPath nodeIdPathForView = this.GetSelectedNodeIDPathForView();
    this._ioDispatcher.ProcessEvent((IIOEvent) new IOEvent((IIOSource) this, IOEventFlags.efNone, IOEventType.evMouseDoubleClick, (object) e, (object) nodeIdPathForView));
  }

  public override int ImageIndex
  {
    get
    {
      if (ImbaseRootView._imageIndex == -1 && ChildrenView._namedImageList != null)
        ImbaseRootView._imageIndex = ChildrenView._namedImageList.ImageIndex("imgContains");
      return ImbaseRootView._imageIndex;
    }
  }

  public override ContentType ViewContentType => ContentType.NonFolders;

  private NodeIDPath GetSelectedNodeIDPathForView()
  {
    NodeIDPath nodeIdPathForView = (NodeIDPath) null;
    INodeID nodeIdForRow = this._grid.CurRow != null ? this.GetNodeIDForRow(this._grid.CurRow) : (INodeID) null;
    if (nodeIdForRow != null && (this.Node.GetAttributesOf(nodeIdForRow) & ContentAttributes.Folder) == ContentAttributes.Folder)
    {
      if (nodeIdForRow is INodeIDExtended)
        this._path = (nodeIdForRow as INodeIDExtended).CorrectPath(this._path, nodeIdForRow);
      nodeIdPathForView = new NodeIDPath(this._path, nodeIdForRow);
    }
    return nodeIdPathForView;
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ImbaseRootView));
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
    this.Name = nameof (ImbaseRootView);
    this._toolTip.SetToolTip((System.Windows.Forms.Control) this, componentResourceManager.GetString("$this.ToolTip"));
    ((ISupportInitialize) this._grid).EndInit();
    ((ISupportInitialize) this._pictureBox).EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
