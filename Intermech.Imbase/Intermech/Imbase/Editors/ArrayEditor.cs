// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Editors.ArrayEditor
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Imbase.Selection;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.PropertyEditors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Editors;

public class ArrayEditor : Form
{
  private static Size _size;
  private AttributeTypeProperties _attProps;
  private DataColumn _dc;
  private DataTable _dt;
  private TableEditor _editor;
  private bool _isRecordRef;
  private bool _isObjectRef;
  private bool _askDeleteRow = true;
  private Dictionary<string, string> _recordRefMap;
  private Dictionary<string, string> _objectRefMap;
  private IContainer components;
  private DataGridView _grid;
  private Button cancelButton;
  private Button okButton;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
  private Button btNewRecord;

  public ArrayEditor()
  {
    this.InitializeComponent();
    this._grid.AutoGenerateColumns = false;
    if (ArrayEditor._size.IsEmpty)
      return;
    this.Size = ArrayEditor._size;
  }

  internal bool EditArray(
    ref object value,
    System.Type elementType,
    AttributeTypeProperties attProps,
    PropertyCollection props,
    string caption,
    TableEditor editor)
  {
    this._attProps = attProps;
    this._editor = editor;
    this._objectRefMap = editor._objectRefMap;
    this._recordRefMap = editor._recordRefMap;
    DataTable dataTable = new DataTable();
    this._dt = dataTable;
    this._dc = new DataColumn("F1", elementType);
    this._dc.DefaultValue = attProps.DefaultValue;
    this._dc.Caption = caption;
    if (props.Keys.Count > 0)
    {
      foreach (object key in (IEnumerable) props.Keys)
        this._dc.ExtendedProperties.Add(key, props[key]);
    }
    dataTable.Columns.Add(this._dc);
    if (value is ValuesArray valuesArray)
    {
      int length = valuesArray.Length;
      for (int index = 0; index < length; ++index)
        dataTable.Rows.Add(valuesArray.GetValue(index));
    }
    this._grid.DataSource = (object) dataTable;
    DialogResult dialogResult = this.ShowDialog();
    if (dialogResult == DialogResult.OK)
    {
      DataRow[] dataRowArray = dataTable.Select();
      int length = dataRowArray.Length;
      if (length != 0)
      {
        Array instance = Array.CreateInstance(typeof (object), length);
        for (int index = 0; index < length; ++index)
        {
          object obj = dataRowArray[index][0];
          instance.SetValue(obj, index);
        }
        value = (object) new ValuesArray(instance, elementType);
      }
      else
        value = (object) DBNull.Value;
    }
    return dialogResult == DialogResult.OK;
  }

  private void NameObjectRefs()
  {
    int count = this._dt.Rows.Count;
    if (count <= 0)
      return;
    List<string> state = new List<string>(count);
    for (int index = 0; index < count; ++index)
    {
      string key = this._dt.Rows[index][0].ToString();
      if (!string.IsNullOrEmpty(key) && !this._objectRefMap.ContainsKey(key) && !state.Contains(key))
        state.Add(key);
    }
    if (state.Count <= 0)
      return;
    state.Sort();
    ThreadPool.QueueUserWorkItem(new WaitCallback(this._editor.ObjectThreadProc), (object) state);
  }

  private void NameRecordRefs()
  {
    int count = this._dt.Rows.Count;
    if (count <= 0)
      return;
    List<string> state = new List<string>(count);
    for (int index = 0; index < count; ++index)
    {
      string key = this._dt.Rows[index][0].ToString();
      if (!string.IsNullOrEmpty(key) && !this._recordRefMap.ContainsKey(key) && !state.Contains(key))
        state.Add(key);
    }
    if (state.Count <= 0)
      return;
    state.Sort();
    ThreadPool.QueueUserWorkItem(new WaitCallback(this._editor.RecordThreadProc), (object) state);
  }

