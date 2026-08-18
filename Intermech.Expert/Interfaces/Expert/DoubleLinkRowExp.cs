// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Expert.DoubleLinkRowExp
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Expert;

/// <summary>
/// Класс, хранящий ССЫЛКИ на два обычных HybridRowExp. Не должен дублировать данные (_data и addData всегда null)
/// </summary>
public class DoubleLinkRowExp : HybridRowExp
{
  internal HybridRowExp row1;
  internal HybridRowExp row2;

  /// <summary>Конструктор</summary>
  /// <param name="columns">Колонки (копируется только ссылка)</param>
  /// <param name="Row1">Первая строка</param>
  /// <param name="Row2">Вторая строка</param>
  public DoubleLinkRowExp(HybridColumnsExp columns, HybridRowExp Row1, HybridRowExp Row2)
  {
    this._columns = columns;
    this.row1 = Row1;
    this.row2 = Row2;
  }

  /// <summary>Элемент строки</summary>
  /// <param name="columnName">Название колонки</param>
  /// <returns></returns>
  public override object this[string columnName]
  {
    get
    {
      int indexByName = this._columns.GetIndexByName(columnName);
      if (indexByName < 0)
        return (object) DBNull.Value;
      if (indexByName < this.row1.Columns.Count)
        return this.row1[indexByName];
      int index1 = indexByName - this.row1.Columns.Count;
      if (index1 < this.row2.Columns.Count)
        return this.row2[index1];
      int index2 = index1 - this.row2.Columns.Count;
      return this.addData != null && index2 < this.addData.Count ? this.addData[index2] : (object) DBNull.Value;
    }
  }

  /// <summary>Элемент строки</summary>
  /// <param name="index">Индекс</param>
  /// <returns></returns>
  public override object this[int index]
  {
    get
    {
      if (index < this.row1.Columns.Count)
        return this.row1[index];
      index -= this.row1.Columns.Count;
      if (index < this.row2.Columns.Count)
        return this.row2[index];
      index -= this.row2.Columns.Count;
      return this.addData != null && index < this.addData.Count ? this.addData[index] : (object) DBNull.Value;
    }
    set
    {
      if (index < this.row1.Columns.Count)
      {
        this.row1[index] = value;
      }
      else
      {
        index -= this.row1.Columns.Count;
        if (index < this.row2.Columns.Count)
        {
          this.row2[index] = value;
        }
        else
        {
          index -= this.row2.Columns.Count;
          if (this.addData == null)
            this.addData = new List<object>();
          while (this.addData.Count <= index)
            this.addData.Add((object) DBNull.Value);
          this.addData[index] = value;
        }
      }
    }
  }

  public override int GetColIndexByName(string name)
  {
    int indexByName = this.row1.Columns.GetIndexByName(name);
    if (indexByName >= 0)
      return indexByName;
    int colIndexByName = this.row2.GetColIndexByName(name);
    return colIndexByName < 0 ? colIndexByName : this.row1.Columns.Count + colIndexByName;
  }
}
