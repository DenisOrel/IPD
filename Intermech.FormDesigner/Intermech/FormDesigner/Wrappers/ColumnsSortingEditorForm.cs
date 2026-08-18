// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.ColumnsSortingEditorForm
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.FormDesigner.Wrappers;

/// <summary>
/// Редактор задания колонок для сортировки по умолчанию контрола ObjectsList.
/// </summary>
public class ColumnsSortingEditorForm : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button _btnCancel;
  private Button _btnApply;
  private Panel pnlBottom;
  private SplitContainer _splitContainer;
  private ListView _lv;
  /// <summary>
  /// 
  /// </summary>
  protected ImageList _img;
  /// <summary>
  /// 
  /// </summary>
  protected Intermech.Bars.ToolBar toolBarLeft;
  /// <summary>
  /// 
  /// </summary>
  protected ButtonItem _btnAdd;
  /// <summary>
  /// 
  /// </summary>
  protected ButtonItem _btnAddAll;
  /// <summary>
  /// 
  /// </summary>
  protected ButtonItem _btnDelete;
  /// <summary>
  /// 
  /// </summary>
  protected ButtonItem _btnDeleteAll;
  private DataGridView _dgv;
  /// <summary>
  /// 
  /// </summary>
  protected Intermech.Bars.ToolBar toolBarRight;
  /// <summary>
  /// 
  /// </summary>
  protected ButtonItem _btnTop;
  /// <summary>
  /// 
  /// </summary>
  protected ButtonItem _btnUp;
  /// <summary>
  /// 
  /// </summary>
  protected ButtonItem _btnDown;
  /// <summary>
  /// 
  /// </summary>
  protected ButtonItem _btnBottom;
  private ColumnHeader _lvColCaption;
  private DataGridViewImageColumn _colImg;
  private DataGridViewTextBoxColumn _colCaption;
  private DataGridViewComboBoxColumn _colSortOrder;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

  /// <summary>Колонки, назначенные для сортировки по умолчанию.</summary>
  public NodeColumnCollection SortingColumns
  {
    get
    {
      NodeColumnCollection sortingColumns = this._dgv.Rows.Count > 0 ? new NodeColumnCollection(this._dgv.Rows.Count) : (NodeColumnCollection) null;
      foreach (DataGridViewRow row in (IEnumerable) this._dgv.Rows)
      {
        NodeColumn tag = row.Tag as NodeColumn;
        tag.SortIndex = row.Index;
        tag.SortOrder = (NodeColumnSortOrder) row.Cells["_colSortOrder"].Value;
        sortingColumns.Add(tag);
      }
      return sortingColumns;
    }
  }

  /// <summary>Конструктор.</summary>
  /// <param name="visibleColumns">Набор всех колонок, видимых пользователю</param>
  /// <param name="sortColumn">Набор колонок, по которым настроена сортировка</param>
  public ColumnsSortingEditorForm(
    NodeColumnCollection visibleColumns,
    NodeColumnCollection sortColumn)
  {
    this.InitializeComponent();
    this._lv.SmallImageList = this._lv.LargeImageList = Statics.IconSrv.ImageList;
    this.LoadData(visibleColumns, sortColumn);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_lst_DoubleClick(object sender, EventArgs e)
  {
    if (this._lv.SelectedItems == null)
      return;
    this.OnLeftRightBtn_Click((object) this._btnAdd, e);
    this.CheckEnableButtons();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_lst_SelectedIndexChanged(object sender, EventArgs e) => this.CheckEnableButtons();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_dgv_DoubleClick(object sender, EventArgs e)
  {
    if (this._dgv.SelectedRows.Count <= 0)
      return;
    this.OnLeftRightBtn_Click((object) this._btnDelete, e);
    this.CheckEnableButtons();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_dgv_SelectionChanged(object sender, EventArgs e) => this.CheckEnableButtons();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnLeftRightBtn_Click(object sender, EventArgs e)
  {
    int int32 = Convert.ToInt32((sender as ButtonItem).Tag);
    int num = 0;
    switch (int32)
    {
      case 0:
        foreach (ListViewItem selectedItem in this._lv.SelectedItems)
        {
          this.AddDataGridViewRow(selectedItem.Tag as NodeColumn);
          num = selectedItem.Index;
          this._lv.Items.Remove(selectedItem);
        }
        int index1 = this._lv.Items.Count == 1 ? 0 : (this._lv.Items.Count == num ? this._lv.Items.Count - 1 : num);
        if (this._lv.Items.Count > 0 && this._lv.Items.Count > index1)
          this._lv.Items[index1].Selected = true;
        this._dgv.ClearSelection();
        this._dgv.Rows[this._dgv.Rows.Count - 1].Selected = true;
        break;
      case 1:
        foreach (ListViewItem listViewItem in this._lv.Items)
          this.AddDataGridViewRow(listViewItem.Tag as NodeColumn);
        this._lv.Items.Clear();
        this._dgv.ClearSelection();
        this._dgv.Rows[this._dgv.Rows.Count - 1].Selected = true;
        break;
      case 2:
        foreach (DataGridViewRow selectedRow in (BaseCollection) this._dgv.SelectedRows)
        {
          this.AddListViewRow(selectedRow.Tag as NodeColumn);
          num = selectedRow.Index;
          this._dgv.Rows.Remove(selectedRow);
        }
        int index2 = this._dgv.Rows.Count == 1 ? 0 : (this._dgv.Rows.Count == num ? this._dgv.Rows.Count - 1 : num);
        if (this._dgv.Rows.Count > 0 && this._dgv.Rows.Count > index2)
          this._dgv.Rows[index2].Selected = true;
        this._lv.SelectedItems.Clear();
        this._lv.Items[this._lv.Items.Count - 1].Selected = true;
        break;
      default:
        foreach (DataGridViewBand row in (IEnumerable) this._dgv.Rows)
          this.AddListViewRow(row.Tag as NodeColumn);
        this._dgv.Rows.Clear();
        this._lv.SelectedItems.Clear();
        this._lv.Items[this._lv.Items.Count - 1].Selected = true;
        break;
    }
    this.CheckEnableButtons();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnUpDounBtn_Click(object sender, EventArgs e)
  {
    int int32 = Convert.ToInt32((sender as ButtonItem).Tag);
    DataGridViewRow selectedRow = this._dgv.SelectedRows[0];
    int index1 = selectedRow.Index;
    this._dgv.Rows.Remove(selectedRow);
    int index2;
    switch (int32)
    {
      case 0:
        this._dgv.Rows.Insert(0, selectedRow);
        index2 = 0;
        break;
      case 1:
        this._dgv.Rows.Insert(index2 = index1 - 1, selectedRow);
        break;
      case 2:
        this._dgv.Rows.Insert(index2 = index1 + 1, selectedRow);
        break;
      default:
        this._dgv.Rows.Add(selectedRow);
        index2 = this._dgv.Rows.Count - 1;
        break;
    }
    this._dgv.ClearSelection();
    this._dgv.Rows[index2].Selected = true;
    this.CheckEnableButtons();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnLoad(EventArgs e)
  {
    base.OnLoad(e);
    FormStorage.LoadLayout((Control) this);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnClosing(CancelEventArgs e)
  {
    base.OnClosing(e);
    FormStorage.SaveLayout((Control) this);
  }

  /// <summary>
  /// 
  /// </summary>
  private void LoadData(NodeColumnCollection visibleColumns, NodeColumnCollection sortColumn)
  {
    if (visibleColumns == null)
      return;
    DataTable dataTable = new DataTable();
    dataTable.Columns.AddRange(new DataColumn[2]
    {
      new DataColumn("Value", typeof (int)),
      new DataColumn("Caption", typeof (string))
    });
    dataTable.Rows.Add((object) 1, (object) LocalizationHolder.rm.GetString("FormDesigner_Sort_ASC"));
    dataTable.Rows.Add((object) 2, (object) LocalizationHolder.rm.GetString("FormDesigner_Sort_DESC"));
    this._colSortOrder.DataSource = (object) dataTable;
    this._colSortOrder.ValueMember = "Value";
    this._colSortOrder.DisplayMember = "Caption";
    if (sortColumn != null && sortColumn.Count > 0)
    {
      List<int> sortingID = sortColumn.Select<NodeColumn, int>((System.Func<NodeColumn, int>) (x => x.Attribute.AttributeID)).ToList<int>();
      List<NodeColumn> list = visibleColumns.Where<NodeColumn>((System.Func<NodeColumn, bool>) (x => !sortingID.Contains(x.Attribute.AttributeID))).ToList<NodeColumn>();
      if (list.Count > 0)
        list.ForEach((Action<NodeColumn>) (x => this.AddListViewRow(x)));
      sortColumn.SortByIndex();
      sortColumn.ForEach((Action<NodeColumn>) (x => this.AddDataGridViewRow(x)));
      this._dgv.Rows[0].Selected = true;
    }
    else
      visibleColumns.ForEach((Action<NodeColumn>) (x => this.AddListViewRow(x)));
    this._lv.Items[0].Selected = true;
  }

  /// <summary>Добавление колонки в список видимых колонок.</summary>
  /// <param name="column"></param>
  private void AddListViewRow(NodeColumn column)
  {
    int imageIndex = Statics.IconSrv.IndexOf(3, -1, (object) column.Attribute.FieldType);
    this._lv.Items.Add(new ListViewItem(column.Caption, imageIndex)
    {
      Tag = (object) column
    });
  }

  /// <summary>Добавление колонки в список колонок для сортировки.</summary>
  /// <param name="column">Колонка</param>
  private void AddDataGridViewRow(NodeColumn column)
  {
    this._dgv.Rows[this._dgv.Rows.Add((object) this._lv.SmallImageList.Images[Statics.IconSrv.IndexOf(3, -1, (object) column.Attribute.FieldType)], (object) column.Caption, (object) (column.SortOrder == NodeColumnSortOrder.None ? 1 : (int) column.SortOrder))].Tag = (object) column;
  }

  /// <summary>Проверка доступности кнопок перемещения колонок.</summary>
  private void CheckEnableButtons()
  {
    this._btnAdd.Enabled = this._lv.SelectedItems.Count > 0;
    this._btnAddAll.Enabled = this._lv.Items.Count > 0;
    this._btnDeleteAll.Enabled = this._dgv.Rows.Count > 0;
    if (this._dgv.SelectedRows.Count > 0)
    {
      this._btnDelete.Enabled = true;
      if (this._dgv.SelectedRows.Count == 1)
      {
        if (this._dgv.SelectedRows[0].Index == 0)
        {
          this._btnTop.Enabled = this._btnUp.Enabled = false;
          this._btnDown.Enabled = this._btnBottom.Enabled = true;
        }
        else if (this._dgv.SelectedRows[0].Index == this._dgv.Rows.Count - 1)
        {
          this._btnTop.Enabled = this._btnUp.Enabled = true;
          this._btnDown.Enabled = this._btnBottom.Enabled = false;
        }
        else
          this._btnTop.Enabled = this._btnUp.Enabled = this._btnDown.Enabled = this._btnBottom.Enabled = true;
      }
      else
        this._btnTop.Enabled = this._btnUp.Enabled = this._btnDown.Enabled = this._btnBottom.Enabled = false;
    }
    else
    {
      this._btnDelete.Enabled = false;
      this._btnTop.Enabled = this._btnUp.Enabled = this._btnDown.Enabled = this._btnBottom.Enabled = false;
    }
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ColumnsSortingEditorForm));
    DataGridViewCellStyle gridViewCellStyle = new DataGridViewCellStyle();
    this._splitContainer = new SplitContainer();
    this._lv = new ListView();
    this._lvColCaption = new ColumnHeader();
    this.toolBarLeft = new Intermech.Bars.ToolBar();
    this._img = new ImageList(this.components);
    this._btnAdd = new ButtonItem();
    this._btnAddAll = new ButtonItem();
    this._btnDelete = new ButtonItem();
    this._btnDeleteAll = new ButtonItem();
    this._dgv = new DataGridView();
    this._colImg = new DataGridViewImageColumn();
    this._colCaption = new DataGridViewTextBoxColumn();
    this._colSortOrder = new DataGridViewComboBoxColumn();
    this.toolBarRight = new Intermech.Bars.ToolBar();
    this._btnTop = new ButtonItem();
    this._btnUp = new ButtonItem();
    this._btnDown = new ButtonItem();
    this._btnBottom = new ButtonItem();
    this._btnCancel = new Button();
    this._btnApply = new Button();
    this.pnlBottom = new Panel();
    this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
    this._splitContainer.BeginInit();
    this._splitContainer.Panel1.SuspendLayout();
    this._splitContainer.Panel2.SuspendLayout();
    this._splitContainer.SuspendLayout();
    ((ISupportInitialize) this._dgv).BeginInit();
    this.pnlBottom.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._splitContainer, "_splitContainer");
    this._splitContainer.Name = "_splitContainer";
    this._splitContainer.Panel1.Controls.Add((Control) this._lv);
    this._splitContainer.Panel1.Controls.Add((Control) this.toolBarLeft);
    componentResourceManager.ApplyResources((object) this._splitContainer.Panel1, "_splitContainer.Panel1");
    this._splitContainer.Panel2.Controls.Add((Control) this._dgv);
    this._splitContainer.Panel2.Controls.Add((Control) this.toolBarRight);
    this._lv.Columns.AddRange(new ColumnHeader[1]
    {
      this._lvColCaption
    });
    componentResourceManager.ApplyResources((object) this._lv, "_lv");
    this._lv.FullRowSelect = true;
    this._lv.HeaderStyle = ColumnHeaderStyle.Nonclickable;
    this._lv.HideSelection = false;
    this._lv.Name = "_lv";
    this._lv.UseCompatibleStateImageBehavior = false;
    this._lv.View = View.Details;
    this._lv.SelectedIndexChanged += new EventHandler(this.On_lst_SelectedIndexChanged);
    this._lv.DoubleClick += new EventHandler(this.On_lst_DoubleClick);
    componentResourceManager.ApplyResources((object) this._lvColCaption, "_lvColCaption");
    this.toolBarLeft.AddRemoveButtonsVisible = false;
    this.toolBarLeft.AllowHorizontalDock = false;
    componentResourceManager.ApplyResources((object) this.toolBarLeft, "toolBarLeft");
    this.toolBarLeft.DockLine = 3;
    this.toolBarLeft.DrawActionsButton = false;
    this.toolBarLeft.Flow = ToolBarLayout.Vertical;
    this.toolBarLeft.FullMenus = true;
    this.toolBarLeft.Guid = new Guid("ba855ba6-35ae-4775-b979-b76ac70a54e0");
    this.toolBarLeft.Hidden = false;
    this.toolBarLeft.ImageList = this._img;
    this.toolBarLeft.Items.AddRange(new ToolbarItemBase[4]
    {
      (ToolbarItemBase) this._btnAdd,
      (ToolbarItemBase) this._btnAddAll,
      (ToolbarItemBase) this._btnDelete,
      (ToolbarItemBase) this._btnDeleteAll
    });
    this.toolBarLeft.MinimumFloatingSize = new Size(250, 30);
    this.toolBarLeft.Name = "toolBarLeft";
    this.toolBarLeft.Overflow = ToolBarOverflow.Wrap;
    this.toolBarLeft.Stretch = true;
    this.toolBarLeft.Tearable = false;
    this._img.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("_img.ImageStream");
    this._img.TransparentColor = Color.Transparent;
    this._img.Images.SetKeyName(0, "arrow_right_blue.ico");
    this._img.Images.SetKeyName(1, "arrow_left_blue.ico");
    this._img.Images.SetKeyName(2, "arrow_all_right_blue.ico");
    this._img.Images.SetKeyName(3, "arrow_all_left_blue.ico");
    this._img.Images.SetKeyName(4, "arrow_up_blue.ico");
    this._img.Images.SetKeyName(5, "arrow_down_blue.ico");
    this._img.Images.SetKeyName(6, "");
    this._img.Images.SetKeyName(7, "");
    componentResourceManager.ApplyResources((object) this._btnAdd, "_btnAdd");
    this._btnAdd.ImageIndex = 0;
    this._btnAdd.Tag = (object) "0";
    this._btnAdd.Click += new EventHandler(this.OnLeftRightBtn_Click);
    componentResourceManager.ApplyResources((object) this._btnAddAll, "_btnAddAll");
    this._btnAddAll.ImageIndex = 2;
    this._btnAddAll.Tag = (object) "1";
    this._btnAddAll.Click += new EventHandler(this.OnLeftRightBtn_Click);
    this._btnDelete.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this._btnDelete, "_btnDelete");
    this._btnDelete.ImageIndex = 1;
    this._btnDelete.Tag = (object) "2";
    this._btnDelete.Click += new EventHandler(this.OnLeftRightBtn_Click);
    componentResourceManager.ApplyResources((object) this._btnDeleteAll, "_btnDeleteAll");
    this._btnDeleteAll.ImageIndex = 3;
    this._btnDeleteAll.Tag = (object) "3";
    this._btnDeleteAll.Click += new EventHandler(this.OnLeftRightBtn_Click);
    this._dgv.AllowUserToAddRows = false;
    this._dgv.AllowUserToDeleteRows = false;
    this._dgv.AllowUserToResizeColumns = false;
    this._dgv.AllowUserToResizeRows = false;
    this._dgv.BackgroundColor = SystemColors.Window;
    this._dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    this._dgv.Columns.AddRange((DataGridViewColumn) this._colImg, (DataGridViewColumn) this._colCaption, (DataGridViewColumn) this._colSortOrder);
    componentResourceManager.ApplyResources((object) this._dgv, "_dgv");
    this._dgv.EditMode = DataGridViewEditMode.EditOnEnter;
    this._dgv.Name = "_dgv";
    this._dgv.RowHeadersVisible = false;
    this._dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    this._dgv.SelectionChanged += new EventHandler(this.On_dgv_SelectionChanged);
    this._dgv.DoubleClick += new EventHandler(this.On_dgv_DoubleClick);
    gridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
    gridViewCellStyle.NullValue = componentResourceManager.GetObject("dataGridViewCellStyle1.NullValue");
    gridViewCellStyle.Padding = new Padding(5, 0, 0, 0);
    this._colImg.DefaultCellStyle = gridViewCellStyle;
    componentResourceManager.ApplyResources((object) this._colImg, "_colImg");
    this._colImg.Name = "_colImg";
    this._colImg.ReadOnly = true;
    this._colImg.Resizable = DataGridViewTriState.False;
    this._colCaption.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    componentResourceManager.ApplyResources((object) this._colCaption, "_colCaption");
    this._colCaption.Name = "_colCaption";
    this._colCaption.ReadOnly = true;
    this._colCaption.Resizable = DataGridViewTriState.False;
    this._colCaption.SortMode = DataGridViewColumnSortMode.NotSortable;
    componentResourceManager.ApplyResources((object) this._colSortOrder, "_colSortOrder");
    this._colSortOrder.MaxDropDownItems = 2;
    this._colSortOrder.Name = "_colSortOrder";
    this._colSortOrder.Resizable = DataGridViewTriState.False;
    this.toolBarRight.AddRemoveButtonsVisible = false;
    this.toolBarRight.AllowHorizontalDock = false;
    componentResourceManager.ApplyResources((object) this.toolBarRight, "toolBarRight");
    this.toolBarRight.DockLine = 3;
    this.toolBarRight.DrawActionsButton = false;
    this.toolBarRight.Flow = ToolBarLayout.Vertical;
    this.toolBarRight.FullMenus = true;
    this.toolBarRight.Guid = new Guid("ba855ba6-35ae-4775-b979-b76ac70a54e0");
    this.toolBarRight.Hidden = false;
    this.toolBarRight.ImageList = this._img;
    this.toolBarRight.Items.AddRange(new ToolbarItemBase[4]
    {
      (ToolbarItemBase) this._btnTop,
      (ToolbarItemBase) this._btnUp,
      (ToolbarItemBase) this._btnDown,
      (ToolbarItemBase) this._btnBottom
    });
    this.toolBarRight.MinimumFloatingSize = new Size(250, 30);
    this.toolBarRight.Name = "toolBarRight";
    this.toolBarRight.Overflow = ToolBarOverflow.Wrap;
    this.toolBarRight.Stretch = true;
    this.toolBarRight.Tearable = false;
    componentResourceManager.ApplyResources((object) this._btnTop, "_btnTop");
    this._btnTop.Image = (Image) componentResourceManager.GetObject("_btnTop.Image");
    this._btnTop.Tag = (object) "0";
    this._btnTop.Click += new EventHandler(this.OnUpDounBtn_Click);
    componentResourceManager.ApplyResources((object) this._btnUp, "_btnUp");
    this._btnUp.ImageIndex = 4;
    this._btnUp.Tag = (object) "1";
    this._btnUp.Click += new EventHandler(this.OnUpDounBtn_Click);
    this._btnDown.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this._btnDown, "_btnDown");
    this._btnDown.ImageIndex = 5;
    this._btnDown.Tag = (object) "2";
    this._btnDown.Click += new EventHandler(this.OnUpDounBtn_Click);
    componentResourceManager.ApplyResources((object) this._btnBottom, "_btnBottom");
    this._btnBottom.Image = (Image) componentResourceManager.GetObject("_btnBottom.Image");
    this._btnBottom.Tag = (object) "3";
    this._btnBottom.Click += new EventHandler(this.OnUpDounBtn_Click);
    componentResourceManager.ApplyResources((object) this._btnCancel, "_btnCancel");
    this._btnCancel.DialogResult = DialogResult.Cancel;
    this._btnCancel.Name = "_btnCancel";
    this._btnCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._btnApply, "_btnApply");
    this._btnApply.DialogResult = DialogResult.OK;
    this._btnApply.Name = "_btnApply";
    this._btnApply.UseVisualStyleBackColor = true;
    this.pnlBottom.Controls.Add((Control) this._btnCancel);
    this.pnlBottom.Controls.Add((Control) this._btnApply);
    componentResourceManager.ApplyResources((object) this.pnlBottom, "pnlBottom");
    this.pnlBottom.Name = "pnlBottom";
    this.dataGridViewTextBoxColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    componentResourceManager.ApplyResources((object) this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
    this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
    this.dataGridViewTextBoxColumn1.ReadOnly = true;
    this.dataGridViewTextBoxColumn1.Resizable = DataGridViewTriState.False;
    this.dataGridViewTextBoxColumn1.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.AcceptButton = (IButtonControl) this._btnApply;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this._btnCancel;
    this.Controls.Add((Control) this._splitContainer);
    this.Controls.Add((Control) this.pnlBottom);
    this.DoubleBuffered = true;
    this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
    this.Name = nameof (ColumnsSortingEditorForm);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this._splitContainer.Panel1.ResumeLayout(false);
    this._splitContainer.Panel2.ResumeLayout(false);
    this._splitContainer.EndInit();
    this._splitContainer.ResumeLayout(false);
    ((ISupportInitialize) this._dgv).EndInit();
    this.pnlBottom.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
