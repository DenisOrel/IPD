// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.Enumeration
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Collections;
using Intermech.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public static class Enumeration
{
  public const int MinimumZeroCapacity = 16 /*0x10*/;
  public const int DefaultListCapacity = 16 /*0x10*/;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool MoreThanOne<T>([Intermech.Diagnostics.NotNull] this IEnumerable<T> enumerable)
  {
    using (IEnumerator<T> enumerator = enumerable.GetEnumerator())
      return enumerator.MoveNext() && enumerator.MoveNext();
  }

  [Intermech.Diagnostics.NotNull]
  [LinqTunnel]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<T> Distinct<T, TKey>(
    [Intermech.Diagnostics.NotNull] this IEnumerable<T> enumerable,
    [Intermech.Diagnostics.NotNull] Func<T, TKey> keySelector)
  {
    return enumerable.Distinct<T>((IEqualityComparer<T>) new EqualityComparerByKey<T, TKey>((EqualityComparerByKey<T, TKey>.KeySelectorMethodDelegate) (item => keySelector(item))));
  }

  [Intermech.Diagnostics.NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<T> Append<T>([Intermech.Diagnostics.NotNull] this IEnumerable<T> enumerable, [Intermech.Diagnostics.NotNull] params T[] elements)
  {
    return enumerable.Concat<T>((IEnumerable<T>) elements);
  }

  [Intermech.Diagnostics.NotNull]
  [ItemNotNull]
  [LinqTunnel]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<TResult> SelectManyNotNull<TSource, TResult>(
    [Intermech.Diagnostics.NotNull] this IEnumerable<TSource> enumerable,
    [Intermech.Diagnostics.NotNull] Func<TSource, IEnumerable<TResult>> selector)
    where TResult : class
  {
    return enumerable.SelectMany<TSource, TResult>(selector).Where<TResult>((Func<TResult, bool>) (result => (object) result != null));
  }

  [Intermech.Diagnostics.NotNull]
  [ItemNotNull]
  [LinqTunnel]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<TResult> SelectManyNotNull<TSource, TCollection, TResult>(
    [Intermech.Diagnostics.NotNull] this IEnumerable<TSource> enumerable,
    [Intermech.Diagnostics.NotNull] Func<TSource, int, IEnumerable<TCollection>> collectionSelector,
    [Intermech.Diagnostics.NotNull] Func<TSource, TCollection, TResult> resultSelector)
    where TResult : class
  {
    return enumerable.SelectMany<TSource, TCollection, TResult>(collectionSelector, resultSelector).Where<TResult>((Func<TResult, bool>) (result => (object) result != null));
  }

  [Intermech.Diagnostics.NotNull]
  [ItemNotNull]
  [LinqTunnel]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<TResult> SelectManyNotNull<TSource, TResult>(
    [Intermech.Diagnostics.NotNull] this IEnumerable<TSource> enumerable,
    [Intermech.Diagnostics.NotNull] Func<TSource, int, IEnumerable<TResult>> selector)
    where TResult : class
  {
    return enumerable.SelectMany<TSource, TResult>(selector).Where<TResult>((Func<TResult, bool>) (result => (object) result != null));
  }

  [Intermech.Diagnostics.NotNull]
  [ItemNotNull]
  [LinqTunnel]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<TResult> SelectManyNotNull<TSource, TCollection, TResult>(
    [Intermech.Diagnostics.NotNull] this IEnumerable<TSource> enumerable,
    [Intermech.Diagnostics.NotNull] Func<TSource, IEnumerable<TCollection>> collectionSelector,
    [Intermech.Diagnostics.NotNull] Func<TSource, TCollection, TResult> resultSelector)
    where TResult : class
  {
    return enumerable.SelectMany<TSource, TCollection, TResult>(collectionSelector, resultSelector).Where<TResult>((Func<TResult, bool>) (result => (object) result != null));
  }

  [CanBeNull]
  [LinqTunnel]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<TResult> NullOrSelect<TSource, TResult>(
    [CanBeNull] this IEnumerable<TSource> enumerable,
    [Intermech.Diagnostics.NotNull] Func<TSource, TResult> selector)
    where TResult : class
  {
    return enumerable == null ? (IEnumerable<TResult>) null : enumerable.Select<TSource, TResult>(selector);
  }

  [Intermech.Diagnostics.NotNull]
  [LinqTunnel]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<T> EmptyIfNull<T>([CanBeNull] this IEnumerable<T> enumerable)
  {
    return enumerable ?? Enumerable.Empty<T>();
  }

  [Intermech.Diagnostics.NotNull]
  [LinqTunnel]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<string> SelectNotNullNotEmptyStrings<T>(
    [Intermech.Diagnostics.NotNull] this IEnumerable<T> enumerable,
    [Intermech.Diagnostics.NotNull] Func<T, string> selector)
  {
    return enumerable.Select<T, string>(selector).Where<string>((Func<string, bool>) (result => !string.IsNullOrEmpty(result))).WrapWithCountOrCapacity<string>((IEnumerable) enumerable, false);
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TResult SelectFirstNotNull<TSource, TResult>(
    [Intermech.Diagnostics.NotNull] this IEnumerable<TSource> enumerable,
    [Intermech.Diagnostics.NotNull, InstantHandle] Func<TSource, TResult> selector)
    where TResult : class
  {
    return enumerable.Select<TSource, TResult>(selector).FirstOrDefault<TResult>((Func<TResult, bool>) (result => (object) result != null));
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TResult SelectFirstNotNullOrDefault<TSource, TResult>(
    [Intermech.Diagnostics.NotNull] this IEnumerable<TSource> enumerable,
    [Intermech.Diagnostics.NotNull, InstantHandle] Func<TSource, TResult> selector,
    [CanBeNull] TResult defaultResult)
    where TResult : class
  {
    return enumerable.Select<TSource, TResult>(selector).FirstOrDefault<TResult>((Func<TResult, bool>) (result => (object) result != null), defaultResult);
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TResult SelectFirstNotNullOrDefault<TSource, TResult>(
    [Intermech.Diagnostics.NotNull] this IEnumerable<TSource> enumerable,
    [Intermech.Diagnostics.NotNull, InstantHandle] Func<TSource, TResult> selector,
    [Intermech.Diagnostics.NotNull] Func<TResult> createResult)
    where TResult : class
  {
    return enumerable.Select<TSource, TResult>(selector).FirstOrDefault<TResult>((Func<TResult, bool>) (result => (object) result != null), createResult);
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T SelectFirstNotNull<T>([Intermech.Diagnostics.NotNull] this IEnumerable<T> enumerable) where T : class
  {
    foreach (T obj in enumerable)
    {
      if ((object) obj != null)
        return obj;
    }
    return default (T);
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T SelectFirstNotNull<T>([Intermech.Diagnostics.NotNull] this IEnumerable<T> enumerable, [CanBeNull] T defaultResult) where T : class
  {
    foreach (T obj in enumerable)
    {
      if ((object) obj != null)
        return obj;
    }
    return defaultResult;
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T SelectFirstNotNull<T>([Intermech.Diagnostics.NotNull] this IEnumerable<T> enumerable, [Intermech.Diagnostics.NotNull] Func<T> createResult) where T : class
  {
    foreach (T obj in enumerable)
    {
      if ((object) obj != null)
        return obj;
    }
    return createResult();
  }

  [Intermech.Diagnostics.NotNull]
  [ItemCanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<T> Create<T>([CanBeNull] T element)
  {
    return (IEnumerable<T>) new T[1]{ element };
  }

  [Intermech.Diagnostics.NotNull]
  [ItemCanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<T> CreateOrEmptyIfNull<T>([CanBeNull] T element) where T : class
  {
    if ((object) element == null)
      return (IEnumerable<T>) Array.Empty<T>();
    return (IEnumerable<T>) new T[1]{ element };
  }

  [Intermech.Diagnostics.NotNull]
  [ItemCanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<T> Create<T>([Intermech.Diagnostics.NotNull, ItemCanBeNull] params T[] args)
  {
    return Enumeration.Enumerate<T>(args);
  }

  [Intermech.Diagnostics.NotNull]
  [ItemNotNull]
  [MustUseReturnValue]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal static T[] BuildArray<T>(
    [Intermech.Diagnostics.NotNull] T item,
    [Intermech.Diagnostics.NotNull] Enumeration.GetNextItemDelegate<T> getNextItem,
    int index)
    where T : class
  {
    T obj = getNextItem(item);
    T[] objArray = (object) obj == null ? new T[index + 1] : Enumeration.BuildArray<T>(obj, getNextItem, index + 1);
    objArray[index] = item;
    return objArray;
  }

  [Intermech.Diagnostics.NotNull]
  [ItemNotNull]
  [MustUseReturnValue]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyList<T> Create<T>([Intermech.Diagnostics.NotNull] T item, [Intermech.Diagnostics.NotNull] Enumeration.GetNextItemDelegate<T> getNextItem) where T : class
  {
    return (IReadOnlyList<T>) Enumeration.BuildArray<T>(item, getNextItem, 0);
  }

  [Intermech.Diagnostics.NotNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyList<T> Create<T>([Intermech.Diagnostics.NotNull] T item, [Intermech.Diagnostics.NotNull, InstantHandle] Enumeration.ExpandFuncDelegate<T> expandFunc) where T : class
  {
    return Enumeration.Create<T>(item).Expand<T>(expandFunc);
  }

  [Intermech.Diagnostics.NotNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<T> CreateUnique<T>(
    [Intermech.Diagnostics.NotNull] T item,
    [Intermech.Diagnostics.NotNull, InstantHandle] Func<T, T> getNextItem,
    int capacity = 0)
    where T : class
  {
    HashSet<T> unique = capacity == 0 ? new HashSet<T>() : new HashSet<T>(capacity);
    unique.Add(item);
    do
    {
      item = getNextItem(item);
    }
    while ((object) item != null && !unique.Contains(item));
    return (IReadOnlyCollection<T>) unique;
  }

  [Intermech.Diagnostics.NotNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<T> CreateUnique<T>(
    [Intermech.Diagnostics.NotNull] T startItem,
    [Intermech.Diagnostics.NotNull, InstantHandle] Enumeration.ExpandFuncDelegate<T> expandFunc,
    int capacity = 0)
    where T : class
  {
    HashSet<T> unique = capacity == 0 ? new HashSet<T>() : new HashSet<T>(capacity);
    unique.Add(startItem);
    Queue<T> objQueue = new Queue<T>();
    objQueue.Enqueue(startItem);
    while (objQueue.Count > 0)
    {
      T obj1 = objQueue.Dequeue();
      IEnumerable<T> objs = expandFunc(obj1);
      if (objs != null)
      {
        foreach (T obj2 in objs)
        {
          if ((object) obj2 != null && !objQueue.Contains(obj2))
          {
            unique.Add(obj2);
            objQueue.Enqueue(obj2);
          }
        }
      }
    }
    return (IReadOnlyCollection<T>) unique;
  }

  [Intermech.Diagnostics.NotNull]
  [ItemCanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static IEnumerable<T> Enumerate<T>([Intermech.Diagnostics.NotNull, ItemCanBeNull] params T[] args)
  {
    return (IEnumerable<T>) args;
  }

  [Intermech.Diagnostics.NotNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<T> NotNull<T>([Intermech.Diagnostics.NotNull, ItemCanBeNull] this IEnumerable<T> enumerable) where T : class
  {
    return enumerable.Where<T>((Func<T, bool>) (element => (object) element != null)).WrapWithCountOrCapacity<T>((IEnumerable) enumerable, false);
  }

  [Intermech.Diagnostics.NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<T> NotNull<T>([Intermech.Diagnostics.NotNull, ItemCanBeNull] this IEnumerable<T?> enumerable) where T : struct
  {
    int result;
    return enumerable.TryGetCount<T?>(out result) && result == 0 ? (IEnumerable<T>) Array.Empty<T>() : enumerable.Where<T?>((Func<T?, bool>) (element => element.HasValue)).Select<T?, T>((Func<T?, T>) (element => element.Value)).WrapWithCountOrCapacity<T>((IEnumerable) enumerable, false);
  }

  [Intermech.Diagnostics.NotNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<T> NotDefault<T>([Intermech.Diagnostics.NotNull, ItemCanBeNull] this IEnumerable<T> enumerable)
  {
    EqualityComparer<T> equalityComparer = EqualityComparer<T>.Default;
    return enumerable.Where<T>((Func<T, bool>) (element => !equalityComparer.Equals(element, default (T)))).WrapWithCountOrCapacity<T>((IEnumerable) enumerable, false);
  }

  [Intermech.Diagnostics.NotNull]
  [ItemCanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<T> InvokeForAll<T, T2>(
    [Intermech.Diagnostics.NotNull] this IEnumerable<T> enumerable,
    [Intermech.Diagnostics.NotNull] Func<T, IEnumerable<T2>> convertFunc,
    [Intermech.Diagnostics.NotNull, InstantHandle] Action<T, T2> handler)
  {
    foreach (T obj1 in enumerable)
    {
      foreach (T2 obj2 in convertFunc(obj1))
        handler(obj1, obj2);
    }
    return enumerable;
  }

  [Intermech.Diagnostics.NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<T> InvokeForAll<T>(
    [Intermech.Diagnostics.NotNull] this IEnumerable<T> enumerable,
    [Intermech.Diagnostics.NotNull, InstantHandle] Action<int, T> handler)
  {
    int num = 0;
    foreach (T obj in enumerable)
      handler(num++, obj);
    return enumerable;
  }

  [Intermech.Diagnostics.NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<T> InvokeWhile<T>([Intermech.Diagnostics.NotNull] this IEnumerable<T> enumerable, [Intermech.Diagnostics.NotNull, InstantHandle] Func<T, bool> handler)
  {
    foreach (T obj in enumerable)
    {
      if (!handler(obj))
        break;
    }
    return enumerable;
  }

  [Intermech.Diagnostics.NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<T> InvokeForFirst<T>([Intermech.Diagnostics.NotNull] this IEnumerable<T> enumerable, [Intermech.Diagnostics.NotNull, InstantHandle] Action<T> handler)
  {
    using (IEnumerator<T> enumerator = enumerable.GetEnumerator())
    {
      if (enumerator.MoveNext())
        handler(enumerator.Current);
    }
    return enumerable;
  }

  [Intermech.Diagnostics.NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<T> InvokeForFirst<T>(
    [Intermech.Diagnostics.NotNull] this IEnumerable<T> enumerable,
    [Intermech.Diagnostics.NotNull, InstantHandle] Predicate<T> condition,
    [Intermech.Diagnostics.NotNull, InstantHandle] Action<T> handler)
  {
    if (enumerable is List<T> objList)
    {
      int index = objList.FindIndex(condition);
      if (index != -1)
      {
        T obj = objList[index];
        handler(obj);
      }
    }
    else
    {
      foreach (T obj in enumerable)
      {
        if (condition(obj))
        {
          handler(obj);
          break;
        }
      }
    }
    return enumerable;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int IndexOfFirst<T>([CanBeNull] this IEnumerable<T> enumerable, [Intermech.Diagnostics.NotNull, InstantHandle] Predicate<T> condition)
  {
    if (enumerable != null)
    {
      if (enumerable is List<T> objList)
        return objList.FindIndex(condition);
      int num = 0;
      foreach (T obj in enumerable)
      {
        if (condition(obj))
          return num;
        ++num;
      }
    }
    return -1;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int IndexOf<T>([CanBeNull] this IEnumerable<T> enumerable, [Intermech.Diagnostics.NotNull, InstantHandle] T item)
  {
    switch (enumerable)
    {
      case null:
        return -1;
      case List<T> objList:
        return objList.IndexOf(item);
      case ArrayList arrayList:
        return arrayList.IndexOf((object) item);
      default:
        int num = 0;
        using (IEnumerator<T> enumerator = enumerable.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            T current = enumerator.Current;
            if (object.Equals((object) item, (object) current))
              return num;
            ++num;
          }
          goto case null;
        }
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int IndexOf<T>([CanBeNull] this IEnumerable<T> enumerable, [Intermech.Diagnostics.NotNull, InstantHandle] Predicate<T> condition)
  {
    if (enumerable != null)
    {
      if (enumerable is List<T> objList)
        return objList.FindIndex(condition);
      int num = 0;
      foreach (T obj in enumerable)
      {
        if (condition(obj))
          return num;
        ++num;
      }
    }
    return -1;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int LastIndexOf<T>([CanBeNull] this IEnumerable<T> enumerable, [Intermech.Diagnostics.NotNull, InstantHandle] T item)
  {
    switch (enumerable)
    {
      case null:
        return -1;
      case List<T> objList:
        return objList.LastIndexOf(item);
      case ArrayList arrayList:
        return arrayList.LastIndexOf((object) arrayList);
      default:
        int num = 0;
        using (IEnumerator<T> enumerator = enumerable.Reverse<T>().GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            T current = enumerator.Current;
            if (object.Equals((object) item, (object) current))
              return num;
            ++num;
          }
          goto case null;
        }
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int LastIndexOf<T>([CanBeNull] this IEnumerable<T> enumerable, [Intermech.Diagnostics.NotNull, InstantHandle] Predicate<T> condition)
  {
    if (enumerable != null)
    {
      int num = 0;
      foreach (T obj in enumerable.Reverse<T>())
      {
        if (condition(obj))
          return num;
        ++num;
      }
    }
    return -1;
  }

  [ContractAnnotation("enumerable:null => false")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool Contains<T>([CanBeNull] this IEnumerable<T> enumerable, [Intermech.Diagnostics.NotNull, InstantHandle] Predicate<T> condition)
  {
    if (enumerable == null)
      return false;
    return enumerable is List<T> objList ? objList.FindIndex(condition) != -1 : enumerable.Any<T>((Func<T, bool>) (item => condition(item)));
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int IndexOfFirst([CanBeNull] this IEnumerable enumerable, [Intermech.Diagnostics.NotNull, InstantHandle] Predicate<object> condition)
  {
    if (enumerable != null)
    {
      if (enumerable is List<object> objectList)
        return objectList.FindIndex(condition);
      int num = 0;
      foreach (object obj in enumerable)
      {
        if (condition(obj))
          return num;
        ++num;
      }
    }
    return -1;
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<T> ConcatIgnoreNull<T>(
    [CanBeNull] this IEnumerable<T> enumerable,
    [CanBeNull] IEnumerable<T> secondEnumeration)
  {
    if (enumerable == null)
      return secondEnumeration;
    return secondEnumeration == null ? enumerable : enumerable.Concat<T>(secondEnumeration);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool SequenceEqualIgnoreOrder<T>(
    [CanBeNull] this IEnumerable<T> enumerable1,
    [CanBeNull] IEnumerable<T> enumerable2)
  {
    if (enumerable1 == enumerable2)
      return true;
    return enumerable1 != null && enumerable2 != null && ((IEnumerable<T>) enumerable1.Distinct<T>().OrderBy<T, T>((Func<T, T>) (t => t)).ToArray<T>()).SequenceEqual<T>((IEnumerable<T>) enumerable2.Distinct<T>().OrderBy<T, T>((Func<T, T>) (t => t)).ToArray<T>());
  }

  [Intermech.Diagnostics.NotNull]
  [ItemCanBeNull]
  [LinqTunnel]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<T> TakeWhile<T>(
    [Intermech.Diagnostics.NotNull] this IEnumerable<T> enumerable,
    [Intermech.Diagnostics.NotNull] Func<T, bool> predicate,
    bool includeLastItem)
  {
    if (includeLastItem)
    {
      foreach (T obj in enumerable)
      {
        if (predicate(obj))
        {
          yield return obj;
        }
        else
        {
          yield return obj;
          break;
        }
      }
    }
    else
    {
      foreach (T obj in enumerable.TakeWhile<T>(predicate))
        yield return obj;
    }
  }

  [Intermech.Diagnostics.NotNull]
  [ItemCanBeNull]
  [LinqTunnel]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<T> TakeWhile<T>(
    [Intermech.Diagnostics.NotNull] this IEnumerable<T> enumerable,
    [Intermech.Diagnostics.NotNull] Func<T, int, bool> predicate,
    bool includeLastItem)
  {
    if (includeLastItem)
    {
      int index = 0;
      foreach (T obj in enumerable)
      {
        if (predicate(obj, index))
        {
          yield return obj;
          ++index;
        }
        else
        {
          yield return obj;
          break;
        }
      }
    }
    else
    {
      foreach (T obj in enumerable.TakeWhile<T>(predicate))
        yield return obj;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool Any<T>([Intermech.Diagnostics.NotNull] this IEnumerable<T> enumerable, [Intermech.Diagnostics.NotNull, InstantHandle] Func<T, int, bool> predicate)
  {
    int index = 0;
    return enumerable.Any<T>((Func<T, bool>) (item => predicate(item, index++)));
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool ContainsDuplicates<T>([Intermech.Diagnostics.NotNull] this IEnumerable<T> enumerable)
  {
    using (IEnumerator<T> enumerator = enumerable.GetEnumerator())
    {
      if (enumerator.MoveNext())
      {
        T current = enumerator.Current;
        while (enumerator.MoveNext())
        {
          if (current.Equals((object) enumerator.Current))
            return true;
          current = enumerator.Current;
        }
      }
    }
    return false;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool IsOrdered<T>([Intermech.Diagnostics.NotNull] this IEnumerable<T> enumerable, bool acceptEquals = false)
  {
    return enumerable.IsOrdered<T, T>((Func<T, T>) (item => item), (IComparer<T>) Comparer<T>.Default, acceptEquals);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool IsOrdered<TSource, TKey>(
    [Intermech.Diagnostics.NotNull] this IEnumerable<TSource> enumerable,
    [Intermech.Diagnostics.NotNull, InstantHandle] Func<TSource, TKey> keySelector,
    bool acceptEquals = false)
  {
    return enumerable.IsOrdered<TSource, TKey>(keySelector, (IComparer<TKey>) Comparer<TKey>.Default, acceptEquals);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool IsOrdered<TSource, TKey>(
    [Intermech.Diagnostics.NotNull] this IEnumerable<TSource> enumerable,
    [Intermech.Diagnostics.NotNull, InstantHandle] Func<TSource, TKey> keySelector,
    [Intermech.Diagnostics.NotNull] IComparer<TKey> comparer,
    bool acceptEquals = false)
  {
    using (IEnumerator<TSource> enumerator = enumerable.GetEnumerator())
    {
      if (!enumerator.MoveNext())
        return true;
      TKey x = keySelector(enumerator.Current);
      while (enumerator.MoveNext())
      {
        TKey y = keySelector(enumerator.Current);
        int num = comparer.Compare(x, y);
        if (num > 0 || !acceptEquals && num == 0)
          return false;
        x = y;
      }
      return true;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool IsOrderedByDescending<T>([Intermech.Diagnostics.NotNull] this IEnumerable<T> enumerable, bool acceptEquals = false)
  {
    return enumerable.IsOrdered<T, T>((Func<T, T>) (item => item), (IComparer<T>) Comparer<T>.Default, acceptEquals);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool IsOrderedByDescending<TSource, TKey>(
    [Intermech.Diagnostics.NotNull] this IEnumerable<TSource> enumerable,
    [Intermech.Diagnostics.NotNull, InstantHandle] Func<TSource, TKey> keySelector,
    bool acceptEquals = false)
  {
    return enumerable.IsOrderedByDescending<TSource, TKey>(keySelector, (IComparer<TKey>) Comparer<TKey>.Default, acceptEquals);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool IsOrderedByDescending<TSource, TKey>(
    [Intermech.Diagnostics.NotNull] this IEnumerable<TSource> enumerable,
    [Intermech.Diagnostics.NotNull, InstantHandle] Func<TSource, TKey> keySelector,
    [Intermech.Diagnostics.NotNull] IComparer<TKey> comparer,
    bool acceptEquals = false)
  {
    using (IEnumerator<TSource> enumerator = enumerable.GetEnumerator())
    {
      if (!enumerator.MoveNext())
        return true;
      TKey x = keySelector(enumerator.Current);
      while (enumerator.MoveNext())
      {
        TKey y = keySelector(enumerator.Current);
        int num = comparer.Compare(x, y);
        if (num < 0 || !acceptEquals && num == 0)
          return false;
        x = y;
      }
      return true;
    }
  }

  [ContractAnnotation("returnDefaultIfNotFound:true => CanBeNull; => NotNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TSource FindMax<TSource, TKey>(
    [Intermech.Diagnostics.NotNull] this IEnumerable<TSource> enumerable,
    [Intermech.Diagnostics.NotNull, InstantHandle] Func<TSource, TKey> keySelector,
    bool returnDefaultIfNotFound = true)
  {
    return enumerable.FindMax<TSource, TKey, TSource>(keySelector, (Func<TSource, TSource>) (key => key), (IComparer<TKey>) Comparer<TKey>.Default, returnDefaultIfNotFound);
  }

  [ContractAnnotation("returnDefaultIfNotFound:true => CanBeNull; => NotNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TResult FindMax<TSource, TKey, TResult>(
    [Intermech.Diagnostics.NotNull] this IEnumerable<TSource> enumerable,
    [Intermech.Diagnostics.NotNull, InstantHandle] Func<TSource, TKey> keySelector,
    [Intermech.Diagnostics.NotNull, InstantHandle] Func<TSource, TResult> resultSelector,
    bool returnDefaultIfNotFound = true)
  {
    return enumerable.FindMax<TSource, TKey, TResult>(keySelector, resultSelector, (IComparer<TKey>) Comparer<TKey>.Default, returnDefaultIfNotFound);
  }

  [ContractAnnotation("returnDefaultIfNotFound:true => CanBeNull; => NotNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TSource FindMax<TSource, TKey>(
    [Intermech.Diagnostics.NotNull] this IEnumerable<TSource> enumerable,
    [Intermech.Diagnostics.NotNull, InstantHandle] Func<TSource, TKey> keySelector,
    [Intermech.Diagnostics.NotNull] IComparer<TKey> comparer,
    bool returnDefaultIfNotFound = true)
  {
    return enumerable.FindMax<TSource, TKey, TSource>(keySelector, (Func<TSource, TSource>) (key => key), comparer, returnDefaultIfNotFound);
  }

  [ContractAnnotation("returnDefaultIfNotFound:true => CanBeNull; => NotNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TResult FindMax<TSource, TKey, TResult>(
    [Intermech.Diagnostics.NotNull] this IEnumerable<TSource> enumerable,
    [Intermech.Diagnostics.NotNull, InstantHandle] Func<TSource, TKey> keySelector,
    [Intermech.Diagnostics.NotNull, InstantHandle] Func<TSource, TResult> resultSelector,
    [Intermech.Diagnostics.NotNull] IComparer<TKey> comparer,
    bool returnDefaultIfNotFound = true)
  {
    using (IEnumerator<TSource> enumerator = enumerable.GetEnumerator())
    {
      if (enumerator.MoveNext())
      {
        TSource source = enumerator.Current;
        TKey y = keySelector(source);
        while (enumerator.MoveNext())
        {
          TSource current = enumerator.Current;
          TKey x = keySelector(current);
          if (comparer.Compare(x, y) > 0)
          {
            y = x;
            source = current;
          }
        }
        return resultSelector(source);
      }
    }
    if (returnDefaultIfNotFound)
      return default (TResult);
    throw new Exception("Enumeration is empty");
  }

  [ContractAnnotation("returnDefaultIfNotFound:true => CanBeNull; => NotNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TSource FindMin<TSource, TKey>(
    [Intermech.Diagnostics.NotNull] this IEnumerable<TSource> enumerable,
    [Intermech.Diagnostics.NotNull, InstantHandle] Func<TSource, TKey> keySelector,
    bool returnDefaultIfNotFound = true)
  {
    return enumerable.FindMin<TSource, TKey, TSource>(keySelector, (Func<TSource, TSource>) (key => key), (IComparer<TKey>) Comparer<TKey>.Default, returnDefaultIfNotFound);
  }

  [ContractAnnotation("returnDefaultIfNotFound:true => CanBeNull; => NotNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TResult FindMin<TSource, TKey, TResult>(
    [Intermech.Diagnostics.NotNull] this IEnumerable<TSource> enumerable,
    [Intermech.Diagnostics.NotNull, InstantHandle] Func<TSource, TKey> keySelector,
    [Intermech.Diagnostics.NotNull, InstantHandle] Func<TSource, TResult> resultSelector,
    bool returnDefaultIfNotFound = true)
  {
    return enumerable.FindMin<TSource, TKey, TResult>(keySelector, resultSelector, (IComparer<TKey>) Comparer<TKey>.Default, returnDefaultIfNotFound);
  }

  [ContractAnnotation("returnDefaultIfNotFound:true => CanBeNull; => NotNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TSource FindMin<TSource, TKey>(
    [Intermech.Diagnostics.NotNull] this IEnumerable<TSource> enumerable,
    [Intermech.Diagnostics.NotNull, InstantHandle] Func<TSource, TKey> keySelector,
    [Intermech.Diagnostics.NotNull] IComparer<TKey> comparer,
    bool returnDefaultIfNotFound = true)
  {
    return enumerable.FindMin<TSource, TKey, TSource>(keySelector, (Func<TSource, TSource>) (key => key), comparer, returnDefaultIfNotFound);
  }

  [ContractAnnotation("returnDefaultIfNotFound:true => CanBeNull; => NotNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TResult FindMin<TSource, TKey, TResult>(
    [Intermech.Diagnostics.NotNull] this IEnumerable<TSource> enumerable,
    [Intermech.Diagnostics.NotNull, InstantHandle] Func<TSource, TKey> keySelector,
    [Intermech.Diagnostics.NotNull, InstantHandle] Func<TSource, TResult> resultSelector,
    [Intermech.Diagnostics.NotNull] IComparer<TKey> comparer,
    bool returnDefaultIfNotFound = true)
  {
    using (IEnumerator<TSource> enumerator = enumerable.GetEnumerator())
    {
      if (enumerator.MoveNext())
      {
        TSource source = enumerator.Current;
        TKey y = keySelector(source);
        while (enumerator.MoveNext())
        {
          TSource current = enumerator.Current;
          TKey x = keySelector(current);
          if (comparer.Compare(x, y) < 0)
          {
            y = x;
            source = current;
          }
        }
        return resultSelector(source);
      }
    }
    if (returnDefaultIfNotFound)
      return default (TResult);
    throw new Exception("Enumeration is empty");
  }

  [Intermech.Diagnostics.NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<T> Except<T>([Intermech.Diagnostics.NotNull] this IEnumerable<T> enumerable, [CanBeNull] T exceptItem)
  {
    return enumerable.Where<T>((Func<T, bool>) (item => !object.Equals((object) item, (object) (T) exceptItem))).WrapWithCountOrCapacity<T>((IEnumerable) enumerable, false);
  }

  [Intermech.Diagnostics.NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<T> OfTypes<T>([Intermech.Diagnostics.NotNull] this IEnumerable<T> enumerable, [Intermech.Diagnostics.NotNull, ItemNotNull] ICollection<Type> types)
  {
    if (types.Count == 0)
      return (IEnumerable<T>) Array.Empty<T>();
    return typeof (T).IsValueType ? enumerable.Where<T>((Func<T, bool>) (sourceElement => types.Any<Type>((Func<Type, bool>) (type => sourceElement.GetType().IsAssignableFrom(type))))).WrapWithCountOrCapacity<T>((IEnumerable) enumerable, false) : enumerable.Where<T>((Func<T, bool>) (sourceElement => (object) sourceElement != null && types.Any<Type>((Func<Type, bool>) (type => sourceElement.GetType().IsAssignableFrom(type))))).WrapWithCountOrCapacity<T>((IEnumerable) enumerable, false);
  }

  [Intermech.Diagnostics.NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<T> ExceptTypes<T>(
    [Intermech.Diagnostics.NotNull] this IEnumerable<T> enumerable,
    [Intermech.Diagnostics.NotNull, ItemNotNull] ICollection<Type> types,
    bool checkSubTypes = true)
  {
    if (types.Count == 0)
      return enumerable;
    return typeof (T).IsValueType ? (!checkSubTypes ? enumerable.Where<T>((Func<T, bool>) (sourceElement => types.Contains(sourceElement.GetType()))).WrapWithCountOrCapacity<T>((IEnumerable) enumerable, false) : enumerable.Where<T>((Func<T, bool>) (sourceElement => types.Any<Type>((Func<Type, bool>) (type =>
    {
      Type type1 = sourceElement.GetType();
      return type1 == type || type1.IsSubclassOf(type);
    })))).WrapWithCountOrCapacity<T>((IEnumerable) enumerable, false)) : (!checkSubTypes ? enumerable.Where<T>((Func<T, bool>) (sourceElement => (object) sourceElement == null || types.Contains(sourceElement.GetType()))).WrapWithCountOrCapacity<T>((IEnumerable) enumerable, false) : enumerable.Where<T>((Func<T, bool>) (sourceElement => (object) sourceElement == null || types.Any<Type>((Func<Type, bool>) (type =>
    {
      Type type2 = sourceElement.GetType();
      return type2 == type || type2.IsSubclassOf(type);
    })))).WrapWithCountOrCapacity<T>((IEnumerable) enumerable, false));
  }

  [Intermech.Diagnostics.NotNull]
  [LinqTunnel]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<T> SelectFromSecond<T>(
    [Intermech.Diagnostics.NotNull, NotEmpty] this IEnumerable<T> enumerable,
    [CanBeNull] out T firstElement)
  {
    firstElement = enumerable.First<T>();
    return enumerable.Skip<T>(1);
  }

  [ContractAnnotation("firstSourceEnumeration:null => false; secondSourceEnumeration:null => false")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool All<T1, T2>(
    [CanBeNull] this IEnumerable<T1> firstSourceEnumeration,
    [CanBeNull] IEnumerable<T2> secondSourceEnumeration,
    [Intermech.Diagnostics.NotNull, InstantHandle] Func<T1, T2, bool> predicate)
  {
    if (firstSourceEnumeration == null && secondSourceEnumeration != null || firstSourceEnumeration != null && secondSourceEnumeration == null)
      return false;
    if (firstSourceEnumeration != null)
    {
      if (firstSourceEnumeration is ICollection collection1 && secondSourceEnumeration is ICollection collection2 && collection1.Count != collection2.Count)
        return false;
      if (((ICollection) firstSourceEnumeration).Count > 0)
      {
        using (IEnumerator<T2> secondSourceEnumerator = secondSourceEnumeration.GetEnumerator())
        {
          if (firstSourceEnumeration.Any<T1>((Func<T1, bool>) (firstEnumerationElement => !secondSourceEnumerator.MoveNext() || !predicate(firstEnumerationElement, secondSourceEnumerator.Current))))
            return false;
        }
      }
    }
    return true;
  }

  [Intermech.Diagnostics.NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<Decimal> Abs([Intermech.Diagnostics.NotNull] this IEnumerable<Decimal> enumeration)
  {
    int result;
    if (!enumeration.TryGetCount<Decimal>(out result))
      return enumeration.Select<Decimal, Decimal>(new Func<Decimal, Decimal>(Math.Abs));
    return result == 0 ? (IEnumerable<Decimal>) Array.Empty<Decimal>() : (IEnumerable<Decimal>) enumeration.Select<Decimal, Decimal>(new Func<Decimal, Decimal>(Math.Abs)).WrapWithCount<Decimal>(result);
  }

  [Intermech.Diagnostics.NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<double> Abs([Intermech.Diagnostics.NotNull] this IEnumerable<double> enumeration)
  {
    int result;
    if (!enumeration.TryGetCount<double>(out result))
      return enumeration.Select<double, double>(new Func<double, double>(Math.Abs));
    return result == 0 ? (IEnumerable<double>) Array.Empty<double>() : (IEnumerable<double>) enumeration.Select<double, double>(new Func<double, double>(Math.Abs)).WrapWithCount<double>(result);
  }

  [Intermech.Diagnostics.NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<float> Abs([Intermech.Diagnostics.NotNull] this IEnumerable<float> enumeration)
  {
    int result;
    if (!enumeration.TryGetCount<float>(out result))
      return enumeration.Select<float, float>(new Func<float, float>(Math.Abs));
    return result == 0 ? (IEnumerable<float>) Array.Empty<float>() : (IEnumerable<float>) enumeration.Select<float, float>(new Func<float, float>(Math.Abs)).WrapWithCount<float>(result);
  }

  [Intermech.Diagnostics.NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<int> Abs([Intermech.Diagnostics.NotNull] this IEnumerable<int> enumeration)
  {
    int result;
    if (!enumeration.TryGetCount<int>(out result))
      return enumeration.Select<int, int>(new Func<int, int>(Math.Abs));
    return result == 0 ? (IEnumerable<int>) Array.Empty<int>() : (IEnumerable<int>) enumeration.Select<int, int>(new Func<int, int>(Math.Abs)).WrapWithCount<int>(result);
  }

  [Intermech.Diagnostics.NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<long> Abs([Intermech.Diagnostics.NotNull] this IEnumerable<long> enumeration)
  {
    int result;
    if (!enumeration.TryGetCount<long>(out result))
      return enumeration.Select<long, long>(new Func<long, long>(Math.Abs));
    return result == 0 ? (IEnumerable<long>) Array.Empty<long>() : (IEnumerable<long>) enumeration.Select<long, long>(new Func<long, long>(Math.Abs)).WrapWithCount<long>(result);
  }

  [Intermech.Diagnostics.NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<short> Abs([Intermech.Diagnostics.NotNull] this IEnumerable<short> enumeration)
  {
    int result;
    if (!enumeration.TryGetCount<short>(out result))
      return enumeration.Select<short, short>(new Func<short, short>(Math.Abs));
    return result == 0 ? (IEnumerable<short>) Array.Empty<short>() : (IEnumerable<short>) enumeration.Select<short, short>(new Func<short, short>(Math.Abs)).WrapWithCount<short>(result);
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T? FirstOrNull<T>([Intermech.Diagnostics.NotNull] this IEnumerable<T> enumeration) where T : struct
  {
    using (IEnumerator<T> enumerator = enumeration.GetEnumerator())
    {
      if (enumerator.MoveNext())
        return new T?(enumerator.Current);
    }
    return new T?();
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T? FirstOrNull<T>([Intermech.Diagnostics.NotNull] this IEnumerable<T> enumeration, [Intermech.Diagnostics.NotNull, InstantHandle] Func<T, bool> predicate) where T : struct
  {
    using (IEnumerator<T> enumerator = enumeration.GetEnumerator())
    {
      if (enumerator.MoveNext())
      {
        T current = enumerator.Current;
        if (predicate(current))
          return new T?(current);
      }
    }
    return new T?();
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetFirst<T>([Intermech.Diagnostics.NotNull] this IEnumerable<T> enumeration, [CanBeNull] out T result)
  {
    using (IEnumerator<T> enumerator = enumeration.GetEnumerator())
    {
      if (enumerator.MoveNext())
      {
        result = enumerator.Current;
        return true;
      }
    }
    result = default (T);
    return false;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetIndex<T>([Intermech.Diagnostics.NotNull] this IEnumerable<T> enumeration, [CanBeNull] T value, out int index)
  {
    index = -1;
    using (IEnumerator<T> enumerator = enumeration.GetEnumerator())
    {
      if (enumerator.MoveNext())
      {
        ++index;
        if (object.Equals((object) enumerator.Current, (object) value))
          return true;
      }
    }
    index = -1;
    return false;
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetFirst<T>(
    [Intermech.Diagnostics.NotNull] this IEnumerable<T> enumeration,
    [Intermech.Diagnostics.NotNull, InstantHandle] Func<T, bool> predicate,
    [CanBeNull] out T result)
  {
    using (IEnumerator<T> enumerator = enumeration.GetEnumerator())
    {
      if (enumerator.MoveNext())
      {
        result = enumerator.Current;
        if (predicate(result))
          return true;
      }
    }
    result = default (T);
    return false;
  }

  [ContractAnnotation("defaultValue:null => CanBeNull; => NotNull")]
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T FirstOrDefault<T>([Intermech.Diagnostics.NotNull] this IEnumerable<T> enumeration, T defaultValue = null)
  {
    using (IEnumerator<T> enumerator = enumeration.GetEnumerator())
    {
      if (enumerator.MoveNext())
        return enumerator.Current;
    }
    return defaultValue;
  }

  [Pure]
  [ContractAnnotation("defaultValue:null => CanBeNull; => NotNull")]
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T FirstOrDefault<T>(
    [Intermech.Diagnostics.NotNull] this IEnumerable<T> enumeration,
    [Intermech.Diagnostics.NotNull, InstantHandle] Func<T, bool> predicate,
    T defaultValue)
  {
    foreach (T obj in enumeration)
    {
      if (predicate(obj))
        return obj;
    }
    return defaultValue;
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T FirstOrDefault<T>(
    [Intermech.Diagnostics.NotNull] this IEnumerable<T> enumeration,
    [Intermech.Diagnostics.NotNull, InstantHandle] Func<T, bool> predicate,
    [Intermech.Diagnostics.NotNull] Func<T> getDefaultValue)
  {
    foreach (T obj in enumeration)
    {
      if (predicate(obj))
        return obj;
    }
    return getDefaultValue();
  }

  [ContractAnnotation("defaultValue:null => CanBeNull; => NotNull")]
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T FirstOrAction<T>([Intermech.Diagnostics.NotNull] this IEnumerable<T> enumeration, [Intermech.Diagnostics.NotNull, InstantHandle] Action action, T defaultValue = null)
  {
    using (IEnumerator<T> enumerator = enumeration.GetEnumerator())
    {
      if (enumerator.MoveNext())
        return enumerator.Current;
    }
    action();
    return defaultValue;
  }

  [ContractAnnotation("defaultValue:null => CanBeNull; => NotNull")]
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T FirstOrAction<T>(
    [Intermech.Diagnostics.NotNull] this IEnumerable<T> enumeration,
    [Intermech.Diagnostics.NotNull, InstantHandle] Func<T, bool> predicate,
    [Intermech.Diagnostics.NotNull, InstantHandle] Action action,
    T defaultValue = null)
  {
    foreach (T obj in enumeration)
    {
      if (predicate(obj))
        return obj;
    }
    action();
    return defaultValue;
  }

  [Intermech.Diagnostics.NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<T> GetCollection<T>([Intermech.Diagnostics.NotNull] this IEnumerable<T> enumeration, int count = 16 /*0x10*/)
  {
    return enumeration is IReadOnlyCollection<T> objs ? objs : (IReadOnlyCollection<T>) enumeration.ToList<T>(enumeration.TryGetCount<T>() ?? 16 /*0x10*/);
  }

  [Intermech.Diagnostics.NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<T> AsReadOnlyCollection<T>(
    [Intermech.Diagnostics.NotNull] this IEnumerable<T> enumeration,
    int capacity = 0)
  {
    if (enumeration is IReadOnlyCollection<T> objs)
      return objs;
    return capacity == 0 ? enumeration.WrapWithCount<T>(enumeration.TryGetCount<T>() ?? 0) : enumeration.WrapWithCount<T>(capacity);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool AllEqualObjects<T>([Intermech.Diagnostics.NotNull] this IEnumerable<T> enumeration) where T : class
  {
    using (IEnumerator<T> enumerator = enumeration.GetEnumerator())
    {
      if (enumerator.MoveNext())
      {
        T current1 = enumerator.Current;
        while (enumerator.MoveNext())
        {
          T current2 = enumerator.Current;
          if ((object) current1 != (object) current2 || (object) current1 == null || (object) current2 == null || !current1.Equals((object) current2))
            return false;
        }
      }
    }
    return true;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool AllEqualStructs<T>([Intermech.Diagnostics.NotNull] this IEnumerable<T> enumeration) where T : struct
  {
    using (IEnumerator<T> enumerator = enumeration.GetEnumerator())
    {
      if (enumerator.MoveNext())
      {
        T current = enumerator.Current;
        while (enumerator.MoveNext())
        {
          if (!current.Equals((object) enumerator.Current))
            return false;
        }
      }
    }
    return true;
  }

  [Intermech.Diagnostics.NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<T> WrapWithCount<T>([Intermech.Diagnostics.NotNull] this IEnumerable<T> enumeration, int count)
  {
    if (count == 0)
      return (IReadOnlyCollection<T>) Array.Empty<T>();
    return enumeration is IReadOnlyCollection<T> objs ? objs : (IReadOnlyCollection<T>) new EnumerationList<T>(enumeration, count);
  }

  [Intermech.Diagnostics.NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<T> WrapWithCount<T>(
    [Intermech.Diagnostics.NotNull] this IEnumerable<T> enumeration,
    [Intermech.Diagnostics.NotNull, NoEnumeration] IEnumerable countSource)
  {
    int result;
    if (enumeration.TryGetCount<T>(out int _) || !countSource.TryGetCount(out result))
      return enumeration;
    return result == 0 ? (IEnumerable<T>) Array.Empty<T>() : (IEnumerable<T>) new EnumerationList<T>(enumeration, result);
  }

  [Intermech.Diagnostics.NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<T> WrapWithCapacity<T>([Intermech.Diagnostics.NotNull] this IEnumerable<T> enumeration, int capacity)
  {
    return (IEnumerable<T>) new EnumerationCapacityWrapper<T>(enumeration, capacity);
  }

  [Intermech.Diagnostics.NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<T> WrapWithCountOrCapacity<T>(
    [Intermech.Diagnostics.NotNull] this IEnumerable<T> enumeration,
    [Intermech.Diagnostics.NotNull, NoEnumeration] IEnumerable capacitySource,
    bool checkCount = true)
  {
    int result1;
    if (enumeration.TryGetCount<T>(out result1) || enumeration.TryGetCapacity<T>(out result1))
      return enumeration;
    int result2;
    if (checkCount && capacitySource.TryGetCount(out result2))
      return result2 == 0 ? (IEnumerable<T>) Array.Empty<T>() : (IEnumerable<T>) new EnumerationList<T>(enumeration, result2);
    int result3;
    if ((!checkCount || !capacitySource.TryGetCapacity(out result3)) && (checkCount || !capacitySource.TryGetCountOrCapacity(out result3)))
      return enumeration;
    return result3 == 0 ? (IEnumerable<T>) Array.Empty<T>() : (IEnumerable<T>) new EnumerationCapacityWrapper<T>(enumeration, result3);
  }

  [Intermech.Diagnostics.NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IList<T> WrapAsLazyList<T>([Intermech.Diagnostics.NotNull] this IEnumerable<T> enumeration, int count)
  {
    return (IList<T>) new EnumerationList<T>(enumeration, count);
  }

  [Intermech.Diagnostics.NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IList<T> WrapWithCount<T>([Intermech.Diagnostics.NotNull] this IEnumerable<T> enumeration)
  {
    return (IList<T>) new EnumerationList<T>(enumeration);
  }

  [Intermech.Diagnostics.NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ListWithCast<T, TMapped> ToListWithCast<T, TMapped>(
    [Intermech.Diagnostics.NotNull] this IEnumerable<T> enumeration,
    int capacity = 16 /*0x10*/)
    where T : TMapped
  {
    return new ListWithCast<T, TMapped>(enumeration, capacity);
  }

  [Intermech.Diagnostics.NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ReadOnlyListWithCast<T, TMapped> ToReadOnlyListWithCast<T, TMapped>(
    [Intermech.Diagnostics.NotNull] this IEnumerable<T> enumeration,
    int capacity = 16 /*0x10*/)
    where T : TMapped
  {
    return new ReadOnlyListWithCast<T, TMapped>(enumeration, capacity);
  }

  [Intermech.Diagnostics.NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ListWithMapping<T, TMapped> ToListWithMapping<T, TMapped>(
    [Intermech.Diagnostics.NotNull] this IEnumerable<T> enumeration,
    [Intermech.Diagnostics.NotNull] Func<T, TMapped> selector,
    int capacity = 16 /*0x10*/)
  {
    return new ListWithMapping<T, TMapped>(enumeration, selector, capacity);
  }

  [Intermech.Diagnostics.NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ReadOnlyListWithMapping<T, TMapped> ToReadOnlyListWithMapping<T, TMapped>(
    [Intermech.Diagnostics.NotNull] this IEnumerable<T> enumeration,
    [Intermech.Diagnostics.NotNull] Func<T, TMapped> selector,
    int capacity = 16 /*0x10*/)
  {
    return new ReadOnlyListWithMapping<T, TMapped>(enumeration, selector, capacity);
  }

  [Intermech.Diagnostics.NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DictionaryWithCast<TKey, TValue, TMappedValue> ToDictionaryWithCast<TKey, TValue, TMappedValue>(
    [Intermech.Diagnostics.NotNull] this IEnumerable<TValue> enumeration,
    [Intermech.Diagnostics.NotNull] Func<TValue, TKey> keySelector,
    [CanBeNull] IEqualityComparer<TKey> comparer = null)
    where TValue : TMappedValue
  {
    return new DictionaryWithCast<TKey, TValue, TMappedValue>(enumeration.Select<TValue, KeyValuePair<TKey, TValue>>((Func<TValue, KeyValuePair<TKey, TValue>>) (element => new KeyValuePair<TKey, TValue>(keySelector(element), element))), comparer);
  }

  [Intermech.Diagnostics.NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ReadOnlyDictionaryWithCast<TKey, TValue, TMappedValue> ToReadOnlyDictionaryWithCast<TKey, TValue, TMappedValue>(
    [Intermech.Diagnostics.NotNull] this IEnumerable<TValue> enumeration,
    [Intermech.Diagnostics.NotNull] Func<TValue, TKey> keySelector,
    [CanBeNull] IEqualityComparer<TKey> comparer = null)
    where TValue : TMappedValue
  {
    return new ReadOnlyDictionaryWithCast<TKey, TValue, TMappedValue>(enumeration.Select<TValue, KeyValuePair<TKey, TValue>>((Func<TValue, KeyValuePair<TKey, TValue>>) (element => new KeyValuePair<TKey, TValue>(keySelector(element), element))), comparer);
  }

  [Intermech.Diagnostics.NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DictionaryWithMapping<TKey, TValue, TMappedValue> ToDictionaryWithMapping<TKey, TValue, TMappedValue>(
    [Intermech.Diagnostics.NotNull] this IEnumerable<TValue> enumeration,
    [Intermech.Diagnostics.NotNull] Func<TValue, TKey> keySelector,
    [Intermech.Diagnostics.NotNull] Func<TValue, TMappedValue> selector,
    [CanBeNull] IEqualityComparer<TKey> comparer = null)
  {
    return new DictionaryWithMapping<TKey, TValue, TMappedValue>(enumeration.Select<TValue, KeyValuePair<TKey, TValue>>((Func<TValue, KeyValuePair<TKey, TValue>>) (element => new KeyValuePair<TKey, TValue>(keySelector(element), element))), selector, comparer);
  }

  [Intermech.Diagnostics.NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ReadOnlyDictionaryWithMapping<TKey, TValue, TMappedValue> ToReadOnlyDictionaryWithMapping<TKey, TValue, TMappedValue>(
    [Intermech.Diagnostics.NotNull] this IEnumerable<TValue> enumeration,
    [Intermech.Diagnostics.NotNull] Func<TValue, TKey> keySelector,
    [Intermech.Diagnostics.NotNull] Func<TValue, TMappedValue> selector,
    [CanBeNull] IEqualityComparer<TKey> comparer = null)
  {
    return new ReadOnlyDictionaryWithMapping<TKey, TValue, TMappedValue>(enumeration.Select<TValue, KeyValuePair<TKey, TValue>>((Func<TValue, KeyValuePair<TKey, TValue>>) (element => new KeyValuePair<TKey, TValue>(keySelector(element), element))), selector, comparer);
  }

  [Intermech.Diagnostics.NotNull]
  [LinqTunnel]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerableWithCapacity<TOutput> Map<TSource, TOutput>(
    [Intermech.Diagnostics.NotNull, NoEnumeration] this IEnumerable<TSource> enumeration,
    [Intermech.Diagnostics.NotNull] Func<TSource, TOutput> selector)
  {
    return (IEnumerableWithCapacity<TOutput>) new EnumerationMapAdapter<TSource, TOutput>(enumeration, selector);
  }

  [Intermech.Diagnostics.NotNull]
  [LinqTunnel]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerableWithCapacity<TOutput> MapNotNull<TSource, TOutput>(
    [Intermech.Diagnostics.NotNull, NoEnumeration] this IEnumerable<TSource> enumeration,
    [Intermech.Diagnostics.NotNull] Func<TSource, TOutput> selector)
    where TOutput : class
  {
    return (IEnumerableWithCapacity<TOutput>) new EnumerationFilterAdapter<TOutput>((IEnumerable<TOutput>) new EnumerationMapAdapter<TSource, TOutput>(enumeration, selector), (Func<TOutput, bool>) (result => (object) result != null));
  }

  [Intermech.Diagnostics.NotNull]
  [LinqTunnel]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerableWithCapacity<TOutput> CastWithCapacity<TSource, TOutput>(
    [Intermech.Diagnostics.NotNull, NoEnumeration] this IEnumerable<TSource> enumeration)
    where TSource : TOutput
  {
    return (IEnumerableWithCapacity<TOutput>) new EnumerationCastAdapter<TSource, TOutput>(enumeration);
  }

  [Intermech.Diagnostics.NotNull]
  [LinqTunnel]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerableWithCapacity<T> Filter<T>(
    [Intermech.Diagnostics.NotNull, NoEnumeration] this IEnumerable<T> enumeration,
    [Intermech.Diagnostics.NotNull] Func<T, bool> predicate)
  {
    return (IEnumerableWithCapacity<T>) new EnumerationFilterAdapter<T>(enumeration, predicate);
  }

  [Intermech.Diagnostics.NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerableWithCapacity<T> DistinctWithCapacity<T>([Intermech.Diagnostics.NotNull] this IEnumerable<T> enumeration)
  {
    return (IEnumerableWithCapacity<T>) new EnumerationOperationAdapter<T>(enumeration, new Func<IEnumerable<T>, IEnumerable<T>>(Enumerable.Distinct<T>));
  }

  [Pure]
  [Intermech.Diagnostics.NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyList<T> Expand<T>(
    [Intermech.Diagnostics.NotNull] this IEnumerable<T> source,
    [Intermech.Diagnostics.NotNull] Enumeration.ExpandFuncDelegate<T> expandFunc)
  {
    int? count = source.TryGetCount<T>();
    int? nullable = count;
    int num = 0;
    if (nullable.GetValueOrDefault() == num & nullable.HasValue)
      return (IReadOnlyList<T>) Array.Empty<T>();
    List<T> objList = count.HasValue ? new List<T>(Math.Max(count.Value * 2, 16 /*0x10*/)) : new List<T>(16 /*0x10*/);
    objList.AddRange(source);
    for (int index = 0; index < objList.Count; ++index)
    {
      IEnumerable<T> collection = expandFunc(objList[index]);
      if (collection != null)
        objList.AddRange(collection);
    }
    return (IReadOnlyList<T>) objList;
  }

  [Pure]
  [Intermech.Diagnostics.NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<T> ExpandSafe<T>(
    [Intermech.Diagnostics.NotNull] this IEnumerable<T> source,
    [Intermech.Diagnostics.NotNull] Enumeration.ExpandFuncDelegate<T> expandFunc,
    int capacity = 0)
  {
    int? count = source.TryGetCount<T>();
    int? nullable = count;
    int num = 0;
    if (nullable.GetValueOrDefault() == num & nullable.HasValue)
      return (IReadOnlyCollection<T>) Array.Empty<T>();
    HashSet<T> objSet = count.HasValue ? new HashSet<T>(Math.Max(count.Value * 2, Math.Max(capacity, 16 /*0x10*/))) : new HashSet<T>(Math.Max(capacity, 16 /*0x10*/));
    Queue<T> objQueue = new Queue<T>();
    foreach (T obj in source)
    {
      objSet.Add(obj);
      objQueue.Enqueue(obj);
    }
    while (objQueue.Count > 0)
    {
      T obj1 = objQueue.Dequeue();
      IEnumerable<T> objs = expandFunc(obj1);
      if (objs != null)
      {
        foreach (T obj2 in objs)
        {
          if (!objSet.Contains(obj2))
          {
            objSet.Add(obj2);
            objQueue.Enqueue(obj2);
          }
        }
      }
    }
    return (IReadOnlyCollection<T>) objSet;
  }

  [Intermech.Diagnostics.NotNull]
  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T[] AsArrayOf<T>([Intermech.Diagnostics.NotNull] this IEnumerable enumeration, [CanBeNull] IFormatProvider formatProvider = null)
  {
    if (enumeration is T[] objArray)
      return objArray;
    int result;
    if (!enumeration.TryGetCount(out result))
      return enumeration.Cast<T>().ToArray<T>();
    return result == 0 ? Array.Empty<T>() : enumeration.Cast<T>().AsArray<T>(result);
  }

  [Intermech.Diagnostics.NotNull]
  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<long> ConvertAll2Long(
    [Intermech.Diagnostics.NotNull] this IEnumerable enumeration,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    if (enumeration is IEnumerable<long> longs)
      return longs;
    int result;
    return enumeration.TryGetCount(out result) && result == 0 ? (IEnumerable<long>) Array.Empty<long>() : enumeration.Cast<object>().Where<object>((Func<object, bool>) (val => val != null && !(val is DBNull))).Select<object, long>((Func<object, long>) (val => Convert.ToInt64(val, formatProvider))).WrapWithCountOrCapacity<long>(enumeration, false);
  }

  [Intermech.Diagnostics.NotNull]
  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<int> ConvertAll2Int(
    [Intermech.Diagnostics.NotNull] this IEnumerable enumeration,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    if (enumeration is IEnumerable<int> ints)
      return ints;
    int result;
    return enumeration.TryGetCount(out result) && result == 0 ? (IEnumerable<int>) Array.Empty<int>() : enumeration.Cast<object>().Where<object>((Func<object, bool>) (val => val != null && !(val is DBNull))).Select<object, int>((Func<object, int>) (val => Convert.ToInt32(val, formatProvider))).WrapWithCountOrCapacity<int>(enumeration, false);
  }

  [Intermech.Diagnostics.NotNull]
  [ItemNotNull]
  [ItemCanBeEmpty]
  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<string> ConvertAll2String(
    [Intermech.Diagnostics.NotNull] this IEnumerable enumeration,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    int result;
    return enumeration.TryGetCount(out result) && result == 0 ? (IEnumerable<string>) Array.Empty<string>() : (enumeration is IEnumerable<string> source ? source.Select<string, string>((Func<string, string>) (str => str ?? string.Empty)) : (IEnumerable<string>) null) ?? enumeration.Cast<object>().Where<object>((Func<object, bool>) (val => val != null && !(val is DBNull))).Select<object, string>((Func<object, string>) (val => Convert.ToString(val, formatProvider))).WrapWithCountOrCapacity<string>(enumeration, false);
  }

  [Intermech.Diagnostics.NotNull]
  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<bool> ConvertAll2Bool(
    [Intermech.Diagnostics.NotNull] this IEnumerable enumeration,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    if (enumeration is IEnumerable<bool> bools)
      return bools;
    int result;
    return enumeration.TryGetCount(out result) && result == 0 ? (IEnumerable<bool>) Array.Empty<bool>() : enumeration.Cast<object>().Where<object>((Func<object, bool>) (val => val != null && !(val is DBNull))).Select<object, bool>((Func<object, bool>) (val => Convert.ToBoolean(val, formatProvider))).WrapWithCountOrCapacity<bool>(enumeration, false);
  }

  [Intermech.Diagnostics.NotNull]
  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<double> ConvertAll2Double(
    [Intermech.Diagnostics.NotNull] this IEnumerable enumeration,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    if (enumeration is IEnumerable<double> doubles)
      return doubles;
    int result;
    return enumeration.TryGetCount(out result) && result == 0 ? (IEnumerable<double>) Array.Empty<double>() : enumeration.Cast<object>().Where<object>((Func<object, bool>) (val => val != null && !(val is DBNull))).Select<object, double>((Func<object, double>) (val => Convert.ToDouble(val, formatProvider))).WrapWithCountOrCapacity<double>(enumeration, false);
  }

  [Intermech.Diagnostics.NotNull]
  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<DateTime> ConvertAll2DateTime(
    [Intermech.Diagnostics.NotNull] this IEnumerable enumeration,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    if (enumeration is IEnumerable<DateTime> dateTimes)
      return dateTimes;
    int result;
    return enumeration.TryGetCount(out result) && result == 0 ? (IEnumerable<DateTime>) Array.Empty<DateTime>() : enumeration.Cast<object>().Where<object>((Func<object, bool>) (val => val != null && !(val is DBNull))).Select<object, DateTime>((Func<object, DateTime>) (val => Convert.ToDateTime(val, formatProvider))).WrapWithCountOrCapacity<DateTime>(enumeration, false);
  }

  [Intermech.Diagnostics.NotNull]
  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<T> ConvertAll<T>(
    [Intermech.Diagnostics.NotNull] this IEnumerable enumeration,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    if (enumeration is IEnumerable<T> objs)
      return objs;
    int result;
    return enumeration.TryGetCount(out result) && result == 0 ? (IEnumerable<T>) Array.Empty<T>() : Enumeration._ConvertAll<T>(enumeration, formatProvider).WrapWithCountOrCapacity<T>(enumeration, false);
  }

  [Intermech.Diagnostics.NotNull]
  [ItemCanBeNull]
  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static IEnumerable<T> _ConvertAll<T>(
    [Intermech.Diagnostics.NotNull] IEnumerable enumeration,
    [CanBeNull] IFormatProvider formatProvider = null)
  {
    Type type = typeof (T);
    foreach (object obj in enumeration)
    {
      switch (obj)
      {
        case null:
        case DBNull _:
          yield return default (T);
          continue;
        default:
          yield return (T) Convert.ChangeType(obj, type, formatProvider);
          continue;
      }
    }
  }

  [Intermech.Diagnostics.NotNull]
  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyList<TOutput> ConvertAll<T, TOutput>(
    [Intermech.Diagnostics.NotNull] this IEnumerable<T> enumeration,
    [Intermech.Diagnostics.NotNull] Converter<T, TOutput> converter)
  {
    if (enumeration is IReadOnlyList<TOutput> outputList1)
      return outputList1;
    int result;
    if (enumeration.TryGetCount<T>(out result) && result == 0)
      return (IReadOnlyList<TOutput>) Array.Empty<TOutput>();
    List<TOutput> outputList2;
    switch (enumeration)
    {
      case T[] array:
        return (IReadOnlyList<TOutput>) Array.ConvertAll<T, TOutput>(array, converter);
      case List<T> objList:
        outputList2 = objList.ConvertAll<TOutput>(converter);
        break;
      default:
        outputList2 = (List<TOutput>) null;
        break;
    }
    if (outputList2 == null)
      outputList2 = Enumeration._ConvertAll<T, TOutput>(enumeration, converter);
    return (IReadOnlyList<TOutput>) outputList2;
  }

  [Intermech.Diagnostics.NotNull]
  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static List<TOutput> _ConvertAll<T, TOutput>(
    [Intermech.Diagnostics.NotNull] IEnumerable<T> enumeration,
    [Intermech.Diagnostics.NotNull] Converter<T, TOutput> converter)
  {
    List<TOutput> outputList = new List<TOutput>(enumeration is IReadOnlyCollection<object> objects ? objects.Count : 16 /*0x10*/);
    foreach (T input in enumeration)
      outputList.Add(converter(input));
    return outputList;
  }

  [Intermech.Diagnostics.NotNull]
  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyList<T> ConvertAll<T>(
    [Intermech.Diagnostics.NotNull] this IEnumerable enumeration,
    [Intermech.Diagnostics.NotNull] Converter<object, T> converter)
  {
    List<T> objList1;
    switch (enumeration)
    {
      case IReadOnlyList<T> objList2:
        return objList2;
      case object[] array:
        return (IReadOnlyList<T>) Array.ConvertAll<object, T>(array, converter);
      case List<object> objectList:
        objList1 = objectList.ConvertAll<T>(converter);
        break;
      default:
        objList1 = (List<T>) null;
        break;
    }
    if (objList1 == null)
      objList1 = Enumeration._ConvertAll<T>(enumeration, converter);
    return (IReadOnlyList<T>) objList1;
  }

  [Intermech.Diagnostics.NotNull]
  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static List<T> _ConvertAll<T>([Intermech.Diagnostics.NotNull] IEnumerable enumeration, [Intermech.Diagnostics.NotNull] Converter<object, T> converter)
  {
    List<T> objList = new List<T>(enumeration is IReadOnlyCollection<object> objects ? objects.Count : 16 /*0x10*/);
    foreach (T input in enumeration)
      objList.Add(converter((object) input));
    return objList;
  }

  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T Min<T>([Intermech.Diagnostics.NotNull] this IEnumerable<T> enumeration, T defaultValue) where T : struct, IComparable<T>
  {
    using (IEnumerator<T> enumerator = enumeration.GetEnumerator())
    {
      if (!enumerator.MoveNext())
        return defaultValue;
      T other = enumerator.Current;
      while (enumerator.MoveNext())
      {
        T current = enumerator.Current;
        if (current.CompareTo(other) < 0)
          other = current;
      }
      return other;
    }
  }

  [DebuggerStepThrough]
  [Intermech.Diagnostics.NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Tuple<T, T> Get2Values<T>([Intermech.Diagnostics.NotNull] this IEnumerable<T> enumeration)
  {
    T obj1 = default (T);
    T obj2 = default (T);
    using (IEnumerator<T> enumerator = enumeration.GetEnumerator())
    {
      if (enumerator.MoveNext())
      {
        obj1 = enumerator.Current;
        if (enumerator.MoveNext())
          obj2 = enumerator.Current;
      }
    }
    return new Tuple<T, T>(obj1, obj2);
  }

  [DebuggerStepThrough]
  [Intermech.Diagnostics.NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Tuple<T, T, T> Get3Values<T>([Intermech.Diagnostics.NotNull] this IEnumerable<T> enumeration)
  {
    T obj1 = default (T);
    T obj2 = default (T);
    T obj3 = default (T);
    using (IEnumerator<T> enumerator = enumeration.GetEnumerator())
    {
      if (enumerator.MoveNext())
      {
        obj1 = enumerator.Current;
        if (enumerator.MoveNext())
        {
          obj2 = enumerator.Current;
          if (enumerator.MoveNext())
            obj3 = enumerator.Current;
        }
      }
    }
    return new Tuple<T, T, T>(obj1, obj2, obj3);
  }

  [LinqTunnel]
  [Intermech.Diagnostics.NotNull]
  public static IEnumerable<(int index, T item)> IndexIteration<T>([Intermech.Diagnostics.NotNull] this IEnumerable<T> enumeration)
  {
    int index = 0;
    foreach (T obj in enumeration)
      yield return (index++, obj);
  }

  [CanBeNull]
  public delegate T GetNextItemDelegate<T>([Intermech.Diagnostics.NotNull] T item) where T : class;

  [CanBeNull]
  public delegate IEnumerable<T> ExpandFuncDelegate<T>([CanBeNull] T value);
}
