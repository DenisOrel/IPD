// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.BordersStatus
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Статус границ ячейки</summary>
public class BordersStatus
{
  /// <summary>Левая</summary>
  public bool? Left;
  /// <summary>Левая граница еще не проверялась</summary>
  public bool FirstLeft = true;
  /// <summary>Правая</summary>
  public bool? Right;
  /// <summary>Правая граница еще не проверялась</summary>
  public bool FirstRight = true;
  /// <summary>Верхняя</summary>
  public bool? Top;
  /// <summary>Верхняя граница еще не проверялась</summary>
  public bool FirstTop = true;
  /// <summary>Нижняя</summary>
  public bool? Bottom;
  /// <summary>Нижняя граница еще не проверялась</summary>
  public bool FirstBottom = true;
  /// <summary>Внутренние горизонтальные</summary>
  public bool? InnerHorizontal;
  /// <summary>Внутренние горизонтальные границы еще не проверялись</summary>
  public bool FirstHorizontal = true;
  /// <summary>Внутренние вертикальные</summary>
  public bool? InnerVertical;
  /// <summary>Внутренние вертикальные границы еще не проверялись</summary>
  public bool FirstVertical = true;

  /// <summary>Конструктор</summary>
  public BordersStatus()
  {
    this.Left = new bool?();
    this.Right = new bool?();
    this.Top = new bool?();
    this.Bottom = new bool?();
    this.InnerHorizontal = new bool?();
    this.InnerVertical = new bool?();
  }

  /// <summary>Конструктор</summary>
  /// <param name="left">Левая</param>
  /// <param name="right">Правая</param>
  /// <param name="top">Верхняя</param>
  /// <param name="bottom">Нижняя</param>
  /// <param name="innerHorizontal">Внутренние горизонтальные</param>
  /// <param name="innerVertical">Внутренние вертикальные</param>
  public BordersStatus(
    bool? left,
    bool? right,
    bool? top,
    bool? bottom,
    bool? innerHorizontal,
    bool? innerVertical)
  {
    this.Left = left;
    this.Right = right;
    this.Top = top;
    this.Bottom = bottom;
    this.InnerHorizontal = innerHorizontal;
    this.InnerVertical = innerVertical;
  }
}
