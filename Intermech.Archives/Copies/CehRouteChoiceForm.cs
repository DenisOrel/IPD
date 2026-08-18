// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.Copies.CehRouteChoiceForm
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Archives.Common;
using Intermech.DataFormats;
using Intermech.Expert;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;

#nullable disable
namespace Intermech.Archives.Copies;

/// <summary>Форма выбора расцеховочного маршрута</summary>
public class CehRouteChoiceForm : Form
{
  /// <summary>
  /// Выделенное в гриде изделие, маршруты обработки которого нас интересуют
  /// </summary>
  private long _currentObjectID;
  /// <summary>Выделенный в гриде расцеховочный маршрут</summary>
  private long _chosedCehRouteID;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panelButtons;
  private Button btnCancel;
  private Button btnOk;
  private SplitContainer splitContainer1;
  private Label label1;
  private ObjectsViewBase ovbItems;
  private ObjectsViewBase ovbCehRoutes;
  private Label label2;

  /// <summary>ИД выбранного расцеховочного маршрута</summary>
  /// <value>The ceh route ID.</value>
  public long CehRouteID => this._chosedCehRouteID;

  /// <summary>Конструктор</summary>
  /// <param name="typedIDs">Типированные ИД версий объектов</param>
  public CehRouteChoiceForm(Dictionary<int, List<long>> typedIDs)
  {
    this.InitializeComponent();
    this.ovbItems.Initialize((IDescriptor) new DictDescriptor(Intermech.Navigator.Consts.CategoryAllObjectTypes, 0, string.Empty, typedIDs), (System.IServiceProvider) this.ovbItems.Services);
    this.ovbItems.DisableIMContextMenu = true;
    this.ovbItems.DisableContextSearch = true;
    this.ovbItems.DisableDoubleClicks = true;
    this.ovbItems.DisableGroupBox = true;
    this.ovbItems.AutoScroll = true;
    this.ovbItems.Grid.SelectionMode = iGSelectionMode.One;
    this.ovbItems.Activate((IView) null);
    this.ovbCehRoutes.DisableIMContextMenu = true;
    this.ovbCehRoutes.DisableContextSearch = true;
    this.ovbCehRoutes.DisableDoubleClicks = true;
    this.ovbCehRoutes.DisableGroupBox = true;
    this.ovbCehRoutes.AutoScroll = true;
    this.ovbCehRoutes.Grid.SelectionMode = iGSelectionMode.One;
    this._chosedCehRouteID = 0L;
  }

  /// <summary>
  /// Загрузить расцеховочные маршруты для выделенного изделия в грид
  /// </summary>
  private void LoadCehRoutes()
  {
    Dictionary<int, List<long>> cehRoutesTypedIds = this.GetCehRoutesTypedIDs();
    DictDescriptor rootDescriptor = new DictDescriptor(Intermech.Navigator.Consts.CategoryAllObjectTypes, 0, string.Empty, cehRoutesTypedIds);
    this.ovbCehRoutes.Deactivate((IView) null);
    this.ovbCehRoutes.Initialize((IDescriptor) rootDescriptor, (System.IServiceProvider) this.ovbItems.Services);
    this.ovbCehRoutes.Activate((IView) null);
  }

  /// <summary>Устанавливает значение поля _currentObjectID</summary>
  private void SetCurrentObjectID()
  {
    if (this.ovbItems.SelectedItems.Count == 0)
      this._currentObjectID = 0L;
    else
      this._currentObjectID = this.ovbItems.SelectedItems.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData ? itemData.ObjectID : 0L;
  }

  /// <summary>Установить ИД выбранного маршрута обработки</summary>
  private void SetChosedRouteID()
  {
    if (this.ovbCehRoutes.SelectedItems.Count == 0)
      this._chosedCehRouteID = 0L;
    else
      this._chosedCehRouteID = this.ovbItems.SelectedItems.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData ? itemData.ObjectID : 0L;
  }

  /// <summary>
  /// Получает типированный словарь с расцеховочными маршрутами выделенного в гриде объекта
  /// </summary>
  /// <returns>Типированный словарь с расцеховочными маршрутами выделенного в гриде объекта</returns>
  private Dictionary<int, List<long>> GetCehRoutesTypedIDs()
  {
    Dictionary<int, List<long>> cehRoutesTypedIds = new Dictionary<int, List<long>>();
    if (this._currentObjectID == 0L)
      return cehRoutesTypedIds;
    DataTable cehRoutesTable = this.GetCehRoutesTable();
    if (cehRoutesTable == null || cehRoutesTable.Rows.Count == 0)
      return cehRoutesTypedIds;
    List<long> longList = new List<long>();
    foreach (DataRow row in (InternalDataCollectionBase) cehRoutesTable.Rows)
    {
      long int64 = Convert.ToInt64(row[-2.ToString()]);
      longList.Add(int64);
    }
    cehRoutesTypedIds.Add(ConstsHolder.CehRouteID, longList);
    return cehRoutesTypedIds;
  }

