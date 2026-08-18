// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Editors.DataTableTransactionLog
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Imbase.Editors;

internal class DataTableTransactionLog
{
  protected List<DataTableTransactionRecord> _transactions;
  protected DataTable _sourceTable;
  protected bool _logging;
  protected int _lastAcceptedChangeIndex;
  protected Dictionary<DataRow, List<int>> _uncomittedRows;
  protected List<DataTableTransactionRecord> _waitingForChangedEventList;

  public event DataTableTransactionLog.TransactionEventHandler TransactionAdding;

  public event DataTableTransactionLog.TransactionEventHandler TransactionAdded;

  public DataTable SourceTable
  {
    get => this._sourceTable;
    set
    {
      if (value == null)
        throw new ArgumentNullException("The source table cannot be null.");
      if (this._sourceTable != null)
        this.Unhook();
      this._sourceTable = value;
      this.Hook();
    }
  }

  public List<DataTableTransactionRecord> Log => this._transactions;

  public DataTableTransactionRecord this[int idx]
  {
    get
    {
      if (idx < 0 || idx >= this._transactions.Count)
        throw new ArgumentOutOfRangeException("Indexer is out of range.");
      return this._transactions[idx];
    }
  }

  public DataTableTransactionLog() => this.Initialize();

  public DataTableTransactionLog(DataTable sourceTable)
  {
    this.SourceTable = sourceTable;
    this.Initialize();
  }

  protected void Initialize()
  {
    this._transactions = new List<DataTableTransactionRecord>();
    this._uncomittedRows = new Dictionary<DataRow, List<int>>();
    this._waitingForChangedEventList = new List<DataTableTransactionRecord>();
    this._logging = true;
  }

  public void ClearLog()
  {
    this._lastAcceptedChangeIndex = 0;
    this._transactions.Clear();
    this._uncomittedRows.Clear();
    this._waitingForChangedEventList.Clear();
    this._transactions.TrimExcess();
    this._logging = true;
  }

  public bool SuspendLogging()
  {
    int num = this._logging ? 1 : 0;
    this._logging = false;
    return num != 0;
  }

  public void ResumeLogging() => this._logging = true;

  public void AcceptChanges()
  {
    this._lastAcceptedChangeIndex = this._transactions.Count;
    this._sourceTable.AcceptChanges();
  }

  public void RejectChanges()
  {
    this._transactions.RemoveRange(this._lastAcceptedChangeIndex, this._transactions.Count - this._lastAcceptedChangeIndex);
    this._sourceTable.RejectChanges();
  }

  public void CollectUncommittedRows()
  {
    List<int> intList = new List<int>();
    foreach (List<int> collection in this._uncomittedRows.Values)
      intList.AddRange((IEnumerable<int>) collection);
    intList.Sort();
    for (int index = intList.Count - 1; index >= 0; --index)
      this._transactions.RemoveAt(intList[index]);
    this._uncomittedRows.Clear();
  }

  public DataTableTransactionRecord Undo(int index, out DataRow newRow)
  {
    if (index < 0 || index >= this._transactions.Count)
      throw new ArgumentOutOfRangeException("Index cannot be negative or greater than the number of transactions.");
    this.SuspendLogging();
    DataTableTransactionRecord transaction = this._transactions[index];
    newRow = transaction.Undo(this._sourceTable);
    if (newRow != null)
      this.FixupRowsReverse(index, newRow);
    this.ResumeLogging();
    return transaction;
  }

  public DataTableTransactionRecord Redo(int index, out DataRow newRow)
  {
    if (index < 0 || index >= this._transactions.Count)
      throw new ArgumentOutOfRangeException("Index cannot be negative or greater than the number of transactions.");
    this.SuspendLogging();
    DataTableTransactionRecord transaction = this._transactions[index];
    newRow = transaction.Redo(this._sourceTable);
    if (newRow != null)
      this.FixupRowsForward(index, newRow);
    this.ResumeLogging();
    return transaction;
  }

  protected void FixupRowsReverse(int index, DataRow newRow)
  {
    DataRow row = this._transactions[index].Row;
    for (int index1 = index; index1 >= 0; --index1)
    {
      if (this._transactions[index1].Row == row)
        this._transactions[index1].Row = newRow;
    }
  }

  protected void FixupRowsForward(int idx, DataRow newRow)
  {
    DataRow row = this._transactions[idx].Row;
    for (int index = idx; index < this._transactions.Count; ++index)
    {
      if (this._transactions[index].Row == row)
        this._transactions[index].Row = newRow;
    }
  }

