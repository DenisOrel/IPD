// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.ColumnsNamesEditorForm
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.FormDesigner.Wrappers;

/// <summary>Редактор для переименования колонок.</summary>
public class ColumnsNamesEditorForm : Form
{
  private NodeColumnCollection _columns;
  private Dictionary<Guid, string> _columnsAliases;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button _btnApply;
  private Button _btnCancel;
  private DataGridView _dgv;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
  private Button _btnClear;
  private DataGridViewTextBoxColumn _colGuid;
  private DataGridViewTextBoxColumn _colName;
  private DataGridViewTextBoxColumn _colAlias;

  /// <summary>Набор колонок.</summary>
  internal NodeColumnCollection Columns
  {
    get => this._columns;
    set => this._columns = value;
  }

  /// <summary>Список переименованных колонок.</summary>
  internal Dictionary<Guid, string> ColumnsAliases
  {
    get => this._columnsAliases;
    set => this._columnsAliases = value;
  }

  /// <summary>Конструктор.</summary>
  public ColumnsNamesEditorForm() => this.InitializeComponent();

  /// <summary>Очистить псевдонимы.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_btnClear_Click(object sender, EventArgs e)
  {
    if (this._dgv.Rows.Count <= 0)
      return;
    string caption = LocalizationHolder.rm.GetString("FormDesigner_ClearColumnsAliases_DialogCaption");
    if (MessageBox.Show(LocalizationHolder.rm.GetString("FormDesigner_ClearColumnsAliases_DialogMsg"), caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
      return;
    foreach (DataGridViewRow row in (IEnumerable) this._dgv.Rows)
      row.Cells["_colAlias"].Value = (object) string.Empty;
  }

  /// <summary>Закрытие формы.</summary>
  /// <param name="e"></param>
  protected override void OnClosing(CancelEventArgs e)
  {
    base.OnClosing(e);
    if (this.DialogResult != DialogResult.OK)
      return;
    this._columnsAliases.Clear();
    foreach (DataGridViewRow row in (IEnumerable) this._dgv.Rows)
    {
      object obj1 = row.Cells["_colGuid"].Value;
      if (obj1 != null)
      {
        string str1 = obj1.ToString();
        if (!string.IsNullOrEmpty(str1) && GuidHelper.IsGuid(str1))
        {
          object obj2 = row.Cells["_colAlias"].Value;
          if (obj2 != null)
          {
            string str2 = obj2.ToString();
            if (!string.IsNullOrEmpty(str2))
              this._columnsAliases.Add(new Guid(str1), str2);
          }
        }
      }
    }
  }

  /// <summary>Загрузка формы.</summary>
  /// <param name="e"></param>
  protected override void OnLoad(EventArgs e)
  {
    base.OnLoad(e);
    if (this._columns != null && this._columns.Count > 0)
    {
      this._columnsAliases = this._columnsAliases ?? new Dictionary<Guid, string>(0);
      foreach (NodeColumn column in (List<NodeColumn>) this._columns)
      {
        Guid attributeGuid = column.Attribute.AttributeGuid;
        DataGridViewRow dataGridViewRow = new DataGridViewRow();
        dataGridViewRow.CreateCells(this._dgv);
        dataGridViewRow.Cells[0].Value = (object) attributeGuid;
        dataGridViewRow.Cells[1].Value = (object) column.Attribute.Name;
        if (this._columnsAliases.ContainsKey(attributeGuid))
          dataGridViewRow.Cells[2].Value = (object) this._columnsAliases[attributeGuid];
        this._dgv.Rows.Add(dataGridViewRow);
      }
    }
    else
      this._btnApply.Enabled = this._btnClear.Enabled = false;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ColumnsNamesEditorForm));
    this._btnApply = new Button();
    this._btnCancel = new Button();
    this._dgv = new DataGridView();
    this._colGuid = new DataGridViewTextBoxColumn();
    this._colName = new DataGridViewTextBoxColumn();
    this._colAlias = new DataGridViewTextBoxColumn();
    this._btnClear = new Button();
    this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
    this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
    this.dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
    ((ISupportInitialize) this._dgv).BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._btnApply, "_btnApply");
    this._btnApply.DialogResult = DialogResult.OK;
    this._btnApply.Name = "_btnApply";
    this._btnApply.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._btnCancel, "_btnCancel");
    this._btnCancel.DialogResult = DialogResult.Cancel;
    this._btnCancel.Name = "_btnCancel";
    this._btnCancel.UseVisualStyleBackColor = true;
    this._dgv.AllowUserToAddRows = false;
    this._dgv.AllowUserToDeleteRows = false;
    this._dgv.AllowUserToOrderColumns = true;
    componentResourceManager.ApplyResources((object) this._dgv, "_dgv");
    this._dgv.BackgroundColor = SystemColors.Window;
    this._dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    this._dgv.Columns.AddRange((DataGridViewColumn) this._colGuid, (DataGridViewColumn) this._colName, (DataGridViewColumn) this._colAlias);
    this._dgv.Name = "_dgv";
    this._dgv.RowHeadersVisible = false;
    this._dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    componentResourceManager.ApplyResources((object) this._colGuid, "_colGuid");
    this._colGuid.Name = "_colGuid";
    this._colGuid.SortMode = DataGridViewColumnSortMode.NotSortable;
    componentResourceManager.ApplyResources((object) this._colName, "_colName");
    this._colName.Name = "_colName";
    this._colName.ReadOnly = true;
    this._colAlias.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    componentResourceManager.ApplyResources((object) this._colAlias, "_colAlias");
    this._colAlias.Name = "_colAlias";
    componentResourceManager.ApplyResources((object) this._btnClear, "_btnClear");
    this._btnClear.Name = "_btnClear";
    this._btnClear.UseVisualStyleBackColor = true;
    this._btnClear.Click += new EventHandler(this.On_btnClear_Click);
    componentResourceManager.ApplyResources((object) this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
    this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
    this.dataGridViewTextBoxColumn1.SortMode = DataGridViewColumnSortMode.NotSortable;
    componentResourceManager.ApplyResources((object) this.dataGridViewTextBoxColumn2, "dataGridViewTextBoxColumn2");
    this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
    this.dataGridViewTextBoxColumn2.ReadOnly = true;
    this.dataGridViewTextBoxColumn3.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    componentResourceManager.ApplyResources((object) this.dataGridViewTextBoxColumn3, "dataGridViewTextBoxColumn3");
    this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
    this.AcceptButton = (IButtonControl) this._btnApply;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this._btnCancel;
    this.Controls.Add((Control) this._btnClear);
    this.Controls.Add((Control) this._dgv);
    this.Controls.Add((Control) this._btnCancel);
    this.Controls.Add((Control) this._btnApply);
    this.DoubleBuffered = true;
    this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ColumnsNamesEditorForm);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Show;
    ((ISupportInitialize) this._dgv).EndInit();
    this.ResumeLayout(false);
  }
}
