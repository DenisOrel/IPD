// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.DictionaryFactory
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public static class DictionaryFactory
{
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Dictionary<TKey, TValue> Create<TKey, TValue>(
    [CanBeNull] IEnumerable<KeyValuePair<TKey, TValue>> enumeration = null,
    int capacity = 16 /*0x10*/,
    [CanBeNull] IEqualityComparer<TKey> comparer = null)
  {
    Intermech.Diagnostics.Check.ArgumentIsZeroOrPositive(capacity, nameof (capacity));
    int result;
    Dictionary<TKey, TValue> dictionary = enumeration == null || !enumeration.TryGetCountOrCapacity<KeyValuePair<TKey, TValue>>(out result) ? new Dictionary<TKey, TValue>(capacity, comparer) : new Dictionary<TKey, TValue>(Math.Max(capacity, result), comparer);
    if (enumeration != null)
      dictionary.AddRange<TKey, TValue>(enumeration);
    return dictionary;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Dictionary<TKey, TValue> Create<TKey, TValue>(
    [NotNull] params KeyValuePair<TKey, TValue>[] keyValues)
  {
    Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>(keyValues.Length);
    dictionary.AddRange<TKey, TValue>((IEnumerable<KeyValuePair<TKey, TValue>>) keyValues);
    return dictionary;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Dictionary<TKey, TValue> Create<TKey, TValue>(
    [NotNull] params (TKey key, TValue value)[] keyValues)
  {
    Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>(keyValues.Length);
    dictionary.AddRange<TKey, TValue>(keyValues);
    return dictionary;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Dictionary<TKey, TValue> Create<TKey, TValue>(
    [CanBeNull] IEnumerable<KeyValuePair<TKey, TValue>> enumeration,
    [CanBeNull] IEqualityComparer<TKey> comparer,
    int capacity = 16 /*0x10*/)
  {
    Intermech.Diagnostics.Check.ArgumentIsZeroOrPositive(capacity, nameof (capacity));
    int result;
    Dictionary<TKey, TValue> dictionary = enumeration == null || !enumeration.TryGetCountOrCapacity<KeyValuePair<TKey, TValue>>(out result) ? new Dictionary<TKey, TValue>(capacity, comparer) : new Dictionary<TKey, TValue>(Math.Max(capacity, result), comparer);
    if (enumeration != null)
      dictionary.AddRange<TKey, TValue>(enumeration);
    return dictionary;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Dictionary<TKey, TValue> Create<TKey, TValue>(
    int capacity,
    [CanBeNull] IEnumerable<KeyValuePair<TKey, TValue>> enumeration = null,
    [CanBeNull] IEqualityComparer<TKey> comparer = null)
  {
    Intermech.Diagnostics.Check.ArgumentIsZeroOrPositive(capacity, nameof (capacity));
    int result;
    Dictionary<TKey, TValue> dictionary = enumeration == null || !enumeration.TryGetCountOrCapacity<KeyValuePair<TKey, TValue>>(out result) ? new Dictionary<TKey, TValue>(capacity, comparer) : new Dictionary<TKey, TValue>(Math.Max(capacity, result), comparer);
    if (enumeration != null)
      dictionary.AddRange<TKey, TValue>(enumeration);
    return dictionary;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Dictionary<TKey, TValue> Create<TKey, TValue>(
    int capacity,
    [CanBeNull] IEnumerable<(TKey Key, TValue Value)> enumeration = null,
    [CanBeNull] IEqualityComparer<TKey> comparer = null)
  {
    Intermech.Diagnostics.Check.ArgumentIsZeroOrPositive(capacity, nameof (capacity));
    int result;
    Dictionary<TKey, TValue> dictionary = enumeration == null || !enumeration.TryGetCountOrCapacity<(TKey, TValue)>(out result) ? new Dictionary<TKey, TValue>(capacity, comparer) : new Dictionary<TKey, TValue>(Math.Max(capacity, result), comparer);
    if (enumeration != null)
    {
      foreach ((TKey Key, TValue Value) tuple in enumeration)
        dictionary.Add(tuple.Key, tuple.Value);
    }
    return dictionary;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Dictionary<TKey, TValue> Create<TKey, TValue>(
    int capacity,
    [NotNull] IEqualityComparer<TKey> comparer,
    [CanBeNull] IEnumerable<KeyValuePair<TKey, TValue>> enumeration = null)
  {
    Intermech.Diagnostics.Check.ArgumentIsZeroOrPositive(capacity, nameof (capacity));
    int result;
    Dictionary<TKey, TValue> dictionary = enumeration == null || !enumeration.TryGetCountOrCapacity<KeyValuePair<TKey, TValue>>(out result) ? new Dictionary<TKey, TValue>(capacity, comparer) : new Dictionary<TKey, TValue>(Math.Max(capacity, result), comparer);
    if (enumeration != null)
      dictionary.AddRange<TKey, TValue>(enumeration);
    return dictionary;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Dictionary<TKey, TValue> Create<TKey, TValue>(
    [NotNull] IEqualityComparer<TKey> comparer,
    [CanBeNull] IEnumerable<KeyValuePair<TKey, TValue>> enumeration = null,
    int capacity = 16 /*0x10*/)
  {
    Intermech.Diagnostics.Check.ArgumentIsZeroOrPositive(capacity, nameof (capacity));
    int result;
    Dictionary<TKey, TValue> dictionary = enumeration == null || !enumeration.TryGetCountOrCapacity<KeyValuePair<TKey, TValue>>(out result) ? new Dictionary<TKey, TValue>(capacity, comparer) : new Dictionary<TKey, TValue>(Math.Max(capacity, result), comparer);
    if (enumeration != null)
      dictionary.AddRange<TKey, TValue>(enumeration);
    return dictionary;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Dictionary<TKey, TValue> Create<TKey, TValue>(
    [NotNull] IEqualityComparer<TKey> comparer,
    int capacity,
    [CanBeNull] IEnumerable<KeyValuePair<TKey, TValue>> enumeration = null)
  {
    Intermech.Diagnostics.Check.ArgumentIsZeroOrPositive(capacity, nameof (capacity));
    int result;
    Dictionary<TKey, TValue> dictionary = enumeration == null || !enumeration.TryGetCountOrCapacity<KeyValuePair<TKey, TValue>>(out result) ? new Dictionary<TKey, TValue>(capacity, comparer) : new Dictionary<TKey, TValue>(Math.Max(capacity, result), comparer);
    if (enumeration != null)
      dictionary.AddRange<TKey, TValue>(enumeration);
    return dictionary;
  }
}
