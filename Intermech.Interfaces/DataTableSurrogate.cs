using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Globalization;


[Serializable]
public class DataTableSurrogate
{
  private string _tableName;
  private string _namespace;
  private string _prefix;
  private bool _caseSensitive;
  private CultureInfo _locale;
  private string _displayExpression;
  private int _minimumCapacity;
  private DataColumnSurrogate[] _dataColumnSurrogates;
  private ArrayList _uniqueConstraints;
  private Hashtable _extendedProperties;
  private BitArray _rowStates;
  private object[][] _records;
  private Hashtable _rowErrors = new Hashtable();
  private Hashtable _colErrors = new Hashtable();

  public DataTableSurrogate(DataTable dt)
  {
    this._tableName = dt != null ? dt.TableName : throw new ArgumentNullException("The parameter dt is null");
    this._namespace = dt.Namespace;
    this._prefix = dt.Prefix;
    this._caseSensitive = dt.CaseSensitive;
    this._locale = dt.Locale;
    this._displayExpression = dt.DisplayExpression;
    this._minimumCapacity = dt.MinimumCapacity;
    this._dataColumnSurrogates = new DataColumnSurrogate[dt.Columns.Count];
    for (int index = 0; index < dt.Columns.Count; ++index)
      this._dataColumnSurrogates[index] = new DataColumnSurrogate(dt.Columns[index]);
    this._uniqueConstraints = this.GetUniqueConstraints(dt);
    this._extendedProperties = new Hashtable();
    if (dt.ExtendedProperties.Keys.Count > 0)
    {
      foreach (object key in (IEnumerable) dt.ExtendedProperties.Keys)
        this._extendedProperties.Add(key, dt.ExtendedProperties[key]);
    }
    if (dt.Rows.Count <= 0)
      return;
    this._rowStates = new BitArray(dt.Rows.Count << 1);
    this._records = new object[dt.Columns.Count][];
    for (int index = 0; index < dt.Columns.Count; ++index)
      this._records[index] = new object[dt.Rows.Count << 1];
    for (int index = 0; index < dt.Rows.Count; ++index)
      this.GetRecords(dt.Rows[index], index << 1);
  }

  public DataTable ConvertToDataTable()
  {
    DataTable dt = new DataTable();
    this.ReadSchemaIntoDataTable(dt);
    this.ReadDataIntoDataTable(dt);
    return dt;
  }

  public void ReadSchemaIntoDataTable(DataTable dt)
  {
    if (dt == null)
      throw new ArgumentNullException("The datatable parameter cannot be null");
    dt.TableName = this._tableName;
    dt.Namespace = this._namespace;
    dt.Prefix = this._prefix;
    dt.CaseSensitive = this._caseSensitive;
    dt.Locale = this._locale;
    dt.DisplayExpression = this._displayExpression;
    dt.MinimumCapacity = this._minimumCapacity;
    for (int index = 0; index < this._dataColumnSurrogates.Length; ++index)
    {
      DataColumn dataColumn = this._dataColumnSurrogates[index].ConvertToDataColumn();
      dt.Columns.Add(dataColumn);
    }
    this.SetUniqueConstraints(dt, this._uniqueConstraints);
    if (this._extendedProperties.Keys.Count <= 0)
      return;
    foreach (object key in (IEnumerable) this._extendedProperties.Keys)
      dt.ExtendedProperties.Add(key, this._extendedProperties[key]);
  }

  public void ReadDataIntoDataTable(DataTable dt) => this.ReadDataIntoDataTable(dt, true);

