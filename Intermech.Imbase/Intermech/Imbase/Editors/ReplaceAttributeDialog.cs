// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Editors.ReplaceAttributeDialog
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Editors;

public class ReplaceAttributeDialog : Form
{
  private DataTable _dtOldValues;
  private string _sourceColName = string.Empty;
  private string _targetColName = string.Empty;
  private FieldTypes _targetType;
  private bool _hasError;
  private int _ErrorsCount;
  private string _ErrorText = string.Empty;
  private DataTable _dtCmbValues;
  private bool _DisableDBNull = true;
  private IContainer components;
  private DataGridView dgvValues;
  private Button btnOK;
  private Button btnCancel;
  private DataSet dsValues;
  private DataTable dtValues;
  private DataColumn colError;
  private DataColumn colOldValues;
  private DataColumn colNewValues;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
  private DataGridViewComboBoxColumn dataGridViewComboBoxColumn1;
  private DataGridViewCheckBoxColumn dgvColError;
  private DataGridViewTextBoxColumn dgvColOldValues;

  public ReplaceAttributeDialog(
    DataTable dt,
    string sourceColName,
    string targetColName,
    FieldTypes targetType)
  {
    this.InitializeComponent();
    this._dtOldValues = dt;
    this._sourceColName = sourceColName;
    this._targetColName = targetColName;
    this._targetType = targetType;
  }

  private void ChangeDataTable()
  {
    this._dtOldValues.Columns.Add(this._targetType == FieldTypes.ftMeasured ? new DataColumn(this._targetColName, typeof (double)) : new DataColumn(this._targetColName, this.GetSystemType(this._targetType)));
    for (int index = 0; index < this.dtValues.Rows.Count; ++index)
      this._dtOldValues.Rows[index][this._targetColName] = this.dtValues.Rows[index]["colNewValues"];
    this._dtOldValues.Columns.Remove(this._sourceColName);
  }

  private System.Type GetSystemType(FieldTypes fldType)
  {
    switch (fldType)
    {
      case FieldTypes.ftString:
        return typeof (string);
      case FieldTypes.ftInteger:
        return typeof (long);
      case FieldTypes.ftDouble:
        return typeof (double);
      case FieldTypes.ftDateTime:
        return typeof (DateTime);
      case FieldTypes.ftBoolean:
        return typeof (bool);
      case FieldTypes.ftMeasured:
        return typeof (MeasuredValue);
      default:
        return typeof (object);
    }
  }

  private void OndgvValues_CellEndEdit(object sender, DataGridViewCellEventArgs e)
  {
    object obj = this.dgvValues.Rows[e.RowIndex].Cells["dgvColNewValues"].Value;
    object parsedValue = (object) null;
    if (this.ParseValue(obj, out parsedValue))
    {
      this.dgvValues.Rows[e.RowIndex].Cells["dgvColNewValues"].Value = parsedValue;
      if (!Convert.ToBoolean(this.dgvValues.Rows[e.RowIndex].Cells["dgvColError"].Value))
        return;
      this.dgvValues.Rows[e.RowIndex].Cells["dgvColNewValues"].ErrorText = string.Empty;
      this.dgvValues.Rows[e.RowIndex].Cells["dgvColError"].Value = (object) false;
      if (--this._ErrorsCount != 0)
        return;
      this.btnOK.Enabled = true;
    }
    else
    {
      if (Convert.ToBoolean(this.dgvValues.Rows[e.RowIndex].Cells["dgvColError"].Value))
        return;
      this.dgvValues.Rows[e.RowIndex].Cells["dgvColNewValues"].ErrorText = this._ErrorText;
      this.dgvValues.Rows[e.RowIndex].Cells["dgvColError"].Value = (object) true;
      ++this._ErrorsCount;
      this.btnOK.Enabled = false;
    }
  }

  private bool ParseValue(object value, out object parsedValue)
  {
    parsedValue = (object) null;
    if (value == DBNull.Value)
      return !this._DisableDBNull;
    switch (this._targetType)
    {
      case FieldTypes.ftString:
        parsedValue = (object) value.ToString();
        return true;
      case FieldTypes.ftInteger:
        long result1 = 0;
        if (!long.TryParse(value.ToString(), out result1))
          return false;
        parsedValue = (object) result1;
        break;
      case FieldTypes.ftDouble:
      case FieldTypes.ftMeasured:
        double result2 = 0.0;
        if (!double.TryParse(value.ToString(), out result2))
          return false;
        parsedValue = (object) result2;
        break;
      case FieldTypes.ftDateTime:
        DateTime result3 = DateTime.MinValue;
        if (!DateTime.TryParse(value.ToString(), out result3))
          return false;
        parsedValue = (object) result3;
        break;
      case FieldTypes.ftBoolean:
        bool result4 = false;
        if (!bool.TryParse(value.ToString(), out result4))
          return false;
        parsedValue = (object) result4;
        break;
    }
    return true;
  }

