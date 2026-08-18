// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.MatrixWrapper
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Оболочка для работы с матрицей преобразования</summary>
[Serializable]
public class MatrixWrapper
{
  internal Matrix matrix;
  internal float[] matrixElements;

  /// <summary>Конструктор</summary>
  /// <param name="matrix">Матрица преобразования</param>
  public MatrixWrapper(Matrix matrix) => this.Matrix = matrix;

  /// <summary>Конструктор</summary>
  public MatrixWrapper(float m11, float m12, float m21, float m22, float dx, float dy)
  {
    this.Matrix = new Matrix(m11, m12, m21, m22, dx, dy);
  }

  /// <summary>Матрица преобразования</summary>
  public Matrix Matrix
  {
    [DebuggerStepThrough] get => this.matrix;
    set
    {
      if (this.matrix == value)
        return;
      this.matrix = value;
      this.matrixElements = this.matrix.Elements;
    }
  }

  /// <summary>Конструктор</summary>
  public MatrixWrapper() => this.Matrix = new Matrix();

  /// <summary>Выполнить преобразование координат</summary>
  /// <param name="matrixElements">Элементы матрицы преобразования</param>
  /// <param name="point">Точка</param>
  /// <returns>Точка после преобразования</returns>
  public static PointF TransformPoint(float[] matrixElements, PointF point)
  {
    return new PointF((float) ((double) matrixElements[0] * (double) point.X + (double) matrixElements[2] * (double) point.Y) + matrixElements[4], (float) ((double) matrixElements[1] * (double) point.X + (double) matrixElements[3] * (double) point.Y) + matrixElements[5]);
  }

  /// <summary>Выполнить преобразование координат</summary>
  /// <param name="points">Массив точек</param>
  /// <returns>Массив точек после преобразования</returns>
  public static PointF[] TransformPoints(float[] matrixElements, PointF[] points)
  {
    PointF[] pointFArray = new PointF[points.Length];
    int index = 0;
    for (int length = points.Length; index < length; ++index)
      pointFArray[index] = MatrixWrapper.TransformPoint(matrixElements, points[index]);
    return pointFArray;
  }

  /// <summary>Выполнить преобразование координат</summary>
  /// <param name="points">Массив точек</param>
  /// <returns>Массив точек после преобразования</returns>
  public static RectangleF TransformPoints(float[] matrixElements, RectangleF rectangle)
  {
    PointF pointF1 = MatrixWrapper.TransformPoint(matrixElements, rectangle.Location);
    PointF pointF2 = MatrixWrapper.TransformPoint(matrixElements, new PointF(rectangle.Right, rectangle.Bottom));
    if ((double) pointF1.X < (double) pointF2.X)
    {
      rectangle.X = pointF1.X;
      rectangle.Width = pointF2.X - pointF1.X;
    }
    else
    {
      rectangle.X = pointF2.X;
      rectangle.Width = pointF1.X - pointF2.X;
    }
    if ((double) pointF1.Y < (double) pointF2.Y)
    {
      rectangle.Y = pointF1.Y;
      rectangle.Height = pointF2.Y - pointF1.Y;
    }
    else
    {
      rectangle.Y = pointF2.Y;
      rectangle.Height = pointF1.Y - pointF2.Y;
    }
    return rectangle;
  }

  /// <summary>Выполнить преобразование координат</summary>
  /// <param name="point">Точка</param>
  /// <returns>Точка после преобразования</returns>
  public PointF TransformPoint(PointF point)
  {
    return new PointF((float) ((double) this.matrixElements[0] * (double) point.X + (double) this.matrixElements[2] * (double) point.Y) + this.matrixElements[4], (float) ((double) this.matrixElements[1] * (double) point.X + (double) this.matrixElements[3] * (double) point.Y) + this.matrixElements[5]);
  }

