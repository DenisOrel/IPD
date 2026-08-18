// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.DistributeDataEnumerator
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Специальный энумератор для прохода по ячейкам при разбивке таблицы</summary>
internal class DistributeDataEnumerator : IEnumerator<RectangleElement>, IDisposable, IEnumerator
{
  private CellWithPosition _currentCellPosition;
  /// <summary>Разбиваемая таблица для которой собираются ячейки</summary>
  private readonly TableData _baseTable;
  /// <summary>В этой таблице могут быть ячейки типа заголовок</summary>
  private readonly bool _headerCellsIsAvailable;
  /// <summary>Эту таблицу можно разбивать по вертикали</summary>
  private readonly bool _canVerticalDistribute;
  private readonly DistributeContext _context;

  public RectangleElement Current => this.Cell;

  public RectangleElement Cell
  {
    get => this._currentCellPosition.Cell;
    set => this._currentCellPosition.Cell = value;
  }

  internal int SourceIndex
  {
    get => this._currentCellPosition.SourceIndex;
    set => this._currentCellPosition.SourceIndex = value;
  }

  internal TableData SourceTable
  {
    get => this._currentCellPosition.SourceTable;
    set => this._currentCellPosition.SourceTable = value;
  }

  internal int BufferIndex
  {
    get => this._currentCellPosition.BufferIndex;
    set => this._currentCellPosition.BufferIndex = value;
  }

  internal bool IsCellFromBuffer => this.CurrentCellPosition.IsCellFromBuffer;

  private bool IsFirstCell
  {
    get => this._currentCellPosition.IsFirstCell;
    set => this._currentCellPosition.IsFirstCell = value;
  }

  public bool IsLastCell => this._currentCellPosition.IsLastCell;

  public CellWithPosition CurrentCellPosition
  {
    get => this._currentCellPosition;
    set => this._currentCellPosition = value;
  }

  public void Dispose() => this._currentCellPosition.Cell = (RectangleElement) null;

  object IEnumerator.Current => (object) this.Current;

  /// <summary>Конструктор</summary>
  /// <param name="table">Разбиваемая таблица для которой собираются ячейки</param>
  /// <param name="headerCellsIsAvailable">В этой таблице могут быть ячейки типа заголовок</param>
  /// <param name="canVerticalDistribute">Эту таблицу можно разбивать по вертикали</param>
  public DistributeDataEnumerator(
    TableData table,
    bool headerCellsIsAvailable,
    bool canVerticalDistribute,
    DistributeContext context)
  {
    this._baseTable = table;
    this._headerCellsIsAvailable = headerCellsIsAvailable;
    this._canVerticalDistribute = canVerticalDistribute;
    this._context = context;
    this._currentCellPosition = new CellWithPosition((RectangleElement) null, (TableData) null, -1, -1, true, false);
    this.Reset();
  }

  public bool MoveNext()
  {
    if (this.SourceTable == null)
      return false;
    this._currentCellPosition = this.GetNextDataCellForDistribute(this._currentCellPosition);
    if (this._currentCellPosition.IsBreakPositionForDataFlow)
      this._context.VertDistributed = DistributeResult.Part;
    return this.Cell != null;
  }

  internal CellWithPosition GetNextDataCellForDistribute(CellWithPosition cellPosition)
  {
    CellWithPosition cellForDistribute = cellPosition.Clone();
    while (cellForDistribute.SourceTable != null)
    {
      cellForDistribute = this.GetNextCellPositionInCellSource(cellForDistribute);
      if (cellForDistribute.IsCellFromBuffer)
        return cellForDistribute;
      if (cellForDistribute.SourceIndex < cellForDistribute.SourceTable.Nodes.Count)
      {
        if (cellForDistribute.Cell == null || this._headerCellsIsAvailable && cellForDistribute.Cell.TableCellType != CellType.DataCell)
        {
          cellForDistribute.Cell = (RectangleElement) null;
        }
        else
        {
          cellForDistribute.IsFirstCell = false;
          if (cellForDistribute.Cell != null)
            break;
        }
      }
      else
      {
        cellForDistribute.IsFirstCell = false;
        if (cellForDistribute.SourceTable.NextCell == null)
          return cellForDistribute;
        if (this._canVerticalDistribute)
        {
          if (cellForDistribute.SourceTable.Page != null && cellForDistribute.SourceTable.NextTable.Page != null && cellForDistribute.SourceTable.NextTable.Page.IsNextToAdditionalPage)
          {
            cellForDistribute.SetStopPosition();
            cellForDistribute.IsBreakPositionForDataFlow = true;
            return cellForDistribute;
          }
          this.SetCellPositionBeforeFirstInTable(cellForDistribute.SourceTable.NextTable, cellForDistribute);
          DistributeDataEnumerator.WaitTableIfLockedForLoad(cellForDistribute);
        }
        else
          cellForDistribute.SetStopPosition();
      }
    }
    return cellForDistribute;
  }

  private static bool NeedSkipBufferIfFirstCellIsNextTable(
    CellWithPosition cellPosition,
    int bufferIndex,
    int sourceIndex)
  {
    if (bufferIndex < 0 || bufferIndex == int.MaxValue)
      return false;
    bool flag = false;
    if (cellPosition.IsFirstCell && sourceIndex >= 0 && sourceIndex < cellPosition.SourceTable.Nodes.Count)
      flag = (cellPosition.SourceTable.Nodes[sourceIndex] is TableData node ? node.PrevCell : (RectangleElement) null) != null;
    return flag;
  }

  private static void WaitTableIfLockedForLoad(CellWithPosition newCellPosition)
  {
    for (int index = 0; newCellPosition.SourceTable.Page != null && newCellPosition.SourceTable.Page.IsLockedForLoad && index < 500; ++index)
      Thread.Sleep(25);
  }

