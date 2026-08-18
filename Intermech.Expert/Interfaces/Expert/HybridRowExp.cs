// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Expert.HybridRowExp
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Interfaces.Expert;

/// <summary>Строка в таблице</summary>
[Serializable]
public class HybridRowExp
{
  protected HybridColumnsExp _columns;
  protected object[] _data;
  protected List<object> addData;

  /// <summary>Конструктор для DoubleLinkRowExp</summary>
  protected HybridRowExp() => this._data = (object[]) null;

  /// <summary>Конструктор</summary>
  /// <param name="columns">Колонки</param>
  public HybridRowExp(HybridColumnsExp columns)
  {
    this._columns = columns;
    this._data = new object[columns.Count];
    this.FillUnknown();
  }

  /// <summary>Конструктор для копирования DataRow</summary>
  /// <param name="columns"></param>
  /// <param name="row">Копируемая строка</param>
  public HybridRowExp(HybridColumnsExp columns, DataRow row)
  {
    this._columns = columns;
    if (row != null)
    {
      this._data = row.ItemArray;
    }
    else
    {
      this._data = new object[columns.Count];
      this.FillUnknown();
    }
  }

  /// <summary>
  /// Конструктор для копирования HybridRowExp БЕЗ пересоздания данных (Shallow copy)
  /// </summary>
  /// <param name="columns"></param>
  /// <param name="row">Копируемая строка</param>
  public HybridRowExp(HybridColumnsExp columns, HybridRowExp row)
  {
    this._columns = columns;
    if (row != null)
    {
      this._data = row._data;
      if (row.addData == null)
        return;
      this.addData = new List<object>();
      foreach (object obj in row.addData)
        this.addData.Add(obj);
    }
    else
    {
      this._data = new object[columns.Count];
      this.FillUnknown();
    }
  }

  /// <summary>Создание строки</summary>
  /// <param name="row">DataRow</param>
  /// <param name="fullCopyMode">Режим "полного" копирования</param>
  public void Create(DataRow row, bool fullCopyMode = false)
  {
    if (row == null)
      return;
    if (fullCopyMode)
    {
      this._data = row.ItemArray;
    }
    else
    {
      this.FillUnknown();
      int count = this._columns.Count;
      this._data = new object[count];
      for (int index = 0; index < count; ++index)
        this._data[index] = row[this._columns[index].ColumnName];
    }
  }

  /// <summary>Элемент строки</summary>
  /// <param name="columnName">Название колонки</param>
  /// <returns></returns>
  public virtual object this[string columnName]
  {
    get
    {
      int indexByName = this._columns.GetIndexByName(columnName);
      if (indexByName < 0)
        return (object) DBNull.Value;
      if (indexByName < this._data.Length)
        return this._data[indexByName];
      int index = indexByName - this._data.Length;
      return this.addData == null || index >= this.addData.Count ? (object) DBNull.Value : this.addData[index];
    }
    set
    {
      int indexByName = this._columns.GetIndexByName(columnName);
      if (indexByName < 0)
        return;
      if (indexByName < this._data.Length)
      {
        this._data[indexByName] = value;
      }
      else
      {
        int index = indexByName - this._data.Length;
        if (this.addData == null)
          this.addData = new List<object>();
        while (this.addData.Count <= index)
          this.addData.Add((object) DBNull.Value);
        this.addData[index] = value;
      }
    }
  }

  /// <summary>Элемент строки</summary>
  /// <param name="index">Индекс</param>
  /// <returns></returns>
  public virtual object this[int index]
  {
    get
    {
      if (index < this._data.Length)
        return this._data[index];
      index -= this._data.Length;
      return this.addData == null || index >= this.addData.Count ? (object) DBNull.Value : this.addData[index];
    }
    set
    {
      if (index < this._data.Length)
      {
        this._data[index] = value;
      }
      else
      {
        index -= this._data.Length;
        if (this.addData == null)
          this.addData = new List<object>();
        while (this.addData.Count <= index)
          this.addData.Add((object) DBNull.Value);
        this.addData[index] = value;
      }
    }
  }

  /// <summary>Колонки</summary>
  public HybridColumnsExp Columns => this._columns;

  /// <summary>Возвращает строку в виде DataRow</summary>
  public DataRow AsDataRow
  {
    get
    {
      DataTable dataTable = new DataTable();
      int count1 = this._columns.Count;
      for (int index = 0; index < count1; ++index)
      {
        HybridColumnsExp.HybridColumnExp column = this._columns[index];
        dataTable.Columns.Add(new DataColumn(column.ColumnName, column.DataType));
      }
      int count2 = dataTable.Columns.Count;
      DataRow asDataRow = dataTable.NewRow();
      for (int index = 0; index < count2; ++index)
      {
        string columnName = dataTable.Columns[index].ColumnName;
        asDataRow[index] = this[columnName];
      }
      return asDataRow;
    }
  }

  /// <summary>
  /// Скопировать только данные, shallow copy без проверки совпадения столбцов. Используется при дублировании HybridTable
  /// </summary>
  /// <param name="other"></param>
  internal void CopyData(HybridRowExp other)
  {
    this._data = new object[other._data.Length];
    for (int index = 0; index < other._data.Length; ++index)
      this._data[index] = other._data[index];
    if (other.addData == null)
    {
      if (this.addData == null)
        return;
      this.addData.Clear();
    }
    else
    {
      if (this.addData == null)
        this.addData = new List<object>(other.addData.Count);
      else
        this.addData.Clear();
      for (int index = 0; index < other.addData.Count; ++index)
        this.addData.Add(other.addData[index]);
    }
  }

  public HybridTableExp CloneEmptyTable()
  {
    HybridTableExp hybridTableExp = new HybridTableExp();
    hybridTableExp.Create();
    hybridTableExp.AddColumns(this._columns);
    return hybridTableExp;
  }

  public void PurgeData()
  {
    if (this._data != null)
    {
      for (int index = 0; index < this._data.Length; ++index)
        this._data[index] = (object) DBNull.Value;
    }
    if (this.addData == null)
      return;
    this.addData.Clear();
  }

  /// <summary>
  /// Добавить новые значения для строки. Обычно вызывается после добавления новых столбцов в таблицу.
  /// </summary>
  public void AddNullsForNewColumns()
  {
    if (this._data.Length >= this._columns.Count)
      return;
    object[] objArray = new object[this._columns.Count];
    this._data.CopyTo((Array) objArray, 0);
    this._data = objArray;
  }

  public virtual int GetColIndexByName(string name) => this.Columns.GetIndexByName(name);

  /// <summary>Заполнить всю строку Unknown.Instance</summary>
  internal void FillUnknown()
  {
    for (int index = 0; index < this._data.Length; ++index)
      this._data[index] = (object) Unknown.Value;
  }
}
