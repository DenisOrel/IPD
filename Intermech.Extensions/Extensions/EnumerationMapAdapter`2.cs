// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.EnumerationMapAdapter`2
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

public class EnumerationMapAdapter<T, TMapped> : 
  IEnumerableWithCapacity<TMapped>,
  IEnumerable<TMapped>,
  IEnumerable,
  ICapacity
{
  [CanBeNull]
  private readonly IEnumerable<T> _enumerable;
  [NotNull]
  private readonly Func<T, TMapped> _selector;
  [CanBeNull]
  private IEnumerable<TMapped> _casted;

  [NotNull]
  private IEnumerable<TMapped> Casted
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      IEnumerable<TMapped> casted = this._casted;
      if (casted != null)
        return casted;
      IEnumerable<T> enumerable = this._enumerable;
      return this._casted = (IEnumerable<TMapped>) ((enumerable != null ? (object) enumerable.Select<T, TMapped>(this._selector) : (object) null) ?? (object) EnumerationMapAdapter<T, TMapped>.EmptyArray);
    }
  }

  [NotNull]
  private static TMapped[] EmptyArray { get; } = Array.Empty<TMapped>();

  public EnumerationMapAdapter([CanBeNull] IEnumerable<T> enumerable, [NotNull] Func<T, TMapped> selector)
  {
    this._enumerable = enumerable;
    this._selector = selector;
  }

  public IEnumerator<TMapped> GetEnumerator() => this.Casted.GetEnumerator();

  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.Casted.GetEnumerator();

  public int Capacity
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._enumerable.GetRecommendedCapacity<T>();
    }
  }
}