  internal void ReadDataIntoDataTable(DataTable dt, bool suppressSchema)
  {
    if (dt == null)
      throw new ArgumentNullException("The datatable parameter cannot be null");
    ArrayList readOnlyList = (ArrayList) null;
    ArrayList constraintRulesList = (ArrayList) null;
    if (suppressSchema)
    {
      readOnlyList = this.SuppressReadOnly(dt);
      constraintRulesList = this.SuppressConstraintRules(dt);
    }
    if (this._records != null && dt.Columns.Count > 0)
    {
      int num = this._records[0].Length >> 1;
      for (int index = 0; index < num; ++index)
        this.ConvertToDataRow(dt, index << 1);
    }
    if (!suppressSchema)
      return;
    this.ResetReadOnly(dt, readOnlyList);
    this.ResetConstraintRules(dt, constraintRulesList);
  }

  private ArrayList GetUniqueConstraints(DataTable dt)
  {
    ArrayList uniqueConstraints = new ArrayList();
    for (int index1 = 0; index1 < dt.Constraints.Count; ++index1)
    {
      Constraint constraint = dt.Constraints[index1];
      if (constraint is UniqueConstraint uniqueConstraint)
      {
        string constraintName = constraint.ConstraintName;
        int[] numArray = new int[uniqueConstraint.Columns.Length];
        for (int index2 = 0; index2 < numArray.Length; ++index2)
          numArray[index2] = uniqueConstraint.Columns[index2].Ordinal;
        ArrayList arrayList = new ArrayList();
        arrayList.Add((object) constraintName);
        arrayList.Add((object) numArray);
        arrayList.Add((object) uniqueConstraint.IsPrimaryKey);
        Hashtable hashtable = new Hashtable();
        if (uniqueConstraint.ExtendedProperties.Keys.Count > 0)
        {
          foreach (object key in (IEnumerable) uniqueConstraint.ExtendedProperties.Keys)
            hashtable.Add(key, uniqueConstraint.ExtendedProperties[key]);
        }
        arrayList.Add((object) hashtable);
        uniqueConstraints.Add((object) arrayList);
      }
    }
    return uniqueConstraints;
  }

  private void SetUniqueConstraints(DataTable dt, ArrayList constraintList)
  {
    foreach (ArrayList constraint in constraintList)
    {
      string name = (string) constraint[0];
      int[] numArray = (int[]) constraint[1];
      bool isPrimaryKey = (bool) constraint[2];
      Hashtable hashtable = (Hashtable) constraint[3];
      DataColumn[] columns = new DataColumn[numArray.Length];
      for (int index = 0; index < numArray.Length; ++index)
        columns[index] = dt.Columns[numArray[index]];
      UniqueConstraint uniqueConstraint = new UniqueConstraint(name, columns, isPrimaryKey);
      if (hashtable.Keys.Count > 0)
      {
        foreach (object key in (IEnumerable) hashtable.Keys)
          uniqueConstraint.ExtendedProperties.Add(key, hashtable[key]);
      }
      dt.Constraints.Add((Constraint) uniqueConstraint);
    }
  }

  internal void SetColumnExpressions(DataTable dt)
  {
    for (int index = 0; index < dt.Columns.Count; ++index)
    {
      DataColumn column = dt.Columns[index];
      this._dataColumnSurrogates[index].SetColumnExpression(column);
    }
  }

  private void GetRecords(DataRow row, int bitIndex)
  {
    this.ConvertToSurrogateRowState(row.RowState, bitIndex);
    this.ConvertToSurrogateRecords(row, bitIndex);
    this.ConvertToSurrogateRowError(row, bitIndex >> 1);
  }

  public DataRow ConvertToDataRow(DataTable dt, int bitIndex)
  {
    DataRowState rowState = this.ConvertToRowState(bitIndex);
    DataRow row = this.ConstructRow(dt, rowState, bitIndex);
    this.ConvertToRowError(row, bitIndex >> 1);
    return row;
  }