  /// <summary>
  /// Получает таблицу с информацией об ИД расцеховочных маршрутов изделия
  /// </summary>
  /// <returns>Таблица с информацией об ИД расцеховочных маршрутов изделия</returns>
  private DataTable GetCehRoutesTable()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DBRecordSetParams dbRsp = new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[1]
      {
        new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0)
      });
      return DataHelper.GetChildSostavData((IEnumerable<ObjInfoItem>) new List<ObjInfoItem>()
      {
        new ObjInfoItem(this._currentObjectID)
      }, sessionKeeper.Session, (IEnumerable<int>) new int[1]
      {
        MetaDataHelper.GetRelationTypeID("cad0019f-306c-11d8-b4e9-00304f19f545")
      }, 2, dbRsp, (VersionsRule) null, DataHelper.Consts.cnt_def_filtrationRule, (Dictionary<long, HybridDictionary>) null, (IEnumerable<int>) new List<int>()
      {
        ConstsHolder.CehRouteID
      }, (IEnumerable<int>) new List<int>()
      {
        MetaDataHelper.GetObjectTypeID("cad0016f-306c-11d8-b4e9-00304f19f545")
      });
    }
  }

  /// <summary>Изменилось выделенное изделие</summary>
  /// <param name="sender">The source of the event.</param>
  /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
  private void ovbItems_SelectedItemsChanged(object sender, EventArgs e)
  {
    this.SetCurrentObjectID();
    this.LoadCehRoutes();
  }

  /// <summary>Изменился выделенный расцеховочный маршрут.</summary>
  /// <param name="sender">The source of the event.</param>
  /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
  private void ovbCehRoutes_SelectedItemsChanged(object sender, EventArgs e)
  {
    if (this.ovbCehRoutes.SelectedItems.Count == 0)
      this._chosedCehRouteID = 0L;
    else
      this._chosedCehRouteID = this.ovbCehRoutes.SelectedItems.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData ? itemData.ObjectID : 0L;
  }

  /// <summary>Кнопка Отмена</summary>
  /// <param name="sender">The source of the event.</param>
  /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
  private void btnCancel_Click(object sender, EventArgs e) => this._chosedCehRouteID = 0L;

  /// <summary>Кнопка ОК.</summary>
  /// <param name="sender">The source of the event.</param>
  /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
  private void btnOk_Click(object sender, EventArgs e)
  {
    this.SetChosedRouteID();
    if (this._chosedCehRouteID == 0L)
    {
      int num = (int) MessageBox.Show(ServiceHolder.rm.GetString("Archives_174"), ServiceHolder.rm.GetString("Archives_111"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }
    else
      this.Close();
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
    this.panelButtons = new Panel();
    this.btnCancel = new Button();
    this.btnOk = new Button();
    this.splitContainer1 = new SplitContainer();
    this.label1 = new Label();
    this.ovbItems = new ObjectsViewBase();
    this.ovbCehRoutes = new ObjectsViewBase();
    this.label2 = new Label();
    this.panelButtons.SuspendLayout();
    this.splitContainer1.BeginInit();
    this.splitContainer1.Panel1.SuspendLayout();
    this.splitContainer1.Panel2.SuspendLayout();
    this.splitContainer1.SuspendLayout();
    this.SuspendLayout();
    this.panelButtons.Controls.Add((Control) this.btnCancel);
    this.panelButtons.Controls.Add((Control) this.btnOk);
    this.panelButtons.Dock = DockStyle.Bottom;
    this.panelButtons.Location = new Point(0, 492);
    this.panelButtons.Name = "panelButtons";
    this.panelButtons.Size = new Size(1185, 54);
    this.panelButtons.TabIndex = 5;
    this.btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Location = new Point(1063, 15);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(110, 27);
    this.btnCancel.TabIndex = 1;
    this.btnCancel.Text = "Отмена";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
    this.btnOk.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnOk.Location = new Point(947, 15);
    this.btnOk.Name = "btnOk";
    this.btnOk.Size = new Size(110, 27);
    this.btnOk.TabIndex = 0;
    this.btnOk.Text = "OK";
    this.btnOk.UseVisualStyleBackColor = true;
    this.btnOk.Click += new EventHandler(this.btnOk_Click);
    this.splitContainer1.Dock = DockStyle.Fill;
    this.splitContainer1.Location = new Point(0, 0);
    this.splitContainer1.Name = "splitContainer1";
    this.splitContainer1.Orientation = Orientation.Horizontal;
    this.splitContainer1.Panel1.Controls.Add((Control) this.label1);
    this.splitContainer1.Panel1.Controls.Add((Control) this.ovbItems);
    this.splitContainer1.Panel2.Controls.Add((Control) this.ovbCehRoutes);
    this.splitContainer1.Panel2.Controls.Add((Control) this.label2);
    this.splitContainer1.Size = new Size(1185, 492);
    this.splitContainer1.SplitterDistance = 235;
    this.splitContainer1.SplitterWidth = 6;
    this.splitContainer1.TabIndex = 6;
    this.label1.AutoSize = true;
    this.label1.Location = new Point(12, 9);
    this.label1.Name = "label1";
    this.label1.Size = new Size(96 /*0x60*/, 13);
    this.label1.TabIndex = 9;
    this.label1.Text = "Выберите объект";
    this.ovbItems.AllowCustomGroupValues = true;
    this.ovbItems.AllowEditing = true;
    this.ovbItems.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.ovbItems.AutoScroll = true;
    this.ovbItems.Control = (object) this.ovbItems;
    this.ovbItems.DisableDoubleClicks = true;
    this.ovbItems.DisableFiltration = true;
    this.ovbItems.DisableGroupBox = true;
    this.ovbItems.DisableKeyDownEvents = false;
    this.ovbItems.EditingMode = false;
    this.ovbItems.EmbeddedFocusAndSelection = (iFocusAndSelection) null;
    this.ovbItems.Font = new Font("Tahoma", 8.25f);
    this.ovbItems.Location = new Point(12, 25);
    this.ovbItems.Name = "ovbItems";
    this.ovbItems.Size = new Size(1161, 207);
    this.ovbItems.TabIndex = 8;
    this.ovbItems.ViewContentType = ContentType.NonFolders;
    this.ovbItems.SelectedItemsChanged += new EventHandler(this.ovbItems_SelectedItemsChanged);
    this.ovbCehRoutes.AllowCustomGroupValues = true;
    this.ovbCehRoutes.AllowEditing = true;
    this.ovbCehRoutes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.ovbCehRoutes.AutoScroll = true;
    this.ovbCehRoutes.Control = (object) this.ovbCehRoutes;
    this.ovbCehRoutes.DisableKeyDownEvents = false;
    this.ovbCehRoutes.EditingMode = false;
    this.ovbCehRoutes.EmbeddedFocusAndSelection = (iFocusAndSelection) null;
    this.ovbCehRoutes.Font = new Font("Tahoma", 8.25f);
    this.ovbCehRoutes.Location = new Point(12, 22);
    this.ovbCehRoutes.Name = "ovbCehRoutes";
    this.ovbCehRoutes.Size = new Size(1161, 213);
    this.ovbCehRoutes.TabIndex = 10;
    this.ovbCehRoutes.ViewContentType = ContentType.NonFolders;
    this.ovbCehRoutes.SelectedItemsChanged += new EventHandler(this.ovbCehRoutes_SelectedItemsChanged);
    this.label2.AutoSize = true;
    this.label2.Location = new Point(12, 6);
    this.label2.Name = "label2";
    this.label2.Size = new Size(185, 13);
    this.label2.TabIndex = 9;
    this.label2.Text = "Выберите расцеховочный маршрут";
    this.AcceptButton = (IButtonControl) this.btnOk;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.ClientSize = new Size(1185, 546);
    this.Controls.Add((Control) this.splitContainer1);
    this.Controls.Add((Control) this.panelButtons);
    this.Name = nameof (CehRouteChoiceForm);
    this.Text = "Выбор  расцеховочного маршрута";
    this.panelButtons.ResumeLayout(false);
    this.splitContainer1.Panel1.ResumeLayout(false);
    this.splitContainer1.Panel1.PerformLayout();
    this.splitContainer1.Panel2.ResumeLayout(false);
    this.splitContainer1.Panel2.PerformLayout();
    this.splitContainer1.EndInit();
    this.splitContainer1.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
