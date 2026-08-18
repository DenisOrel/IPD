// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.GeneralListExtensions
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

public static class GeneralListExtensions
{
  [NotNull]
  public static IReadOnlyList<TMapped> CastList<TMapped>([NotNull] this IList sourceList)
  {
    if (sourceList.Count == 0)
      return (IReadOnlyList<TMapped>) Array.Empty<TMapped>();
    return sourceList is IReadOnlyList<TMapped> mappedList ? mappedList : (IReadOnlyList<TMapped>) new GeneralListCastAdapter<TMapped>(sourceList);
  }

  [NotNull]
  public static IReadOnlyList<TMapped> MapList<TMapped>(
    [NotNull] this IList sourceList,
    [NotNull] Func<object, TMapped> selector)
  {
    if (sourceList.Count == 0)
      return (IReadOnlyList<TMapped>) Array.Empty<TMapped>();
    return sourceList is IReadOnlyList<TMapped> mappedList ? mappedList : (IReadOnlyList<TMapped>) new GeneralListMapAdapter<TMapped>(sourceList, selector);
  }

  [NotNull]
  public static IList<TMapped> Cast2MutableList<TMapped>([NotNull] this IList sourceList)
  {
    if (sourceList.Count == 0)
      return (IList<TMapped>) Array.Empty<TMapped>();
    return sourceList is IList<TMapped> mappedList ? mappedList : (IList<TMapped>) new GeneralListCastAdapter<TMapped>(sourceList);
  }

  [NotNull]
  public static IList<TMapped> Map2MutableList<TMapped>(
    [NotNull] this IList sourceList,
    [NotNull] Func<object, TMapped> selector)
  {
    if (sourceList.Count == 0)
      return (IList<TMapped>) Array.Empty<TMapped>();
    return sourceList is IList<TMapped> mappedList ? mappedList : (IList<TMapped>) new GeneralListMapAdapter<TMapped>(sourceList, selector);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool ContainsFrom<T>([NotNull] this List<T> list, int startIndex, [CanBeNull] T element)
  {
    for (int count = list.Count; startIndex < count; ++startIndex)
    {
      if (object.Equals((object) list[startIndex], (object) element))
        return true;
    }
    return false;
  }
}
