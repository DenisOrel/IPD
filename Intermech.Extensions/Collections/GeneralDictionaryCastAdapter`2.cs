// Decompiled with JetBrains decompiler
// Type: Intermech.Collections.GeneralDictionaryCastAdapter`2
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
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Collections;

[Serializable]
internal sealed class GeneralDictionaryCastAdapter<TKey, TValue>([NotNull] IDictionary dictionary) : 
  GeneralDictionaryAdapterBase(dictionary),
  IDictionary,
  ICollection,
  IEnumerable,
  IDictionary<TKey, TValue>,
  ICollection<KeyValuePair<TKey, TValue>>,
  IEnumerable<KeyValuePair<TKey, TValue>>,
  IReadOnlyDictionary<TKey, TValue>,
  IReadOnlyCollection<KeyValuePair<TKey, TValue>>,
  IEquatable<IDictionary>,
  ISerializable
{
  private GeneralDictionaryCastAdapter([NotNull] SerializationInfo info, StreamingContext context)
    : this((IDictionary) DictionaryFactory.Create<TKey, TValue>((KeyValuePair<TKey, TValue>[]) info.GetValue("AsArray", typeof (KeyValuePair<TKey, TValue>[])) ?? throw new KeyNotFoundException("AsArray")))
  {
  }

  protected override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    info.AddValue("AsArray", (object) this.Dictionary.Cast<KeyValuePair<TKey, TValue>>().AsArray<KeyValuePair<TKey, TValue>>(this.Dictionary.Count));
  }

  public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
  {
    return this.Dictionary.Cast<KeyValuePair<TKey, TValue>>().GetEnumerator();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void Add(KeyValuePair<TKey, TValue> item)
  {
    this.Dictionary.Add((object) item.Key, (object) item.Value);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool Contains(KeyValuePair<TKey, TValue> item)
  {
    if (!this.Dictionary.Contains((object) item.Key))
      return false;
    object obj = this.Dictionary[(object) item.Key];
    return obj == null ? (object) item.Value == null : obj.Equals((object) item.Value);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
  {
    this.Dictionary.CopyTo((Array) array, arrayIndex);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool Remove(KeyValuePair<TKey, TValue> item)
  {
    if (!this.Contains(item))
      return false;
    this.Dictionary.Remove((object) item.Key);
    return true;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool ContainsKey([NotNull] TKey key) => this.Dictionary.Contains((object) key);

  [ContractAnnotation("=> true, value: notnull; => false, value: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool TryGetValue([NotNull] TKey key, [CanBeNull] out TValue value)
  {
    if (this.Dictionary.Contains((object) key))
    {
      value = (TValue) this.Dictionary[(object) key];
      return true;
    }
    value = default (TValue);
    return false;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void Add([NotNull] TKey key, [CanBeNull] TValue value)
  {
    this.Dictionary.Add((object) key, (object) value);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool Remove(TKey key)
  {
    if (!this.Dictionary.Contains((object) key))
      return false;
    this.Dictionary.Remove((object) key);
    return true;
  }

  [CanBeNull]
  public TValue this[[NotNull] TKey key]
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return (TValue) this.Dictionary[(object) key];
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this.Dictionary[(object) key] = (object) value;
    }
  }

  [NotNull]
  public IReadOnlyCollection<TKey> Keys
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.Dictionary.Keys.CastCollection<TKey>();
    }
  }

  [NotNull]
  public IReadOnlyCollection<TValue> Values
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.Dictionary.Values.CastCollection<TValue>();
    }
  }

  ICollection<TKey> IDictionary<TKey, TValue>.Keys
  {
    get => this.Dictionary.Keys.Cast2MutableCollection<TKey>();
  }

  ICollection<TValue> IDictionary<TKey, TValue>.Values
  {
    get => this.Dictionary.Values.Cast2MutableCollection<TValue>();
  }

  IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys => this.Dictionary.Keys.Cast<TKey>();

  IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
  {
    get => this.Dictionary.Values.Cast<TValue>();
  }
}
