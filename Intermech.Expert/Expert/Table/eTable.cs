// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Table.eTable
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Expert.Table;

/// <summary>Класс-описатель таблицы (всей)</summary>
[Serializable]
public class eTable : ISerializable, ICloneable
{
  private List<eColumn> _fixedColumns = new List<eColumn>();
  private List<eRow> _fixedRows = new List<eRow>();
  private Hashtable _columnsWidth = new Hashtable();
  private Hashtable _rowsHeight = new Hashtable();
  private eValuesTable _valuesTable = new eValuesTable();
  private string _name = string.Empty;
  private eTableType _type = eTableType.SingleEntry;
  private List<CommonTypeHolder> _result = new List<CommonTypeHolder>();

  /// <summary>Конструктор</summary>
  /// <param name="type">Тип таблицы</param>
  public eTable(eTableType type) => this._type = type;

  /// <summary>Имя таблицы</summary>
  public string Name
  {
    get => this._name;
    set => this._name = value;
  }

  /// <summary>Список фиксированных столбцов</summary>
  public IList<eColumn> FixedColumns => (IList<eColumn>) this._fixedColumns;

  /// <summary>Список фиксированных строк</summary>
  public IList<eRow> FixedRows => (IList<eRow>) this._fixedRows;

  /// <summary>Таблицы значений</summary>
  public eValuesTable ValuesTable
  {
    get => this._valuesTable;
    set => this._valuesTable = value;
  }

  /// <summary>Количество строк (общее)</summary>
  public int RowsCount => this._valuesTable.RowsCount + this._fixedRows.Count;

  /// <summary>Количество столбцов (общее)</summary>
  public int ColumnsCount => this._valuesTable.ColumnsCount + this._fixedColumns.Count;

  /// <summary>Установить ширину столбца</summary>
  /// <param name="index">Индекс столбца</param>
  /// <param name="width">Ширина столбца</param>
  public void SetColumnWidth(int index, int width)
  {
    this._columnsWidth[(object) index] = (object) width;
  }

  /// <summary>Получить ширину столбца</summary>
  /// <param name="index">Индекс столбца</param>
  /// <returns>Ширина столбца</returns>
  public int GetColumnWidth(int index)
  {
    object obj = this._columnsWidth[(object) index];
    return obj != null && obj.GetType().Equals(typeof (int)) ? Convert.ToInt32(obj) : 0;
  }

  /// <summary>Установить высоту строки</summary>
  /// <param name="index">Индекс строки</param>
  /// <param name="height">Высота строки</param>
  public void SetRowHeight(int index, int height)
  {
    this._rowsHeight[(object) index] = (object) height;
  }

  /// <summary>Получить высоту строки</summary>
  /// <param name="index">Индекс строки</param>
  /// <returns>Высота строки</returns>
  public int GetRowHeight(int index)
  {
    object obj = this._rowsHeight[(object) index];
    return obj != null && obj.GetType().Equals(typeof (int)) ? Convert.ToInt32(obj) : 0;
  }

  /// <summary>Список объектов результатов</summary>
  public CommonTypeHolder[] Result
  {
    get => this._result.ToArray();
    set
    {
      this._result.Clear();
      this._result.AddRange((IEnumerable<CommonTypeHolder>) value);
    }
  }

  /// <summary>Тип таблицы</summary>
  public eTableType TableType => this._type;

  /// <summary>Получение ячейки из таблицы (из всей)</summary>
  /// <param name="row">Номер ряда</param>
  /// <param name="column">Ромер колонки</param>
  /// <returns>eCell если нашло, иначе null</returns>
  public eCell GetCell(int row, int column)
  {
    int num1 = row - this._fixedRows.Count;
    int num2 = column - this._fixedColumns.Count;
    if (column < this._fixedColumns.Count && num1 >= -1)
    {
      eColumn fixedColumn = this._fixedColumns[column];
      if (fixedColumn != null)
      {
        if (num1.Equals(-1))
          return fixedColumn.Header;
        return num1 < fixedColumn.RowsCount ? fixedColumn[num1] : (eCell) null;
      }
    }
    if (row < this._fixedRows.Count && num2 >= -1)
    {
      eRow fixedRow = this._fixedRows[row];
      if (fixedRow != null)
      {
        if (num2.Equals(-1))
          return fixedRow.Header;
        return num2 < fixedRow.ColumnsCount ? fixedRow[num2] : (eCell) null;
      }
    }
    return num2 >= 0 && num2 < this._valuesTable.ColumnsCount && num1 >= 0 && num1 < this._valuesTable.RowsCount ? this._valuesTable[num1, num2] : (eCell) null;
  }

