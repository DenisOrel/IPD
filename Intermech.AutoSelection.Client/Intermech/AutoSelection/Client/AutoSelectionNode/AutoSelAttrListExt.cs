// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.AutoSelectionNode.AutoSelAttrListExt
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.AutoSelection.Client.AutoSelectionNode;

internal static class AutoSelAttrListExt
{
  public static ICollection<Guid> CollectAttrTypeGuids(
    this IEnumerable<AutoSelAttr> source,
    IMSGlobals type,
    ICollection<Guid> collector)
  {
    if (type == IMSGlobals.IMSAttributeType || type == IMSGlobals.Unknown)
      source.CollectAttrTypeGuids(collector);
    return collector;
  }

  public static ICollection<Guid> CollectAttrTypeGuids(
    this IEnumerable<AutoSelAttr> source,
    ICollection<Guid> collector)
  {
    return AutoSelAttrListExt.CollectUnique<AutoSelAttr, Guid>(source, collector, new Predicate<AutoSelAttr>(CheckAttrGuid), new Func<AutoSelAttr, Guid>(GetAttrGuid));

    bool CheckAttrGuid(AutoSelAttr a) => a != null && !a.AttrGuid.Equals(Guid.Empty);

    Guid GetAttrGuid(AutoSelAttr a) => a.AttrGuid;
  }

  public static ICollection<T> CollectUnique<T>(IEnumerable<T> source, ICollection<T> collector)
  {
    return AutoSelAttrListExt.CollectUnique<T, T>(source, collector, (Predicate<T>) (e => true), (Func<T, T>) (e => e));
  }

  public static ICollection<TResult> CollectUnique<TSource, TResult>(
    IEnumerable<TSource> source,
    ICollection<TResult> collector,
    Predicate<TSource> condition,
    Func<TSource, TResult> selector)
  {
    if (collector.IsReadOnly)
      return collector;
    foreach (TResult result in source.Where<TSource>((Func<TSource, bool>) (src => condition(src))).Select<TSource, TResult>((Func<TSource, TResult>) (src => selector(src))))
    {
      if (!collector.Contains(result))
        collector.Add(result);
    }
    return collector;
  }
}
