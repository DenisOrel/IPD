// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.Dictionary
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public static class Dictionary
{
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Dictionary<TKey, TValue> Create<TKey, TValue>(
    [NotNull] IEnumerable<KeyValuePair<TKey, TValue>> keyValueEnumeration,
    int capacity = 0,
    [CanBeNull] IEqualityComparer<TKey> comparer = null)
  {
    Dictionary<TKey, TValue> dictionary = capacity != 0 ? new Dictionary<TKey, TValue>(capacity, comparer) : new Dictionary<TKey, TValue>(comparer);
    dictionary.AddRange<TKey, TValue>(keyValueEnumeration);
    return dictionary;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool IsEquals<TKey, TValue>(
    [CanBeNull] IDictionary<TKey, TValue> dictionary1,
    [CanBeNull] IDictionary<TKey, TValue> dictionary2)
  {
    if (dictionary1 == dictionary2)
      return true;
    if (dictionary1 == null || dictionary2 == null || dictionary1.Count != dictionary2.Count)
      return false;
    IEqualityComparer<TValue> valuesComparer = (IEqualityComparer<TValue>) EqualityComparer<TValue>.Default;
    TValue value2;
    return dictionary1.All<KeyValuePair<TKey, TValue>>((Func<KeyValuePair<TKey, TValue>, bool>) (keyValue1 => dictionary2.TryGetValue(keyValue1.Key, out value2) && valuesComparer.Equals(keyValue1.Value, value2)));
  }
}
