// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Views.ImbaseTableMixView
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Client.Core.Navigator.Classes.ObjectNode;
using Intermech.DataFormats;
using Intermech.Imbase.Editors;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Views;

[ViewDescriptionProvider(typeof (ImbaseTableMixView.ImbaseTableMixViewDescriptionProvider))]
public class ImbaseTableMixView : UserControl, IView
{
  private bool _loaded;
  private QuickObjectInfo _tableInfo;
  private long _objectId;
  private DataSet _dataSet;
  private DataTable _dtData;
  private DataTable _dtReceptureNames;
  private DataTable _dtReceptureComposition;
  private string _captionColName = "CAPTION";
  private string _childComponentColName = Intermech.Imbase.Consts.LinkToComponentOfCompositeObjectAttGuid.ToString();
  private string _componentColName = Intermech.Imbase.Consts.LinkToCompoundObjectAttGUID.ToString();
  private IContainer components;
  private TextWithButtonColumn textWithButtonColumn2;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
  private TextWithButtonColumn textWithButtonColumn1;
  private SplitContainer splitContainer1;
  private DataGridView recepturesGrid;
  private DataGridViewTextWithButtonColumn colCaption;
  private Label lbReceptures;
  private DataGridView compositionGrid;
  private Label lbComposition;
  private DataGridViewTextWithButtonColumn colComponent;
  private DataGridViewTextWithButtonColumn colCount;
  private ContextMenuStrip cmRecepturesName;
  private ToolStripMenuItem перейтиКОбъектуImbaseToolStripMenuItem;
  private ContextMenuStrip cmRecepturesComposition;
  private ToolStripMenuItem перейтиКОбъектуImbaseToolStripMenuItem1;

  public ImbaseTableMixView()
  {
    this.InitializeComponent();
    this.Subscribe();
  }

