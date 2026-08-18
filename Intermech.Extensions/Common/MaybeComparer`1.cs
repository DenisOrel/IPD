// Decompiled with JetBrains decompiler
// Type: Intermech.Common.MaybeComparer`1
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Common;

[Serializable]
internal class MaybeComparer<TValue> : Comparer<Maybe<TValue>> where TValue : class, IComparable<TValue>
{
  public override int Compare(Maybe<TValue> x, Maybe<TValue> y)
  {
    if (x.HasValue)
    {
      if (!y.HasValue)
        return 1;
      if ((object) x._Value == (object) y._Value)
        return 0;
      return (object) x._Value == null ? -1 : x._Value.CompareTo(y._Value);
    }
    return !y.HasValue ? 0 : -1;
  }

  [Pure]
  public override bool Equals([CanBeNull] object obj) => obj is MaybeComparer<TValue>;

  [Pure]
  public override int GetHashCode() => this.GetType().Name.GetHashCode();

  [Pure]
  public override string ToString() => this.GetType().Name;
}
