// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Expert.HybridTableExp
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;

#nullable disable
namespace Intermech.Interfaces.Expert;

/// <summary>Таблица для замены DataTable</summary>
[Serializable]
public class HybridTableExp : ICloneable
{
  /// <summary>Колонки</summary>
  protected HybridColumnsExp _columns;
  /// <summary>Строки</summary>
  protected List<HybridRowExp> _rows;
  /// <summary>
  /// "Имя таблицы" - используется в одном месте для передачи параметра. Практически возрожденный рудимент
  /// </summary>
  protected string tableName = "";
  /// <summary>
  /// Индекс по первому параметру (objId). Key = objId, value = index в _rows
  /// Если не null, то индексирование включено и должно поддерживаться автоматически
  /// </summary>
  protected Dictionary<long, int> _objIdIndex;

  /// <summary>Конструктор для создания новой таблицы</summary>
  public HybridTableExp(bool createEmptyTable = true)
  {
    if (!createEmptyTable)
      return;
    this.Create();
  }

  /// <summary>Конструктор из DataTable</summary>
  /// <param name="table">Исходная таблица</param>
  /// <param name="createEmptyTable">Режим "принудительного" создания пустой таблицы</param>
  /// <param name="makeIndex">Нужно ли создавать индекс</param>
  public HybridTableExp(DataTable table, bool createEmptyTable = true, bool makeIndex = false)
  {
    if (makeIndex)
      this._objIdIndex = new Dictionary<long, int>(table.Rows.Count);
    if (!(!this.Create(table) & createEmptyTable))
      return;
    this.Create();
  }

  /// <summary>Конструктор из DataRow</summary>
  /// <param name="row">Исходный DataRow</param>
  /// <param name="createEmptyTable">Режим "принудительного" создания пустой таблицы</param>
  public HybridTableExp(DataRow row, bool createEmptyTable = true)
  {
    if (!(!this.Create(row) & createEmptyTable))
      return;
    this.Create();
  }

  /// <summary>Конструктор для копирования</summary>
  /// <param name="other">Таблица, из которой копируем</param>
  /// <param name="createEmptyTable">Режим "принудительного" создания пустой таблицы</param>
  /// <param name="makeIndex">Нужно ли создавать индекс</param>
  /// <param name="shallowCopy">Режим копирования по ссылкам (без создания новых объектов)</param>
  public HybridTableExp(
    HybridTableExp other,
    bool createEmptyTable = true,
    bool makeIndex = false,
    bool shallowCopy = false)
  {
    if (makeIndex || other._objIdIndex != null)
      this._objIdIndex = new Dictionary<long, int>(other.RowsCount);
    if (!(!this.Create(other, shallowCopy) & createEmptyTable))
      return;
    this.Create();
  }

  /// <summary>Создание таблицы из DataTable</summary>
  /// <param name="table">Исходная DataTable</param>
  /// <returns>true, если таблица не null</returns>
  public bool Create(DataTable table)
  {
    if (table == null)
      return false;
    this._columns = new HybridColumnsExp(table.Columns);
    this._rows = new List<HybridRowExp>(table.Rows.Count);
    foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
    {
      this._rows.Add(new HybridRowExp(this._columns, row));
      this._CollectIndexRow(this._rows.Count - 1);
    }
    this.tableName = table.TableName;
    return true;
  }

  /// <summary>Создание таблицы из DataRow</summary>
  /// <param name="row">Исходная DataRow</param>
  /// <returns>true, если таблица не null</returns>
  public bool Create(DataRow row)
  {
    if (row == null)
      return false;
    this._columns = new HybridColumnsExp(row.Table.Columns);
    this._rows = new List<HybridRowExp>(1);
    HybridRowExp hybridRowExp = new HybridRowExp(this._columns);
    hybridRowExp.Create(row, true);
    this._rows.Add(hybridRowExp);
    this._CollectIndexRow(0);
    return true;
  }

