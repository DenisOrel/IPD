
// Type: Intermech.MatrixD
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Diagnostics;


namespace Intermech
{
    /// <summary>Инкапсулирует матрицу 3 на 3 афинного преобразования. Этот класс не может быть наследован.</summary>
    /// <remarks>Матрица 3 на 3 содержит значения x в первом столбце, y – во втором и w – в третьем.</remarks>
    [DebuggerDisplay("(m11={_data[0,0]} m12={_data[0,1]} m21={_data[1,0]} m22={_data[1,1]}) [{_data[2,0]} {_data[2,1]}]")]
    public sealed class MatrixD : MarshalByRefObject, IDisposable
    {
      private double[,] _data = new double[3, 2];
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      private bool disposed;

      private double _dx
      {
        get => this._data[2, 0];
        set => this._data[2, 0] = value;
      }

      private double _dy
      {
        get => this._data[2, 1];
        set => this._data[2, 1] = value;
      }

      /// <summary>Инициализирует новый экземпляр класса MatrixD в виде единичной матрицы.</summary>
      public MatrixD() => this._data[0, 0] = this._data[1, 1] = 1.0;

      /// <summary>Инициализирует новый экземпляр класса MatrixD с указанными элементами.</summary>
      /// <param name="m11"> Значение на пересечении первой строки и первого столбца нового объекта MatrixD.</param>
      /// <param name="m12"> Значение на пересечении первой строки и второго столбца нового объекта MatrixD. </param>
      /// <param name="m21"> Значение на пересечении второй строки и первого столбца нового объекта MatrixD. </param>
      /// <param name="m22"> Значение на пересечении второй строки и второго столбца нового объекта MatrixD. </param>
      /// <param name="dx">  Значение на пересечении третьей строки и первого столбца нового объекта MatrixD.</param>
      /// <param name="dy">  Значение на пересечении третьей строки и второго столбца нового объекта MatrixD.</param>
      public MatrixD(double m11, double m12, double m21, double m22, double dx, double dy)
      {
        this._data[0, 0] = m11;
        this._data[0, 1] = m12;
        this._data[1, 0] = m21;
        this._data[1, 1] = m22;
        this._dx = dx;
        this._dy = dy;
      }

      /// <summary>Получает массив значений с плавающей запятой, представляющий элементы объекта MatrixD.</summary>
      /// <returns>Массив значений с плавающей запятой, представляющий элементы объекта MatrixD.</returns>
      public double[] Elements
      {
        get
        {
          return new double[6]
          {
            this._data[0, 0],
            this._data[0, 1],
            this._data[1, 0],
            this._data[1, 1],
            this._dx,
            this._dy
          };
        }
      }

      /// <summary>Создает точную копию объекта MatrixD.</summary>
      /// <returns>Объект MatrixD, созданный данным методом.</returns>
      public MatrixD Clone()
      {
        return new MatrixD(this._data[0, 0], this._data[0, 1], this._data[1, 0], this._data[1, 1], this._dx, this._dy);
      }

      public void Dispose()
      {
        this.Dispose(true);
        GC.SuppressFinalize((object) this);
      }

      private void Dispose(bool disposing)
      {
        if (!this.disposed && disposing)
          this._data = (double[,]) null;
        this.disposed = true;
      }

      ~MatrixD() => this.Dispose(false);

      /// <summary>Проверяет, является ли заданный объект объектом MatrixD и идентичен ли он этому объекту MatrixD.</summary>
      /// <returns>Данный метод возвращает значение true, если obj является указанным объектом MatrixD, идентичным объекту MatrixD; в противном случае – false.</returns>
      /// <param name="obj">Объект для проверки.</param>
      public override bool Equals(object obj)
      {
        return obj is MatrixD matrixD && this._dx == matrixD._dx && this._dy == matrixD._dy && this._data[0, 0] == matrixD._data[0, 0] && this._data[0, 1] == matrixD._data[0, 1] && this._data[1, 0] == matrixD._data[1, 0] && this._data[1, 1] == matrixD._data[1, 1];
      }

