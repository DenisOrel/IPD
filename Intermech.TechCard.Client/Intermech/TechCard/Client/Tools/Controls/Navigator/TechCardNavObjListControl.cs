// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Tools.Controls.Navigator.TechCardNavObjListControl
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Search.iGrid;
using Intermech.TechCard.Client.Navigator.Descriptors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using TenTec.Windows.iGridLib;

#nullable disable
namespace Intermech.TechCard.Client.Tools.Controls.Navigator;

/// <summary>TechCard custom objects list control</summary>
public class TechCardNavObjListControl : ObjectsViewBase
{
  /// <summary>Root custom category's id</summary>
  private int _rootCategoryId;
  /// <summary>Root custom category guid</summary>
  private Guid _rootCategoryGuid = Guid.Empty;
  /// <summary>Custom descriptor to show object's list</summary>
  private IDescriptor _customDescriptor;
  /// <summary>Custom services (by default)</summary>
  private System.IServiceProvider _customServices;
  /// <summary>Custom context menu strip</summary>
  private ContextMenuStrip _customContextMenuStrip;

  /// <summary>
  /// 
  /// </summary>
  private void InitializeComponent()
  {
    ((ISupportInitialize) this._grid).BeginInit();
    ((ISupportInitialize) this._pictureBox).BeginInit();
    this.SuspendLayout();
    this._toolBar.Visible = false;
    this._grid.DefaultAutoGroupRow.Height = 21;
    this._grid.FrozenArea.ColCount = 1;
    this._grid.FrozenArea.SortFrozenRows = true;
    this._grid.GroupBox.BackColor = SystemColors.GrayText;
    this._grid.GroupBox.HintBackColor = SystemColors.GrayText;
    this._grid.GroupBox.HintForeColor = SystemColors.ControlText;
    this._grid.GroupBox.Text = "Перетащите заголовок колонки в эту область для группировки по значениям этой колонки";
    this._grid.GroupBox.Visible = true;
    this._grid.Header.AutoHeightFlags = iGHdrAutoHeightFlags.OnAddCol | iGHdrAutoHeightFlags.OnRemoveCol | iGHdrAutoHeightFlags.OnShowCol | iGHdrAutoHeightFlags.OnContentsChange | iGHdrAutoHeightFlags.OnThemeChange | iGHdrAutoHeightFlags.OnResizeCol;
    this._grid.Header.Height = 19;
    this._grid.LayoutObject.Flags = iGLayoutFlags.Grouping | iGLayoutFlags.Sorting | iGLayoutFlags.ColVisibility | iGLayoutFlags.ColWidth | iGLayoutFlags.ColOrder;
    this._grid.Size = new Size(602, 130);
    this.buttonHeightSet.Padding.Bottom = 0;
    this.buttonHeightSet.Padding.Left = 0;
    this.buttonHeightSet.Padding.Right = 0;
    this.buttonHeightSet.Padding.Top = 0;
    this._filtersComboBoxItem.Locked = false;
    this._filtersComboBoxItem.Padding.Bottom = 0;
    this._filtersComboBoxItem.Padding.Left = 1;
    this._filtersComboBoxItem.Padding.Right = 1;
    this._filtersComboBoxItem.Padding.Top = 0;
    this.Name = nameof (TechCardNavObjListControl);
    ((ISupportInitialize) this._grid).EndInit();
    ((ISupportInitialize) this._pictureBox).EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  /// <summary>Initialize custom controls</summary>
  private void InitializeCustomComponents()
  {
  }

  /// <summary>Initialize custom data</summary>
  private void InitializeCustomProperties()
  {
    IServiceContainer serviceContainer = (IServiceContainer) new ServiceContainer();
    serviceContainer.AddService(typeof (IViewState), (object) new ViewStateService());
    this._customServices = (System.IServiceProvider) serviceContainer;
    this.DisableGroupBox = true;
    this.DisableToolBar = true;
    this.DisableStatusBar = true;
  }

  /// <summary>Show custom menu handler</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  protected virtual void DoShowContextMenu(object sender, ContextMenuEventArgs e)
  {
    ContextMenuStrip contextMenuStrip = this.CustomContextMenuStrip;
    if (contextMenuStrip == null || contextMenuStrip.Visible)
      return;
    contextMenuStrip.Show(e.Control, e.Location);
  }

  /// <summary>
  /// 
  /// </summary>
  protected void FireItemsChanged()
  {
    EventHandler itemsChanged = this.ItemsChanged;
    if (itemsChanged == null)
      return;
    itemsChanged((object) this, (EventArgs) null);
  }

  /// <summary>
  /// 
  /// </summary>
  private void RegisterRootCategory()
  {
    IGuidMapper service = ServiceUtils.GetService<IGuidMapper>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    if (this._rootCategoryGuid == Guid.Empty)
      this._rootCategoryGuid = Guid.NewGuid();
    this._rootCategoryId = service.Register(this._rootCategoryGuid);
  }

  /// <summary>
  /// 
  /// </summary>
  private void UnRegisterRootCategory()
  {
    if (this._rootCategoryId == 0)
      return;
    IGuidMapper service = ServiceUtils.GetService<IGuidMapper>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    service.Unregister(this._rootCategoryId);
    this._rootCategoryId = 0;
  }

  /// <summary>Constructor</summary>
  public TechCardNavObjListControl()
  {
    this.InitializeCustomComponents();
    this.InitializeCustomProperties();
  }

  /// <summary>Dispose class data</summary>
  /// <param name="disposing"></param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
      this.UnRegisterRootCategory();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Категория для названия потока с сохранёнными настройками
  /// </summary>
  protected override int StateStreamCategoryID => 0;

  /// <summary>Название потока, в котором будут сохранены настройки</summary>
  public override string StateStreamPrefix => "ControlState_" + this.Name;

  /// <summary>Eof of data source</summary>
  protected override bool Eof => true;

  /// <summary>Initialize control</summary>
  /// <param name="rootDescriptor"></param>
  /// <param name="services"></param>
  public override void Initialize(IDescriptor rootDescriptor, System.IServiceProvider services)
  {
    base.Initialize(rootDescriptor, services);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  protected override void GridMouseDoubleClick(object sender, EventArgs e)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  public new void NotificationEventFired(object sender, NotificationEventArgs e)
  {
    base.NotificationEventFired(sender, e);
  }

  /// <summary>Отпущена клавиша в grid</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected override void Grid_KeyUp(object sender, KeyEventArgs e)
  {
    base.Grid_KeyUp(sender, e);
    if (e.Handled || this.CustomContextMenuStrip == null)
      return;
    Keys pressed = e.KeyCode;
    if (e.Control)
      pressed |= Keys.Control;
    if (e.Shift)
      pressed |= Keys.Shift;
    if (e.Alt)
      pressed |= Keys.Alt;
    Action<ToolStripItemCollection> checkShortcut = (Action<ToolStripItemCollection>) null;
    checkShortcut = (Action<ToolStripItemCollection>) (items =>
    {
      if (items == null)
        return;
      foreach (ToolStripItem toolStripItem in (ArrangedElementCollection) items)
      {
        if (toolStripItem is ToolStripMenuItem toolStripMenuItem2)
        {
          if (toolStripMenuItem2.ShortcutKeys == pressed)
          {
            toolStripMenuItem2.PerformClick();
            e.SuppressKeyPress = true;
            break;
          }
          checkShortcut(toolStripMenuItem2.DropDownItems);
        }
      }
    });
    checkShortcut(this.CustomContextMenuStrip.Items);
  }

  /// <summary>Load Data</summary>
  /// <param name="objIdList">Object's ids list</param>
  public void LoadData(List<long> objIdList)
  {
    this.LoadData(objIdList, -1, TechObjectListMode.UniqueValue);
  }

  /// <summary>Load Data</summary>
  /// <param name="objIdList">Object's ids list</param>
  /// <param name="objTypeId">Object type id</param>
  /// <param name="multiMode">Mode</param>
  public void LoadData(List<long> objIdList, int objTypeId, TechObjectListMode multiMode)
  {
    this.LoadData(objIdList, objTypeId, multiMode, string.Empty);
  }

  /// <summary>Load Data</summary>
  /// <param name="objIdList">Object's ids list</param>
  /// <param name="objTypeId">Object type id</param>
  /// <param name="multiMode">Mode</param>
  /// <param name="rootCaption">Root node's caption</param>
  public void LoadData(
    List<long> objIdList,
    int objTypeId,
    TechObjectListMode multiMode,
    string rootCaption)
  {
    this.LoadData(objIdList, objTypeId, multiMode, rootCaption, this._customServices);
  }

  /// <summary>Load data</summary>
  /// <param name="objIdList">Object's ids list</param>
  /// <param name="objTypeId">Object type id</param>
  /// <param name="multiMode">Mode</param>
  /// <param name="rootCaption">Root node's caption</param>
  /// <param name="customServices">Custom services</param>
  public void LoadData(
    List<long> objIdList,
    int objTypeId,
    TechObjectListMode multiMode,
    string rootCaption,
    System.IServiceProvider customServices)
  {
    if (this._rootCategoryId == 0)
      this.RegisterRootCategory();
    this.LoadData((IDescriptor) new TechObjectListDescriptor(this._rootCategoryId, objTypeId, rootCaption, (IList) objIdList)
    {
      Mode = multiMode
    }, customServices);
  }

  /// <summary>Load data</summary>
  /// <param name="customDescriptor"></param>
  public void LoadData(IDescriptor customDescriptor)
  {
    this.LoadData(customDescriptor, this._customServices);
  }

  /// <summary>Load data</summary>
  /// <param name="customDescriptor"></param>
  /// <param name="customServices"></param>
  public void LoadData(IDescriptor customDescriptor, System.IServiceProvider customServices)
  {
    this._customDescriptor = customDescriptor;
    this.Initialize(customDescriptor, customServices);
  }

  /// <summary>
  /// Установить или убрать выделение ячейкам указанной строки
  /// </summary>
  /// <param name="row">Строка</param>
  /// <param name="select">true - установить выделение, false - убрать</param>
  public void GridSelectRowCells(iGRow row, bool select)
  {
    if (row == null)
      return;
    row.SetSelectedForAllCells(select);
  }

  /// <summary>Очистить в grid список выделенных ячеек</summary>
  /// <param name="lockGrid">Блокировать прорисовку в grid</param>
  public new void GridDeselectAll(bool lockGrid) => base.GridDeselectAll(lockGrid);

  /// <summary>Root category guid</summary>
  public Guid RootCategoryGuid => this._rootCategoryGuid;

  /// <summary>Control's root category</summary>
  public int RootCategoryID => this._rootCategoryId;

  /// <summary>Custom context menu strip</summary>
  [Browsable(true)]
  [DisplayName("CustomContextMenuStrip")]
  [Category("Behavior")]
  public ContextMenuStrip CustomContextMenuStrip
  {
    get => this._customContextMenuStrip;
    set
    {
      this.ShowCustomContextMenu -= new EventHandler<ContextMenuEventArgs>(this.DoShowContextMenu);
      this._customContextMenuStrip = value;
      if (value == null)
        return;
      this.ShowCustomContextMenu += new EventHandler<ContextMenuEventArgs>(this.DoShowContextMenu);
    }
  }

  /// <summary>Custom descriptor to show object's list</summary>
  public IDescriptor CustomDescriptor => this._customDescriptor;

  /// <summary>Custom services</summary>
  public System.IServiceProvider CustomServices => this._customServices;

  /// <summary>
  /// 
  /// </summary>
  public event EventHandler ItemsChanged;
}
