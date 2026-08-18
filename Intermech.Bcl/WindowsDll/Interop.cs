
// Type: Intermech.WindowsDll.Interop
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Drawing;
using System.Globalization;
using System.Runtime.CompilerServices;


namespace Intermech.WindowsDll
{
    public static class Interop
    {
      public static int MakeLParam(short LoWord, short HiWord)
      {
        return (int) HiWord << 16 /*0x10*/ | (int) LoWord & (int) ushort.MaxValue;
      }

      /// <summary>Structure, that defines the coordinates of the upper-left and lower-right corners of a rectangle</summary>
      [Serializable]
      public struct RECT(int left, int top, int width, int height)
      {
        public int Left = left;
        public int Top = top;
        public int Right = left + width;
        public int Bottom = top + height;

        public int Width
        {
          [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.Right - this.Left;
        }

        public int Height
        {
          [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.Bottom - this.Top;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Rectangle(RECT source)
        {
          return new Rectangle(source.Left, source.Top, source.Width, source.Height);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator RECT(Rectangle source)
        {
          return new RECT(source.Left, source.Top, source.Width, source.Height);
        }

        public Size Size
        {
          [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Size(this.Width, this.Height);
        }

        public override int GetHashCode()
        {
          return (this.Left, this.Top, this.Right, this.Bottom).GetHashCode();
        }

        public override bool Equals(object obj)
        {
          return obj is RECT rect && rect.Left == this.Left && rect.Top == this.Top && rect.Right == this.Right && rect.Bottom == this.Bottom;
        }

        public static bool operator ==(RECT left, RECT right)
        {
          return left.Left == right.Left && left.Top == right.Top && left.Right == right.Right && left.Bottom == right.Bottom;
        }

        public static bool operator !=(RECT left, RECT right) => !(left == right);

        public override string ToString()
        {
          return $"{{Left={this.Left.ToString((IFormatProvider) CultureInfo.CurrentCulture)},Top={this.Top.ToString((IFormatProvider) CultureInfo.CurrentCulture)},Right={this.Right.ToString((IFormatProvider) CultureInfo.CurrentCulture)},Bottom={this.Bottom.ToString((IFormatProvider) CultureInfo.CurrentCulture)}}}";
        }
      }

      /// <summary>The POINT structure defines the x- and y- coordinates of a point</summary>
      [Serializable]
      public struct POINT(int x, int y)
      {
        public int X = x;
        public int Y = y;

        public static implicit operator Point(POINT source) => new Point(source.X, source.Y);

        public static implicit operator POINT(Point source)
        {
          return new POINT(source.X, source.Y);
        }

        public override int GetHashCode() => this.X ^ this.Y;

        public override bool Equals(object obj)
        {
          return obj is POINT point && point.X == this.X && point.Y == this.Y;
        }

        public static bool operator ==(POINT left, POINT right)
        {
          return left.X == right.X && left.Y == right.Y;
        }

        public static bool operator !=(POINT left, POINT right) => !(left == right);

        public static Rectangle operator +(POINT point, SIZE size)
        {
          return new Rectangle(point.X, point.Y, size.Width, size.Height);
        }

        public static Rectangle operator +(POINT point, Size size)
        {
          return new Rectangle(point.X, point.Y, size.Width, size.Height);
        }

        public override string ToString()
        {
          return $"{{X={this.X.ToString((IFormatProvider) CultureInfo.CurrentCulture)},Y={this.Y.ToString((IFormatProvider) CultureInfo.CurrentCulture)}}}";
        }
      }

      /// <summary>The SIZE structure defines width and height</summary>
      [Serializable]
      public readonly struct SIZE(int width, int height)
      {
        public readonly int Width = width;
        public readonly int Height = height;

        public static implicit operator Size(SIZE source)
        {
          return new Size(source.Width, source.Height);
        }

        public static implicit operator SIZE(Size source)
        {
          return new SIZE(source.Width, source.Height);
        }

        public override int GetHashCode() => this.Width ^ this.Height;

        public override bool Equals(object obj)
        {
          return obj is SIZE size && size.Width == this.Width && size.Height == this.Height;
        }

        public static bool operator ==(SIZE left, SIZE right)
        {
          return left.Width == right.Width && left.Height == right.Height;
        }

        public static bool operator !=(SIZE left, SIZE right) => !(left == right);

        public override string ToString()
        {
          return $"{{Width={this.Width.ToString((IFormatProvider) CultureInfo.CurrentCulture)},Height={this.Height.ToString((IFormatProvider) CultureInfo.CurrentCulture)}}}";
        }
      }
    }
}
