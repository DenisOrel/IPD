// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.GridIdPosition
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Положение стандартной строки в сетке</summary>
[Serializable]
public class GridIdPosition : TableGridPosition
{
  /// <summary>Временый флаг для совместимости</summary>
  internal bool stdGridPosition = true;
  /// <summary>Индекс в сетке</summary>
  private int gridID = -1;

  /// <summary>Конструктор</summary>
  public GridIdPosition()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="gridID">Индекс в сетке</param>
  public GridIdPosition(int gridID) => this.gridID = gridID;

  /// <summary>Получить индекс столбца сетки для заданной ячейки</summary>
  /// <param name="gridCell">Ячейка</param>
  /// <returns>Индекс столбца в сетке</returns>
  public override int GetGridColumnIndex(RectangleElement gridCell)
  {
    if (gridCell == null)
      throw new ArgumentNullException(nameof (gridCell));
    if (this.gridID != -1)
    {
      TableData parentCell = gridCell.ParentCell;
      if (parentCell != null && parentCell.IsRow)
      {
        List<RowColParams> gridColumnsParams = parentCell.GridColumnsParams;
        return gridColumnsParams != null ? TableData.GetRowColIndex(gridColumnsParams, this.gridID) : -1;
      }
    }
    return base.GetGridColumnIndex(gridCell);
  }

  /// <summary>Получить индекс строки сетки для заданной ячейки</summary>
  /// <param name="gridCell">Ячейка</param>
  /// <returns>Индекс строки в сетке</returns>
  public override int GetGridRowIndex(RectangleElement gridCell)
  {
    if (gridCell == null)
      throw new ArgumentNullException(nameof (gridCell));
    if (this.gridID != -1)
    {
      TableData parentCell = gridCell.ParentCell;
      if (parentCell != null && parentCell.IsColumn)
      {
        List<RowColParams> gridRowsParams = parentCell.GridRowsParams;
        if (gridRowsParams != null)
        {
          if (!this.stdGridPosition)
            return TableData.GetRowColIndex(gridRowsParams, this.gridID);
          if (this.gridID < gridRowsParams.Count)
          {
            int gridId = this.gridID;
            if (gridRowsParams[gridId].ID == RowColParams.EmptyIDValue)
              gridRowsParams[gridId].ID = TableData.GenerateGridID(gridRowsParams, this.gridID);
            this.gridID = gridRowsParams[gridId].ID;
            this.stdGridPosition = false;
            return gridId;
          }
        }
        return -1;
      }
    }
    return base.GetGridRowIndex(gridCell);
  }

  /// <summary>Индекс в сетке</summary>
  public int GridID
  {
    [DebuggerStepThrough] get => this.gridID;
    set => this.gridID = value;
  }

  /// <summary>Создать копию объекта</summary>
  /// <returns>Копия объекта</returns>
  public override TableGridPosition Clone()
  {
    GridIdPosition gridIdPosition = (GridIdPosition) base.Clone();
    gridIdPosition.gridID = this.gridID;
    gridIdPosition.stdGridPosition = this.stdGridPosition;
    return (TableGridPosition) gridIdPosition;
  }
}
