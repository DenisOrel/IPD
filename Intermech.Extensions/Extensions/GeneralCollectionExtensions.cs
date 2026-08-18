// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.GeneralCollectionExtensions
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Collections;
using Intermech.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public static class GeneralCollectionExtensions
{
  [Pure]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<TMapped> CastCollection<TMapped>(
    [NotNull] this ICollection sourceCollection)
  {
    if (sourceCollection.Count == 0)
      return (IReadOnlyCollection<TMapped>) Array.Empty<TMapped>();
    return sourceCollection is IReadOnlyCollection<TMapped> mappeds ? mappeds : (IReadOnlyCollection<TMapped>) new GeneralCollectionCastAdapter<TMapped>(sourceCollection);
  }

  [Pure]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<TMapped> MapCollection<TMapped>(
    [NotNull] this ICollection sourceCollection,
    [NotNull] Func<object, TMapped> selector)
  {
    if (sourceCollection.Count == 0)
      return (IReadOnlyCollection<TMapped>) Array.Empty<TMapped>();
    return sourceCollection is IReadOnlyCollection<TMapped> mappeds ? mappeds : (IReadOnlyCollection<TMapped>) new GeneralCollectionMapAdapter<TMapped>(sourceCollection, selector);
  }

  [Pure]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ICollection<TMapped> Cast2MutableCollection<TMapped>(
    [NotNull] this ICollection sourceCollection)
  {
    if (sourceCollection.Count == 0)
      return (ICollection<TMapped>) Array.Empty<TMapped>();
    return sourceCollection is ICollection<TMapped> mappeds ? mappeds : (ICollection<TMapped>) new GeneralCollectionCastAdapter<TMapped>(sourceCollection);
  }

  [Pure]
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ICollection<TMapped> Map2MutableCollection<TMapped>(
    [NotNull] this ICollection sourceCollection,
    [NotNull] Func<object, TMapped> selector)
  {
    if (sourceCollection.Count == 0)
      return (ICollection<TMapped>) Array.Empty<TMapped>();
    return sourceCollection is ICollection<TMapped> mappeds ? mappeds : (ICollection<TMapped>) new GeneralCollectionMapAdapter<TMapped>(sourceCollection, selector);
  }
}
