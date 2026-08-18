// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.PropertyPages.NormalizedIndexesReplacesPage
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.DatabaseConfigurator.Utils;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

#nullable disable
namespace Intermech.DatabaseConfigurator.PropertyPages;

public class NormalizedIndexesReplacesPage : Form, IPropertyPage, IPropertyPageSearchOptionEvents
{
  private System.ComponentModel.Container components;
  private DataSet dataSet;
  private DataTable dubbedDataTable;
  private DataTable replaceDataTable;
  private DataColumn valueDataColumn;
  private DataColumn value1DataColumn;
  private DataColumn value2DataColumn;
  private System.IServiceProvider _provider;
  private const char separator = '|';
  private string dubSafe = string.Empty;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
  private SplitContainer splitContainer1;
  private Panel panelReplace;
  private Label labelReplace;
  private DataGridView dgvReplace;
  private ToolStrip toolStrip1;
  private ToolStripButton btnAddString;
  private ToolStripButton btnDeleteString;
  private Panel panelDub;
  private DataGridView dgvDubbedSymbol;
  private ToolStrip toolStrip2;
  private ToolStripButton btnAddSymbol;
  private ToolStripButton btnDelSymbol;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
  private DataGridViewTextBoxColumn fVALUEDataGridViewTextBoxColumn;
  private DataGridViewTextBoxColumn DataGridViewTextBoxColumnString;
  private DataGridViewTextBoxColumn DataGridViewTextBoxColumnToString;
  private string replSafe = string.Empty;

  public NormalizedIndexesReplacesPage() => this.InitializeComponent();

