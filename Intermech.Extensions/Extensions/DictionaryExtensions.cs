// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.DictionaryExtensions
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public static class DictionaryExtensions
{
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TValue LazyGet<TKey, TValue>(
    [NotNull] this IDictionary<TKey, TValue> dictionary,
    [NotNull] TKey key,
    [NotNull, InstantHandle] Func<TKey, TValue> initLambda)
  {
    TValue obj;
    if (!dictionary.TryGetValue(key, out obj))
    {
      obj = initLambda(key);
      dictionary[key] = obj;
    }
    return obj;
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TValue LazyGet<TKey, TValue>(
    [NotNull] this IDictionary<TKey, TValue> dictionary,
    [NotNull] TKey key,
    [NotNull, InstantHandle] Func<TValue> initLambda)
  {
    TValue obj;
    if (!dictionary.TryGetValue(key, out obj))
    {
      obj = initLambda();
      dictionary[key] = obj;
    }
    return obj;
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TValue GetOrAdd<TKey, TValue>(
    [NotNull] this Dictionary<TKey, TValue> dictionary,
    [NotNull] TKey key,
    [NotNull, InstantHandle] Func<TValue> initLambda)
  {
    TValue orAdd;
    if (!dictionary.TryGetValue(key, out orAdd))
    {
      orAdd = initLambda();
      dictionary[key] = orAdd;
    }
    return orAdd;
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TValue GetOrAdd<TKey, TValue>(
    [NotNull] this Dictionary<TKey, TValue> dictionary,
    [NotNull] TKey key,
    [NotNull, InstantHandle] Func<TKey, TValue> initLambda)
  {
    TValue orAdd;
    if (!dictionary.TryGetValue(key, out orAdd))
    {
      orAdd = initLambda(key);
      dictionary[key] = orAdd;
    }
    return orAdd;
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TValue GetOrAdd<TKey, TValue>(
    [NotNull] this Dictionary<TKey, TValue> dictionary,
    [NotNull] TKey key,
    [CanBeNull] TValue value)
  {
    TValue orAdd;
    if (!dictionary.TryGetValue(key, out orAdd))
    {
      orAdd = value;
      dictionary[key] = orAdd;
    }
    return orAdd;
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TValue LazyGetThreadSafe<TKey, TValue>(
    [NotNull] this Dictionary<TKey, TValue> dictionary,
    [NotNull] TKey key,
    [NotNull, InstantHandle] Func<TKey, TValue> initLambda)
  {
    TValue threadSafe;
    if (!dictionary.TryGetValue(key, out threadSafe))
    {
      lock (dictionary)
      {
        if (!dictionary.TryGetValue(key, out threadSafe))
        {
          threadSafe = initLambda(key);
          dictionary[key] = threadSafe;
        }
      }
    }
    return threadSafe;
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TValue LazyGetThreadSafe<TKey, TValue>(
    [NotNull] this Dictionary<TKey, TValue> dictionary,
    [NotNull] TKey key,
    [NotNull, InstantHandle] Func<TValue> initLambda)
  {
    TValue threadSafe;
    if (!dictionary.TryGetValue(key, out threadSafe))
    {
      lock (dictionary)
      {
        if (!dictionary.TryGetValue(key, out threadSafe))
        {
          threadSafe = initLambda();
          dictionary[key] = threadSafe;
        }
      }
    }
    return threadSafe;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void AddRange<TKey, TValue>(
    [NotNull] this IDictionary<TKey, TValue> dictionary,
    [NotNull] IEnumerable<KeyValuePair<TKey, TValue>> items)
  {
    foreach (KeyValuePair<TKey, TValue> keyValuePair in items)
      dictionary.Add(keyValuePair);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void AddRange<TKey, TValue>(
    [NotNull] this IDictionary<TKey, TValue> dictionary,
    [NotNull] IEnumerable<(TKey key, TValue value)> items)
  {
    foreach ((TKey key, TValue value) in items)
      dictionary.Add(key, value);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void AddRange<TKey, TValue>(
    [NotNull] this IDictionary<TKey, TValue> dictionary,
    [NotNull] params (TKey key, TValue value)[] items)
  {
    foreach ((TKey key, TValue value) in items)
      dictionary.Add(key, value);
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TValue GetOrDefault<TKey, TValue>(
    [CanBeNull] this IDictionary<TKey, TValue> dictionary,
    [NotNull] TKey key)
  {
    TValue obj;
    return dictionary == null || !dictionary.TryGetValue(key, out obj) ? default (TValue) : obj;
  }

  [ContractAnnotation("defaultValue:null => CanBeNull; => NotNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TValue GetOrDefault<TKey, TValue>(
    [CanBeNull] this IDictionary<TKey, TValue> dictionary,
    [NotNull] TKey key,
    TValue defaultValue)
  {
    TValue obj;
    return dictionary == null || !dictionary.TryGetValue(key, out obj) ? defaultValue : obj;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyDictionary<TKey, TValue> GetReadOnly<TKey, TValue>(
    [NotNull] this IDictionary<TKey, TValue> dictionary)
  {
    return (IReadOnlyDictionary<TKey, TValue>) new ReadOnlyDictionaryWrapper<TKey, TValue>(dictionary);
  }
}
