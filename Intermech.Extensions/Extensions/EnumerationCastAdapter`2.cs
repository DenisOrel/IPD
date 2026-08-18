// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.EnumerationCastAdapter`2
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

public class EnumerationCastAdapter<T, TMapped> : 
  IEnumerableWithCapacity<TMapped>,
  IEnumerable<TMapped>,
  IEnumerable,
  ICapacity
  where T : TMapped
{
  [CanBeNull]
  private readonly IEnumerable<T> _enumerable;
  [CanBeNull]
  private IEnumerable<TMapped> _casted;
  [CanBeNull]
  private static TMapped[] _emptyArray;

  [NotNull]
  private IEnumerable<TMapped> Casted
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      IEnumerable<TMapped> casted = this._casted;
      if (casted != null)
        return casted;
      IEnumerable<T> enumerable = this._enumerable;
      return this._casted = (IEnumerable<TMapped>) ((enumerable != null ? (object) enumerable.Select<T, TMapped>((Func<T, TMapped>) (item => (TMapped) item)) : (object) null) ?? (object) EnumerationCastAdapter<T, TMapped>.EmptyArray);
    }
  }

  [NotNull]
  private static TMapped[] EmptyArray
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return EnumerationCastAdapter<T, TMapped>._emptyArray ?? (EnumerationCastAdapter<T, TMapped>._emptyArray = Array.Empty<TMapped>());
    }
  }

  public EnumerationCastAdapter([CanBeNull] IEnumerable<T> enumerable)
  {
    this._enumerable = enumerable;
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
