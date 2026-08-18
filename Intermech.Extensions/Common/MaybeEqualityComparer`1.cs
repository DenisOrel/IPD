// Decompiled with JetBrains decompiler
// Type: Intermech.Common.MaybeEqualityComparer`1
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Common;

internal class MaybeEqualityComparer<T> : EqualityComparer<Maybe<T>> where T : class, IEquatable<T>
{
  [Pure]
  public override bool Equals(Maybe<T> x, Maybe<T> y)
  {
    if (!x.HasValue)
      return !y.HasValue;
    return y.HasValue && this.Equals((Maybe<T>) x._Value, (Maybe<T>) y._Value);
  }

  [Pure]
  public override int GetHashCode(Maybe<T> obj) => obj.GetHashCode();

  public override bool Equals(object obj) => obj is MaybeEqualityComparer<T>;

  public override int GetHashCode() => this.GetType().Name.GetHashCode();

  [Pure]
  public override string ToString() => this.GetType().Name;
}
