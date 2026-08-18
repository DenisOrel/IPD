// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.SynchObjectsResultCtrl
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase;

public class SynchObjectsResultCtrl : UserControl
{
  private BaseSynchObjectsService _srv;
  private int _rowIndex = -1;
  private int _columnIndex = -1;
  private IContainer components;
  private Label _lbFilter;
  private ComboBox _cmbFilter;
  private Panel _pnlTop;
  private Panel _pnlProcess;
  private ProgressBar _progress;
  private Label _lbResult;
  private DataGridView _dgv;
  private Label _lbProcess;
  private RichTextBox _rtbReport;
  private SplitContainer splitContainer1;
  private ToolTip toolTip1;
  private ContextMenuStrip _cm;
  private ToolStripMenuItem _miCopyRow;
  private ToolStripMenuItem _miCopyCell;
  private ToolStripMenuItem _miCopyTable;

  public SynchObjectsResultCtrl(BaseSynchObjectsService srv)
  {
    this.InitializeComponent();
    this._srv = srv;
    this._dgv.DataSource = (object) this._srv.GridDataSource;
    this._srv.CustomizeGrid(this._dgv);
    this._cmbFilter.DataSource = (object) this._srv.FilterDataSource;
    this._cmbFilter.SelectedIndex = 0;
    this._srv.Subscribe(new Action<DataTable>(this.AddResultRow));
    this._srv.Subscribe(new Action<string, int, int>(this.DataChanged));
  }

  private void On_cm_Opening(object sender, CancelEventArgs e)
  {
    this._miCopyCell.Enabled = this._dgv.SelectedRows.Count == 1 && this._rowIndex > -1 && this._rowIndex < this._dgv.Rows.Count && this._columnIndex > -1 && this._columnIndex < this._dgv.Columns.Count;
    this._miCopyRow.Enabled = this._dgv.SelectedRows.Count > 0;
    this._miCopyTable.Enabled = this._dgv.Rows.Count > 0;
  }

  private void On_miCopyCell_Click(object sender, EventArgs e)
  {
    DataGridViewSelectedRowCollection selectedRows = this._dgv.SelectedRows;
    DataGridViewSelectionMode selectionMode = this._dgv.SelectionMode;
    try
    {
      this.SetDataGridViewSelectionMode(DataGridViewSelectionMode.CellSelect);
      this._dgv.Rows[this._rowIndex].Cells[this._columnIndex].Selected = true;
      this.CopyData(DataGridViewClipboardCopyMode.EnableWithoutHeaderText);
    }
    finally
    {
      this.SetDataGridViewSelectionMode(selectionMode);
      foreach (DataGridViewBand dataGridViewBand in (BaseCollection) selectedRows)
        dataGridViewBand.Selected = true;
    }
  }

  private void On_miCopyRow_Click(object sender, EventArgs e)
  {
    this.CopyData(DataGridViewClipboardCopyMode.EnableWithoutHeaderText);
  }

  private void On_miCopyTable_Click(object sender, EventArgs e)
  {
    DataGridViewSelectedRowCollection selectedRows = this._dgv.SelectedRows;
    this._dgv.SelectAll();
    this.CopyData(DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText);
    this._dgv.ClearSelection();
    foreach (DataGridViewBand dataGridViewBand in (BaseCollection) selectedRows)
      dataGridViewBand.Selected = true;
  }

  private void On_cmbFilter_SelectedIndexChanged(object sender, EventArgs e)
  {
    this._srv.SetFilter(Convert.ToString(this._cmbFilter.SelectedItem));
  }

  private void On_dgv_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
  {
    if (this._dgv.SelectedRows.Count != 1 || this._dgv.SelectedRows[0].Index != e.RowIndex)
      return;
    this._rowIndex = e.RowIndex;
    this._columnIndex = e.ColumnIndex;
  }

  private void On_dgv_SelectionChanged(object sender, EventArgs e)
  {
    if (this._dgv.SelectedCells != null && this._dgv.SelectedCells.Count > 0)
      this._rtbReport.Text = this._srv.GetReport((this._dgv.CurrentRow.DataBoundItem as DataRowView).Row, this._dgv.CurrentCell.ColumnIndex);
    else
      this._rtbReport.Text = string.Empty;
  }

  private void AddResultRow(DataTable table)
  {
    if (this.InvokeRequired)
    {
      this.Invoke((Delegate) new Action<DataTable>(this.AddResultRow), (object) table);
    }
    else
    {
      this._srv.AddResultRow(table);
      this._dgv.Invalidate();
    }
  }

