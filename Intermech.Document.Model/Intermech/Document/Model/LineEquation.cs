// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.LineEquation
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;

#nullable disable
namespace Intermech.Document.Model;

/// <summary>Коэффициенты уравнения прямой</summary>
[Serializable]
public struct LineEquation
{
  /// <summary>Коэффициент уравнения А</summary>
  public float A;
  /// <summary>Коэффициент уравнения В</summary>
  public float B;
  /// <summary>Коэффициент уравнения С</summary>
  public float C;

  /// <summary>Конструктор</summary>
  /// <param name="p0">Первая точка на прямой</param>
  /// <param name="p1">Вторая точка на прямой</param>
  public LineEquation(PointF p0, PointF p1)
  {
    this.A = p1.Y - p0.Y;
    this.B = p0.X - p1.X;
    this.C = (float) ((double) p0.Y * ((double) p1.X - (double) p0.X) - (double) p0.X * ((double) p1.Y - (double) p0.Y));
  }

  /// <summary>Конструктор</summary>
  /// <param name="A">Коэффициент уравнения А</param>
  /// <param name="B">Коэффициент уравнения B</param>
  /// <param name="C">Коэффициент уравнения C</param>
  public LineEquation(float A, float B, float C)
  {
    this.A = A;
    this.B = B;
    this.C = C;
  }
}