  /// <summary>Создание таблицы из ничего...</summary>
  public bool Create()
  {
    if (this._columns != null)
      this.Columns.Clear();
    else
      this._columns = new HybridColumnsExp();
    if (this._rows != null)
      this.ClearRows();
    else
      this._rows = new List<HybridRowExp>(1);
    return true;
  }

  /// <summary>Копирование другой таблицы</summary>
  /// <param name="other">Копируемая таблица</param>
  /// <param name="shallowCopy">Режим копирования ссылок (реальные объекты не создаются)</param>
  public bool Create(HybridTableExp other, bool shallowCopy = false)
  {
    if (other == null)
      return false;
    this._columns = (HybridColumnsExp) other._columns.Clone();
    this._rows = new List<HybridRowExp>(other.RowsCount);
    for (int index = 0; index < other.RowsCount; ++index)
    {
      HybridRowExp hybridRowExp;
      if (shallowCopy)
      {
        hybridRowExp = !(other[index] is DoubleLinkRowExp) ? new HybridRowExp(this._columns, other[index]) : other[index];
      }
      else
      {
        hybridRowExp = new HybridRowExp(this._columns);
        hybridRowExp.CopyData(other[index]);
      }
      this._rows.Add(hybridRowExp);
      this._CollectIndexRow(this._rows.Count - 1);
    }
    return true;
  }

  /// <summary>Количество строк в таблице</summary>
  public int RowsCount
  {
    [DebuggerStepThrough] get => this._rows == null ? 0 : this._rows.Count;
  }

  public string TableName
  {
    [DebuggerStepThrough] get => this.tableName;
    [DebuggerStepThrough] set => this.tableName = value;
  }

  /// <summary>Очистка таблицы</summary>
  public void Clear()
  {
    this._columns.Clear();
    this.ClearRows();
  }

  /// <summary>Очистить только строки - столбцы оставить!</summary>
  public void ClearRows()
  {
    this._rows.Clear();
    if (this._objIdIndex == null)
      return;
    this._objIdIndex.Clear();
  }

  /// <summary>Строка</summary>
  /// <param name="index">Индекс</param>
  /// <returns></returns>
  public HybridRowExp this[int index]
  {
    [DebuggerStepThrough] get => this._rows[index];
    [DebuggerStepThrough] set => this._rows[index] = value;
  }

  public List<HybridRowExp> Rows => this._rows;

  /// <summary>Колонки</summary>
  public HybridColumnsExp Columns
  {
    [DebuggerStepThrough] get => this._columns;
    [DebuggerStepThrough] set => this._columns = value;
  }

  /// <summary>Добавить строку</summary>
  /// <param name="row"></param>
  public void Add(DataRow row)
  {
    HybridRowExp hybridRowExp = new HybridRowExp(this._columns);
    hybridRowExp.Create(row);
    this._rows.Add(hybridRowExp);
    this._CollectIndexRow(this._rows.Count - 1);
  }

  /// <summary>
  /// Добавить новую строку (уже заполненную). Не добавляет строки, имеющие другой набор столбцов!
  /// </summary>
  /// <param name="hrow">добавляемая строка</param>
  /// <returns>true, если строка привязана к этой же таблице, и добавление удачно</returns>
  public bool Add(HybridRowExp hrow)
  {
    if (hrow.Columns != this._columns)
      return false;
    this._rows.Add(hrow);
    this._CollectIndexRow(this._rows.Count - 1);
    return true;
  }

  /// <summary>Создать новую пустую строку для внешнего заполнения</summary>
  /// <returns>новая строка</returns>
  public HybridRowExp NewRow() => new HybridRowExp(this._columns);

  /// <summary>Вставить строку по нужному индексу</summary>
  /// <param name="row">Вставляемая строка</param>
  /// <param name="index">Индекс</param>
  public void InsertAt(HybridRowExp row, int index)
  {
    this._rows.Insert(index, row);
    long int64 = Convert.ToInt64(row[0]);
    if (this._objIdIndex == null || this._objIdIndex.ContainsKey(int64))
      return;
    List<long> longList = new List<long>();
    foreach (long key in this._objIdIndex.Keys)
    {
      if (this._objIdIndex[key] >= index)
        longList.Add(key);
    }
    foreach (long key in longList)
      ++this._objIdIndex[key];
    this._objIdIndex.Add(int64, index);
  }

