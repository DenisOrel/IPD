// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.Forms.AutoSelectionRowSelectForm
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.AutoSelection.Client.Common;
using Intermech.Imbase.Controls;
using Intermech.Imbase.Views;
using Intermech.Interfaces;
using Intermech.Protection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AutoSelection.Client.Forms;

public class AutoSelectionRowSelectForm : Form
{
  private long _objectId;
  private AS_ImTableView _tableView;
  private IContainer components;
  private Button btnOK;
  private Button btnCancel;
  private TableLayoutPanel tableLayoutPanel1;
  private Panel pnlMain;
  private Panel pnlTop;
  private Label lblObject;
  private Label lblCaption;
  private Panel pnlTable;

  private void InitData()
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = AutoSelectionProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = AutoSelectionProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(AutoSelectionProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("AutoSelection.Client_75"), (object) LocalizationHolder.rm.GetString("AutoSelection.Client_67"), (object) num));
    AS_ImTableView asImTableView = new AS_ImTableView();
    asImTableView.Parent = (Control) this.pnlTable;
    asImTableView.Dock = DockStyle.Fill;
    this._tableView = asImTableView;
    this._tableView.ItemChecked += new CheckEventHandler(this.tableView_ItemChecked);
    this._tableView.ItemDoubleClick += new EventHandler(this.tableView_ItemDoubleClick);
    this._tableView.Grid.SelectionChanged += new EventHandler(this.Grid_SelectionChanged);
    this._tableView.Grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    this._tableView.Grid.MultiSelect = true;
    CheckedRecords.ActiveChanged += new CheckedRecords.ContextChangedEventHandler(this.checkedRecords_ActiveChanged);
    this.ResumeLayout(false);
    this.UpdateControls();
  }

  protected virtual void UpdateControls()
  {
    List<DataRow> selectedRows = this.SelectedRows;
    this.btnOK.Enabled = selectedRows != null && selectedRows.Count > 0;
  }

  public AutoSelectionRowSelectForm()
  {
    this.InitializeComponent();
    this.InitData();
  }

  public long ObjectID
  {
    get => this._objectId;
    set
    {
      if (this._objectId == value)
        return;
      this._objectId = value;
      if (this._objectId == 0L)
      {
        this.lblCaption.Text = string.Empty;
      }
      else
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(this._objectId);
          this.lblCaption.Text = !objectInfo.Empty ? objectInfo.Caption : string.Empty;
        }
      }
    }
  }

  internal AS_ImTableView TableView => this._tableView;

  public List<DataRow> SelectedRows
  {
    get
    {
      string columnName = -2.ToString();
      long[] array;
      if (CheckedRecords.Active)
      {
        array = this._tableView.CheckedRecords;
      }
      else
      {
        List<long> longList = new List<long>();
        foreach (DataGridViewRow selectedRow in (BaseCollection) this._tableView.Grid.SelectedRows)
        {
          if (selectedRow != null)
          {
            long int64 = Convert.ToInt64(selectedRow.Cells[columnName].Value);
            if (!longList.Contains(int64))
              longList.Add(int64);
          }
        }
        array = longList.ToArray();
      }
      List<DataRow> selectedRows = new List<DataRow>(array != null ? array.Length : 0);
      if (array == null || this._tableView.Table == null)
        return selectedRows;
      foreach (DataRow row in (InternalDataCollectionBase) this._tableView.Table.Rows)
      {
        if (row != null)
        {
          long int64 = Convert.ToInt64(row[columnName]);
          if (Array.IndexOf<long>(array, int64) != -1 && !selectedRows.Contains(row))
            selectedRows.Add(row);
        }
      }
      return selectedRows;
    }
  }

  private void AutoSelectionRowSelectForm_Load(object sender, EventArgs e)
  {
    this._tableView.Grid.MultiSelect = !CheckedRecords.Active;
    AutoSelectionUtils.Forms.LoadSettings((Form) this);
  }

  private void AutoSelectionRowSelectForm_FormClosed(object sender, FormClosedEventArgs e)
  {
    AutoSelectionUtils.Forms.SaveSettings((Form) this);
  }

  private void tableView_ItemChecked(object sender, Intermech.Imbase.Controls.TableView.CheckEventArgs ce)
  {
    if (this._tableView?.Grid?.CurrentRow == null)
      return;
    this._tableView.CheckRecord(this._tableView.Grid.CurrentRow.Index, !ce.Checked);
    this.UpdateControls();
  }

  private void tableView_ItemDoubleClick(object sender, EventArgs e)
  {
    if (!CheckedRecords.Active || !(e is DataGridViewCellEventArgs viewCellEventArgs))
      return;
    this.TableView.CheckRecord(viewCellEventArgs.RowIndex, !this.TableView.CheckedRecord(viewCellEventArgs.RowIndex));
    this.UpdateControls();
  }

  private void Grid_SelectionChanged(object sender, EventArgs e)
  {
    if (CheckedRecords.Active)
      return;
    this.UpdateControls();
  }

  private void checkedRecords_ActiveChanged() => this.UpdateControls();

  private void AutoSelectionRowSelectForm_FormClosing(object sender, FormClosingEventArgs e)
  {
    this.TableView.Detach();
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AutoSelectionRowSelectForm));
    this.btnOK = new Button();
    this.btnCancel = new Button();
    this.tableLayoutPanel1 = new TableLayoutPanel();
    this.pnlMain = new Panel();
    this.pnlTable = new Panel();
    this.pnlTop = new Panel();
    this.lblObject = new Label();
    this.lblCaption = new Label();
    this.tableLayoutPanel1.SuspendLayout();
    this.pnlMain.SuspendLayout();
    this.pnlTop.SuspendLayout();
    this.SuspendLayout();
    this.btnOK.DialogResult = DialogResult.OK;
    componentResourceManager.ApplyResources((object) this.btnOK, "btnOK");
    this.btnOK.Name = "btnOK";
    this.btnOK.UseVisualStyleBackColor = true;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.tableLayoutPanel1, "tableLayoutPanel1");
    this.tableLayoutPanel1.Controls.Add((Control) this.btnCancel, 2, 1);
    this.tableLayoutPanel1.Controls.Add((Control) this.btnOK, 1, 1);
    this.tableLayoutPanel1.Controls.Add((Control) this.pnlMain, 0, 0);
    this.tableLayoutPanel1.Name = "tableLayoutPanel1";
    this.tableLayoutPanel1.SetColumnSpan((Control) this.pnlMain, 13);
    this.pnlMain.Controls.Add((Control) this.pnlTable);
    this.pnlMain.Controls.Add((Control) this.pnlTop);
    componentResourceManager.ApplyResources((object) this.pnlMain, "pnlMain");
    this.pnlMain.Name = "pnlMain";
    componentResourceManager.ApplyResources((object) this.pnlTable, "pnlTable");
    this.pnlTable.Name = "pnlTable";
    this.pnlTop.BackColor = SystemColors.Control;
    this.pnlTop.Controls.Add((Control) this.lblObject);
    this.pnlTop.Controls.Add((Control) this.lblCaption);
    componentResourceManager.ApplyResources((object) this.pnlTop, "pnlTop");
    this.pnlTop.Name = "pnlTop";
    componentResourceManager.ApplyResources((object) this.lblObject, "lblObject");
    this.lblObject.Name = "lblObject";
    componentResourceManager.ApplyResources((object) this.lblCaption, "lblCaption");
    this.lblCaption.ForeColor = SystemColors.ControlText;
    this.lblCaption.Name = "lblCaption";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.tableLayoutPanel1);
    this.Name = nameof (AutoSelectionRowSelectForm);
    this.FormClosing += new FormClosingEventHandler(this.AutoSelectionRowSelectForm_FormClosing);
    this.FormClosed += new FormClosedEventHandler(this.AutoSelectionRowSelectForm_FormClosed);
    this.Load += new EventHandler(this.AutoSelectionRowSelectForm_Load);
    this.tableLayoutPanel1.ResumeLayout(false);
    this.pnlMain.ResumeLayout(false);
    this.pnlTop.ResumeLayout(false);
    this.pnlTop.PerformLayout();
    this.ResumeLayout(false);
  }
}