  private void DataChanged(string text, int count, int current)
  {
    if (this.InvokeRequired)
    {
      this.Invoke((Delegate) new Action<string, int, int>(this.DataChanged), (object) text, (object) count, (object) current);
    }
    else
    {
      this.SetProgressState(text, count, current);
      this._dgv.Invalidate();
    }
  }

  private void CopyData(DataGridViewClipboardCopyMode mode)
  {
    this._dgv.ClipboardCopyMode = mode;
    Clipboard.SetDataObject((object) this._dgv.GetClipboardContent());
  }

  private void SetDataGridViewSelectionMode(DataGridViewSelectionMode mode)
  {
    this._dgv.ClearSelection();
    this._dgv.SelectionMode = mode;
  }

  private void SetProgressState(string caption, int max, int value)
  {
    this._lbProcess.Text = caption;
    this._progress.Maximum = max;
    this._progress.Minimum = 0;
    this._progress.Value = value;
    this._lbResult.Text = $"{value.ToString()} из {max.ToString()}";
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    this._lbFilter = new Label();
    this._cmbFilter = new ComboBox();
    this._pnlTop = new Panel();
    this._pnlProcess = new Panel();
    this._progress = new ProgressBar();
    this._lbResult = new Label();
    this._lbProcess = new Label();
    this._dgv = new DataGridView();
    this._cm = new ContextMenuStrip(this.components);
    this._miCopyRow = new ToolStripMenuItem();
    this._miCopyCell = new ToolStripMenuItem();
    this._miCopyTable = new ToolStripMenuItem();
    this._rtbReport = new RichTextBox();
    this.splitContainer1 = new SplitContainer();
    this.toolTip1 = new ToolTip(this.components);
    this._pnlTop.SuspendLayout();
    this._pnlProcess.SuspendLayout();
    ((ISupportInitialize) this._dgv).BeginInit();
    this._cm.SuspendLayout();
    this.splitContainer1.BeginInit();
    this.splitContainer1.Panel1.SuspendLayout();
    this.splitContainer1.Panel2.SuspendLayout();
    this.splitContainer1.SuspendLayout();
    this.SuspendLayout();
    this._lbFilter.AutoSize = true;
    this._lbFilter.Dock = DockStyle.Left;
    this._lbFilter.Location = new Point(5, 5);
    this._lbFilter.Name = "_lbFilter";
    this._lbFilter.Padding = new Padding(0, 3, 0, 0);
    this._lbFilter.Size = new Size(194, 16 /*0x10*/);
    this._lbFilter.TabIndex = 1;
    this._lbFilter.Text = "Отобразить результаты со статусом";
    this._cmbFilter.Dock = DockStyle.Fill;
    this._cmbFilter.DropDownStyle = ComboBoxStyle.DropDownList;
    this._cmbFilter.FormattingEnabled = true;
    this._cmbFilter.Location = new Point(199, 5);
    this._cmbFilter.Name = "_cmbFilter";
    this._cmbFilter.Size = new Size(395, 21);
    this._cmbFilter.TabIndex = 2;
    this.toolTip1.SetToolTip((Control) this._cmbFilter, "Статус синхронизации");
    this._cmbFilter.SelectedIndexChanged += new EventHandler(this.On_cmbFilter_SelectedIndexChanged);
    this._pnlTop.Controls.Add((Control) this._cmbFilter);
    this._pnlTop.Controls.Add((Control) this._lbFilter);
    this._pnlTop.Dock = DockStyle.Top;
    this._pnlTop.Location = new Point(0, 0);
    this._pnlTop.Name = "_pnlTop";
    this._pnlTop.Padding = new Padding(5);
    this._pnlTop.Size = new Size(599, 33);
    this._pnlTop.TabIndex = 3;
    this._pnlProcess.Controls.Add((Control) this._progress);
    this._pnlProcess.Controls.Add((Control) this._lbResult);
    this._pnlProcess.Controls.Add((Control) this._lbProcess);
    this._pnlProcess.Dock = DockStyle.Bottom;
    this._pnlProcess.Location = new Point(0, 319);
    this._pnlProcess.Name = "_pnlProcess";
    this._pnlProcess.Padding = new Padding(5);
    this._pnlProcess.Size = new Size(599, 53);
    this._pnlProcess.TabIndex = 4;
    this._progress.Dock = DockStyle.Fill;
    this._progress.Location = new Point(5, 26);
    this._progress.Name = "_progress";
    this._progress.Size = new Size(549, 22);
    this._progress.TabIndex = 0;
    this._lbResult.AutoSize = true;
    this._lbResult.Dock = DockStyle.Right;
    this._lbResult.Location = new Point(554, 26);
    this._lbResult.Name = "_lbResult";
    this._lbResult.Padding = new Padding(3, 3, 0, 0);
    this._lbResult.Size = new Size(40, 16 /*0x10*/);
    this._lbResult.TabIndex = 1;
    this._lbResult.Text = "0 из 0";
    this._lbProcess.Dock = DockStyle.Top;
    this._lbProcess.Location = new Point(5, 5);
    this._lbProcess.Name = "_lbProcess";
    this._lbProcess.Size = new Size(589, 21);
    this._lbProcess.TabIndex = 2;
    this._lbProcess.Text = "Синхронизация объектов";
    this._lbProcess.TextAlign = ContentAlignment.MiddleLeft;
    this._dgv.AllowUserToAddRows = false;
    this._dgv.AllowUserToDeleteRows = false;
    this._dgv.AllowUserToResizeRows = false;
    this._dgv.BackgroundColor = SystemColors.Window;
    this._dgv.BorderStyle = BorderStyle.Fixed3D;
    this._dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    this._dgv.ContextMenuStrip = this._cm;
    this._dgv.Dock = DockStyle.Fill;
    this._dgv.Location = new Point(0, 0);
    this._dgv.Name = "_dgv";
    this._dgv.ReadOnly = true;
    this._dgv.RowHeadersVisible = false;
    this._dgv.Size = new Size(599, 178);
    this._dgv.TabIndex = 5;
    this._dgv.CellMouseDown += new DataGridViewCellMouseEventHandler(this.On_dgv_CellMouseDown);
    this._dgv.SelectionChanged += new EventHandler(this.On_dgv_SelectionChanged);
    this._cm.Items.AddRange(new ToolStripItem[3]
    {
      (ToolStripItem) this._miCopyRow,
      (ToolStripItem) this._miCopyCell,
      (ToolStripItem) this._miCopyTable
    });
    this._cm.Name = "_cm";
    this._cm.Size = new Size(188, 70);
    this._cm.Opening += new CancelEventHandler(this.On_cm_Opening);
    this._miCopyRow.Enabled = false;
    this._miCopyRow.Name = "_miCopyRow";
    this._miCopyRow.Size = new Size(187, 22);
    this._miCopyRow.Text = "Копировать";
    this._miCopyRow.ToolTipText = "Копирует всю строку";
    this._miCopyRow.Click += new EventHandler(this.On_miCopyRow_Click);
    this._miCopyCell.Enabled = false;
    this._miCopyCell.Name = "_miCopyCell";
    this._miCopyCell.Size = new Size(187, 22);
    this._miCopyCell.Text = "Копировать текст";
    this._miCopyCell.ToolTipText = "Копирует значение указанной ячейки";
    this._miCopyCell.Click += new EventHandler(this.On_miCopyCell_Click);
    this._miCopyTable.Name = "_miCopyTable";
    this._miCopyTable.Size = new Size(187, 22);
    this._miCopyTable.Text = "Копировать таблицу";
    this._miCopyTable.ToolTipText = "Копирует всю таблицу";
    this._miCopyTable.Click += new EventHandler(this.On_miCopyTable_Click);
    this._rtbReport.BorderStyle = BorderStyle.FixedSingle;
    this._rtbReport.Dock = DockStyle.Fill;
    this._rtbReport.Location = new Point(0, 0);
    this._rtbReport.Name = "_rtbReport";
    this._rtbReport.ReadOnly = true;
    this._rtbReport.Size = new Size(599, 107);
    this._rtbReport.TabIndex = 6;
    this._rtbReport.Text = "";
    this.splitContainer1.Dock = DockStyle.Fill;
    this.splitContainer1.Location = new Point(0, 33);
    this.splitContainer1.Name = "splitContainer1";
    this.splitContainer1.Orientation = Orientation.Horizontal;
    this.splitContainer1.Panel1.Controls.Add((Control) this._dgv);
    this.splitContainer1.Panel2.Controls.Add((Control) this._rtbReport);
    this.splitContainer1.Size = new Size(599, 286);
    this.splitContainer1.SplitterDistance = 178;
    this.splitContainer1.SplitterWidth = 1;
    this.splitContainer1.TabIndex = 7;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.splitContainer1);
    this.Controls.Add((Control) this._pnlTop);
    this.Controls.Add((Control) this._pnlProcess);
    this.Name = nameof (SynchObjectsResultCtrl);
    this.Size = new Size(599, 372);
    this._pnlTop.ResumeLayout(false);
    this._pnlTop.PerformLayout();
    this._pnlProcess.ResumeLayout(false);
    this._pnlProcess.PerformLayout();
    ((ISupportInitialize) this._dgv).EndInit();
    this._cm.ResumeLayout(false);
    this.splitContainer1.Panel1.ResumeLayout(false);
    this.splitContainer1.Panel2.ResumeLayout(false);
    this.splitContainer1.EndInit();
    this.splitContainer1.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
