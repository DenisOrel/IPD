// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.GeneralEnumerationExtensions
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public static class GeneralEnumerationExtensions
{
  public const int MinimumZeroCapacity = 16 /*0x10*/;
  public const int DefaultListCapacity = 16 /*0x10*/;

  [NotNull]
  [LinqTunnel]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<T> GeneralSelect<T>(
    [NotNull] this IEnumerable enumeration,
    [NotNull] Func<object, T> selector)
  {
    if (!(enumeration is IEnumerable<object> objects1))
      objects1 = enumeration.Cast<object>();
    IEnumerable<object> objects2 = objects1;
    int result;
    if (!objects2.TryGetCount<object>(out result))
      return objects2.Select<object, T>(selector);
    return result == 0 ? (IEnumerable<T>) Array.Empty<T>() : (IEnumerable<T>) objects2.Select<object, T>(selector).WrapWithCount<T>(result);
  }

  [NotNull]
  [LinqTunnel]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<object> GeneralWhere(
    [NotNull] this IEnumerable enumeration,
    [NotNull] Func<object, bool> filter)
  {
    if (!(enumeration is IEnumerable<object> source))
      source = enumeration.Cast<object>();
    return source.Where<object>(filter).WrapWithCountOrCapacity<object>(enumeration, false);
  }

  [NotNull]
  [LinqTunnel]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<T> GeneralWhere<T>([NotNull] this IEnumerable enumeration, [NotNull] Func<T, bool> filter)
  {
    if (!(enumeration is IEnumerable<T> source))
      source = enumeration.Cast<T>();
    return source.Where<T>(filter).WrapWithCountOrCapacity<T>(enumeration, false);
  }

  [CollectionAccess(CollectionAccessType.None)]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int GetRecommendedCapacity([CanBeNull, NoEnumeration] this IEnumerable enumeration, int baseCapacity = 16 /*0x10*/)
  {
    if (enumeration != null)
    {
      switch (enumeration)
      {
        case ArrayList arrayList:
          return Math.Max(baseCapacity, arrayList.Capacity);
        case CollectionBase collectionBase:
          return Math.Max(baseCapacity, collectionBase.Capacity);
        case SortedList sortedList:
          return Math.Max(baseCapacity, sortedList.Capacity);
        case ICapacity capacity:
          return Math.Max(baseCapacity, capacity.Capacity);
        default:
          int result;
          if (enumeration.TryGetCount(out result))
            return Math.Max(baseCapacity, result);
          break;
      }
    }
    return Math.Max(baseCapacity, 16 /*0x10*/);
  }

  [CollectionAccess(CollectionAccessType.None)]
  [ContractAnnotation("enumeration: Null => null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int? TryGetCapacity([CanBeNull, NoEnumeration] this IEnumerable enumeration)
  {
    int result;
    return !enumeration.TryGetCapacity(out result) ? new int?() : new int?(result);
  }

  [CollectionAccess(CollectionAccessType.None)]
  [ContractAnnotation("enumeration: Null => false")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetCapacity([CanBeNull, NoEnumeration] this IEnumerable enumeration, out int result)
  {
    if (enumeration != null)
    {
      switch (enumeration)
      {
        case ArrayList arrayList:
          result = arrayList.Capacity;
          return true;
        case CollectionBase collectionBase:
          result = collectionBase.Capacity;
          return true;
        case SortedList sortedList:
          result = sortedList.Capacity;
          return true;
        case ICapacity capacity:
          result = capacity.Capacity;
          return true;
        default:
          if (enumeration.TryGetCount(out result))
            return true;
          break;
      }
    }
    result = -1;
    return false;
  }

  [CollectionAccess(CollectionAccessType.None)]
  [ContractAnnotation("enumeration: Null => false")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TrySetCapacity([CanBeNull, NoEnumeration] this IEnumerable enumeration, int capacity)
  {
    if (enumeration != null)
    {
      switch (enumeration)
      {
        case ArrayList arrayList:
          arrayList.Capacity = capacity;
          return true;
        case CollectionBase collectionBase:
          collectionBase.Capacity = capacity;
          return true;
        case SortedList sortedList:
          sortedList.Capacity = capacity;
          return true;
      }
    }
    return false;
  }

  [CanBeNull]
  [CollectionAccess(CollectionAccessType.None)]
  [ContractAnnotation("enumeration: Null => null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int? TryGetCountOrCapacity([CanBeNull, NoEnumeration] this IEnumerable enumeration)
  {
    int result;
    return !enumeration.TryGetCountOrCapacity(out result) ? new int?() : new int?(result);
  }

  [CollectionAccess(CollectionAccessType.None)]
  [ContractAnnotation("enumeration: Null => false")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetCountOrCapacity([CanBeNull, NoEnumeration] this IEnumerable enumeration, out int result)
  {
    if (enumeration != null)
    {
      if (enumeration.TryGetCount(out result))
        return true;
      if (enumeration is ICapacity capacity)
      {
        result = capacity.Capacity;
        return true;
      }
    }
    result = -1;
    return false;
  }

  [CanBeNull]
  [CollectionAccess(CollectionAccessType.None)]
  [ContractAnnotation("enumeration: Null => null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int? TryGetCount([CanBeNull, NoEnumeration] this IEnumerable enumeration)
  {
    int result;
    return !enumeration.TryGetCount(out result) ? new int?() : new int?(result);
  }

  [CollectionAccess(CollectionAccessType.None)]
  [ContractAnnotation("enumeration: Null => false")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetCount([CanBeNull, NoEnumeration] this IEnumerable enumeration, out int result)
  {
    IEnumerable enumerable = enumeration;
    if (enumerable != null && enumerable is ICollection collection)
    {
      result = collection.Count;
      return true;
    }
    result = -1;
    return false;
  }
}
