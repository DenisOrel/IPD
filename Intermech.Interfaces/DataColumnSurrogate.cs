
// Type: DataColumnSurrogate
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections;
using System.Data;


[Serializable]
public class DataColumnSurrogate
{
  private string _columnName;
  private string _namespace;
  private string _prefix;
  private MappingType _columnMapping;
  private bool _allowNull;
  private bool _autoIncrement;
  private long _autoIncrementStep;
  private long _autoIncrementSeed;
  private string _caption;
  private object _defaultValue;
  private bool _readOnly;
  private int _maxLength;
  private Type _dataType;
  private string _expression;
  private Hashtable _extendedProperties;

  public DataColumnSurrogate(DataColumn dc)
  {
    this._columnName = dc != null ? dc.ColumnName : throw new ArgumentNullException("The datacolumn parameter is null");
    this._namespace = dc.Namespace;
    this._dataType = dc.DataType;
    this._prefix = dc.Prefix;
    this._columnMapping = dc.ColumnMapping;
    this._allowNull = dc.AllowDBNull;
    this._autoIncrement = dc.AutoIncrement;
    this._autoIncrementStep = dc.AutoIncrementStep;
    this._autoIncrementSeed = dc.AutoIncrementSeed;
    this._caption = dc.Caption;
    this._defaultValue = dc.DefaultValue;
    this._readOnly = dc.ReadOnly;
    this._maxLength = dc.MaxLength;
    this._expression = dc.Expression;
    this._extendedProperties = new Hashtable();
    if (dc.ExtendedProperties.Keys.Count <= 0)
      return;
    foreach (object key in (IEnumerable) dc.ExtendedProperties.Keys)
      this._extendedProperties.Add(key, dc.ExtendedProperties[key]);
  }

  public DataColumn ConvertToDataColumn()
  {
    DataColumn dataColumn = new DataColumn();
    dataColumn.ColumnName = this._columnName;
    dataColumn.Namespace = this._namespace;
    dataColumn.DataType = this._dataType;
    dataColumn.Prefix = this._prefix;
    dataColumn.ColumnMapping = this._columnMapping;
    dataColumn.AllowDBNull = this._allowNull;
    dataColumn.AutoIncrement = this._autoIncrement;
    dataColumn.AutoIncrementStep = this._autoIncrementStep;
    dataColumn.AutoIncrementSeed = this._autoIncrementSeed;
    dataColumn.Caption = this._caption;
    dataColumn.DefaultValue = this._defaultValue;
    dataColumn.ReadOnly = this._readOnly;
    dataColumn.MaxLength = this._maxLength;
    if (this._extendedProperties.Keys.Count > 0)
    {
      foreach (object key in (IEnumerable) this._extendedProperties.Keys)
        dataColumn.ExtendedProperties.Add(key, this._extendedProperties[key]);
    }
    return dataColumn;
  }

  internal void SetColumnExpression(DataColumn dc)
  {
    if (this._expression == null || this._expression.Equals(string.Empty))
      return;
    dc.Expression = this._expression;
  }

  internal bool IsSchemaIdentical(DataColumn dc)
  {
    return !(dc.ColumnName != this._columnName) && !(dc.Namespace != this._namespace) && !(dc.DataType != this._dataType) && !(dc.Prefix != this._prefix) && dc.ColumnMapping == this._columnMapping && dc.ColumnMapping == this._columnMapping && dc.AllowDBNull == this._allowNull && dc.AutoIncrement == this._autoIncrement && dc.AutoIncrementStep == this._autoIncrementStep && dc.AutoIncrementSeed == this._autoIncrementSeed && !(dc.Caption != this._caption) && DataColumnSurrogate.AreDefaultValuesEqual(dc.DefaultValue, this._defaultValue) && dc.MaxLength == this._maxLength && !(dc.Expression != this._expression);
  }

  internal static bool AreDefaultValuesEqual(object o1, object o2)
  {
    if (o1 == null && o2 == null)
      return true;
    return o1 != null && o2 != null && o1.Equals(o2);
  }
}
