// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.DataNodesEnumerator
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

/// <summary>Enumerator для перемещения по данным в таблице</summary>
public class DataNodesEnumerator : IEnumerator<RectangleElement>, IDisposable, IEnumerator
{
  private TableData mainTable;
  /// <summary>Только для внутреннего использования. Предыдущий элемент после вызова MoveNext()</summary>
  public RectangleElement PrevCell;
  private RectangleElement currentCell;
  private int currentCellIndex = -1;
  private TableData currentCellOwner;
  private TableData previousCellOwner;
  private int dataIndex;
  private int previousDataIndex;

  /// <summary>Конструктор</summary>
  /// <param name="mainTable">Таблица, владелец данных</param>
  public DataNodesEnumerator(TableData mainTable)
  {
    this.mainTable = mainTable != null ? mainTable : throw new ArgumentNullException(nameof (mainTable));
  }

  public PageData PrevCellPage => this.PrevCell?.Page;

  public PageData CurrentCellPage => this.currentCell?.Page;

  /// <summary>Индекс текущей ячейки в родительском объекте</summary>
  public int CurrentCellIndex
  {
    [DebuggerStepThrough] get => this.currentCellIndex;
  }

  /// <summary>таблица </summary>
  public TableData CurrentCellOwner => this.currentCellOwner;

  /// <summary>Сквозной индекс текущего элемента</summary>
  public int DataIndex
  {
    [DebuggerStepThrough] get => this.dataIndex;
    set
    {
      if (this.dataIndex == value)
        return;
      this.dataIndex = value;
      this.currentCellIndex = this.mainTable.FindDataPositionInFlow(this.dataIndex, out this.currentCellOwner);
      this.PrevCell = (RectangleElement) null;
      this.currentCell = (RectangleElement) null;
      if (this.currentCellIndex == -1 || this.currentCellOwner == null || this.currentCellIndex >= this.currentCellOwner.Nodes.Count)
        return;
      this.currentCell = this.currentCellOwner.Nodes[this.currentCellIndex] as RectangleElement;
    }
  }

  /// <summary>Главная таблица по которой осуществляется проход</summary>
  public TableData MainTable => this.mainTable;

  /// <summary>Перечислитель находится в позиции перед первым элементом.
  /// Чтобы получить первый элемент (или перейти в первую позицию) необходимо вызвать MoveNext()</summary>
  public bool IsBeforeStart => this.currentCell == null && this.currentCellIndex == -1;

  /// <summary>Установить текущую позицию</summary>
  /// <param name="cell">Ячейка</param>
  /// <param name="dataIndex">Индекс данных</param>
  public void SetCurrentCell(RectangleElement cell, int dataIndex)
  {
    if (cell == null && dataIndex != -1)
      throw new ArgumentNullException(nameof (cell));
    this.PrevCell = (RectangleElement) null;
    this.currentCell = cell;
    if (cell != null)
    {
      this.currentCellOwner = cell.ParentCell;
      this.currentCellIndex = cell.Index;
    }
    else
    {
      this.currentCellOwner = (TableData) null;
      this.currentCellIndex = -1;
    }
    this.dataIndex = dataIndex;
  }

  /// <summary>Удалить</summary>
  public void RemoveCurrentAndGotoPrev()
  {
    if (this.currentCell == null)
      return;
    this.currentCell.UniteTable();
    this.currentCell.Remove(false, false, false);
    if (this.PrevCell != null)
      this.SetCurrentCell(this.PrevCell, this.DataIndex - 1);
    else
      this.Reset();
  }

  /// <summary>Вставить новый элемент в текущую позицию и сделать его текущим</summary>
  /// <param name="newCell">Новый элемент</param>
  public void InsertAtCurrentPos(RectangleElement newCell)
  {
    if (newCell == null)
      throw new ArgumentNullException(nameof (newCell));
    if (this.currentCell == null && this.currentCellIndex == -1)
      this.currentCellIndex = this.currentCellOwner.Nodes.Count;
    if (this.currentCellIndex >= 0 && this.currentCellIndex <= this.currentCellOwner.Nodes.Count)
      this.currentCellOwner.InsertChildNode(this.currentCellIndex, (DocumentTreeNode) newCell, newCell.Parent != null, true, false, false, false);
    this.currentCell = newCell;
  }

