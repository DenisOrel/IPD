// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.IEnumerableWithCapacityExtensions
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public static class IEnumerableWithCapacityExtensions
{
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerableWithCapacity<TOutput> Map<TSource, TOutput>(
    [CanBeNull] this IEnumerableWithCapacity<TSource> enumerableWithCapacity,
    [NotNull] Func<TSource, TOutput> selector)
  {
    return (IEnumerableWithCapacity<TOutput>) new EnumerationMapAdapter<TSource, TOutput>((IEnumerable<TSource>) enumerableWithCapacity, selector);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerableWithCapacity<TOutput> CastWithCapacity<TSource, TOutput>(
    [CanBeNull] this IEnumerableWithCapacity<TSource> enumerableWithCapacity)
    where TSource : TOutput
  {
    return (IEnumerableWithCapacity<TOutput>) new EnumerationCastAdapter<TSource, TOutput>((IEnumerable<TSource>) enumerableWithCapacity);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerableWithCapacity<T> Filter<T>(
    [CanBeNull] this IEnumerableWithCapacity<T> enumerableWithCapacity,
    [NotNull] Func<T, bool> predicate)
  {
    return (IEnumerableWithCapacity<T>) new EnumerationFilterAdapter<T>((IEnumerable<T>) enumerableWithCapacity, predicate);
  }

  [NotNull]
  public static IEnumerableWithCapacity<T> Distinct<T>(
    [NotNull] this IEnumerableWithCapacity<T> enumerableWithCapacity)
  {
    return (IEnumerableWithCapacity<T>) new EnumerationOperationAdapter<T>((IEnumerable<T>) enumerableWithCapacity, new Func<IEnumerable<T>, IEnumerable<T>>(Enumerable.Distinct<T>));
  }

  [NotNull]
  public static List<T> AsList<T>(
    [CanBeNull] this IEnumerableWithCapacity<T> enumerableWithCapacity)
  {
    if (enumerableWithCapacity == null)
      return new List<T>();
    List<T> objList = new List<T>(enumerableWithCapacity.TryGetCount<T>() ?? enumerableWithCapacity.Capacity);
    objList.AddRange((IEnumerable<T>) enumerableWithCapacity);
    return objList;
  }
}
