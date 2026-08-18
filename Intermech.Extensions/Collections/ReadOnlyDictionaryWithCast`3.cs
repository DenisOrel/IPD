// Decompiled with JetBrains decompiler
// Type: Intermech.Collections.ReadOnlyDictionaryWithCast`3
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
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
public class ReadOnlyDictionaryWithCast<TKey, TValue, TMappedValue> : 
  SerializableReadOnlyDictionary<TKey, TValue>,
  IReadOnlyDictionary<TKey, TMappedValue>,
  IReadOnlyCollection<KeyValuePair<TKey, TMappedValue>>,
  IEnumerable<KeyValuePair<TKey, TMappedValue>>,
  IEnumerable,
  ISerializable,
  IDeserializationCallback
  where TValue : TMappedValue
{
  [CanBeNull]
  private ReadOnlyDictionaryCastValuesAdapter<TKey, TValue, TMappedValue> _readOnlyDictionaryCastValuesAdapter;

  [NotNull]
  public IReadOnlyDictionary<TKey, TMappedValue> Mapped
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return (IReadOnlyDictionary<TKey, TMappedValue>) this._readOnlyDictionaryCastValuesAdapter ?? (IReadOnlyDictionary<TKey, TMappedValue>) (this._readOnlyDictionaryCastValuesAdapter = new ReadOnlyDictionaryCastValuesAdapter<TKey, TValue, TMappedValue>((IReadOnlyDictionary<TKey, TValue>) this));
    }
  }

  public ReadOnlyDictionaryWithCast(
    [CanBeNull] IEnumerable<KeyValuePair<TKey, TValue>> enumeration = null,
    int capacity = 16 /*0x10*/,
    [CanBeNull] IEqualityComparer<TKey> comparer = null)
    : base(enumeration, capacity, comparer)
  {
  }

  public ReadOnlyDictionaryWithCast(
    [CanBeNull] IEnumerable<KeyValuePair<TKey, TValue>> enumeration,
    [CanBeNull] IEqualityComparer<TKey> comparer,
    int capacity = 16 /*0x10*/)
    : base(enumeration, capacity, comparer)
  {
  }

  public ReadOnlyDictionaryWithCast(
    int capacity,
    [CanBeNull] IEnumerable<KeyValuePair<TKey, TValue>> enumeration = null,
    [CanBeNull] IEqualityComparer<TKey> comparer = null)
    : base(enumeration, capacity, comparer)
  {
  }

  public ReadOnlyDictionaryWithCast(
    int capacity,
    [NotNull] IEqualityComparer<TKey> comparer,
    [CanBeNull] IEnumerable<KeyValuePair<TKey, TValue>> enumeration = null)
    : base(enumeration, capacity, comparer)
  {
  }

  public ReadOnlyDictionaryWithCast(
    [NotNull] IEqualityComparer<TKey> comparer,
    [CanBeNull] IEnumerable<KeyValuePair<TKey, TValue>> enumeration = null,
    int capacity = 16 /*0x10*/)
    : base(enumeration, capacity, comparer)
  {
  }

  public ReadOnlyDictionaryWithCast(
    [NotNull] IEqualityComparer<TKey> comparer,
    int capacity,
    [CanBeNull] IEnumerable<KeyValuePair<TKey, TValue>> enumeration = null)
    : base(enumeration, capacity, comparer)
  {
  }

  protected ReadOnlyDictionaryWithCast([NotNull] SerializationInfo info, StreamingContext context)
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
