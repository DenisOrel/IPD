// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.EnumerationFilterAdapter`1
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

public class EnumerationFilterAdapter<T> : 
  IEnumerableWithCapacity<T>,
  IEnumerable<T>,
  IEnumerable,
  ICapacity
{
  [CanBeNull]
  private readonly IEnumerable<T> _enumerable;
  [NotNull]
  private readonly Func<T, bool> _predicate;
  [CanBeNull]
  private static T[] _emptyArray;

  [NotNull]
  private static T[] EmptyArray
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return EnumerationFilterAdapter<T>._emptyArray ?? (EnumerationFilterAdapter<T>._emptyArray = Array.Empty<T>());
    }
  }

  public EnumerationFilterAdapter([CanBeNull] IEnumerable<T> enumerable, [NotNull] Func<T, bool> predicate)
  {
    this._enumerable = enumerable;
    this._predicate = predicate;
  }

  public IEnumerator<T> GetEnumerator()
  {
    IEnumerable<T> enumerable = this._enumerable;
    return (enumerable != null ? enumerable.Where<T>(this._predicate).GetEnumerator() : (IEnumerator<T>) null) ?? ((IEnumerable<T>) EnumerationFilterAdapter<T>.EmptyArray).GetEnumerator();
  }

  IEnumerator IEnumerable.GetEnumerator()
  {
    IEnumerable<T> enumerable = this._enumerable;
    return (enumerable != null ? (IEnumerable) enumerable.Where<T>(this._predicate) : (IEnumerable) null)?.GetEnumerator() ?? EnumerationFilterAdapter<T>.EmptyArray.GetEnumerator();
  }

  public int Capacity => this._enumerable.GetRecommendedCapacity<T>();
}
