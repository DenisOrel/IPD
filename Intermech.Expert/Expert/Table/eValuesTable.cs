// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Table.eValuesTable
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Expert.Table;

/// <summary>Класс описатель таблицы со значениями</summary>
[Serializable]
public class eValuesTable : ISerializable, ICloneable
{
  private eCell[,] _cells;

  /// <summary>Конструктор</summary>
  public eValuesTable()
    : this(0, 0)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="rows">Количество рядов</param>
  /// <param name="columns">Количество колонок</param>
  public eValuesTable(int rows, int columns)
  {
    this._cells = new eCell[rows, columns];
    for (int index1 = 0; index1 < rows; ++index1)
    {
      for (int index2 = 0; index2 < columns; ++index2)
        this._cells[index1, index2] = new eCell();
    }
  }

  /// <summary>Доступ к ячейке [row, column]</summary>
  public eCell this[int row, int column]
  {
    get => this._cells[row, column];
    set => this._cells[row, column] = value;
  }

  /// <summary>Доступ к массиву</summary>
  public eCell[,] Array
  {
    get => this._cells;
    set => this._cells = value;
  }

  /// <summary>Копирование одного массива значений в другой</summary>
  /// <param name="source">Исходный массив</param>
  /// <param name="dest">Заполняемый массив</param>
  public static void CopyTo(eCell[,] source, eCell[,] dest)
  {
    int num1 = Math.Max(source.GetLowerBound(0), dest.GetLowerBound(0));
    int num2 = Math.Min(source.GetUpperBound(0), dest.GetUpperBound(0));
    int num3 = Math.Max(source.GetLowerBound(1), dest.GetLowerBound(1));
    int num4 = Math.Min(source.GetUpperBound(1), dest.GetUpperBound(1));
    for (int index1 = num1; index1 <= num2; ++index1)
    {
      for (int index2 = num3; index2 <= num4; ++index2)
        dest[index1, index2] = source[index1, index2];
    }
  }

  /// <summary>Назначение одного массива значений в другой</summary>
  /// <param name="source">Исходный массив</param>
  /// <param name="dest">Заполняемый массив</param>
  public static void AssignTo(eCell[,] source, eCell[,] dest)
  {
    int num1 = Math.Max(source.GetLowerBound(0), dest.GetLowerBound(0));
    int num2 = Math.Min(source.GetUpperBound(0), dest.GetUpperBound(0));
    int num3 = Math.Max(source.GetLowerBound(1), dest.GetLowerBound(1));
    int num4 = Math.Min(source.GetUpperBound(1), dest.GetUpperBound(1));
    for (int index1 = num1; index1 <= num2; ++index1)
    {
      for (int index2 = num3; index2 <= num4; ++index2)
        dest[index1, index2].Assign(source[index1, index2]);
    }
  }

  /// <summary>Обновление размера таблцы</summary>
  /// <param name="rows">к-во рядов</param>
  /// <param name="columns">к-во колонок</param>
  protected void UpdateDimension(int rows, int columns)
  {
    eCell[,] eCellArray = new eCell[rows, columns];
    eValuesTable.CopyTo(this._cells, eCellArray);
    this._cells = new eCell[rows, columns];
    eValuesTable.CopyTo(eCellArray, this._cells);
  }

  /// <summary>Очистка данных</summary>
  public void Clear()
  {
    for (int index1 = 0; index1 < this.RowsCount; ++index1)
    {
      for (int index2 = 0; index2 < this.ColumnsCount; ++index2)
        this._cells[index1, index2].CellValue = (ExpertValue) null;
    }
  }

  /// <summary>Возвращает количество колонок в таблице</summary>
  public int ColumnsCount => this._cells.GetUpperBound(1) - this._cells.GetLowerBound(1) + 1;

  /// <summary>Получение столбца</summary>
  /// <param name="column">номер столбца</param>
  /// <returns>eColumn</returns>
  public eColumn GetColumn(int column) => eValuesTable.GetColumn(column, this._cells);

