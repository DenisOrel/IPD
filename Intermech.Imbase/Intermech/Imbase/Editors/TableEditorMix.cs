// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Editors.TableEditorMix
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Bars;
using Intermech.Client.Core.Navigator.Classes.ObjectNode;
using Intermech.Docking;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;
using Intermech.Localization;
using Intermech.PropertyEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Editors;

public class TableEditorMix : DockControl, ICommandTarget
{
  private string _captionColName = "CAPTION";
  private string _childComponentColName = Intermech.Imbase.Consts.LinkToComponentOfCompositeObjectAttGuid.ToString();
  private string _componentColName = Intermech.Imbase.Consts.LinkToCompoundObjectAttGUID.ToString();
  private bool _dirty;
  private DataSet _dataSet;
  private DataTable _dtData;
  private DataTable _dtReceptureNames;
  private DataTable _dtReceptureComposition;
  private bool _checkoutNeed;
  private long _userId = -1;
  private ICommandState _saveCommandState;
  internal const string TableEditorMixGuid = "E2E2DD9A-566F-48E2-94A0-53BD7A500CE1";
  private IContainer components;
  private SplitContainer splitContainer1;
  private Label lbReceptures;
  private Label lbComposition;
  private ContextMenuStrip contextMenuStrip1;
  private ToolStripMenuItem новаяРецептураToolStripMenuItem;
  private ToolStripMenuItem удалитьРецептуруToolStripMenuItem;
  private ContextMenuStrip contextMenuStrip2;
  private DataGridView recepturesGrid;
  private DataGridView compositionGrid;
  private ToolStripMenuItem добавитьКомпонентToolStripMenuItem;
  private ToolStripMenuItem удалитьКомпонентToolStripMenuItem;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
  private TextWithButtonColumn textWithButtonColumn1;
  private TextWithButtonColumn textWithButtonColumn2;
  private DataGridViewTextWithButtonColumn colCaption;
  private DataGridViewTextWithButtonColumn colComponent;
  private DataGridViewTextWithButtonColumn colCount;

  internal long TableMixId { get; private set; }

  private CheckOutMode CheckedOut { get; set; }

  public TableEditorMix()
  {
    this.InitializeComponent();
    this.Guid = new Guid("E2E2DD9A-566F-48E2-94A0-53BD7A500CE1");
    this._saveCommandState = ServicesManager.GetService<ICommandManager>().FindCommand("Save");
  }

  internal void Initialize(long tableId, long parentId)
  {
    this.TableMixId = tableId;
    this.InitGridColumns();
    this.LoadData();
  }

  private void InitGridColumns()
  {
    this.recepturesGrid.AutoGenerateColumns = this.compositionGrid.AutoGenerateColumns = false;
    this.colCaption.TextReadOnly = this.colComponent.TextReadOnly = this.colCount.TextReadOnly = true;
    this.colComponent.ButtonClick += new EventHandler(this.ColComponent_ButtonClick);
    this.colCount.ButtonClick += new EventHandler(this.ColCount_ButtonClick);
    this.colComponent.KeyDown += new EventHandler(this.ColComponent_KeyDown);
    this.colCount.KeyDown += new EventHandler(this.ColCount_KeyDown);
  }

