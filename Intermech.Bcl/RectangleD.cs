
// Type: Intermech.RectangleD
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;


namespace Intermech
{
    /// <summary>Содержит набор из четырех чисел с плавающей запятой, определяющих расположение и размер прямоугольника.</summary>
    /// <remarks>Прямоугольник определяется шириной, высотой и положением левого верхнего угла.</remarks>
    [TypeConverter(typeof (RectangleDConverter))]
    [Serializable]
    public struct RectangleD
    {
      /// <summary>Представляет экземпляр класса RectangleD с неинициализированными членами.</summary>
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      public static readonly RectangleD Empty = new RectangleD(0.0, 0.0, 0.0, 0.0);
      /// <summary>Координата по оси X верхнего левого угла прямоугольника.</summary>
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      private double _x;
      /// <summary>Координата по оси Y верхнего левого угла прямоугольника.</summary>
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      private double _y;
      /// <summary>Ширина прямоугольника.</summary>
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      private double _width;
      /// <summary>Высота прямоугольника.</summary>
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      private double _height;

      /// <summary>Инициализирует новый экземпляр класса RectangleD с указанным расположением и размером.</summary>
      /// <param name="x">Координата по оси X верхнего левого угла прямоугольника.</param>
      /// <param name="y">Координата по оси Y верхнего левого угла прямоугольника.</param>
      /// <param name="width">Ширина прямоугольника.</param>
      /// <param name="height">Высота прямоугольника.</param>
      public RectangleD(double x, double y, double width, double height)
      {
        this._x = x;
        this._y = y;
        this._width = width;
        this._height = height;
      }

      /// <summary>Инициализирует новый экземпляр класса RectangleD с указанным расположением и размером.</summary>
      /// <param name="location">PointD, представляющий левый верхний угол прямоугольной области. </param>
      /// <param name="size">SizeD, представляющий ширину и высоту прямоугольной области.</param>
      public RectangleD(PointD location, SizeD size)
      {
        this._x = location.X;
        this._y = location.Y;
        this._width = size.Width;
        this._height = size.Height;
      }

