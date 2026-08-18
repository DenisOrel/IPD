// Decompiled with JetBrains decompiler
// Type: Intermech.Collections.SerializableReadOnlyDictionary`2
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Collections;

[ComVisible(false)]
[DebuggerDisplay("Count = {Count}")]
[Serializable]
public class SerializableReadOnlyDictionary<TKey, TValue> : 
  ReadOnlyDictionary<TKey, TValue>,
  ISerializable,
  IEnumerable,
  IDeserializationCallback,
  IReadOnlyDictionary<TKey, TValue>,
  IReadOnlyCollection<KeyValuePair<TKey, TValue>>,
  IEnumerable<KeyValuePair<TKey, TValue>>,
  ICapacity
{
  private const string DictionarySerializeName = "Dictionary";

  [CanBeNull]
  public IEqualityComparer<TKey> Comparer { get; }

  public SerializableReadOnlyDictionary(
    [CanBeNull] IDictionary<TKey, TValue> dictionary,
    [CanBeNull] IEqualityComparer<TKey> comparer)
    : base(dictionary)
  {
    this.Comparer = comparer ?? (dictionary is System.Collections.Generic.Dictionary<TKey, TValue> dictionary1 ? dictionary1.Comparer : (IEqualityComparer<TKey>) null);
  }

  public SerializableReadOnlyDictionary(
    [CanBeNull] IEnumerable<KeyValuePair<TKey, TValue>> enumeration = null,
    int capacity = 16 /*0x10*/,
    [CanBeNull] IEqualityComparer<TKey> comparer = null)
  {
    if (!(enumeration is IDictionary<TKey, TValue> dictionary))
      dictionary = (IDictionary<TKey, TValue>) DictionaryFactory.Create<TKey, TValue>(enumeration, capacity, comparer);
    // ISSUE: explicit constructor call
    this.\u002Ector(dictionary, comparer);
  }

  public SerializableReadOnlyDictionary(
    [CanBeNull] IEnumerable<KeyValuePair<TKey, TValue>> enumeration,
    [NotNull] IEqualityComparer<TKey> comparer,
    int capacity = 16 /*0x10*/)
  {
    if (!(enumeration is IDictionary<TKey, TValue> dictionary))
      dictionary = (IDictionary<TKey, TValue>) DictionaryFactory.Create<TKey, TValue>(enumeration, capacity, comparer);
    // ISSUE: explicit constructor call
    this.\u002Ector(dictionary, comparer);
  }

  public SerializableReadOnlyDictionary(
    int capacity,
    [CanBeNull] IEnumerable<KeyValuePair<TKey, TValue>> enumeration = null,
    [CanBeNull] IEqualityComparer<TKey> comparer = null)
  {
    if (!(enumeration is IDictionary<TKey, TValue> dictionary))
      dictionary = (IDictionary<TKey, TValue>) DictionaryFactory.Create<TKey, TValue>(enumeration, capacity, comparer);
    // ISSUE: explicit constructor call
    this.\u002Ector(dictionary, comparer);
  }

  public SerializableReadOnlyDictionary(
    int capacity,
    [NotNull] IEqualityComparer<TKey> comparer,
    [CanBeNull] IEnumerable<KeyValuePair<TKey, TValue>> enumeration = null)
  {
    if (!(enumeration is IDictionary<TKey, TValue> dictionary))
      dictionary = (IDictionary<TKey, TValue>) DictionaryFactory.Create<TKey, TValue>(enumeration, capacity, comparer);
    // ISSUE: explicit constructor call
    this.\u002Ector(dictionary, comparer);
  }

  public SerializableReadOnlyDictionary(
    [NotNull] IEqualityComparer<TKey> comparer,
    [CanBeNull] IEnumerable<KeyValuePair<TKey, TValue>> enumeration = null,
    int capacity = 16 /*0x10*/)
  {
    if (!(enumeration is IDictionary<TKey, TValue> dictionary))
      dictionary = (IDictionary<TKey, TValue>) DictionaryFactory.Create<TKey, TValue>(enumeration, capacity, comparer);
    // ISSUE: explicit constructor call
    this.\u002Ector(dictionary, comparer);
  }

  public SerializableReadOnlyDictionary(
    [NotNull] IEqualityComparer<TKey> comparer,
    int capacity,
    [CanBeNull] IEnumerable<KeyValuePair<TKey, TValue>> enumeration = null)
  {
    if (!(enumeration is IDictionary<TKey, TValue> dictionary))
      dictionary = (IDictionary<TKey, TValue>) DictionaryFactory.Create<TKey, TValue>(enumeration, capacity, comparer);
    // ISSUE: explicit constructor call
    this.\u002Ector(dictionary, comparer);
  }

  [NotNull]
  private static IDictionary<TKey, TValue> GetDictionaryFromSerializationInfo([NotNull] SerializationInfo info)
  {
    return (IDictionary<TKey, TValue>) DictionaryFactory.Create<TKey, TValue>((KeyValuePair<TKey, TValue>[]) info.GetValue("Dictionary", typeof (KeyValuePair<TKey, TValue>[])));
  }

  protected SerializableReadOnlyDictionary([NotNull] SerializationInfo info, StreamingContext context)
    : base(SerializableReadOnlyDictionary<TKey, TValue>.GetDictionaryFromSerializationInfo(info))
  {
    this.OnDeserialization((object) this);
  }

  public virtual void OnDeserialization([CanBeNull] object sender)
  {
  }

  public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    KeyValuePair<TKey, TValue>[] keyValuePairArray = this.AsArray<KeyValuePair<TKey, TValue>>(this.Count);
    info.AddValue("Dictionary", (object) keyValuePairArray);
  }

  [NotNull]
  [DebuggerStepThrough]
  [ContractAnnotation("null => null; notnull => notnull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static implicit operator System.Collections.Generic.Dictionary<TKey, TValue>(
    [CanBeNull] SerializableReadOnlyDictionary<TKey, TValue> readOnlyDictionary)
  {
    return DictionaryFactory.Create<TKey, TValue>((IEnumerable<KeyValuePair<TKey, TValue>>) readOnlyDictionary, readOnlyDictionary?.Comparer);
  }

  [DebuggerStepThrough]
  [CanBeNull]
  [ContractAnnotation("null => null; notnull => notnull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static implicit operator SerializableReadOnlyDictionary<TKey, TValue>(
    [CanBeNull] System.Collections.Generic.Dictionary<TKey, TValue> dictionary)
  {
    return dictionary == null ? (SerializableReadOnlyDictionary<TKey, TValue>) null : new SerializableReadOnlyDictionary<TKey, TValue>((IEnumerable<KeyValuePair<TKey, TValue>>) dictionary);
  }

  public int Capacity
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.Count;
  }
}