  protected override void OnClosing(CancelEventArgs e)
  {
    if (this.DialogResult == DialogResult.OK)
      this.ChangeDataTable();
    base.OnClosing(e);
  }

  protected override void OnLoad(EventArgs e)
  {
    base.OnLoad(e);
    this._ErrorText = LocalizationHolder.rm.GetString("Imbase_ReplaceAttributeDialog_TypeConvertErrorMessage");
    foreach (DataGridViewRow row in (IEnumerable) this.dgvValues.Rows)
    {
      if (Convert.ToBoolean(row.Cells["dgvColError"].Value))
      {
        row.Cells["dgvColNewValues"].ErrorText = this._ErrorText;
        ++this._ErrorsCount;
      }
    }
  }

  public bool ValidatingValues()
  {
    if (!this._dtOldValues.Columns.Contains(this._targetColName))
      return true;
    bool flag1 = false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(new Guid(this._targetColName));
      if (attributeType != null)
        this._DisableDBNull = (attributeType.Options & AttributeOptions.DisableNulls) != 0;
      DataGridViewColumn dataGridViewColumn;
      if (attributeType != null && (attributeType.MultipleValued == MultiValueModes.SingleValueFromList || attributeType.MultipleValued == MultiValueModes.MultiValuesFromList))
      {
        flag1 = true;
        DataGridViewComboBoxColumn viewComboBoxColumn = new DataGridViewComboBoxColumn();
        viewComboBoxColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
        this._dtCmbValues = attributeType.GetPossibleValues();
        DataRow[] dataRowArray = this._dtCmbValues.Select($"{"F_DESCRIPTION"}='{DBNull.Value}'");
        if (this._DisableDBNull)
        {
          if (dataRowArray.Length != 0)
          {
            int num = 0;
            while (num < dataRowArray.Length)
              this._dtCmbValues.Rows.Remove(dataRowArray[num++]);
          }
        }
        else if (dataRowArray.Length == 0)
        {
          DataTable toTable = this._dtCmbValues.Clone();
          toTable.Rows.Add(toTable.NewRow());
          DataSetProcessor.AssignRows(toTable, (IEnumerable<DataRow>) this._dtCmbValues.Select());
          this._dtCmbValues = toTable;
        }
        viewComboBoxColumn.DataSource = (object) this._dtCmbValues;
        viewComboBoxColumn.ValueMember = this._dtCmbValues.Columns[attributeType.TextFieldName].ColumnName;
        viewComboBoxColumn.DisplayMember = this._dtCmbValues.Columns["F_DESCRIPTION"].ColumnName;
        dataGridViewColumn = (DataGridViewColumn) viewComboBoxColumn;
      }
      else if (this._targetType == FieldTypes.ftBoolean)
      {
        DataGridViewComboBoxColumn viewComboBoxColumn = new DataGridViewComboBoxColumn();
        viewComboBoxColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
        this._dtCmbValues = new DataTable();
        this._dtCmbValues.Columns.Add(new DataColumn("Keys", typeof (bool)));
        this._dtCmbValues.Columns.Add(new DataColumn("Values", typeof (string)));
        if (!this._DisableDBNull)
          this._dtCmbValues.Rows.Add((object) DBNull.Value, (object) string.Empty);
        this._dtCmbValues.Rows.Add((object) true, (object) LocalizationHolder.rm.GetString("Imbase.Table.AttributeRedactor.BoolConverter.True"));
        this._dtCmbValues.Rows.Add((object) false, (object) LocalizationHolder.rm.GetString("Imbase.Table.AttributeRedactor.BoolConverter.False"));
        viewComboBoxColumn.DataSource = (object) this._dtCmbValues;
        viewComboBoxColumn.DisplayMember = "Values";
        viewComboBoxColumn.ValueMember = "Keys";
        dataGridViewColumn = (DataGridViewColumn) viewComboBoxColumn;
      }
      else if (this._targetType == FieldTypes.ftDateTime)
      {
        dataGridViewColumn = (DataGridViewColumn) new DataGridViewCalendarColumn();
      }
      else
      {
        dataGridViewColumn = (DataGridViewColumn) new DataGridViewTextBoxColumn();
        dataGridViewColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
      }
      dataGridViewColumn.Name = "dgvColNewValues";
      dataGridViewColumn.HeaderText = LocalizationHolder.rm.GetString("Imbase_ReplaceAttributeDialog_ColumnName_NewValues");
      dataGridViewColumn.DataPropertyName = "colNewValues";
      dataGridViewColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dgvValues.Columns.Add(dataGridViewColumn);
    }
    foreach (DataRow row in (InternalDataCollectionBase) this._dtOldValues.Rows)
    {
      object obj = row[this._sourceColName];
      object parsedValue = (object) null;
      bool flag2 = this.ParseValue(obj, out parsedValue);
      if (flag1 & flag2 && this._dtCmbValues.Select($"{this._dtCmbValues.Columns["F_DESCRIPTION"].ColumnName}='{parsedValue}'").Length == 0)
      {
        flag2 = false;
        parsedValue = (object) DBNull.Value;
      }
      this.dtValues.Rows.Add((object) !flag2, obj, parsedValue);
      if (!flag2)
        this._hasError = true;
    }
    if (!this._hasError)
      this.ChangeDataTable();
    return !this._hasError;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ReplaceAttributeDialog));
    this.dgvValues = new DataGridView();
    this.dgvColError = new DataGridViewCheckBoxColumn();
    this.dgvColOldValues = new DataGridViewTextBoxColumn();
    this.dsValues = new DataSet();
    this.dtValues = new DataTable();
    this.colError = new DataColumn();
    this.colOldValues = new DataColumn();
    this.colNewValues = new DataColumn();
    this.btnOK = new Button();
    this.btnCancel = new Button();
    this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
    this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
    this.dataGridViewComboBoxColumn1 = new DataGridViewComboBoxColumn();
    ((ISupportInitialize) this.dgvValues).BeginInit();
    this.dsValues.BeginInit();
    this.dtValues.BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.dgvValues, "dgvValues");
    this.dgvValues.AllowUserToAddRows = false;
    this.dgvValues.AllowUserToDeleteRows = false;
    this.dgvValues.AllowUserToResizeRows = false;
    this.dgvValues.AutoGenerateColumns = false;
    this.dgvValues.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    this.dgvValues.Columns.AddRange((DataGridViewColumn) this.dgvColError, (DataGridViewColumn) this.dgvColOldValues);
    this.dgvValues.DataMember = "dtValues";
    this.dgvValues.DataSource = (object) this.dsValues;
    this.dgvValues.MultiSelect = false;
    this.dgvValues.Name = "dgvValues";
    this.dgvValues.RowHeadersVisible = false;
    this.dgvValues.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    this.dgvValues.ShowCellToolTips = false;
    this.dgvValues.CellEndEdit += new DataGridViewCellEventHandler(this.OndgvValues_CellEndEdit);
    this.dgvColError.DataPropertyName = "colError";
    componentResourceManager.ApplyResources((object) this.dgvColError, "dgvColError");
    this.dgvColError.Name = "dgvColError";
    this.dgvColOldValues.DataPropertyName = "colOldValues";
    componentResourceManager.ApplyResources((object) this.dgvColOldValues, "dgvColOldValues");
    this.dgvColOldValues.Name = "dgvColOldValues";
    this.dgvColOldValues.ReadOnly = true;
    this.dgvColOldValues.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.dsValues.DataSetName = "dsValues";
    this.dsValues.Tables.AddRange(new DataTable[1]
    {
      this.dtValues
    });
    this.dtValues.Columns.AddRange(new DataColumn[3]
    {
      this.colError,
      this.colOldValues,
      this.colNewValues
    });
    this.dtValues.TableName = "dtValues";
    this.colError.AllowDBNull = false;
    this.colError.Caption = "Ошибка";
    this.colError.ColumnName = "colError";
    this.colError.DataType = typeof (bool);
    this.colError.DefaultValue = (object) false;
    this.colOldValues.Caption = "Старые значения";
    this.colOldValues.ColumnName = "colOldValues";
    this.colOldValues.DataType = typeof (object);
    this.colNewValues.Caption = "Новые значения";
    this.colNewValues.ColumnName = "colNewValues";
    this.colNewValues.DataType = typeof (object);
    componentResourceManager.ApplyResources((object) this.btnOK, "btnOK");
    this.btnOK.DialogResult = DialogResult.OK;
    this.btnOK.Name = "btnOK";
    this.btnOK.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.dataGridViewTextBoxColumn1.DataPropertyName = "colOldValues";
    componentResourceManager.ApplyResources((object) this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
    this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
    this.dataGridViewTextBoxColumn1.ReadOnly = true;
    this.dataGridViewTextBoxColumn1.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.dataGridViewTextBoxColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    this.dataGridViewTextBoxColumn2.DataPropertyName = "colNewValues";
    componentResourceManager.ApplyResources((object) this.dataGridViewTextBoxColumn2, "dataGridViewTextBoxColumn2");
    this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
    this.dataGridViewTextBoxColumn2.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.dataGridViewComboBoxColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    this.dataGridViewComboBoxColumn1.DataPropertyName = "colNewValues";
    this.dataGridViewComboBoxColumn1.DataSource = (object) this.dsValues;
    componentResourceManager.ApplyResources((object) this.dataGridViewComboBoxColumn1, "dataGridViewComboBoxColumn1");
    this.dataGridViewComboBoxColumn1.Name = "dataGridViewComboBoxColumn1";
    this.dataGridViewComboBoxColumn1.ValueMember = "dtValues.colNewValues";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.btnOK);
    this.Controls.Add((Control) this.dgvValues);
    this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ReplaceAttributeDialog);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    ((ISupportInitialize) this.dgvValues).EndInit();
    this.dsValues.EndInit();
    this.dtValues.EndInit();
    this.ResumeLayout(false);
  }
}