  public NormalizedIndexesReplacesPage(System.IServiceProvider provider)
    : this()
  {
    this.TopLevel = false;
    this._provider = provider;
    ((IPropertyPagesService) this._provider.GetService(typeof (IPropertyPagesService)))?.AddPage(LocalizationHolder.rm.GetString("DatabaseConfigurator_85"), (IPropertyPage) this);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (NormalizedIndexesReplacesPage));
    this.splitContainer1 = new SplitContainer();
    this.panelDub = new Panel();
    this.dgvDubbedSymbol = new DataGridView();
    this.fVALUEDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
    this.dataSet = new DataSet();
    this.dubbedDataTable = new DataTable();
    this.valueDataColumn = new DataColumn();
    this.replaceDataTable = new DataTable();
    this.value1DataColumn = new DataColumn();
    this.value2DataColumn = new DataColumn();
    this.toolStrip2 = new ToolStrip();
    this.btnAddSymbol = new ToolStripButton();
    this.btnDelSymbol = new ToolStripButton();
    this.panelReplace = new Panel();
    this.labelReplace = new Label();
    this.toolStrip1 = new ToolStrip();
    this.btnAddString = new ToolStripButton();
    this.btnDeleteString = new ToolStripButton();
    this.dgvReplace = new DataGridView();
    this.DataGridViewTextBoxColumnString = new DataGridViewTextBoxColumn();
    this.DataGridViewTextBoxColumnToString = new DataGridViewTextBoxColumn();
    this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
    this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
    this.dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
    this.splitContainer1.BeginInit();
    this.splitContainer1.Panel1.SuspendLayout();
    this.splitContainer1.Panel2.SuspendLayout();
    this.splitContainer1.SuspendLayout();
    this.panelDub.SuspendLayout();
    ((ISupportInitialize) this.dgvDubbedSymbol).BeginInit();
    this.dataSet.BeginInit();
    this.dubbedDataTable.BeginInit();
    this.replaceDataTable.BeginInit();
    this.toolStrip2.SuspendLayout();
    this.panelReplace.SuspendLayout();
    this.toolStrip1.SuspendLayout();
    ((ISupportInitialize) this.dgvReplace).BeginInit();
    this.SuspendLayout();
    this.splitContainer1.BackColor = SystemColors.InactiveCaption;
    componentResourceManager.ApplyResources((object) this.splitContainer1, "splitContainer1");
    this.splitContainer1.Name = "splitContainer1";
    componentResourceManager.ApplyResources((object) this.splitContainer1.Panel1, "splitContainer1.Panel1");
    this.splitContainer1.Panel1.BackColor = SystemColors.Control;
    this.splitContainer1.Panel1.Controls.Add((System.Windows.Forms.Control) this.panelDub);
    componentResourceManager.ApplyResources((object) this.splitContainer1.Panel2, "splitContainer1.Panel2");
    this.splitContainer1.Panel2.BackColor = SystemColors.Control;
    this.splitContainer1.Panel2.Controls.Add((System.Windows.Forms.Control) this.panelReplace);
    componentResourceManager.ApplyResources((object) this.panelDub, "panelDub");
    this.panelDub.Controls.Add((System.Windows.Forms.Control) this.dgvDubbedSymbol);
    this.panelDub.Controls.Add((System.Windows.Forms.Control) this.toolStrip2);
    this.panelDub.Name = "panelDub";
    this.dgvDubbedSymbol.AllowUserToAddRows = false;
    this.dgvDubbedSymbol.AllowUserToResizeColumns = false;
    this.dgvDubbedSymbol.AllowUserToResizeRows = false;
    componentResourceManager.ApplyResources((object) this.dgvDubbedSymbol, "dgvDubbedSymbol");
    this.dgvDubbedSymbol.AutoGenerateColumns = false;
    this.dgvDubbedSymbol.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    this.dgvDubbedSymbol.Columns.AddRange((DataGridViewColumn) this.fVALUEDataGridViewTextBoxColumn);
    this.dgvDubbedSymbol.DataMember = "Dubbed";
    this.dgvDubbedSymbol.DataSource = (object) this.dataSet;
    this.dgvDubbedSymbol.MultiSelect = false;
    this.dgvDubbedSymbol.Name = "dgvDubbedSymbol";
    this.dgvDubbedSymbol.RowHeadersVisible = false;
    this.dgvDubbedSymbol.CurrentCellChanged += new EventHandler(this.dgvDubbedSymbol_CurrentCellChanged);
    this.fVALUEDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    this.fVALUEDataGridViewTextBoxColumn.DataPropertyName = "F_VALUE";
    componentResourceManager.ApplyResources((object) this.fVALUEDataGridViewTextBoxColumn, "fVALUEDataGridViewTextBoxColumn");
    this.fVALUEDataGridViewTextBoxColumn.Name = "fVALUEDataGridViewTextBoxColumn";
    this.fVALUEDataGridViewTextBoxColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.dataSet.DataSetName = "NewDataSet";
    this.dataSet.Locale = new CultureInfo("ru-RU");
    this.dataSet.Tables.AddRange(new DataTable[2]
    {
      this.dubbedDataTable,
      this.replaceDataTable
    });
    this.dubbedDataTable.Columns.AddRange(new DataColumn[1]
    {
      this.valueDataColumn
    });
    this.dubbedDataTable.TableName = "Dubbed";
    this.valueDataColumn.Caption = "Устранять дублирование следующих символов";
    this.valueDataColumn.ColumnName = "F_VALUE";
    this.valueDataColumn.DefaultValue = (object) "";
    this.replaceDataTable.Columns.AddRange(new DataColumn[2]
    {
      this.value1DataColumn,
      this.value2DataColumn
    });
    this.replaceDataTable.TableName = "Replace";
    this.value1DataColumn.Caption = "";
    this.value1DataColumn.ColumnName = "строку:";
    this.value1DataColumn.DefaultValue = (object) "";
    this.value2DataColumn.Caption = "";
    this.value2DataColumn.ColumnName = "на строку:";
    this.value2DataColumn.DefaultValue = (object) "";
    this.toolStrip2.GripStyle = ToolStripGripStyle.Hidden;
    this.toolStrip2.Items.AddRange(new ToolStripItem[2]
    {
      (ToolStripItem) this.btnAddSymbol,
      (ToolStripItem) this.btnDelSymbol
    });
    componentResourceManager.ApplyResources((object) this.toolStrip2, "toolStrip2");
    this.toolStrip2.Name = "toolStrip2";
    this.btnAddSymbol.BackColor = SystemColors.Control;
    this.btnAddSymbol.DisplayStyle = ToolStripItemDisplayStyle.Image;
    componentResourceManager.ApplyResources((object) this.btnAddSymbol, "btnAddSymbol");
    this.btnAddSymbol.Name = "btnAddSymbol";
    this.btnAddSymbol.Padding = new Padding(2, 0, 2, 0);
    this.btnAddSymbol.Click += new EventHandler(this.btnAddSymbol_Click);
    this.btnDelSymbol.DisplayStyle = ToolStripItemDisplayStyle.Image;
    componentResourceManager.ApplyResources((object) this.btnDelSymbol, "btnDelSymbol");
    this.btnDelSymbol.Name = "btnDelSymbol";
    this.btnDelSymbol.Click += new EventHandler(this.btnDelSymbol_Click);
    componentResourceManager.ApplyResources((object) this.panelReplace, "panelReplace");
    this.panelReplace.Controls.Add((System.Windows.Forms.Control) this.labelReplace);
    this.panelReplace.Controls.Add((System.Windows.Forms.Control) this.toolStrip1);
    this.panelReplace.Controls.Add((System.Windows.Forms.Control) this.dgvReplace);
    this.panelReplace.Name = "panelReplace";
    componentResourceManager.ApplyResources((object) this.labelReplace, "labelReplace");
    this.labelReplace.Name = "labelReplace";
    this.toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
    this.toolStrip1.Items.AddRange(new ToolStripItem[2]
    {
      (ToolStripItem) this.btnAddString,
      (ToolStripItem) this.btnDeleteString
    });
    componentResourceManager.ApplyResources((object) this.toolStrip1, "toolStrip1");
    this.toolStrip1.Name = "toolStrip1";
    this.btnAddString.BackColor = SystemColors.Control;
    this.btnAddString.DisplayStyle = ToolStripItemDisplayStyle.Image;
    componentResourceManager.ApplyResources((object) this.btnAddString, "btnAddString");
    this.btnAddString.Name = "btnAddString";
    this.btnAddString.Padding = new Padding(2, 0, 2, 0);
    this.btnAddString.Click += new EventHandler(this.btnAdd_Click);
    this.btnDeleteString.DisplayStyle = ToolStripItemDisplayStyle.Image;
    componentResourceManager.ApplyResources((object) this.btnDeleteString, "btnDeleteString");
    this.btnDeleteString.Name = "btnDeleteString";
    this.btnDeleteString.Click += new EventHandler(this.btnDeleteString_Click);
    this.dgvReplace.AllowUserToAddRows = false;
    this.dgvReplace.AllowUserToDeleteRows = false;
    this.dgvReplace.AllowUserToResizeRows = false;
    componentResourceManager.ApplyResources((object) this.dgvReplace, "dgvReplace");
    this.dgvReplace.AutoGenerateColumns = false;
    this.dgvReplace.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    this.dgvReplace.Columns.AddRange((DataGridViewColumn) this.DataGridViewTextBoxColumnString, (DataGridViewColumn) this.DataGridViewTextBoxColumnToString);
    this.dgvReplace.DataMember = "Replace";
    this.dgvReplace.DataSource = (object) this.dataSet;
    this.dgvReplace.MultiSelect = false;
    this.dgvReplace.Name = "dgvReplace";
    this.dgvReplace.RowHeadersVisible = false;
    this.dgvReplace.CurrentCellChanged += new EventHandler(this.dgvReplace_CurrentCellChanged);
    this.DataGridViewTextBoxColumnString.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
    this.DataGridViewTextBoxColumnString.DataPropertyName = "строку:";
    componentResourceManager.ApplyResources((object) this.DataGridViewTextBoxColumnString, "DataGridViewTextBoxColumnString");
    this.DataGridViewTextBoxColumnString.Name = "DataGridViewTextBoxColumnString";
    this.DataGridViewTextBoxColumnString.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.DataGridViewTextBoxColumnToString.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    this.DataGridViewTextBoxColumnToString.DataPropertyName = "на строку:";
    componentResourceManager.ApplyResources((object) this.DataGridViewTextBoxColumnToString, "DataGridViewTextBoxColumnToString");
    this.DataGridViewTextBoxColumnToString.Name = "DataGridViewTextBoxColumnToString";
    this.DataGridViewTextBoxColumnToString.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.dataGridViewTextBoxColumn1.DataPropertyName = "строку:";
    componentResourceManager.ApplyResources((object) this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
    this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
    this.dataGridViewTextBoxColumn2.DataPropertyName = "на строку:";
    componentResourceManager.ApplyResources((object) this.dataGridViewTextBoxColumn2, "dataGridViewTextBoxColumn2");
    this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
    this.dataGridViewTextBoxColumn3.DataPropertyName = "на строку:";
    componentResourceManager.ApplyResources((object) this.dataGridViewTextBoxColumn3, "dataGridViewTextBoxColumn3");
    this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((System.Windows.Forms.Control) this.splitContainer1);
    this.FormBorderStyle = FormBorderStyle.None;
    this.Name = nameof (NormalizedIndexesReplacesPage);
    this.Tag = (object) "              ";
    this.Load += new EventHandler(this.NormalizedIndexesReplacesPage_Load);
    this.splitContainer1.Panel1.ResumeLayout(false);
    this.splitContainer1.Panel1.PerformLayout();
    this.splitContainer1.Panel2.ResumeLayout(false);
    this.splitContainer1.Panel2.PerformLayout();
    this.splitContainer1.EndInit();
    this.splitContainer1.ResumeLayout(false);
    this.panelDub.ResumeLayout(false);
    this.panelDub.PerformLayout();
    ((ISupportInitialize) this.dgvDubbedSymbol).EndInit();
    this.dataSet.EndInit();
    this.dubbedDataTable.EndInit();
    this.replaceDataTable.EndInit();
    this.toolStrip2.ResumeLayout(false);
    this.toolStrip2.PerformLayout();
    this.panelReplace.ResumeLayout(false);
    this.panelReplace.PerformLayout();
    this.toolStrip1.ResumeLayout(false);
    this.toolStrip1.PerformLayout();
    ((ISupportInitialize) this.dgvReplace).EndInit();
    this.ResumeLayout(false);
  }