  /// <summary>Удалить строку</summary>
  /// <param name="index">Индекс строки</param>
  public void RemoveAt(int index)
  {
    long int64 = Convert.ToInt64(this._rows[index][0]);
    this._rows.RemoveAt(index);
    if (this._objIdIndex == null)
      return;
    this._objIdIndex.Remove(int64);
  }

  /// <summary>Удалить строку</summary>
  /// <param name="hRow">Строка</param>
  public void Remove(HybridRowExp hRow)
  {
    long int64 = Convert.ToInt64(hRow[0]);
    this._rows.Remove(hRow);
    if (this._objIdIndex == null)
      return;
    this._objIdIndex.Remove(int64);
  }

  /// <summary>
  /// Импортировать в эту таблицу строку из другой таблицы, заполнив все возможные столбцы
  /// </summary>
  /// <param name="hr">Импортируемая строка</param>
  /// <returns>Добавленная строка</returns>
  public HybridRowExp ImportRow(HybridRowExp hr)
  {
    HybridRowExp hybridRowExp = this.NewRow();
    HybridColumnsExp columns = hr.Columns;
    for (int index = 0; index < this._columns.Count; ++index)
    {
      int indexByName = columns.GetIndexByName(this._columns[index].ColumnName);
      if (indexByName >= 0)
        hybridRowExp[index] = hr[indexByName];
    }
    this._rows.Add(hybridRowExp);
    this._CollectIndexRow(this._rows.Count - 1);
    return hybridRowExp;
  }

  /// <summary>
  /// Добавить в эту таблицу ССЫЛКУ на строку из другой таблицы
  /// </summary>
  /// <param name="hr">Добавленная строка</param>
  /// <returns>Добавленная строка</returns>
  public HybridRowExp AddRow(HybridRowExp hr)
  {
    this._rows.Add(hr);
    this._CollectIndexRow(this._rows.Count - 1);
    return hr;
  }

  /// <summary>Импортировать из другой HybridTable</summary>
  /// <param name="otherTable"></param>
  public void ImportTable(HybridTableExp otherTable)
  {
    for (int index = 0; index < otherTable.RowsCount; ++index)
      this.ImportRow(otherTable[index]);
  }

  /// <summary>Выборка всей таблицы в DataTable</summary>
  /// <param name="table">DataTable</param>
  /// <returns></returns>
  public DataTable Select(DataTable table)
  {
    for (int index1 = 0; index1 < this._rows.Count; ++index1)
    {
      DataRow dataRow = table.NewRow();
      HybridRowExp row = this._rows[index1];
      for (int index2 = 0; index2 < this._columns.Count; ++index2)
      {
        string columnName = this._columns[index2].ColumnName;
        dataRow[columnName] = row[index1];
      }
    }
    table.AcceptChanges();
    return table;
  }

