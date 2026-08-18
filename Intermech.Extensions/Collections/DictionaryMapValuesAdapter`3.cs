// Decompiled with JetBrains decompiler
// Type: Intermech.Collections.DictionaryMapValuesAdapter`3
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

#nullable disable
namespace Intermech.Collections;

[ComVisible(false)]
[DebuggerDisplay("Count = {Count}")]
internal sealed class DictionaryMapValuesAdapter<TKey, TValue, TMappedValue> : 
  DictionaryAdapterBase<TKey, TValue>,
  IReadOnlyDictionary<TKey, TMappedValue>,
  IReadOnlyCollection<KeyValuePair<TKey, TMappedValue>>,
  IEnumerable<KeyValuePair<TKey, TMappedValue>>,
  IEnumerable,
  IEquatable<IDictionary<TKey, TValue>>
{
  [NotNull]
  private readonly Func<TValue, TMappedValue> _selector;

  public DictionaryMapValuesAdapter(
    [NotNull] IDictionary<TKey, TValue> sourceDictionary,
    [NotNull] Func<TValue, TMappedValue> selector)
    : base(sourceDictionary)
  {
    this._selector = selector;
  }

  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  IEnumerator<KeyValuePair<TKey, TMappedValue>> IEnumerable<KeyValuePair<TKey, TMappedValue>>.GetEnumerator()
  {
    return this.Dictionary.Select<KeyValuePair<TKey, TValue>, KeyValuePair<TKey, TMappedValue>>((Func<KeyValuePair<TKey, TValue>, KeyValuePair<TKey, TMappedValue>>) (keyValue => new KeyValuePair<TKey, TMappedValue>(keyValue.Key, this._selector(keyValue.Value)))).GetEnumerator();
  }

  [DebuggerStepThrough]
  [ContractAnnotation("=> true, value: notnull; => false, value: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool TryGetValue([NotNull] TKey key, [CanBeNull] out TMappedValue value)
  {
    TValue obj;
    if (this.Dictionary.TryGetValue(key, out obj))
    {
      value = this._selector(obj);
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
      return this._selector(this.Dictionary[key]);
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
      return this.Dictionary.Values.Select<TValue, TMappedValue>((Func<TValue, TMappedValue>) (valueClass => this._selector(valueClass)));
    }
  }
}
