// Decompiled with JetBrains decompiler
// Type: Intermech.Values
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

public static class Values
{
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T Coalesce<T>([CanBeNull] T val1, [CanBeNull] T val2)
  {
    return !EqualityComparer<T>.Default.Equals(val1, default (T)) ? val2 : val1;
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T Coalesce<T>([CanBeNull] T val1, [CanBeNull] T val2, [CanBeNull] T val3)
  {
    EqualityComparer<T> equalityComparer = EqualityComparer<T>.Default;
    if (equalityComparer.Equals(val1, default (T)))
      return val1;
    return !equalityComparer.Equals(val2, default (T)) ? val3 : val2;
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T Coalesce<T>([CanBeNull] T val1, [CanBeNull] T val2, [CanBeNull] T val3, [CanBeNull] T val4)
  {
    EqualityComparer<T> equalityComparer = EqualityComparer<T>.Default;
    if (equalityComparer.Equals(val1, default (T)))
      return val1;
    if (equalityComparer.Equals(val2, default (T)))
      return val2;
    return !equalityComparer.Equals(val3, default (T)) ? val4 : val3;
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T Coalesce<T>([CanBeNull] T val1, [CanBeNull] T val2, [CanBeNull] T val3, [CanBeNull] T val4, [CanBeNull] T val5)
  {
    EqualityComparer<T> equalityComparer = EqualityComparer<T>.Default;
    if (equalityComparer.Equals(val1, default (T)))
      return val1;
    if (equalityComparer.Equals(val2, default (T)))
      return val2;
    if (equalityComparer.Equals(val3, default (T)))
      return val3;
    return !equalityComparer.Equals(val4, default (T)) ? val5 : val4;
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T Coalesce<T>([CanBeNull] T val1, [CanBeNull] T val2, [CanBeNull] T val3, [CanBeNull] T val4, [CanBeNull] T val5, [CanBeNull] T val6)
  {
    EqualityComparer<T> equalityComparer = EqualityComparer<T>.Default;
    if (equalityComparer.Equals(val1, default (T)))
      return val1;
    if (equalityComparer.Equals(val2, default (T)))
      return val2;
    if (equalityComparer.Equals(val3, default (T)))
      return val3;
    if (equalityComparer.Equals(val4, default (T)))
      return val4;
    return !equalityComparer.Equals(val5, default (T)) ? val6 : val5;
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T Coalesce<T>([CanBeNull] T val1, [CanBeNull] T val2, [CanBeNull] T val3, [CanBeNull] T val4, [CanBeNull] T val5, [CanBeNull] T val6, [CanBeNull] T val7)
  {
    EqualityComparer<T> equalityComparer = EqualityComparer<T>.Default;
    if (equalityComparer.Equals(val1, default (T)))
      return val1;
    if (equalityComparer.Equals(val2, default (T)))
      return val2;
    if (equalityComparer.Equals(val3, default (T)))
      return val3;
    if (equalityComparer.Equals(val4, default (T)))
      return val4;
    if (equalityComparer.Equals(val5, default (T)))
      return val5;
    return !equalityComparer.Equals(val6, default (T)) ? val7 : val6;
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T Coalesce<T>([CanBeNull] T val1, [CanBeNull] T val2, [CanBeNull] T val3, [CanBeNull] T val4, [CanBeNull] T val5, [CanBeNull] T val6, [CanBeNull] T val7, [CanBeNull] T val8)
  {
    EqualityComparer<T> equalityComparer = EqualityComparer<T>.Default;
    if (equalityComparer.Equals(val1, default (T)))
      return val1;
    if (equalityComparer.Equals(val2, default (T)))
      return val2;
    if (equalityComparer.Equals(val3, default (T)))
      return val3;
    if (equalityComparer.Equals(val4, default (T)))
      return val4;
    if (equalityComparer.Equals(val5, default (T)))
      return val5;
    if (equalityComparer.Equals(val6, default (T)))
      return val6;
    return !equalityComparer.Equals(val7, default (T)) ? val8 : val7;
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T Coalesce<T>(
    [CanBeNull] T val1,
    [CanBeNull] T val2,
    [CanBeNull] T val3,
    [CanBeNull] T val4,
    [CanBeNull] T val5,
    [CanBeNull] T val6,
    [CanBeNull] T val7,
    [CanBeNull] T val8,
    [CanBeNull] T val9)
  {
    EqualityComparer<T> equalityComparer = EqualityComparer<T>.Default;
    if (equalityComparer.Equals(val1, default (T)))
      return val1;
    if (equalityComparer.Equals(val2, default (T)))
      return val2;
    if (equalityComparer.Equals(val3, default (T)))
      return val3;
    if (equalityComparer.Equals(val4, default (T)))
      return val4;
    if (equalityComparer.Equals(val5, default (T)))
      return val5;
    if (equalityComparer.Equals(val6, default (T)))
      return val6;
    if (equalityComparer.Equals(val7, default (T)))
      return val7;
    return !equalityComparer.Equals(val8, default (T)) ? val9 : val8;
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T Coalesce<T>(
    [CanBeNull] T val1,
    [CanBeNull] T val2,
    [CanBeNull] T val3,
    [CanBeNull] T val4,
    [CanBeNull] T val5,
    [CanBeNull] T val6,
    [CanBeNull] T val7,
    [CanBeNull] T val8,
    [CanBeNull] T val9,
    [CanBeNull] T val10)
  {
    EqualityComparer<T> equalityComparer = EqualityComparer<T>.Default;
    if (equalityComparer.Equals(val1, default (T)))
      return val1;
    if (equalityComparer.Equals(val2, default (T)))
      return val2;
    if (equalityComparer.Equals(val3, default (T)))
      return val3;
    if (equalityComparer.Equals(val4, default (T)))
      return val4;
    if (equalityComparer.Equals(val5, default (T)))
      return val5;
    if (equalityComparer.Equals(val6, default (T)))
      return val6;
    if (equalityComparer.Equals(val7, default (T)))
      return val7;
    if (equalityComparer.Equals(val8, default (T)))
      return val8;
    return !equalityComparer.Equals(val9, default (T)) ? val10 : val9;
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T Coalesce<T>(
    [CanBeNull] T val1,
    [CanBeNull] T val2,
    [CanBeNull] T val3,
    [CanBeNull] T val4,
    [CanBeNull] T val5,
    [CanBeNull] T val6,
    [CanBeNull] T val7,
    [CanBeNull] T val8,
    [CanBeNull] T val9,
    [CanBeNull] T val10,
    [CanBeNull] T val11)
  {
    EqualityComparer<T> equalityComparer = EqualityComparer<T>.Default;
    if (equalityComparer.Equals(val1, default (T)))
      return val1;
    if (equalityComparer.Equals(val2, default (T)))
      return val2;
    if (equalityComparer.Equals(val3, default (T)))
      return val3;
    if (equalityComparer.Equals(val4, default (T)))
      return val4;
    if (equalityComparer.Equals(val5, default (T)))
      return val5;
    if (equalityComparer.Equals(val6, default (T)))
      return val6;
    if (equalityComparer.Equals(val7, default (T)))
      return val7;
    if (equalityComparer.Equals(val8, default (T)))
      return val8;
    if (equalityComparer.Equals(val9, default (T)))
      return val9;
    return !equalityComparer.Equals(val10, default (T)) ? val11 : val10;
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T Coalesce<T>(
    [CanBeNull] T val1,
    [CanBeNull] T val2,
    [CanBeNull] T val3,
    [CanBeNull] T val4,
    [CanBeNull] T val5,
    [CanBeNull] T val6,
    [CanBeNull] T val7,
    [CanBeNull] T val8,
    [CanBeNull] T val9,
    [CanBeNull] T val10,
    [CanBeNull] T val11,
    [CanBeNull] T val12)
  {
    EqualityComparer<T> equalityComparer = EqualityComparer<T>.Default;
    if (equalityComparer.Equals(val1, default (T)))
      return val1;
    if (equalityComparer.Equals(val2, default (T)))
      return val2;
    if (equalityComparer.Equals(val3, default (T)))
      return val3;
    if (equalityComparer.Equals(val4, default (T)))
      return val4;
    if (equalityComparer.Equals(val5, default (T)))
      return val5;
    if (equalityComparer.Equals(val6, default (T)))
      return val6;
    if (equalityComparer.Equals(val7, default (T)))
      return val7;
    if (equalityComparer.Equals(val8, default (T)))
      return val8;
    if (equalityComparer.Equals(val9, default (T)))
      return val9;
    if (equalityComparer.Equals(val10, default (T)))
      return val10;
    return !equalityComparer.Equals(val11, default (T)) ? val12 : val11;
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T Coalesce<T>(
    [CanBeNull] T val1,
    [CanBeNull] T val2,
    [CanBeNull] T val3,
    [CanBeNull] T val4,
    [CanBeNull] T val5,
    [CanBeNull] T val6,
    [CanBeNull] T val7,
    [CanBeNull] T val8,
    [CanBeNull] T val9,
    [CanBeNull] T val10,
    [CanBeNull] T val11,
    [CanBeNull] T val12,
    [CanBeNull] T val13)
  {
    EqualityComparer<T> equalityComparer = EqualityComparer<T>.Default;
    if (equalityComparer.Equals(val1, default (T)))
      return val1;
    if (equalityComparer.Equals(val2, default (T)))
      return val2;
    if (equalityComparer.Equals(val3, default (T)))
      return val3;
    if (equalityComparer.Equals(val4, default (T)))
      return val4;
    if (equalityComparer.Equals(val5, default (T)))
      return val5;
    if (equalityComparer.Equals(val6, default (T)))
      return val6;
    if (equalityComparer.Equals(val7, default (T)))
      return val7;
    if (equalityComparer.Equals(val8, default (T)))
      return val8;
    if (equalityComparer.Equals(val9, default (T)))
      return val9;
    if (equalityComparer.Equals(val10, default (T)))
      return val10;
    if (equalityComparer.Equals(val11, default (T)))
      return val11;
    return !equalityComparer.Equals(val12, default (T)) ? val13 : val12;
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T Coalesce<T>(
    [CanBeNull] T val1,
    [CanBeNull] T val2,
    [CanBeNull] T val3,
    [CanBeNull] T val4,
    [CanBeNull] T val5,
    [CanBeNull] T val6,
    [CanBeNull] T val7,
    [CanBeNull] T val8,
    [CanBeNull] T val9,
    [CanBeNull] T val10,
    [CanBeNull] T val11,
    [CanBeNull] T val12,
    [CanBeNull] T val13,
    [CanBeNull] T val14)
  {
    EqualityComparer<T> equalityComparer = EqualityComparer<T>.Default;
    if (equalityComparer.Equals(val1, default (T)))
      return val1;
    if (equalityComparer.Equals(val2, default (T)))
      return val2;
    if (equalityComparer.Equals(val3, default (T)))
      return val3;
    if (equalityComparer.Equals(val4, default (T)))
      return val4;
    if (equalityComparer.Equals(val5, default (T)))
      return val5;
    if (equalityComparer.Equals(val6, default (T)))
      return val6;
    if (equalityComparer.Equals(val7, default (T)))
      return val7;
    if (equalityComparer.Equals(val8, default (T)))
      return val8;
    if (equalityComparer.Equals(val9, default (T)))
      return val9;
    if (equalityComparer.Equals(val10, default (T)))
      return val10;
    if (equalityComparer.Equals(val11, default (T)))
      return val11;
    if (equalityComparer.Equals(val12, default (T)))
      return val12;
    return !equalityComparer.Equals(val13, default (T)) ? val14 : val13;
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T Coalesce<T>(
    [CanBeNull] T val1,
    [CanBeNull] T val2,
    [CanBeNull] T val3,
    [CanBeNull] T val4,
    [CanBeNull] T val5,
    [CanBeNull] T val6,
    [CanBeNull] T val7,
    [CanBeNull] T val8,
    [CanBeNull] T val9,
    [CanBeNull] T val10,
    [CanBeNull] T val11,
    [CanBeNull] T val12,
    [CanBeNull] T val13,
    [CanBeNull] T val14,
    T val15)
  {
    EqualityComparer<T> equalityComparer = EqualityComparer<T>.Default;
    if (equalityComparer.Equals(val1, default (T)))
      return val1;
    if (equalityComparer.Equals(val2, default (T)))
      return val2;
    if (equalityComparer.Equals(val3, default (T)))
      return val3;
    if (equalityComparer.Equals(val4, default (T)))
      return val4;
    if (equalityComparer.Equals(val5, default (T)))
      return val5;
    if (equalityComparer.Equals(val6, default (T)))
      return val6;
    if (equalityComparer.Equals(val7, default (T)))
      return val7;
    if (equalityComparer.Equals(val8, default (T)))
      return val8;
    if (equalityComparer.Equals(val9, default (T)))
      return val9;
    if (equalityComparer.Equals(val10, default (T)))
      return val10;
    if (equalityComparer.Equals(val11, default (T)))
      return val11;
    if (equalityComparer.Equals(val12, default (T)))
      return val12;
    if (equalityComparer.Equals(val13, default (T)))
      return val13;
    return !equalityComparer.Equals(val14, default (T)) ? val15 : val14;
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T Coalesce<T>(
    [CanBeNull] T val1,
    [CanBeNull] T val2,
    [CanBeNull] T val3,
    [CanBeNull] T val4,
    [CanBeNull] T val5,
    [CanBeNull] T val6,
    [CanBeNull] T val7,
    [CanBeNull] T val8,
    [CanBeNull] T val9,
    [CanBeNull] T val10,
    [CanBeNull] T val11,
    [CanBeNull] T val12,
    [CanBeNull] T val13,
    [CanBeNull] T val14,
    T val15,
    T val16)
  {
    EqualityComparer<T> equalityComparer = EqualityComparer<T>.Default;
    if (equalityComparer.Equals(val1, default (T)))
      return val1;
    if (equalityComparer.Equals(val2, default (T)))
      return val2;
    if (equalityComparer.Equals(val3, default (T)))
      return val3;
    if (equalityComparer.Equals(val4, default (T)))
      return val4;
    if (equalityComparer.Equals(val5, default (T)))
      return val5;
    if (equalityComparer.Equals(val6, default (T)))
      return val6;
    if (equalityComparer.Equals(val7, default (T)))
      return val7;
    if (equalityComparer.Equals(val8, default (T)))
      return val8;
    if (equalityComparer.Equals(val9, default (T)))
      return val9;
    if (equalityComparer.Equals(val10, default (T)))
      return val10;
    if (equalityComparer.Equals(val11, default (T)))
      return val11;
    if (equalityComparer.Equals(val12, default (T)))
      return val12;
    if (equalityComparer.Equals(val13, default (T)))
      return val13;
    if (equalityComparer.Equals(val14, default (T)))
      return val14;
    return !equalityComparer.Equals(val15, default (T)) ? val16 : val15;
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T Coalesce<T>(
    [CanBeNull] T val1,
    [CanBeNull] T val2,
    [CanBeNull] T val3,
    [CanBeNull] T val4,
    [CanBeNull] T val5,
    [CanBeNull] T val6,
    [CanBeNull] T val7,
    [CanBeNull] T val8,
    [CanBeNull] T val9,
    [CanBeNull] T val10,
    [CanBeNull] T val11,
    [CanBeNull] T val12,
    [CanBeNull] T val13,
    [CanBeNull] T val14,
    T val15,
    T val16,
    T val17)
  {
    EqualityComparer<T> equalityComparer = EqualityComparer<T>.Default;
    if (equalityComparer.Equals(val1, default (T)))
      return val1;
    if (equalityComparer.Equals(val2, default (T)))
      return val2;
    if (equalityComparer.Equals(val3, default (T)))
      return val3;
    if (equalityComparer.Equals(val4, default (T)))
      return val4;
    if (equalityComparer.Equals(val5, default (T)))
      return val5;
    if (equalityComparer.Equals(val6, default (T)))
      return val6;
    if (equalityComparer.Equals(val7, default (T)))
      return val7;
    if (equalityComparer.Equals(val8, default (T)))
      return val8;
    if (equalityComparer.Equals(val9, default (T)))
      return val9;
    if (equalityComparer.Equals(val10, default (T)))
      return val10;
    if (equalityComparer.Equals(val11, default (T)))
      return val11;
    if (equalityComparer.Equals(val12, default (T)))
      return val12;
    if (equalityComparer.Equals(val13, default (T)))
      return val13;
    if (equalityComparer.Equals(val14, default (T)))
      return val14;
    if (equalityComparer.Equals(val15, default (T)))
      return val15;
    return !equalityComparer.Equals(val16, default (T)) ? val17 : val16;
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T Coalesce<T>(
    [CanBeNull] T val1,
    [CanBeNull] T val2,
    [CanBeNull] T val3,
    [CanBeNull] T val4,
    [CanBeNull] T val5,
    [CanBeNull] T val6,
    [CanBeNull] T val7,
    [CanBeNull] T val8,
    [CanBeNull] T val9,
    [CanBeNull] T val10,
    [CanBeNull] T val11,
    [CanBeNull] T val12,
    [CanBeNull] T val13,
    [CanBeNull] T val14,
    T val15,
    T val16,
    T val17,
    T val18)
  {
    EqualityComparer<T> equalityComparer = EqualityComparer<T>.Default;
    if (equalityComparer.Equals(val1, default (T)))
      return val1;
    if (equalityComparer.Equals(val2, default (T)))
      return val2;
    if (equalityComparer.Equals(val3, default (T)))
      return val3;
    if (equalityComparer.Equals(val4, default (T)))
      return val4;
    if (equalityComparer.Equals(val5, default (T)))
      return val5;
    if (equalityComparer.Equals(val6, default (T)))
      return val6;
    if (equalityComparer.Equals(val7, default (T)))
      return val7;
    if (equalityComparer.Equals(val8, default (T)))
      return val8;
    if (equalityComparer.Equals(val9, default (T)))
      return val9;
    if (equalityComparer.Equals(val10, default (T)))
      return val10;
    if (equalityComparer.Equals(val11, default (T)))
      return val11;
    if (equalityComparer.Equals(val12, default (T)))
      return val12;
    if (equalityComparer.Equals(val13, default (T)))
      return val13;
    if (equalityComparer.Equals(val14, default (T)))
      return val14;
    if (equalityComparer.Equals(val15, default (T)))
      return val15;
    if (equalityComparer.Equals(val16, default (T)))
      return val16;
    return !equalityComparer.Equals(val17, default (T)) ? val18 : val17;
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T Coalesce<T>(
    [CanBeNull] T val1,
    [CanBeNull] T val2,
    [CanBeNull] T val3,
    [CanBeNull] T val4,
    [CanBeNull] T val5,
    [CanBeNull] T val6,
    [CanBeNull] T val7,
    [CanBeNull] T val8,
    [CanBeNull] T val9,
    [CanBeNull] T val10,
    [CanBeNull] T val11,
    [CanBeNull] T val12,
    [CanBeNull] T val13,
    [CanBeNull] T val14,
    T val15,
    T val16,
    T val17,
    T val18,
    T val19)
  {
    EqualityComparer<T> equalityComparer = EqualityComparer<T>.Default;
    if (equalityComparer.Equals(val1, default (T)))
      return val1;
    if (equalityComparer.Equals(val2, default (T)))
      return val2;
    if (equalityComparer.Equals(val3, default (T)))
      return val3;
    if (equalityComparer.Equals(val4, default (T)))
      return val4;
    if (equalityComparer.Equals(val5, default (T)))
      return val5;
    if (equalityComparer.Equals(val6, default (T)))
      return val6;
    if (equalityComparer.Equals(val7, default (T)))
      return val7;
    if (equalityComparer.Equals(val8, default (T)))
      return val8;
    if (equalityComparer.Equals(val9, default (T)))
      return val9;
    if (equalityComparer.Equals(val10, default (T)))
      return val10;
    if (equalityComparer.Equals(val11, default (T)))
      return val11;
    if (equalityComparer.Equals(val12, default (T)))
      return val12;
    if (equalityComparer.Equals(val13, default (T)))
      return val13;
    if (equalityComparer.Equals(val14, default (T)))
      return val14;
    if (equalityComparer.Equals(val15, default (T)))
      return val15;
    if (equalityComparer.Equals(val16, default (T)))
      return val16;
    if (equalityComparer.Equals(val17, default (T)))
      return val17;
    return !equalityComparer.Equals(val18, default (T)) ? val19 : val18;
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T Coalesce<T>(
    [CanBeNull] T val1,
    [CanBeNull] T val2,
    [CanBeNull] T val3,
    [CanBeNull] T val4,
    [CanBeNull] T val5,
    [CanBeNull] T val6,
    [CanBeNull] T val7,
    [CanBeNull] T val8,
    [CanBeNull] T val9,
    [CanBeNull] T val10,
    [CanBeNull] T val11,
    [CanBeNull] T val12,
    [CanBeNull] T val13,
    [CanBeNull] T val14,
    T val15,
    T val16,
    T val17,
    T val18,
    T val19,
    T val20)
  {
    EqualityComparer<T> equalityComparer = EqualityComparer<T>.Default;
    if (equalityComparer.Equals(val1, default (T)))
      return val1;
    if (equalityComparer.Equals(val2, default (T)))
      return val2;
    if (equalityComparer.Equals(val3, default (T)))
      return val3;
    if (equalityComparer.Equals(val4, default (T)))
      return val4;
    if (equalityComparer.Equals(val5, default (T)))
      return val5;
    if (equalityComparer.Equals(val6, default (T)))
      return val6;
    if (equalityComparer.Equals(val7, default (T)))
      return val7;
    if (equalityComparer.Equals(val8, default (T)))
      return val8;
    if (equalityComparer.Equals(val9, default (T)))
      return val9;
    if (equalityComparer.Equals(val10, default (T)))
      return val10;
    if (equalityComparer.Equals(val11, default (T)))
      return val11;
    if (equalityComparer.Equals(val12, default (T)))
      return val12;
    if (equalityComparer.Equals(val13, default (T)))
      return val13;
    if (equalityComparer.Equals(val14, default (T)))
      return val14;
    if (equalityComparer.Equals(val15, default (T)))
      return val15;
    if (equalityComparer.Equals(val16, default (T)))
      return val16;
    if (equalityComparer.Equals(val17, default (T)))
      return val17;
    if (equalityComparer.Equals(val18, default (T)))
      return val18;
    return !equalityComparer.Equals(val19, default (T)) ? val20 : val19;
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T Coalesce<T>(
    [CanBeNull] T val1,
    [CanBeNull] T val2,
    [CanBeNull] T val3,
    [CanBeNull] T val4,
    [CanBeNull] T val5,
    [CanBeNull] T val6,
    [CanBeNull] T val7,
    [CanBeNull] T val8,
    [CanBeNull] T val9,
    [CanBeNull] T val10,
    [CanBeNull] T val11,
    [CanBeNull] T val12,
    [CanBeNull] T val13,
    [CanBeNull] T val14,
    T val15,
    T val16,
    T val17,
    T val18,
    T val19,
    T val20,
    [NotNull, NotEmpty, ItemCanBeNull] params T[] otherValues)
  {
    EqualityComparer<T> comparer = EqualityComparer<T>.Default;
    T x = comparer.Equals(val1, default (T)) ? val1 : (comparer.Equals(val2, default (T)) ? val2 : (comparer.Equals(val3, default (T)) ? val3 : (comparer.Equals(val4, default (T)) ? val4 : (comparer.Equals(val5, default (T)) ? val5 : (comparer.Equals(val6, default (T)) ? val6 : (comparer.Equals(val7, default (T)) ? val7 : (comparer.Equals(val8, default (T)) ? val8 : (comparer.Equals(val9, default (T)) ? val9 : (comparer.Equals(val10, default (T)) ? val10 : (comparer.Equals(val11, default (T)) ? val11 : (comparer.Equals(val12, default (T)) ? val12 : (comparer.Equals(val13, default (T)) ? val13 : (comparer.Equals(val14, default (T)) ? val14 : (comparer.Equals(val15, default (T)) ? val15 : (comparer.Equals(val16, default (T)) ? val16 : (comparer.Equals(val17, default (T)) ? val17 : (comparer.Equals(val18, default (T)) ? val18 : (comparer.Equals(val19, default (T)) ? val19 : val20))))))))))))))))));
    return comparer.Equals(x, default (T)) ? ((IEnumerable<T>) otherValues).FirstOrDefault<T>((Func<T, bool>) (val => !comparer.Equals(val, default (T))), (Func<T>) (() => otherValues[otherValues.Length - 1])) : x;
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T Coalesce<T>([NotNull, NotEmpty, ItemCanBeNull] IEnumerable<T> enumeration)
  {
    EqualityComparer<T> equalityComparer = EqualityComparer<T>.Default;
    T x = default (T);
    foreach (T obj in enumeration)
    {
      x = obj;
      if (!equalityComparer.Equals(x, default (T)))
        return x;
    }
    return x;
  }

  [CanBeNull]
  [CanBeEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CoalesceNotEmpty([CanBeNull, CanBeEmpty] string val1, [CanBeNull, CanBeEmpty] string val2)
  {
    return string.IsNullOrEmpty(val1) ? val2 : val1;
  }

  [CanBeNull]
  [CanBeEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CoalesceNotEmpty([CanBeNull, CanBeEmpty] string val1, [CanBeNull, CanBeEmpty] string val2, [CanBeNull, CanBeEmpty] string val3)
  {
    if (!string.IsNullOrEmpty(val1))
      return val1;
    return string.IsNullOrEmpty(val2) ? val3 : val2;
  }

  [CanBeNull]
  [CanBeEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CoalesceNotEmpty([CanBeNull, CanBeEmpty] string val1, [CanBeNull, CanBeEmpty] string val2, [CanBeNull, CanBeEmpty] string val3, [CanBeNull, CanBeEmpty] string val4)
  {
    if (!string.IsNullOrEmpty(val1))
      return val1;
    if (!string.IsNullOrEmpty(val2))
      return val2;
    return string.IsNullOrEmpty(val3) ? val4 : val3;
  }

  [CanBeNull]
  [CanBeEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CoalesceNotEmpty(
    [CanBeNull, CanBeEmpty] string val1,
    [CanBeNull, CanBeEmpty] string val2,
    [CanBeNull, CanBeEmpty] string val3,
    [CanBeNull, CanBeEmpty] string val4,
    [CanBeNull, CanBeEmpty] string val5)
  {
    if (!string.IsNullOrEmpty(val1))
      return val1;
    if (!string.IsNullOrEmpty(val2))
      return val2;
    if (!string.IsNullOrEmpty(val3))
      return val3;
    return string.IsNullOrEmpty(val4) ? val5 : val4;
  }

  [CanBeNull]
  [CanBeEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CoalesceNotEmpty(
    [CanBeNull, CanBeEmpty] string val1,
    [CanBeNull, CanBeEmpty] string val2,
    [CanBeNull, CanBeEmpty] string val3,
    [CanBeNull, CanBeEmpty] string val4,
    [CanBeNull, CanBeEmpty] string val5,
    [CanBeNull, CanBeEmpty] string val6)
  {
    if (!string.IsNullOrEmpty(val1))
      return val1;
    if (!string.IsNullOrEmpty(val2))
      return val2;
    if (!string.IsNullOrEmpty(val3))
      return val3;
    if (!string.IsNullOrEmpty(val4))
      return val4;
    return string.IsNullOrEmpty(val5) ? val6 : val5;
  }

  [CanBeNull]
  [CanBeEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CoalesceNotEmpty(
    [CanBeNull, CanBeEmpty] string val1,
    [CanBeNull, CanBeEmpty] string val2,
    [CanBeNull, CanBeEmpty] string val3,
    [CanBeNull, CanBeEmpty] string val4,
    [CanBeNull, CanBeEmpty] string val5,
    [CanBeNull, CanBeEmpty] string val6,
    [CanBeNull, CanBeEmpty] string val7)
  {
    if (!string.IsNullOrEmpty(val1))
      return val1;
    if (!string.IsNullOrEmpty(val2))
      return val2;
    if (!string.IsNullOrEmpty(val3))
      return val3;
    if (!string.IsNullOrEmpty(val4))
      return val4;
    if (!string.IsNullOrEmpty(val5))
      return val5;
    return string.IsNullOrEmpty(val6) ? val7 : val6;
  }

  [CanBeNull]
  [CanBeEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CoalesceNotEmpty(
    [CanBeNull, CanBeEmpty] string val1,
    [CanBeNull, CanBeEmpty] string val2,
    [CanBeNull, CanBeEmpty] string val3,
    [CanBeNull, CanBeEmpty] string val4,
    [CanBeNull, CanBeEmpty] string val5,
    [CanBeNull, CanBeEmpty] string val6,
    [CanBeNull, CanBeEmpty] string val7,
    [CanBeNull, CanBeEmpty] string val8)
  {
    if (!string.IsNullOrEmpty(val1))
      return val1;
    if (!string.IsNullOrEmpty(val2))
      return val2;
    if (!string.IsNullOrEmpty(val3))
      return val3;
    if (!string.IsNullOrEmpty(val4))
      return val4;
    if (!string.IsNullOrEmpty(val5))
      return val5;
    if (!string.IsNullOrEmpty(val6))
      return val6;
    return string.IsNullOrEmpty(val7) ? val8 : val7;
  }

  [CanBeNull]
  [CanBeEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CoalesceNotEmpty(
    [CanBeNull, CanBeEmpty] string val1,
    [CanBeNull, CanBeEmpty] string val2,
    [CanBeNull, CanBeEmpty] string val3,
    [CanBeNull, CanBeEmpty] string val4,
    [CanBeNull, CanBeEmpty] string val5,
    [CanBeNull, CanBeEmpty] string val6,
    [CanBeNull, CanBeEmpty] string val7,
    [CanBeNull, CanBeEmpty] string val8,
    [CanBeNull, CanBeEmpty] string val9)
  {
    if (!string.IsNullOrEmpty(val1))
      return val1;
    if (!string.IsNullOrEmpty(val2))
      return val2;
    if (!string.IsNullOrEmpty(val3))
      return val3;
    if (!string.IsNullOrEmpty(val4))
      return val4;
    if (!string.IsNullOrEmpty(val5))
      return val5;
    if (!string.IsNullOrEmpty(val6))
      return val6;
    if (!string.IsNullOrEmpty(val7))
      return val7;
    return string.IsNullOrEmpty(val8) ? val9 : val8;
  }

  [CanBeNull]
  [CanBeEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CoalesceNotEmpty(
    [CanBeNull, CanBeEmpty] string val1,
    [CanBeNull, CanBeEmpty] string val2,
    [CanBeNull, CanBeEmpty] string val3,
    [CanBeNull, CanBeEmpty] string val4,
    [CanBeNull, CanBeEmpty] string val5,
    [CanBeNull, CanBeEmpty] string val6,
    [CanBeNull, CanBeEmpty] string val7,
    [CanBeNull, CanBeEmpty] string val8,
    [CanBeNull, CanBeEmpty] string val9,
    [CanBeNull, CanBeEmpty] string val10)
  {
    if (!string.IsNullOrEmpty(val1))
      return val1;
    if (!string.IsNullOrEmpty(val2))
      return val2;
    if (!string.IsNullOrEmpty(val3))
      return val3;
    if (!string.IsNullOrEmpty(val4))
      return val4;
    if (!string.IsNullOrEmpty(val5))
      return val5;
    if (!string.IsNullOrEmpty(val6))
      return val6;
    if (!string.IsNullOrEmpty(val7))
      return val7;
    if (!string.IsNullOrEmpty(val8))
      return val8;
    return string.IsNullOrEmpty(val9) ? val10 : val9;
  }

  [CanBeNull]
  [CanBeEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CoalesceNotEmpty(
    [CanBeNull, CanBeEmpty] string val1,
    [CanBeNull, CanBeEmpty] string val2,
    [CanBeNull, CanBeEmpty] string val3,
    [CanBeNull, CanBeEmpty] string val4,
    [CanBeNull, CanBeEmpty] string val5,
    [CanBeNull, CanBeEmpty] string val6,
    [CanBeNull, CanBeEmpty] string val7,
    [CanBeNull, CanBeEmpty] string val8,
    [CanBeNull, CanBeEmpty] string val9,
    [CanBeNull, CanBeEmpty] string val10,
    [CanBeNull, CanBeEmpty] string val11)
  {
    if (!string.IsNullOrEmpty(val1))
      return val1;
    if (!string.IsNullOrEmpty(val2))
      return val2;
    if (!string.IsNullOrEmpty(val3))
      return val3;
    if (!string.IsNullOrEmpty(val4))
      return val4;
    if (!string.IsNullOrEmpty(val5))
      return val5;
    if (!string.IsNullOrEmpty(val6))
      return val6;
    if (!string.IsNullOrEmpty(val7))
      return val7;
    if (!string.IsNullOrEmpty(val8))
      return val8;
    if (!string.IsNullOrEmpty(val9))
      return val9;
    return string.IsNullOrEmpty(val10) ? val11 : val10;
  }

  [CanBeNull]
  [CanBeEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CoalesceNotEmpty(
    [CanBeNull, CanBeEmpty] string val1,
    [CanBeNull, CanBeEmpty] string val2,
    [CanBeNull, CanBeEmpty] string val3,
    [CanBeNull, CanBeEmpty] string val4,
    [CanBeNull, CanBeEmpty] string val5,
    [CanBeNull, CanBeEmpty] string val6,
    [CanBeNull, CanBeEmpty] string val7,
    [CanBeNull, CanBeEmpty] string val8,
    [CanBeNull, CanBeEmpty] string val9,
    [CanBeNull, CanBeEmpty] string val10,
    [CanBeNull, CanBeEmpty] string val11,
    [CanBeNull, CanBeEmpty] string val12)
  {
    if (!string.IsNullOrEmpty(val1))
      return val1;
    if (!string.IsNullOrEmpty(val2))
      return val2;
    if (!string.IsNullOrEmpty(val3))
      return val3;
    if (!string.IsNullOrEmpty(val4))
      return val4;
    if (!string.IsNullOrEmpty(val5))
      return val5;
    if (!string.IsNullOrEmpty(val6))
      return val6;
    if (!string.IsNullOrEmpty(val7))
      return val7;
    if (!string.IsNullOrEmpty(val8))
      return val8;
    if (!string.IsNullOrEmpty(val9))
      return val9;
    if (!string.IsNullOrEmpty(val10))
      return val10;
    return string.IsNullOrEmpty(val11) ? val12 : val11;
  }

  [CanBeNull]
  [CanBeEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CoalesceNotEmpty(
    [CanBeNull, CanBeEmpty] string val1,
    [CanBeNull, CanBeEmpty] string val2,
    [CanBeNull, CanBeEmpty] string val3,
    [CanBeNull, CanBeEmpty] string val4,
    [CanBeNull, CanBeEmpty] string val5,
    [CanBeNull, CanBeEmpty] string val6,
    [CanBeNull, CanBeEmpty] string val7,
    [CanBeNull, CanBeEmpty] string val8,
    [CanBeNull, CanBeEmpty] string val9,
    [CanBeNull, CanBeEmpty] string val10,
    [CanBeNull, CanBeEmpty] string val11,
    [CanBeNull, CanBeEmpty] string val12,
    [CanBeNull, CanBeEmpty] string val13)
  {
    if (!string.IsNullOrEmpty(val1))
      return val1;
    if (!string.IsNullOrEmpty(val2))
      return val2;
    if (!string.IsNullOrEmpty(val3))
      return val3;
    if (!string.IsNullOrEmpty(val4))
      return val4;
    if (!string.IsNullOrEmpty(val5))
      return val5;
    if (!string.IsNullOrEmpty(val6))
      return val6;
    if (!string.IsNullOrEmpty(val7))
      return val7;
    if (!string.IsNullOrEmpty(val8))
      return val8;
    if (!string.IsNullOrEmpty(val9))
      return val9;
    if (!string.IsNullOrEmpty(val10))
      return val10;
    if (!string.IsNullOrEmpty(val11))
      return val11;
    return string.IsNullOrEmpty(val12) ? val13 : val12;
  }

  [CanBeNull]
  [CanBeEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CoalesceNotEmpty(
    [CanBeNull, CanBeEmpty] string val1,
    [CanBeNull, CanBeEmpty] string val2,
    [CanBeNull, CanBeEmpty] string val3,
    [CanBeNull, CanBeEmpty] string val4,
    [CanBeNull, CanBeEmpty] string val5,
    [CanBeNull, CanBeEmpty] string val6,
    [CanBeNull, CanBeEmpty] string val7,
    [CanBeNull, CanBeEmpty] string val8,
    [CanBeNull, CanBeEmpty] string val9,
    [CanBeNull, CanBeEmpty] string val10,
    [CanBeNull, CanBeEmpty] string val11,
    [CanBeNull, CanBeEmpty] string val12,
    [CanBeNull, CanBeEmpty] string val13,
    [CanBeNull, CanBeEmpty] string val14)
  {
    if (!string.IsNullOrEmpty(val1))
      return val1;
    if (!string.IsNullOrEmpty(val2))
      return val2;
    if (!string.IsNullOrEmpty(val3))
      return val3;
    if (!string.IsNullOrEmpty(val4))
      return val4;
    if (!string.IsNullOrEmpty(val5))
      return val5;
    if (!string.IsNullOrEmpty(val6))
      return val6;
    if (!string.IsNullOrEmpty(val7))
      return val7;
    if (!string.IsNullOrEmpty(val8))
      return val8;
    if (!string.IsNullOrEmpty(val9))
      return val9;
    if (!string.IsNullOrEmpty(val10))
      return val10;
    if (!string.IsNullOrEmpty(val11))
      return val11;
    if (!string.IsNullOrEmpty(val12))
      return val12;
    return string.IsNullOrEmpty(val13) ? val14 : val13;
  }

  [CanBeNull]
  [CanBeEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CoalesceNotEmpty(
    [CanBeNull, CanBeEmpty] string val1,
    [CanBeNull, CanBeEmpty] string val2,
    [CanBeNull, CanBeEmpty] string val3,
    [CanBeNull, CanBeEmpty] string val4,
    [CanBeNull, CanBeEmpty] string val5,
    [CanBeNull, CanBeEmpty] string val6,
    [CanBeNull, CanBeEmpty] string val7,
    [CanBeNull, CanBeEmpty] string val8,
    [CanBeNull, CanBeEmpty] string val9,
    [CanBeNull, CanBeEmpty] string val10,
    [CanBeNull, CanBeEmpty] string val11,
    [CanBeNull, CanBeEmpty] string val12,
    [CanBeNull, CanBeEmpty] string val13,
    [CanBeNull, CanBeEmpty] string val14,
    [CanBeNull, CanBeEmpty] string val15)
  {
    if (!string.IsNullOrEmpty(val1))
      return val1;
    if (!string.IsNullOrEmpty(val2))
      return val2;
    if (!string.IsNullOrEmpty(val3))
      return val3;
    if (!string.IsNullOrEmpty(val4))
      return val4;
    if (!string.IsNullOrEmpty(val5))
      return val5;
    if (!string.IsNullOrEmpty(val6))
      return val6;
    if (!string.IsNullOrEmpty(val7))
      return val7;
    if (!string.IsNullOrEmpty(val8))
      return val8;
    if (!string.IsNullOrEmpty(val9))
      return val9;
    if (!string.IsNullOrEmpty(val10))
      return val10;
    if (!string.IsNullOrEmpty(val11))
      return val11;
    if (!string.IsNullOrEmpty(val12))
      return val12;
    if (!string.IsNullOrEmpty(val13))
      return val13;
    return string.IsNullOrEmpty(val14) ? val15 : val14;
  }

  [CanBeNull]
  [CanBeEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CoalesceNotEmpty(
    [CanBeNull, CanBeEmpty] string val1,
    [CanBeNull, CanBeEmpty] string val2,
    [CanBeNull, CanBeEmpty] string val3,
    [CanBeNull, CanBeEmpty] string val4,
    [CanBeNull, CanBeEmpty] string val5,
    [CanBeNull, CanBeEmpty] string val6,
    [CanBeNull, CanBeEmpty] string val7,
    [CanBeNull, CanBeEmpty] string val8,
    [CanBeNull, CanBeEmpty] string val9,
    [CanBeNull, CanBeEmpty] string val10,
    [CanBeNull, CanBeEmpty] string val11,
    [CanBeNull, CanBeEmpty] string val12,
    [CanBeNull, CanBeEmpty] string val13,
    [CanBeNull, CanBeEmpty] string val14,
    [CanBeNull, CanBeEmpty] string val15,
    [CanBeNull, CanBeEmpty] string val16)
  {
    if (!string.IsNullOrEmpty(val1))
      return val1;
    if (!string.IsNullOrEmpty(val2))
      return val2;
    if (!string.IsNullOrEmpty(val3))
      return val3;
    if (!string.IsNullOrEmpty(val4))
      return val4;
    if (!string.IsNullOrEmpty(val5))
      return val5;
    if (!string.IsNullOrEmpty(val6))
      return val6;
    if (!string.IsNullOrEmpty(val7))
      return val7;
    if (!string.IsNullOrEmpty(val8))
      return val8;
    if (!string.IsNullOrEmpty(val9))
      return val9;
    if (!string.IsNullOrEmpty(val10))
      return val10;
    if (!string.IsNullOrEmpty(val11))
      return val11;
    if (!string.IsNullOrEmpty(val12))
      return val12;
    if (!string.IsNullOrEmpty(val13))
      return val13;
    if (!string.IsNullOrEmpty(val14))
      return val14;
    return string.IsNullOrEmpty(val15) ? val16 : val15;
  }

  [CanBeNull]
  [CanBeEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CoalesceNotEmpty(
    [CanBeNull, CanBeEmpty] string val1,
    [CanBeNull, CanBeEmpty] string val2,
    [CanBeNull, CanBeEmpty] string val3,
    [CanBeNull, CanBeEmpty] string val4,
    [CanBeNull, CanBeEmpty] string val5,
    [CanBeNull, CanBeEmpty] string val6,
    [CanBeNull, CanBeEmpty] string val7,
    [CanBeNull, CanBeEmpty] string val8,
    [CanBeNull, CanBeEmpty] string val9,
    [CanBeNull, CanBeEmpty] string val10,
    [CanBeNull, CanBeEmpty] string val11,
    [CanBeNull, CanBeEmpty] string val12,
    [CanBeNull, CanBeEmpty] string val13,
    [CanBeNull, CanBeEmpty] string val14,
    [CanBeNull, CanBeEmpty] string val15,
    [CanBeNull, CanBeEmpty] string val16,
    [CanBeNull, CanBeEmpty] string val17)
  {
    if (!string.IsNullOrEmpty(val1))
      return val1;
    if (!string.IsNullOrEmpty(val2))
      return val2;
    if (!string.IsNullOrEmpty(val3))
      return val3;
    if (!string.IsNullOrEmpty(val4))
      return val4;
    if (!string.IsNullOrEmpty(val5))
      return val5;
    if (!string.IsNullOrEmpty(val6))
      return val6;
    if (!string.IsNullOrEmpty(val7))
      return val7;
    if (!string.IsNullOrEmpty(val8))
      return val8;
    if (!string.IsNullOrEmpty(val9))
      return val9;
    if (!string.IsNullOrEmpty(val10))
      return val10;
    if (!string.IsNullOrEmpty(val11))
      return val11;
    if (!string.IsNullOrEmpty(val12))
      return val12;
    if (!string.IsNullOrEmpty(val13))
      return val13;
    if (!string.IsNullOrEmpty(val14))
      return val14;
    if (!string.IsNullOrEmpty(val15))
      return val15;
    return string.IsNullOrEmpty(val16) ? val17 : val16;
  }

  [CanBeNull]
  [CanBeEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CoalesceNotEmpty(
    [CanBeNull, CanBeEmpty] string val1,
    [CanBeNull, CanBeEmpty] string val2,
    [CanBeNull, CanBeEmpty] string val3,
    [CanBeNull, CanBeEmpty] string val4,
    [CanBeNull, CanBeEmpty] string val5,
    [CanBeNull, CanBeEmpty] string val6,
    [CanBeNull, CanBeEmpty] string val7,
    [CanBeNull, CanBeEmpty] string val8,
    [CanBeNull, CanBeEmpty] string val9,
    [CanBeNull, CanBeEmpty] string val10,
    [CanBeNull, CanBeEmpty] string val11,
    [CanBeNull, CanBeEmpty] string val12,
    [CanBeNull, CanBeEmpty] string val13,
    [CanBeNull, CanBeEmpty] string val14,
    [CanBeNull, CanBeEmpty] string val15,
    [CanBeNull, CanBeEmpty] string val16,
    [CanBeNull, CanBeEmpty] string val17,
    [CanBeNull, CanBeEmpty] string val18)
  {
    if (!string.IsNullOrEmpty(val1))
      return val1;
    if (!string.IsNullOrEmpty(val2))
      return val2;
    if (!string.IsNullOrEmpty(val3))
      return val3;
    if (!string.IsNullOrEmpty(val4))
      return val4;
    if (!string.IsNullOrEmpty(val5))
      return val5;
    if (!string.IsNullOrEmpty(val6))
      return val6;
    if (!string.IsNullOrEmpty(val7))
      return val7;
    if (!string.IsNullOrEmpty(val8))
      return val8;
    if (!string.IsNullOrEmpty(val9))
      return val9;
    if (!string.IsNullOrEmpty(val10))
      return val10;
    if (!string.IsNullOrEmpty(val11))
      return val11;
    if (!string.IsNullOrEmpty(val12))
      return val12;
    if (!string.IsNullOrEmpty(val13))
      return val13;
    if (!string.IsNullOrEmpty(val14))
      return val14;
    if (!string.IsNullOrEmpty(val15))
      return val15;
    if (!string.IsNullOrEmpty(val16))
      return val16;
    return string.IsNullOrEmpty(val17) ? val18 : val17;
  }

  [CanBeNull]
  [CanBeEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CoalesceNotEmpty(
    [CanBeNull, CanBeEmpty] string val1,
    [CanBeNull, CanBeEmpty] string val2,
    [CanBeNull, CanBeEmpty] string val3,
    [CanBeNull, CanBeEmpty] string val4,
    [CanBeNull, CanBeEmpty] string val5,
    [CanBeNull, CanBeEmpty] string val6,
    [CanBeNull, CanBeEmpty] string val7,
    [CanBeNull, CanBeEmpty] string val8,
    [CanBeNull, CanBeEmpty] string val9,
    [CanBeNull, CanBeEmpty] string val10,
    [CanBeNull, CanBeEmpty] string val11,
    [CanBeNull, CanBeEmpty] string val12,
    [CanBeNull, CanBeEmpty] string val13,
    [CanBeNull, CanBeEmpty] string val14,
    [CanBeNull, CanBeEmpty] string val15,
    [CanBeNull, CanBeEmpty] string val16,
    [CanBeNull, CanBeEmpty] string val17,
    [CanBeNull, CanBeEmpty] string val18,
    [CanBeNull, CanBeEmpty] string val19)
  {
    if (!string.IsNullOrEmpty(val1))
      return val1;
    if (!string.IsNullOrEmpty(val2))
      return val2;
    if (!string.IsNullOrEmpty(val3))
      return val3;
    if (!string.IsNullOrEmpty(val4))
      return val4;
    if (!string.IsNullOrEmpty(val5))
      return val5;
    if (!string.IsNullOrEmpty(val6))
      return val6;
    if (!string.IsNullOrEmpty(val7))
      return val7;
    if (!string.IsNullOrEmpty(val8))
      return val8;
    if (!string.IsNullOrEmpty(val9))
      return val9;
    if (!string.IsNullOrEmpty(val10))
      return val10;
    if (!string.IsNullOrEmpty(val11))
      return val11;
    if (!string.IsNullOrEmpty(val12))
      return val12;
    if (!string.IsNullOrEmpty(val13))
      return val13;
    if (!string.IsNullOrEmpty(val14))
      return val14;
    if (!string.IsNullOrEmpty(val15))
      return val15;
    if (!string.IsNullOrEmpty(val16))
      return val16;
    if (!string.IsNullOrEmpty(val17))
      return val17;
    return string.IsNullOrEmpty(val18) ? val19 : val18;
  }

  [CanBeNull]
  [CanBeEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CoalesceNotEmpty(
    [CanBeNull, CanBeEmpty] string val1,
    [CanBeNull, CanBeEmpty] string val2,
    [CanBeNull, CanBeEmpty] string val3,
    [CanBeNull, CanBeEmpty] string val4,
    [CanBeNull, CanBeEmpty] string val5,
    [CanBeNull, CanBeEmpty] string val6,
    [CanBeNull, CanBeEmpty] string val7,
    [CanBeNull, CanBeEmpty] string val8,
    [CanBeNull, CanBeEmpty] string val9,
    [CanBeNull, CanBeEmpty] string val10,
    [CanBeNull, CanBeEmpty] string val11,
    [CanBeNull, CanBeEmpty] string val12,
    [CanBeNull, CanBeEmpty] string val13,
    [CanBeNull, CanBeEmpty] string val14,
    [CanBeNull, CanBeEmpty] string val15,
    [CanBeNull, CanBeEmpty] string val16,
    [CanBeNull, CanBeEmpty] string val17,
    [CanBeNull, CanBeEmpty] string val18,
    [CanBeNull, CanBeEmpty] string val19,
    [CanBeNull, CanBeEmpty] string val20)
  {
    if (!string.IsNullOrEmpty(val1))
      return val1;
    if (!string.IsNullOrEmpty(val2))
      return val2;
    if (!string.IsNullOrEmpty(val3))
      return val3;
    if (!string.IsNullOrEmpty(val4))
      return val4;
    if (!string.IsNullOrEmpty(val5))
      return val5;
    if (!string.IsNullOrEmpty(val6))
      return val6;
    if (!string.IsNullOrEmpty(val7))
      return val7;
    if (!string.IsNullOrEmpty(val8))
      return val8;
    if (!string.IsNullOrEmpty(val9))
      return val9;
    if (!string.IsNullOrEmpty(val10))
      return val10;
    if (!string.IsNullOrEmpty(val11))
      return val11;
    if (!string.IsNullOrEmpty(val12))
      return val12;
    if (!string.IsNullOrEmpty(val13))
      return val13;
    if (!string.IsNullOrEmpty(val14))
      return val14;
    if (!string.IsNullOrEmpty(val15))
      return val15;
    if (!string.IsNullOrEmpty(val16))
      return val16;
    if (!string.IsNullOrEmpty(val17))
      return val17;
    if (!string.IsNullOrEmpty(val18))
      return val18;
    return string.IsNullOrEmpty(val19) ? val20 : val19;
  }

  [CanBeNull]
  [CanBeEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CoalesceNotEmpty(
    [CanBeNull, CanBeEmpty] string val1,
    [CanBeNull, CanBeEmpty] string val2,
    [CanBeNull, CanBeEmpty] string val3,
    [CanBeNull, CanBeEmpty] string val4,
    [CanBeNull, CanBeEmpty] string val5,
    [CanBeNull, CanBeEmpty] string val6,
    [CanBeNull, CanBeEmpty] string val7,
    [CanBeNull, CanBeEmpty] string val8,
    [CanBeNull, CanBeEmpty] string val9,
    [CanBeNull, CanBeEmpty] string val10,
    [CanBeNull, CanBeEmpty] string val11,
    [CanBeNull, CanBeEmpty] string val12,
    [CanBeNull, CanBeEmpty] string val13,
    [CanBeNull, CanBeEmpty] string val14,
    [CanBeNull, CanBeEmpty] string val15,
    [CanBeNull, CanBeEmpty] string val16,
    [CanBeNull, CanBeEmpty] string val17,
    [CanBeNull, CanBeEmpty] string val18,
    [CanBeNull, CanBeEmpty] string val19,
    [CanBeNull, CanBeEmpty] string val20,
    [NotNull, NotEmpty, ItemCanBeNull, ItemCanBeEmpty] params string[] otherValues)
  {
    string str = !string.IsNullOrEmpty(val1) ? val1 : (!string.IsNullOrEmpty(val2) ? val2 : (!string.IsNullOrEmpty(val3) ? val3 : (!string.IsNullOrEmpty(val4) ? val4 : (!string.IsNullOrEmpty(val5) ? val5 : (!string.IsNullOrEmpty(val6) ? val6 : (!string.IsNullOrEmpty(val7) ? val7 : (!string.IsNullOrEmpty(val8) ? val8 : (!string.IsNullOrEmpty(val9) ? val9 : (!string.IsNullOrEmpty(val10) ? val10 : (!string.IsNullOrEmpty(val11) ? val11 : (!string.IsNullOrEmpty(val12) ? val12 : (!string.IsNullOrEmpty(val13) ? val13 : (!string.IsNullOrEmpty(val14) ? val14 : (!string.IsNullOrEmpty(val15) ? val15 : (!string.IsNullOrEmpty(val16) ? val16 : (!string.IsNullOrEmpty(val17) ? val17 : (!string.IsNullOrEmpty(val18) ? val18 : (!string.IsNullOrEmpty(val19) ? val19 : val20))))))))))))))))));
    return string.IsNullOrEmpty(str) ? ((IEnumerable<string>) otherValues).FirstOrDefault<string>((Func<string, bool>) (val => !string.IsNullOrEmpty(val)), (Func<string>) (() => otherValues[otherValues.Length - 1])) : str;
  }

  [CanBeNull]
  [CanBeEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CoalesceNotEmpty([NotNull, NotEmpty, ItemCanBeNull, ItemCanBeEmpty] IEnumerable<string> enumeration)
  {
    string str1 = (string) null;
    foreach (string str2 in enumeration)
    {
      str1 = str2;
      if (!string.IsNullOrEmpty(str1))
        return str1;
    }
    return str1;
  }

  [CanBeNull]
  [CanBeEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CoalesceNotWhitespace([CanBeNull, CanBeEmpty] string val1, [CanBeNull, CanBeEmpty] string val2)
  {
    return string.IsNullOrWhiteSpace(val1) ? val2 : val1;
  }

  [CanBeNull]
  [CanBeEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CoalesceNotWhitespace([CanBeNull, CanBeEmpty] string val1, [CanBeNull, CanBeEmpty] string val2, [CanBeNull, CanBeEmpty] string val3)
  {
    if (!string.IsNullOrWhiteSpace(val1))
      return val1;
    return string.IsNullOrWhiteSpace(val2) ? val3 : val2;
  }

  [CanBeNull]
  [CanBeEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CoalesceNotWhitespace([CanBeNull, CanBeEmpty] string val1, [CanBeNull, CanBeEmpty] string val2, [CanBeNull, CanBeEmpty] string val3, [CanBeNull, CanBeEmpty] string val4)
  {
    if (!string.IsNullOrWhiteSpace(val1))
      return val1;
    if (!string.IsNullOrWhiteSpace(val2))
      return val2;
    return string.IsNullOrWhiteSpace(val3) ? val4 : val3;
  }

  [CanBeNull]
  [CanBeEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CoalesceNotWhitespace(
    [CanBeNull, CanBeEmpty] string val1,
    [CanBeNull, CanBeEmpty] string val2,
    [CanBeNull, CanBeEmpty] string val3,
    [CanBeNull, CanBeEmpty] string val4,
    [CanBeNull, CanBeEmpty] string val5)
  {
    if (!string.IsNullOrWhiteSpace(val1))
      return val1;
    if (!string.IsNullOrWhiteSpace(val2))
      return val2;
    if (!string.IsNullOrWhiteSpace(val3))
      return val3;
    return string.IsNullOrWhiteSpace(val4) ? val5 : val4;
  }

  [CanBeNull]
  [CanBeEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CoalesceNotWhitespace(
    [CanBeNull, CanBeEmpty] string val1,
    [CanBeNull, CanBeEmpty] string val2,
    [CanBeNull, CanBeEmpty] string val3,
    [CanBeNull, CanBeEmpty] string val4,
    [CanBeNull, CanBeEmpty] string val5,
    [CanBeNull, CanBeEmpty] string val6)
  {
    if (!string.IsNullOrWhiteSpace(val1))
      return val1;
    if (!string.IsNullOrWhiteSpace(val2))
      return val2;
    if (!string.IsNullOrWhiteSpace(val3))
      return val3;
    if (!string.IsNullOrWhiteSpace(val4))
      return val4;
    return string.IsNullOrWhiteSpace(val5) ? val6 : val5;
  }

  [CanBeNull]
  [CanBeEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CoalesceNotWhitespace(
    [CanBeNull, CanBeEmpty] string val1,
    [CanBeNull, CanBeEmpty] string val2,
    [CanBeNull, CanBeEmpty] string val3,
    [CanBeNull, CanBeEmpty] string val4,
    [CanBeNull, CanBeEmpty] string val5,
    [CanBeNull, CanBeEmpty] string val6,
    [CanBeNull, CanBeEmpty] string val7)
  {
    if (!string.IsNullOrWhiteSpace(val1))
      return val1;
    if (!string.IsNullOrWhiteSpace(val2))
      return val2;
    if (!string.IsNullOrWhiteSpace(val3))
      return val3;
    if (!string.IsNullOrWhiteSpace(val4))
      return val4;
    if (!string.IsNullOrWhiteSpace(val5))
      return val5;
    return string.IsNullOrWhiteSpace(val6) ? val7 : val6;
  }

  [CanBeNull]
  [CanBeEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CoalesceNotWhitespace(
    [CanBeNull, CanBeEmpty] string val1,
    [CanBeNull, CanBeEmpty] string val2,
    [CanBeNull, CanBeEmpty] string val3,
    [CanBeNull, CanBeEmpty] string val4,
    [CanBeNull, CanBeEmpty] string val5,
    [CanBeNull, CanBeEmpty] string val6,
    [CanBeNull, CanBeEmpty] string val7,
    [CanBeNull, CanBeEmpty] string val8)
  {
    if (!string.IsNullOrWhiteSpace(val1))
      return val1;
    if (!string.IsNullOrWhiteSpace(val2))
      return val2;
    if (!string.IsNullOrWhiteSpace(val3))
      return val3;
    if (!string.IsNullOrWhiteSpace(val4))
      return val4;
    if (!string.IsNullOrWhiteSpace(val5))
      return val5;
    if (!string.IsNullOrWhiteSpace(val6))
      return val6;
    return string.IsNullOrWhiteSpace(val7) ? val8 : val7;
  }

  [CanBeNull]
  [CanBeEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CoalesceNotWhitespace(
    [CanBeNull, CanBeEmpty] string val1,
    [CanBeNull, CanBeEmpty] string val2,
    [CanBeNull, CanBeEmpty] string val3,
    [CanBeNull, CanBeEmpty] string val4,
    [CanBeNull, CanBeEmpty] string val5,
    [CanBeNull, CanBeEmpty] string val6,
    [CanBeNull, CanBeEmpty] string val7,
    [CanBeNull, CanBeEmpty] string val8,
    [CanBeNull, CanBeEmpty] string val9)
  {
    if (!string.IsNullOrWhiteSpace(val1))
      return val1;
    if (!string.IsNullOrWhiteSpace(val2))
      return val2;
    if (!string.IsNullOrWhiteSpace(val3))
      return val3;
    if (!string.IsNullOrWhiteSpace(val4))
      return val4;
    if (!string.IsNullOrWhiteSpace(val5))
      return val5;
    if (!string.IsNullOrWhiteSpace(val6))
      return val6;
    if (!string.IsNullOrWhiteSpace(val7))
      return val7;
    return string.IsNullOrWhiteSpace(val8) ? val9 : val8;
  }

  [CanBeNull]
  [CanBeEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CoalesceNotWhitespace(
    [CanBeNull, CanBeEmpty] string val1,
    [CanBeNull, CanBeEmpty] string val2,
    [CanBeNull, CanBeEmpty] string val3,
    [CanBeNull, CanBeEmpty] string val4,
    [CanBeNull, CanBeEmpty] string val5,
    [CanBeNull, CanBeEmpty] string val6,
    [CanBeNull, CanBeEmpty] string val7,
    [CanBeNull, CanBeEmpty] string val8,
    [CanBeNull, CanBeEmpty] string val9,
    [CanBeNull, CanBeEmpty] string val10)
  {
    if (!string.IsNullOrWhiteSpace(val1))
      return val1;
    if (!string.IsNullOrWhiteSpace(val2))
      return val2;
    if (!string.IsNullOrWhiteSpace(val3))
      return val3;
    if (!string.IsNullOrWhiteSpace(val4))
      return val4;
    if (!string.IsNullOrWhiteSpace(val5))
      return val5;
    if (!string.IsNullOrWhiteSpace(val6))
      return val6;
    if (!string.IsNullOrWhiteSpace(val7))
      return val7;
    if (!string.IsNullOrWhiteSpace(val8))
      return val8;
    return string.IsNullOrWhiteSpace(val9) ? val10 : val9;
  }

  [CanBeNull]
  [CanBeEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CoalesceNotWhitespace(
    [CanBeNull, CanBeEmpty] string val1,
    [CanBeNull, CanBeEmpty] string val2,
    [CanBeNull, CanBeEmpty] string val3,
    [CanBeNull, CanBeEmpty] string val4,
    [CanBeNull, CanBeEmpty] string val5,
    [CanBeNull, CanBeEmpty] string val6,
    [CanBeNull, CanBeEmpty] string val7,
    [CanBeNull, CanBeEmpty] string val8,
    [CanBeNull, CanBeEmpty] string val9,
    [CanBeNull, CanBeEmpty] string val10,
    [CanBeNull, CanBeEmpty] string val11)
  {
    if (!string.IsNullOrWhiteSpace(val1))
      return val1;
    if (!string.IsNullOrWhiteSpace(val2))
      return val2;
    if (!string.IsNullOrWhiteSpace(val3))
      return val3;
    if (!string.IsNullOrWhiteSpace(val4))
      return val4;
    if (!string.IsNullOrWhiteSpace(val5))
      return val5;
    if (!string.IsNullOrWhiteSpace(val6))
      return val6;
    if (!string.IsNullOrWhiteSpace(val7))
      return val7;
    if (!string.IsNullOrWhiteSpace(val8))
      return val8;
    if (!string.IsNullOrWhiteSpace(val9))
      return val9;
    return string.IsNullOrWhiteSpace(val10) ? val11 : val10;
  }

  [CanBeNull]
  [CanBeEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CoalesceNotWhitespace(
    [CanBeNull, CanBeEmpty] string val1,
    [CanBeNull, CanBeEmpty] string val2,
    [CanBeNull, CanBeEmpty] string val3,
    [CanBeNull, CanBeEmpty] string val4,
    [CanBeNull, CanBeEmpty] string val5,
    [CanBeNull, CanBeEmpty] string val6,
    [CanBeNull, CanBeEmpty] string val7,
    [CanBeNull, CanBeEmpty] string val8,
    [CanBeNull, CanBeEmpty] string val9,
    [CanBeNull, CanBeEmpty] string val10,
    [CanBeNull, CanBeEmpty] string val11,
    [CanBeNull, CanBeEmpty] string val12)
  {
    if (!string.IsNullOrWhiteSpace(val1))
      return val1;
    if (!string.IsNullOrWhiteSpace(val2))
      return val2;
    if (!string.IsNullOrWhiteSpace(val3))
      return val3;
    if (!string.IsNullOrWhiteSpace(val4))
      return val4;
    if (!string.IsNullOrWhiteSpace(val5))
      return val5;
    if (!string.IsNullOrWhiteSpace(val6))
      return val6;
    if (!string.IsNullOrWhiteSpace(val7))
      return val7;
    if (!string.IsNullOrWhiteSpace(val8))
      return val8;
    if (!string.IsNullOrWhiteSpace(val9))
      return val9;
    if (!string.IsNullOrWhiteSpace(val10))
      return val10;
    return string.IsNullOrWhiteSpace(val11) ? val12 : val11;
  }

  [CanBeNull]
  [CanBeEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CoalesceNotWhitespace(
    [CanBeNull, CanBeEmpty] string val1,
    [CanBeNull, CanBeEmpty] string val2,
    [CanBeNull, CanBeEmpty] string val3,
    [CanBeNull, CanBeEmpty] string val4,
    [CanBeNull, CanBeEmpty] string val5,
    [CanBeNull, CanBeEmpty] string val6,
    [CanBeNull, CanBeEmpty] string val7,
    [CanBeNull, CanBeEmpty] string val8,
    [CanBeNull, CanBeEmpty] string val9,
    [CanBeNull, CanBeEmpty] string val10,
    [CanBeNull, CanBeEmpty] string val11,
    [CanBeNull, CanBeEmpty] string val12,
    [CanBeNull, CanBeEmpty] string val13)
  {
    if (!string.IsNullOrWhiteSpace(val1))
      return val1;
    if (!string.IsNullOrWhiteSpace(val2))
      return val2;
    if (!string.IsNullOrWhiteSpace(val3))
      return val3;
    if (!string.IsNullOrWhiteSpace(val4))
      return val4;
    if (!string.IsNullOrWhiteSpace(val5))
      return val5;
    if (!string.IsNullOrWhiteSpace(val6))
      return val6;
    if (!string.IsNullOrWhiteSpace(val7))
      return val7;
    if (!string.IsNullOrWhiteSpace(val8))
      return val8;
    if (!string.IsNullOrWhiteSpace(val9))
      return val9;
    if (!string.IsNullOrWhiteSpace(val10))
      return val10;
    if (!string.IsNullOrWhiteSpace(val11))
      return val11;
    return string.IsNullOrWhiteSpace(val12) ? val13 : val12;
  }

  [CanBeNull]
  [CanBeEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CoalesceNotWhitespace(
    [CanBeNull, CanBeEmpty] string val1,
    [CanBeNull, CanBeEmpty] string val2,
    [CanBeNull, CanBeEmpty] string val3,
    [CanBeNull, CanBeEmpty] string val4,
    [CanBeNull, CanBeEmpty] string val5,
    [CanBeNull, CanBeEmpty] string val6,
    [CanBeNull, CanBeEmpty] string val7,
    [CanBeNull, CanBeEmpty] string val8,
    [CanBeNull, CanBeEmpty] string val9,
    [CanBeNull, CanBeEmpty] string val10,
    [CanBeNull, CanBeEmpty] string val11,
    [CanBeNull, CanBeEmpty] string val12,
    [CanBeNull, CanBeEmpty] string val13,
    [CanBeNull, CanBeEmpty] string val14)
  {
    if (!string.IsNullOrWhiteSpace(val1))
      return val1;
    if (!string.IsNullOrWhiteSpace(val2))
      return val2;
    if (!string.IsNullOrWhiteSpace(val3))
      return val3;
    if (!string.IsNullOrWhiteSpace(val4))
      return val4;
    if (!string.IsNullOrWhiteSpace(val5))
      return val5;
    if (!string.IsNullOrWhiteSpace(val6))
      return val6;
    if (!string.IsNullOrWhiteSpace(val7))
      return val7;
    if (!string.IsNullOrWhiteSpace(val8))
      return val8;
    if (!string.IsNullOrWhiteSpace(val9))
      return val9;
    if (!string.IsNullOrWhiteSpace(val10))
      return val10;
    if (!string.IsNullOrWhiteSpace(val11))
      return val11;
    if (!string.IsNullOrWhiteSpace(val12))
      return val12;
    return string.IsNullOrWhiteSpace(val13) ? val14 : val13;
  }

  [CanBeNull]
  [CanBeEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CoalesceNotWhitespace(
    [CanBeNull, CanBeEmpty] string val1,
    [CanBeNull, CanBeEmpty] string val2,
    [CanBeNull, CanBeEmpty] string val3,
    [CanBeNull, CanBeEmpty] string val4,
    [CanBeNull, CanBeEmpty] string val5,
    [CanBeNull, CanBeEmpty] string val6,
    [CanBeNull, CanBeEmpty] string val7,
    [CanBeNull, CanBeEmpty] string val8,
    [CanBeNull, CanBeEmpty] string val9,
    [CanBeNull, CanBeEmpty] string val10,
    [CanBeNull, CanBeEmpty] string val11,
    [CanBeNull, CanBeEmpty] string val12,
    [CanBeNull, CanBeEmpty] string val13,
    [CanBeNull, CanBeEmpty] string val14,
    [CanBeNull, CanBeEmpty] string val15)
  {
    if (!string.IsNullOrWhiteSpace(val1))
      return val1;
    if (!string.IsNullOrWhiteSpace(val2))
      return val2;
    if (!string.IsNullOrWhiteSpace(val3))
      return val3;
    if (!string.IsNullOrWhiteSpace(val4))
      return val4;
    if (!string.IsNullOrWhiteSpace(val5))
      return val5;
    if (!string.IsNullOrWhiteSpace(val6))
      return val6;
    if (!string.IsNullOrWhiteSpace(val7))
      return val7;
    if (!string.IsNullOrWhiteSpace(val8))
      return val8;
    if (!string.IsNullOrWhiteSpace(val9))
      return val9;
    if (!string.IsNullOrWhiteSpace(val10))
      return val10;
    if (!string.IsNullOrWhiteSpace(val11))
      return val11;
    if (!string.IsNullOrWhiteSpace(val12))
      return val12;
    if (!string.IsNullOrWhiteSpace(val13))
      return val13;
    return string.IsNullOrWhiteSpace(val14) ? val15 : val14;
  }

  [CanBeNull]
  [CanBeEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CoalesceNotWhitespace(
    [CanBeNull, CanBeEmpty] string val1,
    [CanBeNull, CanBeEmpty] string val2,
    [CanBeNull, CanBeEmpty] string val3,
    [CanBeNull, CanBeEmpty] string val4,
    [CanBeNull, CanBeEmpty] string val5,
    [CanBeNull, CanBeEmpty] string val6,
    [CanBeNull, CanBeEmpty] string val7,
    [CanBeNull, CanBeEmpty] string val8,
    [CanBeNull, CanBeEmpty] string val9,
    [CanBeNull, CanBeEmpty] string val10,
    [CanBeNull, CanBeEmpty] string val11,
    [CanBeNull, CanBeEmpty] string val12,
    [CanBeNull, CanBeEmpty] string val13,
    [CanBeNull, CanBeEmpty] string val14,
    [CanBeNull, CanBeEmpty] string val15,
    [CanBeNull, CanBeEmpty] string val16)
  {
    if (!string.IsNullOrWhiteSpace(val1))
      return val1;
    if (!string.IsNullOrWhiteSpace(val2))
      return val2;
    if (!string.IsNullOrWhiteSpace(val3))
      return val3;
    if (!string.IsNullOrWhiteSpace(val4))
      return val4;
    if (!string.IsNullOrWhiteSpace(val5))
      return val5;
    if (!string.IsNullOrWhiteSpace(val6))
      return val6;
    if (!string.IsNullOrWhiteSpace(val7))
      return val7;
    if (!string.IsNullOrWhiteSpace(val8))
      return val8;
    if (!string.IsNullOrWhiteSpace(val9))
      return val9;
    if (!string.IsNullOrWhiteSpace(val10))
      return val10;
    if (!string.IsNullOrWhiteSpace(val11))
      return val11;
    if (!string.IsNullOrWhiteSpace(val12))
      return val12;
    if (!string.IsNullOrWhiteSpace(val13))
      return val13;
    if (!string.IsNullOrWhiteSpace(val14))
      return val14;
    return string.IsNullOrWhiteSpace(val15) ? val16 : val15;
  }

  [CanBeNull]
  [CanBeEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CoalesceNotWhitespace(
    [CanBeNull, CanBeEmpty] string val1,
    [CanBeNull, CanBeEmpty] string val2,
    [CanBeNull, CanBeEmpty] string val3,
    [CanBeNull, CanBeEmpty] string val4,
    [CanBeNull, CanBeEmpty] string val5,
    [CanBeNull, CanBeEmpty] string val6,
    [CanBeNull, CanBeEmpty] string val7,
    [CanBeNull, CanBeEmpty] string val8,
    [CanBeNull, CanBeEmpty] string val9,
    [CanBeNull, CanBeEmpty] string val10,
    [CanBeNull, CanBeEmpty] string val11,
    [CanBeNull, CanBeEmpty] string val12,
    [CanBeNull, CanBeEmpty] string val13,
    [CanBeNull, CanBeEmpty] string val14,
    [CanBeNull, CanBeEmpty] string val15,
    [CanBeNull, CanBeEmpty] string val16,
    [CanBeNull, CanBeEmpty] string val17)
  {
    if (!string.IsNullOrWhiteSpace(val1))
      return val1;
    if (!string.IsNullOrWhiteSpace(val2))
      return val2;
    if (!string.IsNullOrWhiteSpace(val3))
      return val3;
    if (!string.IsNullOrWhiteSpace(val4))
      return val4;
    if (!string.IsNullOrWhiteSpace(val5))
      return val5;
    if (!string.IsNullOrWhiteSpace(val6))
      return val6;
    if (!string.IsNullOrWhiteSpace(val7))
      return val7;
    if (!string.IsNullOrWhiteSpace(val8))
      return val8;
    if (!string.IsNullOrWhiteSpace(val9))
      return val9;
    if (!string.IsNullOrWhiteSpace(val10))
      return val10;
    if (!string.IsNullOrWhiteSpace(val11))
      return val11;
    if (!string.IsNullOrWhiteSpace(val12))
      return val12;
    if (!string.IsNullOrWhiteSpace(val13))
      return val13;
    if (!string.IsNullOrWhiteSpace(val14))
      return val14;
    if (!string.IsNullOrWhiteSpace(val15))
      return val15;
    return string.IsNullOrWhiteSpace(val16) ? val17 : val16;
  }

  [CanBeNull]
  [CanBeEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CoalesceNotWhitespace(
    [CanBeNull, CanBeEmpty] string val1,
    [CanBeNull, CanBeEmpty] string val2,
    [CanBeNull, CanBeEmpty] string val3,
    [CanBeNull, CanBeEmpty] string val4,
    [CanBeNull, CanBeEmpty] string val5,
    [CanBeNull, CanBeEmpty] string val6,
    [CanBeNull, CanBeEmpty] string val7,
    [CanBeNull, CanBeEmpty] string val8,
    [CanBeNull, CanBeEmpty] string val9,
    [CanBeNull, CanBeEmpty] string val10,
    [CanBeNull, CanBeEmpty] string val11,
    [CanBeNull, CanBeEmpty] string val12,
    [CanBeNull, CanBeEmpty] string val13,
    [CanBeNull, CanBeEmpty] string val14,
    [CanBeNull, CanBeEmpty] string val15,
    [CanBeNull, CanBeEmpty] string val16,
    [CanBeNull, CanBeEmpty] string val17,
    [CanBeNull, CanBeEmpty] string val18)
  {
    if (!string.IsNullOrWhiteSpace(val1))
      return val1;
    if (!string.IsNullOrWhiteSpace(val2))
      return val2;
    if (!string.IsNullOrWhiteSpace(val3))
      return val3;
    if (!string.IsNullOrWhiteSpace(val4))
      return val4;
    if (!string.IsNullOrWhiteSpace(val5))
      return val5;
    if (!string.IsNullOrWhiteSpace(val6))
      return val6;
    if (!string.IsNullOrWhiteSpace(val7))
      return val7;
    if (!string.IsNullOrWhiteSpace(val8))
      return val8;
    if (!string.IsNullOrWhiteSpace(val9))
      return val9;
    if (!string.IsNullOrWhiteSpace(val10))
      return val10;
    if (!string.IsNullOrWhiteSpace(val11))
      return val11;
    if (!string.IsNullOrWhiteSpace(val12))
      return val12;
    if (!string.IsNullOrWhiteSpace(val13))
      return val13;
    if (!string.IsNullOrWhiteSpace(val14))
      return val14;
    if (!string.IsNullOrWhiteSpace(val15))
      return val15;
    if (!string.IsNullOrWhiteSpace(val16))
      return val16;
    return string.IsNullOrWhiteSpace(val17) ? val18 : val17;
  }

  [CanBeNull]
  [CanBeEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CoalesceNotWhitespace(
    [CanBeNull, CanBeEmpty] string val1,
    [CanBeNull, CanBeEmpty] string val2,
    [CanBeNull, CanBeEmpty] string val3,
    [CanBeNull, CanBeEmpty] string val4,
    [CanBeNull, CanBeEmpty] string val5,
    [CanBeNull, CanBeEmpty] string val6,
    [CanBeNull, CanBeEmpty] string val7,
    [CanBeNull, CanBeEmpty] string val8,
    [CanBeNull, CanBeEmpty] string val9,
    [CanBeNull, CanBeEmpty] string val10,
    [CanBeNull, CanBeEmpty] string val11,
    [CanBeNull, CanBeEmpty] string val12,
    [CanBeNull, CanBeEmpty] string val13,
    [CanBeNull, CanBeEmpty] string val14,
    [CanBeNull, CanBeEmpty] string val15,
    [CanBeNull, CanBeEmpty] string val16,
    [CanBeNull, CanBeEmpty] string val17,
    [CanBeNull, CanBeEmpty] string val18,
    [CanBeNull, CanBeEmpty] string val19)
  {
    if (!string.IsNullOrWhiteSpace(val1))
      return val1;
    if (!string.IsNullOrWhiteSpace(val2))
      return val2;
    if (!string.IsNullOrWhiteSpace(val3))
      return val3;
    if (!string.IsNullOrWhiteSpace(val4))
      return val4;
    if (!string.IsNullOrWhiteSpace(val5))
      return val5;
    if (!string.IsNullOrWhiteSpace(val6))
      return val6;
    if (!string.IsNullOrWhiteSpace(val7))
      return val7;
    if (!string.IsNullOrWhiteSpace(val8))
      return val8;
    if (!string.IsNullOrWhiteSpace(val9))
      return val9;
    if (!string.IsNullOrWhiteSpace(val10))
      return val10;
    if (!string.IsNullOrWhiteSpace(val11))
      return val11;
    if (!string.IsNullOrWhiteSpace(val12))
      return val12;
    if (!string.IsNullOrWhiteSpace(val13))
      return val13;
    if (!string.IsNullOrWhiteSpace(val14))
      return val14;
    if (!string.IsNullOrWhiteSpace(val15))
      return val15;
    if (!string.IsNullOrWhiteSpace(val16))
      return val16;
    if (!string.IsNullOrWhiteSpace(val17))
      return val17;
    return string.IsNullOrWhiteSpace(val18) ? val19 : val18;
  }

  [CanBeNull]
  [CanBeEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CoalesceNotWhitespace(
    [CanBeNull, CanBeEmpty] string val1,
    [CanBeNull, CanBeEmpty] string val2,
    [CanBeNull, CanBeEmpty] string val3,
    [CanBeNull, CanBeEmpty] string val4,
    [CanBeNull, CanBeEmpty] string val5,
    [CanBeNull, CanBeEmpty] string val6,
    [CanBeNull, CanBeEmpty] string val7,
    [CanBeNull, CanBeEmpty] string val8,
    [CanBeNull, CanBeEmpty] string val9,
    [CanBeNull, CanBeEmpty] string val10,
    [CanBeNull, CanBeEmpty] string val11,
    [CanBeNull, CanBeEmpty] string val12,
    [CanBeNull, CanBeEmpty] string val13,
    [CanBeNull, CanBeEmpty] string val14,
    [CanBeNull, CanBeEmpty] string val15,
    [CanBeNull, CanBeEmpty] string val16,
    [CanBeNull, CanBeEmpty] string val17,
    [CanBeNull, CanBeEmpty] string val18,
    [CanBeNull, CanBeEmpty] string val19,
    [CanBeNull, CanBeEmpty] string val20)
  {
    if (!string.IsNullOrWhiteSpace(val1))
      return val1;
    if (!string.IsNullOrWhiteSpace(val2))
      return val2;
    if (!string.IsNullOrWhiteSpace(val3))
      return val3;
    if (!string.IsNullOrWhiteSpace(val4))
      return val4;
    if (!string.IsNullOrWhiteSpace(val5))
      return val5;
    if (!string.IsNullOrWhiteSpace(val6))
      return val6;
    if (!string.IsNullOrWhiteSpace(val7))
      return val7;
    if (!string.IsNullOrWhiteSpace(val8))
      return val8;
    if (!string.IsNullOrWhiteSpace(val9))
      return val9;
    if (!string.IsNullOrWhiteSpace(val10))
      return val10;
    if (!string.IsNullOrWhiteSpace(val11))
      return val11;
    if (!string.IsNullOrWhiteSpace(val12))
      return val12;
    if (!string.IsNullOrWhiteSpace(val13))
      return val13;
    if (!string.IsNullOrWhiteSpace(val14))
      return val14;
    if (!string.IsNullOrWhiteSpace(val15))
      return val15;
    if (!string.IsNullOrWhiteSpace(val16))
      return val16;
    if (!string.IsNullOrWhiteSpace(val17))
      return val17;
    if (!string.IsNullOrWhiteSpace(val18))
      return val18;
    return string.IsNullOrWhiteSpace(val19) ? val20 : val19;
  }

  [CanBeNull]
  [CanBeEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CoalesceNotWhitespace(
    [CanBeNull, CanBeEmpty] string val1,
    [CanBeNull, CanBeEmpty] string val2,
    [CanBeNull, CanBeEmpty] string val3,
    [CanBeNull, CanBeEmpty] string val4,
    [CanBeNull, CanBeEmpty] string val5,
    [CanBeNull, CanBeEmpty] string val6,
    [CanBeNull, CanBeEmpty] string val7,
    [CanBeNull, CanBeEmpty] string val8,
    [CanBeNull, CanBeEmpty] string val9,
    [CanBeNull, CanBeEmpty] string val10,
    [CanBeNull, CanBeEmpty] string val11,
    [CanBeNull, CanBeEmpty] string val12,
    [CanBeNull, CanBeEmpty] string val13,
    [CanBeNull, CanBeEmpty] string val14,
    [CanBeNull, CanBeEmpty] string val15,
    [CanBeNull, CanBeEmpty] string val16,
    [CanBeNull, CanBeEmpty] string val17,
    [CanBeNull, CanBeEmpty] string val18,
    [CanBeNull, CanBeEmpty] string val19,
    [CanBeNull, CanBeEmpty] string val20,
    [NotNull, NotEmpty, ItemCanBeNull, ItemCanBeEmpty] params string[] otherValues)
  {
    string str = !string.IsNullOrWhiteSpace(val1) ? val1 : (!string.IsNullOrWhiteSpace(val2) ? val2 : (!string.IsNullOrWhiteSpace(val3) ? val3 : (!string.IsNullOrWhiteSpace(val4) ? val4 : (!string.IsNullOrWhiteSpace(val5) ? val5 : (!string.IsNullOrWhiteSpace(val6) ? val6 : (!string.IsNullOrWhiteSpace(val7) ? val7 : (!string.IsNullOrWhiteSpace(val8) ? val8 : (!string.IsNullOrWhiteSpace(val9) ? val9 : (!string.IsNullOrWhiteSpace(val10) ? val10 : (!string.IsNullOrWhiteSpace(val11) ? val11 : (!string.IsNullOrWhiteSpace(val12) ? val12 : (!string.IsNullOrWhiteSpace(val13) ? val13 : (!string.IsNullOrWhiteSpace(val14) ? val14 : (!string.IsNullOrWhiteSpace(val15) ? val15 : (!string.IsNullOrWhiteSpace(val16) ? val16 : (!string.IsNullOrWhiteSpace(val17) ? val17 : (!string.IsNullOrWhiteSpace(val18) ? val18 : (!string.IsNullOrWhiteSpace(val19) ? val19 : val20))))))))))))))))));
    return string.IsNullOrWhiteSpace(str) ? ((IEnumerable<string>) otherValues).FirstOrDefault<string>((Func<string, bool>) (val => !string.IsNullOrWhiteSpace(val)), (Func<string>) (() => otherValues[otherValues.Length - 1])) : str;
  }

  [CanBeNull]
  [CanBeEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CoalesceNotWhitespace([NotNull, NotEmpty, ItemCanBeNull, ItemCanBeEmpty] IEnumerable<string> enumeration)
  {
    string str1 = (string) null;
    foreach (string str2 in enumeration)
    {
      str1 = str2;
      if (!string.IsNullOrWhiteSpace(str1))
        return str1;
    }
    return str1;
  }
}