  private bool IsChanged
  {
    get
    {
      string dub;
      string repl;
      this.ReturnValues(out dub, out repl, false);
      return dub != this.dubSafe || repl != this.replSafe;
    }
  }

  public string HelpTopicID => "1064";

  public void Cancel()
  {
    if (!this.IsChanged)
      return;
    this.FillValues(this.dubSafe, this.replSafe);
  }

  public object Control => (object) this;

  public void Apply()
  {
    if (!this.IsChanged)
      return;
    this.ReturnValues(out this.dubSafe, out this.replSafe, true);
    this.SaveValues(this.dubSafe, this.replSafe);
  }

  public PropertyPageType Type => PropertyPageType.Control;

  public string PageName => LocalizationHolder.rm.GetString("DatabaseConfigurator_86");

  public string HeaderText
  {
    [DebuggerStepThrough] get => this.PageName;
  }

  public event EventHandler Changed;

  public List<string> GetOptionNames()
  {
    return !(this.Control is System.Windows.Forms.Control control) ? new List<string>() : IPropertyPageHelper.GetOptionNames(control);
  }

  private void NormalizedIndexesReplacesPage_Load(object sender, EventArgs e)
  {
    this.LoadValues(out this.dubSafe, out this.replSafe);
    this.FillValues(this.dubSafe, this.replSafe);
  }

