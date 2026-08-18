
// Type: Intermech.HashCode
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System.Runtime.CompilerServices;


namespace Intermech
{
    /// <summary>Класс для работы с хэшкодами</summary>
    public class HashCode
    {
      /// <summary>Рекомбинация нескольких хэшкодов</summary>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static int Combine(int h1, int h2) => (h1 << 5) + h1 ^ h1;

      /// <summary>Рекомбинация нескольких хэшкодов</summary>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static int Combine(int h1, int h2, int h3) => (h1 << 5) + h1 ^ (h2 << 5) + h2 ^ h3;

      /// <summary>Рекомбинация нескольких хэшкодов</summary>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static int Combine(int h1, int h2, int h3, int h4)
      {
        return (h1 << 5) + h1 ^ (h2 << 5) + h2 ^ (h3 << 5) + h3 ^ h4;
      }

      /// <summary>Рекомбинация нескольких хэшкодов</summary>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static int Combine(int h1, int h2, int h3, int h4, int h5)
      {
        return (h1 << 5) + h1 ^ (h2 << 5) + h2 ^ (h3 << 5) + h3 ^ (h4 << 5) + h4 ^ h5;
      }

      /// <summary>Рекомбинация нескольких хэшкодов</summary>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static int Combine(int h1, int h2, int h3, int h4, int h5, int h6)
      {
        return (h1 << 5) + h1 ^ (h2 << 5) + h2 ^ (h3 << 5) + h3 ^ (h4 << 5) + h4 ^ (h5 << 5) + h5 ^ h6;
      }

      /// <summary>Рекомбинация нескольких хэшкодов</summary>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static int Combine(int h1, int h2, int h3, int h4, int h5, int h6, int h7)
      {
        return (h1 << 5) + h1 ^ (h2 << 5) + h2 ^ (h3 << 5) + h3 ^ (h4 << 5) + h4 ^ (h5 << 5) + h5 ^ (h6 << 5) + h6 ^ h7;
      }

      /// <summary>Рекомбинация нескольких хэшкодов</summary>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static int Combine(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8)
      {
        return (h1 << 5) + h1 ^ (h2 << 5) + h2 ^ (h3 << 5) + h3 ^ (h4 << 5) + h4 ^ (h5 << 5) + h5 ^ (h6 << 5) + h6 ^ (h7 << 5) + h7 ^ h8;
      }

      /// <summary>Рекомбинация нескольких хэшкодов</summary>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static int Combine(
        int h1,
        int h2,
        int h3,
        int h4,
        int h5,
        int h6,
        int h7,
        int h8,
        int h9)
      {
        return (h1 << 5) + h1 ^ (h2 << 5) + h2 ^ (h3 << 5) + h3 ^ (h4 << 5) + h4 ^ (h5 << 5) + h5 ^ (h6 << 5) + h6 ^ (h7 << 5) + h7 ^ (h8 << 5) + h8 ^ h9;
      }