  private void LoadData()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      this._userId = session.UserID;
      IDBObject tableObject = session.GetObject(this.TableMixId);
      if (this.UpdateCheckoutStatus(tableObject))
        tableObject = session.GetObject(this.TableMixId);
      this.Text = tableObject.Caption;
      this.LoadTableDataSet(session);
    }
  }

  private void LoadTableDataSet(IUserSession session)
  {
    this._dataSet = TableLoadHelper.GetTables(session, this.TableMixId, true);
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

  private void AddRecepture()
  {
    this.CheckCheckout();
    string empty = string.Empty;
    string str = ServiceUtils.GetService<IImbaseSelector>((object) ServicesManager.ServiceContainer, true).SelectRecord(empty, true);
    if (string.IsNullOrEmpty(str))
      return;
    this._dtData.BeginLoadData();
    DataRow row = this._dtData.NewRow();
    row["F_GUID"] = (object) Guid.NewGuid();
    row[this._componentColName] = (object) str;
    this._dtData.Rows.Add(row);
    this._dtData.EndLoadData();
    this._dtData.AcceptChanges();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.PrepareComponentsData(sessionKeeper.Session);
    this.recepturesGrid.Rows[this.recepturesGrid.Rows.Count - 1].Selected = true;
    this.recepturesGrid_RowEnter((object) this, new DataGridViewCellEventArgs(0, 0));
    this._saveCommandState.Enabled = this._dirty = true;
  }

  private void RemoveRecepture()
  {
    this.CheckCheckout();
    if (this.recepturesGrid.SelectedRows.Count <= 0 || !(this.recepturesGrid.SelectedRows[0].DataBoundItem is DataRowView dataBoundItem))
      return;
    int index = this.recepturesGrid.SelectedRows[0].Index;
    string recordKey = Convert.ToString(dataBoundItem.Row[this._componentColName]);
    List<DataRow> list = this._dtData.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (x => Convert.ToString(x[this._componentColName]) == recordKey)).ToList<DataRow>();
    this._dtData.BeginLoadData();
    list.ForEach((Action<DataRow>) (x => x.Delete()));
    this._dtData.EndLoadData();
    this._dtData.AcceptChanges();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.PrepareComponentsData(sessionKeeper.Session);
    if (this.recepturesGrid.Rows.Count > index)
      this.recepturesGrid.Rows[index].Selected = true;
    this.recepturesGrid_RowEnter((object) this, new DataGridViewCellEventArgs(0, 0));
    this._saveCommandState.Enabled = this._dirty = true;
  }

  private void AddComponent()
  {
    this.CheckCheckout();
    if (this.compositionGrid.SelectedCells.Count <= 0 || !(this.compositionGrid.Rows[this.compositionGrid.SelectedCells[0].RowIndex].DataBoundItem is DataRowView dataBoundItem))
      return;
    object obj = dataBoundItem[this._componentColName];
    string str = ServiceUtils.GetService<IImbaseSelector>((object) ServicesManager.ServiceContainer, true).SelectRecord(string.Empty, true);
    if (string.IsNullOrEmpty(str))
      return;
    this._dtData.BeginLoadData();
    DataRow row = this._dtData.NewRow();
    row["F_GUID"] = (object) Guid.NewGuid();
    row[this._componentColName] = obj;
    row[this._childComponentColName] = (object) str;
    this._dtData.Rows.Add(row);
    this._dtData.EndLoadData();
    this._dtData.AcceptChanges();
    this.recepturesGrid_RowEnter((object) this, new DataGridViewCellEventArgs(0, 0));
    this.compositionGrid.CurrentCell = this.compositionGrid.Rows[this.compositionGrid.Rows.Count - 1].Cells[0];
    this._saveCommandState.Enabled = this._dirty = true;
  }

  private void RemoveComponent()
  {
    this.CheckCheckout();
    if (this.compositionGrid.SelectedCells.Count <= 0 || !(this.compositionGrid.Rows[this.compositionGrid.SelectedCells[0].RowIndex].DataBoundItem is DataRowView dataBoundItem))
      return;
    int rowIndex = this.compositionGrid.SelectedCells[0].RowIndex;
    int columnIndex = this.compositionGrid.SelectedCells[0].ColumnIndex;
    long recordKey = Convert.ToInt64(dataBoundItem.Row["F_KEY"]);
    List<DataRow> list = this._dtData.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (x => Convert.ToInt64(x["F_KEY"]) == recordKey)).ToList<DataRow>();
    this._dtData.BeginLoadData();
    list.ForEach((Action<DataRow>) (x => x.Delete()));
    this._dtData.EndLoadData();
    this._dtData.AcceptChanges();
    this.recepturesGrid_RowEnter((object) this, new DataGridViewCellEventArgs(0, 0));
    if (this.compositionGrid.Rows.Count > rowIndex)
      this.compositionGrid.CurrentCell = this.compositionGrid.Rows[rowIndex].Cells[columnIndex];
    this._saveCommandState.Enabled = this._dirty = true;
  }

  private bool SaveChanges()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this._dtData.AcceptChanges();
      TableLoadHelper.StoreData(sessionKeeper.Session, this.TableMixId, this._dataSet, sessionKeeper.Session.GetCustomService(typeof (ITablesIndexer)) as ITablesIndexer);
      this.CheckIn(sessionKeeper.Session);
      return true;
    }
  }

  private bool UpdateCheckoutStatus(IDBObject tableObject)
  {
    bool flag = false;
    this.CheckedOut = CheckOutMode.None;
    this._checkoutNeed = tableObject.ObjectModifyMode == ObjectModifyModes.Checkout;
    long checkoutBy = tableObject.CheckoutBy;
    if (checkoutBy == this._userId)
    {
      this.CheckedOut = CheckOutMode.CheckedOut;
      flag = true;
    }
    else if (checkoutBy != 0L)
      this.CheckedOut = CheckOutMode.OtherUser;
    return flag;
  }

  private void CheckCheckout()
  {
    if (this.CheckedOut != CheckOutMode.None || !this._checkoutNeed)
      return;
    this.CheckOut();
  }

  private void CheckOut()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this.TableMixId);
      if (dbObject.CheckoutBy != this._userId)
        dbObject.CheckOut(true);
      this.CheckedOut = CheckOutMode.CheckedOut;
    }
  }

  private bool CheckIn(IUserSession session)
  {
    bool flag = false;
    if (this.CheckedOut == CheckOutMode.CheckedOut)
    {
      IDBObject dbObject = session.GetObject(this.TableMixId);
      if (dbObject.CheckoutBy == this._userId)
      {
        dbObject.CheckIn();
        flag = true;
      }
      this.CheckedOut = CheckOutMode.None;
    }
    return flag;
  }

  private void ColCount_ButtonClick(object sender, EventArgs e)
  {
    if (this.compositionGrid.SelectedCells.Count <= 0 || !(this.compositionGrid.Rows[this.compositionGrid.SelectedCells[0].RowIndex].DataBoundItem is DataRowView dataBoundItem))
      return;
    int rowIndex = this.compositionGrid.SelectedCells[0].RowIndex;
    int columnIndex = this.compositionGrid.SelectedCells[0].ColumnIndex;
    int key = Convert.ToInt32(dataBoundItem["F_KEY"]);
    MeasuredValue aMeasureValue = dataBoundItem["cad00267-306c-11d8-b4e9-00304f19f545"] as MeasuredValue;
    using (MeasureForm measureForm = new MeasureForm())
    {
      if (measureForm.ExecuteDialog(ref aMeasureValue, MeasureHelper.Measures) != DialogResult.OK)
        return;
      DataRow dataRow = this._dtData.AsEnumerable().FirstOrDefault<DataRow>((System.Func<DataRow, bool>) (x => Convert.ToInt32(x["F_KEY"]) == key));
      if (dataRow == null)
        return;
      this._dtData.BeginLoadData();
      dataRow["cad00267-306c-11d8-b4e9-00304f19f545"] = (object) aMeasureValue;
      this._dtData.EndLoadData();
      this._dtData.AcceptChanges();
      this.recepturesGrid_RowEnter((object) this, new DataGridViewCellEventArgs(0, 0));
      this.compositionGrid.CurrentCell = this.compositionGrid.Rows[rowIndex].Cells[columnIndex];
      this._saveCommandState.Enabled = this._dirty = true;
    }
  }

  private void ColComponent_ButtonClick(object sender, EventArgs e)
  {
    if (this.compositionGrid.SelectedCells.Count <= 0 || !(this.compositionGrid.Rows[this.compositionGrid.SelectedCells[0].RowIndex].DataBoundItem is DataRowView dataBoundItem))
      return;
    int rowIndex = this.compositionGrid.SelectedCells[0].RowIndex;
    int columnIndex = this.compositionGrid.SelectedCells[0].ColumnIndex;
    long key = Convert.ToInt64(dataBoundItem["F_KEY"]);
    object obj = dataBoundItem[this._childComponentColName];
    IImbaseSelector service = ServiceUtils.GetService<IImbaseSelector>((object) ServicesManager.ServiceContainer, true);
    string empty = string.Empty;
    if (obj != null && obj != DBNull.Value)
      empty = obj.ToString();
    string strImbaseKey = empty;
    string str = service.SelectRecord(strImbaseKey, true);
    if (string.IsNullOrEmpty(str))
      return;
    DataRow dataRow = this._dtData.AsEnumerable().FirstOrDefault<DataRow>((System.Func<DataRow, bool>) (x => Convert.ToInt64(x["F_KEY"]) == key));
    if (dataRow == null)
      return;
    this._dtData.BeginLoadData();
    dataRow[this._childComponentColName] = (object) str;
    this._dtData.EndLoadData();
    this._dtData.AcceptChanges();
    this.recepturesGrid_RowEnter((object) this, new DataGridViewCellEventArgs(0, 0));
    this.compositionGrid.CurrentCell = this.compositionGrid.Rows[rowIndex].Cells[columnIndex];
    this._saveCommandState.Enabled = this._dirty = true;
  }

  private void ColCount_KeyDown(object sender, EventArgs e)
  {
    if (this.compositionGrid.SelectedCells.Count <= 0 || !(this.compositionGrid.Rows[this.compositionGrid.SelectedCells[0].RowIndex].DataBoundItem is DataRowView dataBoundItem))
      return;
    int rowIndex = this.compositionGrid.SelectedCells[0].RowIndex;
    int columnIndex = this.compositionGrid.SelectedCells[0].ColumnIndex;
    long key = Convert.ToInt64(dataBoundItem["F_KEY"]);
    dataBoundItem["cad00267-306c-11d8-b4e9-00304f19f545"] = (object) DBNull.Value;
    DataRow dataRow = this._dtData.AsEnumerable().FirstOrDefault<DataRow>((System.Func<DataRow, bool>) (x => (long) Convert.ToInt32(x["F_KEY"]) == key));
    if (dataRow == null)
      return;
    this._dtData.BeginLoadData();
    dataRow["cad00267-306c-11d8-b4e9-00304f19f545"] = (object) DBNull.Value;
    this._dtData.EndLoadData();
    this._dtData.AcceptChanges();
    this.recepturesGrid_RowEnter((object) this, new DataGridViewCellEventArgs(0, 0));
    this.compositionGrid.CurrentCell = this.compositionGrid.Rows[rowIndex].Cells[columnIndex];
    this._saveCommandState.Enabled = this._dirty = true;
  }

  private void ColComponent_KeyDown(object sender, EventArgs e)
  {
    if (this.compositionGrid.SelectedCells.Count <= 0 || !(this.compositionGrid.Rows[this.compositionGrid.SelectedCells[0].RowIndex].DataBoundItem is DataRowView dataBoundItem))
      return;
    int rowIndex = this.compositionGrid.SelectedCells[0].RowIndex;
    int columnIndex = this.compositionGrid.SelectedCells[0].ColumnIndex;
    long key = Convert.ToInt64(dataBoundItem["F_KEY"]);
    dataBoundItem[this._childComponentColName] = (object) DBNull.Value;
    dataBoundItem[this._captionColName] = (object) DBNull.Value;
    DataRow dataRow = this._dtData.AsEnumerable().FirstOrDefault<DataRow>((System.Func<DataRow, bool>) (x => Convert.ToInt64(x["F_KEY"]) == key));
    if (dataRow == null)
      return;
    this._dtData.BeginLoadData();
    dataRow[this._childComponentColName] = (object) DBNull.Value;
    this._dtData.EndLoadData();
    this._dtData.AcceptChanges();
    this.recepturesGrid_RowEnter((object) this, new DataGridViewCellEventArgs(0, 0));
    this.compositionGrid.CurrentCell = this.compositionGrid.Rows[rowIndex].Cells[columnIndex];
    this._saveCommandState.Enabled = this._dirty = true;
  }

  private void newReceptureToolStripMenuItem_Click(object sender, EventArgs e)
  {
    this.AddRecepture();
  }

  private void removeReceptureToolStripMenuItem_Click(object sender, EventArgs e)
  {
    this.RemoveRecepture();
  }

  private void recepturesGrid_RowEnter(object sender, DataGridViewCellEventArgs e)
  {
    if (this.recepturesGrid.SelectedRows.Count > 0 && this.recepturesGrid.SelectedRows[0].DataBoundItem is DataRowView dataBoundItem)
    {
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
    else
      this.compositionGrid.DataSource = (object) null;
  }

  private void addComponentToolStripMenuItem_Click(object sender, EventArgs e)
  {
    this.AddComponent();
  }

  private void removeComponentToolStripMenuItem_Click(object sender, EventArgs e)
  {
    this.RemoveComponent();
  }

  private void contextMenuStrip2_Opening(object sender, CancelEventArgs e)
  {
    ToolStripMenuItem toolStripMenuItem1 = this.добавитьКомпонентToolStripMenuItem;
    DataTable receptureComposition1 = this._dtReceptureComposition;
    int num1 = receptureComposition1 != null ? (receptureComposition1.Rows.Count > 0 ? 1 : 0) : 0;
    toolStripMenuItem1.Enabled = num1 != 0;
    ToolStripMenuItem toolStripMenuItem2 = this.удалитьКомпонентToolStripMenuItem;
    DataTable receptureComposition2 = this._dtReceptureComposition;
    int num2 = receptureComposition2 != null ? (receptureComposition2.Rows.Count > 1 ? 1 : 0) : 0;
    toolStripMenuItem2.Enabled = num2 != 0;
  }

  protected override void OnClosing(CancelEventArgs e)
  {
    if (!this._dirty)
      return;
    switch (MessageBox.Show(LocalizationHolder.rm.GetString("IMB_TABLECHANGED"), LocalizationHolder.rm.GetString("IMB_WARN"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation))
    {
      case DialogResult.Cancel:
        e.Cancel = true;
        break;
      case DialogResult.Yes:
        e.Cancel = !this.SaveChanges();
        break;
    }
    if (!(ServicesManager.GetService(typeof (INotificationService)) is INotificationService service))
      return;
    service.FireEvent((object) this, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", this.TableMixId));
  }

  protected override string GetPersistString() => this.TableMixId.ToString();

  public bool Execute(ICommandState commandState)
  {
    if (commandState == null || !(commandState.CommandName == "Save"))
      return false;
    if (this._dirty)
    {
      this.SaveChanges();
      this._saveCommandState.Enabled = this._dirty = false;
      if (ServicesManager.GetService(typeof (INotificationService)) is INotificationService service)
        service.FireEvent((object) this, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", this.TableMixId));
    }
    return true;
  }

  public bool QueryStatus(ICommandState commandState)
  {
    if (commandState == null || !(commandState.CommandName == "Save"))
      return false;
    commandState.Enabled = this._dirty;
    return true;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
    {
      EditorMixHelper.EditorsMix.Remove(this);
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
    this.contextMenuStrip1 = new ContextMenuStrip(this.components);
    this.новаяРецептураToolStripMenuItem = new ToolStripMenuItem();
    this.удалитьРецептуруToolStripMenuItem = new ToolStripMenuItem();
    this.lbReceptures = new Label();
    this.compositionGrid = new DataGridView();
    this.colComponent = new DataGridViewTextWithButtonColumn();
    this.colCount = new DataGridViewTextWithButtonColumn();
    this.contextMenuStrip2 = new ContextMenuStrip(this.components);
    this.добавитьКомпонентToolStripMenuItem = new ToolStripMenuItem();
    this.удалитьКомпонентToolStripMenuItem = new ToolStripMenuItem();
    this.lbComposition = new Label();
    this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
    this.textWithButtonColumn1 = new TextWithButtonColumn();
    this.textWithButtonColumn2 = new TextWithButtonColumn();
    this.splitContainer1.BeginInit();
    this.splitContainer1.Panel1.SuspendLayout();
    this.splitContainer1.Panel2.SuspendLayout();
    this.splitContainer1.SuspendLayout();
    ((ISupportInitialize) this.recepturesGrid).BeginInit();
    this.contextMenuStrip1.SuspendLayout();
    ((ISupportInitialize) this.compositionGrid).BeginInit();
    this.contextMenuStrip2.SuspendLayout();
    this.SuspendLayout();
    this.splitContainer1.Dock = DockStyle.Fill;
    this.splitContainer1.Location = new Point(0, 0);
    this.splitContainer1.Name = "splitContainer1";
    this.splitContainer1.Orientation = Orientation.Horizontal;
    this.splitContainer1.Panel1.Controls.Add((Control) this.recepturesGrid);
    this.splitContainer1.Panel1.Controls.Add((Control) this.lbReceptures);
    this.splitContainer1.Panel2.Controls.Add((Control) this.compositionGrid);
    this.splitContainer1.Panel2.Controls.Add((Control) this.lbComposition);
    this.splitContainer1.Size = new Size(652, 433);
    this.splitContainer1.SplitterDistance = 217;
    this.splitContainer1.TabIndex = 0;
    this.recepturesGrid.AllowUserToAddRows = false;
    this.recepturesGrid.AllowUserToDeleteRows = false;
    this.recepturesGrid.AllowUserToResizeColumns = false;
    this.recepturesGrid.AllowUserToResizeRows = false;
    this.recepturesGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
    this.recepturesGrid.BackgroundColor = SystemColors.Control;
    this.recepturesGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    this.recepturesGrid.ColumnHeadersVisible = false;
    this.recepturesGrid.Columns.AddRange((DataGridViewColumn) this.colCaption);
    this.recepturesGrid.ContextMenuStrip = this.contextMenuStrip1;
    gridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
    gridViewCellStyle1.BackColor = SystemColors.Window;
    gridViewCellStyle1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    gridViewCellStyle1.ForeColor = SystemColors.ControlText;
    gridViewCellStyle1.SelectionBackColor = SystemColors.ControlDark;
    gridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
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
    this.recepturesGrid.Size = new Size(652, 197);
    this.recepturesGrid.TabIndex = 6;
    this.recepturesGrid.RowEnter += new DataGridViewCellEventHandler(this.recepturesGrid_RowEnter);
    this.colCaption.DataPropertyName = "Caption";
    this.colCaption.HeaderText = "Рецептура";
    this.colCaption.Name = "colCaption";
    this.colCaption.ReadOnly = true;
    this.colCaption.TextReadOnly = false;
    this.colCaption.ToolTipText = "Рецептура";
    this.contextMenuStrip1.Items.AddRange(new ToolStripItem[2]
    {
      (ToolStripItem) this.новаяРецептураToolStripMenuItem,
      (ToolStripItem) this.удалитьРецептуруToolStripMenuItem
    });
    this.contextMenuStrip1.Name = "contextMenuStrip1";
    this.contextMenuStrip1.Size = new Size(179, 48 /*0x30*/);
    this.новаяРецептураToolStripMenuItem.Name = "новаяРецептураToolStripMenuItem";
    this.новаяРецептураToolStripMenuItem.Size = new Size(178, 22);
    this.новаяРецептураToolStripMenuItem.Text = "Новая рецептура";
    this.новаяРецептураToolStripMenuItem.Click += new EventHandler(this.newReceptureToolStripMenuItem_Click);
    this.удалитьРецептуруToolStripMenuItem.Name = "удалитьРецептуруToolStripMenuItem";
    this.удалитьРецептуруToolStripMenuItem.Size = new Size(178, 22);
    this.удалитьРецептуруToolStripMenuItem.Text = "Удалить рецептуру";
    this.удалитьРецептуруToolStripMenuItem.Click += new EventHandler(this.removeReceptureToolStripMenuItem_Click);
    this.lbReceptures.Dock = DockStyle.Top;
    this.lbReceptures.Location = new Point(0, 0);
    this.lbReceptures.Margin = new Padding(3);
    this.lbReceptures.Name = "lbReceptures";
    this.lbReceptures.Padding = new Padding(3);
    this.lbReceptures.Size = new Size(652, 20);
    this.lbReceptures.TabIndex = 5;
    this.lbReceptures.Text = "Рецептуры:";
    this.compositionGrid.AllowUserToAddRows = false;
    this.compositionGrid.AllowUserToDeleteRows = false;
    this.compositionGrid.AllowUserToResizeRows = false;
    this.compositionGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
    this.compositionGrid.BackgroundColor = SystemColors.Control;
    this.compositionGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    this.compositionGrid.Columns.AddRange((DataGridViewColumn) this.colComponent, (DataGridViewColumn) this.colCount);
    this.compositionGrid.ContextMenuStrip = this.contextMenuStrip2;
    gridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
    gridViewCellStyle2.BackColor = SystemColors.Window;
    gridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    gridViewCellStyle2.ForeColor = SystemColors.ControlText;
    gridViewCellStyle2.SelectionBackColor = SystemColors.ControlDark;
    gridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
    gridViewCellStyle2.WrapMode = DataGridViewTriState.False;
    this.compositionGrid.DefaultCellStyle = gridViewCellStyle2;
    this.compositionGrid.Dock = DockStyle.Fill;
    this.compositionGrid.Location = new Point(0, 20);
    this.compositionGrid.MultiSelect = false;
    this.compositionGrid.Name = "compositionGrid";
    this.compositionGrid.RowHeadersVisible = false;
    this.compositionGrid.SelectionMode = DataGridViewSelectionMode.CellSelect;
    this.compositionGrid.Size = new Size(652, 192 /*0xC0*/);
    this.compositionGrid.TabIndex = 7;
    this.colComponent.DataPropertyName = "Caption";
    this.colComponent.HeaderText = "Компонент";
    this.colComponent.Name = "colComponent";
    this.colComponent.TextReadOnly = false;
    this.colComponent.ToolTipText = "Компонент";
    this.colCount.DataPropertyName = "cad00267-306c-11d8-b4e9-00304f19f545";
    this.colCount.HeaderText = "Часть";
    this.colCount.Name = "colCount";
    this.colCount.TextReadOnly = false;
    this.colCount.ToolTipText = "Часть";
    this.contextMenuStrip2.Items.AddRange(new ToolStripItem[2]
    {
      (ToolStripItem) this.добавитьКомпонентToolStripMenuItem,
      (ToolStripItem) this.удалитьКомпонентToolStripMenuItem
    });
    this.contextMenuStrip2.Name = "contextMenuStrip2";
    this.contextMenuStrip2.Size = new Size(191, 70);
    this.contextMenuStrip2.Opening += new CancelEventHandler(this.contextMenuStrip2_Opening);
    this.добавитьКомпонентToolStripMenuItem.Name = "добавитьКомпонентToolStripMenuItem";
    this.добавитьКомпонентToolStripMenuItem.Size = new Size(190, 22);
    this.добавитьКомпонентToolStripMenuItem.Text = "Добавить компонент";
    this.добавитьКомпонентToolStripMenuItem.Click += new EventHandler(this.addComponentToolStripMenuItem_Click);
    this.удалитьКомпонентToolStripMenuItem.Name = "удалитьКомпонентToolStripMenuItem";
    this.удалитьКомпонентToolStripMenuItem.Size = new Size(190, 22);
    this.удалитьКомпонентToolStripMenuItem.Text = "Удалить компонент";
    this.удалитьКомпонентToolStripMenuItem.Click += new EventHandler(this.removeComponentToolStripMenuItem_Click);
    this.lbComposition.Dock = DockStyle.Top;
    this.lbComposition.Location = new Point(0, 0);
    this.lbComposition.Margin = new Padding(3);
    this.lbComposition.Name = "lbComposition";
    this.lbComposition.Padding = new Padding(3);
    this.lbComposition.Size = new Size(652, 20);
    this.lbComposition.TabIndex = 5;
    this.lbComposition.Text = "Состав:";
    this.dataGridViewTextBoxColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    this.dataGridViewTextBoxColumn1.DataPropertyName = "Caption";
    this.dataGridViewTextBoxColumn1.HeaderText = "Caption";
    this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
    this.textWithButtonColumn1.DataPropertyName = "Caption";
    this.textWithButtonColumn1.HeaderText = "Компонент";
    this.textWithButtonColumn1.Name = "textWithButtonColumn1";
    this.textWithButtonColumn1.TextReadOnly = false;
    this.textWithButtonColumn1.Width = 325;
    this.textWithButtonColumn2.DataPropertyName = "cad00267-306c-11d8-b4e9-00304f19f545";
    this.textWithButtonColumn2.HeaderText = "Часть";
    this.textWithButtonColumn2.Name = "textWithButtonColumn2";
    this.textWithButtonColumn2.TextReadOnly = false;
    this.textWithButtonColumn2.Width = 324;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.splitContainer1);
    this.Name = nameof (TableEditorMix);
    this.Size = new Size(652, 433);
    this.splitContainer1.Panel1.ResumeLayout(false);
    this.splitContainer1.Panel2.ResumeLayout(false);
    this.splitContainer1.EndInit();
    this.splitContainer1.ResumeLayout(false);
    ((ISupportInitialize) this.recepturesGrid).EndInit();
    this.contextMenuStrip1.ResumeLayout(false);
    ((ISupportInitialize) this.compositionGrid).EndInit();
    this.contextMenuStrip2.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