  private void LoadValues(out string dub, out string repl)
  {
    IDBConfigurations service = ServicesManager.GetService(typeof (IDBConfigurations)) as IDBConfigurations;
    dub = service.ReadString("KERNEL", "INDEX_PARAMS", "DUPLICATES", string.Empty, DBConfigMode.GlobalOnly);
    repl = service.ReadString("KERNEL", "INDEX_PARAMS", "REPLACES", string.Empty, DBConfigMode.GlobalOnly);
  }

  private void FillValues(string dub, string repl)
  {
    this.dubbedDataTable.Rows.Clear();
    string[] strArray1;
    if (dub.Length != 0)
      strArray1 = dub.Split('|');
    else
      strArray1 = new string[0];
    foreach (object obj in strArray1)
    {
      DataRow row = this.dubbedDataTable.NewRow();
      row[0] = obj;
      this.dubbedDataTable.Rows.Add(row);
    }
    this.replaceDataTable.Rows.Clear();
    string[] strArray2;
    if (repl.Length != 0)
      strArray2 = repl.Split('|');
    else
      strArray2 = new string[0];
    string[] strArray3 = strArray2;
    for (int index = 0; index < strArray3.Length; index += 2)
    {
      DataRow row = this.replaceDataTable.NewRow();
      row[0] = (object) strArray3[index];
      row[1] = (object) strArray3[index + 1];
      this.replaceDataTable.Rows.Add(row);
    }
  }

