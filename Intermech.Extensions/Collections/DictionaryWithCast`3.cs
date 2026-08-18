// Decompiled with JetBrains decompiler
// Type: Intermech.Collections.DictionaryWithCast`3
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Collections;

[ComVisible(false)]
[DebuggerDisplay("Count = {Count}")]
[Serializable]
public class DictionaryWithCast<TKey, TValue, TMappedValue> : 
  Dictionary<TKey, TValue>,
  IReadOnlyDictionary<TKey, TMappedValue>,
  IReadOnlyCollection<KeyValuePair<TKey, TMappedValue>>,
  IEnumerable<KeyValuePair<TKey, TMappedValue>>,
  IEnumerable,
  ISerializable,
  IDeserializationCallback
  where TValue : TMappedValue
{
  [CanBeNull]
  private DictionaryCastValuesAdapter<TKey, TValue, TMappedValue> _dictionaryCastValuesAdapter;

  [NotNull]
  public IReadOnlyDictionary<TKey, TMappedValue> Mapped
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return (IReadOnlyDictionary<TKey, TMappedValue>) this._dictionaryCastValuesAdapter ?? (IReadOnlyDictionary<TKey, TMappedValue>) (this._dictionaryCastValuesAdapter = new DictionaryCastValuesAdapter<TKey, TValue, TMappedValue>((IDictionary<TKey, TValue>) this));
    }
  }

  public DictionaryWithCast(
    [CanBeNull] IEnumerable<KeyValuePair<TKey, TValue>> enumeration = null,
    int capacity = 16 /*0x10*/,
    [CanBeNull] IEqualityComparer<TKey> comparer = null)
    : base(enumeration.GetRecommendedCapacity<KeyValuePair<TKey, TValue>>(capacity), comparer)
  {
    if (enumeration == null)
      return;
    this.AddRange<KeyValuePair<TKey, TValue>>(enumeration);
  }

  public DictionaryWithCast(
    [CanBeNull] IEnumerable<KeyValuePair<TKey, TValue>> enumeration,
    [CanBeNull] IEqualityComparer<TKey> comparer,
    int capacity = 16 /*0x10*/)
    : base(enumeration.GetRecommendedCapacity<KeyValuePair<TKey, TValue>>(capacity), comparer)
  {
    if (enumeration == null)
      return;
    this.AddRange<KeyValuePair<TKey, TValue>>(enumeration);
  }

  public DictionaryWithCast(
    int capacity,
    [CanBeNull] IEnumerable<KeyValuePair<TKey, TValue>> enumeration = null,
    [CanBeNull] IEqualityComparer<TKey> comparer = null)
    : base(enumeration.GetRecommendedCapacity<KeyValuePair<TKey, TValue>>(capacity), comparer)
  {
    if (enumeration == null)
      return;
    this.AddRange<KeyValuePair<TKey, TValue>>(enumeration);
  }

  public DictionaryWithCast(
    int capacity,
    [NotNull] IEqualityComparer<TKey> comparer,
    [CanBeNull] IEnumerable<KeyValuePair<TKey, TValue>> enumeration = null)
    : base(enumeration.GetRecommendedCapacity<KeyValuePair<TKey, TValue>>(capacity), comparer)
  {
    if (enumeration == null)
      return;
    this.AddRange<KeyValuePair<TKey, TValue>>(enumeration);
  }

  public DictionaryWithCast(
    [NotNull] IEqualityComparer<TKey> comparer,
    [CanBeNull] IEnumerable<KeyValuePair<TKey, TValue>> enumeration = null,
    int capacity = 16 /*0x10*/)
    : base(enumeration.GetRecommendedCapacity<KeyValuePair<TKey, TValue>>(capacity), comparer)
  {
    if (enumeration == null)
      return;
    this.AddRange<KeyValuePair<TKey, TValue>>(enumeration);
  }

  public DictionaryWithCast(
    [NotNull] IEqualityComparer<TKey> comparer,
    int capacity,
    [CanBeNull] IEnumerable<KeyValuePair<TKey, TValue>> enumeration = null)
    : base(enumeration.GetRecommendedCapacity<KeyValuePair<TKey, TValue>>(capacity), comparer)
  {
    if (enumeration == null)
      return;
    this.AddRange<KeyValuePair<TKey, TValue>>(enumeration);
  }

  protected DictionaryWithCast([NotNull] SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  IEnumerator<KeyValuePair<TKey, TMappedValue>> IEnumerable<KeyValuePair<TKey, TMappedValue>>.GetEnumerator()
  {
    return this.Mapped.GetEnumerator();
  }

  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool TryGetValue(TKey key, out TMappedValue value)
  {
    return this.Mapped.TryGetValue(key, out value);
  }

  [CanBeNull]
  TMappedValue IReadOnlyDictionary<TKey, TMappedValue>.this[[NotNull] TKey key]
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return (TMappedValue) this[key];
    }
  }

  [NotNull]
  [ItemNotNull]
  IEnumerable<TKey> IReadOnlyDictionary<TKey, TMappedValue>.Keys
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return (IEnumerable<TKey>) this.Keys;
    }
  }

  [NotNull]
  [ItemCanBeNull]
  IEnumerable<TMappedValue> IReadOnlyDictionary<TKey, TMappedValue>.Values
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.Values.Cast<TMappedValue>();
    }
  }
}
