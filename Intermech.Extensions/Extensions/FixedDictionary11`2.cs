// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.FixedDictionary11`2
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

public class FixedDictionary11<TKey, TValue> : 
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
  private const int ItemsCount = 11;
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
  [NotNull]
  private readonly TKey _key4;
  [CanBeNull]
  private readonly TValue _value4;
  [NotNull]
  private readonly TKey _key5;
  [CanBeNull]
  private readonly TValue _value5;
  [NotNull]
  private readonly TKey _key6;
  [CanBeNull]
  private readonly TValue _value6;
  [NotNull]
  private readonly TKey _key7;
  [CanBeNull]
  private readonly TValue _value7;
  [NotNull]
  private readonly TKey _key8;
  [CanBeNull]
  private readonly TValue _value8;
  [NotNull]
  private readonly TKey _key9;
  [CanBeNull]
  private readonly TValue _value9;
  [NotNull]
  private readonly TKey _key10;
  [CanBeNull]
  private readonly TValue _value10;
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
      return FixedDictionary11<TKey, TValue>._keyIsReference ?? (FixedDictionary11<TKey, TValue>._keyIsReference = new bool?(typeof (TKey).IsByRef)).Value;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public FixedDictionary11(
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
      (this._key4, this._value4) = enumerator.MoveNext() ? enumerator.Current : throw new Exception("Wrong items count!");
      (this._key5, this._value5) = enumerator.MoveNext() ? enumerator.Current : throw new Exception("Wrong items count!");
      (this._key6, this._value6) = enumerator.MoveNext() ? enumerator.Current : throw new Exception("Wrong items count!");
      (this._key7, this._value7) = enumerator.MoveNext() ? enumerator.Current : throw new Exception("Wrong items count!");
      (this._key8, this._value8) = enumerator.MoveNext() ? enumerator.Current : throw new Exception("Wrong items count!");
      (this._key9, this._value9) = enumerator.MoveNext() ? enumerator.Current : throw new Exception("Wrong items count!");
      (this._key10, this._value10) = enumerator.MoveNext() ? enumerator.Current : throw new Exception("Wrong items count!");
    }
  }

  public FixedDictionary11(
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
      if (!enumerator.MoveNext())
        throw new Exception("Wrong items count!");
      enumerator.Current.Deconstruct<TKey, TValue>(out key, out obj);
      this._key4 = key;
      this._value4 = obj;
      if (!enumerator.MoveNext())
        throw new Exception("Wrong items count!");
      enumerator.Current.Deconstruct<TKey, TValue>(out key, out obj);
      this._key5 = key;
      this._value5 = obj;
      if (!enumerator.MoveNext())
        throw new Exception("Wrong items count!");
      enumerator.Current.Deconstruct<TKey, TValue>(out key, out obj);
      this._key6 = key;
      this._value6 = obj;
      if (!enumerator.MoveNext())
        throw new Exception("Wrong items count!");
      enumerator.Current.Deconstruct<TKey, TValue>(out key, out obj);
      this._key7 = key;
      this._value7 = obj;
      if (!enumerator.MoveNext())
        throw new Exception("Wrong items count!");
      enumerator.Current.Deconstruct<TKey, TValue>(out key, out obj);
      this._key8 = key;
      this._value8 = obj;
      if (!enumerator.MoveNext())
        throw new Exception("Wrong items count!");
      enumerator.Current.Deconstruct<TKey, TValue>(out key, out obj);
      this._key9 = key;
      this._value9 = obj;
      if (!enumerator.MoveNext())
        throw new Exception("Wrong items count!");
      enumerator.Current.Deconstruct<TKey, TValue>(out key, out obj);
      this._key10 = key;
      this._value10 = obj;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public FixedDictionary11(
    [CanBeNull] IEqualityComparer<TKey> keyComparer,
    [NotNull] TKey key0,
    [CanBeNull] TValue value0,
    [NotNull] TKey key1,
    [CanBeNull] TValue value1,
    [NotNull] TKey key2,
    [CanBeNull] TValue value2,
    [NotNull] TKey key3,
    [CanBeNull] TValue value3,
    [NotNull] TKey key4,
    [CanBeNull] TValue value4,
    [NotNull] TKey key5,
    [CanBeNull] TValue value5,
    [NotNull] TKey key6,
    [CanBeNull] TValue value6,
    [NotNull] TKey key7,
    [CanBeNull] TValue value7,
    [NotNull] TKey key8,
    [CanBeNull] TValue value8,
    [NotNull] TKey key9,
    [CanBeNull] TValue value9,
    [NotNull] TKey key10,
    [CanBeNull] TValue value10)
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
    this._key4 = key4;
    this._value4 = value4;
    this._key5 = key5;
    this._value5 = value5;
    this._key6 = key6;
    this._value6 = value6;
    this._key7 = key7;
    this._value7 = value7;
    this._key8 = key8;
    this._value8 = value8;
    this._key9 = key9;
    this._value9 = value9;
    this._key10 = key10;
    this._value10 = value10;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public IEnumerator<(TKey Key, TValue Value)> GetEnumerator()
  {
    return (IEnumerator<(TKey, TValue)>) new FixedDictionary11<TKey, TValue>.TupleEnumerator(this);
  }

  IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
  {
    return (IEnumerator<KeyValuePair<TKey, TValue>>) new FixedDictionary11<TKey, TValue>.KeyValuePairEnumerator(this);
  }

  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

  public int Count
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => 11;
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
        case 4:
          return (this._key4, this._value4);
        case 5:
          return (this._key5, this._value5);
        case 6:
          return (this._key6, this._value6);
        case 7:
          return (this._key7, this._value7);
        case 8:
          return (this._key8, this._value8);
        case 9:
          return (this._key9, this._value9);
        case 10:
          return (this._key10, this._value10);
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
        case 4:
          return new KeyValuePair<TKey, TValue>(this._key4, this._value4);
        case 5:
          return new KeyValuePair<TKey, TValue>(this._key5, this._value5);
        case 6:
          return new KeyValuePair<TKey, TValue>(this._key6, this._value6);
        case 7:
          return new KeyValuePair<TKey, TValue>(this._key7, this._value7);
        case 8:
          return new KeyValuePair<TKey, TValue>(this._key8, this._value8);
        case 9:
          return new KeyValuePair<TKey, TValue>(this._key9, this._value9);
        case 10:
          return new KeyValuePair<TKey, TValue>(this._key10, this._value10);
        default:
          throw new InvalidOperationException("Invalid index");
      }
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool ContainsKey([NotNull] TKey key)
  {
    int num = FixedDictionary11<TKey, TValue>.KeyIsReference ? 1 : 0;
    return this._keyComparer.Equals(this._key0, key) || this._keyComparer.Equals(this._key1, key) || this._keyComparer.Equals(this._key2, key) || this._keyComparer.Equals(this._key3, key) || this._keyComparer.Equals(this._key4, key) || this._keyComparer.Equals(this._key5, key) || this._keyComparer.Equals(this._key6, key) || this._keyComparer.Equals(this._key7, key) || this._keyComparer.Equals(this._key8, key) || this._keyComparer.Equals(this._key9, key) || this._keyComparer.Equals(this._key10, key);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool TryGetValue([NotNull] TKey key, [CanBeNull] out TValue value)
  {
    int num = FixedDictionary11<TKey, TValue>.KeyIsReference ? 1 : 0;
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
    if (this._keyComparer.Equals(this._key4, key))
    {
      value = this._value4;
      return true;
    }
    if (this._keyComparer.Equals(this._key5, key))
    {
      value = this._value5;
      return true;
    }
    if (this._keyComparer.Equals(this._key6, key))
    {
      value = this._value6;
      return true;
    }
    if (this._keyComparer.Equals(this._key7, key))
    {
      value = this._value7;
      return true;
    }
    if (this._keyComparer.Equals(this._key8, key))
    {
      value = this._value8;
      return true;
    }
    if (this._keyComparer.Equals(this._key9, key))
    {
      value = this._value9;
      return true;
    }
    if (this._keyComparer.Equals(this._key10, key))
    {
      value = this._value10;
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
      int num = FixedDictionary11<TKey, TValue>.KeyIsReference ? 1 : 0;
      if (this._keyComparer.Equals(this._key0, key))
        return this._value0;
      if (this._keyComparer.Equals(this._key1, key))
        return this._value1;
      if (this._keyComparer.Equals(this._key2, key))
        return this._value2;
      if (this._keyComparer.Equals(this._key3, key))
        return this._value3;
      if (this._keyComparer.Equals(this._key4, key))
        return this._value4;
      if (this._keyComparer.Equals(this._key5, key))
        return this._value5;
      if (this._keyComparer.Equals(this._key6, key))
        return this._value6;
      if (this._keyComparer.Equals(this._key7, key))
        return this._value7;
      if (this._keyComparer.Equals(this._key8, key))
        return this._value8;
      if (this._keyComparer.Equals(this._key9, key))
        return this._value9;
      if (this._keyComparer.Equals(this._key10, key))
        return this._value10;
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
      return (IEnumerable<TKey>) (this._keys = new TKey[11]
      {
        this._key0,
        this._key1,
        this._key2,
        this._key3,
        this._key4,
        this._key5,
        this._key6,
        this._key7,
        this._key8,
        this._key9,
        this._key10
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
      return (IEnumerable<TValue>) (this._values = new TValue[11]
      {
        this._value0,
        this._value1,
        this._value2,
        this._value3,
        this._value4,
        this._value5,
        this._value6,
        this._value7,
        this._value8,
        this._value9,
        this._value10
      });
    }
  }

  private class KeyValuePairEnumerator : 
    IEnumerator<KeyValuePair<TKey, TValue>>,
    IDisposable,
    IEnumerator
  {
    [NotNull]
    private readonly FixedDictionary11<TKey, TValue> _owner;
    private int _fieldNum = -1;

    public KeyValuePairEnumerator([NotNull] FixedDictionary11<TKey, TValue> owner)
    {
      this._owner = owner;
    }

    public void Dispose()
    {
    }

    public bool MoveNext()
    {
      if (this._fieldNum < 11)
        ++this._fieldNum;
      return this._fieldNum < 11;
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
          case 4:
            return new KeyValuePair<TKey, TValue>(this._owner._key4, this._owner._value4);
          case 5:
            return new KeyValuePair<TKey, TValue>(this._owner._key5, this._owner._value5);
          case 6:
            return new KeyValuePair<TKey, TValue>(this._owner._key6, this._owner._value6);
          case 7:
            return new KeyValuePair<TKey, TValue>(this._owner._key7, this._owner._value7);
          case 8:
            return new KeyValuePair<TKey, TValue>(this._owner._key8, this._owner._value8);
          case 9:
            return new KeyValuePair<TKey, TValue>(this._owner._key9, this._owner._value9);
          case 10:
            return new KeyValuePair<TKey, TValue>(this._owner._key10, this._owner._value10);
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
    private readonly FixedDictionary11<TKey, TValue> _owner;
    private int _fieldNum = -1;

    public TupleEnumerator([NotNull] FixedDictionary11<TKey, TValue> owner) => this._owner = owner;

    public void Dispose()
    {
    }

    public bool MoveNext()
    {
      if (this._fieldNum < 11)
        ++this._fieldNum;
      return this._fieldNum < 11;
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
          case 4:
            return (this._owner._key4, this._owner._value4);
          case 5:
            return (this._owner._key5, this._owner._value5);
          case 6:
            return (this._owner._key6, this._owner._value6);
          case 7:
            return (this._owner._key7, this._owner._value7);
          case 8:
            return (this._owner._key8, this._owner._value8);
          case 9:
            return (this._owner._key9, this._owner._value9);
          case 10:
            return (this._owner._key10, this._owner._value10);
          default:
            throw new InvalidOperationException("Call to the Reset and MoveNext method first");
        }
      }
    }

    [NotNull]
    object IEnumerator.Current => (object) this.Current;
  }
}