  protected void Hook()
  {
    this._sourceTable.ColumnChanging += new DataColumnChangeEventHandler(this.OnColumnChanging);
    this._sourceTable.ColumnChanged += new DataColumnChangeEventHandler(this.OnColumnChanged);
    this._sourceTable.RowDeleting += new DataRowChangeEventHandler(this.OnRowDeleting);
    this._sourceTable.RowChanged += new DataRowChangeEventHandler(this.OnRowChanged);
    this._sourceTable.TableNewRow += new DataTableNewRowEventHandler(this.OnTableNewRow);
    this._sourceTable.TableCleared += new DataTableClearEventHandler(this.OnTableCleared);
  }

  protected void Unhook()
  {
    this._sourceTable.ColumnChanging -= new DataColumnChangeEventHandler(this.OnColumnChanging);
    this._sourceTable.ColumnChanged -= new DataColumnChangeEventHandler(this.OnColumnChanged);
    this._sourceTable.RowDeleting -= new DataRowChangeEventHandler(this.OnRowDeleting);
    this._sourceTable.RowChanged -= new DataRowChangeEventHandler(this.OnRowChanged);
    this._sourceTable.TableNewRow -= new DataTableNewRowEventHandler(this.OnTableNewRow);
    this._sourceTable.TableCleared -= new DataTableClearEventHandler(this.OnTableCleared);
  }

  protected void OnTableCleared(object sender, DataTableClearEventArgs e) => this.ClearLog();

  protected void OnTableNewRow(object sender, DataTableNewRowEventArgs e)
  {
    if (!this._logging)
      return;
    int count = this._transactions.Count;
    DataTableTransactionRecord record = new DataTableTransactionRecord(count, e.Row, DataTableTransactionRecord.RecordType.NewRow);
    this.OnTransactionAdding(new TransactionEventArgs(record));
    this._transactions.Add(record);
    this.OnTransactionAdded(new TransactionEventArgs(record));
    this._uncomittedRows.Add(e.Row, new List<int>()
    {
      count
    });
  }

  protected void OnRowChanged(object sender, DataRowChangeEventArgs e)
  {
    if (!this._logging || e.Action != DataRowAction.Add)
      return;
    if (!this._uncomittedRows.ContainsKey(e.Row))
      throw new DataTableTransactionException("Attempting to commit a row that doesn't exist in the uncommitted row collection.");
    this._uncomittedRows.Remove(e.Row);
  }

  protected void OnColumnChanging(object sender, DataColumnChangeEventArgs e)
  {
    if (!this._logging)
      return;
    object oldValue = e.Row[e.Column];
    int count = this._transactions.Count;
    DataTableTransactionRecord record = new DataTableTransactionRecord(count, e.Row, e.Column.ColumnName, oldValue, e.ProposedValue);
    TransactionEventArgs e1 = new TransactionEventArgs(record);
    this.OnTransactionAdding(e1);
    if (e1.Cancel)
      return;
    this._transactions.Add(record);
    this.OnTransactionAdded(e1);
    this._waitingForChangedEventList.Add(record);
    if (!this._uncomittedRows.ContainsKey(e.Row))
      return;
    this._uncomittedRows[e.Row].Add(count);
  }

  protected void OnColumnChanged(object sender, DataColumnChangeEventArgs e)
  {
    if (!this._logging)
      return;
    for (int index = this._waitingForChangedEventList.Count - 1; index >= 0; --index)
    {
      DataTableTransactionRecord waitingForChangedEvent = this._waitingForChangedEventList[index];
      if (waitingForChangedEvent.ColumnName == e.Column.ColumnName && waitingForChangedEvent.Row == e.Row)
      {
        waitingForChangedEvent.NewValue = e.ProposedValue;
        this._waitingForChangedEventList.RemoveAt(index);
        break;
      }
    }
  }

  protected void OnRowDeleting(object sender, DataRowChangeEventArgs e)
  {
    if (!this._logging)
      return;
    DataTableTransactionRecord record = new DataTableTransactionRecord(this._transactions.Count, e.Row, DataTableTransactionRecord.RecordType.DeleteRow);
    record.SaveRowFields(e.Row);
    this.OnTransactionAdding(new TransactionEventArgs(record));
    this._transactions.Add(record);
    Dictionary<string, object> columnValues = record.ColumnValues;
    for (int index = 0; index < this._transactions.Count - 1; ++index)
    {
      if (this._transactions[index].Row == e.Row)
        this._transactions[index].ColumnValues = columnValues;
    }
    this.OnTransactionAdded(new TransactionEventArgs(record));
  }

  protected virtual void OnTransactionAdding(TransactionEventArgs e)
  {
    DataTableTransactionLog.TransactionEventHandler transactionAdding = this.TransactionAdding;
    if (transactionAdding == null)
      return;
    transactionAdding((object) this, e);
  }

  protected virtual void OnTransactionAdded(TransactionEventArgs e)
  {
    DataTableTransactionLog.TransactionEventHandler transactionAdded = this.TransactionAdded;
    if (transactionAdded == null)
      return;
    transactionAdded((object) this, e);
  }

  public delegate void TransactionEventHandler(object sender, TransactionEventArgs e);
}
