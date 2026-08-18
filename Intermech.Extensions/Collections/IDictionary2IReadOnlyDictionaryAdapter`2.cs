// Decompiled with JetBrains decompiler
// Type: Intermech.Collections.IDictionary2IReadOnlyDictionaryAdapter`2
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Collections;

[Serializable]
public sealed class IDictionary2IReadOnlyDictionaryAdapter<TKey, TValue> : 
  IReadOnlyDictionary<TKey, TValue>,
  IReadOnlyCollection<KeyValuePair<TKey, TValue>>,
  IEnumerable<KeyValuePair<TKey, TValue>>,
  IEnumerable,
  IEquatable<IDictionary2IReadOnlyDictionaryAdapter<TKey, TValue>>,
  IEquatable<IDictionary<TKey, TValue>>,
  IEquatable<IDictionary>,
  ISerializable,
  ICapacity
{
  private const string SerializeArrayName = "AsArray";
  [NotNull]
  private readonly IDictionary<TKey, TValue> _dictionary;

  public IDictionary2IReadOnlyDictionaryAdapter([NotNull] IDictionary<TKey, TValue> dictionary)
  {
    this._dictionary = dictionary;
  }

  private IDictionary2IReadOnlyDictionaryAdapter([NotNull] SerializationInfo info, StreamingContext context)
  {
    this._dictionary = (IDictionary<TKey, TValue>) DictionaryFactory.Create<TKey, TValue>((KeyValuePair<TKey, TValue>[]) info.GetValue("AsArray", typeof (KeyValuePair<TKey, TValue>[])) ?? throw new KeyNotFoundException("AsArray"));
  }

  public void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    info.AddValue("AsArray", (object) this._dictionary.AsArray<KeyValuePair<TKey, TValue>>());
  }

  public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
  {
    return this._dictionary.GetEnumerator();
  }

  IEnumerator IEnumerable.GetEnumerator() => this._dictionary.GetEnumerator();

  public int Count
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._dictionary.Count;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool ContainsKey([NotNull] TKey key) => this._dictionary.ContainsKey(key);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool TryGetValue(TKey key, out TValue value)
  {
    return this._dictionary.TryGetValue(key, out value);
  }

  [CanBeNull]
  public TValue this[[NotNull] TKey key]
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._dictionary[key];
  }

  public IEnumerable<TKey> Keys
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return (IEnumerable<TKey>) this._dictionary.Keys;
    }
  }

  public IEnumerable<TValue> Values
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return (IEnumerable<TValue>) this._dictionary.Values;
    }
  }

  public int Capacity
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._dictionary.Count;
  }

  public override int GetHashCode() => this._dictionary.GetHashCode();

  public override string ToString() => this._dictionary.ToString();

  public override bool Equals([CanBeNull] object obj)
  {
    if (obj == null)
      return false;
    return this == obj || this._dictionary == obj || obj is IDictionary2IReadOnlyDictionaryAdapter<TKey, TValue> other1 && this.Equals(other1) || obj is IDictionary<TKey, TValue> other2 && this.Equals(other2) || obj is IDictionary other3 && this.Equals(other3) || obj.Equals((object) this._dictionary);
  }

  public bool Equals(
    [CanBeNull] IDictionary2IReadOnlyDictionaryAdapter<TKey, TValue> other)
  {
    if (other == null)
      return false;
    return this == other || this._dictionary == other || other._dictionary.Equals((object) this._dictionary);
  }

  public bool Equals([CanBeNull] IDictionary<TKey, TValue> other)
  {
    if (other == null)
      return false;
    return this == other || this._dictionary == other || other.Equals((object) this._dictionary);
  }

  public bool Equals([CanBeNull] IDictionary other)
  {
    if (other == null)
      return false;
    if (this == other || this._dictionary == other)
      return true;
    return this._dictionary is IDictionary dictionary && other.Equals((object) dictionary);
  }
}