  private CellWithPosition GetNextCellPositionInCellSource(CellWithPosition cellPosition)
  {
    CellWithPosition positionInCellSource = cellPosition.Clone();
    positionInCellSource.Cell = (RectangleElement) null;
    positionInCellSource.IsMoved = false;
    int indexInCellSource = DistributeDataEnumerator.GetNextBufferIndexInCellSource(cellPosition);
    int num = cellPosition.SourceIndex + 1;
    bool flag = DistributeDataEnumerator.NeedSkipBufferIfFirstCellIsNextTable(cellPosition, indexInCellSource, num);
    if (flag || indexInCellSource < 0)
    {
      positionInCellSource.SourceIndex = num;
      if (!flag)
        positionInCellSource.BufferIndex = indexInCellSource;
      if (num < cellPosition.SourceTable.Nodes.Count)
      {
        positionInCellSource.Cell = cellPosition.SourceTable.Nodes[num] as RectangleElement;
        positionInCellSource.IsMoved = false;
      }
    }
    else
    {
      positionInCellSource.BufferIndex = indexInCellSource;
      positionInCellSource.Cell = cellPosition.SourceTable.DistributeBuffer[indexInCellSource];
      positionInCellSource.IsMoved = false;
      if (this._baseTable == cellPosition.SourceTable)
        positionInCellSource.SourceIndex = num;
    }
    return positionInCellSource;
  }

  private static CellWithPosition GetPrevCellPositionInCellSource(CellWithPosition cellPosition)
  {
    CellWithPosition positionInCellSource = cellPosition.Clone();
    positionInCellSource.Cell = (RectangleElement) null;
    positionInCellSource.IsMoved = true;
    int indexInCellSource = DistributeDataEnumerator.GetPrevBufferIndexInCellSource(cellPosition);
    int sourceIndex = cellPosition.SourceIndex;
    if (sourceIndex >= 0)
      --sourceIndex;
    if (indexInCellSource == int.MaxValue)
    {
      positionInCellSource.SourceIndex = sourceIndex;
      positionInCellSource.BufferIndex = indexInCellSource;
      if (sourceIndex >= 0)
      {
        positionInCellSource.Cell = cellPosition.SourceTable.Nodes[sourceIndex] as RectangleElement;
        positionInCellSource.IsMoved = false;
      }
    }
    else
    {
      positionInCellSource.BufferIndex = indexInCellSource;
      positionInCellSource.Cell = cellPosition.SourceTable.DistributeBuffer[indexInCellSource];
      positionInCellSource.IsMoved = false;
    }
    return positionInCellSource;
  }

  private static int GetPrevBufferIndexInCellSource(CellWithPosition cellPosition)
  {
    return cellPosition.SourceTable.DistributeBuffer.IsEmpty<RectangleElement>() || cellPosition.BufferIndex >= cellPosition.SourceTable.DistributeBuffer.Count - 1 ? int.MaxValue : cellPosition.BufferIndex + 1;
  }

  private static int GetNextBufferIndexInCellSource(CellWithPosition cellPosition)
  {
    int indexInCellSource = cellPosition.BufferIndex;
    if (cellPosition.BufferIndex == int.MaxValue)
      indexInCellSource = !cellPosition.SourceTable.DistributeBuffer.IsEmpty<RectangleElement>() ? cellPosition.SourceTable.DistributeBuffer.Count - 1 : -1;
    else if (cellPosition.BufferIndex > -1)
      indexInCellSource = cellPosition.BufferIndex - 1;
    return indexInCellSource;
  }

  internal void RemoveCurrentCellFromDataFlow()
  {
    if (this.Cell == null)
      return;
    if (this.IsCellFromBuffer && !this.CurrentCellPosition.IsMoved)
      this.SourceTable.DistributeBuffer.RemoveAt(this.BufferIndex);
    this.Cell.UniteTable();
    this.Cell.SetPrevCell((RectangleElement) null);
    if (this.Cell.Parent != null)
    {
      this.Cell.Remove(false, false, false);
      if (this.SourceIndex >= 0)
        --this.SourceIndex;
    }
    this.CurrentCellPosition.Cell = (RectangleElement) null;
    this.CurrentCellPosition.IsMoved = true;
  }

  public void Reset()
  {
    this.IsFirstCell = true;
    this.SetCellPositionBeforeFirstInTable(this._baseTable, this._currentCellPosition);
    this._currentCellPosition.SourceIndex = this._context.HeaderCount - 1;
    this._currentCellPosition.IsBreakPositionForDataFlow = false;
  }

  private void SetCellPositionBeforeFirstInTable(
    TableData sourceTable,
    CellWithPosition cellPosition)
  {
    cellPosition.SourceTable = sourceTable;
    cellPosition.SourceIndex = -1;
    cellPosition.BufferIndex = int.MaxValue;
    cellPosition.Cell = (RectangleElement) null;
  }

  internal bool FromNewPageInCurrentContext(CellWithPosition cellPosition)
  {
    if (this._baseTable.IsRow)
      return false;
    CellWithPosition positionInCellSource = DistributeDataEnumerator.GetPrevCellPositionInCellSource(cellPosition);
    if (positionInCellSource?.Cell != null && positionInCellSource.Cell.IsDynamicGroupHeader)
      return false;
    if (!cellPosition.Cell.IsDynamicGroupHeader)
      return cellPosition.Cell.FromNewPage;
    CellWithPosition cellForDistribute = this.GetNextDataCellForDistribute(cellPosition);
    if (cellForDistribute?.Cell == null)
      return false;
    return !cellForDistribute.Cell.IsDynamicGroupHeader ? cellForDistribute.Cell.FromNewPage : this.FromNewPageInCurrentContext(cellForDistribute);
  }
}
