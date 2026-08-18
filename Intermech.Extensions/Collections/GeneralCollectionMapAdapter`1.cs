// Decompiled with JetBrains decompiler
// Type: Intermech.Collections.GeneralCollectionMapAdapter`1
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

internal sealed class GeneralCollectionMapAdapter<TMapped> : 
  GeneralCollectionAdapterBase,
  ICollection,
  IEnumerable,
  ICollection<TMapped>,
  IEnumerable<TMapped>,
  IReadOnlyCollection<TMapped>,
  IEquatable<ICollection>
{
  [NotNull]
  private readonly Func<object, TMapped> _selector;

  public GeneralCollectionMapAdapter([NotNull] ICollection collection, [NotNull] Func<object, TMapped> selector)
    : base(collection)
  {
    this._selector = selector;
  }

  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  IEnumerator<TMapped> IEnumerable<TMapped>.GetEnumerator()
  {
    return this.WrappedObject.GeneralSelect<TMapped>(this._selector).GetEnumerator();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void CopyTo([NotNull] TMapped[] array, int arrayIndex)
  {
    this.WrappedObject.CopyTo((Array) array, arrayIndex);
  }

  void ICollection<TMapped>.Add([CanBeNull] TMapped item)
  {
    throw new NotSupportedException($"ICollection<{typeof (TMapped)}>.Add({typeof (TMapped)} item) for {this.WrappedObject.GetType()} wrapped by {typeof (GeneralCollectionMapAdapter<TMapped>)}");
  }

  void ICollection<TMapped>.Clear()
  {
    switch (this.WrappedObject)
    {
      case IList list:
        list.Clear();
        break;
      case ICollection<TMapped> mappeds:
        mappeds.Clear();
        break;
      default:
        throw new NotSupportedException($"ICollection<{typeof (TMapped)}>.Clear() for {this.WrappedObject.GetType()} wrapped by {typeof (GeneralCollectionMapAdapter<TMapped>)}");
    }
  }

  bool ICollection<TMapped>.Contains([CanBeNull] TMapped item)
  {
    return this.WrappedObject.GeneralSelect<TMapped>(this._selector).Contains<TMapped>(item);
  }

  bool ICollection<TMapped>.Remove([CanBeNull] TMapped item)
  {
    throw new NotSupportedException($"ICollection<{typeof (TMapped)}>.Remove({typeof (TMapped)} item) for {this.WrappedObject.GetType()} wrapped by {typeof (GeneralCollectionMapAdapter<TMapped>)}");
  }

  bool ICollection<TMapped>.IsReadOnly
  {
    get
    {
      switch (this.WrappedObject)
      {
        case IList list:
          return list.IsReadOnly;
        case ICollection<TMapped> mappeds:
          return mappeds.IsReadOnly;
        default:
          return true;
      }
    }
  }
}