  private void AddColumn()
  {
    DataGridViewColumn dataGridViewColumn = (DataGridViewColumn) null;
    bool flag = true;
    $"{this._grid.Handle}";
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(this._attProps.AttributeID);
      if (attributeType.MultipleValued == MultiValueModes.MultiValuesFromList)
      {
        DataTable possibleValues = attributeType.GetPossibleValues();
        if ((attributeType.Options & AttributeOptions.DisableNulls) == AttributeOptions.None)
        {
          DataRow row = possibleValues.NewRow();
          row[0] = row[1] = (object) DBNull.Value;
          row[2] = (object) " ";
          possibleValues.Rows.InsertAt(row, 0);
        }
        foreach (DataRow row in (InternalDataCollectionBase) possibleValues.Rows)
        {
          if (row[2] == DBNull.Value || string.IsNullOrEmpty(row[2].ToString()))
            row[2] = row[1];
        }
        if (possibleValues.Columns[attributeType.TextFieldName].DataType == typeof (Decimal))
        {
          possibleValues.Columns[attributeType.TextFieldName].ColumnName = "F_DECIMAL_VALUE";
          if (attributeType.TextFieldName == "F_INTEGER_VALUE")
            possibleValues.Columns.Add(attributeType.TextFieldName, typeof (long)).Expression = "F_DECIMAL_VALUE";
          else if (attributeType.TextFieldName == "F_DOUBLE_VALUE")
            possibleValues.Columns.Add(attributeType.TextFieldName, typeof (double)).Expression = "F_DECIMAL_VALUE";
        }
        string columnName = attributeType.TextFieldName;
        if (attributeType.AttributeType == FieldTypes.ftMeasured && this._dc.ExtendedProperties.Contains((object) "F_MEASURE"))
        {
          columnName = "F_DOUBLE";
          possibleValues.Columns.Add(columnName, typeof (double));
          long int64 = Convert.ToInt64(this._dc.ExtendedProperties[(object) "F_MEASURE"]);
          MeasureDescriptor descriptor = MeasureHelper.FindDescriptor(int64);
          foreach (DataRow row in (InternalDataCollectionBase) possibleValues.Rows)
          {
            string mValue = Convert.ToString(row[attributeType.TextFieldName]);
            if (!string.IsNullOrEmpty(mValue))
            {
              MeasuredValue measuredValue = MeasureHelper.ConvertToMeasuredValue(mValue, descriptor, true);
              if (measuredValue.MeasureID != int64)
                measuredValue = MeasureHelper.ConvertToMeasuredValue(measuredValue, int64);
              row[columnName] = (object) measuredValue.Value;
            }
            else
              row[columnName] = (object) DBNull.Value;
          }
        }
        if (attributeType.AttributeType == FieldTypes.ftObjectLink)
        {
          columnName = "F_GUID";
          possibleValues.Columns.Add(columnName, typeof (string));
          foreach (DataRow row in (InternalDataCollectionBase) possibleValues.Rows)
          {
            if (!DBNull.Value.Equals(row[attributeType.ValueFieldName]))
            {
              long int64 = Convert.ToInt64(row[attributeType.ValueFieldName]);
              if (DBNull.Value.Equals(row[columnName]))
              {
                QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(int64);
                if (objectInfo.Empty)
                {
                  row[columnName] = (object) int64.ToString();
                }
                else
                {
                  row[columnName] = (object) objectInfo.VersionGuid.ToString();
                  row[2] = (object) objectInfo.Caption;
                }
              }
            }
          }
        }
        dataGridViewColumn = (DataGridViewColumn) new DataGridViewComboBoxColumn()
        {
          DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton,
          DisplayStyleForCurrentCellOnly = true,
          DataSource = (object) possibleValues,
          DisplayMember = possibleValues.Columns[2].ColumnName,
          ValueMember = columnName,
          MaxDropDownItems = 10
        };
      }
      else if ((this._attProps.Options & AttributeOptions.ImbaseFlag_TableRecordRef) == AttributeOptions.ImbaseFlag_TableRecordRef || (attributeType.Options & AttributeOptions.ImbaseFlag_TableRecordRef) == AttributeOptions.ImbaseFlag_TableRecordRef)
      {
        TextWithButtonColumn withButtonColumn = new TextWithButtonColumn();
        withButtonColumn.SortMode = DataGridViewColumnSortMode.Automatic;
        withButtonColumn.TextReadOnly = true;
        withButtonColumn.ButtonClick += new EventHandler(this.RecordReference_ButtonClick);
        dataGridViewColumn = (DataGridViewColumn) withButtonColumn;
        this._isRecordRef = true;
        flag = false;
      }
      else if (attributeType.AttributeType == FieldTypes.ftObjectLink)
      {
        TextWithButtonColumn withButtonColumn = new TextWithButtonColumn();
        withButtonColumn.SortMode = DataGridViewColumnSortMode.Automatic;
        withButtonColumn.TextReadOnly = true;
        if (attributeType is IDBObjectLinkAttributeType linkAttributeType)
          withButtonColumn.Tag = (object) linkAttributeType.GetValidObjectTypes();
        withButtonColumn.ButtonClick += new EventHandler(this.ObjectReference_ButtonClick);
        dataGridViewColumn = (DataGridViewColumn) withButtonColumn;
        this._isObjectRef = true;
        flag = false;
      }
      else if (attributeType.AttributeType == FieldTypes.ftMemo)
      {
        TextWithButtonColumn withButtonColumn = new TextWithButtonColumn();
        withButtonColumn.SortMode = DataGridViewColumnSortMode.Automatic;
        withButtonColumn.TextReadOnly = false;
        withButtonColumn.Tag = (object) attributeType.SizeType;
        withButtonColumn.ButtonClick += new EventHandler(this.MemoColumn_ButtonClick);
        dataGridViewColumn = (DataGridViewColumn) withButtonColumn;
        flag = false;
      }
      else if (attributeType.AttributeType == FieldTypes.ftDateTime)
      {
        DataGridViewCalendarColumn viewCalendarColumn = new DataGridViewCalendarColumn();
        viewCalendarColumn.SortMode = DataGridViewColumnSortMode.Automatic;
        viewCalendarColumn.Tag = (object) attributeType.SizeType;
        dataGridViewColumn = (DataGridViewColumn) viewCalendarColumn;
      }
    }
    if (dataGridViewColumn == null)
      dataGridViewColumn = (DataGridViewColumn) new DataGridViewTextBoxColumn();
    if (flag)
    {
      this.btNewRecord.Visible = false;
      this._grid.AllowUserToAddRows = true;
    }
    dataGridViewColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    dataGridViewColumn.Resizable = DataGridViewTriState.True;
    dataGridViewColumn.DataPropertyName = "F1";
    this._grid.Columns.Add(dataGridViewColumn);
  }

  protected override void OnClosed(EventArgs e)
  {
    ArrayEditor._size = this.Size;
    if (this._editor != null)
      this._editor.InvalidateGrid -= new EventHandler(this.Editor_InvalidateGrid);
    base.OnClosed(e);
  }

  private void Editor_InvalidateGrid(object sender, EventArgs e) => this._grid.Invalidate();

  protected override void OnShown(EventArgs e)
  {
    base.OnShown(e);
    Rectangle bounds = Screen.GetBounds((Control) this);
    int num1 = bounds.Right - this.Right;
    if (num1 < 0)
      this.Left += num1 - 2;
    int num2 = bounds.Bottom - this.Bottom - 32 /*0x20*/;
    if (num2 < 0)
      this.Top += num2 - 2;
    this._grid.Invalidate();
    if (this._editor == null)
      return;
    this._editor.InvalidateGrid += new EventHandler(this.Editor_InvalidateGrid);
  }

  private void Grid_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
  {
    if (this._askDeleteRow)
    {
      if (MessageBox.Show(LocalizationHolder.rm.GetString("Imbase.Client_73"), LocalizationHolder.rm.GetString("Imbase.Client_74"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
        e.Cancel = true;
      else
        e.Cancel = false;
      if (this._grid.SelectedRows.Count <= 1)
        return;
      this._askDeleteRow = false;
    }
    else
    {
      if (this._grid.SelectedRows.Count != 1)
        return;
      this._askDeleteRow = true;
    }
  }

  private void Grid_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
  {
    DataColumn dc = this._dc;
    string str = e.Value.ToString();
    DataGridViewColumn column = this._grid.Columns[e.ColumnIndex];
    if (string.IsNullOrEmpty(str) && column is DataGridViewComboBoxColumn)
    {
      e.Value = (object) DBNull.Value;
      e.ParsingApplied = true;
    }
    else
    {
      if (!dc.ExtendedProperties.Contains((object) "F_MEASURE"))
        return;
      long int64 = Convert.ToInt64(dc.ExtendedProperties[(object) "F_MEASURE"]);
      MeasureDescriptor descriptor = MeasureHelper.FindDescriptor(int64);
      if (!string.IsNullOrEmpty(str))
      {
        MeasuredValue measuredValue = MeasureHelper.ConvertToMeasuredValue(e.Value.ToString(), descriptor, true);
        if (measuredValue.MeasureID != int64)
          measuredValue = MeasureHelper.ConvertToMeasuredValue(measuredValue, int64);
        e.Value = (object) measuredValue.Value;
      }
      else
        e.Value = (object) null;
      e.ParsingApplied = true;
    }
  }

  private void Grid_DataError(object sender, DataGridViewDataErrorEventArgs e)
  {
    if (e.Exception.Message.Contains("ComboBox"))
    {
      if (sender is DataGridView dataGridView)
      {
        object obj = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value ?? (object) "(null)";
        dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = string.Format(LocalizationHolder.rm.GetString("Imbase.Client_1134"), (object) obj.ToString(), (object) dataGridView.Columns[e.ColumnIndex].HeaderText);
      }
      e.ThrowException = false;
    }
    else
    {
      int num = (int) MessageBox.Show(e.Exception.Message);
    }
  }

  private void RecordReference_ButtonClick(object sender, EventArgs e)
  {
    DataGridViewTextBoxCell gridViewTextBoxCell = sender as DataGridViewTextBoxCell;
    if (!(ServicesManager.GetService(typeof (IImbaseSelector)) is ImbaseSelector service))
      return;
    object obj = gridViewTextBoxCell.Value;
    string empty = string.Empty;
    if (obj != null && obj != DBNull.Value)
      empty = obj.ToString();
    string key = service.SelectRecord(empty, true);
    if (string.IsNullOrEmpty(key))
      return;
    gridViewTextBoxCell.Value = (object) key;
    this._grid.EndEdit();
    if (this._recordRefMap.ContainsKey(key))
      return;
    ThreadPool.QueueUserWorkItem(new WaitCallback(this._editor.RecordThreadProc), (object) new List<string>(1)
    {
      key
    });
  }

  private void ObjectReference_ButtonClick(object sender, EventArgs e)
  {
    if (!(sender is DataGridViewTextBoxCell gridViewTextBoxCell) || this._grid.Columns.Count <= gridViewTextBoxCell.ColumnIndex)
      return;
    DataGridViewColumn column = this._grid.Columns[gridViewTextBoxCell.ColumnIndex];
    if (column.Tag == null || !(column.Tag is int[] tag))
      return;
    DescriptorCollection descriptors = new DescriptorCollection();
    foreach (int objTypeID in tag)
      descriptors.Add((IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(objTypeID));
    IDescriptor rootDescriptor = (IDescriptor) new Intermech.Navigator.CustomNode.Descriptor(LocalizationHolder.rm.GetString("Imbase.Client_110"), descriptors);
    Intermech.Navigator.SelectionWindow.RegisterAnalyze((ISelectedItemsAnalyzer) new TypedObjectsSelectedItemsAnalyzer(new List<int>((IEnumerable<int>) tag), true), true);
    long[] numArray = Intermech.Navigator.SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("Imbase.Client_5"), "", rootDescriptor, SelectionOptions.SelectObjects | SelectionOptions.DisableMultiselect);
    if (numArray == null || numArray.Length == 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(numArray[0], false);
      if (objectActualCopy == null)
        return;
      string key = objectActualCopy.ObjectGUID.ToString();
      if (!this._objectRefMap.ContainsKey(key) && !this._objectRefMap.ContainsKey(key))
        ThreadPool.QueueUserWorkItem(new WaitCallback(this._editor.ObjectThreadProc), (object) new List<string>(1)
        {
          key
        });
      gridViewTextBoxCell.Value = (object) key;
      this._grid.EndEdit();
    }
  }

  private void MemoColumn_ButtonClick(object sender, EventArgs e)
  {
    if (!(sender is DataGridViewTextBoxCell gridViewTextBoxCell) || this._grid.Columns.Count <= gridViewTextBoxCell.ColumnIndex)
      return;
    string empty = string.Empty;
    object obj = gridViewTextBoxCell.Value;
    if (obj != null)
      empty = obj.ToString();
    using (MemoForm memoForm = new MemoForm())
    {
      memoForm.Memo = empty;
      if (memoForm.ShowDialog() != DialogResult.OK)
        return;
      gridViewTextBoxCell.Value = (object) memoForm.Memo;
    }
  }

  private void ArrayEditor_Shown(object sender, EventArgs e)
  {
    this.AddColumn();
    if (this._isObjectRef)
    {
      this.NameObjectRefs();
    }
    else
    {
      if (!this._isRecordRef)
        return;
      this.NameRecordRefs();
    }
  }

  private void btNewRecord_Click(object sender, EventArgs e)
  {
    this._dt.Rows.Add(this._dt.NewRow());
    this._grid.CurrentCell = this._grid.Rows[this._grid.Rows.Count - 1].Cells[0];
    this._grid.BeginEdit(false);
  }

  private void Grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
  {
    if (this._isRecordRef)
    {
      string str;
      if (!this._recordRefMap.TryGetValue(e.Value.ToString(), out str))
        return;
      e.Value = (object) str;
      e.FormattingApplied = true;
      e.CellStyle.ForeColor = Color.DarkBlue;
    }
    else
    {
      string str;
      if (!this._isObjectRef || !this._objectRefMap.TryGetValue(e.Value.ToString(), out str))
        return;
      e.Value = (object) str;
      e.FormattingApplied = true;
      e.CellStyle.ForeColor = Color.DarkBlue;
    }
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ArrayEditor));
    this.cancelButton = new Button();
    this.okButton = new Button();
    this._grid = new DataGridView();
    this.btNewRecord = new Button();
    this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
    ((ISupportInitialize) this._grid).BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.cancelButton, "cancelButton");
    this.cancelButton.DialogResult = DialogResult.Cancel;
    this.cancelButton.Name = "cancelButton";
    this.cancelButton.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.okButton, "okButton");
    this.okButton.DialogResult = DialogResult.OK;
    this.okButton.Name = "okButton";
    this.okButton.UseVisualStyleBackColor = true;
    this._grid.AllowUserToAddRows = false;
    componentResourceManager.ApplyResources((object) this._grid, "_grid");
    this._grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
    this._grid.BackgroundColor = SystemColors.Control;
    this._grid.BorderStyle = BorderStyle.Fixed3D;
    this._grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    this._grid.Name = "_grid";
    this._grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    this._grid.CellFormatting += new DataGridViewCellFormattingEventHandler(this.Grid_CellFormatting);
    this._grid.CellParsing += new DataGridViewCellParsingEventHandler(this.Grid_CellParsing);
    this._grid.DataError += new DataGridViewDataErrorEventHandler(this.Grid_DataError);
    this._grid.UserDeletingRow += new DataGridViewRowCancelEventHandler(this.Grid_UserDeletingRow);
    componentResourceManager.ApplyResources((object) this.btNewRecord, "btNewRecord");
    this.btNewRecord.Name = "btNewRecord";
    this.btNewRecord.UseVisualStyleBackColor = true;
    this.btNewRecord.Click += new EventHandler(this.btNewRecord_Click);
    componentResourceManager.ApplyResources((object) this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
    this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
    this.AcceptButton = (IButtonControl) this.okButton;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.cancelButton;
    this.Controls.Add((Control) this.btNewRecord);
    this.Controls.Add((Control) this._grid);
    this.Controls.Add((Control) this.cancelButton);
    this.Controls.Add((Control) this.okButton);
    this.DoubleBuffered = true;
    this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
    this.Name = nameof (ArrayEditor);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Show;
    this.Shown += new EventHandler(this.ArrayEditor_Shown);
    ((ISupportInitialize) this._grid).EndInit();
    this.ResumeLayout(false);
  }
}
