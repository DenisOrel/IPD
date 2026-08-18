// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.CachedIndexNotNull`2
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Extensions;

public class CachedIndexNotNull<TKey, TValue> : 
  CachedIndex<TKey, TValue>,
  IDisposable,
  IReadOnlyDictionary<TKey, TValue>,
  IReadOnlyCollection<KeyValuePair<TKey, TValue>>,
  IEnumerable<KeyValuePair<TKey, TValue>>,
  IEnumerable
  where TValue : class
{
  public CachedIndexNotNull(
    [CanBeNull] IEqualityComparer<TKey> comparer,
    [NotNull] Func<TKey, TValue> getter,
    bool disposeCachedValues = false)
    : base(comparer, getter, disposeCachedValues)
  {
  }

  public CachedIndexNotNull(
    [NotNull] Func<TKey, TValue> getter,
    [CanBeNull] IEqualityComparer<TKey> comparer = null,
    bool disposeCachedValues = false)
    : base(getter, comparer, disposeCachedValues)
  {
  }

  public CachedIndexNotNull(
    [NotNull] Func<TKey, TValue> getter,
    [NotNull] CachedIndex<TKey, TValue>.DisposeAction disposeAction)
    : base(getter, disposeAction)
  {
  }

  public CachedIndexNotNull(
    [CanBeNull] IEqualityComparer<TKey> comparer,
    [NotNull] Func<TKey, TValue> getter,
    [NotNull] CachedIndex<TKey, TValue>.DisposeAction disposeAction)
    : base(comparer, getter, disposeAction)
  {
  }

  public CachedIndexNotNull([NotNull] Func<TKey, TValue> getter, bool disposeCachedValues)
    : base(getter, disposeCachedValues)
  {
  }

  [NotNull]
  public override TValue this[[NotNull] TKey key] => Intermech.Diagnostics.Check.Result.NotNull<TValue>(base[key]);

  [ItemNotNull]
  public new IEnumerable<TValue> Values => base.Values;

  [ContractAnnotation("=> true, value: notnull; => false, value: null")]
  public new bool TryGetValue([NotNull] TKey key, [CanBeNull] out TValue value)
  {
    return base.TryGetValue(key, out value);
  }
}
