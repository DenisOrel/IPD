// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Table.eRow
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

/// <summary>
/// Класс-описатель строки ячеек
/// (набор ячеек)
/// </summary>
[Serializable]
public class eRow : IEnumerable, ISerializable, ICloneable
{
  private List<eCell> _indexes = new List<eCell>();
  private eCell _headerCell;

  /// <summary>Конструктор</summary>
  public eRow()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="count">Количестов ячеек в ряде</param>
  /// <param name="cellDest">Назначение ячеек</param>
  /// <param name="cellHolder">Описатель ячеек</param>
  public eRow(int count, eCellDestination cellDest, CommonTypeHolder cellHolder)
  {
    for (int index = 0; index < count; ++index)
      this.Add(new eCell(cellDest, cellHolder));
  }

  /// <summary>Заголовок ряда</summary>
  public eCell Header
  {
    get => this._headerCell;
    set => this._headerCell = value;
  }

  /// <summary>Доступ к ячейке с индексом index</summary>
  public eCell this[int index]
  {
    get
    {
      int indexesIndex = this.GetIndexesIndex(index);
      return indexesIndex < 0 ? (eCell) null : this._indexes[indexesIndex];
    }
    set
    {
      int indexesIndex = this.GetIndexesIndex(index);
      if (indexesIndex < 0)
        return;
      this._indexes[indexesIndex] = value;
    }
  }

  /// <summary>
  /// Возвращает первое вхождение ячейки в ряд
  /// используется если ColSpan&gt;1
  /// </summary>
  /// <param name="index">Номер ячейки для проверки</param>
  /// <returns>Индекс первой ячейки</returns>
  protected int GetFirstEntry(int index)
  {
    eCell index1 = index < this._indexes.Count ? this._indexes[index] : (eCell) null;
    if (index1 == null || index1.ColSpan <= 1 || index <= 0)
      return index;
    int index2 = index;
    eCell index3 = this._indexes[index2];
    do
      ;
    while (!object.Equals((object) this._indexes[index2--], (object) index1));
    int firstEntry = index2;
    int num = firstEntry + 1;
    return firstEntry;
  }

  /// <summary>
  /// Вернуть индекс в Indexes для столбца номер Index (c учетом всех ColSpan'ов)
  /// </summary>
  /// <param name="Index">Номер столбца</param>
  /// <returns>Номер индекса в _indexes</returns>
  protected int GetIndexesIndex(int Index)
  {
    int num = 0;
    for (int index1 = 0; index1 < this._indexes.Count; ++index1)
    {
      eCell index2 = this._indexes[index1];
      if (Index >= num && Index < num + index2.ColSpan)
        return index1;
      num += index2.ColSpan;
    }
    return -1;
  }

  /// <summary>Добавить ячейку к ряду</summary>
  /// <param name="cell"></param>
  public void Add(eCell cell)
  {
    if (cell.ColSpan > 1)
    {
      for (int index = 1; index <= cell.ColSpan; ++index)
        this._indexes.Add(cell);
    }
    else
      this._indexes.Add(cell);
  }

  /// <summary>Вставить ячейку</summary>
  /// <param name="index">Номер ячейки</param>
  /// <param name="cell">eCell</param>
  public void Insert(int index, eCell cell)
  {
    int indexesIndex = this.GetIndexesIndex(index);
    if (indexesIndex >= 0)
    {
      eCell index1 = this._indexes[indexesIndex];
      if (index1.ColSpan > 1 || index1.CommonType == null && cell.CommonType == null)
        ++index1.ColSpan;
      else
        this._indexes.Insert(indexesIndex, cell);
    }
    else
      this._indexes.Add(cell);
  }

  /// <summary>Удалить ячейку</summary>
  /// <param name="index">Номер ячейки</param>
  public void Remove(int index)
  {
    int indexesIndex = this.GetIndexesIndex(index);
    if (indexesIndex < 0)
      return;
    eCell index1 = this._indexes[indexesIndex];
    if (index1.ColSpan > 1)
      --index1.ColSpan;
    else
      this._indexes.RemoveAt(indexesIndex);
  }

  /// <summary>Очистить ряд от ячеек</summary>
  public void Clear() => this._indexes.Clear();

  /// <summary>Возвращает количество ячеек в ряду</summary>
  public int ColumnsCount => this._indexes.Count;

  /// <summary>Enumerator</summary>
  /// <returns></returns>
  public IEnumerator GetEnumerator() => (IEnumerator) this._indexes.GetEnumerator();

  /// <summary>десериализация</summary>
  /// <param name="info"></param>
  /// <param name="context"></param>
  protected eRow(SerializationInfo info, StreamingContext context)
  {
    this._indexes = info.GetValue("Array", typeof (List<eCell>)) as List<eCell>;
    this._headerCell = info.GetValue(nameof (Header), typeof (eCell)) as eCell;
  }

  /// <summary>сериализация</summary>
  /// <param name="info"></param>
  /// <param name="context"></param>
  public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    info.AddValue("Version", ExpertConsts.TableVersion);
    info.AddValue("Array", (object) this._indexes);
    info.AddValue("Header", (object) this._headerCell);
  }

  /// <summary>Клонирование</summary>
  /// <returns></returns>
  public object Clone()
  {
    eRow eRow = new eRow();
    eRow._headerCell = this._headerCell != null ? this._headerCell.Clone() as eCell : (eCell) null;
    foreach (eCell index in this._indexes)
      eRow._indexes.Add(index.Clone() as eCell);
    return (object) eRow;
  }

  public bool PerformAttrCombine(
    IDBAttributeType fromAttribute,
    IDBAttributeType toAttribute,
    IUserSession session)
  {
    bool flag = false;
    if (this._headerCell != null)
      flag = this._headerCell.PerformAttrCombine(fromAttribute, toAttribute, session);
    foreach (eCell index in this._indexes)
      flag = index.PerformAttrCombine(fromAttribute, toAttribute, session) | flag;
    return flag;
  }
}
