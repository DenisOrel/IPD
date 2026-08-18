// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.ISimpleSelectedItemsExtensions
// Assembly: Intermech.Extensions.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8EE4EE90-67E9-496B-9E84-18C409B882FC
// Assembly location: D:\IPS\Client\Intermech.Extensions.Client.dll

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Navigator.Interfaces;

public static class ISimpleSelectedItemsExtensions
{
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<T> AsItemsList<T>(
    [NotNull] this ISimpleSelectedItems selectedItems,
    bool excludeDefaults = true)
  {
    List<T> objList = new List<T>(selectedItems.Count);
    Type dataFormat = typeof (T);
    T objB = default (T);
    for (int index = 0; index < selectedItems.Count; ++index)
    {
      T itemData = (T) selectedItems.GetItemData(index, dataFormat);
      if (!excludeDefaults || !object.Equals((object) itemData, (object) objB))
        objList.Add(itemData);
    }
    return (IReadOnlyCollection<T>) objList;
  }

  [ContractAnnotation("throwExceptionIfNotFound:true => NotNull; => CanBeNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T GetItemData<T>(
    [NotNull] this ISimpleSelectedItems selectedItems,
    int index,
    bool throwExceptionIfNotFound,
    [CanBeNull] string exceptionMessageIfFail = null)
  {
    object itemData = selectedItems.GetItemData(index, typeof (T));
    return !(itemData == null & throwExceptionIfNotFound) ? (T) itemData : throw new InvalidOperationException(exceptionMessageIfFail ?? $"ISimpleSelectedItems must contains \"{typeof (T).Name}\"");
  }

  [NotNull]
  public static T GetItemData<T>(
    [NotNull] this ISimpleSelectedItems selectedItems,
    int index,
    [CanBeNull] string exceptionMessageIfFail = null)
  {
    object itemData = selectedItems.GetItemData(index, typeof (T));
    if (itemData != null)
      return (T) itemData;
    if (exceptionMessageIfFail != null)
      throw new InvalidOperationException(exceptionMessageIfFail);
    throw new InvalidOperationException();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetItemData<T>(
    [NotNull] this ISimpleSelectedItems selectedItems,
    int index,
    [CanBeNull] out T data)
  {
    object itemData = selectedItems.GetItemData(index, typeof (T));
    data = itemData != null ? (T) itemData : default (T);
    return itemData != null;
  }
}
