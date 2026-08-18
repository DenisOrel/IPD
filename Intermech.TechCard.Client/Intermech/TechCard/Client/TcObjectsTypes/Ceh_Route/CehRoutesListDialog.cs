// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route.CehRoutesListDialog
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using DevExpress.IM.XtraTreeList;
using DevExpress.IM.XtraTreeList.Columns;
using DevExpress.IM.XtraTreeList.Nodes;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.TechCard;
using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.TechCard.Client.Navigator.Descriptors;
using Intermech.TechCard.Client.Tools.Controls.Navigator;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route;

/// <summary>Диалог выбора объектов типа "Расцеховочный маршрут"</summary>
internal class CehRoutesListDialog : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  internal Button btnOk;
  internal Panel pnlBottom;
  internal Panel pnlButtons;
  internal Button btnCancel;
  internal TreeList tlCehRoute_;
  private TreeListColumn treeListColumn1;
  private TreeListColumn treeListColumn2;
  private TreeListColumn treeListColumn11;
  internal TechCardNavObjListControl tcnolcCehRouteList;

  /// <summary>Get default columns for control</summary>
  /// <returns></returns>
  private NodeColumnCollection GetDefaultColumns()
  {
    NodeColumnCollection defaultColumns = new NodeColumnCollection();
    IColumnSchemes service = ServiceUtils.GetService<IColumnSchemes>((object) ApplicationServices.Container, false);
    defaultColumns.Add(service.CreateColumn(Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid, (object) ObligatoryObjectAttributes.F_OBJECT_ID));
    defaultColumns.Add(service.CreateColumn(Intermech.Navigator.Consts.CurrentObjectColumnSchemeGuid, (object) TechCardConsts.AttributeTypes.NameAttrTypeID));
    defaultColumns.Add(service.CreateColumn(Intermech.Navigator.Consts.CurrentObjectColumnSchemeGuid, (object) TechCardConsts.AttributeTypes.DesignationAttrTypeID));
    defaultColumns.Add(service.CreateColumn(Intermech.Navigator.Consts.CurrentObjectColumnSchemeGuid, (object) TechCardConsts.AttributeTypes.RouteStringAttrID));
    return defaultColumns;
  }

  /// <summary>
  /// 
  /// </summary>
  private void InitializeServices()
  {
    Icon icon = ServiceUtils.GetService<ICategoryTypeIconService>((object) ApplicationServices.Container, false)?.GetIcon(4, TechCardConsts.ObjectTypes.CehRouteID);
    if (icon == null)
      return;
    this.Icon = icon;
  }

  /// <summary>Конструктор</summary>
  private CehRoutesListDialog()
  {
    this.InitializeComponent();
    if (this.DesignMode)
      return;
    this.InitializeServices();
  }

  /// <summary>
  /// 
  /// </summary>
  public void LoadData(IEnumerable<long> objectIds)
  {
    this.tcnolcCehRouteList.LoadData(objectIds.ToList<long>(), TechCardConsts.ObjectTypes.CehRouteID, TechObjectListMode.UniqueValue);
    if (this.tcnolcCehRouteList.CustomDescriptor is TechObjectListDescriptor)
      this.tcnolcCehRouteList.SetColumns(this.GetDefaultColumns(), true);
    this.tcnolcCehRouteList.Activate((IView) null);
  }

  /// <summary>Ид. версии выбранного шаблона</summary>
  public ISelectedItems SelectedItems => this.tcnolcCehRouteList.SelectedItems;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tlCehRoute_CheckStateChanging(object sender, CheckStateEventArgs e)
  {
    if (!(sender is TreeList))
      return;
    TreeList treeList = (TreeList) sender;
    if (e.OldValue == CheckState.Indeterminate && e.OldValue != e.NewValue)
    {
      e.NewValue = e.OldValue;
    }
    else
    {
      if (e.NewValue != CheckState.Checked)
        return;
      treeList.CheckStateChanging -= new CheckStateChangingEventHandler(this.tlCehRoute_CheckStateChanging);
      try
      {
        foreach (TreeListNode node in treeList.Nodes)
        {
          if (node != e.Node && node.CheckState == CheckState.Checked)
            node.CheckState = CheckState.Unchecked;
        }
      }
      finally
      {
        treeList.CheckStateChanging += new CheckStateChangingEventHandler(this.tlCehRoute_CheckStateChanging);
      }
    }
  }

  /// <summary>Вызов диалога</summary>
  /// <param name="caption"></param>
  /// <param name="objectIds"></param>
  /// <param name="selectedObjectId"></param>
  /// <returns></returns>
  public static bool ShowDialog(
    string caption,
    IEnumerable<long> objectIds,
    out long selectedObjectId)
  {
    selectedObjectId = 0L;
    Form form = new Form();
    CehRoutesListDialog routesListDialog = new CehRoutesListDialog();
    routesListDialog.pnlButtons.Visible = true;
    routesListDialog.tcnolcCehRouteList.Grid.SelectionMode = iGSelectionMode.One;
    routesListDialog.StartPosition = FormStartPosition.CenterScreen;
    routesListDialog.Size = new Size(520, 350);
    routesListDialog.FormBorderStyle = FormBorderStyle.SizableToolWindow;
    routesListDialog.Text = caption;
    routesListDialog.LoadData(objectIds);
    int num = (int) form.ShowDialog();
    if (form.ShowDialog() != DialogResult.OK)
      return false;
    if (routesListDialog.SelectedItems.Count != 0 && routesListDialog.SelectedItems.GetItemData(0, typeof (IDBObjectID)) is IDBObjectID itemData)
      selectedObjectId = itemData.Value;
    return true;
  }

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
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (CehRoutesListDialog));
    this.btnOk = new Button();
    this.pnlBottom = new Panel();
    this.pnlButtons = new Panel();
    this.btnCancel = new Button();
    this.tlCehRoute_ = new TreeList();
    this.treeListColumn1 = new TreeListColumn();
    this.treeListColumn2 = new TreeListColumn();
    this.treeListColumn11 = new TreeListColumn();
    this.tcnolcCehRouteList = new TechCardNavObjListControl();
    this.pnlBottom.SuspendLayout();
    this.pnlButtons.SuspendLayout();
    this.tlCehRoute_.BeginInit();
    this.SuspendLayout();
    this.btnOk.DialogResult = DialogResult.OK;
    componentResourceManager.ApplyResources((object) this.btnOk, "btnOk");
    this.btnOk.Name = "btnOk";
    this.btnOk.UseVisualStyleBackColor = true;
    this.pnlBottom.Controls.Add((Control) this.pnlButtons);
    componentResourceManager.ApplyResources((object) this.pnlBottom, "pnlBottom");
    this.pnlBottom.Name = "pnlBottom";
    this.pnlButtons.Controls.Add((Control) this.btnCancel);
    this.pnlButtons.Controls.Add((Control) this.btnOk);
    componentResourceManager.ApplyResources((object) this.pnlButtons, "pnlButtons");
    this.pnlButtons.Name = "pnlButtons";
    this.btnCancel.DialogResult = DialogResult.Cancel;
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.tlCehRoute_, "tlCehRoute_");
    this.tlCehRoute_.CheckBoxes = CheckBoxesStyle.TwoState;
    this.tlCehRoute_.Columns.AddRange(new TreeListColumn[3]
    {
      this.treeListColumn1,
      this.treeListColumn2,
      this.treeListColumn11
    });
    this.tlCehRoute_.Name = "tlCehRoute_";
    this.tlCehRoute_.CheckStateChanging += new CheckStateChangingEventHandler(this.tlCehRoute_CheckStateChanging);
    componentResourceManager.ApplyResources((object) this.treeListColumn1, "treeListColumn1");
    this.treeListColumn1.Name = "treeListColumn1";
    componentResourceManager.ApplyResources((object) this.treeListColumn2, "treeListColumn2");
    this.treeListColumn2.Name = "treeListColumn2";
    componentResourceManager.ApplyResources((object) this.treeListColumn11, "treeListColumn11");
    this.treeListColumn11.Name = "treeListColumn11";
    this.tcnolcCehRouteList.AllowCustomGroupValues = true;
    this.tcnolcCehRouteList.Control = (object) this.tcnolcCehRouteList;
    this.tcnolcCehRouteList.CustomContextMenuStrip = (ContextMenuStrip) null;
    this.tcnolcCehRouteList.DisableColumnsGrouping = true;
    this.tcnolcCehRouteList.DisableGroupBox = true;
    this.tcnolcCehRouteList.DisableIMContextMenu = true;
    this.tcnolcCehRouteList.DisableKeyDownEvents = false;
    this.tcnolcCehRouteList.DisableStatusBar = true;
    this.tcnolcCehRouteList.DisableToolBar = true;
    componentResourceManager.ApplyResources((object) this.tcnolcCehRouteList, "tcnolcCehRouteList");
    this.tcnolcCehRouteList.EmbeddedFocusAndSelection = (iFocusAndSelection) null;
    this.tcnolcCehRouteList.Name = "tcnolcCehRouteList";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.tcnolcCehRouteList);
    this.Controls.Add((Control) this.tlCehRoute_);
    this.Controls.Add((Control) this.pnlBottom);
    this.Name = nameof (CehRoutesListDialog);
    this.Tag = (object) " ";
    this.pnlBottom.ResumeLayout(false);
    this.pnlButtons.ResumeLayout(false);
    this.tlCehRoute_.EndInit();
    this.ResumeLayout(false);
  }
}