  private void ConvertToSurrogateRowState(DataRowState rowState, int bitIndex)
  {
    switch (rowState)
    {
      case DataRowState.Unchanged:
        this._rowStates[bitIndex] = false;
        this._rowStates[bitIndex + 1] = false;
        break;
      case DataRowState.Added:
        this._rowStates[bitIndex] = false;
        this._rowStates[bitIndex + 1] = true;
        break;
      case DataRowState.Deleted:
        this._rowStates[bitIndex] = true;
        this._rowStates[bitIndex + 1] = true;
        break;
      case DataRowState.Modified:
        this._rowStates[bitIndex] = true;
        this._rowStates[bitIndex + 1] = false;
        break;
      default:
        throw new InvalidEnumArgumentException($"Unrecognized row state {rowState}");
    }
  }

  private DataRowState ConvertToRowState(int bitIndex)
  {
    bool rowState1 = this._rowStates[bitIndex];
    bool rowState2 = this._rowStates[bitIndex + 1];
    if (!rowState1 && !rowState2)
      return DataRowState.Unchanged;
    if (!rowState1 & rowState2)
      return DataRowState.Added;
    if (rowState1 && !rowState2)
      return DataRowState.Modified;
    if (rowState1 & rowState2)
      return DataRowState.Deleted;
    throw new ArgumentException("Unrecognized bitpattern");
  }

  private void ConvertToSurrogateRecords(DataRow row, int bitIndex)
  {
    int count = row.Table.Columns.Count;
    DataRowState rowState = row.RowState;
    if (rowState != DataRowState.Added)
    {
      for (int columnIndex = 0; columnIndex < count; ++columnIndex)
        this._records[columnIndex][bitIndex] = row[columnIndex, DataRowVersion.Original];
    }
    if (rowState == DataRowState.Unchanged || rowState == DataRowState.Deleted)
      return;
    for (int columnIndex = 0; columnIndex < count; ++columnIndex)
      this._records[columnIndex][bitIndex + 1] = row[columnIndex, DataRowVersion.Current];
  }

  private DataRow ConstructRow(DataTable dt, DataRowState rowState, int bitIndex)
  {
    DataRow row = dt.NewRow();
    int count = dt.Columns.Count;
    switch (rowState)
    {
      case DataRowState.Unchanged:
        for (int columnIndex = 0; columnIndex < count; ++columnIndex)
          row[columnIndex] = this._records[columnIndex][bitIndex];
        dt.Rows.Add(row);
        row.AcceptChanges();
        break;
      case DataRowState.Added:
        for (int columnIndex = 0; columnIndex < count; ++columnIndex)
          row[columnIndex] = this._records[columnIndex][bitIndex + 1];
        dt.Rows.Add(row);
        break;
      case DataRowState.Deleted:
        for (int columnIndex = 0; columnIndex < count; ++columnIndex)
          row[columnIndex] = this._records[columnIndex][bitIndex];
        dt.Rows.Add(row);
        row.AcceptChanges();
        row.Delete();
        break;
      case DataRowState.Modified:
        for (int columnIndex = 0; columnIndex < count; ++columnIndex)
          row[columnIndex] = this._records[columnIndex][bitIndex];
        dt.Rows.Add(row);
        row.AcceptChanges();
        row.BeginEdit();
        for (int columnIndex = 0; columnIndex < count; ++columnIndex)
          row[columnIndex] = this._records[columnIndex][bitIndex + 1];
        row.EndEdit();
        break;
      default:
        throw new InvalidEnumArgumentException($"Unrecognized row state {rowState}");
    }
    return row;
  }

  private void ConvertToSurrogateRowError(DataRow row, int rowIndex)
  {
    if (!row.HasErrors)
      return;
    this._rowErrors.Add((object) rowIndex, (object) row.RowError);
    DataColumn[] columnsInError = row.GetColumnsInError();
    if (columnsInError.Length == 0)
      return;
    int[] numArray = new int[columnsInError.Length];
    string[] strArray = new string[columnsInError.Length];
    for (int index = 0; index < columnsInError.Length; ++index)
    {
      numArray[index] = columnsInError[index].Ordinal;
      strArray[index] = row.GetColumnError(columnsInError[index]);
    }
    this._colErrors.Add((object) rowIndex, (object) new ArrayList()
    {
      (object) numArray,
      (object) strArray
    });
  }

