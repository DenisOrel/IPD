// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.ISelectedItemsExtensions
// Assembly: Intermech.Extensions.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8EE4EE90-67E9-496B-9E84-18C409B882FC
// Assembly location: D:\IPS\Client\Intermech.Extensions.Client.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Navigator.Interfaces;

public static class ISelectedItemsExtensions
{
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<T> Select<T>(
    [NotNull] this ISelectedItems selectedItems,
    [NotNull] Func<INodeID, T> selector)
  {
    return selectedItems.AsNodeIdList().Select<INodeID, T>(selector).WrapWithCount<T>(selectedItems.Count);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<T> Select<T>(
    [NotNull] this ISelectedItems selectedItems,
    [NotNull] Func<INodeID, int, T> selector)
  {
    return selectedItems.AsNodeIdList().Select<INodeID, T>(selector).WrapWithCount<T>(selectedItems.Count);
  }

  [NotNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyList<INodeID> Where(
    [NotNull] this ISelectedItems selectedItems,
    [NotNull] Func<INodeID, bool> predicate)
  {
    List<INodeID> nodeIdList = new List<INodeID>(selectedItems.Count);
    nodeIdList.AddRange(selectedItems.AsNodeIdList().Where<INodeID>(predicate));
    return (IReadOnlyList<INodeID>) nodeIdList;
  }

  [NotNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyList<INodeID> Where(
    [NotNull] this ISelectedItems selectedItems,
    [NotNull] Func<INodeID, int, bool> predicate)
  {
    List<INodeID> nodeIdList = new List<INodeID>(selectedItems.Count);
    nodeIdList.AddRange(selectedItems.AsNodeIdList().Where<INodeID>(predicate));
    return (IReadOnlyList<INodeID>) nodeIdList;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool All([NotNull] this ISelectedItems selectedItems, [NotNull] Func<INodeID, bool> predicate)
  {
    return selectedItems.AsEnumeration().All<INodeID>(predicate);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool Any([NotNull] this ISelectedItems selectedItems)
  {
    return selectedItems.AsEnumeration().Any<INodeID>();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool Any([NotNull] this ISelectedItems selectedItems, [NotNull] Func<INodeID, bool> predicate)
  {
    return selectedItems.AsEnumeration().Any<INodeID>(predicate);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static INodeID First([NotNull] this ISelectedItems selectedItems)
  {
    return selectedItems.AsEnumeration().First<INodeID>();
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static INodeID First([NotNull] this ISelectedItems selectedItems, [NotNull] Func<INodeID, bool> predicate)
  {
    return selectedItems.AsEnumeration().First<INodeID>(predicate);
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static INodeID FirstOrDefault([NotNull] this ISelectedItems selectedItems)
  {
    return selectedItems.AsEnumeration().FirstOrDefault<INodeID>();
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static INodeID FirstOrDefault(
    [NotNull] this ISelectedItems selectedItems,
    [NotNull] Func<INodeID, bool> predicate)
  {
    return selectedItems.AsEnumeration().FirstOrDefault<INodeID>(predicate);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void InvokeForAll([NotNull] this ISelectedItems selectedItems, [NotNull] Action<INodeID> handler)
  {
    for (int index = 0; index < selectedItems.Count; ++index)
    {
      INodeID itemId = selectedItems.GetItemID(index);
      handler(itemId);
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void InvokeForAll([NotNull] this ISelectedItems selectedItems, [NotNull] Action<int, INodeID> handler)
  {
    for (int index = 0; index < selectedItems.Count; ++index)
    {
      INodeID itemId = selectedItems.GetItemID(index);
      handler(index, itemId);
    }
  }

  [NotNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<INodeID> AsEnumeration([NotNull] this ISelectedItems selectedItems)
  {
    for (int i = 0; i < selectedItems.Count; ++i)
      yield return selectedItems.GetItemID(i);
  }

  [NotNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyList<INodeID> AsNodeIdList([NotNull] this ISelectedItems selectedItems)
  {
    int count = selectedItems.Count;
    if (count == 0)
      return (IReadOnlyList<INodeID>) Array.Empty<INodeID>();
    List<INodeID> nodeIdList = new List<INodeID>(count);
    for (int index = 0; index < count; ++index)
    {
      INodeID itemId = selectedItems.GetItemID(index);
      nodeIdList.Add(itemId);
    }
    return (IReadOnlyList<INodeID>) nodeIdList;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyList<TItem> AsItems<TItem>([NotNull] this ISelectedItems selectedItems)
  {
    int count = selectedItems.Count;
    if (count == 0)
      return (IReadOnlyList<TItem>) Array.Empty<TItem>();
    List<TItem> objList = (List<TItem>) null;
    for (int index = 0; index < count; ++index)
    {
      TItem data;
      if (selectedItems.TryGetItemData<TItem>(index, out data))
      {
        if (objList == null)
          objList = new List<TItem>(count);
        objList.Add(data);
      }
    }
    return (IReadOnlyList<TItem>) objList ?? (IReadOnlyList<TItem>) Array.Empty<TItem>();
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetItems<TItem>([CanBeNull] this ISelectedItems selectedItems, out List<TItem> result)
  {
    result = (List<TItem>) null;
    if (selectedItems == null)
      return false;
    int count = selectedItems.Count;
    if (count == 0)
      return false;
    for (int index = 0; index < count; ++index)
    {
      TItem data;
      if (selectedItems.TryGetItemData<TItem>(index, out data))
      {
        if (result == null)
          result = new List<TItem>(count);
        result.Add(data);
      }
    }
    return result != null;
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetItems<TItem>(
    [CanBeNull] this ISelectedItems selectedItems,
    [NotNull] Func<TItem, bool> filter,
    out List<TItem> result)
  {
    result = (List<TItem>) null;
    if (selectedItems == null)
      return false;
    int count = selectedItems.Count;
    if (count == 0)
      return false;
    for (int index = 0; index < count; ++index)
    {
      TItem data;
      if (selectedItems.TryGetItemData<TItem>(index, out data) && filter(data))
      {
        if (result == null)
          result = new List<TItem>(count);
        result.Add(data);
      }
    }
    return result != null;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetItems<TItem>(
    [CanBeNull] this ISelectedItems selectedItems,
    out OneOrMore<TItem> result)
  {
    result = new OneOrMore<TItem>();
    if (selectedItems == null)
      return false;
    int count = selectedItems.Count;
    if (count == 0)
      return false;
    List<TItem> values = (List<TItem>) null;
    for (int index = 0; index < count; ++index)
    {
      TItem data;
      if (selectedItems.TryGetItemData<TItem>(index, out data))
      {
        if (values == null)
          values = new List<TItem>(count);
        values.Add(data);
      }
    }
    if (values == null || values.Count <= 0)
      return false;
    result = new OneOrMore<TItem>((IReadOnlyList<TItem>) values, true);
    return true;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetAll<TItem>(
    [CanBeNull] this ISelectedItems selectedItems,
    out OneOrMore<TItem> result)
  {
    result = new OneOrMore<TItem>();
    if (selectedItems == null)
      return false;
    int count = selectedItems.Count;
    if (count == 0)
      return false;
    List<TItem> values = (List<TItem>) null;
    for (int index = 0; index < count; ++index)
    {
      TItem data;
      if (!selectedItems.TryGetItemData<TItem>(index, out data))
      {
        result = new OneOrMore<TItem>();
        return false;
      }
      if (values == null)
        values = new List<TItem>(count);
      values.Add(data);
    }
    if (values == null || values.Count <= 0)
      return false;
    result = new OneOrMore<TItem>((IReadOnlyList<TItem>) values, true);
    return true;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetItems<TItem>(
    [CanBeNull] this ISelectedItems selectedItems,
    [NotNull] Func<TItem, bool> filter,
    out OneOrMore<TItem> result)
  {
    result = new OneOrMore<TItem>();
    if (selectedItems == null)
      return false;
    int count = selectedItems.Count;
    if (count == 0)
      return false;
    List<TItem> values = (List<TItem>) null;
    for (int index = 0; index < count; ++index)
    {
      TItem data;
      if (selectedItems.TryGetItemData<TItem>(index, out data) && filter(data))
      {
        if (values == null)
          values = new List<TItem>(count);
        values.Add(data);
      }
    }
    if (values == null || values.Count <= 0)
      return false;
    result = new OneOrMore<TItem>((IReadOnlyList<TItem>) values, true);
    return true;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetAll<TItem>(
    [CanBeNull] this ISelectedItems selectedItems,
    [NotNull] Func<TItem, bool> filter,
    out OneOrMore<TItem> result)
  {
    result = new OneOrMore<TItem>();
    if (selectedItems == null)
      return false;
    int count = selectedItems.Count;
    if (count == 0)
      return false;
    List<TItem> values = (List<TItem>) null;
    for (int index = 0; index < count; ++index)
    {
      TItem data;
      if (!selectedItems.TryGetItemData<TItem>(index, out data) || !filter(data))
      {
        result = new OneOrMore<TItem>();
        return false;
      }
      if (values == null)
        values = new List<TItem>(count);
      values.Add(data);
    }
    if (values == null || values.Count <= 0)
      return false;
    result = new OneOrMore<TItem>((IReadOnlyList<TItem>) values, true);
    return true;
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetAll<TItem>(
    [CanBeNull] this ISelectedItems selectedItems,
    [NotNull] Func<TItem, bool> filter,
    out IReadOnlyCollection<TItem> result)
  {
    result = (IReadOnlyCollection<TItem>) null;
    if (selectedItems == null)
      return false;
    int count = selectedItems.Count;
    if (count == 0)
      return false;
    List<TItem> objList = (List<TItem>) null;
    for (int index = 0; index < count; ++index)
    {
      TItem data;
      if (!selectedItems.TryGetItemData<TItem>(index, out data) || !filter(data))
      {
        result = (IReadOnlyCollection<TItem>) null;
        return false;
      }
      if (objList == null)
        objList = new List<TItem>(count);
      objList.Add(data);
    }
    if (objList == null || objList.Count <= 0)
      return false;
    result = (IReadOnlyCollection<TItem>) objList;
    return true;
  }

  [ContractAnnotation("throwExceptionIfNotFound:true => NotNull; throwExceptionIfNotFound:false => CanBeNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T GetParentData<T>(
    [NotNull] this ISelectedItems selectedItems,
    int index,
    bool throwExceptionIfNotFound = true,
    [CanBeNull] string exceptionMessageIfFail = null)
  {
    object parentData = selectedItems.GetParentData(index, typeof (T));
    return !(parentData == null & throwExceptionIfNotFound) ? (T) parentData : throw new InvalidOperationException(exceptionMessageIfFail ?? $"ISimpleSelectedItems must contains \"{typeof (T).Name}\"");
  }
}
