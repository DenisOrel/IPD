// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.CellNodesEnumerator
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Enumerator для перемещения по ячейкам (RectangleElement) в таблице.
/// Пропускает все узлы не являющиеся RectangleElement</summary>
public class CellNodesEnumerator : 
  IEnumerator<RectangleElement>,
  IDisposable,
  IEnumerator,
  IEnumerable<RectangleElement>,
  IEnumerable
{
  private TableData table;
  private RectangleElement currentCell;
  private int currentCellIndex = -1;

  /// <summary>Конструктор</summary>
  /// <param name="table">Таблица, владелец данных</param>
  public CellNodesEnumerator(TableData table) => this.table = table;

  /// <summary>Индекс текущей ячейки в родительском объекте</summary>
  public int CurrentCellIndex
  {
    [DebuggerStepThrough] get => this.currentCellIndex;
  }

  /// <summary>Главная таблица по которой осуществляется проход</summary>
  public TableData Table => this.table;

  public RectangleElement Current
  {
    get
    {
      if (this.currentCellIndex == -1)
        throw new InvalidOperationException(LocalizationHolder.rm.GetString("Interfaces.Document_152"));
      return this.currentCell != null ? this.currentCell : throw new InvalidOperationException(LocalizationHolder.rm.GetString("Interfaces.Document_153"));
    }
  }

  public void Dispose()
  {
    this.table = (TableData) null;
    this.currentCell = (RectangleElement) null;
  }

  object IEnumerator.Current => (object) this.currentCell;

  /// <summary>Перейти к следующему элементу.
  /// Если энумератор в исходном состоянии, то команда переводит его на первый элемент (DataIndex = 0)
  /// Если энумератор был на последнем элементе, то команда переводит энумератор на позицию в которую можно добавлять новый элемент
  /// и возвращает false, при этом Current = null, а DataIndex и currentCellIndex указывают на позицию для вставки</summary>
  /// <returns>Возвращает true, если ещё есть текущий элемент</returns>
  public bool MoveNext()
  {
    if (this.currentCell == null && this.currentCellIndex != -1)
      return false;
    this.currentCell = (RectangleElement) null;
    for (++this.currentCellIndex; this.currentCellIndex < this.table.Nodes.Count; ++this.currentCellIndex)
    {
      this.currentCell = this.table.Nodes[this.currentCellIndex] as RectangleElement;
      if (this.currentCell != null)
        break;
    }
    return this.currentCell != null;
  }

  /// <summary>Перевести энумератор в исходное состояние</summary>
  public void Reset()
  {
    this.currentCell = (RectangleElement) null;
    this.currentCellIndex = -1;
  }

  /// <summary>Получить энумератор. Возвращает самого себя чтобы можно было использовать в foreach</summary>
  /// <returns></returns>
  public IEnumerator<RectangleElement> GetEnumerator() => (IEnumerator<RectangleElement>) this;

  /// <summary>Получить энумератор. Возвращает самого себя чтобы можно было использовать в foreach</summary>
  /// <returns></returns>
  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this;
}