  public DataTable ToDataTable()
  {
    DataTable dataTable = new DataTable();
    Dictionary<int, int> dictionary = new Dictionary<int, int>();
    for (int index = 0; index < this._columns.Count; ++index)
    {
      HybridColumnsExp.HybridColumnExp column = this._columns[index];
      string str1 = (string) null;
      if (GuidHelper.IsGuid(column.ColumnName))
      {
        IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(new Guid(column.ColumnName));
        if (attributeType != null)
        {
          string str2 = attributeType.ShortName != "" ? attributeType.ShortName : attributeType.Name;
          str1 = $"[{attributeType.AttributeID}] {str2}";
        }
      }
      if (str1 == null)
        str1 = column.ColumnName;
      Type type = column.DataType;
      if (type == typeof (MeasuredValue))
        type = typeof (string);
      if (!dataTable.Columns.Contains(str1))
      {
        dataTable.Columns.Add(str1, type);
        dictionary.Add(index, dataTable.Columns.Count - 1);
      }
      else
        dictionary.Add(index, dataTable.Columns.IndexOf(str1));
    }
    for (int index1 = 0; index1 < this._rows.Count; ++index1)
    {
      DataRow row1 = dataTable.NewRow();
      HybridRowExp row2 = this._rows[index1];
      for (int index2 = 0; index2 < this._columns.Count; ++index2)
      {
        try
        {
          object obj = row2[index2];
          if (obj != null)
          {
            if (this._columns[index2].DataType == typeof (MeasuredValue))
              obj = (object) obj.ToString();
            if (obj is ArrayHolder)
              obj = (obj as ArrayHolder)[0, 0];
          }
          row1[dictionary[index2]] = obj.IsNullOrDBNull() ? (object) DBNull.Value : obj;
        }
        catch (ArgumentException ex)
        {
          if (this._columns[index2].DataType == typeof (MeasuredValue))
          {
            if (row2[index2].Equals((object) ""))
              row1[dictionary[index2]] = (object) new MeasuredValue(0.0, 0L);
          }
        }
      }
      dataTable.Rows.Add(row1);
    }
    dataTable.AcceptChanges();
    return dataTable;
  }

  /// <summary>Запись в виде XML</summary>
  /// <param name="s"></param>
  public void WriteXml(Stream s)
  {
  }

  public void AddColumn(HybridColumnsExp.HybridColumnExp col)
  {
    if (this._columns.Contains(col.ColumnName))
      return;
    this.AddColumns(new List<HybridColumnsExp.HybridColumnExp>(1)
    {
      col
    });
  }

  public void AddColumn(string colName, Type dataType)
  {
    if (this._columns.Contains(colName))
      return;
    this.AddColumn(new HybridColumnsExp.HybridColumnExp(colName, dataType));
  }

  public void AddColumns(List<HybridColumnsExp.HybridColumnExp> colList)
  {
    for (int index = colList.Count - 1; index >= 0; --index)
    {
      if (this._columns.Contains(colList[index].ColumnName))
        colList.RemoveAt(index);
    }
    for (int index = 0; index < colList.Count; ++index)
      this._columns.Add(colList[index]);
  }

  public void AddColumns(HybridColumnsExp.HybridColumnExp[] colList)
  {
    for (int index = 0; index < colList.Length; ++index)
    {
      if (!this._columns.Contains(colList[index].ColumnName))
        this._columns.Add(colList[index]);
    }
  }

  public void AddColumns(HybridColumnsExp columns)
  {
    for (int index = 0; index < columns.Count; ++index)
    {
      if (!this._columns.Contains(columns[index].ColumnName))
        this._columns.Add(columns[index]);
    }
  }

  public void UpdateColumnsForAllRows()
  {
    foreach (HybridRowExp row in this._rows)
      row.AddNullsForNewColumns();
  }

  /// <summary>Сортировать таблицу.</summary>
  /// <param name="colNumList">Список номеров колонок. Если порядок DESC, номер колонки отрицательный. Нумерация начинается с 1, чтобы избежать проблем с нулем</param>
  public void Sort(List<int> colNumList)
  {
    this._rows.Sort((IComparer<HybridRowExp>) new HybridTableExp.HybridComparer(colNumList));
    if (this._objIdIndex == null)
      return;
    this._objIdIndex.Clear();
    this._CollectIndex();
  }