      /// <summary>Возвращает хэш-код.</summary>
      /// <returns>Хеш-код для этого объекта MatrixD.</returns>
      public override int GetHashCode() => base.GetHashCode();

      /// <summary>Обращает матрицу MatrixD, если она обратима.</summary>
      public void Invert()
      {
      }

      /// <summary>Умножает объект MatrixD на матрицу, указанную в параметре matrix, путем добавления его в начало.</summary>
      /// <param name="matrix">Объект MatrixD, на который умножается объект MatrixD.</param>
      public void Multiply(MatrixD matrix) => this.Multiply(matrix, MatrixD.MatrixOrder.Prepend);

      /// <summary>Умножает объект MatrixD на матрицу, указанную в параметре matrix, в порядке, задаваемом в параметре order.</summary>
      /// <param name="matrix"> Объект MatrixD, на который умножается объект MatrixD.</param>
      /// <param name="order">Перечисление MatrixOrder, представляющее порядок перемножения.</param>
      public void Multiply(MatrixD matrix, MatrixOrder order)
      {
        double[,] numArray1 = order == MatrixD.MatrixOrder.Prepend ? this._data : matrix._data;
        double[,] numArray2 = order != MatrixD.MatrixOrder.Prepend ? this._data : matrix._data;
        this._data = new double[3, 2];
        this._data[0, 0] = numArray1[0, 0] * numArray2[0, 0] + numArray1[0, 1] * numArray2[1, 0];
        this._data[0, 1] = numArray1[0, 0] * numArray2[0, 1] + numArray1[0, 1] * numArray2[1, 1];
        this._data[1, 0] = numArray1[1, 0] * numArray2[0, 0] + numArray1[1, 1] * numArray2[1, 0];
        this._data[1, 1] = numArray1[1, 0] * numArray2[0, 1] + numArray1[1, 1] * numArray2[1, 1];
        this._dx = numArray2[2, 0] * numArray1[0, 0] + numArray2[2, 1] * numArray1[1, 0] + numArray1[2, 0];
        this._dy = numArray2[2, 0] * numArray1[0, 1] + numArray2[2, 1] * numArray1[1, 1] + numArray1[2, 1];
      }

      /// <summary>Сбрасывает объект MatrixD, так что его элементы становятся единичными матрицами.</summary>
      /// <remarks>Элементы на главной диагонали единичной матрицы равны 1. Все остальные ее элементы равны 0.</remarks>
      public void Reset()
      {
        this._data[0, 0] = 1.0;
        this._data[0, 1] = 0.0;
        this._data[1, 0] = 0.0;
        this._data[1, 1] = 1.0;
        this._data[2, 0] = 0.0;
        this._data[2, 1] = 0.0;
      }

      /// <summary>Добавляет в начало объекта MatrixD поворот по часовой стрелке вокруг начала координат на указанный угол.</summary>
      /// <param name="angle">Угол(градусы) поворота.</param>
      public void Rotate(double angle) => this.Rotate(angle, MatrixD.MatrixOrder.Prepend);

      /// <summary>Применяет поворот по часовой стрелке на величину, указаную в параметре angle, вокруг начала координат (нулевые координаты x и y) к объекту MatrixD.</summary>
      /// <param name="angle">Угол(градусы) поворота.</param>
      /// <param name="order">Перечисление MatrixOrder, указывающее порядок (в конце или в начале), в котором поворот применяется к объекту MatrixD.</param>
      public void Rotate(double angle, MatrixOrder order)
      {
        using (MatrixD matrix = new MatrixD())
        {
          matrix._data[0, 0] = matrix._data[1, 1] = Math.Cos(angle * Math.PI / 180.0);
          matrix._data[1, 0] = -(matrix._data[0, 1] = Math.Sin(angle * Math.PI / 180.0));
          this.Multiply(matrix, order);
        }
      }

