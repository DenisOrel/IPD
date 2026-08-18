// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.IReadOnlyCollectionExtensions
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

public static class IReadOnlyCollectionExtensions
{
  [ContractAnnotation("collection:null=>false")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool IsNullOrEmpty<T>([CanBeNull] this IReadOnlyCollection<T> collection)
  {
    return collection == null || collection.Count == 0;
  }

  [ContractAnnotation("throwExceptIfNull:true => NotNull; source:null => null; => NotNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<TOutput> CastReadOnlyCollection<TSource, TOutput>(
    [CanBeNull] this IReadOnlyCollection<TSource> source,
    bool throwExceptIfNull = true)
    where TSource : TOutput
  {
    if (throwExceptIfNull)
      Intermech.Diagnostics.Check.ArgumentNotNull<IReadOnlyCollection<TSource>>(source, nameof (source));
    else if (source == null)
      return (IReadOnlyCollection<TOutput>) null;
    if (source.Count == 0)
      return (IReadOnlyCollection<TOutput>) Array.Empty<TOutput>();
    return source is IReadOnlyCollection<TOutput> outputs ? outputs : (IReadOnlyCollection<TOutput>) new ReadOnlyCollectionCastAdapter<TSource, TOutput>(source);
  }

  [ContractAnnotation("throwExceptIfNull:true => NotNull; source:null => null; => NotNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<TOutput> MapReadOnlyCollection<TSource, TOutput>(
    [CanBeNull] this IReadOnlyCollection<TSource> source,
    [NotNull] Func<TSource, TOutput> selector,
    bool throwExceptIfNull = true)
  {
    if (throwExceptIfNull)
      Intermech.Diagnostics.Check.ArgumentNotNull<IReadOnlyCollection<TSource>>(source, nameof (source));
    else if (source == null)
      return (IReadOnlyCollection<TOutput>) null;
    if (source.Count == 0)
      return (IReadOnlyCollection<TOutput>) Array.Empty<TOutput>();
    return source is IReadOnlyCollection<TOutput> outputs ? outputs : (IReadOnlyCollection<TOutput>) new ReadOnlyCollectionMapAdapter<TSource, TOutput>(source, selector);
  }

  public static bool IsEqualToCollection<T>(
    [CanBeNull] this IReadOnlyCollection<T> first,
    [CanBeNull] IReadOnlyCollection<T> second)
  {
    if (first == second)
      return true;
    if (first == null || second == null || first.Count != second.Count)
      return false;
    if (first.Count == 0)
      return true;
    using (IEnumerator<T> enumerator1 = first.GetEnumerator())
    {
      using (IEnumerator<T> enumerator2 = second.GetEnumerator())
      {
        while (enumerator1.MoveNext())
        {
          if (enumerator2.MoveNext())
          {
            if (!object.Equals((object) enumerator1.Current, (object) enumerator2.Current))
              return false;
          }
          else
            break;
        }
      }
    }
    return true;
  }
}