  public void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    this._loaded = false;
    this._objectId = items.GetItemData(0, typeof (IDBObjectID)) is IDBObjectID itemData ? itemData.Value : 0L;
  }

  public void Activate(IView previousView)
  {
    if (this._loaded)
      return;
    this._loaded = true;
    this.InitGridColumns();
    this.LoadData();
    this.recepturesGrid_RowEnter((object) this, new DataGridViewCellEventArgs(0, 0));
  }

  public void Deactivate(IView nextView)
  {
  }

  public string Caption { get; } = LocalizationHolder.rm.GetString("Imbase.Receptures.ViewName");

  public int ImageIndex { get; } = -1;

  public int OrderID { get; } = int.MinValue;

  private void InitGridColumns()
  {
    this.recepturesGrid.AutoGenerateColumns = this.compositionGrid.AutoGenerateColumns = false;
    this.colCaption.TextReadOnly = this.colComponent.TextReadOnly = this.colCount.TextReadOnly = true;
  }

  private void LoadData()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      this._tableInfo = session.GetObjectInfo(this._objectId);
      this.Text = this._tableInfo.Caption;
      this.LoadTableDataSet(session);
    }
  }

  private void LoadTableDataSet(IUserSession session)
  {
    this._dataSet = TableLoadHelper.GetTables(session, this._objectId, true);
    if (this._dataSet == null || !this._dataSet.Tables.Contains("IMS_ATTR_TYPES") || !this._dataSet.Tables.Contains("IMS_DATA"))
      return;
    this._dtData = this._dataSet.Tables["IMS_DATA"];
    if (this._dtData == null)
      return;
    this.PrepareComponentsData(session);
  }

  private void PrepareComponentsData(IUserSession session)
  {
    this._dtReceptureNames = new DataView(this._dtData).ToTable(true, this._componentColName);
    List<string> list = this._dtReceptureNames.AsEnumerable().Select<DataRow, string>((System.Func<DataRow, string>) (x => Convert.ToString(x[this._componentColName]))).ToList<string>();
    Dictionary<string, string> dictionary = ServiceUtils.GetService<IImbaseServer>((object) session, true).NameRecordReferences(session.SessionGUID, list);
    this._dtReceptureNames.BeginLoadData();
    this._dtReceptureNames.Columns.Add(this._captionColName, typeof (string));
    foreach (DataRow row in (InternalDataCollectionBase) this._dtReceptureNames.Rows)
    {
      string str;
      if (dictionary.TryGetValue(Convert.ToString(row[this._componentColName]), out str))
        row[this._captionColName] = (object) str;
    }
    this._dtReceptureNames.AcceptChanges();
    this._dtReceptureNames.EndLoadData();
    this.recepturesGrid.DataSource = (object) this._dtReceptureNames;
  }

  private void Subscribe()
  {
    if (!(ServicesManager.GetService(typeof (INotificationService)) is INotificationService service))
      return;
    service.Subscribe("ObjectsChanged", new NotificationEventHandler(this.OnObjectChanged));
  }

  private void Unsubscribe()
  {
    if (!(ServicesManager.GetService(typeof (INotificationService)) is INotificationService service))
      return;
    service.Unsubscribe("ObjectsChanged", new NotificationEventHandler(this.OnObjectChanged));
  }

  private void GoToImbaseRecord(IUserSession session, string imbaseKey)
  {
    long linkId;
    long recordId;
    if (!ImbaseHelper.TryParseRecordReference(session, imbaseKey, out linkId, out recordId))
      return;
    NodeIDPath pathToImbaseObject = ImbaseClientHelper.CreatePathToImbaseObject(session, linkId);
    SelectedRecords.Add(linkId, new long[1]{ recordId });
    SelectedRecords.Add(-linkId, new long[1]{ recordId });
    Utils.OpenNewWindow(pathToImbaseObject.RootDescriptor, (System.IServiceProvider) null, new GetSupportedColumnsEventHandler(Utils.DefaultSupportedColumnsObjects), pathToImbaseObject);
  }

  private void recepturesGrid_RowEnter(object sender, DataGridViewCellEventArgs e)
  {
    if (this.recepturesGrid.SelectedRows.Count <= 0 || !(this.recepturesGrid.SelectedRows[0].DataBoundItem is DataRowView dataBoundItem))
      return;
    this._dtReceptureComposition = ((IEnumerable<DataRow>) this._dtData.Select($"[{this._componentColName}] = '{dataBoundItem[this._componentColName]}'")).CopyToDataTable<DataRow>();
    this._dtReceptureComposition.BeginLoadData();
    this._dtReceptureComposition.Columns.Add(this._captionColName, typeof (string));
    List<string> list = this._dtReceptureComposition.AsEnumerable().Select<DataRow, string>((System.Func<DataRow, string>) (x => Convert.ToString(x[this._childComponentColName]))).ToList<string>();
    Dictionary<string, string> dictionary;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      dictionary = ServiceUtils.GetService<IImbaseServer>((object) sessionKeeper.Session, true).NameRecordReferences(sessionKeeper.Session.SessionGUID, list);
    foreach (DataRow row in (InternalDataCollectionBase) this._dtReceptureComposition.Rows)
    {
      string str;
      if (dictionary.TryGetValue(Convert.ToString(row[this._childComponentColName]), out str))
        row[this._captionColName] = (object) str;
    }
    this._dtReceptureComposition.EndLoadData();
    this._dtReceptureComposition.AcceptChanges();
    this.compositionGrid.DataSource = (object) this._dtReceptureComposition;
  }

  private void OnObjectChanged(object sender, NotificationEventArgs ne)
  {
    if (ne is DBObjectsEventArgs objectsEventArgs && !objectsEventArgs.ObjectIDs.Contains(this._objectId))
      return;
    this.LoadData();
    this.recepturesGrid_RowEnter((object) this, new DataGridViewCellEventArgs(0, 0));
  }

  private void GoToImbaseObjectToolStripMenuItem_Click(object sender, EventArgs e)
  {
    if (this.recepturesGrid.SelectedCells.Count <= 0 || !(this.recepturesGrid.Rows[this.recepturesGrid.SelectedCells[0].RowIndex].DataBoundItem is DataRowView dataBoundItem))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      string imbaseKey = Convert.ToString(dataBoundItem[this._componentColName]);
      this.GoToImbaseRecord(sessionKeeper.Session, imbaseKey);
    }
  }

  private void GoToImbaseObjectToolStripMenuItem1_Click(object sender, EventArgs e)
  {
    if (this.compositionGrid.SelectedCells.Count <= 0 || !(this.compositionGrid.Rows[this.compositionGrid.SelectedCells[0].RowIndex].DataBoundItem is DataRowView dataBoundItem))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      string imbaseKey = Convert.ToString(dataBoundItem[this._childComponentColName]);
      this.GoToImbaseRecord(sessionKeeper.Session, imbaseKey);
    }
  }

  private void compositionGrid_MouseClick(object sender, MouseEventArgs e)
  {
    DataGridView.HitTestInfo hitTestInfo = this.compositionGrid.HitTest(e.X, e.Y);
    if (hitTestInfo.ColumnIndex != 0 || hitTestInfo.RowIndex == -1 || e.Button != MouseButtons.Right)
      return;
    DataGridViewCell dataGridViewCell = ((DataGridView) sender)[hitTestInfo.ColumnIndex, hitTestInfo.RowIndex];
    if (!dataGridViewCell.Selected)
    {
      dataGridViewCell.DataGridView.ClearSelection();
      dataGridViewCell.DataGridView.CurrentCell = dataGridViewCell;
      dataGridViewCell.Selected = true;
    }
    this.cmRecepturesComposition.Show((Control) this.compositionGrid, new Point(e.X, e.Y));
  }

  private void recepturesGrid_MouseClick(object sender, MouseEventArgs e)
  {
    DataGridView.HitTestInfo hitTestInfo = this.recepturesGrid.HitTest(e.X, e.Y);
    if (hitTestInfo.ColumnIndex == -1 || hitTestInfo.RowIndex == -1 || e.Button != MouseButtons.Right)
      return;
    DataGridViewCell dataGridViewCell = ((DataGridView) sender)[hitTestInfo.ColumnIndex, hitTestInfo.RowIndex];
    if (!dataGridViewCell.Selected)
    {
      dataGridViewCell.DataGridView.ClearSelection();
      dataGridViewCell.DataGridView.CurrentCell = dataGridViewCell;
      dataGridViewCell.Selected = true;
    }
    this.cmRecepturesName.Show((Control) this.recepturesGrid, new Point(e.X, e.Y));
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
    {
      this.Unsubscribe();
      this.components.Dispose();
    }
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    DataGridViewCellStyle gridViewCellStyle1 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle2 = new DataGridViewCellStyle();
    this.splitContainer1 = new SplitContainer();
    this.recepturesGrid = new DataGridView();
    this.colCaption = new DataGridViewTextWithButtonColumn();
    this.lbReceptures = new Label();
    this.compositionGrid = new DataGridView();
    this.colComponent = new DataGridViewTextWithButtonColumn();
    this.colCount = new DataGridViewTextWithButtonColumn();
    this.lbComposition = new Label();
    this.cmRecepturesName = new ContextMenuStrip(this.components);
    this.перейтиКОбъектуImbaseToolStripMenuItem = new ToolStripMenuItem();
    this.cmRecepturesComposition = new ContextMenuStrip(this.components);
    this.перейтиКОбъектуImbaseToolStripMenuItem1 = new ToolStripMenuItem();
    this.textWithButtonColumn2 = new TextWithButtonColumn();
    this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
    this.textWithButtonColumn1 = new TextWithButtonColumn();
    this.splitContainer1.BeginInit();
    this.splitContainer1.Panel1.SuspendLayout();
    this.splitContainer1.Panel2.SuspendLayout();
    this.splitContainer1.SuspendLayout();
    ((ISupportInitialize) this.recepturesGrid).BeginInit();
    ((ISupportInitialize) this.compositionGrid).BeginInit();
    this.cmRecepturesName.SuspendLayout();
    this.cmRecepturesComposition.SuspendLayout();
    this.SuspendLayout();
    this.splitContainer1.Dock = DockStyle.Fill;
    this.splitContainer1.Location = new Point(0, 0);
    this.splitContainer1.Name = "splitContainer1";
    this.splitContainer1.Orientation = Orientation.Horizontal;
    this.splitContainer1.Panel1.Controls.Add((Control) this.recepturesGrid);
    this.splitContainer1.Panel1.Controls.Add((Control) this.lbReceptures);
    this.splitContainer1.Panel2.Controls.Add((Control) this.compositionGrid);
    this.splitContainer1.Panel2.Controls.Add((Control) this.lbComposition);
    this.splitContainer1.Size = new Size(724, 415);
    this.splitContainer1.SplitterDistance = 207;
    this.splitContainer1.TabIndex = 2;
    this.recepturesGrid.AllowUserToAddRows = false;
    this.recepturesGrid.AllowUserToDeleteRows = false;
    this.recepturesGrid.AllowUserToResizeColumns = false;
    this.recepturesGrid.AllowUserToResizeRows = false;
    this.recepturesGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
    this.recepturesGrid.BackgroundColor = SystemColors.Window;
    this.recepturesGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    this.recepturesGrid.ColumnHeadersVisible = false;
    this.recepturesGrid.Columns.AddRange((DataGridViewColumn) this.colCaption);
    gridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
    gridViewCellStyle1.BackColor = SystemColors.Window;
    gridViewCellStyle1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    gridViewCellStyle1.ForeColor = SystemColors.ControlText;
    gridViewCellStyle1.SelectionBackColor = SystemColors.ControlLight;
    gridViewCellStyle1.SelectionForeColor = SystemColors.ControlText;
    gridViewCellStyle1.WrapMode = DataGridViewTriState.False;
    this.recepturesGrid.DefaultCellStyle = gridViewCellStyle1;
    this.recepturesGrid.Dock = DockStyle.Fill;
    this.recepturesGrid.EditMode = DataGridViewEditMode.EditProgrammatically;
    this.recepturesGrid.Location = new Point(0, 20);
    this.recepturesGrid.MultiSelect = false;
    this.recepturesGrid.Name = "recepturesGrid";
    this.recepturesGrid.ReadOnly = true;
    this.recepturesGrid.RowHeadersVisible = false;
    this.recepturesGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    this.recepturesGrid.ShowEditingIcon = false;
    this.recepturesGrid.Size = new Size(724, 187);
    this.recepturesGrid.TabIndex = 6;
    this.recepturesGrid.RowEnter += new DataGridViewCellEventHandler(this.recepturesGrid_RowEnter);
    this.recepturesGrid.MouseClick += new MouseEventHandler(this.recepturesGrid_MouseClick);
    this.colCaption.DataPropertyName = "Caption";
    this.colCaption.HeaderText = "Рецептура";
    this.colCaption.Name = "colCaption";
    this.colCaption.ReadOnly = true;
    this.colCaption.TextReadOnly = false;
    this.colCaption.ToolTipText = "Рецептура";
    this.lbReceptures.Dock = DockStyle.Top;
    this.lbReceptures.Location = new Point(0, 0);
    this.lbReceptures.Margin = new Padding(3);
    this.lbReceptures.Name = "lbReceptures";
    this.lbReceptures.Padding = new Padding(3);
    this.lbReceptures.Size = new Size(724, 20);
    this.lbReceptures.TabIndex = 5;
    this.lbReceptures.Text = "Рецептуры:";
    this.compositionGrid.AllowUserToAddRows = false;
    this.compositionGrid.AllowUserToDeleteRows = false;
    this.compositionGrid.AllowUserToResizeRows = false;
    this.compositionGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
    this.compositionGrid.BackgroundColor = SystemColors.Window;
    this.compositionGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    this.compositionGrid.Columns.AddRange((DataGridViewColumn) this.colComponent, (DataGridViewColumn) this.colCount);
    gridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
    gridViewCellStyle2.BackColor = SystemColors.Window;
    gridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    gridViewCellStyle2.ForeColor = SystemColors.ControlText;
    gridViewCellStyle2.SelectionBackColor = SystemColors.ControlLight;
    gridViewCellStyle2.SelectionForeColor = SystemColors.ControlText;
    gridViewCellStyle2.WrapMode = DataGridViewTriState.False;
    this.compositionGrid.DefaultCellStyle = gridViewCellStyle2;
    this.compositionGrid.Dock = DockStyle.Fill;
    this.compositionGrid.Location = new Point(0, 20);
    this.compositionGrid.MultiSelect = false;
    this.compositionGrid.Name = "compositionGrid";
    this.compositionGrid.RowHeadersVisible = false;
    this.compositionGrid.SelectionMode = DataGridViewSelectionMode.CellSelect;
    this.compositionGrid.Size = new Size(724, 184);
    this.compositionGrid.TabIndex = 7;
    this.compositionGrid.MouseClick += new MouseEventHandler(this.compositionGrid_MouseClick);
    this.colComponent.DataPropertyName = "Caption";
    this.colComponent.HeaderText = "Компонент";
    this.colComponent.Name = "colComponent";
    this.colComponent.ReadOnly = true;
    this.colComponent.TextReadOnly = false;
    this.colComponent.ToolTipText = "Компонент";
    this.colCount.DataPropertyName = "cad00267-306c-11d8-b4e9-00304f19f545";
    this.colCount.HeaderText = "Часть";
    this.colCount.Name = "colCount";
    this.colCount.ReadOnly = true;
    this.colCount.TextReadOnly = false;
    this.colCount.ToolTipText = "Часть";
    this.lbComposition.Dock = DockStyle.Top;
    this.lbComposition.Location = new Point(0, 0);
    this.lbComposition.Margin = new Padding(3);
    this.lbComposition.Name = "lbComposition";
    this.lbComposition.Padding = new Padding(3);
    this.lbComposition.Size = new Size(724, 20);
    this.lbComposition.TabIndex = 5;
    this.lbComposition.Text = "Состав:";
    this.cmRecepturesName.Items.AddRange(new ToolStripItem[1]
    {
      (ToolStripItem) this.перейтиКОбъектуImbaseToolStripMenuItem
    });
    this.cmRecepturesName.Name = "cmRecepturesName";
    this.cmRecepturesName.Size = new Size(219, 26);
    this.перейтиКОбъектуImbaseToolStripMenuItem.Name = "перейтиКОбъектуImbaseToolStripMenuItem";
    this.перейтиКОбъектуImbaseToolStripMenuItem.Size = new Size(218, 22);
    this.перейтиКОбъектуImbaseToolStripMenuItem.Text = "Перейти к объекту Imbase";
    this.перейтиКОбъектуImbaseToolStripMenuItem.Click += new EventHandler(this.GoToImbaseObjectToolStripMenuItem_Click);
    this.cmRecepturesComposition.Items.AddRange(new ToolStripItem[1]
    {
      (ToolStripItem) this.перейтиКОбъектуImbaseToolStripMenuItem1
    });
    this.cmRecepturesComposition.Name = "cmRecepturesComposition";
    this.cmRecepturesComposition.Size = new Size(219, 26);
    this.перейтиКОбъектуImbaseToolStripMenuItem1.Name = "перейтиКОбъектуImbaseToolStripMenuItem1";
    this.перейтиКОбъектуImbaseToolStripMenuItem1.Size = new Size(218, 22);
    this.перейтиКОбъектуImbaseToolStripMenuItem1.Text = "Перейти к объекту Imbase";
    this.перейтиКОбъектуImbaseToolStripMenuItem1.Click += new EventHandler(this.GoToImbaseObjectToolStripMenuItem1_Click);
    this.textWithButtonColumn2.DataPropertyName = "cad00267-306c-11d8-b4e9-00304f19f545";
    this.textWithButtonColumn2.HeaderText = "Часть";
    this.textWithButtonColumn2.Name = "textWithButtonColumn2";
    this.textWithButtonColumn2.TextReadOnly = false;
    this.textWithButtonColumn2.Width = 324;
    this.dataGridViewTextBoxColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    this.dataGridViewTextBoxColumn1.DataPropertyName = "Caption";
    this.dataGridViewTextBoxColumn1.HeaderText = "Caption";
    this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
    this.textWithButtonColumn1.DataPropertyName = "Caption";
    this.textWithButtonColumn1.HeaderText = "Компонент";
    this.textWithButtonColumn1.Name = "textWithButtonColumn1";
    this.textWithButtonColumn1.TextReadOnly = false;
    this.textWithButtonColumn1.Width = 325;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.splitContainer1);
    this.Name = nameof (ImbaseTableMixView);
    this.Size = new Size(724, 415);
    this.splitContainer1.Panel1.ResumeLayout(false);
    this.splitContainer1.Panel2.ResumeLayout(false);
    this.splitContainer1.EndInit();
    this.splitContainer1.ResumeLayout(false);
    ((ISupportInitialize) this.recepturesGrid).EndInit();
    ((ISupportInitialize) this.compositionGrid).EndInit();
    this.cmRecepturesName.ResumeLayout(false);
    this.cmRecepturesComposition.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private sealed class ImbaseTableMixViewDescriptionProvider : BaseViewDescriptionProvider
  {
    public override ViewDescription DoGetViewDescription(
      ISelectedItems selectedItems,
      System.IServiceProvider serviceProvider)
    {
      return new ViewDescription()
      {
        Caption = LocalizationHolder.rm.GetString("Imbase.Receptures.ViewName"),
        ImageIndex = -1,
        OrderID = int.MinValue
      };
    }
  }
}