      /// <summary>Применяет поворот по часовой стрелке вокруг указанной в параметре point точки к объекту MatrixD, добавляя поворот в начало.</summary>
      /// <param name="angle">Угол(градусы) поворота. </param>
      /// <param name="point">Структура PointD, представляющая центральную точку поворота.</param>
      public void RotateAt(double angle, PointD point)
      {
        this.RotateAt(angle, point, MatrixD.MatrixOrder.Prepend);
      }

      /// <summary>Применяет поворот по часовой стрелке вокруг указанной точки к объекту MatrixD в указанном порядке.</summary>
      /// <param name="angle">Угол(градусы) поворота.</param>
      /// <param name="point">Структура PointD, представляющая центральную точку поворота.</param>
      /// <param name="order">Перечисление MatrixOrder, указывающее порядок (в конце или в начале), в котором применяется поворот.</param>
      public void RotateAt(double angle, PointD point, MatrixOrder order)
      {
        if (order == MatrixD.MatrixOrder.Prepend)
        {
          this.Translate(point.X, point.Y, order);
          this.Rotate(angle, order);
          this.Translate(-point.X, -point.Y, order);
        }
        else
        {
          this.Translate(-point.X, -point.Y, order);
          this.Rotate(angle, order);
          this.Translate(point.X, point.Y, order);
        }
      }

      /// <summary>Применяет указанный вектор масштабирования к объекту MatrixD, добавляя вектор в начало.</summary>
      /// <param name="scaleX">Коэффициент изменения масштаба объекта MatrixD вдоль оси Х.</param>
      /// <param name="scaleY">Коэффициент изменения масштаба объекта MatrixD вдоль оси Y.</param>
      public void Scale(double scaleX, double scaleY)
      {
        this.Scale(scaleX, scaleY, MatrixD.MatrixOrder.Prepend);
      }

      /// <summary>Применяет указанный вектор масштабирования (scaleX и scaleY) к объекту MatrixD в указанном порядке.</summary>
      /// <param name="scaleX">Коэффициент изменения масштаба объекта MatrixD вдоль оси Х.</param>
      /// <param name="scaleY">Коэффициент изменения масштаба объекта MatrixD вдоль оси Y.</param>
      /// <param name="order">Перечисление MatrixOrder, указывающее порядок (в конце или в начале), в котором вектор масштабирования применяется к объекту MatrixD.</param>
      public void Scale(double scaleX, double scaleY, MatrixOrder order)
      {
        using (MatrixD matrix = new MatrixD())
        {
          matrix._data[0, 0] = scaleX;
          matrix._data[1, 1] = scaleY;
          this.Multiply(matrix, order);
        }
      }

      /// <summary>Применяет указанный вектор сдвига к объекту MatrixD, добавляя преобразование сдвига в начало.</summary>
      /// <param name="shearX">Коэффициент горизонтального сдвига.</param>
      /// <param name="shearY">Коэффициент вертикального сдвига. </param>
      public void Shear(double shearX, double shearY)
      {
        this.Shear(shearX, shearY, MatrixD.MatrixOrder.Prepend);
      }

      /// <summary>Применяет указанный вектор сдвига к объекту MatrixD в указанном порядке.</summary>
      /// <param name="shearX">Коэффициент горизонтального сдвига.</param>
      /// <param name="shearY">Коэффициент вертикального сдвига.</param>
      /// <param name="order">Перечисление MatrixOrder, указывающее порядок (в конце или в начале), в котором применяется сдвиг.</param>
      public void Shear(double shearX, double shearY, MatrixOrder order)
      {
        using (MatrixD matrix = new MatrixD())
        {
          matrix._data[1, 0] = shearX;
          matrix._data[0, 1] = shearY;
          this.Multiply(matrix, order);
        }
      }

