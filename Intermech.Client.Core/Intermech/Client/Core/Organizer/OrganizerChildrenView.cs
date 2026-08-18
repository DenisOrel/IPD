
// Type: Intermech.Client.Core.Organizer.OrganizerChildrenView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;


namespace Intermech.Client.Core.Organizer;

/// <summary>Закладка для отображения элементов органайзера.</summary>
[ToolboxItem(false)]
public class OrganizerChildrenView : ChildrenView, ICanCloseViews, ICanDeactivateView
{
  private string _caption = string.Empty;
  private int _imageIndex = -1;
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  /// <summary>Наименование закладки.</summary>
  public override string Caption
  {
    get => !string.IsNullOrEmpty(this._caption) ? this._caption : base.Caption;
  }

  /// <summary>Идентификатор иконки.</summary>
  public override int ImageIndex => this._imageIndex;

  /// <summary>
  /// 
  /// </summary>
  public override int OrderID => 1;

  /// <summary>
  /// 
  /// </summary>
  public override ContentType ViewContentType => ContentType.NonFolders;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="services"></param>
  public override void Initialize(ISelectedItems items, System.IServiceProvider services)
  {
    base.Initialize(items, services);
    INodeID itemId = items.GetItemID(0);
    if (itemId == null)
      return;
    OrganizerChildNodeDescriptor childNodeDescriptor = (OrganizerChildNodeDescriptor) null;
    if (ServicesManager.GetService(typeof (IOrganizerService)) is OrganizerService service)
      childNodeDescriptor = service.GetDescriptor(itemId.CategoryID);
    if (childNodeDescriptor != null)
    {
      this._imageIndex = -1;
      this._caption = childNodeDescriptor.Caption;
    }
    else
    {
      this._imageIndex = ChildrenView._namedImageList.ImageIndex("imgContains");
      this._caption = base.Caption;
    }
  }

  /// <summary>
  /// Выполнить запрос, можно ли закрывать форму, на которой расположены закладки.
  /// </summary>
  /// <param name="sender"></param>
  /// <returns></returns>
  public bool CanClose(object sender) => true;

  /// <summary>
  /// Выполнить запрос, можно ли деактивировать текущую закладку.
  /// </summary>
  /// <param name="sender"></param>
  /// <returns></returns>
  public bool CanDeactivate(object sender) => this.CanClose(sender);

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (OrganizerChildrenView));
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
    this.AutoScaleMode = AutoScaleMode.Font;
    this.DoubleBuffered = true;
    this.Name = nameof (OrganizerChildrenView);
    this._toolTip.SetToolTip((System.Windows.Forms.Control) this, componentResourceManager.GetString("$this.ToolTip"));
    ((ISupportInitialize) this._grid).EndInit();
    ((ISupportInitialize) this._pictureBox).EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
