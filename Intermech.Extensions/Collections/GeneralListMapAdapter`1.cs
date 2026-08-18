// Decompiled with JetBrains decompiler
// Type: Intermech.Collections.GeneralListMapAdapter`1
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Collections;

internal sealed class GeneralListMapAdapter<TMapped> : 
  GeneralListAdapterBase,
  IList,
  ICollection,
  IEnumerable,
  IList<TMapped>,
  ICollection<TMapped>,
  IEnumerable<TMapped>,
  IReadOnlyList<TMapped>,
  IReadOnlyCollection<TMapped>,
  IEquatable<IList>
{
  [NotNull]
  private readonly Func<object, TMapped> _selector;

  public GeneralListMapAdapter([NotNull] IList list, [NotNull] Func<object, TMapped> selector)
    : base(list)
  {
    this._selector = selector;
  }

  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  IEnumerator<TMapped> IEnumerable<TMapped>.GetEnumerator()
  {
    return this.WrappedObject.GeneralSelect<TMapped>(this._selector).GetEnumerator();
  }

  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public int IndexOf([CanBeNull] TMapped item) => this.WrappedObject.IndexOf((object) item);

  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void Insert(int index, [CanBeNull] TMapped item)
  {
    this.WrappedObject.Insert(index, (object) item);
  }

  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void Remove([CanBeNull] object value) => this.WrappedObject.Remove(value);

  void IList<TMapped>.RemoveAt(int index) => this.WrappedObject.RemoveAt(index);

  [CanBeNull]
  public TMapped this[int index]
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._selector(this.List[index]);
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this.List[index] = (object) value;
    }
  }

  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void Add([CanBeNull] TMapped item) => this.WrappedObject.Add((object) item);

  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool Contains([CanBeNull] TMapped item) => this.WrappedObject.Contains((object) item);

  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void CopyTo([NotNull] TMapped[] array, int arrayIndex)
  {
    this.WrappedObject.CopyTo((Array) array, arrayIndex);
  }

  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool Remove([CanBeNull] TMapped item)
  {
    int index = this.WrappedObject.IndexOf((object) item);
    if (index < 0)
      return false;
    this.WrappedObject.RemoveAt(index);
    return true;
  }
}
