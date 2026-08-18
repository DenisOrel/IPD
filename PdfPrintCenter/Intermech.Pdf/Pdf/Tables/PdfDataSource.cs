// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Tables.PdfDataSource
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Collections;
using System.Data;

#nullable disable
namespace Syncfusion.Pdf.Tables;

internal class PdfDataSource
{
  private Array m_array;
  private DataRow[] m_cachRows;
  private int m_colCount;
  private DataColumn m_dataColumn;
  private DataTable m_dataTable;
  private int m_rowCount;
  private bool m_useSorting;

  private PdfDataSource() => this.m_useSorting = true;

  public PdfDataSource(Array array)
  {
    this.m_useSorting = true;
    if (array == null)
      throw new ArgumentException("Array can'n be null", nameof (array));
    this.m_array = this.IsArrayValid(array, ref this.m_colCount) ? array : throw new ArgumentException("We don't suuport more than one or two dimensions arrays in this context or you array has diiferent length", nameof (array));
    this.m_rowCount = this.m_array.GetLength(0);
  }

  public PdfDataSource(DataColumn column)
  {
    this.m_useSorting = true;
    if (column == null)
      throw new ArgumentNullException("Column can't be null", nameof (column));
    this.m_dataColumn = column.Table != null ? column : throw new ArgumentNullException("Data column must belong to some table", nameof (column));
    this.m_colCount = 1;
    this.m_rowCount = this.m_dataColumn.Table.Rows.Count;
  }

  public PdfDataSource(DataTable table)
  {
    this.m_useSorting = true;
    if (table == null)
      throw new ArgumentNullException("Data table can't be null", nameof (table));
    this.SetTable(table);
  }

  public PdfDataSource(DataView view)
    : this(PdfDataSource.GetTableFromDataView(view))
  {
  }

  public PdfDataSource(DataSet dataSet, string tableName)
    : this(PdfDataSource.GetTableFromDataSet(dataSet, tableName))
  {
  }

  public bool AllowDBNull(int index)
  {
    if (index < 0 || index >= this.GetVisibleColCount())
      throw new IndexOutOfRangeException("The index must be less than columns count ormore or equels than zero");
    bool flag = false;
    if (this.m_dataTable != null)
      flag = this.m_dataTable.Columns[this.GetVisibleIndex(index)].AllowDBNull;
    if (this.m_dataColumn != null)
      flag = this.m_dataColumn.AllowDBNull;
    if (this.m_array != null)
      throw new ArgumentException("Array does not have allowDBNull propety");
    return flag;
  }

  private void dataTable_ColumnChanged(object sender, DataColumnChangeEventArgs e)
  {
    this.RefreshCache();
  }

  private void dataTable_RowChanged(object sender, DataRowChangeEventArgs e) => this.RefreshCache();

  private void dataTable_RowDeleted(object sender, DataRowChangeEventArgs e) => this.RefreshCache();

  public Type GetColumnDataType(int index)
  {
    if (index < 0 || index >= this.GetVisibleColCount())
      throw new IndexOutOfRangeException("The index must be less than columns count ormore or equels than zero");
    Type columnDataType = (Type) null;
    if (this.m_dataTable != null)
      columnDataType = this.m_dataTable.Columns[this.GetVisibleIndex(index)].DataType;
    if (this.m_dataColumn != null)
      columnDataType = this.m_dataColumn.DataType;
    if (this.m_array != null)
      columnDataType = this.GetTypeOfArray(this.m_array);
    return columnDataType;
  }

  public object GetColumnDefaultValue(int index)
  {
    if (index < 0 || index >= this.GetVisibleColCount())
      throw new IndexOutOfRangeException("The index must be less than columns count ormore or equels than zero");
    object columnDefaultValue = (object) null;
    if (this.m_dataTable != null)
      columnDefaultValue = this.m_dataTable.Columns[this.GetVisibleIndex(index)].DefaultValue;
    if (this.m_dataColumn != null)
      columnDefaultValue = this.m_dataColumn.DefaultValue;
    if (this.m_array != null)
      throw new ArgumentException("Array does not have default value propety");
    return columnDefaultValue;
  }

  public MappingType GetColumnMappingType(int index)
  {
    if (index < 0 || index >= this.GetVisibleColCount())
      throw new IndexOutOfRangeException("The index must be less than columns count ormore or equels than zero");
    MappingType columnMappingType = MappingType.Hidden;
    if (this.m_dataTable != null)
      columnMappingType = this.m_dataTable.Columns[this.GetVisibleIndex(index)].ColumnMapping;
    if (this.m_dataColumn != null)
      columnMappingType = this.m_dataColumn.ColumnMapping;
    if (this.m_array != null)
      throw new ArgumentException("Array does not have mapping type propety");
    return columnMappingType;
  }

