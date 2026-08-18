// Decompiled with JetBrains decompiler
// Type: Intermech.Security.SecurityRootView
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System.ComponentModel;
using System.Drawing;
using TenTec.Windows.iGridLib;

#nullable disable
namespace Intermech.Security;

internal class SecurityRootView : ChildrenView
{
  public override ContentType ViewContentType => ContentType.Folders;

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SecurityRootView));
    ((ISupportInitialize) this._grid).BeginInit();
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
    this._grid.Font = (Font) null;
    this._grid.FrozenArea.ColCount = 1;
    this._grid.FrozenArea.SortFrozenRows = true;
    this._grid.GroupBox.Text = "Перетащите заголовок колонки в эту область для группировки по значениям этой колонки";
    this._grid.GroupBox.Visible = true;
    this._grid.Header.AutoHeightFlags = iGHdrAutoHeightFlags.OnAddCol | iGHdrAutoHeightFlags.OnRemoveCol | iGHdrAutoHeightFlags.OnShowCol | iGHdrAutoHeightFlags.OnContentsChange | iGHdrAutoHeightFlags.OnThemeChange | iGHdrAutoHeightFlags.OnResizeCol;
    this._grid.Header.Height = 19;
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
    this.AccessibleDescription = (string) null;
    this.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.BackgroundImage = (Image) null;
    this.Font = (Font) null;
    this.Name = nameof (SecurityRootView);
    this.Tag = (object) " ";
    this._toolTip.SetToolTip((System.Windows.Forms.Control) this, componentResourceManager.GetString("$this.ToolTip"));
    ((ISupportInitialize) this._grid).EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
