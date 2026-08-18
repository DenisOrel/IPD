// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.FixedDictionary4`2
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

public class FixedDictionary4<TKey, TValue> : 
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
  private const int ItemsCount = 4;
  [NotNull]
  private readonly IEqualityComparer<TKey> _keyComparer;
  [NotNull]
  private readonly TKey _key0;
  [CanBeNull]
  private readonly TValue _value0;
  [NotNull]
  private readonly TKey _key1;
  [CanBeNull]
  private readonly TValue _value1;
  [NotNull]
  private readonly TKey _key2;
  [CanBeNull]
  private readonly TValue _value2;
  [NotNull]
  private readonly TKey _key3;
  [CanBeNull]
  private readonly TValue _value3;
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
      return FixedDictionary4<TKey, TValue>._keyIsReference ?? (FixedDictionary4<TKey, TValue>._keyIsReference = new bool?(typeof (TKey).IsByRef)).Value;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public FixedDictionary4(
    [CanBeNull] IEqualityComparer<TKey> keyComparer,
    [NotNull] IEnumerable<(TKey Key, TValue Value)> keyValues)
  {
    this._keyComparer = keyComparer ?? (IEqualityComparer<TKey>) EqualityComparer<TKey>.Default;
    using (IEnumerator<(TKey Key, TValue Value)> enumerator = keyValues.GetEnumerator())
    {
      (this._key0, this._value0) = enumerator.MoveNext() ? enumerator.Current : throw new Exception("Wrong items count!");
      (this._key1, this._value1) = enumerator.MoveNext() ? enumerator.Current : throw new Exception("Wrong items count!");
      (this._key2, this._value2) = enumerator.MoveNext() ? enumerator.Current : throw new Exception("Wrong items count!");
      (this._key3, this._value3) = enumerator.MoveNext() ? enumerator.Current : throw new Exception("Wrong items count!");
    }
  }

  public FixedDictionary4(
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
      this._key0 = key;
      this._value0 = obj;
      if (!enumerator.MoveNext())
        throw new Exception("Wrong items count!");
      enumerator.Current.Deconstruct<TKey, TValue>(out key, out obj);
      this._key1 = key;
      this._value1 = obj;
      if (!enumerator.MoveNext())
        throw new Exception("Wrong items count!");
      enumerator.Current.Deconstruct<TKey, TValue>(out key, out obj);
      this._key2 = key;
      this._value2 = obj;
      if (!enumerator.MoveNext())
        throw new Exception("Wrong items count!");
      enumerator.Current.Deconstruct<TKey, TValue>(out key, out obj);
      this._key3 = key;
      this._value3 = obj;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public FixedDictionary4(
    [CanBeNull] IEqualityComparer<TKey> keyComparer,
    [NotNull] TKey key0,
    [CanBeNull] TValue value0,
    [NotNull] TKey key1,
    [CanBeNull] TValue value1,
    [NotNull] TKey key2,
    [CanBeNull] TValue value2,
    [NotNull] TKey key3,
    [CanBeNull] TValue value3)
  {
    this._keyComparer = keyComparer ?? (IEqualityComparer<TKey>) EqualityComparer<TKey>.Default;
    this._key0 = key0;
    this._value0 = value0;
    this._key1 = key1;
    this._value1 = value1;
    this._key2 = key2;
    this._value2 = value2;
    this._key3 = key3;
    this._value3 = value3;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public IEnumerator<(TKey Key, TValue Value)> GetEnumerator()
  {
    return (IEnumerator<(TKey, TValue)>) new FixedDictionary4<TKey, TValue>.TupleEnumerator(this);
  }

  IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
  {
    return (IEnumerator<KeyValuePair<TKey, TValue>>) new FixedDictionary4<TKey, TValue>.KeyValuePairEnumerator(this);
  }

  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

  public int Count
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => 4;
  }

  public (TKey Key, TValue Value) this[int index]
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      switch (index)
      {
        case 0:
          return (this._key0, this._value0);
        case 1:
          return (this._key1, this._value1);
        case 2:
          return (this._key2, this._value2);
        case 3:
          return (this._key3, this._value3);
        default:
          throw new InvalidOperationException("Invalid index");
      }
    }
  }

  KeyValuePair<TKey, TValue> IReadOnlyList<KeyValuePair<TKey, TValue>>.this[int index]
  {
    get
    {
      switch (index)
      {
        case 0:
          return new KeyValuePair<TKey, TValue>(this._key0, this._value0);
        case 1:
          return new KeyValuePair<TKey, TValue>(this._key1, this._value1);
        case 2:
          return new KeyValuePair<TKey, TValue>(this._key2, this._value2);
        case 3:
          return new KeyValuePair<TKey, TValue>(this._key3, this._value3);
        default:
          throw new InvalidOperationException("Invalid index");
      }
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool ContainsKey([NotNull] TKey key)
  {
    int num = FixedDictionary4<TKey, TValue>.KeyIsReference ? 1 : 0;
    return this._keyComparer.Equals(this._key0, key) || this._keyComparer.Equals(this._key1, key) || this._keyComparer.Equals(this._key2, key) || this._keyComparer.Equals(this._key3, key);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool TryGetValue([NotNull] TKey key, [CanBeNull] out TValue value)
  {
    int num = FixedDictionary4<TKey, TValue>.KeyIsReference ? 1 : 0;
    if (this._keyComparer.Equals(this._key0, key))
    {
      value = this._value0;
      return true;
    }
    if (this._keyComparer.Equals(this._key1, key))
    {
      value = this._value1;
      return true;
    }
    if (this._keyComparer.Equals(this._key2, key))
    {
      value = this._value2;
      return true;
    }
    if (this._keyComparer.Equals(this._key3, key))
    {
      value = this._value3;
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
      int num = FixedDictionary4<TKey, TValue>.KeyIsReference ? 1 : 0;
      if (this._keyComparer.Equals(this._key0, key))
        return this._value0;
      if (this._keyComparer.Equals(this._key1, key))
        return this._value1;
      if (this._keyComparer.Equals(this._key2, key))
        return this._value2;
      if (this._keyComparer.Equals(this._key3, key))
        return this._value3;
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
      return (IEnumerable<TKey>) (this._keys = new TKey[4]
      {
        this._key0,
        this._key1,
        this._key2,
        this._key3
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
      return (IEnumerable<TValue>) (this._values = new TValue[4]
      {
        this._value0,
        this._value1,
        this._value2,
        this._value3
      });
    }
  }

  private class KeyValuePairEnumerator : 
    IEnumerator<KeyValuePair<TKey, TValue>>,
    IDisposable,
    IEnumerator
  {
    [NotNull]
    private readonly FixedDictionary4<TKey, TValue> _owner;
    private int _fieldNum = -1;

    public KeyValuePairEnumerator([NotNull] FixedDictionary4<TKey, TValue> owner)
    {
      this._owner = owner;
    }

    public void Dispose()
    {
    }

    public bool MoveNext()
    {
      if (this._fieldNum < 4)
        ++this._fieldNum;
      return this._fieldNum < 4;
    }

    public void Reset() => this._fieldNum = -1;

    public KeyValuePair<TKey, TValue> Current
    {
      get
      {
        switch (this._fieldNum)
        {
          case 0:
            return new KeyValuePair<TKey, TValue>(this._owner._key0, this._owner._value0);
          case 1:
            return new KeyValuePair<TKey, TValue>(this._owner._key1, this._owner._value1);
          case 2:
            return new KeyValuePair<TKey, TValue>(this._owner._key2, this._owner._value2);
          case 3:
            return new KeyValuePair<TKey, TValue>(this._owner._key3, this._owner._value3);
          default:
            throw new InvalidOperationException("Call to the Reset and MoveNext method first");
        }
      }
    }

    [NotNull]
    object IEnumerator.Current => (object) this.Current;
  }

  private class TupleEnumerator : IEnumerator<(TKey Key, TValue Value)>, IDisposable, IEnumerator
  {
    [NotNull]
    private readonly FixedDictionary4<TKey, TValue> _owner;
    private int _fieldNum = -1;

    public TupleEnumerator([NotNull] FixedDictionary4<TKey, TValue> owner) => this._owner = owner;

    public void Dispose()
    {
    }

    public bool MoveNext()
    {
      if (this._fieldNum < 4)
        ++this._fieldNum;
      return this._fieldNum < 4;
    }

    public void Reset() => this._fieldNum = -1;

    public (TKey Key, TValue Value) Current
    {
      get
      {
        switch (this._fieldNum)
        {
          case 0:
            return (this._owner._key0, this._owner._value0);
          case 1:
            return (this._owner._key1, this._owner._value1);
          case 2:
            return (this._owner._key2, this._owner._value2);
          case 3:
            return (this._owner._key3, this._owner._value3);
          default:
            throw new InvalidOperationException("Call to the Reset and MoveNext method first");
        }
      }
    }

    [NotNull]
    object IEnumerator.Current => (object) this.Current;
  }
}