  private string[] GetColumnsCaptions()
  {
    string[] columnsCaptions = (string[]) null;
    if (this.m_dataTable != null)
    {
      ArrayList arrayList = new ArrayList();
      for (int index = 0; index < this.m_colCount; ++index)
      {
        DataColumn column = this.m_dataTable.Columns[index];
        if (column.ColumnMapping != MappingType.Hidden)
        {
          if (column.Caption != string.Empty)
            arrayList.Add((object) column.Caption);
          else
            arrayList.Add((object) column.ColumnName);
        }
      }
      columnsCaptions = arrayList.ToArray(typeof (string)) as string[];
    }
    if (this.m_dataColumn == null || this.m_dataColumn.ColumnMapping == MappingType.Hidden)
      return columnsCaptions;
    return this.m_dataColumn.Caption != string.Empty ? new string[1]
    {
      this.m_dataColumn.Caption
    } : new string[1]{ this.m_dataColumn.ColumnName };
  }

  private string[] GetColumnsNames()
  {
    string[] columnsNames = (string[]) null;
    if (this.m_dataTable != null)
    {
      ArrayList arrayList = new ArrayList();
      for (int index = 0; index < this.m_colCount; ++index)
      {
        DataColumn column = this.m_dataTable.Columns[index];
        if (column.ColumnMapping != MappingType.Hidden)
          arrayList.Add((object) column.ColumnName);
      }
      columnsNames = arrayList.ToArray(typeof (string)) as string[];
    }
    if (this.m_dataColumn != null && this.m_dataColumn.ColumnMapping != MappingType.Hidden)
      columnsNames = new string[1]
      {
        this.m_dataColumn.ColumnName
      };
    return columnsNames;
  }

  public string[] GetRow(ref int index)
  {
    if (index < 0)
      throw new IndexOutOfRangeException("The index must be less than rows count ormore or equels than zero");
    string[] row = (string[]) null;
    if (index < this.m_rowCount)
    {
      if (this.m_dataTable != null)
        row = this.GetRowFromTable(this.m_dataTable, ref index);
      if (this.m_dataColumn != null)
        row = this.GetRowFromColumn(this.m_dataColumn, ref index);
      if (this.m_array != null)
        row = this.GetRowFromArray(this.m_array, ref index);
    }
    return row;
  }

  private string[] GetRowFromArray(Array array, ref int index)
  {
    string[] rowFromArray;
    switch (array.Rank)
    {
      case 1:
        if (array.GetValue(0) is Array)
        {
          rowFromArray = new string[this.m_colCount];
          Array array1 = array.GetValue(index) as Array;
          for (int index1 = 0; index1 < this.m_colCount; ++index1)
          {
            object obj = array1.GetValue(index1);
            rowFromArray[index1] = Convert.ToString(obj);
          }
          break;
        }
        rowFromArray = new string[this.m_colCount];
        for (int index2 = 0; index2 < this.m_colCount; ++index2)
        {
          object obj = array.GetValue(index);
          rowFromArray[index2] = Convert.ToString(obj);
        }
        break;
      case 2:
        rowFromArray = new string[this.m_colCount];
        for (int index2 = 0; index2 < this.m_colCount; ++index2)
        {
          object obj = array.GetValue(index, index2);
          rowFromArray[index2] = Convert.ToString(obj);
        }
        break;
      default:
        throw new ArgumentException("We don't suuport more than one or two dimensions arrays in this context or you array has diiferent length", nameof (array));
    }
    ++index;
    return rowFromArray;
  }

  private string[] GetRowFromColumn(DataColumn dataColumn, ref int index)
  {
    if (dataColumn.ColumnMapping == MappingType.Hidden)
      throw new ArgumentException("The source is DataColumn, but this column is hidden");
    string[] rowFromColumn;
    if (this.m_useSorting)
    {
      if (this.m_cachRows == null)
        this.m_cachRows = dataColumn.Table.Select();
      rowFromColumn = new string[1]
      {
        Convert.ToString(this.m_cachRows[index][dataColumn.ColumnName])
      };
    }
    else
      rowFromColumn = new string[1]
      {
        Convert.ToString(dataColumn.Table.Rows[index][dataColumn.ColumnName])
      };
    ++index;
    return rowFromColumn;
  }

  private string[] GetRowFromTable(DataTable dataTable, ref int index)
  {
    if (dataTable.Rows.Count <= 0)
      throw new ArgumentException("There is no rows in data source");
    if (index < 0 || index >= dataTable.Rows.Count)
      throw new IndexOutOfRangeException("The index must be less than rows count ormore or equels than zero");
    object[] itemArray;
    if (this.m_useSorting)
    {
      if (this.m_cachRows == null)
        this.m_cachRows = dataTable.Select();
      itemArray = this.m_cachRows[index].ItemArray;
    }
    else
      itemArray = dataTable.Rows[index].ItemArray;
    ArrayList arrayList = new ArrayList();
    int index1 = 0;
    for (int length = itemArray.Length; index1 < length; ++index1)
    {
      if (dataTable.Columns[index1].ColumnMapping != MappingType.Hidden)
        arrayList.Add((object) Convert.ToString(itemArray[index1]));
    }
    ++index;
    return arrayList.ToArray(typeof (string)) as string[];
  }

