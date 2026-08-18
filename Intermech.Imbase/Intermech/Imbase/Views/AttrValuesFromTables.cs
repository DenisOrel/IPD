// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Views.AttrValuesFromTables
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Views;

public class AttrValuesFromTables : Form
{
  private IContainer components;
  private Button _btnOK;
  private Button _btnCancel;
  private Panel _pnlBottom;
  private DataGridView _dgv;

  public List<string> SelectedValues
  {
    get
    {
      List<string> selectedValues = (List<string>) null;
      if (this._dgv.SelectedRows.Count > 0)
      {
        selectedValues = new List<string>(this._dgv.SelectedRows.Count);
        foreach (DataGridViewRow selectedRow in (BaseCollection) this._dgv.SelectedRows)
          selectedValues.Add(Convert.ToString(selectedRow.Cells[1].Value));
      }
      return selectedValues;
    }
  }

  public AttrValuesFromTables(DataTable dt)
  {
    this.InitializeComponent();
    this.LoadSource(dt);
  }

  private void On_dgv_DoubleClick(object sender, EventArgs e)
  {
    if (this._dgv.SelectedRows.Count <= 0)
      return;
    this.DialogResult = DialogResult.OK;
    this.Close();
  }

  private void LoadSource(DataTable dt)
  {
    if (dt == null)
      return;
    this._dgv.DataSource = (object) dt;
    this._dgv.Columns[1].Visible = false;
    this._btnOK.Enabled = true;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AttrValuesFromTables));
    this._btnOK = new Button();
    this._btnCancel = new Button();
    this._pnlBottom = new Panel();
    this._dgv = new DataGridView();
    this._pnlBottom.SuspendLayout();
    ((ISupportInitialize) this._dgv).BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._btnOK, "_btnOK");
    this._btnOK.DialogResult = DialogResult.OK;
    this._btnOK.Name = "_btnOK";
    this._btnOK.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._btnCancel, "_btnCancel");
    this._btnCancel.DialogResult = DialogResult.Cancel;
    this._btnCancel.Name = "_btnCancel";
    this._btnCancel.UseVisualStyleBackColor = true;
    this._pnlBottom.Controls.Add((Control) this._btnCancel);
    this._pnlBottom.Controls.Add((Control) this._btnOK);
    componentResourceManager.ApplyResources((object) this._pnlBottom, "_pnlBottom");
    this._pnlBottom.Name = "_pnlBottom";
    this._dgv.AllowUserToAddRows = false;
    this._dgv.AllowUserToDeleteRows = false;
    this._dgv.AllowUserToResizeColumns = false;
    this._dgv.AllowUserToResizeRows = false;
    this._dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
    this._dgv.BackgroundColor = SystemColors.Window;
    this._dgv.CellBorderStyle = DataGridViewCellBorderStyle.None;
    this._dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    this._dgv.ColumnHeadersVisible = false;
    componentResourceManager.ApplyResources((object) this._dgv, "_dgv");
    this._dgv.Name = "_dgv";
    this._dgv.ReadOnly = true;
    this._dgv.RowHeadersVisible = false;
    this._dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    this._dgv.DoubleClick += new EventHandler(this.On_dgv_DoubleClick);
    this.AcceptButton = (IButtonControl) this._btnOK;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this._btnCancel;
    this.Controls.Add((Control) this._dgv);
    this.Controls.Add((Control) this._pnlBottom);
    this.DoubleBuffered = true;
    this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
    this.Name = nameof (AttrValuesFromTables);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this._pnlBottom.ResumeLayout(false);
    ((ISupportInitialize) this._dgv).EndInit();
    this.ResumeLayout(false);
  }
}
