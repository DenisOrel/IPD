// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.BordersPosition
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Положение границ ячейки</summary>
public class BordersPosition
{
  /// <summary>Внешняя левая</summary>
  public bool Left;
  /// <summary>Внешняя правая</summary>
  public bool Right;
  /// <summary>Внешняя верхняя</summary>
  public bool Top;
  /// <summary>Внешняя нижняя</summary>
  public bool Bottom;

  /// <summary>Конструктор</summary>
  public BordersPosition()
  {
    this.Left = true;
    this.Right = true;
    this.Top = true;
    this.Bottom = true;
  }

  /// <summary>Конструктор</summary>
  /// <param name="left">Внешняя левая</param>
  /// <param name="right">Внешняя правая</param>
  /// <param name="top">Внешняя верхняя</param>
  /// <param name="bottom">Внешняя нижняя</param>
  public BordersPosition(bool left, bool right, bool top, bool bottom)
  {
    this.Left = left;
    this.Right = right;
    this.Top = top;
    this.Bottom = bottom;
  }
}