  /// <summary>
  /// Вернуть сортированный список строк этой таблицы. Сама таблица НЕ МЕНЯЕТСЯ.
  /// </summary>
  /// <param name="colNumList">Список номеров колонок. Если порядок DESC, номер колонки отрицательный. Нумерация начинается с 1, чтобы избежать проблем с нулем</param>
  /// <returns>Сортированный список строк таблицы</returns>
  public List<HybridRowExp> SortIndex(List<int> colNumList)
  {
    List<HybridRowExp> hybridRowExpList = new List<HybridRowExp>();
    for (int index = 0; index < this._rows.Count; ++index)
      hybridRowExpList.Add(this._rows[index]);
    if (colNumList != null && colNumList.Count > 0)
    {
      HybridTableExp.HybridComparer hybridComparer = new HybridTableExp.HybridComparer(colNumList);
      hybridRowExpList.Sort((IComparer<HybridRowExp>) hybridComparer);
    }
    return hybridRowExpList;
  }

  /// <summary>
  /// Отсортировать список строк из ЭТОЙ же таблицы, по заданному набору столбцов
  /// </summary>
  /// <param name="rows">Список строк из этой же таблицы</param>
  /// <param name="colNumList">Список номеров столбцов</param>
  public void SortList(List<HybridRowExp> rows, List<int> colNumList)
  {
    if (colNumList == null || colNumList.Count <= 0)
      return;
    HybridTableExp.HybridComparer hybridComparer = new HybridTableExp.HybridComparer(colNumList);
    rows.Sort((IComparer<HybridRowExp>) hybridComparer);
  }

  private void QuickSort(List<HybridRowExp> rows, int L, int R, HybridTableExp.HybridComparer cf)
  {
    int num1 = L;
    int num2 = R;
    int index = (L + R) / 2;
    while (true)
    {
      while (cf.Compare(rows[num1], rows[index]) >= 0)
      {
        while (cf.Compare(rows[index], rows[num2]) < 0)
          --num2;
        if (num1 <= num2)
        {
          HybridRowExp row = rows[num1];
          rows[num1] = rows[num2];
          rows[num2] = row;
          ++num1;
          --num2;
        }
        if (num1 > num2)
        {
          if (L < num2)
            this.QuickSort(rows, L, num2, cf);
          if (num1 >= R)
            return;
          this.QuickSort(rows, num1, R, cf);
          return;
        }
      }
      ++num1;
    }
  }

  /// <summary>
  /// Вернуть первый индекс, для которого значение столбца colNum равно Value
  /// </summary>
  /// <param name="colNum"></param>
  /// <param name="Value"></param>
  /// <returns></returns>
  public int SelectFirst(int colNum, object Value)
  {
    if (colNum >= this._columns.Count)
      return -1;
    if (this._objIdIndex != null && colNum == 0)
    {
      long int64 = Convert.ToInt64(Value);
      int num = -1;
      this._objIdIndex.TryGetValue(int64, out num);
      return num;
    }
    for (int index = 0; index < this._rows.Count; ++index)
    {
      if (this._rows[index][colNum].Equals(Value))
        return index;
    }
    return -1;
  }

  /// <summary>
  /// Вернуть список индексов, у которых значение столбца colNum равно Value
  /// </summary>
  /// <param name="colNum"></param>
  /// <param name="Value"></param>
  /// <returns></returns>
  public List<int> Select(int colNum, object Value)
  {
    List<int> intList = new List<int>();
    if (colNum >= this._columns.Count)
      return intList;
    for (int index = 0; index < this._rows.Count; ++index)
    {
      if (this._rows[index][colNum].Equals(Value))
        intList.Add(index);
    }
    return intList;
  }

  public HybridRowExp SelectFirstRow(int colNum, object Value)
  {
    if (colNum >= this._columns.Count)
      return (HybridRowExp) null;
    if (this._objIdIndex != null && colNum == 0)
    {
      long int64 = Convert.ToInt64(Value);
      int index = -1;
      this._objIdIndex.TryGetValue(int64, out index);
      return index >= 0 ? this._rows[index] : (HybridRowExp) null;
    }
    for (int index = 0; index < this._rows.Count; ++index)
    {
      if (this._rows[index][colNum].Equals(Value))
        return this._rows[index];
    }
    return (HybridRowExp) null;
  }

