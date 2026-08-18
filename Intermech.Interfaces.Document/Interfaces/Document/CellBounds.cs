// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.CellBounds
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System.Drawing;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Границы ячейки. Используется для возвращения в результате одного запроса.</summary>
public class CellBounds
{
  /// <summary>Внешние границы с пропусками</summary>
  public RectangleF OutBounds;
  /// <summary>Собственные границы ячейки</summary>
  public RectangleF ProperBounds;

  /// <summary>Конструктор</summary>
  public CellBounds()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="outBounds">Внешние границы с пропусками</param>
  /// <param name="properBounds">Собственные границы ячейки</param>
  public CellBounds(RectangleF outBounds, RectangleF properBounds)
  {
    this.OutBounds = outBounds;
    this.ProperBounds = properBounds;
  }
}
