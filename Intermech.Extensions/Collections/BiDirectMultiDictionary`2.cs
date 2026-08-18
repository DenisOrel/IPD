// Decompiled with JetBrains decompiler
// Type: Intermech.Collections.BiDirectMultiDictionary`2
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
public class BiDirectMultiDictionary<TKey, TValue> : 
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
  private readonly Dictionary<TValue, List<TKey>> _valueKeyDic = new Dictionary<TValue, List<TKey>>();
  [CanBeNull]
  private IReadOnlyDictionary<TValue, List<TKey>> _publicKeysByValue;

  [NotNull]
  public IReadOnlyDictionary<TKey, TValue> ValueByKey
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._publicValueByKey ?? (this._publicValueByKey = this._keyValueDic.GetReadOnly<TKey, TValue>());
    }
  }

  [NotNull]
  public IReadOnlyDictionary<TValue, List<TKey>> KeysByValue
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._publicKeysByValue ?? (this._publicKeysByValue = this._valueKeyDic.GetReadOnly<TValue, List<TKey>>());
    }
  }

  [NotNull]
  public Dictionary<TKey, TValue>.KeyCollection Keys => this._keyValueDic.Keys;

  [NotNull]
  public Dictionary<TKey, TValue>.ValueCollection Values => this._keyValueDic.Values;

  [CanBeNull]
  public TValue this[[NotNull] TKey key]
  {
    get => this._keyValueDic[key];
    set
    {
      this._keyValueDic[key] = !this._valueKeyDic.ContainsKey(value) ? value : throw new Exception("Value already in dictionary: " + (object) value);
      if (!this._valueKeyDic.ContainsKey(value))
        this._valueKeyDic[value] = new List<TKey>();
      this._valueKeyDic[value].Add(key);
    }
  }

  public bool TryGetValue([NotNull] TKey key, out TValue value)
  {
    return this._keyValueDic.TryGetValue(key, out value);
  }

  public bool TryGetKeys([NotNull] TValue value, [CanBeNull] out List<TKey> keys)
  {
    return this._valueKeyDic.TryGetValue(value, out keys);
  }

  public Dictionary<TKey, TValue>.Enumerator GetEnumerator() => this._keyValueDic.GetEnumerator();

  public void Add([NotNull] TKey key, [NotNull] TValue value)
  {
    if (this.ContainsKey(key))
      throw new Exception("Key already in dictionary: " + (object) value);
    if (this.ContainsValue(value))
      throw new Exception("Value already in dictionary: " + (object) value);
    this._keyValueDic.Add(key, value);
    List<TKey> keyList;
    if (!this._valueKeyDic.TryGetValue(value, out keyList) || keyList == null)
    {
      keyList = new List<TKey>();
      this._valueKeyDic[value] = keyList;
    }
    keyList.Add(key);
  }

  public bool Remove([NotNull] TKey key)
  {
    TValue obj;
    return this._keyValueDic.TryGetValue(key, out obj) && this.Remove(key, obj);
  }

  public bool RemoveValues([NotNull] TValue value)
  {
    bool flag = false;
    List<TKey> keyList;
    if (this._valueKeyDic.TryGetValue(value, out keyList) && keyList != null)
    {
      foreach (TKey key in keyList)
        flag |= this._keyValueDic.Remove(key);
      this._valueKeyDic.Remove(value);
    }
    return flag;
  }

  private bool Remove([NotNull] TKey key, [NotNull] TValue value)
  {
    List<TKey> keyList;
    if (this._valueKeyDic.TryGetValue(value, out keyList) && keyList != null)
    {
      keyList.Remove(key);
      if (keyList.Count == 0)
        this._valueKeyDic.Remove(value);
    }
    this._keyValueDic.Remove(key);
    return this._keyValueDic.Remove(key) && this._valueKeyDic.Remove(value);
  }

  public bool ContainsKey([NotNull] TKey key) => this._keyValueDic.ContainsKey(key);

  public bool ContainsValue([NotNull] TValue value) => this._valueKeyDic.ContainsKey(value);

  private void RebuildValueToKey()
  {
    this._valueKeyDic.Clear();
    foreach (KeyValuePair<TKey, TValue> keyValuePair in this._keyValueDic)
    {
      List<TKey> keyList;
      if (!this._valueKeyDic.TryGetValue(keyValuePair.Value, out keyList) || keyList == null)
      {
        keyList = new List<TKey>();
        this._valueKeyDic[keyValuePair.Value] = keyList;
      }
      keyList.Add(keyValuePair.Key);
    }
  }

  public void Add(KeyValuePair<TKey, TValue> item) => this.Add(item.Key, item.Value);

  public void Clear()
  {
    this._keyValueDic.Clear();
    this._valueKeyDic.Clear();
  }

  public bool Contains(KeyValuePair<TKey, TValue> item)
  {
    return this._keyValueDic.ContainsKey(item.Key) && object.Equals((object) this._keyValueDic[item.Key], (object) item.Value);
  }

  public int Count => this._keyValueDic.Count;

  public bool Remove(KeyValuePair<TKey, TValue> item) => this.Remove(item.Key, item.Value);

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

  IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
  {
    return (IEnumerator<KeyValuePair<TKey, TValue>>) this._keyValueDic.GetEnumerator();
  }

  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this._keyValueDic.GetEnumerator();

  public void OnDeserialization([CanBeNull] object sender) => this.RebuildValueToKey();

  public void CopyTo(Array array, int index)
  {
    if (!(array is KeyValuePair<TKey, TValue>[] array1))
      throw new InvalidOperationException();
    this.CopyTo(array1, index);
  }

  bool ICollection.IsSynchronized => ((ICollection) this._keyValueDic).IsSynchronized;

  object ICollection.SyncRoot => ((ICollection) this._keyValueDic).SyncRoot;

  bool IDictionary.IsFixedSize => ((IDictionary) this._keyValueDic).IsFixedSize;

  int ICollection.Count => this.Count;

  public void Add(object key, [NotNull] object value) => this.Add((TKey) key, (TValue) value);

  public bool Contains(object key) => this._keyValueDic.ContainsKey((TKey) key);

  IDictionaryEnumerator IDictionary.GetEnumerator() => (IDictionaryEnumerator) this.GetEnumerator();

  ICollection IDictionary.Keys => (ICollection) this.Keys;

  public void Remove(object key) => this.Remove((TKey) key);

  ICollection IDictionary.Values => (ICollection) this.Values;

  [CanBeNull]
  public object this[[NotNull] object key]
  {
    get => (object) this[(TKey) key];
    set => this[(TKey) key] = (TValue) value;
  }

  ICollection<TKey> IDictionary<TKey, TValue>.Keys => (ICollection<TKey>) this.Keys;

  bool IDictionary<TKey, TValue>.Remove([NotNull] TKey key) => this.Remove(key);

  ICollection<TValue> IDictionary<TKey, TValue>.Values => (ICollection<TValue>) this.Values;
}
