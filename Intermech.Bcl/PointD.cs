
// Type: Intermech.PointD
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
    /// <summary>Представляет упорядоченную пару координат Х и Y с плавающей запятой, определяющую точку на двумерной плоскости.</summary>
    /// <filterpriority>1</filterpriority>
    [TypeConverter(typeof (PointDConverter))]
    [DebuggerDisplay("({X} {Y})")]
    [Serializable]
    /// <summary>Инициализирует новый экземпляр класса PointD с указанными координатами.</summary>
    /// <param name="x">Горизонтальное положение точки.</param>
    /// <param name="y">Вертикальное положение точки.</param>
    public struct PointD(double x, double y) : IEquatable<PointD>
    {
      /// <summary>Представляет новый экземпляр класса PointD с неинициализированными данными членов.</summary>
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      public static readonly PointD Empty = new PointD(0.0, 0.0);
      /// <summary> координата X </summary>
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      private double _x = x;
      /// <summary> координата Y </summary>
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      private double _y = y;

      /// <summary>Инициализирует новый экземпляр класса PointD с указанными координатами.</summary>
      /// <param name="pointF">точка типа  PointF</param>
      public PointD(PointF pointF)
        : this((double) pointF.X, (double) pointF.Y)
      {
      }

      /// <summary>Получает значение, определяющее, пуст ли класс PointD. </summary>
      /// <returns>Это свойство возвращает значение true, если значения параметров X и Y равны нулю; в противном случае — false.</returns>
      public bool IsEmpty
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this == PointD.Empty;
      }

      /// <summary>Получает координату Х PointD.</summary>
      /// <returns>Координата Х PointD.</returns>
      public double X
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._x;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] set => this._x = value;
      }

      /// <summary>Получает координату Y PointD.</summary>
      /// <returns>Координата Y PointD.</returns>
      public double Y
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._y;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] set => this._y = value;
      }

      /// <summary>Сравнивает две структуры PointD. Результат указывает, равны значения свойств X и Y двух объектов PointD или нет.</summary>
      /// <param name="left">Класс PointD для сравнения.</param>
      /// <param name="right">Класс PointD для сравнения.</param>
      /// <returns>Возвращает значение true, если значения свойств X и Y левого и правого объектов PointD равны; в противном случае — false.</returns>
      public static bool operator ==(PointD left, PointD right) => left.Equals(right);

      /// <summary>Сравнивает два объекта PointD. Результат указывает несовпадение значений свойств X или Y двух объектов PointD.</summary>
      /// <param name="left">Класс PointD для сравнения.</param>
      /// <param name="right">Класс PointD для сравнения.</param>
      /// <returns>Оператор возвращает значение true, если значения свойств Х и Y левого и правого параметров различаются; в противном случае — false.</returns>
      public static bool operator !=(PointD left, PointD right) => !left.Equals(right);

      /// <summary>Задает, содержит объект PointD те же координаты, что и указанный объект Object, или нет. </summary>
      /// <param name="obj">Object для проверки.</param>
      /// <returns>Метод возвращает значение true, если obj является PointD и имеет такие же координаты, как и PointD.</returns>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public override bool Equals(object obj) => obj is PointD other && this.Equals(other);

      /// <summary>
      /// 
      /// </summary>
      /// <param name="other"></param>
      /// <returns></returns>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public bool Equals(PointD other) => this._x == other._x && this._y == other._y;

      /// <summary> Возвращает хеш-код объекта PointD.</summary>
      /// <returns>Целое значение, указывающее значение хеша для объекта PointD.</returns>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public override int GetHashCode() => (int) ((long) this._x ^ (long) this._y);

      /// <summary>Преобразует PointD в строку, доступную для чтения.</summary>
      /// <returns>Строка, представляющая PointD.</returns>
      public override string ToString() => $"X={(object) this.X}, Y={(object) this.Y}";

      /// <summary>Преобразует PointD при помощи заданного SizeD. </summary>
      /// <param name="point">Класс PointD для преобразования.</param>
      /// <param name="size">SizeD, указывающий пару чисел, которые нужно добавить к значениям координат pt.</param>
      /// <returns>Это свойство возвращает объект PointD.</returns>
      public static PointD operator +(PointD point, SizeD size)
      {
        return new PointD(point._x + size.Width, point._y + size.Height);
      }

      /// <summary>Преобразует PointD при помощи отрицательного значения заданного SizeD. </summary>
      /// <param name="point">Класс PointD для преобразования.</param>
      /// <param name="size">SizeD, указывающий пару чисел, которые нужно отнять из значений координат pt. </param>
      /// <returns>Это свойство возвращает объект PointD.</returns>
      public static PointD operator -(PointD point, SizeD size)
      {
        return new PointD(point._x - size.Width, point._y - size.Height);
      }

      /// <summary>Преобразует указанный объект PointD в объект PointF, усекая значения объекта PointD.</summary>
      /// <param name="pointD">Преобразуемый объект PointD.</param>
      /// <returns>Объект PointF, который является результатом преобразований этого метода.</returns>
      public static PointF ToPointF(PointD pointD) => new PointF((float) pointD._x, (float) pointD._y);

      /// <summary>Преобразует PointD на указанное число.</summary>
      /// <param name="x">Число для смещения координаты Х.</param>
      /// <param name="y">Число для смещения координаты Y.</param>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public void Offset(double x, double y)
      {
        this._x += x;
        this._y += y;
      }
    }
}
