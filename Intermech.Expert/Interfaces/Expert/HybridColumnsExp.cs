// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Expert.HybridColumnsExp
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Expert;

/// <summary>Колонки</summary>
[Serializable]
public class HybridColumnsExp : ICloneable
{
  /// <summary>Список колонок</summary>
  private List<HybridColumnsExp.HybridColumnExp> _columns;
  /// <summary>Индекс по имени</summary>
  private HybridColumnsExp.IndexCache _indexCache;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="columns"></param>
  public HybridColumnsExp(DataColumnCollection columns)
  {
    int count = columns.Count;
    this._columns = new List<HybridColumnsExp.HybridColumnExp>();
    this._indexCache = new HybridColumnsExp.IndexCache(count);
    for (int index = 0; index < count; ++index)
    {
      DataColumn column = columns[index];
      string columnName = column.ColumnName;
      this._columns.Add(new HybridColumnsExp.HybridColumnExp(columnName, column.DataType));
      this._indexCache.Add(columnName, index);
    }
  }

  public HybridColumnsExp()
  {
    this._columns = new List<HybridColumnsExp.HybridColumnExp>();
    this._indexCache = new HybridColumnsExp.IndexCache(10);
  }

  public HybridColumnsExp(HybridColumnsExp other)
    : this()
  {
    for (int index = 0; index < other.Count; ++index)
    {
      HybridColumnsExp.HybridColumnExp hybridColumnExp = other[index];
      string columnName = hybridColumnExp.ColumnName;
      this._columns.Add(new HybridColumnsExp.HybridColumnExp(columnName, hybridColumnExp.DataType));
      if (!this._indexCache.ContainsKey(columnName))
        this._indexCache.Add(hybridColumnExp.ColumnName, index);
    }
  }

  /// <summary>Индекс колонки в коллекции</summary>
  /// <param name="columnName">Название</param>
  /// <returns></returns>
  public HybridColumnsExp.HybridColumnExp this[string columnName]
  {
    get => this._columns[this.GetIndexByName(columnName)];
  }

  /// <summary>Колонка</summary>
  /// <param name="index">Индекс в коллекции</param>
  /// <returns></returns>
  public HybridColumnsExp.HybridColumnExp this[int index]
  {
    [DebuggerStepThrough] get => this._columns[index];
    set => this._columns[index] = value;
  }

  /// <summary>Получение индекса колонки по имени</summary>
  /// <param name="columnName"></param>
  public int GetIndexByName(string columnName) => this._indexCache.Get(columnName);

  /// <summary>
  /// Возвращает, есть такой столбец или нет - чисто для красоты
  /// </summary>
  /// <param name="columnName">Имя столбца</param>
  /// <returns>true, если столбец с таким именем есть в таблице, иначе false</returns>
  public bool Contains(string columnName) => this._indexCache.ContainsKey(columnName);

  /// <summary>Добавить столбец в коллекцию</summary>
  /// <param name="col">Описание добавляемого столбца</param>
  /// <returns>true, если был добавлен. Иначе, скорее всего, такой уже был</returns>
  public bool Add(HybridColumnsExp.HybridColumnExp col)
  {
    if (this.Contains(col.ColumnName))
      return false;
    this._columns.Add(col);
    this._indexCache.Add(col.ColumnName, this._columns.Count - 1);
    return true;
  }

  public bool Add(string columnName, Type valType)
  {
    if (this.Contains(columnName))
      return false;
    HybridColumnsExp.HybridColumnExp hybridColumnExp = new HybridColumnsExp.HybridColumnExp(columnName, valType);
    this._columns.Add(hybridColumnExp);
    this._indexCache.Add(hybridColumnExp.ColumnName, this._columns.Count - 1);
    return true;
  }

  /// <summary>
  /// Специально для HybridRow. Добавить столбец даже если такой уже есть! Чтобы не нарушалась нумерация столбцов во второй строке (row2)
  /// </summary>
  /// <param name="col">Добавляемый столбец</param>
  public void AddDuplicate(HybridColumnsExp.HybridColumnExp col)
  {
    if (!this.Contains(col.ColumnName))
      this._indexCache.Add(col.ColumnName, this._columns.Count);
    this._columns.Add(col);
  }

  /// <summary>Количество колонок</summary>
  public int Count
  {
    [DebuggerStepThrough] get => this._columns.Count;
  }

  /// <summary>
  /// 
  /// </summary>
  protected internal void Clear()
  {
    this._columns.Clear();
    this._indexCache.Clear();
  }

  public object Clone() => (object) new HybridColumnsExp(this);

  /// <summary>Класс для описания колонки</summary>
  [Serializable]
  /// <summary>Конструктор</summary>
  /// <param name="columnName">Наименование столбца</param>
  /// <param name="dataType">Тип данных</param>
  public struct HybridColumnExp(string columnName, Type dataType)
  {
    /// <summary>Наименование столбца</summary>
    public string ColumnName = columnName;
    /// <summary>Тип данных</summary>
    public Type DataType = dataType;
    /// <summary>
    /// Ид типа атрибута, ассоциированного с этим столбцом (назначается снаружи)
    /// </summary>
    public int attrTypeId = -1;
    /// <summary>Тип данных (назначается снаружи)</summary>
    public FieldTypes fldType = FieldTypes.ftUnknown;
  }

  /// <summary>Кеш для индексов</summary>
  /// <remarks>Для ускорения повторного поиска полей Only</remarks>
  [Serializable]
  internal sealed class IndexCache
  {
    /// <summary>Последний ключ</summary>
    private string _key;
    /// <summary>Последнее значение</summary>
    private int _value = -1;
    /// <summary>Внутренний кеш</summary>
    private Dictionary<string, int> _cache;

    /// <summary>Конструктор</summary>
    /// <param name="capacity"></param>
    public IndexCache(int capacity) => this._cache = new Dictionary<string, int>(capacity);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void Add(string key, int value)
    {
      this._cache.Add(key, value);
      if (!(this._key == key))
        return;
      this._value = value;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public int Get(string key)
    {
      if (this._key == key)
        return this._value;
      int num = -1;
      if (!this._cache.TryGetValue(key, out num))
        num = -1;
      this._key = key;
      this._value = num;
      return num;
    }

    public bool ContainsKey(string Key) => this._cache.ContainsKey(Key);

    public void Clear()
    {
      this._key = (string) null;
      this._value = -1;
      this._cache.Clear();
    }
  }
}
