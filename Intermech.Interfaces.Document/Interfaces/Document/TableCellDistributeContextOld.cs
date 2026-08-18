// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.TableCellDistributeContextOld
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System.Drawing;

#nullable disable
namespace Intermech.Interfaces.Document;

internal class TableCellDistributeContextOld : DistributeContext
{
  /// <inheritdoc />
  public TableCellDistributeContextOld(
    DocumentTreeNode ownerNode,
    SizeF newSize,
    SizeF maxSize,
    bool isFirstCell,
    bool isFirstDataCell,
    DistributeContext parentContext)
    : base(ownerNode, newSize, maxSize, isFirstCell, isFirstDataCell, parentContext)
  {
  }

  /// <inheritdoc />
  public TableCellDistributeContextOld()
  {
  }

  public RectangleF CalculatedProperTableBounds { get; internal set; }

  public int СurrentCellIndex { get; internal set; }
}
