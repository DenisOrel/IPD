// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.CachedIndex`2
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.Extensions;

public class CachedIndex<TKey, TValue> : 
  Index<TKey, TValue>,
  IDisposable,
  IReadOnlyDictionary<TKey, TValue>,
  IReadOnlyCollection<KeyValuePair<TKey, TValue>>,
  IEnumerable<KeyValuePair<TKey, TValue>>,
  IEnumerable
{
  private readonly bool _disposeCachedValues;
  [CanBeNull]
  private readonly IEqualityComparer<TKey> _comparer;
  [CanBeNull]
  private System.Collections.Generic.Dictionary<TKey, TValue> _dictionary;
  [CanBeNull]
  private readonly CachedIndex<TKey, TValue>.DisposeAction _disposeAction;

  [NotNull]
  protected System.Collections.Generic.Dictionary<TKey, TValue> Dictionary
  {
    get => this._dictionary ?? (this._dictionary = new System.Collections.Generic.Dictionary<TKey, TValue>(this._comparer));
  }

  public CachedIndex(
    [CanBeNull] IEqualityComparer<TKey> comparer,
    [NotNull] Func<TKey, TValue> getter,
    bool disposeCachedValues = false)
    : base(getter)
  {
    int num = disposeCachedValues ? 1 : 0;
    this._comparer = comparer;
    this._disposeCachedValues = disposeCachedValues;
  }

  public CachedIndex(
    [NotNull] Func<TKey, TValue> getter,
    [CanBeNull] IEqualityComparer<TKey> comparer = null,
    bool disposeCachedValues = false)
    : this(comparer, getter, disposeCachedValues)
  {
  }

  public CachedIndex(
    [NotNull] Func<TKey, TValue> getter,
    [NotNull] CachedIndex<TKey, TValue>.DisposeAction disposeAction)
    : this((IEqualityComparer<TKey>) null, getter, disposeAction)
  {
  }

  public CachedIndex(
    [CanBeNull] IEqualityComparer<TKey> comparer,
    [NotNull] Func<TKey, TValue> getter,
    [NotNull] CachedIndex<TKey, TValue>.DisposeAction disposeAction)
    : this(comparer, getter)
  {
    this._disposeAction = disposeAction;
  }

  public CachedIndex([NotNull] Func<TKey, TValue> getter, bool disposeCachedValues)
    : this((IEqualityComparer<TKey>) null, getter, disposeCachedValues)
  {
  }

  public virtual void ClearCache()
  {
    if (this._disposeCachedValues && this._dictionary != null)
    {
      foreach (TValue obj in this._dictionary.Values.Distinct<TValue>())
      {
        if (obj is IDisposable disposable)
          disposable.Dispose();
      }
    }
    this._dictionary?.Clear();
  }

  public virtual void Dispose()
  {
    if (this._disposeAction != null)
    {
      this._disposeAction(this._dictionary);
    }
    else
    {
      if (!this._disposeCachedValues || this._dictionary == null)
        return;
      this.ClearCache();
    }
  }

  [CanBeNull]
  public override TValue this[[NotNull] TKey key]
  {
    get => this.Dictionary.GetOrAdd<TKey, TValue>(key, base[key]);
  }

  public bool ContainsKey([NotNull] TKey key)
  {
    System.Collections.Generic.Dictionary<TKey, TValue> dictionary = this._dictionary;
    // ISSUE: explicit non-virtual call
    return dictionary != null && __nonvirtual (dictionary.ContainsKey(key));
  }

  public bool TryGetValue([NotNull] TKey key, [CanBeNull] out TValue value)
  {
    if (this._dictionary != null)
      return this._dictionary.TryGetValue(key, out value);
    value = default (TValue);
    return false;
  }

  public IEnumerable<TKey> Keys
  {
    get => (IEnumerable<TKey>) this._dictionary?.Keys ?? (IEnumerable<TKey>) Array.Empty<TKey>();
  }

  [ItemCanBeNull]
  public IEnumerable<TValue> Values
  {
    get
    {
      return (IEnumerable<TValue>) this._dictionary?.Values ?? (IEnumerable<TValue>) Array.Empty<TValue>();
    }
  }

  public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
  {
    return (IEnumerator<KeyValuePair<TKey, TValue>>) this._dictionary?.GetEnumerator() ?? (IEnumerator<KeyValuePair<TKey, TValue>>) Array.Empty<KeyValuePair<TKey, TValue>>().GetEnumerator();
  }

  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

  public int Count
  {
    get
    {
      System.Collections.Generic.Dictionary<TKey, TValue> dictionary = this._dictionary;
      return dictionary == null ? 0 : __nonvirtual (dictionary.Count);
    }
  }

  public delegate void DisposeAction([CanBeNull] System.Collections.Generic.Dictionary<TKey, TValue> cacheDictionary);
}
