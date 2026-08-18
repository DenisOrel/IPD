// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Editors.DataTableTransactionRecord
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Imbase.Editors;

internal class DataTableTransactionRecord
{
  protected DataRow _row;
  protected int _rowIndex = -1;
  protected int _transNum;
  protected DataTableTransactionRecord.RecordType _transactionType;
  protected string _columnName;
  protected object _oldValue;
  protected object _newValue;
  protected Dictionary<string, object> _columnValues;

  public Dictionary<string, object> ColumnValues
  {
    get => this._columnValues;
    set => this._columnValues = value;
  }

  public bool WasDeleted => this._columnValues.Count > 0;

  public int TransNum => this._transNum;

  public object NewValue
  {
    get => this._newValue;
    set => this._newValue = value;
  }

  public object OldValue => this._oldValue;

  public string ColumnName => this._columnName;

  public DataTableTransactionRecord.RecordType TransactionType => this._transactionType;

  public DataRow Row
  {
    get => this._row;
    set => this._row = value;
  }

  public bool IsRowDeleted => this._row.RowState == DataRowState.Deleted;

  public object GetGuaranteedRowValue(string fieldName)
  {
    object guaranteedRowValue;
    if (this.WasDeleted)
      guaranteedRowValue = this._columnValues[fieldName];
    else
      guaranteedRowValue = this._row.RowState != DataRowState.Deleted ? this._row[fieldName] : throw new DataTableTransactionException("Row has been deleted and there is no saved column values.");
    return guaranteedRowValue;
  }

  protected DataTableTransactionRecord()
  {
  }

  public DataTableTransactionRecord(
    int transNum,
    DataRow row,
    DataTableTransactionRecord.RecordType transType)
  {
    if (row == null)
      throw new ArgumentNullException("DataRow cannot be null.");
    if (transType == DataTableTransactionRecord.RecordType.ChangeField)
      throw new DataTableTransactionException("ChangeField transactions cannot use this constructor.");
    if (transType == DataTableTransactionRecord.RecordType.DeleteRow)
      this._rowIndex = row.Table.Rows.IndexOf(row);
    this._transNum = transNum;
    this._row = row;
    this._transactionType = transType;
    this.Initialize();
  }

  public DataTableTransactionRecord(
    int transNum,
    DataRow row,
    string columnName,
    object oldValue,
    object newValue)
  {
    if (columnName == null)
      throw new ArgumentNullException("Column name cannot be null.");
    if (row == null)
      throw new ArgumentNullException("DataRow cannot be null.");
    this._transNum = transNum;
    this._row = row;
    this._transactionType = DataTableTransactionRecord.RecordType.ChangeField;
    this._columnName = columnName;
    this._oldValue = oldValue;
    this._newValue = newValue;
    this.Initialize();
  }

  public void AddColumnNameValuePair(string columnName, object val)
  {
    if (columnName == null)
      throw new ArgumentNullException("Column name cannot be null.");
    this._columnValues.Add(columnName, val);
  }

  public object GetValue(string columnName)
  {
    if (columnName == null)
      throw new ArgumentNullException("Column name cannot be null.");
    return this._columnValues.ContainsKey(columnName) ? this._columnValues[columnName] : throw new ArgumentException("Column name not in deleted column values collection.");
  }

  public virtual DataRow Undo(DataTable dataTable)
  {
    DataRow row = (DataRow) null;
    switch (this._transactionType)
    {
      case DataTableTransactionRecord.RecordType.NewRow:
        if (!this.WasDeleted)
          this.SaveRowFields(this._row);
        this._row.Delete();
        break;
      case DataTableTransactionRecord.RecordType.DeleteRow:
        row = dataTable.NewRow();
        this.RestoreRowFields(row);
        if (this._rowIndex != -1)
        {
          dataTable.Rows.InsertAt(row, this._rowIndex);
          break;
        }
        dataTable.Rows.Add(row);
        break;
      case DataTableTransactionRecord.RecordType.ChangeField:
        this._row[this._columnName] = this._oldValue;
        break;
    }
    return row;
  }

  public virtual DataRow Redo(DataTable dataTable)
  {
    DataRow row = (DataRow) null;
    switch (this._transactionType)
    {
      case DataTableTransactionRecord.RecordType.NewRow:
        row = dataTable.NewRow();
        if (this.WasDeleted)
          this.RestoreRowFields(row);
        dataTable.Rows.Add(row);
        break;
      case DataTableTransactionRecord.RecordType.DeleteRow:
        if (!this.WasDeleted)
          this.SaveRowFields(this._row);
        this._row.Delete();
        break;
      case DataTableTransactionRecord.RecordType.ChangeField:
        this._row[this._columnName] = this._newValue;
        break;
    }
    return row;
  }

  protected void Initialize() => this._columnValues = new Dictionary<string, object>();

  public void RestoreRowFields(DataRow row)
  {
    foreach (DataColumn column in (InternalDataCollectionBase) row.Table.Columns)
      row[column] = this.GetValue(column.ColumnName);
  }

  public void SaveRowFields(DataRow row)
  {
    foreach (DataColumn column in (InternalDataCollectionBase) row.Table.Columns)
      this.AddColumnNameValuePair(column.ColumnName, row[column]);
  }

  public enum RecordType
  {
    NewRow,
    DeleteRow,
    ChangeField,
  }
}
