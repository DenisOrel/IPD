// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.DistributeDataEnumeratorOld
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
internal class DistributeDataEnumeratorOld : IEnumerator<RectangleElement>, IDisposable, IEnumerator
{
  public RectangleElement Cell;
  public int sourceIndex;
  public TableData sourceTable;
  public int bufferCount;
  private bool firstCell;
  /// <summary>Разбиваемая таблица для которой собираются ячейки</summary>
  private TableData baseTable;
  /// <summary>В этой таблице могут быть ячейки типа заголовок</summary>
  private bool headerCellsIsAvailable;
  /// <summary>Эту таблицу можно разбивать по вертикали</summary>
  private bool canVerticalDistribute;

  public RectangleElement Current => this.Cell;

  public void Dispose() => this.Cell = (RectangleElement) null;

  object IEnumerator.Current => (object) this.Current;

  /// <summary>Конструктор</summary>
  /// <param name="table">Разбиваемая таблица для которой собираются ячейки</param>
  /// <param name="headerCellsIsAvailable">В этой таблице могут быть ячейки типа заголовок</param>
  /// <param name="canVerticalDistribute">Эту таблицу можно разбивать по вертикали</param>
  public DistributeDataEnumeratorOld(
    TableData table,
    bool headerCellsIsAvailable,
    bool canVerticalDistribute)
  {
    this.baseTable = table;
    this.headerCellsIsAvailable = headerCellsIsAvailable;
    this.canVerticalDistribute = canVerticalDistribute;
    this.Reset();
  }

  public bool MoveNext()
  {
    if (this.sourceTable == null)
      return false;
    while (this.sourceTable != null)
    {
      this.bufferCount = 0;
      this.Cell = (RectangleElement) null;
      bool flag = false;
      if (this.firstCell && this.sourceIndex < this.sourceTable.Nodes.Count)
        flag = this.sourceTable.DistributeBuffer == null || this.sourceTable.DistributeBuffer.Count == 0 || this.sourceTable.Nodes[this.sourceIndex] is TableData node && node.PrevCell != null;
      if (flag)
      {
        this.Cell = this.sourceTable.Nodes[this.sourceIndex++] as RectangleElement;
        if (this.Cell == null || this.headerCellsIsAvailable && this.Cell.TableCellType != CellType.DataCell)
        {
          this.Cell = (RectangleElement) null;
          continue;
        }
        this.firstCell = false;
      }
      else
      {
        if (this.sourceTable.DistributeBuffer != null && (this.bufferCount = this.sourceTable.DistributeBuffer.Count) != 0)
        {
          this.Cell = this.sourceTable.DistributeBuffer[this.bufferCount - 1];
          if (this.sourceTable == this.baseTable)
            ++this.sourceIndex;
        }
        if (this.bufferCount == 0)
        {
          if (this.sourceIndex < this.sourceTable.Nodes.Count)
          {
            this.Cell = this.sourceTable.Nodes[this.sourceIndex++] as RectangleElement;
            if (this.Cell == null || this.sourceTable.IsColumn && this.Cell.TableCellType != CellType.DataCell)
            {
              this.Cell = (RectangleElement) null;
              continue;
            }
          }
          else
          {
            if (this.sourceTable.NextCell == null)
            {
              if (!this.sourceTable.IsTopLevelTable && this.sourceTable.ParentCell.IsColumn)
              {
                while (this.sourceTable != this.baseTable && this.sourceTable.AllFlowsIsEmpty() && this.sourceTable.PrevCell != null)
                {
                  TableData sourceTable = this.sourceTable;
                  this.sourceTable = this.sourceTable.PrevTable;
                  sourceTable.UniteTable();
                  if (!sourceTable.IsTopLevelTable)
                    sourceTable.Remove(false, false);
                }
              }
              this.sourceTable = (TableData) null;
            }
            else if (this.canVerticalDistribute)
            {
              if (this.sourceTable.Page != null && this.sourceTable.Page.IsAdditionalPage && this.sourceTable.NextTable.Page != null && !this.sourceTable.NextTable.Page.IsAdditionalPage)
              {
                this.sourceTable = (TableData) null;
              }
              else
              {
                this.sourceTable = this.sourceTable.NextTable;
                for (int index = 0; this.sourceTable.Page != null && this.sourceTable.Page.IsLockedForLoad && index < 500; ++index)
                  Thread.Sleep(25);
              }
            }
            else
              this.sourceTable = (TableData) null;
            this.sourceIndex = 0;
            continue;
          }
        }
      }
      if (this.Cell != null)
        break;
    }
    return this.Cell != null;
  }

  public void Reset()
  {
    this.bufferCount = 0;
    this.Cell = (RectangleElement) null;
    this.sourceTable = this.baseTable;
    this.sourceIndex = 0;
    this.firstCell = true;
  }
}
