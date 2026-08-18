// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.FileStorage.FileStorageView
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Bars;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;

#nullable disable
namespace Intermech.DatabaseConfigurator.FileStorage;

[ViewDescriptionProvider(typeof (FileStorageView.FileStorageViewDescriptionProvider))]
internal class FileStorageView : ChildrenView
{
  internal static ConditionStructure[] FilterStructures;
  private const string _stateStreamName = "FileStorageView";
  private int _imageIndex = -1;
  private static FileStorageFilterForm _filterForm;
  private IContainer components;
  private StatusStrip statusStrip1;
  private ToolStripStatusLabel toolStripStatusLabel1;
  private ButtonItem tbiFilter;

  public FileStorageView()
  {
    this.InitializeComponent();
    this._embeddedViewsDropDownMenuItem.Visible = false;
    INamedImageList service = (INamedImageList) ServicesManager.GetService(typeof (INamedImageList));
    if (service != null)
      this._imageIndex = service.ImageIndex("imgFilesList");
    this.SelectedItemsChanged += new EventHandler(this.fileGridSelectionChanged);
    this.UpdateFilterButton();
    this.AllowEditing = false;
  }

  public override ContentType ViewContentType => ContentType.NonFolders;

  protected override bool UseInheritedNavViews
  {
    get => false;
    set => base.UseInheritedNavViews = false;
  }

  public override string Caption => LocalizationHolder.rm.GetString("DatabaseConfigurator_43");

  public override int OrderID => 50;

  public override int ImageIndex => this._imageIndex;

  public override string StateStreamPrefix => nameof (FileStorageView);

  protected override bool ShowContextMenu(Point location)
  {
    this._grid.CurCell = this.GetCellCursor(location);
    return base.ShowContextMenu(location);
  }

  protected override ISelectedItems GetItemsForContextMenu(Point location)
  {
    if (this.GetCellCursor(location) != null)
      return base.GetItemsForContextMenu(location);
    this._grid.PerformAction(iGActions.DeselectAll);
    return (ISelectedItems) this._parentSelItem;
  }

  private void fileGridSelectionChanged(object sender, EventArgs e)
  {
    string str = string.Empty;
    long val = 0;
    if (this.SelectedItems.Count > 0)
    {
      for (int index = 0; index < this.SelectedItems.Count; ++index)
      {
        FileNodeID itemId = (FileNodeID) this.SelectedItems.GetItemID(index);
        val += itemId.FileZipSize;
      }
      str = $"{LocalizationHolder.rm.GetString("DatabaseConfigurator_44")}{Win32Subst.StrFormatByteSize(val, 1)}, {string.Format(LocalizationHolder.rm.GetString("DatabaseConfigurator_45"), (object) this.SelectedItems.Count, (object) (this._grid.Rows.Count - this._groupRowsCount))}";
    }
    this.toolStripStatusLabel1.Text = str;
  }

  private void tbFilter_Click(object sender, EventArgs e)
  {
    if (FileStorageView._filterForm == null)
      FileStorageView._filterForm = new FileStorageFilterForm();
    if (FileStorageView._filterForm.ShowDialog() != DialogResult.OK)
      return;
    FileStorageView.FilterStructures = FileStorageView._filterForm.ConditionStructures;
    this.UpdateFilterButton();
    this.ReloadItems();
  }

  private void UpdateFilterButton()
  {
    this.tbiFilter.Text = LocalizationHolder.rm.GetString("DatabaseConfigurator_46") + (FileStorageView.FilterStructures != null ? LocalizationHolder.rm.GetString("DatabaseConfigurator_47") : string.Empty);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
    {
      this.SelectedItemsChanged -= new EventHandler(this.fileGridSelectionChanged);
      this.components.Dispose();
    }
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FileStorageView));
    this.statusStrip1 = new StatusStrip();
    this.toolStripStatusLabel1 = new ToolStripStatusLabel();
    this.tbiFilter = new ButtonItem();
    ((ISupportInitialize) this._grid).BeginInit();
    this.statusStrip1.SuspendLayout();
    this.SuspendLayout();
    this._toolBar.AccessibleDescription = (string) null;
    this._toolBar.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this._toolBar, "tbViewBar");
    this._toolBar.BackgroundImage = (Image) null;
    this._toolBar.Font = (Font) null;
    this._toolBar.Items.AddRange(new ToolbarItemBase[1]
    {
      (ToolbarItemBase) this.tbiFilter
    });
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
    this.statusStrip1.AccessibleDescription = (string) null;
    this.statusStrip1.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.statusStrip1, "statusStrip1");
    this.statusStrip1.BackgroundImage = (Image) null;
    this.statusStrip1.Font = (Font) null;
    this.statusStrip1.Items.AddRange(new ToolStripItem[1]
    {
      (ToolStripItem) this.toolStripStatusLabel1
    });
    this.statusStrip1.Name = "statusStrip1";
    this.statusStrip1.SizingGrip = false;
    this.statusStrip1.Stretch = false;
    this._toolTip.SetToolTip((System.Windows.Forms.Control) this.statusStrip1, componentResourceManager.GetString("statusStrip1.ToolTip"));
    this.toolStripStatusLabel1.AccessibleDescription = (string) null;
    this.toolStripStatusLabel1.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.toolStripStatusLabel1, "toolStripStatusLabel1");
    this.toolStripStatusLabel1.BackgroundImage = (Image) null;
    this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
    componentResourceManager.ApplyResources((object) this.tbiFilter, "tbiFilter");
    this.tbiFilter.Image = (Image) Intermech.DatabaseConfigurator.Properties.Resources.search1;
    this.tbiFilter.ShowText = true;
    this.tbiFilter.Click += new EventHandler(this.tbFilter_Click);
    this.AccessibleDescription = (string) null;
    this.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.BackgroundImage = (Image) null;
    this.Controls.Add((System.Windows.Forms.Control) this.statusStrip1);
    this.Font = (Font) null;
    this.Name = nameof (FileStorageView);
    this.Tag = (object) " ";
    this._toolTip.SetToolTip((System.Windows.Forms.Control) this, componentResourceManager.GetString("$this.ToolTip"));
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this.statusStrip1, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this._toolBar, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this._grid, 0);
    ((ISupportInitialize) this._grid).EndInit();
    this.statusStrip1.ResumeLayout(false);
    this.statusStrip1.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private sealed class FileStorageViewDescriptionProvider : BaseViewDescriptionProvider
  {
    public override ViewDescription DoGetViewDescription(
      ISelectedItems selectedItems,
      System.IServiceProvider serviceProvider)
    {
      if (!(serviceProvider.GetService(typeof (INamedImageList)) is INamedImageList service))
        service = ServicesManager.GetService(typeof (INamedImageList)) as INamedImageList;
      INamedImageList namedImageList = service;
      return new ViewDescription()
      {
        Caption = LocalizationHolder.rm.GetString("DatabaseConfigurator_43"),
        ImageIndex = namedImageList != null ? namedImageList.ImageIndex("imgFilesList") : -1,
        OrderID = 50
      };
    }
  }
}
