// Decompiled with JetBrains decompiler
// Type: Intermech.Collections.GeneralDictionaryMapAdapter`2
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Collections;

internal sealed class GeneralDictionaryMapAdapter<TKey, TValue> : 
  GeneralDictionaryAdapterBase,
  IDictionary,
  ICollection,
  IEnumerable,
  IDictionary<TKey, TValue>,
  ICollection<KeyValuePair<TKey, TValue>>,
  IEnumerable<KeyValuePair<TKey, TValue>>,
  IReadOnlyDictionary<TKey, TValue>,
  IReadOnlyCollection<KeyValuePair<TKey, TValue>>,
  IEquatable<IDictionary>
{
  [NotNull]
  private readonly Func<object, TKey> _keySelector;
  [NotNull]
  private readonly Func<object, TValue> _valueSelector;

  public GeneralDictionaryMapAdapter(
    [NotNull] IDictionary dictionary,
    [NotNull] Func<object, TKey> keySelector,
    [NotNull] Func<object, TValue> valueSelector)
    : base(dictionary)
  {
    this._keySelector = keySelector;
    this._valueSelector = valueSelector;
  }

  public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
  {
    return this.WrappedObject.Cast<DictionaryEntry>().Select<DictionaryEntry, KeyValuePair<TKey, TValue>>((Func<DictionaryEntry, KeyValuePair<TKey, TValue>>) (dictionaryEntry => new KeyValuePair<TKey, TValue>(this._keySelector(dictionaryEntry.Key), this._valueSelector(dictionaryEntry.Value)))).GetEnumerator();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void Add(KeyValuePair<TKey, TValue> item)
  {
    this.WrappedObject.Add((object) item.Key, (object) item.Value);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool Contains(KeyValuePair<TKey, TValue> item)
  {
    if (!this.WrappedObject.Contains((object) item.Key))
      return false;
    object obj = this.WrappedObject[(object) item.Key];
    return obj == null ? (object) item.Value == null : obj.Equals((object) item.Value);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
  {
    this.WrappedObject.CopyTo((Array) array, arrayIndex);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool Remove(KeyValuePair<TKey, TValue> item)
  {
    if (!this.Contains(item))
      return false;
    this.WrappedObject.Remove((object) item.Key);
    return true;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool ContainsKey([NotNull] TKey key) => this.WrappedObject.Contains((object) key);

  [ContractAnnotation("=> true, value: notnull; => false, value: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool TryGetValue([NotNull] TKey key, [CanBeNull] out TValue value)
  {
    if (this.WrappedObject.Contains((object) key))
    {
      value = (TValue) this.WrappedObject[(object) key];
      return true;
    }
    value = default (TValue);
    return false;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void Add([NotNull] TKey key, [CanBeNull] TValue value)
  {
    this.WrappedObject.Add((object) key, (object) value);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool Remove(TKey key)
  {
    if (!this.WrappedObject.Contains((object) key))
      return false;
    this.WrappedObject.Remove((object) key);
    return true;
  }

  [CanBeNull]
  public TValue this[[NotNull] TKey key]
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return (TValue) this.WrappedObject[(object) key];
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this.WrappedObject[(object) key] = (object) value;
    }
  }

  [NotNull]
  public IReadOnlyCollection<TKey> Keys
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.WrappedObject.Keys.MapCollection<TKey>(this._keySelector);
    }
  }

  [NotNull]
  public IReadOnlyCollection<TValue> Values
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.WrappedObject.Values.MapCollection<TValue>(this._valueSelector);
    }
  }

  ICollection<TKey> IDictionary<TKey, TValue>.Keys
  {
    get => this.WrappedObject.Keys.Map2MutableCollection<TKey>(this._keySelector);
  }

  ICollection<TValue> IDictionary<TKey, TValue>.Values
  {
    get => this.WrappedObject.Values.Map2MutableCollection<TValue>(this._valueSelector);
  }

  IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
  {
    get => this.WrappedObject.Keys.GeneralSelect<TKey>(this._keySelector);
  }

  IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
  {
    get => this.WrappedObject.Values.GeneralSelect<TValue>(this._valueSelector);
  }
}
