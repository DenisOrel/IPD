// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.EmptyDictionary`2
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

public class EmptyDictionary<TKey, TValue> : 
  IEnumerable<(TKey Key, TValue Value)>,
  IEnumerable,
  IEnumerable<KeyValuePair<TKey, TValue>>,
  IReadOnlyCollection<(TKey Key, TValue Value)>,
  IReadOnlyCollection<KeyValuePair<TKey, TValue>>,
  IReadOnlyList<(TKey Key, TValue Value)>,
  IReadOnlyList<KeyValuePair<TKey, TValue>>,
  IReadOnlyDictionary<TKey, TValue>
{
  private static bool? _keyIsReference;
  private const int ItemsCount = 0;

  private static bool KeyIsReference
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return EmptyDictionary<TKey, TValue>._keyIsReference ?? (EmptyDictionary<TKey, TValue>._keyIsReference = new bool?(typeof (TKey).IsByRef)).Value;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private EmptyDictionary()
  {
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public IEnumerator<(TKey Key, TValue Value)> GetEnumerator()
  {
    return Enumerable.Empty<(TKey, TValue)>().GetEnumerator();
  }

  IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
  {
    return Enumerable.Empty<KeyValuePair<TKey, TValue>>().GetEnumerator();
  }

  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

  public int Count
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => 0;
  }

  public (TKey Key, TValue Value) this[int index]
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      throw new IndexOutOfRangeException("Dictionary is empty!");
    }
  }

  KeyValuePair<TKey, TValue> IReadOnlyList<KeyValuePair<TKey, TValue>>.this[int index]
  {
    get => throw new IndexOutOfRangeException("Dictionary is empty!");
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool ContainsKey([NotNull] TKey key) => false;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool TryGetValue([NotNull] TKey key, [CanBeNull] out TValue value)
  {
    value = default (TValue);
    return false;
  }

  [CanBeNull]
  public TValue this[[NotNull] TKey key]
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      throw new KeyNotFoundException("Dictionary is empty!");
    }
  }

  public IEnumerable<TKey> Keys
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return (IEnumerable<TKey>) Array.Empty<TKey>();
    }
  }

  public IEnumerable<TValue> Values
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return (IEnumerable<TValue>) Array.Empty<TValue>();
    }
  }

  internal static class Singleton
  {
    [NotNull]
    public static readonly EmptyDictionary<TKey, TValue> Instance = new EmptyDictionary<TKey, TValue>();
  }
}
