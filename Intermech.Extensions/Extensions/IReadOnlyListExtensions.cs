// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.IReadOnlyListExtensions
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Collections;
using Intermech.Diagnostics;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public static class IReadOnlyListExtensions
{
  [ContractAnnotation("throwExceptIfNull:true => NotNull; source:null => null; => NotNull")]
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyList<TOutput> CastListReadOnly<TSource, TOutput>(
    [CanBeNull] this IReadOnlyList<TSource> source,
    bool throwExceptIfNull = true)
    where TSource : TOutput
  {
    if (throwExceptIfNull)
      Intermech.Diagnostics.Check.ArgumentNotNull<IReadOnlyList<TSource>>(source, nameof (source));
    else if (source == null)
      return (IReadOnlyList<TOutput>) null;
    if (source.Count == 0)
      return (IReadOnlyList<TOutput>) Array.Empty<TOutput>();
    return source is IReadOnlyList<TOutput> outputList ? outputList : (IReadOnlyList<TOutput>) new ReadOnlyListCastAdapter<TSource, TOutput>(source);
  }

  [ContractAnnotation("throwExceptIfNull:true => NotNull; source:null => null; => NotNull")]
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyList<TOutput> MapListReadOnly<TSource, TOutput>(
    [CanBeNull] this IReadOnlyList<TSource> source,
    [NotNull] Func<TSource, TOutput> selector,
    bool throwExceptIfNull = true)
  {
    if (throwExceptIfNull)
      Intermech.Diagnostics.Check.ArgumentNotNull<IReadOnlyList<TSource>>(source, nameof (source));
    else if (source == null)
      return (IReadOnlyList<TOutput>) null;
    return source.Count == 0 ? (IReadOnlyList<TOutput>) Array.Empty<TOutput>() : (IReadOnlyList<TOutput>) new ReadOnlyListMapAdapter<TSource, TOutput>(source, selector);
  }

  public static bool IsEqualToList<T>([CanBeNull] this IReadOnlyList<T> first, [CanBeNull] IReadOnlyList<T> second)
  {
    if (first == second)
      return true;
    if (first == null || second == null)
      return false;
    int count = first.Count;
    if (count != second.Count)
      return false;
    if (count == 0)
      return true;
    for (int index = count - 1; index >= 0; --index)
    {
      if (object.Equals((object) first[index], (object) second[index]))
        return false;
    }
    return true;
  }

  [ContractAnnotation("list: null => null; => NotNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T[] GetArray<T>([CanBeNull] this IReadOnlyList<T> list)
  {
    if (list == null)
      return (T[]) null;
    if (list is T[] array)
      return array;
    return list.Count != 0 ? list.ToArray<T>(list.Count) : Array.Empty<T>();
  }
}
