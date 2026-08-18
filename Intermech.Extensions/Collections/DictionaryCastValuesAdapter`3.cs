// Decompiled with JetBrains decompiler
// Type: Intermech.Collections.DictionaryCastValuesAdapter`3
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
internal sealed class DictionaryCastValuesAdapter<TKey, TValue, TMappedValue> : 
  DictionaryAdapterBase<TKey, TValue>,
  IReadOnlyDictionary<TKey, TMappedValue>,
  IReadOnlyCollection<KeyValuePair<TKey, TMappedValue>>,
  IEnumerable<KeyValuePair<TKey, TMappedValue>>,
  IEnumerable,
  IEquatable<IDictionary<TKey, TValue>>,
  ISerializable
  where TValue : TMappedValue
{
  public DictionaryCastValuesAdapter([NotNull] IDictionary<TKey, TValue> sourceDictionary)
    : base(sourceDictionary)
  {
  }

  private DictionaryCastValuesAdapter([NotNull] SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  IEnumerator<KeyValuePair<TKey, TMappedValue>> IEnumerable<KeyValuePair<TKey, TMappedValue>>.GetEnumerator()
  {
    return this.Dictionary.Select<KeyValuePair<TKey, TValue>, KeyValuePair<TKey, TMappedValue>>((Func<KeyValuePair<TKey, TValue>, KeyValuePair<TKey, TMappedValue>>) (keyValue => new KeyValuePair<TKey, TMappedValue>(keyValue.Key, (TMappedValue) keyValue.Value))).GetEnumerator();
  }

  [DebuggerStepThrough]
  [ContractAnnotation("=> true, value: notnull; => false, value: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool TryGetValue([NotNull] TKey key, [CanBeNull] out TMappedValue value)
  {
    TValue obj;
    if (this.Dictionary.TryGetValue(key, out obj))
    {
      value = (TMappedValue) obj;
      return true;
    }
    value = default (TMappedValue);
    return false;
  }

  [CanBeNull]
  TMappedValue IReadOnlyDictionary<TKey, TMappedValue>.this[[NotNull] TKey key]
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return (TMappedValue) this.Dictionary[key];
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
