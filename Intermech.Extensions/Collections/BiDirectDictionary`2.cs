// Decompiled with JetBrains decompiler
// Type: Intermech.Collections.BiDirectDictionary`2
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Collections;

[Serializable]
public class BiDirectDictionary<TKey, TValue> : 
  IDictionary<TKey, TValue>,
  ICollection<KeyValuePair<TKey, TValue>>,
  IEnumerable<KeyValuePair<TKey, TValue>>,
  IEnumerable,
  IDictionary,
  ICollection,
  IDeserializationCallback
{
  [NotNull]
  private readonly Dictionary<TKey, TValue> _keyValueDic = new Dictionary<TKey, TValue>();
  [CanBeNull]
  private IReadOnlyDictionary<TKey, TValue> _publicValueByKey;
  [NotNull]
  [NonSerialized]
  private readonly Dictionary<TValue, TKey> _valueKeyDic = new Dictionary<TValue, TKey>();
  [CanBeNull]
  private IReadOnlyDictionary<TValue, TKey> _publicKeyByValue;

  [NotNull]
  public IReadOnlyDictionary<TKey, TValue> ValueByKey
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._publicValueByKey ?? (this._publicValueByKey = this._keyValueDic.GetReadOnly<TKey, TValue>());
    }
  }

  [NotNull]
  public IReadOnlyDictionary<TValue, TKey> KeyByValue
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._publicKeyByValue ?? (this._publicKeyByValue = this._valueKeyDic.GetReadOnly<TValue, TKey>());
    }
  }

  [NotNull]
  public Dictionary<TKey, TValue>.KeyCollection Keys
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._keyValueDic.Keys;
  }

  [NotNull]
  public Dictionary<TKey, TValue>.ValueCollection Values
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._keyValueDic.Values;
  }

  [NotNull]
  public TValue this[[NotNull] TKey key]
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._keyValueDic[key];
    [MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this._keyValueDic[key] = !this._valueKeyDic.ContainsKey(value) ? value : throw new Exception("Value already in dictionary: " + (object) value);
      this._valueKeyDic[value] = key;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool TryGetValue([NotNull] TKey key, [CanBeNull] out TValue value)
  {
    return this._keyValueDic.TryGetValue(key, out value);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool TryGetKey([NotNull] TValue value, [CanBeNull] out TKey key)
  {
    return this._valueKeyDic.TryGetValue(value, out key);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public Dictionary<TKey, TValue>.Enumerator GetEnumerator() => this._keyValueDic.GetEnumerator();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void Add([NotNull] TKey key, [NotNull] TValue value)
  {
    if (this.ContainsKey(key))
      throw new ArgumentException("Key already in dictionary: " + (object) value);
    if (this.ContainsValue(value))
      throw new ArgumentException("Value already in dictionary: " + (object) value);
    this._keyValueDic.Add(key, value);
    this._valueKeyDic.Add(value, key);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool Remove([NotNull] TKey key)
  {
    TValue obj = this._keyValueDic[key];
    return this.Remove(key, obj);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool RemoveValue([NotNull] TValue value) => this.Remove(this._valueKeyDic[value], value);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private bool Remove([NotNull] TKey key, [NotNull] TValue value)
  {
    return this.ContainsKey(key) && this.ContainsValue(value) && this._keyValueDic.Remove(key) && this._valueKeyDic.Remove(value);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool ContainsKey([NotNull] TKey key) => this._keyValueDic.ContainsKey(key);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool ContainsValue([NotNull] TValue value) => this._valueKeyDic.ContainsKey(value);

  private void RebuildValueToKey()
  {
    this._valueKeyDic.Clear();
    foreach (KeyValuePair<TKey, TValue> keyValuePair in this._keyValueDic)
      this._valueKeyDic.Add(keyValuePair.Value, keyValuePair.Key);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public TKey GetKeyByValue([NotNull] TValue value) => this._valueKeyDic[value];

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void Add(KeyValuePair<TKey, TValue> item) => this.Add(item.Key, item.Value);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void Clear()
  {
    this._keyValueDic.Clear();
    this._valueKeyDic.Clear();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool Contains(KeyValuePair<TKey, TValue> item)
  {
    return this._keyValueDic.ContainsKey(item.Key) && object.Equals((object) this._keyValueDic[item.Key], (object) item.Value);
  }

  public int Count
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._keyValueDic.Count;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool Remove(KeyValuePair<TKey, TValue> item)
  {
    return this._keyValueDic.Remove(item.Key) && this._valueKeyDic.Remove(item.Value);
  }

  public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
  {
    int index = arrayIndex;
    foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
    {
      array[index] = keyValuePair;
      ++index;
    }
  }

  public bool IsReadOnly => true;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
  {
    return (IEnumerator<KeyValuePair<TKey, TValue>>) this._keyValueDic.GetEnumerator();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this._keyValueDic.GetEnumerator();

  public void OnDeserialization([CanBeNull] object sender) => this.RebuildValueToKey();

  public void CopyTo(Array array, int index)
  {
    this.CopyTo((KeyValuePair<TKey, TValue>[]) array, index);
  }

  bool ICollection.IsSynchronized => ((ICollection) this._keyValueDic).IsSynchronized;

  object ICollection.SyncRoot => ((ICollection) this._keyValueDic).SyncRoot;

  bool IDictionary.IsFixedSize => ((IDictionary) this._keyValueDic).IsFixedSize;

  int ICollection.Count => this.Count;

  public void Add([NotNull] object key, [NotNull] object value)
  {
    this.Add((TKey) key, (TValue) value);
  }

  public bool Contains([NotNull] object key) => this.ContainsKey((TKey) key);

  IDictionaryEnumerator IDictionary.GetEnumerator() => (IDictionaryEnumerator) this.GetEnumerator();

  ICollection IDictionary.Keys => (ICollection) this.Keys;

  public void Remove([NotNull] object key) => this.Remove((TKey) key);

  ICollection IDictionary.Values => (ICollection) this.Values;

  [NotNull]
  object IDictionary.this[object key]
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (object) this[(TKey) key];
    set => this[(TKey) key] = (TValue) value;
  }

  ICollection<TKey> IDictionary<TKey, TValue>.Keys => (ICollection<TKey>) this.Keys;

  bool IDictionary<TKey, TValue>.Remove([NotNull] TKey key) => this.Remove(key);

  ICollection<TValue> IDictionary<TKey, TValue>.Values => (ICollection<TValue>) this.Values;
}
