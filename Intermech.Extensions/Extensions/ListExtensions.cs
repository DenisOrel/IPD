// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.ListExtensions
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Collections;
using Intermech.Diagnostics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public static class ListExtensions
{
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyList<T> GetRange<T>([NotNull] this IList<T> source, int index, int count)
  {
    switch (source)
    {
      case List<T> objList:
        return (IReadOnlyList<T>) objList.GetRange(index, count);
      case T[] sourceArray:
        T[] range1 = new T[count];
        int sourceIndex = index;
        T[] destinationArray = range1;
        int length = count;
        Array.Copy((Array) sourceArray, sourceIndex, (Array) destinationArray, 0, length);
        return (IReadOnlyList<T>) range1;
      default:
        List<T> range2 = new List<T>(count);
        int num = index;
        for (int index1 = 0; index1 < count; ++index1)
          range2.Add(source[num++]);
        return (IReadOnlyList<T>) range2;
    }
  }

  [NotNull]
  [ItemCanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<T> FromIndexToRight<T>([NotNull] this IList<T> list, int startIndex)
  {
    int count = ((ICollection<T>) list).Count;
    for (int index = startIndex; index < count; ++index)
      yield return list[index];
  }

  [NotNull]
  [ItemCanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<T> FromIndexToLeft<T>([NotNull] this IList<T> list, int startIndex)
  {
    for (int index = startIndex; index >= 0; --index)
      yield return list[index];
  }

  [NotNull]
  [ItemCanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<T> Between<T>([NotNull] this IList<T> list, int startIndex, int finishIndex)
  {
    int index;
    if (finishIndex > startIndex)
    {
      for (index = startIndex; index <= finishIndex; ++index)
        yield return list[index];
    }
    else
    {
      for (index = startIndex; index >= finishIndex; --index)
        yield return list[index];
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int BinaryIndexOfAnyMatch<T>([NotNull] this IList<T> list, [NotNull] Func<T, int> predicate)
  {
    int num1 = list.Count - 1;
    if (num1 > 0)
    {
      int num2 = 0;
      while (num2 <= num1)
      {
        int index = num2 + (num1 - num2 >> 1);
        int num3 = predicate(list[index]);
        if (num3 == 0)
          return index;
        if (num3 < 0)
          num2 = index + 1;
        else
          num1 = index - 1;
      }
    }
    return -1;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool BinaryTryIndexOfAnyMatch<T>(
    [NotNull] this IList<T> list,
    out int index,
    [NotNull] Func<T, int> predicate)
  {
    index = list.BinaryIndexOfAnyMatch<T>(predicate);
    return index >= 0;
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T BinaryGetAnyMatch<T>([NotNull] this IList<T> list, [NotNull] Func<T, int> predicate)
  {
    int index = list.BinaryIndexOfAnyMatch<T>(predicate);
    return index < 0 ? default (T) : list[index];
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool BinaryTryGetAnyMatch<T>(
    [NotNull] this IList<T> list,
    [CanBeNull] out T result,
    [NotNull] Func<T, int> predicate)
  {
    int index = list.BinaryIndexOfAnyMatch<T>(predicate);
    result = index >= 0 ? list[index] : default (T);
    return index >= 0;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int BinaryIndexOfLeftMostMatch<T>([NotNull] this IList<T> list, [NotNull] Func<T, int> predicate)
  {
    int num1 = list.Count - 1;
    if (num1 > 0)
    {
      int num2 = 0;
      bool flag = false;
      while (num2 <= num1)
      {
        int index = num2 + (num1 - num2 >> 1);
        int num3 = predicate(list[index]);
        if (num3 < 0)
        {
          num2 = index + 1;
        }
        else
        {
          if (!flag && num3 == 0)
            flag = true;
          num1 = index - 1;
        }
      }
      if (flag)
        return num2;
    }
    return -1;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool BinaryTryIndexOfLeftMostMatch<T>(
    [NotNull] this IList<T> list,
    out int index,
    [NotNull] Func<T, int> predicate)
  {
    index = list.BinaryIndexOfLeftMostMatch<T>(predicate);
    return index >= 0;
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T BinaryGetLeftMostMatch<T>([NotNull] this IList<T> list, [NotNull] Func<T, int> predicate)
  {
    int index = list.BinaryIndexOfLeftMostMatch<T>(predicate);
    return index < 0 ? default (T) : list[index];
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool BinaryTryGetLeftMostMatch<T>(
    [NotNull] this IList<T> list,
    [CanBeNull] out T result,
    [NotNull] Func<T, int> predicate)
  {
    int index = list.BinaryIndexOfLeftMostMatch<T>(predicate);
    result = index >= 0 ? list[index] : default (T);
    return index >= 0;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int BinaryIndexOfRightMostMatch<T>([NotNull] this IList<T> list, [NotNull] Func<T, int> predicate)
  {
    int num1 = list.Count - 1;
    if (num1 > 0)
    {
      int num2 = 0;
      bool flag = false;
      while (num2 <= num1)
      {
        int index = num2 + (num1 - num2 >> 1);
        int num3 = predicate(list[index]);
        if (num3 <= 0)
        {
          if (!flag && num3 == 0)
            flag = true;
          num2 = index + 1;
        }
        else
          num1 = index - 1;
      }
      if (flag)
        return num2 - 1;
    }
    return -1;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool BinaryTryIndexOfRightMostMatch<T>(
    [NotNull] this IList<T> list,
    out int index,
    [NotNull] Func<T, int> predicate)
  {
    index = list.BinaryIndexOfRightMostMatch<T>(predicate);
    return index >= 0;
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T BinaryGetRightMostMatch<T>([NotNull] this IList<T> list, [NotNull] Func<T, int> predicate)
  {
    int index = list.BinaryIndexOfRightMostMatch<T>(predicate);
    return index < 0 ? default (T) : list[index];
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool BinaryTryGetRightMostMatch<T>(
    [NotNull] this IList<T> list,
    [CanBeNull] out T result,
    [NotNull] Func<T, int> predicate)
  {
    int index = list.BinaryIndexOfRightMostMatch<T>(predicate);
    result = index >= 0 ? list[index] : default (T);
    return index >= 0;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool BinaryTryIndexOfMultipleValues<T>(
    [NotNull] this IList<T> list,
    out int low,
    out int high,
    [NotNull] Func<T, int> predicate)
  {
    if (list.Count == 0)
    {
      low = -1;
      high = -1;
      return false;
    }
    int num1 = list.Count - 1;
    int num2 = 0;
    int num3 = num1;
    int num4 = -1;
    int num5 = -1;
    while (num2 <= num3)
    {
      int index = num2 + (num3 - num2 >> 1);
      int num6 = predicate(list[index]);
      if (num6 > 0)
      {
        num4 = index;
        num3 = index - 1;
      }
      else if (num6 < 0)
      {
        num2 = index + 1;
      }
      else
      {
        if (num5 == -1)
          num5 = index;
        num3 = index - 1;
      }
    }
    if (num5 == -1)
    {
      low = -1;
      high = -1;
      return false;
    }
    low = num2;
    if (low == num1)
      high = low;
    else if (num4 == num5 + 1)
    {
      high = num5;
    }
    else
    {
      int num7 = num5;
      int num8 = num4 >= 0 ? num4 : num1;
      while (num7 <= num8)
      {
        int index = num7 + (num8 - num7 >> 1);
        if (predicate(list[index]) > 0)
          num8 = index - 1;
        else
          num7 = index + 1;
      }
      high = num7 - 1;
    }
    return true;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<T> BinaryWhere<T>([NotNull] this IList<T> list, [NotNull] Func<T, int> predicate)
  {
    int low;
    int high;
    return list.Count > 0 && list.BinaryTryIndexOfMultipleValues<T>(out low, out high, predicate) ? list.Between<T>(low, high) : Enumerable.Empty<T>();
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T[] BinaryWhereArray<T>([NotNull] this IList<T> list, [NotNull] Func<T, int> predicate)
  {
    int low;
    int high;
    if (list.Count <= 0 || !list.BinaryTryIndexOfMultipleValues<T>(out low, out high, predicate))
      return Array.Empty<T>();
    T[] objArray = new T[high - low + 1];
    int num = 0;
    foreach (T obj in list.Between<T>(low, high))
      objArray[num++] = obj;
    return objArray;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static List<T> CreateFromSingle<T>([CanBeNull] T singleElement)
  {
    return new List<T>(1) { singleElement };
  }

  [ContractAnnotation("list:null => false")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool RemoveLast<T>([CanBeNull] this IList<T> list, [CanBeNull] T item)
  {
    if (list != null && list.Any<T>())
    {
      for (int index = list.Count - 1; index >= 0; --index)
      {
        if (object.Equals((object) list[index], (object) item))
        {
          list.RemoveAt(index);
          return true;
        }
      }
    }
    return false;
  }

  [ContractAnnotation("throwExceptIfNull:true => NotNull; source:null => null; => NotNull")]
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyList<T> GetAsReadOnlyList<T>([CanBeNull] this IList<T> source, bool throwExceptIfNull = true)
  {
    if (throwExceptIfNull)
      Intermech.Diagnostics.Check.ArgumentNotNull<IList<T>>(source, nameof (source));
    else if (source == null)
      return (IReadOnlyList<T>) null;
    if (source.Count == 0)
      return (IReadOnlyList<T>) Array.Empty<T>();
    return source is IReadOnlyList<T> objList ? objList : (IReadOnlyList<T>) new ReadOnlyList<T>((IEnumerable<T>) source);
  }

  [ContractAnnotation("throwExceptIfNull:true => NotNull; source:null => null; => NotNull")]
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyList<TOutput> CastList<TSource, TOutput>(
    [CanBeNull] this IList<TSource> source,
    bool throwExceptIfNull = true)
    where TSource : TOutput
  {
    if (throwExceptIfNull)
      Intermech.Diagnostics.Check.ArgumentNotNull<IList<TSource>>(source, nameof (source));
    else if (source == null)
      return (IReadOnlyList<TOutput>) null;
    if (source.Count == 0)
      return (IReadOnlyList<TOutput>) Array.Empty<TOutput>();
    return source is IReadOnlyList<TOutput> outputList ? outputList : (IReadOnlyList<TOutput>) new ListCastAdapter<TSource, TOutput>(source);
  }

  [ContractAnnotation("throwExceptIfNull:true => NotNull; source:null => null; => NotNull")]
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyList<TOutput> MapList<TSource, TOutput>(
    [CanBeNull] this IList<TSource> source,
    [NotNull] Func<TSource, TOutput> selector,
    bool throwExceptIfNull = true)
  {
    if (throwExceptIfNull)
      Intermech.Diagnostics.Check.ArgumentNotNull<IList<TSource>>(source, nameof (source));
    else if (source == null)
      return (IReadOnlyList<TOutput>) null;
    return source.Count == 0 ? (IReadOnlyList<TOutput>) Array.Empty<TOutput>() : (IReadOnlyList<TOutput>) new ListMapAdapter<TSource, TOutput>(source, selector);
  }

  [NotNull]
  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static List<T> SortList<T>([NotNull] this List<T> list)
  {
    list.Sort();
    return list;
  }

  [NotNull]
  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static List<T> SortList<T>([NotNull] this List<T> list, [NotNull] Comparison<T> comparison)
  {
    list.Sort(comparison);
    return list;
  }

  [NotNull]
  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static List<T> SortList<T>([NotNull] this List<T> list, [NotNull] IComparer<T> comparer)
  {
    list.Sort(comparer);
    return list;
  }
}