  /// <summary>Получение столбца из массива source</summary>
  /// <param name="column">номер колонки</param>
  /// <param name="source">исходный массив</param>
  /// <returns>eColumn</returns>
  public static eColumn GetColumn(int column, eCell[,] source)
  {
    eColumn column1 = new eColumn();
    for (int lowerBound = source.GetLowerBound(0); lowerBound <= source.GetUpperBound(0); ++lowerBound)
      column1.Add(source[lowerBound, column]);
    return column1;
  }

  /// <summary>Добавить колонку</summary>
  /// <param name="column">eColumn</param>
  public void AddColumn(eColumn column)
  {
    if (!this.RowsCount.Equals(column.RowsCount))
      throw new ArgumentException(LocalizationHolder.rm.GetString("Expert_4"));
    this.UpdateDimension(this.RowsCount, this.ColumnsCount + 1);
    int upperBound = this._cells.GetUpperBound(1);
    for (int index = 0; index < column.RowsCount; ++index)
      this._cells[index, upperBound] = column[index];
  }

  /// <summary>Вставить колонку</summary>
  /// <param name="index">Индекс для вставки</param>
  /// <param name="column">eColumn</param>
  public void InsertColumn(int index, eColumn column)
  {
    if (index < 0)
      throw new ArgumentException(LocalizationHolder.rm.GetString("Expert_5"));
    if (index >= this.ColumnsCount)
    {
      this.AddColumn(column);
    }
    else
    {
      eCell[,] eCellArray = new eCell[this.RowsCount, this.ColumnsCount];
      eValuesTable.CopyTo(this._cells, eCellArray);
      this._cells = new eCell[this.RowsCount, 0];
      for (int lowerBound = eCellArray.GetLowerBound(1); lowerBound <= eCellArray.GetUpperBound(1); ++lowerBound)
      {
        if (lowerBound.Equals(index))
          this.AddColumn(column);
        this.AddColumn(eValuesTable.GetColumn(lowerBound, eCellArray));
      }
    }
  }

  /// <summary>Удалить колонку</summary>
  /// <param name="index">Индекс колонки</param>
  public void RemoveColumn(int index)
  {
    if (index < 0)
      throw new ArgumentException(LocalizationHolder.rm.GetString("Expert_6"));
    eCell[,] eCellArray = index < this.ColumnsCount ? new eCell[this.RowsCount, this.ColumnsCount] : throw new ArgumentException(LocalizationHolder.rm.GetString("Expert_7"));
    eValuesTable.CopyTo(this._cells, eCellArray);
    this._cells = new eCell[this.RowsCount, 0];
    for (int lowerBound = eCellArray.GetLowerBound(1); lowerBound <= eCellArray.GetUpperBound(1); ++lowerBound)
    {
      if (!lowerBound.Equals(index))
        this.AddColumn(eValuesTable.GetColumn(lowerBound, eCellArray));
    }
  }

  /// <summary>Возвращает количество рядов в таблице</summary>
  public int RowsCount => this._cells.GetUpperBound(0) - this._cells.GetLowerBound(0) + 1;

  /// <summary>Получение ряда</summary>
  /// <param name="row">номер ряда</param>
  /// <returns>eRow</returns>
  public eRow GetRow(int row) => eValuesTable.GetRow(row, this._cells);

  /// <summary>Возвращает ряд из массива source</summary>
  /// <param name="row">номер ряда</param>
  /// <param name="source">исходный массив</param>
  /// <returns>eRow</returns>
  public static eRow GetRow(int row, eCell[,] source)
  {
    eRow row1 = new eRow();
    for (int lowerBound = source.GetLowerBound(1); lowerBound <= source.GetUpperBound(1); ++lowerBound)
      row1.Add(source[row, lowerBound]);
    return row1;
  }

  /// <summary>Добавляет ряд к массиву</summary>
  /// <param name="row">eRow</param>
  public void AddRow(eRow row)
  {
    if (!this.ColumnsCount.Equals(row.ColumnsCount))
      throw new ArgumentException(LocalizationHolder.rm.GetString("Expert_8"));
    this.UpdateDimension(this.RowsCount + 1, this.ColumnsCount);
    int upperBound = this._cells.GetUpperBound(0);
    for (int index = 0; index < row.ColumnsCount; ++index)
      this._cells[upperBound, index] = row[index];
  }