      /// <summary>Возвращает или задает координату по оси X левого верхнего угла структуры RectangleD.</summary>
      public double X
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._x;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] set => this._x = value;
      }

      /// <summary>Возвращает или задает координату по оси Y левого верхнего угла структуры RectangleD. </summary>
      public double Y
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._y;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] set => this._y = value;
      }

      /// <summary>Возвращает или задает ширину структуры RectangleD. </summary>
      public double Width
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._width;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] set => this._width = value;
      }

      /// <summary>Получает или задает высоту структуры RectangleD. </summary>
      public double Height
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._height;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] set => this._height = value;
      }

      /// <summary>Получает координату по оси Y нижнего края структуры RectangleD. </summary>
      public double Bottom
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._y + this._height;
      }

      /// <summary>Возвращает координату по оси Y верхнего края структуры RectangleD.</summary>
      public double Top
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._y;
      }

      /// <summary>Возвращает координату по оси X левого края структуры RectangleD.</summary>
      public double Left
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._x;
      }

      /// <summary>Возвращает координату по оси X правого края структуры RectangleD. </summary>
      public double Right
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._x + this._width;
      }

      /// <summary>Проверяет, все ли числовые свойства этого RectangleD имеют нулевые значения. </summary>
      public bool IsEmpty
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return this.Width <= 0.0 || this.Height <= 0.0;
        }
      }

      /// <summary>Возвращает или задает координаты левого верхнего угла структуры RectangleD.</summary>
      public PointD Location
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new PointD(this._x, this._y);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] set
        {
          this._x = value.X;
          this._y = value.Y;
        }
      }

      /// <summary>Возвращает или задает размер этого RectangleD. </summary>
      public SizeD Size
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new SizeD(this._width, this._height);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] set
        {
          this._width = value.Width;
          this._height = value.Height;
        }
      }

      /// <summary>Определяет, содержится ли заданная точка в структуре RectangleD. </summary>
      /// <param name="pt">Объект PointD для проверки. </param>
      /// <returns>Этот метод возвращает true, если точка, представленная параметром pt, содержится в структуре RectangleD; в противном случае — значение false. </returns>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public bool Contains(PointD pt) => this.Contains(pt.X, pt.Y);

      /// <summary>Определяет, содержится ли вся прямоугольная область, представленная rect, в структуре RectangleD.</summary>
      /// <param name="rectangle">Объект RectangleD для проверки.</param>
      /// <returns>Этот метод возвращает true, если прямоугольная область, представленная rect, полностью содержится в прямоугольной области, представленной этим RectangleD; в противном случае — значение false.</returns>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public bool Contains(RectangleD rectangle)
      {
        return this.Left <= rectangle.Left && rectangle.Right <= this.Right && this.Top <= rectangle.Top && rectangle.Bottom <= this.Bottom;
      }

      /// <summary>Определяет, содержится ли заданная точка в структуре RectangleD.</summary>
      /// <param name="x">Координата проверяемой точки по оси X.</param>
      /// <param name="y">Координата проверяемой точки по оси Y.</param>
      /// <returns>Этот метод возвращает true, если точка, представленная параметром pt, содержится в структуре RectangleD; в противном случае — значение false.</returns>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public bool Contains(double x, double y) => this.ContainsX(x) && this.ContainsY(y);

      /// <summary>Проверяет, является ли obj прямоугольником RectangleD с таким же расположением и размером, что и RectangleD.</summary>
      /// <param name="obj">Объект Object для проверки.</param>
      /// <returns>Этот метод возвращает значение true, если obj является RectangleD и его свойства X, Y, Width и Height совпадают с соответствующими свойствами RectangleD; в противном случае — значение false.</returns>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public override bool Equals(object obj) => obj is RectangleD other && this.Equals(other);

      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public bool Equals(RectangleD other)
      {
        return other._x == this._x && other._y == this._y && other._width == this._width && other._height == this._height;
      }

      /// <summary>Возвращает хеш-код для этой структуры RectangleD.</summary>
      /// <returns>Хеш-код для этого RectangleD.</returns>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public override int GetHashCode()
      {
        return (int) (uint) this._x ^ ((int) (uint) this._y << 13 | (int) ((uint) this._y >> 19)) ^ ((int) (uint) this._width << 26 | (int) ((uint) this._width >> 6)) ^ ((int) (uint) this._height << 7 | (int) ((uint) this._height >> 25));
      }

      /// <summary>Проверяет, имеют ли две структуры RectangleD одинаковое положение и размер.</summary>
      /// <param name="left">Структура RectangleD, которая находится слева от оператора равенства.</param>
      /// <param name="right">Структура RectangleD, которая находится справа от оператора равенства.</param>
      /// <returns>Этот оператор возвращает true, если две указанные структуры RectangleD имеют одинаковые свойства X, Y, Width и Height.</returns>
      public static bool operator ==(RectangleD left, RectangleD right) => left.Equals(right);

      /// <summary>Проверяет, различаются ли две структуры RectangleD по положению или размеру.</summary>
      /// <param name="left">Структура RectangleD, которая находится слева от оператора равенства.</param>
      /// <param name="right">Структура RectangleD, которая находится справа от оператора равенства.</param>
      /// <returns>Этот оператор возвращает значение true, если значения каких-либо из свойств X, Y, Width или Height двух структур RectangleD не совпадают; в противном случае — значение false.</returns>
      public static bool operator !=(RectangleD left, RectangleD right) => !(left == right);

      /// <summary>Преобразует указанный Rectangle в RectangleD.</summary>
      /// <param name="value">Преобразуемый Rectangle.</param>
      /// <returns>Объект RectangleD, который является результатом преобразований этого метода.</returns>
      public static implicit operator RectangleD(Rectangle value)
      {
        return new RectangleD((double) value.X, (double) value.Y, (double) value.Width, (double) value.Height);
      }

      /// <summary>Преобразует указанный RectangleF в RectangleD.</summary>
      /// <param name="value">Преобразуемый RectangleF.</param>
      /// <returns>Объект RectangleD, который является результатом преобразований этого метода.</returns>
      public static implicit operator RectangleD(RectangleF value)
      {
        return new RectangleD((double) value.X, (double) value.Y, (double) value.Width, (double) value.Height);
      }

      /// <summary>Преобразует указанный объект RectangleD в объект RectangleF, усекая значения объекта RectangleD.</summary>
      /// <param name="rectangleD">Преобразуемый объект RectangleD.</param>
      /// <returns>Объект RectangleF, который является результатом преобразований этого метода.</returns>
      public static RectangleF ToRectangleF(RectangleD rectangleD)
      {
        return new RectangleF((float) rectangleD._x, (float) rectangleD._y, (float) rectangleD._width, (float) rectangleD._height);
      }

      /// <summary>Преобразует Location и Size этого RectangleD в удобную для восприятия строку. </summary>
      /// <returns>Строка, содержащая положение, ширину и высоту данной структуры RectangleD</returns>
      public override string ToString()
      {
        return $"X={(object) this._x}, Y={(object) this._y}, Width={(object) this._width}, Height= {(object) this._height}";
      }

      /// <summary>Увеличивает структуру этого RectangleD на указанную величину.</summary>
      /// <param name="x">Величина, на которую увеличится структура RectangleD в горизонтальном направлении.</param>
      /// <param name="y">Величина, на которую увеличится структура RectangleD в вертикальном направлении.</param>
      public void Inflate(double x, double y)
      {
        this._x -= x;
        this._y -= y;
        this._width += 2.0 * x;
        this._height += 2.0 * y;
      }

      /// <summary>Увеличивает этот RectangleD на указанную величину.</summary>
      /// <param name="size">Величина, на которую увеличится данный прямоугольник.</param>
      public void Inflate(SizeD size) => this.Inflate(size.Width, size.Height);

      /// <summary>Создает и возвращает увеличенную копию указанной структуры RectangleD. Копия увеличивается на указанную величину. Исходный прямоугольник остается без изменений.</summary>
      /// <param name="rect">Копируемый RectangleD. Этот прямоугольник не был изменен.</param>
      /// <param name="x">Величина, на которую увеличится копия прямоугольника в горизонтальном направлении.</param>
      /// <param name="y">Величина, на которую увеличится копия прямоугольника в вертикальном направлении.</param>
      /// <returns>Увеличенный RectangleD.</returns>
      public static RectangleD Inflate(RectangleD rect, double x, double y)
      {
        RectangleD rectangleD = rect;
        rectangleD.Inflate(x, y);
        return rectangleD;
      }

      /// <summary>Заменяет структуру RectangleD ее пересечением с указанной структурой RectangleD.</summary>
      /// <param name="rect">Прямоугольник для пересечения.</param>
      public void Intersect(RectangleD rect)
      {
        RectangleD rectangleD = RectangleD.Intersect(rect, this);
        this._x = rectangleD._x;
        this._y = rectangleD._y;
        this._width = rectangleD._width;
        this._height = rectangleD._height;
      }

      /// <summary>Возвращает структуру RectangleD, представляющую пересечение двух прямоугольников. Если пересечение отсутствует, возвращается пустое значение.</summary>
      /// <param name="first">Прямоугольник для пересечения.</param>
      /// <param name="second">Прямоугольник для пересечения.</param>
      /// <returns>Третья структура RectangleD, размер которой представляет область наложения двух указанных прямоугольников.</returns>
      public static RectangleD Intersect(RectangleD first, RectangleD second)
      {
        double x = Math.Max(first.Left, second.Left);
        double num1 = Math.Min(first.Right, second.Right);
        double y = Math.Max(first.Top, second.Top);
        double num2 = Math.Min(first.Bottom, second.Bottom);
        return num1 >= x && num2 >= y ? new RectangleD(x, y, num1 - x, num2 - y) : RectangleD.Empty;
      }

      /// <summary>Определяет, пересекается ли данный прямоугольник с rect.</summary>
      /// <param name="rect">Прямоугольник для проверки.</param>
      /// <returns>Этот метод возвращает true, если имеется какое-либо пересечение.</returns>
      public bool IntersectsWith(RectangleD rect)
      {
        return rect.Left < this.Right && this.Left < rect.Right && rect.Top < this.Bottom && this.Top < rect.Bottom;
      }

      /// <summary>Изменяет положение этого прямоугольника на указанную величину.</summary>
      /// <param name="pos">Величина смещения.</param>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public void Offset(PointD pos) => this.Offset(pos.X, pos.Y);

      /// <summary>Изменяет положение этого прямоугольника на указанную величину.</summary>
      /// <param name="x">Величина смещения по горизонтали.</param>
      /// <param name="y">Величина смещения по вертикали.</param>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public void Offset(double x, double y)
      {
        this._x += x;
        this._y += y;
      }

      /// <summary>Создает третий прямоугольник минимального размера, позволяющего ему вместить оба прямоугольника, образующих объединение.</summary>
      /// <param name="first">Прямоугольник для объединения.</param>
      /// <param name="second">Прямоугольник для объединения.</param>
      /// <returns>Структура третьего RectangleD, содержащая оба прямоугольника, образующих объединение.</returns>
      public static RectangleD Union(RectangleD first, RectangleD second)
      {
        double x = Math.Min(first.Left, second.Left);
        double num1 = Math.Max(first.Right, second.Right);
        double y = Math.Min(first.Top, second.Top);
        double num2 = Math.Max(first.Bottom, second.Bottom);
        return new RectangleD(x, y, num1 - x, num2 - y);
      }

      /// <summary>геометрический центр прямоугольника</summary>
      public PointD Center
      {
        get => new PointD((this.Right + this.Left) / 2.0, (this.Bottom + this.Top) / 2.0);
      }

      /// <summary>Определяет, содержится ли заданная x-координата в структуре RectangleD.</summary>
      /// <param name="x">Координата проверяемой точки по оси X.</param>
      /// <returns>Этот метод возвращает true, если x-координата, содержится в структуре RectangleD; в противном случае — значение false.</returns>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public bool ContainsX(double x) => this.Left <= x && x <= this.Right;

      /// <summary>Определяет, содержится ли заданная y-координата в структуре RectangleD.</summary>
      /// <param name="y">Координата проверяемой точки по оси Y.</param>
      /// <returns>Этот метод возвращает true, если y-координата, содержится в структуре RectangleD; в противном случае — значение false.</returns>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public bool ContainsY(double y) => this.Top <= y && y <= this.Bottom;

      /// <summary>проекция точки на прямоугольник.</summary>
      /// <param name="x">x- координата точки</param>
      /// <param name="y">y- координата точки</param>
      /// <returns>точки после преобразования</returns>
      public PointD PerimeterTranslation(double x, double y)
      {
        double num1 = Math.Min(Math.Abs(x - this.Left), Math.Abs(x - this.Right));
        double num2 = Math.Min(Math.Abs(y - this.Top), Math.Abs(y - this.Bottom));
        bool flag1 = this.ContainsX(x);
        bool flag2 = this.ContainsY(y);
        PointD center;
        double x1;
        if (flag1 && (!(flag1 & flag2) || num1 >= num2))
        {
          x1 = x;
        }
        else
        {
          double num3 = x;
          center = this.Center;
          double x2 = center.X;
          x1 = num3 <= x2 ? this.Left : this.Right;
        }
        double num4;
        if (flag2 && (!(flag1 & flag2) || num2 >= num1))
        {
          num4 = y;
        }
        else
        {
          double num5 = y;
          center = this.Center;
          double y1 = center.Y;
          num4 = num5 <= y1 ? this.Top : this.Bottom;
        }
        double y2 = num4;
        return new PointD(x1, y2);
      }

      /// <summary>проекция точки на прямоугольник.</summary>
      /// <param name="pt">точка для преобразования</param>
      /// <returns>точки после преобразования</returns>
      public PointD PerimeterTranslation(PointD pt) => this.PerimeterTranslation(pt.X, pt.Y);
    }
}