  /// <summary>Добавить новый элемент после предыдущей позиции и сделать его текущим</summary>
  /// <param name="newCell">Новый элемент</param>
  public void AppendAfterPreviousPos(RectangleElement newCell)
  {
    if (newCell == null)
      throw new ArgumentNullException(nameof (newCell));
    if (this.previousCellOwner == null)
    {
      this.InsertAtCurrentPos(newCell);
    }
    else
    {
      this.previousCellOwner.InsertChildNode(this.previousCellOwner.Nodes.Count, (DocumentTreeNode) newCell, newCell.Parent != null, true, false, false, false);
      this.SetCurrentCell(newCell, this.previousDataIndex + 1);
    }
  }

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
    this.mainTable = (TableData) null;
    this.PrevCell = (RectangleElement) null;
    this.currentCell = (RectangleElement) null;
    this.currentCellOwner = (TableData) null;
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
    this.previousCellOwner = this.currentCellOwner;
    if (this.currentCellIndex == -1)
    {
      this.dataIndex = 0;
      this.previousDataIndex = 0;
      this.PrevCell = (RectangleElement) null;
      this.currentCell = (RectangleElement) null;
      this.currentCellIndex = this.mainTable.FindDataPositionInFlow(0, out this.currentCellOwner);
      if (this.currentCellIndex != -1 && this.currentCellOwner != null && this.currentCellIndex >= this.currentCellOwner.Nodes.Count)
      {
        TableData dataOwner;
        this.currentCellIndex = this.currentCellOwner.FindDataCellFromPosition(this.currentCellIndex, out dataOwner);
        if (dataOwner != null)
          this.currentCellOwner = dataOwner;
      }
      if (this.currentCellIndex == -1 || this.currentCellOwner == null)
        return false;
      this.currentCell = this.currentCellOwner.Nodes[this.currentCellIndex] as RectangleElement;
      return true;
    }
    this.currentCellOwner = this.currentCell.ParentCell;
    if (this.currentCellOwner == null)
    {
      this.currentCellIndex = this.mainTable.FindDataPositionInFlow(this.dataIndex + 1, out this.currentCellOwner);
      if (this.currentCellOwner == null)
        return false;
    }
    else if (this.currentCellIndex >= this.currentCellOwner.Nodes.Count || this.currentCellOwner.Nodes[this.currentCellIndex] != this.currentCell)
      this.currentCellIndex = this.currentCell.Index;
    this.previousDataIndex = this.dataIndex;
    do
    {
      ++this.dataIndex;
      TableData dataOwner;
      int dataPositionInFlow = this.currentCellOwner.FindNextDataPositionInFlow(this.currentCellIndex, out dataOwner);
      if (dataOwner != this.currentCellOwner || dataPositionInFlow != this.currentCellIndex)
      {
        this.currentCellOwner = dataOwner;
        this.currentCellIndex = dataPositionInFlow;
      }
      else
        goto label_18;
    }
    while (this.currentCellIndex == -1 || this.currentCellIndex >= this.currentCellOwner.Nodes.Count);
    this.PrevCell = this.currentCell;
    this.currentCell = this.currentCellOwner.Nodes[this.currentCellIndex] as RectangleElement;
    return true;
label_18:
    this.PrevCell = this.currentCell;
    this.currentCell = (RectangleElement) null;
    return false;
  }

  /// <summary>Перевести энумератор в исходное состояние</summary>
  public void Reset()
  {
    this.dataIndex = -1;
    this.previousDataIndex = -1;
    this.currentCell = (RectangleElement) null;
    this.currentCellIndex = -1;
    this.currentCellOwner = (TableData) null;
    this.previousCellOwner = (TableData) null;
    this.PrevCell = (RectangleElement) null;
  }
}