  private void ConvertToRowError(DataRow row, int rowIndex)
  {
    if (this._rowErrors.ContainsKey((object) rowIndex))
      row.RowError = (string) this._rowErrors[(object) rowIndex];
    if (!this._colErrors.ContainsKey((object) rowIndex))
      return;
    ArrayList colError = (ArrayList) this._colErrors[(object) rowIndex];
    int[] numArray = (int[]) colError[0];
    string[] strArray = (string[]) colError[1];
    for (int index = 0; index < numArray.Length; ++index)
      row.SetColumnError(numArray[index], strArray[index]);
  }

  private ArrayList SuppressReadOnly(DataTable dt)
  {
    ArrayList arrayList = new ArrayList();
    for (int index = 0; index < dt.Columns.Count; ++index)
    {
      if (dt.Columns[index].Expression == string.Empty && dt.Columns[index].ReadOnly)
        arrayList.Add((object) index);
    }
    return arrayList;
  }

  private ArrayList SuppressConstraintRules(DataTable dt)
  {
    ArrayList arrayList = new ArrayList();
    DataSet dataSet = dt.DataSet;
    if (dataSet != null)
    {
      for (int index1 = 0; index1 < dataSet.Tables.Count; ++index1)
      {
        DataTable table = dataSet.Tables[index1];
        for (int index2 = 0; index2 < table.Constraints.Count; ++index2)
        {
          Constraint constraint = table.Constraints[index2];
          if (constraint is ForeignKeyConstraint)
          {
            ForeignKeyConstraint foreignKeyConstraint = (ForeignKeyConstraint) constraint;
            if (foreignKeyConstraint.RelatedTable == dt)
            {
              arrayList.Add((object) new ArrayList()
              {
                (object) new int[2]{ index1, index2 },
                (object) new int[3]
                {
                  (int) foreignKeyConstraint.AcceptRejectRule,
                  (int) foreignKeyConstraint.UpdateRule,
                  (int) foreignKeyConstraint.DeleteRule
                }
              });
              foreignKeyConstraint.AcceptRejectRule = AcceptRejectRule.None;
              foreignKeyConstraint.UpdateRule = Rule.None;
              foreignKeyConstraint.DeleteRule = Rule.None;
            }
          }
        }
      }
    }
    return arrayList;
  }

  private void ResetReadOnly(DataTable dt, ArrayList readOnlyList)
  {
    DataSet dataSet = dt.DataSet;
    foreach (int index in readOnlyList)
      dt.Columns[index].ReadOnly = true;
  }

  private void ResetConstraintRules(DataTable dt, ArrayList constraintRulesList)
  {
    DataSet dataSet = dt.DataSet;
    foreach (ArrayList constraintRules in constraintRulesList)
    {
      int[] numArray1 = (int[]) constraintRules[0];
      int[] numArray2 = (int[]) constraintRules[1];
      int index1 = numArray1[0];
      int index2 = numArray1[1];
      ForeignKeyConstraint constraint = (ForeignKeyConstraint) dataSet.Tables[index1].Constraints[index2];
      constraint.AcceptRejectRule = (AcceptRejectRule) numArray2[0];
      constraint.UpdateRule = (Rule) numArray2[1];
      constraint.DeleteRule = (Rule) numArray2[2];
    }
  }

  private bool IsSchemaIdentical(DataTable dt)
  {
    if (dt.TableName != this._tableName || dt.Namespace != this._namespace || dt.Columns.Count != this._dataColumnSurrogates.Length)
      return false;
    for (int index = 0; index < dt.Columns.Count; ++index)
    {
      DataColumn column = dt.Columns[index];
      if (!this._dataColumnSurrogates[index].IsSchemaIdentical(column))
        return false;
    }
    return true;
  }
}
