
// Type: Intermech.MathUtils
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Runtime.CompilerServices;


namespace Intermech
{
    public static class MathUtils
    {
      public const float Eps = 1E-05f;

      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static bool AlmostEqual(double x, double y) => Math.Abs(x - y) < 9.9999997473787516E-06;

      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static bool AlmostEqual(float x, float y)
      {
        return (double) Math.Abs(x - y) < 9.9999997473787516E-06;
      }

      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static bool AlmostZero(double v) => Math.Abs(v) < 9.9999997473787516E-06;

      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static bool AlmostZero(float v) => (double) Math.Abs(v) < 9.9999997473787516E-06;

      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static double Chop(double v) => !MathUtils.AlmostZero(v) ? v : 0.0;

      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static float Chop(float v) => !MathUtils.AlmostZero(v) ? v : 0.0f;
    }
}