  private void ReturnValues(out string dub, out string repl, bool removeEmptyValues)
  {
    ArrayList arrayList = new ArrayList();
    string empty = string.Empty;
    foreach (DataRow row in (InternalDataCollectionBase) this.dubbedDataTable.Rows)
    {
      if (row.RowState != DataRowState.Detached && row.RowState != DataRowState.Deleted)
      {
        if (row[0].ToString() != string.Empty)
        {
          if (empty.Length > 0)
            empty += "|";
          empty += row[0].ToString();
        }
        else
          arrayList.Add((object) row);
      }
    }
    dub = empty;
    if (removeEmptyValues)
    {
      for (int index = 0; index < arrayList.Count; ++index)
        this.dubbedDataTable.Rows.Remove((DataRow) arrayList[index]);
    }
    arrayList.Clear();
    string str = string.Empty;
    foreach (DataRow row in (InternalDataCollectionBase) this.replaceDataTable.Rows)
    {
      if (row.RowState != DataRowState.Detached && row.RowState != DataRowState.Deleted)
      {
        if (row[0].ToString() != string.Empty)
        {
          if (str.Length > 0)
            str += "|";
          str = $"{str}{row[0].ToString()}|{row[1].ToString()}";
        }
        else
          arrayList.Add((object) row);
      }
    }
    repl = str;
    if (!removeEmptyValues)
      return;
    for (int index = 0; index < arrayList.Count; ++index)
      this.replaceDataTable.Rows.Remove((DataRow) arrayList[index]);
  }

  private void SaveValues(string dub, string repl)
  {
    IDBConfigurations service = ServicesManager.GetService(typeof (IDBConfigurations)) as IDBConfigurations;
    service.WriteString("KERNEL", "INDEX_PARAMS", "DUPLICATES", dub, 0L);
    service.WriteString("KERNEL", "INDEX_PARAMS", "REPLACES", repl, 0L);
    ((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IAdminUtilsService)) as IAdminUtilsService).ReloadIndexSettings();
    if (IndexRebuilder.Indexing || MessageBox.Show(LocalizationHolder.rm.GetString("DatabaseConfigurator_87"), LocalizationHolder.rm.GetString("DatabaseConfigurator_88"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation) == DialogResult.Cancel)
      return;
    IndexRebuilder task = new IndexRebuilder();
    ((IBackgroundTaskView) ServicesManager.GetService(typeof (IBackgroundTaskView)))?.AddTask((IBackgroundTask) task);
    new Thread(new ThreadStart(task.RebuildIndex)).Start();
  }

  private void btnAdd_Click(object sender, EventArgs e)
  {
    this.replaceDataTable.Rows.Add(this.replaceDataTable.NewRow());
  }

  private void btnDeleteString_Click(object sender, EventArgs e)
  {
    if (this.dgvReplace.CurrentRow == null)
      return;
    int index = this.dgvReplace.CurrentRow.Index;
    this.dgvReplace.DataSource = (object) null;
    this.replaceDataTable.Rows.RemoveAt(index);
    this.dgvReplace.DataSource = (object) this.replaceDataTable;
    this.dgvReplace.Rows.RemoveAt(this.dgvReplace.CurrentRow.Index);
    this.replaceDataTable.AcceptChanges();
    if (this.replaceDataTable.Rows.Count == 0)
      this.btnDeleteString.Enabled = false;
    this.Changed((object) this, new EventArgs());
  }

  private void btnAddSymbol_Click(object sender, EventArgs e)
  {
    this.dubbedDataTable.Rows.Add(this.dubbedDataTable.NewRow());
  }

  private void dgvDubbedSymbol_CurrentCellChanged(object sender, EventArgs e)
  {
    this.btnDelSymbol.Enabled = true;
    if (!this.IsChanged)
      return;
    this.Changed((object) this, new EventArgs());
  }

  private void dgvReplace_CurrentCellChanged(object sender, EventArgs e)
  {
    this.btnDeleteString.Enabled = true;
    if (!this.IsChanged)
      return;
    this.Changed((object) this, new EventArgs());
  }

  private void btnDelSymbol_Click(object sender, EventArgs e)
  {
    if (this.dgvDubbedSymbol.CurrentRow == null)
      return;
    int index = this.dgvDubbedSymbol.CurrentRow.Index;
    this.dgvDubbedSymbol.DataSource = (object) null;
    this.dubbedDataTable.Rows.RemoveAt(index);
    this.dgvDubbedSymbol.DataSource = (object) this.dubbedDataTable;
    if (this.dubbedDataTable.Rows.Count == 0)
      this.btnDelSymbol.Enabled = false;
    this.Changed((object) this, new EventArgs());
  }
}
