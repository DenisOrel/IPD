// Decompiled with JetBrains decompiler
// Type: Intermech.Collections.ListWithMapping`2
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Collections;

[DebuggerDisplay("Count = {Count}")]
public class ListWithMapping<T, TMapped> : 
  List<T>,
  IReadOnlyList<TMapped>,
  IReadOnlyCollection<TMapped>,
  IEnumerable<TMapped>,
  IEnumerable
{
  [NotNull]
  private readonly Func<T, TMapped> _selector;

  public ListWithMapping([NotNull] Func<T, TMapped> selector, [CanBeNull] IEnumerable<T> enumeration = null, int capacity = 16 /*0x10*/)
    : base(enumeration.GetRecommendedCapacity<T>(capacity))
  {
    this._selector = selector;
    if (enumeration == null)
      return;
    this.AddRange(enumeration);
  }

  public ListWithMapping([CanBeNull] IEnumerable<T> enumeration, [NotNull] Func<T, TMapped> selector, int capacity = 16 /*0x10*/)
    : base(enumeration.GetRecommendedCapacity<T>(capacity))
  {
    this._selector = selector;
    if (enumeration == null)
      return;
    this.AddRange(enumeration);
  }

  public ListWithMapping([CanBeNull] IEnumerable<T> enumeration, int capacity, [NotNull] Func<T, TMapped> selector)
    : base(enumeration.GetRecommendedCapacity<T>(capacity))
  {
    this._selector = selector;
    if (enumeration == null)
      return;
    this.AddRange(enumeration);
  }

  public ListWithMapping([NotNull] Func<T, TMapped> selector, int capacity, [CanBeNull] IEnumerable<T> enumeration = null)
    : base(enumeration.GetRecommendedCapacity<T>(capacity))
  {
    this._selector = selector;
    if (enumeration == null)
      return;
    this.AddRange(enumeration);
  }

  public ListWithMapping(int capacity, [NotNull] Func<T, TMapped> selector, [CanBeNull] IEnumerable<T> enumeration = null)
    : base(enumeration.GetRecommendedCapacity<T>(capacity))
  {
    this._selector = selector;
    if (enumeration == null)
      return;
    this.AddRange(enumeration);
  }

  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  IEnumerator<TMapped> IEnumerable<TMapped>.GetEnumerator()
  {
    return this.Select<T, TMapped>(this._selector).GetEnumerator();
  }

  [CanBeNull]
  TMapped IReadOnlyList<TMapped>.this[int index]
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._selector(this[index]);
    }
  }
}
