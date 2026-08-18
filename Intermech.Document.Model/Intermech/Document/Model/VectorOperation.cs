// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.VectorOperation
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Localization;
using System;
using System.Drawing;

#nullable disable
namespace Intermech.Document.Model;

/// <summary>Класс для операций с векторами</summary>
public class VectorOperation
{
  /// <summary>Вектор единичной длины по направлению от точки p0 в сторону точки p1</summary>
  /// <param name="p0">Начальная точка</param>
  /// <param name="p1">Конечная точка</param>
  /// <returns>Вектор направления единичной длины</returns>
  public static PointF DirectionVector(PointF p0, PointF p1)
  {
    PointF pointF = new PointF(p1.X - p0.X, p1.Y - p0.Y);
    float num = (float) Math.Sqrt((double) pointF.X * (double) pointF.X + (double) pointF.Y * (double) pointF.Y);
    if ((double) num == 0.0)
      throw new DivideByZeroException(LocalizationHolder.rm.GetString("Document.Model_516"));
    pointF.X /= num;
    pointF.Y /= num;
    return pointF;
  }

  /// <summary>Вектор перпендикулярный заданному</summary>
  /// <param name="vector">Исходный вектор</param>
  /// <returns>Вектор перпендикулярный исходному</returns>
  public static PointF OrthoVector(PointF vector) => new PointF(-vector.Y, vector.X);

  /// <summary>Точка пересечения двух прямых</summary>
  /// <param name="l1">Общее уравнение первой прямой</param>
  /// <param name="l2">Общее уравнение второй прямой</param>
  /// <returns>Точка пересечения заданных прямых</returns>
  public static PointF LineIntersection(LineEquation l1, LineEquation l2)
  {
    PointF empty = PointF.Empty;
    if ((double) l1.A == 0.0 && (double) l2.A == 0.0)
      return PointF.Empty;
    float num1 = (float) ((double) l1.A * (double) l2.B - (double) l1.B * (double) l2.A);
    if ((double) num1 == 0.0)
      return PointF.Empty;
    float num2 = (float) ((double) l2.A * (double) l1.C - (double) l1.A * (double) l2.C);
    empty.Y = num2 / num1;
    empty.X = (double) l1.A == 0.0 ? (float) (-(double) l2.C - (double) l2.B * (double) empty.Y) / l2.A : (float) (-(double) l1.C - (double) l1.B * (double) empty.Y) / l1.A;
    return empty;
  }
}
