// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.HashSetExtensions
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public static class HashSetExtensions
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void AddCheckUnique<T>([NotNull] this HashSet<T> hashSet, [CanBeNull] T value, [CanBeNull, CanBeEmpty] string exceptMessage = null)
  {
    if (hashSet.Contains(value))
      throw new Exception(exceptMessage);
    hashSet.Add(value);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void AddCheckUnique<T>(
    [NotNull] this HashSet<T> hashSet,
    [CanBeNull] T value,
    [NotNull, InstantHandle] Func<T, Exception> exceptionConstructor)
  {
    if (hashSet.Contains(value))
      throw exceptionConstructor(value);
    hashSet.Add(value);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void AddRangeCheckUnique<T>(
    [NotNull] this HashSet<T> hashSet,
    [NotNull] IEnumerable<T> values,
    [CanBeNull, CanBeEmpty] string exceptMessage = null)
  {
    foreach (T obj in values)
    {
      if (hashSet.Contains(obj))
        throw new Exception(exceptMessage);
      hashSet.Add(obj);
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void AddRangeCheckUnique<T>(
    [NotNull] this HashSet<T> hashSet,
    [NotNull] IEnumerable<T> values,
    [NotNull, InstantHandle] Func<T, Exception> exceptionConstructor)
  {
    foreach (T obj in values)
    {
      if (hashSet.Contains(obj))
        throw exceptionConstructor(obj);
      hashSet.Add(obj);
    }
  }
}
