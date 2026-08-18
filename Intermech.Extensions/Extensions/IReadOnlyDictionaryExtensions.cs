// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.IReadOnlyDictionaryExtensions
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public static class IReadOnlyDictionaryExtensions
{
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TValue GetOrDefaultReadOnly<TKey, TValue>(
    [CanBeNull] this IReadOnlyDictionary<TKey, TValue> dictionary,
    [NotNull] TKey key,
    [CanBeNull] TValue defaultValue = null)
  {
    TValue obj;
    return dictionary == null || !dictionary.TryGetValue(key, out obj) ? defaultValue : obj;
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TValue GetOrDefaultReadOnly<TKey, TValue>(
    [CanBeNull] this IReadOnlyDictionary<TKey, TValue> dictionary,
    [NotNull] TKey key,
    [NotNull] Func<TKey, TValue> getDefaultValueFunc)
  {
    TValue obj;
    return dictionary == null || !dictionary.TryGetValue(key, out obj) ? getDefaultValueFunc(key) : obj;
  }

  [ContractAnnotation("throwExceptIfNull:true => NotNull; source:null => null; => NotNull")]
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyDictionary<TKey, TOutput> CastValuesReadOnlyDictionary<TKey, TSource, TOutput>(
    [CanBeNull] this IReadOnlyDictionary<TKey, TSource> source,
    bool throwExceptIfNull = true)
    where TSource : TOutput
  {
    if (throwExceptIfNull)
      Intermech.Diagnostics.Check.ArgumentNotNull<IReadOnlyDictionary<TKey, TSource>>(source, nameof (source));
    else if (source == null)
      return (IReadOnlyDictionary<TKey, TOutput>) null;
    return source is IReadOnlyDictionary<TKey, TOutput> readOnlyDictionary ? readOnlyDictionary : (IReadOnlyDictionary<TKey, TOutput>) new IReadOnlyDictionaryExtensions.CastReadOnlyDictionaryAdapter<TKey, TSource, TOutput>(source);
  }

  [ContractAnnotation("throwExceptIfNull:true => NotNull; source:null => null; => NotNull")]
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyDictionary<TKey, TOutput> SelectValuesReadOnlyDictionary<TKey, TSource, TOutput>(
    [CanBeNull] this IReadOnlyDictionary<TKey, TSource> source,
    [NotNull] Func<TSource, TOutput> selector,
    bool throwExceptIfNull = true)
  {
    if (throwExceptIfNull)
      Intermech.Diagnostics.Check.ArgumentNotNull<IReadOnlyDictionary<TKey, TSource>>(source, nameof (source));
    else if (source == null)
      return (IReadOnlyDictionary<TKey, TOutput>) null;
    return source is IReadOnlyDictionary<TKey, TOutput> readOnlyDictionary ? readOnlyDictionary : (IReadOnlyDictionary<TKey, TOutput>) new IReadOnlyDictionaryExtensions.SelectReadOnlyDictionaryAdapter<TKey, TSource, TOutput>(source, selector);
  }

  private sealed class CastReadOnlyDictionaryAdapter<TKey, TSource, TOutput> : 
    IEnumerable<KeyValuePair<TKey, TOutput>>,
    IEnumerable,
    IReadOnlyDictionary<TKey, TOutput>,
    IReadOnlyCollection<KeyValuePair<TKey, TOutput>>
    where TSource : TOutput
  {
    [NotNull]
    private readonly IReadOnlyDictionary<TKey, TSource> _sourceDictionary;

    public CastReadOnlyDictionaryAdapter([NotNull] IReadOnlyDictionary<TKey, TSource> source)
    {
      this._sourceDictionary = source;
    }

    public IEnumerator<KeyValuePair<TKey, TOutput>> GetEnumerator()
    {
      return this._sourceDictionary.Select<KeyValuePair<TKey, TSource>, KeyValuePair<TKey, TOutput>>((Func<KeyValuePair<TKey, TSource>, KeyValuePair<TKey, TOutput>>) (keyValue => new KeyValuePair<TKey, TOutput>(keyValue.Key, (TOutput) keyValue.Value))).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this._sourceDictionary.Select<KeyValuePair<TKey, TSource>, KeyValuePair<TKey, TOutput>>((Func<KeyValuePair<TKey, TSource>, KeyValuePair<TKey, TOutput>>) (keyValue => new KeyValuePair<TKey, TOutput>(keyValue.Key, (TOutput) keyValue.Value))).GetEnumerator();
    }

    public int Count => this._sourceDictionary.Count;

    public bool ContainsKey([NotNull] TKey key) => this._sourceDictionary.ContainsKey(key);

    [ContractAnnotation("value:NotNull => true; => value:null")]
    public bool TryGetValue([NotNull] TKey key, [CanBeNull] out TOutput value)
    {
      TSource source;
      if (this._sourceDictionary.TryGetValue(key, out source))
      {
        value = (TOutput) source;
        return true;
      }
      value = default (TOutput);
      return false;
    }

    [CanBeNull]
    public TOutput this[[NotNull] TKey key] => (TOutput) this._sourceDictionary[key];

    public IEnumerable<TKey> Keys => this._sourceDictionary.Keys;

    public IEnumerable<TOutput> Values => this._sourceDictionary.Values.Cast<TOutput>();
  }

  private sealed class SelectReadOnlyDictionaryAdapter<TKey, TSource, TOutput> : 
    IEnumerable<KeyValuePair<TKey, TOutput>>,
    IEnumerable,
    IReadOnlyDictionary<TKey, TOutput>,
    IReadOnlyCollection<KeyValuePair<TKey, TOutput>>
  {
    [NotNull]
    private readonly IReadOnlyDictionary<TKey, TSource> _sourceDictionary;
    [NotNull]
    private readonly Func<TSource, TOutput> _selector;

    public SelectReadOnlyDictionaryAdapter(
      [NotNull] IReadOnlyDictionary<TKey, TSource> source,
      [NotNull] Func<TSource, TOutput> selector)
    {
      this._sourceDictionary = source;
      this._selector = selector;
    }

    public IEnumerator<KeyValuePair<TKey, TOutput>> GetEnumerator()
    {
      return this._sourceDictionary.Select<KeyValuePair<TKey, TSource>, KeyValuePair<TKey, TOutput>>((Func<KeyValuePair<TKey, TSource>, KeyValuePair<TKey, TOutput>>) (keyValue => new KeyValuePair<TKey, TOutput>(keyValue.Key, this._selector(keyValue.Value)))).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this._sourceDictionary.Select<KeyValuePair<TKey, TSource>, KeyValuePair<TKey, TOutput>>((Func<KeyValuePair<TKey, TSource>, KeyValuePair<TKey, TOutput>>) (keyValue => new KeyValuePair<TKey, TOutput>(keyValue.Key, this._selector(keyValue.Value)))).GetEnumerator();
    }

    public int Count => this._sourceDictionary.Count;

    public bool ContainsKey([NotNull] TKey key) => this._sourceDictionary.ContainsKey(key);

    [ContractAnnotation("value:null => false; => value:notnull")]
    public bool TryGetValue([NotNull] TKey key, [CanBeNull] out TOutput value)
    {
      TSource source;
      if (this._sourceDictionary.TryGetValue(key, out source))
      {
        value = this._selector(source);
        return true;
      }
      value = default (TOutput);
      return false;
    }

    [CanBeNull]
    public TOutput this[[NotNull] TKey key] => this._selector(this._sourceDictionary[key]);

    public IEnumerable<TKey> Keys => this._sourceDictionary.Keys;

    public IEnumerable<TOutput> Values => this._sourceDictionary.Values.Cast<TOutput>();
  }
}
