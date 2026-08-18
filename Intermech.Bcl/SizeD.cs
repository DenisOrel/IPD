
// Type: Intermech.SizeD
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Diagnostics;
using System.Drawing;


namespace Intermech
{
    /// <summary>Содержит упорядоченную пару чисел с плавающей запятой, обычно ширину и высоту прямоугольника.</summary>
    [Serializable]
    public struct SizeD
    {
      /// <summary>Инициализирует новый экземпляр класса SizeD.</summary>
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      public static readonly SizeD Empty = new SizeD(0.0, 0.0);
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      private double _width;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      private double _height;

      /// <summary>Инициализирует новый экземпляр класса SizeD из указанного существующего SizeD.</summary>
      /// <param name="size">SizeD, из которого создается новый SizeD.</param>
      public SizeD(SizeD size)
      {
        this._width = size._width;
        this._height = size._height;
      }

      /// <summary>Инициализирует новый экземпляр класса SizeD из указанного PointD.</summary>
      /// <param name="pt">PointD, из которого инициализируется данный SizeD.</param>
      public SizeD(PointD pt)
      {
        this._width = pt.X;
        this._height = pt.Y;
      }

      /// <summary>Инициализирует новый экземпляр класса SizeD из указанных размеров.</summary>
      /// <param name="width">Компонент ширины нового SizeD.</param>
      /// <param name="height">Компонент высоты нового SizeD.</param>
      public SizeD(double width, double height)
      {
        this._width = width;
        this._height = height;
      }

      /// <summary>Получает или задает вертикальный компонент этого SizeD.</summary>
      /// <return>Вертикальный компонент этого SizeD.</return>
      public double Height
      {
        get => this._height;
        set => this._height = value;
      }

      /// <summary>Получает или задает горизонтальный компонент этого SizeD.</summary>
      /// <return>Горизонтальный компонент этого SizeD.</return>
      public double Width
      {
        get => this._width;
        set => this._width = value;
      }

      /// <summary>Получает значение, указывающее, имеет ли этот SizeD нулевые ширину и высоту.</summary>
      /// <return>Это свойство возвращает true, когда этот SizeD имеет нулевую ширину и высоту, в противном случае возвращается значение — false.</return>
      public bool IsEmpty => this._width == 0.0 && this._height == 0.0;

      /// <summary>Проверяет, является ли указанный объект SizeD с такими же размерами, что и SizeD.</summary>
      /// <param name="obj">Объект Object для проверки.</param>
      /// <returns>Данный метод возвращает значение true, если obj является SizeD с такой же шириной и высотой, как SizeD, в противном случае — значение false.</returns>
      public override bool Equals(object obj)
      {
        return obj is SizeD sizeD && sizeD._width == this._width && sizeD._height == this._height && sizeD.GetType().Equals(this.GetType());
      }

      /// <summary>Возвращает хеш-код для этой структуры SizeD.</summary>
      /// <returns>Целочисленное значение, определяющее значение хеша для этой структуры SizeD.</returns>
      public override int GetHashCode() => base.GetHashCode();

      /// <summary>Проверяет равенство двух структур SizeD.</summary>
      /// <param name="left">Структура SizeD, которая находится слева от оператора равенства.</param>
      /// <param name="right">Структура SizeD, которая находится справа от оператора равенства.</param>
      /// <returns>Этот оператор возвращает значение true, если left и right имеют равные ширину и высоту; в противном случае возвращается значение — false.</returns>
      public static bool operator ==(SizeD left, SizeD right)
      {
        return left._width == right._width && left._height == right._height;
      }

      /// <summary>Проверяет, различны ли две структуры SizeF.</summary>
      /// <param name="left">Структура SizeD, которая находится слева от оператора неравенства.</param>
      /// <param name="right">Структура SizeD, которая находится справа от оператора неравенства.</param>
      /// <returns>Этот оператор возвращает значение true, если left и right отличаются по ширине или по высоте; в противном случае возвращается значение — false.</returns>
      public static bool operator !=(SizeD left, SizeD right) => !(left == right);

      /// <summary>Прибавляет ширину и высоту одной структуры SizeD к ширине и высоте другой структуры SizeD.</summary>
      /// <param name="left">Структура SizeD, которая находится слева от оператора сложения.</param>
      /// <param name="right">Структура SizeD, которая находится справа от оператора сложения.</param>
      /// <returns>Структура SizeD, полученная в результате операции сложения.</returns>
      public static SizeD operator +(SizeD left, SizeD right)
      {
        return new SizeD(left._width + right._width, left._height + right._height);
      }

      /// <summary>Вычитает ширину и высоту одной структуры SizeD из ширины и высоты другой структуры SizeD.</summary>
      /// <param name="left">Структура SizeD, которая находится слева от оператора вычитания.</param>
      /// <param name="right">Структура SizeD, которая находится справа от оператора вычитания.</param>
      /// <returns>Структура SizeD, полученная в результате операции вычитания.</returns>
      public static SizeD operator -(SizeD left, SizeD right)
      {
        return new SizeD(left._width - right._width, left._height - right._height);
      }

      /// <summary>Преобразует указанный SizeD в PointD.</summary>
      /// <param name="size">Преобразуемый SizeD.</param>
      /// <returns>Структура PointD, полученная в результате операции преобразования.</returns>
      public static explicit operator PointD(SizeD size) => new PointD(size._width, size._height);

      /// <summary>Создает удобную для восприятия строку, представляющую SizeF.</summary>
      /// <returns>Строка, представляющая этот SizeF.</returns>
      public override string ToString()
      {
        return $"Width={(object) this._width}, Height={(object) this._height}";
      }

      /// <summary>Преобразует указанный SizeD в SizeF.</summary>
      /// <param name="size">Преобразуемый SizeD.</param>
      /// <returns>Структура SizeF, полученная в результате операции преобразования.</returns>
      public static SizeF ToSizeF(SizeD size) => new SizeF((float) size._width, (float) size._height);
    }
}
