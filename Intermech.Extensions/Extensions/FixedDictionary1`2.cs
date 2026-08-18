// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.FixedDictionary1`2
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public class FixedDictionary1<TKey, TValue> : 
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
  private const int ItemsCount = 1;
  [NotNull]
  private readonly IEqualityComparer<TKey> _keyComparer;
  [NotNull]
  private readonly TKey _key;
  [CanBeNull]
  private readonly TValue _value;
  [CanBeNull]
  [ItemNotNull]
  private TKey[] _keys;
  [CanBeNull]
  [ItemCanBeNull]
  private TValue[] _values;

  private static bool KeyIsReference
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return FixedDictionary1<TKey, TValue>._keyIsReference ?? (FixedDictionary1<TKey, TValue>._keyIsReference = new bool?(typeof (TKey).IsByRef)).Value;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public FixedDictionary1(
    [CanBeNull] IEqualityComparer<TKey> keyComparer,
    [NotNull] IEnumerable<(TKey Key, TValue Value)> keyValues)
  {
    this._keyComparer = keyComparer ?? (IEqualityComparer<TKey>) EqualityComparer<TKey>.Default;
    using (IEnumerator<(TKey Key, TValue Value)> enumerator = keyValues.GetEnumerator())
      (this._key, this._value) = enumerator.MoveNext() ? enumerator.Current : throw new Exception("Wrong items count!");
  }

  public FixedDictionary1(
    [CanBeNull] IEqualityComparer<TKey> keyComparer,
    [NotNull] IEnumerable<KeyValuePair<TKey, TValue>> keyValues)
  {
    this._keyComparer = keyComparer ?? (IEqualityComparer<TKey>) EqualityComparer<TKey>.Default;
    using (IEnumerator<KeyValuePair<TKey, TValue>> enumerator = keyValues.GetEnumerator())
    {
      if (!enumerator.MoveNext())
        throw new Exception("Wrong items count!");
      TKey key;
      TValue obj;
      enumerator.Current.Deconstruct<TKey, TValue>(out key, out obj);
      this._key = key;
      this._value = obj;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public FixedDictionary1([CanBeNull] IEqualityComparer<TKey> keyComparer, [NotNull] TKey key, [CanBeNull] TValue value)
  {
    this._keyComparer = keyComparer ?? (IEqualityComparer<TKey>) EqualityComparer<TKey>.Default;
    this._key = key;
    this._value = value;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public IEnumerator<(TKey Key, TValue Value)> GetEnumerator()
  {
    return SingleEnumerator.FromStruct<(TKey, TValue)>((this._key, this._value));
  }

  IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
  {
    return SingleEnumerator.FromStruct<KeyValuePair<TKey, TValue>>(new KeyValuePair<TKey, TValue>(this._key, this._value));
  }

  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

  public int Count
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => 1;
  }

  public (TKey Key, TValue Value) this[int index]
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (this._key, this._value);
  }

  KeyValuePair<TKey, TValue> IReadOnlyList<KeyValuePair<TKey, TValue>>.this[int index]
  {
    get => new KeyValuePair<TKey, TValue>(this._key, this._value);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool ContainsKey([NotNull] TKey key)
  {
    int num = FixedDictionary1<TKey, TValue>.KeyIsReference ? 1 : 0;
    return this._keyComparer.Equals(this._key, key);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool TryGetValue([NotNull] TKey key, [CanBeNull] out TValue value)
  {
    int num = FixedDictionary1<TKey, TValue>.KeyIsReference ? 1 : 0;
    if (this._keyComparer.Equals(this._key, key))
    {
      value = this._value;
      return true;
    }
    value = default (TValue);
    return false;
  }

  [CanBeNull]
  public TValue this[[NotNull] TKey key]
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      int num = FixedDictionary1<TKey, TValue>.KeyIsReference ? 1 : 0;
      if (this._keyComparer.Equals(this._key, key))
        return this._value;
      throw new KeyNotFoundException($"Value with key={key} not found!");
    }
  }

  public IEnumerable<TKey> Keys
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      TKey[] keys = this._keys;
      if (keys != null)
        return (IEnumerable<TKey>) keys;
      return (IEnumerable<TKey>) (this._keys = new TKey[1]
      {
        this._key
      });
    }
  }

  public IEnumerable<TValue> Values
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      TValue[] values = this._values;
      if (values != null)
        return (IEnumerable<TValue>) values;
      return (IEnumerable<TValue>) (this._values = new TValue[1]
      {
        this._value
      });
    }
  }
}