  private static DataTable GetTableFromDataSet(DataSet dataSet, string tableName)
  {
    if (dataSet == null)
      throw new ArgumentNullException("Data Set can't be null", nameof (dataSet));
    if (dataSet.Tables.Count <= 0)
      throw new ArgumentException("The data set should contain at least one data table", nameof (dataSet));
    if (tableName == null || !(tableName != string.Empty))
      return dataSet.Tables[0];
    if (!dataSet.Tables.Contains(tableName))
      throw new ArgumentNullException("The data set should contain a table with specified table name", tableName);
    return dataSet.Tables[tableName];
  }

  private static DataTable GetTableFromDataView(DataView view)
  {
    return view != null ? view.Table : throw new ArgumentNullException("Data view", nameof (view));
  }

  private Type GetTypeOfArray(Array array)
  {
    switch (array.Rank)
    {
      case 1:
        return !(array.GetValue(0) is Array array1) ? array.GetValue(0).GetType() : array1.GetValue(0).GetType();
      case 2:
        return array.GetValue(0, 0).GetType();
      default:
        return (Type) null;
    }
  }

  private int GetVisibleColCount()
  {
    int visibleColCount = 0;
    if (this.m_dataTable != null)
    {
      for (int index = 0; index < this.m_colCount; ++index)
      {
        if (this.m_dataTable.Columns[index].ColumnMapping != MappingType.Hidden)
          ++visibleColCount;
      }
    }
    if (this.m_dataColumn != null)
      visibleColCount = this.m_dataColumn.ColumnMapping != MappingType.Hidden ? 1 : 0;
    if (this.m_array != null)
      visibleColCount = this.m_colCount;
    return visibleColCount;
  }

  private int GetVisibleIndex(int index)
  {
    if (index < 0 || index >= this.GetVisibleColCount())
      throw new IndexOutOfRangeException("The index must be less than columns count ormore than or equel to zero");
    int visibleIndex = 0;
    if (this.m_dataTable != null)
    {
      int num = index;
      int index1 = 0;
      while (num > -1)
      {
        if (this.m_dataTable.Columns[index1].ColumnMapping == MappingType.Hidden)
        {
          ++index1;
        }
        else
        {
          --num;
          ++index1;
        }
      }
      visibleIndex = index1 - 1;
    }
    if (this.m_dataColumn != null)
    {
      if (this.m_dataColumn.ColumnMapping == MappingType.Hidden)
        throw new ArgumentException("The source is DataColumn, but this column is hidden");
      visibleIndex = 0;
    }
    if (this.m_array != null)
      visibleIndex = index;
    return visibleIndex;
  }

  private bool IsArrayValid(Array array, ref int count)
  {
    bool flag = false;
    switch (array.Rank)
    {
      case 1:
        int num1 = 0;
        if (!(array.GetValue(0) is Array array1))
        {
          int num2;
          count = num2 = num1 + 1;
          return true;
        }
        if (array1.Rank > 1)
          return false;
        int length1 = array1.GetLength(0);
        int index = 1;
        for (int length2 = array.Length; index < length2; ++index)
        {
          int num3 = 0;
          if (array.GetValue(index) is Array array2)
          {
            if (array2.Rank > 1)
              return false;
            num3 = array2.GetLength(0);
          }
          if (length1 != num3)
            return false;
          flag = true;
          count = length1;
        }
        return flag;
      case 2:
        count = array.GetLength(1);
        return true;
      default:
        return false;
    }
  }

  public bool IsColumnReadOnly(int index)
  {
    if (index < 0 || index >= this.GetVisibleColCount())
      throw new IndexOutOfRangeException("The index must be less than columns count ormore than or equal to zero.");
    bool flag = false;
    if (this.m_dataTable != null)
      flag = this.m_dataTable.Columns[this.GetVisibleIndex(index)].ReadOnly;
    if (this.m_dataColumn != null)
      flag = this.m_dataColumn.ReadOnly;
    if (this.m_array != null)
      flag = this.m_array.IsReadOnly;
    return flag;
  }

  private void RefreshCache() => this.m_cachRows = (DataRow[]) null;

  private void SetTable(DataTable table)
  {
    if (table.Columns.Count == 0)
      table.Columns.Add("Col0");
    this.m_dataTable = table;
    this.m_colCount = this.m_dataTable.Columns.Count;
    this.m_rowCount = this.m_dataTable.Rows.Count;
    this.m_dataTable.ColumnChanged += new DataColumnChangeEventHandler(this.dataTable_ColumnChanged);
    this.m_dataTable.RowChanged += new DataRowChangeEventHandler(this.dataTable_RowChanged);
    this.m_dataTable.RowDeleted += new DataRowChangeEventHandler(this.dataTable_RowDeleted);
  }

  public string[] ColumnCaptions => this.GetColumnsCaptions();

  public int ColumnCount => this.GetVisibleColCount();

  public string[] ColumnNames => this.GetColumnsNames();

  public int RowCount => this.m_rowCount;

  internal bool UseSorting
  {
    get => this.m_useSorting;
    set => this.m_useSorting = value;
  }
}