      /// <summary>Рекомбинация нескольких хэшкодов</summary>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static int Combine(
        int h1,
        int h2,
        int h3,
        int h4,
        int h5,
        int h6,
        int h7,
        int h8,
        int h9,
        int h10)
      {
        return (h1 << 5) + h1 ^ (h2 << 5) + h2 ^ (h3 << 5) + h3 ^ (h4 << 5) + h4 ^ (h5 << 5) + h5 ^ (h6 << 5) + h6 ^ (h7 << 5) + h7 ^ (h8 << 5) + h8 ^ (h9 << 5) + h9 ^ h10;
      }

      /// <summary>Рекомбинация нескольких хэшкодов</summary>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static int Combine(
        int h1,
        int h2,
        int h3,
        int h4,
        int h5,
        int h6,
        int h7,
        int h8,
        int h9,
        int h10,
        int h11)
      {
        return (h1 << 5) + h1 ^ (h2 << 5) + h2 ^ (h3 << 5) + h3 ^ (h4 << 5) + h4 ^ (h5 << 5) + h5 ^ (h6 << 5) + h6 ^ (h7 << 5) + h7 ^ (h8 << 5) + h8 ^ (h9 << 5) + h9 ^ (h10 << 5) + h10 ^ h11;
      }

      /// <summary>Рекомбинация нескольких хэшкодов</summary>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static int Combine(
        int h1,
        int h2,
        int h3,
        int h4,
        int h5,
        int h6,
        int h7,
        int h8,
        int h9,
        int h10,
        int h11,
        int h12)
      {
        return (h1 << 5) + h1 ^ (h2 << 5) + h2 ^ (h3 << 5) + h3 ^ (h4 << 5) + h4 ^ (h5 << 5) + h5 ^ (h6 << 5) + h6 ^ (h7 << 5) + h7 ^ (h8 << 5) + h8 ^ (h9 << 5) + h9 ^ (h10 << 5) + h10 ^ (h11 << 5) + h11 ^ h12;
      }

      /// <summary>Рекомбинация нескольких хэшкодов</summary>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static int Combine(
        int h1,
        int h2,
        int h3,
        int h4,
        int h5,
        int h6,
        int h7,
        int h8,
        int h9,
        int h10,
        int h11,
        int h12,
        int h13)
      {
        return (h1 << 5) + h1 ^ (h2 << 5) + h2 ^ (h3 << 5) + h3 ^ (h4 << 5) + h4 ^ (h5 << 5) + h5 ^ (h6 << 5) + h6 ^ (h7 << 5) + h7 ^ (h8 << 5) + h8 ^ (h9 << 5) + h9 ^ (h10 << 5) + h10 ^ (h11 << 5) + h11 ^ (h12 << 5) + h12 ^ h13;
      }

      /// <summary>Рекомбинация нескольких хэшкодов</summary>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static int Combine(
        int h1,
        int h2,
        int h3,
        int h4,
        int h5,
        int h6,
        int h7,
        int h8,
        int h9,
        int h10,
        int h11,
        int h12,
        int h13,
        int h14)
      {
        return (h1 << 5) + h1 ^ (h2 << 5) + h2 ^ (h3 << 5) + h3 ^ (h4 << 5) + h4 ^ (h5 << 5) + h5 ^ (h6 << 5) + h6 ^ (h7 << 5) + h7 ^ (h8 << 5) + h8 ^ (h9 << 5) + h9 ^ (h10 << 5) + h10 ^ (h11 << 5) + h11 ^ (h12 << 5) + h12 ^ (h13 << 5) + h13 ^ h14;
      }

      /// <summary>Рекомбинация нескольких хэшкодов</summary>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static int Combine(
        int h1,
        int h2,
        int h3,
        int h4,
        int h5,
        int h6,
        int h7,
        int h8,
        int h9,
        int h10,
        int h11,
        int h12,
        int h13,
        int h14,
        int h15)
      {
        return (h1 << 5) + h1 ^ (h2 << 5) + h2 ^ (h3 << 5) + h3 ^ (h4 << 5) + h4 ^ (h5 << 5) + h5 ^ (h6 << 5) + h6 ^ (h7 << 5) + h7 ^ (h8 << 5) + h8 ^ (h9 << 5) + h9 ^ (h10 << 5) + h10 ^ (h11 << 5) + h11 ^ (h12 << 5) + h12 ^ (h13 << 5) + h13 ^ (h14 << 5) + h14 ^ h15;
      }

      /// <summary>Рекомбинация нескольких хэшкодов</summary>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static int Combine(
        int h1,
        int h2,
        int h3,
        int h4,
        int h5,
        int h6,
        int h7,
        int h8,
        int h9,
        int h10,
        int h11,
        int h12,
        int h13,
        int h14,
        int h15,
        int h16)
      {
        return (h1 << 5) + h1 ^ (h2 << 5) + h2 ^ (h3 << 5) + h3 ^ (h4 << 5) + h4 ^ (h5 << 5) + h5 ^ (h6 << 5) + h6 ^ (h7 << 5) + h7 ^ (h8 << 5) + h8 ^ (h9 << 5) + h9 ^ (h10 << 5) + h10 ^ (h11 << 5) + h11 ^ (h12 << 5) + h12 ^ (h13 << 5) + h13 ^ (h14 << 5) + h14 ^ (h15 << 5) + h15 ^ h16;
      }

      /// <summary>Рекомбинация нескольких хэшкодов</summary>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static int Combine(
        int h1,
        int h2,
        int h3,
        int h4,
        int h5,
        int h6,
        int h7,
        int h8,
        int h9,
        int h10,
        int h11,
        int h12,
        int h13,
        int h14,
        int h15,
        int h16,
        int h17)
      {
        return (h1 << 5) + h1 ^ (h2 << 5) + h2 ^ (h3 << 5) + h3 ^ (h4 << 5) + h4 ^ (h5 << 5) + h5 ^ (h6 << 5) + h6 ^ (h7 << 5) + h7 ^ (h8 << 5) + h8 ^ (h9 << 5) + h9 ^ (h10 << 5) + h10 ^ (h11 << 5) + h11 ^ (h12 << 5) + h12 ^ (h13 << 5) + h13 ^ (h14 << 5) + h14 ^ (h15 << 5) + h15 ^ (h16 << 5) + h16 ^ h17;
      }

      /// <summary>Рекомбинация нескольких хэшкодов</summary>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static int Combine(
        int h1,
        int h2,
        int h3,
        int h4,
        int h5,
        int h6,
        int h7,
        int h8,
        int h9,
        int h10,
        int h11,
        int h12,
        int h13,
        int h14,
        int h15,
        int h16,
        int h17,
        int h18)
      {
        return (h1 << 5) + h1 ^ (h2 << 5) + h2 ^ (h3 << 5) + h3 ^ (h4 << 5) + h4 ^ (h5 << 5) + h5 ^ (h6 << 5) + h6 ^ (h7 << 5) + h7 ^ (h8 << 5) + h8 ^ (h9 << 5) + h9 ^ (h10 << 5) + h10 ^ (h11 << 5) + h11 ^ (h12 << 5) + h12 ^ (h13 << 5) + h13 ^ (h14 << 5) + h14 ^ (h15 << 5) + h15 ^ (h16 << 5) + h16 ^ (h17 << 5) + h17 ^ h18;
      }

      /// <summary>Рекомбинация нескольких хэшкодов</summary>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static int Combine(
        int h1,
        int h2,
        int h3,
        int h4,
        int h5,
        int h6,
        int h7,
        int h8,
        int h9,
        int h10,
        int h11,
        int h12,
        int h13,
        int h14,
        int h15,
        int h16,
        int h17,
        int h18,
        int h19)
      {
        return (h1 << 5) + h1 ^ (h2 << 5) + h2 ^ (h3 << 5) + h3 ^ (h4 << 5) + h4 ^ (h5 << 5) + h5 ^ (h6 << 5) + h6 ^ (h7 << 5) + h7 ^ (h8 << 5) + h8 ^ (h9 << 5) + h9 ^ (h10 << 5) + h10 ^ (h11 << 5) + h11 ^ (h12 << 5) + h12 ^ (h13 << 5) + h13 ^ (h14 << 5) + h14 ^ (h15 << 5) + h15 ^ (h16 << 5) + h16 ^ (h17 << 5) + h17 ^ (h18 << 5) + h18 ^ h19;
      }

      /// <summary>Рекомбинация нескольких хэшкодов</summary>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static int Combine(
        int h1,
        int h2,
        int h3,
        int h4,
        int h5,
        int h6,
        int h7,
        int h8,
        int h9,
        int h10,
        int h11,
        int h12,
        int h13,
        int h14,
        int h15,
        int h16,
        int h17,
        int h18,
        int h19,
        int h20)
      {
        return (h1 << 5) + h1 ^ (h2 << 5) + h2 ^ (h3 << 5) + h3 ^ (h4 << 5) + h4 ^ (h5 << 5) + h5 ^ (h6 << 5) + h6 ^ (h7 << 5) + h7 ^ (h8 << 5) + h8 ^ (h9 << 5) + h9 ^ (h10 << 5) + h10 ^ (h11 << 5) + h11 ^ (h12 << 5) + h12 ^ (h13 << 5) + h13 ^ (h14 << 5) + h14 ^ (h15 << 5) + h15 ^ (h16 << 5) + h16 ^ (h17 << 5) + h17 ^ (h18 << 5) + h18 ^ (h19 << 5) + h19 ^ h20;
      }
    }
}
