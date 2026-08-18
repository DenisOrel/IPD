// Decompiled with JetBrains decompiler
// Type: Intermech.Collections.ReadOnlyListMapAdapter`2
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Collections;

internal sealed class ReadOnlyListMapAdapter<T, TMapped> : 
  ReadOnlyListAdapterBase<T>,
  IReadOnlyList<TMapped>,
  IReadOnlyCollection<TMapped>,
  IEnumerable<TMapped>,
  IEnumerable,
  IEquatable<IReadOnlyList<T>>
{
  [NotNull]
  private readonly Func<T, TMapped> _selector;

  public ReadOnlyListMapAdapter([NotNull] IReadOnlyList<T> list, [NotNull] Func<T, TMapped> selector)
    : base(list)
  {
    this._selector = selector;
  }

  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  IEnumerator<TMapped> IEnumerable<TMapped>.GetEnumerator()
  {
    return this.List.Select<T, TMapped>(this._selector).GetEnumerator();
  }

  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  IEnumerator IEnumerable.GetEnumerator()
  {
    return this.List.Select<T, TMapped>(this._selector).GetEnumerator();
  }

  [CanBeNull]
  TMapped IReadOnlyList<TMapped>.this[int index]
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._selector(this.List[index]);
    }
  }
}