      /// <summary>Применяет геометрическое преобразование, представляемое объектом MatrixD, к указанному массиву точек.</summary>
      /// <param name="pts">Массив структур PointD, который представлет точки для преобразования.</param>
      public void TransformPoints(PointD[] pts)
      {
        if (pts == null)
          throw new ArgumentNullException(nameof (pts));
        for (int index = 0; index < pts.Length; ++index)
        {
          double x = pts[index].X;
          double y = pts[index].Y;
          pts[index].X = this._data[0, 0] * x + this._data[0, 1] * y + this._dx;
          pts[index].Y = this._data[0, 1] * x + this._data[1, 1] * y + this._dy;
        }
      }

      /// <summary>Умножает каждый вектор массива на матрицу. Элементы сдвига данной матрицы (третья строка) игнорируются.</summary>
      /// <param name="pts">Массив структур Point, который представляет точки для преобразования.</param>
      public void TransformVectors(PointD[] pts)
      {
        if (pts == null)
          throw new ArgumentNullException(nameof (pts));
        for (int index = 0; index < pts.Length; ++index)
        {
          double x = pts[index].X;
          double y = pts[index].Y;
          pts[index].X = this._data[0, 0] * x + this._data[0, 1] * y;
          pts[index].Y = this._data[0, 1] * x + this._data[1, 1] * y;
        }
      }

      /// <summary>Применяет указанный вектор сдвига (offsetX и offsetY) к объекту MatrixD, добавляя вектор в начало.</summary>
      /// <param name="offsetX">Величина x, на которую сдвигается объект MatrixD.</param>
      /// <param name="offsetY">Величина y, на которую сдвигается объект MatrixD.</param>
      public void Translate(double offsetX, double offsetY)
      {
        this.Translate(offsetX, offsetY, MatrixD.MatrixOrder.Prepend);
      }

      /// <summary>Применяет указанный вектор сдвига к объекту MatrixD в указанном порядке.</summary>
      /// <param name="offsetX">Величина x, на которую сдвигается объект MatrixD.</param>
      /// <param name="offsetY">Величина y, на которую сдвигается объект MatrixD.</param>
      /// <param name="order">Перечисление MatrixOrder, указывающее порядок (в конце или в начале), в котором сдвиг применяется к объекту MatrixD.</param>
      public void Translate(double offsetX, double offsetY, MatrixOrder order)
      {
        using (MatrixD matrix = new MatrixD())
        {
          matrix._dx = offsetX;
          matrix._dy = offsetY;
          this.Multiply(matrix, order);
        }
      }

      /// <summary>Получает значение, определяющее, является ли объект MatrixD единичной матрицей.</summary>
      /// <returns>Это свойство имеет значение true, если матрица Matrix единичная; в противном случае false.</returns>
      public bool IsIdentity => false;

      /// <summary>Получает значение, определяющее, является ли объект MatrixD обратимым.</summary>
      /// <returns>Это свойство имеет значение true, если матрица Matrix является обратимой; в противном случае false.</returns>
      public bool IsInvertible => false;

      /// <summary>Получает значение сдвига по x (значение dx, или элемент на пересечении третьей строки и первого столбца) объекта MatrixD.</summary>
      /// <returns>Значение сдвига по x объекта MatrixD.</returns>
      public double OffsetX => this._dx;

      /// <summary>Получает значение сдвига по y (значение dy, или элемент на пересечении третьей строки и второго столбца) объекта MatrixD.</summary>
      /// <returns>Значение сдвига по y объекта MatrixD.</returns>
      public double OffsetY => this._dy;

      /// <summary> Указывает порядок матричных преобразований. </summary>
      /// <remarks>Матричные преобразования не обязательно перестановочны. Важен порядок их применения. </remarks>
      [Serializable]
      public enum MatrixOrder
      {
        /// <summary> Новое преобразование применяется после старого. </summary>
        Append,
        /// <summary> Новое преобразование применяется перед старым. </summary>
        Prepend,
      }
    }
}