  /// <summary>Выполнить преобразование координат</summary>
  /// <param name="point">Точка</param>
  /// <returns>Точка после преобразования</returns>
  public PointF TransformPoint(Point point)
  {
    return new PointF((float) ((double) this.matrixElements[0] * (double) point.X + (double) this.matrixElements[2] * (double) point.Y) + this.matrixElements[4], (float) ((double) this.matrixElements[1] * (double) point.X + (double) this.matrixElements[3] * (double) point.Y) + this.matrixElements[5]);
  }

  /// <summary>Выполнить преобразование координат</summary>
  /// <param name="points">Массив точек</param>
  /// <returns>Массив точек после преобразования</returns>
  public PointF[] TransformPoints(PointF[] points)
  {
    PointF[] pointFArray = new PointF[points.Length];
    int index = 0;
    for (int length = points.Length; index < length; ++index)
      pointFArray[index] = this.TransformPoint(points[index]);
    return pointFArray;
  }

  /// <summary>Создать матрицу для поворота текста. Угол может принимать только значения: 0, 90, -90, 270, 180</summary>
  /// <param name="rect">Прямоугольник в котором находится текст. Переворачивается вместе с текстом</param>
  /// <param name="angle">Угол поворота. Может принимать только значения: 90, -90, 270, 180</param>
  /// <returns>Матрица пересчета</returns>
  public static Matrix GetMatrixForRotateTextInBox(ref RectangleF rect, int angle)
  {
    Matrix forRotateTextInBox = new Matrix();
    RectangleF rectangleF = rect;
    switch (angle)
    {
      case -90:
      case 270:
        rectangleF = new RectangleF(rect.X, rect.Y, rect.Height, rect.Width);
        forRotateTextInBox = new Matrix(0.0f, -1f, 1f, 0.0f, -rectangleF.Y + rectangleF.X, rectangleF.X + rectangleF.Y + rectangleF.Width);
        break;
      case 90:
        rectangleF = new RectangleF(rect.X, rect.Y, rect.Height, rect.Width);
        forRotateTextInBox = new Matrix(0.0f, 1f, -1f, 0.0f, rectangleF.Y + rectangleF.X + rectangleF.Height, -rectangleF.X + rectangleF.Y);
        break;
      case 180:
        forRotateTextInBox = new Matrix(-1f, 0.0f, 0.0f, -1f, rectangleF.X + rectangleF.Right, rectangleF.Y + rectangleF.Bottom);
        break;
    }
    rect = rectangleF;
    return forRotateTextInBox;
  }

  /// <summary>Создать матрицу для поворота текста. Угол может принимать только значения: 0, 90, -90, 270, 180</summary>
  /// <param name="rect">Прямоугольник в котором находится текст. Переворачивается вместе с текстом</param>
  /// <param name="angle">Угол поворота. Может принимать только значения: 90, -90, 270, 180</param>
  /// <returns>Матрица пересчета</returns>
  public static Matrix GetMatrixForRotateTextInBox(ref Rectangle rect, int angle)
  {
    Matrix forRotateTextInBox = new Matrix();
    Rectangle rectangle = rect;
    switch (angle)
    {
      case -90:
      case 270:
        rectangle = new Rectangle(rect.X, rect.Y, rect.Height, rect.Width);
        forRotateTextInBox = new Matrix(0.0f, -1f, 1f, 0.0f, (float) (-rectangle.Y + rectangle.X), (float) (rectangle.X + rectangle.Y + rectangle.Width));
        break;
      case 90:
        rectangle = new Rectangle(rect.X, rect.Y, rect.Height, rect.Width);
        forRotateTextInBox = new Matrix(0.0f, 1f, -1f, 0.0f, (float) (rectangle.Y + rectangle.X + rectangle.Height), (float) (-rectangle.X + rectangle.Y));
        break;
      case 180:
        forRotateTextInBox = new Matrix(-1f, 0.0f, 0.0f, -1f, (float) (rectangle.X + rectangle.Right), (float) (rectangle.Y + rectangle.Bottom));
        break;
    }
    rect = rectangle;
    return forRotateTextInBox;
  }
}
