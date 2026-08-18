// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.EnumerationOperationAdapter`1
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Extensions;

public class EnumerationOperationAdapter<T> : 
  IEnumerableWithCapacity<T>,
  IEnumerable<T>,
  IEnumerable,
  ICapacity
{
  [NotNull]
  private readonly IEnumerable<T> _enumerable;
  [NotNull]
  private readonly Func<IEnumerable<T>, IEnumerable<T>> _operation;

  public EnumerationOperationAdapter(
    [NotNull] IEnumerable<T> enumerable,
    [NotNull] Func<IEnumerable<T>, IEnumerable<T>> operation)
  {
    this._enumerable = enumerable;
    this._operation = operation;
  }

  public IEnumerator<T> GetEnumerator() => this._operation(this._enumerable).GetEnumerator();

  IEnumerator IEnumerable.GetEnumerator()
  {
    return ((IEnumerable) this._operation(this._enumerable).GetEnumerator()).GetEnumerator();
  }

  public int Capacity => this._enumerable.GetRecommendedCapacity<T>();
}