  public List<HybridRowExp> SelectRows(int colNum, object Value)
  {
    List<HybridRowExp> hybridRowExpList = new List<HybridRowExp>();
    if (colNum >= this._columns.Count)
      return hybridRowExpList;
    for (int index = 0; index < this._rows.Count; ++index)
    {
      if (this._rows[index][colNum].Equals(Value))
        hybridRowExpList.Add(this._rows[index]);
    }
    return hybridRowExpList;
  }

  public object Clone() => (object) new HybridTableExp(this);

  public object CloneShallow()
  {
    return (object) new HybridTableExp(this, makeIndex: true, shallowCopy: true);
  }

  public HybridTableExp CloneEmpty()
  {
    HybridTableExp hybridTableExp = new HybridTableExp();
    hybridTableExp.Create();
    hybridTableExp.AddColumns(this._columns);
    return hybridTableExp;
  }

  /// <summary>
  /// Объединить эту таблицу с другой. Считаем, что в нулевом столбце ObjectID
  /// </summary>
  /// <param name="other">Другая таблица</param>
  /// <param name="addColumns">Надо ли копировать столбцы</param>
  public void Merge(HybridTableExp other, bool addColumns = false)
  {
    if (addColumns)
    {
      List<HybridColumnsExp.HybridColumnExp> colList = new List<HybridColumnsExp.HybridColumnExp>();
      for (int index = 0; index < other.Columns.Count; ++index)
      {
        if (!this.Columns.Contains(other.Columns[index].ColumnName))
          colList.Add(other.Columns[index]);
      }
      if (colList.Count > 0)
        this.AddColumns(colList);
    }
    if (this._objIdIndex == null)
    {
      this._objIdIndex = new Dictionary<long, int>();
      this._CollectIndex();
    }
    List<int> intList = new List<int>();
    for (int index = 0; index < this._columns.Count; ++index)
    {
      int indexByName = other.Columns.GetIndexByName(this._columns[index].ColumnName);
      intList.Add(indexByName);
    }
    for (int index1 = 0; index1 < other.RowsCount; ++index1)
    {
      HybridRowExp hybridRowExp = other[index1];
      long int64 = Convert.ToInt64(hybridRowExp[0]);
      int index2 = -1;
      HybridRowExp hrow;
      if (this._objIdIndex.TryGetValue(int64, out index2))
      {
        hrow = this._rows[index2];
      }
      else
      {
        hrow = this.NewRow();
        this.Add(hrow);
      }
      for (int index3 = 0; index3 < this._columns.Count; ++index3)
      {
        if (intList[index3] >= 0)
          hrow[index3] = hybridRowExp[intList[index3]];
      }
    }
  }

  /// <summary>
  /// Объединить эту таблицу с другой. Считаем, что в нулевом столбце ObjectID
  /// </summary>
  /// <param name="other">Другая таблица</param>
  public void Merge(DataTable other)
  {
    if (this._objIdIndex == null)
    {
      this._objIdIndex = new Dictionary<long, int>();
      this._CollectIndex();
    }
    List<int> intList = new List<int>();
    for (int index = 0; index < other.Columns.Count; ++index)
    {
      int indexByName = this._columns.GetIndexByName(other.Columns[index].ColumnName);
      intList.Add(indexByName);
    }
    foreach (DataRow row in (InternalDataCollectionBase) other.Rows)
    {
      long int64 = Convert.ToInt64(row[0]);
      int index1 = -1;
      HybridRowExp hrow;
      if (this._objIdIndex.TryGetValue(int64, out index1))
      {
        hrow = this._rows[index1];
      }
      else
      {
        hrow = this.NewRow();
        this.Add(hrow);
      }
      for (int index2 = 0; index2 < intList.Count; ++index2)
      {
        if (intList[index2] >= 0)
          hrow[intList[index2]] = row[index2];
      }
    }
  }

  public bool IndexEnabled
  {
    get => this._objIdIndex != null;
    set
    {
      if (value)
      {
        if (this._objIdIndex != null)
          return;
        this._objIdIndex = new Dictionary<long, int>();
        this._CollectIndex();
      }
      else
        this._objIdIndex = (Dictionary<long, int>) null;
    }
  }

