// Decompiled with JetBrains decompiler
// Type: Intermech.Int
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech;

public static class Int
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int Coalesce(int val1, int val2) => val1 == 0 ? val2 : val1;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int Coalesce(int val1, int val2, int val3)
  {
    if (val1 != 0)
      return val1;
    return val2 == 0 ? val3 : val2;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int Coalesce(int val1, int val2, int val3, int val4)
  {
    if (val1 != 0)
      return val1;
    if (val2 != 0)
      return val2;
    return val3 == 0 ? val4 : val3;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int Coalesce(int val1, int val2, int val3, int val4, int val5)
  {
    if (val1 != 0)
      return val1;
    if (val2 != 0)
      return val2;
    if (val3 != 0)
      return val3;
    return val4 == 0 ? val5 : val4;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int Coalesce(int val1, int val2, int val3, int val4, int val5, int val6)
  {
    if (val1 != 0)
      return val1;
    if (val2 != 0)
      return val2;
    if (val3 != 0)
      return val3;
    if (val4 != 0)
      return val4;
    return val5 == 0 ? val6 : val5;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int Coalesce(
    int val1,
    int val2,
    int val3,
    int val4,
    int val5,
    int val6,
    int val7)
  {
    if (val1 != 0)
      return val1;
    if (val2 != 0)
      return val2;
    if (val3 != 0)
      return val3;
    if (val4 != 0)
      return val4;
    if (val5 != 0)
      return val5;
    return val6 == 0 ? val7 : val6;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int Coalesce(
    int val1,
    int val2,
    int val3,
    int val4,
    int val5,
    int val6,
    int val7,
    int val8)
  {
    if (val1 != 0)
      return val1;
    if (val2 != 0)
      return val2;
    if (val3 != 0)
      return val3;
    if (val4 != 0)
      return val4;
    if (val5 != 0)
      return val5;
    if (val6 != 0)
      return val6;
    return val7 == 0 ? val8 : val7;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int Coalesce(
    int val1,
    int val2,
    int val3,
    int val4,
    int val5,
    int val6,
    int val7,
    int val8,
    int val9)
  {
    if (val1 != 0)
      return val1;
    if (val2 != 0)
      return val2;
    if (val3 != 0)
      return val3;
    if (val4 != 0)
      return val4;
    if (val5 != 0)
      return val5;
    if (val6 != 0)
      return val6;
    if (val7 != 0)
      return val7;
    return val8 == 0 ? val9 : val8;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int Coalesce(
    int val1,
    int val2,
    int val3,
    int val4,
    int val5,
    int val6,
    int val7,
    int val8,
    int val9,
    int val10)
  {
    if (val1 != 0)
      return val1;
    if (val2 != 0)
      return val2;
    if (val3 != 0)
      return val3;
    if (val4 != 0)
      return val4;
    if (val5 != 0)
      return val5;
    if (val6 != 0)
      return val6;
    if (val7 != 0)
      return val7;
    if (val8 != 0)
      return val8;
    return val9 == 0 ? val10 : val9;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int Coalesce(
    int val1,
    int val2,
    int val3,
    int val4,
    int val5,
    int val6,
    int val7,
    int val8,
    int val9,
    int val10,
    int val11)
  {
    if (val1 != 0)
      return val1;
    if (val2 != 0)
      return val2;
    if (val3 != 0)
      return val3;
    if (val4 != 0)
      return val4;
    if (val5 != 0)
      return val5;
    if (val6 != 0)
      return val6;
    if (val7 != 0)
      return val7;
    if (val8 != 0)
      return val8;
    if (val9 != 0)
      return val9;
    return val10 == 0 ? val11 : val10;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int Coalesce(
    int val1,
    int val2,
    int val3,
    int val4,
    int val5,
    int val6,
    int val7,
    int val8,
    int val9,
    int val10,
    int val11,
    int val12)
  {
    if (val1 != 0)
      return val1;
    if (val2 != 0)
      return val2;
    if (val3 != 0)
      return val3;
    if (val4 != 0)
      return val4;
    if (val5 != 0)
      return val5;
    if (val6 != 0)
      return val6;
    if (val7 != 0)
      return val7;
    if (val8 != 0)
      return val8;
    if (val9 != 0)
      return val9;
    if (val10 != 0)
      return val10;
    return val11 == 0 ? val12 : val11;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int Coalesce(
    int val1,
    int val2,
    int val3,
    int val4,
    int val5,
    int val6,
    int val7,
    int val8,
    int val9,
    int val10,
    int val11,
    int val12,
    int val13)
  {
    if (val1 != 0)
      return val1;
    if (val2 != 0)
      return val2;
    if (val3 != 0)
      return val3;
    if (val4 != 0)
      return val4;
    if (val5 != 0)
      return val5;
    if (val6 != 0)
      return val6;
    if (val7 != 0)
      return val7;
    if (val8 != 0)
      return val8;
    if (val9 != 0)
      return val9;
    if (val10 != 0)
      return val10;
    if (val11 != 0)
      return val11;
    return val12 == 0 ? val13 : val12;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int Coalesce(
    int val1,
    int val2,
    int val3,
    int val4,
    int val5,
    int val6,
    int val7,
    int val8,
    int val9,
    int val10,
    int val11,
    int val12,
    int val13,
    int val14)
  {
    if (val1 != 0)
      return val1;
    if (val2 != 0)
      return val2;
    if (val3 != 0)
      return val3;
    if (val4 != 0)
      return val4;
    if (val5 != 0)
      return val5;
    if (val6 != 0)
      return val6;
    if (val7 != 0)
      return val7;
    if (val8 != 0)
      return val8;
    if (val9 != 0)
      return val9;
    if (val10 != 0)
      return val10;
    if (val11 != 0)
      return val11;
    if (val12 != 0)
      return val12;
    return val13 == 0 ? val14 : val13;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int Coalesce(
    int val1,
    int val2,
    int val3,
    int val4,
    int val5,
    int val6,
    int val7,
    int val8,
    int val9,
    int val10,
    int val11,
    int val12,
    int val13,
    int val14,
    int val15)
  {
    if (val1 != 0)
      return val1;
    if (val2 != 0)
      return val2;
    if (val3 != 0)
      return val3;
    if (val4 != 0)
      return val4;
    if (val5 != 0)
      return val5;
    if (val6 != 0)
      return val6;
    if (val7 != 0)
      return val7;
    if (val8 != 0)
      return val8;
    if (val9 != 0)
      return val9;
    if (val10 != 0)
      return val10;
    if (val11 != 0)
      return val11;
    if (val12 != 0)
      return val12;
    if (val13 != 0)
      return val13;
    return val14 == 0 ? val15 : val14;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int Coalesce(
    int val1,
    int val2,
    int val3,
    int val4,
    int val5,
    int val6,
    int val7,
    int val8,
    int val9,
    int val10,
    int val11,
    int val12,
    int val13,
    int val14,
    int val15,
    int val16)
  {
    if (val1 != 0)
      return val1;
    if (val2 != 0)
      return val2;
    if (val3 != 0)
      return val3;
    if (val4 != 0)
      return val4;
    if (val5 != 0)
      return val5;
    if (val6 != 0)
      return val6;
    if (val7 != 0)
      return val7;
    if (val8 != 0)
      return val8;
    if (val9 != 0)
      return val9;
    if (val10 != 0)
      return val10;
    if (val11 != 0)
      return val11;
    if (val12 != 0)
      return val12;
    if (val13 != 0)
      return val13;
    if (val14 != 0)
      return val14;
    return val15 == 0 ? val16 : val15;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int Coalesce(
    int val1,
    int val2,
    int val3,
    int val4,
    int val5,
    int val6,
    int val7,
    int val8,
    int val9,
    int val10,
    int val11,
    int val12,
    int val13,
    int val14,
    int val15,
    int val16,
    int val17)
  {
    if (val1 != 0)
      return val1;
    if (val2 != 0)
      return val2;
    if (val3 != 0)
      return val3;
    if (val4 != 0)
      return val4;
    if (val5 != 0)
      return val5;
    if (val6 != 0)
      return val6;
    if (val7 != 0)
      return val7;
    if (val8 != 0)
      return val8;
    if (val9 != 0)
      return val9;
    if (val10 != 0)
      return val10;
    if (val11 != 0)
      return val11;
    if (val12 != 0)
      return val12;
    if (val13 != 0)
      return val13;
    if (val14 != 0)
      return val14;
    if (val15 != 0)
      return val15;
    return val16 == 0 ? val17 : val16;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int Coalesce(
    int val1,
    int val2,
    int val3,
    int val4,
    int val5,
    int val6,
    int val7,
    int val8,
    int val9,
    int val10,
    int val11,
    int val12,
    int val13,
    int val14,
    int val15,
    int val16,
    int val17,
    int val18)
  {
    if (val1 != 0)
      return val1;
    if (val2 != 0)
      return val2;
    if (val3 != 0)
      return val3;
    if (val4 != 0)
      return val4;
    if (val5 != 0)
      return val5;
    if (val6 != 0)
      return val6;
    if (val7 != 0)
      return val7;
    if (val8 != 0)
      return val8;
    if (val9 != 0)
      return val9;
    if (val10 != 0)
      return val10;
    if (val11 != 0)
      return val11;
    if (val12 != 0)
      return val12;
    if (val13 != 0)
      return val13;
    if (val14 != 0)
      return val14;
    if (val15 != 0)
      return val15;
    if (val16 != 0)
      return val16;
    return val17 == 0 ? val18 : val17;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int Coalesce(
    int val1,
    int val2,
    int val3,
    int val4,
    int val5,
    int val6,
    int val7,
    int val8,
    int val9,
    int val10,
    int val11,
    int val12,
    int val13,
    int val14,
    int val15,
    int val16,
    int val17,
    int val18,
    int val19)
  {
    if (val1 != 0)
      return val1;
    if (val2 != 0)
      return val2;
    if (val3 != 0)
      return val3;
    if (val4 != 0)
      return val4;
    if (val5 != 0)
      return val5;
    if (val6 != 0)
      return val6;
    if (val7 != 0)
      return val7;
    if (val8 != 0)
      return val8;
    if (val9 != 0)
      return val9;
    if (val10 != 0)
      return val10;
    if (val11 != 0)
      return val11;
    if (val12 != 0)
      return val12;
    if (val13 != 0)
      return val13;
    if (val14 != 0)
      return val14;
    if (val15 != 0)
      return val15;
    if (val16 != 0)
      return val16;
    if (val17 != 0)
      return val17;
    return val18 == 0 ? val19 : val18;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int Coalesce(
    int val1,
    int val2,
    int val3,
    int val4,
    int val5,
    int val6,
    int val7,
    int val8,
    int val9,
    int val10,
    int val11,
    int val12,
    int val13,
    int val14,
    int val15,
    int val16,
    int val17,
    int val18,
    int val19,
    int val20)
  {
    if (val1 != 0)
      return val1;
    if (val2 != 0)
      return val2;
    if (val3 != 0)
      return val3;
    if (val4 != 0)
      return val4;
    if (val5 != 0)
      return val5;
    if (val6 != 0)
      return val6;
    if (val7 != 0)
      return val7;
    if (val8 != 0)
      return val8;
    if (val9 != 0)
      return val9;
    if (val10 != 0)
      return val10;
    if (val11 != 0)
      return val11;
    if (val12 != 0)
      return val12;
    if (val13 != 0)
      return val13;
    if (val14 != 0)
      return val14;
    if (val15 != 0)
      return val15;
    if (val16 != 0)
      return val16;
    if (val17 != 0)
      return val17;
    if (val18 != 0)
      return val18;
    return val19 == 0 ? val20 : val19;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int Coalesce(
    int val1,
    int val2,
    int val3,
    int val4,
    int val5,
    int val6,
    int val7,
    int val8,
    int val9,
    int val10,
    int val11,
    int val12,
    int val13,
    int val14,
    int val15,
    int val16,
    int val17,
    int val18,
    int val19,
    int val20,
    [NotNull, NotEmpty] params int[] otherValues)
  {
    int num = val1 != 0 ? val1 : (val2 != 0 ? val2 : (val3 != 0 ? val3 : (val4 != 0 ? val4 : (val5 != 0 ? val5 : (val6 != 0 ? val6 : (val7 != 0 ? val7 : (val8 != 0 ? val8 : (val9 != 0 ? val9 : (val10 != 0 ? val10 : (val11 != 0 ? val11 : (val12 != 0 ? val12 : (val13 != 0 ? val13 : (val14 != 0 ? val14 : (val15 != 0 ? val15 : (val16 != 0 ? val16 : (val17 != 0 ? val17 : (val18 != 0 ? val18 : (val19 != 0 ? val19 : val20))))))))))))))))));
    return num == 0 ? ((IEnumerable<int>) otherValues).FirstOrDefault<int>((Func<int, bool>) (val => val != 0), (Func<int>) (() => otherValues[otherValues.Length - 1])) : num;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int Coalesce([NotNull, NotEmpty] IEnumerable<int> enumeration)
  {
    int num1 = 0;
    foreach (int num2 in enumeration)
    {
      num1 = num2;
      if (num1 != 0)
        return num1;
    }
    return num1;
  }
}
