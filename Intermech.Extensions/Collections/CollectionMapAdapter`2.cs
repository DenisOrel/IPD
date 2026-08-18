// Decompiled with JetBrains decompiler
// Type: Intermech.Collections.CollectionMapAdapter`2
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.Collections;

internal sealed class CollectionMapAdapter<T, TMapped> : 
  CollectionAdapterBase<T>,
  IReadOnlyCollection<TMapped>,
  IEnumerable<TMapped>,
  IEnumerable,
  ICapacity
{
  [NotNull]
  private readonly Func<T, TMapped> _selector;

  public CollectionMapAdapter([NotNull] ICollection<T> collection, [NotNull] Func<T, TMapped> selector)
    : base(collection)
  {
    this._selector = selector;
  }

  IEnumerator<TMapped> IEnumerable<TMapped>.GetEnumerator()
  {
    return this.Collection.Select<T, TMapped>(this._selector).GetEnumerator();
  }
}