  /// <summary>Установить ячейку в таблицу</summary>
  /// <param name="row">ряд</param>
  /// <param name="column">колонка</param>
  /// <param name="cell">ячейка</param>
  public void SetCell(int row, int column, eCell cell)
  {
    int num1 = row - this._fixedRows.Count;
    int num2 = column - this._fixedColumns.Count;
    if (row < this._fixedRows.Count)
    {
      eRow fixedRow = this._fixedRows[row];
      if (fixedRow != null)
      {
        if (num2.Equals(-1) && fixedRow.Header != null)
          fixedRow.Header = cell;
        else if (num2 >= 0)
          fixedRow[num2] = cell;
      }
    }
    if (column < this._fixedColumns.Count)
    {
      eColumn fixedColumn = this._fixedColumns[column];
      if (fixedColumn != null)
      {
        if (num1.Equals(-1) && fixedColumn.Header != null)
          fixedColumn.Header = cell;
        else if (num1 >= 0)
          fixedColumn[num1] = cell;
      }
    }
    if (num2 < 0 || num2 >= this._valuesTable.ColumnsCount || num1 < 0 || num1 >= this._valuesTable.RowsCount)
      return;
    this._valuesTable[num1, num2] = cell;
  }

  /// <summary>десериализация</summary>
  /// <param name="info"></param>
  /// <param name="context"></param>
  protected eTable(SerializationInfo info, StreamingContext context)
  {
    if (info.GetInt32("Version") < 100)
      return;
    this._name = info.GetString(nameof (Name));
    this._fixedRows = info.GetValue(nameof (FixedRows), typeof (List<eRow>)) as List<eRow>;
    this._fixedColumns = info.GetValue(nameof (FixedColumns), typeof (List<eColumn>)) as List<eColumn>;
    this._rowsHeight = info.GetValue("RowsHeight", typeof (Hashtable)) as Hashtable;
    this._columnsWidth = info.GetValue("ColumnsWidth", typeof (Hashtable)) as Hashtable;
    this._valuesTable = info.GetValue("Values", typeof (eValuesTable)) as eValuesTable;
    this._type = (eTableType) EnumTypeHelper.GetEnumValue(typeof (eTableType), info.GetString(nameof (TableType)), (object) eTableType.SingleEntry);
    this._result = info.GetValue(nameof (Result), typeof (List<CommonTypeHolder>)) as List<CommonTypeHolder>;
  }

  /// <summary>сериализация</summary>
  /// <param name="info"></param>
  /// <param name="context"></param>
  public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    info.AddValue("Version", ExpertConsts.TableVersion);
    info.AddValue("Name", (object) this._name);
    info.AddValue("FixedRows", (object) this._fixedRows);
    info.AddValue("FixedColumns", (object) this._fixedColumns);
    info.AddValue("RowsHeight", (object) this._rowsHeight);
    info.AddValue("ColumnsWidth", (object) this._columnsWidth);
    info.AddValue("Values", (object) this._valuesTable);
    info.AddValue("TableType", (object) EnumTypeHelper.GetCaption((Enum) this._type));
    info.AddValue("Result", (object) this._result);
  }

  /// <summary>Клонирование</summary>
  /// <returns></returns>
  public object Clone()
  {
    eTable eTable = new eTable(this._type);
    foreach (eColumn fixedColumn in this._fixedColumns)
      eTable._fixedColumns.Add(fixedColumn.Clone() as eColumn);
    foreach (eRow fixedRow in this._fixedRows)
      eTable._fixedRows.Add(fixedRow.Clone() as eRow);
    eTable._valuesTable = this._valuesTable.Clone() as eValuesTable;
    foreach (DictionaryEntry dictionaryEntry in this._columnsWidth)
      eTable._columnsWidth[dictionaryEntry.Key] = dictionaryEntry.Value;
    foreach (DictionaryEntry dictionaryEntry in this._rowsHeight)
      eTable._rowsHeight[dictionaryEntry.Key] = dictionaryEntry.Value;
    foreach (CommonTypeHolder commonTypeHolder in this._result)
      eTable._result.Add(commonTypeHolder.Clone() as CommonTypeHolder);
    eTable._name = this._name;
    return (object) eTable;
  }

  public bool PerformAttrCombine(
    IDBAttributeType fromAttribute,
    IDBAttributeType toAttribute,
    IUserSession session)
  {
    bool flag = false;
    foreach (eColumn fixedColumn in this._fixedColumns)
      flag = fixedColumn.PerformAttrCombine(fromAttribute, toAttribute, session) | flag;
    foreach (eRow fixedRow in this._fixedRows)
      flag = fixedRow.PerformAttrCombine(fromAttribute, toAttribute, session) | flag;
    return this._valuesTable.PerformAttrCombine(fromAttribute, toAttribute, session) | flag;
  }
}
