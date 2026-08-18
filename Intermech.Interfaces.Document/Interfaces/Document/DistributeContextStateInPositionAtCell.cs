// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.DistributeContextStateInPositionAtCell
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System.Drawing;

#nullable disable
namespace Intermech.Interfaces.Document;

internal class DistributeContextStateInPositionAtCell
{
  public SizeF FreeSpace { get; set; }

  public RectangleF CalculatedProperTableBounds { get; set; }

  public RectangleF PrevCellBounds { get; set; }

  public RectangleElement FirstKeepWithNext { get; set; }

  public int LastCellFromBuffer { get; set; } = -1;

  public int CurrentCellIndex { get; set; }

  public DistributeContext PrevCellContext { get; set; }
}