  /// <summary>Вставляет ряд в массив</summary>
  /// <param name="index">куда вставлять (индекс)</param>
  /// <param name="row">eRow</param>
  public void InsertRow(int index, eRow row)
  {
    if (index < 0)
      throw new ArgumentException(LocalizationHolder.rm.GetString("Expert_9"));
    if (index >= this.RowsCount)
    {
      this.AddRow(row);
    }
    else
    {
      eCell[,] eCellArray = new eCell[this.RowsCount, this.ColumnsCount];
      eValuesTable.CopyTo(this._cells, eCellArray);
      this._cells = new eCell[0, this.ColumnsCount];
      for (int lowerBound = eCellArray.GetLowerBound(0); lowerBound <= eCellArray.GetUpperBound(0); ++lowerBound)
      {
        if (lowerBound.Equals(index))
          this.AddRow(row);
        this.AddRow(eValuesTable.GetRow(lowerBound, eCellArray));
      }
    }
  }

  /// <summary>Удаляет ряд из массива</summary>
  /// <param name="index">Индекс ряда</param>
  public void RemoveRow(int index)
  {
    if (index < 0)
      throw new ArgumentException(LocalizationHolder.rm.GetString("Expert_10"));
    eCell[,] eCellArray = index < this.RowsCount ? new eCell[this.RowsCount, this.ColumnsCount] : throw new ArgumentException(LocalizationHolder.rm.GetString("Expert_11"));
    eValuesTable.CopyTo(this._cells, eCellArray);
    this._cells = new eCell[0, this.ColumnsCount];
    for (int lowerBound = eCellArray.GetLowerBound(0); lowerBound <= eCellArray.GetUpperBound(0); ++lowerBound)
    {
      if (!lowerBound.Equals(index))
        this.AddRow(eValuesTable.GetRow(lowerBound, eCellArray));
    }
  }

  /// <summary>десериализация</summary>
  /// <param name="info"></param>
  /// <param name="context"></param>
  protected eValuesTable(SerializationInfo info, StreamingContext context)
  {
    int int32_1 = info.GetInt32(nameof (RowsCount));
    int int32_2 = info.GetInt32(nameof (ColumnsCount));
    this._cells = new eCell[int32_1, int32_2];
    for (int index1 = 0; index1 < int32_1; ++index1)
    {
      for (int index2 = 0; index2 < int32_2; ++index2)
        this._cells[index1, index2] = info.GetValue($"{index1}:{index2}", typeof (eCell)) as eCell;
    }
  }

  /// <summary>сериализация</summary>
  /// <param name="info"></param>
  /// <param name="context"></param>
  public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    info.AddValue("RowsCount", this.RowsCount);
    info.AddValue("ColumnsCount", this.ColumnsCount);
    for (int index1 = 0; index1 < this.RowsCount; ++index1)
    {
      for (int index2 = 0; index2 < this.ColumnsCount; ++index2)
        info.AddValue($"{index1}:{index2}", (object) this._cells[index1, index2]);
    }
  }

  /// <summary>Клонирование</summary>
  /// <returns></returns>
  public object Clone()
  {
    eValuesTable eValuesTable = new eValuesTable(this.RowsCount, this.ColumnsCount);
    for (int index1 = 0; index1 < this.RowsCount; ++index1)
    {
      for (int index2 = 0; index2 < this.ColumnsCount; ++index2)
        eValuesTable._cells[index1, index2] = this._cells[index1, index2].Clone() as eCell;
    }
    return (object) eValuesTable;
  }

  public bool PerformAttrCombine(
    IDBAttributeType fromAttribute,
    IDBAttributeType toAttribute,
    IUserSession session)
  {
    bool flag = false;
    for (int lowerBound1 = this._cells.GetLowerBound(0); lowerBound1 <= this._cells.GetUpperBound(0); ++lowerBound1)
    {
      for (int lowerBound2 = this._cells.GetLowerBound(1); lowerBound2 <= this._cells.GetUpperBound(1); ++lowerBound2)
        flag = this._cells[lowerBound1, lowerBound2].PerformAttrCombine(fromAttribute, toAttribute, session) | flag;
    }
    return flag;
  }
}
