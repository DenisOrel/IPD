// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.CollectionExtensions
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Collections;
using Intermech.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public static class CollectionExtensions
{
  [Pure]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyList<T> Clone<T>([NotNull] this ICollection<T> collectionToClone) where T : ICloneable
  {
    List<T> objList = new List<T>(collectionToClone.Count);
    objList.AddRange(collectionToClone.Select<T, T>((Func<T, T>) (item => (T) item?.Clone())));
    return (IReadOnlyList<T>) objList;
  }

  [Pure]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyList<T> CloneStructsList<T>([NotNull] this ICollection<T> collectionToClone) where T : struct
  {
    List<T> objList = new List<T>(collectionToClone.Count);
    objList.AddRange((IEnumerable<T>) collectionToClone);
    return (IReadOnlyList<T>) objList;
  }

  [Pure]
  [ContractAnnotation("throwExceptIfNull:true => NotNull; source:null => null; => NotNull")]
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<T> GetAsReadOnlyCollection<T>(
    [CanBeNull] this ICollection<T> source,
    bool throwExceptIfNull = true)
  {
    if (throwExceptIfNull)
      Intermech.Diagnostics.Check.ArgumentNotNull<ICollection<T>>(source, nameof (source));
    else if (source == null)
      return (IReadOnlyCollection<T>) null;
    if (source.Count == 0)
      return (IReadOnlyCollection<T>) Array.Empty<T>();
    return source is IReadOnlyCollection<T> objs ? objs : (IReadOnlyCollection<T>) new ReadOnlyList<T>((IEnumerable<T>) source);
  }

  [Pure]
  [ContractAnnotation("throwExceptIfNull:true => NotNull; source:null => null; => NotNull")]
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<TOutput> CastCollection<TSource, TOutput>(
    [CanBeNull] this ICollection<TSource> source,
    bool throwExceptIfNull = true)
    where TSource : TOutput
  {
    if (throwExceptIfNull)
      Intermech.Diagnostics.Check.ArgumentNotNull<ICollection<TSource>>(source, nameof (source));
    else if (source == null)
      return (IReadOnlyCollection<TOutput>) null;
    return source.Count == 0 ? (IReadOnlyCollection<TOutput>) Array.Empty<TOutput>() : (IReadOnlyCollection<TOutput>) new CollectionCastAdapter<TSource, TOutput>(source);
  }

  [Pure]
  [ContractAnnotation("throwExceptIfNull:true => NotNull; source:null => null; => NotNull")]
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<TOutput> MapCollection<TSource, TOutput>(
    [CanBeNull] this ICollection<TSource> source,
    [NotNull] Func<TSource, TOutput> selector,
    bool throwExceptIfNull = true)
  {
    if (throwExceptIfNull)
      Intermech.Diagnostics.Check.ArgumentNotNull<ICollection<TSource>>(source, nameof (source));
    else if (source == null)
      return (IReadOnlyCollection<TOutput>) null;
    return source.Count == 0 ? (IReadOnlyCollection<TOutput>) Array.Empty<TOutput>() : (IReadOnlyCollection<TOutput>) new CollectionMapAdapter<TSource, TOutput>(source, selector);
  }
}
