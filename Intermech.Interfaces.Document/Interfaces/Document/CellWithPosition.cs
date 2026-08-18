// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.CellWithPosition
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

#nullable disable
namespace Intermech.Interfaces.Document;

internal class CellWithPosition
{
  public TableData SourceTable;
  public int SourceIndex;
  public RectangleElement Cell;
  public DistributeContextStateInPositionAtCell ContextState;
  public bool IsFirstCell;
  public bool IsBreakPositionForDataFlow;

  public int BufferIndex { get; set; }

  public CellWithPosition(
    RectangleElement cell,
    TableData sourceTable,
    int sourceIndex,
    int bufferIndex,
    bool isFirstCell,
    bool isBreakPositionForDataFlow)
  {
    this.SourceTable = sourceTable;
    this.SourceIndex = sourceIndex;
    this.BufferIndex = bufferIndex;
    this.IsFirstCell = isFirstCell;
    this.IsBreakPositionForDataFlow = isBreakPositionForDataFlow;
    this.Cell = cell;
  }

  public CellWithPosition Clone()
  {
    return new CellWithPosition(this.Cell, this.SourceTable, this.SourceIndex, this.BufferIndex, this.IsFirstCell, this.IsBreakPositionForDataFlow);
  }

  public bool IsEnd => this.SourceTable == null;

  public bool IsLastCell
  {
    get
    {
      if (this.SourceTable == null)
        return true;
      return this.SourceTable.NextCell == null && this.SourceIndex >= this.SourceTable.Nodes.Count - 1 && !this.IsCellFromBuffer;
    }
  }

  public bool IsCellFromBuffer => this.BufferIndex >= 0 && this.BufferIndex != int.MaxValue;

  public bool IsMoved { get; internal set; }

  public void SetStopPosition()
  {
    this.SourceTable = (TableData) null;
    this.Cell = (RectangleElement) null;
  }

  public override string ToString()
  {
    return $"{this.SourceTable} {(this.IsCellFromBuffer ? (object) $" [Buffer:{this.BufferIndex}]" : (object) $"[{this.SourceIndex}]")} = {$" {this.Cell}"}";
  }
}
