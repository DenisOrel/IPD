// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.SelectSectionForm
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS;

public class SelectSectionForm : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button _btnCancel;
  private Button _btnOK;
  private DataSet _dataSet;
  private DataGridView dgAllItems;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

  /// <summary> Конструктор </summary>
  /// <param name="exceptSectionWithIDs"> Список идентификатор разделов, которые не надо показывать </param>
  public SelectSectionForm(List<SpecificationSectionInfo> sections)
  {
    this.InitializeComponent();
    sections.Sort();
    foreach (SpecificationSectionInfo section in sections)
      this.dgAllItems.Rows[this.dgAllItems.Rows.Add(new object[1]
      {
        (object) section.Caption
      })].Tag = (object) section;
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 3288);
  }

  public bool Multiselect
  {
    get => this.dgAllItems.MultiSelect;
    set => this.dgAllItems.MultiSelect = value;
  }

  /// <summary> Обновление возможности закрыть диалог </summary>
  private void UpdateEnabled() => this._btnOK.Enabled = this.dgAllItems.SelectedRows.Count > 0;

  /// <summary> Получить список идентификаторов выбранных разделов </summary>
  public List<long> GetSelectedSectionIDs()
  {
    List<long> selectedSectionIds = new List<long>();
    foreach (DataGridViewBand selectedRow in (BaseCollection) this.dgAllItems.SelectedRows)
    {
      SpecificationSectionInfo tag = selectedRow.Tag as SpecificationSectionInfo;
      selectedSectionIds.Add(tag.SectionID);
    }
    return selectedSectionIds;
  }

  private void SelectSectionForm_FormClosing(object sender, FormClosingEventArgs e)
  {
    if (this.DialogResult != DialogResult.OK)
      return;
    this.UpdateEnabled();
    if (this._btnOK.Enabled)
      return;
    e.Cancel = true;
  }

  private void _gridEnterancesToLink_DoubleClick(object sender, EventArgs e)
  {
    this.DialogResult = DialogResult.OK;
    this.Close();
    this.DialogResult = DialogResult.OK;
  }

  private void dgAllItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
  {
    this.DialogResult = DialogResult.OK;
    this.Close();
    this.DialogResult = DialogResult.OK;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SelectSectionForm));
    this._btnCancel = new Button();
    this._btnOK = new Button();
    this._dataSet = new DataSet();
    this.dgAllItems = new DataGridView();
    this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
    this._dataSet.BeginInit();
    ((ISupportInitialize) this.dgAllItems).BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._btnCancel, "_btnCancel");
    this._btnCancel.DialogResult = DialogResult.Cancel;
    this._btnCancel.Name = "_btnCancel";
    this._btnCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._btnOK, "_btnOK");
    this._btnOK.DialogResult = DialogResult.OK;
    this._btnOK.Name = "_btnOK";
    this._btnOK.UseVisualStyleBackColor = true;
    this._dataSet.DataSetName = "NewDataSet";
    this.dgAllItems.AllowDrop = true;
    this.dgAllItems.AllowUserToAddRows = false;
    this.dgAllItems.AllowUserToDeleteRows = false;
    this.dgAllItems.AllowUserToResizeColumns = false;
    this.dgAllItems.AllowUserToResizeRows = false;
    componentResourceManager.ApplyResources((object) this.dgAllItems, "dgAllItems");
    this.dgAllItems.BackgroundColor = Color.White;
    this.dgAllItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    this.dgAllItems.ColumnHeadersVisible = false;
    this.dgAllItems.Columns.AddRange((DataGridViewColumn) this.dataGridViewTextBoxColumn1);
    this.dgAllItems.MultiSelect = false;
    this.dgAllItems.Name = "dgAllItems";
    this.dgAllItems.ReadOnly = true;
    this.dgAllItems.RowHeadersVisible = false;
    this.dgAllItems.RowTemplate.Height = 20;
    this.dgAllItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    this.dgAllItems.CellDoubleClick += new DataGridViewCellEventHandler(this.dgAllItems_CellDoubleClick);
    this.dataGridViewTextBoxColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    componentResourceManager.ApplyResources((object) this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
    this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
    this.dataGridViewTextBoxColumn1.ReadOnly = true;
    this.AcceptButton = (IButtonControl) this._btnOK;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this._btnCancel;
    this.Controls.Add((Control) this.dgAllItems);
    this.Controls.Add((Control) this._btnOK);
    this.Controls.Add((Control) this._btnCancel);
    this.HelpButton = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (SelectSectionForm);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.FormClosing += new FormClosingEventHandler(this.SelectSectionForm_FormClosing);
    this._dataSet.EndInit();
    ((ISupportInitialize) this.dgAllItems).EndInit();
    this.ResumeLayout(false);
  }
}
