// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.TableGridPosition
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;
using System.ComponentModel;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Положение в сетке таблицы</summary>
[TypeConverter(typeof (LocalizedExpandableObjectConverter))]
[Serializable]
public class TableGridPosition : ICloneable
{
  private int spanCount = 1;
  private bool startMerge;
  [ExternalLink]
  private RectangleElement mergeWithCell;

  /// <summary>Конструктор</summary>
  public TableGridPosition()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="spanCount">Количество последующих ячеек которые покрываются этой ячейкой</param>
  public TableGridPosition(int spanCount) => this.spanCount = spanCount;

  /// <summary>Получить индекс столбца сетки для заданной ячейки</summary>
  /// <param name="gridCell">Ячейка</param>
  /// <returns>Индекс столбца в сетке</returns>
  public virtual int GetGridColumnIndex(RectangleElement gridCell)
  {
    return gridCell != null ? this.GetGridColumnIndex(gridCell, -1, -1) : throw new ArgumentNullException(nameof (gridCell));
  }

  /// <summary>Получить индекс столбца сетки для заданной ячейки</summary>
  /// <param name="gridCell">Ячейка</param>
  /// <param name="prevCellNodeIndex">Идекс предыдущей известной ячейки в nodes</param>
  /// <param name="prevCellGridIndex">Идекс предыдущей известной ячейки в сетке</param>
  /// <returns>Индекс столбца в сетке</returns>
  internal virtual int GetGridColumnIndex(
    RectangleElement gridCell,
    int prevCellNodeIndex,
    int prevCellGridIndex)
  {
    if (gridCell == null)
      throw new ArgumentNullException(nameof (gridCell));
    int gridColumnIndex = prevCellGridIndex;
    TableData parentCell = gridCell.ParentCell;
    if (parentCell != null)
    {
      if (parentCell.IsRow)
      {
        int count = parentCell.Nodes.Count;
        int index1 = gridCell.Index;
        for (int index2 = prevCellNodeIndex + 1; index2 < index1 && index2 < count; ++index2)
        {
          if (parentCell.Nodes[index2] is RectangleElement node)
          {
            if (node.IsDefaultGridPos || node.GridPos is GridIdPosition)
              ++gridColumnIndex;
            else
              gridColumnIndex += node.GridPos.SpanCount;
          }
        }
        ++gridColumnIndex;
      }
      else
        gridColumnIndex = parentCell.GetGridColumnIndex(prevCellNodeIndex, prevCellGridIndex);
    }
    return gridColumnIndex;
  }

  /// <summary>Получить индекс столбца сетки для заданной ячейки</summary>
  /// <param name="gridCell">Ячейка</param>
  /// <param name="prevCellGridIndex">Индекс в сетке предыдущей ячейки</param>
  /// <returns>Индекс столбца в сетке</returns>
  public virtual int GetGridColumnIndex(RectangleElement gridCell, int prevCellGridIndex)
  {
    if (gridCell == null)
      throw new ArgumentNullException(nameof (gridCell));
    return this.GetGridColumnIndex(gridCell, gridCell.Index - 1, prevCellGridIndex);
  }

  /// <summary>Получить индекс строки сетки для заданной ячейки</summary>
  /// <param name="gridCell">Ячейка</param>
  /// <returns>Индекс строки в сетке</returns>
  public virtual int GetGridRowIndex(RectangleElement gridCell) => -1;

  /// <summary>Установить значение SpanCount</summary>
  /// <param name="value">Значение</param>
  public void SetCellSpan(int value)
  {
    if (this.spanCount == value)
      return;
    this.spanCount = value;
  }

  /// <summary>Установить значение SpanCount</summary>
  /// <param name="value">Значение</param>
  public void AddCellSpan(int value) => this.spanCount += value;

  /// <summary>Количество последующих ячеек которые покрываются этой ячейкой</summary>
  [Category("Debug")]
  public int SpanCount
  {
    [DebuggerStepThrough] get => this.spanCount;
    set => this.spanCount = value;
  }

  /// <summary>Ячейка является объединением ячеек</summary>
  [Category("Debug")]
  public bool StartMerge
  {
    [DebuggerStepThrough] get => this.startMerge;
    set => this.startMerge = value;
  }

  /// <summary>Ячейка, с которой объединена эта ячейка</summary>
  [Category("Debug")]
  public RectangleElement MergeWithCell
  {
    [DebuggerStepThrough] get => this.mergeWithCell;
    set => this.mergeWithCell = value;
  }

  /// <summary>Создать копию объекта</summary>
  /// <returns>Копия объекта</returns>
  public virtual TableGridPosition Clone()
  {
    TableGridPosition instance = (TableGridPosition) Activator.CreateInstance(this.GetType(), true);
    instance.spanCount = this.spanCount;
    instance.startMerge = this.startMerge;
    return instance;
  }

  object ICloneable.Clone() => (object) this.Clone();
}
