// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.ReadOnlyDictionaryWrapper`2
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Extensions;

[DebuggerDisplay("Count = {Count}")]
[Serializable]
public class ReadOnlyDictionaryWrapper<TKey, TValue> : 
  IReadOnlyDictionary<TKey, TValue>,
  IReadOnlyCollection<KeyValuePair<TKey, TValue>>,
  IEnumerable<KeyValuePair<TKey, TValue>>,
  IEnumerable,
  IEquatable<ReadOnlyDictionaryWrapper<TKey, TValue>>,
  IEquatable<IDictionary<TKey, TValue>>,
  IEquatable<IReadOnlyDictionary<TKey, TValue>>,
  ISerializable
{
  [NotNull]
  private readonly IDictionary<TKey, TValue> _dict;

  public ReadOnlyDictionaryWrapper([NotNull] IDictionary<TKey, TValue> dict) => this._dict = dict;

  protected ReadOnlyDictionaryWrapper([NotNull] SerializationInfo info, StreamingContext context)
  {
    KeyValuePair<TKey, TValue>[] items = info.GetValue<KeyValuePair<TKey, TValue>[]>("Items");
    this._dict = (IDictionary<TKey, TValue>) new Dictionary<TKey, TValue>(items.Length);
    this._dict.AddRange<TKey, TValue>((IEnumerable<KeyValuePair<TKey, TValue>>) items);
  }

  public void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    info.AddValue("Items", (object) this._dict.ToArray<KeyValuePair<TKey, TValue>>(this._dict.Count));
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => this._dict.GetEnumerator();

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  IEnumerator IEnumerable.GetEnumerator() => this._dict.GetEnumerator();

  public int Count
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._dict.Count;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool ContainsKey([NotNull] TKey key) => this._dict.ContainsKey(key);

  public bool TryGetValue([NotNull] TKey key, out TValue value)
  {
    return this._dict.TryGetValue(key, out value);
  }

  [CanBeNull]
  public TValue this[[NotNull] TKey key]
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._dict[key];
  }

  [NotNull]
  public IEnumerable<TKey> Keys
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (IEnumerable<TKey>) this._dict.Keys;
  }

  [NotNull]
  public IEnumerable<TValue> Values
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return (IEnumerable<TValue>) this._dict.Values;
    }
  }

  [NotNull]
  public override string ToString() => this._dict.ToString();

  public override int GetHashCode() => this._dict.GetHashCode();

  public bool Equals([CanBeNull] ReadOnlyDictionaryWrapper<TKey, TValue> other)
  {
    if (other == null)
      return false;
    return other == this || other._dict == this._dict || object.Equals((object) other._dict, (object) this._dict);
  }

  public bool Equals([CanBeNull] IDictionary<TKey, TValue> other)
  {
    if (other == null)
      return false;
    return other == this._dict || object.Equals((object) other, (object) this._dict);
  }

  public bool Equals([CanBeNull] IReadOnlyDictionary<TKey, TValue> other)
  {
    if (other == null)
      return false;
    return other == this._dict || object.Equals((object) other, (object) this._dict);
  }

  public override bool Equals([CanBeNull] object other)
  {
    if (other == null)
      return false;
    if (this == other || this._dict == other)
      return true;
    if (!(other is ReadOnlyDictionaryWrapper<TKey, TValue> dictionaryWrapper))
      return object.Equals(other, (object) this._dict);
    return dictionaryWrapper._dict == this._dict || dictionaryWrapper._dict == this || object.Equals((object) dictionaryWrapper._dict, (object) this._dict);
  }
}