  public bool Contains(long objId)
  {
    return this._objIdIndex != null && this._objIdIndex.ContainsKey(objId);
  }

  public int GetIndexFor(long objId) => this._objIdIndex == null ? -1 : this._objIdIndex[objId];

  protected void _CollectIndex()
  {
    if (this._objIdIndex == null)
      return;
    for (int rowIndex = 0; rowIndex < this._rows.Count; ++rowIndex)
      this._CollectIndexRow(rowIndex);
  }

  protected void _CollectIndexRow(int rowIndex)
  {
    if (this._objIdIndex == null)
      return;
    long int64 = Convert.ToInt64(this._rows[rowIndex][0]);
    if (this._objIdIndex.ContainsKey(int64))
      return;
    this._objIdIndex.Add(int64, rowIndex);
  }

  internal class HybridComparer : IComparer<HybridRowExp>
  {
    private List<int> cnList;

    public HybridComparer(List<int> colNumList) => this.cnList = colNumList;

    private bool IsEmpty(object o) => o == null || o.Equals((object) DBNull.Value);

    public int Compare(HybridRowExp x, HybridRowExp y)
    {
      int num1 = 0;
      for (int index1 = 0; index1 < this.cnList.Count; ++index1)
      {
        bool flag1 = this.cnList[index1] >= 0;
        int index2 = Math.Abs(this.cnList[index1]) - 1;
        HybridColumnsExp.HybridColumnExp column = x.Columns[index2];
        object obj1 = x[index2];
        object obj2 = y[index2];
        bool flag2 = this.IsEmpty(obj1) || column.DataType != typeof (string) && obj1 is string && (string) obj1 == "";
        bool flag3 = this.IsEmpty(obj2) || column.DataType != typeof (string) && obj2 is string && (string) obj2 == "";
        if (flag2 | flag3)
        {
          if (flag2 & flag3)
          {
            num1 = 0;
            break;
          }
          if (flag2)
          {
            num1 = flag1 ? -1 : 1;
            break;
          }
          if (flag3)
          {
            num1 = flag1 ? 1 : -1;
            break;
          }
        }
        if (column.DataType == typeof (long) || column.DataType == typeof (int) || column.DataType == typeof (short) || column.DataType == typeof (Decimal))
        {
          long int64_1 = Convert.ToInt64(obj1);
          long int64_2 = Convert.ToInt64(obj2);
          if (int64_1 > int64_2)
            num1 = flag1 ? 1 : -1;
          if (int64_1 < int64_2)
            num1 = flag1 ? -1 : 1;
          if (num1 != 0)
            break;
        }
        else if (column.DataType == typeof (double) || column.DataType == typeof (float))
        {
          double num2 = Convert.ToDouble(obj1);
          double num3 = Convert.ToDouble(obj2);
          if (num2 - num3 > 1E-07)
            num1 = flag1 ? 1 : -1;
          if (num3 - num2 > 1E-07)
            num1 = flag1 ? -1 : 1;
          if (num1 != 0)
            break;
        }
        else if (column.DataType == typeof (MeasuredValue))
        {
          int num4 = (int) MeasureHelper.Compare((MeasuredValue) obj1, (MeasuredValue) obj2);
          if (num4 == 2)
            num1 = flag1 ? -1 : 1;
          if (num4 == 1)
            num1 = flag1 ? 1 : -1;
          if (num1 != 0)
            break;
        }
        else if (column.DataType == typeof (DateTime))
        {
          num1 = ((DateTime) obj1).CompareTo((DateTime) obj2);
          if (!flag1)
            num1 = -num1;
          if (num1 != 0)
            break;
        }
        else
        {
          num1 = string.Compare(Convert.ToString(obj1), Convert.ToString(obj2));
          if (!flag1)
            num1 = -num1;
          if (num1 != 0)
            break;
        }
      }
      return num1;
    }
  }
}
