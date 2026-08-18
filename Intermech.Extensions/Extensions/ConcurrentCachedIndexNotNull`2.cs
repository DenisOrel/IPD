// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.ConcurrentCachedIndexNotNull`2
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Extensions;

public class ConcurrentCachedIndexNotNull<TKey, TValue> : 
  ConcurrentCachedIndex<TKey, TValue>,
  IDisposable
  where TValue : class
{
  public ConcurrentCachedIndexNotNull(
    [CanBeNull] IEqualityComparer<TKey> comparer,
    [NotNull] Func<TKey, TValue> getter,
    [NotNull] CachedIndex<TKey, TValue>.DisposeAction disposeAction)
    : base(comparer, getter, disposeAction)
  {
  }

  public ConcurrentCachedIndexNotNull(
    [NotNull] Func<TKey, TValue> getter,
    [NotNull] CachedIndex<TKey, TValue>.DisposeAction disposeAction)
    : base(getter, disposeAction)
  {
  }

  public ConcurrentCachedIndexNotNull([NotNull] Func<TKey, TValue> getter, bool disposeCachedValues)
    : base((IEqualityComparer<TKey>) null, getter, disposeCachedValues)
  {
  }

  public ConcurrentCachedIndexNotNull(
    [CanBeNull] IEqualityComparer<TKey> comparer,
    [NotNull] Func<TKey, TValue> getter,
    bool disposeCachedValues = false)
    : base(comparer, getter, disposeCachedValues)
  {
  }

  public ConcurrentCachedIndexNotNull(
    [NotNull] Func<TKey, TValue> getter,
    [CanBeNull] IEqualityComparer<TKey> comparer = null,
    bool disposeCachedValues = false)
    : base(comparer, getter, disposeCachedValues)
  {
  }

  [NotNull]
  public override TValue this[TKey key] => Intermech.Diagnostics.Check.Result.NotNull<TValue>(base[key]);
}
