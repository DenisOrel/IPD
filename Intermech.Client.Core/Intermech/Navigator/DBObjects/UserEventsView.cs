
// Type: Intermech.Navigator.DBObjects.UserEventsView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.EventLog;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.ComponentModel;
using System.Drawing;
using TenTec.Windows.iGridLib;


namespace Intermech.Navigator.DBObjects;

/// <summary>Страница вывода действий пользователя</summary>
[ViewDescriptionProvider(typeof (UserEventsView.UserEventsViewDescriptionProvider))]
public class UserEventsView : LinkedEventsView
{
  public override ConditionStructure[] Conditions
  {
    get
    {
      return new ConditionStructure[1]
      {
        new ConditionStructure(-36, RelationalOperators.Equal, (object) Math.Abs((this._parentNode.GetData(this._nodeID, typeof (IDBObjectID)) as IDBObjectID).Value), LogicalOperators.NONE, 0, false)
      };
    }
  }

  public override string Caption => LocalizationHolder.rm.GetString("Client.Core_323");

  public override string StateStreamPrefix => nameof (UserEventsView);

  public override int OrderID => 66;

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (UserEventsView));
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
    this._grid.GroupBox.Text = LocalizationHolder.rm.GetString("Client.Core_1340");
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
    this.Name = nameof (UserEventsView);
    this.Tag = (object) "  ";
    this._toolTip.SetToolTip((System.Windows.Forms.Control) this, componentResourceManager.GetString("$this.ToolTip"));
    ((ISupportInitialize) this._grid).EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private sealed class UserEventsViewDescriptionProvider : 
    LinkedEventsView.LinkedEventsViewDescriptionProvider
  {
    public override ViewDescription DoGetViewDescription(
      ISelectedItems selectedItems,
      System.IServiceProvider serviceProvider)
    {
      ViewDescription viewDescription = base.DoGetViewDescription(selectedItems, serviceProvider);
      viewDescription.Caption = LocalizationHolder.rm.GetString("Client.Core_323");
      viewDescription.OrderID = 66;
      return viewDescription;
    }
  }
}
