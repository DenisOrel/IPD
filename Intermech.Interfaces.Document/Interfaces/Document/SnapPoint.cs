// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.SnapPoint
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System.Drawing;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Класс с информацией о точке привязки в объекте</summary>
public class SnapPoint
{
  /// <summary>Координаты точки</summary>
  public PointF Point = PointF.Empty;
  /// <summary>Тип точки</summary>
  public SnapPointType PointType;

  /// <summary>Конструктор</summary>
  /// <param name="point">Координаты точки</param>
  /// <param name="pointType">Тип точки</param>
  /// <param name="snapX">Координата X привязана</param>
  /// <param name="snapY">Координата Y привязана</param>
  public SnapPoint(PointF point, SnapPointType pointType) => this.SetPoint(point, pointType);

  /// <summary>Назначить точку</summary>
  /// <param name="point">Точка</param>
  /// <param name="pointType">Тип точки</param>
  /// <param name="snapX">Координата X привязана</param>
  /// <param name="snapY">Координата Y привязана</param>
  internal void SetPoint(PointF point, SnapPointType pointType)
  {
    this.Point = point;
    this.PointType = pointType;
  }
}
